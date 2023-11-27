using MachineWarehouse.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace MachineWarehouse.Middlewares
{
    public class JwtHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public JwtHandlerMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if (context.Response.Headers.ContainsKey("IS-TOKEN-EXPIRED"))
            {
                var headerValue = context.Response.Headers["IS-TOKEN-EXPIRED"];
                if (headerValue == "true")
                {
                    context.Request.Headers.Remove("Authorization");
                    string? refreshToken = context.Request.Cookies["refreshToken"];
                    string? role = context.Request.Cookies["role"];
                    //Если refresh token истек логинимся заново
                    if (refreshToken == null)
                    {
                        context.Response.Redirect("api/Account/Authenticate");
                    }

                    var acceessToken = _tokenService.CreateToken(role);
                    context.Response.Cookies.Append("acceessToken", acceessToken);
                    context.Request.Headers.Add("Authorization", "Bearer " + acceessToken);
                }
            }
        }
    }
}
