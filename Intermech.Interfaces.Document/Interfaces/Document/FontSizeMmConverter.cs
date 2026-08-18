// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.FontSizeMmConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер типа Float для размера шрифта в миллиметрах</summary>
public class FontSizeMmConverter : MMConverter
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
    return new TypeConverter.StandardValuesCollection((ICollection) new float[10]
    {
      1.8f,
      2.5f,
      3.5f,
      5f,
      7f,
      10f,
      14f,
      20f,
      28f,
      40f
    });
  }
}
