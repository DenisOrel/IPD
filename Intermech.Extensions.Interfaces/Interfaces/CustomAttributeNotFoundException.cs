// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.CustomAttributeNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class CustomAttributeNotFoundException : AttributeNotFoundException
{
  public readonly int AttributeID;
  public readonly Guid AttributeGuid;
  [CanBeNull]
  private string _exceptionMessage;

  public long EntityID { get; protected set; }

  [NotNull]
  public string ExceptionMessage
  {
    get
    {
      return string.IsNullOrWhiteSpace(this._exceptionMessage) ? (this._exceptionMessage = this.GenerateMessage()) : this._exceptionMessage;
    }
  }

  [NotNull]
  protected virtual string GenerateMessage()
  {
    return this.AttributeID == 0 ? $"Атрибут с GUID={this.AttributeGuid} не найден" : $"Атрибут с ID={this.AttributeID} не найден";
  }

  [Conditional("DEBUG")]
  [Conditional("FORCE_CHECKS")]
  protected virtual void CheckEntityID(long entityID)
  {
  }

  public CustomAttributeNotFoundException([NotEmpty] int attributeID, [NotEmpty] long entityID, [CanBeNull] string exceptionMessage = null)
    : base(attributeID, entityID)
  {
    this.AttributeID = attributeID;
    this.AttributeGuid = Guid.Empty;
    this.EntityID = entityID;
    this._exceptionMessage = exceptionMessage;
  }

  public CustomAttributeNotFoundException(
    [NotEmpty] Guid attributeGuid,
    [NotEmpty] long entityID,
    [CanBeNull] string exceptionMessage = null)
    : base(0, entityID)
  {
    this.AttributeID = 0;
    this.AttributeGuid = attributeGuid;
    this.EntityID = entityID;
    this._exceptionMessage = exceptionMessage;
  }

  protected CustomAttributeNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.AttributeID = info.GetInt32(nameof (AttributeID));
    this.AttributeGuid = Guid.Parse(info.GetString(nameof (AttributeGuid)) ?? throw new KeyNotFoundException(nameof (AttributeGuid)));
    this.EntityID = info.GetInt64(nameof (EntityID));
    this._exceptionMessage = info.GetString(nameof (ExceptionMessage));
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("AttributeID", this.AttributeID);
    info.AddValue("AttributeGuid", (object) this.AttributeGuid.ToString());
    info.AddValue("EntityID", this.EntityID);
    info.AddValue("ExceptionMessage", (object) this._exceptionMessage);
  }

  [NotNull]
  public override string Message => this.ExceptionMessage;
}
