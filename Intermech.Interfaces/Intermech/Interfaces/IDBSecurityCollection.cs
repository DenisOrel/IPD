
// Type: Intermech.Interfaces.IDBSecurityCollection
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для проверки и назначения прав доступа сразу к нескольким объектам одинаковой категории
    /// </summary>
    public interface IDBSecurityCollection : IDBSecurity
    {
      /// <summary>
      /// Функция проверяет идентичен ли набор прав доступа к объектам categoryID с набором прав доступа текущего объекта
      /// </summary>
      /// <param name="categoryID">Идентификаторы проверяемых объектов</param>
      /// <returns>true, если набор прав доступа идентичен</returns>
      bool IsIdenticalAccess(long[] categoryID);

      /// <summary>
      /// Назначает права доступа к текущему объекту, а также на объекты с идентификаторами categoryID.
      /// Формат accessList аналогичен GetAccessList()
      /// </summary>
      /// <param name="categoryID">Идентификаторы объектов</param>
      /// <param name="accessList">Список прав доступа</param>
      /// <param name="AddInfo">Дополнительная информация</param>
      void SetAccess(long[] categoryID, DataTable accessList, params object[] AddInfo);

      /// <summary>
      /// Возвращает интерфейс связанной коллекции безопасности, права которой также нужно показывать и
      /// администрировать вместе с правами объектов данного categoryType. Если возвращает null, то больше ничего назначать не нужно.
      /// </summary>
      IDBSecurityCollection GetRelatedSecurityCollection(long[] categoryID);

      /// <summary>
      /// Имя коллекции для отображения в диалогах, на список чего назначают права доступа
      /// </summary>
      string SecurityCollectionName { get; }

      /// <summary>
      /// Метод проверяет являются ли указанные в categoryID элементы совместимыми для назначения прав доступа
      /// </summary>
      bool IsCompatibleElements(long[] categoryID);
    }
}
