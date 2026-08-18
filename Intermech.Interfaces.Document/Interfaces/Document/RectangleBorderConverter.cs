// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.RectangleBorderConverter
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Конвертер для класса RectangleBorder</summary>
public class RectangleBorderConverter : LocalizedExpandableObjectConverter
{
  /// <summary>Создает экземпляр типа, с которым связан этот TypeConverter,
  /// используя заданную контекстную информацию и переданный набор значений свойств
  /// для этого объекта</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную
  /// информацию о формате</param>
  /// <param name="propertyValues">IDictionary новых значений свойства</param>
  /// <returns>Object, представляющий данный IDictionary или пустая ссылка,
  /// если объект не может быть создан</returns>
  public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
  {
    CustomBorder instance = !(context.PropertyDescriptor.GetValue(context.Instance) is CustomBorder customBorder) ? new CustomBorder() : (CustomBorder) customBorder.Clone();
    instance.Top = (BorderLine) propertyValues[(object) "Top"];
    instance.Bottom = (BorderLine) propertyValues[(object) "Bottom"];
    instance.Left = (BorderLine) propertyValues[(object) "Left"];
    instance.Right = (BorderLine) propertyValues[(object) "Right"];
    return (object) instance;
  }

  /// <summary>Возвращает значение, показывающее, требуется ли при изменении значения
  /// этого объекта вызывать CreateInstance, чтобы создать новое значение, используя
  /// заданную контекстную информацию</summary>
  /// <param name="context">ITypeDescriptorContext, предоставляющий контекстную информацию
  /// о формате</param>
  /// <returns>true, если при изменении значения этого объекта требуется вызывать CreateInstance,
  /// чтобы создать новое значение, false, если нет</returns>
  public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => true;
}
