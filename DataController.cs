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
        string accountId = data.GetProperty("id").GetString();
        string name = data.GetProperty("name").GetString();
        string classtype = data.GetProperty("classtype").GetString();
        int level = data.GetProperty("level").GetInt32();

        DatabaseCalls.CreateCharacter(accountId, name, classtype, level);

        return Ok("Data received and processed successfully!");
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
