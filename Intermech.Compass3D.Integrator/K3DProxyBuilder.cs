// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DProxyBuilder
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DProxyBuilder : CADSystemProxyBuilder
{
  private IDrawing2DFeatureDetector drawing2DDetector;

  public IDrawing2DFeatureDetector Drawing2DDetector
  {
    [DebuggerStepThrough] get => this.drawing2DDetector;
    [DebuggerStepThrough] set => this.drawing2DDetector = value;
  }

  protected override IModelConfigurationNameMangler DoCreateConfigurationNameMangler()
  {
    return (IModelConfigurationNameMangler) new DynamicModelConfigurationNameMangler("Compass-3D Default Configuration");
  }

  protected override ModelConfigurationProxy DoCreateModelConfiguration(
    IModelConfigurationProvider configurationProvider,
    CADDocumentProxy document,
    CADSystemProxy appProxy,
    IModelConfigurationCreationContext creationContext)
  {
    return (ModelConfigurationProxy) new K3DModelConfiguration(configurationProvider, document, appProxy, creationContext);
  }

  protected override CADDocumentProxy DoCreateDocument(
    ICADDocumentProvider provider,
    CADSystemProxy appProxy)
  {
    K3DDocument document = new K3DDocument(provider, appProxy);
    if (this.Drawing2DDetector != null)
      document.AttachDrawing2DDetector(this.Drawing2DDetector);
    return (CADDocumentProxy) document;
  }
}
