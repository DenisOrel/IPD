// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.UnitsConverter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Globalization;

#nullable disable
namespace Intermech.FormDesigner.Wrappers;

/// <summary>Конвертер для единиц измерения.</summary>
internal class UnitsConverter : TypeConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <param name="destinationType"></param>
  /// <returns></returns>
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    object obj = value;
    try
    {
      string str = Convert.ToString(value);
      if (!string.IsNullOrEmpty(str))
      {
        if (GuidHelper.IsGuid(str))
        {
          if (new Guid(str) != Guid.Empty)
            obj = (object) ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(new Guid(value.ToString())).Caption;
        }
      }
    }
    catch (Exception ex)
    {
    }
    return obj;
  }
}
