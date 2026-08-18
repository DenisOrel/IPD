// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckObject(
  UserSession session,
  DataSet metadata,
  ImportingObject briefObject) : CheckItem<IDBObject, ImportingObject>(session, metadata, 2, briefObject, CheckOptions.None)
{
  public long ObjectID;
  public long ID;

  protected override bool nullable => true;

  public override void Initialize()
  {
    this.UniIdentifiler = string.Format(BriefcaseConsts.logFormatObject, this.briefRow.Object.Caption == string.Empty ? (object) $"{{{this.briefRow.Object.ObjectGuid}}}" : (object) $"\"{this.briefRow.Object.Caption}\"");
  }

  protected override void OnCheck()
  {
    int conformityObjectType = Helper.GetConformityObjectType((IUserSession) this.session, this.metaData.Tables["IMS_OBJECT_TYPES"], this.briefRow.Object.ObjectType);
    if (conformityObjectType == -1)
    {
      DataRow dataRow = this.metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find((object) this.briefRow.Object.ObjectType);
      this.AddErrorToLog(string.Format(LocalizationHolder.rm.GetString("Kernel_944"), dataRow != null ? (object) Convert.ToString(dataRow["F_OBJ_TYPE_NAME"]) : (object) this.briefRow.Object.ObjectType.ToString()));
    }
    else
    {
      IDBObjectType objectType = this.session.GetObjectType(conformityObjectType);
      long objectID = 0;
      ObjectSearchEngine.FoundType foundType = ObjectSearchEngine.FoundType.None;
      try
      {
        objectID = ObjectSearchEngine.FindObject((IUserSession) this.session, objectType, this.briefRow, out this.ID, out foundType);
      }
      catch (Exception ex)
      {
        this.AddErrorToLog(ex.Message);
      }
      if (objectID == 0L)
        return;
      IDBObject dbObject = this.session.GetObject(objectID, false);
      string message;
      if (!this.CheckObjectType(dbObject, conformityObjectType, out message))
        this.AddErrorToLog(message);
      if (foundType != ObjectSearchEngine.FoundType.IDAttribute && !this.CheckGUID(dbObject, (Guid) this.briefRow.Object.IdGuid, out message))
        this.AddErrorToLog(message);
      this.ObjectID = dbObject.ObjectID;
    }
  }

  private bool CheckObjectType(IDBObject obj, int objTypeId, out string message)
  {
    if (obj.ObjectType != objTypeId)
    {
      message = $"Несоотвествие типов в портфеле ({objTypeId}) и базе назначения ({obj.ObjectType}) у объекта {obj.NameInMessages} ";
      return false;
    }
    message = string.Empty;
    return true;
  }

  private bool CheckGUID(IDBObject obj, Guid guid, out string message)
  {
    if (!obj.GUID.Equals(guid))
    {
      message = $"Несоотвествие Глобальных идентификаторов для версий объектов в портфеле ({guid}) и базе назначения ({obj.GUID}) у объекта {obj.NameInMessages} ";
      return false;
    }
    message = string.Empty;
    return true;
  }
}
