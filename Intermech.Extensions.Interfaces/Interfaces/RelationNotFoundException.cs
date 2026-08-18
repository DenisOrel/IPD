// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.RelationNotFoundException
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
public class RelationNotFoundException : KernelException, ISerializable
{
  private readonly RelationNotFoundException.Params _params;

  public long ID
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
  public RelationNotFoundException()
    : this(new RelationNotFoundException.Params())
  {
  }

  public RelationNotFoundException([NotEmpty] long relationID, [CanBeNull] string customMessage = null)
    : this(new RelationNotFoundException.Params(new long?(relationID)), customMessage)
  {
  }

  public RelationNotFoundException([NotEmpty] Guid relationGuid, [CanBeNull] string customMessage = null)
    : this(new RelationNotFoundException.Params(guid: new Guid?(relationGuid)), customMessage)
  {
  }

  public RelationNotFoundException([NotNull] string relationName, [CanBeNull] string customMessage)
    : this(new RelationNotFoundException.Params(name: relationName), customMessage)
  {
  }

  public RelationNotFoundException(
    in RelationNotFoundException.Params relationParams,
    [CanBeNull] string customMessage = null)
    : base(customMessage ?? RelationNotFoundException.CreateMessage(in relationParams))
  {
    this._params = relationParams;
  }

  [NotNull]
  private static string CreateMessage(in RelationNotFoundException.Params relationParams)
  {
    if (!string.IsNullOrWhiteSpace(relationParams.Name))
      return $"Тип связи \"{relationParams.Name}\" не найден!";
    if (relationParams.ID != -1L)
      return $"Тип связи с ID={relationParams.ID} не найден!";
    return !(relationParams.Guid != Guid.Empty) ? "Тип связи не найден!" : $"Тип связи с GUID={relationParams.Guid} не найден!";
  }

  [SecuritySafeCritical]
  protected RelationNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._params = info.GetValue<RelationNotFoundException.Params>("Params");
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Params", (object) this._params, typeof (RelationNotFoundException.Params));
  }

  public readonly struct Params(long? id = null, Guid? guid = null, [CanBeNull] string name = null)
  {
    public readonly long ID = id ?? -1L;
    public readonly Guid Guid = guid ?? Guid.Empty;
    [CanBeNull]
    public readonly string Name = name;
  }
}
