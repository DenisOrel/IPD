// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Scripts.LCStepScriptConverter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.DatabaseConfigurator.Scripts;

internal class LCStepScriptConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (!(value is LCStepScriptValue lcStepScriptValue))
      return (object) null;
    return lcStepScriptValue.NewScriptId.HasValue ? (object) lcStepScriptValue.NewScriptName : (object) lcStepScriptValue.ScriptName;
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return base.CanConvertTo(context, destinationType);
  }

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return base.CanConvertFrom(context, sourceType);
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return base.ConvertFrom(context, culture, value);
  }
}
