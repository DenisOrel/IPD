// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ITransactionKeeper
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces;

public interface ITransactionKeeper : IDisposable
{
  long ID { get; }

  [NotNull]
  [NotWhitespace]
  string Name { get; }

  [NotNull]
  [NotWhitespace]
  string CallerFilePath { get; }

  TransactionStatus Status { get; }

  TransactionKeeperDisposeAction DisposeAction { get; set; }

  ExternalTransactionRelationship ExternalTransactionRelationship { get; }

  bool CanCommit { get; set; }

  [CanBeNull]
  Func<bool> CanCommitMethod { get; set; }

  [CanBeNull]
  event Action BeforeCommit;

  [CanBeNull]
  event Action AfterCommit;

  [CanBeNull]
  event Action<ExactRollbackCause> BeforeRollback;

  [CanBeNull]
  event Action<ExactRollbackCause> AfterRollback;

  void Commit();

  bool TryCommit();

  void Rollback();

  bool TransactionStartedByKeeper { get; }

  bool IsNestedTransaction { get; }

  bool InTransaction { get; }

  [CanBeNull]
  SynchronizationContext SynchronizationContext { get; set; }

  System.Threading.CancellationToken? CancellationToken { get; set; }
}
