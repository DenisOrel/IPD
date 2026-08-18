// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WorkflowExporter
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Workflow.Server;

public class WorkflowExporter : ICategoryExport
{
  public string ExporterName => "Workflow.WorkflowExporter";

  public long[] GetLinkedObjectVersions(IUserSession session, int category, object id)
  {
    List<long> longList = new List<long>();
    if (category == 1)
    {
      IDBObject dbObject = session.GetObject(Convert.ToInt64(id), false);
      if (dbObject != null && (dbObject.ObjectType == wfConsts.SchemesTypeID || dbObject.ObjectType == wfConsts.ProcessesTypeID))
      {
        IScheme scheme = dbObject as IScheme;
        foreach (IActivity activity in scheme.Activities)
          longList.Add(activity.ObjectID);
        foreach (IDBObject allLink in scheme.AllLinks)
          longList.Add(allLink.ObjectID);
      }
      else if (dbObject != null)
      {
        ActivityInfo byId = ActivityInfos.FindByID(dbObject.ObjectType);
        if (byId != null && wfConsts.IsParticipantActivity(byId.Kind))
        {
          IDBAttribute attributeById = dbObject.GetAttributeByID(wfConsts.AttrParticipantsID);
          if (attributeById != null)
          {
            foreach (Participant participant in new ParticipantList(session)
            {
              AsString = attributeById.Value.ToString()
            })
            {
              if ((participant.Kind == ParticipantKind.User || participant.Kind == ParticipantKind.Group) && !longList.Contains(participant.ID))
                longList.Add(participant.ID);
            }
          }
        }
      }
    }
    return longList.ToArray();
  }

  private void AddObjects(List<ExportAttribute> ret, HashSet<long> objectIDs)
  {
    if (objectIDs.Count <= 0)
      return;
    List<object> objectList = new List<object>();
    foreach (long objectId in objectIDs)
      objectList.Add((object) objectId);
    ret.Add(new ExportAttribute(1, objectList.ToArray()));
  }

  public ExportAttribute[] GetLinkedDataByAttribute(
    IUserSession session,
    AttributableElements kind,
    long id,
    IDBAttributable iDBAttributable,
    int attributeId,
    object attrValueOriginal,
    ref object attrValueCurrent)
  {
    List<ExportAttribute> ret = new List<ExportAttribute>();
    if (kind == AttributableElements.Object && attrValueCurrent != null)
    {
      if (attributeId == wfConsts.AttrParticipantsID)
      {
        char[] chArray = attrValueCurrent as char[];
        ParticipantList participantList = new ParticipantList(session);
        participantList.AsString = new string(chArray);
        participantList.WriteGuids = true;
        attrValueCurrent = (object) participantList.AsString.ToCharArray();
        this.AddObjects(ret, participantList.ObjectIDs);
      }
      else if (attributeId == wfConsts.AttrVariablesID)
      {
        VarList varList = new VarList(session, true, false);
        varList.LoadFromStream((Stream) (attrValueCurrent as MemoryStream));
        foreach (Variable variable in varList)
          ret.Add(new ExportAttribute(3, new object[1]
          {
            (object) variable.AttrTypeID
          }));
        varList.WriteGuids = true;
        MemoryStream memoryStream = new MemoryStream();
        varList.SaveToStream((Stream) memoryStream);
        memoryStream.Position = 0L;
        attrValueCurrent = (object) memoryStream;
        this.AddObjects(ret, varList.ObjectIDs);
      }
      else if (attributeId == wfConsts.AttrNotificationsID)
      {
        Notifications notifications = new Notifications(session);
        notifications.XMLOnlyMode = true;
        notifications.LoadFromStream((Stream) (attrValueCurrent as MemoryStream));
        notifications.WriteGuids = true;
        MemoryStream memoryStream = new MemoryStream();
        notifications.SaveToStream((Stream) memoryStream);
        memoryStream.Position = 0L;
        attrValueCurrent = (object) memoryStream;
        this.AddObjects(ret, notifications.ObjectIDs);
      }
      else if (attributeId == wfConsts.AttrTermsID)
      {
        Terms terms = new Terms(session);
        terms.XMLOnlyMode = true;
        terms.LoadFromStream((Stream) (attrValueCurrent as MemoryStream));
        terms.WriteGuids = true;
        MemoryStream memoryStream = new MemoryStream();
        terms.SaveToStream((Stream) memoryStream);
        memoryStream.Position = 0L;
        attrValueCurrent = (object) memoryStream;
      }
      else if (iDBAttributable.TypeID == wfConsts.CaseTypeID && attributeId == wfConsts.AttrConditionID)
      {
        ConditionList conditionList = new ConditionList(session);
        conditionList.LoadFromStream((Stream) (attrValueCurrent as MemoryStream));
        conditionList.WriteGuids = true;
        MemoryStream memoryStream = new MemoryStream();
        conditionList.SaveToStream((Stream) memoryStream);
        memoryStream.Position = 0L;
        attrValueCurrent = (object) memoryStream;
      }
      else if (attributeId == wfConsts.AttrAddInfoID)
      {
        XmlIni xmlIni = new XmlIni();
        xmlIni.Load((Stream) (attrValueCurrent as MemoryStream));
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
          MemoryStream memoryStream = new MemoryStream();
          xmlIni.Save((Stream) memoryStream);
          memoryStream.Position = 0L;
          attrValueCurrent = (object) memoryStream;
        }
      }
    }
    return ret.ToArray();
  }

  public bool ProcessShortBlobs => true;

  public static void Init()
  {
    if (!(ApplicationServices.Container.GetService(typeof (ICategoryExportManager)) is ICategoryExportManager service))
      return;
    ICategoryExport iCategoryExport = (ICategoryExport) new WorkflowExporter();
    service.RegisterCategoryExport(1, iCategoryExport);
    service.RegisterCategoryExport(3, iCategoryExport);
  }
}
