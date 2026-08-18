// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.ExpressionList
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Evaluator;

public class ExpressionList : List<Expression>
{
  public ExpressionList()
  {
  }

  public ExpressionList([NotNull] IEnumerable<Expression> list)
    : base(list)
  {
  }

  public override int GetHashCode()
  {
    int count = this.Count;
    foreach (Expression expression in (List<Expression>) this)
    {
      count *= 17;
      if (expression != null)
        count += expression.GetHashCode();
    }
    return count;
  }

  public override string ToString()
  {
    string empty = string.Empty;
    foreach (Expression expression in (List<Expression>) this)
    {
      if (empty != string.Empty)
        empty += " && ";
      empty += expression.ToString();
    }
    return empty;
  }
}
