
// Type: Intermech.Docking.ControlLayoutSystemConverter
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;


namespace Intermech.Docking;

internal class ControlLayoutSystemConverter : TypeConverter
{
  public override bool CanConvertTo(ITypeDescriptorContext A_0, Type A_1)
  {
    return A_1 == typeof (InstanceDescriptor) || base.CanConvertTo(A_0, A_1);
  }

  public override object ConvertTo(
    ITypeDescriptorContext A_0,
    CultureInfo A_1,
    object A_2,
    Type A_3)
  {
    if (A_3 == (Type) null)
      throw new ArgumentNullException();
    if (A_3 == typeof (InstanceDescriptor) && A_2 is ControlLayoutSystem)
    {
      ControlLayoutSystem controlLayoutSystem = (ControlLayoutSystem) A_2;
      Type[] types = new Type[4]
      {
        typeof (int),
        typeof (int),
        typeof (DockControl[]),
        typeof (DockControl)
      };
      MemberInfo constructor = (MemberInfo) A_2.GetType().GetConstructor(types);
      DockControl[] array = new DockControl[controlLayoutSystem.Controls.Count];
      controlLayoutSystem.Controls.CopyTo(array, 0);
      object[] arguments = new object[4]
      {
        (object) (int) controlLayoutSystem._workingSize.Width,
        (object) (int) controlLayoutSystem._workingSize.Height,
        (object) array,
        (object) controlLayoutSystem.SelectedControl
      };
      if (constructor != (MemberInfo) null)
        return (object) new InstanceDescriptor(constructor, (ICollection) arguments);
    }
    return base.ConvertTo(A_0, A_1, A_2, A_3);
  }
}
