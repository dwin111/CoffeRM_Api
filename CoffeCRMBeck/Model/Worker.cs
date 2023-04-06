using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; }
    }
}
