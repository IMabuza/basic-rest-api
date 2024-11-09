namespace simpleRestApi.Models{
    public class Auth{
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }

         public byte[] PasswordSalt { get; set; }

         public Auth(){
            Email ??= "";
            PasswordHash = Array.Empty<byte>();
            PasswordSalt = Array.Empty<byte>();
         }
    }
}