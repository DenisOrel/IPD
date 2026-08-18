
// Type: Intermech.Docking.SplitLayoutSystemConverter
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;


namespace Intermech.Docking;

internal class SplitLayoutSystemConverter : TypeConverter
{
  private System.Type MakeArrayType(System.Type firstType)
  {
    return firstType.Assembly.GetType(firstType.FullName + "[]");
  }

  public override bool CanConvertTo(ITypeDescriptorContext A_0, System.Type type)
  {
    return type == typeof (InstanceDescriptor) || base.CanConvertTo(A_0, type);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    System.Type destinationType)
  {
    if (destinationType == (System.Type) null)
      throw new ArgumentNullException();
    if (!(destinationType == typeof (InstanceDescriptor)) || !(value is SplitLayoutSystem))
      return base.ConvertTo(context, culture, value, destinationType);
    System.Type type = value.GetType();
    System.Type baseType = type.BaseType;
    MemberInfo constructor = (MemberInfo) type.GetConstructor(new System.Type[3]
    {
      typeof (SizeF),
      typeof (Orientation),
      this.MakeArrayType(baseType)
    });
    ICollection collection = (ICollection) type.GetProperty("LayoutSystems", BindingFlags.Instance | BindingFlags.Public).GetValue(value, (object[]) null);
    object[] instance = (object[]) Activator.CreateInstance(this.MakeArrayType(baseType), (object) collection.Count);
    collection.CopyTo((Array) instance, 0);
    SizeF sizeF = (SizeF) type.GetProperty("WorkingSize", BindingFlags.Instance | BindingFlags.Public).GetValue(value, (object[]) null);
    Orientation orientation = (Orientation) type.GetProperty("SplitMode", BindingFlags.Instance | BindingFlags.Public).GetValue(value, (object[]) null);
    return (object) new InstanceDescriptor(constructor, (ICollection) new object[3]
    {
      (object) sizeF,
      (object) orientation,
      (object) instance
    });
  }
}
