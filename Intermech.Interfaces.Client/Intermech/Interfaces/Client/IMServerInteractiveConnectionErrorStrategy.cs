// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IMServerInteractiveConnectionErrorStrategy
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Diagnostics;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерактивный алгоритм обработки ошибок подключения к серверу приложений, который
/// в диалоговом режиме спрашивает у пользователя, что требуется делать дальше.
/// Реализация является thread safe.
/// </summary>
public sealed class IMServerInteractiveConnectionErrorStrategy : IMServerConnectionErrorStrategy
{
  private static readonly int CallbackStartWaitTime = (int) TimeSpan.FromSeconds(20.0).TotalMilliseconds;
  private static readonly int CallbackFinishWaitTime = (int) TimeSpan.FromHours(1.0).TotalMilliseconds;
  private readonly IUIDispatcherService uiDispatcherService;
  private readonly object syncRoot;
  private volatile bool canAbortConnection;

  /// <summary>Создает объект.</summary>
  public IMServerInteractiveConnectionErrorStrategy(IUIDispatcherService uiDispatcherService)
  {
    this.uiDispatcherService = uiDispatcherService != null ? uiDispatcherService : throw new ArgumentNullException(nameof (uiDispatcherService));
    this.syncRoot = new object();
    this.canAbortConnection = true;
  }

  /// <summary>
  /// Возвращает или задает флаг, разрешающий пользователю при обрыве подключения к серверу приложений отказаться от восстановления подключения.
  /// По умолчанию значение свойства равно true.
  /// </summary>
  public bool CanAbortConnection
  {
    [DebuggerStepThrough] get
    {
      lock (this.syncRoot)
        return this.canAbortConnection;
    }
    [DebuggerStepThrough] set
    {
      lock (this.syncRoot)
        this.canAbortConnection = value;
    }
  }

  /// <summary>
  /// Обрабатывает ошибку подключения к серверу приложений, спрашивая у пользователя, что требуется делать дальше.
  /// </summary>
  /// <param name="exception">Исключение, возникшее при попытке подключения к серверу приложений</param>
  /// <returns>Способ переподключения к серверу приложений</returns>
  public override IMServerReconnectType HandleConnectionException(
    IMServerConnectionException exception)
  {
    if (exception == null)
      throw new ArgumentNullException(nameof (exception));
    if (!exception.TryLater)
      return IMServerReconnectType.AbortApplication;
    if (this.uiDispatcherService.IsUIThread())
      return this.AskUserOnUIThread(exception);
    IMServerInteractiveConnectionErrorStrategy.AskUserCallbackData state = new IMServerInteractiveConnectionErrorStrategy.AskUserCallbackData(exception);
    this.uiDispatcherService.PostToUIThread(new SendOrPostCallback(this.AskUserOnUIThread), (object) state);
    if (!state.StartedWaitEvent.Wait(IMServerInteractiveConnectionErrorStrategy.CallbackStartWaitTime))
      return IMServerReconnectType.AbortConnection;
    state.FinishedWaitEvent.Wait(IMServerInteractiveConnectionErrorStrategy.CallbackFinishWaitTime);
    return state.Result;
  }

  private void AskUserOnUIThread(object state)
  {
    IMServerInteractiveConnectionErrorStrategy.AskUserCallbackData userCallbackData = (IMServerInteractiveConnectionErrorStrategy.AskUserCallbackData) state;
    try
    {
      userCallbackData.StartedWaitEvent.Set();
      userCallbackData.Result = this.AskUserOnUIThread(userCallbackData.Exception);
      userCallbackData.FinishedWaitEvent.Set();
    }
    catch (Exception ex)
    {
      string currentMethodName = this.GetCurrentMethodName(nameof (AskUserOnUIThread));
      SuppressedExceptions.TraceException(ex, currentMethodName);
      userCallbackData.FinishedWaitEvent.Set();
    }
  }

  private IMServerReconnectType AskUserOnUIThread(IMServerConnectionException exception)
  {
    IMServerConnectionErrorInfo connectionErrorInfo = this.FormatConnectionException(exception);
    MessageBoxButtons buttons = this.canAbortConnection ? MessageBoxButtons.YesNoCancel : MessageBoxButtons.RetryCancel;
    switch (MessageBox.Show($"{connectionErrorInfo.ExceptionText}{Environment.NewLine}{Environment.NewLine}Попробовать еще раз?", LocalizationHolder.rm.GetString("Interfaces.Client_97"), buttons, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly))
    {
      case DialogResult.Cancel:
        return IMServerReconnectType.AbortApplicationSilently;
      case DialogResult.Retry:
      case DialogResult.Yes:
        return IMServerReconnectType.TryConnectAgain;
      default:
        return IMServerReconnectType.AbortConnection;
    }
  }

  private sealed class AskUserCallbackData
  {
    public AskUserCallbackData(IMServerConnectionException exception)
    {
      this.Exception = exception;
      this.Result = IMServerReconnectType.AbortConnection;
      this.StartedWaitEvent = new ManualResetEventSlim(false);
      this.FinishedWaitEvent = new ManualResetEventSlim(false);
    }

    public IMServerConnectionException Exception { get; }

    public IMServerReconnectType Result { get; set; }

    public ManualResetEventSlim StartedWaitEvent { get; }

    public ManualResetEventSlim FinishedWaitEvent { get; }
  }
}
