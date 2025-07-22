
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();              
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ------------------Basic setup for enviroment------------------//
/*
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();                           // Enable Swagger UI in development mode
    app.UseSwaggerUI();                         // Use Swagger UI to visualize and interact with the API
}
else
{
    app.UseHttpsRedirection();
}
*/
// ------------------Basic setup for enviroment------------------//

// ------------------CORS setup for enviroment------------------//
// This code sets up CORS policies for connecting to frontend applications
// It allows requests from specific origins, methods, and headers, and supports credentials
builder.Services.AddCors((options) =>
    {
        // Development to allow requests from local frontend applications
        // Angular - port 4200, React - port 3000, Vue - port 8000, Blazor - port 5000
        options.AddPolicy("DevCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000", "http://localhost:5000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        // Production to allow requests from the production frontend application with https that has certificate
        options.AddPolicy("ProdCors", (corsBuilder) =>
            {
                corsBuilder.WithOrigins("https://myProductionSite.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
    });
 
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
// ------------------CORS setup for enviroment------------------//


app.MapControllers();                          

app.Run();                                      

