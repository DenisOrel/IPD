// Decompiled with JetBrains decompiler
// Type: Intermech.IpsXmlViewer.Interfaces.FieldTypes
// Assembly: Intermech.IpsXmlViewer.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 35FFF223-7A37-420F-9D15-CF4A93D8C384
// Assembly location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.IpsXmlViewer.Interfaces.xml

#nullable disable
namespace Intermech.IpsXmlViewer.Interfaces;

/// <summary>
/// Типы атрибутов:
/// 1 - строковый, 2 - целочисленный, 3 - вещественный, 4 - дата+время,
/// 5 - короткий блоб,
/// 6 - файл, 7 - ссылка на запись таблицы внешней БД,
/// 8 - ссылка на объект, 9 - пароль, 10 - текст (мемо), 11 - двоичные данные,
/// 12 - логический тип, 13 - вещественный с единицей измерения,
/// 14 - автоинкрементный тип, 15 - системный, 16 - глобальный идентификатор
/// </summary>
public enum FieldTypes
{
  /// <summary>Неизвестный тип</summary>
  ftUnknown,
  /// <summary>
  /// Строка.
  /// Смысл полей IMS_OBJECT_ATTRS и IMS_OBJECT_ATTRS для соотв. типов атрибутов:
  /// F_STRING_VALUE - строка
  /// </summary>
  ftString,
  /// <summary>
  /// Целое число.
  /// F_INTEGER_VALUE - число
  /// </summary>
  ftInteger,
  /// <summary>
  /// Вещественное число.
  /// F_DOUBLE_VALUE - число
  /// </summary>
  ftDouble,
  /// <summary>
  /// Дата.
  /// F_DATE_VALUE - дата и время по Гринвичу
  /// </summary>
  ftDateTime,
  /// <summary>
  /// Короткие двоичные данные.
  /// F_INTEGER_VALUE - ид. блоба в таблице IMS_BLOBS
  /// F_STRING_VALUE - комментарии к блобу
  /// F_DATE_VALUE - дата последней модификации блоба
  /// </summary>
  ftShortBlob,
  /// <summary>
  /// Файл.
  /// F_INTEGER_VALUE - ид. файла в таблице файлового шкафа
  /// F_STRING_VALUE - имя файла
  /// F_DATE_VALUE - дата последней модификации файла
  /// F_DOUBLE_VALUE - ObjectID файлового шкафа
  /// </summary>
  ftFile,
  /// <summary>
  /// Внешняя ссылка.
  /// F_INTEGER_VALUE - ид. первичного ключа в этой таблице (или хэш ключа)
  /// F_STRING_VALUE - значение атрибута
  /// F_DOUBLE_VALUE - ObjectID объекта, описывающего внешнюю таблицу БД
  /// </summary>
  ftExternalLink,
  /// <summary>
  /// Ссылка на объект.
  /// F_INTEGER_VALUE - ObjectID объекта, на который ссылается атрибут
  /// F_STRING_VALUE - заголовок объекта
  /// </summary>
  ftObjectLink,
  /// <summary>
  /// Пароль.
  /// F_STRING_VALUE - хэш пароля
  /// F_DATE_VALUE - дата назначения пароля
  /// </summary>
  ftPassword,
  /// <summary>
  /// Текст.
  /// F_INTEGER_VALUE - ид. блоба в таблице IMS_MEMOS
  /// F_STRING_VALUE - первые 450 символов текста
  /// F_DATE_VALUE - дата последней модификации текста
  /// </summary>
  ftMemo,
  /// <summary>
  /// Двоичные данные.
  /// F_INTEGER_VALUE - ид. блоба в таблице файлового шкафа
  /// F_STRING_VALUE - комментарии к блобу
  /// F_DATE_VALUE - дата последней модификации блоба
  /// F_DOUBLE_VALUE - ObjectID файлового шкафа
  /// </summary>
  ftBlob,
  /// <summary>
  /// Логический
  /// F_INTEGER_VALUE - 0 - false, 1 - true
  /// </summary>
  ftBoolean,
  /// <summary>
  /// Вещественное число, выраженное в единицах измерения.
  /// F_INTEGER_VALUE - ид. базовой единицы измерения
  /// F_STRING_VALUE - строковое представление значения, введенное в единицах измерения, выбранных пользователем
  /// F_DOUBLE_VALUE - значение в базовой единице измерения
  /// </summary>
  ftMeasured,
  /// <summary>
  /// Автоинкрементное целое число.
  /// F_INTEGER_VALUE - число
  /// </summary>
  ftAutoInc,
  /// <summary>
  /// Системный атрибут.
  /// значения хранятся в системных таблицах в собственных полях
  /// </summary>
  ftSystem,
  /// <summary>
  /// Глобальный идентификатор.
  /// F_STRING_VALUE - строковое представление Guid
  /// </summary>
  ftGuid,
  /// <summary>
  /// Ссылка на объект без конкретизации версии
  /// F_INTEGER_VALUE - ID объекта, на который ссылается атрибут
  /// F_STRING_VALUE - заголовок объекта
  /// </summary>
  ftObjectLinkByID,
}
