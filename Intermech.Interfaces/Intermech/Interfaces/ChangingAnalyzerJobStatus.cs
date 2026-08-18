
// Type: Intermech.Interfaces.ChangingAnalyzerJobStatus
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Состояние задания по анализу списка изменяемых объектов
    /// </summary>
    [Serializable]
    public class ChangingAnalyzerJobStatus : ICloneable
    {
      /// <summary>Количество объектов, добавленных для изменения</summary>
      public long Objects;
      /// <summary>Индикатор выполнения задания</summary>
      public ChangingAnalyzerJobProgress Progress;
      /// <summary>
      /// Список описаний изменяемых объектов.
      /// Значение будет заполнено только когда задание будет успешно выполнено
      /// </summary>
      public ChangingObjects Items;
      /// <summary>Исключение</summary>
      public Exception Exception;

      /// <summary>Создать экземпляр класса</summary>
      public ChangingAnalyzerJobStatus()
      {
        this.Objects = 0L;
        this.Progress = ChangingAnalyzerJobProgress.NotStarted;
        this.Items = (ChangingObjects) null;
        this.Exception = (Exception) null;
      }

      /// <summary>Установить поля класса в "Задание стартовало"</summary>
      public virtual void Start()
      {
        this.Objects = 0L;
        this.Progress = ChangingAnalyzerJobProgress.Working;
        this.Items = (ChangingObjects) null;
        this.Exception = (Exception) null;
      }

      /// <summary>Установить поля класса в "Задание прервано"</summary>
      public virtual void Cancel()
      {
        this.Progress = ChangingAnalyzerJobProgress.Cancelled;
        this.Exception = (Exception) null;
      }

      /// <summary>
      /// Установить поля класса в "Задание остановлено из-за ошибки"
      /// </summary>
      /// <param name="exception">Возникшее исключение</param>
      /// <param name="items">Коллекция описаний изменяемых объектов</param>
      public virtual void Error(Exception exception, ChangingObjects items)
      {
        this.Progress = ChangingAnalyzerJobProgress.Error;
        this.Items = items;
        this.Exception = exception;
      }

      /// <summary>Установить поля класса в "Задание успешно выполнено"</summary>
      /// <param name="objects">Количество добавленных описаний объектов</param>
      /// <param name="items">Коллекция описаний изменяемых объектов</param>
      public virtual void Complete(int objects, ChangingObjects items)
      {
        this.Objects = (long) objects;
        this.Progress = ChangingAnalyzerJobProgress.Completed;
        this.Items = items;
        this.Exception = (Exception) null;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new ChangingAnalyzerJobStatus()
        {
          Objects = this.Objects,
          Progress = this.Progress,
          Items = this.Items,
          Exception = this.Exception
        };
      }
    }
}
