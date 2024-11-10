using simpleRestApi.Data;
using simpleRestApi.Interfaces;
using simpleRestApi.Models;

namespace simpleRestApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        DataContextEF _ef;
        public AuthRepository(IConfiguration config)
        {
            _ef = new DataContextEF(config);

        }

        public bool Save()
        {
            return _ef.SaveChanges() > 0;
        }

        public bool SaveHashedUser(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Auth auth= new Auth();
            auth.Email = email;
            auth.PasswordHash = passwordHash;
            auth.PasswordSalt=passwordSalt;

            _ef.Auth.Add(auth);
            return this.Save();
        }

        public bool UserExists(string email)
        {
            Auth? authUser = _ef.Auth.Where(au => au.Email == email).FirstOrDefault<Auth>();
            return authUser != null;
        }
    }
}