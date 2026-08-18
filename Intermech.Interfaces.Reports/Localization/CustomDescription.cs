// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Localization;

/// <summary>
/// Класс позволяет получать описания (Descriptions) из ресурсов текущей сборки
/// </summary>
internal class CustomDescription : DescriptionAttribute
{
  /// <summary>
  /// Загрузить описание с указанным именем из ресурсов [CustomAttributesResources] текущей сборки
  /// </summary>
  /// <param name="description">Имя описания в ресурсах [CustomAttributesResources] текущей сборки</param>
  public CustomDescription(string description)
  {
    object obj = (object) LocalizationHolder.rma.GetString(description);
    this.DescriptionValue = obj != null ? (string) obj : string.Empty;
  }
}
