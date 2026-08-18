
// Type: Intermech.Navigator.Controls.JobState
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>Описывает текущее состояние задания.</summary>
internal enum JobState
{
  /// <summary>Задание ожидает своей очереди на выполнение.</summary>
  Waiting,
  /// <summary>Задание выполняется.</summary>
  Running,
  /// <summary>Выполнение задания отменено.</summary>
  Cancelled,
  /// <summary>Выполнение задания завершено без ошибок.</summary>
  Complete,
  /// <summary>
  /// Выполнение задания завершено, но в процессе выполнения возникла
  /// исключительная ситуация.
  /// </summary>
  Failed,
}
