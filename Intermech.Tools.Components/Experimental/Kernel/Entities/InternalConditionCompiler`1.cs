// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.InternalConditionCompiler`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities.Queries;
using Intermech;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class InternalConditionCompiler<TEntity>
{
  private IDBObjectEntityTypeDescriptor entityTypeDescriptor;
  private ConditionAstParser<TEntity> parser;

  public InternalConditionCompiler(IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    this.entityTypeDescriptor = entityTypeDescriptor != null ? entityTypeDescriptor : throw new ArgumentNullException(nameof (entityTypeDescriptor));
    this.parser = new ConditionAstParser<TEntity>();
  }

  public ConditionStructure[] Compile(Expression<Func<TEntity, bool>> condition)
  {
    return condition != null ? this.CompileInternal(this.parser.Parse(condition)) : throw new ArgumentNullException(nameof (condition));
  }

  private ConditionStructure[] CompileInternal(ConditionAstNode astNode)
  {
    switch (astNode)
    {
      case ConditionGroupNode _:
        return this.CompileGroup((ConditionGroupNode) astNode);
      case ConditionCompareNode _:
        return this.CompileCompare((ConditionCompareNode) astNode);
      default:
        throw new NotSupportedException("Неподдерживаемый тип узла ast.");
    }
  }

  private ConditionStructure[] CompileGroup(ConditionGroupNode astNode)
  {
    LogicalOperators logicalOperator = this.AstOperatorToLogicalOperator(astNode.Operator);
    List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(astNode.Nodes.Count);
    int num = astNode.Nodes.Count - 1;
    for (int index = 0; index <= num; ++index)
    {
      ConditionStructure[] collection = this.CompileInternal(astNode.Nodes[index]);
      collection[collection.Length - 1].LogicalOperator = index < num ? logicalOperator : LogicalOperators.NONE;
      if (collection.Length != 1)
      {
        ++collection[0].GroupID;
        --collection[collection.Length - 1].GroupID;
      }
      conditionStructureList.AddRange((IEnumerable<ConditionStructure>) collection);
    }
    return conditionStructureList.ToArray();
  }

  private LogicalOperators AstOperatorToLogicalOperator(ConditionGroupOperator @operator)
  {
    if (@operator == ConditionGroupOperator.And)
      return LogicalOperators.AND;
    if (@operator == ConditionGroupOperator.Or)
      return LogicalOperators.OR;
    throw new NotSupportedEnumException((Enum) @operator);
  }

  private ConditionStructure[] CompileCompare(ConditionCompareNode astNode)
  {
    if (astNode.Operator == ConditionCompareOperator.Equal && this.IsNullValue(astNode.Value))
      return this.CreateConditionArray(new ConditionStructure(this.ToDBAttributeId(astNode.Name), RelationalOperators.Empty, (object) null, LogicalOperators.NONE, 0, true));
    return astNode.Operator == ConditionCompareOperator.NotEqual && this.IsNullValue(astNode.Value) ? this.CreateConditionArray(new ConditionStructure(this.ToDBAttributeId(astNode.Name), RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0, true)) : this.CreateConditionArray(new ConditionStructure(this.ToDBAttributeId(astNode.Name), this.AstOperatorToRelationalOperator(astNode.Operator), this.ToDBAttributeValue(astNode.Value), LogicalOperators.NONE, 0, true));
  }

  private ConditionStructure[] CreateConditionArray(ConditionStructure condition)
  {
    return new ConditionStructure[1]{ condition };
  }

  private int ToDBAttributeId(string propertyName)
  {
    return this.entityTypeDescriptor.DataPropertiesMappings.GetByPropertyName(propertyName, true).Id;
  }

  private bool IsNullValue(object propertyValue)
  {
    return propertyValue == null || Convert.IsDBNull(propertyValue);
  }

  private object ToDBAttributeValue(object propertyValue)
  {
    return propertyValue == null ? (object) DBNull.Value : propertyValue;
  }

  private RelationalOperators AstOperatorToRelationalOperator(ConditionCompareOperator @operator)
  {
    switch (@operator)
    {
      case ConditionCompareOperator.Equal:
        return RelationalOperators.Equal;
      case ConditionCompareOperator.NotEqual:
        return RelationalOperators.NotEqual;
      case ConditionCompareOperator.GreaterThan:
        return RelationalOperators.Greater;
      case ConditionCompareOperator.GreaterThanOrEqual:
        return RelationalOperators.GreaterOrEqual;
      case ConditionCompareOperator.LessThan:
        return RelationalOperators.Less;
      case ConditionCompareOperator.LessThanOrEqual:
        return RelationalOperators.LessOrEqual;
      default:
        throw new NotSupportedEnumException((Enum) @operator);
    }
  }
}
