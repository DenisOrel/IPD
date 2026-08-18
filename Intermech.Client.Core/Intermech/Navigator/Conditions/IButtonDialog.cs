
// Type: Intermech.Navigator.Conditions.IButtonDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Conditions;

/// <summary>
/// Диалог, отображаемый по нажатию кнопки в TextBoxButton
/// </summary>
internal interface IButtonDialog
{
  /// <summary>Выбранное в диалоге значение</summary>
  object Value { get; }

  /// <summary>Текстовое отображение выбранного значения</summary>
  string Text { get; }

  /// <summary>
  /// Метод отображения диалога. Возвращает признак изменения значения.
  /// </summary>
  /// <returns></returns>
  bool OnOpenDialog(bool multiselect);
}
