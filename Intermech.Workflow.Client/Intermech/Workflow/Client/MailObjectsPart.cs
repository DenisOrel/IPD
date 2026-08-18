// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailObjectsPart
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

public class MailObjectsPart : ObjectsPart
{
  internal static NodeColumnID ncAttrRecipStatusID = new NodeColumnID((object) wfConsts.AttrRecipStatusID, AttributeSourceTypes.Object);
  internal static NodeColumnID ncAttrSenderStatusID = new NodeColumnID((object) wfConsts.AttrSenderStatusID, AttributeSourceTypes.Object);
  internal static NodeColumnID ncAttrActivityStatusID = new NodeColumnID((object) wfConsts.AttrActivityStatusID, AttributeSourceTypes.Object);
  internal static NodeColumnID ncAttrCompletedTermID = new NodeColumnID((object) wfConsts.AttrCompletedTermID, AttributeSourceTypes.Object);
  internal static NodeColumnID ncAttrProcessID = new NodeColumnID((object) wfConsts.AttrProcessID, AttributeSourceTypes.Object);
  private bool _showRecipient;
  private bool _showSender;
  private bool _showCompletedDate;
  private int _mailCategory;
  private bool _inInbox;

  private void BaseInit(int MailCategory)
  {
    this._mailCategory = MailCategory;
    if (MailCategory == Intermech.Navigator.Consts.CategoryMailInbox)
    {
      this._showSender = true;
      this._showRecipient = false;
      this._showCompletedDate = false;
      this._inInbox = true;
    }
    else if (MailCategory == Intermech.Navigator.Consts.CategoryMailProcessed)
    {
      this._showSender = true;
      this._showRecipient = false;
      this._showCompletedDate = true;
    }
    else if (MailCategory == Intermech.Navigator.Consts.CategoryMailOutbox)
    {
      this._showSender = false;
      this._showRecipient = true;
      this._showCompletedDate = false;
    }
    else
    {
      if (MailCategory != Intermech.Navigator.Consts.CategoryMailTrash)
        return;
      this._showSender = true;
      this._showRecipient = true;
      this._showCompletedDate = false;
    }
  }

  public MailObjectsPart(
    int objTypeID,
    ConditionStructure condition,
    int MailCategory,
    IServiceProvider services)
    : base(objTypeID, condition, services)
  {
    this.BaseInit(MailCategory);
  }

  public MailObjectsPart(
    int objTypeID,
    ConditionStructure[] condition,
    int MailCategory,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(objTypeID, condition, conditionsProvider, services)
  {
    this.BaseInit(MailCategory);
  }

  public MailObjectsPart(
    int objTypeID,
    ConditionStructure[] condition,
    int MailCategory,
    IServiceProvider services)
    : this(objTypeID, condition, MailCategory, (IConditionsProvider) null, services)
  {
  }

  private void AddMailColumns(NodeColumnCollection columns, bool addAll)
  {
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
    NodeColumn column1 = service.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.CAPTION);
    column1.Width = 200;
    columns.Add(column1);
    NodeColumn column2 = service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrProcessID);
    column2.Width = 235;
    columns.Add(column2);
    NodeColumn column3 = service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrPriorityID);
    column3.Width = 16 /*0x10*/;
    columns.Add(column3);
    NodeColumn column4 = service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrAttachmentsID);
    column4.Width = 16 /*0x10*/;
    columns.Add(column4);
    NodeColumn nodeColumn = (NodeColumn) null;
    if (this._showCompletedDate | addAll)
    {
      nodeColumn = service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrCompletedID);
      columns.Add(nodeColumn);
    }
    if (!this._showCompletedDate | addAll)
    {
      nodeColumn = service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrStartedID);
      columns.Add(nodeColumn);
    }
    if (this._showSender | addAll)
      columns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrSenderID));
    if (this._showRecipient | addAll)
      columns.Add(service.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrRecipID));
    if (nodeColumn == null)
      return;
    nodeColumn.SortOrder = NodeColumnSortOrder.Descending;
    nodeColumn.SortIndex = 0;
  }

  public override NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    this.AddMailColumns(columns, false);
    return columns;
  }

  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection supportedColumns = base.GetSupportedColumns(ColumnSetName);
    this.AddMailColumns(supportedColumns, true);
    return supportedColumns;
  }

  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    if (this._inInbox)
    {
      specialFields.Add((object) MailObjectsPart.ncAttrSenderStatusID);
      specialFields.Add((object) MailObjectsPart.ncAttrCompletedTermID);
    }
    specialFields.Add((object) MailObjectsPart.ncAttrRecipStatusID);
    specialFields.Add((object) MailObjectsPart.ncAttrActivityStatusID);
    return specialFields;
  }

  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string str = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long num1 = 1;
    long num2 = 0;
    DateTime dateTime = DateTime.MinValue;
    if (this._inInbox)
    {
      object fieldValue1 = fieldValues[adapter.GetFieldIndex((object) MailObjectsPart.ncAttrSenderStatusID)];
      if (!DBNull.Value.Equals(fieldValue1))
        num2 = Convert.ToInt64(fieldValue1);
      object fieldValue2 = fieldValues[adapter.GetFieldIndex((object) MailObjectsPart.ncAttrCompletedTermID)];
      if (!DBNull.Value.Equals(fieldValue2))
        dateTime = Convert.ToDateTime(fieldValue2);
    }
    if (this._mailCategory != Intermech.Navigator.Consts.CategoryMailTrash)
    {
      object fieldValue = fieldValues[adapter.GetFieldIndex((object) MailObjectsPart.ncAttrRecipStatusID)];
      if (!DBNull.Value.Equals(fieldValue))
        num1 = Convert.ToInt64(fieldValue);
      else if (this._inInbox)
        num1 = 0L;
    }
    ActivityStatus activityStatus = ActivityStatus.Executed;
    object fieldValue3 = fieldValues[adapter.GetFieldIndex((object) MailObjectsPart.ncAttrActivityStatusID)];
    if (!DBNull.Value.Equals(fieldValue3))
      activityStatus = (ActivityStatus) Convert.ToInt64(fieldValue3);
    long num3 = 0;
    int fieldIndex = adapter.GetFieldIndex((object) MailObjectsPart.ncAttrProcessID);
    if (fieldIndex != -1)
    {
      object fieldValue4 = fieldValues[fieldIndex];
      if (!DBNull.Value.Equals(fieldValue4))
        num3 = 1L;
    }
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    int lcStepID = int32_2;
    string caption = str;
    long recipStatus = num1;
    long senderStatus = num2;
    int status = (int) activityStatus;
    DateTime completedTerm = dateTime;
    long processID = num3;
    return (INodeID) new MailNodeID(int32_1, objId, id, checkedOutBy, lcStepID, caption, recipStatus, senderStatus, (ActivityStatus) status, completedTerm, processID);
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    INodeQuery query = base.GetQuery(conditions);
    if (query is ObjectsQuery)
      ((ObjectsQuery) query).Services.AddService(typeof (MailObjectsPart), (object) this);
    return query;
  }
}
