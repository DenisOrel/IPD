// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.Operations.CopyCADFilesOperation
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

internal sealed class CopyCADFilesOperation : LongRunningOperation
{
  private CopyingSession _session;
  private IFileVault _fileVaultService;
  private string _workspacePath;
  private IIntegrator _integrator;
  private CADHeuristics _integratorHeuristics;
  private ICADSettingsService _integratorSettingsService;
  private ICADInterfaceService _cadInterfaceService;
  private ICADPrepareNewObjectsService _cadPrepareNewObjectsService;
  private bool _operationCancellationInCAD;
  private List<ValueRecord> _emptyCADModelArticleAttributeValues;
  private ICollection<StringKey> _writeAlwaysCADModelArticleAttributeKeys;
  private ICollection<StringKey> _writeAlwaysCADModelDocumentAttributeKeys;
  private CADInterfaceFormatter _cadGeneralDocumentFormatter;
  private ModelDocumentFormatter _cadModelDocumentFormatter;
  private ModelDocumentFormatter _cadModelDocumentFormatterWithIndependentDesignation;
  private ModelArticleFormatter _cadModelArticleFormatter;
  private long _uniqueID;
  private static readonly ICollection<StringKey> _emptyAttributeKeys = (ICollection<StringKey>) new StringKey[0];

