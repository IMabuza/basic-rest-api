namespace  simpleRestApi.Dtos.User
{
    public partial class UserLoginDto{
        public string Email { get; set; }
public string Password { get; set; }

        public UserLoginDto(){
            Email ??= "";
            Password ??= "";
        }
    }
}