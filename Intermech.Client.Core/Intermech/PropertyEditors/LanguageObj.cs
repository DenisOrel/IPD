
// Type: Intermech.PropertyEditors.LanguageObj
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors;

internal class LanguageObj
{
  public string Name = string.Empty;
  public string Id = string.Empty;

  public LanguageObj(string name, string id)
  {
    this.Name = name;
    this.Id = id;
  }

  public override string ToString() => this.Name;
}
