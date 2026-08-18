
// Type: Intermech.Interfaces.WebPortal.SoapExceptionHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    public class SoapExceptionHelper
    {
      public static void ParceMessage(string inMessage, ref string message, ref string stack)
      {
        int num1 = inMessage.IndexOf("--->");
        if (num1 >= 0)
          inMessage = inMessage.Remove(0, num1 + 4);
        bool flag = false;
        int length1 = inMessage.IndexOf("Stack:");
        if (length1 >= 0)
        {
          message = inMessage.Substring(0, length1);
          inMessage = inMessage.Remove(0, length1 + 6);
          flag = true;
        }
        int length2 = inMessage.IndexOf("Server stack trace:");
        if (length2 >= 0)
        {
          if (!flag)
            message = inMessage.Substring(0, length2);
          inMessage = inMessage.Remove(0, length2 + 6);
          stack = "Remote server" + inMessage;
        }
        int num2 = message.IndexOf("System.Exception:");
        if (num2 >= 0)
          message = message.Remove(0, num2 + "System.Exception:".Length);
        if (!(message != string.Empty))
          return;
        message = message.Trim();
      }
    }
}
