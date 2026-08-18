
// Type: Intermech.Navigator.SelectionView.AttributeValueItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для работы с допустимыми значениями атрибутов
/// </summary>
internal sealed class AttributeValueItem
{
  /// <summary>Поле для хранения допустимого значения атрибута</summary>
  private object _Value;
  /// <summary>
  /// Поле для хранения строки, соответствующей допустимому значению атрибута
  /// </summary>
  private string _text = "";

  /// <summary>Допустимое значение атрибута</summary>
  public object Value => this._Value;

  /// <summary>Конструктор</summary>
  /// <param name="value">Допустимое значение атрибута</param>
  /// <param name="text">Строка соответствующая допустимому значению атрибута</param>
  public AttributeValueItem(object value, string text)
  {
    this._Value = value;
    this._text = text;
  }

  /// <summary>
  /// Перекрытый метод для получения строкового представления допустимого значения атрибута
  /// </summary>
  /// <returns>Строковое представление допустимого значения атрибута</returns>
  public override string ToString() => this._text;
}
