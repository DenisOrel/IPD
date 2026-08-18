// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.RelationAttributeValueIsEmptyException
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
public class RelationAttributeValueIsEmptyException : 
  AttributeValueIsEmptyException,
  ISerializable,
  IRelationException
{
  public long RelationID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.EntityID;
  }

  public RelationAttributeValueIsEmptyException(
    [CanBeNull, CanBeEmpty, InvokerParameterName] string attributeName,
    [NotEmpty] int attributeID,
    [NotEmpty] long relationID,
    [CanBeNull] string message = null)
    : base(attributeName, attributeID, relationID, message)
  {
  }

  public RelationAttributeValueIsEmptyException([NotEmpty] int attributeID, [NotEmpty] long relationID, [CanBeNull] string message = null)
    : base(attributeID, relationID, message)
  {
  }

  protected RelationAttributeValueIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
