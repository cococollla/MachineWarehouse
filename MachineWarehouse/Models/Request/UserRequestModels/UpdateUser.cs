namespace MachineWarehouse.Models.Request.UserRequestModels
{
    public class UpdateUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
    }
}
