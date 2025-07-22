

// ------------------Basic setup for a .NET Web API application------------------//
// Build the builder to create the application that will run the server
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();     // To look what endpoints are available
builder.Services.AddSwaggerGen();               // To generate Swagger documentation for the API

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
