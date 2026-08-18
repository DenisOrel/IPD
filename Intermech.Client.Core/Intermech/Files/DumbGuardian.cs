
// Type: Intermech.Files.DumbGuardian
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.IO;


namespace Intermech.Files;

/// <summary>
/// Используется, когда сервис защиты файлового хранилища не установлен на компьютере.
/// </summary>
internal sealed class DumbGuardian : IFileVaultGuardian, IDisposable
{
  /// <summary>Включает защиту файлового хранилища.</summary>
  /// <param name="homePath">Путь к корню файлового хранилища</param>
  /// <param name="userFolder">Имя папки пользователя внутри файлового хранилища</param>
  /// <exception cref="T:System.Exception">В процессе включения защиты произошла ошибка</exception>
  public void Initialize(string homePath, string userFolder)
  {
    try
    {
      string path = Path.Combine(homePath, userFolder);
      if (Directory.Exists(path))
        return;
      Directory.CreateDirectory(path);
    }
    catch (Exception ex)
    {
      throw new Exception(LocalizationHolder.rm.GetString("Client.Core_1284"), ex);
    }
  }

  public void Dispose()
  {
  }
}
