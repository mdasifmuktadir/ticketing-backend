using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Eapproval.Factories.IFactories;


namespace Eapproval.Factories;

public class Connection: IConnection
{
    private readonly string _connectionString;

    public Connection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqlConnection GetConnection(){
              var connection = new SqlConnection(_connectionString);
              return connection;

    }

   
}



