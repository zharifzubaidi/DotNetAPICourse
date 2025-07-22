

// ------------------Basic setup for a .NET Web API application------------------//
// Build the builder to create the application that will run the server
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();              // Add controllers to the service container
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

app.MapControllers();                           // Map the controllers to the application

app.Run();                                      // Will start the server application in the console

