
// Type: Intermech.Search.MessageCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Linq;


namespace Intermech.Search;

public sealed class MessageCollection : BindingList<_Message>
{
  public bool IsEmpty => this.Count == 0;

  public bool HasErrors
  {
    get
    {
      return this.Where<_Message>((Func<_Message, bool>) (o => o.Type == _MessageType.Error)).Count<_Message>() > 0;
    }
  }
}
