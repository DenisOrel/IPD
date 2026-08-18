// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBSiteObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Protection;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

public class DBSiteObject(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  internal static bool autoCreate;

  protected override void DoBeforeCommitCreation()
  {
    if (DBSiteObject.autoCreate)
      return;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeSiteCode);
    char ch = SqlHelper.NextLetter(this.Session.GetObjectCollection(this.ObjectType).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[1]
    {
      (object) attributeTypeId
    })).Rows);
    this.Attributes.AddAttribute(attributeTypeId, false, new object[1]
    {
      (object) ch
    });
    IDBRelationCollection relationCollection = this.UserSession.GetRelationCollection(this.UserSession.IdentHelper.SimpleRelationTypeID);
    relationCollection.ChildObjectTypes = (IList<int>) new int[1]
    {
      this.UserSession.IdentHelper.UsersTypeID
    };
    DataTable dataTable = relationCollection.ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-17, RelationalOperators.Empty, (object) null, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }), this.ObjectID);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      (this.UserSession.GetObject(Convert.ToInt64(dataTable.Rows[index][0])) as DBObject).SetSiteID(string.Format("{0}{0}", (object) ch));
  }

  protected override void DoAfterCommitCreation()
  {
    if (DBSiteObject.autoCreate)
      return;
    ((ISitesCacheService) this.Session.GetCustomService(typeof (ISitesCacheService))).Reload((object) this.Session);
  }

  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    if (DBSiteObject.autoCreate || !((ISiteServerService) this.Session.GetCustomService(typeof (ISiteServerService))).Initialized)
      return;
    ISitesCacheService customService1 = (ISitesCacheService) this.Session.GetCustomService(typeof (ISitesCacheService));
    if (customService1.Info == null || !customService1.Info.GUID.Equals(this.ObjectGUID))
      return;
    IDBObject objectById = this.Session.GetObjectByID(newrelation.PartID, true);
    if (objectById.ObjectType != this.Session.IdentHelper.UsersTypeID)
      return;
    IPortalConnector customService2 = (IPortalConnector) this.Session.GetCustomService(typeof (IPortalConnector));
    if (!customService2.IsAdmin(this.UserSession.SessionGUID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_995"), (object) objectById.NameInMessages, (object) customService1.Info.Caption));
    string asString1 = objectById.GetAttributeByGuid(new Guid("cad0001d-306c-11d8-b4e9-00304f19f545")).AsString;
    string asString2 = objectById.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AsString;
    IDBAttribute attributeByGuid = objectById.GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545"));
    if (asString1 == string.Empty || asString2 == string.Empty)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_996"), (object) this.NameInMessages));
    if (attributeByGuid.Values != null && attributeByGuid.Values[0] is PswPackage password)
      customService2.AddUser(this.Session.SessionGUID, asString1, asString2, password, objectById.ObjectGUID);
    else
      customService2.AddUser(this.Session.SessionGUID, asString1, asString2, attributeByGuid.AsString, objectById.ObjectGUID);
    (objectById as DBObject).SetSiteID(string.Format("{0}{0}{0}", (object) customService1.Info.Code));
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    if (!((ISiteServerService) this.Session.GetCustomService(typeof (ISiteServerService))).Initialized)
      return;
    ISitesCacheService customService1 = (ISitesCacheService) this.Session.GetCustomService(typeof (ISitesCacheService));
    if (customService1.Info == null || !customService1.Info.GUID.Equals(this.ObjectGUID))
      return;
    IDBObject objectById = this.Session.GetObjectByID(relation.PartID, true);
    if (objectById.ObjectType != this.Session.IdentHelper.UsersTypeID)
      return;
    IPortalConnector customService2 = (IPortalConnector) this.Session.GetCustomService(typeof (IPortalConnector));
    if (!customService2.IsAdmin(this.UserSession.SessionGUID))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_997"), (object) objectById.NameInMessages, (object) customService1.Info.Caption));
    customService2.DeleteUser(this.Session.SessionGUID, objectById.GetAttributeByGuid(new Guid("cad00018-306c-11d8-b4e9-00304f19f545")).AsString);
  }

  protected override void DoDelete()
  {
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(PortalConsts.attributeAutoCreateUsers);
    if (attributeByGuid == null)
      return;
    List<long> longList = new List<long>(attributeByGuid.ValuesCount);
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      if (attributeByGuid.Values[index] != null && attributeByGuid.Values[index] != DBNull.Value)
        longList.Add((long) attributeByGuid.Values[index]);
    }
    for (int index = 0; index < longList.Count; ++index)
      this.UserSession.GetObject(longList[index], false)?.Delete(0L);
  }

  protected override void DoCommitCreation()
  {
    DataTable dataTable = this.UserSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad00022-306c-11d8-b4e9-00304f19f545")).ConsistFrom(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(-7, RelationalOperators.Equal, (object) this.UserSession.IdentHelper.UsersTypeID, LogicalOperators.AND, 0, false)
    }, new object[1]{ (object) -2 }), this.ObjectID);
    IDBAttribute dbAttribute = this.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(PortalConsts.attributeAutoCreateUsers), false);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      long int64 = Convert.ToInt64(dataTable.Rows[index][0]);
      if (index == 0)
        dbAttribute.Value = (object) int64;
      else
        dbAttribute.AddValue((object) int64);
    }
  }
}
