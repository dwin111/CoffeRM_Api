using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CoffeCRMBeck.Model
{
    public class Login
    {
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string? Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [PasswordPropertyText]
        public string? Password { get; set; }
    }
}
