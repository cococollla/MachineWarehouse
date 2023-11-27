namespace MachineWarehouse.Models.Identuty
{
    public class AuthResponse
    {
        public string Role { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken {  get; set; }
    }
}
