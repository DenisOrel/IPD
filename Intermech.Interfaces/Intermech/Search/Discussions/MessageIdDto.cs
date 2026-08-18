
// Type: Intermech.Search.Discussions.MessageIdDto
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Search.Discussions
{
    [Serializable]
    public sealed class MessageIdDto
    {
      public long DiscussionVersionId { get; set; }

      public DateTime CreationTimestamp { get; set; }

      public Guid AuthorVersionGuid { get; set; }

      public override bool Equals(object obj)
      {
        if (this == obj)
          return true;
        return obj is MessageIdDto messageIdDto && this.DiscussionVersionId == messageIdDto.DiscussionVersionId && this.AuthorVersionGuid == messageIdDto.AuthorVersionGuid && this.CreationTimestamp.ToUniversalTime() == messageIdDto.CreationTimestamp.ToUniversalTime();
      }

      public override int GetHashCode()
      {
        return this.DiscussionVersionId.GetHashCode() ^ this.CreationTimestamp.GetHashCode() ^ this.AuthorVersionGuid.GetHashCode();
      }
    }
}
