
// Type: Intermech.Interfaces.IDBAttributable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Базовый интерфейс для работы с экземплярами атрибутов. Поддерживается информационными объектами и связями.
    /// </summary>
    public interface IDBAttributable : IDBSessionable, IPluginsData
    {
      /// <summary>Список атрибутов</summary>
      IDBAttributeCollection Attributes { get; }

      /// <summary>
      /// Получить атрибут по его идентификатору. Если атрибута нет, то возвращает null.
      /// </summary>
      IDBAttribute GetAttributeByID(int attributeID);

      /// <summary>
      /// Получить атрибут по его GUIDу. Возвращает null если такой атрибут у объекта отсутствует.
      /// </summary>
      IDBAttribute GetAttributeByGuid(Guid attributeGuid);

      /// <summary>
      /// Получить атрибут по его имени. Возвращает null если такой атрибут у объекта отсутствует.
      /// </summary>
      IDBAttribute GetAttributeByName(string attributeName);

      /// <summary>
      /// Получить атрибут по его GUIDу. Если throwNotFoundException=true, то
      /// генерирует AttributeNotFoundException
      /// </summary>
      IDBAttribute GetAttributeByGuid(Guid attributeGuid, bool throwNotFoundException);

      /// <summary>
      /// Получить атрибут по его имени. Если throwNotFoundException=true, то
      /// генерирует AttributeNotFoundException
      /// </summary>
      IDBAttribute GetAttributeByName(string attributeName, bool throwNotFoundException);

      /// <summary>
      /// Возвращает массив значений атрибутов в соответствии с modes
      /// </summary>
      AttributeValues[] GetAttributesValues(GetAttributeValuesModes modes);

      /// <summary>
      /// Записывает значения атрибутам объекта посредством массива valuesList. Недостающие атрибуты
      /// добавляются объекту. Часть обязательных атрибутов тоже может быть записана данным методом.
      /// Если valuesList.ReadOnly=true, то запись игнорируется. Блобы также игнорируются.
      /// Внимание!!! Если deleteNotExistingAttributes=true, то атрибуты, отсутствующие в данном
      /// списке, будут удалены (кроме обязательных атрибутов). При этом dontDeleteBlobs=true означает,
      /// что блобовые атрибуты удаляться не будут.
      /// Если returnDelta==true, то ф-ция возвращает массив атрибутов, которые были изменены сервером.
      /// Если таких атрибутов нет (или returnDelta==false), то ф-ция возвращает null.
      /// </summary>
      AttributeValues[] SetAttributesValues(
        AttributeValues[] valuesList,
        bool deleteNotExistingAttributes,
        bool dontDeleteBlobs,
        bool returnDelta,
        GetAttributeValuesModes modes);

      /// <summary>returnDelta=false</summary>
      AttributeValues[] SetAttributesValues(
        AttributeValues[] valuesList,
        bool deleteNotExistingAttributes,
        bool dontDeleteBlobs);

      /// <summary>
      /// Аналогично предыдущему методу, только deleteNotExistingAttributes=false, dontDeleteBlobs=true,
      /// returnDelta=false
      /// </summary>
      AttributeValues[] SetAttributesValues(AttributeValues[] valuesList);

      /// <summary>
      /// Установить значения атрибутов, вернув список исключений, которые произошли при попытке записи значений атрибутов.
      /// </summary>
      /// <param name="valuesList">Значения атрибутов</param>
      /// <param name="deleteNotExistingAttributes">Удалять несуществующие атрибуты</param>
      /// <param name="dontDeleteBlobs">Не удалять Blob-атрибуты</param>
      /// <param name="returnDelta">Вернуть разницу значений атрибутов</param>
      /// <param name="modes">Режимы</param>
      /// <returns>Словарь типа: Наименование атрибута=Исключение, произошедшее при записи значения атрибута</returns>
      Dictionary<string, Exception> SetAttributesValuesEx(
        AttributeValues[] valuesList,
        bool deleteNotExistingAttributes,
        bool dontDeleteBlobs,
        bool returnDelta,
        GetAttributeValuesModes modes);

      /// <summary>
      /// Говорит о том, что атрибуты данного объекта (связи) вообще нельзя редактировать
      /// </summary>
      bool ReadOnly { get; }

      /// <summary>
      /// Возвращает значения атрибута guid, если таковой имеется. Если throwNotFoundException, а
      /// атрибута нет, то возвращает null. Метод хорош тем, что возвращает значения и обязательных
      /// атрибутов.
      /// </summary>
      object[] GetValuesByGuid(Guid guid, bool throwNotFoundException);

      /// <summary>Получить значения указанного атрибута</summary>
      /// <param name="attributeName">Наименование атрибута</param>
      /// <param name="throwNotFoundException">true - генерировать исключение при ошибке</param>
      /// <returns>Значения указанного атрибута</returns>
      object[] GetValuesByName(string attributeName, bool throwNotFoundException);

      /// <summary>
      /// Возвращает значения атрибута attributeID, если таковой имеется. Если throwNotFoundException, а
      /// атрибута нет, то возвращает null. Метод хорош тем, что возвращает значения и обязательных
      /// атрибутов.
      /// </summary>
      object[] GetValuesByID(int attributeID, bool throwNotFoundException);

      /// <summary>
      /// Возвращает строковую расшифровку значений атрибута с идентификатором attributeID.
      /// Если throwNotFoundException==true, то генерирует исключение AttributeNotFoundException в случае
      /// отсутствия данного атрибута у объекта.
      /// </summary>
      string[] GetDescriptionsByID(int attributeID, bool throwNotFoundException);

      /// <summary>
      /// Возвращает строковую расшифровку значений атрибута с идентификатором guid.
      /// Если throwNotFoundException==true, то генерирует исключение AttributeNotFoundException в случае
      /// отсутствия данного атрибута у объекта.
      /// </summary>
      string[] GetDescriptionsByGuid(Guid guid, bool throwNotFoundException);

      /// <summary>
      /// Возвращает тип объекта или связи, которому(ой) принадлежат эти атрибуты
      /// </summary>
      int TypeID { get; }

      /// <summary>
      /// Возвращает обработчик типа атрибута номер attributeID применительно к данному объекту/связи
      /// </summary>
      IDBAttributeType GetAttributeType(int attributeID);

      /// <summary>
      /// Метод пробует добавить атрибут с указанным идентификатором в коллекцию, если это допустимо.
      /// Если предполагаемое новое значение равно null, а атрибут не найден, то он не будет создаваться.
      /// Если значение равно null, а атрибут существует и является добавляемым вручную, он будет удалён.
      /// </summary>
      /// <param name="attrID">Идентификатор атрибута</param>
      /// <param name="newValue">Предполагаемое новое значение атрибута. null - удалить атрибут, если он добавляемый вручную</param>
      /// <returns>Вновь добавленный атрибут или null</returns>
      IDBAttribute TryToAddOrDelAttribute(int attrID, object newValue);

      /// <summary>
      /// Ф-ция формирует набор информации (возможность записи, значение по умолчанию) об атрибутах attributeIDs
      /// </summary>
      /// <param name="attributeIDs">Идентификаторы атрибутов</param>
      /// <returns>Массив значений. если в свойстве атрибута readonly == true, то значение по умолчанию не заполняется</returns>
      AttributeValues[] GetInitAttributesValues(int[] attributeIDs);

      /// <summary>
      /// Метод предназначен для получения новых значений вычисляемых атрибутов без записи этих значений в базу данных
      /// </summary>
      /// <param name="valuesList">Список изменяемых атрибутов с их новыми значениями</param>
      /// <param name="modes">Свойства, управляющие заполнением массива вычесленных значений</param>
      /// <returns>Список вычисленных сервером значений атрибутов.</returns>
      AttributeValues[] GetCalculatedValues(AttributeValues[] valuesList, GetAttributeValuesModes modes);

      /// <summary>
      /// Возвращает массив идентификаторов атрибутов, которые есть у данного объекта/связи (за исключением системных)
      /// </summary>
      /// <returns>Массив AttributeID</returns>
      int[] GetExistsAttributes();
    }
}
