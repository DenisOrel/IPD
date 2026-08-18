// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DCADInterfaceService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DCADInterfaceService(IIntegrator owner) : CADInterfaceService(owner, CompassConsts.IntegratorAppName, (ComObjectProvider) new ProgIdProvider(CompassConsts.ProgID, true))
{
  private Drawing2DDetectorService drawing2DDetectorService;
  private Drawing2DDocumentCodec drawing2DDocumentCodec;
  private Drawing2DHeadArticleCodec drawing2DHeadArticleCodec;
  private Drawing2DComponentArticleCodec drawing2DComponentArticleCodec;

  public Drawing2DDetectorService Drawing2DDetectorService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.drawing2DDetectorService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.drawing2DDetectorService = value;
      }
    }
  }

  private K3DSettingsService K3DSettingsService
  {
    [DebuggerStepThrough] get => (K3DSettingsService) this.SettingsService;
  }

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.Drawing2DDetectorService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "Drawing2DDetectorService");
    this.drawing2DDocumentCodec = new Drawing2DDocumentCodec();
    this.drawing2DHeadArticleCodec = new Drawing2DHeadArticleCodec();
    this.drawing2DComponentArticleCodec = new Drawing2DComponentArticleCodec();
  }

  protected override CADSystemProxyBuilder CreateProxyBuilder()
  {
    K3DProxyBuilder proxyBuilder = new K3DProxyBuilder();
    if (this.K3DSettingsService.GetSettings().EnableDrawings2DSupport)
      proxyBuilder.Drawing2DDetector = (IDrawing2DFeatureDetector) this.drawing2DDetectorService;
    proxyBuilder.AfterCreateModelConfiguration += new Action<CADSystemProxyBuilder, ModelConfigurationProxy>(this.OnAttachModelConfigurationFeatureDetectors);
    return (CADSystemProxyBuilder) proxyBuilder;
  }

  private void OnAttachModelConfigurationFeatureDetectors(
    CADSystemProxyBuilder sender,
    ModelConfigurationProxy configuration)
  {
    if (configuration.CreationContext is ExternalModelConfigurationContext)
    {
      Drawing2DComponentConfigurationSlowDetector inMemoryDetector = new Drawing2DComponentConfigurationSlowDetector(configuration);
      ((K3DModelConfiguration) configuration).AttachInMemoryDetector((IInMemoryConfigurationFeatureDetector) inMemoryDetector);
      ((K3DDocument) configuration.Document).AttachInMemoryDetector((IInMemoryConfigurationFeatureDetector) inMemoryDetector);
    }
    else
    {
      if (!(configuration.CreationContext is AssemblyComponentConfigurationContext))
        return;
      IInMemoryConfigurationFeatureDetector inMemoryDetector = !((K3DDocument) ((AssemblyComponentConfigurationContext) configuration.CreationContext).AssemblyDocument).IsDrawing2D() ? (IInMemoryConfigurationFeatureDetector) new K3DLibraryComponentConfigurationDetector(configuration) : (IInMemoryConfigurationFeatureDetector) new Drawing2DComponentConfigurationFastDetector();
      ((K3DModelConfiguration) configuration).AttachInMemoryDetector(inMemoryDetector);
      ((K3DDocument) configuration.Document).AttachInMemoryDetector(inMemoryDetector);
    }
  }

  protected override IAttributeCodec DoGetDocumentCodec(CADDocumentProxy document)
  {
    return this.drawing2DDetectorService.IsDrawing2D(document) ? this.GetDrawing2DDocumentCodec() : base.DoGetDocumentCodec(document);
  }

  internal IAttributeCodec GetDrawing2DDocumentCodec()
  {
    this.RequireReadyState();
    return (IAttributeCodec) this.drawing2DDocumentCodec;
  }

  protected override IAttributeCodec DoGetArticleCodec(CADDocumentProxy document)
  {
    if (this.drawing2DDetectorService.IsDrawing2D(document))
      return (IAttributeCodec) this.drawing2DHeadArticleCodec;
    return !string.IsNullOrEmpty(document.FullName) && PathUtils.IsSamePath(Path.GetFileName(document.FullName), "VirtualComponents.m3d") ? (IAttributeCodec) this.drawing2DComponentArticleCodec : base.DoGetArticleCodec(document);
  }

  protected override ArticleProcessingMethod DoGetArticleProcessingMethod(
    ArticleProcessingParams articleInfo)
  {
    if (articleInfo == null)
      throw new ArgumentNullException(nameof (articleInfo));
    return articleInfo.DocumentType.HasValue && this.drawing2DDetectorService.IsDrawing2D(articleInfo.DocumentType.Value) ? ArticleProcessingMethod.NormalObject : base.DoGetArticleProcessingMethod(articleInfo);
  }
}
