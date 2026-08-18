
// Type: Intermech.Search.Diff.DiffCollectionConverterBase`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Globalization;


namespace Intermech.Search.Diff;

public abstract class DiffCollectionConverterBase<T> : TypeConverter where T : IDiff
{
  protected abstract PropertyDescriptorCollection CreatePropertyDescriptorCollection(
    IDiffCollection<T> diffCollection);

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return destinationType == typeof (string) ? (object) "" : base.ConvertTo(context, culture, value, destinationType);
  }

  public override PropertyDescriptorCollection GetProperties(
    ITypeDescriptorContext context,
    object value,
    Attribute[] attributes)
  {
    if (value == null)
      throw new ArgumentNullException(nameof (value));
    return value is IDiffCollection<T> ? this.CreatePropertyDescriptorCollection((IDiffCollection<T>) value) : throw new ArgumentException();
  }

  public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
}
