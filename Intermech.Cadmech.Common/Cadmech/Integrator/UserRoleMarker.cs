// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.UserRoleMarker
// Assembly: Intermech.Cadmech.Common, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3D1D989-0F34-4F5C-8A7E-7002449397DA
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Common.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Common.xml

using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

public sealed class UserRoleMarker : ICloneable
{
  private readonly Guid id;
  private readonly string name;

  public UserRoleMarker(Guid id, string name)
  {
    if (id == Guid.Empty)
      throw new ArgumentException();
    if (string.IsNullOrEmpty(name))
      throw new ArgumentException();
    this.id = id;
    this.name = name;
  }

  public UserRoleMarker Clone() => new UserRoleMarker(this.id, this.name);

  object ICloneable.Clone() => (object) this.Clone();

  public Guid Id => this.id;

  public string Name => this.name;

  public override int GetHashCode() => this.id.GetHashCode();

  public override bool Equals(object obj)
  {
    return !(obj is UserRoleMarker userRoleMarker) ? base.Equals(obj) : userRoleMarker.id == this.id;
  }

  public override string ToString() => this.name;
}
