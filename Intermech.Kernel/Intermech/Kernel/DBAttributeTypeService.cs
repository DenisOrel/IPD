// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeTypeService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBAttributeTypeService : CreatorContainer, IDBAttributeTypeService
{
  public virtual IDBAttributeType GetDBAttributeType(
    IUserSession uSession,
    int attributeID,
    bool failIfNotFound)
  {
    UserSession us = uSession as UserSession;
    DataRow row = us.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID);
    if (row == null)
    {
      DataTable dataTable = us.DataManager.ExecuteDataTable("SELECT * FROM IMS_ATTRIBUTES WHERE F_ATTRIBUTE_ID = :attrID", us.DataManager.Parameter("attrID", (object) attributeID));
      if (dataTable.Rows.Count > 0)
      {
        row = dataTable.Rows[0];
        us.DBCache.ReloadTables(uSession, us.DataManager, "IMS_ATTRIBUTES", "IMS_ATTR4OBJ_TYPES", "IMS_ATTR4RELATION_TYPES", "IMS_FORMULA_ATTRS", "IMS_POSSIBLE_VALUES");
      }
      else
      {
        if (failIfNotFound)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_644"), (object) attributeID));
        return (IDBAttributeType) null;
      }
    }
    return DBAttributeTypeService.GetDBAttributeType(us, row);
  }

  internal static IDBAttributeType GetDBAttributeType(UserSession us, DataRow row)
  {
    IDBAttributeType dbAttributeType;
    switch (Convert.ToInt32(row["F_ATTRIBUTE_TYPE"]))
    {
      case 1:
        dbAttributeType = (IDBAttributeType) new DBStringAttributeType(us, row);
        break;
      case 2:
        dbAttributeType = (IDBAttributeType) new DBIntegerAttributeType(us, row);
        break;
      case 3:
        dbAttributeType = (IDBAttributeType) new DBDoubleAttributeType(us, row);
        break;
      case 4:
        dbAttributeType = (IDBAttributeType) new DBDateAttributeType(us, row);
        break;
      case 5:
        dbAttributeType = (IDBAttributeType) new DBShortBlobAttributeType(us, row);
        break;
      case 6:
        dbAttributeType = (IDBAttributeType) new DBFileAttributeType(us, row);
        break;
      case 7:
        dbAttributeType = (IDBAttributeType) new DBExternalLinkAttributeType(us, row);
        break;
      case 8:
        dbAttributeType = (IDBAttributeType) new DBObjectLinkAttributeType(us, row);
        break;
      case 9:
        dbAttributeType = (IDBAttributeType) new DBEncryptedAttributeType(us, row);
        break;
      case 10:
        dbAttributeType = (IDBAttributeType) new DBMemoAttributeType(us, row);
        break;
      case 11:
        dbAttributeType = (IDBAttributeType) new DBBlobAttributeType(us, row);
        break;
      case 12:
        dbAttributeType = (IDBAttributeType) new DBBoolAttributeType(us, row);
        break;
      case 13:
        dbAttributeType = (IDBAttributeType) new DBMeasureAttributeType(us, row);
        break;
      case 14:
        dbAttributeType = (IDBAttributeType) new DBAutoincrementAttributeType(us, row);
        break;
      case 15:
        dbAttributeType = (IDBAttributeType) new DBSystemAttributeType(us, row);
        break;
      case 16 /*0x10*/:
        dbAttributeType = (IDBAttributeType) new DBGuidAttributeType(us, row);
        break;
      case 17:
        dbAttributeType = (IDBAttributeType) new DBObjectLinkByIDAttributeType(us, row);
        break;
      default:
        dbAttributeType = (IDBAttributeType) new DBAttributeType(us, row);
        break;
    }
    return dbAttributeType;
  }
}
