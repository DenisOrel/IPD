// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.RelationTypeNotFoundException
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
public class RelationTypeNotFoundException : KernelException, ISerializable
{
  private readonly RelationTypeNotFoundException.Params _params;

  public int ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._params.ID;
  }

  public Guid Guid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._params.Guid;
  }

  [CanBeNull]
  public string Name
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._params.Name;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public RelationTypeNotFoundException()
    : this(new RelationTypeNotFoundException.Params())
  {
  }

  public RelationTypeNotFoundException([NotEmpty] int relationTypeID, [CanBeNull] string customMessage = null)
    : this(new RelationTypeNotFoundException.Params(new int?(relationTypeID)), customMessage)
  {
  }

  public RelationTypeNotFoundException([NotEmpty] Guid relationTypeGuid, [CanBeNull] string customMessage = null)
    : this(new RelationTypeNotFoundException.Params(guid: new Guid?(relationTypeGuid)), customMessage)
  {
  }

  public RelationTypeNotFoundException([NotNull] string relationTypeName, [CanBeNull] string customMessage)
    : this(new RelationTypeNotFoundException.Params(name: relationTypeName), customMessage)
  {
  }

  public RelationTypeNotFoundException(
    in RelationTypeNotFoundException.Params relationTypeParams,
    [CanBeNull] string customMessage = null)
    : base(customMessage ?? RelationTypeNotFoundException.CreateMessage(in relationTypeParams))
  {
    this._params = relationTypeParams;
  }

  [NotNull]
  private static string CreateMessage(
    in RelationTypeNotFoundException.Params relationTypeParams)
  {
    if (!string.IsNullOrWhiteSpace(relationTypeParams.Name))
      return $"Тип связи \"{relationTypeParams.Name}\" не найден!";
    if (!Intermech.Check.RelationTypeIdIsEmpty(relationTypeParams.ID))
      return $"Тип связи с ID={relationTypeParams.ID} не найден!";
    return !(relationTypeParams.Guid != Guid.Empty) ? "Тип связи не найден!" : $"Тип связи с GUID={relationTypeParams.Guid} не найден!";
  }

  [SecuritySafeCritical]
  protected RelationTypeNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._params = info.GetValue<RelationTypeNotFoundException.Params>("Params");
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Params", (object) this._params, typeof (RelationTypeNotFoundException.Params));
  }

  public readonly struct Params(int? id = null, Guid? guid = null, [CanBeNull] string name = null)
  {
    public readonly int ID = id ?? -1;
    public readonly Guid Guid = guid ?? Guid.Empty;
    [CanBeNull]
    public readonly string Name = name;
  }
}
