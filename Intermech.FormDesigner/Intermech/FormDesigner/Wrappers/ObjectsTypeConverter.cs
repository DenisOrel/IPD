// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Wrappers.ObjectsTypeConverter
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

/// <summary>Конвертер для свойства "Тип".</summary>
public class ObjectsTypeConverter : TypeConverter
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="context"></param>
  /// <param name="culture"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  public override object ConvertFrom(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value)
  {
    return base.ConvertFrom(context, culture, value);
  }

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
    if (value != null && value != DBNull.Value)
    {
      string str = Convert.ToString(value);
      if (GuidHelper.IsGuid(str))
      {
        Guid objTypeGuid = new Guid(str);
        if (objTypeGuid == Guid.Empty)
        {
          obj = (object) string.Empty;
        }
        else
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeGuid);
          obj = objectType != null ? (object) objectType.ObjectTypeName : (object) string.Empty;
        }
      }
      else
      {
        int result = -1;
        if (int.TryParse(str, out result))
        {
          if (result == -1)
          {
            obj = (object) string.Empty;
          }
          else
          {
            IMSObjectType objectType = MetaDataHelper.GetObjectType(result);
            obj = objectType != null ? (object) objectType.ObjectTypeName : (object) string.Empty;
          }
        }
        else
          obj = base.ConvertTo(context, culture, value, destinationType);
      }
    }
    return obj;
  }
}
