// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CAD.NXHeuristics
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.CAD;

internal sealed class NXHeuristics : CADHeuristics
{
  private ICADInterfaceService cadInterfaceService;

  public NXHeuristics(IIntegrator integrator, ICopyingSessionServices services)
    : base(integrator, services, CADCloneDataCapabilities.CanHandleOnlyCADFiles | CADCloneDataCapabilities.IncludeUnmodifiedReferenceFiles)
  {
    this.cadInterfaceService = ServiceUtils.GetService<ICADInterfaceService>((object) integrator, true);
  }

  protected override void DoPrepareDocumentParametersToWrite(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    new AIDocumentParametersPreparer(this.cadInterfaceService).PrepareDocumentParametersToWrite(session, dbObjectVertex, virtualContainerSet);
  }
}
