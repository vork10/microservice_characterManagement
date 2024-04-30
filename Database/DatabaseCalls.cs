using Npgsql;
using System.Data;

public static class DatabaseCalls
{
    public static void FetchCharacters(string accountID)
    {
        int id;
        string receivedAccountID;
        string name;
        string classType;
        int level;

        var database = new DatabaseAccess();

        // Create parameter
        var parameters = new NpgsqlParameter[]
        {
            new NpgsqlParameter("@accountID", accountID)
        };

        // Execute the query with the parameter
        var dataTable = database.ExecuteQuery("SELECT * FROM characters WHERE accountID = @accountID;", parameters);

        if (dataTable.Rows.Count > 0)
        {
            System.Diagnostics.Debug.WriteLine("Email found");

            foreach (DataRow row in dataTable.Rows)
            {
                id = Convert.ToInt32(row["id"]);
                receivedAccountID = row["accountID"].ToString();
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
        }
    }

    public static void CreateCharacter(string accountID, string name, string classtype, int level)
    {
        var parameters = new NpgsqlParameter[]
        {
        new NpgsqlParameter("@accountID", accountID),
        new NpgsqlParameter("@name", name),
        new NpgsqlParameter("@classtype", classtype),
        new NpgsqlParameter("@level", level)
        };

        var database = new DatabaseAccess();

        string sql = "INSERT INTO characters (accountID, name, classType, level) VALUES (@accountID, @name, @classtype, @level);";
        database.ExecuteNonQuery(sql, parameters);
    }

}
