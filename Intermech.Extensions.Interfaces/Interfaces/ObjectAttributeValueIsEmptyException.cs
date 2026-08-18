// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectAttributeValueIsEmptyException
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
public class ObjectAttributeValueIsEmptyException : 
  AttributeValueIsEmptyException,
  ISerializable,
  IObjectException
{
  public long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.EntityID;
  }

  public ObjectAttributeValueIsEmptyException(
    [CanBeNull, CanBeEmpty, InvokerParameterName] string attributeName,
    [NotEmpty] int attributeID,
    [NotEmpty] long objectID,
    [CanBeNull] string message = null)
    : base(attributeName, attributeID, objectID, message)
  {
  }

  public ObjectAttributeValueIsEmptyException([NotEmpty] int attributeID, [NotEmpty] long objectID, [CanBeNull] string message = null)
    : base(attributeID, objectID, message)
  {
  }

  protected ObjectAttributeValueIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
