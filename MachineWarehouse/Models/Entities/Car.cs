namespace MachineWarehouse.Models.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string YearRelese { get; set; }
        public double Price { get; set; }
        public string? ShorDescription { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
