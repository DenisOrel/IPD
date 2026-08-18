// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.InvalidCommitException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class InvalidCommitException : TransactionRollbackException, ISerializable
{
  public InvalidCommitException(
    [NotEmpty] long transactionID,
    [NotNull, NotWhitespace] string transactionName,
    [NotNull, NotWhitespace] string transactionCallerFilePath,
    [NotNull, NotWhitespace] string message)
    : base(transactionID, transactionName, transactionCallerFilePath, message)
  {
  }

  protected InvalidCommitException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
