using MachineWarehouse.Middlewares;
using MachineWarehouse.Repository;
using MachineWarehouse.Services.CarServices;
using MachineWarehouse.Services.Contracts;
using MachineWarehouse.Services.Implementations;
using MachineWarehouse.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
var jwtSettings = builder.Configuration.GetSection("JWT");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICarServices, CarServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = TokenService.GetSymmetricSecurityKey(),

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                }

                return Task.CompletedTask;
            }
        };

    });






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseMiddleware<JwtHeaderMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseCors(MyAllowSpecificOrigins);
app.Run();
