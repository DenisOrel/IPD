// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.RelationTypeIdIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class RelationTypeIdIsEmptyException : ValueEmptyException, ISerializable
{
  public RelationTypeIdIsEmptyException()
  {
  }

  public RelationTypeIdIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    : base(valueName, message)
  {
  }

  protected RelationTypeIdIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static RelationTypeIdIsEmptyException ForCollection([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
  {
    return new RelationTypeIdIsEmptyException(collectionName, RelationTypeIdIsEmptyException.CreateCollectionMessage(collectionName, message));
  }

  public override string Message
  {
    get => RelationTypeIdIsEmptyException.CreateMessage(this.ValueName, this.OriginalMessage);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(valueName) ? "Идентификатор типа связи пуст." : $"Идентификатор типа связи {valueName} пуст.";
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateCollectionMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(collectionName) ? "Коллекция идентификаторов типов связей содержит пустой идентификатор." : $"Коллекция идентификаторов типов связей {collectionName} содержит пустой идентификатор.";
  }
}
