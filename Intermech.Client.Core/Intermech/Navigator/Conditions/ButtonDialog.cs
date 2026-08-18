
// Type: Intermech.Navigator.Conditions.ButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Диалог, отображаемый по нажатию кнопки в TextBoxButton
/// </summary>
internal abstract class ButtonDialog : IButtonDialog
{
  protected IConditionDataProvider dataProvider;
  protected int attributeID;

  public ButtonDialog(IConditionDataProvider dataProvider, int attributeID, object value)
  {
    this.dataProvider = dataProvider;
    this.attributeID = attributeID;
    this.Value = value;
  }

  /// <summary>Выбранное в диалоге значение</summary>
  public object Value { get; protected set; }

  /// <summary>Текстовое отображение выбранного значения</summary>
  public string Text { get; protected set; }

  /// <summary>
  /// Метод обработчика нажатия кнопки в TextBoxButton.
  /// Возвращает признак изменения значения.
  /// </summary>
  /// <returns></returns>
  public abstract bool OnOpenDialog(bool multiselect);
}
