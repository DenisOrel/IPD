// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.ObjectLinkRecordFormer
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Briefcase;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal class ObjectLinkRecordFormer(
  IUserSession session,
  IEventLogHelper eventHelper,
  Dictionary<Guid, ImportedInfo> links,
  Dictionary<Guid, long> measures,
  string path) : RecordFormer(session, eventHelper, links, measures, path)
{
  public override void SetRecordValues(
    AttributeInfo attrInfo,
    IDBAttributeType attrType,
    AttributeValue rec,
    AttributeRecord record)
  {
    IDBObject dbObject = (IDBObject) null;
    if (rec.GuidValue == string.Empty || !GuidHelper.IsGuid(rec.GuidValue))
    {
      if (attrType.PropertiesStructure.AttributeGuid == new Guid("cad00142-306c-11d8-b4e9-00304f19f545"))
        dbObject = this.FindRankObject(this.session, rec.StringValue);
      else if (attrType.PropertiesStructure.AttributeGuid == new Guid("cad0038c-306c-11d8-b4e9-00304f19f545"))
        dbObject = this.FindMaterialObject(this.session, rec.StringValue);
      else if (attrType.PropertiesStructure.AttributeGuid == SystemGUIDs.attributeArchive)
        dbObject = this.FindArchiveObject(this.session, rec.StringValue);
    }
    if (dbObject == null && (rec.GuidValue == string.Empty || !GuidHelper.IsGuid(rec.GuidValue)))
    {
      this.eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1117"), (object) attrType.Name), Consts.traceAlways, string.Empty);
      record.IntegerValue = (object) null;
      record.DoubleValue = (object) null;
      record.StringValue = (object) null;
    }
    else
    {
      if (dbObject == null)
        dbObject = this.FindObject(this.session, this.links, new Guid(rec.GuidValue));
      if (dbObject == null)
      {
        dbObject = this.session.GetObjectCollection(new Guid("cadd960d-306c-11d8-b4e9-00304f19f545")).Create(new Guid(rec.GuidValue));
        dbObject.Caption = rec.StringValue;
        dbObject.CommitCreation(true);
      }
      else if (attrType.PropertiesStructure.AttributeGuid == new Guid("cad00209-306c-11d8-b4e9-00304f19f545") && dbObject.ObjectType == MetaDataHelper.GetObjectTypeID(new Guid("cad00227-306c-11d8-b4e9-00304f19f545")))
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0020b-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null && attributeByGuid.AsInteger != 0L && ((IDBObjectLinkAttribute) attributeByGuid).DBObject.GetAttributeByGuid(PortalConsts.attributeImportedTableData) != null)
          throw new Exception($"Невозможно восстановить ссылку на {((IDBObjectLinkAttribute) attributeByGuid).DBObject.NameInMessages} так как существует неразрешенный конфликт импорта данных");
      }
      record.IntegerValue = (object) dbObject.ObjectID;
      record.StringValue = (object) dbObject.Caption;
    }
  }

  private IDBObject FindRankObject(IUserSession session, string rankName)
  {
    if (rankName == null || rankName == string.Empty)
      return (IDBObject) null;
    IDBAttributeType attrType = (IDBAttributeType) null;
    if (Intermech.Kernel.Briefcase.Helper.FindAttribute(session as UserSession, out attrType, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), "", "") != CheckResult.FindByGuid)
      return (IDBObject) null;
    IDBObjectType objectType = session.GetObjectType(new Guid("cad00147-306c-11d8-b4e9-00304f19f545"), false);
    if (objectType != null)
    {
      DataTable dataTable = session.GetObjectCollection(objectType.ObjectType).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(attrType.AttributeID, RelationalOperators.Equal, (object) rankName, LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -2 }));
      if (dataTable.Rows.Count > 0)
      {
        long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
        return session.GetObject(int64, false);
      }
    }
    return (IDBObject) null;
  }

  private IDBObject FindArchiveObject(IUserSession session, string archiveName)
  {
    DataTable dataTable = session.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cad0011e-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cad00020-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) archiveName, LogicalOperators.AND, 0, true)
    }, new object[1]{ (object) -2 }));
    return dataTable.Rows.Count > 0 ? session.GetObject(Convert.ToInt64(dataTable.Rows[0][0])) : (IDBObject) null;
  }

  private IDBObject FindMaterialObject(IUserSession session, string materialName)
  {
    if (materialName == null || materialName == string.Empty)
      return (IDBObject) null;
    string conditionValue = materialName;
    int length = conditionValue.IndexOf("&^");
    if (length > 0)
      conditionValue = conditionValue.Substring(0, length);
    IDBAttributeType attrType = (IDBAttributeType) null;
    if (Intermech.Kernel.Briefcase.Helper.FindAttribute(session as UserSession, out attrType, new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), "", "") != CheckResult.FindByGuid)
      return (IDBObject) null;
    IDBObjectType objectType = session.GetObjectType(new Guid("cad00170-306c-11d8-b4e9-00304f19f545"), false);
    if (objectType != null)
    {
      DataTable dataTable = session.GetObjectCollection(objectType.ObjectType).Select(new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(attrType.AttributeID, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, false)
      }, new object[1]{ (object) -2 }));
      if (dataTable.Rows.Count > 0)
      {
        long int64 = Convert.ToInt64(dataTable.Rows[0][0]);
        return session.GetObject(int64, false);
      }
    }
    if (ServerServices.GetService(typeof (IImbaseUpdatingService)) is IImbaseUpdatingService service)
    {
      List<Tuple<int, object>> data = new List<Tuple<int, object>>(1);
      data.Add(new Tuple<int, object>(attrType.AttributeID, (object) conditionValue));
      Guid guid = new Guid("cad008db-306c-11d8-b4e9-00304f19f545");
      Tuple<long, long> tuple = service.SearchData(session.SessionGUID, guid, data);
      if (tuple != null && tuple.Item1 != 0L && tuple.Item2 != -1L && session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService)
      {
        IDBObject dbObject = session.GetObject(guid, false);
        long objectID = customService.CreateObject(session.SessionGUID, dbObject.ObjectID, tuple.Item1, tuple.Item2, true, -1);
        return session.GetObject(objectID, false);
      }
    }
    IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad0081d-306c-11d8-b4e9-00304f19f545"));
    if (objectCollection == null)
      return (IDBObject) null;
    IDBObject materialObject = objectCollection.Create();
    IDBAttribute dbAttribute = materialObject.Attributes.AddAttribute(attrType.AttributeID, false);
    if (dbAttribute != null)
      dbAttribute.AsString = conditionValue;
    materialObject.CommitCreation(true);
    return materialObject;
  }
}
