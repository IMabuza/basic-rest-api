using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces;
using simpleRestApi.Models;

namespace simpleRestApi.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{

    IUserRepository _userRepository;
    IMapper _mapper;
    public UserController(IUserRepository userRepository){

        _userRepository = userRepository;
        _mapper = new Mapper(new MapperConfiguration(cfg => {
            cfg.CreateMap<UserDto, User>();
        }));
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        return _userRepository.GetUsers();
    }

      [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user = _userRepository.GetUser(userId);

        if(user != null){
            return user;
        }

        throw new Exception("Failed to get user");
    }

      [HttpPost("AddUser")]
    public bool AddUser(UserDto userDto)
    {
        User newUser = _mapper.Map<User>(userDto);
        _userRepository.Add(newUser);
        if(_userRepository.Save()){
            return true;
        }
        return false;
    }
}
