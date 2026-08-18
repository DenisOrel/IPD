
// Type: Intermech.Navigator.Controls.StatesManagerEntry
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

internal class StatesManagerEntry
{
  private StatesRecord _record;
  private int _useCount;

  public StatesManagerEntry(StatesRecord record)
  {
    this._record = record;
    this._useCount = 0;
  }

  public StatesRecord Record => this._record;

  public int UseCount
  {
    get => this._useCount;
    set => this._useCount = value;
  }
}
