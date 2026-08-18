// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Queries.TechDescriptorsQuery
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Queries;

/// <summary>
/// 
/// </summary>
/// <param name="descriptors"></param>
/// <param name="sortedQuery"></param>
internal class TechDescriptorsQuery(DescriptorCollection descriptors, bool sortedQuery) : 
  DescriptorsQuery(descriptors, sortedQuery)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="bookmark"></param>
  /// <param name="count"></param>
  public override void Execute(object bookmark, int count)
  {
    bool sortedQuery = this._sortedQuery;
    try
    {
      this._sortedQuery = this._sortedQuery && this._columns.FirstOrDefault<NodeColumn>((Func<NodeColumn, bool>) (item => item.SortOrder != 0)) != null;
      base.Execute(bookmark, count);
    }
    finally
    {
      this._sortedQuery = sortedQuery;
    }
  }
}
