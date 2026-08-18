// Decompiled with JetBrains decompiler
// Type: Intermech.SimpleFuncs
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

#nullable disable
namespace Intermech;

public class SimpleFuncs
{
  public static string GetEnumDescription(Enum value)
  {
    return SimpleFuncs.GetEnumDescription(value, false);
  }

  public static string GetEnumDescription(Enum value, bool emptyIfNotExists)
  {
    FieldInfo field = value.GetType().GetField(value.ToString());
    if (!(field != (FieldInfo) null))
      return value.ToString();
    DescriptionAttribute[] customAttributes1 = (DescriptionAttribute[]) field.GetCustomAttributes(typeof (DescriptionAttribute), false);
    if (customAttributes1.Length != 0)
      return customAttributes1[0].Description;
    DBEnum[] customAttributes2 = (DBEnum[]) value.GetType().GetCustomAttributes(typeof (DBEnum), false);
    if (customAttributes2.Length != 0)
    {
      int id = SimpleFuncs.AttributeGuidToID(customAttributes2[0].AttributeGuid);
      if (id > 0)
        return SimpleFuncs.GetAttributeValueDescription(id, Convert.ToInt64((object) value));
    }
    return !emptyIfNotExists ? value.ToString() : "";
  }

  public static KeyValuePair<Enum, string> EnumToKVP(Enum value)
  {
    return new KeyValuePair<Enum, string>(value, SimpleFuncs.GetEnumDescription(value));
  }

  public static IList EnumToList(Type type) => SimpleFuncs.EnumToList(type, (List<Enum>) null);

  public static IList EnumToList(Type type, List<Enum> skip)
  {
    ArrayList list = new ArrayList();
    foreach (Enum key in Enum.GetValues(type))
    {
      if (skip == null || !skip.Contains(key))
        list.Add((object) new KeyValuePair<Enum, string>(key, SimpleFuncs.GetEnumDescription(key)));
    }
    return (IList) list;
  }

  public static string UnitsToStr(TimeUnits units, int unitsCount)
  {
    return string.Format(LocalizationHolder.rm.GetString("Workflow.Design_143"), (object) unitsCount, (object) SimpleFuncs.GetEnumDescription((Enum) units));
  }

  public static Guid AttributeIDToGuid(int typeID)
  {
    Guid guid = Guid.Empty;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(typeID);
    if (attributeType != null)
      guid = attributeType.AttributeGuid;
    return guid;
  }

  public static int AttributeGuidToID(Guid typeGuid)
  {
    int id = 0;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(typeGuid);
    if (attributeType != null)
      id = attributeType.AttributeID;
    return id;
  }

  private static string GetAttributeValueDescription(int AttrTypeID, long value)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(AttrTypeID);
    if (attributeType != null)
    {
      int index = attributeType.PossibleValues.IndexOf((object) value);
      if (index != -1)
        return attributeType.PossibleValuesDescriptions[index].ToString();
    }
    return "";
  }

  public static object GetPropValue(object obj, string name)
  {
    string str = name;
    char[] chArray = new char[1]{ '.' };
    foreach (string name1 in str.Split(chArray))
    {
      if (obj == null)
        return (object) null;
      PropertyInfo property = obj.GetType().GetProperty(name1);
      if (property == (PropertyInfo) null)
        return (object) null;
      obj = property.GetValue(obj, (object[]) null);
    }
    return obj;
  }

  public static Type GetPropType(Type type, string name)
  {
    string str = name;
    char[] chArray = new char[1]{ '.' };
    foreach (string name1 in str.Split(chArray))
    {
      PropertyInfo property = type.GetProperty(name1);
      if (property == (PropertyInfo) null)
        return (Type) null;
      type = property.PropertyType;
    }
    return type;
  }

  public static bool In(object Value, params object[] Values)
  {
    foreach (object obj in Values)
    {
      if (obj.Equals(Value))
        return true;
    }
    return false;
  }

  public static int StringToIntDef(string s, int defValue)
  {
    try
    {
      return Convert.ToInt32(s);
    }
    catch
    {
      return defValue;
    }
  }
}
