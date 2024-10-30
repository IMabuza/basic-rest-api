using Microsoft.AspNetCore.Mvc;

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
    public string[] GetUsers()
    {
        return new[] { "test", "test2" };
    }
}
