
// Type: Intermech.Interfaces.IFormDesignerFormLinksImages
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для отображения иконок для ссылки реализуется у провайдера.
    /// </summary>
    public interface IFormDesignerFormLinksImages
    {
      /// <summary>
      /// Получить изображение для ссылок в провайдере реализуется только у провайдера.
      /// </summary>
      /// <param name="imageList">imageList</param>
      void GetLinkImages(object imageList);
    }
}
