
// Type: Intermech.UI.Wpf.DelayedUserInputHandler
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Windows.Threading;


namespace Intermech.UI.Wpf;

/// <summary>
/// Обработчик пользовательского ввода, который позволяет выполнять обработку в паузах между активностью пользователя.
/// </summary>
/// <remarks>
/// Предназначен для использования только в UI-потоке. Реализация не является thread safe.
/// </remarks>
public sealed class DelayedUserInputHandler
{
  private DispatcherTimer timer;
  private TimeSpan reactionInterval;
  private bool isStarted;
  private DateTime userInputLastTimeMark;

  /// <summary>Создает объект.</summary>
  /// <param name="checkInterval">Интервал времени, через который следует проверять активность пользователя. Значение должно быть небольшим</param>
  /// <param name="reactionInterval">Интервал времени с момента последнего пользовательского ввода, когда должен сработать обработчик. Значение должно быть быть больше <paramref name="checkInterval" /></param>
  public DelayedUserInputHandler(TimeSpan checkInterval, TimeSpan reactionInterval)
  {
    if (checkInterval <= TimeSpan.Zero)
      throw new ArgumentOutOfRangeException(nameof (checkInterval));
    this.reactionInterval = !(reactionInterval < checkInterval) ? reactionInterval : throw new ArgumentOutOfRangeException(nameof (reactionInterval));
    this.timer = new DispatcherTimer();
    this.timer.Interval = checkInterval;
    this.timer.Tick += new EventHandler(this.OnTimerTick);
    this.timer.IsEnabled = false;
  }

  /// <summary>Возвращает признак, что обработчик был запущен.</summary>
  public bool IsStarted => this.isStarted;

  /// <summary>Запускает обработчик.</summary>
  /// <exception cref="T:System.InvalidOperationException">Обработчик уже был запущен ранее</exception>
  public void Start()
  {
    this.CheckIfNotStarted();
    this.isStarted = true;
  }

  /// <summary>
  /// Останавливает обработчик, если он был запущен ранее, и освобождает все его ресурсы.
  /// </summary>
  public void Stop()
  {
    if (!this.isStarted)
      return;
    this.CancelUserInput();
    this.isStarted = false;
  }

  /// <summary>
  /// Регистрирует событие пользовательского ввода и активирует процесс слежения за пользовательской активностью.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Обработчик не был запущен с помощью метода <see cref="M:Intermech.UI.Wpf.DelayedUserInputHandler.Start" /></exception>
  public void RegisterUserInput()
  {
    this.CheckIfStarted();
    this.userInputLastTimeMark = DateTime.UtcNow;
    if (this.timer.IsEnabled)
      return;
    this.timer.IsEnabled = true;
  }

  /// <summary>
  /// Отменяет запущенный ранее процесс слежения за пользовательской активностью.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Обработчик не был запущен с помощью метода <see cref="M:Intermech.UI.Wpf.DelayedUserInputHandler.Start" /></exception>
  public void CancelUserInput()
  {
    this.CheckIfStarted();
    if (!this.timer.IsEnabled)
      return;
    this.timer.IsEnabled = false;
  }

  /// <summary>
  /// Событие отложенной обработки пользовательского ввода.
  /// Он срабатывает, если с момента последнего события ввода прошло достаточно времени, а пользователь не активен.
  /// </summary>
  public event EventHandler ProcessUserInput;

  private void CheckIfNotStarted()
  {
    if (this.isStarted)
      throw new InvalidOperationException($"The component {this.GetType().Name} must not be started.");
  }

  private void CheckIfStarted()
  {
    if (!this.isStarted)
      throw new InvalidOperationException($"The component {this.GetType().Name} must be started first.");
  }

  private void OnTimerTick(object sender, EventArgs e)
  {
    if (this.isStarted && !this.timer.IsEnabled)
      return;
    this.timer.IsEnabled = false;
    if (DateTime.UtcNow > this.userInputLastTimeMark + this.reactionInterval)
    {
      EventHandler processUserInput = this.ProcessUserInput;
      if (processUserInput == null)
        return;
      processUserInput((object) this, EventArgs.Empty);
    }
    else
      this.timer.IsEnabled = true;
  }
}
