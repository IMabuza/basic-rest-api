namespace simpleRestApi.Interfaces.Auth
{
    public interface IAuthService{
        public bool MatchPasswords(string password, string confirmPassword);
    }
}