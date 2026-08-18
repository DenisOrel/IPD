
// Type: Intermech.Interfaces.IDBObjectTypeCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;
using System.Data;


namespace Intermech.Interfaces
{
    public interface IDBObjectTypeCollection : IDBCollection
    {
      /// <summary>
      /// Создает новый тип объектов и возвращает его идентификатор.
      /// </summary>
      /// <param name="typeProperties"></param>
      /// <returns></returns>
      int Create(ObjectTypeProperties typeProperties);

      /// <summary>
      /// Возвращает таблицу иерархии типов объектов (f_object_type -&gt; f_parent_id)
      /// </summary>
      DataTable GetTypesHierarchy();

      /// <summary>
      /// Возвращает список типов объектов, для которых задано использование атрибута номер attributeID
      /// </summary>
      DataTable GetUsedByAttribute(int attributeID);

      /// <summary>
      /// Возвращает рекурсивный список всех типов объектов, которые входят в данный тип.
      /// </summary>
      DataTable SelectRecursive(string orderBy, params object[] addInfo);

      /// <summary>
      /// Возвращает массив идентификаторов типов, у которых данному юзеру право ActionType отобрали
      /// </summary>
      List<int> GetDisabledAccess(ActionType at);

      /// <summary>
      /// Метод проверяет имеет ли право данный юзер просматривать список объектов того типа, для которого создана данная коллекция.
      /// </summary>
      bool CanViewObjects();
    }
}
