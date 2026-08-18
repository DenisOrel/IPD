// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MetadataUpdates.ObligatoryElementKey
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;

#nullable disable
namespace Intermech.Interfaces.MetadataUpdates;

public sealed class ObligatoryElementKey : IEquatable<ObligatoryElementKey>
{
  public ObligatoryElementKey(ObligatoryElementKind kind, object keyComponents)
  {
    if (keyComponents == null)
      throw new ArgumentNullException(nameof (keyComponents));
    this.Kind = kind;
    this.Components = keyComponents;
  }

  private ObligatoryElementKind Kind { get; set; }

  private object Components { get; set; }

  public bool Equals(ObligatoryElementKey other)
  {
    return other != null && this.Kind == other.Kind && object.Equals(this.Components, other.Components);
  }

  public override bool Equals(object obj)
  {
    return !(obj is ObligatoryElementKey other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => (int) this.Kind << 24 ^ this.Components.GetHashCode();

  public override string ToString() => $"{this.Kind}: {this.Components}";
}
