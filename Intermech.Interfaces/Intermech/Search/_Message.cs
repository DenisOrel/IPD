
// Type: Intermech.Search._Message
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search
{
    [Serializable]
    public class _Message
    {
      public _Message(_MessageType type, string text)
      {
        if (string.IsNullOrEmpty(text))
          throw new ArgumentException();
        this.Type = type;
        this.Text = text;
      }

      public _MessageType Type { get; private set; }

      public string Text { get; private set; }

      public object Tag { get; set; }
    }
}
