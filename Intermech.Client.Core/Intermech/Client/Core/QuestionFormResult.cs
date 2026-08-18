
// Type: Intermech.Client.Core.QuestionFormResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Client.Core;

/// <summary>Возможные результаты работы с ошибками.</summary>
[CustomDescription("Attribute.Client.Core_188")]
[TypeConverter(typeof (EnumDescConverter))]
public enum QuestionFormResult
{
  /// <summary>Пропустить одну ошибку</summary>
  [CustomDescription("Attribute.Client.Core_189")] Skip,
  /// <summary>Прорустить все ошибки</summary>
  [CustomDescription("Attribute.Client.Core_190")] SkipAll,
  /// <summary>Прервать выполнение операций</summary>
  [CustomDescription("Attribute.Client.Core_191")] Break,
}
