
// Type: Intermech.Client.Core.ThumbnailDocs.IPreviewExtract
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Drawing;


namespace Intermech.Client.Core.ThumbnailDocs;

/// <summary>Интерфейс для извленечения превью из файлов</summary>
public interface IPreviewExtract
{
  /// <summary>Поддерживает ли экстрактор такой файл</summary>
  /// <param name="filename">имя файла, (файла на диске нет, анализируется только имя файла)</param>
  /// <returns></returns>
  bool Supports(string filename);

  /// <summary>Метод для извлечения превью из файла</summary>
  /// <param name="filename">
  /// Имя файла на диске, из которого надо достать превью
  /// </param>
  /// <param name="image">Извлеченная картинка превью</param>
  /// <returns>
  /// OK - если превью был извлечен
  /// NotFound - превью не найден в этом файле
  /// NotSupported - этот класс не поддерживает извлечение из этих файлов
  /// </returns>
  PreviewExtractStatus ExtractPreview(string filename, out Image image);

  /// <summary>Получить поддерживаемые расширения</summary>
  /// <returns></returns>
  string GetSupportExtensions();
}
