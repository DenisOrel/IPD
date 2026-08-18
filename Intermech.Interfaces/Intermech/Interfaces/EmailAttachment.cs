
// Type: Intermech.Interfaces.EmailAttachment
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Diagnostics;
using System;


namespace Intermech.Interfaces
{
    [Serializable]
    public class EmailAttachment
    {
      public string FileName { get; private set; }

      public string StotageFileName { get; private set; }

      public EmailAttachment([NotNull] string fileName)
      {
        this.FileName = fileName;
        this.StotageFileName = $"{Guid.NewGuid()}.tmp";
      }
    }
}
