
// Type: Intermech.Interfaces.WebPortal.PublicationNecessary
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.WebPortal
{
    /// <summary>Необходимость публикации объекта</summary>
    public enum PublicationNecessary
    {
      /// <summary>Публикация не нужна</summary>
      None,
      /// <summary>Опубликовать объект</summary>
      Object,
      /// <summary>Опубликовать в виде контейнера атрибутов</summary>
      FCAttributes,
      /// <summary>Публикация запрещена</summary>
      Forbidden,
    }
}
