// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.PropInfo
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project.Evaluator;

public class PropInfo
{
  [NotNull]
  [NotEmpty]
  public readonly string Name;
  [NotNull]
  [NotEmpty]
  public readonly string DisplayName;
  [CanBeNull]
  public Dictionary<string, string> OpMapping;

  public PropInfo([NotNull] string name, [NotNull] string displayName)
  {
    this.Name = name;
    this.DisplayName = displayName;
  }

  public PropInfo([NotNull] string name)
    : this(name, name)
  {
    this.DisplayName = Localization.GetString("TaskParam" + name.Replace(".", string.Empty));
  }

  public override string ToString() => this.DisplayName;

  [NotNull]
  public Type PropType
  {
    get
    {
      Type propType = SimpleFuncs.GetPropType(typeof (Task), this.Name);
      return !(propType == (Type) null) ? propType : throw new Exception($"Type for property \"{this.Name}\" not found");
    }
  }

  [CanBeNull]
  public PossibleValues PossibleValues
  {
    get
    {
      PossibleValues possibleValues;
      PossibleValues.All.TryGetValue(this.PropType, out possibleValues);
      if (this.PropType.IsEnum)
      {
        if (possibleValues == null)
          possibleValues = new PossibleValues();
        foreach (Enum enumValue in this.PropType.GetEnumValues())
        {
          string enumDescription = SimpleFuncs.GetEnumDescription(enumValue, true);
          if (!string.IsNullOrEmpty(enumDescription))
          {
            PossibleValue possibleValue = new PossibleValue(enumDescription, (object) enumValue);
            possibleValues.Add(possibleValue);
          }
        }
      }
      return possibleValues;
    }
  }

  public override int GetHashCode() => this.DisplayName.GetHashCode();
}
