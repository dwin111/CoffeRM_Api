using CoffeCRMBeck.Model.Enums;

namespace CoffeCRMBeck.Model.ViewModel
{
    public class WorkerViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public Role Roles { get; set; }
    }
}
