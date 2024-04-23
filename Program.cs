using microservice_characterManagement;


Communicator communicator = new Communicator();


var mockEmail = "casper@example.com";

/*
DatabaseCalls.FetchCharacters(mockEmail);

var characters = Character.AllCharacters;

var dataToSend = new
{
    Id = characters[0].Id,
    Name = characters[0].Name,
    ClassType = characters[0].ClassType,
    Level = characters[0].Level,
};

var result = await communicator.PostDataAsync("https://localhost:7124/api/data", dataToSend);


*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Apply CORS policy

// Add routes and controllers if you have any
app.MapControllers(); // Ensure you have controllers added to your project

app.Run();





