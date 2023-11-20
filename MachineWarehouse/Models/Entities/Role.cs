using Microsoft.AspNetCore.Identity;

namespace MachineWarehouse.Models.Entities
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
