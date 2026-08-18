
// Type: Intermech.Protection.ExceptionInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Protection
{
    internal class ExceptionInfo
    {
      public string ComputerName;
      public DateTime Date;
      public Exception Exception;

      public override string ToString()
      {
        return $"{this.Date.ToString("F")}. Компьютер : '{this.ComputerName}'. Текст сообщения: '{this.Exception.Message}'.";
      }
    }
}
