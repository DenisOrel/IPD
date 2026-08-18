// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.TransactionRollbackException
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Interfaces;

[Serializable]
public class TransactionRollbackException : Exception, ISerializable
{
  [NotEmpty]
  public long TransactionID { get; }

  [NotNull]
  [NotWhitespace]
  public string TransactionName { get; }

  [NotNull]
  [NotWhitespace]
  public string TransactionCallerFilePath { get; }

  public TransactionRollbackException(
    [NotEmpty] long transactionID,
    [NotNull, NotWhitespace] string transactionName,
    [NotNull, NotWhitespace] string transactionCallerFilePath,
    [NotNull, NotWhitespace] string message)
    : base(message)
  {
    this.TransactionID = transactionID;
    this.TransactionName = transactionName;
    this.TransactionCallerFilePath = transactionCallerFilePath;
  }

  protected TransactionRollbackException([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.TransactionID = info.GetInt64(nameof (TransactionID));
    this.TransactionName = info.GetString(nameof (TransactionName)) ?? "Unknown";
    this.TransactionCallerFilePath = info.GetString(nameof (TransactionCallerFilePath)) ?? "Unknown";
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    base.GetObjectData(info, context);
    info.AddValue("TransactionID", this.TransactionID);
    info.AddValue("TransactionName", (object) this.TransactionName);
    info.AddValue("TransactionCallerFilePath", (object) this.TransactionCallerFilePath);
  }
}
