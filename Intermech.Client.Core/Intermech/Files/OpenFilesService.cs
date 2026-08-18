
// Type: Intermech.Files.OpenFilesService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Localization;
using Intermech.Threading;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Files;

/// <summary>
/// Реализует сервис открытых файлов. Все методы сервиса являются thread-safe.
/// </summary>
internal sealed class OpenFilesService : IOpenFilesService, IOpenFiles
{
  private readonly LinkedList<IOpenFilesServiceExtension> extensions;
  private readonly ReaderWriterLockSlim rwl;

  /// <summary>Создает объект.</summary>
  public OpenFilesService()
  {
    this.extensions = new LinkedList<IOpenFilesServiceExtension>();
    this.rwl = new ReaderWriterLockSlim();
  }

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл открыт в приложении</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void RegisterExtension(IOpenFilesServiceExtension @extension)
  {
    OpenFilesService.CheckExtension(@extension);
    using (new DataWriteLockSlim(this.rwl))
    {
      if (this.extensions.Contains(@extension))
        throw new ArgumentException(LocalizationHolder.rm.GetString("Client.Core_1294"), nameof (@extension));
      this.extensions.AddLast(@extension);
    }
  }

  /// <summary>Отменяет регистрацию расширения сервиса.</summary>
  /// <param name="extension">Объект расширения</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект расширения не может быть null</exception>
  public void UnregisterExtension(IOpenFilesServiceExtension @extension)
  {
    OpenFilesService.CheckExtension(@extension);
    using (new DataWriteLockSlim(this.rwl))
      this.extensions.Remove(@extension);
  }

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл открыт в приложении</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsOpen(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (filePath == string.Empty)
      return false;
    using (new DataReadLockSlim(this.rwl))
      return CollectionUtils.Exists<IOpenFilesServiceExtension>((IEnumerable<IOpenFilesServiceExtension>) this.extensions, (Predicate<IOpenFilesServiceExtension>) (@extension => @extension.IsOpen(filePath)));
  }

  /// <summary>
  /// Возвращает true, если указанный файл открыт в каком-либо приложении и имеет несохраненные изменения.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак того, что файл имеет несохраненные изменения</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsDirty(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (filePath == string.Empty)
      return false;
    using (new DataReadLockSlim(this.rwl))
      return CollectionUtils.Exists<IOpenFilesServiceExtension>((IEnumerable<IOpenFilesServiceExtension>) this.extensions, (Predicate<IOpenFilesServiceExtension>) (@extension => @extension.IsDirty(filePath)));
  }

  /// <summary>Сохраняет на диск имеющиеся изменения в файле.</summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void Save(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (filePath == string.Empty)
      return;
    using (new DataReadLockSlim(this.rwl))
    {
      foreach (IOpenFiles openFiles in this.extensions)
        openFiles.Save(filePath);
    }
  }

  /// <summary>Управляет возможностью внесения изменений в документ.</summary>
  /// <param name="filePath">Путь к файлу, открытому в приложении</param>
  /// <param name="readOnlyFlag">Значение флага</param>
  public void SetReadOnlyFlag(string filePath, bool readOnlyFlag)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (filePath == string.Empty)
      return;
    using (new DataReadLockSlim(this.rwl))
    {
      foreach (IOpenFiles openFiles in this.extensions)
        openFiles.SetReadOnlyFlag(filePath, readOnlyFlag);
    }
  }

  /// <summary>
  /// Проверяет, поддерживается ли перезагрузка октрытого файла без его предварительного закрытия. Если файл не открыт, то метод вернет false.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <returns>Признак, что поддерживается перезагрузка октрытого файла без его предварительного закрытия</returns>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public bool IsReloadable(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (filePath == string.Empty)
      return false;
    using (new DataReadLockSlim(this.rwl))
      return CollectionUtils.Exists<IOpenFilesServiceExtension>((IEnumerable<IOpenFilesServiceExtension>) this.extensions, (Predicate<IOpenFilesServiceExtension>) (@extension => @extension.IsReloadable(filePath)));
  }

  /// <summary>
  /// Выполняет перезагрузку открытого файла без его предварительного закрытия.
  /// </summary>
  /// <param name="filePath">Путь к файлу</param>
  /// <exception cref="T:System.ArgumentNullException">Путь к файлу не может быть null</exception>
  public void Reload(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (filePath == string.Empty)
      return;
    using (new DataReadLockSlim(this.rwl))
    {
      foreach (IOpenFiles openFiles in this.extensions)
        openFiles.Reload(filePath);
    }
  }

  /// <summary>
  /// Выгружает из приложений все документы, которые используют указанные файлы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <param name="fileList">Список путей к файлам, которые должны быть освобождены приложениями</param>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на список файлов не может быть null</exception>
  public object Unload(IEnumerable<string> fileList)
  {
    LinkedList<string> fileList1 = fileList != null ? CollectionUtils.FindAllAsLinkedList<string>(fileList, (Predicate<string>) (item => !string.IsNullOrEmpty(item))) : throw new ArgumentNullException("fullPathList", LocalizationHolder.rm.GetString("Client.Core_1295"));
    if (fileList1.Count != 0)
    {
      LinkedList<Tuple<IOpenFilesServiceExtension, object>> linkedList = new LinkedList<Tuple<IOpenFilesServiceExtension, object>>();
      using (new DataReadLockSlim(this.rwl))
      {
        foreach (IOpenFilesServiceExtension serviceExtension in this.extensions)
        {
          object obj = serviceExtension.Unload((IEnumerable<string>) fileList1);
          if (obj != null)
            linkedList.AddLast(Tuple.Create<IOpenFilesServiceExtension, object>(serviceExtension, obj));
        }
      }
      if (linkedList.Count != 0)
        return (object) linkedList;
    }
    return (object) null;
  }

  /// <summary>
  /// Выгружает все открытые в приложениях документы. Все несохраненные изменения будут потеряны.
  /// </summary>
  /// <returns>Объект с информацией для переоткрытия закрытых документов. Может быть null, если ни один документ не был закрыт</returns>
  public object UnloadAll()
  {
    LinkedList<Tuple<IOpenFilesServiceExtension, object>> linkedList = new LinkedList<Tuple<IOpenFilesServiceExtension, object>>();
    using (new DataReadLockSlim(this.rwl))
    {
      foreach (IOpenFilesServiceExtension serviceExtension in this.extensions)
      {
        object obj = serviceExtension.UnloadAll();
        if (obj != null)
          linkedList.AddLast(new Tuple<IOpenFilesServiceExtension, object>(serviceExtension, obj));
      }
    }
    return linkedList.Count <= 0 ? (object) null : (object) linkedList;
  }

  /// <summary>Переоткрывает закрытые ранее документы.</summary>
  /// <param name="reloadState">Объект состояния с информацией для переоткрытия документов</param>
  public void Reload(object reloadState)
  {
    if (reloadState == null)
      return;
    foreach (Tuple<IOpenFilesServiceExtension, object> tuple in (LinkedList<Tuple<IOpenFilesServiceExtension, object>>) reloadState)
      tuple.Item1.Reload(tuple.Item2);
  }

  private static void CheckExtension(IOpenFilesServiceExtension @extension)
  {
    if (@extension == null)
      throw new ArgumentNullException(nameof (@extension), LocalizationHolder.rm.GetString("Client.Core_1297"));
  }
}
