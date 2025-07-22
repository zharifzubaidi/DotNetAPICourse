// Controller for handling API routing and responses
// All routes in this controller will be prefixed with the controller name

// File name <controller name><Controller>.cs
// Example: WeatherForecastController.cs

using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers;

// Basic controller example for handling weather forecast data
// This controller provides an endpoint to get a five-day weather forecast

[ApiController]         // Api controller tag for Json file
[Route("[controller]")] // Route for the controller
public class WeatherForecastController : ControllerBase
{
    // Member variable to hold summaries
    private readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    // Build a method to get the weather forecast
    [HttpGet("", Name = "GetWeatherForecast")]      // API HTTP GET method with a specific route. End point
    public IEnumerable<WeatherForecast> GetFiveDayForecast()
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            _summaries[Random.Shared.Next(_summaries.Length)]
        ))
        .ToArray();
        return forecast;
    }
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
