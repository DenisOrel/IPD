// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.LifeCycleStepNotFoundException
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
public class LifeCycleStepNotFoundException : KernelException, ISerializable
{
  private readonly LifeCycleStepNotFoundException.Params _params;

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
  public LifeCycleStepNotFoundException()
    : this(new LifeCycleStepNotFoundException.Params())
  {
  }

  public LifeCycleStepNotFoundException([NotEmpty] int lcStepID, [CanBeNull] string customMessage = null)
    : this(new LifeCycleStepNotFoundException.Params(new int?(lcStepID)), customMessage)
  {
  }

  public LifeCycleStepNotFoundException([NotEmpty] Guid lcStepGuid, [CanBeNull] string customMessage = null)
    : this(new LifeCycleStepNotFoundException.Params(guid: new Guid?(lcStepGuid)), customMessage)
  {
  }

  public LifeCycleStepNotFoundException([NotNull] string lcStepName, [CanBeNull] string customMessage)
    : this(new LifeCycleStepNotFoundException.Params(name: lcStepName), customMessage)
  {
  }

  public LifeCycleStepNotFoundException(
    in LifeCycleStepNotFoundException.Params lcStepParams,
    [CanBeNull] string customMessage = null)
    : base(customMessage ?? LifeCycleStepNotFoundException.CreateMessage(in lcStepParams))
  {
    this._params = lcStepParams;
  }

  [NotNull]
  private static string CreateMessage(
    in LifeCycleStepNotFoundException.Params lcStepParams)
  {
    if (!string.IsNullOrWhiteSpace(lcStepParams.Name))
      return $"Шаг жизненного цикла \"{lcStepParams.Name}\" не найден!";
    if (lcStepParams.ID != -1)
      return $"Шаг жизненного цикла с ID={lcStepParams.ID} не найден!";
    return !(lcStepParams.Guid != Guid.Empty) ? "Шаг жизненного цикла не найден!" : $"Шаг жизненного цикла с GUID={lcStepParams.Guid} не найден!";
  }

  [SecuritySafeCritical]
  protected LifeCycleStepNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this._params = info.GetValue<LifeCycleStepNotFoundException.Params>("Params");
  }

  public override void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Params", (object) this._params, typeof (LifeCycleStepNotFoundException.Params));
  }

  public readonly struct Params(int? id = null, Guid? guid = null, [CanBeNull] string name = null)
  {
    public readonly int ID = id ?? -1;
    public readonly Guid Guid = guid ?? Guid.Empty;
    [CanBeNull]
    public readonly string Name = name;
  }
}
