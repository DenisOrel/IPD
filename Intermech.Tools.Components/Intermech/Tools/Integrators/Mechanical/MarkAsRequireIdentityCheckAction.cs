// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MarkAsRequireIdentityCheckAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Interfaces.Data.Actions;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class MarkAsRequireIdentityCheckAction : IAction
{
  private readonly CaptureChangesDriverContext ctx;
  private readonly SectionEntity item;

  public MarkAsRequireIdentityCheckAction(CaptureChangesDriverContext ctx, SectionEntity item)
  {
    if (ctx == null)
      throw new ArgumentNullException(nameof (ctx));
    if (item == null)
      throw new ArgumentNullException(nameof (item));
    this.ctx = ctx;
    this.item = item;
  }

  public void Perform()
  {
    SameDocSection sameDocSection = this.item.Sections.Get<SameDocSection>();
    SectionEntity objectEntity;
    if (sameDocSection.Reference.Value is long)
    {
      long objectId = (long) sameDocSection.Reference.Value;
      objectEntity = ObjectSection.FindByObjectId(this.ctx.Database, objectId, false) ?? this.ctx.Database.AddReferencedDBObject(objectId);
    }
    else
      objectEntity = (SectionEntity) sameDocSection.Reference.Value;
    AttributeValues[] attributeValues = DBAttributeHelper.ToAttributeValues((IList<ValueRecord>) new List<ValueRecord>()
    {
      new ValueRecord((StringKey) IDCache.Default.RequireIdentityCheck.Text, (object) sameDocSection.IdentityValue)
    });
    objectEntity.Sections.Get<ObjectActionsSection>().ObjectActions.ServerActions.Add((IAction) new WriteObjectAttributesAction((IDBObjectRef) new DBObjectEntityRef(objectEntity), attributeValues));
  }
}
