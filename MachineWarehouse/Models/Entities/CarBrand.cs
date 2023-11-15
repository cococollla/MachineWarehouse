namespace MachineWarehouse.Models.Entities
{
    public class CarBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public List<Car> Cars { get; set; }
    }
}
