
// Type: Intermech.PropertyEditors.ExPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

internal class ExPropertyClass
{
  private string id;

  public string Id => this.id;

  public ExPropertyClass(string aId) => this.id = aId;

  public override string ToString() => this.id + " to string";
}
