
using System.Text;
using DotNetAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();              
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------CORS setup for enviroment------------------//
#region CORS Policy
builder.Services.AddCors((options) =>
    {
        options.AddPolicy("DevCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000", "http://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        options.AddPolicy("ProdCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("https://myProductionSite.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });
#endregion

// Add addscoped connection betweeen user repository class and interface
builder.Services.AddScoped<IUserRepository, UserRepository>();

#region JWT Token Authentication setup
// JWT Token Validation setup to use with Postman
// Get the token key from appsettings.json
string? tokenKeyString = builder.Configuration.GetSection("AppSettings:TokenKey").Value;
SymmetricSecurityKey tokenKey = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes(
        tokenKeyString != null ? tokenKeyString : ""
    )
);

// Token validation parameters
TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
{
    IssuerSigningKey = tokenKey,
    ValidateIssuer = false,
    ValidateIssuerSigningKey = false,
    ValidateAudience = false,
};

// Add authentication services to the service collection
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();                          

app.Run();                                      

