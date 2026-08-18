
// Type: Intermech.Search.Discussions.DiscussionSerializer
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;


namespace Intermech.Search.Discussions
{
    public sealed class DiscussionSerializer
    {
      public string Serialize(MessageDto[] messages)
      {
        if (messages == null)
          messages = new MessageDto[0];
        return this.SerializeMessages(messages);
      }

      private string SerializeMessages(MessageDto[] messages)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (MessageDto message in messages)
          this.SerializeMessage(message, stringBuilder);
        return stringBuilder.ToString();
      }

      private void SerializeMessage(MessageDto message, StringBuilder stringBuilder)
      {
        if (message.LastModificationTimestamp.HasValue)
          stringBuilder.AppendFormat("|{0}", (object) message.LastModificationTimestamp.Value.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture));
        stringBuilder.AppendFormat("|{0}|{1}|{2}|{3}", (object) message.Id.AuthorVersionGuid.ToString("D"), (object) message.Id.CreationTimestamp.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture), (object) message.Caption, (object) message.Text);
        if (message.CuriousUsers == null)
          return;
        stringBuilder.AppendFormat("|users:{0}", (object) string.Join(",", ((IEnumerable<Guid>) message.CuriousUsers).Select<Guid, string>((Func<Guid, string>) (userVersionGuid => userVersionGuid.ToString("D")))));
      }
    }
}
