// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.PrescanFilesOperation
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model.Operations;

internal sealed class PrescanFilesOperation : LongRunningOperation
{
  private readonly ICollection<DBObjectGraphVertex> result;
  private IFileVault fileVaultService;
  private string workAreaPath;
  private CADSettings integratorSettings;
  private IApplicationFileTypes cadFilesService;
  private ICADInterfaceService cadInterfaceService;
  private CADModelDesignationHelper cadModelDesignationHelper;
  private PrescanDBObjectRecord currentScanRecord;
  private DBObjectGraphVertex currentDBObject;
  private List<DBObjectFileEntry> currentFiles;
  private CADSystemProxy currentCADSystem;

  public PrescanFilesOperation()
  {
    this.result = (ICollection<DBObjectGraphVertex>) new HashSet<DBObjectGraphVertex>();
  }

  public ICollection<DBObjectGraphVertex> Result
  {
    [DebuggerStepThrough] get => this.result;
  }

  public void Invoke(CopyingSession session, ICollection<PrescanDBObjectRecord> vertices)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (vertices == null)
      throw new ArgumentNullException(nameof (vertices));
    this.result.Clear();
    this.ErrorsBuilder.Clear();
    try
    {
      this.Initialize(session);
      if (vertices.Count == 0 || this.IsCancellationRequested)
        return;
      this.ScanDocuments(vertices);
    }
    finally
    {
      this.Cleanup();
    }
  }

  private void Initialize(CopyingSession session)
  {
    this.fileVaultService = session.Services.FileVaultService;
    this.workAreaPath = this.fileVaultService.WorkArea.AreaPath;
    this.integratorSettings = session.IntegratorSettings;
    this.cadFilesService = ServiceUtils.GetService<IApplicationFileTypes>((object) session.Integrator, true);
    this.cadInterfaceService = ServiceUtils.GetService<ICADInterfaceService>((object) session.Integrator, true);
    this.cadModelDesignationHelper = new CADModelDesignationHelper();
  }

  private void Cleanup()
  {
    this.fileVaultService = (IFileVault) null;
    this.workAreaPath = (string) null;
    this.integratorSettings = (CADSettings) null;
    this.cadFilesService = (IApplicationFileTypes) null;
    this.cadInterfaceService = (ICADInterfaceService) null;
    this.cadModelDesignationHelper = (CADModelDesignationHelper) null;
  }

  private void ScanDocuments(ICollection<PrescanDBObjectRecord> vertices)
  {
    double num1 = 100.0 / (double) vertices.Count;
    int num2 = 0;
    this.ReportLogMessage("Подключение к CAD-системе");
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadInterfaceService))
    {
      CADSystemProxy application = cadApiSession.Application;
      foreach (PrescanDBObjectRecord vertex in (IEnumerable<PrescanDBObjectRecord>) vertices)
      {
        this.ReportLogMessage($"Сканирование объекта '{vertex.DBObjectVertex.Caption}' (ид. версии {vertex.DBObjectVertex.ObjectId})");
        this.ScanDocument(vertex, application);
        ++num2;
        this.ReportProgress((int) Math.Round(num1 * (double) num2));
        if (this.IsCancellationRequested)
        {
          this.ReportLogMessage("Прерывание сканирования...");
          break;
        }
      }
    }
    this.ReportLogMessage("Отключение от CAD-системы");
  }

  private void ScanDocument(PrescanDBObjectRecord scanRecord, CADSystemProxy cadSystem)
  {
    this.currentScanRecord = scanRecord;
    this.currentDBObject = scanRecord.DBObjectVertex;
    this.currentFiles = scanRecord.Files;
    this.currentCADSystem = cadSystem;
    try
    {
      this.DispatchAndScanCurrentDocument();
      this.result.Add(this.currentDBObject);
    }
    catch (PrescanFilesOperation.PrescanErrorException ex)
    {
      this.ErrorsBuilder.AddError(new OperationError(ex.Message));
    }
    catch (Exception ex)
    {
      this.ErrorsBuilder.AddError(new OperationError(ex.Message));
    }
    finally
    {
      this.currentScanRecord = (PrescanDBObjectRecord) null;
      this.currentDBObject = (DBObjectGraphVertex) null;
      this.currentFiles = (List<DBObjectFileEntry>) null;
      this.currentCADSystem = (CADSystemProxy) null;
    }
  }

  private void DispatchAndScanCurrentDocument()
  {
    if (this.currentFiles.Count == 0)
    {
      this.currentScanRecord.Content = (DBObjectContent) new NonCADDocumentContent();
    }
    else
    {
      IntegratorObject integratorObject = IntegratorServices.Find(this.currentDBObject.ObjectTypeId);
      if (integratorObject != null && integratorObject.Id != this.cadInterfaceService.Integrator.Id)
        throw new PrescanFilesOperation.PrescanErrorException($"Документ '{this.currentDBObject.Caption}' (ид.версии {this.currentDBObject.ObjectId}) обрабатывается другим интегратором '{integratorObject}'. Отмените копирование данного документа.");
      DBObjectFileEntry currentFile = this.currentFiles[0];
      PrescanFilesOperation.CADNativeFormatInfo cadNativeFormatInfo = this.DetectCADNativeFormat(this.currentDBObject);
      if (cadNativeFormatInfo != null)
        this.ScanCADDocument(cadNativeFormatInfo, currentFile);
      else
        this.ScanNonCADDocument(currentFile);
    }
  }

  private void ScanCADDocument(
    PrescanFilesOperation.CADNativeFormatInfo cadNativeFormatInfo,
    DBObjectFileEntry firstFileRecord)
  {
    if (!this.cadFilesService.IsApplicationFile(firstFileRecord.OriginalName))
      throw new PrescanFilesOperation.PrescanErrorException($"Ошибка в файловом атрибуте документа '{this.currentDBObject.Caption}' (ид.версии {this.currentDBObject.ObjectId}). Первый файл документа не является файлом CAD-системы. Отмените копирование данного документа.");
    CADDocumentProxy firstFileCADDocument;
    try
    {
      firstFileCADDocument = this.currentCADSystem.OpenDocument(this.GetLocalPath(firstFileRecord), false);
    }
    catch (Exception ex)
    {
      throw new PrescanFilesOperation.PrescanErrorException($"CAD-системе не удалось открыть документ '{firstFileRecord.OriginalName}' по причине {ex.Message}. Возможно, формат файла документа не поддерживается установленной версией CAD-системы");
    }
    if (cadNativeFormatInfo.IsModel)
      this.ScanCADModelFirstFile(cadNativeFormatInfo, firstFileRecord, firstFileCADDocument);
    else
      this.ScanCADGenericFirstFile(cadNativeFormatInfo, firstFileRecord, firstFileCADDocument);
  }

  private void ScanCADModelFirstFile(
    PrescanFilesOperation.CADNativeFormatInfo cadNativeFormatInfo,
    DBObjectFileEntry firstFileRecord,
    CADDocumentProxy firstFileCADDocument)
  {
    if (!firstFileCADDocument.IsMasterDocument)
      throw new PrescanFilesOperation.PrescanErrorException(string.Format("Ошибка в файловом атрибуте документа '{0}' (ид.версии {1}). Первый файл документа не является мастер-файлом модели CAD-системы. Исправьте расположение файлов или отмените копирование данного документа.", (object) this.currentDBObject.Caption, (object) this.currentDBObject.ObjectId, (object) firstFileRecord.OriginalName));
    CADConfigurationTable configurationTable = new CADConfigurationTable();
    firstFileRecord.Content = (DBObjectFileContent) new CADMainFileContent();
    foreach (Tuple<ModelConfigurationPath, ModelConfigurationProxy> configuration in ModelConfigurationUtils.GetConfigurationList(firstFileCADDocument))
    {
      string targetConfiguration = configuration.Item1.TargetConfiguration;
      string str = configuration.Item2.FullPath;
      if (!string.IsNullOrEmpty(str) && Path.IsPathRooted(str))
        str = PathUtils.GetRelativePath(str, this.workAreaPath, RelativePathOptions.ThrowIfNotPossible);
      CADConfigurationTableRow configurationTableRow = new CADConfigurationTableRow(firstFileRecord.OriginalName, targetConfiguration, str);
      configurationTable.Add(configurationTableRow);
      if (!string.IsNullOrEmpty(str))
      {
        DBObjectFileEntry dbObjectFileEntry = this.currentFiles.Find((Predicate<DBObjectFileEntry>) (x => PathUtils.IsSamePath(x.OriginalName, configurationTableRow.ConfigurationPath)));
        if (dbObjectFileEntry == null)
          throw new PrescanFilesOperation.PrescanErrorException($"Ошибка в файловом атрибуте документа '{this.currentDBObject.Caption}' (ид.версии {this.currentDBObject.ObjectId}). Отсутствует файл '{firstFileRecord.OriginalName}' для конфигурации модели '{targetConfiguration}'. Исправьте расположение файлов и повторите текущую операцию.");
        dbObjectFileEntry.Content = (DBObjectFileContent) new CADModelConfigurationFileContent(configurationTableRow);
      }
    }
    foreach (DBObjectFileEntry currentFile in this.currentFiles)
    {
      if (currentFile.Content == null)
        currentFile.Content = !this.cadFilesService.IsApplicationFile(currentFile.OriginalName) ? (DBObjectFileContent) new NonCADAncillaryFileContent() : (DBObjectFileContent) new CADAncillaryFileContent();
    }
    StringKey name1 = firstFileCADDocument.DefaultConfiguration.Name;
    ValueBag detectionData = this.cadModelDesignationHelper.GetDetectionData(firstFileCADDocument);
    bool indendentDesignationMode = this.cadModelDesignationHelper.IsIndependentDesignationMode(firstFileCADDocument, detectionData);
    StringKey name2 = this.cadModelDesignationHelper.GetBasicArticleInstance(firstFileCADDocument, detectionData).Name;
    this.currentScanRecord.Content = (DBObjectContent) new CADModelContent(configurationTable, (string) name1, new CADModelDesignationSettings(indendentDesignationMode, (string) name2), this.cadInterfaceService.GetDocumentCodec(firstFileCADDocument), this.cadInterfaceService.GetArticleCodec(firstFileCADDocument));
  }

  private void ScanCADGenericFirstFile(
    PrescanFilesOperation.CADNativeFormatInfo cadNativeFormatInfo,
    DBObjectFileEntry firstFileRecord,
    CADDocumentProxy firstFileCADDocument)
  {
    CADConfigurationTable configurationTable = new CADConfigurationTable();
    firstFileRecord.Content = (DBObjectFileContent) new CADMainFileContent();
    foreach (DBObjectFileEntry currentFile in this.currentFiles)
    {
      if (currentFile != firstFileRecord)
        currentFile.Content = !this.cadFilesService.IsApplicationFile(currentFile.OriginalName) ? (DBObjectFileContent) new NonCADAncillaryFileContent() : (DBObjectFileContent) new CADAncillaryFileContent();
    }
    this.currentScanRecord.Content = (DBObjectContent) new CADGeneralDocumentContent(this.cadInterfaceService.GetDocumentCodec(firstFileCADDocument));
  }

  private void ScanNonCADDocument(DBObjectFileEntry firstFileRecord)
  {
    firstFileRecord.Content = (DBObjectFileContent) new NonCADMainFileContent();
    foreach (DBObjectFileEntry currentFile in this.currentFiles)
    {
      if (currentFile != firstFileRecord)
        currentFile.Content = (DBObjectFileContent) new NonCADAncillaryFileContent();
    }
    this.currentScanRecord.Content = (DBObjectContent) new NonCADDocumentContent();
  }

  private string GetLocalPath(DBObjectFileEntry fileRecord)
  {
    string fullPath = Path.GetFullPath(Path.Combine(this.workAreaPath, fileRecord.OriginalName));
    return !string.IsNullOrEmpty(fullPath) && Path.IsPathRooted(fullPath) ? fullPath : throw new InvalidOperationException("Требуется непустой абсолютный путь к файлу.");
  }

  private PrescanFilesOperation.CADNativeFormatInfo DetectCADNativeFormat(
    DBObjectGraphVertex dbObjectVertex)
  {
    DocumentGroup byDocumentType = this.integratorSettings.FileDocumentGroups.FindByDocumentType(dbObjectVertex.ObjectTypeId, false);
    return byDocumentType != null && ((IEnumerable<string>) CADSettings.CommonGroups.All).Contains<string>(byDocumentType.Name) ? new PrescanFilesOperation.CADNativeFormatInfo(byDocumentType, ((IEnumerable<string>) byDocumentType.Flags).Contains<string>("model")) : (PrescanFilesOperation.CADNativeFormatInfo) null;
  }

  private sealed class PrescanErrorException(string message) : Exception(message)
  {
  }

  private sealed class CADNativeFormatInfo
  {
    public CADNativeFormatInfo(DocumentGroup documentGroup, bool isModel)
    {
      this.DocumentGroup = documentGroup;
      this.IsModel = isModel;
    }

    public DocumentGroup DocumentGroup { get; }

    public bool IsModel { get; }
  }
}
