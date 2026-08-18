
// Type: Intermech.Interfaces.MailEnvelop
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    public class MailEnvelop
    {
      public string id;
      public DateTime date;
      public string from;
      public string subject;
      public bool unread;
      public int atts;
    }
}
