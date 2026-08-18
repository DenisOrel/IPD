
// Type: Intermech.Interfaces.WebPortal.TransferedObjectCategory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Категория изменяемого объекта</summary>
    [Serializable]
    public enum TransferedObjectCategory
    {
      /// <summary>Объект</summary>
      [Description("Объект")] Object,
      /// <summary>Связь</summary>
      [Description("Связь")] Relation,
      /// <summary>Объект-ссылка</summary>
      [Description("Ссылка на объект")] ObjectLink,
      /// <summary>
      /// Объект, характеризующий пакет как автоматически транзитный
      /// </summary>
      [Description("Системная информация для автоматического транзита")] AutoTransfer,
      /// <summary>Контейнер с атрибутами объекта</summary>
      [Description("Контейнер с атрибутами объекта")] AttributesContainer,
      /// <summary>Объект в составе группы</summary>
      [Description("Объект в составе группы")] GroupObject,
      /// <summary>Связь в группе</summary>
      [Description("Связь в составе группы")] GroupRelation,
      /// <summary>Квитанция</summary>
      [Description("Квитанция")] Receipt,
      /// <summary>Объект в составе пакета</summary>
      [Description("Объект в составе пакета")] PacketObject,
      /// <summary>Связь в пакете</summary>
      [Description("Связь в составе пакета")] PacketRelation,
      /// <summary>Пакет</summary>
      [Description("Пакет")] Packet,
      /// <summary>Связь-пустышка к неполному ссылочному объекту</summary>
      [Description("Связь-пустышка к неполному ссылочному объекту")] IncompleteRelation,
    }
}
