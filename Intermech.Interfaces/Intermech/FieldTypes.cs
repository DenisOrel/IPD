
// Type: Intermech.FieldTypes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Типы атрибутов:
    /// 1 - строковый, 2 - целочисленный, 3 - вещественный, 4 - дата+время,
    /// 5 - короткий блоб,
    /// 6 - файл, 7 - ссылка на запись таблицы внешней БД,
    /// 8 - ссылка на объект, 9 - пароль, 10 - текст (мемо), 11 - двоичные данные,
    /// 12 - логический тип, 13 - вещественный с единицей измерения,
    /// 14 - автоинкрементный тип, 15 - системный, 16 - глобальный идентификатор
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_103")]
    [Category("Misc")]
    public enum FieldTypes
    {
      /// <summary>Неизвестный тип</summary>
      [CustomDescription("Attribute.Interfaces_104")] ftUnknown,
      /// <summary>
      /// Строка.
      /// Смысл полей IMS_OBJECT_ATTRS и IMS_OBJECT_ATTRS для соотв. типов атрибутов:
      /// F_STRING_VALUE - строка
      /// </summary>
      [TypeOfAttributeValue(typeof (string)), RDBMSTypeOfAttributeValue(typeof (string)), CustomDescription("Attribute.Interfaces_105")] ftString,
      /// <summary>
      /// Целое число.
      /// F_INTEGER_VALUE - число
      /// </summary>
      [TypeOfAttributeValue(typeof (long)), RDBMSTypeOfAttributeValue(typeof (long)), CustomDescription("Attribute.Interfaces_106")] ftInteger,
      /// <summary>
      /// Вещественное число.
      /// F_DOUBLE_VALUE - число
      /// </summary>
      [TypeOfAttributeValue(typeof (double)), RDBMSTypeOfAttributeValue(typeof (double)), CustomDescription("Attribute.Interfaces_107")] ftDouble,
      /// <summary>
      /// Дата.
      /// F_DATE_VALUE - дата и время по Гринвичу
      /// </summary>
      [TypeOfAttributeValue(typeof (DateTime)), RDBMSTypeOfAttributeValue(typeof (DateTime)), CustomDescription("Attribute.Interfaces_108")] ftDateTime,
      /// <summary>
      /// Короткие двоичные данные.
      /// F_INTEGER_VALUE - ид. блоба в таблице IMS_BLOBS
      /// F_STRING_VALUE - комментарии к блобу
      /// F_DATE_VALUE - дата последней модификации блоба
      /// </summary>
      [CustomDescription("Attribute.Interfaces_109")] ftShortBlob,
      /// <summary>
      /// Файл.
      /// F_INTEGER_VALUE - ид. файла в таблице файлового шкафа
      /// F_STRING_VALUE - имя файла
      /// F_DATE_VALUE - дата последней модификации файла
      /// F_DOUBLE_VALUE - ObjectID файлового шкафа
      /// </summary>
      [CustomDescription("Attribute.Interfaces_110")] ftFile,
      /// <summary>
      /// Внешняя ссылка.
      /// F_INTEGER_VALUE - ид. первичного ключа в этой таблице (или хэш ключа)
      /// F_STRING_VALUE - значение атрибута
      /// F_DOUBLE_VALUE - ObjectID объекта, описывающего внешнюю таблицу БД
      /// </summary>
      [CustomDescription("Attribute.Interfaces_111")] ftExternalLink,
      /// <summary>
      /// Ссылка на версию объекта.
      /// F_INTEGER_VALUE - ObjectID объекта, на который ссылается атрибут
      /// F_STRING_VALUE - заголовок объекта
      /// </summary>
      [TypeOfAttributeValue(typeof (long)), RDBMSTypeOfAttributeValue(typeof (long)), CustomDescription("Attribute.Interfaces_112")] ftObjectLink,
      /// <summary>
      /// Пароль.
      /// F_STRING_VALUE - хэш пароля
      /// F_DATE_VALUE - дата назначения пароля
      /// </summary>
      [TypeOfAttributeValue(typeof (string)), RDBMSTypeOfAttributeValue(typeof (string)), CustomDescription("Attribute.Interfaces_113")] ftPassword,
      /// <summary>
      /// Текст.
      /// F_INTEGER_VALUE - ид. блоба в таблице IMS_MEMOS
      /// F_STRING_VALUE - первые 850 символов текста
      /// F_DATE_VALUE - дата последней модификации текста
      /// </summary>
      [TypeOfAttributeValue(typeof (string)), RDBMSTypeOfAttributeValue(typeof (string)), CustomDescription("Attribute.Interfaces_114")] ftMemo,
      /// <summary>
      /// Двоичные данные.
      /// F_INTEGER_VALUE - ид. блоба в таблице файлового шкафа
      /// F_STRING_VALUE - комментарии к блобу
      /// F_DATE_VALUE - дата последней модификации блоба
      /// F_DOUBLE_VALUE - ObjectID файлового шкафа
      /// </summary>
      [CustomDescription("Attribute.Interfaces_115")] ftBlob,
      /// <summary>
      /// Логический
      /// F_INTEGER_VALUE - 0 - false, 1 - true
      /// </summary>
      [TypeOfAttributeValue(typeof (bool)), RDBMSTypeOfAttributeValue(typeof (bool)), CustomDescription("Attribute.Interfaces_116")] ftBoolean,
      /// <summary>
      /// Вещественное число, выраженное в единицах измерения.
      /// F_INTEGER_VALUE - ид. базовой единицы измерения
      /// F_STRING_VALUE - строковое представление значения, введенное в единицах измерения, выбранных пользователем
      /// F_DOUBLE_VALUE - значение в базовой единице измерения
      /// </summary>
      [TypeOfAttributeValue(typeof (MeasuredValue)), RDBMSTypeOfAttributeValue(typeof (string)), CustomDescription("Attribute.Interfaces_117")] ftMeasured,
      /// <summary>
      /// Автоинкрементное целое число.
      /// F_INTEGER_VALUE - число
      /// </summary>
      [TypeOfAttributeValue(typeof (long)), RDBMSTypeOfAttributeValue(typeof (long)), CustomDescription("Attribute.Interfaces_118")] ftAutoInc,
      /// <summary>
      /// Системный атрибут.
      /// значения хранятся в системных таблицах в собственных полях
      /// </summary>
      [CustomDescription("Attribute.Interfaces_119")] ftSystem,
      /// <summary>
      /// Глобальный идентификатор.
      /// F_STRING_VALUE - строковое представление Guid
      /// </summary>
      [TypeOfAttributeValue(typeof (Guid)), RDBMSTypeOfAttributeValue(typeof (string)), CustomDescription("Attribute.Interfaces_120")] ftGuid,
      /// <summary>
      /// Ссылка на объект без конкретизации версии
      /// F_INTEGER_VALUE - ID объекта, на который ссылается атрибут
      /// F_STRING_VALUE - заголовок объекта
      /// </summary>
      [TypeOfAttributeValue(typeof (long)), RDBMSTypeOfAttributeValue(typeof (long)), CustomDescription("ftObjectLinkByID")] ftObjectLinkByID,
    }
}
