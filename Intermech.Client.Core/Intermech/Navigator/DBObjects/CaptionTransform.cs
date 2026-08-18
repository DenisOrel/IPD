
// Type: Intermech.Navigator.DBObjects.CaptionTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Преобразование идентификаторов пользователей в их имена
/// </summary>
public class CaptionTransform : INodeColumnTransform
{
  /// <summary>
  /// Составное значение: атрибут VERSION : источник - объект
  /// </summary>
  private static NodeColumnID ncVERSION = new NodeColumnID((object) ObligatoryObjectAttributes.F_VERSION_ID, AttributeSourceTypes.Object);

  /// <summary>Тип данных</summary>
  public Type DataType => typeof (string);

  /// <summary>Выполнить преобразование</summary>
  /// <param name="sourceValue">Исходные данные</param>
  /// <param name="column">Описание колонки</param>
  /// <param name="adapter">Ссылка на объект типа Intermech.Navigator.Queries.RecordAdapter</param>
  /// <param name="allValues">Все допустимые значения в строке с данными</param>
  /// <returns>Новое значение</returns>
  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    RecordAdapter recordAdapter = adapter as RecordAdapter;
    if (UISettings.ShowVersionIDs == NavigatorCaptionVersionsMode.Caption || recordAdapter == null || allValues == null || allValues.Length < 2)
      return sourceValue;
    int fieldIndex = recordAdapter.GetFieldIndex((object) CaptionTransform.ncVERSION);
    if (fieldIndex < 0)
      fieldIndex = recordAdapter.GetFieldIndex((object) ObligatoryObjectAttributes.F_VERSION_ID);
    if (fieldIndex < 0)
      return sourceValue;
    object allValue = allValues[fieldIndex];
    string empty = string.Empty;
    if (sourceValue != null && sourceValue != DBNull.Value)
      empty = sourceValue.ToString();
    long version = 0;
    if (allValue != null && allValue != DBNull.Value)
      version = Convert.ToInt64(allValue);
    return version == 0L ? sourceValue : CellValue.GetValue(sourceValue, column, (object) CaptionTransform.GetCaption(empty, version));
  }

  public static string GetCaption(string caption, long version)
  {
    if (version == 0L)
      return caption;
    switch (UISettings.ShowVersionIDs)
    {
      case NavigatorCaptionVersionsMode.BracketCaption:
        return $"[{version}] {caption}";
      case NavigatorCaptionVersionsMode.CaptionBracket:
        return $"{caption} [{version}]";
      case NavigatorCaptionVersionsMode.VersionBracketCaption:
        return string.Format(LocalizationHolder.rm.GetString("Client.Core_1342"), (object) version, (object) caption);
      case NavigatorCaptionVersionsMode.CaptionVersionBracket:
        return string.Format(LocalizationHolder.rm.GetString("Client.Core_1343"), (object) caption, (object) version);
      case NavigatorCaptionVersionsMode.CaptionVersion:
        return string.Format(LocalizationHolder.rm.GetString("Client.Core_1344"), (object) caption, (object) version);
      default:
        return caption;
    }
  }
}
