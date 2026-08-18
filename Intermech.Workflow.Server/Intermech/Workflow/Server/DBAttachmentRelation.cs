// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBAttachmentRelation
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Workflow.Server.Activities;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBAttachmentRelation(UserSession uSession, DataTable relationsTable) : DBRelation(uSession, relationsTable)
{
  private void UpdateActivityMessagesAttachFlags(long newflag)
  {
  }

  protected override int DoDelete(long deleteMode)
  {
    if (this.UserSession.GetObject(this.ProjID, false) is WFActivity activity)
    {
      string str = string.Empty;
      if ((deleteMode == (long) Consts.PurgeMode || deleteMode == 0L) && !(this.SenderObject is WFActivity))
      {
        ActivityStatus status = activity.Status;
        if (status != ActivityStatus.OnApproach)
        {
          if (deleteMode == 0L)
          {
            if (this.UserSession.IsAdmin || this.UserSession.IsSystemSession)
            {
              if (activity.Flags.HasFlag((Enum) ActivityFlags.AllowAdminAttach) && this.UserSession.IsAdmin || activity.Flags.HasFlag((Enum) ActivityFlags.AllowSystemAttach) && this.UserSession.IsSystemSession)
              {
                if (!wfConsts.ExecStatuses.Contains(status))
                  str = "Вложения могут быть откреплены только во время выполнения";
              }
              else
                str = this.CheckActivityAttachment(activity, str, status);
            }
            else
              str = this.CheckActivityAttachment(activity, str, status);
            if (string.IsNullOrEmpty(str) && (activity is Task || activity is Script script && script.ExecSide == ScriptExecSide.Client || activity is RemoteProcess remoteProcess && remoteProcess.Participants.Count > 0))
            {
              AttachmentList attachments = new AttachmentList();
              attachments.AddAttachment(this.PartObjectID);
              activity.HandleTemporaryRights(attachments, false);
            }
          }
          else if (wfConsts.ExecStatuses.Contains(status))
          {
            IDBAttribute attributeById1 = activity.GetAttributeByID(wfConsts.SysVarDenyDocDeleteID);
            if (attributeById1 != null && attributeById1.AsBoolean)
            {
              IDBAttribute attributeById2 = this.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID);
              if (attributeById2 != null)
              {
                IDBObject dbObject = this.UserSession.GetObject(attributeById2.AsInteger, false);
                if (dbObject != null)
                  str = string.Format(LocalizationHolder.rm.GetString("Workflow.Server_15"), (object) dbObject.NameInMessages, (object) activity.Process.NameInMessages, (object) activity.NameInMessages);
              }
            }
          }
        }
      }
      if (!string.IsNullOrEmpty(str))
        throw new Exception(str);
    }
    int num = base.DoDelete(deleteMode);
    if (activity == null)
      return num;
    IDBAttribute attributeById = activity.GetAttributeByID(wfConsts.AttrAttachmentsID);
    if (attributeById == null)
      return num;
    long newValue = this.UserSession.GetRelationCollection(this.TypeID).ConsistFrom(new DBRecordSetParams(1), this.ProjID).Rows.Count > 0 ? 1L : 0L;
    if (attributeById.AsInteger == newValue)
      return num;
    attributeById.AsInteger = newValue;
    activity.UpdateMessagesAttachmentFlags(newValue);
    return num;
  }

  private string CheckActivityAttachment(WFActivity activity, string err, ActivityStatus status)
  {
    if (activity.Flags.HasFlag((Enum) ActivityFlags.DenyDetach))
      err = LocalizationHolder.rm.GetString("ErrDetachDenied1");
    else if (!wfConsts.ExecStatuses.Contains(status) || activity.ParticipantID != this.UserSession.UserID && activity.ParticipantID != wfConsts.SystemUserID)
      err = LocalizationHolder.rm.GetString("ErrDetachDenied2");
    return err;
  }

  private void CheckIsExecutedActivitiesExists(
    long AttachObjectID,
    long PrototypeSchemeID,
    long ExceptProcessID)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) wfConsts.AttrProcessID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Default, SortOrders.NONE, 0)
    };
    List<int> intList = new List<int>();
    foreach (ActivityStatus execStatuse in wfConsts.ExecStatuses)
      intList.Add((int) execStatuse);
    ConditionStructure[] conds1 = new ConditionStructure[1]
    {
      new ConditionStructure(wfConsts.AttrActivityStatusID, RelationalOperators.In, (object) intList.ToArray(), (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Object)
    };
    DataTable attachmentUsage = AttachmentFuncs.GetAttachmentUsage((IUserSession) this.UserSession, AttachObjectID, conds1, columns);
    List<long> longList = new List<long>();
    foreach (DataRow row in (InternalDataCollectionBase) attachmentUsage.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      if (int64 != ExceptProcessID && !longList.Contains(int64))
        longList.Add(int64);
    }
    if (longList.Count == 0)
      return;
    List<long> objectVersionsList = this.UserSession.GetAllObjectVersionsList(PrototypeSchemeID, false, false, false);
    ConditionStructure[] conds2 = new ConditionStructure[2]
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.AND, 0, false),
      new ConditionStructure(wfConsts.AttrPrototypeID, RelationalOperators.In, (object) objectVersionsList.ToArray(), (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto, ColumnContents.ID)
    };
    DataTable dataTable = MiscFunx.SimpleSelect((IUserSession) this.UserSession, wfConsts.ProcessesTypeID, new object[2]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION
    }, conds2);
    if (dataTable.Rows.Count > 0)
    {
      string caption = this.UserSession.GetObject(PrototypeSchemeID).Caption;
      string str = $"{string.Format(LocalizationHolder.rm.GetString("Workflow.Server_16"), (object) caption)}\r\n\"{this.UserSession.GetObject(AttachObjectID).NameInMessages}{LocalizationHolder.rm.GetString("Workflow.Server_17")}";
      string empty = string.Empty;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (!string.IsNullOrEmpty(empty))
          empty += ", ";
        empty += $"\"{row[1].ToString()}\"({Convert.ToInt64(row[0])})";
      }
      throw new Exception(str + empty);
    }
  }

  public override void DoAfterCreate(int assignMode)
  {
    IDBObject dbObject = this.UserSession.GetObject(this.ProjID, false);
    if (dbObject is WFActivity wfActivity)
    {
      IDBAttribute attributeById1 = wfActivity.GetAttributeByID(wfConsts.SysVarMultiStartID);
      if (attributeById1 != null && !attributeById1.AsBoolean)
      {
        IDBAttribute attributeById2 = this.GetAttributeByID(this.UserSession.IdentHelper.CompositionVersionID);
        if (attributeById2 != null)
        {
          long AttachObjectID = attributeById2.AsInteger;
          QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(attributeById2.AsInteger);
          if (objectInfo.Empty)
          {
            objectInfo = this.UserSession.GetObjectInfo(-attributeById2.AsInteger);
            if (objectInfo.Empty)
              return;
            AttachObjectID = -attributeById2.AsInteger;
          }
          if (wfActivity.Process is WFProcess process)
            this.CheckIsExecutedActivitiesExists(AttachObjectID, process.PrototypeSchemeID, process.ObjectID);
        }
      }
    }
    base.DoAfterCreate(assignMode);
    if (dbObject == null)
      return;
    IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrAttachmentsID);
    if (attributeById == null || attributeById.AsInteger == 1L)
      return;
    attributeById.AsInteger = 1L;
    wfActivity?.UpdateMessagesAttachmentFlags(1L);
  }
}
