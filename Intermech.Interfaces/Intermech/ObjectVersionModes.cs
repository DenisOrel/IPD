
// Type: Intermech.ObjectVersionModes
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System.ComponentModel;


namespace Intermech
{
    /// <summary>
    /// Управление версионностью типов объектов
    /// 0 - абстрактный тип объекта (контейнер для группировки других типов объектов);
    /// 1 - объекты данного типа не могут иметь версий;
    /// 2 - объекты данного типа могут иметь версии.
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [CustomDescription("Attribute.Interfaces_188")]
    [Category("Misc")]
    public enum ObjectVersionModes
    {
      /// <summary>Абстрактный тип</summary>
      [CustomDescription("Attribute.Interfaces_189")] Abstract,
      /// <summary>Неверсионный тип</summary>
      [CustomDescription("Attribute.Interfaces_190")] SingleVersion,
      /// <summary>Версионный тип</summary>
      [CustomDescription("Attribute.Interfaces_191")] MultiVersion,
    }
}
