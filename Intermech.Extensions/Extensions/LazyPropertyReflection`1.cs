// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.LazyPropertyReflection`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Extensions;

public sealed class LazyPropertyReflection<TValue> : LazyReflectionValue<PropertyInfo, TValue>
{
  internal LazyPropertyReflection(
    [CanBeNull] object obj,
    [NotNull] Type objectType,
    [NotNull] string propertyName,
    BindingFlags bindingFlags,
    bool cacheValue = false)
    : base(obj, objectType, propertyName, bindingFlags, cacheValue)
  {
  }

  [NotNull]
  protected override PropertyInfo GetMemberInfo()
  {
    PropertyInfo property = this.ClassType.GetProperty(this.ValueName, this.BindingFlags, (Binder) null, typeof (TValue), Array.Empty<Type>(), (ParameterModifier[]) null);
    return !(property == (PropertyInfo) null) ? property : throw new InvalidOperationException($"{((this.BindingFlags & BindingFlags.Static) != BindingFlags.Default ? "Static" : "Instance")} property {this.ValueName} with type {typeof (TValue).Name} not found in {this.ClassType.Name}!");
  }

  [CanBeNull]
  protected override TValue GetValue() => (TValue) this.MemberInfo.GetValue(this.Obj);
}
