using simpleRestApi.Models;

namespace simpleRestApi.Interfaces{
    public interface IUserRepository{
         public bool Save();
         public void Add<UserDto>(UserDto user);
         public void Remove<UserDto>(UserDto user);
          public IEnumerable<User> GetUsers();

          public User? GetUser(int userId);

          public User? GetUserByEmail(string email);
         
    }
}