using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces;

namespace simpleRestApi.Services
{
    public class AuthService : IAuthService
    {
        IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthService(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        public bool HashPassword(UserRegistrationDto userRegistrationDto)
        {
            byte[] passwordSalt = new byte[128/8];
                using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create()){
                    randomNumberGenerator.GetNonZeroBytes(passwordSalt);
                }

                string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey")
                .Value + Convert.ToBase64String(passwordSalt);

                byte[] passwordHash = KeyDerivation.Pbkdf2(password: userRegistrationDto.Password, 
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString), 
                prf: KeyDerivationPrf.HMACSHA256, 
                iterationCount: 100000, numBytesRequested: 256/8);

                return _authRepository.SaveHashedUser(userRegistrationDto.Email, passwordHash, passwordSalt);
                
        }

        public bool MatchPasswords(string password, string confirmPassword)
        {
            return !string.IsNullOrEmpty(password) && password == confirmPassword;
        }

        public bool UserExists(string email)
        {
            return _authRepository.UserExists(email);
        }

    }
}