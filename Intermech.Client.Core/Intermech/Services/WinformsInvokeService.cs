
// Type: Intermech.Services.WinformsInvokeService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Services;

/// <summary>
/// Реализует сервис, позволяющего выполнить произвольный метод на основном потоке приложения, используя для
/// переключения потоков механизм Control.Invoke.
/// </summary>
public sealed class WinformsInvokeService : InvokeService
{
  private Form mainForm;

  /// <summary>Создает сервис.</summary>
  /// <param name="mainForm">Основная форма приложения</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на основную форму приложения не может быть null</exception>
  public WinformsInvokeService(Form mainForm)
  {
    this.mainForm = mainForm != null ? mainForm : throw new ArgumentNullException(nameof (mainForm));
  }

  /// <summary>
  /// Проверяет, возможен ли прямой вызов указанного метода без переключения потоков. Этот метод позволяет
  /// выявить случаи, когда обращение к сервису осуществляется из основного потока приложения.
  /// </summary>
  /// <param name="method">Делегат выполняемого метода</param>
  /// <param name="args">Аргументы вызова метода</param>
  /// <returns>true, если обращение к сервису выполняется из основного потока приложения и переключение потоков не требуется, false - если должен использоваться непрямой вызов с переключением потоков</returns>
  protected override bool IsDirectInvokeAllowed(Delegate method) => !this.mainForm.InvokeRequired;

  /// <summary>
  /// Реализует непрямой вызов метода с переключением потоков.
  /// </summary>
  /// <param name="timeout">Таймаут, в течение которого следует ожидать готовности основного потока приложения</param>
  /// <param name="method">Делегат выполняемого метода</param>
  /// <param name="args">Аргументы вызова метода</param>
  /// <returns>Результат выполнения метода</returns>
  protected override T DoIndirectInvoke<T>(int timeout, Func<T> method)
  {
    WinformsInvokeService.WinformsInvokeHelperArgs<T> helperArgs = new WinformsInvokeService.WinformsInvokeHelperArgs<T>(method);
    IAsyncResult asyncResult = this.mainForm.BeginInvoke((Delegate) (() => this.WinformIndirectInvoke<T>(helperArgs)));
    asyncResult.AsyncWaitHandle.WaitOne(timeout);
    if (helperArgs.TrySetCancelledFlag())
      throw new TimeoutException(LocalizationHolder.rm.GetString("Client.Core_1587"));
    asyncResult.AsyncWaitHandle.WaitOne();
    if (helperArgs.Exception != null)
      helperArgs.Exception.Throw();
    return helperArgs.ReturnValue;
  }

  private void WinformIndirectInvoke<T>(
    WinformsInvokeService.WinformsInvokeHelperArgs<T> helperArgs)
  {
    if (helperArgs.TrySetStartedFlag())
    {
      try
      {
        T result = helperArgs.Method();
        helperArgs.SetReturnValue(result);
      }
      catch (Exception ex)
      {
        helperArgs.SetException(ex);
      }
    }
    else
      helperArgs.SetException((Exception) new TimeoutException(LocalizationHolder.rm.GetString("Client.Core_1588")));
  }

  private sealed class WinformsInvokeHelperArgs<T>
  {
    private const int METHOD_NOT_STARTED = 0;
    private const int METHOD_STARTED = 1;
    private const int METHOD_CANCELLED = 2;
    private int methodState;

    public WinformsInvokeHelperArgs(Func<T> method) => this.Method = method;

    public Func<T> Method { get; private set; }

    public T ReturnValue { get; private set; }

    public ExceptionDispatchInfo Exception { get; private set; }

    public bool TrySetStartedFlag() => Interlocked.CompareExchange(ref this.methodState, 1, 0) == 0;

    public bool TrySetCancelledFlag()
    {
      return Interlocked.CompareExchange(ref this.methodState, 2, 0) == 0;
    }

    public void SetReturnValue(T result)
    {
      Thread.MemoryBarrier();
      this.ReturnValue = result;
    }

    public void SetException(Exception exception)
    {
      Thread.MemoryBarrier();
      this.Exception = ExceptionDispatchInfo.Capture(exception);
    }
  }
}
