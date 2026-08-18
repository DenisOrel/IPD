// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.Xml.CustomDisplayName
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization.Xml;

/// <summary>
/// Класс позволяет получать отображаемое имя (DisplayName) из ресурсов текущей сборки
/// </summary>
internal class CustomDisplayName : DisplayNameAttribute
{
  /// <summary>
  /// Загрузить атрибут с указанным именем из ресурсов [CustomAttributesResources] текущей сборки
  /// </summary>
  /// <param name="displayName">Имя атрибута в ресурсах [CustomAttributesResources] текущей сборки</param>
  public CustomDisplayName(string displayName)
  {
    object obj = (object) LocalizationHolder.rma.GetString(displayName);
    this.DisplayNameValue = obj != null ? (string) obj : string.Empty;
  }
}
