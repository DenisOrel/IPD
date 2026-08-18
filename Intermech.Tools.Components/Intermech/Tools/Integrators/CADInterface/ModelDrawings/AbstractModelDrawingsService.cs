// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ModelDrawings.AbstractModelDrawingsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.IO;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface.ModelDrawings;

/// <summary>
/// Реализует базовый класс сервиса интегратора для определения файлов чертежей, а также поиска чертежей, связанных с 3D-моделями по имени файла.
/// </summary>
/// <remarks>Реализация является thread safe.</remarks>
public abstract class AbstractModelDrawingsService : 
  IntegratorService,
  IModelDrawingsService,
  IIntegratorService
{
  private readonly string drawingExtension;
  private readonly string[] modelExtensions;
  private IModelDrawingsServiceSettings settingsProvider;
  private ICollection<string> possibleSuffixes;
  private ICollection<string> possibleSuffixesSource;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <param name="drawingExtension">Расширение файлов чертежей, должно начинаться с символа '.' (точка)</param>
  /// <param name="modelExtensions">Расширения файлов моделей</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылкы на владельца компонента и на расширения файлов моделей не могут быть null</exception>
  /// <exception cref="T:System.ArgumentException">Расширение файлов чертежей пусто, либо начинается не с символа '.'</exception>
  public AbstractModelDrawingsService(
    IIntegrator owner,
    string drawingExtension,
    params string[] modelExtensions)
    : base(owner)
  {
    if (string.IsNullOrEmpty(drawingExtension))
      throw new ArgumentException("Расширение файлов чертежей не может быть пустым.", nameof (drawingExtension));
    if (drawingExtension[0] != '.')
      throw new ArgumentException("Расширение файлов чертежей должно начинаться с символа '.' (точка).", nameof (drawingExtension));
    if (modelExtensions == null)
      throw new ArgumentNullException(nameof (modelExtensions));
    this.drawingExtension = drawingExtension;
    this.modelExtensions = modelExtensions;
  }

  /// <summary>
  /// Возвращает или задает объект для доступа к настройкам, необходимым для работы сервиса.
  /// Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IModelDrawingsServiceSettings SettingsProvider
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsProvider;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsProvider = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsProvider == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsProvider");
  }

  /// <summary>
  /// Позволяет определить по имени файла, является ли он чертежом.
  /// </summary>
  /// <param name="fileName">Имя файла, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл чертежа</returns>
  /// <exception cref="T:System.ArgumentException">Имя файла не может быть пустым</exception>
  public bool IsDrawingFileName(string fileName)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException("Имя файла не может быть пустым.", nameof (fileName));
    this.RequireReadyState();
    return this.DoIsDrawingFileName(fileName);
  }

  /// <summary>Определяет по имени файла, является ли он чертежом.</summary>
  /// <param name="fileName">Имя файла, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это файл чертежа</returns>
  protected abstract bool DoIsDrawingFileName(string fileName);

  /// <summary>
  /// Создает вспомогательный объект, который используется в операциях поиска чертежей по документу 3D-модели.
  /// Он используется для вычисления расположения файлов чертежей на диске, если они могут находиться в каталоге,
  /// отличном от каталога 3D-модели.
  /// </summary>
  /// <param name="modelMasterFileName">Имя мастер-файла документа</param>
  /// <returns>Созданный вспомогательный объект</returns>
  /// <exception cref="T:System.ArgumentException">Имя мастер-файла документа не может быть пустым</exception>
  protected ModelDrawingsFindContext CreateFindContext(string modelMasterFileName)
  {
    return !string.IsNullOrEmpty(modelMasterFileName) ? new ModelDrawingsFindContext(modelMasterFileName) : throw new ArgumentException("Имя мастер-файла документа не может быть пустым.", nameof (modelMasterFileName));
  }

  /// <summary>
  /// Выполняет преобразование каталога для файла 3D-модели с использованием созданного ранее вспомогательного объекта.
  /// Реализация по умолчанию просто возвращает исходное имя файла 3D-модели без каких-либо модификаций.
  /// </summary>
  /// <param name="fileName">Имя файла 3D-модели</param>
  /// <param name="findContext">Контекст поиска</param>
  /// <returns>Преобразованное имя файла 3D-модели</returns>
  protected virtual string DoTranslateModelFileName(
    string fileName,
    ModelDrawingsFindContext findContext)
  {
    return fileName;
  }

  /// <summary>
  /// Позволяет найти файл чертежа по имени файла 3D-модели.
  /// </summary>
  /// <param name="modelFileName">Имя файла 3D-модели, может содержать абсолютный или относительный путь</param>
  /// <param name="fileExists">Функция для тестирования существования файла с указанным именем файла и путем</param>
  /// <returns>Имя файла найденного чертежа или null</returns>
  /// <exception cref="T:System.ArgumentException">Имя файла 3D-модели не может быть пустым</exception>
  /// <exception cref="T:System.ArgumentNullException">Функция для тестирования существования файла не указана</exception>
  public string FindDrawingFile(string modelFileName, Func<string, bool> fileExists)
  {
    if (string.IsNullOrEmpty(modelFileName))
      throw new ArgumentException("Имя файла 3D-модели не может быть пустым.", nameof (modelFileName));
    if (fileExists == null)
      throw new ArgumentNullException(nameof (fileExists));
    this.RequireReadyState();
    return this.IsPossibleModelFile(modelFileName) ? this.DoEnumerateDrawingFiles(modelFileName, fileExists).FirstOrDefault<string>() : (string) null;
  }

  /// <summary>Находит все файлы чертежей по имени файла 3D-модели.</summary>
  /// <param name="modelFileName">Имя файла 3D-модели, может содержать абсолютный или относительный путь</param>
  /// <param name="fileExists">Функция для тестирования существования файла с указанным именем файла и путем</param>
  /// <returns>Коллекция имен файлов найденных чертежей</returns>
  protected abstract IEnumerable<string> DoEnumerateDrawingFiles(
    string modelFileName,
    Func<string, bool> fileExists);

  /// <summary>
  /// Позволяет найти все файлы чертежей, связанные с указанным документом 3D-модели.
  /// </summary>
  /// <param name="modelDocumentFiles">Список файлов документа 3D-модели</param>
  /// <param name="fileExists">Функция для тестирования существования файла с указанным именем файла и путем</param>
  /// <returns>Коллекция найденных файлов чертежей</returns>
  /// <exception cref="T:System.ArgumentNullException">Ни один из аргументов метода не может быть null</exception>
  public PathCollection FindAllDrawingFiles(
    IEnumerable<string> modelDocumentFiles,
    Func<string, bool> fileExists)
  {
    if (modelDocumentFiles == null)
      throw new ArgumentNullException(nameof (modelDocumentFiles));
    if (fileExists == null)
      throw new ArgumentNullException(nameof (fileExists));
    this.RequireReadyState();
    ModelDrawingsFindContext findContext = this.CreateFindContext(CollectionUtils.GetFirstItem<string>(modelDocumentFiles));
    PathCollection allDrawingFiles = new PathCollection();
    foreach (string modelDocumentFile in modelDocumentFiles)
    {
      if (this.IsPossibleModelFile(modelDocumentFile))
      {
        string modelFileName = this.DoTranslateModelFileName(modelDocumentFile, findContext);
        allDrawingFiles.AddRange(this.DoEnumerateDrawingFiles(modelFileName, fileExists));
      }
    }
    return allDrawingFiles;
  }

  /// <summary>
  /// Позволяет проверить, соответствуют ли друг другу указанные имена файлов чертежа и 3D-модели.
  /// </summary>
  /// <param name="modelFileName">Имя файла 3D-модели</param>
  /// <param name="drawingFileName">Имя файла чертежа</param>
  /// <returns>true, если имена файлов соответствуют друг другу</returns>
  /// <exception cref="T:System.ArgumentException">Имена файлов чертежа и 3D-модели не могут быть пустыми</exception>
  public bool IsSourceModelFile(string modelFileName, string drawingFileName)
  {
    if (string.IsNullOrEmpty(modelFileName))
      throw new ArgumentException("Имя файла 3D-модели не может быть пустым.", nameof (modelFileName));
    if (string.IsNullOrEmpty(drawingFileName))
      throw new ArgumentException("Имя файла не может быть пустым.", nameof (drawingFileName));
    this.RequireReadyState();
    return this.IsPossibleModelFile(modelFileName) && this.DoIsSourceModelFile(modelFileName, drawingFileName);
  }

  /// <summary>
  /// Проверяет, соответствуют ли друг другу указанные имена файлов чертежа и 3D-модели.
  /// </summary>
  /// <param name="modelFileName">Имя файла 3D-модели</param>
  /// <param name="drawingFileName">Имя файла чертежа</param>
  /// <returns>true, если имена файлов соответствуют друг другу</returns>
  /// <exception cref="T:System.ArgumentException">Имена файлов чертежа и 3D-модели не могут быть пустыми</exception>
  protected abstract bool DoIsSourceModelFile(string modelFileName, string drawingFileName);

  /// <summary>
  /// Позволяет найти среди файлов документа 3D-модели тот, который соответствует указанному файлу чертежа.
  /// </summary>
  /// <param name="modelDocumentFiles">Список файлов документа 3D-модели</param>
  /// <param name="drawingFileName">Имя файла чертежа</param>
  /// <returns>Найденный файл 3D-модели или null</returns>
  public string FindSourceModelFile(IEnumerable<string> modelDocumentFiles, string drawingFileName)
  {
    if (modelDocumentFiles == null)
      throw new ArgumentNullException(nameof (modelDocumentFiles));
    if (string.IsNullOrEmpty(drawingFileName))
      throw new ArgumentException("Имя файла не может быть пустым.", nameof (drawingFileName));
    this.RequireReadyState();
    ModelDrawingsFindContext findContext = this.CreateFindContext(CollectionUtils.GetFirstItem<string>(modelDocumentFiles));
    foreach (string modelDocumentFile in modelDocumentFiles)
    {
      if (this.IsPossibleModelFile(modelDocumentFile) && this.DoIsSourceModelFile(this.DoTranslateModelFileName(modelDocumentFile, findContext), drawingFileName))
        return modelDocumentFile;
    }
    return (string) null;
  }

  /// <summary>
  /// Возвращает расширение, используемое для файлов чертежей.
  /// </summary>
  protected string DrawingExtension => this.drawingExtension;

  /// <summary>Возвращает суффиксы для файлов чертежей.</summary>
  /// <returns>Коллекция суффиксов для файлов чертежей</returns>
  protected ICollection<string> GetPossibleSuffixes()
  {
    lock (this.Integrator.SyncRoot)
    {
      ICollection<string> drawingSuffixes = this.settingsProvider.GetDrawingSuffixes();
      if (this.possibleSuffixes != null && this.possibleSuffixesSource != drawingSuffixes)
      {
        this.possibleSuffixes = (ICollection<string>) null;
        this.possibleSuffixesSource = (ICollection<string>) null;
      }
      if (this.possibleSuffixes == null)
      {
        this.possibleSuffixes = this.DoGeneratePossibleSuffixesFromSettings(drawingSuffixes);
        this.possibleSuffixesSource = drawingSuffixes;
      }
      return this.possibleSuffixes;
    }
  }

  /// <summary>
  /// Генерирует все возможные суффиксы чертежей, используя настройки интегратора.
  /// Метод используется в тех случаях, когда в настройках интегратора задаются не сами значения суффиксов, а правила для их генерации.
  /// </summary>
  /// <param name="drawingSuffixes">Значения, заданные в настройках интегратора</param>
  /// <returns>Коллекция всех возможных суффиксиов чертежей</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="drawingSuffixes" /> содержит null</exception>
  protected virtual ICollection<string> DoGeneratePossibleSuffixesFromSettings(
    ICollection<string> drawingSuffixes)
  {
    return (ICollection<string>) new List<string>((IEnumerable<string>) drawingSuffixes);
  }

  /// <summary>
  /// Позволяет определить по расширению имени файла, может ли это быть файл 3D-модели.
  /// </summary>
  /// <param name="fileName">Имя файла, путь может быть относительным или отсутствовать</param>
  /// <returns>true, если это может быть файл 3D-модели</returns>
  /// <exception cref="T:System.ArgumentException">Имя файла не может быть пустым</exception>
  protected bool IsPossibleModelFile(string fileName)
  {
    string fileExtension = !string.IsNullOrEmpty(fileName) ? Path.GetExtension(fileName) : throw new ArgumentException("Имя файла не может быть пустым.", nameof (fileName));
    return Array.Exists<string>(this.modelExtensions, (Predicate<string>) (item => PathUtils.IsSamePath(fileExtension, item)));
  }
}
