
// Type: Intermech.Interfaces.CustomSelectThread`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces.Contexts;
using System;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Поток, выполняющий запросы для получения состава / применяемости
    /// </summary>
    public abstract class CustomSelectThread<T>
    {
      /// <summary>Название задачи</summary>
      private Guid _id;
      /// <summary>Фоновый поток, который выполняет задачу</summary>
      private Thread _thread;
      /// <summary>
      /// Фиксированный контекст редактирования или <see cref="P:Intermech.Interfaces.Contexts.CurrentEditingContext.Dummy" />
      /// </summary>
      private CurrentEditingContext _editingContext;
      /// <summary>GUID сессии, в рамках которой выполняется работа</summary>
      protected Guid userSessionGuid;
      /// <summary>Результат работы</summary>
      protected T result;
      /// <summary>Событие, возникающее по завершении выполнении задачи</summary>
      public SelectThreadEndEvent SelectThreadEnd;
      /// <summary>Выполнена ли задача</summary>
      public bool IsCompleted;
      /// <summary>Процент выполнения задачи, 0 .. 100</summary>
      public int Percent;
      /// <summary>Возникла ли ошибка в потоке</summary>
      public bool IsError;
      /// <summary>
      /// Исключение, которое вывалилось при возникновении ошибки в потоке
      /// </summary>
      public Exception ErrorException;

      /// <summary>
      /// Создать экземпляр потока, выполняющего запросы для получения состава / применяемости
      /// </summary>
      /// <param name="id">Уникальный идентифкатор задачи</param>
      /// <param name="userSessionGuid">GUID сессии, в рамках которой выполняется работа</param>
      public CustomSelectThread(Guid id, Guid userSessionGuid)
      {
        this._id = id;
        this._editingContext = CurrentEditingContext.Dummy;
        this.userSessionGuid = userSessionGuid;
      }

      /// <summary>
      /// Возвращает или задает фиксированный контекст редактирования, в рамках которого выполняется задача.
      /// Значение свойства может содержать объект-пустышку <see cref="P:Intermech.Interfaces.Contexts.CurrentEditingContext.Dummy" />,
      /// который обозначает, что контекст редактирования не фиксирован.
      /// </summary>
      public CurrentEditingContext EditingContext
      {
        [DebuggerStepThrough] get => this._editingContext;
        set => this._editingContext = value ?? throw new ArgumentNullException(nameof (value));
      }

      /// <summary>Результаты работы задачи</summary>
      public T Result => this.result;

      /// <summary>Стартовать задачу</summary>
      public void Start()
      {
        this._thread = new Thread(this._editingContext.SendToThread(new ThreadStart(this.ThreadMethod)))
        {
          IsBackground = true,
          Name = "SelectComposition_" + this._id.ToString()
        };
        this._thread.Start();
      }

      /// <summary>Остановить задачу</summary>
      public void Stop()
      {
        if (this._thread == null || !this._thread.IsAlive)
          return;
        this._thread.Abort();
        this.result = default (T);
        this.SetPercent(100);
        this.IsError = true;
        this.ErrorException = (Exception) new AbortException("Прервано пользователем");
        this._thread = (Thread) null;
      }

      /// <summary>Установить значение процента выполненной задачи</summary>
      /// <param name="percent">Процент выполненной задачи, 0 .. 100</param>
      protected void SetPercent(int percent)
      {
        if (percent == 0)
          this.IsCompleted = false;
        if (percent == 100)
          this.IsCompleted = true;
        this.Percent = percent;
      }

      /// <summary>Основной метод потока</summary>
      protected abstract void ThreadMethod();
    }
}
