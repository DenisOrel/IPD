// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectGraph
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using QuickGraph;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal class DBObjectGraph
{
  private BidirectionalGraph<DBObjectGraphVertex, DBObjectGraphEdge> internalGraph;
  private DBObjectGraphVertex rootVertex;

  public DBObjectGraph()
  {
    this.internalGraph = new BidirectionalGraph<DBObjectGraphVertex, DBObjectGraphEdge>();
  }

  public bool IsEmpty
  {
    [DebuggerStepThrough] get => this.rootVertex == null;
  }

  public DBObjectGraphVertex RootVertext
  {
    [DebuggerStepThrough] get => this.rootVertex;
  }

  public void Clear()
  {
    if (this.IsEmpty)
      return;
    this.internalGraph.Clear();
    this.rootVertex = (DBObjectGraphVertex) null;
  }

  public void Initialize(DBObjectGraphVertex rootVertex)
  {
    if (rootVertex == null)
      throw new ArgumentNullException(nameof (rootVertex));
    this.Clear();
    this.internalGraph.AddVertex(rootVertex);
    this.rootVertex = rootVertex;
  }

  public bool AddVertex(DBObjectGraphVertex vertex)
  {
    return vertex != null ? this.internalGraph.AddVertex(vertex) : throw new ArgumentNullException(nameof (vertex));
  }

  public bool AddEdge(DBObjectGraphEdge edge)
  {
    return edge != null ? this.internalGraph.AddEdge(edge) : throw new ArgumentNullException(nameof (edge));
  }

  public bool ContainsVertex(DBObjectGraphVertex vertex)
  {
    return vertex != null ? this.internalGraph.ContainsVertex(vertex) : throw new ArgumentNullException(nameof (vertex));
  }

  public bool ContainsEdge(DBObjectGraphVertex source, DBObjectGraphVertex target)
  {
    if (source == null)
      throw new ArgumentNullException(nameof (source));
    if (target == null)
      throw new ArgumentNullException(nameof (target));
    return this.internalGraph.ContainsEdge(source, target);
  }

  public DBObjectGraphVertex GetFirstVertexOrDefault(Predicate<DBObjectGraphVertex> predicate)
  {
    if (predicate == null)
      throw new ArgumentNullException(nameof (predicate));
    foreach (DBObjectGraphVertex vertex in this.internalGraph.Vertices)
    {
      if (predicate(vertex))
        return vertex;
    }
    return (DBObjectGraphVertex) null;
  }

  public ICollection<DBObjectGraphVertex> GetAllVertices()
  {
    return (ICollection<DBObjectGraphVertex>) new HashSet<DBObjectGraphVertex>(this.internalGraph.Vertices);
  }

  public ICollection<DBObjectGraphVertex> GetAllVertices(Predicate<DBObjectGraphVertex> predicate)
  {
    if (predicate == null)
      throw new ArgumentNullException(nameof (predicate));
    HashSet<DBObjectGraphVertex> allVertices = new HashSet<DBObjectGraphVertex>();
    foreach (DBObjectGraphVertex vertex in this.internalGraph.Vertices)
    {
      if (predicate(vertex))
        allVertices.Add(vertex);
    }
    return (ICollection<DBObjectGraphVertex>) allVertices;
  }

  public ICollection<DBObjectGraphVertex> GetVerticesByInEdgesRecursive(
    DBObjectGraphVertex vertex,
    Predicate<DBObjectGraphVertex> predicate = null)
  {
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    if (!this.internalGraph.ContainsVertex(vertex))
      throw new InvalidOperationException();
    HashSet<DBObjectGraphVertex> result = new HashSet<DBObjectGraphVertex>();
    this.GetVerticesByInEdgesCore(vertex, result, predicate);
    return (ICollection<DBObjectGraphVertex>) result;
  }

  private void GetVerticesByInEdgesCore(
    DBObjectGraphVertex vertex,
    HashSet<DBObjectGraphVertex> result,
    Predicate<DBObjectGraphVertex> predicate = null)
  {
    if (predicate != null)
    {
      if (predicate(vertex))
        result.Add(vertex);
    }
    else
      result.Add(vertex);
    IEnumerable<DBObjectGraphEdge> edges;
    if (!this.internalGraph.TryGetInEdges(vertex, out edges))
      return;
    foreach (DBObjectGraphEdge dbObjectGraphEdge in edges)
    {
      if (!result.Contains(dbObjectGraphEdge.Source))
        this.GetVerticesByInEdgesCore(dbObjectGraphEdge.Source, result, predicate);
    }
  }

  public ICollection<DBObjectGraphVertex> GetVerticesByOutEdges(DBObjectGraphVertex vertex)
  {
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    if (!this.internalGraph.ContainsVertex(vertex))
      throw new InvalidOperationException();
    HashSet<DBObjectGraphVertex> verticesByOutEdges = new HashSet<DBObjectGraphVertex>();
    IEnumerable<DBObjectGraphEdge> edges;
    if (this.internalGraph.TryGetOutEdges(vertex, out edges))
    {
      foreach (DBObjectGraphEdge dbObjectGraphEdge in edges)
      {
        if (!verticesByOutEdges.Contains(dbObjectGraphEdge.Target))
          verticesByOutEdges.Add(dbObjectGraphEdge.Target);
      }
    }
    return (ICollection<DBObjectGraphVertex>) verticesByOutEdges;
  }

  public ICollection<DBObjectGraphVertex> GetVerticesByInEdges(
    DBObjectGraphVertex vertex,
    Predicate<DBObjectGraphVertex> predicate = null)
  {
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    if (!this.internalGraph.ContainsVertex(vertex))
      throw new InvalidOperationException();
    HashSet<DBObjectGraphVertex> verticesByInEdges = new HashSet<DBObjectGraphVertex>();
    IEnumerable<DBObjectGraphEdge> edges;
    if (this.internalGraph.TryGetInEdges(vertex, out edges))
    {
      foreach (DBObjectGraphEdge dbObjectGraphEdge in edges)
      {
        if (!verticesByInEdges.Contains(dbObjectGraphEdge.Source))
        {
          if (predicate != null)
          {
            if (predicate(dbObjectGraphEdge.Source))
              verticesByInEdges.Add(dbObjectGraphEdge.Source);
          }
          else
            verticesByInEdges.Add(dbObjectGraphEdge.Source);
        }
      }
    }
    return (ICollection<DBObjectGraphVertex>) verticesByInEdges;
  }

  public ICollection<DBObjectGraphVertex> GetVerticesByOutEdgesRecursive(
    DBObjectGraphVertex vertex,
    Predicate<DBObjectGraphVertex> predicate = null)
  {
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    if (!this.internalGraph.ContainsVertex(vertex))
      throw new InvalidOperationException();
    HashSet<DBObjectGraphVertex> result = new HashSet<DBObjectGraphVertex>();
    this.GetVerticesByOutEdgesCore(vertex, result, predicate);
    return (ICollection<DBObjectGraphVertex>) result;
  }

  private void GetVerticesByOutEdgesCore(
    DBObjectGraphVertex vertex,
    HashSet<DBObjectGraphVertex> result,
    Predicate<DBObjectGraphVertex> predicate = null)
  {
    if (predicate != null)
    {
      if (predicate(vertex))
        result.Add(vertex);
    }
    else
      result.Add(vertex);
    IEnumerable<DBObjectGraphEdge> edges;
    if (!this.internalGraph.TryGetOutEdges(vertex, out edges))
      return;
    foreach (DBObjectGraphEdge dbObjectGraphEdge in edges)
    {
      if (!result.Contains(dbObjectGraphEdge.Target))
        this.GetVerticesByOutEdgesCore(dbObjectGraphEdge.Target, result, predicate);
    }
  }

  public ICollection<DBObjectGraphEdge> GetOutEdges(
    DBObjectGraphVertex vertex,
    Predicate<DBObjectGraphEdge> predicate = null)
  {
    HashSet<DBObjectGraphEdge> outEdges = new HashSet<DBObjectGraphEdge>();
    IEnumerable<DBObjectGraphEdge> edges;
    if (this.internalGraph.TryGetOutEdges(vertex, out edges))
    {
      foreach (DBObjectGraphEdge dbObjectGraphEdge in edges)
      {
        if (predicate != null)
        {
          if (predicate(dbObjectGraphEdge))
            outEdges.Add(dbObjectGraphEdge);
        }
        else
          outEdges.Add(dbObjectGraphEdge);
      }
    }
    return (ICollection<DBObjectGraphEdge>) outEdges;
  }

  public ICollection<DBObjectGraphEdge> GetInEdges(
    DBObjectGraphVertex vertex,
    Predicate<DBObjectGraphEdge> predicate = null)
  {
    HashSet<DBObjectGraphEdge> inEdges = new HashSet<DBObjectGraphEdge>();
    IEnumerable<DBObjectGraphEdge> edges;
    if (this.internalGraph.TryGetInEdges(vertex, out edges))
    {
      foreach (DBObjectGraphEdge dbObjectGraphEdge in edges)
      {
        if (predicate != null)
        {
          if (predicate(dbObjectGraphEdge))
            inEdges.Add(dbObjectGraphEdge);
        }
        else
          inEdges.Add(dbObjectGraphEdge);
      }
    }
    return (ICollection<DBObjectGraphEdge>) inEdges;
  }
}
