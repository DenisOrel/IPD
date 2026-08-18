
// Type: Intermech.Client.Core.Navigator.Drawing.ColorsSchemeProperties
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator;
using System;


namespace Intermech.Client.Core.Navigator.Drawing;

/// <summary>описание пользовательской схемы цветов</summary>
[Serializable]
public class ColorsSchemeProperties : ICloneable
{
  /// <summary>имя схемы</summary>
  private string schemeName = string.Empty;
  /// <summary>уникальный id схемы</summary>
  private string schemeGuid = string.Empty;
  /// <summary>собственно сама схема</summary>
  private UIColorsScheme scheme;

  /// <summary>имя схемы</summary>
  public string SchemeName => this.schemeName;

  /// <summary>уникальный Guid схемы</summary>
  public string SchemeGuid
  {
    get => this.schemeGuid;
    set => this.schemeGuid = value;
  }

  /// <summary>собствеено сама схема</summary>
  public UIColorsScheme Scheme
  {
    get => this.scheme;
    set => this.scheme = value;
  }

  public ColorsSchemeProperties(string schemeName, string schemeGuid, UIColorsScheme scheme)
  {
    this.schemeName = schemeName;
    this.schemeGuid = schemeGuid;
    this.scheme = scheme;
  }

  public object Clone()
  {
    return (object) new ColorsSchemeProperties(this.schemeName, this.schemeGuid, this.scheme);
  }

  public override string ToString() => this.schemeName;
}
