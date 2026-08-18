// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DCaptureChangesFactory
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Runtime;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DCaptureChangesFactory(IIntegrator owner) : CADCaptureChangesFactory(owner)
{
  private K3DCADInterfaceService cadInterfaceService;
  private Drawing2DDetectorService drawing2DDetectorService;

  public K3DCADInterfaceService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.cadInterfaceService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.cadInterfaceService = value;
      }
    }
  }

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

  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
    if (this.Drawing2DDetectorService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "Drawing2DDetectorService");
  }

  protected override CICaptureChangesDriver DoCreateDriver()
  {
    return (CICaptureChangesDriver) new K3DCaptureChangesDriver(this.Integrator, this.cadInterfaceService, this.drawing2DDetectorService);
  }
}
