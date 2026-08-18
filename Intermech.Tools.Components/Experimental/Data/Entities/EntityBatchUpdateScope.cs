// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityBatchUpdateScope
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime;
using System;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

public class EntityBatchUpdateScope : IEntityBatchUpdateScope, IDisposable
{
  private IEntityBatchUpdateService service;
  private IEntityChangeTrackerBase changeTracker;
  private IEntityBatchUpdateLog updateLog;
  private bool isComplete;
  private bool isDisposed;

  public EntityBatchUpdateScope(
    IEntityBatchUpdateService service,
    IEntityChangeTrackerBase changeTracker)
  {
    if (service == null)
      throw new ArgumentNullException(nameof (service));
    if (changeTracker == null)
      throw new ArgumentNullException(nameof (changeTracker));
    this.service = service;
    this.changeTracker = changeTracker;
  }

  public IEntityBatchUpdateLog UpdateLog
  {
    [DebuggerStepThrough] get => this.updateLog;
    set
    {
      this.CheckNotDisposed();
      this.updateLog = value;
    }
  }

  public void Complete()
  {
    this.CheckNotDisposed();
    if (this.isComplete)
      return;
    this.isComplete = true;
  }

  public void Dispose()
  {
    if (this.isDisposed)
      return;
    try
    {
      if (!this.isComplete)
        return;
      this.service.SaveChanges(this.changeTracker, this.updateLog);
    }
    finally
    {
      SilentActionInvoker.Default.Invoke(new Action(this.DoCloseScope));
      this.isDisposed = true;
    }
  }

  protected virtual void DoCloseScope()
  {
  }

  private void CheckNotDisposed()
  {
    if (this.isDisposed)
      throw new ObjectDisposedException(this.GetType().FullName);
  }
}
