
// Type: Intermech.Interfaces.IDBAttributeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Список атрибутов объекта и связи.</summary>
    public interface IDBAttributeCollection
    {
      /// <summary>Возвращает список атрибутов</summary>
      /// <returns>Список атрибутов</returns>
      List<IDBAttribute> ToList();

      /// <summary>i-ый атрибут в списке атрибутов</summary>
      IDBAttribute this[int AttrIndex] { get; }

      /// <summary>Возвращает количество атрибутов в списке.</summary>
      int Count { get; }

      /// <summary>
      /// Ид. типа объекта или связи, к которым относится данный список атрибутов (только
      /// для чтения).
      /// </summary>
      int ObjectType { get; }

      /// <summary>
      /// Ид. объекта или связи, к которым относится данный список атрибутов (только для
      /// чтения).
      /// </summary>
      long ObjectID { get; }

      /// <summary>Возвращает атрибут по имени.</summary>
      /// <param name="AttributeName">Имя атрибута</param>
      IDBAttribute FindByName(string AttributeName);

      /// <summary>Найти атрибут по идентификатору</summary>
      IDBAttribute FindByID(int AttributeID);

      /// <summary>Найти атрибут по GUIDу</summary>
      IDBAttribute FindByGUID(Guid AttributeGUID);

      /// <summary>Найти атрибут по алиасу</summary>
      IDBAttribute FindByAlias(string attributeAlias);

      /// <summary>
      /// Добавляет к данному списку атрибутов все атрибуты из коллекции sourceAttributes.
      /// Если такой атрибут уже есть, то меняется его значение. Отсутствующие в sourceAttributes
      /// атрибуты не удаляются.
      /// </summary>
      void Assign(IDBAttributeCollection sourceAttributes);

      /// <summary>
      /// Добавляет к данному списку атрибутов все атрибуты из коллекции sourceAttributes.
      /// Если такой атрибут уже есть, то меняется его значение. assignMode содержит флаги,
      /// регулирующие присвоение набора атрибутов:
      /// Флаг Consts.DeleteInstances позволяет удалить атрибуты коллекции, которых нет в sourceAttributes.
      /// </summary>
      void Assign(IDBAttributeCollection sourceAttributes, int assignMode);

      /// <summary>
      /// Добавляет к данному списку атрибутов все атрибуты из коллекции sourceAttributes, которые
      /// допускает данный тип объекта/связи.
      /// Если такой атрибут уже есть, то меняется его значение. Отсутствующие в sourceAttributes
      /// атрибуты не удаляются. Функция возвращает идентификаторы атрибутов, которые добавить не удалось.
      /// assignMode содержит флаги, регулирующие присвоение набора атрибутов.
      /// </summary>
      int[] AssignPossibleAttributes(IDBAttributeCollection sourceAttributes, int assignMode);

      /// <summary>
      /// Добавляет атрибут номер attributeID к объекту (связи). Если failIfExists==true и атрибут
      /// уже существует то генерируется исключение. Если failIfExists==false и атрибут уже
      /// существует, то возвращает этот атрибут.
      /// </summary>
      IDBAttribute AddAttribute(int attributeID, bool failIfExists);

      /// <summary>
      /// Добавляет атрибут номер attributeID к объекту (связи) и инициализирует его значениями initValues.
      /// Если failIfExists==true и атрибут уже существует то генерируется исключение.
      /// Если failIfExists==false и атрибут уже существует, то возвращает этот атрибут.
      /// </summary>
      IDBAttribute AddAttribute(int attributeID, bool failIfExists, object[] initValues);

      /// <summary>
      /// Добавляет атрибут attributeGuid к объекту (связи). Если failIfExists==true и атрибут
      /// уже существует то генерируется исключение. Если failIfExists==false и атрибут уже
      /// существует, то возвращает этот атрибут.
      /// </summary>
      IDBAttribute AddAttribute(Guid attributeGuid, bool failIfExists);

      /// <summary>
      /// Добавляет атрибут attributeGuid к объекту (связи) и инициализирует его значениями initValues.
      /// Если failIfExists==true и атрибут уже существует то генерируется исключение.
      /// Если failIfExists==false и атрибут уже существует, то возвращает этот атрибут.
      /// </summary>
      IDBAttribute AddAttribute(Guid attributeGuid, bool failIfExists, object[] initValues);

      /// <summary>
      /// Добавляет временный атрибут номер attributeID к объекту (связи) и инициализирует его значениями
      /// initValues. Если failIfExists==true и атрибут
      /// уже существует то генерируется исключение. Если failIfExists==false и атрибут уже
      /// существует, то возвращает этот атрибут. Временные атрибуты хранятся только в памяти и для них не
      /// производится проверка допустимости их применения к данному типу объектов и связей.
      /// </summary>
      IDBAttribute AddTemporaryAttribute(int attributeID, bool failIfExists);

      IDBAttribute AddTemporaryAttribute(int attributeID, bool failIfExists, object[] initValues);

      /// <summary>
      /// Проверяет наличие атрибутов в базе данных перед их добавлением объекту или связи (по умолчанию выключено для уменьшения тормозов)
      /// </summary>
      bool CheckExistMode { get; set; }

      /// <summary>
      /// Возвращает список значений атрибутов, которые были изменены серверной частью системы
      /// (например, вычисляемых атрибутов или измененных плагинами значений).
      /// Если таковых нет, то возвращается null. Внимание! Не все флажки из modes поддерживаются.
      /// </summary>
      AttributeValues[] GetDeltaValues(GetAttributeValuesModes modes);

      /// <summary>Очищает список измененных сервером атрибутов</summary>
      void ClearDeltaValues();

      /// <summary>
      /// Добавляет ид. атрибута для формирования списка измененных сервером атрибутов
      /// </summary>
      void AddDeltaValue(int attributeID);

      /// <summary>
      /// Выдает список атрибутов типа ft, которые есть у данного объекта (связи)
      /// </summary>
      IDBAttribute[] GetAttributesByType(FieldTypes ft);

      /// <summary>
      /// Возвращает таблицу со значениями всех дополнительных атрибутов объекта/связи. Используется портфелем.
      /// </summary>
      DataTable GetAttributesDataTable();

      /// <summary>
      /// Содержит флаги, указывающие на режим, в котором происходит пакетная запись коллекции атрибутов.
      /// Может содержать флаги Consts.CheckInMode и Consts.CheckOutMode.
      /// </summary>
      int AssignMode { get; }

      /// <summary>
      /// Ф-ция инициализирует значения подчинённых атрибутов, используя значения мастер-атрибутов, указанных в функции.
      /// </summary>
      /// <param name="masterIDs">Идентификаторы мастер-атрибутов, подчинённые атрибуты которых нужно заполнить в данном объекте. Значения мастер-атрибутов уже должны быть проинициализированы.</param>
      void SetDependentAttributes(int[] masterIDs);

      /// <summary>
      /// Режим, в котором коллекция содержит все атрибуты, включая обязательные системные атрибуты объекта/связи
      /// </summary>
      bool AllAttributesMode { get; set; }

      /// <summary>
      /// Возвращает массив идентификаторов атрибутов, которые есть у данного объекта/связи (за исключением системных)
      /// </summary>
      /// <returns>Массив AttributeID</returns>
      int[] GetExistsAttributes();
    }
}
