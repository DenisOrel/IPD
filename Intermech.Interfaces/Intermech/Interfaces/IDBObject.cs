using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;


namespace Intermech.Interfaces
{
    /// <summary>Интерфейс объекта</summary>
    public interface IDBObject : IDBAttributable, IDBSessionable, IPluginsData
    {
      /// <summary>
      /// Уникальный идентификатор объекта данной версии (только для чтения).
      /// </summary>
      long ObjectID { get; }

      /// <summary>Уникальный идентификатор объекта (только для чтения)</summary>
      long ID { get; }

      /// <summary>Порядковый номер версии объекта (только для чтения).</summary>
      int VersionID { get; }

      /// <summary>Дата создания объекта</summary>
      DateTime CreateDate { get; }

      /// <summary>Идентификатор этапа жизненного цикла объекта</summary>
      int LCStep { get; set; }

      /// <summary>
      /// Метод проверяет можно ли данный объект перевести на шаг ЖЦ nextstepID. Если нет - генерит соотв. эксепшен.
      /// </summary>
      /// <param name="nextstepID">Следующий шаг ЖЦ</param>
      void ValidateSetNextLCStep(int nextstepID);

      /// <summary>
      /// Метод проверяет можно ли данный объект перевести на шаг ЖЦ nextstepGUID. Если нет - генерит соотв. эксепшен.
      /// </summary>
      /// <param name="nextstepGUID">Следующий шаг ЖЦ</param>
      void ValidateSetNextLCStep(Guid nextstepGUID);

      /// <summary>
      /// Возвращает true, если данный объект можно перевести на шаг ЖЦ nextstepID
      /// </summary>
      /// <param name="nextstepID">Ид. шага ЖЦ</param>
      /// <param name="errorMessage">Сообщение об ошибке, из-за которой объект нельзя перевести на новый шаг ЖЦ</param>
      /// <returns>True если перевод возможен</returns>
      bool CanSetNextLCStep(int nextstepID, out string errorMessage);

      /// <summary>
      /// Идентификатор пользователя, взявшего объект на изменение
      /// </summary>
      long CheckoutBy { get; }

      /// <summary>Идентификатор типа объекта</summary>
      int ObjectType { get; set; }

      /// <summary>Идентификатор объекта-владельца</summary>
      long OwnerID { get; set; }

      /// <summary>Идентификатор пользователя-создателя</summary>
      long CreatorID { get; }

      /// <summary>
      /// Возвращает строковое представление объекта для его отображения в
      /// проводнике объектов.
      /// </summary>
      string Caption { get; set; }

      /// <summary>
      /// Возвращает строку для отображения имени объекта в информационных сообщениях
      /// </summary>
      string NameInMessages { get; }

      /// <summary>
      /// Дата последней модификации содержимого объекта (только для чтения). Вызывает исключение KernelException, если у объекта нет такого атрибута.
      /// </summary>
      DateTime ModifyDate { get; }

      /// <summary>
      /// Статус версии (-1 – заготовка, 0 – версия объекта, 1 – версия находится в процессе импорта из портфеля или web-портала).
      /// Значения соответствуют перечислителю Intermech.ObjectRecordKind
      /// </summary>
      int ObjectVerType { get; set; }

      /// <summary>Номер группы изменений</summary>
      long ModificationID { get; }

      /// <summary>Узлы информационной системы</summary>
      string SiteID { get; }

      /// <summary>Признак базовой версии</summary>
      bool IsBaseVersion { get; }

      /// <summary>
      /// Взять объект на изменение. Возвращает объект взятой на изменение рабочей копии, новую
      /// версию объекта или ссылку на этот же объект (все зависит от поведения данного типа объекта
      /// на данном шаге ЖЦ).
      /// </summary>
      IDBObject CheckOut();

      /// <summary>
      /// Взять объект на изменение. Если throwModifyModeException == true, то при попытке взять на изменение объект,
      /// чей шаг не допускает взятие на изменение, метод генерирует исключение. Иначе метод возвращает интерфейс
      /// объекта, который допускает изменение. При этом возвращается либо данный объект, либо его рабочая копия,
      /// либо новая версия объекта.
      /// </summary>
      /// <param name="throwModifyModeException">Если throwModifyModeException == true, то при попытке взять
      /// на изменение объект, чей шаг не допускает взятие на изменение, метод генерирует исключение</param>
      IDBObject CheckOut(bool throwModifyModeException);

      /// <summary>Вернуть объект в базу с сохранением изменений.</summary>
      /// <returns>Зарезервировано</returns>
      int CheckIn();

