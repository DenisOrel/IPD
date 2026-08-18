// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectVersionNotFoundException
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
public class ObjectVersionNotFoundException : ObjectNotFoundException, ISerializable
{
  public readonly long ObjectVersionID;
  public readonly int VersionNum = -1;
  public Guid ObjectVersionGuid = Guid.Empty;
  [CanBeNull]
  private string _customMessage;

  public ObjectVersionNotFoundException([NotEmpty] long objectVersionID, [CanBeNull] string customMessage = null)
    : base(0L)
  {
    this.ObjectVersionID = objectVersionID;
    if (string.IsNullOrWhiteSpace(customMessage))
      return;
    this._customMessage = customMessage;
  }

  public ObjectVersionNotFoundException([NotEmpty] Guid objectVersionGuid, [CanBeNull] string customMessage = null)
    : base(0L)
  {
    this.ObjectVersionGuid = objectVersionGuid;
    if (string.IsNullOrWhiteSpace(customMessage))
      return;
    this._customMessage = customMessage;
  }

  public ObjectVersionNotFoundException([NotEmpty] long objectID, [CanBeEmpty] int versionNum, [CanBeNull] string customMessage = null)
    : this(objectID, customMessage)
  {
    this.VersionNum = versionNum;
  }

  protected ObjectVersionNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.ObjectVersionID = info.GetInt64(nameof (ObjectVersionID));
    this.VersionNum = info.GetInt32(nameof (VersionNum));
    this.ObjectVersionGuid = info.GetValue<Guid>(nameof (ObjectVersionGuid));
    this._customMessage = info.GetString("CustomMessage");
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("ObjectVersionID", this.ObjectVersionID);
    info.AddValue("VersionNum", this.VersionNum);
    info.AddValue("ObjectVersionGuid", (object) this.ObjectVersionGuid, typeof (Guid));
    info.AddValue("CustomMessage", (object) this._customMessage);
  }

  [NotNull]
  public override string Message
  {
    get
    {
      if (this._customMessage != null)
        return this._customMessage;
      if (!Intermech.Check.ObjectIdIsEmpty(this.ObjectVersionID))
        return $"Объект с идентификатором версии={this.ObjectVersionID} не найден";
      if (!Intermech.Check.ObjectIdIsEmpty(this.ObjectID) && this.VersionNum >= 0)
        return $"Версия номер {this.VersionNum} объекта ID={this.ObjectVersionID} не найдена";
      return this.ObjectVersionGuid != Guid.Empty ? $"Версия объекта c GUID={this.ObjectVersionGuid} не найдена" : base.Message ?? string.Empty;
    }
  }
}
