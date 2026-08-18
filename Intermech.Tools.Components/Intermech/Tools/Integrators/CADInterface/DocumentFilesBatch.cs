// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.DocumentFilesBatch
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует базовый класс для пакетного получения информации о документах IPS, используя имена мастер-файлов документов.
/// Класс не является thread-safe.
/// </summary>
public abstract class DocumentFilesBatch
{
  private bool initialized;
  protected IFileVault fileVault;
  protected IList<string> filenames;
  private PathDictionary<List<int>> filenamePosTable;
  private List<string> workAreaPaths;
  private Dictionary<long, List<int>> objectPosTable;
  private List<long> objectIds;

  /// <summary>Выполняет пакетное получение информации.</summary>
  /// <param name="documentFiles">Коллекция абсолютных путей к мастер-файлам документов. Может содержать пустые имена и дубликаты</param>
  /// <returns>Объект, содержащий собранную информацию</returns>
  protected object PerformBatch(IList<string> documentFiles)
  {
    if (documentFiles == null)
      throw new ArgumentNullException(nameof (documentFiles));
    this.Initialize();
    try
    {
      return this.PerformCore(documentFiles);
    }
    finally
    {
      this.filenames = (IList<string>) null;
      this.filenamePosTable = (PathDictionary<List<int>>) null;
      this.workAreaPaths = (List<string>) null;
      this.objectPosTable = (Dictionary<long, List<int>>) null;
      this.objectIds = (List<long>) null;
    }
  }

  private object PerformCore(IList<string> documentFiles)
  {
    object resultObject = this.CreateResultObject(documentFiles.Count);
    if (documentFiles.Count == 0)
      return resultObject;
    this.filenames = documentFiles;
    this.filenamePosTable = new PathDictionary<List<int>>(this.filenames.Count);
    this.workAreaPaths = new List<string>(this.filenames.Count);
    for (int index = 0; index < this.filenames.Count; ++index)
    {
      string filename = this.filenames[index];
      if (!string.IsNullOrEmpty(filename) && Path.IsPathRooted(filename) && PathUtils.IsPlacedIn(filename, this.fileVault.WorkArea.AreaPath))
      {
        List<int> intList;
        if (!this.filenamePosTable.TryGetValue(filename, out intList))
        {
          intList = new List<int>();
          this.filenamePosTable.Add(filename, intList);
          this.workAreaPaths.Add(filename);
        }
        intList.Add(index);
      }
      else
        this.SetResultForUnknownFile(resultObject, index);
    }
    if (this.workAreaPaths.Count == 0)
      return resultObject;
    List<FileOrigin> fileOrigins = this.fileVault.WorkArea.GetFileOrigins((IList<string>) this.workAreaPaths, false);
    this.objectPosTable = new Dictionary<long, List<int>>(fileOrigins.Count);
    this.objectIds = new List<long>(fileOrigins.Count);
    for (int index1 = 0; index1 < fileOrigins.Count; ++index1)
    {
      FileOrigin origin = fileOrigins[index1];
      List<int> collection = this.filenamePosTable[origin.FileName];
      if (origin.OriginType == FileOriginType.WorkFile)
      {
        if (this.SupportsFastCheckedOutInfo(origin))
        {
          foreach (int index2 in collection)
            this.SetResultForCheckedOutObject(resultObject, origin.FileName, index2);
        }
        else
        {
          List<int> intList;
          if (!this.objectPosTable.TryGetValue(origin.WorkObject.ObjectId, out intList))
          {
            intList = new List<int>();
            this.objectPosTable.Add(origin.WorkObject.ObjectId, intList);
            this.objectIds.Add(origin.WorkObject.ObjectId);
          }
          intList.AddRange((IEnumerable<int>) collection);
        }
      }
      else
      {
        foreach (int index3 in collection)
          this.SetResultForUnknownFile(resultObject, index3);
      }
    }
    if (this.objectIds.Count == 0)
      return resultObject;
    foreach (DataRow row in (InternalDataCollectionBase) this.GetTable(this.objectIds).Rows)
    {
      List<int> posList = this.objectPosTable[Convert.ToInt64(row[0])];
      this.SetResultForObject(resultObject, row, posList);
    }
    return resultObject;
  }

  private void Initialize()
  {
    if (this.initialized)
      return;
    this.DoInitialize();
    this.initialized = true;
  }

  /// <summary>
  /// Позволяет инициализировать компонент, получить ссылки на используемые сервисы. Вызывается при первом обращении к компоненту.
  /// </summary>
  protected virtual void DoInitialize()
  {
    this.fileVault = ServiceUtils.GetService<IFileVault>((object) ServicesManager.ServiceContainer, true);
  }

  /// <summary>
  /// Создает объект, который будет содержать результат работы компонента.
  /// </summary>
  /// <param name="fileCount">Количество файлов, для которых информация была запрошена</param>
  /// <returns>Созданный объект</returns>
  protected abstract object CreateResultObject(int fileCount);

  /// <summary>Возвращает из базы данных информацию об объектах.</summary>
  /// <param name="objectIds">Идентификаторы версий объектов</param>
  /// <returns>Таблица с информацией об объектах. Первый столбец должен содержать идентификатор версии объекта</returns>
  protected abstract DataTable GetTable(List<long> objectIds);

  /// <summary>
  /// Помещает в результат информацию об объекте, полученную из базы данных.
  /// </summary>
  /// <param name="result">Объект результата</param>
  /// <param name="row">Строка таблицы</param>
  /// <param name="posList">Индексы файлов объекта в исходной коллекции</param>
  protected abstract void SetResultForObject(object result, DataRow row, List<int> posList);

  /// <summary>
  /// Помещает в результат информацию об объекте для файла, чье имя пусто, либо сам файл находится вне рабочей области.
  /// </summary>
  /// <param name="result">Объект результата</param>
  /// <param name="index">Индекс файла в исходной коллекции</param>
  protected abstract void SetResultForUnknownFile(object result, int index);

  /// <summary>
  /// Возвращает true, если для этого файла можно получить информацию без обращения в базу данных.
  /// </summary>
  /// <param name="origin">Сведения о файле и объекте, к которому он принадлежит</param>
  /// <returns>Результат тестирования</returns>
  protected abstract bool SupportsFastCheckedOutInfo(FileOrigin origin);

  /// <summary>
  /// Помещает в результат информацию об объекте, не используя обращения в базу данных. Этот метод вызывается только тогда,
  /// когда метод <see cref="M:Intermech.Tools.Integrators.CADInterface.DocumentFilesBatch.SupportsFastCheckedOutInfo(Intermech.Files.FileOrigin)" /> вернул true.
  /// </summary>
  /// <param name="result">Объект результата</param>
  /// <param name="fullPath">Абсолютный путь к файлу</param>
  /// <param name="index">Индекс файла в исходной коллекции</param>
  protected abstract void SetResultForCheckedOutObject(object result, string fullPath, int index);
}
