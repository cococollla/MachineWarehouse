namespace MachineWarehouse.Models.View
{
    public class UserVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login {  get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
    }
}
