// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalFileDependenciesHandler
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Tools.DataExchange;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public class MechanicalFileDependenciesHandler(
  MechanicalDriver driver,
  CaptureChangesDriverContext driverContext,
  IFileVault fileVaultService) : FileDependenciesHandler(driverContext, (IDocumentBuilder) driver, fileVaultService, driver.Operations.DraftDocuments)
{
  protected override FileDependencyProcessingParameters GetDependencyProcessingParameters(
    FileDependencyProcessingData dependency)
  {
    return !dependency.IsNewFile && dependency.ObjectId < 0L ? FileDependencyProcessingParameters.Analyse : base.GetDependencyProcessingParameters(dependency);
  }
}
