
// Type: Intermech.Search.Discussions.MessageDto
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Discussions
{
    [Serializable]
    public sealed class MessageDto
    {
      public MessageIdDto Id { get; set; }

      public string AuthorName { get; set; }

      public string Caption { get; set; }

      public DateTime? LastModificationTimestamp { get; set; }

      public string Text { get; set; }

      public Guid[] CuriousUsers { get; set; }

      public MessageContextDto Context { get; set; }
    }
}
