// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.RelationAttributeNotFoundException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class RelationAttributeNotFoundException : 
  CustomAttributeNotFoundException,
  IRelationException
{
  public long RelationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.EntityID;
  }

  [NotNull]
  protected override string GenerateMessage()
  {
    return this.AttributeID == 0 ? $"Атрибут с GUID={this.AttributeGuid} у связи с ID={this.RelationID} не найден." : $"Атрибут с ID={this.AttributeID} у связи с ID={this.RelationID} не найден.";
  }

  protected override void CheckEntityID(long entityID)
  {
  }

  public RelationAttributeNotFoundException(
    [NotEmpty] int attributeID,
    [NotEmpty] long relationID,
    [CanBeNull] string exceptionMessage = null)
    : base(attributeID, relationID, exceptionMessage)
  {
  }

  public RelationAttributeNotFoundException(
    [NotEmpty] Guid attributeGuid,
    [NotEmpty] long relationID,
    [CanBeNull] string exceptionMessage = null)
    : base(attributeGuid, relationID, exceptionMessage)
  {
  }

  protected RelationAttributeNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
