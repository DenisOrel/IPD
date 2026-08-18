// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WorkflowImporter
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

internal class WorkflowImporter
{
  private List<long> _activities = new List<long>();
  private List<long> _links = new List<long>();
  private List<long> _schemes = new List<long>();

  public WorkflowImporter()
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICustomImport)) is ICustomImport service))
      return;
    service.CustomImportedEvent += new CustomImported(this.CustomImportedEvent);
    service.AfterImportObjects += new AfterCustomImport(this.AfterImportObjects);
  }

  private void CheckConstsInited(IUserSession session)
  {
    if (wfConsts.SchemesTypeID != 0)
      return;
    wfConsts.Init(session);
  }

  private void CustomImportedEvent(object sender, CustomImportedEventArgs e)
  {
    if (e.CategoryID != 1)
      return;
    IDBObject dbSessionable = e.DBSessionable as IDBObject;
    if (dbSessionable is WFActivity)
      this._activities.Add(dbSessionable.ObjectID);
    else if (dbSessionable is WFLink)
      this._links.Add(dbSessionable.ObjectID);
    if (!(dbSessionable is WFScheme))
      return;
    this._schemes.Add(dbSessionable.ObjectID);
  }

  private void AfterImportObjects(object sender, AfterCustomImportEventArgs e)
  {
    IUserSession session = (IUserSession) null;
    try
    {
      if (this._activities.Count <= 0)
        return;
      if (ApplicationServices.Container.GetService(typeof (IDBTimedEvents)) is IDBTimedEvents service)
        session = service.GetSystemSessionTemporaryClone("workflow.import");
      if (session == null)
        return;
      foreach (long activity in this._activities)
      {
        if (session.GetObject(activity, false) is DBObject src)
        {
          int typeId = src.TypeID;
          this.CheckConstsInited(e.Session);
          IDBAttribute attributeById1 = src.GetAttributeByID(wfConsts.AttrParticipantsID);
          if (attributeById1 != null)
          {
            ParticipantList participantList = new ParticipantList(session);
            participantList.AsString = attributeById1.Value.ToString();
            ((DBAttribute) attributeById1).SetValidatingMode(1);
            attributeById1.Value = (object) participantList.AsString;
          }
          IDBAttribute attributeById2 = src.GetAttributeByID(wfConsts.AttrVariablesID);
          if (attributeById2 != null)
          {
            VarList varList = new VarList((IDBObject) src, true, false);
            ((DBAttribute) attributeById2).SetValidatingMode(1);
            IBlobWriter writer = attributeById2 as IBlobWriter;
            int num = typeId == wfConsts.SchemesTypeID ? 1 : 0;
            varList.Save(writer, num != 0);
          }
          IDBAttribute attributeById3 = src.GetAttributeByID(wfConsts.AttrNotificationsID);
          if (attributeById3 != null)
          {
            Notifications notifications = new Notifications(session);
            notifications.Load(attributeById3);
            ((DBAttribute) attributeById3).SetValidatingMode(1);
            notifications.Save(attributeById3);
          }
          IDBAttribute attributeById4 = src.GetAttributeByID(wfConsts.AttrTermsID);
          if (attributeById4 != null)
          {
            Terms terms = new Terms(session);
            terms.Load(attributeById4);
            ((DBAttribute) attributeById4).SetValidatingMode(1);
            terms.Save(attributeById4);
          }
          if (typeId == wfConsts.CaseTypeID)
          {
            IDBAttribute attributeById5 = src.GetAttributeByID(wfConsts.AttrConditionID);
            if (attributeById5 != null)
            {
              ConditionList conditionList = new ConditionList(session);
              conditionList.Load(attributeById5);
              ((DBAttribute) attributeById5).SetValidatingMode(1);
              conditionList.Save(attributeById5);
            }
            IDBAttribute attributeById6 = src.GetAttributeByID(wfConsts.AttrConditionFormulaID);
            if (attributeById6 != null)
            {
              List<ExpressionInfo> expressionInfoList = new List<ExpressionInfo>((IEnumerable<ExpressionInfo>) MiscFunx.GetExpressionListFromAttr(attributeById6));
              ((DBAttribute) attributeById6).SetValidatingMode(1);
              IDBAttribute attr = attributeById6;
              MiscFunx.ExpressionsToAttribute(expressionInfoList, attr);
            }
          }
          if (typeId == wfConsts.TimerTypeID)
          {
            IDBAttribute attributeById7 = src.GetAttributeByID(wfConsts.AttrAddInfoID);
            if (attributeById7 != null)
            {
              XmlIni xmlIni = new XmlIni();
              StreamHelper.LoadFromBlobStream(attributeById7 as IBlobReader, new ProcessStreamDelegate(xmlIni.Load));
              bool flag = false;
              string str = xmlIni.ReadString("Props", "TimerPeriod");
              if (string.IsNullOrEmpty(str) && xmlIni.Root.Name == "Period")
                str = xmlIni.AsString;
              if (!string.IsNullOrEmpty(str))
              {
                PeriodInformation periodInformation = new PeriodInformation(session)
                {
                  AsString = str,
                  WriteGuids = true
                };
                xmlIni.WriteString("Props", "TimerPeriod", periodInformation.AsString);
                flag = true;
              }
              if (flag)
              {
                ((DBAttribute) attributeById7).SetValidatingMode(1);
                string asString = attributeById7.AsString;
                StreamHelper.SaveToBlobStream(attributeById7 as IBlobWriter, new ProcessStreamDelegate(xmlIni.Save), asString);
              }
            }
          }
        }
      }
      if (e.Error != null)
        return;
      foreach (long scheme in this._schemes)
      {
        IDBObjectCollection objectCollection = session.GetObjectCollection(wfConsts.ActivitiesTypeID);
        DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(wfConsts.AttrProcessID, RelationalOperators.Equal, (object) Math.Abs(scheme), (object) null, LogicalOperators.NONE, 0, true, AttributeSourceTypes.Auto, ColumnContents.ID)
        }, new object[1]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID
        });
        if (paramSet.Tags == null)
          paramSet.Tags = new HybridDictionary();
        paramSet.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesByObjectRefSelector(wfConsts.AttrProcessID, Math.Abs(scheme));
        foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!this._activities.Contains(int64) && !this._activities.Contains(-int64) && session.GetObject(int64) is WFActivity wfActivity)
            wfActivity.InternalDelete(true);
        }
        foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(wfConsts.LinksTypeID).Select(paramSet).Rows)
        {
          long int64 = Convert.ToInt64(row[0]);
          if (!this._links.Contains(int64) && !this._links.Contains(-int64) && session.GetObject(int64) is WFLink wfLink)
            wfLink.InternalDelete(true);
        }
      }
    }
    finally
    {
      this._activities = new List<long>();
      this._links = new List<long>();
      this._schemes = new List<long>();
      session?.Logout("workflow.import");
    }
  }
}
