// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.Import.RelationAttributesXMLParser
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services.PortalServices.Import;

internal class RelationAttributesXMLParser(Dictionary<Guid, ImportedInfo> links, string path) : 
  AttributesXMLParser<IDBRelationType, ImportingRelation>(links, path)
{
  protected override long UnknownAttributableId => 0;

  protected override bool CheckAttribute4Type(
    IDBRelationType parent,
    IDBAttributeType attrType,
    IEventLogHelper eventHelper)
  {
    if (parent.AnyAttributes || parent.Attributes.GetAttributeByID(attrType.AttributeID, false) != null)
      return true;
    eventHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_1158"), (object) parent.Description, (object) attrType.Name), Consts.traceAlways, string.Empty);
    return false;
  }
}
