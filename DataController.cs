using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        var dataToSend = new
        {
            Id = characters[0].Id,
            Name = characters[0].Name,
            ClassType = characters[0].ClassType.ToString(),
            Level = characters[0].Level
        };

        return Ok(dataToSend);
    }
}
