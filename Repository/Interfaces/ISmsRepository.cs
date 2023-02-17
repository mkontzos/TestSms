namespace Sms.Application.Interfaces;

public interface ISmsRepository
{
   Task SendSmsAsync(Domain.Entities.Sms sms);
}
