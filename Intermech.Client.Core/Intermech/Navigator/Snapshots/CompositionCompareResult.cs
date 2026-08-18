
// Type: Intermech.Navigator.Snapshots.CompositionCompareResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Navigator.Snapshots;

/// <summary>Результат сравнения актуального состава с сохранённым в итерации для данной ноды (имеет смысл только в том, случае, если нода
/// была создана в контексте сравнения, что можно узнать из контекста (IContextAware) запросив значение типа
/// SnapshotDescriptor.Content)</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum CompositionCompareResult
{
  /// <summary>Значение ещё не загружено</summary>
  [CustomDescription("CompareResultNotChecked")] NotChecked,
  /// <summary>Сравнение не требуется, нода создана вне контекста сравнения состава (напр. простая демонстрация состава итерации)</summary>
  [CustomDescription("CompareResultNotCompared")] NotCompared,
  /// <summary>Объект не изменялся с момента сохранения в составе итерации</summary>
  [CustomDescription("CompareResultNotChanged")] NotChanged,
  /// <summary>Объект отсутствует в итерации, но присутствует в актуальном составе, скорее всего был добавлен в состав позже</summary>
  [CustomDescription("CompareResultNew")] New,
  /// <summary>Объект присутствует и в итерации, и в актуальном составе, однако его параметры изменились</summary>
  [CustomDescription("CompareResultEdited")] Edited,
  /// <summary>Объект присутствует в итерации, но отсутствует в актуальном составе, скорее всего был удалён из состава</summary>
  [CustomDescription("CompareResultDeleted")] Deleted,
}
