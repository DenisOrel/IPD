
// Type: Intermech.Interfaces.DeleteObjectsJobStatus
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Состояние задания по удалению объектов</summary>
    [Serializable]
    public class DeleteObjectsJobStatus : ICloneable
    {
      /// <summary>Количество объектов, успешно удалённых</summary>
      public int Objects;
      /// <summary>
      /// Количество объектов, удаление которых не было выполнено
      /// </summary>
      public int Skipped;
      /// <summary>Количество связей, успешно удалённых</summary>
      public int RelationsCount;
      /// <summary>Индикатор выполнения задания</summary>
      public DeleteObjectsJobProgress Progress;
      /// <summary>Режим удаления объектов</summary>
      public DeleteObjectsJobMode Mode;
      /// <summary>
      /// Список идентификаторов версий удалённых объектов.
      /// Значение будет заполнено только когда задание будет успешно выполнено
      /// </summary>
      public List<long> Items;
      /// <summary>
      /// Список идентификаторов удалённых связей.
      /// Значение будет заполнено только когда задание будет успешно выполнено
      /// </summary>
      public List<long> Relations;
      /// <summary>
      /// Список идентификаторов родительских объектов удалённых связей.
      /// Значение будет заполнено только когда задание будет успешно выполнено
      /// </summary>
      public List<long> RelationsProjIDs;
      /// <summary>
      /// Список идентификаторов типов удалённых связей.
      /// Значение будет заполнено только когда задание будет успешно выполнено
      /// </summary>
      public List<int> RelationsTypeIDs;
      /// <summary>Исключение</summary>
      public Exception Exception;

      /// <summary>Создать экземпляр класса</summary>
      public DeleteObjectsJobStatus()
      {
        this.Objects = 0;
        this.Skipped = 0;
        this.RelationsCount = 0;
        this.Progress = DeleteObjectsJobProgress.NotStarted;
        this.Items = (List<long>) null;
        this.Relations = (List<long>) null;
        this.RelationsProjIDs = (List<long>) null;
        this.RelationsTypeIDs = (List<int>) null;
        this.Exception = (Exception) null;
      }

      /// <summary>Установить поля класса в "Задание стартовало"</summary>
      public virtual void Start()
      {
        this.Objects = 0;
        this.Skipped = 0;
        this.RelationsCount = 0;
        this.Progress = DeleteObjectsJobProgress.Working;
        this.Items = (List<long>) null;
        this.Relations = (List<long>) null;
        this.RelationsProjIDs = (List<long>) null;
        this.RelationsTypeIDs = (List<int>) null;
        this.Exception = (Exception) null;
      }

      /// <summary>Установить поля класса в "Задание прервано"</summary>
      /// <param name="objects">Количество добавленных описаний объектов</param>
      /// <param name="skipped">Количество объектов, удаление которых не было выполнено</param>
      /// <param name="relationsCount">Количество удалённых связей</param>
      /// <param name="relations">Список идентификаторов удалённых связей</param>
      /// <param name="relProjIDs">Список идентификаторов версий родительских объектов удалённых связей</param>
      /// <param name="relTypeIDs">Список типов удалённых связей</param>
      /// <param name="items">Коллекция идентификаторов успешно удалённых объектов</param>
      public virtual void Cancel(
        int objects,
        int skipped,
        int relationsCount,
        List<long> items,
        List<long> relations,
        List<long> relProjIDs,
        List<int> relTypeIDs)
      {
        this.Objects = objects;
        this.Skipped = skipped;
        this.RelationsCount = relationsCount;
        this.Progress = DeleteObjectsJobProgress.Cancelled;
        this.Items = items;
        this.Relations = relations;
        this.RelationsProjIDs = relProjIDs;
        this.RelationsTypeIDs = relTypeIDs;
        this.Exception = (Exception) null;
      }

      /// <summary>
      /// Установить поля класса в "Задание остановлено из-за ошибки"
      /// </summary>
      /// <param name="exception">Возникшее исключение</param>
      /// <param name="objects">Количество добавленных описаний объектов</param>
      /// <param name="skipped">Количество объектов, удаление которых не было выполнено</param>
      /// <param name="relationsCount">Количество удалённых связей</param>
      /// <param name="relations">Список идентификаторов удалённых связей</param>
      /// <param name="relProjIDs">Список идентификаторов версий родительских объектов удалённых связей</param>
      /// <param name="relTypeIDs">Список типов удалённых связей</param>
      /// <param name="items">Коллекция идентификаторов успешно удалённых объектов</param>
      public virtual void Error(
        Exception exception,
        int objects,
        int skipped,
        int relationsCount,
        List<long> items,
        List<long> relations,
        List<long> relProjIDs,
        List<int> relTypeIDs)
      {
        this.Objects = objects;
        this.Skipped = skipped;
        this.RelationsCount = relationsCount;
        this.Progress = DeleteObjectsJobProgress.Error;
        this.Items = items;
        this.Relations = relations;
        this.RelationsProjIDs = relProjIDs;
        this.RelationsTypeIDs = relTypeIDs;
        this.Exception = exception;
      }

      /// <summary>Установить поля класса в "Задание успешно выполнено"</summary>
      /// <param name="objects">Количество добавленных описаний объектов</param>
      /// <param name="skipped">Количество объектов, удаление которых не было выполнено</param>
      /// <param name="relationsCount">Количество удалённых связей</param>
      /// <param name="relations">Список идентификаторов удалённых связей</param>
      /// <param name="relProjIDs">Список идентификаторов версий родительских объектов удалённых связей</param>
      /// <param name="relTypeIDs">Список типов удалённых связей</param>
      /// <param name="items">Коллекция идентификаторов успешно удалённых объектов</param>
      public virtual void Complete(
        int objects,
        int skipped,
        int relationsCount,
        List<long> items,
        List<long> relations,
        List<long> relProjIDs,
        List<int> relTypeIDs)
      {
        this.Objects = objects;
        this.Skipped = skipped;
        this.RelationsCount = relationsCount;
        this.Progress = DeleteObjectsJobProgress.Completed;
        this.Items = items;
        this.Relations = relations;
        this.RelationsProjIDs = relProjIDs;
        this.RelationsTypeIDs = relTypeIDs;
        this.Exception = (Exception) null;
      }

      /// <summary>
      /// Приостановить задание, ожидая реакцию со стороны клиента
      /// </summary>
      /// <param name="exception">Возникшее исключение</param>
      /// <param name="objects">Количество добавленных описаний объектов</param>
      /// <param name="skipped">Количество объектов, удаление которых не было выполнено</param>
      /// <param name="items">Коллекция идентификаторов успешно удалённых объектов</param>
      /// <param name="relationsCount">Количество удалённых связей</param>
      /// <param name="relations">Список идентификаторов удалённых связей</param>
      /// <param name="relProjIDs">Список идентификаторов версий родительских объектов удалённых связей</param>
      /// <param name="relTypeIDs">Список типов удалённых связей</param>
      /// <param name="mode">Режим удаления объектов</param>
      public virtual void Pause(
        Exception exception,
        int objects,
        int skipped,
        int relationsCount,
        List<long> items,
        List<long> relations,
        List<long> relProjIDs,
        List<int> relTypeIDs,
        DeleteObjectsJobMode mode)
      {
        this.Objects = objects;
        this.Skipped = skipped;
        this.RelationsCount = relationsCount;
        this.Progress = DeleteObjectsJobProgress.Error;
        this.Items = items;
        this.Relations = relations;
        this.RelationsProjIDs = relProjIDs;
        this.RelationsTypeIDs = relTypeIDs;
        this.Exception = exception;
        this.Mode = mode;
      }

      /// <summary>Создать копию экземпляра класса</summary>
      /// <returns>Копия экземпляра класса</returns>
      public object Clone()
      {
        return (object) new DeleteObjectsJobStatus()
        {
          Objects = this.Objects,
          Skipped = this.Skipped,
          RelationsCount = this.RelationsCount,
          Progress = this.Progress,
          Items = this.Items,
          Relations = this.Relations,
          RelationsProjIDs = this.RelationsProjIDs,
          RelationsTypeIDs = this.RelationsTypeIDs,
          Exception = this.Exception,
          Mode = this.Mode
        };
      }
    }
}
