using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces;
using simpleRestApi.Models;

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

        public byte[] GetPasswordHash(string password, byte[] salt)
        {

                string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey")
                .Value + Convert.ToBase64String(salt);

                byte[] passwordHash = KeyDerivation.Pbkdf2(password: password, 
                salt: Encoding.ASCII.GetBytes(passwordSaltPlusString), 
                prf: KeyDerivationPrf.HMACSHA256, 
                iterationCount: 100000, numBytesRequested: 256/8);

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
           return  _authRepository.GetAuthUser(email);
        }

        public bool SaveHashedUser(string email, byte[] passwordHash, byte[] passwordSalt)
        {
               return _authRepository.SaveHashedUser(email, passwordHash, passwordSalt);
        }
    }
}