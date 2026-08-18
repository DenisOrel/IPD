// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.COM.SearchAPIBase
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Cadmech.Integrator.Properties;
using Intermech.Client.Core;
using Intermech.Commands;
using Intermech.ControlFlow;
using Intermech.DataFormats;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Pdm;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace Intermech.Cadmech.Integrator.COM;

public abstract class SearchAPIBase : SingleThreadedObject, ISearchAPI, ISearchAPISpecifications
{
  private static readonly Guid legacyArtIdAttribute = new Guid("cad00622-306c-11d8-b4e9-00304f19f545");
  private static readonly Guid legacyDocIdAttribute = new Guid("cad00623-306c-11d8-b4e9-00304f19f545");
  private const int legacyUnknownObjectId = -1;
  private SearchAPIBase.ErrorCodes errorCode;
  private string errorMessage;
  private bool displayErrors;
  private const int apiVersionsId = 20000;
  private const string outputViewCategory = "SearchAPI";
  private Guid integratorId;
  private IIntegrator integrator;
  private string integratorName;
  private long openArticleId;
  private long openDocumentId;
  private List<string> docArticles;
  private List<Guid> selectedDocs;
  private List<Guid> selectedArticles;
  private SearchAPIServiceLink serviceLink;

  protected SearchAPIBase()
  {
    this.openArticleId = 0L;
    this.openDocumentId = 0L;
    this.docArticles = new List<string>(256 /*0x0100*/);
    this.selectedDocs = new List<Guid>(32 /*0x20*/);
    this.selectedArticles = new List<Guid>(32 /*0x20*/);
  }

  internal void Initialize(SearchAPIServiceLink serviceLink)
  {
    this.serviceLink = serviceLink != null ? serviceLink : throw new ArgumentNullException(nameof (serviceLink));
  }

  internal SearchAPIServiceLink ServiceLink
  {
    [DebuggerStepThrough] get
    {
      return this.serviceLink != null ? this.serviceLink : throw new InvalidOperationException("Method Initialize() must be called first.");
    }
  }

  internal IFileVault FileVaultService
  {
    [DebuggerStepThrough] get => this.ServiceLink.FileVaultService;
  }

  internal IArticleService PdmArticleService
  {
    [DebuggerStepThrough] get => this.ServiceLink.PdmArticleService.Value;
  }

  public bool KnockKnock() => true;

  public int GetVersion() => 20000;

  public Guid IntegratorId
  {
    get => this.integratorId;
    set
    {
      if (!(this.integratorId != value))
        return;
      this.integratorId = value;
      this.ResetIntegrator();
    }
  }

  public void OpenArticleByGuid(string articleGuid)
  {
    this.SetNoError();
    try
    {
      Guid guidArgument = SearchAPIBase.ParseGuidArgument(articleGuid, nameof (articleGuid));
      this.CloseArticleInternal();
      this.openArticleId = this.ConvertToObjectVersionId(guidArgument, true);
    }
    catch (Exception ex)
    {
      this.ShowException(ex, false);
    }
  }

  public void OpenArticleByID(int artId)
  {
    this.SetNoError();
    try
    {
      this.CloseArticleInternal();
      this.openArticleId = this.GetArticleIdByLegacyId(artId);
    }
    catch (Exception ex)
    {
      this.ShowException(ex, false);
    }
  }

  public void CloseArticle()
  {
    this.SetNoError();
    this.CloseArticleInternal();
  }

  private void CloseArticleInternal() => this.openArticleId = 0L;

