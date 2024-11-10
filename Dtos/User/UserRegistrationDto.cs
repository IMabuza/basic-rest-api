namespace simpleRestApi.Dtos.User
{
    public partial class UserRegistrationDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public UserRegistrationDto()
        {
            Email ??= "";
            Password ??= "";
            ConfirmPassword ??= "";
            Name ??= "";
            Surname ??= "";
        }
    }
}