
// Type: Intermech.Interfaces.WebPortal.TaskPriority
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Приоритет задачи</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_494")]
    [Category("Misc")]
    public enum TaskPriority
    {
      /// <summary>Низкий</summary>
      [CustomDescription("Attribute.Interfaces_495")] Low = -1, // 0xFFFFFFFF
      /// <summary>Обычный</summary>
      [CustomDescription("Attribute.Interfaces_496")] Normal = 0,
      /// <summary>Высокий</summary>
      [CustomDescription("Attribute.Interfaces_497")] Hight = 1,
    }
}
