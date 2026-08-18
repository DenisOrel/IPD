
// Type: Intermech.Client.Core.FormDesigner.Controls.AttrDestinationPointConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
/// <description>
/// Этот конвертер временный. Написан только как заплатка.
/// Проблемма была следующая:
/// В AttributeDestinationPoint вместо значений перечисления в базу записывалось его строковое значение. При этом стока записывалась с учетом локали.
/// Следовательно если установить значения атрибута, например, в английской локали, а потом переключиться на русскую локаль и попробовать считать это значение,
/// то конвертация обратно в перечисление происходила неправильно.
/// Сейчас в базу записывается не строковое значение а значение перечисления (например Default). Но так как создано уже много форм,
/// то было решено создать этот конвертер, чтобы обеспечить приемственность.
/// В дальнейшем НУЖНО!!! будет удалить.
/// Но вместе с конвертером нужно будет удалить атрибут [TypeConverter(typeof(AttrDestinationPointConverter))] у перечисления AttributeDestinationPoint.
/// </description>
/// <summary>Конструктор.</summary>
/// <param name="type"></param>
public class AttrDestinationPointConverter(Type type) : EnumConverter(type)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    string str = Convert.ToString(value);
    return str.CompareTo("Relation") == 0 || str.CompareTo("Default") == 0 ? base.ConvertFrom(context, culture, value) : (object) AttributeDestinationPoint.Default;
  }
}
