// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.FillDocumentCodeHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class FillDocumentCodeHandler : IAction
{
  private readonly SectionEntity docItem;

  public FillDocumentCodeHandler(SectionEntity docItem)
  {
    this.docItem = docItem != null ? docItem : throw new ArgumentNullException();
  }

  public void Perform()
  {
    AttributesSection attributesSection = this.docItem.Sections.Get<AttributesSection>();
    if (!attributesSection.WorkingSet.CanUpdate((StringKey) CADDocumentResources.EMB_DocumentCode, typeof (string), true))
      return;
    string docCode = DocumentDesignationHelper.GetDocCode(ObjectSection.GetObjectType(this.docItem));
    if (string.IsNullOrEmpty(docCode))
      return;
    attributesSection.WorkingSet.Update((StringKey) CADDocumentResources.EMB_DocumentCode, (object) docCode);
  }
}
