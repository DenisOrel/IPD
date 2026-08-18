// Decompiled with JetBrains decompiler
// Type: Intermech.Exceptions.ImageResourceNotFoundException
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

#nullable disable
namespace Intermech.Exceptions;

[Serializable]
public class ImageResourceNotFoundException : ResourceNotFoundException, ISerializable
{
  [NotNull]
  [NotWhitespace]
  public string ImageName
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ResourceName;
  }

  public ImageResourceNotFoundException([NotNull, NotWhitespace] string assemblyName, [NotNull, NotWhitespace] string imageName, [CanBeNull] string message = null)
    : base(assemblyName, imageName, message ?? ImageResourceNotFoundException.GetDefaultMessage(assemblyName, imageName))
  {
  }

  [NotNull]
  [NotWhitespace]
  private static string GetDefaultMessage([NotNull, NotWhitespace] string assemblyName, [NotNull, NotWhitespace] string imageName)
  {
    return $"Resource \"{imageName}\" not found in assembly \"{assemblyName}\"!";
  }

  [SecuritySafeCritical]
  protected ImageResourceNotFoundException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