  public string GetFieldValue_Articles(string fieldGuidOrName)
  {
    this.SetNoError();
    try
    {
      if (string.IsNullOrEmpty(fieldGuidOrName))
        throw new ArgumentException("Не задан идентификатор атрибута изделия.", nameof (fieldGuidOrName));
      this.CheckArticleOpen();
      string fieldValueArticles = this.GetVirtualValue_Articles(fieldGuidOrName);
      if (fieldValueArticles != null)
        return fieldValueArticles;
      SearchAPIBase.FieldAttribute fieldAttr = SearchAPIBase.DecodeAttributeId(fieldGuidOrName);
      if (fieldAttr == null)
      {
        this.LogMessage($"Атрибут изделия '{fieldGuidOrName}' отсутствует в базе IPS.");
        return string.Empty;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.openArticleId, true);
        fieldValueArticles = this.TransformFieldValue_Articles(fieldAttr, this.ReadObjectField(dbObject, fieldAttr));
      }
      return fieldValueArticles;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  private string GetVirtualValue_Articles(string fieldGuidOrName) => (string) null;

  private string TransformFieldValue_Articles(
    SearchAPIBase.FieldAttribute fieldAttr,
    Tuple<object, string> fieldValuePair)
  {
    if (fieldAttr.Id == IDCache.Default.Mass.Id)
      return SearchAPIBase.TransformMassValue(fieldAttr, fieldValuePair);
    return fieldAttr.Id == IDCache.Default.Material.Id ? this.TransformMaterialValue(fieldAttr, fieldValuePair) : fieldValuePair.Item2;
  }

  private void CheckArticleOpen()
  {
    if (this.openArticleId == 0L)
      throw new InvalidOperationException("Изделие не определено методом OpenArticle.");
  }

  public void OpenDocumentByGuid(string documentGuid)
  {
    this.SetNoError();
    try
    {
      Guid guidArgument = SearchAPIBase.ParseGuidArgument(documentGuid, nameof (documentGuid));
      this.CloseDocumentInternal();
      this.openDocumentId = this.ConvertToObjectVersionId(guidArgument, true);
    }
    catch (Exception ex)
    {
      this.ShowException(ex, false);
    }
  }

  public void OpenDocumentByID(int docId)
  {
    this.SetNoError();
    try
    {
      this.CloseDocumentInternal();
      this.openDocumentId = this.GetDocumentIdByLegacyId(docId);
    }
    catch (Exception ex)
    {
      this.ShowException(ex, false);
    }
  }

  public void CloseDocument()
  {
    this.SetNoError();
    this.CloseDocumentInternal();
  }

  public void CloseDocumentInternal() => this.openDocumentId = 0L;

  public string GetFieldValue(string fieldGuidOrName)
  {
    this.SetNoError();
    try
    {
      if (string.IsNullOrEmpty(fieldGuidOrName))
        throw new ArgumentException("Не задан идентификатор атрибута документа.", nameof (fieldGuidOrName));
      this.CheckDocumentOpen();
      string fieldValue = this.GetVirtualValue(fieldGuidOrName);
      if (fieldValue != null)
        return fieldValue;
      SearchAPIBase.FieldAttribute fieldAttr = SearchAPIBase.DecodeAttributeId(fieldGuidOrName);
      if (fieldAttr == null)
      {
        this.LogMessage($"Атрибут документа '{fieldGuidOrName}' отсутствует в базе IPS.");
        return string.Empty;
      }
      long objectID = this.openDocumentId;
      if (fieldAttr.Id == IDCache.Default.Mass.Id || fieldAttr.Id == IDCache.Default.Material.Id)
      {
        long articleForOpenDocument = this.FindBaseArticleForOpenDocument();
        if (articleForOpenDocument == 0L)
        {
          this.LogMessage($"Не удалось найти основное исполнение изделия, выпускаемого по документу с идентификатором версии {this.openDocumentId}. Значение атрибута '{fieldAttr}' не может быть прочитано.");
          return string.Empty;
        }
        objectID = articleForOpenDocument;
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, true);
        fieldValue = this.TransformFieldValue(fieldAttr, this.ReadObjectField(dbObject, fieldAttr));
      }
      return fieldValue;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  private string GetVirtualValue(string fieldGuidOrName)
  {
    if (string.Compare(fieldGuidOrName, Resources.SR_LegacyDocTypeField, true) == 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(this.openDocumentId, true);
        return sessionKeeper.Session.GetObjectType(dbObject.ObjectType).ObjectTypeName;
      }
    }
    if (string.Compare(fieldGuidOrName, Resources.SR_LegacyFileNameField, true) != 0)
      return (string) null;
    this.CheckDocumentOpen();
    string masterFileName = this.FileVaultService.DBFilesInfo.GetMasterFileName(this.openDocumentId, false);
    return !string.IsNullOrEmpty(masterFileName) ? masterFileName : string.Empty;
  }

  private string TransformFieldValue(
    SearchAPIBase.FieldAttribute fieldAttr,
    Tuple<object, string> fieldValuePair)
  {
    if (fieldAttr.Id == IDCache.Default.Mass.Id)
      return SearchAPIBase.TransformMassValue(fieldAttr, fieldValuePair);
    return fieldAttr.Id == IDCache.Default.Material.Id ? this.TransformMaterialValue(fieldAttr, fieldValuePair) : fieldValuePair.Item2;
  }

  public void SetFieldValue(string fieldGuidOrName, string fieldValue)
  {
    this.SetNoError();
    try
    {
      if (string.IsNullOrEmpty(fieldGuidOrName))
        throw new ArgumentException("Не задан идентификатор атрибута документа.", nameof (fieldGuidOrName));
      this.CheckDocumentOpen();
      if (this.SetVirtualValue(fieldGuidOrName, fieldValue))
        return;
      SearchAPIBase.FieldAttribute fieldAttr = SearchAPIBase.DecodeAttributeId(fieldGuidOrName);
      if (fieldAttr == null)
      {
        this.LogMessage($"Атрибут документа '{fieldGuidOrName}' отсутствует в базе IPS.");
      }
      else
      {
        long objectID = this.openDocumentId;
        if (fieldAttr.Id == IDCache.Default.Mass.Id || fieldAttr.Id == IDCache.Default.Material.Id)
        {
          long articleForOpenDocument = this.FindBaseArticleForOpenDocument();
          if (articleForOpenDocument == 0L)
          {
            this.LogMessage($"Не удалось найти основное исполнение изделия, выпускаемого по документу с идентификатором версии {this.openDocumentId}. Значение атрибута '{fieldAttr}' не может быть записано в базу IPS.");
            return;
          }
          try
          {
            objectID = this.PrepareForEdit(articleForOpenDocument);
          }
          catch (AbortException ex)
          {
            this.LogMessage($"Не удалось взять на изменение основное исполнение изделия, выпускаемого по документу с идентификатором версии {this.openDocumentId}. Значение атрибута '{fieldAttr}' не может быть записано в базу IPS.");
            return;
          }
        }
        int objectType;
        object obj;
        object initValue;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, true);
          objectType = dbObject.ObjectType;
          obj = this.UntransformFieldValue(fieldAttr, fieldValue);
          initValue = this.WriteObjectField(dbObject, fieldAttr, obj);
        }
        if (fieldAttr.Id != IDCache.Default.Designation.Id && fieldAttr.Id != IDCache.Default.Name.Id)
          return;
        DBObjectsExtendedEventArgs e = new DBObjectsExtendedEventArgs(objectID, objectType, new AttributeValues(fieldAttr.Id, initValue), new AttributeValues(fieldAttr.Id, obj));
        try
        {
          this.ServiceLink.NotificationService.FireEvent((object) null, (NotificationEventArgs) e);
        }
        catch (Exception ex)
        {
          this.LogMessage($"Ошибка синхронного изменения атрибута '{fieldAttr}'.");
          this.LogMessage(ex.Message);
        }
      }
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  private bool SetVirtualValue(string fieldGuidOrName, string fieldValue)
  {
    return string.Compare(fieldGuidOrName, Resources.SR_LegacyDocTypeField, true) == 0;
  }

  private object UntransformFieldValue(SearchAPIBase.FieldAttribute fieldAttr, string fieldValue)
  {
    return fieldAttr.Id == IDCache.Default.Material.Id ? this.UntransformMaterialValue(fieldAttr, fieldValue) : (object) fieldValue;
  }

  private long FindBaseArticleForOpenDocument()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject baseArticle = this.PdmArticleService.FindBaseArticle(this.openDocumentId, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session);
      return baseArticle != null ? baseArticle.ObjectID : 0L;
    }
  }

  public void EditParameters()
  {
    this.SetNoError();
    try
    {
      this.CheckDocumentOpen();
      int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, this.openDocumentId);
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  public string CopyToDir(string dirName)
  {
    this.SetNoError();
    try
    {
      this.CheckDocumentOpen();
      return this.FileVaultService.PublishTree(this.openDocumentId, true, VersionsRuleSources.GetEditorRule(), (IFileArea) this.FileVaultService.WorkArea);
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public void CheckOut()
  {
    this.SetNoError();
    try
    {
      this.CheckDocumentOpen();
      if (this.openDocumentId <= 0L)
        return;
      ObjectCopyCommand checkoutCommand = ObjectCommandFactory.CreateCheckoutCommand(true);
      checkoutCommand.ObjectId = this.openDocumentId;
      checkoutCommand.Execute();
      this.openDocumentId = checkoutCommand.NewObjectId;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  public void CheckIn()
  {
    this.SetNoError();
    try
    {
      this.CheckDocumentOpen();
      if (this.openDocumentId >= 0L)
        return;
      ObjectCopyCommand checkinCommand = ObjectCommandFactory.CreateCheckinCommand(true);
      checkinCommand.ObjectId = this.openDocumentId;
      checkinCommand.Execute();
      this.openDocumentId = checkinCommand.NewObjectId;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  public string CreateFileDocument(string fullPath, string objectTypeCode, string documentGuid)
  {
    this.SetNoError();
    try
    {
      if (string.IsNullOrEmpty(fullPath))
        throw new ArgumentException("Не задан путь к регистрируемому документу.", nameof (fullPath));
      if (!File.Exists(fullPath))
        throw new FileNotFoundException($"Файл '{fullPath}' не найден на диске, его регистрация в IPS невозможна.");
      FileOrigin fileOrigin = this.FileVaultService.FindArea(fullPath) == this.FileVaultService.WorkArea ? this.FileVaultService.WorkArea.GetFileOrigin(fullPath, false) : (FileOrigin) null;
      long objectID;
      if (fileOrigin != null && fileOrigin.OriginType == FileOriginType.WorkFile)
      {
        objectID = fileOrigin.WorkObject.ObjectId;
      }
      else
      {
        using (new DynamicScope())
        {
          if (UIVars.UICommand.Value == null)
            UIVars.UICommand.Declare(new UICommandInfo("Импорт чертежа"));
          FileVars.SoftMode.Declare(false);
          this.CreateFileImportContext(fullPath, objectTypeCode);
          AcadFileImportService importSvc = ServiceUtils.GetService<AcadFileImportService>((object) this.Integrator, true);
          TransferFileToWorkspaceMode transferFileToWorkspace = importSvc.AllowTransferFileToWorkspace;
          importSvc.AllowTransferFileToWorkspace = TransferFileToWorkspaceMode.None;
          try
          {
            objectID = ProgressSinks.DialogService.Invoke<long>($"Импорт файла '{Path.GetFileName(fullPath)}'", ProgressSinkDialogFlags.Default, (System.Func<IPercentageProgressSink, long>) (progressSink => importSvc.ImportFile(fullPath, new FileImportOptions()
            {
              NotifyOnDeferredFilesErrors = true,
              ProgressSink = progressSink
            }).UnwrapObjectId()));
          }
          finally
          {
            importSvc.AllowTransferFileToWorkspace = transferFileToWorkspace;
          }
        }
      }
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectID, true);
        if (dbObject.ObjectModifyMode != ObjectModifyModes.InBase && dbObject.CheckoutBy == 0L)
          dbObject = dbObject.CheckOut();
        if (GuidHelper.IsGuid(documentGuid))
        {
          Guid guid = new Guid(documentGuid);
          if (dbObject.GUID != guid)
            dbObject.GUID = guid;
        }
        return dbObject.GUID.ToString();
      }
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  protected abstract void CreateFileImportContext(string fullPath, string objectTypeCode);

  public string GenerateFileName(string prefix, string @extension)
  {
    this.SetNoError();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return ServiceUtils.GetService<IFileNameGenerator>((object) sessionKeeper.Session, true).GenerateFileName((object) sessionKeeper.Session.SessionGUID, prefix, @extension);
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public void SetDocType(string docTypeName)
  {
    this.SetNoError();
    try
    {
      this.CheckDocumentOpen();
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  public long GetDocStatus()
  {
    this.SetNoError();
    try
    {
      this.CheckDocumentOpen();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetObject(this.openDocumentId, true).CheckoutBy;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return 0;
    }
  }

  private void CheckDocumentOpen()
  {
    if (this.openDocumentId == 0L)
      throw new InvalidOperationException("Документ не определен функцией OpenDocument.");
  }

  public void OpenDocArticlesByGuid(string documentGuid)
  {
    this.SetNoError();
    try
    {
      Guid guidArgument = SearchAPIBase.ParseGuidArgument(documentGuid, nameof (documentGuid));
      this.CloseDocArticlesInternal();
      this.OpenDocArticlesCore(this.ConvertToObjectVersionId(guidArgument, true));
    }
    catch (Exception ex)
    {
      this.CloseDocArticlesInternal();
      this.ShowException(ex, false);
    }
  }

  public void OpenDocArticlesByID(int docId)
  {
    this.SetNoError();
    try
    {
      this.CloseDocArticlesInternal();
      this.OpenDocArticlesCore(this.GetDocumentIdByLegacyId(docId));
    }
    catch (Exception ex)
    {
      this.CloseDocArticlesInternal();
      this.ShowException(ex, false);
    }
  }

  private void OpenDocArticlesCore(long documentVersion)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long article in this.PdmArticleService.FindArticles(documentVersion, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session))
      {
        string str = sessionKeeper.Session.GetObject(article, true).GUID.ToString();
        if (!this.docArticles.Contains(str))
          this.docArticles.Add(str);
      }
    }
  }

  public void CloseDocArticles()
  {
    this.SetNoError();
    this.CloseDocArticlesInternal();
  }

  private void CloseDocArticlesInternal() => this.docArticles.Clear();

  public int GetArticlesCount()
  {
    this.SetNoError();
    return this.docArticles.Count;
  }

  public string GetDocArticleID(int index)
  {
    this.SetNoError();
    try
    {
      if (index >= 0 && index < this.docArticles.Count)
        return this.docArticles[index];
      throw new SilentException("Исполнения изделия, выпускаемого по документу, не определены функцией OpenDocArticles, либо отсутствуют в базе IPS.");
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public string GetArtGuid_byDocGuid(string documentGuid)
  {
    this.SetNoError();
    try
    {
      long objectVersionId = this.ConvertToObjectVersionId(SearchAPIBase.ParseGuidArgument(documentGuid, nameof (documentGuid)), true);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return (this.PdmArticleService.FindBaseArticle(objectVersionId, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session) ?? throw new SilentException($"Основное исполнение изделия, выпускаемого по документу с идентификатором версии {objectVersionId}, отсутствует в базе IPS.")).GUID.ToString();
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public int GetArtId_byDocId(int docId)
  {
    this.SetNoError();
    try
    {
      long documentIdByLegacyId = this.GetDocumentIdByLegacyId(docId);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject baseArticle = this.PdmArticleService.FindBaseArticle(documentIdByLegacyId, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session);
        IDBAttribute dbAttribute = baseArticle != null ? baseArticle.GetAttributeByGuid(SearchAPIBase.legacyArtIdAttribute) : throw new SilentException($"Основное исполнение изделия, выпускаемого по документу с идентификатором версии {documentIdByLegacyId}, отсутствует в базе IPS.");
        return dbAttribute != null && !dbAttribute.IsNull ? (int) dbAttribute.AsInteger : throw new SilentException($"У изделия '{baseArticle.NameInMessages}' не заполнен атрибут 'Идентификатор изделия' (ART_ID).");
      }
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return 0;
    }
  }

  public string ActiveArtGUID()
  {
    this.SetNoError();
    try
    {
      string empty = string.Empty;
      if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) is ISimpleSelectedItems service && service.Count == 1)
      {
        if (service.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.Value, false);
            if (dbObject != null)
              empty = dbObject.GUID.ToString();
          }
        }
      }
      else if (service != null)
      {
        if (service.Count > 0)
          this.LogMessage("В IPS выбрано более 1 объекта.");
        else
          this.LogMessage("В IPS не выбрано ни одного объекта.");
      }
      return empty;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return string.Empty;
    }
  }

  public int ActiveArtID()
  {
    this.SetNoError();
    try
    {
      int num = -1;
      if (ServicesManager.GetService(typeof (ISimpleSelectedItems)) is ISimpleSelectedItems service && service.Count == 1)
      {
        if (service.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(itemData.Value, false);
            if (dbObject != null)
            {
              IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(SearchAPIBase.legacyArtIdAttribute);
              if (attributeByGuid != null && !attributeByGuid.IsNull)
                num = Convert.ToInt32(attributeByGuid.AsInteger);
              else
                this.LogMessage($"ActiveArtID: у объекта '{dbObject.NameInMessages}' не заполнен атрибут 'Идентификатор изделия' (ART_ID).");
            }
          }
        }
      }
      else if (service != null)
      {
        if (service.Count > 0)
          this.LogMessage("В IPS выбрано более 1 объекта.");
        else
          this.LogMessage("В IPS не выбрано ни одного объекта.");
      }
      return num;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return 0;
    }
  }

  public long GetUserId()
  {
    this.SetNoError();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.UserID;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return 0;
    }
  }

  public string GetUserFullName_ByUserID(long userId)
  {
    this.SetNoError();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetObject(userId, true).Caption;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public int GetDocTypeByDocTypeName(string docTypeName)
  {
    this.SetNoError();
    try
    {
      return (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(docTypeName, true).ObjectType;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return 0;
    }
  }

  public string GetDocVersionByFileName(string fullPath)
  {
    this.SetNoError();
    try
    {
      if (this.FileVaultService.FindArea(fullPath) == this.FileVaultService.WorkArea)
      {
        FileOrigin fileOrigin = this.FileVaultService.WorkArea.GetFileOrigin(fullPath, false);
        if (fileOrigin.OriginType == FileOriginType.WorkFile)
        {
          DBObjectState workObject = fileOrigin.WorkObject;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
            return sessionKeeper.Session.GetObject(workObject.ObjectId, true).ObjectGUID.ToString();
        }
      }
      this.LogMessage($"GetDocVersionByFileName: файл '{fullPath}' не зарегистрирован в IPS.");
      return string.Empty;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return string.Empty;
    }
  }

  public string GetDocFileName(string documentGuid)
  {
    this.SetNoError();
    try
    {
      long objectVersionId = this.ConvertToObjectVersionId(SearchAPIBase.ParseGuidArgument(documentGuid, "documentGuidValue"), false);
      if (objectVersionId != 0L && this.FileVaultService.WorkArea.IsObjectPublished(objectVersionId))
      {
        string masterFileName = this.FileVaultService.DBFilesInfo.GetMasterFileName(objectVersionId, false);
        if (masterFileName != null)
        {
          string path = Path.Combine(this.FileVaultService.WorkArea.AreaPath, masterFileName);
          if (File.Exists(path))
            return path;
        }
      }
      return string.Empty;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return string.Empty;
    }
  }

  public int GetDocID_byFileName(string fullPath)
  {
    this.SetNoError();
    try
    {
      if (this.FileVaultService.FindArea(fullPath) == this.FileVaultService.WorkArea)
      {
        FileOrigin fileOrigin = this.FileVaultService.WorkArea.GetFileOrigin(fullPath, false);
        if (fileOrigin.OriginType == FileOriginType.WorkFile)
        {
          DBObjectState workObject = fileOrigin.WorkObject;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(workObject.ObjectId, false);
            if (dbObject != null)
            {
              IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(SearchAPIBase.legacyDocIdAttribute);
              if (attributeByGuid != null && !attributeByGuid.IsNull)
                return (int) attributeByGuid.AsInteger;
              this.LogMessage($"GetDocID_byFileName: у объекта '{dbObject.NameInMessages}' не заполнен атрибут 'Идентификатор документа' (DOC_ID).");
              return -1;
            }
          }
        }
      }
      this.LogMessage($"GetDocID_byFileName: файл '{fullPath}' не зарегистрирован в IPS.");
      return -1;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return -1;
    }
  }

  public string GetDocGuid_byFileName(string fullPath)
  {
    this.SetNoError();
    try
    {
      if (this.FileVaultService.FindArea(fullPath) == this.FileVaultService.WorkArea)
      {
        FileOrigin fileOrigin = this.FileVaultService.WorkArea.GetFileOrigin(fullPath, false);
        if (fileOrigin.OriginType == FileOriginType.WorkFile)
        {
          DBObjectState workObject = fileOrigin.WorkObject;
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(workObject.ObjectId, false);
            if (dbObject != null)
              return dbObject.GUID.ToString();
          }
        }
      }
      this.LogMessage($"GetDocGuid_byFileName: файл '{fullPath}' не зарегистрирован в IPS.");
      return Guid.Empty.ToString();
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return Guid.Empty.ToString();
    }
  }

  public string GetDocTypeNameInDocs(int DocTypeID)
  {
    this.SetNoError();
    try
    {
      DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(DocTypeID);
      return settings.DocumentNameInStamp ? settings.DocumentTypeName : string.Empty;
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return string.Empty;
    }
  }

  public string GetWorkFolder()
  {
    this.SetNoError();
    try
    {
      StringBuilder stringBuilder = new StringBuilder(this.FileVaultService.WorkArea.AreaPath);
      if (stringBuilder.Length > 0 && (int) stringBuilder[stringBuilder.Length - 1] != (int) Path.DirectorySeparatorChar)
        stringBuilder.Append(Path.DirectorySeparatorChar);
      return stringBuilder.ToString();
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public string GetArticleVersion(string articleGuid)
  {
    this.SetNoError();
    try
    {
      return this.ConvertToObjectVersionGuid(SearchAPIBase.ParseGuidArgument(articleGuid, nameof (articleGuid)), true).ToString();
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public void StartSelectArticles() => this.SetNoError();

  public void SelectArticles()
  {
    this.SetNoError();
    try
    {
      this.selectedArticles.AddRange((IEnumerable<Guid>) SearchAPIBase.SelectObjects((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(IDCache.Default.AllArticles.Id), "Выберите изделия"));
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  public int SelectedArticlesCount()
  {
    this.SetNoError();
    return this.selectedArticles.Count;
  }

  public string GetSelectedArticleID(int index)
  {
    this.SetNoError();
    try
    {
      if (index >= 0 && index < this.selectedArticles.Count)
        return this.selectedArticles[index].ToString();
      throw new ArgumentOutOfRangeException($"Изделия с индексом {index} нет в списке выбранных изделий.");
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public void EndSelectArticles()
  {
    this.SetNoError();
    this.selectedArticles.Clear();
  }

  public void StartSelectDocs() => this.SetNoError();

  public void SelectDocs()
  {
    this.SetNoError();
    try
    {
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (LocalId<int> selectableDocumentType in this.GetSelectableDocumentTypes())
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(selectableDocumentType.Id));
      this.selectedDocs.AddRange((IEnumerable<Guid>) SearchAPIBase.SelectObjects((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor($"Документы {this.ApplicationName}", descriptors), "Выберите документы"));
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  protected abstract List<LocalId<int>> GetSelectableDocumentTypes();

  public int SelectedDocsCount()
  {
    this.SetNoError();
    return this.selectedDocs.Count;
  }

  public string GetSelectedDocID(int index)
  {
    this.SetNoError();
    try
    {
      if (index >= 0 && index < this.selectedDocs.Count)
        return this.selectedDocs[index].ToString();
      throw new ArgumentOutOfRangeException($"Документа с индексом {index} нет в списке выбранных документов.");
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public void EndSelectDocs()
  {
    this.SetNoError();
    this.selectedDocs.Clear();
  }

  public string CreateSpecification(
    string dwgPath,
    string inpFieldLayout,
    string outFieldLayout,
    string structFileContent,
    string passportData)
  {
    this.SetNoError();
    try
    {
      SpecificationUpdater specificationUpdater = new SpecificationUpdater(this.Integrator, this.ServiceLink);
      StructData orUpdateProjects = specificationUpdater.CreateOrUpdateProjects(dwgPath, inpFieldLayout, structFileContent, passportData);
      specificationUpdater.CheckoutProjects(orUpdateProjects);
      specificationUpdater.EditSpecification(orUpdateProjects);
      return specificationUpdater.CreateStructFileContent(dwgPath, outFieldLayout, orUpdateProjects);
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (string) null;
    }
  }

  public ICreateSpecificationAsyncTask CreateSpecificationAsync(
    string dwgPath,
    string inpFieldLayout,
    string outFieldLayout,
    string structFileContent,
    string passportData)
  {
    this.SetNoError();
    try
    {
      SpecificationUpdater updater = new SpecificationUpdater(this.Integrator, this.ServiceLink);
      StructData structData = updater.CreateOrUpdateProjects(dwgPath, inpFieldLayout, structFileContent, passportData);
      updater.CheckoutProjects(structData);
      return (ICreateSpecificationAsyncTask) new CreateSpecificationAsyncTask(Task.Factory.StartNew<string>((Func<string>) (() =>
      {
        updater.EditSpecification(structData);
        return updater.CreateStructFileContent(dwgPath, outFieldLayout, structData);
      })));
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return (ICreateSpecificationAsyncTask) null;
    }
  }

  public string GetPositions(string dwgFullPath, string settingsFullPath)
  {
    this.errorCode = SearchAPIBase.ErrorCodes.Error;
    this.errorMessage = "Метод GetPositions не реализован.";
    return (string) null;
  }

  private long SwitchObjectVersion(long objectVersion)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersion, true);
      return sessionKeeper.Session.GetObjectByVersionsRule(dbObject.ID, VersionsRuleSources.GetEditorRule().OwnerId, true).ObjectID;
    }
  }

  private long ConvertToObjectVersionId(Guid guid, bool throwIfNotFound)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(guid, VersionsRuleSources.GetEditorRule().OwnerId, throwIfNotFound);
      return objectByVersionsRule != null ? objectByVersionsRule.ObjectID : 0L;
    }
  }

  private Guid ConvertToObjectVersionGuid(Guid guid, bool throwIfNotFound)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(guid, VersionsRuleSources.GetEditorRule().OwnerId, throwIfNotFound);
      return objectByVersionsRule != null ? objectByVersionsRule.ObjectGUID : Guid.Empty;
    }
  }

  private long FindDocumentIdByLegacyId(int docId)
  {
    ConditionStructure conditionStructure = new ConditionStructure(SearchAPIBase.legacyDocIdAttribute, RelationalOperators.Equal, (object) docId, LogicalOperators.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad00070-306c-11d8-b4e9-00304f19f545")).Select(paramSet);
    return dataTable.Rows.Count != 1 ? 0L : this.SwitchObjectVersion(Convert.ToInt64(dataTable.Rows[0][0]));
  }

  private long GetDocumentIdByLegacyId(int docId)
  {
    long documentIdByLegacyId = this.FindDocumentIdByLegacyId(docId);
    return documentIdByLegacyId != 0L ? documentIdByLegacyId : throw new FaultException($"Не удалось найти в базе IPS документ по значению атрибута 'Идентификатор документа' (DOC_ID), равному {docId}.");
  }

  private long FindArticleIdByLegacyId(int artId)
  {
    ConditionStructure conditionStructure = new ConditionStructure(SearchAPIBase.legacyArtIdAttribute, RelationalOperators.Equal, (object) artId, LogicalOperators.NONE, 0);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = 1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      conditionStructure
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      dataTable = sessionKeeper.Session.GetObjectCollection(new Guid("cad00268-306c-11d8-b4e9-00304f19f545")).Select(paramSet);
    return dataTable.Rows.Count != 1 ? 0L : this.SwitchObjectVersion(Convert.ToInt64(dataTable.Rows[0][0]));
  }

  private long GetArticleIdByLegacyId(int artId)
  {
    long articleIdByLegacyId = this.FindArticleIdByLegacyId(artId);
    return articleIdByLegacyId != 0L ? articleIdByLegacyId : throw new FaultException($"Не удалось найти в базе IPS изделие по значению атрибута 'Идентификатор изделия' (ART_ID), равному {artId}.");
  }

  private static List<Guid> SelectObjects(IDescriptor rootDescriptor, string caption)
  {
    long[] numArray = SearchAPIBase.SelectObjectVersions(rootDescriptor, caption);
    List<Guid> guidList = new List<Guid>(numArray.Length);
    if (numArray.Length != 0)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        foreach (long objectID in numArray)
        {
          Guid guid = sessionKeeper.Session.GetObject(objectID, true).GUID;
          if (!guidList.Contains(guid))
            guidList.Add(guid);
        }
      }
    }
    return guidList;
  }

  private static long[] SelectObjectVersions(IDescriptor rootDescriptor, string caption)
  {
    return Intermech.Navigator.SelectionWindow.SelectObjects(caption, string.Empty, rootDescriptor, SelectionOptions.Default | SelectionOptions.ForceFilterObjectsByRule) ?? new long[0];
  }

  private static SearchAPIBase.FieldAttribute DecodeAttributeId(string fieldGuidOrName)
  {
    int id;
    bool isObligatory;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      Guid guid;
      if (SearchAPIBase.TryParseGuid(fieldGuidOrName, out guid))
      {
        ObligatoryObjectAttributes obligatoryAttributeId = sessionKeeper.Session.IdentHelper.GetObligatoryAttributeID(guid);
        switch (obligatoryAttributeId)
        {
          case ObligatoryObjectAttributes.None:
            IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(guid, false);
            if (attributeType == null)
              return (SearchAPIBase.FieldAttribute) null;
            id = attributeType.AttributeID;
            isObligatory = false;
            break;
          case ObligatoryObjectAttributes.Zero:
            return (SearchAPIBase.FieldAttribute) null;
          default:
            id = (int) obligatoryAttributeId;
            isObligatory = true;
            break;
        }
      }
      else if (ObligatoryObjectAttributesHelper.IsObligatoryAttribute(fieldGuidOrName))
      {
        id = (int) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute(fieldGuidOrName);
        isObligatory = true;
      }
      else
      {
        IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(fieldGuidOrName, false);
        if (attributeType == null)
          return (SearchAPIBase.FieldAttribute) null;
        id = attributeType.AttributeID;
        isObligatory = false;
      }
    }
    return new SearchAPIBase.FieldAttribute(fieldGuidOrName, id, isObligatory);
  }

  private Tuple<object, string> ReadObjectField(
    IDBObject dbObject,
    SearchAPIBase.FieldAttribute fieldAttr)
  {
    if (fieldAttr.IsObligatory)
    {
      object[] valuesById = dbObject.GetValuesByID(fieldAttr.Id, true);
      return new Tuple<object, string>(valuesById[0], Convert.ToString(valuesById[0]));
    }
    IDBAttribute attributeById = dbObject.GetAttributeByID(fieldAttr.Id);
    if (attributeById != null)
      return new Tuple<object, string>(attributeById.Value, attributeById.AsString);
    this.LogMessage($"Атрибут '{fieldAttr}' не найден среди атрибутов объекта '{dbObject.NameInMessages}'.");
    return new Tuple<object, string>((object) null, string.Empty);
  }

  private object WriteObjectField(
    IDBObject dbObject,
    SearchAPIBase.FieldAttribute fieldAttr,
    object newValue)
  {
    if (fieldAttr.IsObligatory)
    {
      this.LogMessage($"Атрибут '{fieldAttr}' является обязательным, изменение его значения отклонено.");
      return (object) null;
    }
    IDBAttribute attributeById = dbObject.GetAttributeByID(fieldAttr.Id);
    if (attributeById != null)
    {
      try
      {
        if (newValue is string)
        {
          string asString = attributeById.AsString;
          attributeById.AsString = (string) newValue;
          return (object) asString;
        }
        object obj = attributeById.Value;
        attributeById.Value = newValue;
        return (object) (string) Convert.ChangeType(obj, typeof (string));
      }
      catch (KernelException ex)
      {
        this.LogMessage($"При изменении атрибута '{fieldAttr}' у объекта '{dbObject.NameInMessages}' произошла ошибка.");
        this.LogMessage(ex.Message);
        return (object) null;
      }
    }
    else
    {
      try
      {
        dbObject.Attributes.AddAttribute(fieldAttr.Id, true, new object[1]
        {
          newValue
        });
        return (object) null;
      }
      catch (KernelException ex)
      {
        this.LogMessage($"При добавлении атрибута '{fieldAttr}' к объекту '{dbObject.NameInMessages}' произошла ошибка.");
        this.LogMessage(ex.Message);
        return (object) null;
      }
    }
  }

  private static string TransformMassValue(
    SearchAPIBase.FieldAttribute fieldAttr,
    Tuple<object, string> fieldValuePair)
  {
    string str = fieldValuePair.Item2;
    MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(IDCache.Default.KilogramMeasure.Id);
    if (descriptor.Empty)
      throw new InvalidOperationException("В базе IPS отсутствует единица измерения массы - килограмм.");
    if (str.EndsWith(descriptor.ShortName, StringComparison.CurrentCultureIgnoreCase))
      str = str.Remove(str.Length - descriptor.ShortName.Length).TrimEnd();
    return str;
  }

  private string TransformMaterialValue(
    SearchAPIBase.FieldAttribute fieldAttr,
    Tuple<object, string> fieldValuePair)
  {
    long materialID = fieldValuePair.Item1 == null || Convert.IsDBNull(fieldValuePair.Item1) ? 0L : (long) fieldValuePair.Item1;
    if (materialID == 0L)
      return string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.PdmArticleService.GetMaterialName(materialID, (object) sessionKeeper.Session);
  }

  private object UntransformMaterialValue(SearchAPIBase.FieldAttribute fieldAttr, string fieldValue)
  {
    if (string.IsNullOrEmpty(fieldValue))
      return (object) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (object) this.PdmArticleService.GetMaterialID(fieldValue, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session, false);
  }

  private long PrepareForEdit(long objectId)
  {
    return DBDocumentHelper.Checkout((IList<long>) new List<long>()
    {
      objectId
    }, (DBDocumentHelper.CheckoutErrorHandler) null)[0];
  }

  private void SetNoError()
  {
    this.errorCode = SearchAPIBase.ErrorCodes.NoError;
    this.errorMessage = string.Empty;
  }

  private void ShowException(Exception x, bool allowDisplayErrors = true)
  {
    this.errorCode = SearchAPIBase.ErrorCodes.Error;
    this.errorMessage = x.Message;
    switch (x)
    {
      case SilentException _:
        this.LogMessage(x.Message);
        break;
      case FormatException _:
      case OverflowException _:
        this.LogMessage(x.Message);
        break;
      default:
        if (!(this.displayErrors & allowDisplayErrors))
          break;
        ExceptionHelper.ExceptionService.ShowException(x);
        break;
    }
  }

  public string ErrorMessage() => this.errorMessage;

  public int ErrorCode() => (int) this.errorCode;

  public string GetErrorMessage() => this.errorMessage;

  public int GetErrorCode() => (int) this.errorCode;

  public void DisplayErrors(bool state) => this.displayErrors = state;

  private void ResetIntegrator()
  {
    this.integrator = (IIntegrator) null;
    this.integratorName = (string) null;
  }

  protected internal IIntegrator Integrator
  {
    get
    {
      if (this.integrator == null)
      {
        if (this.integratorId != Guid.Empty)
        {
          this.integrator = this.ServiceLink.IntegratorRegistry.GetIntegrator(new IntegratorObject(this.integratorId, "Интегратор для COM-объекта SearchAPI"), true);
        }
        else
        {
          this.integrator = this.ServiceLink.IntegratorRegistry.GetIntegrator(this.ServiceLink.ActiveCADSystemService.GetActiveCADSystem(), true);
          this.integratorId = this.integrator.Id;
        }
      }
      return this.integrator;
    }
  }

  public string ApplicationName
  {
    get
    {
      if (this.integratorName == null)
        this.integratorName = ServiceUtils.GetService<IApplicationApiService>((object) this.Integrator, true).ApplicationName;
      return this.integratorName;
    }
  }

  public string[] SelectObjects(bool allowDocuments, bool allowArticles)
  {
    this.SetNoError();
    try
    {
      DescriptorCollection descriptors = new DescriptorCollection();
      if (allowDocuments)
      {
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(IDCache.Default.MechanicalDocuments.Id));
        foreach (LocalId<int> selectableDocumentType in this.GetSelectableDocumentTypes())
          descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(selectableDocumentType.Id));
      }
      if (allowArticles)
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(IDCache.Default.AllArticles.Id));
      return SearchAPIBase.SelectObjects((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor($"Объекты {this.ApplicationName}", descriptors), "Выберите объекты").ConvertAll<string>((Converter<Guid, string>) (id => id.ToString())).ToArray();
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
      return new string[0];
    }
  }

  private static bool TryParseGuid(string value, out Guid guid)
  {
    try
    {
      guid = new Guid(value);
      return true;
    }
    catch (FormatException ex)
    {
      guid = Guid.Empty;
      return false;
    }
    catch (OverflowException ex)
    {
      guid = Guid.Empty;
      return false;
    }
  }

  private static Guid ParseGuidArgument(string value, string argName)
  {
    Guid guid;
    if (SearchAPIBase.TryParseGuid(value, out guid))
      return guid;
    throw new FormatException($"Значение аргумента '{argName}' = '{value}' не является Guid.");
  }

  private void LogMessage(string text)
  {
    if (text == null)
      return;
    this.ServiceLink.OutputView.WriteString("SearchAPI", text);
  }

  public void SetFieldValue_Articles(string fieldGuidOrName, string fieldValue)
  {
    this.SetNoError();
    try
    {
      if (string.IsNullOrEmpty(fieldGuidOrName))
        throw new ArgumentException("Не задан идентификатор атрибута изделия.", nameof (fieldGuidOrName));
      this.CheckArticleOpen();
      SearchAPIBase.FieldAttribute fieldAttr = SearchAPIBase.DecodeAttributeId(fieldGuidOrName);
      if (fieldAttr == null)
      {
        this.LogMessage($"Атрибут изделия '{fieldGuidOrName}' отсутствует в базе IPS.");
      }
      else
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.WriteObjectField(sessionKeeper.Session.GetObject(this.openArticleId, true), fieldAttr, this.UntransformFieldValue_Articles(fieldAttr, fieldValue));
      }
    }
    catch (Exception ex)
    {
      this.ShowException(ex);
    }
  }

  private object UntransformFieldValue_Articles(
    SearchAPIBase.FieldAttribute fieldAttr,
    string fieldValue)
  {
    return fieldAttr.Id == IDCache.Default.Material.Id ? this.UntransformMaterialValue(fieldAttr, fieldValue) : (object) fieldValue;
  }

  private class FieldAttribute
  {
    public readonly string NameOrGuid;
    public readonly int Id;
    public readonly bool IsObligatory;

    public FieldAttribute(string nameOrGuid, int id, bool isObligatory)
    {
      this.NameOrGuid = nameOrGuid;
      this.Id = id;
      this.IsObligatory = isObligatory;
    }

    public override string ToString() => this.NameOrGuid;
  }

  private enum ErrorCodes
  {
    NoError,
    Error,
  }
}
