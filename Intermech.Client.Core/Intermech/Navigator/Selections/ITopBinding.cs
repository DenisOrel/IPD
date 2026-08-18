
// Type: Intermech.Navigator.Selections.ITopBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using System;


namespace Intermech.Navigator.Selections;

/// <summary>Обеспечивает функционирование корня дерева выборок.</summary>
public interface ITopBinding
{
  /// <summary>
  /// Возвращает набор условий, с помощью которых можно найти выборки,
  /// находящиеся на верхнем уровне дерева выборок.
  /// </summary>
  ConditionStructure[] TopConditions { get; }

  /// <summary>
  /// Выполняет вставку выборки в верхний уровень дерева выборок. Вызывается сразу
  /// после создания новой выборки.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  void BindSelection(long selObjectID);

  /// <summary>Возвращает название корня дерева выборок.</summary>
  /// <param name="selTypeID">Идентификатор базового типа выборок в дереве</param>
  /// <returns>Название корня дерева выборок</returns>
  string GetCaption(int selTypeID);

  /// <summary>
  /// Возвращает для корня дерева выборок данные в указанном формате.
  /// </summary>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Данные в запрошенном формате</returns>
  object GetData(Type dataFormat);

  /// <summary>Возвращает тип привязки</summary>
  BindingType BindingType { get; }
}
