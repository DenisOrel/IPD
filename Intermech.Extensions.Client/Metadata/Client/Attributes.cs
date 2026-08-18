// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.Client.Attributes
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Client;
using Intermech.Diagnostics;
using Intermech.PropertyEditors;
using System;
using System.Diagnostics;
using System.Drawing.Design;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata.Client;

public abstract class Attributes : Intermech.Metadata.Attributes
{
  [NotNull]
  public static IAttributePropertyDescriberService PropertyDescriberService
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return Services.AttributePropertyDescriber;
    }
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IAttributePropertyDescriber GetDescriber(
    [NotEmpty] int attrTypeID,
    bool throwExceptionIfNotFound = false)
  {
    IAttributePropertyDescriber result;
    if (!Attributes.TryGetDescriber(attrTypeID, out result))
      throw new InvalidOperationException($"{"IAttributePropertyDescriber"} for attribute type {attrTypeID} not found!");
    return result;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetDescriber([NotEmpty] int attrTypeID, out IAttributePropertyDescriber result)
  {
    result = Services.AttributePropertyDescriber.GetDescriber(attrTypeID);
    return result != null;
  }

  [ContractAnnotation("throwExceptionIfNotFound:true => CanBeNull; => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static UITypeEditor GetEditor([NotEmpty] int attrTypeID, bool throwExceptionIfNotFound = false)
  {
    UITypeEditor result;
    if (!Attributes.TryGetEditor(attrTypeID, out result))
      throw new InvalidOperationException($"{"UITypeEditor"} for attribute type {attrTypeID} not found!");
    return result;
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool TryGetEditor([NotEmpty] int attrTypeID, out UITypeEditor result)
  {
    IAttributePropertyDescriber result1;
    result = Attributes.TryGetDescriber(attrTypeID, out result1) ? result1 as UITypeEditor : (UITypeEditor) null;
    return result != null;
  }
}
