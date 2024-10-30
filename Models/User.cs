namespace simpleRestApi{
    public partial class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public User(){
        Name ??= "";
        Surname ??= "";
        Email ??= "";
    }
}
}