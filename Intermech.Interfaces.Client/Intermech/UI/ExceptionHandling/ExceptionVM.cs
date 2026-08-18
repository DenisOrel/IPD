// Decompiled with JetBrains decompiler
// Type: Intermech.UI.ExceptionHandling.ExceptionVM
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.UI.ExceptionHandling;

/// <summary>
/// Модель вида, предназначенная для отображения информации о возникшей исключительной ситуации (Exception)
/// </summary>
public sealed class ExceptionVM : ViewModel, ICloseableViewModel, INotifyPropertyChanged
{
  private double fontSize;
  private Exception exception;
  private DateTime dateTime;
  private string message;
  private string technicalInfo;
  private ExceptionSaveHandler saveHandler;
  private ErrorRecoveryHandler recoveryHandler;
  private bool isClosed;
  private bool isAborted;
  private bool showAbortCommand;
  private string closeCommandName;
  private bool closeCommandIsAbort;
  private PluggableCommand saveToFileCommand;
  private PluggableCommand emailReportCommand;
  private PluggableCommand abortCommand;
  private PluggableCommand closeCommand;
  private EventHandler saveToFileEventHandler;
  private EventHandler emailReportEventHandler;

  /// <summary>Создает объект</summary>
  public ExceptionVM()
  {
    this.fontSize = 11.0;
    this.showAbortCommand = false;
    this.closeCommandName = "Пропустить";
    this.closeCommandIsAbort = false;
    this.dateTime = DateTime.Now;
    this.message = string.Empty;
    this.technicalInfo = string.Empty;
    this.saveToFileCommand = new PluggableCommand();
    this.emailReportCommand = new PluggableCommand();
    this.abortCommand = new PluggableCommand(new Action(this.Abort));
    this.abortCommand.Enabled = this.showAbortCommand;
    this.closeCommand = new PluggableCommand(new Action(this.Close));
    this.saveHandler = new ExceptionSaveHandler();
  }

  /// <summary>
  /// Возвращает и задает базовый размер шрифта для текстовых элементов
  /// Значение по умолчанию 11 соответствует размеру шрифта по умолчанию
  /// (8,25 pt в GDI+)
  /// </summary>
  public double FontSize
  {
    [DebuggerStepThrough] get => this.fontSize;
    set
    {
      if (value <= 0.0)
        throw new ArgumentOutOfRangeException(nameof (value));
      if (this.fontSize == value)
        return;
      this.fontSize = value;
      this.RaisePropertyChanged(nameof (FontSize));
    }
  }

  /// <summary>
  /// Возвращает или задает признак, что в интерфейсе пользователя должна быть доступной
  /// команда экстренного завершения приложения.
  /// </summary>
  public bool ShowAbortCommand
  {
    [DebuggerStepThrough] get => this.showAbortCommand;
    [DebuggerStepThrough] set
    {
      if (this.showAbortCommand == value)
        return;
      this.showAbortCommand = value;
      this.RaisePropertyChanged(nameof (ShowAbortCommand));
      this.abortCommand.Enabled = value;
    }
  }

  /// <summary>
  /// Возвращает отображаемое имя закрытия окна отображения исключения
  /// </summary>
  public string CloseCommandName
  {
    [DebuggerStepThrough] get => this.closeCommandName;
  }

  /// <summary>
  /// Возвращает режим поведения команды закрытия окна отображения исключения.
  /// Если значение равно true, то команда закрывает и окно, и приложение.
  /// Если false - команда закрывает окно, а приложение продолжает работать
  /// </summary>
  public bool CloseCommandIsAbort
  {
    [DebuggerStepThrough] get => this.closeCommandIsAbort;
  }

  /// <summary>Возвращает или задает отображаемый объект исключения.</summary>
  public Exception Exception
  {
    [DebuggerStepThrough] get => this.exception;
    [DebuggerStepThrough] set
    {
      if (this.exception == value)
        return;
      this.exception = value;
      this.RaisePropertyChanged(nameof (Exception));
    }
  }

  /// <summary>Возвращает или задает дату падения исключения.</summary>
  public DateTime DateTime
  {
    [DebuggerStepThrough] get => this.dateTime;
    [DebuggerStepThrough] set
    {
      if (!(this.dateTime != value))
        return;
      this.dateTime = value;
      this.RaisePropertyChanged(nameof (DateTime));
    }
  }

