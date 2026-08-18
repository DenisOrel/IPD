
// Type: Intermech.Navigator.Conditions.IEditControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.SelectionView;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Интерфейс, реализуемый контролами значений редактора условий выборки
/// </summary>
public interface IEditControl
{
  /// <summary>Ссылка на контрол</summary>
  Control Control { get; }

  /// <summary>Значение</summary>
  object Value { get; set; }

  /// <summary>
  /// Флаг, обозначающий первый контрол в случае с двумя контролами для условия,
  /// например для задания границ диапазона значений
  /// </summary>
  bool IsFirstValue { get; }

  /// <summary>Функция создания контрола</summary>
  /// <param name="valueMode">Режим отображения</param>
  /// <param name="value">Значение</param>
  void CreateControl(ShowValueMode valueMode, object value);

  /// <summary>
  /// Событие, возникающие при изменении значения в контроле
  /// </summary>
  event ValueChangedEventHandler ValueChangedEvent;

  /// <summary>Метод вызывается при создании нового значения</summary>
  bool OnAddNewValue(OnOpenDialogEventArgs e);
}
