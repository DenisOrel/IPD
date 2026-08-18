// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.SpecHandleAttributesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services;

public sealed class SpecHandleAttributesService : LongLifeObject, ISpecHandleAttributes
{
  private List<Guid> _notUpdatingAttributes = new List<Guid>();

  public event SpecHandleAttributeEventHandler SpecHandleRelationAttributeEvent;

  public event SpecHandleAttributeEventHandler SpecHandleObjectAttributeEvent;

  public void FireEventForObjectAttribute(SpecHandleAttributeEventArgs e)
  {
    if (this.SpecHandleObjectAttributeEvent == null)
      return;
    this.SpecHandleObjectAttributeEvent((object) this, e);
  }

  public void FireEventForRelationAttribute(SpecHandleAttributeEventArgs e)
  {
    if (this.SpecHandleRelationAttributeEvent == null)
      return;
    this.SpecHandleRelationAttributeEvent((object) this, e);
  }

  public void RegisterNotUpdatingAttribute(Guid attributeGuid)
  {
    if (this._notUpdatingAttributes.Contains(attributeGuid))
      return;
    this._notUpdatingAttributes.Add(attributeGuid);
  }

  public bool IsNotUpdatingAttribute(Guid attributeGuid)
  {
    return this._notUpdatingAttributes.Contains(attributeGuid);
  }
}
