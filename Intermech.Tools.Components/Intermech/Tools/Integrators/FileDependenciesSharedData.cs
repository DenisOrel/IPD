// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileDependenciesSharedData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

internal sealed class FileDependenciesSharedData
{
  public FileDependenciesSharedData()
  {
    this.ProcessedFiles = new PathDictionary<FileDependencyProcessingResult>();
  }

  public FileDependencyProcessingResult FindAlreadyProcessedFile(string path)
  {
    FileDependencyProcessingResult processingResult;
    return this.ProcessedFiles.TryGetValue(path, out processingResult) ? processingResult : (FileDependencyProcessingResult) null;
  }

  public void RegisterAlreadyProcessedFile(FileDependencyProcessingResult result)
  {
    this.ProcessedFiles.Add(result.Path, result);
  }

  private PathDictionary<FileDependencyProcessingResult> ProcessedFiles { get; set; }
}
