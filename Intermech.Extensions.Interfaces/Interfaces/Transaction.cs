// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Transaction
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Interfaces;

public static class Transaction
{
  [NotNull]
  [MustUseReturnValue]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static ITransactionKeeper Start(
    [NotNull] IUserSession userSession,
    [NotNull, NotWhitespace] string name,
    TransactionKeeperDisposeAction disposeAction = TransactionKeeperDisposeAction.AutoCommit,
    ExternalTransactionRelationship externalTransactionRelationship = ExternalTransactionRelationship.CreateNestedTransaction,
    [CanBeNull] Func<bool> getCanCommit = null,
    [CanBeNull] Action beforeCommit = null,
    [CanBeNull] Action afterCommit = null,
    [CanBeNull] Action<ExactRollbackCause> beforeRollback = null,
    [CanBeNull] Action<ExactRollbackCause> afterRollback = null,
    [CanBeNull] SynchronizationContext synchronizationContext = null,
    [CanBeNull] CancellationToken? cancellationToken = null,
    [CanBeNull, CallerFilePath] string callerFilePath = null)
  {
    return (ITransactionKeeper) new TransactionKeeper(userSession, name, disposeAction, externalTransactionRelationship, getCanCommit, beforeCommit, afterCommit, beforeRollback, afterRollback, synchronizationContext, cancellationToken, callerFilePath);
  }

  public static TransactionStatus GetKeeperStatus([NotEmpty] long transactionID)
  {
    return TransactionKeeper.GetStatus(transactionID);
  }

  public static bool TryGetKeeperStatus([NotEmpty] long transactionID, out TransactionStatus status)
  {
    return TransactionKeeper.TryGetStatus(transactionID, out status);
  }

  internal static ExactRollbackCause GetRollbackCause([NotEmpty] long transactionID)
  {
    return TransactionKeeper.GetRollbackCause(transactionID);
  }

  internal static bool TryGetRollbackCause([NotEmpty] long transactionID, out ExactRollbackCause rollbackCause)
  {
    return TransactionKeeper.TryGetRollbackCause(transactionID, out rollbackCause);
  }

  [NotNull]
  [ItemNotNull]
  public static ITransactionKeeper[] GetActiveKeepers() => TransactionKeeper.GetActiveKeepers();
}
