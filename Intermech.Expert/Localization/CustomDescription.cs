// Decompiled with JetBrains decompiler
// Type: Intermech.Localization.CustomDescription
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

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
