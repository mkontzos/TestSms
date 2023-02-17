using Sms.Application.Interfaces;

namespace Sms.Infrastructure.Repository;

public class SmsRepository : ISmsRepository
{
   private readonly IDictionary<string, ISmsVendor> _vendors;

   public async Task SendSmsAsync(Domain.Entities.Sms sms)
   {
      var phoneCountryCode = sms.PhoneNumber.Substring(0, 2);
      var vendor = GetVendor(phoneCountryCode);
      await vendor.SendAsync(sms);
   }

   private ISmsVendor GetVendor(string countryCode)
   {
      var vendorMap = new Dictionary<string, ISmsVendor>
      {
        { "GR", _vendors["GR"] },
        { "CY", _vendors["CY"] },
        { "REST", _vendors["REST"] }
      };

      if (vendorMap.TryGetValue(countryCode, out var vendor))
      {
         return vendor;
      }

      throw new ArgumentException($"Invalid country code: {countryCode}");
   }
}
