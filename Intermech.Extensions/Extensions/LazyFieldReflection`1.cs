// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.LazyFieldReflection`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Extensions;

public sealed class LazyFieldReflection<TValue> : LazyReflectionValue<FieldInfo, TValue>
{
  internal LazyFieldReflection(
    [CanBeNull] object obj,
    [NotNull] Type objectType,
    [NotNull] string valueName,
    BindingFlags bindingFlags,
    bool cacheValue = false)
    : base(obj, objectType, valueName, bindingFlags, cacheValue)
  {
  }

  [NotNull]
  protected override FieldInfo GetMemberInfo()
  {
    FieldInfo field = this.ClassType.GetField(this.ValueName, this.BindingFlags);
    return !(field == (FieldInfo) null) ? field : throw new InvalidOperationException($"{((this.BindingFlags & BindingFlags.Static) != BindingFlags.Default ? "Static" : "Instance")} field {this.ValueName} with type {typeof (TValue).Name} not found in {this.ClassType.Name}!");
  }

  [CanBeNull]
  protected override TValue GetValue() => (TValue) this.MemberInfo.GetValue(this.Obj);
}
