namespace MachineWarehouse.Models.Entities
{
    public class CarColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public List<Car> Cars { get; set; }
    }
}
