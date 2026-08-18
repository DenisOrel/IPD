// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileDependencyProcessingResult
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators;

internal abstract class FileDependencyProcessingResult
{
  private FileDependencyProcessingResult(string path) => this.Path = path;

  public string Path { get; private set; }

  public sealed class IgnoredFile(string path) : FileDependencyProcessingResult(path)
  {
  }

  public sealed class SatelliteFile(string path) : FileDependencyProcessingResult(path)
  {
  }

  public sealed class Document : FileDependencyProcessingResult
  {
    public Document(string path, SectionEntity documentEntity)
      : base(path)
    {
      this.DocumentEntity = documentEntity;
    }

    public SectionEntity DocumentEntity { get; private set; }
  }
}
