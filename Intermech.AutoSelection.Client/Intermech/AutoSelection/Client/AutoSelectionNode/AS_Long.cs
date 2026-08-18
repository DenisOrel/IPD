// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AS_Long
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AS_Long
{
  private readonly long _value;

  public AS_Long(long value) => this._value = value;

  public AS_Long()
    : this(0L)
  {
  }

  public long Value => this._value;

  public override string ToString() => this.Value.ToString();

  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case AS_Long asLong:
        return this.Value.Equals(asLong.Value);
      case long num:
        return this.Value.Equals(num);
      default:
        return this == obj;
    }
  }

  public override int GetHashCode() => this._value.GetHashCode();
}
