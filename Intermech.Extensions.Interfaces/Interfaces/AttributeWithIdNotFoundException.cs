// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AttributeWithIdNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class AttributeWithIdNotFoundException : AttributeNotFoundException, ISerializable
{
  private const string Msg = "Атрибут с ID = {0} не найден";
  [CanBeNull]
  private protected readonly string _CustomMessage;

  public int AttributeID { get; }

  public AttributeWithIdNotFoundException([NotEmpty] int attributeID, [CanBeNull] string customMessage = null)
    : base(attributeID, 0L)
  {
    this.AttributeID = attributeID;
    this._CustomMessage = customMessage;
  }

  protected AttributeWithIdNotFoundException([NotEmpty] Guid attributeGuid, [CanBeNull] string customMessage = null)
    : base($"Атрибут с Guid = {attributeGuid}", attributeGuid.ToString(), 0L)
  {
    this.AttributeID = 0;
  }

  protected AttributeWithIdNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.AttributeID = info.GetInt32("_AttributeID");
  }

  [NotNull]
  public override string Message
  {
    get => this._CustomMessage ?? $"Атрибут с ID = {this.AttributeID} не найден";
  }

  public override int GetHashCode() => this.AttributeID;

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (obj == this)
      return true;
    return obj is AttributeWithIdNotFoundException notFoundException && this.AttributeID == notFoundException.AttributeID;
  }
}
