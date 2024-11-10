using simpleRestApi.Dtos.User;

namespace simpleRestApi.Interfaces
{
    public interface IAuthService{
        public bool MatchPasswords(string password, string confirmPassword);
        public bool UserExists(string email);

        public bool HashPassword(UserRegistrationDto userRegistrationDto);
    }
}