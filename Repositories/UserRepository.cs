using simpleRestApi.Data;
using simpleRestApi.Interfaces;
using simpleRestApi.Models;

namespace simpleRestApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        DataContextEF _ef;
        public UserRepository(IConfiguration config)
        {
            _ef = new DataContextEF(config);

        }

        public bool Save()
        {
            return _ef.SaveChanges() > 0;
        }

        public void Add<UserDto>(UserDto user)
        {
            if (user != null)
            {
                _ef.Add(user);
            }

        }

        public void Remove<UserDto>(UserDto user)
        {
            if (user != null)
            {
                _ef.Remove(user);
            }

        }

        public IEnumerable<User> GetUsers()
        {
            return _ef.Users.ToList<User>();
        }

        public User? GetUser(int userId){

             return _ef.Users.Where(u => u.Id == userId).FirstOrDefault<User>() ?? null;
        }

        public User? GetUserByEmail(string email)
        {
             return _ef.Users.Where(u => u.Email == email).FirstOrDefault<User>() ?? null;
        }
    }
}