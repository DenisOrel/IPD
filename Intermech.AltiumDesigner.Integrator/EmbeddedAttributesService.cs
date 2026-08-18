// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.EmbeddedAttributesService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class EmbeddedAttributesService(IIntegrator owner) : IntegratorService(owner)
{
  protected override void DoInitialize() => base.DoInitialize();

  public IAttributeCodec GetDocumentCodec()
  {
    this.RequireReadyState();
    throw new NotImplementedException();
  }

  public IAttributeCodec GetPartCodec()
  {
    this.RequireReadyState();
    throw new NotImplementedException();
  }

  public IAttributeCodec GetAssemblyCodec()
  {
    this.RequireReadyState();
    throw new NotImplementedException();
  }
}
