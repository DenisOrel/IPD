// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.ObjectTypeIdIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class ObjectTypeIdIsEmptyException : ValueEmptyException, ISerializable
{
  public ObjectTypeIdIsEmptyException()
  {
  }

  public ObjectTypeIdIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    : base(valueName, message)
  {
  }

  protected ObjectTypeIdIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ObjectTypeIdIsEmptyException ForCollection([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
  {
    return new ObjectTypeIdIsEmptyException(collectionName, ObjectTypeIdIsEmptyException.CreateCollectionMessage(collectionName, message));
  }

  public override string Message
  {
    get => ObjectTypeIdIsEmptyException.CreateMessage(this.ValueName, this.OriginalMessage);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(valueName) ? "Идентификатор типа объекта пуст." : $"Идентификатор типа объекта {valueName} пуст.";
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateCollectionMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(collectionName) ? "Коллекция идентификаторов типов объектов содержит пустой идентификатор." : $"Коллекция идентификаторов типов объектов {collectionName} содержит пустой идентификатор.";
  }
}
