// Decompiled with JetBrains decompiler
// Type: Intermech.CompositionTracking.Server.Methods.CompositionTrackingCreateVersionMethod
// Assembly: Intermech.CompositionTracking.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 560FA293-6728-4C34-9171-0CC07BE87BF4
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.CompositionTracking.Server.dll

using Intermech.CompositionTracking.Server.Params;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.CompositionTracking;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.CompositionTracking.Server.Methods;

internal class CompositionTrackingCreateVersionMethod : CompositionTrackingBaseMethod
{
  private readonly IPairedObjectsCreatorService _pairedObjService;

  private bool CreateTargetVersion(IDBObject sourceDbObject, ref IDBObject targetDbObject)
  {
    IUserSession session = targetDbObject.Session;
    IDBObject createdVersion = this._pairedObjService.FindCreatedVersion(session, targetDbObject.ObjectID);
    if (createdVersion != null)
    {
      targetDbObject = createdVersion;
      return true;
    }
    IDBObject version = session.GetObjectCollection(targetDbObject.ObjectType).CreateVersion(targetDbObject.ObjectID);
    if (targetDbObject.ObjectID != version.ObjectID)
      this.WriteTargetConcretization(sourceDbObject, targetDbObject, version);
    targetDbObject = version;
    return true;
  }

  private void WriteTargetConcretization(
    IDBObject sourceDbObject,
    IDBObject targetDbObject,
    IDBObject newTargetVersion)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545");
    List<int> list = new List<int>();
    foreach (IMSApplicability typeApplicability in MetaDataHelper.GetObjectTypeApplicabilities(sourceDbObject.ObjectType))
    {
      if (typeApplicability != null && MetaDataHelper.IsObjectTypeChildOf(newTargetVersion.ObjectType, typeApplicability.ChildObjectTypeID))
        list.Add(typeApplicability.RelationTypeID);
    }
    GenericListHelper.MakeUnique<int>(list);
    List<int> intList = new List<int>(list.Count);
    foreach (int RelationTypeID in list)
    {
      if (MetaDataHelper.GetAttribute4RelationType(RelationTypeID, attributeTypeId) != null)
        intList.Add(RelationTypeID);
    }
    if (intList.Count == 0)
      return;
    DBRecordSetParams dbRsp = new DBRecordSetParams(new List<ConditionStructure>()
    {
      new ConditionStructure(-2, RelationalOperators.In, (object) new long[2]
      {
        targetDbObject.ObjectID,
        newTargetVersion.ObjectID
      }, (object) null, LogicalOperators.AND, 0, false, AttributeSourceTypes.Auto),
      new ConditionStructure(-23, RelationalOperators.In, (object) intList.ToArray(), LogicalOperators.NONE, 0, false)
    }.ToArray(), new List<ColumnDescriptor>()
    {
      new ColumnDescriptor((object) -20, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    }.ToArray());
    List<ObjInfoItem> projObjList = new List<ObjInfoItem>()
    {
      new ObjInfoItem(sourceDbObject)
    };
    if (sourceDbObject.CheckoutBy != 0L)
      projObjList.Add(new ObjInfoItem(Math.Abs(sourceDbObject.ObjectID), sourceDbObject.ObjectType));
    DataTable childSostavData = DataHelper.GetChildSostavData((IEnumerable<ObjInfoItem>) projObjList, sourceDbObject.Session, (IEnumerable<int>) intList.ToArray(), false, dbRsp);
    if (childSostavData == null)
      return;
    childSostavData.AcceptChanges();
    foreach (DataRow row in (InternalDataCollectionBase) childSostavData.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      IDBRelation relation = sourceDbObject.Session.GetRelation(int64, false);
      if (relation != null)
      {
        AttributeValues[] valuesList = new AttributeValues[1]
        {
          new AttributeValues(attributeTypeId, (object) Math.Abs(newTargetVersion.ObjectID))
        };
        relation.SetAttributesValues(valuesList);
      }
    }
  }

  public CompositionTrackingCreateVersionMethod()
  {
    this._pairedObjService = ServiceUtils.GetService<IPairedObjectsCreatorService>((object) ServerServices.ServiceContainer, true);
  }

  public override CompositionTrackingCommands Command
  {
    get => CompositionTrackingCommands.ctcCreateVersion;
  }

  internal override bool Validate(CompositionTrackingParams trackingParams)
  {
    return base.Validate(trackingParams) && trackingParams.DbObject.VersionID != 0;
  }

  internal override bool Execute(
    CompositionTrackingParams trackingParams,
    IDBObject sourceDbObject,
    ref IDBObject targetDbObject)
  {
    return targetDbObject != null && targetDbObject.CheckoutBy == 0L && targetDbObject.ObjectModifyMode != ObjectModifyModes.Checkout && targetDbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion && this.CreateTargetVersion(sourceDbObject, ref targetDbObject);
  }
}
