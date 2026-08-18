// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.UnitsConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase;

internal class UnitsConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    object obj = value;
    if (value != null)
    {
      if (value != DBNull.Value)
      {
        try
        {
          string str = value.ToString();
          if (!string.IsNullOrEmpty(str))
          {
            if (GuidHelper.IsGuid(str))
            {
              if (new Guid(str) != Guid.Empty)
              {
                using (SessionKeeper sessionKeeper = new SessionKeeper())
                  obj = (object) sessionKeeper.Session.GetObjectInfo(new Guid(value.ToString())).Caption;
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
      }
    }
    return obj;
  }
}
