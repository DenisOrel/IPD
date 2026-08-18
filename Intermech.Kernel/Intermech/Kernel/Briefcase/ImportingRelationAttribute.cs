// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportingRelationAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportingRelationAttribute(
  UserSession session,
  ImportEventLog eventLog,
  SetImportProgressEventHandler setImportProgressEvent) : ImportingAttribute(session, eventLog, setImportProgressEvent, "связь {0}")
{
  protected override bool CheckAdded(
    DataTable typesTable,
    int attributeID,
    string attributeName,
    long attributableID,
    int typeID)
  {
    int conformityRelationType = Helper.GetConformityRelationType((IUserSession) this.session, typesTable, typeID);
    if (conformityRelationType == -1)
      return false;
    IDBRelationType relationType = this.session.GetRelationType(conformityRelationType);
    if (!relationType.AnyAttributes && relationType.Attributes.GetAttributeByID(attributeID, false) == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1155"), (object) attributeName, (object) attributableID, (object) relationType.Description));
    return true;
  }
}
