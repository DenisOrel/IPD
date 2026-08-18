// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.Queries.ConstantEvaluator
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Experimental.Data.Entities.Queries;

public class ConstantEvaluator
{
  public T Evaluate<T>(Expression<Func<T>> expression)
  {
    return expression != null ? (T) this.Evaluate(expression.Body) : throw new ArgumentNullException(nameof (expression));
  }

  public object Evaluate(Expression expression)
  {
    if (expression == null)
      throw new ArgumentNullException(nameof (expression));
    switch (expression.NodeType)
    {
      case ExpressionType.ArrayIndex:
        return this.EvaluateArrayIndexExpression((BinaryExpression) expression);
      case ExpressionType.Call:
        return this.EvaluateMethodCallExpression((MethodCallExpression) expression);
      case ExpressionType.Constant:
        return this.EvaluateConstantExpression((ConstantExpression) expression);
      case ExpressionType.MemberAccess:
        return this.EvaluateMemberAccessExpression((MemberExpression) expression);
      default:
        throw new NotSupportedException($"Невозможно вычислить значение выражения '{expression}' типа '{expression.NodeType}'.");
    }
  }

  private object EvaluateConstantExpression(ConstantExpression expression) => expression.Value;

  private object EvaluateMemberAccessExpression(MemberExpression expression)
  {
    MemberInfo member = expression.Member;
    if ((object) (member as FieldInfo) != null)
    {
      FieldInfo fieldInfo = (FieldInfo) member;
      return fieldInfo.GetValue(fieldInfo.IsStatic ? (object) null : this.Evaluate(expression.Expression));
    }
    PropertyInfo propertyInfo = (PropertyInfo) member;
    return propertyInfo.GetValue(propertyInfo.GetMethod.IsStatic ? (object) null : this.Evaluate(expression.Expression));
  }

  private object EvaluateArrayIndexExpression(BinaryExpression expression)
  {
    return ((Array) this.Evaluate(expression.Left)).GetValue((int) this.Evaluate(expression.Right));
  }

  private object EvaluateMethodCallExpression(MethodCallExpression expression)
  {
    object[] parameters = new object[expression.Arguments.Count];
    for (int index = 0; index < parameters.Length; ++index)
      parameters[index] = this.Evaluate(expression.Arguments[index]);
    object obj = expression.Method.IsStatic ? (object) null : this.Evaluate(expression.Object);
    return expression.Method.Invoke(obj, parameters);
  }
}
