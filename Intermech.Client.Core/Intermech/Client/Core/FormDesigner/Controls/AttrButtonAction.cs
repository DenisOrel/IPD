
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrButtonAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>Назначение действий на кнопки.</summary>
[TypeConverter(typeof (EnumDescConverter))]
[CustomDescription("Attribute.Client.Core_213")]
public enum AttrButtonAction
{
  /// <summary>Нет действия</summary>
  [CustomDescription("Attribute.Client.Core_214")] None,
  /// <summary>Отмена</summary>
  [CustomDescription("Attribute.Client.Core_215")] Cancel,
  /// <summary>Применить</summary>
  [CustomDescription("Attribute.Client.Core_216")] Apply,
  /// <summary>Расчитать</summary>
  [CustomDescription("Attribute.Client.Core_217")] Calc,
  /// <summary>Пересчитать</summary>
  [CustomDescription("Attribute.Client.Core_218")] ReCalc,
}
