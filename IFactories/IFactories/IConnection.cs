using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Eapproval.Factories.IFactories;

public interface IConnection
{
    SqlConnection GetConnection();
}
