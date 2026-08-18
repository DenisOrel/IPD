// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AttributeValuesCache
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

[Serializable]
public class AttributeValuesCache : AttributeValueMap
{
  private ArrayList Values;

  /// <summary>Идентификатор объекта</summary>
  public virtual long F_ID
  {
    get
    {
      object obj = this.GetValue(-3, false);
      switch (obj)
      {
        case null:
        case DBNull _:
          return -1;
        default:
          return Convert.ToInt64(obj.ToString());
      }
    }
  }

  /// <summary>Идентификатор версии объекта</summary>
  public virtual long ObjectId
  {
    get
    {
      if (this.idIndex == -1)
        this.idIndex = this.GetValueIndex(-2);
      if (this.idIndex != -1)
      {
        object valueByIndex = this.GetValueByIndex(this.idIndex);
        switch (valueByIndex)
        {
          case null:
          case DBNull _:
            break;
          default:
            return Convert.ToInt64(valueByIndex.ToString());
        }
      }
      return -1;
    }
  }

  public virtual string ObjectCaption => Convert.ToString(this.GetValue(-50, false));

  public virtual Guid ObjectGuid
  {
    get
    {
      object obj = this.GetValue(-12, false);
      return obj != null ? new Guid(obj.ToString()) : Guid.Empty;
    }
  }

  public virtual int ObjectType
  {
    get
    {
      object obj = this.GetValue(-7, false);
      return obj != null ? Convert.ToInt32(obj.ToString()) : -1;
    }
  }

  protected AttributeValuesCache()
  {
  }

  public AttributeValuesCache(
    Dictionary<int, int> attributeDictionary,
    List<AvsRowAttributeInfo> attrInfo,
    ArrayList values)
    : base(attributeDictionary, attrInfo)
  {
    this.Values = values;
  }

  public AttributeValuesCache(
    Dictionary<int, int> attributeDictionary,
    List<AvsRowAttributeInfo> attrInfo)
    : base(attributeDictionary, attrInfo)
  {
    if (attrInfo == null)
      return;
    this.Values = new ArrayList((ICollection) new object[attrInfo.Count]);
  }

  public void SetObjectID(
    long f_ID,
    long objectID,
    Guid objectGuid,
    int objectType,
    string objectCaption)
  {
    this.SetValue(-3, (object) f_ID, false);
    this.SetValue(-2, (object) objectID, false);
    this.SetValue(-12, (object) objectGuid.ToString(), false);
    this.SetValue(-7, (object) objectType, false);
    this.SetValue(-50, (object) objectCaption, false);
  }

  public object GetValue(
    ObligatoryObjectAttributes attributeID,
    bool failIfNotFound,
    bool replaceDBNull = false)
  {
    return this.GetValue((int) attributeID, failIfNotFound, replaceDBNull);
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attr">Информация об атрибуте</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="replaceDBNull">Заменять DBNull на null</param>
  /// <returns></returns>
  public object GetValue(AvsRowAttributeInfo attr, bool failIfNotFound, bool replaceDBNull = false)
  {
    object obj = (object) null;
    attr.IndexInValueList = this.GetUpdatedValueIndex(attr.AttributeId, attr.IndexInValueList);
    if (attr.IndexInValueList != -1)
      obj = this.Values[attr.IndexInValueList];
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attr.AttributeId} объекта или связи не найден");
    if (replaceDBNull && obj is DBNull)
      obj = (object) null;
    return obj;
  }

