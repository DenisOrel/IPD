
// Type: Intermech.Interfaces.IDBLifecycleStep
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с этапом жизненного цикла данного типа объектов.
    /// </summary>
    public interface IDBLifecycleStep
    {
      /// <summary>Идентификтор этапа ЖЦ (только для чтения)</summary>
      int LCStep { get; }

      /// <summary>Наименование этапа ЖЦ</summary>
      string LCName { get; set; }

      /// <summary>Комментарии</summary>
      string Note { get; set; }

      /// <summary>
      /// Идентификатор типа объекта, к которому относится данный этап ЖЦ (только для
      /// чтения)
      /// </summary>
      int ObjectTypeID { get; }

      /// <summary>
      /// Тип доступа к объектам на данном этапе ЖЦ:
      /// 0 - контроль прав не производится,
      /// 1 - контроль только по правам ЖЦ (без возможности индивидуального назначения
      /// прав).
      /// 2 - контроль по ЖЦ и персонально объекту (но без возможности передачи прав
      /// доступа по наследству),
      /// 3 - то же, но с возможностью публиковать права подтипам.
      /// </summary>
      LCAccessTypes AccessType { get; set; }

      /// <summary>Идентификатор уровня продвижения.</summary>
      int LevelID { get; set; }

      /// <summary>Удалить этап</summary>
      int Delete(long DeleteMode);

      /// <summary>
      /// Получить идентификаторы шагов, на которые может быть перемещен объект, находящийся на данном шаге ЖЦ
      /// </summary>
      /// <returns></returns>
      int[] GetNextSteps();

      /// <summary>Получить доступные шаги с уровнем продвижения levelID</summary>
      /// <param name="levelID">Уровень продвижения</param>
      /// <returns>Массив идентификаторов допустимых шагов или пустой массив</returns>
      int GetNextStep(int levelID);

      /// <summary>
      /// Возвращает ид. шага ЖЦ, который доступен с данного шага и имеет уровень продвижения "Удаленный"
      /// </summary>
      int GetDeleteStepID();

      /// <summary>Структура со свойствами этапа ЖЦ</summary>
      DBLifecycleStepProperties Properties { get; set; }

      /// <summary>Способ модификации объектов на данном шаге ЖЦ</summary>
      ObjectModifyModes ObjectModifyMode { get; set; }

      /// <summary>
      /// Признак того, что данный шаг является первым в схеме ЖЦ
      /// </summary>
      bool IsFirstStep { get; set; }

      /// <summary>Присвоить глобальный идентификатор шагу ЖЦ</summary>
      void SetGUID(Guid guid);

      /// <summary>Возвращает true если шаг удалён.</summary>
      bool IsDeleted { get; }

      /// <summary>
      /// Ид. схемы, к которой принадлежит данный шаг жизненного цикла
      /// </summary>
      int SchemaID { get; }

      /// <summary>Опции шага ЖЦ</summary>
      LCStepOptions Options { get; set; }

      /// <summary>
      /// Возвращает интерфейс для проверки и назначения прав доступа на атрибут attrID на данном шаге ЖЦ применительно к типу объекта
      /// </summary>
      /// <param name="attrID">Идентификатор атрибута</param>
      /// <returns>Интерфейс для работы с правами доступа</returns>
      IDBSecurity GetAttributeSecurity(int attrID);

      /// <summary>
      /// Возвращает идентификатор шага ЖЦ, на который будут вытесняться версии с данного шага (или 0)
      /// </summary>
      int AutoTransferStepID { get; }
    }
}
