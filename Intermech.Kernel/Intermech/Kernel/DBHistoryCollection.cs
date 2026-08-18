// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBHistoryCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel;

public class DBHistoryCollection : DBRecordSet, IDBHistoryCollection, IDBRecords, IDBSessionable
{
  public DBHistoryCollection(UserSession uSession)
    : base(uSession, -1)
  {
    this._DBObjectTableName = "IMS_ATTR_HISTORY";
    this._DBKeyField = "F_KEY";
    this._DBKeyFieldID = Convert.ToInt32((object) ObligatoryObjectAttributes.F_KEY);
    this._DBAttributesTableName = "IMS_OBJECT_ATTRS";
    uSession.GetSystemSecurity().CheckAccess(ActionType.ShowHistory);
    this.UserSession.QueryBuilder.SystemTableName = "IMS_ATTR_HISTORY";
    this.LocalTypesMode = true;
  }

  public DataTable Select(ConditionStructure[] conditions, long lastKey, int recCount)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[12]
    {
      new ColumnDescriptor((object) -57, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.DESC, 0),
      new ColumnDescriptor((object) -58, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -7, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -23, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -51, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -36, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -3, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -54, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -53, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -55, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -56, AttributeSourceTypes.History, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    };
    DBRecordSetParams paramSet = new DBRecordSetParams(conditions, columns, lastKey, (object) lastKey, recCount);
    DataTable dataTable = this.Select(ref paramSet);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      IDBAttributeType attributeType = this.UserSession.GetAttributeType(Convert.ToInt32(row[1]), false);
      if (attributeType != null)
      {
        if (attributeType.MultipleValued == MultiValueModes.MultiValuesFromList || attributeType.MultipleValued == MultiValueModes.SingleValueFromList)
        {
          string result = row[attributeType.ValueFieldName].ToString();
          (attributeType as DBAttributeType).GetPossibleValueDescription(row[attributeType.ValueFieldName], ref result);
          row[8] = (object) result;
        }
        else if (attributeType.TextFieldName != "F_STRING_VALUE")
          row[8] = (object) row[attributeType.TextFieldName].ToString();
      }
    }
    dataTable.Columns.Remove(dataTable.Columns["F_INTEGER_VALUE"]);
    dataTable.Columns.Remove(dataTable.Columns["F_DATE_VALUE"]);
    dataTable.Columns.Remove(dataTable.Columns["F_DOUBLE_VALUE"]);
    return dataTable;
  }

  public override string ObjectName => "История изменений";

  protected override AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.History;
  }
}
