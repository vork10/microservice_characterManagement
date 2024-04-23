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

    [HttpGet]
    public IActionResult GetData(JsonElement data)
    {
        // Your method logic here
        return Ok("This is a test data response." + data);
    }
}
