using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shubham_Bhawtu.Authentication.Filters;
using Shubham_Bhawtu.Authentication.Repositories;
using Shubham_Bhawtu.Authentication.Repositories.Interface;
using Shubham_Bhawtu.Authentication.Services;
using Shubham_Bhawtu.Authentication.Services.Interface;
using Shubham_Bhawtu.Authentication.Utilities;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
string key = "rlZyCC9cVOlErTlqErzuaVh3jV8t041H";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

#region JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});
#endregion
builder.Services.AddSingleton<IJwtAuthentcationManager>(new JwtAuthentcationManager(key));
#region DI Register
builder.Services.AddSingleton<DBUtilities>();
builder.Services.AddScoped<IDBUtilities, DBUtilities>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
