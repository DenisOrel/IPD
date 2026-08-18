
// Type: Intermech.Interfaces.Contexts.EditingContextSource
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Intermech.Localization;
using System;
using System.ComponentModel;


namespace Intermech.Interfaces.Contexts
{
    /// <summary>
    /// Источник информации о текущем контексте редактирования
    /// </summary>
    [TypeConverter(typeof (EnumDescConverter))]
    [Serializable]
    public enum EditingContextSource
    {
      /// <summary>
      /// Контекст редактирования независимый для элементов приложения,
      /// привязан к настройкам фильтрации, передаваемым в параметрах запросов.
      /// Последний применяемый контекст привязывается также к Guid мастер-сессии
      /// (эмуляция глобального контекста для кода, который явно не умеет работать с
      /// контекстами)
      /// </summary>
      [CustomDescription("Attribute.Interfaces_480")] WindowContext,
      /// <summary>
      /// Контекст редактирования общий для всего приложения, привязан к Guid мастер-сессии
      /// </summary>
      [CustomDescription("Attribute.Interfaces_481")] SessionContext,
    }
}
