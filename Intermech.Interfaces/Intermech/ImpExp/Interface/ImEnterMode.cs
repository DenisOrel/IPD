
// Type: Intermech.ImpExp.Interface.ImEnterMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


namespace Intermech.ImpExp.Interface
{
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
      /// <summary>Ссылка на документ SEARCH"</summary>
      [Description("Ссылка на документ SEARCH")] IEM_SEARCH_DOCUMENT = 15, // 0x0000000F
      /// <summary>Ссылка на объект SEARCH</summary>
      [Description("Ссылка на объект SEARCH")] IEM_SEARCH_OBJECT = 16, // 0x00000010
      /// <summary>Глобальный идентификатор</summary>
      [Description("Глобальный идентификатор.")] IEM_GUID = 21, // 0x00000015
    }
}
