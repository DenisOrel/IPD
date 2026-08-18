// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RestoreDefaultValues
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class RestoreDefaultValues(
  IUserSession session,
  List<IDСorresponds> importingObjectIDs,
  ImportEventLog eventLog) : RestoreImportingValues<SaveImportValues>(session, importingObjectIDs, eventLog)
{
  protected override void OnRestore(SaveImportValues item, BriefcaseImportProgress bip)
  {
    if (item.ObjectTypeID == -1)
    {
      if (item.RelationTypeID == -1)
      {
        try
        {
          this.SetDefaultValue4Attribute(this.session.GetAttributeType(item.AttributeTypeID, true), item);
          return;
        }
        catch (Exception ex)
        {
          this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_343"), (object) item.AttributeTypeID, (object) ex.Message));
          throw;
        }
      }
    }
    if (item.RelationTypeID == -1)
    {
      if (item.ObjectTypeID != -1)
      {
        try
        {
          this.SetDefaultValue4TypeAttribute((IDBAttributableType) this.session.GetObjectType(item.ObjectTypeID, true), item.AttributeTypeID, item);
        }
        catch (Exception ex)
        {
          this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_344"), (object) item.AttributeTypeID, (object) item.ObjectTypeID, (object) ex.Message));
          throw;
        }
      }
    }
    if (item.RelationTypeID == -1)
      return;
    if (item.ObjectTypeID != -1)
      return;
    try
    {
      this.SetDefaultValue4TypeAttribute((IDBAttributableType) this.session.GetRelationType(item.RelationTypeID, true), item.AttributeTypeID, item);
    }
    catch (Exception ex)
    {
      this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_345"), (object) item.AttributeTypeID, (object) item.RelationTypeID, (object) ex.Message));
      throw;
    }
  }

  private void SetDefaultValue4TypeAttribute(
    IDBAttributableType type,
    int attributeID,
    SaveImportValues item)
  {
    this.SetDefaultValue4Attribute((IDBAttributeType) type.Attributes.GetAttributeByID(attributeID), item);
  }

  private void SetDefaultValue4Attribute(IDBAttributeType attributeType, SaveImportValues item)
  {
    IDСorresponds idСorresponds = attributeType == null || item.Value == null ? (IDСorresponds) null : this.importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == Convert.ToInt64(item.Value)));
    if (idСorresponds == null)
      return;
    attributeType.DefaultValue = (object) idСorresponds.HostObjectID;
  }
}
