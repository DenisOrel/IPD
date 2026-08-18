
// Type: Intermech.Search.Discussions.MessageContextDto
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Search.Discussions
{
    [Serializable]
    public sealed class MessageContextDto
    {
      public MessageContextDto() => this.ObjectVersionGuidToCaptionMap = new Dictionary<Guid, string>();

      public Dictionary<Guid, string> ObjectVersionGuidToCaptionMap { get; set; }
    }
}
