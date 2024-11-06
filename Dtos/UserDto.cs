namespace simpleRestApi{
    public partial class UserDto
{
    public long Id { get; set; }
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