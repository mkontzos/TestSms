using System.Text.RegularExpressions;

namespace Sms.Application.Helpers;

public static class MessageManipulation
{
   public static bool IsGreekMessage(string message)
   {
      return Regex.IsMatch(message, @"^\p{IsGreek}*$");
   }

   public static List<string> SplitMessage(string message, int maxLength)
   {
      var messages = new List<string>();
      for (int i = 0; i < message.Length; i += maxLength)
      {
         var length = Math.Min(maxLength, message.Length - i);
         messages.Add(message.Substring(i, length));
      }
      return messages;
   }

   public static bool isGreaterThanMaxCharacterLength(string message, int length)
   {
      return message.Length > 480;
   }
}
