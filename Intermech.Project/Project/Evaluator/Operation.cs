// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.Operation
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Evaluator;

public class Operation
{
  [NotNull]
  [NotEmpty]
  public readonly string Name;
  [NotNull]
  [NotEmpty]
  public readonly string Symbol;
  [CanBeNull]
  public Dictionary<Type, string> TypeMapping;

  public Operation([NotNull, NotEmpty] string name, [NotNull, NotEmpty] string symbol)
  {
    this.Name = name;
    this.Symbol = symbol;
  }

  public override string ToString() => this.Name;

  public override int GetHashCode() => this.Name.GetHashCode();

  [CanBeNull]
  public string GetMapping([NotNull] PropInfo pi)
  {
    string key = this.Symbol;
    string str = (string) null;
    if (pi.OpMapping != null && pi.OpMapping.TryGetValue(key, out str))
      key = str;
    Type propType = pi.PropType;
    if (str == null && this.TypeMapping != null)
    {
      if (this.TypeMapping.TryGetValue(propType, out str))
        key = str;
      else if (this.TypeMapping.TryGetValue(typeof (object), out str))
        key = str;
    }
    return key;
  }
}
