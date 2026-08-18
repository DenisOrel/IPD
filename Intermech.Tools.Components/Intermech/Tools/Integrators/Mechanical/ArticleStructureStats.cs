// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.ArticleStructureStats
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

public sealed class ArticleStructureStats
{
  private int createdRelations;
  private int changedRelations;
  private int removedRelations;

  public int CreatedRelations
  {
    get => this.createdRelations;
    internal set => this.createdRelations = value;
  }

  public int ChangedRelations
  {
    get => this.changedRelations;
    internal set => this.changedRelations = value;
  }

  public int DeletedRelations
  {
    get => this.removedRelations;
    internal set => this.removedRelations = value;
  }
}
