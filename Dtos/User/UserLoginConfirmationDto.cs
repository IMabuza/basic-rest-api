namespace  simpleRestApi.Dtos.User
{
    public partial class UserLoginConfirmationDto{
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public UserLoginConfirmationDto(){
            PasswordHash ??= Array.Empty<byte>();
            PasswordSalt ??= Array.Empty<byte>();
        }
    }
}