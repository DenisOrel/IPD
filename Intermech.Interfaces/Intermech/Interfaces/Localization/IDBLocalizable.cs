
// Type: Intermech.Interfaces.Localization.IDBLocalizable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Localization
{
    /// <summary>
    /// Интерфейс, предназначенный для локализации (переводу на разные языки) объектов и метаданных
    /// </summary>
    public interface IDBLocalizable
    {
      /// <summary>
      /// Содержит строку идентификаторов языков, в которых данный объект/метаданное имеет смысл
      /// </summary>
      string Languages { get; set; }
    }
}
