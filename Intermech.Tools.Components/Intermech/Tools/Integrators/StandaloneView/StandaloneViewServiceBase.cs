// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.StandaloneViewServiceBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Checksums;
using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using Intermech.IO;
using Intermech.Runtime;
using Intermech.Signs.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

#nullable disable
namespace Intermech.Tools.Integrators.StandaloneView;

/// <summary>
/// Реализует основу для сервиса интегратора, отвечающий за внедрение в файлы документов сведений, необходимых для режима автономного просмотра.
/// Эти сведения включают в себя информацию об актуальных подписях документа, контрольной сумме файла, атрибутах документа, заполняемых после согласования документа, и др.
/// </summary>
/// <remarks>
/// Данный класс лишь собирает информацию для записи, а сам процесс записи реализуется в унаследованных классах.
/// </remarks>
public abstract class StandaloneViewServiceBase : 
  IntegratorService,
  IStandaloneViewService,
  IIntegratorService
{
  private Lazy<bool> signsPluginAvailable;
  private IApplicationFileTypes fileTypeService;
  private TempFileStrategy tempFileStrategy;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public StandaloneViewServiceBase(IIntegrator owner)
    : base(owner)
  {
    this.signsPluginAvailable = new Lazy<bool>(new Func<bool>(this.CheckSignsPluginAvailable), true);
  }

  private bool CheckSignsPluginAvailable()
  {
    if (ServiceUtils.GetService<ISignsClientService>((object) ServicesManager.ServiceContainer, false) == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (ServiceUtils.GetService<ISignsService>((object) sessionKeeper.Session, false) == null)
        return false;
    }
    return true;
  }

  /// <summary>
  /// Возвращает или задает ссылку на сервис типов файлов интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
      }
    }
  }

  /// <summary>
  /// Возвращает или задает стратегию создания временного файла. Свойство может быть не задано, в этом случае будет использована стратегия по умолчанию.
  /// </summary>
  public TempFileStrategy TempFileStrategy
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.tempFileStrategy;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.tempFileStrategy = value;
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
    if (this.TempFileStrategy == null)
      this.TempFileStrategy = (TempFileStrategy) new TempAreaStrategy();
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
  }

  /// <summary>
  /// Записывает в файл документа сведения для автономного просмотра.
  /// </summary>
  /// <param name="parameters">Параметры выполнения операции</param>
  /// <returns>Результат выполнения операции</returns>
  /// <exception cref="T:ArgumentNullException">parameters</exception>
  /// <exception cref="T:ArgumentException">Параметры операции содержат некорректные данные</exception>
  public StandaloneViewServiceResult InjectViewData(StandaloneViewDataInjectionParameters parameters)
  {
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    if (parameters.ObjectId == 0L)
      throw new ArgumentException("Не задан идентификатор версии объекта.", "parameters.ObjectId");
    if (string.IsNullOrEmpty(parameters.FileName))
      throw new ArgumentException("Не задано имя файла объекта.", "parameters.FileName");
    if (string.IsNullOrEmpty(parameters.FilePath))
      throw new ArgumentException("Не задан абсолютный путь к файлу объекта.", "parameters.FilePath");
    if (!Path.IsPathRooted(parameters.FilePath))
      throw new ArgumentException("Путь к файлу объекта должен быть задан в абсолютной форме.", "parameters.FilePath");
    if (parameters.ObjectTypeSettings == null)
      throw new ArgumentException("Не заданы настройки для типа объектов.", "parameters.ObjectTypeSettings");
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.InjectViewDataCore(parameters);
  }

  private StandaloneViewServiceResult InjectViewDataCore(
    StandaloneViewDataInjectionParameters parameters)
  {
    StandaloneViewDataInjectionOperation operation = new StandaloneViewDataInjectionOperation();
    operation.Parameters = parameters;
    operation.ObjectTypeId = DBHelper.GetObjectType(parameters.ObjectId);
    if (!this.fileTypeService.IsApplicationFile(parameters.FilePath))
      return operation.Result;
    this.CollectViewData(operation);
    if (operation.ViewData.IsEmpty)
      return operation.Result;
    try
    {
      this.DoInjectViewData(operation);
    }
    catch (Exception ex)
    {
      operation.Result.Errors.Add(ErrorInfo.FromException(ex));
    }
    finally
    {
      operation.CustomData = (object) null;
    }
    return operation.Result;
  }

  private void CollectViewData(StandaloneViewDataInjectionOperation operation)
  {
    if (this.CanInjectObjectSigns(operation))
    {
      try
      {
        operation.ViewData.ObjectSigns = this.GetObjectSignsToInject(operation);
      }
      catch (Exception ex)
      {
        operation.Result.Errors.Add(ErrorInfo.FromException(ex, "Не удалось получить сведения о подписях документа для записи в файл документа."));
      }
    }
    if (this.CanInjectFileChecksum(operation))
    {
      try
      {
        operation.ViewData.FileChecksum = this.GetFileChecksumToInject(operation);
      }
      catch (Exception ex)
      {
        operation.Result.Errors.Add(ErrorInfo.FromException(ex, $"Не удалось расчитать контрольную сумму файла '{operation.Parameters.FileName}'."));
      }
    }
    if (!this.CanInjectObjectAttributes(operation))
      return;
    try
    {
      operation.ViewData.ObjectAttributes = this.GetObjectAttributesToInject(operation);
    }
    catch (Exception ex)
    {
      operation.Result.Errors.Add(ErrorInfo.FromException(ex, "Не удалось получить атрибуты документа для записи в файл документа."));
    }
  }

  /// <summary>
  /// Организует запись в файл документа информации для автономного просмотра.
  /// </summary>
  /// <param name="operation">Параметры выполняемой операции</param>
  protected virtual void DoInjectViewData(StandaloneViewDataInjectionOperation operation)
  {
    this.DoInjectViewDataIntoTempFile(operation);
  }

  protected virtual void DoInjectViewDataIntoAlreadyOpenFile(
    StandaloneViewDataInjectionOperation operation)
  {
    throw new FaultException($"Невозможно записать необходимые сведения в файл документа '{Path.GetFileName(operation.Parameters.FilePath)}', так как файл открыт в другом приложении и занят им.");
  }

  protected virtual void DoInjectViewDataIntoTempFile(StandaloneViewDataInjectionOperation operation)
  {
    bool readOnlyAttribute = FileUtils.GetReadOnlyAttribute(operation.Parameters.FilePath);
    try
    {
      if (readOnlyAttribute)
        FileUtils.SetReadOnlyAttribute(operation.Parameters.FilePath, false);
      if (!FileUtils.CanWriteFile(operation.Parameters.FilePath))
        throw new FaultException($"Невозможно записать необходимые сведения в файл документа '{Path.GetFileName(operation.Parameters.FilePath)}', так как файл открыт в другом приложении и занят им.");
      this.TempFileStrategy.Initialize(operation);
      string filePath = this.TempFileStrategy.FilePath;
      File.Copy(operation.Parameters.FilePath, filePath);
      File.SetAttributes(filePath, FileAttributes.Normal);
      this.DoWriteViewDataIntoTempFile(operation, filePath);
      DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(operation.Parameters.FilePath);
      File.Copy(filePath, operation.Parameters.FilePath, true);
      File.SetLastWriteTimeUtc(operation.Parameters.FilePath, lastWriteTimeUtc);
    }
    finally
    {
      if (readOnlyAttribute)
        FileUtils.SetReadOnlyAttribute(operation.Parameters.FilePath, true);
      if (this.TempFileStrategy.IsInitialized)
      {
        this.TempFileStrategy.RemoveFiles();
        this.TempFileStrategy.Cleanup();
      }
    }
  }

  protected abstract void DoWriteViewDataIntoTempFile(
    StandaloneViewDataInjectionOperation operation,
    string tempFilePath);

  /// <summary>
  /// Записывает в предварительно открытый файл документа информацию для автономного просмотра. Объект открытого файла передается через свойство opParams.CustomData.
  /// </summary>
  /// <param name="operation">Параметры выполняемой операции</param>
  protected virtual void DoWriteViewDataIntoOpenFile(StandaloneViewDataInjectionOperation operation)
  {
    List<ValueRecord> attributeValues = new List<ValueRecord>(32 /*0x20*/);
    if (!operation.ViewData.IsObjectSignsEmpty)
      attributeValues.AddRange((IEnumerable<ValueRecord>) this.GetObjectSignsDataValues(operation.ViewData.ObjectSigns, operation.Parameters.InjectSignNamesOnly));
    if (!operation.ViewData.IsFileChecksumEmpty)
      attributeValues.AddRange((IEnumerable<ValueRecord>) this.GetFileChecksumDataValues(operation.ViewData.FileChecksum));
    if (!operation.ViewData.IsObjectAttributesEmpty)
      attributeValues.AddRange((IEnumerable<ValueRecord>) this.GetObjectAttributesDataValues(operation.ViewData.ObjectAttributes));
    if (attributeValues.Count == 0)
      return;
    try
    {
      this.DoWriteAttributesIntoOpenFile(operation, attributeValues);
    }
    catch (Exception ex)
    {
      operation.Result.Errors.Add(ErrorInfo.FromException(ex, "Не удалось записать значения атрибутов в файл документа."));
    }
  }

  protected abstract void DoWriteAttributesIntoOpenFile(
    StandaloneViewDataInjectionOperation operation,
    List<ValueRecord> attributeValues);

  private bool CanInjectObjectSigns(StandaloneViewDataInjectionOperation operation)
  {
    StandaloneViewObjectTypeSettings objectTypeSettings = operation.Parameters.ObjectTypeSettings;
    if (SignsHolder.SignOutputEnabled)
    {
      bool? injectSigns = objectTypeSettings.InjectSigns;
      if (injectSigns.HasValue)
      {
        injectSigns = objectTypeSettings.InjectSigns;
        return injectSigns.Value;
      }
    }
    return false;
  }

  private ICollection<SignParams> GetObjectSignsToInject(
    StandaloneViewDataInjectionOperation operation)
  {
    ICollection<SignParams> objectSignsToInject = (ICollection<SignParams>) null;
    if (this.signsPluginAvailable.Value)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        objectSignsToInject = (ICollection<SignParams>) ServiceUtils.GetService<ISignsService>((object) sessionKeeper.Session, true).GetObjectSignsParams(operation.Parameters.ObjectId, sessionKeeper.Session.SessionGUID);
    }
    else
      operation.Result.Errors.Add(new ErrorInfo($"Невозможно записать подписи в файл документа '{Path.GetFileName(operation.Parameters.FilePath)}', так как клиентский модуль подписей не загружен."));
    if (objectSignsToInject == null)
      objectSignsToInject = (ICollection<SignParams>) new SignParams[0];
    return objectSignsToInject;
  }

  private ICollection<ValueRecord> GetObjectSignsDataValues(
    ICollection<SignParams> objectSigns,
    bool injectSignNamesOnly)
  {
    List<ValueRecord> objectSignsDataValues = new List<ValueRecord>(objectSigns.Count * 3);
    foreach (SignParams objectSign in (IEnumerable<SignParams>) objectSigns)
    {
      objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.SignSurnameParam, (object) objectSign.Surname));
      if (injectSignNamesOnly)
      {
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.SignDateParam, (object) string.Empty));
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.SignValueParam, (object) string.Empty));
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.RankParam, (object) string.Empty));
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.GraphNameParam, (object) string.Empty));
      }
      else
      {
        string asFormattedString = objectSign.SignDateAsFormattedString;
        if (string.IsNullOrEmpty(asFormattedString))
          asFormattedString = objectSign.SignDate.ToString("d", (IFormatProvider) CultureInfo.InstalledUICulture);
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.SignDateParam, (object) asFormattedString));
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.SignValueParam, (object) objectSign.SignValue));
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.RankParam, (object) objectSign.Rank));
        objectSignsDataValues.Add(new ValueRecord((StringKey) objectSign.GraphNameParam, (object) objectSign.GraphName));
      }
    }
    return (ICollection<ValueRecord>) objectSignsDataValues;
  }

  private bool CanInjectFileChecksum(StandaloneViewDataInjectionOperation operation)
  {
    StandaloneViewObjectTypeSettings objectTypeSettings = operation.Parameters.ObjectTypeSettings;
    if (SignsHolder.SignOutputEnabled)
    {
      bool? injectFileChecksum = objectTypeSettings.InjectFileChecksum;
      if (injectFileChecksum.HasValue)
      {
        injectFileChecksum = objectTypeSettings.InjectFileChecksum;
        return injectFileChecksum.Value;
      }
    }
    return false;
  }

  private Tuple<string, string> GetFileChecksumToInject(
    StandaloneViewDataInjectionOperation operation)
  {
    int fileIndex = CollectionUtils.IndexOf<string>((IEnumerable<string>) ClientContext.FileVault.DBFilesInfo.GetFileNames(operation.Parameters.ObjectId), (Predicate<string>) (item => PathUtils.IsSamePath(item, operation.Parameters.FileName)));
    if (fileIndex == -1)
      throw new FaultException(string.Format("Невозможно вычислить контрольную сумму файла документа '{1}', так как не удалось найти его у документа с ид. версии={0}.", (object) operation.Parameters.ObjectId, (object) operation.Parameters.FileName));
    return Tuple.Create<string, string>(SignsHolder.CheckSumAttribute, this.CalcFileChecksum(operation.Parameters.ObjectId, fileIndex, operation.Parameters.FileName).ToString());
  }

  private ChecksumClass CalcFileChecksum(long objectId, int fileIndex, string fileName)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IChecksumsService service = ServiceUtils.GetService<IChecksumsService>((object) sessionKeeper.Session, true);
      Guid taskGuid = service.CalcChecksum(sessionKeeper.Session.SessionGUID, objectId, AttributableElements.Object, sessionKeeper.Session.IdentHelper.FileAttributeID, fileIndex, SignsHolder.CheckSumType);
      try
      {
        int millisecondsTimeout = 1;
        ChecksumTaskProgress checksumTaskProgress;
        do
        {
          Thread.Sleep(millisecondsTimeout);
          if (millisecondsTimeout < 100)
            millisecondsTimeout *= 10;
          checksumTaskProgress = service.GetChecksumTaskProgress(taskGuid);
        }
        while (checksumTaskProgress.Operation != ChecksumOperationType.Finished && checksumTaskProgress.Operation != ChecksumOperationType.Error);
        if (checksumTaskProgress.ErrorException != null)
          throw new FaultException(string.Format("При вычислении контрольной суммы файла '{1}' у документа с ид. версии={0} произошла ошибка.", (object) objectId, (object) fileName), checksumTaskProgress.ErrorException);
        return service.GetChecksum(taskGuid);
      }
      finally
      {
        service.ChecksumFree(taskGuid);
      }
    }
  }

  private ICollection<ValueRecord> GetFileChecksumDataValues(Tuple<string, string> fileChecksum)
  {
    return (ICollection<ValueRecord>) new List<ValueRecord>()
    {
      new ValueRecord((StringKey) fileChecksum.Item1, (object) fileChecksum.Item2)
    };
  }

  private bool CanInjectObjectAttributes(StandaloneViewDataInjectionOperation operation)
  {
    StandaloneViewObjectTypeSettings objectTypeSettings = operation.Parameters.ObjectTypeSettings;
    return objectTypeSettings.InjectedAttributes != null && objectTypeSettings.InjectedAttributes.Enabled && objectTypeSettings.InjectedAttributes.Identifiers.Count != 0;
  }

  private ICollection<Tuple<string, string>> GetObjectAttributesToInject(
    StandaloneViewDataInjectionOperation operation)
  {
    StandaloneViewObjectTypeSettings objectTypeSettings = operation.Parameters.ObjectTypeSettings;
    List<Tuple<string, string>> attributesToInject = new List<Tuple<string, string>>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(operation.Parameters.ObjectId, true);
      foreach (Guid identifier in (IEnumerable<Guid>) objectTypeSettings.InjectedAttributes.Identifiers)
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(identifier, false);
        if (attributeType != null)
        {
          string[] descriptionsByGuid = dbObject.GetDescriptionsByGuid(identifier, false);
          if (descriptionsByGuid != null && descriptionsByGuid.Length != 0)
          {
            string str = descriptionsByGuid[0] ?? string.Empty;
            attributesToInject.Add(Tuple.Create<string, string>(attributeType.Name, str));
          }
        }
      }
    }
    return (ICollection<Tuple<string, string>>) attributesToInject;
  }

  private ICollection<ValueRecord> GetObjectAttributesDataValues(
    ICollection<Tuple<string, string>> objectAttributes)
  {
    List<ValueRecord> attributesDataValues = new List<ValueRecord>(objectAttributes.Count);
    foreach (Tuple<string, string> objectAttribute in (IEnumerable<Tuple<string, string>>) objectAttributes)
      attributesDataValues.Add(new ValueRecord((StringKey) objectAttribute.Item1, (object) objectAttribute.Item2));
    return (ICollection<ValueRecord>) attributesDataValues;
  }
}
