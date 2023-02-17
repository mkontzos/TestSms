namespace Sms.Application.Interfaces;
public interface ISmsVendor
{
   Task SendAsync(Domain.Entities.Sms sms);
}
