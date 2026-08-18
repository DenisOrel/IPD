
// Type: Intermech.Tools.Data.Sync.AttributeSyncDefect
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Tools.Data.Sync;

/// <summary>
/// Описывает ошибку переноса атрибута из одной системы в другую.
/// </summary>
public sealed class AttributeSyncDefect
{
  private StringKey attrKey;
  private string defectDetails;

  /// <summary>Создает объект.</summary>
  /// <param name="attributeKey">Ключ атрибута</param>
  /// <param name="defectDetails">Подробное описание возникшей ошибки</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на ключ атрибута не может быть null</exception>
  /// <exception cref="T:System.ArgumentException">Описание возникшей ошибки не задано</exception>
  public AttributeSyncDefect(StringKey attributeKey, string defectDetails)
  {
    if (attributeKey == (StringKey) null)
      throw new ArgumentNullException(nameof (attributeKey));
    if (string.IsNullOrEmpty(defectDetails))
      throw new ArgumentException(LocalizationHolder.rm.GetString("SR_1624"), nameof (defectDetails));
    this.attrKey = attributeKey;
    this.defectDetails = defectDetails;
  }

  /// <summary>
  /// Возвращает ключ атрибута, при переносе которого произошла ошибка.
  /// </summary>
  public StringKey AttributeKey => this.attrKey;

  /// <summary>
  /// Возвращает подробное описание ошибки переноса атрибута.
  /// </summary>
  public string DefectDetails => this.defectDetails;
}
