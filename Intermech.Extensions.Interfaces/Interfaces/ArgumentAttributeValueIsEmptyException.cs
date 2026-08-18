// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ArgumentAttributeValueIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class ArgumentAttributeValueIsEmptyException : ArgumentValueEmptyException, ISerializable
{
  public readonly int AttributeID;

  public long EntityID { get; protected set; }

  [CanBeNull]
  [CanBeEmpty]
  public string AttributeName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ParamName;
  }

  public ArgumentAttributeValueIsEmptyException(
    [CanBeNull, CanBeEmpty, InvokerParameterName] string attributeName,
    [NotEmpty] int attributeID,
    [NotEmpty] long entityID,
    [CanBeNull] string message = null)
    : base(attributeName, message)
  {
    this.AttributeID = attributeID;
    this.EntityID = entityID;
  }

  public ArgumentAttributeValueIsEmptyException([NotEmpty] int attributeID, [NotEmpty] long entityID, [CanBeNull] string message = null)
    : base($"Атрибут с ID={attributeID}", message)
  {
    this.AttributeID = attributeID;
    this.EntityID = entityID;
  }

  protected ArgumentAttributeValueIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.AttributeID = info.GetInt32(nameof (AttributeID));
    this.EntityID = info.GetInt64(nameof (EntityID));
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("AttributeID", this.AttributeID);
    info.AddValue("EntityID", this.EntityID);
  }

  [NotNull]
  public override string Message
  {
    get
    {
      if (!string.IsNullOrWhiteSpace(this.OriginalMessage))
        return this.OriginalMessage;
      return !string.IsNullOrWhiteSpace(this.ParamName) ? $"Атрибут {this.ParamName} с ID={this.AttributeID} пуст" : $"Атрибут с ID={this.AttributeID} пуст";
    }
  }
}
