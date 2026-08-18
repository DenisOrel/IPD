
// Type: Intermech.Navigator.Controls.IJobManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Позволяет реализовать менеджер, распределяющий задания по
/// фоновым потокам.
/// </summary>
internal interface IJobManager
{
  /// <summary>Ставит новое задание в очередь на выполнение.</summary>
  /// <param name="job">Задание, которое должно быть выполнено в фоновом потоке.</param>
  /// <param name="marker">Неуникальная метка, присваиваемая заданию.</param>
  void Queue(IJob job, object marker);

  /// <summary>
  /// Отменяет выполнение всех заданий, чьи метки совпадают с указанной.
  /// </summary>
  /// <param name="marker">Метка заданий, выполнение которых должно быть отменено.</param>
  void Cancel(object marker);

  /// <summary>Отменяет выполнение всех заданий.</summary>
  void Cancel();

  /// <summary>
  /// Событие, наступающее при завершении каждого фонового задания.
  /// Срабатывает в контексте фонового потока, в котором выполнялось
  /// задание.
  /// </summary>
  event JobCompleteEventHandler Complete;
}
