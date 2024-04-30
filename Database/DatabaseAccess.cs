using Microsoft.AspNetCore.Hosting.Server;
using Npgsql;
using System.Data;

public class DatabaseAccess
{
    private readonly string connectionString;

    public DatabaseAccess()
    {
        connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is not initialized.");
        }
    }

    public DataTable ExecuteQuery(string query, NpgsqlParameter[] parameters)
    {
        try
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
        catch (Exception ex)
        {
            throw;
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
