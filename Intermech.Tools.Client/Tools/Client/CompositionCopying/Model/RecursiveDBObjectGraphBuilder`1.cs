// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.RecursiveDBObjectGraphBuilder`1
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal abstract class RecursiveDBObjectGraphBuilder<TEdgeProperties> : IDBObjectGraphBuilder
{
  private CopyingSession session;
  private Dictionary<long, DBObjectGraphVertex> vertexCache;

  protected RecursiveDBObjectGraphBuilder(CopyingSession session)
  {
    this.session = session != null ? session : throw new ArgumentNullException(nameof (session));
  }

  public CopyingSession Session => this.session;

  public void Build()
  {
    this.DoValidateConfiguration();
    this.ClearSession();
    try
    {
      this.DoInitialize();
      this.Session.Graph.Initialize(this.DoBuildRootVertex());
      this.DoBuild();
    }
    catch
    {
      this.ClearSession();
      throw;
    }
    finally
    {
      this.DoCleanup();
    }
  }

  protected virtual void DoBuild()
  {
    this.vertexCache.Add(this.Session.Graph.RootVertext.ObjectId, this.Session.Graph.RootVertext);
    this.BuildChildrenInternal(this.Session.Graph.RootVertext);
  }

  private void ClearSession() => this.Session.Graph.Clear();

  private void BuildChildrenInternal(DBObjectGraphVertex parentVertext)
  {
    List<(DBObjectGraphVertex, TEdgeProperties)> tupleList = this.DoBuildChildrenVertices(parentVertext);
    List<DBObjectGraphVertex> objectGraphVertexList = new List<DBObjectGraphVertex>();
    foreach ((DBObjectGraphVertex vertex, TEdgeProperties childEdgeProperties) in tupleList)
    {
      DBObjectGraphVertex childVertex;
      if (!this.vertexCache.TryGetValue(vertex.ObjectId, out childVertex))
      {
        this.Session.Graph.AddVertex(vertex);
        this.vertexCache.Add(vertex.ObjectId, vertex);
        objectGraphVertexList.Add(vertex);
        childVertex = vertex;
      }
      this.Session.Graph.AddEdge(this.DoBuildChildEdge(parentVertext, childVertex, childEdgeProperties));
    }
    foreach (DBObjectGraphVertex parentVertext1 in objectGraphVertexList)
      this.BuildChildrenInternal(parentVertext1);
  }

  protected virtual void DoValidateConfiguration()
  {
  }

  protected virtual void DoInitialize()
  {
    this.vertexCache = new Dictionary<long, DBObjectGraphVertex>();
  }

  protected virtual void DoCleanup()
  {
    this.vertexCache = (Dictionary<long, DBObjectGraphVertex>) null;
  }

  protected abstract DBObjectGraphVertex DoBuildRootVertex();

  protected abstract List<(DBObjectGraphVertex, TEdgeProperties)> DoBuildChildrenVertices(
    DBObjectGraphVertex parentVertex);

  protected abstract DBObjectGraphEdge DoBuildChildEdge(
    DBObjectGraphVertex parentVertex,
    DBObjectGraphVertex childVertex,
    TEdgeProperties childEdgeProperties);
}
