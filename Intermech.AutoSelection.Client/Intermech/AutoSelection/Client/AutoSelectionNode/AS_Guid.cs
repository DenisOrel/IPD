// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AS_Guid
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AS_Guid : IComparable
{
  private readonly Guid _value;

  public AS_Guid(Guid value) => this._value = value;

  public AS_Guid()
    : this(Guid.Empty)
  {
  }

  public Guid Value => this._value;

  public override string ToString() => this.Value.ToString();

  public override bool Equals(object obj)
  {
    switch (obj)
    {
      case AS_Guid asGuid:
        return this.Value.Equals(asGuid.Value);
      case Guid g:
        return this.Value.Equals(g);
      default:
        return this == obj;
    }
  }

  public override int GetHashCode() => this._value.GetHashCode();

  public int CompareTo(object obj)
  {
    Guid guid1 = Guid.Empty;
    switch (obj)
    {
      case Guid guid2:
        guid1 = guid2;
        break;
      case AS_Guid asGuid:
        guid1 = asGuid.Value;
        break;
    }
    return this.Value.CompareTo(guid1);
  }
}
