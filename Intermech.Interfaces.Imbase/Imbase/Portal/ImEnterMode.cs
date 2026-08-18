// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Portal.ImEnterMode
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase.Portal;

/// <summary>
/// Режим ввода данных в поле таблицы IMBASE (т.е.каким способом заполняется или вычисляется поле в таблице)
/// </summary>
public enum ImEnterMode
{
  /// <summary>Тип ввода данных не известен или не определен</summary>
  [Description("Тип ввода данных не известен или не определен")] IEM_UNKNOWN = 0,
  /// <summary>Обычное поле. Данные вводятся с клавиатуры</summary>
  [Description("Обычное поле. Данные вводятся с клавиатуры")] IEM_SIMPLE = 1,
  /// <summary>
  /// Обычное поле. Данные вводятся с клавиатуры или выбираются из списка
  /// </summary>
  [Description("Обычное поле. Данные вводятся с клавиатуры или выбираются из списка.")] IEM_LIST = 2,
  /// <summary>Данные выбираются только из списка</summary>
  [Description("Данные выбираются только из списка.")] IEM_LISTONLY = 3,
  /// <summary>
  /// Вычисляемое поле. Данные из записи в Каталоге из поля с аналогичным длинным именем
  /// </summary>
  [Description("Вычисляемое поле. Данные из записи в Каталоге из поля с аналогичным длинным именем.")] IEM_ASPARENT = 4,
  /// <summary>
  /// Вычисляемое поле. Данные по формуле (для целых и вещ.) либо как макроподстановка (для строковых)
  /// </summary>
  [Description("Вычисляемое поле. Данные по формуле (для целых и вещ.) либо как макроподстановка (для строковых)")] IEM_EXPRESSION = 5,
  /// <summary>Ссылка на папку IMBASE</summary>
  [Description("Ссылка на папку IMBASE.")] IEM_FOLDER = 6,
  /// <summary>Ссылка на таблицу IMBASE</summary>
  [Description("Ссылка на таблицу IMBASE.")] IEM_TABLE = 7,
  /// <summary>Битовый набор</summary>
  [Description("Битовый набор.")] IEM_BITSET = 8,
  /// <summary>Символьный набор</summary>
  [Description("Символьный набор.")] IEM_CHARSET = 9,
  /// <summary>Логическое поле. Отображается в виде \"Y\" или \"N\"</summary>
  [Description("Логическое поле. Отображается в виде \"Y\" или \"N\"")] IEM_BOOLYN = 10, // 0x0000000A
  /// <summary>Логическое поле. Отображается в виде \"+\" или \"-\"</summary>
  [Description("Логическое поле. Отображается в виде \"+\" или \"-\"")] IEM_BOOLPM = 11, // 0x0000000B
  /// <summary>
  /// Логическое поле. Отображается в виде символов, определенных пользователем.
  /// </summary>
  [Description("Логическое поле. Отображается в виде символов, определенных пользователем.")] IEM_BOOLOTHER = 12, // 0x0000000C
  /// <summary>
  /// Ссылка на запись из другой таблицы. В поле хранится ключ IMBASE
  /// </summary>
  [Description("Ссылка на запись из другой таблицы. В поле хранится ключ IMBASE.")] IEM_RECORD = 13, // 0x0000000D
  /// <summary>Строковый набор</summary>
  [Description("Строковый набор.")] IEM_STRSET = 14, // 0x0000000E
  /// <summary>Глобальный идентификатор</summary>
  [Description("Глобальный идентификатор.")] IEM_GUID = 21, // 0x00000015
}
