// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.LifeCycleLevelNotFoundException
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
public class LifeCycleLevelNotFoundException : KernelException, ISerializable
{
  private readonly LifeCycleLevelNotFoundException.Params _params;

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
  public LifeCycleLevelNotFoundException()
    : this(new LifeCycleLevelNotFoundException.Params())
  {
  }

  public LifeCycleLevelNotFoundException([NotEmpty] int lcLevelID, [CanBeNull] string customMessage = null)
    : this(new LifeCycleLevelNotFoundException.Params(new int?(lcLevelID)), customMessage)
  {
  }

  public LifeCycleLevelNotFoundException([NotEmpty] Guid lcLevelGuid, [CanBeNull] string customMessage = null)
    : this(new LifeCycleLevelNotFoundException.Params(guid: new Guid?(lcLevelGuid)), customMessage)
  {
  }

  public LifeCycleLevelNotFoundException([NotNull] string lcLevelName, [CanBeNull] string customMessage)
    : this(new LifeCycleLevelNotFoundException.Params(name: lcLevelName), customMessage)
  {
  }

  public LifeCycleLevelNotFoundException(
    in LifeCycleLevelNotFoundException.Params lcLevelParams,
    [CanBeNull] string customMessage = null)
    : base(customMessage ?? LifeCycleLevelNotFoundException.CreateMessage(in lcLevelParams))
  {
    this._params = lcLevelParams;
  }

  [NotNull]
  private static string CreateMessage(
    in LifeCycleLevelNotFoundException.Params lcLevelParams)
  {
    if (!string.IsNullOrWhiteSpace(lcLevelParams.Name))
      return $"Уровень продвижения \"{lcLevelParams.Name}\" не найден!";
    if (lcLevelParams.ID != 0)
      return $"Уровень продвижения с ID={lcLevelParams.ID} не найден!";
    return !(lcLevelParams.Guid != Guid.Empty) ? "Уровень продвижения не найден!" : $"Уровень продвижения с GUID={lcLevelParams.Guid} не найден!";
  }

  [SecuritySafeCritical]
  protected LifeCycleLevelNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._params = info.GetValue<LifeCycleLevelNotFoundException.Params>("Params");
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Params", (object) this._params, typeof (LifeCycleLevelNotFoundException.Params));
  }

  public readonly struct Params(int? id = null, Guid? guid = null, [CanBeNull] string name = null)
  {
    public readonly int ID = id ?? 0;
    public readonly Guid Guid = guid ?? Guid.Empty;
    [CanBeNull]
    public readonly string Name = name;
  }
}
