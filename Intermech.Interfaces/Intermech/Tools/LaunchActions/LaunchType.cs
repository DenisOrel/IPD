
// Type: Intermech.Tools.LaunchActions.LaunchType
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Tools.LaunchActions
{
    /// <summary>Тип запуска инструмента</summary>
    [Serializable]
    public enum LaunchType
    {
      /// <summary>Редактирование</summary>
      [CustomDescription("Attribute.Interfaces_488")] Edit,
      /// <summary>Просмотр</summary>
      [CustomDescription("Attribute.Interfaces_489")] View,
      /// <summary>Печать</summary>
      [CustomDescription("Attribute.Interfaces_490")] Print,
    }
}
