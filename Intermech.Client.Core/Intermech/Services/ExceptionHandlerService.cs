
// Type: Intermech.Services.ExceptionHandlerService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Pools;
using Intermech.Runtime;
using Intermech.Text;
using Intermech.UI;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Services;

/// <summary>
/// Реализация сервиса для обработки исключительных стиуаций с возможностью подключения пользовательских обработчиков.
/// </summary>
public sealed class ExceptionHandlerService : IExceptionDisplayService, IExceptionHandlerService
{
  private IUIDispatcherService _uiDispatcher;
  private Func<Exception, DialogResult> _showExceptionAction;
  private object _syncRoot;
  private IOptionalService<IApplicationEventLogService> _eventLogService;
  private IOptionalService<ISplashService> _splashService;
  private IOptionalService<IUINotificationService> _uiNotificationService;

  public ExceptionHandlerService(
    IUIDispatcherService uiDispatcher,
    Func<Exception, DialogResult> showExceptionAction)
  {
    if (uiDispatcher == null)
      throw new ArgumentNullException(nameof (uiDispatcher));
    if (showExceptionAction == null)
      throw new ArgumentNullException(nameof (showExceptionAction));
    this._uiDispatcher = uiDispatcher;
    this._showExceptionAction = showExceptionAction;
    this._syncRoot = new object();
  }

