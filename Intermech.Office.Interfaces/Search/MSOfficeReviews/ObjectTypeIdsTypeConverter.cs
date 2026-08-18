// Decompiled with JetBrains decompiler
// Type: Intermech.Search.MSOfficeReviews.ObjectTypeIdsTypeConverter
// Assembly: Intermech.Office.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9492297C-4143-4944-80A1-CEF9501FC1B8
// Assembly location: D:\IPS\Client\Intermech.Office.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Office.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Intermech.Search.MSOfficeReviews;

public sealed class ObjectTypeIdsTypeConverter : TypeConverter
{
  public override object ConvertTo(
    ITypeDescriptorContext context,
    CultureInfo culture,
    object value,
    Type destinationType)
  {
    return value != null && destinationType == typeof (string) ? (object) string.Join(", ", ((IEnumerable<int>) (int[]) value).Distinct<int>().Select<int, string>((Func<int, string>) (o =>
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(o);
      return objectType == null ? "Неопределенный тип" : objectType.ObjectTypeName;
    }))) : base.ConvertTo(context, culture, value, destinationType);
  }
}
