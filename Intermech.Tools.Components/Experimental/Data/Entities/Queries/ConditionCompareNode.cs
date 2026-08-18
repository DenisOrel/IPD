// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.Queries.ConditionCompareNode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;

#nullable disable
namespace Experimental.Data.Entities.Queries;

public class ConditionCompareNode : ConditionAstNode
{
  public ConditionCompareNode(string name, ConditionCompareOperator @operator, object value)
  {
    this.Name = name != null ? name : throw new ArgumentNullException("propertyName");
    this.Operator = @operator;
    this.Value = value;
  }

  public string Name { get; private set; }

  public ConditionCompareOperator Operator { get; private set; }

  public object Value { get; private set; }
}
