using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using simpleRestApi.Dtos.User;
using simpleRestApi.Interfaces;
using simpleRestApi.Models;

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

            byte[] passwordSalt = new byte[128 / 8];
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetNonZeroBytes(passwordSalt);
            }

            byte[] hash = _authService.GetPasswordHash(userRegistrationDto.Password, passwordSalt);

            string? token = _authService.SaveHashedUser(hash, passwordSalt, userRegistrationDto);
            if (token == null)
            {
                throw new Exception("Failed to register user");
            };

            return Ok(new Dictionary<string, string> { { "token", token } });


        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            //Get user
            Auth? authUser = _authService.GetAuthUser(userLoginDto.Email);
            if (authUser == null)
            {
                throw new Exception("User does not exist");
            }

            byte[] passwordHash = _authService.GetPasswordHash(userLoginDto.Password, authUser.PasswordSalt);

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != authUser.PasswordHash[i])
                {
                    return StatusCode(401, "Incorrect password");
                }
            }

            string? token = _authService.GetUserToken(userLoginDto.Email);
            if (token == null)
            {
                throw new Exception("Failed to login user");
            };
            return Ok(new Dictionary<string, string> { { "token", token } });
        }
    }
}