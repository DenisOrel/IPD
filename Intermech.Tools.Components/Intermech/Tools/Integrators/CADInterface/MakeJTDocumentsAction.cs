// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.MakeJTDocumentsAction
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public class MakeJTDocumentsAction : IAction
{
  private readonly long documentId;
  private readonly int documentType;
  private readonly List<long> jtDocuments;
  private readonly List<Exception> errors;
  private readonly IFileVault fileVault;
  private readonly IIntegratorRegistry integrators;
  private IIntegrator integrator;
  private string documentPath;

  public MakeJTDocumentsAction(long documentId, int documentType)
  {
    if (documentId == 0L)
      throw new ArgumentException();
    if (documentType == -1)
      throw new ArgumentException();
    this.documentId = documentId;
    this.documentType = documentType;
    this.jtDocuments = new List<long>();
    this.errors = new List<Exception>();
    this.fileVault = ClientContext.FileVault;
    this.integrators = ClientContext.Integrators;
  }

  public long SourceDocumentId => this.documentId;

  public int SourceDocumentType => this.documentType;

  public string SourceFilePath => this.documentPath;

  public List<long> JTDocuments => this.jtDocuments;

  public List<Exception> Errors => this.errors;

  public void Perform()
  {
    try
    {
      this.DoInitializeAction();
      this.DoPerformAction();
    }
    finally
    {
      this.DoCleanupAction();
    }
  }

  private void DoInitializeAction()
  {
    this.ClearResultProperties();
    this.integrator = this.integrators.GetIntegrator(IntegratorServices.Find(this.documentType), true);
  }

  private void DoPerformAction()
  {
    IDictionary<string, long> articleLookupTable = this.GetDbArticlesLookupTable();
    if (articleLookupTable.Count == 0)
      throw new FaultException("Невозможно создать/обновить JT-представление документа, так как в базе данных нет связанных с ним изделий, созданных интегратором. Выполните расширенное сохранение документа и повторите операцию.");
    this.EnsureResultPropertiesCapacity(articleLookupTable.Count);
    this.PublishSourceDocument();
    try
    {
      this.ScanSourceDocumentArticles((Action<ModelConfigurationPath, ModelConfigurationProxy, string>) ((articlePath, articleConfigration, articleExternalKey) =>
      {
        try
        {
          this.ProcessSourceDocumentArticle(articleLookupTable, articlePath, articleConfigration, articleExternalKey);
        }
        catch (Exception ex)
        {
          this.errors.Add(ex);
        }
      }));
    }
    finally
    {
      if (this.jtDocuments.Count != 0)
        this.MarkDocumentAsJTSource();
    }
  }

  private void DoCleanupAction()
  {
    this.integrator = (IIntegrator) null;
    this.documentPath = (string) null;
  }

  private void ClearResultProperties()
  {
    this.jtDocuments.Clear();
    this.errors.Clear();
    this.documentPath = (string) null;
  }

  private void EnsureResultPropertiesCapacity(int articleCount)
  {
    this.jtDocuments.Capacity = Math.Max(this.jtDocuments.Capacity, articleCount);
    this.errors.Capacity = Math.Max(this.errors.Capacity, articleCount);
  }

  private IDictionary<string, long> GetDbArticlesLookupTable()
  {
    DataTable documentArticles = DBDocumentHelper.FindDocumentArticles(this.documentId, VersionsRuleSources.GetEditorRule(), true);
    Dictionary<string, long> articlesLookupTable = new Dictionary<string, long>(documentArticles.Rows.Count, (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase);
    foreach (DataRow row in (InternalDataCollectionBase) documentArticles.Rows)
    {
      long int64 = Convert.ToInt64(row[1]);
      string key = (string) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBAttribute attributeById = sessionKeeper.Session.GetRelation(new Guid(Convert.ToString(row[0])), int64).GetAttributeByID(IDCache.Default.ObjectExternalKey.Id);
        if (attributeById != null)
        {
          if (!attributeById.IsNull)
            key = attributeById.AsString;
        }
      }
      if (key != null)
        articlesLookupTable.Add(key, int64);
      else if (UIReport.Enabled)
        UIReport.ReportEvent($"Изделие с ид. версии '{int64}' не будет использовано при создании JT-представлений, так как на связи этого изделия с моделью нет заполненного атрибута '{IDCache.Default.ObjectExternalKey.Text}'.", TraceLevel.Warning);
    }
    return (IDictionary<string, long>) articlesLookupTable;
  }

  private void PublishSourceDocument()
  {
    this.documentPath = this.fileVault.PublishTree(this.documentId, true, VersionsRuleSources.GetEditorRule(), (IFileArea) this.fileVault.WorkArea);
  }

  private void ScanSourceDocumentArticles(
    Action<ModelConfigurationPath, ModelConfigurationProxy, string> method)
  {
    if (method == null)
      throw new ArgumentNullException(nameof (method));
    StringKey[] keyNames = CADArticleExternalKeys.GetKeyNames();
    DecodeAttributesOptions decodeOptions = DocumentAttributesOptions.GetDecodeOptions(this.documentType);
    using (CADApiSession cadApiSession = new CADApiSession(this.integrator))
    {
      foreach (Tuple<ModelConfigurationPath, ModelConfigurationProxy> configuration1 in ModelConfigurationUtils.GetConfigurationList(cadApiSession.Application.OpenDocument(this.documentPath, false)))
      {
        ModelConfigurationPath configurationPath = configuration1.Item1;
        ModelConfigurationProxy configuration2 = configuration1.Item2;
        string externalKey = CADArticleExternalKeys.GetExternalKey(CADDocumentHelper.ReadAttributes((IServiceProvider) this.integrator, configuration2, (ICollection<StringKey>) keyNames, decodeOptions).Bag, (string) configuration2.Name);
        method(configurationPath, configuration2, externalKey);
      }
    }
  }

  private void ProcessSourceDocumentArticle(
    IDictionary<string, long> articleLookupTable,
    ModelConfigurationPath articlePath,
    ModelConfigurationProxy articleConfiguration,
    string articleExternalKey = null)
  {
    if (string.IsNullOrEmpty(articleExternalKey))
      throw new FaultException($"Для конфигурации '{articlePath.TargetConfiguration}' не будет создано JT-представление, так как внешний ключ изделия в параметрах конфигурации пуст или имеет некорректное значение. Выполните расширенное сохранение для модели и повторите текущую операцию.");
    long articleId;
    if (!articleLookupTable.TryGetValue(articleExternalKey, out articleId))
      throw new FaultException($"Для конфигурации '{articlePath.TargetConfiguration}' не будет создано JT-представление, так как для этой конфигурации отсутсвует изделие в базе IPS. Выполните расширенное сохранение для модели и повторите текущую операцию.");
    if (articleLookupTable.Count != 1 && string.IsNullOrEmpty(articleConfiguration.FullPath))
      throw new FaultException($"Для конфигурации '{articlePath.TargetConfiguration}' не будет создано JT-представление, так как у нее отсутствует файл.");
    Tuple<long, string, bool> tuple = this.DoMakeJTDocument(articleExternalKey, articleId, articleConfiguration);
    this.jtDocuments.Add(tuple.Item1);
    this.FireJTDocumentCreated(tuple.Item1, tuple.Item3);
  }

  private Tuple<long, string, bool> DoMakeJTDocument(
    string articleExternalKey,
    long articleId,
    ModelConfigurationProxy articleConfiguration)
  {
    if (string.IsNullOrEmpty(articleExternalKey))
      throw new ArgumentException();
    if (articleId == 0L)
      throw new ArgumentException();
    if (articleConfiguration == null)
      throw new ArgumentNullException("articleCfg");
    long jtDocId = 0;
    string path = (string) null;
    bool isNewJTDoc = false;
    try
    {
      jtDocId = JTLinkManager.FindJTDocument(this.documentId, articleExternalKey);
      if (jtDocId == 0L)
      {
        isNewJTDoc = true;
        jtDocId = this.CreateNewJTDocument(articleExternalKey);
      }
      path = this.UpdateJTDocumentFile(articleConfiguration, jtDocId, isNewJTDoc);
      this.UpdateJTDocumentAttributes(articleId, jtDocId);
      return Tuple.Create<long, string, bool>(jtDocId, path, isNewJTDoc);
    }
    catch
    {
      if (isNewJTDoc)
      {
        if (jtDocId != 0L)
          this.DeleteNewJTDocument(jtDocId);
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
          File.Delete(path);
      }
      throw;
    }
  }

  private long CreateNewJTDocument(string articleExternalKey)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(IDCache.Default.JTDocuments.Id);
      long fromParentVersion = JTLinkManager.FindJTDocumentFromParentVersion(this.documentId, articleExternalKey);
      IDBObject jtDocument = fromParentVersion != 0L ? objectCollection.CreateVersion(fromParentVersion) : objectCollection.Create();
      JTLinkManager.WriteReferenceToSourceDocument(jtDocument, this.documentId, articleExternalKey);
      jtDocument.CommitCreation(true);
      return jtDocument.ObjectID;
    }
  }

  private void DeleteNewJTDocument(long jtDocId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(jtDocId, true).Delete(0L);
  }

  private string UpdateJTDocumentFile(
    ModelConfigurationProxy articleConfiguration,
    long jtDocId,
    bool isNewJTDoc)
  {
    string str1 = articleConfiguration.FullPath;
    if (string.IsNullOrEmpty(str1))
      str1 = articleConfiguration.Document.FullName;
    string str2 = this.fileVault.PublishTree(jtDocId, false, VersionsRuleSources.GetEditorRule(), (IFileArea) this.fileVault.WorkArea);
    if (string.IsNullOrEmpty(str2))
      str2 = Path.ChangeExtension(str1, ".jt");
    if (!this.CanUpdateJTDocumentFile(str2, jtDocId))
      throw new FaultException($"Не удалось создать JT-представление, так как имя файла '{str2}' уже занято.");
    (PathUtils.IsSamePath(str1, articleConfiguration.Document.FullName) ? articleConfiguration.Document : articleConfiguration.Document.CADSystem.OpenDocument(str1, false)).Export(str2);
    string relativePath = PathUtils.GetRelativePath(str2, this.fileVault.WorkArea.AreaPath, RelativePathOptions.ThrowIfNotPossible);
    UploadFileAction uploadResult = new UploadFileAction(FileState.FromFile(str2, relativePath), str2);
    uploadResult.AllowNewFiles = isNewJTDoc;
    FileOperations.BatchUpdateFiles(jtDocId, (IList<IFileAttributeAction>) new IFileAttributeAction[1]
    {
      (IFileAttributeAction) uploadResult
    });
    new TrackUploadedFileAction(this.fileVault.WorkArea.FileTracker, jtDocId, (IObjectFilesUploadResult) uploadResult).Perform();
    return str2;
  }

  private bool CanUpdateJTDocumentFile(string jtFilePath, long jtDocId)
  {
    FileOrigin fileOrigin = this.fileVault.WorkArea.GetFileOrigin(jtFilePath, false);
    if (fileOrigin.OriginType == FileOriginType.NewFile)
      return true;
    return fileOrigin.OriginType == FileOriginType.WorkFile && fileOrigin.WorkObject != null && fileOrigin.WorkObject.ObjectId == jtDocId;
  }

  private void UpdateJTDocumentAttributes(long articleId, long jtDocId)
  {
    ValueBag valueBag = AlternativeRepresentationsHelper.CopyAttributes(articleId, -1);
    ValueRecord valueRecord1 = valueBag.Find((StringKey) IDCache.Default.Designation.Text);
    if (valueRecord1 != null)
      valueRecord1.Value = (object) DocumentDesignationHelper.AppendDocCode((string) valueRecord1.Value, IDCache.Default.JTDocuments.Id);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(jtDocId);
      foreach (ValueRecord valueRecord2 in valueBag)
      {
        IDBAttribute attributeByName = dbObject.GetAttributeByName((string) valueRecord2.Key);
        if (attributeByName != null && !attributeByName.ReadOnly)
          attributeByName.Value = valueRecord2.Value;
      }
    }
  }

  private void MarkDocumentAsJTSource()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(this.documentId).SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(IDCache.Default.JTSourceDocumentMarker.Id, (object) true)
      });
  }

  private void FireJTDocumentCreated(long jtDocId, bool isNewJTDoc)
  {
    ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true).FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs(isNewJTDoc ? "ObjectsCreated" : "ObjectsChanged", jtDocId));
  }
}
