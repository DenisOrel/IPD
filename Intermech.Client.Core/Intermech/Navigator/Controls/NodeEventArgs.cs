
// Type: Intermech.Navigator.Controls.NodeEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Controls;

public class NodeEventArgs : EventArgs
{
  public NodeEventArgs(NavigatorTreeNode node)
  {
    this.Node = node != null ? node : throw new ArgumentNullException(nameof (node));
  }

  public NavigatorTreeNode Node { get; private set; }
}
