// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgArticleData
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class DwgArticleData
{
  private readonly List<LocalId<int>> possibleObjectTypes;
  private readonly ValueBag fileProperties;
  private readonly List<ArticleStructureOccurence> structure;

  public DwgArticleData()
  {
    this.possibleObjectTypes = new List<LocalId<int>>();
    this.fileProperties = new ValueBag();
    this.structure = new List<ArticleStructureOccurence>();
  }

  public List<LocalId<int>> PossibleObjectTypes => this.possibleObjectTypes;

  public ValueBag FileProperties => this.fileProperties;

  public List<ArticleStructureOccurence> Structure => this.structure;
}