  public object GetValue(int attributeID, bool failIfNotFound, bool replaceDBNull = false)
  {
    object obj = (object) null;
    int valueIndex = this.GetValueIndex(attributeID);
    if (valueIndex != -1)
      obj = this.GetValueByIndex(valueIndex);
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID} объекта или связи не найден");
    if (replaceDBNull && obj is DBNull)
      obj = (object) null;
    return obj;
  }

  public long GetValueInt64(AvsRowAttributeInfo attr, bool failIfNotFound, long defaultValue = -1)
  {
    return AvsIDCache.ConvertDbValueToInt64(this.GetValue(attr, failIfNotFound, true), defaultValue);
  }

  public long GetValueInt64(int attributeID, bool failIfNotFound, long defaultValue = -1)
  {
    return AvsIDCache.ConvertDbValueToInt64(this.GetValue(attributeID, failIfNotFound, true), defaultValue);
  }

  public int GetValueInt32(AvsRowAttributeInfo attr, bool failIfNotFound, int defaultValue = -1)
  {
    return AvsIDCache.ConvertDbValueToInt32(this.GetValue(attr, failIfNotFound, true), defaultValue);
  }

  public int GetValueInt32(int attributeID, bool failIfNotFound, int defaultValue = -1)
  {
    return AvsIDCache.ConvertDbValueToInt32(this.GetValue(attributeID, failIfNotFound, true), defaultValue);
  }

  public string GetValueString(AvsRowAttributeInfo attr, bool failIfNotFound, string defaultValue = "")
  {
    object obj = this.GetValue(attr, failIfNotFound, true);
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      case AVSObjectInfo avsObjectInfo:
        return avsObjectInfo.Text;
      default:
        return Convert.ToString(obj);
    }
  }

  public string GetValueString(int attributeID, bool failIfNotFound, string defaultValue = "")
  {
    object obj = this.GetValue(attributeID, failIfNotFound, true);
    switch (obj)
    {
      case null:
      case DBNull _:
        return defaultValue;
      case AVSObjectInfo avsObjectInfo:
        return avsObjectInfo.Text;
      default:
        return Convert.ToString(obj);
    }
  }

  public Guid GetValueGuid(AvsRowAttributeInfo attr, bool failIfNotFound)
  {
    return this.ConvertToGuid(this.GetValue(attr, failIfNotFound, true));
  }

  public Guid GetValueGuid(int attributeID, bool failIfNotFound)
  {
    return this.ConvertToGuid(this.GetValue(attributeID, failIfNotFound, true));
  }

  private Guid ConvertToGuid(object value)
  {
    Guid guid = Guid.Empty;
    if (value != null)
    {
      string g = Convert.ToString(value);
      if (!string.IsNullOrWhiteSpace(g))
        guid = new Guid(g);
    }
    return guid;
  }

  public bool GetValueBool(AvsRowAttributeInfo attr, bool failIfNotFound, bool defaultValue = false)
  {
    return AttributeValuesCache.ConvertToBool(this.GetValue(attr, failIfNotFound, true));
  }

  public bool GetValueBool(int attributeID, bool failIfNotFound, bool defaultValue = false)
  {
    return AttributeValuesCache.ConvertToBool(this.GetValue(attributeID, failIfNotFound, true));
  }

  internal static bool ConvertToBool(object value, bool defaultValue = false)
  {
    if (value == null)
      return defaultValue;
    if (value is string str)
    {
      if (str == "")
        return defaultValue;
      switch (str.ToUpper())
      {
        case "ДА":
        case "TRUE":
          return true;
        case "НЕТ":
        case "FALSE":
          return false;
      }
    }
    return Convert.ToBoolean(value);
  }

  internal object GetValueByIndex(int valueIndex)
  {
    return this.GetValue(this.AttrsInfo[valueIndex], true);
  }

  public void SetValue(
    ObligatoryObjectAttributes attributeID,
    object attrValue,
    bool failIfNotFound)
  {
    this.SetValue((int) attributeID, attrValue, failIfNotFound);
  }

  public void SetValue(AvsRowAttributeInfo attr, object attrValue, bool failIfNotFound)
  {
    attr.IndexInValueList = this.GetUpdatedValueIndex(attr.AttributeId, attr.IndexInValueList);
    if (attr.IndexInValueList != -1)
      this.Values[attr.IndexInValueList] = attrValue;
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attr.AttributeId.ToString()} объекта или связи не найден");
  }

  public void SetValue(int attributeID, object attrValue, bool failIfNotFound)
  {
    int valueIndex = this.GetValueIndex(attributeID);
    if (valueIndex != -1)
      this.SetValue(this.AttrsInfo[valueIndex], attrValue, true);
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  public void SetObjectID(int attributeID, object id, bool failIfNotFound)
  {
    AvsRowAttributeInfo attributeInfo = this.GetAttributeInfo(attributeID);
    if (attributeInfo != null)
    {
      long int64 = AvsIDCache.ConvertDbValueToInt64(id);
      object attrValue = this.GetValue(attributeInfo, true);
      if (attrValue == null)
      {
        attrValue = (object) new AVSObjectInfo();
        this.SetValue(attributeInfo, attrValue, true);
      }
      ((AVSObjectInfo) attrValue).Id = int64;
    }
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  public void SetObjectText(int attributeID, string text, bool failIfNotFound)
  {
    AvsRowAttributeInfo attributeInfo = this.GetAttributeInfo(attributeID);
    if (attributeInfo != null)
    {
      object attrValue = this.GetValue(attributeInfo, true);
      if (attrValue == null)
      {
        attrValue = (object) new AVSObjectInfo();
        this.SetValue(attributeInfo, attrValue, true);
      }
      (attrValue as AVSObjectInfo).Text = text;
    }
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  public void SetMeasureID(int attributeID, object value, bool failIfNotFound)
  {
    long int64 = AvsIDCache.ConvertDbValueToInt64(value);
    if (int64.IsDefinedId())
      this.SetMeasureID(attributeID, int64, failIfNotFound);
    else
      this.SetValue(attributeID, value, failIfNotFound);
  }

  public void SetMeasureID(int attributeID, long measureID, bool failIfNotFound)
  {
    int valueIndex = this.GetValueIndex(attributeID);
    if (valueIndex != -1)
    {
      double aValue = 0.0;
      object valueByIndex = this.GetValueByIndex(valueIndex);
      switch (valueByIndex)
      {
        case double _:
        case Decimal _:
          aValue = Convert.ToDouble(valueByIndex);
          break;
      }
      this.SetValue(this.AttrsInfo[valueIndex], (object) new MeasuredValue(aValue, measureID), true);
    }
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  public void SetMeasuredValue(int attributeID, double value, bool failIfNotFound)
  {
    AvsRowAttributeInfo attributeInfo = this.GetAttributeInfo(attributeID);
    if (attributeInfo != null)
    {
      long measureID = -1;
      object obj = this.GetValue(attributeInfo, true);
      switch (obj)
      {
        case long _:
        case int _:
          measureID = (long) obj;
          break;
        case Decimal _:
        case string _:
          measureID = Convert.ToInt64(obj);
          break;
      }
      MeasuredValue attrValue = (MeasuredValue) null;
      if (obj != null && obj is MeasuredValue)
        attrValue = (MeasuredValue) obj;
      if (attrValue == null)
      {
        attrValue = new MeasuredValue(value, measureID);
      }
      else
      {
        attrValue.Value = value;
        attrValue.Caption = (string) null;
      }
      this.SetValue(attributeInfo, (object) attrValue, true);
    }
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  public void SetMeasuredValue(int attributeID, object value, bool failIfNotFound)
  {
    switch (value)
    {
      case null:
      case DBNull _:
        this.SetValue(attributeID, value, failIfNotFound);
        break;
      default:
        double result;
        bool flag = double.TryParse(value.ToString(), out result);
        if (value is double | flag)
        {
          int attributeID1 = attributeID;
          if (!(value is double num1))
            num1 = result;
          int num2 = failIfNotFound ? 1 : 0;
          this.SetMeasuredValue(attributeID1, num1, num2 != 0);
          break;
        }
        if (!(value is string))
          throw new ArgumentException("value может быть только типа Double или String", nameof (value));
        this.SetMeasuredValue(attributeID, (string) value, failIfNotFound);
        break;
    }
  }

  public void SetMeasuredValueCaption(
    int attributeID,
    string caption,
    bool exceptionIfNotFound,
    bool exceptionIfConvertFail)
  {
    AvsRowAttributeInfo attributeInfo = this.GetAttributeInfo(attributeID);
    if (attributeInfo != null)
    {
      if (!(this.GetValue(attributeInfo, true) is MeasuredValue measuredValue))
        this.SetValue(attributeInfo, (object) AVSRow.ConvertCountToMeasuredValue((object) caption, exceptionIfConvertFail), true);
      else
        measuredValue.Caption = caption;
    }
    else if (exceptionIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  public void SetMeasuredValue(int attributeID, string strValue, bool failIfNotFound)
  {
    AvsRowAttributeInfo attributeInfo = this.GetAttributeInfo(attributeID);
    if (attributeInfo != null)
      this.SetValue(attributeInfo, (object) AVSRow.ConvertCountToMeasuredValue((object) strValue), true);
    else if (failIfNotFound)
      throw new Exception($"Атрибут {attributeID.ToString()} объекта или связи не найден");
  }

  internal void UpdateValuesForAttrsInfoCount()
  {
    int count = this.AttrsInfo.Count - this.Values.Count;
    if (count > 0)
    {
      this.Values.AddRange((ICollection) new object[count]);
    }
    else
    {
      if (count >= 0)
        return;
      this.Values.RemoveRange(this.Values.Count - count, count);
    }
  }

  public override AttributeValueMap Clone()
  {
    AttributeValuesCache attributeValuesCache = (AttributeValuesCache) base.Clone();
    attributeValuesCache.Values = (ArrayList) this.Values.Clone();
    return (AttributeValueMap) attributeValuesCache;
  }
}
