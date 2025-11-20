using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace AppManage.AppCode.DAL.System
{
    public class DalBase : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public string ConnectionString => _connection.ConnectionString;

        public ConnectionState ConnectionState => _connection.State;

        public DalBase(IConfiguration configuration)
        {
            var server = configuration["ProjectSettings:AppS"];
            var user = configuration["ProjectSettings:AppL"];
            var password = configuration["ProjectSettings:AppP"];
            var database = configuration["ProjectSettings:AppDB"];

            // Construct the connection string
            var connectionString = $"Server={server};Database={database};User Id={user};Password={password};TrustServerCertificate=True;";
            _connection = new SqlConnection(connectionString);
        }

        public void BeginTran()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }

        public void CommitTran()
        {
            _transaction?.Commit();
        }

        public void RollBackTran()
        {
            _transaction?.Rollback();
        }

        //public void ChangeConnection(SqlConnection connection)
        //{
        //    _connection = connection;
        //}

        public SqlConnection GetConnection() => _connection;

        private int CommandTimeOut()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            return builder.ConnectTimeout;
        }

        public DataSet GetResults(SqlCommand command)
        {
            var ds = new DataSet();
            var da = new SqlDataAdapter(command);
            da.Fill(ds); // Async fill is not available, so we use Task.Run
            return ds;
        }

        public DataTable GetResult(SqlCommand command)
        {
            var dt = new DataTable();
            var da = new SqlDataAdapter(command);
            da.Fill(dt); // Async fill is not available, so we use Task.Run
            return dt;
        }

        public async Task<T> GetScalarAsync<T>(SqlCommand cmd)
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            var result = await cmd.ExecuteScalarAsync();
            TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
            Type type = Nullable.GetUnderlyingType(typeof(T));
            if (type != null && result == null)
            {
                return default(T);
            }
            return (T)conv.ConvertFrom(result.ToString());
        }

        public async Task<int> ExecuteNonQueryAsync(SqlCommand cmd)
        {
            if (_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            return await cmd.ExecuteNonQueryAsync();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }

        // Command creation methods
        public SqlCommand NewCommand(string commandText, CommandType commandType = CommandType.StoredProcedure)
        {
            var command = new SqlCommand
            {
                CommandText = commandText,
                Connection = _connection,
                CommandType = commandType,
                CommandTimeout = CommandTimeOut()
            };
            return command;
        }
        public SqlCommand InlineQueryCommand(string commandText)
        {
            var command = new SqlCommand
            {
                CommandText = commandText,
                Connection = _connection,
                CommandType = CommandType.Text, // Use CommandType.Text for inline queries
                CommandTimeout = CommandTimeOut()
            };
            return command;
        }

        public SqlCommand NewCommand(string commandText, SqlTransaction transaction, CommandType commandType = CommandType.StoredProcedure)
        {
            var command = NewCommand(commandText, commandType);
            command.Transaction = transaction;
            return command;
        }
    }
}
