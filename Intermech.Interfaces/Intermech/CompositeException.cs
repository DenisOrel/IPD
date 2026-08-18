
// Type: Intermech.CompositeException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech
{
    /// <summary>
    /// Исключение, которое содержит список связанных исключений
    /// </summary>
    public class CompositeException : Exception
    {
      public readonly System.Collections.Generic.List<Exception> List;

      public CompositeException(System.Collections.Generic.List<Exception> list)
        : base("")
      {
        this.List = list;
      }

      public override string Message
      {
        get
        {
          string message = "";
          if (this.List != null)
          {
            for (int index = 0; index < this.List.Count; ++index)
            {
              if (message != "")
                message += "\r\n\r\n";
              if (this.List.Count > 1)
                message += $"[{index + 1}] ";
              message = $"{message}{this.List[index].Source}: \r\n" + this.List[index].Message;
            }
          }
          return message;
        }
      }
    }
}
