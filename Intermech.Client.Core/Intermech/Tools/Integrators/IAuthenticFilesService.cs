
// Type: Intermech.Tools.Integrators.IAuthenticFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections.Generic;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис интегратора, отвечающий за создание аутентичных файлов для документов приложения, с которым осуществляется интеграция.
/// </summary>
public interface IAuthenticFilesService
{
  /// <summary>
  /// Возвращает список типов файлов, которыми могут быть аутентичные файлы.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Список расширений файлов, начинающихся с точки</returns>
  /// <exception cref="T:System.ArgumentException">Не задан идентификатор типа документа</exception>
  ICollection<string> GetPossibleFileTypes(int documentType);

  /// <summary>
  /// Создает имя и путь для аутентичного файла с учетом особенностей приложения.
  /// </summary>
  /// <param name="documentFilePath">Абсолютный путь к файлу документа</param>
  /// <param name="authenticFileType">Расширение аутентичного файла, начинающееся с точки</param>
  /// <returns>Абсолютный путь к аутентичному файлу</returns>
  string MakeFilePath(string documentFilePath, string authenticFileType);

  /// <summary>
  /// Создает/обновляет аутентичный файл для указанного документа.
  /// </summary>
  /// <param name="documentFilePath">Абсолютный путь к файлу документа</param>
  /// <param name="authenticFilePath">Абсолютный путь к аутентичному файлу</param>
  /// <exception cref="T:System.ArgumentNullException">documentFilePath или authenticFilePath</exception>
  void MakeFile(string documentFilePath, string authenticFilePath);
}
