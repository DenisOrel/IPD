// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttributeService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBAttributeService : CreatorContainer, IDBAttributeService
{
  public IDBAttribute CreateAttribute(
    IUserSession uSession,
    DataTable table,
    int index,
    bool temporary,
    IDBAttributable parent)
  {
    if (table.Rows.Count == 0)
      throw new KernelException(sc_13787.ssp_appserver_13788());
    int int32_1 = Convert.ToInt32(table.Rows[index]["F_ATTRIBUTE_ID"]);
    UserSession uSession1 = uSession as UserSession;
    DataRow attributeTypeRow = uSession1.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) int32_1);
    Guid guid = attributeTypeRow != null ? new Guid(Convert.ToString(attributeTypeRow["F_GUID"])) : throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13787.ssp_appserver_13789()), (object) int32_1));
    if (this.GetCreator((object) guid) is IDBAttributeCreator creator)
      return creator.CreateAttribute((IUserSession) uSession1, guid, attributeTypeRow, table, index);
    FieldTypes int32_2 = (FieldTypes) Convert.ToInt32(attributeTypeRow["F_ATTRIBUTE_TYPE"]);
    switch (int32_2)
    {
      case FieldTypes.ftString:
        return (IDBAttribute) new DBStringAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftInteger:
        return (IDBAttribute) new DBIntegerAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftDouble:
        return (IDBAttribute) new DBDoubleAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftDateTime:
        return (IDBAttribute) new DBDateAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftShortBlob:
        return (IDBAttribute) new DBShortBlobAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftFile:
        return (IDBAttribute) new DBFileAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftExternalLink:
        return (IDBAttribute) new DBExternalLinkAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftObjectLink:
        return (IDBAttribute) new DBObjectLinkAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftPassword:
        return (IDBAttribute) new DBEncryptedAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftMemo:
        return (IDBAttribute) new DBMemoAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftBlob:
        return (IDBAttribute) new DBBlobAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftBoolean:
        return (IDBAttribute) new DBBoolAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftMeasured:
        return (IDBAttribute) new DBMeasureAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftAutoInc:
        return (IDBAttribute) new DBAutoincrementAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftGuid:
        return (IDBAttribute) new DBGuidAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      case FieldTypes.ftObjectLinkByID:
        return (IDBAttribute) new DBObjectLinkByIDAttribute(uSession1, attributeTypeRow, table, index, temporary, parent as DBAttributable);
      default:
        throw new KernelExceptionID(sc_13787.ssp_appserver_13790(190188885), (object) int32_2);
    }
  }

  public IDBAttribute GetObjectAttribute(
    IUserSession uSession,
    long objectID,
    int attributeID,
    IDBAttributable parent)
  {
    if (attributeID < 0)
      throw new KernelException("Интерфейс IDBAttribute для системных атрибутов не поддерживается.");
    UserSession userSession = uSession as UserSession;
    string attributesTableName = userSession.DBCache.GetAttributesTableName(parent.TypeID);
    DataTable table = userSession.DataManager.ExecuteDataTable($"SELECT * FROM {attributesTableName} WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID ORDER BY F_INLIST_ID", userSession.DataManager.Parameter("objID", (object) objectID), userSession.DataManager.Parameter("attrID", (object) attributeID));
    return table.Rows.Count == 0 ? (IDBAttribute) null : this.CreateAttribute(uSession, table, 0, false, parent);
  }

  public IDBAttribute GetRelationAttribute(
    IUserSession uSession,
    long relationID,
    int attributeID,
    IDBAttributable parent)
  {
    if (attributeID < 0)
      throw new KernelException("Интерфейс IDBAttribute для системных атрибутов не поддерживается.");
    IDbManager dataManager = (uSession as UserSession).DataManager;
    DataTable table = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATION_ATTRS WHERE F_PRJLINK_ID = :plinkID AND F_ATTRIBUTE_ID = :attrID ORDER BY F_INLIST_ID", dataManager.Parameter("plinkID", (object) relationID), dataManager.Parameter("attrID", (object) attributeID));
    return table.Rows.Count == 0 ? (IDBAttribute) null : this.CreateAttribute(uSession, table, 0, false, parent);
  }
}
