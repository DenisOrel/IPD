// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.ArgumentRelationIdIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class ArgumentRelationIdIsEmptyException : ArgumentValueEmptyException, ISerializable
{
  public ArgumentRelationIdIsEmptyException()
  {
  }

  public ArgumentRelationIdIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string argumentName, [CanBeNull] string message = null)
    : base(argumentName, message)
  {
  }

  protected ArgumentRelationIdIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ArgumentRelationIdIsEmptyException ForCollection(
    [CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName,
    [CanBeNull] string message = null)
  {
    return new ArgumentRelationIdIsEmptyException(collectionName, RelationIdIsEmptyException.CreateCollectionMessage(collectionName, message));
  }

  public override string Message
  {
    get => RelationIdIsEmptyException.CreateMessage(this.ParamName, this.OriginalMessage);
  }
}
