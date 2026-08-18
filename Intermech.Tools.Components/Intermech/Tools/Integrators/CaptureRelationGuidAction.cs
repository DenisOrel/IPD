// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CaptureRelationGuidAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces.Data.Actions;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

internal sealed class CaptureRelationGuidAction : IAction
{
  private SectionEntity relationItem;
  private CreateRelationAction createAction;

  public CaptureRelationGuidAction(SectionEntity relationItem, CreateRelationAction createAction)
  {
    if (relationItem == null)
      throw new ArgumentNullException();
    if (createAction == null)
      throw new ArgumentNullException();
    this.relationItem = relationItem;
    this.createAction = createAction;
  }

  public void Perform()
  {
    this.relationItem.Sections.Get<RelationSection>().RelationGuid = this.createAction.RelationGuid;
  }

  public override string ToString() => LocalizationHolder.rm.GetString("SR_553");
}
