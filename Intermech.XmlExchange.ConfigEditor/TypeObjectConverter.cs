// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.TypeObjectConverter
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor;

internal class TypeObjectConverter : TypeObjectConverterBase
{
  public TypeObjectConverter(Type type)
    : base(type)
  {
  }

  public TypeObjectConverter()
  {
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return value.ToString() == Guid.Empty.ToString() ? (object) "Любой тип объекта" : base.ConvertTo(context, culture, value, destinationType);
  }

  public object ConvertToListView(object value)
  {
    return this.ConvertTo((ITypeDescriptorContext) null, (CultureInfo) null, value, typeof (string));
  }
}
