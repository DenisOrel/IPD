// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDisplayName
// Assembly: Intermech.TechAcad.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=8d2bc20ab69e4bb4
// MVID: 512FF008-192B-42A6-A8D1-B0B0A687059D
// Assembly location: D:\IPS\Client\Intermech.TechAcad.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.TechAcad.Interfaces.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization;

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
