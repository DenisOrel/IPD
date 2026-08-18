// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IBackgroundTask
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс фоновой задачи для представления в окне фоновых задач IBackgroundTaskView
/// </summary>
public interface IBackgroundTask
{
  /// <summary>
  /// Событие, вызываемое при изменении состояния фоновой задачи
  /// </summary>
  event BackgroundTaskChangedEventHandler Changed;

  /// <summary>Индекс изображения</summary>
  int ImageIndex { get; }

  /// <summary>Название фоновой задачи</summary>
  string Name { get; }

  /// <summary>Максимально допустимое значение индикатора прогресса</summary>
  int MaximumValue { get; set; }

  /// <summary>Минимально допустимое значение индикатора прогресса</summary>
  int MinimumValue { get; set; }

  /// <summary>Текущее значение индикатора прогресса</summary>
  object Value { get; set; }

  /// <summary>Результат выполнения фоновой задачи</summary>
  object Result { get; set; }

  /// <summary>Текущее состояние фоновой задачи</summary>
  BackgroundTaskState State { get; set; }

  /// <summary>Режим отображения состояния фоновой задачи</summary>
  BackgroundTaskShowMode ShowMode { get; }

  /// <summary>Является ли активной указанная фоновая задача</summary>
  bool Active { get; }

  /// <summary>
  /// Установить предельно допустимые значения для индикатора прогресса
  /// </summary>
  /// <param name="max">Максимально допустимое значение индикатора прогресса</param>
  /// <param name="min">Минимально допустимое значение индикатора прогресса</param>
  void SetMaxMin(int max, int min);

  /// <summary>Можно ли останавливать фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно останавливать</returns>
  bool CanStop();

  /// <summary>Можно ли приостанавливать фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно приостанавливать</returns>
  bool CanPause();

  /// <summary>Можно ли возобновлять фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно возобновлять</returns>
  bool CanResume();

  /// <summary>Можно ли прерывать фоновую задачу</summary>
  /// <returns>true - фоновую задачу можно прерывать</returns>
  bool CanTerminate();

  /// <summary>Остановить фоновую задачу</summary>
  void Stop();

  /// <summary>Приостановить фоновую задачу</summary>
  void Pause();

  /// <summary>Возобновить фоновую задачу</summary>
  void Resume();

  /// <summary>Прервать фоновую задачу</summary>
  void Terminate();
}
