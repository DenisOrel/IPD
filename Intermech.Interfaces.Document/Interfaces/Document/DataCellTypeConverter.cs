// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.DataCellTypeConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System;
using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер для выбора типа ячеек, отображающих данные</summary>
public class DataCellTypeConverter : StringConverter
{
  /// <summary>Получает значение, показывающее, поддерживает ли этот объект стандартный
  /// набор значений, которые можно выбрать из списка, используя заданную
  /// контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <returns>true, если, чтобы найти стандартный набор значений, поддерживаемых данным объектом, следует
  /// вызвать метод GetStandardValues, false, если нет</returns>
  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;

  /// <summary>Возвращает коллекцию стандартных значений для того типа данных,
  /// которым предназначен этот конвертер типа, если предоставлена контекстная
  /// информация о формате</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате, которая может быть использована для извлечения дополнительных сведений о среде,
  ///  из которой вызывается этот конвертер. Этот параметр или свойства этого параметра
  ///  могут иметь значение пустая ссылка</param>
  /// <returns>TypeConverter.StandardValuesCollection, содержащий стандартный
  /// набор допустимых значений, или пустая ссылка, если этот тип данных не поддерживает
  /// стандартный набор значений</returns>
  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (!(context.Instance is TableData instance))
      return (TypeConverter.StandardValuesCollection) null;
    Type[] showElementTypes = instance.OwnerDocument.GetAviableDataShowElementTypes();
    string[] values = new string[showElementTypes.Length];
    for (int index = 0; index < showElementTypes.Length; ++index)
      values[index] = showElementTypes[index].FullName;
    return new TypeConverter.StandardValuesCollection((ICollection) values);
  }

  /// <summary>Возвращает значение, показывающее, является ли исчерпывающим списком возможных
  /// значений коллекция стандартных значений, возвращаемая методом GetStandardValues,
  /// используя заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию о формате</param>
  /// <returns>true, если объект TypeConverter.StandardValuesCollection, возвращенный
  /// методом GetStandardValues, является исчерпывающим списком возможных значений,
  /// false, если возможны другие значения</returns>
  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => false;
}
