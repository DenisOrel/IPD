// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ResotreMeasureValues
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Briefcase;

internal sealed class ResotreMeasureValues(
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
          IDBAttributeType attributeType = this.session.GetAttributeType(Convert.ToInt32(item.AttributeTypeID), true);
          IDСorresponds idСorresponds = item.Value != null ? this.importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == Convert.ToInt64(item.Value))) : (IDСorresponds) null;
          if (idСorresponds != null)
            attributeType.SizeType = idСorresponds.HostObjectID;
          if (item.MeasuredDefaultVAlue == null)
            return;
          attributeType.DefaultValue = item.MeasuredDefaultVAlue;
          return;
        }
        catch (Exception ex)
        {
          this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_346"), (object) item.AttributeTypeID, (object) ex.Message));
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
          IDBAttributeType4 attributeById = this.session.GetObjectType(item.ObjectTypeID, true).Attributes.GetAttributeByID(item.AttributeTypeID);
          IDСorresponds idСorresponds = attributeById == null || item.Value == null ? (IDСorresponds) null : this.importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == Convert.ToInt64(item.Value)));
          if (attributeById != null)
          {
            if (idСorresponds != null)
              attributeById.SizeType = idСorresponds.HostObjectID;
            if (item.MeasuredDefaultVAlue != null)
              attributeById.DefaultValue = item.MeasuredDefaultVAlue;
          }
        }
        catch (Exception ex)
        {
          this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_347"), (object) item.AttributeTypeID, (object) item.ObjectTypeID, (object) ex.Message));
          throw;
        }
      }
    }
    if (item.RelationTypeID == -1 || item.ObjectTypeID != -1)
      return;
    if (item.Value == null)
      return;
    try
    {
      IDBAttributeType4 attributeById = this.session.GetRelationType(item.RelationTypeID, true).Attributes.GetAttributeByID(item.AttributeTypeID);
      IDСorresponds idСorresponds = attributeById == null || item.Value == null ? (IDСorresponds) null : this.importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == Convert.ToInt64(item.Value)));
      if (attributeById == null)
        return;
      if (idСorresponds != null)
        attributeById.SizeType = idСorresponds.HostObjectID;
      if (item.MeasuredDefaultVAlue == null)
        return;
      attributeById.DefaultValue = item.MeasuredDefaultVAlue;
    }
    catch (Exception ex)
    {
      this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_348"), (object) item.AttributeTypeID, (object) item.RelationTypeID, (object) ex.Message));
      throw;
    }
  }
}
