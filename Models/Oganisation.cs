namespace simpleRestApi{
    public partial class Organisation
{
    public long UserId { get; set; }
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