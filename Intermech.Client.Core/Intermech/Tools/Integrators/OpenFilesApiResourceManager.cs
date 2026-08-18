
// Type: Intermech.Tools.Integrators.OpenFilesApiResourceManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Diagnostics;
using Intermech.IO;
using System;
using System.IO;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует менеджер ресурсов приложения, позволяющий отслеживать и закрывать документы, открытые интегратором в сессии подключения к API приложения.
/// </summary>
public abstract class OpenFilesApiResourceManager : ApplicationApiResourceManager
{
  private readonly PathCollection openFiles;

  /// <summary>Создает объект.</summary>
  protected OpenFilesApiResourceManager() => this.openFiles = new PathCollection(16 /*0x10*/);

  /// <summary>
  /// Освобождает ресурсы приложения, открытые интегратором, а также деактивирует сохранение информации об открытых ресурсах приложения.
  /// Метод не должен сбрасывать исключения. Все ошибки освобождения ресурсов приложения должны сохраняться в коллекции Errors.
  /// </summary>
  protected override void DoReleaseResourcesAndStop()
  {
    this.ReleaseTrackedOpenFiles();
    base.DoReleaseResourcesAndStop();
  }

  private void ReleaseTrackedOpenFiles()
  {
    foreach (string openFile in (OrderedList<string>) this.openFiles)
    {
      try
      {
        this.DoCloseFileIfOpen(openFile);
      }
      catch (Exception ex)
      {
        this.Errors.Add(ErrorInfo.FromException(ex, $"Не удалось закрыть документ '{openFile}'."));
      }
    }
  }

  /// <summary>
  /// Закрывает файл документа, если он открыт в приложении.
  /// </summary>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  protected abstract void DoCloseFileIfOpen(string fullPath);

  /// <summary>Сохраняет факт открытия интегратором файла документа.</summary>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  /// <exception cref="T:System.ArgumentNullException">fullPath</exception>
  /// <exception cref="T:System.ArgumentException">Путь к файлу должен быть задан в абсолютной форме</exception>
  public void TrackOpenFile(string fullPath)
  {
    if (fullPath == null)
      throw new ArgumentNullException(nameof (fullPath));
    if (!Path.IsPathRooted(fullPath))
      throw new ArgumentException("Путь к файлу должен быть задан в абсолютной форме.", nameof (fullPath));
    this.openFiles.Add(fullPath);
  }
}
