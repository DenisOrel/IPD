
// Type: Intermech.Kernel.Search.SQLFunctions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>
    /// Функции, которые можно применять в SQL-запросах при получении списков объектов и связей
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_334")]
    [Category("SQL")]
    public enum SQLFunctions
    {
      /// <summary>Текущая дата и время</summary>
      [CustomDescription("Attribute.Interfaces_335")] Now = 1,
    }
}
