
// Type: Intermech.Interfaces.IDBSecurity
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Data;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс для проверки и назначения прав доступа.</summary>
    public interface IDBSecurity
    {
      /// <summary>
      /// Проверяет права доступа rightID текущей сессии к данному объекту.
      /// </summary>
      /// <param name="rightID">Проверяемые права доступа</param>
      bool CheckAccess(ActionType rightID);

      /// <summary>
      /// Проверяет права доступа rightID текущей сессии к данному объекту.
      /// defaultAccess - права доступа по умолчанию для простых пользователей (не в роли Администратор).
      /// В случае отсутствия прав выдает исключение AccessDeniedException.
      /// </summary>
      /// <param name="rightID">Проверяемые права доступа</param>
      /// <param name="defaultAccess">Права доступа по умолчанию</param>
      /// <returns>true - есть указанные права доступа</returns>
      bool CheckAccess(ActionType rightID, bool defaultAccess);

      /// <summary>
      /// Проверяет права доступа rightID текущей сессии к данному объекту.
      /// Если aThrowACException == false, то возвращает результат проверки (true - есть права,
      /// false - нет прав). Иначе в случае отсутствия прав выдает исключение AccessDeniedException.
      /// defaultAccess - права доступа по умолчанию для простых пользователей (не в роли Администратор).
      /// </summary>
      /// <param name="rightID">Проверяемые права доступа</param>
      /// <param name="defaultAccess">Права доступа по умолчанию</param>
      /// <param name="aThrowACException">Генерировать исключение, если нет прав доступа</param>
      /// <returns>true - есть указанные права доступа</returns>
      bool CheckAccess(ActionType rightID, bool defaultAccess, bool aThrowACException);

      /// <summary>Проверяет права доступа</summary>
      /// <param name="anAction">Проверяемые права доступа</param>
      /// <param name="aDefaultAccess">Права доступа по умолчанию</param>
      /// <param name="flags">Флаги, управляющие проверкой прав доступа</param>
      /// <returns>возвращает true, если право есть, false - если прав нет и об этом не генерится исключение</returns>
      bool CheckAccess(ActionType anAction, bool aDefaultAccess, CheckAccessFlags flags);

      /// <summary>
      /// Возвращает true, если последняя проверка прав доступа завершилась приоритетным запретом
      /// </summary>
      bool IsAccessTypeDeny { get; }

      /// <summary>
      /// Возвращает true, если последняя проверка прав доступа вернула права по умолчанию
      /// </summary>
      bool IsLastDefault { get; }

      /// <summary>
      /// Получить список прав доступа к указанному объекту указанной категории. Формат
      /// DataTable аналогичен формату таблицы IMS_ACCESS_CATEGORY + поле F_BEGIN_DATE -
      /// дата начала действия прав. В расширенных свойствах таблицы атрибут ReadOnly=1 говорит
      /// о том, что данный юзер права доступа менять не имеет права.
      /// actions содержит список допустимых действий над объектом, для которых
      /// можно задавать права доступа.
      /// </summary>
      /// <param name="actions">Действия</param>
      /// <param name="users">Пользователи</param>
      /// <returns>Список прав доступа</returns>
      DataTable GetAccessList(out ActionProperties[] actions, out QuickObjectInfo[] users);

      /// <summary>
      /// Назначить права доступа. Формат accessList аналогичен GetAccessList()
      /// </summary>
      /// <param name="accessList">Список прав доступа</param>
      /// <param name="AddInfo">Дополнительная информация</param>
      void SetAccess(DataTable accessList, params object[] AddInfo);

      /// <summary>
      /// Возвращает массив связанных интерфейсов безопасности, права которых также нужно показывать и
      /// администрировать вместе с правами данного объекта
      /// </summary>
      /// <returns>Массив связанных интерфейсов безопасности</returns>
      IDBSecurity[] GetRelatedSecurity();

      /// <summary>
      /// Возвращает имя данного объекта для записи его в различные логи и сообщения
      /// </summary>
      string ObjectName { get; }

      /// <summary>
      /// Возвращает идентификационную структуру данного объекта, используемую
      /// для хранения информации о правах доступа для него.
      /// </summary>
      CategoryDescriptor Descriptor { get; }

      /// <summary>
      /// Метод восстанавливает права роли Администратор на данный объект
      /// </summary>
      void RestoreAdminAccess();

      /// <summary>
      /// Допускает ли данный объект условные проверки прав доступа
      /// </summary>
      bool EnabledConditionAccess { get; }
    }
}
