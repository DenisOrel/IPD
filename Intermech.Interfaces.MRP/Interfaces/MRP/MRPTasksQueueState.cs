// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPTasksQueueState
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Класс, позволяющий получить состояние выполняющейся очереди заданий
/// </summary>
[Serializable]
public sealed class MRPTasksQueueState
{
  /// <summary>Уничтожены ли ресурсы очереди заданий</summary>
  public volatile bool IsDisposed;
  /// <summary>
  /// Самозавершающаяся очередь задач (по умолчанию очередь полностью освободит все ресурсы, как только будет выполнено последнее задание)
  /// </summary>
  public volatile bool AutoComplete;
  /// <summary>Количество заданий, ожидающих выполнения в очереди</summary>
  public volatile int InQueue;
  /// <summary>
  /// Количество заданий, выполняющихся в текущий момент времени
  /// </summary>
  public volatile int InProcess;
  /// <summary>
  /// Количество заданий, успешно выполненных очередью заданий
  /// </summary>
  public volatile int ProcessedTasks;
  /// <summary>Количество заданий, которые были прерваны</summary>
  public volatile int CancelledTasks;
  /// <summary>
  /// Количество заданий, которые были проигнорированы из-за исключений, возникших в связанных с ними заданиями
  /// </summary>
  public volatile int SkippedTasks;
  /// <summary>
  /// Суммарное количество заданий, которое было получено очередью
  /// </summary>
  public volatile int TotalTasks;
  /// <summary>
  /// Суммарное количество вложенных заданий, которое было получено очередью
  /// </summary>
  public volatile int NestedTasks;
  /// <summary>
  /// Установка значения в True обязывает все фоновые задачи прервать свои действия и завершиться.
  /// </summary>
  public volatile bool IsBreaked;
  /// <summary>Guid очереди заданий</summary>
  public Guid QueueGuid = Guid.Empty;
  /// <summary>Guid сессии, в рамках которой выполняется задание.</summary>
  public Guid SessionGuid = Guid.Empty;
  /// <summary>Текущая выполняемая задача</summary>
  public volatile string TaskOperation = string.Empty;
  /// <summary>Минимальное значение для прогресс-бара</summary>
  public volatile int MinProgress;
  /// <summary>Максимальное значение для прогресс-бара</summary>
  public volatile int MaxProgress = 100;
  /// <summary>Текущее значение для прогресс-бара</summary>
  public volatile int Progress;
  /// <summary>
  /// Возникшая исключительная ситуация, если задания были прерваны из-за неё
  /// </summary>
  public volatile Exception Exception;
  /// <summary>
  /// Контейнер с информацией для уведомления Навигатора о произошедших изменениях
  /// </summary>
  public volatile MRPNavigatorEventsRef NavigatorEvents;

  /// <summary>
  /// Создать класс, позволяющий получить состояние выполняющейся очереди заданий
  /// </summary>
  /// <param name="IsDisposed">Уничтожены ли ресурсы очереди заданий</param>
  /// <param name="AutoComplete">Самозавершающаяся очередь задач (по умолчанию очередь полностью освободит все ресурсы, как только будет выполнено последнее задание)</param>
  /// <param name="InQueue">Количество заданий, ожидающих выполнения в очереди</param>
  /// <param name="InProcess">Количество заданий, выполняющихся в текущий момент времени</param>
  /// <param name="ProcessedTasks">Количество заданий, успешно выполненных очередью заданий</param>
  /// <param name="CancelledTasks">Количество заданий, которые были прерваны</param>
  /// <param name="SkippedTasks">Количество заданий, которые были проигнорированы из-за исключений, возникших в связанных с ними заданиями</param>
  /// <param name="TotalTasks">Суммарное количество заданий, которое было получено очередью</param>
  /// <param name="NestedTasks">Суммарное количество вложенных заданий, которое было получено очередью</param>
  /// <param name="IsBreaked">Установка значения в True обязывает все фоновые задачи прервать свои действия и завершиться.</param>
  /// <param name="QueueGuid">Guid очереди заданий</param>
  /// <param name="SessionGuid">Guid сессии, в рамках которой выполняется задание</param>
  /// <param name="TaskOperation">Текущая выполняемая задача</param>
  /// <param name="MinProgress">Минимальное значение для прогресс-бара</param>
  /// <param name="MaxProgress">Максимальное значение для прогресс-бара</param>
  /// <param name="Progress">Максимальное значение для прогресс-бара</param>
  /// <param name="Exception">Возникшая исключительная ситуация, если задания были прерваны из-за неё</param>
  /// <param name="NavigatorEvents">Контейнер с информацией для уведомления Навигатора о произошедших изменениях</param>
  public MRPTasksQueueState(
    bool IsDisposed,
    bool AutoComplete,
    int InQueue,
    int InProcess,
    int ProcessedTasks,
    int CancelledTasks,
    int SkippedTasks,
    int TotalTasks,
    int NestedTasks,
    bool IsBreaked,
    Guid QueueGuid,
    Guid SessionGuid,
    string TaskOperation,
    int MinProgress,
    int MaxProgress,
    int Progress,
    Exception Exception,
    MRPNavigatorEventsRef NavigatorEvents)
  {
    this.IsDisposed = IsDisposed;
    this.AutoComplete = AutoComplete;
    this.InQueue = InQueue;
    this.InProcess = InProcess;
    this.ProcessedTasks = ProcessedTasks;
    this.CancelledTasks = CancelledTasks;
    this.SkippedTasks = SkippedTasks;
    this.TotalTasks = TotalTasks;
    this.NestedTasks = NestedTasks;
    this.IsBreaked = IsBreaked;
    this.QueueGuid = QueueGuid;
    this.SessionGuid = SessionGuid;
    this.TaskOperation = TaskOperation;
    this.MinProgress = MinProgress;
    this.MaxProgress = MaxProgress;
    this.Progress = Progress;
    this.Exception = Exception;
    this.NavigatorEvents = NavigatorEvents;
  }
}
