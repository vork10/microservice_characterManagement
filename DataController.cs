using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

[ApiController]
[Route("api/data")]
public class DataController : ControllerBase
{
    [HttpPost]
    public IActionResult Post([FromBody] JsonElement data)
    {
        Console.WriteLine("Received data: " + data.ToString());
        return Ok("Data received successfully!");
    }

    [HttpGet("{id}")]
    public IActionResult GetData(string id)
    {
        DatabaseCalls.FetchCharacters(id);

        var characters = Character.AllCharacters;

        // Create JsonSerializerOptions and add JsonStringEnumConverter
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        List<string> result = new List<string>();
        foreach (var character in characters)
        {
            var json = JsonSerializer.Serialize(character, options);
            result.Add(json);
        }

        characters.Clear(); // Clear characters if necessary

        return Ok(result);
    }
}
