namespace simpleRestApi.Models
{
    public partial class User
{
    public int Id { get; set; }
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