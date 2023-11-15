namespace MachineWarehouse.Models.Request.UserRequestModels
{
    public class CreateUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
