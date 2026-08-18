
// Type: Intermech.Interfaces.IDBAttributesGroupInfo
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс с информацией о группе атрибутов</summary>
    public interface IDBAttributesGroupInfo
    {
      /// <summary>Идентификатор группы</summary>
      int GroupID { get; }

      /// <summary>Имя группы (уникальное)</summary>
      string GroupName { get; }

      /// <summary>Комментарии</summary>
      string Note { get; }

      /// <summary>
      /// Ид. группы атрибутов, в которую входит данная группа. Если 0, то группа находится на верхнем уровне иерархии групп.
      /// </summary>
      int ParentID { get; }

      /// <summary>Коллекция атрибутов, входящих в состав данной группы</summary>
      IDBAttributeTypeInfoCollection Attributes { get; }

      /// <summary>
      /// Возвращает true, если атрибут attrID включен в данную группу
      /// </summary>
      /// <param name="attrID">Ид. атрибута</param>
      /// <returns>Результат</returns>
      bool HasAttribute(int attrID);
    }
}
