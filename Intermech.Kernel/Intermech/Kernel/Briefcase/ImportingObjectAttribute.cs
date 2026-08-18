// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportingObjectAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportingObjectAttribute(
  UserSession session,
  ImportEventLog eventLog,
  SetImportProgressEventHandler setImportProgressEvent) : ImportingAttribute(session, eventLog, setImportProgressEvent, "объект {0}")
{
  protected override bool CheckAdded(
    DataTable typesTable,
    int attributeID,
    string attributeName,
    long attributableID,
    int typeID)
  {
    int conformityObjectType = Helper.GetConformityObjectType((IUserSession) this.session, typesTable, typeID);
    if (conformityObjectType == -1)
      return false;
    IDBObjectType objectType = this.session.GetObjectType(conformityObjectType);
    if (!objectType.AnyAttributes && objectType.Attributes.GetAttributeByID(attributeID, false) == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_983"), (object) attributeName, (object) attributableID, (object) objectType.ObjectTypeName));
    return true;
  }
}
