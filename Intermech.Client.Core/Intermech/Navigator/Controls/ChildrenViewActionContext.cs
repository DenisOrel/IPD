
// Type: Intermech.Navigator.Controls.ChildrenViewActionContext
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.Controls;

public sealed class ChildrenViewActionContext
{
  public ChildrenViewActionContext(INodeID sourceActionNodeID)
  {
    this.SourceActionNodeID = sourceActionNodeID != null ? sourceActionNodeID : throw new ArgumentNullException(nameof (sourceActionNodeID));
  }

  public INodeID SourceActionNodeID { get; private set; }
}
