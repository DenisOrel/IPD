// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DModelConfiguration
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DModelConfiguration(
  IModelConfigurationProvider configurationProvider,
  CADDocumentProxy document,
  CADSystemProxy appProxy,
  IModelConfigurationCreationContext creationContext) : ModelConfigurationProxy(configurationProvider, document, appProxy, creationContext)
{
  private IInMemoryConfigurationFeatureDetector inMemoryDetector;

  internal void AttachInMemoryDetector(
    IInMemoryConfigurationFeatureDetector inMemoryDetector)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      Trace.TraceInformation("K3DModelConfiguration.AttachInMemoryDetector({0})", (object) inMemoryDetector);
    this.inMemoryDetector = inMemoryDetector != null ? inMemoryDetector : throw new ArgumentNullException(nameof (inMemoryDetector));
    this.ResetPropertyCache();
  }

  protected override bool DetectIsInMemory()
  {
    return this.inMemoryDetector == null ? base.DetectIsInMemory() : this.inMemoryDetector.IsInMemory;
  }

  protected override string GetFullPath() => !this.IsInMemory ? base.GetFullPath() : string.Empty;
}
