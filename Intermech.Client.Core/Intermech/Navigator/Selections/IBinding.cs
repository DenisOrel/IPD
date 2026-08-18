
// Type: Intermech.Navigator.Selections.IBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;


namespace Intermech.Navigator.Selections;

/// <summary>Обеспечивает функционирование выборок в дереве.</summary>
public interface IBinding
{
  /// <summary>
  /// Возвращает набор условий для выборки с указанным идентификатором.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  /// <returns>
  /// Массив условий, которые позволяют найти в базе данных объекты,
  /// удовлетворяющие условиям выборки.
  /// </returns>
  ConditionStructure[] GetConditions(long selObjectID);

  /// <summary>
  /// Возвращает часть элемента навигации, которая будет работать с объектами,
  /// найденными с помощью условий выборки.
  /// </summary>
  /// <param name="conditionProvider">Провайдер, предоставляющий условия выборки</param>
  /// <returns>Часть элемента навигации</returns>
  INodePart GetPart(IConditionsProvider conditionProvider);

  /// <summary>
  /// Возвращает название закладки, на которой будут отображаться объекты,
  /// найденные с помощью условий выборки.
  /// </summary>
  string ViewCaption { get; }
}
