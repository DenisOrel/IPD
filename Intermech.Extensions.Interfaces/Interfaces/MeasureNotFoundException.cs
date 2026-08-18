// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MeasureNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class MeasureNotFoundException : 
  ObjectNotFoundException,
  IEquatable<MeasureNotFoundException>,
  ISerializable
{
  private const string Msg = "Тип связи с ID = {0} не найден";
  public readonly Guid MeasureGuid;
  [CanBeNull]
  private readonly string _customMessage;

  public long MeasureID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ObjectID;
  }

  public MeasureNotFoundException([NotEmpty] long measureID, [CanBeNull] string customMessage = null)
    : base(measureID)
  {
    this._customMessage = customMessage;
  }

  public MeasureNotFoundException([NotEmpty] Guid measureGuid, [CanBeNull] string customMessage = null)
    : base(0L)
  {
    this.MeasureGuid = measureGuid;
  }

  protected MeasureNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.MeasureGuid = info.GetGuid("Guid");
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("Guid", (object) this.MeasureGuid);
  }

  [NotNull]
  public override string Message
  {
    get => this._customMessage ?? $"Тип связи с ID = {this.MeasureID} не найден";
  }

  public override int GetHashCode()
  {
    return !Intermech.Check.ObjectIdIsEmpty(this.MeasureID) ? this.MeasureID.GetHashCode() : this.MeasureGuid.GetHashCode();
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (obj == this)
      return true;
    return obj is MeasureNotFoundException notFoundException && base.Equals((object) notFoundException) && this.MeasureGuid == notFoundException.MeasureGuid;
  }

  public bool Equals(MeasureNotFoundException other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    return base.Equals((object) other) && this.MeasureGuid.Equals(other.MeasureGuid);
  }
}
