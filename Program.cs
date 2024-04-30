using microservice_characterManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks(); // Register health checks

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

// Register controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthChecks("/health"); // Map the health checks endpoint

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Apply CORS policy

// Add routes and controllers if you have any
app.MapControllers(); // Ensure you have controllers added to your project

app.Run();





