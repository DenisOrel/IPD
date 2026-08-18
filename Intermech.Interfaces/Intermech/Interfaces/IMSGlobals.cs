
// Type: Intermech.Interfaces.IMSGlobals
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Перечислитель позволяет определить, к какому типу относится какой-то элемент метаданных
    /// </summary>
    [Serializable]
    public enum IMSGlobals
    {
      /// <summary>Неизвестные метаданные</summary>
      Unknown = 0,
      /// <summary>Тип атрибута</summary>
      IMSAttributeType = 1,
      /// <summary>Группа атрибутов</summary>
      IMSAttributeGroup = 2,
      /// <summary>Уровень продвижения</summary>
      IMSLifeCycleLevel = 10, // 0x0000000A
      /// <summary>Схема жизненного цикла</summary>
      IMSLifeCycleScheme = 11, // 0x0000000B
      /// <summary>Шаг жизненного цикла</summary>
      IMSLifeCycleStep = 12, // 0x0000000C
      /// <summary>Тип объекта</summary>
      IMSObjectType = 20, // 0x00000014
      /// <summary>Тип связи</summary>
      IMSRelationType = 30, // 0x0000001E
    }
}
