using Microsoft.AspNetCore.Mvc;
using simpleRestApi.Data;
using simpleRestApi.Models;

namespace simpleRestApi.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    DataContextEF _ef;
    public UserController(IConfiguration config){
        _ef = new DataContextEF(config);
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        return _ef.Users.ToList<User>();
    }

      [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user =  _ef.Users.Where(u => u.Id == userId).FirstOrDefault<User>();

        if(user != null){
            return user;
        }

        throw new Exception("Failed to get user");
    }

      [HttpPost("AddUser")]
    public bool AddUser(UserDto user)
    {
        User newUser = new User();
        
        newUser.Name = user.Name;
        newUser.Email = user.Email;
        newUser.Surname = user.Surname;

        _ef.Add(newUser);
        if(_ef.SaveChanges() >  0){
            return true;
        }
        return false;
    }
}
