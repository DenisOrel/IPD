
// Type: Intermech.Navigator.Controls.BeforeExpandNodeEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

public class BeforeExpandNodeEventArgs : NodeEventArgs
{
  private bool _canExpand;

  public BeforeExpandNodeEventArgs(NavigatorTreeNode node)
    : base(node)
  {
    this._canExpand = true;
  }

  public bool CanExpand
  {
    get => this._canExpand;
    set => this._canExpand = true;
  }
}
