namespace Sms.Application.Helpers;

public class ResponseMessage
{
   public Domain.Entities.Sms? Data { get; set; }
   public bool Success { get; set; }
   public string? Message { get; set; }
   public int? ErrorCode { get; set; }
}
