using Npgsql;
using System.Data;

public class DatabaseAccess
{
    private readonly string connectionString;

    public DatabaseAccess()
    {
        connectionString = "Host=localhost; Port=5432; Database=characterDB; Username=postgres; Password=1234;";
    }

    public DataTable ExecuteQuery(string query, NpgsqlParameter[] parameters)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                using (var reader = cmd.ExecuteReader())
                {
                    var dataTable = new DataTable();
                    dataTable.Load(reader);
                    return dataTable;
                }
            }
        }
    }

    public void ExecuteNonQuery(string query, NpgsqlParameter[] parameters)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                cmd.ExecuteNonQuery();
            }
        }
    }
}
