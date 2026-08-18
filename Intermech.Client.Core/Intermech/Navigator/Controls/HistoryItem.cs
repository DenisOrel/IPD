
// Type: Intermech.Navigator.Controls.HistoryItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

internal class HistoryItem
{
  private string _displayName;
  private object _tag;

  public HistoryItem(string displayName, object tag)
  {
    this._displayName = displayName;
    this._tag = tag;
  }

  public string DisplayName => this._displayName;

  public object Tag => this._tag;
}
