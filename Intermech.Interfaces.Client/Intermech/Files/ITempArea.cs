// Decompiled with JetBrains decompiler
// Type: Intermech.Files.ITempArea
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Область для временных файлов в файловом хранилище пользователя. Все методы интерфейса являются thread-safe.
/// </summary>
public interface ITempArea : IFileArea
{
  /// <summary>
  /// Генерирует и возвращает случайное имя для папки или файла.
  /// </summary>
  /// <returns>Случайное имя для папки или файла</returns>
  string GetRandomFileName();

  /// <summary>
  /// Создает новый файл нулевой длины со случайным именем и возвращает абсолютный путь к этому файлу.
  /// </summary>
  /// <returns>Абсолютный путь к временному файлу со случайным именем</returns>
  string GetTempFileName();
}
