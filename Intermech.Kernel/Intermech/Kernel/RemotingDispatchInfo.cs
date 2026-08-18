// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.RemotingDispatchInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class RemotingDispatchInfo
{
  private readonly string _clientThreadKey;
  private readonly int _serverThreadId;
  private CancellationTokenSource _operation;

  public RemotingDispatchInfo(string clientThreadKey, int serverThreadId)
  {
    this._clientThreadKey = clientThreadKey;
    this._serverThreadId = serverThreadId;
  }

  public string ClientThreadKey => this._clientThreadKey;

  public int ServerThreadId => this._serverThreadId;

  public CancellationTokenSource Operation
  {
    [DebuggerStepThrough] get => this._operation == null ? this.CreateOperation() : this._operation;
  }

  private CancellationTokenSource CreateOperation()
  {
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    return Interlocked.CompareExchange<CancellationTokenSource>(ref this._operation, cancellationTokenSource, (CancellationTokenSource) null) ?? cancellationTokenSource;
  }
}
