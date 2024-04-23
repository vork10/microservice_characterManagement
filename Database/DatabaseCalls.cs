using Npgsql;
using System.Data;

public static class DatabaseCalls
{
    public static void FetchCharacters(string email)
    {
        int id;
        string receivedEmail;
        string name;
        string classType;
        int level;

        var database = new DatabaseAccess();

        // Create parameter
        var parameters = new NpgsqlParameter[]
        {
            new NpgsqlParameter("@email", email)
        };

        // Execute the query with the parameter
        var dataTable = database.ExecuteQuery("SELECT * FROM characters WHERE email = @email;", parameters);

        if (dataTable.Rows.Count > 0)
        {
            System.Diagnostics.Debug.WriteLine("Email found");

            foreach (DataRow row in dataTable.Rows)
            {
                id = Convert.ToInt32(row["id"]);
                receivedEmail = row["email"].ToString();
                name = row["name"].ToString();
                classType = row["classType"].ToString();
                level = Convert.ToInt32(row["level"]);

                // Convert classType from string to CharacterClassType enum
                CharacterClassType enumClassType;
                if (Enum.TryParse(classType, true, out enumClassType))
                {
                    // Create the character using the constructor
                    Character newCharacter = new Character(id, name, enumClassType, level);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Invalid class type");
                }
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Email not found");
            CreateNewUser(email);
        }
    }

    public static void CreateNewUser(string email)
    {
        var parameters = new NpgsqlParameter[]
        {
            new NpgsqlParameter("@email", email)
        };

        var database = new DatabaseAccess();

        database.ExecuteNonQuery("INSERT INTO characters (email, name, classType, level) VALUES (@email, null, null, null);", parameters);
    }  
}
