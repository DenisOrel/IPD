// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.BaseCompareState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal abstract class BaseCompareState : IAttributeAnalyzerState
{
  public virtual void Handle(SynchronizationAttributesAnalyzer context)
  {
  }

  protected void CompareWithRow(SynchronizationAttributesAnalyzer context, DataRow row)
  {
    List<AttributeValues> linksToRec = new List<AttributeValues>();
    List<AttributeValues> linksToObj = new List<AttributeValues>();
    foreach ((_, _, _) in this.GetAttributeValuesForRow(row))
    {
      (int, object, long) rowAttrVal;
      IMSAttribute4ObjectType imsAttr = context.ComparedAttributes.FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == rowAttrVal.Item1));
      try
      {
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(rowAttrVal.Item1);
        if (attributeType == null)
        {
          if (imsAttr != null)
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_BadAttr"));
        }
        else
        {
          if (imsAttr != null)
          {
            if (rowAttrVal.Item2 != null && rowAttrVal.Item2 != DBNull.Value)
            {
              AttributeValues attributeValues = this.AnalyzeValueFromRow(context, imsAttr, rowAttrVal.Item2, rowAttrVal.Item3);
              if (attributeValues != null)
                context.DifferentAttributeValues.Add(attributeValues);
            }
            context.ComparedAttributes.Remove(imsAttr);
            if (context.FinishAnalyze)
              return;
          }
          this.FillLinkList(context, attributeType, new AttributeValues(rowAttrVal.Item1, rowAttrVal.Item2), linksToRec, linksToObj);
        }
      }
      catch (ApplicationException ex)
      {
        context.Log.AddMessage(MessageType.Extended, ex.Message);
        context.ComparedAttributes.Remove(imsAttr);
      }
    }
    this.ProcessLinksToRecs(context, linksToRec);
    this.ProcessLinksToObjs(context, linksToObj);
  }

  protected void CompareWithObject(SynchronizationAttributesAnalyzer context, long objId)
  {
    List<AttributeValues> linksToRec = new List<AttributeValues>();
    List<AttributeValues> linksToObj = new List<AttributeValues>();
    List<AttributeValues> attributeValuesForObject = this.GetAttributeValuesForObject(context, objId);
    if (attributeValuesForObject == null)
      return;
    foreach (AttributeValues attributeValues1 in attributeValuesForObject)
    {
      AttributeValues objAV = attributeValues1;
      if (objAV.Values.Length != 1 || !string.IsNullOrEmpty(Convert.ToString(objAV.Values[0])))
      {
        IMSAttribute4ObjectType attribute4ObjectType = context.ComparedAttributes.FirstOrDefault<IMSAttribute4ObjectType>((System.Func<IMSAttribute4ObjectType, bool>) (x => x.AttributeID == objAV.AttributeID));
        try
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(objAV.AttributeID);
          if (attributeType == null)
          {
            if (attribute4ObjectType != null)
              throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_BadAttr"));
          }
          else
          {
            if (attribute4ObjectType != null && !attribute4ObjectType.Options.HasFlag((Enum) AttributeOptions.DontCopyPrototypeValue))
            {
              AttributeValues attributeValues2 = this.AnalyzeValueFromObject(context, objAV);
              if (attributeValues2 != null)
                context.DifferentAttributeValues.Add(attributeValues2);
              context.ComparedAttributes.Remove(attribute4ObjectType);
              if (context.FinishAnalyze)
                return;
            }
            this.FillLinkList(context, attributeType, objAV, linksToRec, linksToObj);
          }
        }
        catch (ApplicationException ex)
        {
          context.Log.AddMessage(MessageType.Extended, ex.Message);
          context.ComparedAttributes.Remove(attribute4ObjectType);
        }
      }
    }
    this.ProcessLinksToRecs(context, linksToRec);
    this.ProcessLinksToObjs(context, linksToObj);
  }

  private void FillLinkList(
    SynchronizationAttributesAnalyzer context,
    IMSAttributeType attrType,
    AttributeValues objAV,
    List<AttributeValues> linksToRec,
    List<AttributeValues> linksToObj)
  {
    if (attrType.MultiValueMode != MultiValueModes.SingleValue && attrType.MultiValueMode != MultiValueModes.SingleValueFromList || (attrType.FieldType != FieldTypes.ftString || !attrType.Options.HasFlag((Enum) AttributeOptions.ImbaseFlag_TableRecordRef)) && attrType.FieldType != FieldTypes.ftObjectLink)
      return;
    if (context.NotExpandableAttributes.Contains(attrType.AttributeID))
      context.Log.AddMessage(MessageType.Extended, $"Ссылка по атрибуту {attrType.Name} [{attrType.AttributeID}] не будет обработана, т.е. атрибут находится в списке игнорируемых ссылочных атрибутов.");
    else if (attrType.FieldType == FieldTypes.ftString && attrType.Options.HasFlag((Enum) AttributeOptions.ImbaseFlag_TableRecordRef))
    {
      linksToRec.Add(objAV);
    }
    else
    {
      if (attrType.FieldType != FieldTypes.ftObjectLink)
        return;
      linksToObj.Add(objAV);
    }
  }

  private AttributeValues AnalyzeValueFromRow(
    SynchronizationAttributesAnalyzer context,
    IMSAttribute4ObjectType imsAttr,
    object rowValue,
    long measureId)
  {
    AttributeValues objAV = context.SourceAttributeValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == imsAttr.AttributeID));
    AttributeValues attributeValueFromRow;
    if (objAV != null)
    {
      attributeValueFromRow = this.GetAttributeValueFromRow(context.Session, objAV, rowValue, measureId);
    }
    else
    {
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(imsAttr.AttributeID);
      objAV = new AttributeValues(imsAttr.AttributeID, attributeType.FieldType, attributeType.MultiValueMode, new object[1]
      {
        (object) DBNull.Value
      })
      {
        AttributeName = attributeType.Name
      };
      attributeValueFromRow = this.GetAttributeValueFromRow(context.Session, objAV, rowValue, measureId);
    }
    if (attributeValueFromRow != null)
      context.Log.AddMessage(MessageType.Extended, $" - значение атрибута '{attributeValueFromRow.AttributeName}' отличается: '{objAV?.AsString}' => '{attributeValueFromRow.AsString}'");
    return attributeValueFromRow;
  }

  private AttributeValues AnalyzeValueFromObject(
    SynchronizationAttributesAnalyzer context,
    AttributeValues imbaseAV)
  {
    AttributeValues objAV = context.SourceAttributeValues.FirstOrDefault<AttributeValues>((System.Func<AttributeValues, bool>) (x => x.AttributeID == imbaseAV.AttributeID));
    AttributeValues attributeValues = objAV != null ? this.GetAttributeValue(objAV, imbaseAV) : imbaseAV;
    if (attributeValues != null)
      context.Log.AddMessage(MessageType.Extended, $" - значение атрибута '{attributeValues.AttributeName}' отличается: '{objAV?.AsString}' => '{attributeValues.AsString}'");
    return attributeValues;
  }

  private AttributeValues GetAttributeValueFromRow(
    IUserSession session,
    AttributeValues objAV,
    object imbaseValue,
    long measureId)
  {
    AttributeValues attributeValueFromRow = (AttributeValues) null;
    object[] objArray1 = (object[]) null;
    if (objAV.MultipleValued == MultiValueModes.SingleValue || objAV.MultipleValued == MultiValueModes.SingleValueFromList)
    {
      string str = Convert.ToString(imbaseValue);
      if (!string.IsNullOrEmpty(str))
      {
        if (objAV.AttributeType == FieldTypes.ftMeasured)
        {
          MeasuredValue measuredValueFromImbase = this.GetMeasuredValueFromImbase(str, measureId);
          if (measuredValueFromImbase == null)
            throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_Convert_FromImbaseRecToMeasuredValue_Error"), (object) objAV.AttributeName, (object) objAV.AttributeID.ToString()));
          object[] objArray2;
          if (objAV.Values[0] is MeasuredValue val1 && MeasureHelper.Compare(val1, measuredValueFromImbase) == CompareResult.Equal && !(val1.Caption != measuredValueFromImbase.Caption))
            objArray2 = (object[]) null;
          else
            objArray2 = new object[1]
            {
              (object) measuredValueFromImbase
            };
          objArray1 = objArray2;
        }
        else if (objAV.AttributeType == FieldTypes.ftObjectLink)
        {
          long result1;
          if (!long.TryParse(Convert.ToString(objAV.Values[0]), out result1))
            result1 = 0L;
          long result2;
          if (GuidHelper.IsGuid(str))
          {
            Guid objectGUID = new Guid(str);
            QuickObjectInfo objectInfo = session.GetObjectInfo(objectGUID);
            result2 = !objectInfo.Empty ? objectInfo.ObjectID : throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_Attr_LinkToObj_BadObj"), (object) objectGUID.ToString()));
          }
          else if (!long.TryParse(str, out result2))
            throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_Convert_FromImbaseRecToObjID_Error"), (object) objAV.AttributeName, (object) objAV.AttributeID.ToString()));
          object[] objArray3;
          if (result1 == result2)
            objArray3 = (object[]) null;
          else
            objArray3 = new object[1]{ (object) result2 };
          objArray1 = objArray3;
        }
        else
        {
          object[] objArray4;
          if (!(Convert.ToString(objAV.Values[0]) != str))
            objArray4 = (object[]) null;
          else
            objArray4 = new object[1]{ imbaseValue };
          objArray1 = objArray4;
        }
      }
    }
    else
    {
      object[] imbaseValues = imbaseValue is ValuesArray valuesArray ? valuesArray.GetArray() : throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_Convert_FromImbaseRecToArrayValue_Error"), (object) objAV.AttributeName, (object) objAV.AttributeID.ToString()));
      if (imbaseValues.Length != 0)
      {
        if (objAV.AttributeType == FieldTypes.ftMeasured)
        {
          try
          {
            objArray1 = this.GetDiffMeasuredArray(objAV.Values, imbaseValues, measureId);
          }
          catch (ApplicationException ex)
          {
            throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_Convert_FromImbaseRecToListMeasuredValues_Error"), (object) objAV.AttributeName, (object) objAV.AttributeID.ToString()));
          }
        }
        else if (objAV.AttributeType == FieldTypes.ftObjectLink)
        {
          try
          {
            objArray1 = this.GetDiffLinkArray(session, objAV.Values, imbaseValues);
          }
          catch (ApplicationException ex)
          {
            throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_Convert_FromImbaseRecToListObjIDs_Error"), (object) objAV.AttributeName, (object) objAV.AttributeID.ToString()));
          }
        }
        else
          objArray1 = this.GetDiffObjectArray(objAV.Values, imbaseValues);
      }
    }
    if (objArray1 != null)
    {
      attributeValueFromRow = (AttributeValues) objAV.Clone();
      attributeValueFromRow.Values = objArray1;
    }
    return attributeValueFromRow;
  }

  private (int AttributeID, object AttributeValue, long MeasureId)[] GetAttributeValuesForRow(
    DataRow row)
  {
    return row.Table.Columns.Cast<DataColumn>().AsEnumerable<DataColumn>().Select<DataColumn, (int, object, long, bool)>((System.Func<DataColumn, (int, object, long, bool)>) (x =>
    {
      int result;
      bool flag = int.TryParse(x.Caption, out result) && result > 0 && MetaDataHelper.GetAttributeType(result) != null;
      object obj = row[x];
      long num = x.ExtendedProperties.ContainsKey((object) "F_MEASURE") ? Convert.ToInt64(x.ExtendedProperties[(object) "F_MEASURE"]) : -1L;
      return (result, obj, num, flag);
    })).Where<(int, object, long, bool)>((System.Func<(int, object, long, bool), bool>) (x => x.Valid)).Select<(int, object, long, bool), (int, object, long)>((System.Func<(int, object, long, bool), (int, object, long)>) (x => (x.AttributeID, x.AttributeValue, x.MeasureId))).ToArray<(int, object, long)>();
  }

  private List<AttributeValues> GetAttributeValuesForObject(
    SynchronizationAttributesAnalyzer context,
    long objID)
  {
    List<AttributeValues> attributeValuesForObject = (List<AttributeValues>) null;
    IDBObject objectActualCopy = context.Session.GetObjectActualCopy(Math.Abs(objID), false);
    if (objectActualCopy != null)
    {
      List<AttributeValues> list = ((IEnumerable<AttributeValues>) objectActualCopy.GetAttributesValues(context.AttributeValuesModes)).ToList<AttributeValues>();
      if (list.Count > 0)
      {
        attributeValuesForObject = new List<AttributeValues>(list.Count);
        int modifyContentDateId = context.Session.IdentHelper.ModifyContentDateID;
        foreach (AttributeValues attributeValues in list)
        {
          int attributeId = attributeValues.AttributeID;
          if (attributeId != modifyContentDateId && !ImbaseHelper.IsSystemAttribute(attributeId) && !ImbaseHelper.SkipAtttribute(attributeId))
            attributeValuesForObject.Add(attributeValues);
        }
      }
    }
    return attributeValuesForObject;
  }

  private AttributeValues GetAttributeValue(AttributeValues objAV, AttributeValues imbaseAV)
  {
    AttributeValues attributeValue = (AttributeValues) null;
    if (objAV.MultipleValued == MultiValueModes.SingleValue || objAV.MultipleValued == MultiValueModes.SingleValueFromList)
    {
      if (objAV.AttributeType == FieldTypes.ftMeasured)
      {
        MeasuredValue val1 = objAV.Values[0] as MeasuredValue;
        MeasuredValue val2 = imbaseAV.Values[0] as MeasuredValue;
        attributeValue = val1 == null || MeasureHelper.Compare(val1, val2) != CompareResult.Equal ? imbaseAV : (AttributeValues) null;
      }
      else
        attributeValue = !AttributeValues.ValueEquals(objAV.Values[0], imbaseAV.Values[0]) ? imbaseAV : (AttributeValues) null;
    }
    else if (objAV.Values.Length == imbaseAV.Values.Length)
    {
      if (objAV.AttributeType == FieldTypes.ftMeasured)
      {
        for (int index = 0; index < imbaseAV.Values.Length; ++index)
        {
          MeasuredValue val1 = objAV.Values[index] as MeasuredValue;
          MeasuredValue val2 = imbaseAV.Values[index] as MeasuredValue;
          if ((val1 != null || val2 != null) && (val1 == null || val2 == null || MeasureHelper.Compare(val1, val2) != CompareResult.Equal))
          {
            attributeValue = imbaseAV;
            break;
          }
        }
      }
      else
      {
        for (int index = 0; index < imbaseAV.Values.Length; ++index)
        {
          if (!AttributeValues.ValueEquals(objAV.Values[index], imbaseAV.Values[index]))
          {
            attributeValue = imbaseAV;
            break;
          }
        }
      }
    }
    else
      attributeValue = imbaseAV;
    return attributeValue;
  }

  private object[] GetDiffMeasuredArray(object[] objValues, object[] imbaseValues, long measureId)
  {
    object[] diffMeasuredArray = (object[]) null;
    List<MeasuredValue> valuesFromImbase = this.GetMeasuredValuesFromImbase(imbaseValues, measureId);
    if (valuesFromImbase == null)
      throw new ApplicationException();
    List<MeasuredValue> list = ((IEnumerable<object>) objValues).Select<object, MeasuredValue>((System.Func<object, MeasuredValue>) (x => x as MeasuredValue)).ToList<MeasuredValue>();
    if (list.Count == valuesFromImbase.Count)
    {
      for (int index = 0; index < valuesFromImbase.Count; ++index)
      {
        if (!list[index].Equals(valuesFromImbase[index]) && (list[index] == null || valuesFromImbase[index] == null || MeasureHelper.Compare(list[index], valuesFromImbase[index]) != CompareResult.Equal))
        {
          diffMeasuredArray = (object[]) valuesFromImbase.ToArray();
          break;
        }
      }
    }
    else
      diffMeasuredArray = (object[]) valuesFromImbase.ToArray();
    return diffMeasuredArray;
  }

  private object[] GetDiffLinkArray(
    IUserSession session,
    object[] objValues,
    object[] imbaseValues)
  {
    object[] diffLinkArray = (object[]) null;
    List<long> longList1 = new List<long>();
    foreach (object imbaseValue in imbaseValues)
    {
      string str = Convert.ToString(imbaseValue);
      if (GuidHelper.IsGuid(str))
      {
        QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid(str));
        longList1.Add(!objectInfo.Empty ? objectInfo.ObjectID : 0L);
      }
      else
      {
        long result;
        longList1.Add(long.TryParse(str, out result) ? result : 0L);
      }
    }
    List<long> longList2 = new List<long>();
    foreach (object objValue in objValues)
    {
      long result;
      longList2.Add(long.TryParse(Convert.ToString(objValue), out result) ? result : 0L);
    }
    bool flag = false;
    if (longList2.Count == longList1.Count)
    {
      for (int index = 0; index < imbaseValues.Length; ++index)
      {
        if (longList2[index] != longList1[index])
        {
          flag = true;
          break;
        }
      }
    }
    else
      flag = true;
    if (flag)
    {
      diffLinkArray = new object[longList1.Count];
      for (int index = 0; index < longList1.Count; ++index)
        diffLinkArray[index] = longList1[index] == 0L ? (object) DBNull.Value : (object) longList1[index];
    }
    return diffLinkArray;
  }

  private object[] GetDiffObjectArray(object[] objValues, object[] imbaseValues)
  {
    object[] diffObjectArray = (object[]) null;
    if (objValues.Length == imbaseValues.Length)
    {
      for (int index = 0; index < imbaseValues.Length; ++index)
      {
        if (!(Convert.ToString(objValues[index]) == Convert.ToString(imbaseValues[index])))
        {
          diffObjectArray = imbaseValues;
          break;
        }
      }
    }
    else
      diffObjectArray = imbaseValues;
    return diffObjectArray;
  }

  private MeasuredValue GetMeasuredValueFromImbase(string imbaseValue, long measureId)
  {
    MeasuredValue measuredValueFromImbase = (MeasuredValue) null;
    double result;
    if (measureId > -1L && double.TryParse(imbaseValue, out result))
      measuredValueFromImbase = new MeasuredValue(result, measureId);
    return measuredValueFromImbase;
  }

  private List<MeasuredValue> GetMeasuredValuesFromImbase(object[] imbaseValues, long measureId)
  {
    List<MeasuredValue> measuredValueList = new List<MeasuredValue>(imbaseValues.Length);
    if (measureId > 0L)
    {
      foreach (object imbaseValue in imbaseValues)
      {
        double result;
        measuredValueList.Add(double.TryParse(Convert.ToString(imbaseValue), out result) ? new MeasuredValue(result, measureId) : (MeasuredValue) null);
      }
    }
    return measuredValueList.Count <= 0 ? (List<MeasuredValue>) null : measuredValueList;
  }

  private void ProcessLinksToRecs(
    SynchronizationAttributesAnalyzer context,
    List<AttributeValues> linksToRec)
  {
    if (context.FinishAnalyze || !linksToRec.Any<AttributeValues>())
      return;
    context.Log.AddMessage(MessageType.Extended, "Обработка ссылочных атрибутов на строку таблицы Imbase");
    foreach (AttributeValues attributeValues in linksToRec)
    {
      AttributeValues linkRec = attributeValues;
      try
      {
        string keyValue = Convert.ToString(linkRec.Value);
        if (!context.ProcessedLinksToRecs.Contains(keyValue))
        {
          context.ProcessedLinksToRecs.Add(keyValue);
          long linkId;
          long recordId;
          if (!ImbaseHelper.TryParseRecordReference(context.Session, keyValue, out linkId, out recordId))
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjID_And_RecID_Error"));
          DataRow recordRow = ImbaseServer.GetRecordRow(context.Session, linkId, recordId, false);
          if (recordRow == null)
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_TableRec_Removed"));
          IDBObject dbObject = context.Session.GetObject(linkId);
          context.Log.AddMessage(MessageType.Extended, $"Анализ атрибутов записи № {recordId} объекта {dbObject.NameInMessages} [{dbObject.ObjectID}] ");
          this.CompareWithRow(context, recordRow);
          if (context.FinishAnalyze)
            break;
        }
      }
      catch (ApplicationException ex)
      {
        context.Log.AddMessage(MessageType.Extended, ex.Message);
        context.DifferentAttributeValues.RemoveWhere((Predicate<AttributeValues>) (x => x.AttributeID == linkRec.AttributeID));
      }
    }
  }

  private void ProcessLinksToObjs(
    SynchronizationAttributesAnalyzer context,
    List<AttributeValues> linksToObj)
  {
    if (context.FinishAnalyze || !linksToObj.Any<AttributeValues>())
      return;
    context.Log.AddMessage(MessageType.Extended, "Обработка ссылочных атрибутов");
    foreach (AttributeValues attributeValues in linksToObj)
    {
      AttributeValues linkToObj = attributeValues;
      string str = Convert.ToString(linkToObj.Value);
      if (!string.IsNullOrEmpty(str))
      {
        try
        {
          QuickObjectInfo objectInfo;
          if (GuidHelper.IsGuid(str))
          {
            objectInfo = context.Session.GetObjectInfo(new Guid(str));
          }
          else
          {
            long result;
            if (!long.TryParse(str, out result))
              throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_Obj_AttrRefers_Error"));
            objectInfo = context.Session.GetObjectInfo(result);
          }
          if (objectInfo.Empty)
            throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_Obj_AttrRefers_Error"));
          if (objectInfo.ObjectID < 0L)
          {
            long objectID = -objectInfo.ObjectID;
            if (!context.Session.GetObjectInfo(objectID).Empty)
              objectInfo.ObjectID = objectID;
          }
          if (!context.ProcessedLinksToObj.Contains(objectInfo.ObjectID))
          {
            context.ProcessedLinksToObj.Add(objectInfo.ObjectID);
            IDBObject dbObject = context.Session.GetObject(objectInfo.ObjectID);
            context.Log.AddMessage(MessageType.Extended, $"Анализ атрибутов объекта {dbObject.NameInMessages} [{dbObject.ObjectID}] ");
            this.CompareWithObject(context, objectInfo.ObjectID);
            if (context.FinishAnalyze)
              break;
          }
        }
        catch (ApplicationException ex)
        {
          context.Log.AddMessage(MessageType.Extended, ex.Message);
          context.DifferentAttributeValues.RemoveWhere((Predicate<AttributeValues>) (x => x.AttributeID == linkToObj.AttributeID));
        }
      }
    }
  }
}
