// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TypeExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Navigator.Views;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Extensions;

public static class TypeExtensions
{
  [NotNull]
  public static TNavigatorViewDescriptor GetNavigatorViewDescriptor<TNavigatorViewDescriptor>(
    [NotNull] this Type type)
    where TNavigatorViewDescriptor : NavigatorViewDescriptorBase
  {
    PropertyInfo property = type.GetProperty("Descriptor", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty, (Binder) null, typeof (TNavigatorViewDescriptor), Array.Empty<Type>(), (ParameterModifier[]) null);
    Intermech.Diagnostics.Check.NotNull<PropertyInfo>(property, "descriptorProperty", $"Type \"{type.Name}\" must have static property Descriptor of type TNavigatorViewDescriptor!");
    TNavigatorViewDescriptor navigatorViewDescriptor = (TNavigatorViewDescriptor) property.GetValue((object) type);
    Intermech.Diagnostics.Check.Result.NotNull<TNavigatorViewDescriptor>(navigatorViewDescriptor, $"Descriptor of {type.Name} type is null!");
    return navigatorViewDescriptor;
  }
}
