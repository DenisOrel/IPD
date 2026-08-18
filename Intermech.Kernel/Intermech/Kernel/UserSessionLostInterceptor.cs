// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionLostInterceptor
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting;
using Intermech.Remoting.Optimized;
using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class UserSessionLostInterceptor : IServerFormatterSinkInterceptor
{
  private const string clientThreadKeyProperty = "X-IPS-ClientThreadKey";
  private const string clientThreadDispathInfoProperty = "X-IPS-ClientThreadDispatchInfo";
  private static readonly RemotingDispatchTable remotingDispatchTable = new RemotingDispatchTable();

  public Action<string> LogAction { get; set; }

  public void ProcessMessageStart(
    IMessage msg,
    ITransportHeaders requestHeaders,
    Stream requestStream)
  {
    this.StartRemotingOperation(msg);
  }

  public ServerProcessing? ProcessMessage(
    IMessage msg,
    ITransportHeaders requestHeaders,
    Stream requestStream,
    out IMessage responseMsg)
  {
    if (!(requestHeaders[(object) "X-IPS-ClientThreadKey"] is string requestHeader))
    {
      responseMsg = (IMessage) null;
      return new ServerProcessing?();
    }
    RemotingDispatchInfo dispatchInfo = new RemotingDispatchInfo(requestHeader, Thread.CurrentThread.ManagedThreadId);
    RemotingDispatchInfo otherThreadDispatchInfo = UserSessionLostInterceptor.remotingDispatchTable.TryMapClientThread(dispatchInfo);
    if (otherThreadDispatchInfo.ServerThreadId == dispatchInfo.ServerThreadId)
    {
      RemotingOperationContext.Current.CancellationToken = dispatchInfo.Operation.Token;
      msg.Properties[(object) "X-IPS-ClientThreadDispatchInfo"] = (object) dispatchInfo;
      responseMsg = (IMessage) null;
      return new ServerProcessing?();
    }
    otherThreadDispatchInfo.Operation.Cancel();
    if (this.LogAction != null)
      this.LogSessionIsLost(otherThreadDispatchInfo);
    responseMsg = (IMessage) new ReturnMessage((Exception) new UserSessionLostException("В результате односторонней ошибки remoting текущая пользовательская сессия была отключена от сервера приложений."), (IMethodCallMessage) msg);
    return new ServerProcessing?(ServerProcessing.Complete);
  }

  public void ProcessMessageFinish(
    IMessage msg,
    ITransportHeaders requestHeaders,
    Stream requestStream,
    IMessage responseMsg,
    ITransportHeaders responseHeaders,
    Stream responseStream,
    ServerProcessing result)
  {
    this.StopRemotingOperation(msg);
  }

  public void ProcessMessageFailed(
    IMessage msg,
    ITransportHeaders requestHeaders,
    IMessage responseMsg,
    ITransportHeaders responseHeaders,
    Exception exception)
  {
    this.StopRemotingOperation(msg);
  }

  private void StartRemotingOperation(IMessage msg) => RemotingOperationContext.Current.Start();

  private void StopRemotingOperation(IMessage msg)
  {
    if (msg.Properties[(object) "X-IPS-ClientThreadDispatchInfo"] is RemotingDispatchInfo property)
      UserSessionLostInterceptor.remotingDispatchTable.UnmapClientThread(property);
    RemotingOperationContext current = RemotingOperationContext.Current;
    if (!current.IsStarted)
      return;
    current.Stop();
  }

  private void LogSessionIsLost(RemotingDispatchInfo otherThreadDispatchInfo)
  {
    this.LogAction($"Client thread ID: {otherThreadDispatchInfo.ClientThreadKey}, Server thread ID: {otherThreadDispatchInfo.ServerThreadId}, ServerFormatterSink: {"Обнаружена односторонняя ошибка remoting. Текущая пользовательская сессия была отключена."}");
  }
}
