using Dapper;
using Sms.Application.Helper;
using Sms.Application.Interfaces;
using System.Data;

namespace Sms.Infrastructure.Repository;
public class SmsVendorCYRepository : ISmsVendor
{
   private readonly IDbConnection _dbConnection;

   public SmsVendorCYRepository(IDbConnection dbConnection)
   {
      _dbConnection = dbConnection;
   }

   public async Task SendAsync(Domain.Entities.Sms sms)
   {
      var messages = new List<string>();

      if (string.IsNullOrEmpty(sms.Message))
      {
         return;
      }

      if (sms.Message.Length > 160)
      {
         messages = MessageManipulation.SplitMessage(sms.Message, sms.Message.Length);
      }

      if (MessageManipulation.isGreaterThanMaxCharacterLength(sms.Message, 480))
      {
         throw new InvalidOperationException("SMSVendorGR cannot send an SMS with greater than 480 characters.");
      }

      await _dbConnection.ExecuteAsync("INSERT INTO SmsMessages (PhoneNumber, Message) VALUES (@PhoneNumber, @Message)", new { PhoneNumber = sms.PhoneNumber, Message = sms.Message });
   }


}
