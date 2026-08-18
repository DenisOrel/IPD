// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CAD.AIHeuristics
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.IO;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.CAD;

internal sealed class AIHeuristics : CADHeuristics
{
  private readonly ICADInterfaceService cadInterfaceService;

  public AIHeuristics(IIntegrator integrator, ICopyingSessionServices services)
    : base(integrator, services, CADCloneDataCapabilities.CanHandleOnlyCADFiles)
  {
    this.cadInterfaceService = ServiceUtils.GetService<ICADInterfaceService>((object) integrator, true);
  }

  protected override string DoRenameFile(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    DBObjectFileEntry fileRecord)
  {
    return fileRecord.Content.Tag == DBObjectFileContentTag.CADModelConfigurationFile ? this.RenameAIConfigurationFilePath(dbObjectVertex, fileRecord) : base.DoRenameFile(session, dbObjectVertex, fileRecord);
  }

  private string RenameAIConfigurationFilePath(
    DBObjectGraphVertex dbObjectVertex,
    DBObjectFileEntry fileRecord)
  {
    CADConfigurationTableRow configurationTableRow = fileRecord.Content.AsCADModelConfigurationFile().ConfigurationTableRow;
    DBObjectFileEntry file = dbObjectVertex.Files[0];
    string fileName = Path.GetFileName(fileRecord.OriginalName);
    return Path.Combine(Path.GetDirectoryName(file.NewName), Path.GetFileNameWithoutExtension(file.NewName), fileName);
  }

  protected override void DoPrepareDocumentParametersToWrite(
    CopyingSession session,
    DBObjectGraphVertex dbObjectVertex,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    new AIDocumentParametersPreparer(this.cadInterfaceService).PrepareDocumentParametersToWrite(session, dbObjectVertex, virtualContainerSet);
  }
}
