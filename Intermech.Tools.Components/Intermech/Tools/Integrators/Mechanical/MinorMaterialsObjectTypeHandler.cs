// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MinorMaterialsObjectTypeHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Tools.Components.Properties;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal sealed class MinorMaterialsObjectTypeHandler : IAction
{
  private SectionEntity articleItem;
  private StringKey attributeKey;

  public MinorMaterialsObjectTypeHandler(SectionEntity articleItem, string attributeKey)
  {
    if (articleItem == null)
      throw new ArgumentNullException();
    if (attributeKey == null)
      throw new ArgumentNullException();
    this.articleItem = articleItem;
    this.attributeKey = (StringKey) attributeKey;
  }

  public void Perform()
  {
    this.articleItem.Sections.Get<AttributesSection>().WorkingSet.Update(this.attributeKey, (object) CADDocumentResources.EMB_MaterialsSection);
  }
}
