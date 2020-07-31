using System.Data;

using MySql.Data.MySqlClient;

namespace EpidemicManager
{
    public class MySql
    {
        private readonly MySqlConnection connection;

        public MySql()
        {
            const string ConnectionString = "server=localhost;database=infectious_disease;uid=root;pwd=123456";
            connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            } catch (MySqlException)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
        }

        /// <exception cref="MySqlException" />
        public void Execute(string sql, params object[] args)
        {
            using var transaction = connection.BeginTransaction();
            using var command = new MySqlCommand(sql, connection);
            for (var i = 0; i < args.Length; i++)
            {
                command.Parameters.Add(new MySqlParameter($"@{i}", args[i]));
            }
            try
            {
                command.ExecuteNonQuery();
            } catch (MySqlException)
            {
                transaction.Rollback();
                throw;
            }
            transaction.Commit();
        }

        /// <exception cref="MySqlException" />
        public DataRowCollection Read(string sql, params object[] args)
        {
            using var command = new MySqlCommand(sql, connection);
            for (var i = 0; i < args.Length; i++)
            {
                command.Parameters.Add(new MySqlParameter($"@{i}", args[i]));
            }
            using var reader = new MySqlDataAdapter(command);
            using var table = new DataTable();
            reader.Fill(table);
            return table.Rows;
        }

        ~MySql()
        {
            connection?.Close();
            connection?.Dispose();
        }
    }
}
