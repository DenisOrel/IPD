
// Type: Intermech.Navigator.Conditions.IValueControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.SelectionService;
using Intermech.Kernel.Search;
using Intermech.Navigator.SelectionView;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Интерфейс, который поддерживают контролы для редактирования значений
/// </summary>
public interface IValueControl
{
  /// <summary>Инициализация контрола</summary>
  /// <param name="attributeID">Идентификатор атрибута в текущей системе</param>
  /// <param name="paramType">Тип данных</param>
  /// <param name="valueMode">Режим отображения</param>
  /// <param name="pValues">Допустимые значения</param>
  /// <param name="conditionStructure">Условие</param>
  /// <param name="objectTypeIDs"></param>
  /// <param name="tag">Дополнительные параметры</param>
  void Initialize(
    int attributeID,
    SelectionParameterTypes paramType,
    ShowValueMode valueMode,
    Dictionary<object, string> pValues,
    ConditionStructure conditionStructure,
    int[] objectTypeIDs,
    object tag);

  /// <summary>
  /// Событие, возникающие при изменении значений в контроле
  /// </summary>
  event ValuesChangedEventHandler ValuesChangedEvent;

  /// <summary>
  /// Событие, возникающее при изменении чуствительности к регистру
  /// </summary>
  event CaseSensitiveChangedEventHandler CaseSensitiveChangedEvent;

  /// <summary>
  /// Событие через которое можно передать текст для лабелов на контроле
  /// </summary>
  event OnGetLabelEventHandler OnGetLabelEvent;
}
