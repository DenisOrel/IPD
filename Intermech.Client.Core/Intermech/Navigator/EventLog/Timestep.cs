
// Type: Intermech.Navigator.EventLog.Timestep
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.EventLog;

public class Timestep
{
  public string Name;
  public string Value;

  public Timestep(string name, string index)
  {
    this.Name = name;
    this.Value = index;
  }

  public override string ToString() => this.Name;
}
