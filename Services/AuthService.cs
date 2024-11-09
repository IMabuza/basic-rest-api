using simpleRestApi.Interfaces.Auth;
using simpleRestApi.Repositories;

namespace simpleRestApi.Services
{
    public class AuthService : IAuthService
    {
        AuthRepository _authRepository;
        public AuthService(AuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public bool MatchPasswords(string password, string confirmPassword)
        {
            return !string.IsNullOrEmpty(password) && password == confirmPassword;
        }
    }
}