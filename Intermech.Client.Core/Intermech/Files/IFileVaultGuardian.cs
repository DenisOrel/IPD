
// Type: Intermech.Files.IFileVaultGuardian
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Files;

/// <summary>
/// Позволяет реализовать взаимодействие с сервисом защиты файлового хранилища.
/// </summary>
internal interface IFileVaultGuardian : IDisposable
{
  /// <summary>Включает защиту файлового хранилища.</summary>
  /// <param name="homePath">Путь к корню файлового хранилища</param>
  /// <param name="userFolder">Имя папки пользователя внутри файлового хранилища</param>
  /// <exception cref="T:System.Exception">В процессе включения защиты произошла ошибка</exception>
  void Initialize(string homePath, string userFolder);
}
