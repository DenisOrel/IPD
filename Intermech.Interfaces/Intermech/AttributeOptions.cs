
// Type: Intermech.AttributeOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Опции, регулирующие поведение атрибутов</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_151")]
    [Category("Misc")]
    [Flags]
    public enum AttributeOptions
    {
      /// <summary>Нет опций</summary>
      [CustomDescription("Attribute.Interfaces_152"), AutoUpdateParameters(false)] None = 0,
      /// <summary>Регистрировать запись атрибута в журнале</summary>
      [CustomDescription("Attribute.Interfaces_153"), AutoUpdateParameters(true)] SaveInLog = 1,
      /// <summary>Сохранять персональную историю значений</summary>
      [CustomDescription("Attribute.Interfaces_154"), AutoUpdateParameters(true)] SavePrivateHistory = 2,
      /// <summary>Сохранять общую историю значений</summary>
      [CustomDescription("Attribute.Interfaces_155"), AutoUpdateParameters(true)] SaveCommonHistory = 4,
      /// <summary>Запрет ввода пустых значений</summary>
      [CustomDescription("Attribute.Interfaces_156"), AutoUpdateParameters(true)] DisableNulls = 8,
      /// <summary>Вызывать расшифровку значений</summary>
      [CustomDescription("Attribute.Interfaces_157"), AutoUpdateParameters(true)] GetDescriptionEvent = 16, // 0x00000010
      /// <summary>Используется для хранения системной информации</summary>
      [CustomDescription("Attribute.Interfaces_158"), AutoUpdateParameters(true)] Internal = 32, // 0x00000020
      /// <summary>Возможна модификация без взятия на изменение</summary>
      [CustomDescription("Attribute.Interfaces_159"), AutoUpdateParameters(true)] ModifyInBase = 64, // 0x00000040
      /// <summary>Запрет редактирования вручную</summary>
      [CustomDescription("Attribute.Interfaces_160"), AutoUpdateParameters(true)] DisableManualEdit = 128, // 0x00000080
      /// <summary>Является идентификационным атрибутом</summary>
      [CustomDescription("Attribute.Interfaces_161"), AutoUpdateParameters(true)] Identifier = 256, // 0x00000100
      /// <summary>Запрет копирования значения у прототипа</summary>
      [CustomDescription("Attribute.Interfaces_162"), AutoUpdateParameters(true)] DontCopyPrototypeValue = 512, // 0x00000200
      /// <summary>Свободное значение 1</summary>
      [CustomDescription("Attribute.Interfaces_163"), AutoUpdateParameters(true)] FreeFlag1 = 1024, // 0x00000400
      /// <summary>Свободное значение 2</summary>
      [CustomDescription("Attribute.Interfaces_164"), AutoUpdateParameters(true)] FreeFlag2 = 2048, // 0x00000800
      /// <summary>Флаг SEARCH</summary>
      [CustomDescription("Attribute.Interfaces_165"), AutoUpdateParameters(false)] ImbaseFlag_SEARCH = 4096, // 0x00001000
      /// <summary>Флаг AVS</summary>
      [CustomDescription("Attribute.Interfaces_166"), AutoUpdateParameters(false)] ImbaseFlag_AVS = 8192, // 0x00002000
      /// <summary>Флаг CADMECH_T</summary>
      [CustomDescription("Attribute.Interfaces_167"), AutoUpdateParameters(false)] ImbaseFlag_CADMECH_T = 16384, // 0x00004000
      /// <summary>Флаг CADMECH</summary>
      [CustomDescription("Attribute.Interfaces_168"), AutoUpdateParameters(false)] ImbaseFlag_CADMECH = 32768, // 0x00008000
      /// <summary>Используется в таблицах IMBASE</summary>
      [CustomDescription("Imbase_UsedInTables"), AutoUpdateParameters(true)] ImbaseFlag_UsedInTables = 65536, // 0x00010000
      /// <summary>Содержит ссылку на запись таблицы IMBASE</summary>
      [CustomDescription("Imbase_TableRecordRef"), AutoUpdateParameters(false)] ImbaseFlag_TableRecordRef = 131072, // 0x00020000
      /// <summary>
      /// Поле используется IMH для генерации новых записей в таблице
      /// </summary>
      [CustomDescription("Imbase_IMHGen"), AutoUpdateParameters(false)] ImbaseFlag_IMHGen = 262144, // 0x00040000
      /// <summary>
      /// Разрешить вычисление владельца объекта при проверке прав доступа к атрибуту. Выключает кэширование прав доступа, замедляя проверку прав.
      /// </summary>
      [CustomDescription("EnableOwnerAccessCheck"), AutoUpdateParameters(false)] EnableOwnerAccessCheck = 524288, // 0x00080000
      /// <summary>Добавлять значения атрибута в общий поисковый индекс.</summary>
      [CustomDescription("AddToGlobalIndex"), AutoUpdateParameters(false)] AddToGlobalIndex = 1048576, // 0x00100000
      /// <summary>
      /// Запрещает разбивать значение атрибута на слова при помещении в общий поисковый индекс. Применяется для индексирования строковых атрибутов, содержащих обозначения, коды ОКП и пр.
      /// </summary>
      [CustomDescription("DisableSplitIndexValue"), AutoUpdateParameters(false)] DisableSplitIndexValue = 2097152, // 0x00200000
      /// <summary>
      /// Локальный атрибут IMBASE (используется для фильтрации атрибутов, которых было лень удалять из базы Imbase перед миграцией в IPS)
      /// </summary>
      [CustomDescription("LocalImbaseAttribute"), AutoUpdateParameters(false)] LocalImbaseAttribute = 4194304, // 0x00400000
      /// <summary>
      /// Aтрибут IMBASE , который разрешено редактировать в таблицах, импортированных из портала
      /// </summary>
      [CustomDescription("EditableLocalImbase"), AutoUpdateParameters(false)] EditableLocalImbaseAttribute = 8388608, // 0x00800000
      /// <summary>Запрет копирования значения у версии</summary>
      [CustomDescription("DontCopyVersionValue"), AutoUpdateParameters(true)] DontCopyVersionValue = 16777216, // 0x01000000
      /// <summary>
      /// Не использвать значения по умолчанию при пустом значении поля
      /// </summary>
      [CustomDescription("DontUseDefaults"), AutoUpdateParameters(true)] Imbase_DontUseDefaultsWithNull = 33554432, // 0x02000000
      /// <summary>
      /// Использвать значение для записи в свойство документа САД системы
      /// </summary>
      [CustomDescription("CadProperty"), AutoUpdateParameters(true)] ImbaseFlag_CADPROPERTY = 67108864, // 0x04000000
      /// <summary>
      /// Запрет копирования значения у прототипа для исполнения
      /// </summary>
      [CustomDescription("Attribute.Interfaces_567"), AutoUpdateParameters(true)] DontCopyPrototypeAttributeValueForArticle = 134217728, // 0x08000000
      /// <summary>Копировать значения дочернему объекту</summary>
      [CustomDescription("CopyValues2ChildObject"), AutoUpdateParameters(true)] CopyValues2ChildObject = 268435456, // 0x10000000
    }
}
