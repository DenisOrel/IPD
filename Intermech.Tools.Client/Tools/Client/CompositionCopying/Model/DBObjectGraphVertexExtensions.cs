// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectGraphVertexExtensions
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal static class DBObjectGraphVertexExtensions
{
  public static bool HasTrait<T>(this IDBObjectGraphTraitOwner vertex)
  {
    if (vertex == null)
      throw new ArgumentNullException(nameof (vertex));
    return vertex.Traits.TryGetByType(typeof (T), false) != null;
  }

  public static bool TryGetTrait<T>(this IDBObjectGraphTraitOwner vertex, out T trait) where T : DBObjectGraphTrait
  {
    T obj = vertex != null ? (T) vertex.Traits.TryGetByType(typeof (T), false) : throw new ArgumentNullException(nameof (vertex));
    if ((object) obj != null)
    {
      trait = obj;
      return true;
    }
    trait = default (T);
    return false;
  }

  public static T GetTrait<T>(this IDBObjectGraphTraitOwner vertex) where T : DBObjectGraphTrait
  {
    return (T) vertex.Traits.TryGetByType(typeof (T), true);
  }

  public static bool IsDocument(this DBObjectGraphVertex vertex)
  {
    return vertex.HasTrait<DocumentTrait>();
  }

  public static bool IsCADModelDrawing(this DBObjectGraphVertex vertex)
  {
    return vertex.HasTrait<CADModelDrawingTrait>();
  }

  public static bool IsArticle(this DBObjectGraphVertex vertex) => vertex.HasTrait<ArticleTrait>();

  public static bool IsArticleDocumentation(this DBObjectGraphEdge edge)
  {
    return edge.HasTrait<ArticleDocumentationTrait>();
  }
}
