// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.SiteUserCreator`1
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

internal class SiteUserCreator<TPasswordType>
{
  public long Create(
    object session,
    string userName,
    string login,
    TPasswordType password,
    Guid userGuid,
    char siteCode)
  {
    IUserSession userSession = session is Guid sessionGUID ? UserSession.GetSessionByID(sessionGUID) : (IUserSession) session;
    Guid attrTypeGuid = new Guid("cad00018-306c-11d8-b4e9-00304f19f545");
    IDBObject dbObject = userSession.GetObjectCollection(userSession.IdentHelper.UsersTypeID).Create(userGuid);
    if (userSession.GetObjectType(userSession.IdentHelper.UsersTypeID).Attributes.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad0001d-306c-11d8-b4e9-00304f19f545")) is IDBAttributeType4Object attributeById && attributeById.Attribute4ObjectPropertiesStructure.ComputeValueMode == ComputeValueModes.NotComputableValue)
      dbObject.Attributes.AddAttribute(attributeById.AttributeID, false, new object[1]
      {
        (object) userName
      });
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID(attrTypeGuid), false, new object[1]
    {
      (object) login
    });
    dbObject.Attributes.AddAttribute(MetaDataHelper.GetAttributeTypeID("cad00019-306c-11d8-b4e9-00304f19f545"), false, new object[1]
    {
      (object) password
    });
    (dbObject as DBObject).SetSiteID(string.Format("{0}{0}", (object) siteCode));
    dbObject.CommitCreation(true);
    return dbObject.ObjectID;
  }

  public void ChangeUserPassword(object session, string login, TPasswordType password)
  {
    IUserSession userSession = session is Guid sessionGUID ? UserSession.GetSessionByID(sessionGUID) : (IUserSession) session;
    ConditionStructure conditionStructure = new ConditionStructure(new Guid("cad00018-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) login, LogicalOperators.AND, 0);
    DataTable dataTable = userSession.GetObjectCollection(userSession.IdentHelper.UsersTypeID).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      conditionStructure
    }, new object[1]{ (object) -2 }));
    if (dataTable.Rows.Count != 1)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1097"), (object) login));
    userSession.GetObject(Convert.ToInt64(dataTable.Rows[0][0]), true).GetAttributeByGuid(new Guid("cad00019-306c-11d8-b4e9-00304f19f545")).Value = (object) password;
  }
}
