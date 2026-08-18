// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.NamedObjectWithID`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

[DebuggerDisplay("ID={ID} Name={Name}")]
public abstract class NamedObjectWithID<TId> : 
  ObjectWithID<TId>,
  IObjectWithID<TId>,
  INamedObject,
  IComparable<INamedObject>,
  IComparable<NamedObjectWithID<TId>>
  where TId : struct
{
  public const StringComparison DefaultStringComparison = StringComparison.CurrentCulture;
  [CanBeNull]
  private string _name;
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> DefaultComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByName));
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> CurrentCultureComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCulture));
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> CurrentCultureIgnoreCaseComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCultureIgnoreCase));
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> InvariantCultureComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByNameInvariantCulture));
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> InvariantCultureIgnoreCaseComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByNameInvariantCultureIgnoreCase));
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> OrdinalComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByNameOrdinal));
  [NotNull]
  public static IComparer<NamedObjectWithID<TId>> OrdinalIgnoreCaseComparer = (IComparer<NamedObjectWithID<TId>>) new Intermech.Common.ObjectComparer<NamedObjectWithID<TId>>(new Func<NamedObjectWithID<TId>, NamedObjectWithID<TId>, int>(NamedObjectWithID<TId>.CompareByNameOrdinalIgnoreCase));
  [NotNull]
  public static IComparer<INamedObject> IDefaultComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByName));
  [NotNull]
  public static IComparer<INamedObject> ICurrentCultureComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCulture));
  [NotNull]
  public static IComparer<INamedObject> ICurrentCultureIgnoreCaseComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCultureIgnoreCase));
  [NotNull]
  public static IComparer<INamedObject> IInvariantCultureComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByNameInvariantCulture));
  [NotNull]
  public static IComparer<INamedObject> IInvariantCultureIgnoreCaseComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByNameInvariantCultureIgnoreCase));
  [NotNull]
  public static IComparer<INamedObject> IOrdinalComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByNameOrdinal));
  [NotNull]
  public static IComparer<INamedObject> IOrdinalIgnoreCaseComparer = (IComparer<INamedObject>) new Intermech.Common.ObjectComparer<INamedObject>(new Func<INamedObject, INamedObject, int>(NamedObjectWithID<TId>.CompareByNameOrdinalIgnoreCase));

  [NotNull]
  public string Name
  {
    get => this._name ?? string.Empty;
    protected set => this._name = value;
  }

  protected NamedObjectWithID()
  {
  }

  protected NamedObjectWithID([NotNull] string name) => this._name = name ?? string.Empty;

  protected NamedObjectWithID(TId id, [CanBeNull] string name = null)
    : base(id)
  {
    this._name = name ?? string.Empty;
  }

  public override string ToString() => this.Name;

  protected virtual StringComparison GetDefaultStringComparison()
  {
    return StringComparison.CurrentCulture;
  }

  public virtual int CompareTo([CanBeNull] NamedObjectWithID<TId> other)
  {
    if (other == null)
      return 1;
    return this == other ? 0 : NamedObjectWithID<TId>.CompareByName(this, other, this.GetDefaultStringComparison());
  }

  [NotNull]
  public static Comparison<NamedObjectWithID<TId>> GetComparison(StringComparison stringComparison = StringComparison.CurrentCulture)
  {
    switch (stringComparison)
    {
      case StringComparison.CurrentCulture:
        return new Comparison<NamedObjectWithID<TId>>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCulture);
      case StringComparison.CurrentCultureIgnoreCase:
        return new Comparison<NamedObjectWithID<TId>>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCultureIgnoreCase);
      case StringComparison.InvariantCulture:
        return new Comparison<NamedObjectWithID<TId>>(NamedObjectWithID<TId>.CompareByNameInvariantCulture);
      case StringComparison.InvariantCultureIgnoreCase:
        return new Comparison<NamedObjectWithID<TId>>(NamedObjectWithID<TId>.CompareByNameInvariantCultureIgnoreCase);
      case StringComparison.Ordinal:
        return new Comparison<NamedObjectWithID<TId>>(NamedObjectWithID<TId>.CompareByNameOrdinal);
      case StringComparison.OrdinalIgnoreCase:
        return new Comparison<NamedObjectWithID<TId>>(NamedObjectWithID<TId>.CompareByNameOrdinalIgnoreCase);
      default:
        throw new InvalidEnumArgumentException(nameof (stringComparison), (int) stringComparison, typeof (StringComparison));
    }
  }

  public static int CompareByNameCurrentCurrentCulture(
    [CanBeNull] NamedObjectWithID<TId> x,
    [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.CurrentCulture);
  }

  public static int CompareByNameCurrentCurrentCultureIgnoreCase(
    [CanBeNull] NamedObjectWithID<TId> x,
    [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.CurrentCultureIgnoreCase);
  }

  public static int CompareByNameInvariantCulture(
    [CanBeNull] NamedObjectWithID<TId> x,
    [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.InvariantCulture);
  }

  public static int CompareByNameInvariantCultureIgnoreCase(
    [CanBeNull] NamedObjectWithID<TId> x,
    [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.InvariantCultureIgnoreCase);
  }

  public static int CompareByNameOrdinal([CanBeNull] NamedObjectWithID<TId> x, [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.Ordinal);
  }

  public static int CompareByNameOrdinalIgnoreCase(
    [CanBeNull] NamedObjectWithID<TId> x,
    [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.OrdinalIgnoreCase);
  }

  public static int CompareByName([CanBeNull] NamedObjectWithID<TId> x, [CanBeNull] NamedObjectWithID<TId> y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.CurrentCulture);
  }

  public static int CompareByName(
    [CanBeNull] NamedObjectWithID<TId> x,
    [CanBeNull] NamedObjectWithID<TId> y,
    StringComparison comparisonType)
  {
    if (x == y)
      return 0;
    if (y == null)
      return 1;
    return x == null ? -1 : string.Compare(x.Name, y.Name, comparisonType);
  }

  int IComparable<INamedObject>.CompareTo([CanBeNull] INamedObject other)
  {
    return NamedObjectWithID<TId>.CompareByName((INamedObject) this, other, this.GetDefaultStringComparison());
  }

  [NotNull]
  public static Comparison<INamedObject> IGetComparison(StringComparison stringComparison = StringComparison.CurrentCulture)
  {
    switch (stringComparison)
    {
      case StringComparison.CurrentCulture:
        return new Comparison<INamedObject>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCulture);
      case StringComparison.CurrentCultureIgnoreCase:
        return new Comparison<INamedObject>(NamedObjectWithID<TId>.CompareByNameCurrentCurrentCultureIgnoreCase);
      case StringComparison.InvariantCulture:
        return new Comparison<INamedObject>(NamedObjectWithID<TId>.CompareByNameInvariantCulture);
      case StringComparison.InvariantCultureIgnoreCase:
        return new Comparison<INamedObject>(NamedObjectWithID<TId>.CompareByNameInvariantCultureIgnoreCase);
      case StringComparison.Ordinal:
        return new Comparison<INamedObject>(NamedObjectWithID<TId>.CompareByNameOrdinal);
      case StringComparison.OrdinalIgnoreCase:
        return new Comparison<INamedObject>(NamedObjectWithID<TId>.CompareByNameOrdinalIgnoreCase);
      default:
        throw new InvalidEnumArgumentException(nameof (stringComparison), (int) stringComparison, typeof (StringComparison));
    }
  }

  public static int CompareByNameCurrentCurrentCulture([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.CurrentCulture);
  }

  public static int CompareByNameCurrentCurrentCultureIgnoreCase([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.CurrentCultureIgnoreCase);
  }

  public static int CompareByNameInvariantCulture([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.InvariantCulture);
  }

  public static int CompareByNameInvariantCultureIgnoreCase([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.InvariantCultureIgnoreCase);
  }

  public static int CompareByNameOrdinal([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.Ordinal);
  }

  public static int CompareByNameOrdinalIgnoreCase([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.OrdinalIgnoreCase);
  }

  public static int CompareByName([CanBeNull] INamedObject x, [CanBeNull] INamedObject y)
  {
    return NamedObjectWithID<TId>.CompareByName(x, y, StringComparison.CurrentCulture);
  }

  public static int CompareByName([CanBeNull] INamedObject x, [CanBeNull] INamedObject y, StringComparison comparisonType)
  {
    if (x == y)
      return 0;
    if (y == null)
      return 1;
    return x == null ? -1 : string.Compare(x.Name, y.Name, comparisonType);
  }
}