      /// <summary>
      /// Отменяет изменения в данном объекте. Отмена может быть выполнена или пользователем,
      /// взявшим объект на изменение, или системной сессией.
      /// Если isAdminMode==true и пользователь зашел под ролью Администратор,
      /// то система позволяет отменять изменения в объектах, взятых на изменение другими пользователями.
      /// </summary>
      /// <param name="isAdminMode">Если isAdminMode==true и пользователь зашел под ролью Администратор,
      /// то система позволяет отменять изменения в объектах, взятых на изменение другими пользователями.</param>
      /// <returns>Зарезервировано</returns>
      int CancelChanges(bool isAdminMode);

      /// <summary>Отменяет изменения в данном объекте.</summary>
      /// <returns>Зарезервировано</returns>
      int CancelChanges();

      /// <summary>
      /// Сохраняет изменения файловой копии объекта в рабочую копию (которая находится в базе данных)
      /// </summary>
      void SaveChanges();

      /// <summary>
      /// Метод вызывается перед редактированием файлов объекта для проверки прав доступа.
      /// </summary>
      void Edit();

      /// <summary>
      /// Сохраняет атрибуты и состав рабочей копии объекта в архивную копию
      /// </summary>
      void SaveToArcCopy();

      /// <summary>Удалить объект</summary>
      /// <param name="DeleteMode">Зарезервировано</param>
      /// <returns>Зарезервировано</returns>
      int Delete(long DeleteMode);

      /// <summary>
      /// Метод завершает создание объекта.
      /// Если deleteOnException=true, то при возникновении исключения заготовка объекта
      /// будет удалена и снова нужно будет вызывать IDBObjectCollection.Create для
      /// создания заготовки объекта.
      /// </summary>
      /// <param name="deleteOnException">Если deleteOnException=true, то при возникновении исключения заготовка объекта будет удалена</param>
      void CommitCreation(bool deleteOnException);

      /// <summary>
      /// Метод завершает создание объекта, переводя заготовку в объект.
      /// </summary>
      /// <param name="deleteOnException">Удалять ли заготовку, если в процессе перевода возникло исключение.</param>
      /// <param name="autoCheckout">Нужно ли автоматически брать на изменение закомиченный объект в случае, если на первом
      /// шаге ЖЦ изменение объекта может производиться только через рабочую копию.</param>
      void CommitCreation(bool deleteOnException, bool autoCheckout);

      /// <summary>Если == true, то значит это заготовка объекта</summary>
      bool IsCreationMode { get; }

      /// <summary>
      /// Возвращает имя файла объекта, который используется для создания и проверки электронных подписей объекта.
      /// versionID - номер версии алгоритма получения данного файла.
      /// <param name="versionID">Номер версии алгоритма получения данного файла.</param>
      /// <param name="certificate">Сертификат с открытым ключом для определения алгоритма хэширования</param>
      /// <param name="setContent">true -&gt; считает хэш и заполняет hashContent; false -&gt; считает хэш по hashContent</param>
      /// <param name="hashContent">Последовательность информации в хэше</param>
      /// </summary>
      /// <returns>Имя файла объекта</returns>
      string GetHashFile(
        int versionID,
        X509Certificate2 certificate,
        bool setContent,
        IHashContent hashContent);

      /// <summary>
      /// Возвращает номер текущей версии алгоритма генерации хэш-файла функцией GetHashFile()
      /// </summary>
      /// <returns>Номер текущей версии алгоритма генерации хэш-файла функцией GetHashFile()</returns>
      int GetHashVersion();

      /// <summary>
      /// Глобальный идентификатор объекта (один для всех версий объекта)
      /// </summary>
      Guid GUID { get; set; }

      /// <summary>Глобальный идентификатор объекта данной версии</summary>
      Guid ObjectGUID { get; set; }

      /// <summary>
      /// Строка символов, определяющих предметные области, к которым относится данных
      /// объект. Если пусто, то относится ко всем областям.
      /// </summary>
      string SubjectAreas { get; }

      /// <summary>
      /// Возвращает true, если данный объект является объектом типа guid или унаследованным от этого типа
      /// </summary>
      /// <param name="guid">Guid родительской версии объекта</param>
      /// <returns>true, если данный объект является объектом типа guid или унаследованным от этого типа</returns>
      bool isParentType(Guid guid);

      /// <summary>
      /// Идентификатор версии объекта, на основе которой была создана данная версия объекта.
      /// Если это самая первая версия (или родительская версия былу удалена), то возвращает -1.
      /// </summary>
      long ParentVersionID { get; }

      /// <summary>
      /// Возвращает способ модификации объекта на текущем шаге ЖЦ
      /// </summary>
      ObjectModifyModes ObjectModifyMode { get; }

      /// <summary>
      /// Статус последней фильтрации текущего объекта по правилу подбора версий
      /// </summary>
      ObjectFiltrationState FiltrationState { get; set; }

