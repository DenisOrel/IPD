// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectTypeNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class ObjectTypeNotFoundException : KernelException, ISerializable
{
  protected readonly ObjectTypeNotFoundException.Params _Params;

  public int ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.ID;
  }

  public Guid Guid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.Guid;
  }

  [CanBeNull]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._Params.Name;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ObjectTypeNotFoundException()
    : this(new ObjectTypeNotFoundException.Params())
  {
  }

  public ObjectTypeNotFoundException([NotEmpty] int objectTypeID, [CanBeNull] string customMessage = null)
    : this(new ObjectTypeNotFoundException.Params(new int?(objectTypeID)), customMessage)
  {
  }

  public ObjectTypeNotFoundException([NotEmpty] Guid objectTypeGuid, [CanBeNull] string customMessage = null)
    : this(new ObjectTypeNotFoundException.Params(guid: new Guid?(objectTypeGuid)), customMessage)
  {
  }

  public ObjectTypeNotFoundException([NotNull] string objectTypeName, [CanBeNull] string customMessage)
    : this(new ObjectTypeNotFoundException.Params(name: objectTypeName), customMessage)
  {
  }

  public ObjectTypeNotFoundException(
    in ObjectTypeNotFoundException.Params objectTypeParams,
    [CanBeNull] string customMessage = null)
    : base(customMessage ?? ObjectTypeNotFoundException.CreateMessage(in objectTypeParams))
  {
    this._Params = objectTypeParams;
  }

  [NotNull]
  private static string CreateMessage(
    in ObjectTypeNotFoundException.Params objectTypeParams)
  {
    if (!string.IsNullOrWhiteSpace(objectTypeParams.Name))
      return $"Тип объекта \"{objectTypeParams.Name}\" не найден!";
    if (!Intermech.Check.ObjectTypeIdIsEmpty(objectTypeParams.ID))
      return $"Тип объекта с ID={objectTypeParams.ID} не найден!";
    return !(objectTypeParams.Guid != Guid.Empty) ? "Тип объекта не найден!" : $"Тип объекта с GUID={objectTypeParams.Guid} не найден!";
  }

  [SecuritySafeCritical]
  protected ObjectTypeNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._Params = info.GetValue<ObjectTypeNotFoundException.Params>("Params");
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Params", (object) this._Params, typeof (ObjectTypeNotFoundException.Params));
  }

  public readonly struct Params(int? id = null, Guid? guid = null, [CanBeNull] string name = null)
  {
    public readonly int ID = id ?? 0;
    public readonly Guid Guid = guid ?? Guid.Empty;
    [CanBeNull]
    public readonly string Name = name;
  }
}
