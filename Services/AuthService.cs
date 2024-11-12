using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces;
using simpleRestApi.Models;

namespace simpleRestApi.Services
{
    public class AuthService : IAuthService
    {
        IAuthRepository _authRepository;
        IUserRepository _userRepository;

        IMapper _mapper;
        private readonly IConfiguration _config;
        public AuthService(IAuthRepository authRepository, IConfiguration config, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _config = config;

            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegistrationDto, User>();
            }));
        }

        public byte[] GetPasswordHash(string password, byte[] salt)
        {

            string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey")
            .Value + Convert.ToBase64String(salt);

            byte[] passwordHash = KeyDerivation.Pbkdf2(password: password,
            salt: Encoding.ASCII.GetBytes(passwordSaltPlusString),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000, numBytesRequested: 256 / 8);

            return passwordHash;

        }

        public bool MatchPasswords(string password, string confirmPassword)
        {
            return !string.IsNullOrEmpty(password) && password == confirmPassword;
        }

        public bool UserExists(string email)
        {
            return _authRepository.UserExists(email);
        }

        public Auth? GetAuthUser(string email)
        {
            return _authRepository.GetAuthUser(email);
        }

        public string? SaveHashedUser(byte[] passwordHash, byte[] passwordSalt, UserRegistrationDto userRegistrationDto)
        {
            if (_authRepository.SaveHashedUser(userRegistrationDto.Email, passwordHash, passwordSalt))
            {
                User newUser = _mapper.Map<User>(userRegistrationDto);
                _userRepository.Add(newUser);
                if (_userRepository.Save())
                {
                    User? user = _userRepository.GetUserByEmail(userRegistrationDto.Email);
                    if (user != null)
                    {
                        return this.CreateToken(user.Id);
                    }
                }
            };
            return null;
        }

        public string CreateToken(int userId)
        {
            Claim[] claims = new Claim[] {
                new Claim("userId", userId.ToString()),
            };

            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenKeyString ?? ""));

            SigningCredentials credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(1),
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(descriptor);

            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public string? GetUserToken(string email)
        {
            User? user = _userRepository.GetUserByEmail(email);
            if (user != null)
            {
                return this.CreateToken(user.Id);
            }
            return null;
        }

        public User? GetUser(int userId)
        {
            return _userRepository.GetUser(userId);
        }
    }
}