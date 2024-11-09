using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces.Auth;

namespace simpleRestApi.Controllers{
    public class AuthController : ControllerBase{
        IAuthService _authService;
        // IMapper _mapper;

         public AuthController(IAuthService authService){
              _authService = authService;
        //        _mapper = new Mapper(new MapperConfiguration(cfg => {
        //     cfg.CreateMap<UserRegistrationDto, Auth>();
        // }));
         }

         [HttpPost("Register")]
         public IActionResult Register(UserRegistrationDto userRegistrationDto){
             if(_authService.MatchPasswords(userRegistrationDto.Password, userRegistrationDto.ConfirmPassword)){
                return Ok();
             };
             throw new Exception("Passwords do not match");

         }

         [HttpPost("Login")]
         public IActionResult Login(UserLoginConfirmationDto userLoginConfirmationDto ){
            return Ok();
         }
    }
}