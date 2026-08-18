
// Type: Intermech.PropertyEditors.LanguagePropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;


namespace Intermech.PropertyEditors;

public class LanguagePropertyClass
{
  private string language = string.Empty;

  public string Language => this.language;

  public LanguagePropertyClass(string aLanguageID) => this.language = aLanguageID;

  public override string ToString() => DataHolders.LanguagesHolder.GetNamebyID(this.Language);
}
