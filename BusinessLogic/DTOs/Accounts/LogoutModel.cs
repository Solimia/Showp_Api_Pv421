using System.ComponentModel;

namespace BusinessLogic.DTOs.Accounts
{
    public class LogoutModel
    {
        [DefaultValue("rere@gmail.com")]
        public string Email { get; set; } 
        [DefaultValue("Rere@123")]
        public string Password { get; set; } 

    }
}
