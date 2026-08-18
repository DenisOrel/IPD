// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ObjectAttributeNotFoundException
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
public class ObjectAttributeNotFoundException : CustomAttributeNotFoundException, IObjectException
{
  public long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.EntityID;
  }

  [NotNull]
  protected override string GenerateMessage()
  {
    return this.AttributeID == 0 ? $"Атрибут с GUID={this.AttributeGuid} у объекта с ID={this.ObjectID} не найден" : $"Атрибут с ID={this.AttributeID} у объекта с ID={this.ObjectID} не найден";
  }

  protected override void CheckEntityID(long entityID)
  {
  }

  public ObjectAttributeNotFoundException([NotEmpty] int attributeID, [NotEmpty] long objectID, [CanBeNull] string exceptionMessage = null)
    : base(attributeID, objectID, exceptionMessage)
  {
  }

  public ObjectAttributeNotFoundException(
    [NotEmpty] Guid attributeGuid,
    [NotEmpty] long objectID,
    [CanBeNull] string exceptionMessage = null)
    : base(attributeGuid, objectID, exceptionMessage)
  {
  }

  protected ObjectAttributeNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
