// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseApplicabilityEventHandler
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseApplicabilityEventHandler
{
  public static void SubscribeOnSystemEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.AfterCreateRelationExEvent += new CreateRelationExHandler(ImbaseApplicabilityEventHandler.EventHelper_AfterCreateRelationExEvent);
    eventHelper.BeforeCheckinEvent += new ObjectEventHandler(ImbaseApplicabilityEventHandler.EventHelper_BeforeCheckinEvent);
  }

  public static void UnubscribeOnSystemlEvents(IEventLogHelper eventHelper)
  {
    if (eventHelper == null)
      return;
    eventHelper.AfterCreateRelationExEvent -= new CreateRelationExHandler(ImbaseApplicabilityEventHandler.EventHelper_AfterCreateRelationExEvent);
    eventHelper.BeforeCheckinEvent -= new ObjectEventHandler(ImbaseApplicabilityEventHandler.EventHelper_BeforeCheckinEvent);
  }

  private static void EventHelper_AfterCreateRelationExEvent(
    IDBRelation sender,
    IUserSession session,
    int assignMode)
  {
    if (sender == null || (assignMode & 4096 /*0x1000*/) == 4096 /*0x1000*/ || (assignMode & Intermech.Consts.CheckInMode) == Intermech.Consts.CheckInMode || (assignMode & Intermech.Consts.CheckOutMode) == Intermech.Consts.CheckOutMode || (assignMode & 1024 /*0x0400*/) == 1024 /*0x0400*/ || !ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
      return;
    long partObjectId = sender.PartObjectID;
    List<QuickObjectInfo> source;
    if (partObjectId == 0L)
      source = session.GetAllObjectVersionsList(sender.PartID, true, false, false).Select<long, QuickObjectInfo>(new System.Func<long, QuickObjectInfo>(session.GetObjectInfo)).ToList<QuickObjectInfo>();
    else
      source = new List<QuickObjectInfo>()
      {
        session.GetObjectInfo(partObjectId)
      };
    List<int> imbaseCreatedObjTypes;
    if (!ServiceUtils.GetService<IImbaseObjInfoService>((object) ServerServices.ServiceContainer, true).GetCreationTypes(session.SessionGUID, out imbaseCreatedObjTypes))
      return;
    List<QuickObjectInfo> list = source.Where<QuickObjectInfo>((System.Func<QuickObjectInfo, bool>) (x => imbaseCreatedObjTypes.Contains(x.ObjectTypeID))).ToList<QuickObjectInfo>();
    if (list.Count == 0)
      return;
    foreach (QuickObjectInfo quickObjectInfo in list)
    {
      long linkId;
      long recordId;
      switch (ImbaseApplicabilityEventHandler.GetObjectApplicablityFromImbase(session, quickObjectInfo.ObjectID, out linkId, out recordId))
      {
        case ApplicabilityStatusEnum.None:
        case ApplicabilityStatusEnum.NoLimit:
          continue;
        case ApplicabilityStatusEnum.ForbiddenUse:
        case ApplicabilityStatusEnum.TotalForbiddenUse:
          IDBAttribute dbAttribute = sender.Attributes.AddAttribute(new Guid("cadd9ac4-306c-11d8-b4e9-00304f19f545"), false);
          if (dbAttribute != null)
          {
            dbAttribute.AsBoolean = true;
            continue;
          }
          continue;
        case ApplicabilityStatusEnum.LimitedUse:
          IImbaseRestrictiveCache service = ServiceUtils.GetService<IImbaseRestrictiveCache>((object) ServerServices.ServiceContainer, true);
          string imbaseInternalKey = ImbaseHelper.MakeInternalImbaseKey(linkId, recordId);
          if (service.Check(session.UserID, imbaseInternalKey))
          {
            service.Remove(session.UserID, imbaseInternalKey);
            continue;
          }
          goto case ApplicabilityStatusEnum.ForbiddenUse;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }
  }

  private static void EventHelper_BeforeCheckinEvent(IDBObject sender, IUserSession session)
  {
    ImbaseApplicabilityEventHandler.CheckImbaseApplicability(sender, session);
  }

  private static void CheckImbaseApplicability(IDBObject obj, IUserSession session)
  {
    List<int> imbaseCreatedObjTypes;
    if (!ServiceUtils.GetService<IImbaseParamsService>((object) ServerServices.ServiceContainer, true).CommonParams.CheckApplicabilityBeforeCreateComposition || !ServiceUtils.GetService<IImbaseObjInfoService>((object) ServerServices.ServiceContainer, true).GetCreationTypes(session.SessionGUID, out imbaseCreatedObjTypes))
      return;
    List<int> list = MetaDataHelper.GetObjectTypeApplicabilities(obj.ObjectType).Where<IMSApplicability>((System.Func<IMSApplicability, bool>) (x => imbaseCreatedObjTypes.Contains(x.ChildObjectTypeID))).Select<IMSApplicability, int>((System.Func<IMSApplicability, int>) (x => x.RelationTypeID)).Distinct<int>().ToList<int>();
    ICompositionLoadService service = ServiceUtils.GetService<ICompositionLoadService>((object) ServerServices.ServiceContainer, true);
    IEnumerable<ObjInfoItem> objects = (IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
    {
      new ObjInfoItem(obj)
    };
    IEnumerable<ColumnDescriptor> columns = (IEnumerable<ColumnDescriptor>) new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -23, AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0)
    };
    IEnumerable<ConditionStructure> conditions = (IEnumerable<ConditionStructure>) new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.DenyUseOnCheckInAttID, RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0, false)
    };
    DataTable source = service.LoadComplexCompositions((object) session, objects, (IEnumerable<int>) list, (IEnumerable<int>) imbaseCreatedObjTypes, columns, true, false, (VersionsRule) null, conditions, "cad001e0-306c-11d8-b4e9-00304f19f545", (Dictionary<long, HybridDictionary>) null, 1);
    if (source != null && source.Rows.Count != 0)
    {
      Tuple<long, int>[] array = source.AsEnumerable().Select<DataRow, Tuple<long, int>>((System.Func<DataRow, Tuple<long, int>>) (x => new Tuple<long, int>(Convert.ToInt64(x[0]), Convert.ToInt32(x[1])))).ToArray<Tuple<long, int>>();
      throw new ImbaseApplicablityException(ImbaseApplicabilityEventHandler.GetObjectsApplicablity(session, array), obj.ObjectID, obj.NameInMessages, array);
    }
  }

  private static ApplicabilityStatusEnum[] GetObjectsApplicablity(
    IUserSession session,
    Tuple<long, int>[] objectIds)
  {
    HashSet<ApplicabilityStatusEnum> source = new HashSet<ApplicabilityStatusEnum>();
    foreach (Tuple<long, int> objectId in objectIds)
      source.Add(ImbaseApplicabilityEventHandler.GetObjectApplicablityFromImbase(session, objectId.Item1, out long _, out long _));
    return source.ToArray<ApplicabilityStatusEnum>();
  }

  private static ApplicabilityStatusEnum GetObjectApplicablityFromImbase(
    IUserSession session,
    long objectId,
    out long linkId,
    out long recordId)
  {
    linkId = 0L;
    recordId = -1L;
    IDBObject dbObject = session.GetObject(objectId);
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad00209-306c-11d8-b4e9-00304f19f545"));
    IDBAttribute attributeByGuid2 = dbObject.GetAttributeByGuid(new Guid("cad0020f-306c-11d8-b4e9-00304f19f545"));
    if (attributeByGuid1 == null || attributeByGuid1.IsNull)
      return ApplicabilityStatusEnum.None;
    linkId = attributeByGuid1.AsInteger;
    recordId = attributeByGuid2 == null || attributeByGuid2.IsNull ? -1L : attributeByGuid2.AsInteger;
    if (linkId == 0L)
      return ApplicabilityStatusEnum.None;
    string statusStr = string.Empty;
    if (recordId >= 0L)
    {
      string filter = $"[-2]={recordId}";
      DataTable recordsTable;
      ImbaseServer.Instance.LoadRecords(session.SessionGUID, linkId, filter, Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, out recordsTable, out AttributeTypeProperties[] _, out ImbaseKeyInfo _);
      if (recordsTable.Rows.Count > 0)
      {
        int columnIndex = recordsTable.Columns.IndexOf(Intermech.Imbase.Consts.ImbaseUsingAttID.ToString());
        if (columnIndex != -1)
          statusStr = Convert.ToString(recordsTable.Rows[0][columnIndex]);
      }
    }
    else
    {
      IDBAttribute attributeById = session.GetObject(linkId, false)?.GetAttributeByID(Intermech.Imbase.Consts.ImbaseUsingAttID);
      if (attributeById != null)
        statusStr = attributeById.AsString;
    }
    return string.IsNullOrEmpty(statusStr) ? ApplicabilityStatusEnum.None : ApplicabilityStatusHelper.GetStatus(statusStr);
  }
}
