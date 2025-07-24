
using DotNetAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();              
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------CORS setup for enviroment------------------//
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

// Add addscoped connection betweeen user repository class and interface
builder.Services.AddScoped<IUserRepository, UserRepository>();    
 
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

app.MapControllers();                          

app.Run();                                      

