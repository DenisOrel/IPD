
// Type: Intermech.Interfaces.Document.IDocumentConverter
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Document
{
    /// <summary>
    /// загружаем документ интермех, конвертируем его и сохраняем в нужном формате
    /// </summary>
    public interface IDocumentConverter
    {
      /// <summary>Загрузить файл и сохранить его в формате xml</summary>
      /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
      /// <param name="fileName">Имя файла при сохранении</param>
      /// <param name="updateLinks">Обновлять ссылки в документе при сохранении на диск</param>
      void ConvertToXml(IDBAttribute fileAttribute, string fileName, bool updateLinks);

      /// <summary>Загрузить файл и сохранить его в формате Wmf</summary>
      /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
      /// <param name="fileName">Базовое имя файла</param>
      void ConvertToWmf(IDBAttribute fileAttribute, string fileName);

      /// <summary>Загрузить файл и сохранить его в формате Pdf</summary>
      /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
      /// <param name="fileName">Базовое имя файла</param>
      void ConvertToPdf(IDBAttribute fileAttribute, string fileName);

      /// <summary>Загрузить файл и сохранить его в формате Pdf</summary>
      /// <param name="fileAttribute">Атрибут объекта, из которого сохраняем файл</param>
      /// <param name="fileName">Базовое имя файла</param>
      /// <param name="autoStart">Запускать автоматически</param>
      void ConvertToPdf(IDBAttribute fileAttribute, string fileName, bool autoStart);
    }
}
