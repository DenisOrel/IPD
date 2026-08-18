// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ToolbarItemBaseConverter
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace Intermech.Document.Model;

internal class ToolbarItemBaseConverter : TypeConverter
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
    return A_3 == typeof (InstanceDescriptor) ? (object) new InstanceDescriptor((MemberInfo) A_2.GetType().GetConstructor(Type.EmptyTypes), (ICollection) null, false) : base.ConvertTo(A_0, A_1, A_2, A_3);
  }
}
