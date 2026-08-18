
// Type: Intermech.Search.GroupAttributesChanging.ObjectBlankStatuses
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.ComponentModel;


namespace Intermech.Search.GroupAttributesChanging
{
    [Flags]
    public enum ObjectBlankStatuses
    {
      None = 0,
      [Description("Ошибка применения атрибута")] Error = 1,
      [Description("Атрибуты успешно применены")] Sussess = 2,
      [Description("Создается копия существующего объекта")] Copy = 4,
      [Description("Объект попал в список, так как является исполнением")] Instance = 8,
    }
}
