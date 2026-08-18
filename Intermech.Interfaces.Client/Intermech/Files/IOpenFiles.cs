// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IOpenFiles
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Позволяет взаимодействовать с файлами, открытыми во внешних приложениях.
/// </summary>
public interface IOpenFiles
{
  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл открыт в приложении</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  bool IsOpen(string filePath);

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении и имеет несохраненные изменения.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл имеет несохраненные изменения</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  bool IsDirty(string filePath);

  /// <summary>Сохраняет на диск имеющиеся изменения в файле.</summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  void Save(string filePath);

  /// <summary>Управляет возможностью внесения изменений в документ.</summary>
  /// <param name="filePath">Путь к файлу, открытому в приложении</param>
  /// <param name="readOnlyFlag">Значение флага</param>
  void SetReadOnlyFlag(string filePath, bool readOnlyFlag);

  /// <summary>
  /// Проверяет, поддерживается ли перезагрузка октрытого файла без его предварительного закрытия. Если файл не открыт, то метод вернет false.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак, что поддерживается перезагрузка октрытого файла без его предварительного закрытия</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  bool IsReloadable(string filePath);

  /// <summary>
  /// Выполняет перезагрузку открытого файла без его предварительного закрытия.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  void Reload(string filePath);

  /// <summary>
  /// Выгружает из приложений все документы, которые используют указанные файлы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <param name="fileList">Список путей к файлам, которые должны быть освобождены приложениями</param>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список файлов не может быть null</exception>
  object Unload(IEnumerable<string> fileList);

  /// <summary>
  /// Выгружает все открытые в приложениях документы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  object UnloadAll();

  /// <summary>Переоткрывает закрытые ранее документы.</summary>
  /// <param name="reloadState">Объект состояния с информацией для переоткрытия документов</param>
  void Reload(object reloadState);
}
