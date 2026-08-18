
// Type: Intermech.Interfaces.Imbase.ImbaseCatalogSelectMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.Imbase
{
    /// <summary>Режим выбора объектов из каталогов Imbase</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    public enum ImbaseCatalogSelectMode
    {
      /// <summary>Выбор папки справочника</summary>
      [CustomDescription("Attribute.Interfaces_444")] imcmSelectFolder,
      /// <summary>Создание объекта по справочнику</summary>
      [CustomDescription("Attribute.Interfaces_445")] imcmCreateObject,
      /// <summary>Нет</summary>
      [CustomDescription("Attribute.Client.Core_202")] imcmNone,
      /// <summary>Выбор записи в таблице IMBASE</summary>
      /// <remarks>Создавалось для атрибута "Ссылка на таблицу IMBASE", чтобы можно было запомнить идентификатор ссылки на таблицу IMBASE и номер записи.</remarks>
      [Browsable(false)] imcmAllowSelectRow,
    }
}
