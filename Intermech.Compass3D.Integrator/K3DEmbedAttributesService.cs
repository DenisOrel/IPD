// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DEmbedAttributesService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DEmbedAttributesService(IIntegrator owner) : CADEmbedAttributesService(owner)
{
  protected override CIEmbedAttributesDriver CreateDriver()
  {
    return (CIEmbedAttributesDriver) new K3DEmbedAttributesDriver(this.Integrator);
  }
}
