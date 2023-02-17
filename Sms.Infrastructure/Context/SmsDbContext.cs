using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Sms.Infrastructure.Context;

public class SmsDbContext
{
   private readonly IDbConnection _connection;

   public SmsDbContext(IConfiguration configuration)
   {
      _connection = new SqlConnection(configuration.GetConnectionString("SmsDatabase"));
   }

   public IDbConnection GetConnection() => _connection;

   public void Dispose()
   {
      if (_connection != null && _connection.State != ConnectionState.Closed)
      {
         _connection.Close();
      }
   }
}
