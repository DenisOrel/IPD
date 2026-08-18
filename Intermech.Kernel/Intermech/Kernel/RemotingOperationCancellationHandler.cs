// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.RemotingOperationCancellationHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting;
using System;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class RemotingOperationCancellationHandler
{
  private Action cleanupAction;
  private Action cleanupActionHelperMethod;
  private bool alreadyInCleanupAction;

  public RemotingOperationCancellationHandler(Action cleanupAction)
  {
    this.cleanupAction = cleanupAction != null ? cleanupAction : throw new ArgumentNullException(nameof (cleanupAction));
    this.cleanupActionHelperMethod = new Action(this.CleanupActionHelper);
  }

  public void CheckCancellationRequested()
  {
    RemotingOperationContext current = RemotingOperationContext.Current;
    if (!current.IsStarted || this.alreadyInCleanupAction)
      return;
    CancellationToken cancellationToken = current.CancellationToken;
    if (!cancellationToken.CanBeCanceled)
      return;
    current.RegisterCompletionCallback(this.cleanupActionHelperMethod);
    if (cancellationToken.IsCancellationRequested)
      throw new Exception("The current remoting operation is cancelled.");
  }

  private void CleanupActionHelper()
  {
    if (!RemotingOperationContext.Current.CancellationToken.IsCancellationRequested)
      return;
    this.alreadyInCleanupAction = true;
    try
    {
      this.cleanupAction();
    }
    finally
    {
      this.alreadyInCleanupAction = false;
    }
  }
}
