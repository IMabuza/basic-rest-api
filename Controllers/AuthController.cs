using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces;

namespace simpleRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        // IMapper _mapper;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
            //        _mapper = new Mapper(new MapperConfiguration(cfg => {
            //     cfg.CreateMap<UserRegistrationDto, Auth>();
            // }));
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegistrationDto userRegistrationDto)
        {
            if (!_authService.MatchPasswords(userRegistrationDto.Password, userRegistrationDto.ConfirmPassword))
            {
                throw new Exception("Passwords do not match");
            };

            if (_authService.UserExists(userRegistrationDto.Email))
            {
                throw new Exception("User with this email already exists. Please login");
            }

            if (!_authService.HashPassword(userRegistrationDto))
            {
                throw new Exception("Failed to register user");
            };

            return Ok();


        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginConfirmationDto userLoginConfirmationDto)
        {
            return Ok();
        }
    }
}