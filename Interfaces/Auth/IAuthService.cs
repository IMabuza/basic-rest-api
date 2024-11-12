using simpleRestApi.Dtos.User;
using simpleRestApi.Models;

namespace simpleRestApi.Interfaces
{
    public interface IAuthService{
        public bool MatchPasswords(string password, string confirmPassword);
        public bool UserExists(string email);

        public byte[] GetPasswordHash(string password, byte[] salt);

        public Auth? GetAuthUser(string email);

        public string? SaveHashedUser(byte[] passwordHash, byte[] passwordSalt, UserRegistrationDto userRegistrationDto);

        public string? GetUserToken(string email);

        public string CreateToken(int userId);

          public User? GetUser(int userId);
    }
}