// Decompiled with JetBrains decompiler
// Type: Intermech.Diagnostics.AttributeIsEmptyException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Diagnostics;

[Serializable]
public class AttributeIsEmptyException : ValueEmptyException, ISerializable
{
  public AttributeIsEmptyException()
  {
  }

  public AttributeIsEmptyException([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message = null)
    : base(valueName, message)
  {
  }

  protected AttributeIsEmptyException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static AttributeIsEmptyException ForCollection([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message = null)
  {
    return new AttributeIsEmptyException(collectionName, AttributeIsEmptyException.CreateCollectionMessage(collectionName, message));
  }

  public override string Message
  {
    get => AttributeIsEmptyException.CreateMessage(this.ValueName, this.OriginalMessage);
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string valueName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(valueName) ? "Атрибут пуст." : $"Атрибут {valueName} пуст.";
  }

  [NotNull]
  [NotWhitespace]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static string CreateCollectionMessage([CanBeNull, CanBeEmpty, InvokerParameterName] string collectionName, [CanBeNull] string message)
  {
    if (!string.IsNullOrWhiteSpace(message))
      return message;
    return string.IsNullOrWhiteSpace(collectionName) ? "Коллекция атрибутов содержит пустой элемент." : $"Коллекция атрибутов {collectionName} содержит пустой элемент.";
  }
}
