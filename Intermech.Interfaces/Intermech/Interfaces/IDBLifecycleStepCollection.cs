
// Type: Intermech.Interfaces.IDBLifecycleStepCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс коллекции этапов жизненного цикла для объектов определенного типа
    /// </summary>
    public interface IDBLifecycleStepCollection
    {
      /// <summary>
      /// Ид. типа объектов, к которому принадлежит данная схема
      /// </summary>
      int ObjectTypeID { get; }

      /// <summary>Копирует схему в тип toObjectTypeID</summary>
      void CopyTo(int toObjectTypeID);

      /// <summary>
      /// Обновляет связи между шагами ЖЦ данного типа объектов. Таблица linksList должна иметь структуру
      /// таблицы IMS_LC_LINKS и содержать все связи, которые нужно добавить или обновить, вслучае,
      /// если не нужно удалять связи (deleteNotExists == false), либо все связи схемы, если
      /// deleteNotExists == true
      /// </summary>
      void SetLinks(DataTable linksList, bool deleteNotExists);

      /// <summary>
      /// Удаляет связь между шагами ЖЦ номер fromStepID и toStepID
      /// </summary>
      void DeleteLink(int fromStepID, int toStepID);

      /// <summary>
      /// Возвращает dataset с таблицами IMS_LC_STEPS и IMS_LC_LINKS, в которых
      /// описаны шаги и связи схемы жизненного цикла для данного типа объектов
      /// (номер типа см. в ObjectTypeID)
      /// </summary>
      /// <returns></returns>
      DataSet GetSchema();

      /// <summary>
      /// Сохраняет модифицированную схему ЖЦ. Формат dsSchema аналогичен ф-ции GetSchema.
      /// Новые шаги ЖЦ должны иметь отрицательные идентификаторы.
      /// </summary>
      void SetSchema(DataSet dsSchema);

      /// <summary>Создает новый этап жизненного цикла</summary>
      IDBLifecycleStep Create(DBLifecycleStepProperties lcProps);

      /// <summary>Возвращает идентификатор первого шага схемы</summary>
      int GetFirstStep();

      /// <summary>
      /// Возвращает массив структур с шагами ЖЦ заданных объектов
      /// </summary>
      [Obsolete]
      ObjectSteps[] GetObjectsSteps(long[] objectIDs);

      /// <summary>
      /// Возвращает массив структур с общими шагами ЖЦ для списка исходных шагов ЖЦ (оба списка суммируются). Если общих шагов нет - возвращается null.
      /// </summary>
      ObjectSteps[] GetObjectsSteps(List<int> stepsID);

      /// <summary>Изменяет шаг жизненного цикла у заданных объектов</summary>
      void SetObjectsLCStep(long[] objectIDs, int lcStep);

      /// <summary>
      /// Метод ищет среди шагов ЖЦ данной схемы шаг со свойствами, соответствующими шагу oldStep
      /// </summary>
      /// <param name="oldStep">Исходный шаг, для которого нужно найти аналог в данной схеме ЖЦ</param>
      /// <param name="errorMsg">Сообщение об ошибке, почему поиск не удался</param>
      /// <returns>Возвращает найденный шаг или null, если шаг не найден</returns>
      IDBLifecycleStep FindSameStep(IDBLifecycleStep oldStep, out string errorMsg);
    }
}
