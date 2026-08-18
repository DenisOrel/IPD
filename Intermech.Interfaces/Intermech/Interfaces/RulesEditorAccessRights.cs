
// Type: Intermech.Interfaces.RulesEditorAccessRights
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;


namespace Intermech.Interfaces
{
    /// <summary>Права доступа к коллекции правил сортировки составов</summary>
    [Flags]
    [Serializable]
    public enum RulesEditorAccessRights
    {
      /// <summary>Только просмотр правил</summary>
      [CustomDescription("Attribute.Interfaces_1")] ReadOnly = 0,
      /// <summary>
      /// Пользователь может назначать текущее правило сортировки в коллекции
      /// (при этом у него может не быть прав на редактирование самих правил)
      /// </summary>
      [CustomDescription("Attribute.Interfaces_2")] CanSelectCurrentRule = 1,
      /// <summary>
      /// Пользователь может выполнять изменение положения дочерних типов объектов в текущем правиле
      /// </summary>
      [CustomDescription("Attribute.Interfaces_3")] CanModifyChildObjects = 16, // 0x00000010
      /// <summary>
      /// Пользователь может выполнять изменение текущего правила сортировки
      /// (добавлять/изменять родительские типы объектов, менять положение допустимых типов связей и дочерних типов объектов)
      /// </summary>
      [CustomDescription("Attribute.Interfaces_4")] CanModifyCurrentRule = 48, // 0x00000030
      /// <summary>Полный доступ к коллекции правил сортировки составов</summary>
      [CustomDescription("Attribute.Interfaces_5")] FullAccess = 113, // 0x00000071
    }
}
