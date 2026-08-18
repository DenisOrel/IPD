// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.If
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
public readonly struct If(
  RelationalOperators @operator,
  [CanBeNull] object value = null,
  [CanBeNull] object value2 = null,
  bool caseSensitive = true) : ICloneable, IEquatable<If>
{
  public readonly RelationalOperators Operator = @operator;
  [CanBeNull]
  public readonly object Value = value;
  [CanBeNull]
  public readonly object Value2 = value2;
  public readonly bool CaseSensitive = caseSensitive;
  public static readonly If None = new If(RelationalOperators.None);

  public void Deconstruct(out RelationalOperators @operator) => @operator = this.Operator;

  public void Deconstruct(out RelationalOperators @operator, [CanBeNull] out object value)
  {
    @operator = this.Operator;
    value = this.Value;
  }

  public void Deconstruct(out RelationalOperators @operator, [CanBeNull] out object value, [CanBeNull] out object value2)
  {
    @operator = this.Operator;
    value = this.Value;
    value2 = this.Value2;
  }

  public bool IsEmpty
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Operator == RelationalOperators.None || this.Operator == RelationalOperators.NOP;
    }
  }

  public override int GetHashCode()
  {
    return (this.Operator, this.Value, this.Value2, this.CaseSensitive).GetHashCode();
  }

  public bool Equals(If other)
  {
    if (this.Operator == other.Operator)
    {
      if (this.Value != other.Value)
      {
        object obj = this.Value;
        if ((obj != null ? (obj.Equals(other.Value) ? 1 : 0) : 0) == 0)
          goto label_6;
      }
      if (this.Value2 != other.Value2)
      {
        object obj = this.Value2;
        if ((obj != null ? (obj.Equals(other.Value2) ? 1 : 0) : 0) == 0)
          goto label_6;
      }
      return this.CaseSensitive == other.CaseSensitive;
    }
label_6:
    return false;
  }

  public override bool Equals([CanBeNull] object obj) => obj is If other && this.Equals(other);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator ==(If left, If right) => left.Equals(right);

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool operator !=(If left, If right) => !(left == right);

  public override string ToString()
  {
    if (this.Value == null)
      return $"{this.Operator}";
    return this.Value2 != null ? $"{this.Operator} {this.Value} and {this.Value2}" : $"{this.Operator} {this.Value}";
  }

  public If CaseInsensitive()
  {
    return this.CaseSensitive ? new If(this.Operator, this.Value, this.Value2, false) : this;
  }

  public object Clone()
  {
    return (object) new If(this.Operator, this.Value, this.Value2, this.CaseSensitive);
  }
}
