
// Type: Intermech.Navigator.Controls.NodesEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

public class NodesEventArgs : EventArgs
{
  public NodesEventArgs(NavigatorTreeNodes nodes)
  {
    this.Nodes = this.Nodes != null ? nodes : throw new ArgumentNullException(nameof (nodes));
  }

  public NavigatorTreeNodes Nodes { get; private set; }
}
