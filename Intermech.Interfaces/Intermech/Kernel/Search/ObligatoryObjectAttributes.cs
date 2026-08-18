
// Type: Intermech.Kernel.Search.ObligatoryObjectAttributes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_375")]
    [Category("SQL")]
    public enum ObligatoryObjectAttributes
    {
      /// <summary>Дата изменения шага ЖЦ</summary>
      [CustomDescription("Attribute.LCStepDate"), SourceType(AttributeSourceTypes.Object)] F_LCSTEP_DATE = -87, // 0xFFFFFFA9
      /// <summary>Количество входимостей в версии объектов</summary>
      [CustomDescription("Attribute.RelationsCount"), SourceType(AttributeSourceTypes.Object)] F_RELATIONS_COUNT = -86, // 0xFFFFFFAA
      /// <summary>Количество ссылок на версию объекта</summary>
      [CustomDescription("Attribute.ReferencesCount"), SourceType(AttributeSourceTypes.Object)] F_REFERENCE_COUNT = -85, // 0xFFFFFFAB
      /// <summary>Количество версий объекта</summary>
      [CustomDescription("Attribute.VersionsCount"), SourceType(AttributeSourceTypes.Object)] F_VERSIONS_COUNT = -84, // 0xFFFFFFAC
      /// <summary>Идентификатор родительской версии объекта</summary>
      [CustomDescription("Attribute.ParentObjectID"), SourceType(AttributeSourceTypes.Object)] F_PARENT_OBJECT_ID = -83, // 0xFFFFFFAD
      /// <summary>Создатель связи</summary>
      [CustomDescription("Attribute.RelationCreatorID"), SourceType(AttributeSourceTypes.Relation)] F_REL_CREATOR = -82, // 0xFFFFFFAE
      /// <summary>Создатель объекта</summary>
      [CustomDescription("Attribute.CreatorID"), SourceType(AttributeSourceTypes.Object)] F_CREATOR_ID = -81, // 0xFFFFFFAF
      /// <summary>Уровень доступа объекта</summary>
      [CustomDescription("Attribute.Access"), SourceType(AttributeSourceTypes.Object)] F_ACCESS = -80, // 0xFFFFFFB0
      /// <summary>Дата создания итерации</summary>
      [CustomDescription("Attribute.Interfaces_561"), SourceType(AttributeSourceTypes.Snapshot)] F_SNAPSHOT_DATE = -79, // 0xFFFFFFB1
      /// <summary>
      /// Идентификатор итерации (для случаев получения сохранённых в итерации объектов)
      /// </summary>
      [CustomDescription("Attribute.Interfaces_560"), SourceType(AttributeSourceTypes.Snapshot)] F_SNAPSHOT_ID = -78, // 0xFFFFFFB2
      /// <summary>
      /// Статусы элемента (объекта, связи, события, файла) после его дополнительной обработки ядром и плагинами
      /// </summary>
      [CustomDescription("Attribute.Interfaces_426"), SourceType(AttributeSourceTypes.Other)] F_ELEMENT_STATUSES = -77, // 0xFFFFFFB3
      /// <summary>Метод упаковки файла</summary>
      [CustomDescription("Attribute.Interfaces_425"), SourceType(AttributeSourceTypes.FileStorage)] F_ARC_METHOD = -76, // 0xFFFFFFB4
      /// <summary>Идентификатор объекта/связи</summary>
      [CustomDescription("Attribute.Interfaces_424"), SourceType(AttributeSourceTypes.FileStorage)] F_OBJECTLINK_ID = -75, // 0xFFFFFFB5
      /// <summary>Упакованный размер файла</summary>
      [CustomDescription("Attribute.Interfaces_423"), SourceType(AttributeSourceTypes.FileStorage)] F_ZIPSIZE = -74, // 0xFFFFFFB6
      /// <summary>Дата обновления файла</summary>
      [CustomDescription("Attribute.Interfaces_422"), SourceType(AttributeSourceTypes.FileStorage)] F_FILEDATE = -73, // 0xFFFFFFB7
      /// <summary>Размер файла</summary>
      [CustomDescription("Attribute.Interfaces_421"), SourceType(AttributeSourceTypes.FileStorage)] F_FILESIZE = -72, // 0xFFFFFFB8
      /// <summary>Имя файла</summary>
      [CustomDescription("Attribute.Interfaces_420"), SourceType(AttributeSourceTypes.FileStorage)] F_FILENAME = -71, // 0xFFFFFFB9
      /// <summary>Идентификатор файла</summary>
      [CustomDescription("Attribute.Interfaces_419"), SourceType(AttributeSourceTypes.FileStorage)] F_FILE_ID = -70, // 0xFFFFFFBA
      /// <summary>Результат подбора версий</summary>
      [CustomDescription("Attribute.Interfaces_418"), SourceType(AttributeSourceTypes.Other)] F_VERSION_RESULT = -60, // 0xFFFFFFC4
      /// <summary>Идентификатор атрибута</summary>
      [CustomDescription("Attribute.Interfaces_417"), SourceType(AttributeSourceTypes.History)] F_ATTRIBUTE_ID = -58, // 0xFFFFFFC6
      /// <summary>Ключ</summary>
      [CustomDescription("Attribute.Interfaces_416"), SourceType(AttributeSourceTypes.History)] F_KEY = -57, // 0xFFFFFFC7
      /// <summary>Тип данных 'Дата'</summary>
      [CustomDescription("Attribute.Interfaces_415"), SourceType(AttributeSourceTypes.History)] F_DATE_VALUE = -56, // 0xFFFFFFC8
      /// <summary>Тип данных 'Вещественное число'</summary>
      [CustomDescription("Attribute.Interfaces_414"), SourceType(AttributeSourceTypes.History)] F_DOUBLE_VALUE = -55, // 0xFFFFFFC9
      /// <summary>Тип данных 'Строка'</summary>
      [CustomDescription("Attribute.Interfaces_413"), SourceType(AttributeSourceTypes.History)] F_STRING_VALUE = -54, // 0xFFFFFFCA
      /// <summary>Тип данных 'Целое число'</summary>
      [CustomDescription("Attribute.Interfaces_412"), SourceType(AttributeSourceTypes.History)] F_INTEGER_VALUE = -53, // 0xFFFFFFCB
      /// <summary>Статус записи в истории значений</summary>
      [CustomDescription("Attribute.Interfaces_411"), SourceType(AttributeSourceTypes.History)] F_STATUS = -52, // 0xFFFFFFCC
      /// <summary>Дата присвоения значения</summary>
      [CustomDescription("Attribute.Interfaces_410"), SourceType(AttributeSourceTypes.History)] F_SET_DATE = -51, // 0xFFFFFFCD
      /// <summary>Заголовок объекта</summary>
      [CustomDescription("Attribute.Interfaces_409"), SourceType(AttributeSourceTypes.Object)] CAPTION = -50, // 0xFFFFFFCE
      /// <summary>Дата формирования связей</summary>
      [CustomDescription("Attribute.Interfaces_408"), SourceType(AttributeSourceTypes.Other)] F_ACTUAL_DATE = -43, // 0xFFFFFFD5
      /// <summary>Тип события</summary>
      [CustomDescription("Attribute.Interfaces_407"), SourceType(AttributeSourceTypes.Events)] F_AUDIT_TYPE = -42, // 0xFFFFFFD6
      /// <summary>Завершение события</summary>
      [CustomDescription("Attribute.Interfaces_406"), SourceType(AttributeSourceTypes.Events)] F_END_DATE = -41, // 0xFFFFFFD7
      /// <summary>Начало события</summary>
      [CustomDescription("Attribute.Interfaces_405"), SourceType(AttributeSourceTypes.Events)] F_BEGIN_DATE = -40, // 0xFFFFFFD8
      /// <summary>Вид действия</summary>
      [CustomDescription("Attribute.Interfaces_404"), SourceType(AttributeSourceTypes.Events)] F_EVENT_TYPE = -39, // 0xFFFFFFD9
      /// <summary>Комментарии</summary>
      [CustomDescription("Attribute.Interfaces_403"), SourceType(AttributeSourceTypes.Events)] F_NOTE = -38, // 0xFFFFFFDA
      /// <summary>Имя компьютера</summary>
      [CustomDescription("Attribute.Interfaces_402"), SourceType(AttributeSourceTypes.Events)] F_COMPUTER_NAME = -37, // 0xFFFFFFDB
      /// <summary>Пользователь</summary>
      [CustomDescription("Attribute.Interfaces_401"), SourceType(AttributeSourceTypes.Events)] F_USER_ID = -36, // 0xFFFFFFDC
      /// <summary>Имя объекта</summary>
      [CustomDescription("Attribute.Interfaces_400"), SourceType(AttributeSourceTypes.Events)] F_OBJECT_NAME = -35, // 0xFFFFFFDD
      /// <summary>ID связи</summary>
      [CustomDescription("Attribute.Interfaces_399"), SourceType(AttributeSourceTypes.Events)] F_RELATION_ID = -34, // 0xFFFFFFDE
      /// <summary>ID категории</summary>
      [CustomDescription("Attribute.Interfaces_398"), SourceType(AttributeSourceTypes.Events)] F_CATEGORY_ID = -32, // 0xFFFFFFE0
      /// <summary>Категория</summary>
      [CustomDescription("Attribute.Interfaces_397"), SourceType(AttributeSourceTypes.Events)] F_CATEGORY_TYPE = -31, // 0xFFFFFFE1
      /// <summary>ID события</summary>
      [CustomDescription("Attribute.Interfaces_396"), SourceType(AttributeSourceTypes.Events)] F_EVENT_ID = -30, // 0xFFFFFFE2
      /// <summary>Глобальный идентификатор связи</summary>
      [CustomDescription("Attribute.Interfaces_395"), SourceType(AttributeSourceTypes.Relation)] F_PRJ_GUID = -26, // 0xFFFFFFE6
      /// <summary>Дата завершения действия связи</summary>
      [CustomDescription("Attribute.Interfaces_394"), SourceType(AttributeSourceTypes.Other), Obsolete] F_DELETE_DATE = -25, // 0xFFFFFFE7
      /// <summary>Дата начала действия связи</summary>
      [CustomDescription("Attribute.Interfaces_393"), SourceType(AttributeSourceTypes.Relation)] F_CREATE_DATE = -24, // 0xFFFFFFE8
      /// <summary>Тип связи</summary>
      [CustomDescription("Attribute.Interfaces_392"), SourceType(AttributeSourceTypes.Relation)] F_RELATION_TYPE = -23, // 0xFFFFFFE9
      /// <summary>Идентификатор дочернего объекта</summary>
      [CustomDescription("Attribute.Interfaces_391"), SourceType(AttributeSourceTypes.Relation)] F_PART_ID = -22, // 0xFFFFFFEA
      /// <summary>Идентификатор родительского объекта</summary>
      [CustomDescription("Attribute.Interfaces_390"), SourceType(AttributeSourceTypes.Relation)] F_PROJ_ID = -21, // 0xFFFFFFEB
      /// <summary>Идентификатор связи</summary>
      [CustomDescription("Attribute.Interfaces_389"), SourceType(AttributeSourceTypes.Relation)] F_PRJLINK_ID = -20, // 0xFFFFFFEC
      /// <summary>Состояние версии объекта</summary>
      [CustomDescription("Attribute.ObjVerType"), SourceType(AttributeSourceTypes.Object)] F_OBJECT_VER_TYPE = -19, // 0xFFFFFFED
      /// <summary>Глобальный идентификатор объекта</summary>
      [CustomDescription("Attribute.ObjGUID"), SourceType(AttributeSourceTypes.Object)] F_OBJ_GUID = -18, // 0xFFFFFFEE
      /// <summary>Узел информационной сети</summary>
      [CustomDescription("Attribute.SiteID"), SourceType(AttributeSourceTypes.Object)] F_SITE_ID = -17, // 0xFFFFFFEF
      /// <summary>Признак базовой версии</summary>
      [CustomDescription("Attribute.BaseVersion"), SourceType(AttributeSourceTypes.Object)] F_BASE_VERSION = -16, // 0xFFFFFFF0
      /// <summary>Номер группы изменений</summary>
      [CustomDescription("Attribute.ModificationID"), SourceType(AttributeSourceTypes.Object)] F_MODIFICATION_ID = -15, // 0xFFFFFFF1
      /// <summary>Принадлежность проекту</summary>
      [CustomDescription("Attribute.Interfaces_388"), SourceType(AttributeSourceTypes.Object)] F_PROJECT_ID = -14, // 0xFFFFFFF2
      /// <summary>Дата создания объекта</summary>
      [CustomDescription("Attribute.Interfaces_387"), SourceType(AttributeSourceTypes.Object)] F_OBJ_CREATE = -13, // 0xFFFFFFF3
      /// <summary>Глобальный идентификатор версии объекта</summary>
      [CustomDescription("Attribute.Interfaces_386"), SourceType(AttributeSourceTypes.Object)] F_GUID = -12, // 0xFFFFFFF4
      /// <summary>Предметная область</summary>
      [CustomDescription("Attribute.Interfaces_385"), SourceType(AttributeSourceTypes.Other)] F_AREA_ID = -11, // 0xFFFFFFF5
      /// <summary>Дата модификации объекта</summary>
      [CustomDescription("Attribute.Interfaces_384"), SourceType(AttributeSourceTypes.Object)] F_MODIFY_DATE = -10, // 0xFFFFFFF6
      /// <summary>Уровень продвижения объекта</summary>
      [CustomDescription("Attribute.Interfaces_383"), SourceType(AttributeSourceTypes.Object)] F_LEVEL_ID = -9, // 0xFFFFFFF7
      /// <summary>Владелец объекта</summary>
      [CustomDescription("Attribute.Interfaces_382"), SourceType(AttributeSourceTypes.Object)] F_OWNER_ID = -8, // 0xFFFFFFF8
      /// <summary>Тип объекта</summary>
      [CustomDescription("Attribute.Interfaces_381"), SourceType(AttributeSourceTypes.Object)] F_OBJECT_TYPE = -7, // 0xFFFFFFF9
      /// <summary>Кем взят на изменение</summary>
      [CustomDescription("Attribute.Interfaces_380"), SourceType(AttributeSourceTypes.Object)] F_CHKOUT_BY = -6, // 0xFFFFFFFA
      /// <summary>Номер версии объекта</summary>
      [CustomDescription("Attribute.Interfaces_379"), SourceType(AttributeSourceTypes.Object)] F_VERSION_ID = -5, // 0xFFFFFFFB
      /// <summary>Шаг жизненного цикла</summary>
      [CustomDescription("Attribute.Interfaces_378"), SourceType(AttributeSourceTypes.Object)] F_LC_STEP = -4, // 0xFFFFFFFC
      /// <summary>Идентификатор объекта</summary>
      [CustomDescription("Attribute.Interfaces_377"), SourceType(AttributeSourceTypes.Object)] F_ID = -3, // 0xFFFFFFFD
      /// <summary>Идентификатор версии объекта</summary>
      [CustomDescription("Attribute.Interfaces_376"), SourceType(AttributeSourceTypes.Object)] F_OBJECT_ID = -2, // 0xFFFFFFFE
      /// <summary>Зарезервировано</summary>
      [Description(""), SourceType(AttributeSourceTypes.Other)] None = -1, // 0xFFFFFFFF
      /// <summary>Зарезервировано</summary>
      [Description(""), SourceType(AttributeSourceTypes.Other)] Zero = 0,
    }
}