      /// <summary>
      /// Возвращает таблицу с историей изменения шага жизненного цикла данной версии объекта
      /// </summary>
      /// <returns>Таблица с историей изменения шага жизненного цикла данной версии объекта</returns>
      DataTable GetLCHistory();

      /// <summary>
      /// Возвращает таблицу с историей изменения жизненного цикла версии объекта.
      /// Если allVersions == true, то возвращает историю всех версий данного объекта.
      /// </summary>
      /// <param name="allVersions">Если allVersions == true, то возвращает историю всех версий данного объекта.</param>
      /// <returns>Таблица с историей изменения шага жизненного цикла данной версии объекта</returns>
      DataTable GetLCHistory(bool allVersions);

      /// <summary>
      /// Проверяет можно ли редактировать свойства данного объекта (если нельзя, то генерируется исключение).
      /// </summary>
      void CheckEdit();

      /// <summary>
      /// Устанавливает новую дату модификации содержимого объекта на текущую дату и время
      /// </summary>
      void SetModifyContentDate();

      /// <summary>
      /// Проверяет можно ли редактировать исходящие связи данного объекта (если нельзя, то генерируется исключение).
      /// </summary>
      void CheckRelationsEdit();

      /// <summary>
      /// Идентификатор проекта, к которому принадлежит объект. Если == 0, то объект создан вне контекста проекта.
      /// </summary>
      long ProjectID { get; set; }

      /// <summary>Вызывается перед попыткой вывести на печать</summary>
      void Print();

      /// <summary>Вызывается перед сохранением объекта на диск</summary>
      void SaveToDisk();

      /// <summary>
      /// Получает список действий, производимых над данной версией объекта
      /// </summary>
      /// <param name="paramSet">Набор запрашиваемых колонок и их сортировка. Фильтр Conditions не учитывается.</param>
      /// <param name="translateValues">Если translateValues==true, то поля событий будут расшифрованы (имена пользователей, названия действий и т.п.).</param>
      /// <returns>Таблица со списком событий</returns>
      DataTable GetEventsList(DBRecordSetParams paramSet, bool translateValues);

      /// <summary>
      /// Получает список действий, производимых над данной версией объекта
      /// </summary>
      /// <param name="paramSet">Набор запрашиваемых колонок и их сортировка. Фильтр Conditions не учитывается.</param>
      /// <param name="translateValues">Если translateValues==true, то поля событий будут расшифрованы (имена пользователей, названия действий и т.п.).</param>
      /// <param name="archiveMode">Если archiveMode==true, то показывается архивная честь журнала, а иначе - оперативная.</param>
      /// <returns>Таблица со списком событий</returns>
      DataTable GetEventsList(DBRecordSetParams paramSet, bool translateValues, bool archiveMode);

      /// <summary>Делает данную версию базовой версией объекта</summary>
      void MakeBaseVersion();

      /// <summary>
      /// Возвращает локальную дату и время последнего взятия версии объекта на изменение (даже если изменение объекта уже завершено).
      /// Если дата null, то возвращает DateTime.MinValue
      /// </summary>
      DateTime GetCheckOutDate();

      /// <summary>
      /// Метод присваивает указанным связям данной версии объекта значения атрибутов
      /// </summary>
      /// <param name="relValues">Массив классов, описывающих связи и значения атрибутов, которые нужно этим связям присвоить.</param>
      void SetRelationsAttributes(RelationAttributeValues[] relValues);

      /// <summary>Уровень доступа объекта</summary>
      int AccessLevel { get; set; }

      /// <summary>
      /// Метод возвращает из базы полный список ссылок на данную версию объекта.
      /// </summary>
      /// <param name="attributeID">Ид. атрибута, ссылки из которого нужно найти. Если меньше нуля - возвращает ссылки из всех атрибутов.</param>
      /// <returns>Таблица со списком ссылок (в столбцах ид. версии объекта, ид. атрибута, номер значения атрибута)</returns>
      DataTable GetObjectLinks(int attributeID);

      /// <summary>
      /// Ф-ция прикрепляет заготовку объекта в качестве версии другого объекта, порождённой от версии toObjectID
      /// </summary>
      /// <param name="toObjectID">Версия объекта, к которому нужно прикрепить данную заготовку</param>
      /// <returns>Порядковый номер прикреплённой версии</returns>
      int ConnectToObject(long toObjectID);

      /// <summary>
      /// Возвращает количество неудаленных версий объекта в базе данных
      /// </summary>
      int VersionsCount { get; }

      /// <summary>
      /// Метод сбрасывает кэш с результатом предыдущих проверок прав доступа к данному объекту
      /// </summary>
      void ClearObjectAccessCache();
    }
}
