// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.AppendOnlyGraph`2
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Experimental.Data.Entities;

internal class AppendOnlyGraph<T, TEdge> where TEdge : IGraphEdge<T>, IEquatable<TEdge>
{
  private static readonly IList<TEdge> emptyEdgeList = (IList<TEdge>) new ReadOnlyCollection<TEdge>((IList<TEdge>) new TEdge[0]);
  private Dictionary<T, List<TEdge>> sources;
  private Dictionary<T, List<TEdge>> targets;

  public AppendOnlyGraph()
  {
    this.sources = new Dictionary<T, List<TEdge>>();
    this.targets = new Dictionary<T, List<TEdge>>();
  }

  public void AddEdge(TEdge edge)
  {
    List<TEdge> edgeList1;
    if (!this.sources.TryGetValue(edge.Source, out edgeList1))
    {
      edgeList1 = new List<TEdge>();
      this.sources.Add(edge.Source, edgeList1);
    }
    if (!edgeList1.Contains(edge))
      edgeList1.Add(edge);
    List<TEdge> edgeList2;
    if (!this.targets.TryGetValue(edge.Target, out edgeList2))
    {
      edgeList2 = new List<TEdge>();
      this.targets.Add(edge.Target, edgeList2);
    }
    if (edgeList2.Contains(edge))
      return;
    edgeList2.Add(edge);
  }

  public ICollection<TEdge> GetInEdges(T node)
  {
    List<TEdge> edgeList;
    return this.targets.TryGetValue(node, out edgeList) ? (ICollection<TEdge>) edgeList : (ICollection<TEdge>) AppendOnlyGraph<T, TEdge>.emptyEdgeList;
  }

  public ICollection<TEdge> GetOutEdges(T node)
  {
    List<TEdge> edgeList;
    return this.sources.TryGetValue(node, out edgeList) ? (ICollection<TEdge>) edgeList : (ICollection<TEdge>) AppendOnlyGraph<T, TEdge>.emptyEdgeList;
  }
}
