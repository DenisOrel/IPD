// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectWithGuidNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class ObjectWithGuidNotFoundException : ObjectNotFoundException, ISerializable
{
  [CanBeNull]
  private string _customMessage;

  public ObjectWithGuidNotFoundException([NotEmpty] Guid objectGuid, [CanBeNull] string customMessage = null)
    : base(0L)
  {
    this.ObjectGuid = objectGuid;
    if (string.IsNullOrWhiteSpace(customMessage))
      return;
    this._customMessage = customMessage;
  }

  protected ObjectWithGuidNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.ObjectGuid = info.GetGuid(nameof (ObjectGuid));
    this._customMessage = info.GetString("CustomMessage");
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ObjectGuid", (object) this.ObjectGuid);
    info.AddValue("CustomMessage", (object) this._customMessage);
  }

  [NotNull]
  public override string Message
  {
    get => this._customMessage ?? $"Объект с Guid=\"{this.ObjectGuid}\" не найден";
  }

  [NotEmpty]
  public Guid ObjectGuid { get; }
}
