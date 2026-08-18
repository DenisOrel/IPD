// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IConditionController
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Kernel.Search;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс описывает контроллер условия выборки.</summary>
public interface IConditionController
{
  /// <summary>Имя для отображения пользователю</summary>
  string VisibleName { get; }

  /// <summary>Поддерживаемые условием источники данных</summary>
  SelectionDataSource SupportedDataSource { get; }

  /// <summary>Поддерживаемые типы выборок</summary>
  SelectionType[] SupportedTypes { get; }

  /// <summary>Создание нового условия</summary>
  /// <param name="selectionID">Идентификатор выборки</param>
  /// <param name="obectTypeIDs"></param>
  /// <returns>Новое условие</returns>
  ConditionStructure CreateCondition(long selectionID, int[] obectTypeIDs);

  /// <summary>Создание нового условия</summary>
  /// <param name="selectionID">Идентификатор выборки</param>
  /// <param name="current">Текущее условие</param>
  /// <param name="obectTypeIDs"></param>
  /// <returns>Измененное условие</returns>
  ConditionStructure EditCondition(
    long selectionID,
    ConditionStructure current,
    int[] obectTypeIDs);

  /// <summary>
  /// Возвращает признак, обрабатывает ли контроллер conditionStructure
  /// </summary>
  /// <param name="conditionStructure"></param>
  /// <returns></returns>
  bool IsHandleConditionStructure(ConditionStructure conditionStructure);

  /// <summary>Обработка отображения условия в списке условий</summary>
  /// <param name="conditionStructure"></param>
  /// <param name="condition">Текст для колонки Условие</param>
  /// <param name="value">Текст для колонки Значение</param>
  /// <returns></returns>
  bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string condition,
    out string value);

  /// <summary>Условие работает со значениями атрибутов</summary>
  bool AttributesCondition { get; }

  /// <summary>
  /// Возможно ли использование условия во вложенных условиях
  /// </summary>
  bool IsInnerSupported { get; }
}
