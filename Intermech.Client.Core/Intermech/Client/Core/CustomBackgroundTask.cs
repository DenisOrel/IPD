
// Type: Intermech.Client.Core.CustomBackgroundTask
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;


namespace Intermech.Client.Core;

/// <summary>Базовый класс, реализующий интерфейс IBackgroundTask</summary>
public class CustomBackgroundTask : IBackgroundTask
{
  /// <summary>Индекс изображения</summary>
  protected int _imageIndex = -1;
  /// <summary>Название фоновой задачи</summary>
  protected string _name;
  /// <summary>Минимально допустимое значение индикатора прогресса</summary>
  protected int _minValue;
  /// <summary>Максимально допустимое значение индикатора прогресса</summary>
  protected int _maxValue = 100;
  /// <summary>Текущее значение индикатора прогресса</summary>
  protected int _value;
  /// <summary>Результат выполнения фоновой задачи</summary>
  protected object _result;
  /// <summary>Можно ли останавливать фоновую задачу</summary>
  protected bool _canStop;
  /// <summary>Можно ли приостанавливать фоновую задачу</summary>
  protected bool _canPause;
  /// <summary>Можно ли возобновлять фоновую задачу</summary>
  protected bool _canResume;
  /// <summary>Можно ли останавливать фоновую задачу</summary>
  protected bool _canTerminate;
  /// <summary>Текущее состояние фоновой задачи</summary>
  protected BackgroundTaskState _state = BackgroundTaskState.Paused;

  /// <summary>
  /// Создать экземпляр класса. Имя задачи будет "Базовый класс"
  /// </summary>
  public CustomBackgroundTask() => this._name = LocalizationHolder.rm.GetString("Client.Core_1086");

  /// <summary>Сгенерировать событие OnChanged</summary>
  /// <param name="type">Какие изменения произошли в фоновой задаче</param>
  protected virtual void OnChanged(BackgroundTaskChangedType type)
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, type);
  }

  /// <summary>
  /// Событие, вызываемое при изменении состояния фоновой задачи
  /// </summary>
  public event BackgroundTaskChangedEventHandler Changed;

  /// <summary>Индекс изображения</summary>
  public virtual int ImageIndex => this._imageIndex;

  /// <summary>Название фоновой задачи</summary>
  public virtual string Name
  {
    get => this._name;
    set
    {
      if (!(this._name != value))
        return;
      this._name = value;
      this.OnChanged(BackgroundTaskChangedType.Text);
    }
  }

  /// <summary>Результат выполнения фоновой задачи</summary>
  public virtual object Result
  {
    get => this._result;
    set
    {
      if (this._result == value)
        return;
      this._result = value;
      this.OnChanged(BackgroundTaskChangedType.Result);
    }
  }

  /// <summary>Максимально допустимое значение индикатора прогресса</summary>
  public virtual int MaximumValue
  {
    get => this._maxValue;
    set
    {
      if (this._maxValue == value)
        return;
      this._maxValue = value;
      this.OnChanged(BackgroundTaskChangedType.Value);
    }
  }

  /// <summary>Минимально допустимое значение индикатора прогресса</summary>
  public virtual int MinimumValue
  {
    get => this._minValue;
    set
    {
      if (this._minValue == value)
        return;
      this._minValue = value;
      this.OnChanged(BackgroundTaskChangedType.Value);
    }
  }

  /// <summary>Текущее значение индикатора прогресса</summary>
  public virtual object Value
  {
    get => (object) this._value;
    set
    {
      if (!(value is int num))
        return;
      this._value = num;
      this.OnChanged(BackgroundTaskChangedType.Value);
    }
  }

  /// <summary>Текущее состояние фоновой задачи</summary>
  public virtual BackgroundTaskState State
  {
    get => this._state;
    set
    {
      if (this._state == value)
        return;
      this._state = value;
      this.OnChanged(BackgroundTaskChangedType.State);
    }
  }

  /// <summary>Режим отображения состояния фоновой задачи</summary>
  public virtual BackgroundTaskShowMode ShowMode => BackgroundTaskShowMode.Progress;

  /// <summary>Является ли активной указанная фоновая задача</summary>
  public virtual bool Active
  {
    get => this._state == BackgroundTaskState.Paused || this._state == BackgroundTaskState.Running;
  }

  /// <summary>
  /// Установить предельно допустимые значения для индикатора прогресса
  /// </summary>
  /// <param name="max">Максимально допустимое значение индикатора прогресса</param>
  /// <param name="min">Минимально допустимое значение индикатора прогресса</param>
  public void SetMaxMin(int max, int min)
  {
    this._maxValue = max >= min ? max : throw new ArgumentException("Max value bust be greater that Min.");
    this._minValue = min;
    this.OnChanged(BackgroundTaskChangedType.Value);
  }

  /// <summary>Можно ли останавливать фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно останавливать</returns>
  public virtual bool CanStop() => this._canStop;

  /// <summary>Можно ли приостанавливать фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно приостанавливать</returns>
  public virtual bool CanPause() => this._canPause;

  /// <summary>Можно ли возобновлять фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно возобновлять</returns>
  public virtual bool CanResume() => this._canResume;

  /// <summary>Можно ли прерывать фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно прерывать</returns>
  public virtual bool CanTerminate() => this._canTerminate;

  /// <summary>Остановить фоновую задачу</summary>
  public virtual void Stop()
  {
    if (!this._canStop)
      return;
    this.State = BackgroundTaskState.Stopped;
  }

  /// <summary>Приостановить фоновую задачу</summary>
  public virtual void Pause()
  {
    if (!this._canPause)
      return;
    this.State = BackgroundTaskState.Paused;
  }

  /// <summary>Возобновить фоновую задачу</summary>
  public virtual void Resume()
  {
    if (!this._canResume)
      return;
    this.State = BackgroundTaskState.Running;
  }

  /// <summary>Прервать фоновую задачу</summary>
  public virtual void Terminate()
  {
  }
}
