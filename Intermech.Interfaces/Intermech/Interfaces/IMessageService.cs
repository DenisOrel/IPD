
// Type: Intermech.Interfaces.IMessageService
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Summary description for IMessageService.</summary>
    [Obsolete("Use the IAlertMessageService interface instead of this.", true)]
    public interface IMessageService
    {
      void MessageBox(string caption, string message, Exception e);

      void MessageBox(string caption, string message, MessageType messageType, Exception e);
    }
}
