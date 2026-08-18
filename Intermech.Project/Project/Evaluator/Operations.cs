// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.Operations
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project.Evaluator;

public class Operations : List<Operation>
{
  [CanBeNull]
  private static Operations _all;

  [NotNull]
  private static Operation AddOp([NotNull, NotEmpty] string name, [NotNull, NotEmpty] string symbol)
  {
    Operation operation = new Operation(Intermech.Project.Localization.GetString(name), symbol);
    Operations.All.Add(operation);
    return operation;
  }

  [NotNull]
  public static Operations All
  {
    get
    {
      if (Operations._all == null)
      {
        Operations._all = new Operations();
        Operation operation1 = Operations.AddOp("OpE", "==");
        operation1.TypeMapping = new Dictionary<Type, string>();
        operation1.TypeMapping.Add(typeof (AssignmentCollection), (string) null);
        Operation operation2 = Operations.AddOp("OpNE", "!=");
        operation2.TypeMapping = new Dictionary<Type, string>();
        operation2.TypeMapping.Add(typeof (AssignmentCollection), (string) null);
        Operation operation3 = Operations.AddOp("OpG", ">");
        operation3.TypeMapping = new Dictionary<Type, string>();
        operation3.TypeMapping.Add(typeof (bool), (string) null);
        operation3.TypeMapping.Add(typeof (AssignmentCollection), (string) null);
        Operation operation4 = Operations.AddOp("OpGE", ">=");
        operation4.TypeMapping = new Dictionary<Type, string>();
        operation4.TypeMapping.Add(typeof (bool), (string) null);
        operation4.TypeMapping.Add(typeof (AssignmentCollection), (string) null);
        Operation operation5 = Operations.AddOp("OpL", "<");
        operation5.TypeMapping = new Dictionary<Type, string>();
        operation5.TypeMapping.Add(typeof (bool), (string) null);
        operation5.TypeMapping.Add(typeof (AssignmentCollection), (string) null);
        Operation operation6 = Operations.AddOp("OpLE", "<=");
        operation6.TypeMapping = new Dictionary<Type, string>();
        operation6.TypeMapping.Add(typeof (bool), (string) null);
        operation6.TypeMapping.Add(typeof (AssignmentCollection), (string) null);
        Operation operation7 = Operations.AddOp("OpIn", "in");
        operation7.TypeMapping = new Dictionary<Type, string>();
        operation7.TypeMapping.Add(typeof (string), ".Contains(%v%)");
        operation7.TypeMapping.Add(typeof (AssignmentCollection), ".ContainsID(%v%)");
        operation7.TypeMapping.Add(typeof (object), (string) null);
        Operation operation8 = Operations.AddOp("OpOut", "out");
        operation8.TypeMapping = new Dictionary<Type, string>();
        operation8.TypeMapping.Add(typeof (string), "!.Contains(%v%)");
        operation8.TypeMapping.Add(typeof (AssignmentCollection), "!.ContainsID(%v%)");
        operation8.TypeMapping.Add(typeof (object), (string) null);
      }
      return Operations._all;
    }
  }

  [NotNull]
  public List<Operation> Filter([NotNull] PropInfo pi)
  {
    return this.Where<Operation>((Func<Operation, bool>) (operation => operation.GetMapping(pi) != null)).ToList<Operation>(this.Count);
  }
}
