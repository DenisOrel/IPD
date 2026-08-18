
// Type: Intermech.Client.Core.Visualizers.VisualizerExParams
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Compositions;


namespace Intermech.Client.Core.Visualizers;

/// <summary>Параметры визуализатора</summary>
public class VisualizerExParams
{
  public long ObjectId { get; set; }

  public int ValueIndex { get; set; }

  public string FileName { get; set; }

  public int ObjectTypeId { get; set; }

  public RelationPair RelationPair { get; set; }
}
