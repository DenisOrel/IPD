
// Type: Intermech.Interfaces.SearchGroupingObjectJobStatus
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Состояние задания по поиску группирующих объектов</summary>
    [Serializable]
    public class SearchGroupingObjectJobStatus : ICloneable
    {
      /// <summary>Количество объектов, добавленных в список</summary>
      public long Objects;
      /// <summary>Индикатор выполнения задания</summary>
      public SearchGroupingObjectJobProgress Progress;
      /// <summary>
      /// Список описаний объектов.
      /// Значение будет заполнено только когда задание будет успешно выполнено
      /// </summary>
      public SearchGroupingObjects Items;
      /// <summary>Исключение</summary>
      public Exception Exception;

      /// <summary>Создать экземпляр класса</summary>
      public SearchGroupingObjectJobStatus()
      {
        this.Objects = 0L;
        this.Progress = SearchGroupingObjectJobProgress.NotStarted;
        this.Items = (SearchGroupingObjects) null;
        this.Exception = (Exception) null;
      }

      /// <summary>Установить поля класса в "Задание стартовало"</summary>
      public virtual void Start()
      {
        this.Objects = 0L;
        this.Progress = SearchGroupingObjectJobProgress.Working;
        this.Items = (SearchGroupingObjects) null;
        this.Exception = (Exception) null;
      }

      /// <summary>Установить поля класса в "Задание прервано"</summary>
      public virtual void Cancel()
      {
        this.Progress = SearchGroupingObjectJobProgress.Cancelled;
        this.Exception = (Exception) null;
      }

      /// <summary>
      /// Установить поля класса в "Задание остановлено из-за ошибки"
      /// </summary>
      /// <param name="exception">Возникшее исключение</param>
      /// <param name="items">Коллекция описаний объектов</param>
      public virtual void Error(Exception exception, SearchGroupingObjects items)
      {
        this.Progress = SearchGroupingObjectJobProgress.Error;
        this.Items = items;
        this.Exception = exception;
      }

      /// <summary>Установить поля класса в "Задание успешно выполнено"</summary>
      /// <param name="objects">Количество добавленных описаний объектов</param>
      /// <param name="items">Коллекция описаний объектов</param>
      public virtual void Complete(int objects, SearchGroupingObjects items)
      {
        this.Objects = (long) objects;
        this.Progress = SearchGroupingObjectJobProgress.Completed;
        this.Items = items;
        this.Exception = (Exception) null;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new SearchGroupingObjectJobStatus()
        {
          Objects = this.Objects,
          Progress = this.Progress,
          Items = this.Items,
          Exception = this.Exception
        };
      }
    }
}
