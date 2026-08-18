
// Type: Intermech.Interfaces.WebPortal.RelationMigrateType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Передача связей типа через портал</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Interfaces_Portal_16")]
    [Serializable]
    public enum RelationMigrateType
    {
      /// <summary>Не передается через портал</summary>
      [CustomDescription("Interfaces_Portal_17")] None,
      /// <summary>Зависит от настройки конкретной задачи</summary>
      [CustomDescription("Interfaces_Portal_18")] DependsSetting,
      /// <summary>Всегда передается через портал</summary>
      [CustomDescription("Interfaces_Portal_19")] Always,
    }
}
