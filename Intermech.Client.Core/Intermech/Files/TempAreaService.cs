
// Type: Intermech.Files.TempAreaService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.IO;
using System.IO;
using System.Runtime.CompilerServices;


namespace Intermech.Files;

/// <summary>
/// Реализует область для временных файлов в файловом хранилище пользователя. Все методы класса являются thread-safe.
/// </summary>
internal sealed class TempAreaService(
  FileVaultService vault,
  string areaDirectory,
  string displayName) : AreaBase(vault, areaDirectory, displayName), ITempArea, IFileArea
{
  /// <summary>Выполняет инициализацию файловой области.</summary>
  internal override void Initialize()
  {
    base.Initialize();
    this.HideAreaDirectory();
  }

  /// <summary>Очищает файловую область при запуске сервиса.</summary>
  [MethodImpl(MethodImplOptions.Synchronized)]
  internal void Cleanup() => FileUtils.DeleteFilesSilently(this.areaPath, true);

  /// <summary>
  /// Генерирует и возвращает случайное имя для папки или файла.
  /// </summary>
  /// <returns>Случайное имя для папки или файла</returns>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public string GetRandomFileName()
  {
    string randomFileName;
    string path;
    do
    {
      randomFileName = Path.GetRandomFileName();
      path = Path.Combine(this.areaPath, randomFileName);
    }
    while (File.Exists(path) || Directory.Exists(path));
    return randomFileName;
  }

  /// <summary>
  /// Создает новый файл нулевой длины со случайным именем и возвращает абсолютный путь к этому файлу.
  /// </summary>
  /// <returns>Абсолютный путь к временному файлу со случайным именем</returns>
  [MethodImpl(MethodImplOptions.Synchronized)]
  public string GetTempFileName()
  {
    string path = Path.Combine(this.areaPath, this.GetRandomFileName());
    File.WriteAllText(path, string.Empty);
    return path;
  }
}
