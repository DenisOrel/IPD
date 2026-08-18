// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.TableRecordRefFlagConverter
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

public class TableRecordRefFlagConverter : TypeConverter
{
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return base.ConvertFrom(context, culture, value);
  }

  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    if (value != null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
        {
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          List<string> keyValues = new List<string>((IEnumerable<string>) new string[1]
          {
            value.ToString()
          });
          Dictionary<string, string> dictionary = customService.NameRecordReferences(sessionGuid, keyValues);
          if (dictionary != null)
          {
            using (Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator())
            {
              if (enumerator.MoveNext())
                value = (object) enumerator.Current.Value;
            }
          }
        }
      }
    }
    return value;
  }
}
