// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.MarkAsRequireTypeCheckAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Localization;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.UI;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

public sealed class MarkAsRequireTypeCheckAction : IAction
{
  private readonly SectionEntity docItem;

  public MarkAsRequireTypeCheckAction(SectionEntity docItem)
  {
    this.docItem = docItem != null ? docItem : throw new ArgumentNullException(nameof (docItem));
  }

  public void Perform()
  {
    if (!this.docItem.Sections.Get<ObjectSection>().RequireTypeCheck)
      return;
    AttributesSection attributesSection = this.docItem.Sections.Get<AttributesSection>();
    attributesSection.DatabaseSet.Update((StringKey) IDCache.Default.RequireTypeCheck.Text, (object) true);
    attributesSection.DatabaseSet.SetFlag((StringKey) IDCache.Default.RequireTypeCheck.Text, NamedFlags.ThrowSetException);
    if (!UIReport.Enabled)
      return;
    UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_516"), (object) DisplaySection.GetDisplayName(this.docItem)), TraceLevel.Warning);
  }
}
