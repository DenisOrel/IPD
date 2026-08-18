// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SystemAttributeExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Metadata;
using Intermech.PropertyEditors;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class SystemAttributeExtensions
{
  [ContractAnnotation("throwExceptionIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAttributePropertyDescriber GetDescriber(
    [NotNull] this SystemAttribute systemAttribute,
    bool throwExceptionIfNotFound = false)
  {
    return Intermech.Metadata.Client.Attributes.GetDescriber(systemAttribute.Descriptor.AttributeID, throwExceptionIfNotFound);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetDescriber(
    [NotNull] this SystemAttribute systemAttribute,
    out IAttributePropertyDescriber result)
  {
    return Intermech.Metadata.Client.Attributes.TryGetDescriber(systemAttribute.Descriptor.AttributeID, out result);
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UITypeEditor GetEditor(
    [NotNull] this SystemAttribute systemAttribute,
    bool throwExceptionIfNotFound = false)
  {
    return Intermech.Metadata.Client.Attributes.GetEditor(systemAttribute.Descriptor.AttributeID, throwExceptionIfNotFound);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetEditorFor([NotNull] this SystemAttribute systemAttribute, out UITypeEditor result)
  {
    return Intermech.Metadata.Client.Attributes.TryGetEditor(systemAttribute.Descriptor.AttributeID, out result);
  }
}
