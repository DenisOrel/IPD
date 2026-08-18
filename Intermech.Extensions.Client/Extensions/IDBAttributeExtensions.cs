// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IDBAttributeExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata.Client;
using Intermech.PropertyEditors;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class IDBAttributeExtensions
{
  [ContractAnnotation("throwExceptionIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAttributePropertyDescriber GetDescriber(
    [NotNull] this IDBAttribute attributeType,
    bool throwExceptionIfNotFound = false)
  {
    return Attributes.GetDescriber(attributeType.AttributeID, throwExceptionIfNotFound);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetDescriber(
    [NotNull] this IDBAttribute attributeType,
    out IAttributePropertyDescriber result)
  {
    return Attributes.TryGetDescriber(attributeType.AttributeID, out result);
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UITypeEditor GetEditor(
    [NotNull] this IDBAttribute attributeType,
    bool throwExceptionIfNotFound = false)
  {
    return Attributes.GetEditor(attributeType.AttributeID, throwExceptionIfNotFound);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetEditorFor([NotNull] this IDBAttribute attributeType, out UITypeEditor result)
  {
    return Attributes.TryGetEditor(attributeType.AttributeID, out result);
  }
}
