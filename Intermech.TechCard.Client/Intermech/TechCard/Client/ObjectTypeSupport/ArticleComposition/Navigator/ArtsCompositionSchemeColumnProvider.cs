// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator.ArtsCompositionSchemeColumnProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.Navigator.Classes.Providers;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Navigator;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionSchemeColumnProvider : INavigatorSchemeColumnProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodePart"></param>
  /// <param name="columnCollection"></param>
  /// <returns></returns>
  public NodeColumnCollection GetDefaultColumns(
    INodePart nodePart,
    NodeColumnCollection columnCollection)
  {
    NodeColumnCollection columnCollection1 = ArtsCompositionColumnScheme.GetColumnCollection();
    if (columnCollection1 != null)
      columnCollection.AddRange((IEnumerable<NodeColumn>) columnCollection1);
    return columnCollection;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodepart"></param>
  /// <param name="columnCollection"></param>
  /// <param name="columnSetName"></param>
  /// <returns></returns>
  public NodeColumnCollection GetSupportedColumns(
    INodePart nodepart,
    NodeColumnCollection columnCollection,
    string columnSetName)
  {
    NodeColumnCollection columnCollection1 = ArtsCompositionColumnScheme.GetColumnCollection();
    if (columnCollection1 != null)
      columnCollection.InsertRange(0, (IEnumerable<NodeColumn>) columnCollection1);
    return columnCollection;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodePart"></param>
  /// <param name="setNameCollection"></param>
  /// <returns></returns>
  public List<string> GetSupportedColumnSetNames(INodePart nodePart, List<string> setNameCollection)
  {
    return setNameCollection;
  }
}
