// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FillObjectTypeAttributeHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

internal sealed class FillObjectTypeAttributeHandler : IAction
{
  private SectionEntity objItem;
  private string attributeName;

  public FillObjectTypeAttributeHandler(SectionEntity objItem, string attributeName)
  {
    if (objItem == null)
      throw new ArgumentNullException();
    if (string.IsNullOrEmpty(attributeName))
      throw new ArgumentException();
    this.objItem = objItem;
    this.attributeName = attributeName;
  }

  public void Perform()
  {
    AttributesSection attributesSection = this.objItem.Sections.Get<AttributesSection>();
    if (!attributesSection.WorkingSet.CanUpdate((StringKey) this.attributeName, typeof (string), true))
      return;
    int objectType = ObjectSection.GetObjectType(this.objItem);
    string objectTypeName;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectTypeName = sessionKeeper.Session.GetObjectType(objectType, true).ObjectTypeName;
    attributesSection.WorkingSet.Update((StringKey) this.attributeName, (object) objectTypeName);
  }
}
