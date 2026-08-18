// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.LdapTypeConverter
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class LdapTypeConverter : TypeConverter
{
  private static TypeConverter.StandardValuesCollection values;

  public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
  {
    return sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);
  }

  public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
  {
    return destinationType == typeof (string) || base.CanConvertTo(context, destinationType);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return (object) value.ToString();
  }

  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return (object) value.ToString();
  }

  public override TypeConverter.StandardValuesCollection GetStandardValues(
    ITypeDescriptorContext context)
  {
    if (LdapTypeConverter.values == null)
    {
      DomainCollection domains = Forest.GetCurrentForest().Domains;
      List<string> stringList = new List<string>();
      foreach (Domain domain in (ReadOnlyCollectionBase) domains)
        stringList.Add(domain.Name);
      LdapTypeConverter.values = new TypeConverter.StandardValuesCollection((ICollection) stringList.ToArray());
    }
    return LdapTypeConverter.values;
  }

  public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => false;

  public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => true;
}
