// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMSystem
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.AVS;
using Intermech.AVS.CAD;
using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.ControlFlow;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop.LocalServer;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.UI;
using Intermech.Win32;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.PDMTree;

[ComVisible(true)]
[Guid("8EBD4144-8A83-44D3-AAC9-CF6991C9BE3E")]
[ProgId("PDMSystem")]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IPDMSystem9))]
[TypeLibGuid("019B9AA9-104F-4038-A5A7-C3568FD09B59")]
public sealed class PDMSystem : 
  SingleThreadedObject,
  IPDMSystem9,
  IPDMSystem8,
  IPDMSystem7,
  IPDMSystem6,
  IPDMSystem5,
  IPDMSystem4,
  IPDMSystem3,
  IPDMSystem2,
  IPDMSystem
{
  private DynamicScope dscope;
  private const int LegacyUnknownDocumentId = -1;
  private PhysicalValuesService physicalValues;
  private IPDMSystemContext pdmSystemContext;
  private IFileVault fileVault;
  private SoftInstantiationHelper softInstantiationHelper;
  private IIntegrator integrator;
  private ICADSettingsService settingsSvc;
  private ICADInterfaceService cadService;
  private IPDMStandardLibrary stdLibManager;
  private ArticleLocatorBuilder articleLocatorBuilder;
  private IPSAttributeLocalizer attrLocalizer;
  private AttributeLocalizerComAdapter attrLocalizerComAdapter;
  private DocumentStatusesBatch1 docStatusesBatch1;
  private DocumentStatusesBatch2 docStatusesBatch2;
  private ICollection<int> allModelTypesCache;
  private ICollection<int> openableDocumentTypesCache;
  private ICollection<int> insertableModelTypesCache;
  private PhysicalUnit[] avsPhysicalUnitsCache;
  private int errorCode;
  private string errorMessage;
  private bool? _supportConfigurator;

  public PDMSystem()
  {
    this.physicalValues = new PhysicalValuesService();
    this.SetNoError();
  }

  public PhysicalValuesService PhysicalValues => this.physicalValues;

  internal IPDMSystemContext PDMSystemContext
  {
    get => this.pdmSystemContext;
    set => this.pdmSystemContext = value;
  }

  public object[] SelectObjects(ESelectObjectsType eType)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.SelectObjects");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      switch (eType)
      {
        case ESelectObjectsType.SOT_DocumentsToInsert:
          return (object[]) this.SelectDocumentsToInsert();
        case ESelectObjectsType.SOT_ArticlesToInsert:
          return (object[]) this.SelectArticlesToInsert();
        case ESelectObjectsType.SOT_DocumentsToOpen:
          return (object[]) this.SelectDocumentsToOpen();
        default:
          throw new NotSupportedEnumException((Enum) eType);
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private IPDMDocument[] SelectDocumentsToInsert()
  {
    long[] numArray = this.SelectPDMObjects(LocalizationHolder.rm.GetString("Tools.Client_147"), string.Format(LocalizationHolder.rm.GetString("Tools.Client_149"), (object) this.cadService.ApplicationName), this.InsertableModelTypes);
    lock (this.integrator.SyncRoot)
    {
      List<IPDMDocument> pdmDocumentList = new List<IPDMDocument>(numArray.Length);
      for (int index = 0; index < numArray.Length; ++index)
        pdmDocumentList.Add((IPDMDocument) new PDMDocument(numArray[index], this));
      return pdmDocumentList.ToArray();
    }
  }

  private IPDMArticle[] SelectArticlesToInsert()
  {
    long[] numArray = this.SelectPDMObjects(LocalizationHolder.rm.GetString("Tools.Client_147"), string.Format(LocalizationHolder.rm.GetString("Tools.Client_148"), (object) this.cadService.ApplicationName), IDCache.Default.AllArticles.Id);
    List<IPDMArticle> pdmArticleList = new List<IPDMArticle>(numArray.Length);
    foreach (long num in numArray)
    {
      PDMDocument linkedDocument = this.FindLinkedDocument(num);
      if (linkedDocument != null)
        pdmArticleList.Add((IPDMArticle) new PDMArticle(num, linkedDocument, this));
    }
    return pdmArticleList.ToArray();
  }

  private IPDMDocument[] SelectDocumentsToOpen()
  {
    long[] numArray = this.SelectPDMObjects(LocalizationHolder.rm.GetString("Tools.Client_147"), string.Format(LocalizationHolder.rm.GetString("Tools.Client_149"), (object) this.cadService.ApplicationName), this.OpenableDocumentTypes);
    lock (this.integrator.SyncRoot)
    {
      List<IPDMDocument> pdmDocumentList = new List<IPDMDocument>(numArray.Length);
      for (int index = 0; index < numArray.Length; ++index)
        pdmDocumentList.Add((IPDMDocument) new PDMDocument(numArray[index], this));
      return pdmDocumentList.ToArray();
    }
  }

  public PhysicalUnit[] GetUnits(EUnitsSubset eSubset)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetUnits");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      if (eSubset != EUnitsSubset.UNITS_AVS)
        throw new NotSupportedEnumException((Enum) eSubset);
      if (this.avsPhysicalUnitsCache == null)
        this.avsPhysicalUnitsCache = this.physicalValues.GetAvsPhysicalUnits();
      return this.avsPhysicalUnitsCache;
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public IPDMDocument5 GetDocumentByPersistentID(string id)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocumentByPersistentID");
    this.PrepareCall();
    try
    {
      if (string.IsNullOrEmpty(id))
        throw new ArgumentNullException();
      this.CheckInitialized();
      long objectId = this.TryConvertToObjectID(Guid.Parse(id));
      return objectId == 0L ? (IPDMDocument5) null : (IPDMDocument5) new PDMDocument(objectId, this);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public IPDMArticle3 GetArticleByPersistentID(string id)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetArticleByPersistentID");
    this.PrepareCall();
    try
    {
      if (string.IsNullOrEmpty(id))
        throw new ArgumentNullException();
      this.CheckInitialized();
      long objectId = this.TryConvertToObjectID(Guid.Parse(id));
      return objectId == 0L ? (IPDMArticle3) null : (IPDMArticle3) new PDMArticle(objectId, this.FindLinkedDocument(objectId), this);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private long TryConvertToObjectID(Guid objectGuid)
  {
    if (objectGuid == Guid.Empty)
      return 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objectGuid, false);
      return dbObject == null ? 0L : dbObject.ObjectID;
    }
  }

  public void GetDocVersionInfo2(
    string docFullPath,
    out long pCurrentVersion,
    out long pActualVersion,
    out long pMaxVersion)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocVersionInfo2");
    this.PrepareCall();
    try
    {
      if (string.IsNullOrEmpty(docFullPath))
        throw new ArgumentException("Путь к файлу документа не задан.", nameof (docFullPath));
      this.CheckInitialized();
      if (!Path.IsPathRooted(docFullPath) || !PathUtils.IsPlacedIn(docFullPath, this.fileVault.WorkArea.AreaPath))
      {
        pCurrentVersion = 0L;
        pActualVersion = 0L;
        pMaxVersion = 0L;
      }
      else
      {
        FileOrigin fileOrigin = this.fileVault.WorkArea.GetFileOrigin(docFullPath, false);
        if (fileOrigin.OriginType != FileOriginType.WorkFile)
        {
          pCurrentVersion = 0L;
          pActualVersion = 0L;
          pMaxVersion = 0L;
        }
        else
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject objectBaseVersionById = sessionKeeper.Session.GetObjectBaseVersionByID(fileOrigin.Id, true);
            pActualVersion = Math.Abs(objectBaseVersionById.ObjectID);
            IDBObject objectByVersionsRule = sessionKeeper.Session.GetObjectByVersionsRule(fileOrigin.Id, "cad001df-306c-11d8-b4e9-00304f19f545", true);
            pMaxVersion = Math.Abs(objectByVersionsRule.ObjectID);
          }
          pCurrentVersion = Math.Abs(fileOrigin.WorkObject.ObjectId);
        }
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public bool SupportsFeature(EPDMSystemFeatures eFeature)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.SupportsFeature");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      switch (eFeature)
      {
        case EPDMSystemFeatures.PDMSF_Configurator:
          return this.SupportsConfigurator;
        case EPDMSystemFeatures.PDMSF_Substitutes:
          return true;
        case EPDMSystemFeatures.PDMSF_VersionsSupport:
          return true;
        case EPDMSystemFeatures.PDMSF_IMBaseKeyInStandardParts:
          return true;
        case EPDMSystemFeatures.PDMSF_CheckIfSynchronizationNeeded:
          return true;
        default:
          return false;
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public IEditContext GetEditContext()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetEditContext");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      long editingContextId = this.PDMSystemContext.CurrentUserAndRoleService.CachedEditingContextID;
      return editingContextId != 0L ? (IEditContext) new PDMEditContext(editingContextId, this) : (IEditContext) null;
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public void GetDocVersionInfo(
    string DocFullPath,
    out int pCurrentVersion,
    out int pActualVersion,
    out int pMaxVersion)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocVersionInfo");
    this.PrepareCall();
    pCurrentVersion = 0;
    pActualVersion = 0;
    pMaxVersion = 0;
    this.ThrowCantImplement();
  }

  public void GetDocumentStatuses2(
    string[] pDocFullPaths,
    string[] pDesignations,
    string[] pNames,
    string[] pOKPCodes,
    out EDocumentStatus[] pStatuses,
    out string[] pCheckedOutBy,
    out DateTime[] pLastModified)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocumentStatuses2");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      lock (this.docStatusesBatch2)
        this.docStatusesBatch2.GetStatuses(pDocFullPaths, out pStatuses, out pCheckedOutBy, out pLastModified);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public void LaunchProcess(IPDMDocument3[] pPDMDocuments)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.LaunchProcess");
    this.PrepareCall();
    try
    {
      if (pPDMDocuments == null)
        throw new ArgumentNullException(nameof (pPDMDocuments));
      this.CheckInitialized();
      this.InvokeNavigatorCommand(this.PDMDocumentsToNavigatorItems((IPDMDocument2[]) pPDMDocuments), nameof (LaunchProcess));
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public void ShowSignCard(IPDMDocument3[] pdmDocuments)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.CreateNewDocument");
    this.PrepareCall();
    try
    {
      if (pdmDocuments == null)
        throw new ArgumentNullException("pPDMDocuments");
      if (pdmDocuments.Length == 0)
        return;
      this.CheckInitialized();
      this.InvokeNavigatorCommand(this.PDMDocumentsToNavigatorItems((IPDMDocument2[]) pdmDocuments), "SignUp");
    }
    catch (CommandNotSupportedException ex)
    {
      if (Array.Exists<long>(this.PDMDocumentsToObjectIds((IPDMDocument2[]) pdmDocuments), (Predicate<long>) (objId => objId < 0L)))
      {
        CommandNotSupportedException x = new CommandNotSupportedException("Невозможно подписать выбранные документы, так как среди них есть документы, взятые на редактирование. В IPS разрешено подписание только архивных документов.");
        this.ReportException((Exception) x);
        throw x;
      }
      CommandNotSupportedException x1 = new CommandNotSupportedException("Невозможно подписать выбранные документы. Проверьте настройку должностей и граф для подписей.");
      this.ReportException((Exception) x1);
      throw x1;
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private long[] PDMDocumentsToObjectIds(IPDMDocument2[] pdmDocuments)
  {
    long[] objectIds = new long[pdmDocuments.Length];
    for (int index = 0; index < pdmDocuments.Length; ++index)
      objectIds[index] = Convert.ToInt64(pdmDocuments[index].GetID());
    return objectIds;
  }

  private ISelectedItems PDMDocumentsToNavigatorItems(IPDMDocument2[] pdmDocuments)
  {
    return ObjectExtensions.GetItems(this.PDMDocumentsToObjectIds(pdmDocuments));
  }

  private void InvokeNavigatorCommand(ISelectedItems navItems, string commandName)
  {
    ServiceContainer viewServices = new ServiceContainer();
    viewServices.AddService(typeof (IViewState), (object) new ViewStateService());
    CommandsTable commandsTable = Intermech.Navigator.ContextMenu.Services.GetCommandsTable(navItems, (System.IServiceProvider) viewServices);
    if (!commandsTable.Contains(commandName))
      throw new CommandNotSupportedException();
    Intermech.Navigator.ContextMenu.Services.InvokeCommand(commandName, commandsTable, (System.IServiceProvider) viewServices);
  }

  public IPDMDocument3 CreateNewDocument()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.CreateNewDocument");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      int[] array = this.settingsSvc.GetNewFileDocumentTypes().ConvertAll<int>((Converter<LocalId<int>, int>) (item => item.Id)).ToArray();
      this.PDMSystemContext.ObjectCreatorService.AfterObjectCreatedEvent += new AfterObjectCreatedEventHandler(this.OnCreateNewObject);
      try
      {
        long objectByTypeDialog = this.PDMSystemContext.ObjectCreatorService.CreateObjectByTypeDialog(array);
        switch (objectByTypeDialog)
        {
          case -1:
          case 0:
            return (IPDMDocument3) null;
          default:
            return (IPDMDocument3) new PDMDocument(objectByTypeDialog, this);
        }
      }
      finally
      {
        this.PDMSystemContext.ObjectCreatorService.AfterObjectCreatedEvent -= new AfterObjectCreatedEventHandler(this.OnCreateNewObject);
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private void OnCreateNewObject(object sender, AfterObjectCreatedEventArgs e)
  {
    this.PDMSystemContext.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", e.ObjectID));
  }

  public void BeginGroupOperation()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.BeginGroupOperation");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      this.dscope = this.dscope == null ? new DynamicScope() : throw new Exception();
      UIVars.UICommand.Declare(new UICommandInfo("PDMBrowser command"));
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public void EndGroupOperation()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.EndGroupOperation");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      if (this.dscope == null)
        return;
      this.dscope.Dispose();
      this.dscope = (DynamicScope) null;
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public IPDMDocument3 GetDocumentByFilePath(string bstrFilePath)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocumentByFilePath");
    this.PrepareCall();
    try
    {
      if (string.IsNullOrEmpty(bstrFilePath))
        throw new ArgumentException();
      this.CheckInitialized();
      lock (this.integrator.SyncRoot)
      {
        long documentId = this.FindDocumentId(bstrFilePath);
        return documentId == 0L ? (IPDMDocument3) null : (IPDMDocument3) new PDMDocument(documentId, this);
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public void GetDocumentStatuses(
    string[] pDocFullPaths,
    string[] pDesignations,
    string[] pNames,
    string[] pOKPCodes,
    out EDocumentStatus[] pStatuses,
    out string[] pCheckedOutBy)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocumentStatuses");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      lock (this.docStatusesBatch1)
        this.docStatusesBatch1.GetStatuses(pDocFullPaths, out pStatuses, out pCheckedOutBy);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public IParametersContainer[] CreateSpecification2(ICADDocument rawDocument)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.CreateSpecification2");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      if (this.PDMSystemContext.MainFormService.ApplicationHasOpenedModalForms)
        throw new PDMSystemException(LocalizationHolder.rm.GetString("Tools.Client_302"));
      PDMDocumentFileInfo documentIds = this.GetDocumentIds(rawDocument);
      this.PDMSystemContext.MainFormService.MainForm.Activate();
      this.CreateSpecification(rawDocument, documentIds);
      return this.GetSpecificationFromAVS(documentIds.DocumentId);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public void CreateSpecification(ICADDocument rawDocument)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.CreateSpecification");
    this.PrepareCall();
    this.ThrowCantImplement("Метод IPDMSystem2.CreateSpecification() устарел и больше не должен использоваться. Воспользуйтесь новым методом IPDMSystem3.CreateSpecification2().");
  }

  private void CreateSpecification(ICADDocument rawDocument, PDMDocumentFileInfo pdmDocumentFile)
  {
    IExtendedSaveSupport saveSvc = ServiceUtils.GetService<IExtendedSaveSupport>((object) this.integrator, true);
    ProgressSinks.DialogService.Invoke($"Обновление спецификации для {Path.GetFileName(pdmDocumentFile.MasterFilePath)}", ProgressSinkDialogFlags.Default, (Action<IPercentageProgressSink>) (progressSink => saveSvc.CaptureChanges(pdmDocumentFile.DocumentId, new ExtendedSaveOptions(SaveChangesMode.Default)
    {
      WorkAreaPolicy = (IReplaceFilePolicy) new PreserveAnyFile(),
      ProgressSink = progressSink
    })));
    List<QuickObjectInfo> articles = this.FindArticles(pdmDocumentFile.DocumentId);
    if (articles.Count <= 0)
      return;
    long baseArticleId = DBDocumentHelper.Checkout((IList<long>) articles.ConvertAll<long>((Converter<QuickObjectInfo, long>) (qoi => qoi.ObjectID)), (DBDocumentHelper.CheckoutErrorHandler) null)[0];
    using (AVSCommandsBuilder avsCommandsBuilder = new AVSCommandsBuilder(this.PDMSystemContext.BarManager))
    {
      if (this.CanTransferZones(pdmDocumentFile))
        avsCommandsBuilder.AddCommand("TransferZones", "Получить зоны из сборочного чертежа", string.Empty, (EventHandler) ((s, e) => this.TransferZones((AVSWindow) s, rawDocument, pdmDocumentFile)));
      this.EditSpecification(pdmDocumentFile.DocumentId, baseArticleId, avsCommandsBuilder.Build());
    }
  }

  private bool CanTransferZones(PDMDocumentFileInfo pdmDocumentFile)
  {
    if (pdmDocumentFile.IsMasterFile)
    {
      int objectType = DBHelper.GetObjectType(pdmDocumentFile.DocumentId);
      IPDMBrowserService service = ServiceUtils.GetService<IPDMBrowserService>((object) this.integrator, false);
      return service != null && service.CanProvideSpecificationZones(objectType);
    }
    int? nullable = this.GuessAncillaryFileDocumentType(pdmDocumentFile);
    if (!nullable.HasValue)
      return false;
    IPDMBrowserService service1 = ServiceUtils.GetService<IPDMBrowserService>((object) this.integrator, false);
    return service1 != null && service1.CanProvideSpecificationZones(nullable.Value);
  }

  private int? GuessAncillaryFileDocumentType(PDMDocumentFileInfo pdmDocumentFile)
  {
    return ServiceUtils.GetService<IModelDrawingsService>((object) this.integrator, true).IsDrawingFileName(pdmDocumentFile.FilePath) ? this.GuessDrawingDocumentType(pdmDocumentFile) : new int?();
  }

  private int? GuessDrawingDocumentType(PDMDocumentFileInfo pdmDocumentFile)
  {
    CADSettings cadSettings = this.settingsSvc.GetCADSettings();
    DocumentGroup byDocumentType = cadSettings.FileDocumentGroups.FindByDocumentType(DBHelper.GetObjectType(pdmDocumentFile.DocumentId), false);
    if (byDocumentType != null)
    {
      if (byDocumentType.Name == "Assembly")
        return new int?(cadSettings.FileDocumentGroups.FindByName("AssemblyDrawing", true).DocumentTypes[0].Id);
      if (byDocumentType.Name == "Part")
        return new int?(cadSettings.FileDocumentGroups.FindByName("PartDrawing", true).DocumentTypes[0].Id);
    }
    return new int?();
  }

  private void TransferZones(
    AVSWindow avsWindow,
    ICADDocument rawDocument,
    PDMDocumentFileInfo pdmDocumentFile)
  {
    List<ZoneRecord> zones;
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
    {
      CADSystemProxy application = cadApiSession.Application;
      CADDocumentProxy document = application.Builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(rawDocument, pdmDocumentFile.FilePath), application);
      zones = new ZoneManagerProxy(application).GetZones(document);
    }
    if (zones.Count == 0)
      return;
    AvsRowAttributeInfo attrInfo = new AvsRowAttributeInfo(true, IDCache.Default.Zone.Id);
    avsWindow.AVSDocument.SuspendDocumentAndGridUpdates();
    try
    {
      foreach (AVSRow allRow in avsWindow.AVSDocument.GetAllRows(true, true))
      {
        Guid occGuid = AVSHelper.GetFieldValue<Guid>((AttributeValuesCache) allRow.Relations[0], IDCache.Default.OccurenceKey.Id, Guid.Empty);
        if (!(occGuid == Guid.Empty))
        {
          ZoneRecord zoneRecord = zones.Find((Predicate<ZoneRecord>) (item => item.OccurenceGuid == occGuid));
          if (zoneRecord != null)
            allRow.SetFieldValue(attrInfo, -1, -1, (object) zoneRecord.Zone, true, false, true, true, true, false, false);
        }
      }
    }
    finally
    {
      avsWindow.AVSDocument.ResumeDocumentAndGridUpdates(0, true, true, true, true);
    }
  }

  private void EditSpecification(
    long documentId,
    long baseArticleId,
    ExternalAVSCommand[] specialCommands)
  {
    if (!AVSPlugin.HasLoadedInstance)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Client_137"));
    AVSWindow avsWindow = AVSPlugin.Instance.OpenAVSWindow(baseArticleId, externalCommands: specialCommands);
    if (avsWindow == null)
      return;
    this.PDMSystemContext.MainFormService.MainForm.Activate();
    avsWindow.EnableWorkCompleteMode();
    while (avsWindow.IsInContainer)
      Application.DoEvents();
  }

  public string BeginUpdateStandardPart(string partName, string modelFileName)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine($"PDMSystem.BeginUpdateStandardPart ({partName}, {modelFileName})");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      return this.stdLibManager.BeginUpdatePart(partName, modelFileName);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      return (string) null;
    }
  }

  public void EndUpdateStandardPart(string partName, string modelFileName)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine($"PDMSystem.EndUpdateStandardPart({partName}, {modelFileName})");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      this.stdLibManager.EndUpdatePart(partName, modelFileName);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
    }
  }

  public IPDMDocument2 GetDocumentByID(string id)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocumentByID");
    this.PrepareCall();
    try
    {
      if (string.IsNullOrEmpty(id))
        throw new ArgumentNullException();
      this.CheckInitialized();
      return (IPDMDocument2) this.GetDocumentByObjectId(long.Parse(id));
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  internal PDMDocument GetDocumentByObjectId(long objectId) => new PDMDocument(objectId, this);

  public IPDMArticle2 GetArticleByID(string id)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetArticleByID");
    this.PrepareCall();
    try
    {
      if (string.IsNullOrEmpty(id))
        throw new ArgumentNullException();
      this.CheckInitialized();
      string[] strArray = id.Split(';');
      long num = strArray.Length == 1 || strArray.Length == 2 ? long.Parse(strArray[0]) : throw new FormatException();
      long objectId = strArray.Length != 2 || string.IsNullOrEmpty(strArray[1]) ? this.FindLinkedDocumentId(num) : long.Parse(strArray[1]);
      PDMDocument pdmDocumentOrNull = objectId != 0L ? new PDMDocument(objectId, this) : (PDMDocument) null;
      return (IPDMArticle2) new PDMArticle(num, pdmDocumentOrNull, this);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  public IParametersContainer[] GetSpecificationFromAVS(ICADDocument rawDocument)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetSpecificationFromAVS");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      return this.GetSpecificationFromAVS(this.GetDocumentId(rawDocument));
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
      this.ReportException(ex);
      throw;
    }
  }

  private PDMDocumentFileInfo GetDocumentIds(ICADDocument rawDocument)
  {
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
    {
      CADSystemProxy application = cadApiSession.Application;
      CADDocumentProxy document = application.Builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(rawDocument), application);
      long documentId = this.GetDocumentId(document);
      string masterFilePath = Path.Combine(this.fileVault.WorkArea.AreaPath, this.fileVault.DBFilesInfo.GetMasterFileName(documentId, true));
      return new PDMDocumentFileInfo(document.FullName, documentId, masterFilePath);
    }
  }

  private List<QuickObjectInfo> FindArticles(long documentId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return this.PDMSystemContext.ArticleService.FindListInstances(documentId, VersionsRuleSources.GetEditorRule().OwnerId, (object) sessionKeeper.Session);
  }

  private IParametersContainer[] GetSpecificationFromAVS(long documentId)
  {
    List<QuickObjectInfo> articles = this.FindArticles(documentId);
    if (articles.Count <= 0)
      return (IParametersContainer[]) new ParametersContainer[0];
    SpecificationReconstructor specificationReconstructor = new SpecificationReconstructor();
    specificationReconstructor.Document = documentId;
    foreach (QuickObjectInfo quickObjectInfo in articles)
      specificationReconstructor.ArticleInstances.Add(quickObjectInfo.ObjectID);
    return (IParametersContainer[]) specificationReconstructor.CreateSpecification().ConvertAll<PDMSystemSpecRecordWrapper>((Converter<SimpleSpecificationRow, PDMSystemSpecRecordWrapper>) (specRecord => new PDMSystemSpecRecordWrapper(this, specRecord))).ToArray();
  }

  public string GetLastError(out int plErrorCode)
  {
    plErrorCode = this.errorCode;
    return this.errorMessage;
  }

  public void Init(Guid CADSystemID)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.Init");
    this.PrepareCall();
    try
    {
      this.fileVault = this.PDMSystemContext != null ? this.PDMSystemContext.FileVaultService : throw PropertyExceptions.PropertyNotSetException((object) this, "PDMSystemContext");
      this.softInstantiationHelper = new SoftInstantiationHelper();
      this.integrator = this.LookupIntegrator(CADSystemID);
      this.settingsSvc = ServiceUtils.GetService<ICADSettingsService>((object) this.integrator, true);
      this.cadService = ServiceUtils.GetService<ICADInterfaceService>((object) this.integrator, true);
      this.articleLocatorBuilder = new ArticleLocatorBuilder();
      this.stdLibManager = this.CreateStandardLibrary(this.articleLocatorBuilder);
      this.stdLibManager.Log = this.PDMSystemContext.StandardLibraryLog;
      this.attrLocalizer = new IPSAttributeLocalizer();
      this.attrLocalizerComAdapter = new AttributeLocalizerComAdapter((IAttributeLocalizer) this.attrLocalizer);
      this.docStatusesBatch1 = new DocumentStatusesBatch1();
      this.docStatusesBatch2 = new DocumentStatusesBatch2();
    }
    catch (Exception ex)
    {
      this.stdLibManager = (IPDMStandardLibrary) null;
      this.integrator = (IIntegrator) null;
      this.settingsSvc = (ICADSettingsService) null;
      this.cadService = (ICADInterfaceService) null;
      Exception x = new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_140"), (object) ex.Message));
      this.ReportException(x);
      throw x;
    }
  }

  private void CheckInitialized()
  {
    if (this.integrator == null)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Client_141"));
  }

  private IIntegrator LookupIntegrator(Guid cadSystemId)
  {
    foreach (IIntegrator integrator in this.PDMSystemContext.IntegratorRegistry.GetIntegrators())
    {
      IPDMBrowserService service = ServiceUtils.GetService<IPDMBrowserService>((object) integrator, false);
      if (service != null && service.CADSystemId == cadSystemId)
        return integrator;
    }
    throw new Exception(string.Format(LocalizationHolder.rm.GetString("Tools.Client_142"), (object) cadSystemId));
  }

  private IPDMStandardLibrary CreateStandardLibrary(ArticleLocatorBuilder articleLocatorBuilder)
  {
    StandardLibraryMode mode = StandardLibraryServices.GetMode((System.IServiceProvider) this.integrator);
    switch (mode)
    {
      case StandardLibraryMode.NotSupported:
        return (IPDMStandardLibrary) new NotSupportedStandardLibrary(this.integrator);
      case StandardLibraryMode.SeparateStandardSizes:
        return (IPDMStandardLibrary) new SeparateStandardSizesLibrary(this.PDMSystemContext, this.integrator, articleLocatorBuilder);
      case StandardLibraryMode.EmbeddedStandardSizes:
        return (IPDMStandardLibrary) new EmbeddedStandardSizesLibrary(this.PDMSystemContext, this.integrator, articleLocatorBuilder);
      default:
        throw new NotSupportedEnumException((Enum) mode);
    }
  }

  public IPDMArticle GetArticle(IModelConfiguration pConfiguration)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetArticle");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
      {
        CADSystemProxy application = cadApiSession.Application;
        ExplicitModelConfigurationProvider configurationProvider = new ExplicitModelConfigurationProvider(pConfiguration);
        CADDocumentProxy document = application.Builder.CreateDocument((ICADDocumentProvider) new LinkedCADDocumentProvider((IModelConfigurationProvider) configurationProvider), application);
        if (!document.HasConfigurations)
          return (IPDMArticle) null;
        ModelConfigurationProxy modelConfiguration = application.Builder.CreateModelConfiguration((IModelConfigurationProvider) configurationProvider, document, application, (IModelConfigurationCreationContext) ExternalModelConfigurationContext.Default);
        long documentId = this.FindDocumentId(document);
        long articleId = this.FindArticleId(modelConfiguration, documentId);
        if (articleId == 0L)
          return (IPDMArticle) null;
        PDMDocument pdmDocumentOrNull = documentId != 0L ? new PDMDocument(documentId, this) : (PDMDocument) null;
        PDMArticle article = new PDMArticle(articleId, pdmDocumentOrNull, this);
        article.SetCADDocument(document);
        return (IPDMArticle) article;
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private long GetArticleId(ModelConfigurationProxy modelConfiguration, long pdmDocumentIdOrNull)
  {
    long articleId = this.FindArticleId(modelConfiguration, pdmDocumentIdOrNull);
    return articleId != 0L ? articleId : throw new PDMObjectNotFoundException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_143"), (object) modelConfiguration.Name, (object) modelConfiguration.Document.Title));
  }

  private long FindArticleId(ModelConfigurationProxy configuration, long pdmDocumentIdOrNull)
  {
    if (CADDocumentHelper.IsArticleCreationDenied((System.IServiceProvider) this.integrator, configuration))
      return 0;
    ObjectLocatorResult article = this.cadService.FindArticle(configuration, pdmDocumentIdOrNull);
    return article == null ? 0L : article.ObjectId;
  }

  public IPDMDocument GetDocument(ICADDocument pCADDocument)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.GetDocument");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
      {
        CADSystemProxy application = cadApiSession.Application;
        long documentId = this.FindDocumentId(application.Builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(pCADDocument), application));
        return documentId == 0L ? (IPDMDocument) null : (IPDMDocument) new PDMDocument(documentId, this);
      }
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private long GetDocumentId(ICADDocument rawDocument)
  {
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
    {
      CADSystemProxy application = cadApiSession.Application;
      return this.GetDocumentId(application.Builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(rawDocument), application));
    }
  }

  private long GetDocumentId(CADDocumentProxy cadDocument)
  {
    return this.GetDocumentId(cadDocument.FullName);
  }

  private long GetDocumentId(string documentPath)
  {
    long documentId = this.FindDocumentId(documentPath);
    return documentId != 0L ? documentId : throw new PDMObjectNotFoundException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_144"), (object) documentPath));
  }

  private long FindDocumentId(CADDocumentProxy cadDocument)
  {
    return this.FindDocumentId(cadDocument.FullName);
  }

  private long FindDocumentId(string documentPath)
  {
    if (!string.IsNullOrEmpty(documentPath) && Path.IsPathRooted(documentPath) && this.fileVault.FindArea(documentPath) == this.fileVault.WorkArea)
    {
      FileOrigin fileOrigin = this.fileVault.WorkArea.GetFileOrigin(documentPath, false);
      if (fileOrigin.OriginType == FileOriginType.WorkFile)
        return fileOrigin.WorkObject.ObjectId;
    }
    return 0;
  }

  public IPDMDocument RegisterDocument(ICADDocument rawDocument)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.RegisterDocument");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      Tuple<long, string> tuple;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
      {
        CADSystemProxy application = cadApiSession.Application;
        CADDocumentProxy document = application.Builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(rawDocument), application);
        tuple = new Tuple<long, string>(this.FindDocumentId(document), document.MasterFile);
      }
      long num = tuple.Item1;
      string masterFilePath = tuple.Item2;
      if (num != 0L)
        throw new PDMSystemException(string.Format(LocalizationHolder.rm.GetString("Tools.Client_145"), (object) masterFilePath, (object) "Intermech Professional Solutions"));
      return (IPDMDocument) this.RegisterDocumentCore(masterFilePath);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private PDMDocument RegisterDocumentCore(string masterFilePath)
  {
    return new PDMDocument(this.PDMSystemContext.FileImportService.ImportFile(masterFilePath), this);
  }

  public IPDMDocument RegisterDocumentAs(ICADDocument pPrototypeDocument)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.RegisterDocumentAs");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      Tuple<long, string> tuple;
      using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.cadService))
      {
        CADSystemProxy application = cadApiSession.Application;
        CADDocumentProxy document = application.Builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(pPrototypeDocument), application);
        tuple = new Tuple<long, string>(this.FindDocumentId(document), document.MasterFile);
      }
      long prototypeId = tuple.Item1;
      string str = tuple.Item2;
      if (prototypeId != 0L)
        return (IPDMDocument) this.CloneDocument(prototypeId, str);
      SaveFilePresenter saveFilePresenter = new SaveFilePresenter();
      saveFilePresenter.Title = string.Format(LocalizationHolder.rm.GetString("SR_319"), (object) Path.GetFileName(str));
      saveFilePresenter.InitialDirectory = Path.GetDirectoryName(str);
      saveFilePresenter.DefaultExtension = Path.GetExtension(str);
      if (!string.IsNullOrEmpty(saveFilePresenter.DefaultExtension))
        saveFilePresenter.ExtensionFilter = string.Format(LocalizationHolder.rm.GetString("SR_320"), (object) saveFilePresenter.DefaultExtension);
      MvpContext.ViewService.ShowModal((IPresenter) saveFilePresenter);
      if (string.IsNullOrEmpty(saveFilePresenter.SelectedPath))
        return (IPDMDocument) null;
      File.Copy(str, saveFilePresenter.SelectedPath);
      return (IPDMDocument) this.RegisterDocumentCore(saveFilePresenter.SelectedPath);
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private PDMDocument CloneDocument(long prototypeId, string prototypeMasterFilePath)
  {
    long byTemplateDialog = this.PDMSystemContext.ObjectCreatorService.CreateObjectByTemplateDialog(prototypeId);
    switch (byTemplateDialog)
    {
      case -1:
      case 0:
        return (PDMDocument) null;
      default:
        this.fileVault.WorkArea.Publish((IList<DBObjectState>) this.fileVault.DBObjectsInfo.CreateStateListForObjectTree(byTemplateDialog, VersionsRuleSources.GetEditorRule()), (IReplaceFilePolicy) new PreserveAnyChanges());
        this.PDMSystemContext.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", byTemplateDialog));
        return new PDMDocument(byTemplateDialog, this);
    }
  }

  public IPDMArticle[] SelectArticles()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.SelectArticles");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      return this.SelectArticlesToInsert();
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private long FindLinkedDocumentId(long articleId)
  {
    ObjectLocatorResult objectLocatorResult1 = new TypeBasedDocumentLocator((IDocumentTypesLocatorData) new AsmComponentLocatorData(articleId, this.AllModelTypes)).LocateObject();
    if (objectLocatorResult1 != null)
      return objectLocatorResult1.ObjectId;
    CADSettings cadSettings = this.settingsSvc.GetCADSettings();
    if (cadSettings.JTDerivativesEnabled)
    {
      ObjectLocatorResult objectLocatorResult2 = JTLinkManager.DerivedDocumentFromArticle(articleId, cadSettings.JTDerivedDocumentType.Id).LocateObject();
      if (objectLocatorResult2 != null)
        return objectLocatorResult2.ObjectId;
    }
    return 0;
  }

  private PDMDocument FindLinkedDocument(long articleId)
  {
    long linkedDocumentId = this.FindLinkedDocumentId(articleId);
    return linkedDocumentId != 0L ? new PDMDocument(linkedDocumentId, this) : (PDMDocument) null;
  }

  internal ICollection<int> AllModelTypes
  {
    get
    {
      this.CheckInitialized();
      this.InitializeDocumentTypesCacheLazily();
      return this.allModelTypesCache;
    }
  }

  internal ICollection<int> OpenableDocumentTypes
  {
    get
    {
      this.CheckInitialized();
      this.InitializeDocumentTypesCache();
      return this.openableDocumentTypesCache;
    }
  }

  internal ICollection<int> InsertableModelTypes
  {
    get
    {
      this.CheckInitialized();
      this.InitializeDocumentTypesCacheLazily();
      return this.insertableModelTypesCache;
    }
  }

  private void InitializeDocumentTypesCacheLazily()
  {
    if (this.allModelTypesCache != null)
      return;
    this.InitializeDocumentTypesCache();
  }

  private void InitializeDocumentTypesCache()
  {
    CADSettings cadSettings = this.settingsSvc.GetCADSettings();
    List<GlobalId<int>> collection = new List<GlobalId<int>>(8);
    DocumentGroup byName1 = cadSettings.FileDocumentGroups.FindByName("Assembly", false);
    if (byName1 != null)
      collection.AddRange((IEnumerable<GlobalId<int>>) byName1.DocumentTypes);
    DocumentGroup byName2 = cadSettings.FileDocumentGroups.FindByName("Part", false);
    if (byName2 != null)
      collection.AddRange((IEnumerable<GlobalId<int>>) byName2.DocumentTypes);
    List<GlobalId<int>> globalIdList1 = new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) collection);
    List<GlobalId<int>> globalIdList2 = new List<GlobalId<int>>((IEnumerable<GlobalId<int>>) collection);
    if (cadSettings.StandardPartType != null)
      collection.Add(cadSettings.StandardPartType);
    DocumentGroup byName3 = cadSettings.FileDocumentGroups.FindByName("PartDrawing", false);
    if (byName3 != null)
      globalIdList1.AddRange((IEnumerable<GlobalId<int>>) byName3.DocumentTypes);
    DocumentGroup byName4 = cadSettings.FileDocumentGroups.FindByName("AssemblyDrawing", false);
    if (byName4 != null)
      globalIdList1.AddRange((IEnumerable<GlobalId<int>>) byName4.DocumentTypes);
    if (cadSettings.NeutralDocumentTypes.DocumentTypes.Count != 0)
      globalIdList2.AddRange((IEnumerable<GlobalId<int>>) cadSettings.NeutralDocumentTypes.DocumentTypes);
    this.allModelTypesCache = (ICollection<int>) new ReadOnlyCollection<int>((IList<int>) collection.ConvertAll<int>((Converter<GlobalId<int>, int>) (item => item.Id)));
    this.openableDocumentTypesCache = (ICollection<int>) new ReadOnlyCollection<int>((IList<int>) globalIdList1.ConvertAll<int>((Converter<GlobalId<int>, int>) (item => item.Id)));
    this.insertableModelTypesCache = (ICollection<int>) new ReadOnlyCollection<int>((IList<int>) globalIdList2.ConvertAll<int>((Converter<GlobalId<int>, int>) (item => item.Id)));
  }

  public IPDMDocument[] SelectDocuments()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMSystem.SelectDocuments");
    this.PrepareCall();
    try
    {
      this.CheckInitialized();
      return this.SelectDocumentsToInsert();
    }
    catch (Exception ex)
    {
      this.ReportException(ex);
      throw;
    }
  }

  private long[] SelectPDMObjects(string caption, string description, ICollection<int> objectTypes)
  {
    IDescriptor composition = Intermech.Navigator.DBObjectTypes.Descriptor.CreateComposition((IEnumerable<int>) objectTypes);
    return Intermech.Navigator.SelectionWindow.SelectObjects(caption, description, composition, SelectionOptions.SelectObjects | SelectionOptions.ForceFilterObjectsByRule) ?? new long[0];
  }

  private long[] SelectPDMObjects(string caption, string description, int rootObjectType)
  {
    return Intermech.Navigator.SelectionWindow.SelectObjects(caption, description, rootObjectType, SelectionOptions.SelectObjects | SelectionOptions.ForceFilterObjectsByRule) ?? new long[0];
  }

  public string Name => "Intermech Professional Solutions";

  public bool SupportsConfigurator
  {
    get
    {
      if (!this._supportConfigurator.HasValue)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this._supportConfigurator = new bool?(sessionKeeper.Session.EnabledPdmConfigurator);
      }
      return this._supportConfigurator ?? false;
    }
  }

  public bool SupportsSubstitutions => true;

  public AttributeLocalizer AttributeLocalizer => (AttributeLocalizer) this.attrLocalizerComAdapter;

  internal IPSAttributeLocalizer IPSAttributeLocalizer => this.attrLocalizer;

  internal void PrepareCall()
  {
    this.SetNoError();
    ForegroundWindowHelper.Default.AllowActionToAnyProcess();
  }

  internal void ThrowCantImplement(string message = null)
  {
    NotImplementedException x = message != null ? new NotImplementedException(message) : new NotImplementedException();
    this.ReportException((Exception) x);
    throw x;
  }

  internal void ReportException(Exception x)
  {
    if (x is TargetInvocationException)
    {
      Exception x1 = x;
      while (x1 is TargetInvocationException && x1.InnerException != null)
        x1 = x1.InnerException;
      this.ReportExceptionCore(x1);
      throw x1;
    }
    this.ReportExceptionCore(x);
  }

  private void ReportExceptionCore(Exception x)
  {
    this.SetError(x.Message);
    Trace.WriteLine($"PDMSystem: {x.Message}");
    switch (x)
    {
      case COMException _:
        break;
      case ISimpleMessageException _:
        break;
      default:
        UINotificationBuilder notificationBuilder = new UINotificationBuilder();
        notificationBuilder.FillFromException(x);
        this.PDMSystemContext.UINotificationService.ShowNotification(notificationBuilder.Build());
        break;
    }
  }

  private void SetNoError()
  {
    this.errorCode = 0;
    this.errorMessage = string.Empty;
  }

  private void SetError(string errorMessage)
  {
    this.errorCode = -1;
    this.errorMessage = errorMessage;
  }

  internal IIntegrator Integrator => this.integrator;

  internal ICADInterfaceService CADService => this.cadService;

  internal SoftInstantiationHelper SoftInstantiationHelper => this.softInstantiationHelper;
}
