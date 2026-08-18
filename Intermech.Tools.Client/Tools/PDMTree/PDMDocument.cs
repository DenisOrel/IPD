// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.PDMTree.PDMDocument
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Collections;
using Intermech.Commands;
using Intermech.ControlFlow;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Tools.Client.Commands;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.LaunchActions;
using Intermech.Tools.UI;
using Interop.CADInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.PDMTree;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.None)]
[ComDefaultInterface(typeof (IPDMDocument8))]
public sealed class PDMDocument : 
  PDMObject,
  IPDMDocument8,
  IPDMDocument7,
  IPDMDocument6,
  IPDMDocument5,
  IPDMDocument4,
  IPDMDocument3,
  IPDMDocument2,
  IPDMDocument,
  IParametersContainer
{
  private PDMParameterContainer paramContainer;
  private string stringIdCache;

  internal PDMDocument(long objectId, PDMSystem pdmSystem)
    : base(objectId, pdmSystem)
  {
    this.paramContainer = new PDMParameterContainer((IDBObjectRef) this, (IPDMSystemProvider) this);
  }

  public bool IsSynchronizationNeeded()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.IsSynchronizationNeeded");
    this.PDMSystem.PrepareCall();
    try
    {
      using (new DynamicScope())
      {
        VersionsRuleSources.AllowCache.Declare(true);
        SynchronizeOperations synchronizeOperations = new SynchronizeOperations(this.PDMSystem.PDMSystemContext.FileVaultService);
        PDMDocumentVersionInfo actualDocumentVersion = synchronizeOperations.GetActualDocumentVersion(this.ID);
        List<DBObjectState> documentStructure = synchronizeOperations.GetActualDocumentStructure(actualDocumentVersion.DBObjectState.ObjectId, true);
        PDMDocumentSynchronizationInfo synchronizationInfo = synchronizeOperations.AnalyzeDocumentStructure(actualDocumentVersion, documentStructure);
        return synchronizationInfo.OutdatedObjects.Count != 0 || synchronizationInfo.UnpublishedObjects.Count != 0;
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public IPDMDocument8 CreateVersion()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.CreateVersion");
    this.PDMSystem.PrepareCall();
    try
    {
      if (new PDMObjectVersionOperations().CanCreateEditableVersion((PDMObject) this) == PDMObjectVersionOperations.CanCreateVersionStatus.RequireEditContext)
        throw new SimpleMessageException($"Невозможно создать версию документа '{this.Caption}' из PDM-браузера. Вы должны переключиться в IPS, создать ИИ или контекст редактирования, а затем создать версию документа в контексте.");
      ISelectedItems items = Intermech.Navigator.ContextMenu.Services.GetItems(this.ObjectId);
      CreateItemsVersionsCommand itemsVersionsCommand = new CreateItemsVersionsCommand();
      itemsVersionsCommand.Init(items, (System.IServiceProvider) null, (object) null);
      itemsVersionsCommand.Execute();
      return itemsVersionsCommand.Result.Count == 0 ? (IPDMDocument8) null : (IPDMDocument8) new PDMDocument(itemsVersionsCommand.Result[0].ObjectId, this.PDMSystem);
    }
    catch (AbortException ex)
    {
      return (IPDMDocument8) null;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void SaveToDir(string bstrDir, bool bSaveRefs, string bstrExtensionFilter)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SaveToDir");
    this.PDMSystem.PrepareCall();
    try
    {
      throw new NotImplementedException();
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void ExtendedSave(bool silentMode)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.ExtendedSave");
    ExtendedSaveHelper extendedSaveHelper = this.PDMSystem.PDMSystemContext.ExtendedSaveHelper;
    if (this.ObjectType != -1 && extendedSaveHelper.SupportedObjectTypes.Contains(this.ObjectType))
      this.RunExtendedSaveCommand(silentMode);
    else
      this.RunObjectCommand("SaveChanges");
  }

  private void RunExtendedSaveCommand(bool silentMode)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      ExtendedSaveCommand extendedSaveCommand = new ExtendedSaveCommand();
      extendedSaveCommand.ObjectId = this.ObjectId;
      extendedSaveCommand.ObjectTypeId = this.ObjectType;
      extendedSaveCommand.ObjectCaption = this.Caption;
      extendedSaveCommand.Execute();
    }
    catch (AbortException ex)
    {
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string[] SelectFiles(string fileExtensionFilter)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SelectFiles");
    this.PDMSystem.PrepareCall();
    try
    {
      List<string> fileNames = this.PDMSystem.PDMSystemContext.FileVaultService.DBFilesInfo.GetFileNames(this.ObjectId);
      if (fileNames.Count != 0)
        this.ApplyFileExtensionFilter(fileNames, fileExtensionFilter);
      string[] strArray;
      if (fileNames.Count > 1)
      {
        using (SelectItemForm selectItemForm = new SelectItemForm())
        {
          selectItemForm.Text = "Выбор файла";
          selectItemForm.Description = $"Выберить один из файлов документа '{this.Caption}' (ид. версии {this.ObjectId})";
          selectItemForm.Items = (IEnumerable) fileNames;
          if (selectItemForm.ShowDialog() == DialogResult.OK && selectItemForm.SelectedItem != null)
            strArray = new string[1]
            {
              (string) selectItemForm.SelectedItem
            };
          else
            strArray = new string[0];
        }
      }
      else
        strArray = fileNames.Count != 1 ? new string[0] : fileNames.ToArray();
      if (strArray.Length != 0)
      {
        for (int index = 0; index < strArray.Length; ++index)
          strArray[index] = Path.Combine(this.PDMSystem.PDMSystemContext.FileVaultService.WorkArea.AreaPath, strArray[index]);
      }
      return strArray;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  private void ApplyFileExtensionFilter(List<string> objectFiles, string fileExtensionFilter)
  {
    if (string.IsNullOrEmpty(fileExtensionFilter))
      return;
    List<string> allowedExtensions = this.ParseFileExtensionFilter(fileExtensionFilter);
    objectFiles.RemoveAll((Predicate<string>) (objectFileName => !allowedExtensions.Contains(Path.GetExtension(objectFileName))));
  }

  private List<string> ParseFileExtensionFilter(string fileExtensionFilter)
  {
    List<string> list = new List<string>((IEnumerable<string>) fileExtensionFilter.Split(';'));
    CollectionUtils.Transform<string>((IList<string>) list, (Converter<string, string>) (@extension => @extension.Trim()));
    list.RemoveAll(new Predicate<string>(string.IsNullOrEmpty));
    CollectionUtils.Transform<string>((IList<string>) list, (Converter<string, string>) (@extension => !@extension.StartsWith(".") ? "." + @extension : @extension));
    return list;
  }

  public string[] GetAdditionalDrawings()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetAdditionalDrawings");
    this.PDMSystem.PrepareCall();
    try
    {
      if (!this.PDMSystem.AllModelTypes.Contains(this.ObjectType))
        return new string[0];
      List<string> fileNames = this.PDMSystem.PDMSystemContext.FileVaultService.DBFilesInfo.GetFileNames(this.ObjectId);
      if (fileNames.Count == 0)
        return new string[0];
      fileNames.RemoveAt(0);
      IModelDrawingsService service = ServiceUtils.GetService<IModelDrawingsService>((object) this.PDMSystem.Integrator, true);
      List<string> allAsList = CollectionUtils.FindAllAsList<string>((ICollection<string>) fileNames, new Predicate<string>(service.IsDrawingFileName));
      for (int index = 0; index < allAsList.Count; ++index)
        allAsList[index] = Path.Combine(this.PDMSystem.PDMSystemContext.FileVaultService.WorkArea.AreaPath, allAsList[index]);
      return allAsList.ToArray();
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void GetRefsVersionInfo2(
    string[] pDocFullPaths,
    out long[] ppCurrentVersions,
    out long[] ppActualVersions,
    out long[] ppMaxVersions)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetRefsVersionInfo2");
    this.PDMSystem.PrepareCall();
    try
    {
      if (pDocFullPaths == null && pDocFullPaths.Length == 0)
      {
        ppCurrentVersions = new long[0];
        ppActualVersions = new long[0];
        ppMaxVersions = new long[0];
      }
      else
      {
        ppCurrentVersions = new long[pDocFullPaths.Length];
        ppActualVersions = new long[pDocFullPaths.Length];
        ppMaxVersions = new long[pDocFullPaths.Length];
        for (int index = 0; index < pDocFullPaths.Length; ++index)
          this.PDMSystem.GetDocVersionInfo2(pDocFullPaths[index], out ppCurrentVersions[index], out ppActualVersions[index], out ppMaxVersions[index]);
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void SelectVersion2(out long plSelectedVersion, out string pbstrFullPath)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SelectVersion2");
    this.PDMSystem.PrepareCall();
    try
    {
      List<long> objectVersions;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        objectVersions = sessionKeeper.Session.GetObjectVersions(this.ID);
      DescriptorCollection descriptors = new DescriptorCollection();
      foreach (long objID in objectVersions)
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objID));
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects("Выбор объекта", "Выберите нужную версию документа", (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Все версии объекта", descriptors), SelectionOptions.SelectObjects | SelectionOptions.DisableMultiselect);
      if (numArray == null || numArray.Length != 1)
      {
        plSelectedVersion = 0L;
        pbstrFullPath = (string) null;
      }
      else
      {
        long objectId = numArray[0];
        PDMDocument pdmDocument = objectId == this.ObjectId ? this : this.PDMSystem.GetDocumentByObjectId(objectId);
        string workspacePath = pdmDocument.TryGetWorkspacePath();
        if (!string.IsNullOrEmpty(workspacePath))
          pdmDocument.UpdateWorkspace(true);
        long num = Math.Abs(objectId);
        plSelectedVersion = num;
        pbstrFullPath = workspacePath;
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void SetRefVersion2(IPDMDocument4 pRefDoc, long lVersion)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SetRefVersion2");
    this.PDMSystem.PrepareCall();
    try
    {
      if (lVersion == -1L)
        lVersion = Math.Abs(this.GetBaseVersionId());
      PDMDocument implementationDocument = this.ConvertToImplementationDocument((object) pRefDoc, nameof (pRefDoc));
      if (!this.PDMSystem.SoftInstantiationHelper.IsAllowed(this.ObjectType, implementationDocument.ObjectType, IDCache.Default.DocumentTree.Id))
        throw new NotSupportedException($"Настройки IPS запрещают пользователю фиксировать конкретную версию документа '{implementationDocument.Caption}' в составе документа '{this.Caption}'.");
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelation relation = sessionKeeper.Session.GetRelation(this.ObjectId, implementationDocument.ID, IDCache.Default.DocumentTree.Id);
        if (relation == null)
          return;
        AttributeValues[] attributeValues = DBAttributeHelper.ToAttributeValues(new ValueRecord((StringKey) IDCache.Default.FixedRelation.Text, (object) Math.Abs(implementationDocument.ConvertVersionIdToObjectId(lVersion, false))));
        relation.SetAttributesValues(attributeValues);
      }
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string PrepareVersionForInsertingIntoAssembly2(long lVersion, string bstrAssemblyPath)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.PrepareVersionForInsertingIntoAssembly2");
    this.PDMSystem.PrepareCall();
    try
    {
      if (lVersion == -1L)
        lVersion = this.GetBaseVersionId();
      long objectId = this.ConvertVersionIdToObjectId(lVersion, true);
      return (objectId == this.ObjectId ? this : this.PDMSystem.GetDocumentByObjectId(objectId)).PrepareForInsertingIntoAssembly(bstrAssemblyPath);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void GetRefsVersionInfo(
    string[] pDocFullPaths,
    out int[] ppCurrentVersions,
    out int[] ppActualVersions,
    out int[] ppMaxVersions)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetRefsVersionInfo");
    this.PDMSystem.PrepareCall();
    ppCurrentVersions = (int[]) null;
    ppActualVersions = (int[]) null;
    ppMaxVersions = (int[]) null;
    this.PDMSystem.ThrowCantImplement();
  }

  public void SelectVersion(out int plSelectedVersion, out string pbstrFullPath)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SelectVersion");
    this.PDMSystem.PrepareCall();
    plSelectedVersion = 0;
    pbstrFullPath = (string) null;
    this.PDMSystem.ThrowCantImplement();
  }

  public void SetRefVersion(IPDMDocument4 pRefDoc, int lVersion)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SetRefVersion");
    this.PDMSystem.PrepareCall();
    this.PDMSystem.ThrowCantImplement();
  }

  public string PrepareVersionForInsertingIntoAssembly(int lVersion, string bstrAssemblyPath)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.PrepareVersionForInsertingIntoAssembly");
    this.PDMSystem.PrepareCall();
    this.PDMSystem.ThrowCantImplement();
    return (string) null;
  }

  public void SelectEditContext(
    string bstrPath,
    out string[] pWhatToReplace,
    out string[] pToReplaceWith)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SelectEditContext");
    this.PDMSystem.PrepareCall();
    try
    {
      throw new NotSupportedException("Данная функциональность не может быть реализована в IPS.");
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public IPDMArticle2[] GetArticles()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetArticles");
    this.PDMSystem.PrepareCall();
    try
    {
      DataTable documentArticles = DBDocumentHelper.FindDocumentArticles(this.ObjectId, VersionsRuleSources.GetEditorRule(), true);
      IPDMArticle2[] articles = new IPDMArticle2[documentArticles.Rows.Count];
      for (int index = 0; index < documentArticles.Rows.Count; ++index)
      {
        long int64 = Convert.ToInt64(documentArticles.Rows[index][1]);
        articles[index] = (IPDMArticle2) new PDMArticle(int64, this, this.PDMSystem);
      }
      return articles;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public IPDMDocument3[] GetDrawings()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetDrawings");
    this.PDMSystem.PrepareCall();
    try
    {
      CADSettings cadSettings = ServiceUtils.GetService<ICADSettingsService>((object) this.PDMSystem.Integrator, true).GetCADSettings();
      List<LocalId<int>> localIdList = new List<LocalId<int>>(16 /*0x10*/);
      localIdList.AddRange((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("AssemblyDrawing", true).DocumentTypes);
      localIdList.AddRange((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("PartDrawing", true).DocumentTypes);
      List<int> drawingTypes = localIdList.ConvertAll<int>((Converter<LocalId<int>, int>) (item => item.Id));
      List<long> documentDrawings = DBDocumentHelper.FindDocumentDrawings(this.ObjectId, VersionsRuleSources.GetEditorRule(), (IList<int>) drawingTypes);
      IPDMDocument3[] drawings = new IPDMDocument3[documentDrawings.Count];
      for (int index = 0; index < documentDrawings.Count; ++index)
        drawings[index] = (IPDMDocument3) new PDMDocument(documentDrawings[index], this.PDMSystem);
      return drawings;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void ShowAdditionalFiles()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.ShowAdditionalFiles");
    this.PDMSystem.PrepareCall();
    try
    {
      int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, this.ObjectId, "ObjectFiles");
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string GetWorkingCopyPath()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetWorkingCopyPath");
    this.PDMSystem.PrepareCall();
    try
    {
      return this.TryGetWorkspacePath();
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void UpdateWorkingCopy()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.UpdateWorkingCopy");
    this.PDMSystem.PrepareCall();
    try
    {
      this.UpdateWorkspace(true);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string GetID()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetID");
    this.PDMSystem.PrepareCall();
    try
    {
      if (this.stringIdCache == null)
        this.stringIdCache = this.ObjectId.ToString();
      return this.stringIdCache;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void ViewInCAD()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.ViewInCAD");
    this.PDMSystem.PrepareCall();
    try
    {
      lock (this.PDMSystem.Integrator)
        this.PDMSystem.PDMSystemContext.LaunchActionService.Launch(new LaunchParams(ServiceUtils.GetService<ILaunchActionSupport>((object) this.PDMSystem.Integrator, true).IsSupported(LaunchType.View) ? LaunchType.View : LaunchType.Edit, this.ObjectId, this.ObjectType, VersionsRuleSources.GetEditorRule(), false));
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public bool WillBeReopened()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.WillBeReopened");
    this.PDMSystem.PrepareCall();
    try
    {
      IFileVault fileVaultService = this.PDMSystem.PDMSystemContext.FileVaultService;
      DBObjectState objectByVersionId = fileVaultService.WorkArea.FindPublishedObjectByVersionId(this.ObjectId);
      if (objectByVersionId == null)
        return true;
      List<DBObjectState> dbObjectStateList = new List<DBObjectState>();
      dbObjectStateList.Add(objectByVersionId);
      fileVaultService.DBObjectsInfo.RemoveDeadObjects(dbObjectStateList);
      DBObjectFilesDifferenceCalculator differenceCalculator = fileVaultService.WorkArea.CreateObjectFilesDifferenceCalculator();
      differenceCalculator.AddRange((ICollection<DBObjectState>) dbObjectStateList);
      differenceCalculator.Calculate();
      return fileVaultService.DBObjectsInfo.FindOutdatedObjects(differenceCalculator.Results, false).Count > 0;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public void CheckOut()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.CheckOut");
    this.RunObjectCopyCommand("Checkout");
  }

  public void SaveChanges()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SaveChanges");
    this.RunObjectCommand(nameof (SaveChanges));
  }

  public void CancelChanges()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.CancelChanges");
    this.RunObjectCopyCommand(nameof (CancelChanges));
  }

  public void CheckIn()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.CheckIn");
    ServiceContainer contextServices = new ServiceContainer();
    contextServices.AddService(typeof (ExtendedSaveOptions), (object) new ExtendedSaveOptions(SaveChangesMode.Checkin));
    this.RunObjectCopyCommand("Checkin", (System.IServiceProvider) contextServices);
  }

  public string CheckedOutBy
  {
    get
    {
      if (PDMSystemTrace.General.TraceVerbose)
        Trace.WriteLine("PDMDocument.CheckedOutBy");
      this.PDMSystem.PrepareCall();
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, false);
          if (dbObject != null)
          {
            long checkoutBy = dbObject.CheckoutBy;
            switch (checkoutBy)
            {
              case -1:
              case 0:
                break;
              default:
                if (checkoutBy != sessionKeeper.Session.UserID)
                  return sessionKeeper.Session.GetObjectInfo(checkoutBy).Caption;
                break;
            }
          }
          return string.Empty;
        }
      }
      catch (Exception ex)
      {
        this.PDMSystem.ReportException(ex);
        throw;
      }
    }
  }

  public DateTime LastModified
  {
    get
    {
      if (PDMSystemTrace.General.TraceVerbose)
        Trace.WriteLine("PDMDocument.LastModified");
      this.PDMSystem.PrepareCall();
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          return sessionKeeper.Session.GetObject(this.ObjectId, true).ModifyDate;
      }
      catch (Exception ex)
      {
        this.PDMSystem.ReportException(ex);
        throw;
      }
    }
  }

  public EDocumentStatus Status
  {
    get
    {
      if (PDMSystemTrace.General.TraceVerbose)
        Trace.WriteLine("PDMDocument.Status");
      this.PDMSystem.PrepareCall();
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.ObjectId, false);
          if (dbObject == null)
            return EDocumentStatus.DS_Unknown;
          long checkoutBy = dbObject.CheckoutBy;
          return checkoutBy != 0L ? (checkoutBy == sessionKeeper.Session.UserID ? EDocumentStatus.DS_CheckedOut : EDocumentStatus.DS_CheckedOutByDifferentUser) : EDocumentStatus.DS_CheckedIn;
        }
      }
      catch (Exception ex)
      {
        this.PDMSystem.ReportException(ex);
        throw;
      }
    }
  }

  public string[] GetParameterNames(bool bConvertedNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetParameterNames");
    return this.paramContainer.GetParameterNames(bConvertedNames);
  }

  public void GetParameters(
    string[] pParameterNames,
    bool bConvertedNames,
    out object[] ppValues,
    out short[] ppIsReadOnly)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.GetParameters");
    this.paramContainer.GetParameters(pParameterNames, bConvertedNames, out ppValues, out ppIsReadOnly);
  }

  public void SetParameters(string[] pParameterNames, object[] pValues, bool bConvertedNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.SetParameters");
    this.paramContainer.SetParameters(pParameterNames, pValues, bConvertedNames);
  }

  public void DeleteParameters(string[] pParameterNames, bool bConvertedNames)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.DeleteParameters");
    this.paramContainer.DeleteParameters(pParameterNames, bConvertedNames);
  }

  public void EditParameters()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.EditParameters");
    this.PDMSystem.PrepareCall();
    try
    {
      int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, this.ObjectId);
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public string PrepareForInsertingIntoAssembly(string bstrAssemblyPath)
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.PrepareForInsertingIntoAssembly");
    this.PDMSystem.PrepareCall();
    try
    {
      string workspacePath = this.TryGetWorkspacePath();
      if (!string.IsNullOrEmpty(workspacePath))
        this.UpdateWorkspace(true);
      if (PDMSystemTrace.General.TraceVerbose)
      {
        Trace.Indent();
        Trace.WriteLine($"Assembly path: {bstrAssemblyPath}");
        Trace.WriteLine($"Component path: {workspacePath}");
        Trace.Unindent();
      }
      return workspacePath;
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  public bool ShowSynchronizationDialog()
  {
    if (PDMSystemTrace.General.TraceVerbose)
      Trace.WriteLine("PDMDocument.ShowSynchronizationDialog");
    this.PDMSystem.PrepareCall();
    try
    {
      return this.ObjectId != 0L && this.ObjectId != -1L && this.SyncDocumentInternal();
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  private bool SyncDocumentInternal()
  {
    SynchronizeAction synchronizeAction = new SynchronizeAction(this);
    synchronizeAction.Perform();
    return synchronizeAction.ReloadRequired;
  }

  protected override void OnObjectIdChanged()
  {
    base.OnObjectIdChanged();
    this.stringIdCache = (string) null;
  }

  private void RunObjectCopyCommand(string commandName, System.IServiceProvider contextServices = null)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      ObjectCopyCommand copyCommandByName = ObjectCommandFactory.CreateObjectCopyCommandByName(commandName, true);
      copyCommandByName.ObjectId = this.ObjectId;
      if (contextServices != null)
        copyCommandByName.ContextServices = contextServices;
      copyCommandByName.Execute();
      this.ObjectId = copyCommandByName.NewObjectId;
    }
    catch (AbortException ex)
    {
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  private void RunObjectCommand(string commandName)
  {
    this.PDMSystem.PrepareCall();
    try
    {
      ObjectCommand objectCommandByName = ObjectCommandFactory.CreateObjectCommandByName(commandName, true);
      objectCommandByName.ObjectId = this.ObjectId;
      objectCommandByName.Execute();
    }
    catch (AbortException ex)
    {
    }
    catch (Exception ex)
    {
      this.PDMSystem.ReportException(ex);
      throw;
    }
  }

  private PDMDocument ConvertToImplementationDocument(object pdmDocument, string paramName)
  {
    if (pdmDocument == null)
      throw new ArgumentNullException(paramName);
    return pdmDocument is PDMDocument pdmDocument1 ? pdmDocument1 : throw new ArgumentException("Неподдерживаемая реализация PDM-документа.", paramName);
  }

  private long ConvertVersionIdToObjectId(long lVersion, bool useWorkCopyIfAvailable)
  {
    if (Intermech.Consts.IsUndefinedObjectId(lVersion))
      throw new ArgumentException("Не задан идентификатор версии объекта.", nameof (lVersion));
    if (useWorkCopyIfAvailable)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(lVersion, true);
        if (objectActualCopy.ID != this.ID)
          throw this.InvalidVersionNumber(lVersion);
        return objectActualCopy.ObjectID;
      }
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(lVersion);
      if (objectInfo.Empty || objectInfo.ID != this.ID)
        throw this.InvalidVersionNumber(lVersion);
      return lVersion;
    }
  }

  private InvalidOperationException InvalidVersionNumber(long lVersion)
  {
    return new InvalidOperationException($"Указанная версия #{lVersion} не является версией PDM-документа '{this.Caption}'.");
  }

  private string TryGetWorkspacePath()
  {
    if (!this.HasFilesToPublish())
      return string.Empty;
    string masterFileName = this.PDMSystem.PDMSystemContext.FileVaultService.DBFilesInfo.GetMasterFileName(this.ObjectId, false);
    return string.IsNullOrEmpty(masterFileName) ? string.Empty : Path.Combine(this.PDMSystem.PDMSystemContext.FileVaultService.WorkArea.AreaPath, masterFileName);
  }

  private void UpdateWorkspace(bool includeDependencies)
  {
    if (!this.HasFilesToPublish())
      return;
    this.PDMSystem.PDMSystemContext.FileVaultService.WorkArea.Publish(includeDependencies ? (IList<DBObjectState>) this.PDMSystem.PDMSystemContext.FileVaultService.DBObjectsInfo.CreateStateListForObjectTree(this.ObjectId, VersionsRuleSources.GetEditorRule()) : (IList<DBObjectState>) this.PDMSystem.PDMSystemContext.FileVaultService.DBObjectsInfo.CreateStateListForSingleObject(this.ObjectId), (IReplaceFilePolicy) new PreserveAnyChanges());
  }

  private bool HasFilesToPublish()
  {
    FileAttributeEditMode? attributeEditMode = this.PDMSystem.PDMSystemContext.FileAttributeEditorService.GetFileAttributeEditMode(this.ObjectType);
    return attributeEditMode.HasValue && attributeEditMode.Value == FileAttributeEditMode.Normal;
  }
}
