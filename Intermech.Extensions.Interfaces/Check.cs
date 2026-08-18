// Decompiled with JetBrains decompiler
// Type: Intermech.Check
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Kernel.Search;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech;

public abstract class Check : Intermech.Diagnostics.Check
{
  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AttributeIdIsEmpty(int value) => value == 0 || value == -10000;

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool AttributeIdIsEmpty(ObligatoryObjectAttributes value)
  {
    return value == ObligatoryObjectAttributes.None;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int AttributeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.AttributeIdIsEmpty(value) ? value : throw new AttributeIdIsEmptyException(valueName, message);
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ObligatoryObjectAttributes AttributeIdNotEmpty(
    ObligatoryObjectAttributes value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    return !Check.AttributeIdIsEmpty(value) ? value : throw new AttributeIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> AttributeIdNotEmpty(
    [CanBeNull] IEnumerable<int> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<int>>(value, valueName);
    foreach (int num in value)
    {
      if (Check.AttributeIdIsEmpty(num))
        throw AttributeIdIsEmptyException.ForCollection(valueName);
    }
    return value;
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<ObligatoryObjectAttributes> AttributeIdNotEmpty(
    [CanBeNull] IEnumerable<ObligatoryObjectAttributes> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<ObligatoryObjectAttributes>>(value, valueName);
    foreach (ObligatoryObjectAttributes objectAttributes in value)
    {
      if (Check.AttributeIdIsEmpty(objectAttributes))
        throw AttributeIdIsEmptyException.ForCollection(valueName);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int ArgumentAttributeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.AttributeIdIsEmpty(value) ? value : throw new ArgumentAttributeIdIsEmptyException(valueName, message);
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ObligatoryObjectAttributes ArgumentAttributeIdNotEmpty(
    ObligatoryObjectAttributes value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    return !Check.AttributeIdIsEmpty(value) ? value : throw new ArgumentAttributeIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> ArgumentAttributeIdNotEmpty(
    [CanBeNull] IEnumerable<int> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<int>>(value, valueName);
    foreach (int num in value)
    {
      if (Check.AttributeIdIsEmpty(num))
        throw ArgumentAttributeIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<ObligatoryObjectAttributes> ArgumentAttributeIdNotEmpty(
    [CanBeNull] IEnumerable<ObligatoryObjectAttributes> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<ObligatoryObjectAttributes>>(value, valueName);
    foreach (ObligatoryObjectAttributes objectAttributes in value)
    {
      if (Check.AttributeIdIsEmpty(objectAttributes))
        throw ArgumentAttributeIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [Pure]
  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ObjectTypeIdIsEmpty(int value) => value == -1 || value == -1 || value == -1;

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int ObjectTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.ObjectTypeIdIsEmpty(value) ? value : throw new ObjectTypeIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> ObjectTypeIdNotEmpty(
    [CanBeNull] IEnumerable<int> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<int>>(value, valueName);
    foreach (int num in value)
    {
      if (Check.ObjectTypeIdIsEmpty(num))
        throw ObjectTypeIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int ArgumentObjectTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.ObjectTypeIdIsEmpty(value) ? value : throw new ArgumentObjectTypeIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> ArgumentObjectTypeIdNotEmpty(
    [CanBeNull] IEnumerable<int> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<int>>(value, valueName);
    foreach (int num in value)
    {
      if (Check.ObjectTypeIdIsEmpty(num))
        throw ArgumentObjectTypeIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool RelationTypeIdIsEmpty(int value) => value == -1 || value == -1 || value == -1;

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int RelationTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.RelationTypeIdIsEmpty(value) ? value : throw new RelationTypeIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> RelationTypeIdNotEmpty(
    [CanBeNull] IEnumerable<int> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<int>>(value, valueName);
    foreach (int num in value)
    {
      if (Check.RelationTypeIdIsEmpty(num))
        throw RelationTypeIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static int ArgumentRelationTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.RelationTypeIdIsEmpty(value) ? value : throw new ArgumentRelationTypeIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<int> ArgumentRelationTypeIdNotEmpty(
    [CanBeNull] IEnumerable<int> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<int>>(value, valueName);
    foreach (int num in value)
    {
      if (Check.RelationTypeIdIsEmpty(num))
        throw ArgumentRelationTypeIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ObjectIdIsEmpty(long value) => value == 0L || value == -1L || value == 0L;

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ObjectIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.ObjectIdIsEmpty(value) ? value : throw new ObjectIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> ObjectIdNotEmpty(
    [CanBeNull] IEnumerable<long> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<long>>(value, valueName);
    foreach (long num in value)
    {
      if (Check.ObjectIdIsEmpty(num))
        throw ObjectIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ArgumentObjectIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.ObjectIdIsEmpty(value) ? value : throw new ArgumentObjectIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> ArgumentObjectIdNotEmpty(
    [CanBeNull] IEnumerable<long> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<long>>(value, valueName);
    foreach (long num in value)
    {
      if (Check.ObjectIdIsEmpty(num))
        throw ArgumentObjectIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool RelationIdIsEmpty(long value) => value == 0L || value == -1L || value == -1L;

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long RelationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.RelationIdIsEmpty(value) ? value : throw new RelationIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> RelationIdNotEmpty(
    [CanBeNull] IEnumerable<long> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<long>>(value, valueName);
    foreach (long num in value)
    {
      if (Check.RelationIdIsEmpty(num))
        throw RelationIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ArgumentRelationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.RelationIdIsEmpty(value) ? value : throw new ArgumentRelationIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> ArgumentRelationIdNotEmpty(
    [CanBeNull] IEnumerable<long> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<long>>(value, valueName);
    foreach (long num in value)
    {
      if (Check.RelationIdIsEmpty(num))
        throw ArgumentRelationIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IterationIdIsEmpty(long value) => value == 0L;

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long IterationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.IterationIdIsEmpty(value) ? value : throw new IterationIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> IterationIdNotEmpty(
    [CanBeNull] IEnumerable<long> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<long>>(value, valueName);
    foreach (long num in value)
    {
      if (Check.IterationIdIsEmpty(num))
        throw IterationIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static long ArgumentIterationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
  {
    return !Check.IterationIdIsEmpty(value) ? value : throw new ArgumentIterationIdIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<long> ArgumentIterationIdNotEmpty(
    [CanBeNull] IEnumerable<long> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<long>>(value, valueName);
    foreach (long num in value)
    {
      if (Check.IterationIdIsEmpty(num))
        throw ArgumentIterationIdIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool ObligatoryObjectAttributeIsEmpty(ObligatoryObjectAttributes value)
  {
    return value == ObligatoryObjectAttributes.Zero || value == ObligatoryObjectAttributes.None;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ObligatoryObjectAttributes ValueNotEmpty(
    ObligatoryObjectAttributes value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    return !Check.ObligatoryObjectAttributeIsEmpty(value) ? value : throw new AttributeIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<ObligatoryObjectAttributes> ValueNotEmpty(
    [CanBeNull] IEnumerable<ObligatoryObjectAttributes> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.NotNull<IEnumerable<ObligatoryObjectAttributes>>(value, valueName);
    foreach (ObligatoryObjectAttributes objectAttributes in value)
    {
      if (Check.ObligatoryObjectAttributeIsEmpty(objectAttributes))
        throw AttributeIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  [NotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ObligatoryObjectAttributes ArgumentValueNotEmpty(
    ObligatoryObjectAttributes value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    return !Check.ObligatoryObjectAttributeIsEmpty(value) ? value : throw new ArgumentAttributeIsEmptyException(valueName, message);
  }

  [ContractAnnotation("value:null => halt")]
  [Intermech.Diagnostics.NotNull]
  [ItemNotEmpty]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<ObligatoryObjectAttributes> ArgumentValueNotEmpty(
    [CanBeNull] IEnumerable<ObligatoryObjectAttributes> value,
    [CanBeNull, InvokerParameterName] string valueName = null,
    [CanBeNull] string message = null)
  {
    Intermech.Diagnostics.Check.ArgumentNotNull<IEnumerable<ObligatoryObjectAttributes>>(value, valueName);
    foreach (ObligatoryObjectAttributes objectAttributes in value)
    {
      if (Check.ObligatoryObjectAttributeIsEmpty(objectAttributes))
        throw ArgumentAttributeIsEmptyException.ForCollection(valueName, message);
    }
    return value;
  }

  public new abstract class Debug : Intermech.Diagnostics.Check.Debug
  {
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new static void ArgumentNotNull<T>([CanBeNull, NoEnumeration] T value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class
    {
      Intermech.Diagnostics.Check.ArgumentNotNull<T>(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new static void ArgumentNotNull<T>([CanBeNull, NoEnumeration] T? value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
    {
      Intermech.Diagnostics.Check.ArgumentNotNull<T>(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new static void NotNull<T>([CanBeNull, NoEnumeration] T value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : class
    {
      Intermech.Diagnostics.Check.NotNull<T>(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public new static void NotNull<T>([CanBeNull, NoEnumeration] T? value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null) where T : struct
    {
      Intermech.Diagnostics.Check.NotNull<T>(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(int value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(
      ObligatoryObjectAttributes value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      int num = (int) Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentAttributeIdNotEmpty(int value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentAttributeIdNotEmpty(
      ObligatoryObjectAttributes value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      int num = (int) Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentAttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentAttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectTypeIdNotEmpty(int value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.ObjectTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ObjectTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentObjectTypeIdNotEmpty(int value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.ArgumentObjectTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentObjectTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ArgumentObjectTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationTypeIdNotEmpty(int value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentRelationTypeIdNotEmpty(int value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentRelationTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectIdNotEmpty(long value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectIdNotEmpty([CanBeNull, NoEnumeration] IEnumerable<long> value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentObjectIdNotEmpty(long value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentObjectIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationIdNotEmpty(long value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.RelationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.RelationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentRelationIdNotEmpty(long value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.RelationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentRelationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.RelationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IterationIdNotEmpty(long value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.IterationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IterationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.IterationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentIterationIdNotEmpty(long value, [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    {
      Check.IterationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentIterationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.IterationIdNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValueNotEmpty(
      ObligatoryObjectAttributes value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      int num = (int) Check.ValueNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt; => value:NotNull")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValueNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ValueNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentValueNotEmpty(
      ObligatoryObjectAttributes value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      int num = (int) Check.ValueNotEmpty(value, valueName, message);
    }

    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [ContractAnnotation("value:null => halt")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ArgumentValueNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [Intermech.Diagnostics.NotNull, NotWhitespace, InvokerParameterName] string valueName,
      [CanBeNull] string message = null)
    {
      Check.ValueNotEmpty(value, valueName, message);
    }
  }

  public new abstract class Optional : Intermech.Diagnostics.Check.Optional
  {
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int AttributeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObligatoryObjectAttributes AttributeIdNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ObligatoryObjectAttributes> AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.AttributeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ArgumentAttributeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObligatoryObjectAttributes ArgumentAttributeIdNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> ArgumentAttributeIdNotEmpty(
      [NoEnumeration] IEnumerable<int> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ObligatoryObjectAttributes> ArgumentAttributeIdNotEmpty(
      [NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ArgumentAttributeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ObjectTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ObjectTypeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> ObjectTypeIdNotEmpty(
      [NoEnumeration] IEnumerable<int> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ObjectTypeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ArgumentObjectTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ArgumentObjectTypeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> ArgumentObjectTypeIdNotEmpty(
      [NoEnumeration] IEnumerable<int> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ArgumentObjectTypeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RelationTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> RelationTypeIdNotEmpty(
      [NoEnumeration] IEnumerable<int> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ArgumentRelationTypeIdNotEmpty(int value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> ArgumentRelationTypeIdNotEmpty(
      [NoEnumeration] IEnumerable<int> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationTypeIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ObjectIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> ObjectIdNotEmpty(
      [NoEnumeration] IEnumerable<long> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ArgumentObjectIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> ArgumentObjectIdNotEmpty(
      [NoEnumeration] IEnumerable<long> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ObjectIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long RelationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> RelationIdNotEmpty(
      [NoEnumeration] IEnumerable<long> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ArgumentRelationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> ArgumentRelationIdNotEmpty(
      [NoEnumeration] IEnumerable<long> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.RelationIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IterationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.IterationIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> IterationIdNotEmpty(
      [NoEnumeration] IEnumerable<long> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.IterationIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ArgumentIterationIdNotEmpty(long value, [CanBeNull, InvokerParameterName] string valueName = null, [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.IterationIdNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> ArgumentIterationIdNotEmpty(
      [NoEnumeration] IEnumerable<long> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.IterationIdNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObligatoryObjectAttributes ValueNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, valueName, message);
    }

    [Intermech.Diagnostics.NotNull]
    [ContractAnnotation("value:null => halt; => value:NotNull")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ObligatoryObjectAttributes> ValueNotEmpty(
      [NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, valueName, message);
    }

    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObligatoryObjectAttributes ArgumentValueNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, valueName, message);
    }

    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ObligatoryObjectAttributes> ArgumentValueNotEmpty(
      [NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull, InvokerParameterName] string valueName = null,
      [CanBeNull] string message = null)
    {
      return !Intermech.Diagnostics.Check.Optional.Enabled ? value : Check.ValueNotEmpty(value, valueName, message);
    }
  }

  public new abstract class Result : Intermech.Diagnostics.Check.Result
  {
    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int AttributeIdNotEmpty(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.AttributeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObligatoryObjectAttributes AttributeIdNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.AttributeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.AttributeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ObligatoryObjectAttributes> AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.AttributeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ObjectTypeIdNotEmpty(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.ObjectTypeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> ObjectTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.ObjectTypeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int RelationTypeIdNotEmpty(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.RelationTypeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<int> RelationTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.RelationTypeIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ObjectIdNotEmpty(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.ObjectIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> ObjectIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.ObjectIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long RelationIdNotEmpty(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.RelationIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> RelationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.RelationIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long IterationIdNotEmpty(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.IterationIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Intermech.Diagnostics.NotNull]
    [ItemNotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<long> IterationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.IterationIdNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Pure]
    [NotEmpty]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ObligatoryObjectAttributes ValueNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.ValueNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }

    [Intermech.Diagnostics.NotNull]
    [Pure]
    [ContractAnnotation("value:null => halt; => value:NotNull")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<ObligatoryObjectAttributes> ValueNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      return !Intermech.Diagnostics.Check.Result.Enabled ? value : Check.ValueNotEmpty(value, callerMemberName != null ? "Return value of " + callerMemberName : (string) null, message);
    }
  }

  public new abstract class SetValue : Intermech.Diagnostics.Check.SetValue
  {
    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentAttributeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      int num = (int) Check.ArgumentAttributeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentAttributeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AttributeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentAttributeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectTypeIdNotEmpty(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentObjectTypeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentObjectTypeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationTypeIdNotEmpty(int value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentRelationTypeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationTypeIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<int> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentRelationTypeIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectIdNotEmpty(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentObjectIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ObjectIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentObjectIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationIdNotEmpty(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentRelationIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RelationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentRelationIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IterationIdNotEmpty(long value, [CanBeNull] string message = null, [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentIterationIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void IterationIdNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<long> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentIterationIdNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValueNotEmpty(
      ObligatoryObjectAttributes value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      int num = (int) Check.ArgumentValueNotEmpty(value, callerMemberName, message);
    }

    [Pure]
    [ContractAnnotation("value:null => halt")]
    [Conditional("DEBUG")]
    [Conditional("FULL_CHECK")]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ValueNotEmpty(
      [CanBeNull, NoEnumeration] IEnumerable<ObligatoryObjectAttributes> value,
      [CanBeNull] string message = null,
      [CanBeNull] string callerMemberName = null)
    {
      Check.ArgumentValueNotEmpty(value, callerMemberName, message);
    }
  }
}
