// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.PossibleValues
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Project.Evaluator;

public class PossibleValues : List<PossibleValue>
{
  [CanBeNull]
  private static Dictionary<Type, PossibleValues> _all;

  private static void AddVal([NotNull] Type type, [NotNull, NotEmpty] string name, [CanBeNull] object value)
  {
    PossibleValues possibleValues;
    if (!PossibleValues.All.TryGetValue(type, out possibleValues))
    {
      possibleValues = new PossibleValues();
      PossibleValues.All.Add(type, possibleValues);
    }
    name = Localization.GetString(name);
    PossibleValue possibleValue = new PossibleValue(name, value);
    if (possibleValues.Contains(possibleValue))
      return;
    possibleValues.Add(possibleValue);
  }

  [NotNull]
  public static Dictionary<Type, PossibleValues> All
  {
    get
    {
      if (PossibleValues._all == null)
      {
        PossibleValues._all = new Dictionary<Type, PossibleValues>();
        PossibleValues.AddVal(typeof (bool), "ValTrue", (object) true);
        PossibleValues.AddVal(typeof (bool), "ValFalse", (object) false);
        PossibleValues.AddVal(typeof (DateTime), "ValNow", (object) "@DateTime.Now");
        PossibleValues.AddVal(typeof (DateTime), "ValMinDate", (object) "@DateTime.MinValue");
      }
      return PossibleValues._all;
    }
  }

  [CanBeNull]
  public PossibleValue FindByValue([CanBeNull] object value)
  {
    return this.FirstOrDefault<PossibleValue>((Func<PossibleValue, bool>) (val =>
    {
      if (val.Value != null && (val.Value.Equals(value) || value is string str2 && (val.Value.ToString().Equals(str2, StringComparison.CurrentCultureIgnoreCase) || val.Name.Equals(str2, StringComparison.CurrentCultureIgnoreCase))))
        return true;
      return val.Value == null && value == null;
    }));
  }
}
