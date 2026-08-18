// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRoleObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel;

public class DBRoleObject(UserSession uSession, DataTable objectParams) : DBObject(uSession, objectParams)
{
  internal static long AdminRoleID;
  internal static List<long> AdminRoles;

  internal static void ReloadRolesCache()
  {
    (ServerServices.GetService(typeof (IDelayedUpdaterService)) as IDelayedUpdaterService).ReloadRolesCache();
  }

  public static bool IsAdminRole(long roleID)
  {
    if (DBRoleObject.AdminRoles == null)
      return roleID == DBRoleObject.AdminRoleID;
    lock (DBRoleObject.AdminRoles)
      return DBRoleObject.AdminRoles.Contains(roleID);
  }

  internal static void SetRolesList(List<long> rolesID)
  {
    DBRoleObject.AdminRoles = new List<long>((IEnumerable<long>) rolesID);
  }

  internal static long[] GetAdminRoles()
  {
    lock (DBRoleObject.AdminRoles)
      return DBRoleObject.AdminRoles.ToArray();
  }

  protected override void DoBeforeCommitCreation()
  {
    IDBObject dbObject = this.UserSession.GetObject(new Guid("cad00693-306c-11d8-b4e9-00304f19f545"), false);
    long objectId = dbObject != null ? dbObject.ObjectID : 0L;
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID("cad00692-306c-11d8-b4e9-00304f19f545");
    if (objectId != 0L && attributeTypeId != 0)
      this.SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(attributeTypeId, (object) objectId)
      });
    base.DoBeforeCommitCreation();
  }

  protected override void DoAfterCommitCreation()
  {
    base.DoAfterCommitCreation();
    DBRoleObject.ReloadRolesCache();
  }

  public override int Delete(long DeleteMode)
  {
    int num = base.Delete(DeleteMode);
    DBRoleObject.ReloadRolesCache();
    return num;
  }

  protected override void DoBeforeSetAdditionalAttributeValue(
    IDBAttribute attribute,
    object newValue)
  {
    if ((attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cadd93aa-306c-11d8-b4e9-00304f19f545") || attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cadd93a9-306c-11d8-b4e9-00304f19f545") || attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cad014b5-306c-11d8-b4e9-00304f19f545") || attribute.AttributeID == MetaDataHelper.GetAttributeTypeID("cadd93ab-306c-11d8-b4e9-00304f19f545")) && newValue != null && newValue != DBNull.Value && this.ObjectGUID == new Guid("cad00006-306c-11d8-b4e9-00304f19f545") && Convert.ToBoolean(newValue))
      throw new KernelExceptionID(392);
    base.DoBeforeSetAdditionalAttributeValue(attribute, newValue);
  }

  protected override void DoBeforeDeleteRelation(IDBRelation relation, long deleteMode)
  {
    this.RemoveIoSettingsForCurrentRoleAndUserIfExist(this.UserSession.GetObjectBaseVersionByID(relation.PartID, false).ObjectID);
  }

  private void RemoveIoSettingsForCurrentRoleAndUserIfExist(long userId)
  {
    List<long> idsWithCurrentRole = this.GetIoSettingsIdsWithCurrentRole();
    if (idsWithCurrentRole.Count == 0)
      return;
    foreach (long objectID in idsWithCurrentRole)
    {
      IDBObject dbObject = this.UserSession.GetObject(objectID, false);
      if (dbObject != null)
      {
        IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad015c9-306c-11d8-b4e9-00304f19f545"));
        if (attributeByGuid != null)
        {
          List<long> list = attributeByGuid.Values.OfType<long>().ToList<long>();
          if (list.Contains(userId))
          {
            if (list.Count == 1)
            {
              dbObject.Delete(0L);
            }
            else
            {
              list.Remove(userId);
              AttributeValues attributeValues = new AttributeValues(MetaDataHelper.GetAttributeTypeID(new Guid("cad015c9-306c-11d8-b4e9-00304f19f545")), (object) list.OfType<object>().ToArray<object>());
              dbObject.SetAttributesValues(new AttributeValues[1]
              {
                attributeValues
              });
            }
          }
        }
      }
    }
  }

  private List<long> GetIoSettingsIdsWithCurrentRole()
  {
    List<long> idsWithCurrentRole = new List<long>();
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cadd94e2-306c-11d8-b4e9-00304f19f545");
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(new Guid("cadd94e6-306c-11d8-b4e9-00304f19f545"));
    DataTable dataTable = this.UserSession.GetObjectCollection(objectTypeId).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(attributeTypeId, RelationalOperators.Equal, (object) this.ObjectID, LogicalOperators.NONE, 0, false)
    }, new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, -1)
    }));
    if (dataTable == null || dataTable.Rows.Count == 0)
      return idsWithCurrentRole;
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      idsWithCurrentRole.Add(Convert.ToInt64(dataTable.Rows[index][0]));
    return idsWithCurrentRole;
  }
}
