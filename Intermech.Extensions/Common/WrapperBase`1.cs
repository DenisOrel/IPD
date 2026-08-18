// Decompiled with JetBrains decompiler
// Type: Intermech.Common.WrapperBase`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Common;

[Serializable]
public abstract class WrapperBase<TWrappedObject> : 
  IEquatable<WrapperBase<TWrappedObject>>,
  IEquatable<TWrappedObject>,
  ISerializable,
  IDeserializationCallback
  where TWrappedObject : class
{
  protected const string SerializeWrappedObjectValueName = "Object";
  [CanBeNull]
  private TWrappedObject _wrappedObject;
  [CanBeNull]
  private static ConstructorInfo _deserializationConstructor;
  [CanBeNull]
  private static bool? _supportISerializable;
  [CanBeNull]
  private static bool? _hasSerializableAttribute;

  [NotNull]
  public virtual TWrappedObject WrappedObject
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._wrappedObject;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] protected set => this._wrappedObject = value;
  }

  protected WrapperBase()
  {
  }

  protected WrapperBase([NotNull] TWrappedObject wrappedObject)
  {
    this.WrappedObject = wrappedObject is WrapperBase<TWrappedObject> wrapperBase ? wrapperBase.WrappedObject : wrappedObject;
  }

  protected WrapperBase([NotNull] SerializationInfo info, StreamingContext context)
  {
    this._wrappedObject = (TWrappedObject) WrapperBase<TWrappedObject>.DeserializationConstructor.Invoke(new object[2]
    {
      (object) info,
      (object) context
    });
  }

  [CanBeNull]
  private static ConstructorInfo DeserializationConstructor
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      ConstructorInfo deserializationConstructor = WrapperBase<TWrappedObject>._deserializationConstructor;
      if ((object) deserializationConstructor != null)
        return deserializationConstructor;
      Type type = typeof (TWrappedObject);
      Type[] types = new Type[2]
      {
        typeof (SerializationInfo),
        typeof (StreamingContext)
      };
      return WrapperBase<TWrappedObject>._deserializationConstructor = __nonvirtual (type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, types, (ParameterModifier[]) null));
    }
  }

  private static bool SupportISerializable
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return WrapperBase<TWrappedObject>._supportISerializable ?? (WrapperBase<TWrappedObject>._supportISerializable = new bool?(typeof (TWrappedObject).GetInterface("ISerializable") != (Type) null)).Value;
    }
  }

  public static bool HasSerializableAttribute
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return WrapperBase<TWrappedObject>._hasSerializableAttribute ?? (WrapperBase<TWrappedObject>._hasSerializableAttribute = new bool?(Attribute.GetCustomAttribute((MemberInfo) typeof (TWrappedObject), typeof (SerializableAttribute), false) != null)).Value;
    }
  }

  protected virtual void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    ((ISerializable) (object) this.WrappedObject).GetObjectData(info, context);
  }

  void ISerializable.GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    this.GetObjectData(info, context);
  }

  public virtual void OnDeserialization([CanBeNull] object sender)
  {
  }

  public override string ToString() => this.WrappedObject.ToString();

  public override int GetHashCode() => this.WrappedObject.GetHashCode();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj || (object) this.WrappedObject == obj)
      return true;
    switch (obj)
    {
      case TWrappedObject wrappedObject:
        return this.WrappedObject.Equals((object) wrappedObject);
      case WrapperBase<TWrappedObject> wrapperBase:
        return this.WrappedObject.Equals((object) wrapperBase.WrappedObject);
      default:
        return false;
    }
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] WrapperBase<TWrappedObject> other)
  {
    if (other == null)
      return false;
    return this == other || (object) this.WrappedObject == other || (object) this.WrappedObject == (object) other.WrappedObject || other.WrappedObject.Equals((object) this.WrappedObject);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] TWrappedObject other)
  {
    if ((object) other == null)
      return false;
    return this == (object) other || (object) this.WrappedObject == (object) other || other.Equals((object) this.WrappedObject);
  }
}
