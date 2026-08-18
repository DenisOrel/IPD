
// Type: Intermech.Navigator.Controls.CheckStateEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public class CheckStateEventArgs : NodeEventArgs
{
  private CheckState _oldValue;
  private CheckState _newValue;

  public CheckStateEventArgs(NavigatorTreeNode node, CheckState oldValue, CheckState newValue)
    : base(node)
  {
    this._oldValue = oldValue;
    this._newValue = newValue;
  }

  public CheckState OldValue => this._oldValue;

  public CheckState NewValue
  {
    get => this._newValue;
    set => this._newValue = value;
  }
}
