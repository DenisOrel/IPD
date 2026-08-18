
// Type: Intermech.Interfaces.Contexts.EditingContextMode
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>Режим работы текущего контекста редактирования</summary>
    [Serializable]
    public enum EditingContextMode
    {
      /// <summary>
      /// Контекст работает в обычном режиме (без автоматического обновления своего содержимого)
      /// </summary>
      [TypeConverter(typeof (EnumDescConverter)), CustomDescription("Attribute.Interfaces_437")] Default = 1,
      /// <summary>
      /// Контекст работает в режиме автоматического обновления своего содержимого
      /// (отслеживает выпуск новых версий, взятие на изменение, т.п.)
      /// </summary>
      [TypeConverter(typeof (EnumDescConverter)), CustomDescription("Attribute.Interfaces_438")] AutoUpdate = 2,
    }
}
