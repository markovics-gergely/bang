namespace UserIdentity.BLL.Application.Commands.User.DataTransferObject
{
    public class RegistrationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
