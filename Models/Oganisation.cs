namespace simpleRestApi.Models{
    public partial class Organisation
{
     public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public string StreetNumber { get; set; }
    public string StreetName { get; set; }
    public string Suburb { get; set; }
    public string PostalCode { get; set; }

    public Organisation(){
        Name ??= "";
        StreetName ??= "";
        StreetNumber ??= "";
        Suburb ??= "";
        PostalCode ??= "";
    }
}
}