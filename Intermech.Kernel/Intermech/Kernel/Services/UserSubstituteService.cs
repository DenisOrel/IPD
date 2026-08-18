// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.UserSubstituteService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.CustomServices;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Services;

public class UserSubstituteService : LongLifeObject, IUserSubstituteService
{
  public List<UserSubstitute> GetUserSubstitutes(Guid sessionGUID, long userId)
  {
    if (!(UserSession.GetSessionByID(sessionGUID) is UserSession sessionById))
      throw new KernelException($"Сессия с гуидом {sessionGUID} не найдена.");
    List<UserSubstitute> userSubstitutes = new List<UserSubstitute>();
    DataTable dataTable = sessionById.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cadd94e2-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeID((object) new Guid("cad015c9-306c-11d8-b4e9-00304f19f545")), RelationalOperators.Equal, (object) userId, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cadd91f5-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cadd94e4-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cadd94e3-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) new Guid("cadd94e6-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
    }));
    if (dataTable != null)
    {
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        if (dataTable.Rows[index][0] != DBNull.Value && dataTable.Rows[index][1] != DBNull.Value)
        {
          DateTime dateTime1 = DateTime.MinValue;
          if (dataTable.Rows[index][2] != DBNull.Value)
            dateTime1 = Convert.ToDateTime(dataTable.Rows[index][2]);
          DateTime dateTime2 = DateTime.MinValue;
          if (dataTable.Rows[index][3] != DBNull.Value)
            dateTime2 = Convert.ToDateTime(dataTable.Rows[index][3]);
          string empty = string.Empty;
          if (dataTable.Rows[index][4] != DBNull.Value)
            empty = Convert.ToString(dataTable.Rows[index][4]);
          UserSubstitute userSubstitute = new UserSubstitute()
          {
            SettingsId = Convert.ToInt64(dataTable.Rows[index][0]),
            SubstituteUserName = Convert.ToString(dataTable.Rows[index][1]),
            BeginDate = dateTime1,
            EndDate = dateTime2,
            RoleCaption = empty
          };
          userSubstitutes.Add(userSubstitute);
        }
      }
    }
    return userSubstitutes;
  }

  public List<ObjectIOSettings> GetUsersIOSettings(Guid sessionGUID, string userCaption)
  {
    if (!(UserSession.GetSessionByID(sessionGUID) is UserSession sessionById))
      throw new KernelException($"Сессия с гуидом {sessionGUID} не найдена.");
    if (!(sessionById.GetCustomService(typeof (IRolesService)) is IRolesService customService))
      throw new KernelException("Не найдена служба для получения информации о ролях пользователей.");
    List<ObjectIOSettings> usersIoSettings = new List<ObjectIOSettings>();
    List<long> longList = new List<long>();
    DataTable dataTable = sessionById.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cadd94e2-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(MetaDataHelper.GetAttributeTypeID("cadd91f5-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) userCaption, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[5]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cadd94e4-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cadd94e3-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cadd94e6-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.Index, SortOrders.NONE, -1),
      new ColumnDescriptor((object) MetaDataHelper.GetAttributeTypeID("cadd94e6-306c-11d8-b4e9-00304f19f545"), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1)
    }));
    if (dataTable == null || dataTable.Rows.Count == 0)
      return usersIoSettings;
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long int64 = Convert.ToInt64(row[0]);
      DateTime dateTime1 = row[1] == DBNull.Value || row[1] == null ? DateTime.MinValue : Convert.ToDateTime(row[1]);
      string beginDate = dateTime1 != DateTime.MinValue ? dateTime1.ToString() : "";
      DateTime dateTime2 = row[2] == DBNull.Value || row[2] == null ? DateTime.MinValue : Convert.ToDateTime(row[2]);
      string endDate = dateTime2 != DateTime.MinValue ? dateTime2.ToString() : "";
      long roleID = 0;
      string roleName = "";
      if (row[3] != DBNull.Value && row[3] != null)
      {
        roleID = Convert.ToInt64(row[3]);
        roleName = Convert.ToString(row[4]);
      }
      List<MyElement> list = this.AttributeValuesToList(sessionById.GetObjectAttribute(int64, (object) new Guid("cad015c9-306c-11d8-b4e9-00304f19f545"), false, false));
      if (list.Count != 0)
      {
        List<RoleProperties> commonUsersRoles = this.GetCommonUsersRoles(list, customService);
        usersIoSettings.Add(new ObjectIOSettings(int64, list, beginDate, endDate, new RoleProperties(roleID, roleName), commonUsersRoles, false));
      }
    }
    return usersIoSettings;
  }

  public List<long> SaveIoSettings(
    Guid sessionGUID,
    List<ObjectIOSettings> ioSettings,
    long userId)
  {
    IDBObjectCollection objectCollection = UserSession.GetSessionByID(sessionGUID) is UserSession sessionById ? sessionById.GetObjectCollection(MetaDataHelper.GetObjectTypeID("cadd94e2-306c-11d8-b4e9-00304f19f545")) : throw new KernelException($"Сессия с гуидом {sessionGUID} не найдена.");
    List<long> longList = new List<long>();
    foreach (ObjectIOSettings ioSetting in ioSettings)
    {
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      IDBObject dbObject;
      if (ioSetting.ID < 0L)
      {
        dbObject = objectCollection.Create();
        dbObject.GetAttributeByID(MetaDataHelper.GetAttributeID((object) new Guid("cadd91f5-306c-11d8-b4e9-00304f19f545"))).Value = (object) userId;
        if (dbObject.IsCreationMode)
          dbObject.CommitCreation(true, true);
        ioSetting.ID = dbObject.ObjectID;
      }
      else
        dbObject = sessionById.GetObject(ioSetting.ID);
      object[] array = ioSetting.IoList.Select<MyElement, object>((System.Func<MyElement, object>) (x => x.Value)).ToArray<object>();
      attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cad015c9-306c-11d8-b4e9-00304f19f545")), (object) array));
      if (ioSetting.BeginDate != "" && Convert.ToDateTime(ioSetting.BeginDate) != DateTime.MinValue)
        attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd94e4-306c-11d8-b4e9-00304f19f545")), (object) Convert.ToDateTime(ioSetting.BeginDate)));
      else
        attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd94e4-306c-11d8-b4e9-00304f19f545")), (object) DeleteModesEnum.None));
      if (ioSetting.EndDate != "" && Convert.ToDateTime(ioSetting.EndDate) != DateTime.MinValue)
        attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd94e3-306c-11d8-b4e9-00304f19f545")), (object) Convert.ToDateTime(ioSetting.EndDate)));
      else
        attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd94e3-306c-11d8-b4e9-00304f19f545")), (object) DeleteModesEnum.None));
      if (ioSetting.CurrentRole != null && ioSetting.CurrentRole.RoleID != 0L)
        attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd94e6-306c-11d8-b4e9-00304f19f545")), (object) ioSetting.CurrentRole.RoleID));
      else
        attributeValuesList.Add(new AttributeValues(MetaDataHelper.GetAttributeID((object) new Guid("cadd94e6-306c-11d8-b4e9-00304f19f545")), (object) DeleteModesEnum.None));
      dbObject.SetAttributesValues(attributeValuesList.ToArray());
      longList.Add(dbObject.ObjectID);
    }
    return longList;
  }

  private List<MyElement> AttributeValuesToList(IDBAttribute attribute)
  {
    List<MyElement> list = new List<MyElement>();
    for (int index = 0; index < attribute.ValuesCount; ++index)
    {
      if (attribute.Values[index] != null && attribute.Values[index] != DBNull.Value)
        list.Add(new MyElement((object) Convert.ToInt64(attribute.Values[index]), attribute.Descriptions[index], (object) null));
    }
    return list;
  }

  private List<RoleProperties> GetCommonUsersRoles(
    List<MyElement> users,
    IRolesService rolesService)
  {
    List<long> list1 = users.Select<MyElement, long>((System.Func<MyElement, long>) (x => (long) x.Value)).ToList<long>();
    List<RoleProperties> list2 = ((IEnumerable<RoleProperties>) rolesService.GetRolesList(list1[0])).ToList<RoleProperties>();
    for (int index = 1; index < list1.Count; ++index)
    {
      RoleProperties[] rolesList = rolesService.GetRolesList(list1[index]);
      list2 = list2.Intersect<RoleProperties>((IEnumerable<RoleProperties>) rolesList, (IEqualityComparer<RoleProperties>) new RolePropertiesComparer()).ToList<RoleProperties>();
    }
    return list2;
  }
}
