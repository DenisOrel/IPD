
// Type: Intermech.Interfaces.WebPortal.TaskStatus
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Статус задачи</summary>
    public enum TaskStatus
    {
      /// <summary>Успешно выполнена</summary>
      [StatusInWork(false)] Successfully,
      /// <summary>Прервана пользователем</summary>
      [StatusInWork(false)] Aborted,
      /// <summary>Ошибка во время выполнения</summary>
      [StatusInWork(false)] Erroneous,
      /// <summary>В процессе передачи</summary>
      [StatusInWork(true)] Transmitting,
      /// <summary>В ожидании</summary>
      [StatusInWork(false)] Waiting,
      /// <summary>В процессе формирования</summary>
      [StatusInWork(true)] Forming,
      /// <summary>Применение изменений на узле</summary>
      [StatusInWork(true)] ApplyingChangesSite,
      /// <summary>Применение изменений на портале</summary>
      [StatusInWork(true)] ApplyingChangesPortal,
      /// <summary>Удаление задачи на портале</summary>
      [StatusInWork(true)] DeletingPortalTask,
    }
}
