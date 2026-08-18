// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSelectorEntry
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CopyingSelectorEntry
{
  private readonly bool isAllowing;
  private readonly string heuristicsId;
  private readonly string description;
  private readonly DBObjectGraphVertex startVertex;

  private CopyingSelectorEntry(
    bool isAllowing,
    string heuristicsId = "",
    string description = "",
    DBObjectGraphVertex startVertex = null)
  {
    int num = heuristicsId != string.Empty ? 1 : 0;
    this.isAllowing = isAllowing;
    this.heuristicsId = heuristicsId;
    this.description = description;
    this.startVertex = startVertex;
  }

  public static CopyingSelectorEntry CreateByUserChoise() => new CopyingSelectorEntry(true);

  public static CopyingSelectorEntry CreateByHeuristics(
    bool isAllowing,
    string heuristicsId,
    string description,
    DBObjectGraphVertex startVertex)
  {
    if (string.IsNullOrEmpty(heuristicsId))
      throw new ArgumentException("Не задан идентификатор эвристики.", nameof (heuristicsId));
    if (string.IsNullOrEmpty(description))
      throw new ArgumentException("Не задан описатель.", nameof (description));
    if (startVertex == null)
      throw new ArgumentNullException(nameof (startVertex));
    return new CopyingSelectorEntry(isAllowing, heuristicsId, description, startVertex);
  }

  public bool IsAllowing => this.isAllowing;

  public string HeuristicsId => this.heuristicsId;

  public string Description => this.description;

  public DBObjectGraphVertex StartVertex => this.startVertex;
}
