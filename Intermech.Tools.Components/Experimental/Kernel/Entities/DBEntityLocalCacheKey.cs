// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityLocalCacheKey
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal struct DBEntityLocalCacheKey(Type entityType, object entityKey) : 
  IEquatable<DBEntityLocalCacheKey>
{
  private Type entityType = entityType;
  private object entityKey = entityKey;

  public Type EntityType
  {
    [DebuggerStepThrough] get => this.entityType;
  }

  public object EntityKey
  {
    [DebuggerStepThrough] get => this.entityKey;
  }

  public bool Equals(DBEntityLocalCacheKey other)
  {
    return this.entityType == other.entityType && object.Equals(this.entityKey, other.entityKey);
  }

  public override bool Equals(object obj)
  {
    return !(obj is DBEntityLocalCacheKey other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode()
  {
    return this.entityType.GetHashCode() << 16 /*0x10*/ ^ this.entityKey.GetHashCode();
  }
}
