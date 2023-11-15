namespace MachineWarehouse.Models.Request.CarVm
{
    public class GetCarVm
    {
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string YearRelese { get; set; }
        public double Price { get; set; }
        public string? ShorDescription { get; set; }
    }
}
