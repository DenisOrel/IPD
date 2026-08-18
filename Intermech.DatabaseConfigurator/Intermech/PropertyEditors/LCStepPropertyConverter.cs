// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.LCStepPropertyConverter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Holders;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.PropertyEditors;

public class LCStepPropertyConverter(EventsHolder.GetListDelegate getListDelegate) : 
  DropDownTypeConverter(getListDelegate)
{
  public LCStepPropertyConverter()
    : this((EventsHolder.GetListDelegate) null)
  {
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return !(sourceType == typeof (string)) && base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return base.CanConvertTo(context, destinationType);
  }
}
