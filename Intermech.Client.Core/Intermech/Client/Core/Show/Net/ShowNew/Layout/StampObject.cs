
// Type: Intermech.Client.Core.Show.Net.ShowNew.Layout.StampObject
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Show;
using System.Diagnostics;


namespace Intermech.Client.Core.Show.Net.ShowNew.Layout;

[DebuggerDisplay("{Name} {Value}")]
internal class StampObject : IStampField
{
  internal StampObject(string name, string value)
  {
    this.Name = name;
    this.Value = value;
  }

  public string Name { get; }

  public string Value { get; }
}