  /// <summary>
  /// Возвращает или задает экземпляр сервиса IApplicationEventLogService, который используется для протоколирования критических ошибок.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IOptionalService<IApplicationEventLogService> EventLogService
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._eventLogService;
    }
    [DebuggerStepThrough] set
    {
      lock (this._syncRoot)
        this._eventLogService = value;
    }
  }

  private IApplicationEventLogService TryGetEventLogService()
  {
    IApplicationEventLogService eventLogService = (IApplicationEventLogService) null;
    lock (this._syncRoot)
    {
      if (this._splashService != null)
        eventLogService = this._eventLogService.TryGet();
    }
    return eventLogService;
  }

  /// <summary>
  /// Возвращает или задает экземпляр сервиса ISplashService.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IOptionalService<ISplashService> SplashService
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._splashService;
    }
    [DebuggerStepThrough] set
    {
      lock (this._syncRoot)
        this._splashService = value;
    }
  }

  private ISplashService TryGetSplashService()
  {
    ISplashService splashService = (ISplashService) null;
    lock (this._syncRoot)
    {
      if (this._splashService != null)
        splashService = this._splashService.TryGet();
    }
    return splashService;
  }

  /// <summary>
  /// Возвращает или задает экземпляр сервиса IUINotificationService, который будет использоваться для обработки исключительных ситуаций из фонового потока.
  /// Значение свойства может быть не задано, в этом случае исключения из фонового потока будут сразу перенаправляться на обработку в UI-поток приложения.
  /// </summary>
  public IOptionalService<IUINotificationService> UINotificationService
  {
    [DebuggerStepThrough] get
    {
      lock (this._syncRoot)
        return this._uiNotificationService;
    }
    [DebuggerStepThrough] set
    {
      lock (this._syncRoot)
        this._uiNotificationService = value;
    }
  }

  private IUINotificationService TryGetUINotificationService()
  {
    IUINotificationService notificationService = (IUINotificationService) null;
    lock (this._syncRoot)
    {
      if (this._uiNotificationService != null)
        notificationService = this._uiNotificationService.TryGet();
    }
    return notificationService;
  }

  private void ShowExceptionInternal(Exception exception)
  {
    if (Environment.HasShutdownStarted || exception is AbortException)
      return;
    if (this._uiDispatcher.IsUIThread())
    {
      this.ShowExceptionDialogOnUIThread(exception);
    }
    else
    {
      exception.SetOriginalStackTrace(ExceptionServices.GetExtendedStackTrace(exception));
      IUINotificationService notificationService = this.TryGetUINotificationService();
      if (notificationService == null)
      {
        this._uiDispatcher.PostToUIThread((SendOrPostCallback) (arg => this.ShowExceptionInternal(exception)), (object) null);
      }
      else
      {
        UINotificationBuilder notificationBuilder = new UINotificationBuilder();
        notificationBuilder.FillFromException(exception);
        notificationBuilder.Caption = "Необработанное исключение в фоновом потоке";
        notificationService.ShowNotification(notificationBuilder.Build());
      }
    }
  }

  private void ShowExceptionDialogOnUIThread(Exception exception)
  {
    ISplashService splashService = this.TryGetSplashService();
    try
    {
      splashService?.HideSplash();
      if (this.InvokeCustomExceptionHandler(exception))
        return;
      if (exception is ISimpleMessageException)
      {
        int num = (int) this.ShowSimpleExceptionDialog(exception);
      }
      else
      {
        if (this.ShowDefaultExceptionDialog(exception) != DialogResult.Abort)
          return;
        Application.Exit();
      }
    }
    finally
    {
      splashService?.ShowSplash();
    }
  }

  private DialogResult ShowSimpleExceptionDialog(Exception exception)
  {
    return MessageBox.Show(exception.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  private DialogResult ShowDefaultExceptionDialog(Exception exception)
  {
    try
    {
      return this._showExceptionAction(exception);
    }
    catch (Exception ex)
    {
      this.ProcessDefaultExceptionDialogError(ex, exception);
      return DialogResult.Abort;
    }
  }

  private void ProcessDefaultExceptionDialogError(
    Exception dialogException,
    Exception originalException)
  {
    string str;
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      stringBuilder.AppendLine("Критическая ошибка отображения необработанного исключения!");
      stringBuilder.AppendLine(dialogException.Message);
      stringBuilder.AppendLine(dialogException.StackTrace);
      stringBuilder.AppendLine("-----------");
      stringBuilder.AppendLine("Исходное исключение:");
      stringBuilder.AppendLine(originalException.Message);
      stringBuilder.AppendLine(originalException.StackTrace);
      str = stringBuilder.ToString();
    }
    try
    {
      int num = (int) MessageBox.Show(str, "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    catch
    {
    }
    IApplicationEventLogService eventLogService = this.TryGetEventLogService();
    if (eventLogService == null)
      return;
    try
    {
      eventLogService.AllLogs.Write(str, EventLogItemType.Error);
    }
    catch
    {
    }
  }

  private bool InvokeCustomExceptionHandler(Exception exception)
  {
    ExceptionHandler handleException = this.HandleException;
    if (handleException != null)
    {
      Delegate[] invocationList = handleException.GetInvocationList();
      int length = invocationList.Length;
      ExceptionEventArgs handerArgs = new ExceptionEventArgs(exception);
      for (int index = 0; index < length; ++index)
      {
        ExceptionHandler handler = invocationList[index] as ExceptionHandler;
        if (handler != null)
        {
          SilentActionInvoker.Default.Invoke((Action) (() => handler((object) this, handerArgs)));
          if (handerArgs.Handled)
            return true;
        }
      }
    }
    return false;
  }

  /// <summary>
  /// Вызывается при возникновении в системе необработанного исключения.
  /// Обработчики вызываются в порядке регистрации до тех пор, пока кто-то из них не установит свойство Handled в true.
  /// </summary>
  public event ExceptionHandler HandleException;

  public void ShowException(Exception exception)
  {
    if (exception == null)
      throw new ArgumentNullException(nameof (exception));
    this.ShowExceptionInternal(exception);
  }

  [Obsolete("Do not use this method anymore.", true)]
  public void NotifyException(Exception exception) => throw new NotSupportedException();

  [Obsolete("Do not use this method anymore.", true)]
  public void NotifyException(Exception exception, int timeout)
  {
    throw new NotSupportedException();
  }
}
