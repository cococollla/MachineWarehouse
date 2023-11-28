namespace MachineWarehouse.Services.Contracts
{
    public interface ITokenService
    {
        public string CreateToken(string role);
        public string CreateRefreshToken();
    }
}
