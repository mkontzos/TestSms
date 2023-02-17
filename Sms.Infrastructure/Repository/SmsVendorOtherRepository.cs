using Dapper;
using Sms.Application.Helpers;
using Sms.Application.Interfaces;
using System.Data;

namespace Sms.Infrastructure.Repository;

public class SmsVendorOtherRepository : ISmsVendor
{
   private readonly IDbConnection _dbConnection;

   public SmsVendorOtherRepository(IDbConnection dbConnection)
   {
      _dbConnection = dbConnection;
   }

   public async Task SendAsync(Domain.Entities.Sms sms)
   {
      if (string.IsNullOrEmpty(sms.Message))
      {
         return;
      }

      if (MessageManipulation.isGreaterThanMaxCharacterLength(sms.Message, 480))
      {
         throw new InvalidOperationException("SMSVendorGR cannot send an SMS with greater than 480 characters.");
      }

      await _dbConnection.ExecuteAsync("INSERT INTO SmsMessages (PhoneNumber, Message) VALUES (@PhoneNumber, @Message)", new { PhoneNumber = sms.PhoneNumber, Message = sms.Message });
   }
}
