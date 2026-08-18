
// Type: Intermech.Kernel.Search.SortOrders
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Kernel.Search
{
    /// <summary>Порядок сортировки</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_371")]
    [Category("SQL")]
    public enum SortOrders
    {
      /// <summary>Не сортировать</summary>
      [CustomDescription("Attribute.Interfaces_372")] NONE,
      /// <summary>Сортировать по возрастанию</summary>
      [CustomDescription("Attribute.Interfaces_373")] ASC,
      /// <summary>Сортировать по убыванию</summary>
      [CustomDescription("Attribute.Interfaces_374")] DESC,
    }
}
