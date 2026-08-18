// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.Queries.ConditionGroupNode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using System;
using System.Collections.Generic;

#nullable disable
namespace Experimental.Data.Entities.Queries;

public class ConditionGroupNode : ConditionAstNode
{
  public ConditionGroupNode(ConditionGroupOperator @operator, IList<ConditionAstNode> nodes)
  {
    if (nodes == null)
      throw new ArgumentNullException(nameof (nodes));
    if (nodes.Count < 2)
      throw new ArgumentOutOfRangeException(nameof (nodes), "Количество выражений должно быть 2 или больше.");
    this.Operator = @operator;
    this.Nodes = (IList<ConditionAstNode>) new ReadOnlyListWrapper<ConditionAstNode>(nodes);
  }

  public ConditionGroupNode(ConditionGroupOperator @operator, params ConditionAstNode[] nodes)
    : this(@operator, (IList<ConditionAstNode>) nodes)
  {
  }

  public ConditionGroupOperator Operator { get; private set; }

  public IList<ConditionAstNode> Nodes { get; private set; }
}
