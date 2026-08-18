// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.LongRunningOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal abstract class LongRunningOperation
{
  private readonly Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> errorsBuilder;
  private Action<string> logAction;
  private Action<int> progressAction;
  private Func<bool> cancellationPredicate;

  protected LongRunningOperation() => this.errorsBuilder = new Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError>();

  public ICollection<OperationError> Errors
  {
    [DebuggerStepThrough] get => this.errorsBuilder.Items;
  }

  protected Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> ErrorsBuilder
  {
    [DebuggerStepThrough] get => this.errorsBuilder;
  }

  public Action<int> ProgressAction
  {
    [DebuggerStepThrough] get => this.progressAction;
    [DebuggerStepThrough] set => this.progressAction = value;
  }

  public Action<string> LogAction
  {
    [DebuggerStepThrough] get => this.logAction;
    [DebuggerStepThrough] set => this.logAction = value;
  }

  public Func<bool> CancellationPredicate
  {
    [DebuggerStepThrough] get => this.cancellationPredicate;
    [DebuggerStepThrough] set => this.cancellationPredicate = value;
  }

  protected bool IsCancellationRequested
  {
    [DebuggerStepThrough] get => this.cancellationPredicate != null && this.cancellationPredicate();
  }

  protected void ReportLogMessage(string message)
  {
    if (this.logAction == null)
      return;
    this.logAction(message);
  }

  protected void ReportProgress(int percentValue)
  {
    if (this.progressAction == null)
      return;
    if (percentValue > 100)
      percentValue = 100;
    this.progressAction(percentValue);
  }

  protected void CheckCancellationOperation()
  {
    if (this.IsCancellationRequested)
      throw new AbortException();
  }
}
