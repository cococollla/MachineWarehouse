namespace MachineWarehouse.Middlewares
{
    public class JwtHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Cookies["accessToken"];

            if (accessToken != null)
            {

                context.Request.Headers.Add("Authorization", "Bearer " + accessToken);
            }

            await _next.Invoke(context);
        }
    }
}
