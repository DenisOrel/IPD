
// Type: Intermech.Navigator.Controls.BeforeCollapseNodeEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

public class BeforeCollapseNodeEventArgs : NodeEventArgs
{
  private bool _canCollapse;

  public BeforeCollapseNodeEventArgs(NavigatorTreeNode node)
    : base(node)
  {
    this._canCollapse = true;
  }

  public bool CanCollapse
  {
    get => this._canCollapse;
    set => this._canCollapse = value;
  }
}
