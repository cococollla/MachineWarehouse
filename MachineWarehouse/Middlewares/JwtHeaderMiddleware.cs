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
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accessToken = context.Request.Cookies["accessToken"];
            if (token != null)
            {
                //context.Request.Headers.Add("Authorization", "Bearer " + token);
                
            }
                
            await _next.Invoke(context);
        }
    }
}
