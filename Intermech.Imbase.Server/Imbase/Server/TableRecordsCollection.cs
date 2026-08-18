// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.TableRecordsCollection
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class TableRecordsCollection(UserSession session, int objectType) : DBObjectCollection(session, objectType)
{
  private DBRecordSetParams _params;
  private long _tableId = -1;
  private long _referenceId = -1;
  private int _realTypeId;
  private IDBAttributeType[] _columnsList;

  public override DataTable Select(DBRecordSetParams paramSet, bool checkAccess)
  {
    this._columnsList = (IDBAttributeType[]) null;
    this._params = paramSet;
    this._realTypeId = -1;
    if (this._params.Tags != null)
    {
      IDictionary tags = (IDictionary) this._params.Tags;
      if (tags.Contains((object) "$IM_TABLEID"))
        this._tableId = Convert.ToInt64(tags[(object) "$IM_TABLEID"]);
      if (tags.Contains((object) "$IM_PARENTID"))
        this._referenceId = Convert.ToInt64(tags[(object) "$IM_PARENTID"]);
    }
    if (this._tableId != -1L)
      this.ExtractRealObjectType((IDBAttributable) this.Session.GetObject(this._tableId));
    int num = this.TryAddColumns(ref paramSet);
    DataTable result = base.Select(paramSet, checkAccess);
    this.FillCaptionAttribute(result);
    DataColumnCollection columns = result.Columns;
    for (; num > 0; --num)
    {
      if (columns.Count > 0)
        columns.Remove(columns[columns.Count - 1]);
    }
    return result;
  }

  private int TryAddColumns(ref DBRecordSetParams paramSet)
  {
    int num1 = 0;
    if (this.ObjectTypeID >= 0)
    {
      List<IDBAttributeType> list = new List<IDBAttributeType>((IEnumerable<IDBAttributeType>) this.GetColumnsCollection(ref paramSet, paramSet.FailIfNotFound));
      List<object> objectList = new List<object>((IEnumerable<object>) paramSet.Columns);
      List<ColumnInfo> columnInfoList = (List<ColumnInfo>) null;
      if (paramSet.ColumnsInfo != null)
        columnInfoList = new List<ColumnInfo>((IEnumerable<ColumnInfo>) paramSet.ColumnsInfo);
      List<ColumnNameMapping> columnNameMappingList = (List<ColumnNameMapping>) null;
      if (paramSet.ColumnNames != null)
        columnNameMappingList = new List<ColumnNameMapping>((IEnumerable<ColumnNameMapping>) paramSet.ColumnNames);
      List<ColumnContents> columnContentsList = (List<ColumnContents>) null;
      if (paramSet.Contents != null)
        columnContentsList = new List<ColumnContents>((IEnumerable<ColumnContents>) paramSet.Contents);
      int num2 = 0;
      int anObjectTypeID = this.ObjectTypeID;
      if (this._realTypeId != -1)
        anObjectTypeID = this._realTypeId;
      IDBObjectType objectType = this.UserSession.GetObjectType(anObjectTypeID);
      bool flag;
      do
      {
        flag = false;
        int count = list.Count;
        for (int index1 = num2; index1 < count; ++index1)
        {
          IDBAttributeType dbAttributeType1 = list[index1];
          if (dbAttributeType1.AttributeID == Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION))
          {
            int captionAttribute = objectType.CaptionAttribute;
            if (captionAttribute > 0 && this.IndexOf(list, captionAttribute) == -1)
            {
              IDBAttributeType attributeType = this.Session.GetAttributeType(captionAttribute);
              list.Add(attributeType);
              objectList.Add((object) attributeType.AttributeID);
              columnInfoList?.Add(new ColumnInfo((object) attributeType.AttributeID, AttributeSourceTypes.Object, (object) null));
              columnNameMappingList?.Add(ColumnNameMapping.ID);
              columnContentsList?.Add(ColumnContents.Text);
              flag = true;
              ++num1;
            }
          }
          else if (dbAttributeType1.AttributeID > 0)
          {
            IDBAttributeType dbAttributeType2 = (IDBAttributeType) objectType.Attributes.GetAttributeByID(dbAttributeType1.AttributeID) ?? this.UserSession.GetAttributeType(dbAttributeType1.AttributeID);
            if (dbAttributeType2 is IDBAttributeType4Object attributeType4Object && dbAttributeType2.Computed == ComputeValueModes.JITValue)
            {
              int[] formulaAttributes = attributeType4Object.GetRelatedFormulaAttributes();
              if (formulaAttributes != null && formulaAttributes.Length != 0)
              {
                int length = formulaAttributes.Length;
                for (int index2 = 0; index2 < length; ++index2)
                {
                  if (this.IndexOf(list, formulaAttributes[index2]) == -1)
                  {
                    IDBAttributeType attributeType = this.Session.GetAttributeType(formulaAttributes[index2]);
                    list.Add(attributeType);
                    objectList.Add((object) attributeType.AttributeID);
                    columnInfoList?.Add(new ColumnInfo((object) attributeType.AttributeID, AttributeSourceTypes.Object, (object) null));
                    columnNameMappingList?.Add(ColumnNameMapping.ID);
                    columnContentsList?.Add(ColumnContents.Text);
                    flag = true;
                    ++num1;
                  }
                }
              }
            }
          }
        }
        num2 = count;
      }
      while (flag);
      if (num1 > 0)
      {
        paramSet.Columns = objectList.ToArray();
        if (columnInfoList != null)
          paramSet.ColumnsInfo = columnInfoList.ToArray();
        if (columnNameMappingList != null)
          paramSet.ColumnNames = columnNameMappingList.ToArray();
        if (columnContentsList != null)
          paramSet.Contents = columnContentsList.ToArray();
      }
    }
    return num1;
  }

  private void FillCaptionAttribute(DataTable result)
  {
    int columnIndex1 = this.IndexOf(this._columnsList, Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION));
    if (columnIndex1 < 0)
      return;
    int anObjectTypeID = this.ObjectTypeID;
    if (this._realTypeId != -1)
      anObjectTypeID = this._realTypeId;
    int captionAttribute = this.UserSession.GetObjectType(anObjectTypeID).CaptionAttribute;
    if (captionAttribute <= 0)
      return;
    int columnIndex2 = this.IndexOf(this._columnsList, captionAttribute);
    if (columnIndex2 >= 0)
    {
      DataRowCollection rows = result.Rows;
      int count = rows.Count;
      for (int index = 0; index < count; ++index)
      {
        DataRow dataRow = rows[index];
        if (Convert.IsDBNull(dataRow[columnIndex1]))
          dataRow[columnIndex1] = dataRow[columnIndex2];
      }
    }
    result.AcceptChanges();
  }

  private IDBAttributable ExtractRealObjectType(IDBAttributable tableObject)
  {
    IDBAttribute attributeById = tableObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRowsTypeAttID);
    if (attributeById != null)
      this._realTypeId = this.Session.GetObjectType(new Guid(attributeById.AsString)).ObjectType;
    return tableObject;
  }

  private object GetOverrideValue(
    IDBAttributeCollection refAtts,
    IDBAttributeCollection tableAtts,
    int attId)
  {
    object obj = (object) null;
    if (refAtts != null)
      obj = this.GetValue(refAtts, attId);
    if ((obj == null || Convert.IsDBNull(obj)) && tableAtts != null)
      obj = this.GetValue(tableAtts, attId);
    return obj == null || Convert.IsDBNull(obj) ? (object) null : obj;
  }

  private object GetValue(IDBAttributeCollection atts, int attId) => atts?.FindByID(attId)?.Value;

  private int IndexOf(List<IDBAttributeType> list, int attId)
  {
    if (list == null)
      return -1;
    int count = list.Count;
    for (int index = 0; index < count; ++index)
    {
      if (list[index].AttributeID == attId)
        return index;
    }
    return -1;
  }

  private int IndexOf(IDBAttributeType[] list, int attId)
  {
    if (list == null)
      return -1;
    int length = list.Length;
    for (int index = 0; index < length; ++index)
    {
      if (list[index].AttributeID == attId)
        return index;
    }
    return -1;
  }

  private List<IDBAttributeType> GetParentAttributes(IDBAttributeType[] columnsList)
  {
    List<IDBAttributeType> list = new List<IDBAttributeType>();
    if (this.ObjectTypeID >= 0)
    {
      int anObjectTypeID = this.ObjectTypeID;
      if (this._realTypeId != -1)
        anObjectTypeID = this._realTypeId;
      IDBObjectType objectType = this.UserSession.GetObjectType(anObjectTypeID);
      for (int index = 0; index < columnsList.Length; ++index)
      {
        if (columnsList[index].AttributeID > 0)
        {
          IDBAttributeType att = (IDBAttributeType) objectType.Attributes.GetAttributeByID(columnsList[index].AttributeID) ?? this.UserSession.GetAttributeType(columnsList[index].AttributeID);
          if (att is IDBAttributeType4Object && !this.Contains(list, att))
            list.Add(att);
        }
      }
    }
    return list;
  }

  private List<IDBAttributeType> GetCalculatedAttributes(IDBAttributeType[] columnsList)
  {
    List<IDBAttributeType> list = new List<IDBAttributeType>();
    if (this.ObjectTypeID >= 0)
    {
      IDBObjectType objectType = this.UserSession.GetObjectType(this.ObjectTypeID);
      for (int index = 0; index < columnsList.Length; ++index)
      {
        if (columnsList[index].AttributeID > 0)
        {
          IDBAttributeType att = (IDBAttributeType) objectType.Attributes.GetAttributeByID(columnsList[index].AttributeID) ?? this.UserSession.GetAttributeType(columnsList[index].AttributeID);
          if (att.Computed == ComputeValueModes.JITValue && !this.Contains(list, att))
            list.Add(att);
        }
      }
    }
    return list;
  }

  private bool Contains(List<IDBAttributeType> list, IDBAttributeType att)
  {
    if (list == null)
      return false;
    int attributeId = att.AttributeID;
    int count = list.Count;
    for (int index = 0; index < count; ++index)
    {
      if (list[index].AttributeID == attributeId)
        return true;
    }
    return false;
  }

  private bool Contains(IDBAttributeType[] list, IDBAttributeType att)
  {
    if (list == null)
      return false;
    int attributeId = att.AttributeID;
    int length = list.Length;
    for (int index = 0; index < length; ++index)
    {
      if (list[index].AttributeID == attributeId)
        return true;
    }
    return false;
  }
}