  public void Invoke(CopyingSession session, ICollection<DBObjectGraphVertex> vertices)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (vertices == null)
      throw new ArgumentNullException(nameof (vertices));
    try
    {
      this.ReportLogMessage("Начало копирования файлов...");
      this.InitializeCore(session);
      try
      {
        this.InvokeCore(vertices);
        this.ReportLogMessage("Копирование файлов завершено...");
      }
      catch (AbortException ex)
      {
        this.ErrorsBuilder.AddError(new OperationError("Операция была прервана пользователем."));
      }
    }
    finally
    {
      this.CleanupCore();
    }
  }

  [Conditional("DEBUG")]
  private void ValidateVertices(ICollection<DBObjectGraphVertex> vertices)
  {
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
    {
      if (!vertex.IsDocument() || !vertex.IsScanned)
        throw new InvalidOperationException();
    }
  }

  private void InitializeCore(CopyingSession session)
  {
    this._session = session;
    this._fileVaultService = session.Services.FileVaultService;
    this._workspacePath = this._fileVaultService.WorkArea.AreaPath;
    this._integrator = session.Integrator;
    this._integratorHeuristics = session.IntegratorHeuristics;
    this._integratorSettingsService = ServiceUtils.GetService<ICADSettingsService>((object) this._integrator, true);
    this._cadInterfaceService = ServiceUtils.GetService<ICADInterfaceService>((object) this._integrator, true);
    this._cadPrepareNewObjectsService = ServiceUtils.GetService<ICADPrepareNewObjectsService>((object) this._integrator, true);
    this._emptyCADModelArticleAttributeValues = this._cadPrepareNewObjectsService.GetValuesToEraseArticleInfo().GetItemsList();
    this._writeAlwaysCADModelArticleAttributeKeys = (ICollection<StringKey>) this._emptyCADModelArticleAttributeValues.ConvertAll<StringKey>((Converter<ValueRecord, StringKey>) (x => x.Key));
    this._writeAlwaysCADModelDocumentAttributeKeys = (ICollection<StringKey>) new StringKey[2]
    {
      (StringKey) session.Services.IntegratorsIDCache.Designation.Text,
      (StringKey) session.Services.IntegratorsIDCache.Name.Text
    };
    this._cadGeneralDocumentFormatter = new CADInterfaceFormatter();
    this._cadModelDocumentFormatter = new ModelDocumentFormatter()
    {
      WriteTargetStrategy = (ModelParametersWriteTargetStrategy) new CopyCADFilesOperation.DocumentWithBasicArticleWriteTargetStrategy()
    };
    this._cadModelDocumentFormatterWithIndependentDesignation = new ModelDocumentFormatter();
    this._cadModelArticleFormatter = new ModelArticleFormatter();
    this._uniqueID = session.UniqueId;
  }

  private void CleanupCore()
  {
    this._session = (CopyingSession) null;
    this._fileVaultService = (IFileVault) null;
    this._workspacePath = (string) null;
    this._integrator = (IIntegrator) null;
    this._integratorHeuristics = (CADHeuristics) null;
    this._integratorSettingsService = (ICADSettingsService) null;
    this._cadInterfaceService = (ICADInterfaceService) null;
    this._cadPrepareNewObjectsService = (ICADPrepareNewObjectsService) null;
    this._emptyCADModelArticleAttributeValues = (List<ValueRecord>) null;
    this._writeAlwaysCADModelArticleAttributeKeys = (ICollection<StringKey>) null;
    this._writeAlwaysCADModelDocumentAttributeKeys = (ICollection<StringKey>) null;
    this._cadGeneralDocumentFormatter = (CADInterfaceFormatter) null;
    this._cadModelDocumentFormatter = (ModelDocumentFormatter) null;
    this._cadModelDocumentFormatterWithIndependentDesignation = (ModelDocumentFormatter) null;
    this._cadModelArticleFormatter = (ModelArticleFormatter) null;
    this._uniqueID = 0L;
  }

  private void InvokeCore(ICollection<DBObjectGraphVertex> vertices)
  {
    this.ReportLogMessage("Подключение к CAD-системе");
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this._cadInterfaceService))
    {
      CADSystemProxy application = cadApiSession.Application;
      this.CheckCancellationOperation();
      (CloneDataProxy CloneData, List<CloneDataManualFileInfo> ManualCopy) cloneData = this.CreateCloneData(application, vertices);
      this.CheckCancellationOperation();
      this.ReportLogMessage("Начало копирования исходного документа");
      if (cloneData.ManualCopy.Count > 0)
      {
        foreach (CloneDataManualFileInfo dataManualFileInfo in cloneData.ManualCopy)
        {
          this.CheckCancellationOperation();
          try
          {
            if (File.Exists(dataManualFileInfo.NewPath))
              FileUtils.DeleteFileSilently(dataManualFileInfo.NewPath);
            string directoryName = Path.GetDirectoryName(dataManualFileInfo.NewPath);
            if (!Directory.Exists(directoryName))
              Directory.CreateDirectory(directoryName);
            File.Copy(dataManualFileInfo.OriginalPath, dataManualFileInfo.NewPath);
          }
          catch (Exception ex)
          {
            this.ErrorsBuilder.AddError(new OperationError($"Ошибка обработки документа '{dataManualFileInfo.Vertex.Caption}'. Произошла ошибка в копировании файла '{dataManualFileInfo.OriginalPath}' в файл '{dataManualFileInfo.NewPath}'. Полный текст ошибки: {ex.Message}"));
            throw;
          }
          this.CheckCancellationOperation();
        }
      }
      try
      {
        application.Clone(cloneData.CloneData);
      }
      catch (Exception ex)
      {
        this.ErrorsBuilder.AddError(new OperationError(ex.Message));
        throw;
      }
      if (this._operationCancellationInCAD)
        throw new AbortException();
      this.ReportProgress(100);
      this.ReportLogMessage("Копирование завершено");
    }
    this.ReportLogMessage("Отключение от CAD-системы");
  }

  private string MakeNewPath(string filePath)
  {
    return Path.Combine(Path.GetDirectoryName(filePath), $"{Path.GetFileNameWithoutExtension(filePath)}_clone{Path.GetExtension(filePath)}");
  }

  private (CloneDataProxy CloneData, List<CloneDataManualFileInfo> ManualCopy) CreateCloneData(
    CADSystemProxy cadSystem,
    ICollection<DBObjectGraphVertex> vertices)
  {
    this.ReportLogMessage("Подготовка данных для копирования");
    CloneDataProxy cloneData = cadSystem.CreateCloneData();
    this.CheckCancellationOperation();
    this.ReportLogMessage("Подготовка файлов для копирования");
    int num = 0;
    List<CloneDataManualFileInfo> dataManualFileInfoList = new List<CloneDataManualFileInfo>(0);
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
    {
      this.CheckCancellationOperation();
      foreach (DBObjectFileEntry file1 in (IEnumerable<DBObjectFileEntry>) vertex.Files)
      {
        this.CheckCancellationOperation();
        try
        {
          if (!file1.Content.IsCADFile && (this._integratorHeuristics.CloneDataCapabilities & CADCloneDataCapabilities.CanHandleOnlyCADFiles) != CADCloneDataCapabilities.None)
          {
            CloneDataManualFileInfo dataManualFileInfo = new CloneDataManualFileInfo()
            {
              OriginalPath = Path.Combine(this._workspacePath, file1.OriginalName),
              NewPath = Path.Combine(this._workspacePath, file1.NewName),
              Vertex = vertex
            };
            dataManualFileInfoList.Add(dataManualFileInfo);
          }
          else
          {
            CloneDataFileProxy file2 = cloneData.CreateFile();
            file2.OriginalPath = Path.Combine(this._workspacePath, file1.OriginalName);
            file2.NewPath = Path.Combine(this._workspacePath, file1.NewName);
            if (File.Exists(file2.NewPath))
              FileUtils.DeleteFileSilently(file2.NewPath);
            cloneData.AddFile(file2);
            ++num;
          }
        }
        catch (Exception ex)
        {
          this.ErrorsBuilder.AddError(new OperationError(ex.Message, vertex: vertex));
        }
      }
    }
    if ((this._session.IntegratorHeuristics.CloneDataCapabilities & CADCloneDataCapabilities.IncludeUnmodifiedReferenceFiles) != CADCloneDataCapabilities.None)
      this.AddUnmodifiedReferenceFiles(cloneData);
    this.ReportLogMessage("Подготовка файлов завершена");
    this.CheckCancellationOperation();
    this.ReportLogMessage("Подготовка атрибутов для копирования");
    foreach (DBObjectGraphVertex vertex in (IEnumerable<DBObjectGraphVertex>) vertices)
    {
      this.CheckCancellationOperation();
      if (vertex.Content.IsCADDocument)
      {
        DBObjectFileEntry file = vertex.Files[0];
        string originalMainFilePath = Path.Combine(this._workspacePath, file.OriginalName);
        string newMainFilePath = Path.Combine(this._workspacePath, file.NewName);
        if (vertex.Content.Tag == DBObjectContentTag.CADModel)
        {
          CADModelContent cadModelContent = vertex.Content.AsCADModel();
          CADConfigurationTable configurationTable = cadModelContent.ConfigurationTable;
          CADVirtualParametersContainerSet virtualContainerSet = new CADVirtualParametersContainerSet();
          this.CheckLinkedArticlesMappingData(vertex);
          foreach (CADConfigurationTableRow row in (IEnumerable<CADConfigurationTableRow>) configurationTable.Rows)
            this.WriteCADModelArticleAttributes(vertex, row.Name, cadModelContent.ArticleAttributeCodec, virtualContainerSet);
          this.WriteCADModelDocumentAttributes(vertex, cadModelContent.DocumentDesignationSettings, cadModelContent.DocumentAttributeCodec, virtualContainerSet);
          this._integratorHeuristics.PrepareDocumentParametersToWrite(this._session, vertex, virtualContainerSet);
          this.AddModelConfigurationParametersToCloneData(vertex, originalMainFilePath, newMainFilePath, configurationTable, virtualContainerSet, cloneData);
          this.AddDocumentParametersToCloneData(vertex, originalMainFilePath, newMainFilePath, virtualContainerSet, cloneData);
        }
        else if (vertex.Content.Tag == DBObjectContentTag.CADGeneralDocument)
        {
          CADGeneralDocumentContent generalDocumentContent = vertex.Content.AsCADGeneralDocument();
          CADVirtualParametersContainerSet virtualContainerSet = new CADVirtualParametersContainerSet();
          this.WriteCADGeneralDocumentAttributes(vertex, generalDocumentContent.DocumentAttributeCodec, virtualContainerSet);
          this._integratorHeuristics.PrepareDocumentParametersToWrite(this._session, vertex, virtualContainerSet);
          this.AddDocumentParametersToCloneData(vertex, originalMainFilePath, newMainFilePath, virtualContainerSet, cloneData);
        }
      }
    }
    this.ReportLogMessage("Подготовка атрибутов завершена");
    this.CheckCancellationOperation();
    CloneProgressSink cloneProgressSink = new CloneProgressSink();
    double progressVerticesFactor = 99.0 / (double) num;
    int progressCount = 0;
    cloneProgressSink.OnItemCompleted += (EventHandler<CloneDataFileCompletedEventArgs>) ((sender, e) =>
    {
      if (this.IsCancellationRequested)
      {
        e.Result = false;
        this._operationCancellationInCAD = true;
      }
      else
      {
        if (e.File.Result != CloneDataFileResult.Failed)
        {
          this.ReportLogMessage($"Файл '{e.File.OriginalPath}' скопирован в {e.File.NewPath}.");
          e.Result = true;
          ++progressCount;
          this.ReportProgress((int) Math.Round(progressVerticesFactor * (double) progressCount));
        }
        if (e.File.Result != CloneDataFileResult.Failed)
          return;
        e.Result = false;
        Intermech.Tools.Client.CompositionCopying.Model.ErrorsBuilder<OperationError> errorsBuilder = this.ErrorsBuilder;
        OperationError error;
        if (!string.IsNullOrEmpty(e.File.ErrorMessage))
          error = new OperationError(e.File.ErrorMessage);
        else
          error = new OperationError($"Ошибка копирования файла '{e.File.OriginalPath}' в '{e.File.NewPath}'");
        errorsBuilder.AddError(error);
      }
    });
    cloneData.ProgressSink = cloneProgressSink;
    this.ReportLogMessage("Подготовка данных для копирования завершена");
    return (cloneData, dataManualFileInfoList);
  }

  private void AddUnmodifiedReferenceFiles(CloneDataProxy cloneData)
  {
    ICollection<DBObjectGraphVertex> allVertices = this._session.Graph.GetAllVertices((Predicate<DBObjectGraphVertex>) (x => x.IsDocument() && !x.CopyingSelector.IsSelected && !x.IsScanned));
    if (allVertices.Count == 0)
      return;
    PrescanDBObjectFileNamesOperation fileNamesOperation = new PrescanDBObjectFileNamesOperation();
    fileNamesOperation.Invoke(this._session, allVertices);
    if (fileNamesOperation.Errors.Count != 0)
      this.ErrorsBuilder.AddErrors(fileNamesOperation.Errors);
    foreach (PrescanDBObjectRecord prescanDbObjectRecord in fileNamesOperation.Result)
    {
      this.CheckCancellationOperation();
      DBObjectGraphVertex dbObjectVertex = prescanDbObjectRecord.DBObjectVertex;
      foreach (DBObjectFileEntry file1 in prescanDbObjectRecord.Files)
      {
        this.CheckCancellationOperation();
        try
        {
          CloneDataFileProxy file2 = cloneData.CreateFile();
          file2.OriginalPath = Path.Combine(this._workspacePath, file1.OriginalName);
          file2.NewPath = file2.OriginalPath;
          cloneData.AddFile(file2);
        }
        catch (Exception ex)
        {
          this.ErrorsBuilder.AddError(new OperationError(ex.Message, vertex: dbObjectVertex));
        }
      }
    }
  }

  private ContainerValues ConvertDBObjectAttributesToFileParameters(
    DBObjectGraphVertex dbDocumentVertex,
    ValueBag attributesToWrite,
    IAttributeCodec attributeCodec,
    CADInterfaceValueBagContainer targetContainer)
  {
    EncodeAttributesOptions options = new EncodeAttributesOptions();
    options.OptimizeEmptyValues = false;
    options.ReportErrorsOnly = false;
    options.Properties.Add((StringKey) "DocumentType", (object) dbDocumentVertex.ObjectTypeId);
    ContainerValues containerValues = new ContainerValues(new ValueBag(), true);
    attributeCodec.Encode(new EncodeAttributesParams((IValueBagContainer) targetContainer, (ICollection<StringKey>) attributesToWrite.Keys, attributesToWrite, containerValues, options));
    return containerValues;
  }

  private ValueBag CollectDBObjectAttributesToWrite(
    DBObjectGraphVertex dbObjectVertex,
    ICollection<StringKey> attributeFilter,
    ICollection<StringKey> includeAlways,
    bool incudeCloneInfo)
  {
    ValueBag write = new ValueBag(attributeFilter.Count);
    foreach (DBObjectAttributeEntry attribute in (IEnumerable<DBObjectAttributeEntry>) dbObjectVertex.Attributes)
    {
      if (attributeFilter.Contains((StringKey) attribute.Name) && attribute.OriginalValues.Count == 1 && attribute.NewValues.Count == 1)
      {
        object newValue = attribute.NewValues[0];
        if (newValue != null && !object.Equals(newValue, (object) DBNull.Value) && (!object.Equals(attribute.OriginalValues[0], newValue) || includeAlways.Count != 0 && includeAlways.Contains((StringKey) attribute.Name)))
          write.Add((StringKey) attribute.Name, newValue);
      }
    }
    if (incudeCloneInfo)
    {
      write.Add((StringKey) "IsCloned", (object) "true");
      write.Add((StringKey) "CloneSessionID", (object) this._uniqueID.ToString());
    }
    write.AcceptChanges();
    return write;
  }

  private void WriteCADModelDocumentAttributes(
    DBObjectGraphVertex dbDocumentVertex,
    CADModelDesignationSettings documentDsesignationSettings,
    IAttributeCodec documentAttributeCodec,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    ICollection<StringKey> attributes = this._integratorSettingsService.SynchronizedDocumentAttributes.GetAttributes(dbDocumentVertex.ObjectTypeId, false);
    ValueBag write = this.CollectDBObjectAttributesToWrite(dbDocumentVertex, attributes, this._writeAlwaysCADModelDocumentAttributeKeys, true);
    if (write.Count == 0)
      return;
    CADInterfaceValueBagContainer valueBagContainer;
    ModelDocumentFormatter documentFormatter;
    if (documentDsesignationSettings.IndependentDesignationMode)
    {
      valueBagContainer = new CADInterfaceValueBagContainer((IParametersContainerProxy) virtualContainerSet.GetOrCreateDocumentContainer());
      documentFormatter = this._cadModelDocumentFormatterWithIndependentDesignation;
    }
    else
    {
      valueBagContainer = (CADInterfaceValueBagContainer) new CopyCADFilesOperation.DocumentWithBasicArticleContainer(virtualContainerSet.GetOrCreateDocumentContainer(), virtualContainerSet.GetOrCreateConfigurationContainer(documentDsesignationSettings.BasicArticleConfigurationName));
      documentFormatter = this._cadModelDocumentFormatter;
    }
    ContainerValues fileParameters = this.ConvertDBObjectAttributesToFileParameters(dbDocumentVertex, write, documentAttributeCodec, valueBagContainer);
    if (fileParameters.Bag.Count == 0)
      return;
    documentFormatter.Write((IValueBagContainer) valueBagContainer, fileParameters);
  }

  private void WriteCADGeneralDocumentAttributes(
    DBObjectGraphVertex dbDocumentVertex,
    IAttributeCodec documentAttributeCodec,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    ICollection<StringKey> attributes = this._integratorSettingsService.SynchronizedDocumentAttributes.GetAttributes(dbDocumentVertex.ObjectTypeId, false);
    ValueBag write = this.CollectDBObjectAttributesToWrite(dbDocumentVertex, attributes, CopyCADFilesOperation._emptyAttributeKeys, true);
    if (write.Count == 0)
      return;
    CADInterfaceValueBagContainer valueBagContainer = new CADInterfaceValueBagContainer((IParametersContainerProxy) virtualContainerSet.GetOrCreateDocumentContainer());
    ContainerValues fileParameters = this.ConvertDBObjectAttributesToFileParameters(dbDocumentVertex, write, documentAttributeCodec, valueBagContainer);
    if (fileParameters.Bag.Count == 0)
      return;
    this._cadGeneralDocumentFormatter.Write((IValueBagContainer) valueBagContainer, fileParameters);
  }

  private void WriteCADModelArticleAttributes(
    DBObjectGraphVertex dbDocumentVertex,
    string articleModelConfigurationName,
    IAttributeCodec articleAttributeCodec,
    CADVirtualParametersContainerSet virtualContainerSet)
  {
    ICollection<StringKey> attributes = this._integratorSettingsService.SynchronizedArticleAttributes.GetAttributes(dbDocumentVertex.ObjectTypeId, false);
    DBObjectGraphVertex linkedArticle = this.FindLinkedArticle(dbDocumentVertex, articleModelConfigurationName);
    ValueBag attributesToWrite = linkedArticle != null ? this.CollectDBObjectAttributesToWrite(linkedArticle, attributes, this._writeAlwaysCADModelArticleAttributeKeys, false) : new ValueBag(attributes.Count);
    foreach (ValueRecord articleAttributeValue in this._emptyCADModelArticleAttributeValues)
    {
      if (!attributesToWrite.Exists(articleAttributeValue.Key))
        attributesToWrite.Add(articleAttributeValue.Clone());
    }
    attributesToWrite.AcceptChanges();
    if (attributesToWrite.Count == 0)
      return;
    CADInterfaceValueBagContainer valueBagContainer = new CADInterfaceValueBagContainer((IParametersContainerProxy) virtualContainerSet.GetOrCreateConfigurationContainer(articleModelConfigurationName));
    ContainerValues fileParameters = this.ConvertDBObjectAttributesToFileParameters(dbDocumentVertex, attributesToWrite, articleAttributeCodec, valueBagContainer);
    if (fileParameters.Bag.Count == 0)
      return;
    this._cadModelArticleFormatter.Write((IValueBagContainer) valueBagContainer, fileParameters);
  }

  private void CheckLinkedArticlesMappingData(DBObjectGraphVertex dbDocumentVertex)
  {
    foreach (DBObjectGraphEdge inEdge in (IEnumerable<DBObjectGraphEdge>) this._session.Graph.GetInEdges(dbDocumentVertex, (Predicate<DBObjectGraphEdge>) (x => x.IsArticleDocumentation())))
    {
      ArticleDocumentationTrait trait = inEdge.GetTrait<ArticleDocumentationTrait>();
      if (trait.IsBasedOnCADModel && string.IsNullOrEmpty(trait.CADConfigurationName))
      {
        DBObjectGraphVertex source = inEdge.Source;
        this.ErrorsBuilder.AddError(new OperationError($"У изделия '{source.Caption}' (ид.версии = {source.ObjectId}) не заполнен атрибут '{this._session.Services.IntegratorsIDCache.CADConfigurationName.Text}' на связи типа '{this._session.Services.IntegratorsIDCache.ArticleToDocumentTree.Text}'. Параметры соответствующей конфигурации модели не будут заполнены значениями из мастера.", true, source));
      }
    }
  }

  private DBObjectGraphVertex FindLinkedArticle(
    DBObjectGraphVertex dbDocumentVertex,
    string articleModelConfigurationName)
  {
    ArticleDocumentationTrait trait;
    return this._session.Graph.GetInEdges(dbDocumentVertex, (Predicate<DBObjectGraphEdge>) (x => x.TryGetTrait<ArticleDocumentationTrait>(out trait) && string.Equals(trait.CADConfigurationName, articleModelConfigurationName, StringComparison.CurrentCultureIgnoreCase))).FirstOrDefault<DBObjectGraphEdge>()?.Source;
  }

  private DBObjectGraphVertex FindLinkedCADModel(DBObjectGraphVertex dbArticleVertex)
  {
    return this._session.Graph.GetOutEdges(dbArticleVertex, (Predicate<DBObjectGraphEdge>) (x => x.Target.IsDocument() && !x.Target.IsCADModelDrawing())).FirstOrDefault<DBObjectGraphEdge>()?.Target;
  }

  private void AddModelConfigurationParametersToCloneData(
    DBObjectGraphVertex dbDocumentVertex,
    string originalMainFilePath,
    string newMainFilePath,
    CADConfigurationTable modelConfigurationTable,
    CADVirtualParametersContainerSet virtualContainerSet,
    CloneDataProxy cloneData)
  {
    foreach (CADConfigurationTableRow row in (IEnumerable<CADConfigurationTableRow>) modelConfigurationTable.Rows)
    {
      CADVirtualParametersContainer configurationContainer = virtualContainerSet.GetOrCreateConfigurationContainer(row.Name);
      if (configurationContainer.ValueBag.Count != 0)
      {
        CloneDataFileParametersProxy fileParameters = cloneData.CreateFileParameters();
        fileParameters.SetDestination(originalMainFilePath, newMainFilePath, row.Name);
        fileParameters.AddOrUpdateParameters((ICollection<ValueRecord>) configurationContainer.ValueBag);
        cloneData.AddFileParameters(fileParameters);
      }
    }
  }

  private void AddDocumentParametersToCloneData(
    DBObjectGraphVertex dbDocumentVertex,
    string originalMainFilePath,
    string newMainFilePath,
    CADVirtualParametersContainerSet virtualContainerSet,
    CloneDataProxy cloneData)
  {
    CADVirtualParametersContainer documentContainer = virtualContainerSet.GetOrCreateDocumentContainer();
    if (documentContainer.ValueBag.Count == 0)
      return;
    CloneDataFileParametersProxy fileParameters = cloneData.CreateFileParameters();
    fileParameters.SetDestination(originalMainFilePath, newMainFilePath);
    fileParameters.AddOrUpdateParameters((ICollection<ValueRecord>) documentContainer.ValueBag);
    cloneData.AddFileParameters(fileParameters);
  }

  private sealed class DocumentWithBasicArticleContainer : CADInterfaceValueBagContainer
  {
    private CADVirtualParametersContainer basicArticleContainer;

    public DocumentWithBasicArticleContainer(
      CADVirtualParametersContainer documentContainer,
      CADVirtualParametersContainer basicArticleContainer)
      : base((IParametersContainerProxy) documentContainer)
    {
      this.basicArticleContainer = basicArticleContainer != null ? basicArticleContainer : throw new ArgumentNullException(nameof (basicArticleContainer));
    }

    public IParametersContainerProxy BasicArticleContainer
    {
      [DebuggerStepThrough] get => (IParametersContainerProxy) this.basicArticleContainer;
    }
  }

  private sealed class DocumentWithBasicArticleWriteTargetStrategy : 
    ModelParametersWriteTargetStrategy
  {
    public override bool IsIndependentDesignationMode(
      IValueBagContainer documentContainer,
      ValueBag documentParameters)
    {
      return false;
    }

    public override IValueBagContainer GetBasicArticleContainer(
      IValueBagContainer documentContainer,
      ValueBag documentParameters)
    {
      return (IValueBagContainer) new CADInterfaceValueBagContainer(((CopyCADFilesOperation.DocumentWithBasicArticleContainer) documentContainer).BasicArticleContainer);
    }
  }
}
