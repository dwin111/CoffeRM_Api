using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
