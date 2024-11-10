using simpleRestApi.Models;

namespace simpleRestApi.Interfaces
{
    public interface IAuthRepository
    {
        public bool SaveHashedUser(string email, byte[] passwordHash, byte[] passwordSalt);
        public bool UserExists(string email);
        public bool Save();
        public Auth? GetAuthUser(string email);
    }
}