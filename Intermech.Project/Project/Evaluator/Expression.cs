// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Evaluator.Expression
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

#nullable disable
namespace Intermech.Project.Evaluator;

public class Expression
{
  [CanBeNull]
  public readonly PropInfo Property;
  [CanBeNull]
  public readonly Operation Operation;
  [CanBeNull]
  private object _value = (object) string.Empty;
  public GroupOperation GroupOperation;
  [NotNull]
  public static Regex InputFormatRegex = new Regex("^\"(.*?)\"\\?$", RegexOptions.Compiled);

  [CanBeNull]
  public object Value
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._value;
    set
    {
      if (this._value == value)
        return;
      if (this.Property != null)
      {
        PossibleValues possibleValues = this.Property.PossibleValues;
        if (possibleValues != null)
        {
          object byValue = (object) possibleValues.FindByValue(value);
          if (byValue != null)
            value = byValue;
        }
      }
      this._value = value;
    }
  }

  public Expression([CanBeNull] string property, [CanBeNull] string operation, [CanBeNull] object value)
  {
    PropInfo propInfo1 = PropInfos.All.Find((Predicate<PropInfo>) (p => p.Name == property));
    Operation operation1 = Operations.All.Find((Predicate<Operation>) (o => o.Symbol == operation));
    this.Property = propInfo1;
    this.Operation = operation1;
    if (value is string && this.Property?.PossibleValues?.FindByValue(value) == null)
    {
      PropInfo propInfo2 = PropInfos.All.Find((Predicate<PropInfo>) (p => p.Name == value.ToString() || p.DisplayName == value.ToString()));
      if (propInfo2 != null)
        value = (object) propInfo2;
    }
    this.Value = value;
  }

  public Expression([CanBeNull] PropInfo property, [CanBeNull] Operation operation, [CanBeNull] object value)
  {
    this.Property = property;
    this.Operation = operation;
    this.Value = value;
  }

  public Expression([CanBeNull] Expression proto)
  {
    if (proto == null)
      return;
    this.GroupOperation = proto.GroupOperation;
    this.Property = proto.Property;
    this.Operation = proto.Operation;
    this.Value = proto.Value;
  }

  public override int GetHashCode()
  {
    return (this.Value, this.GroupOperation, this.Property, this.Operation).GetHashCode();
  }

  public override string ToString()
  {
    return $"{this.Property?.ToString() ?? "\"\""} {this.Operation?.ToString() ?? string.Empty} {this.Value?.ToString() ?? "\"\""}";
  }

  /// <summary>
  /// Требуется запрашивать ввод значения пользователя, или нет
  /// Значение должно быть уточнено, если в поле "значение" находится строка вида: "Текст запроса:"?
  /// </summary>
  public bool RequiresInput
  {
    get
    {
      return this.Value != null && this.Value.ToString() != string.Empty && Expression.InputFormatRegex.IsMatch(this.Value.ToString());
    }
  }
}
