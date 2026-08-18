
// Type: Intermech.RelationTypeOptions
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>Опции, регулирующие поведение типов связей</summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("RelationTypeOptions")]
    [Category("Misc")]
    [Flags]
    public enum RelationTypeOptions
    {
      /// <summary>Нет опций</summary>
      [CustomDescription("Attribute.Interfaces_147")] None = 0,
      /// <summary>Разрешить циклические связи данного типа</summary>
      [CustomDescription("EnableCycleRelations")] EnableCycleRelations = 1,
      /// <summary>Проверять аннулирование объектов</summary>
      [CustomDescription("EnableCheckAnnulment")] EnableCheckAnnulment = 2,
    }
}
