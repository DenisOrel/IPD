// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.IterationIdIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class IterationIdIsEmptyException : ValueEmptyException, ISerializable
{
  public IterationIdIsEmptyException()
  {
  }

  public IterationIdIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    : base(valueName, message)
  {
  }

  protected IterationIdIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IterationIdIsEmptyException ForCollection([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
  {
    return new IterationIdIsEmptyException(collectionName, IterationIdIsEmptyException.CreateCollectionMessage(collectionName, message));
  }

  public override string Message
  {
    get => IterationIdIsEmptyException.CreateMessage(this.ValueName, this.OriginalMessage);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(valueName) ? "Идентификатор итерации пуст." : $"Идентификатор итерации {valueName} пуст.";
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateCollectionMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(collectionName) ? "Коллекция идентификаторов итераций содержит пустой идентификатор." : $"Коллекция идентификаторов итераций {collectionName} содержит пустой идентификатор.";
  }
}
