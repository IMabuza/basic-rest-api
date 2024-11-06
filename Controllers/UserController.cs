using Microsoft.AspNetCore.Mvc;
using simpleRestApi.Data;
using simpleRestApi.Models;

namespace simpleRestApi.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    DataContextDapper _dapper;
    public UserController(IConfiguration config){
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        return _dapper.LoadData<User>("SELECT * FROM dbo.Users");
    }

      [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        return _dapper.LoadDataSingle<User>($"SELECT * FROM dbo.Users WHERE Id = {userId}");
    }

      [HttpPost("AddUser")]
    public bool AddUser(UserDto user)
    {
        return _dapper.Execute($"INSERT INTO dbo.Users([Name],[Surname],[Email]) VALUES('{user.Name}','{user.Surname}','{user.Email}')");
    }
}
