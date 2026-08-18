
// Type: Intermech.Search.CompositionContexts.CompositionContextEnum
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.ComponentModel;


namespace Intermech.Search.CompositionContexts
{
    /// <summary>
    /// Этот енам нельзя использовать в качестве списка всех контекстов,
    /// т.к. этот список может изменяться согласно значениям атрибута Контекст состава.
    /// Все значения атрибута можно получить через CompositionContextClientHelper.AllContexts
    /// </summary>
    public enum CompositionContextEnum : long
    {
      [Description("Общий")] Common,
      [Description("Конструкторский")] Desing,
      [Description("Технологический")] Technological,
      [Description("Производственный")] Manufacturing,
    }
}
