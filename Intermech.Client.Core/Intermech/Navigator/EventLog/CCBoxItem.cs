
// Type: Intermech.Navigator.EventLog.CCBoxItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.EventLog;

public class CCBoxItem
{
  private int _val;
  private string _name;

  public int Value
  {
    get => this._val;
    set => this._val = value;
  }

  public string Name
  {
    get => this._name;
    set => this._name = value;
  }

  public CCBoxItem()
  {
  }

  public CCBoxItem(string name, int userID)
  {
    this._name = name;
    this._val = userID;
  }

  public override string ToString() => $"{this._name}";
}
