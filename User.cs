public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; } // Note: Storing passwords as plain text is not secure.
}