  /// <summary>Возвращает или задает текст исключения.</summary>
  public string Message
  {
    [DebuggerStepThrough] get => this.message;
    [DebuggerStepThrough] set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!(this.message != value))
        return;
      this.message = value;
      this.RaisePropertyChanged(nameof (Message));
    }
  }

  /// <summary>
  /// Возвращает или задает текст с техническими сведениями об исключении (stack trace и др.)
  /// </summary>
  public string TechnicalInfo
  {
    [DebuggerStepThrough] get => this.technicalInfo;
    [DebuggerStepThrough] set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!(this.technicalInfo != value))
        return;
      this.technicalInfo = value;
      this.RaisePropertyChanged(nameof (TechnicalInfo));
      this.RaisePropertyChanged("HasTechnicalInfo");
    }
  }

  /// <summary>
  /// Возвращает признак, что у исключения есть технические сведения для отображения
  /// </summary>
  public bool HasTechnicalInfo
  {
    [DebuggerStepThrough] get => this.technicalInfo != string.Empty;
  }

  /// <summary>
  /// Возвращает или задает обработчик для записи отчета и для отправки отчета по email.
  /// </summary>
  public ExceptionSaveHandler SaveHandler
  {
    [DebuggerStepThrough] get => this.saveHandler;
    [DebuggerStepThrough] set
    {
      if (this.saveHandler == value)
        return;
      this.saveHandler = value;
      this.RaisePropertyChanged(nameof (SaveHandler));
      bool flag = this.saveHandler != null;
      this.saveToFileCommand.Enabled = flag;
      this.emailReportCommand.Enabled = flag;
    }
  }

  /// <summary>
  /// Возвращает или задает обработчик для действий восстановления после исключения.
  /// </summary>
  public ErrorRecoveryHandler RecoveryHandler
  {
    [DebuggerStepThrough] get => this.recoveryHandler;
    [DebuggerStepThrough] set
    {
      if (this.recoveryHandler == value)
        return;
      this.recoveryHandler = value;
      this.RaisePropertyChanged(nameof (RecoveryHandler));
    }
  }

  /// <summary>
  /// Возвращает или задает признак, что модель вида закрыта.
  /// Изменение этого свойства в true приведет к закрытию окна.
  /// </summary>
  public bool IsClosed
  {
    [DebuggerStepThrough] get => this.isClosed;
    [DebuggerStepThrough] set
    {
      if (this.isClosed == value)
        return;
      this.isClosed = value;
      this.RaisePropertyChanged(nameof (IsClosed));
      if (value)
        return;
      this.IsAborted = false;
    }
  }

  /// <summary>
  /// Возвращает или задает признак, что была выполнена команда экстренного завершения приложения.
  /// </summary>
  public bool IsAborted
  {
    [DebuggerStepThrough] get => this.isAborted;
    [DebuggerStepThrough] private set
    {
      if (this.isAborted == value)
        return;
      this.isAborted = value;
      this.RaisePropertyChanged(nameof (IsAborted));
    }
  }

  /// <summary>Возвращает объект команды для записи отчета в файл.</summary>
  public PluggableCommand SaveToFileCommand
  {
    [DebuggerStepThrough] get => this.saveToFileCommand;
  }

  /// <summary>
  /// Возвращает объект команды для отправки отчета по email.
  /// </summary>
  public PluggableCommand EmailReportCommand
  {
    [DebuggerStepThrough] get => this.emailReportCommand;
  }

  /// <summary>
  /// Возвращает объект команды для закрытия окна и приложения.
  /// </summary>
  public PluggableCommand AbortCommand
  {
    [DebuggerStepThrough] get => this.abortCommand;
  }

  /// <summary>Возвращает объект команды для закрытия только окна.</summary>
  public PluggableCommand CloseCommand
  {
    [DebuggerStepThrough] get => this.closeCommand;
  }

  /// <summary>
  /// Возвращает или задает обработчик команды "Записать в файл"
  /// </summary>
  public event EventHandler SaveToFile
  {
    add
    {
      this.saveToFileEventHandler += value;
      this.UpdateSaveToFileCommandHandler();
    }
    remove
    {
      this.saveToFileEventHandler -= value;
      this.UpdateSaveToFileCommandHandler();
    }
  }

  private void UpdateSaveToFileCommandHandler()
  {
    this.saveToFileCommand.Handler = this.saveToFileEventHandler != null ? new Action(this.RaiseSaveToFileEventHandler) : (Action) null;
  }

  private void RaiseSaveToFileEventHandler()
  {
    EventHandler fileEventHandler = this.saveToFileEventHandler;
    if (fileEventHandler == null)
      return;
    fileEventHandler((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// Возвращает или задает обработчик команды "Отправить отчет"
  /// </summary>
  public event EventHandler EmailReport
  {
    add
    {
      this.emailReportEventHandler += value;
      this.UpdateEmailReportCommandHandler();
    }
    remove
    {
      this.emailReportEventHandler -= value;
      this.UpdateEmailReportCommandHandler();
    }
  }

  private void UpdateEmailReportCommandHandler()
  {
    this.emailReportCommand.Handler = this.emailReportEventHandler != null ? new Action(this.RaiseEmailReportEventHandler) : (Action) null;
  }

  private void RaiseEmailReportEventHandler()
  {
    EventHandler reportEventHandler = this.emailReportEventHandler;
    if (reportEventHandler == null)
      return;
    reportEventHandler((object) this, EventArgs.Empty);
  }

  /// <summary>Закрывает окно и приложение.</summary>
  public void Abort() => this.CloseInternal(true);

  /// <summary>Закрывает только окно.</summary>
  public void Close() => this.CloseInternal(this.closeCommandIsAbort);

  private void CloseInternal(bool abortMode)
  {
    if (this.IsClosed)
      return;
    this.IsAborted = abortMode;
    this.IsClosed = true;
  }

  /// <summary>
  /// Конфигурирует команду закрытия окна как действие "Пропустить".
  /// </summary>
  public void ConfigureCloseCommandAsIgnoreAction()
  {
    this.closeCommandName = "Пропустить";
    this.closeCommandIsAbort = false;
    this.RaisePropertyChanged("CloseCommandName");
    this.RaisePropertyChanged("CloseCommandIsAbort");
  }

  /// <summary>
  /// Конфигурирует команду закрытия окна как действие "Прервать".
  /// </summary>
  public void ConfigureCloseCommandAsAbortAction()
  {
    this.closeCommandName = "Прервать";
    this.closeCommandIsAbort = true;
    this.RaisePropertyChanged("CloseCommandName");
    this.RaisePropertyChanged("CloseCommandIsAbort");
  }
}
