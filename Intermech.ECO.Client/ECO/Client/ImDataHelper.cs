// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ImDataHelper
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Extensions;
using Intermech.Kernel.Search;
using System;
using System.Data;

#nullable disable
namespace Intermech.ECO.Client;

public static class ImDataHelper
{
  public static ColumnDescriptor MakeDescriptor(object attributeID, ColumnNameMapping nameMapping = ColumnNameMapping.Default)
  {
    if (nameMapping == ColumnNameMapping.Default)
    {
      switch (attributeID)
      {
        case Enum _:
          nameMapping = ColumnNameMapping.FieldName;
          break;
        case int _:
          nameMapping = ColumnNameMapping.ID;
          break;
      }
    }
    return new ColumnDescriptor(attributeID, AttributeSourceTypes.Auto, ColumnContents.Text, nameMapping, SortOrders.NONE, 1);
  }

  public static T GetField<T>(this DataRow row, object columnName)
  {
    string columnName1 = columnName is Enum ? ((Enum) columnName).GetName() : columnName.ToString();
    object input = row[columnName1];
    if (input == null || input == DBNull.Value)
      return default (T);
    Type type1 = typeof (T);
    Type underlyingType = Nullable.GetUnderlyingType(type1);
    Type type2 = underlyingType;
    if ((object) type2 == null)
      type2 = type1;
    Type enumType = type2;
    if (enumType.IsEnum)
    {
      try
      {
        T obj = (T) Enum.Parse(enumType, input.ToString());
        return !Enum.IsDefined(enumType, (object) obj) ? default (T) : obj;
      }
      catch (ArgumentException ex)
      {
      }
      return default (T);
    }
    if (enumType == typeof (Guid) && input.GetType() == typeof (string))
    {
      Guid result;
      Guid.TryParse((string) input, out result);
      return (T) (System.ValueType) result;
    }
    return underlyingType != (Type) null ? (T) Convert.ChangeType(input, underlyingType) : (T) Convert.ChangeType(input, type1);
  }
}
