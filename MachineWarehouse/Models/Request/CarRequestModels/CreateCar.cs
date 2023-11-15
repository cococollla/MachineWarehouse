namespace MachineWarehouse.Models.Request.CarRequestModels
{
    public class CreateCar
    {
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public string YearRelese { get; set; }
        public double Price { get; set; }
        public string? ShorDescription { get; set; }
    }
}
