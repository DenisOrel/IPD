
// Type: Intermech.Navigator.SelectionView.ValueUpdater
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.SelectionService;
using Intermech.PropertyEditors;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.SelectionView;

/// <summary>
/// Локальный класс для реализации механизма обновления элементов
/// управления при изменениии значений условия выборки
/// </summary>
internal static class ValueUpdater
{
  /// <summary>Обновление TextBox содержащего ссылку на объект</summary>
  /// <param name="value">значение (ссылка на объект)</param>
  /// <param name="textBox">элемент управления который надо обновить</param>
  /// <param name="objType">тип ссылки на объект</param>
  /// <param name="attributeID"></param>
  public static void UpdateObjectReference(
    object value,
    TextBox textBox,
    SelectionParameterTypes objType,
    int attributeID)
  {
    if (value != null)
    {
      if (objType == SelectionParameterTypes.sptHandler)
      {
        IAttributePropertyDescriber describer = (ServicesManager.GetService(typeof (IAttributePropertyDescriberService)) as IAttributePropertyDescriberService).GetDescriber(attributeID);
        textBox.Text = Convert.ToString(describer.GetPropDescriptorValue((IElementInfo) null, attributeID, value));
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          textBox.Text = SelectionParameter.ConvertToString(sessionKeeper.Session, value, objType);
      }
    }
    else
      textBox.Text = string.Empty;
  }

  /// <summary>Обновление TextBox содержащего ссылку на тип связи</summary>
  /// <param name="value">значение</param>
  /// <param name="textBox">элемент управления который надо обновить</param>
  public static void UpdateRelationType(object value, TextBox textBox)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      textBox.Text = SelectionParameter.ConvertToString(sessionKeeper.Session, value, SelectionParameterTypes.sptLinkType);
  }

  /// <summary>
  /// Обновление элемента управления содержащего значение даты для Value
  /// </summary>
  /// <param name="value">значение </param>
  /// <param name="dateTimePicker">элемент управления который надо обновить</param>
  public static void UpdateDateValue(object value, DateTimePicker dateTimePicker)
  {
    if (dateTimePicker == null)
      return;
    if (value != null)
    {
      if (value is InputObjectAttribute)
        dateTimePicker.Value = DateTime.Today;
      else if (DateTimeHelper.IsDateValid(Convert.ToString(value)))
      {
        DateTime dateTime = Convert.ToDateTime(value);
        if (dateTime.Equals(DateTime.MinValue))
          dateTime = dateTimePicker.MinDate;
        if (!(dateTimePicker.Value != dateTime))
          return;
        dateTimePicker.Value = dateTime;
      }
      else
        dateTimePicker.Value = DateTime.Today;
    }
    else
      dateTimePicker.Value = DateTime.Today;
  }

  /// <summary>
  /// Обновление элемента управления содержащего значение для числовых типов Value
  /// </summary>
  /// <param name="value">значение</param>
  /// <param name="textBox">элемент управления который надо обновить</param>
  public static void UpdateNumValue(object value, TextBox textBox)
  {
    if (textBox == null)
      return;
    if (value is InputObjectAttribute)
    {
      textBox.Text = string.Empty;
    }
    else
    {
      string str = value != null ? Convert.ToString(Convert.ToDecimal(value)) : "";
      if (!(textBox.Text != str))
        return;
      textBox.Text = str;
    }
  }

  /// <summary>
  /// Обновление TextBox содержащего значение для булевых типов Value
  /// </summary>
  /// <param name="value">значение </param>
  /// <param name="comboBox">элемент управления который надо обновить</param>
  public static void UpdateBoolValue(object value, ComboBox comboBox)
  {
    if (comboBox == null && comboBox.Items.Count != 2 || value is InputObjectAttribute)
      return;
    bool flag = value != null && Convert.ToBoolean(value);
    if (comboBox.SelectedIndex == Convert.ToInt32(flag))
      return;
    comboBox.SelectedIndex = Convert.ToInt32(flag);
  }

  /// <summary>
  /// Обновление TextBox содержащего значение для текстовых типов Value
  /// </summary>
  /// <param name="value">значение (строка)</param>
  /// <param name="textBox">элемент управления который надо обновить</param>
  public static void UpdateStringValue(object value, TextBox textBox)
  {
    if (textBox == null)
      return;
    if (value is InputObjectAttribute)
    {
      textBox.Text = string.Empty;
    }
    else
    {
      string str = Convert.ToString(value);
      if (!(textBox.Text != str))
        return;
      textBox.Text = str;
    }
  }

  /// <summary>
  /// Обновление элемента управления содержащего значение из списка
  /// </summary>
  /// <param name="value">значение </param>
  /// <param name="comboBox">элемент управления который надо обновить</param>
  public static void UpdateListValue(object value, ComboBox comboBox)
  {
    if (comboBox == null)
      return;
    int num = -1;
    if (value != null)
    {
      for (int index = 0; index < comboBox.Items.Count && num < 0; ++index)
      {
        if ((comboBox.Items[index] as AttributeValueItem).Value.Equals(value))
          num = index;
      }
    }
    if (num == -1 && comboBox.Items.Count > 0)
      num = 0;
    comboBox.SelectedIndex = num;
  }
}
