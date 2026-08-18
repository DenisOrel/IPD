
// Type: Intermech.Interfaces.IDBAHistoryCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Kernel.Search;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс истории значений атрибута</summary>
    public interface IDBAHistoryCollection : IDBRecords, IDBSessionable
    {
      /// <summary>
      /// Удаляет историю значений атрибута для объекта/связи номер id.
      /// </summary>
      void DeleteHistory(long id, AttributeSourceTypes st);

      /// <summary>Удаляет всю историю значений атрибута</summary>
      void DeleteHistory(AttributeSourceTypes st);

      /// <summary>
      /// Удаляет историю значений атрибута для его типа объектов или связей
      /// </summary>
      /// <param name="id">Тип объекта/связи</param>
      /// <param name="st">Принадлежность атрибута (объекту или связи)</param>
      void DeleteHistory4Type(int typeID, AttributeSourceTypes st);

      /// <summary>
      /// Возвращает идентификатор атрибута, содержащего текстовое представление значений атрибута
      /// </summary>
      int TextFieldID { get; }

      /// <summary>
      /// Возвращает идентификатор атрибута, содержащего значения атрибута
      /// </summary>
      int ValueFieldID { get; }
    }
}
