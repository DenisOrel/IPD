// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.Queries.ConditionAstParser`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech;
using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#nullable disable
namespace Experimental.Data.Entities.Queries;

public class ConditionAstParser<TObject>
{
  private ConstantEvaluator constantEvaluator;
  private Type sourceType;

  public ConditionAstParser()
  {
    this.constantEvaluator = new ConstantEvaluator();
    this.sourceType = typeof (TObject);
  }

  public ConditionAstNode Parse(Expression<Func<TObject, bool>> expression)
  {
    return expression != null ? this.ParseInternal(expression.Body) : throw new ArgumentNullException(nameof (expression));
  }

  private ConditionAstNode ParseInternal(Expression expression)
  {
    switch (expression.NodeType)
    {
      case ExpressionType.AndAlso:
        return this.ParseAndExpression((BinaryExpression) expression);
      case ExpressionType.Not:
        return (ConditionAstNode) this.ParseNotExpression((UnaryExpression) expression);
      case ExpressionType.OrElse:
        return this.ParseOrExpression((BinaryExpression) expression);
      default:
        return expression is BinaryExpression ? (ConditionAstNode) this.ParseBinaryExpression((BinaryExpression) expression) : throw new NotSupportedException($"Не удалось разобрать выражение '{expression}'.");
    }
  }

  private ConditionGroupNode TryExpandBrackets(ConditionGroupNode groupNode)
  {
    if (!CollectionUtils.Exists<ConditionAstNode>((IEnumerable<ConditionAstNode>) groupNode.Nodes, (Predicate<ConditionAstNode>) (subNode => this.CanExpandBrackets(groupNode, subNode))))
      return groupNode;
    List<ConditionAstNode> nodes = new List<ConditionAstNode>();
    foreach (ConditionAstNode node in (IEnumerable<ConditionAstNode>) groupNode.Nodes)
    {
      if (this.CanExpandBrackets(groupNode, node))
        nodes.AddRange((IEnumerable<ConditionAstNode>) ((ConditionGroupNode) node).Nodes);
      else
        nodes.Add(node);
    }
    return this.TryExpandBrackets(new ConditionGroupNode(groupNode.Operator, (IList<ConditionAstNode>) nodes));
  }

  private bool CanExpandBrackets(ConditionGroupNode parentNode, ConditionAstNode subNode)
  {
    return subNode is ConditionGroupNode conditionGroupNode && conditionGroupNode.Operator == parentNode.Operator;
  }

  private ConditionAstNode ParseAndExpression(BinaryExpression expression)
  {
    return (ConditionAstNode) this.TryExpandBrackets(new ConditionGroupNode(ConditionGroupOperator.And, new ConditionAstNode[2]
    {
      this.ParseInternal(expression.Left),
      this.ParseInternal(expression.Right)
    }));
  }

  private ConditionAstNode ParseOrExpression(BinaryExpression expression)
  {
    return (ConditionAstNode) this.TryExpandBrackets(new ConditionGroupNode(ConditionGroupOperator.Or, new ConditionAstNode[2]
    {
      this.ParseInternal(expression.Left),
      this.ParseInternal(expression.Right)
    }));
  }

  private ConditionCompareNode ParseNotExpression(UnaryExpression expression)
  {
    ConditionCompareNode conditionCompareNode = (ConditionCompareNode) this.ParseInternal(expression.Operand);
    return new ConditionCompareNode(conditionCompareNode.Name, this.NegateCompareOpCode(conditionCompareNode.Operator), conditionCompareNode.Value);
  }

  private ConditionCompareNode ParseBinaryExpression(BinaryExpression expression)
  {
    Expression left = expression.Left;
    Expression right = expression.Right;
    string propertyName1 = this.TryGetPropertyName(left);
    if (propertyName1 != null)
    {
      object obj = this.constantEvaluator.Evaluate(right);
      return new ConditionCompareNode(propertyName1, this.NoteTypeToCompareOpCode(expression.NodeType), obj);
    }
    string propertyName2 = this.TryGetPropertyName(right);
    if (right == null)
      throw new NotSupportedException($"Не удалось найти ссылку на свойство доменного объекта в выражении '{expression}'.");
    object obj1 = this.constantEvaluator.Evaluate(left);
    return new ConditionCompareNode(propertyName2, this.ReverseCompareOpCode(this.NoteTypeToCompareOpCode(expression.NodeType)), obj1);
  }

  private string TryGetPropertyName(Expression expression)
  {
    return expression.NodeType == ExpressionType.MemberAccess ? this.TryGetPropertyName((MemberExpression) expression) : (string) null;
  }

  private string TryGetPropertyName(MemberExpression expression)
  {
    Expression expression1 = expression.Expression;
    switch (expression1.NodeType)
    {
      case ExpressionType.MemberAccess:
        string propertyName = this.TryGetPropertyName((MemberExpression) expression1);
        if (propertyName != null)
          propertyName = $"{propertyName}.{expression.Member.Name}";
        return propertyName;
      case ExpressionType.Parameter:
        return expression.Member.Name;
      default:
        return (string) null;
    }
  }

  private ConditionCompareOperator NoteTypeToCompareOpCode(ExpressionType nodeType)
  {
    switch (nodeType)
    {
      case ExpressionType.Equal:
        return ConditionCompareOperator.Equal;
      case ExpressionType.GreaterThan:
        return ConditionCompareOperator.GreaterThan;
      case ExpressionType.GreaterThanOrEqual:
        return ConditionCompareOperator.GreaterThanOrEqual;
      case ExpressionType.LessThan:
        return ConditionCompareOperator.LessThan;
      case ExpressionType.LessThanOrEqual:
        return ConditionCompareOperator.LessThanOrEqual;
      case ExpressionType.NotEqual:
        return ConditionCompareOperator.NotEqual;
      default:
        throw new NotSupportedEnumException((Enum) nodeType);
    }
  }

  private ConditionCompareOperator NegateCompareOpCode(ConditionCompareOperator opCode)
  {
    switch (opCode)
    {
      case ConditionCompareOperator.Equal:
        return ConditionCompareOperator.NotEqual;
      case ConditionCompareOperator.NotEqual:
        return ConditionCompareOperator.Equal;
      case ConditionCompareOperator.GreaterThan:
        return ConditionCompareOperator.LessThanOrEqual;
      case ConditionCompareOperator.GreaterThanOrEqual:
        return ConditionCompareOperator.LessThan;
      case ConditionCompareOperator.LessThan:
        return ConditionCompareOperator.GreaterThanOrEqual;
      case ConditionCompareOperator.LessThanOrEqual:
        return ConditionCompareOperator.GreaterThan;
      default:
        throw new NotSupportedEnumException((Enum) opCode);
    }
  }

  private ConditionCompareOperator ReverseCompareOpCode(ConditionCompareOperator opCode)
  {
    switch (opCode)
    {
      case ConditionCompareOperator.Equal:
        return ConditionCompareOperator.Equal;
      case ConditionCompareOperator.NotEqual:
        return ConditionCompareOperator.NotEqual;
      case ConditionCompareOperator.GreaterThan:
        return ConditionCompareOperator.LessThan;
      case ConditionCompareOperator.GreaterThanOrEqual:
        return ConditionCompareOperator.LessThanOrEqual;
      case ConditionCompareOperator.LessThan:
        return ConditionCompareOperator.GreaterThan;
      case ConditionCompareOperator.LessThanOrEqual:
        return ConditionCompareOperator.GreaterThanOrEqual;
      default:
        throw new NotSupportedEnumException((Enum) opCode);
    }
  }
}
