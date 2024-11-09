namespace simpleRestApi.Dtos.User{
    public partial class UserDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public UserDto(){
        Name ??= "";
        Surname ??= "";
        Email ??= "";
    }
}
}