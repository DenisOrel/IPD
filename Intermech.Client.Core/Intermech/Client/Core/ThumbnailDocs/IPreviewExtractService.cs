
// Type: Intermech.Client.Core.ThumbnailDocs.IPreviewExtractService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>
/// Сервис для извлечения превью, слушает событие ObjectCheckIn на клиенте, после чего пытается обновить превью для документов,
/// с помощью зарегестрированных в нем "экстракторов"
/// </summary>
public interface IPreviewExtractService
{
  /// <summary>Зарегистрировать класс для извлечения превью из файла</summary>
  /// <param name="extractor"></param>
  void RegisterExtractor(IPreviewExtract extractor);

  /// <summary>
  /// Получить поддерживаемые расширения всеми зарегистрированными IPreviewExtract
  /// </summary>
  /// <returns></returns>
  string GetAllSupportExtensions();

  /// <summary>Получает картинку для указанного файла</summary>
  /// <param name="fileFullName"></param>
  /// <returns></returns>
  Image GetImage(string fileFullName);
}
