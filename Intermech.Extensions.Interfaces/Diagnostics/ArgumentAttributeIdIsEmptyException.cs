// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.ArgumentAttributeIdIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class ArgumentAttributeIdIsEmptyException : ArgumentValueEmptyException, ISerializable
{
  public ArgumentAttributeIdIsEmptyException()
  {
  }

  public ArgumentAttributeIdIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string argumentName, [CanBeNull] string message = null)
    : base(argumentName, message)
  {
  }

  protected ArgumentAttributeIdIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ArgumentAttributeIdIsEmptyException ForCollection(
    [CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName,
    [CanBeNull] string message = null)
  {
    return new ArgumentAttributeIdIsEmptyException(collectionName, AttributeIdIsEmptyException.CreateCollectionMessage(collectionName, message));
  }

  public override string Message
  {
    get => AttributeIdIsEmptyException.CreateMessage(this.ParamName, this.OriginalMessage);
  }
}
