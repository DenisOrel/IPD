
// Type: Intermech.Interfaces.IDBAttributesGroup
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс группы атрибутов</summary>
    public interface IDBAttributesGroup
    {
      /// <summary>Идентификатор группы</summary>
      int GroupID { get; }

      /// <summary>Имя группы (уникальное)</summary>
      string GroupName { get; set; }

      /// <summary>Комментарии</summary>
      string Note { get; set; }

      /// <summary>
      /// Ид. группы атрибутов, в которую входит данная группа. Если 0, то группа находится на верхнем уровне иерархии групп.
      /// </summary>
      int ParentID { get; set; }

      /// <summary>
      /// Добавить атрибуты attributeIDs в состав группы атрибутов.
      /// </summary>
      int IncludeAttribute(int[] attributeIDs);

      /// <summary>
      /// Добавить атрибут attributeID в состав группы атрибутов.
      /// </summary>
      int IncludeAttribute(int attributeID);

      /// <summary>Исключить атрибуты attributeIDs из группы атрибутов.</summary>
      int ExcludeAttribute(params int[] attributeIDs);

      /// <summary>Коллекция атрибутов, входящих в состав данной группы</summary>
      IDBAttributeTypeCollection Attributes { get; }

      /// <summary>Удалить группу атрибутов</summary>
      /// <param name="DeleteMode">Зарезервировано.</param>
      /// <returns></returns>
      int Delete(long DeleteMode);

      /// <summary>Присваивает группе новый глобальный идентификатор</summary>
      void SetGUID(Guid guid);

      /// <summary>
      /// Возвращает true, если атрибут attrID включен в данную группу
      /// </summary>
      /// <param name="attrID">Ид. атрибута</param>
      /// <returns>Результат</returns>
      bool HasAttribute(int attrID);
    }
}
