// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADSystemProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Collections;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Win32;
using Interop.CADInterface;
using Interop.Cadmech;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class CADSystemProxy : CADInterfaceObjectProxy
{
  private const string AttInterfaceProgId = "AttributeInterface.AttInterface";
  private ICADSystem2 rawCADSystem;
  private CADSystemProxyBuilder builder;
  private PhysicalValuesService physicalValues;
  private ICADSystemResourceTracker apiResourceTracker;
  private CADSystemCache cache;
  private bool groupOperationStarted;
  private GroupOperationTypes groupOperationType;
  private CADSystemCapabilities? capabilities;
  private CADSystemVisualStateBuilder visualStateBuilder;

  public CADSystemProxy(ICADSystem2 rawCADSystem, CADSystemProxyBuilder builder)
  {
    if (rawCADSystem == null)
      throw new ArgumentNullException(nameof (rawCADSystem));
    if (builder == null)
      throw new ArgumentNullException(nameof (builder));
    this.rawCADSystem = rawCADSystem;
    this.builder = builder;
    this.physicalValues = new PhysicalValuesService();
  }

  /// <summary>
  /// Возвращает простроитель proxy-объектов для COM-объектов CAD-интерфейса.
  /// </summary>
  public CADSystemProxyBuilder Builder
  {
    [DebuggerStepThrough] get => this.builder;
  }

  /// <summary>
  /// Возвращает объект для работы с измеряемыми величинами.
  /// </summary>
  public PhysicalValuesService PhysicalValues
  {
    [DebuggerStepThrough] get => this.physicalValues;
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект CAD-системы. Это свойство должно использоваться в тех случаях,
  /// когда объект CAD-системы требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только CADSystemProxy.
  /// </summary>
  public ICADSystem2 RawObject
  {
    [DebuggerStepThrough] get => this.rawCADSystem;
  }

  public CloneDataProxy CreateCloneData()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADSystemProxy.CreateCloneData()");
    return new CloneDataProxy(this);
  }

  public void Clone(CloneDataProxy cloneData)
  {
    if (cloneData == null)
      throw new ArgumentNullException(nameof (cloneData));
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem7.Clone()");
    if (!cloneData.HasFiles)
      return;
    try
    {
      ((ICADSystem7) this.RawObject).Clone(cloneData.RawObject);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem7.Clone()");
    }
    catch (ArgumentException ex)
    {
      throw this.WrapExternalException((Exception) ex, "ICADSystem7.Clone()", (string) null);
    }
  }

  public virtual List<string> GetExportFormats(CADDocumentType documentType)
  {
    ECADDocType nativeDocumentType = CADDocumentTypeConverter.ToNativeDocumentType(documentType);
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<ECADDocType>("ICADSystem6.GetExportFormats()", nativeDocumentType);
    string[] exportFormats;
    try
    {
      exportFormats = ((ICADSystem6) this.RawObject).GetExportFormats(nativeDocumentType);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem6.GetExportFormats()");
    }
    if (exportFormats == null || exportFormats.Length == 0)
      return new List<string>(0);
    List<string> list = new List<string>((IEnumerable<string>) exportFormats);
    CollectionUtils.RemoveAll<string>((IList<string>) list, new Predicate<string>(string.IsNullOrEmpty));
    for (int index = 0; index < list.Count; ++index)
    {
      if (list[index][0] != '.')
        list[index] = "." + list[index];
    }
    return list;
  }

  public void BeginGroupOperation(GroupOperationTypes operationType)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<GroupOperationTypes>("CADSystemProxy.BeginGroupOperation()", operationType);
    if (this.groupOperationStarted)
      throw new InvalidOperationException("Повторный вызов метода BeginGroupOperation недопустим.");
    if (this.rawCADSystem is ICADSystem4 rawCadSystem)
    {
      this.RawGroupOperation(rawCadSystem, (EGroupOperationTypes) operationType, true);
      this.groupOperationStarted = true;
      this.groupOperationType = operationType;
    }
    else
    {
      if (!CADInterfaceTracing.Proxies.TraceWarning)
        return;
      Trace.TraceWarning("Не удалось привести объект CAD-систесы к интерфейсу ICADSystem4. Объект не поддерживает данный интерфейс.");
    }
  }

  public void EndGroupOperation()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADSystemProxy.EndGroupOperation()");
    if (!this.groupOperationStarted)
      return;
    try
    {
      this.RawGroupOperation((ICADSystem4) this.rawCADSystem, (EGroupOperationTypes) this.groupOperationType, false);
    }
    finally
    {
      this.groupOperationStarted = false;
    }
  }

  private void RawGroupOperation(
    ICADSystem4 rawCADSystem4,
    EGroupOperationTypes rawOperationType,
    bool startMode)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<EGroupOperationTypes, bool>("ICADSystem4.GroupOperation()", rawOperationType, startMode);
    try
    {
      rawCADSystem4.GroupOperation(rawOperationType, startMode);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem4.GroupOperation()");
    }
  }

  public bool GroupOperationStarted => this.groupOperationStarted;

  public void SetLocalizer(AttributeLocalizer localizer)
  {
    if (localizer == null)
      throw new ArgumentNullException(nameof (localizer));
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<AttributeLocalizer>("ICADSystem2.SetAttributeLocalizer()", localizer);
    try
    {
      this.rawCADSystem.SetAttributeLocalizer(localizer);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.SetAttributeLocalizer()");
    }
  }

  public int GetVersion()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem2.GetVersion()");
    try
    {
      return this.rawCADSystem.GetVersion();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.GetVersion()");
    }
  }

  public CADSystemCapabilities Capabilities
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADSystemProxy.Capabilities");
      if (!this.capabilities.HasValue)
      {
        CADSystemCapabilities systemCapabilities = CADSystemCapabilities.None;
        if (this.RawGetCADProperty(ECADProperty.CP_DerivedConfigurations))
          systemCapabilities |= CADSystemCapabilities.DerivedConfigurations;
        if (this.RawGetCADProperty(ECADProperty.CP_SingleConfigurationInDocument))
          systemCapabilities |= CADSystemCapabilities.SingleConfigurationPerFile;
        this.capabilities = new CADSystemCapabilities?(systemCapabilities);
      }
      return this.capabilities.Value;
    }
  }

  private bool RawGetCADProperty(ECADProperty cadProperty)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<ECADProperty>("ICADSystem.GetCADProperty()", cadProperty);
    try
    {
      switch (this.rawCADSystem.GetCADProperty(cadProperty))
      {
        case bool cadProperty1:
          return cadProperty1;
        case int num:
          return num != 0;
        default:
          return false;
      }
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem.GetCADProperty()");
    }
  }

  /// <summary>
  /// Возвращает расширения файлов документов CAD-системы определенного типа.
  /// </summary>
  /// <param name="documentType">Тип документа CAD-системы</param>
  /// <returns>Массив расширений файлов</returns>
  /// <exception cref="T:Intermech.Tools.Integrators.AppNotInstalledException">Не удалось найти CAD-систему на компьютере</exception>
  public string[] GetFileExtensions(CADDocumentType documentType)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<CADDocumentType>("ICADSystem2.GetFileExtensions()", documentType);
    string str;
    try
    {
      str = this.rawCADSystem.GetFileExtensions(CADDocumentTypeConverter.ToNativeDocumentType(documentType));
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.GetFileExtensions()");
    }
    if (str == null)
      str = string.Empty;
    string[] fileExtensions = str.Split(',');
    for (int index = 0; index < fileExtensions.Length; ++index)
    {
      if (!fileExtensions[index].StartsWith("."))
        fileExtensions[index] = "." + fileExtensions[index];
    }
    return fileExtensions;
  }

  public virtual void SetWorkingFolder(string fullPath)
  {
    if (string.IsNullOrEmpty(fullPath))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_320"), nameof (fullPath));
    if (!Path.IsPathRooted(fullPath))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_321"), nameof (fullPath));
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICADSystem2.SetWorkingFolder()", fullPath);
    try
    {
      this.rawCADSystem.SetWorkingFolder(fullPath);
    }
    catch (COMException ex)
    {
      if (ex.ErrorCode == -2147467260 /*0x80004004*/)
        throw new ApplicationProxyException("Не удалось задать рабочий каталог для CAD-системы. Скорее всего, в CAD-системе имеются открытые документы. Закройте все документы и повторите попытку.", (Exception) ex);
      throw this.WrapExternalException(ex, "ICADSystem2.SetWorkingFolder()");
    }
  }

  public virtual CADDocumentProxy OpenDocument(string fullName, bool openVisible)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string, bool>("CADSystemProxy.OpenDocument()", fullName, openVisible);
    return this.builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider((ICADDocument) this.InternalOpenDocument(fullName, false, openVisible), fullName), this);
  }

  public CADDocumentProxy OpenDocumentWithReplaceFiles(
    string fullName,
    bool openVisible,
    ICollection<string> whatToReplaceFiles,
    ICollection<string> replaceWithFiles)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string, bool, ICollection<string>, ICollection<string>>("CADSystemProxy.OpenDocumentWithReplaceFiles()", fullName, openVisible, whatToReplaceFiles, replaceWithFiles);
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullName));
    if (!Path.IsPathRooted(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (fullName));
    if (whatToReplaceFiles == null)
      throw new ArgumentNullException(nameof (whatToReplaceFiles));
    if (replaceWithFiles == null)
      throw new ArgumentNullException(nameof (replaceWithFiles));
    if (replaceWithFiles.Count != whatToReplaceFiles.Count)
      throw new ArgumentException("Длины коллекций whatToReplaceFiles и replaceWithFiles должны быть равны.", nameof (replaceWithFiles));
    string[] strArray1 = new string[whatToReplaceFiles.Count];
    whatToReplaceFiles.CopyTo(strArray1, 0);
    string[] strArray2 = new string[replaceWithFiles.Count];
    replaceWithFiles.CopyTo(strArray2, 0);
    return this.OpenDocumentWithReplaceFilesCore(fullName, openVisible, strArray1, strArray2);
  }

  public CADDocumentProxy OpenDocumentWithReplaceFiles(
    string fullName,
    bool openVisible,
    string[] whatToReplaceFiles,
    string[] replaceWithFiles)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string, bool, string[], string[]>("CADSystemProxy.OpenDocumentWithReplaceFiles()", fullName, openVisible, whatToReplaceFiles, replaceWithFiles);
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullName));
    if (!Path.IsPathRooted(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (fullName));
    if (whatToReplaceFiles == null)
      throw new ArgumentNullException(nameof (whatToReplaceFiles));
    if (replaceWithFiles == null)
      throw new ArgumentNullException(nameof (replaceWithFiles));
    if (replaceWithFiles.Length != whatToReplaceFiles.Length)
      throw new ArgumentException("Длины массивов whatToReplaceFiles и replaceWithFiles должны быть равны.", nameof (replaceWithFiles));
    return this.OpenDocumentWithReplaceFilesCore(fullName, openVisible, whatToReplaceFiles, replaceWithFiles);
  }

  private CADDocumentProxy OpenDocumentWithReplaceFilesCore(
    string fullName,
    bool openVisible,
    string[] whatToReplaceFiles,
    string[] replaceWithFiles)
  {
    ICADDocument3 withReplaceArray = this.RawGetDocumentWithReplaceArray(fullName, openVisible, whatToReplaceFiles, replaceWithFiles);
    if (withReplaceArray != null)
      return this.builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider((ICADDocument) withReplaceArray, fullName), this);
    return this.FindOpenDocument(fullName) ?? throw new ApplicationProxyException($"CAD-интерфейс не поддерживает открытие документа '{fullName}' с заменой файловых ссылок.");
  }

  protected virtual ICADDocument3 RawGetDocumentWithReplaceArray(
    string fullName,
    bool openVisible,
    string[] whatToReplaceFiles,
    string[] replaceWithFiles)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, bool, string[], string[]>("ICADSystem5.GetDocumentWithReplaceArray()", fullName, openVisible, whatToReplaceFiles, replaceWithFiles);
    try
    {
      ICADDocument3 ppDocument;
      ((ICADSystem5) this.RawObject).GetDocumentWithReplaceArray(fullName, openVisible, whatToReplaceFiles, replaceWithFiles, out ppDocument);
      return ppDocument;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem5.GetDocumentWithReplaceArray()");
    }
  }

  public virtual void CloseFiles(ICollection<string> fullPathList)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<ICollection<string>>("CADSystemProxy.CloseFiles()", fullPathList);
    if (fullPathList == null)
      throw new ArgumentNullException(nameof (fullPathList));
    if (fullPathList.Count == 0)
      return;
    List<string> fullPathList1 = new List<string>(fullPathList.Count);
    List<string> fullPathList2 = new List<string>(fullPathList.Count);
    foreach (string fullPath in (IEnumerable<string>) fullPathList)
    {
      if (!string.IsNullOrEmpty(fullPath) && Path.IsPathRooted(fullPath))
      {
        switch (this.GetDocumentOpenStatus(fullPath))
        {
          case CADDocumentOpenStatus.OpenInvisible:
            fullPathList2.Add(fullPath);
            continue;
          case CADDocumentOpenStatus.OpenVisible:
            fullPathList1.Add(fullPath);
            continue;
          default:
            continue;
        }
      }
    }
    if (fullPathList1.Count > 0)
      this.CloseDocumentsCore((ICollection<string>) fullPathList1);
    if (fullPathList2.Count <= 0)
      return;
    this.CloseDocumentsCore((ICollection<string>) fullPathList2);
  }

  private void CloseDocumentsCore(ICollection<string> fullPathList)
  {
    foreach (string fullPath in (IEnumerable<string>) fullPathList)
    {
      CADDocumentProxy openDocument = this.FindOpenDocument(fullPath);
      if (openDocument != null)
      {
        if (PathUtils.IsSamePath(openDocument.FullName, fullPath))
        {
          try
          {
            openDocument.Close();
            if (CADInterfaceTracing.Proxies.TraceVerbose)
              Trace.WriteLine($"A file '{fullPath}' reported as closed (actually = {(this.IsOpenDocument(fullPath) ? (object) "no" : (object) "yes")})");
          }
          catch (Exception ex)
          {
            if (CADInterfaceTracing.Proxies.TraceVerbose)
            {
              Trace.WriteLine($"Unable to close a file '{fullPath}'");
              Trace.WriteLine($"CAD system throws an exception: {ex.Message}");
            }
            throw;
          }
        }
      }
    }
  }

  /// <summary>Ищет открытый документ в памяти CAD-системы.</summary>
  /// <param name="fullName">Абсолютное имя файла документа</param>
  /// <returns>Найденный документ</returns>
  /// <exception cref="T:System.ArgumentException">Имя файла не задано, либо не содержит абсолютного пути</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.ComInterop.ExternalApplicationException">Не удалось выполнить метод из-за ошибки</exception>
  public virtual CADDocumentProxy FindOpenDocument(string fullName)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("CADSystemProxy.FindOpenDocument()", fullName);
    ICADDocument2 rawDocument = this.InternalOpenDocument(fullName, true, false);
    return rawDocument == null ? (CADDocumentProxy) null : this.builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider((ICADDocument) rawDocument, fullName), this);
  }

  public virtual CADDocumentProxy GetActiveDocument()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem2.GetActiveDocument()");
    try
    {
      ICADDocument2 ppDocument;
      this.rawCADSystem.GetActiveDocument(out ppDocument);
      return ppDocument != null ? this.builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider((ICADDocument) ppDocument), this) : (CADDocumentProxy) null;
    }
    catch (COMException ex)
    {
      if (ex.ErrorCode == -2147467259 /*0x80004005*/)
        return (CADDocumentProxy) null;
      throw this.WrapExternalException(ex, "ICADSystem2.GetActiveDocument()");
    }
  }

  public bool IsActiveDocument(string fullName)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("CADSystemProxy.IsActiveDocument()", fullName);
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullName));
    return Path.IsPathRooted(fullName) ? this.DoIsActiveDocument(fullName) : throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (fullName));
  }

  protected virtual bool DoIsActiveDocument(string fullName)
  {
    CADDocumentProxy activeDocument = this.GetActiveDocument();
    return activeDocument != null && PathUtils.IsSamePath(activeDocument.FullName, fullName);
  }

  public virtual CADDocumentProxy CreateDocument(
    string documentFullName,
    CADDocumentType documentType,
    bool openVisible)
  {
    if (string.IsNullOrEmpty(documentFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (documentFullName));
    if (!Path.IsPathRooted(documentFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (documentFullName));
    if (documentType == CADDocumentType.DefinedByTemplate || documentType == CADDocumentType.Undefined)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_326"), nameof (documentType));
    if (File.Exists(documentFullName))
      throw new ApplicationProxyException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_327"), (object) documentFullName));
    ECADDocType nativeDocumentType = CADDocumentTypeConverter.ToNativeDocumentType(documentType);
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, ECADDocType, string, bool>("ICADSystem2.CreateDocument()", documentFullName, nativeDocumentType, string.Empty, openVisible);
    ICADDocument document;
    try
    {
      document = this.rawCADSystem.CreateDocument(documentFullName, nativeDocumentType, string.Empty, openVisible);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.CreateDocument()");
    }
    if (this.apiResourceTracker != null)
      this.apiResourceTracker.TrackOpenDocument(documentFullName, false);
    return this.builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(document, documentFullName), this);
  }

  /// <summary>
  /// Создает новый документ по указанному шаблону. Значения атрибутов в созданном документе могут оставаться
  /// такими же, как и в документе-шаблоне. Может потребоваться очистка обозначений/наименований конфигураций.
  /// </summary>
  /// <param name="documentFullName">Путь к файлу нового документа</param>
  /// <param name="templateFullName">Путь к файлу документа-шаблона</param>
  /// <param name="openVisible">Нужно ли открыть новый документ в окне</param>
  /// <returns>Объект созданного документа</returns>
  public virtual CADDocumentProxy CreateDocument(
    string documentFullName,
    string templateFullName,
    bool openVisible)
  {
    if (string.IsNullOrEmpty(documentFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (documentFullName));
    if (!Path.IsPathRooted(documentFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (documentFullName));
    if (string.IsNullOrEmpty(templateFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_329"), nameof (templateFullName));
    if (!Path.IsPathRooted(templateFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_330"), nameof (templateFullName));
    if (PathUtils.IsSamePath(documentFullName, templateFullName))
      throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_331"), (object) templateFullName));
    ECADDocType nativeDocumentType = CADDocumentTypeConverter.ToNativeDocumentType(CADDocumentType.DefinedByTemplate);
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, ECADDocType, string, bool>("ICADSystem2.CreateDocument()", documentFullName, nativeDocumentType, templateFullName, openVisible);
    ICADDocument document;
    try
    {
      document = this.rawCADSystem.CreateDocument(documentFullName, nativeDocumentType, templateFullName, openVisible);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.CreateDocument()");
    }
    if (this.apiResourceTracker != null)
      this.apiResourceTracker.TrackOpenDocument(documentFullName, false);
    return this.builder.CreateDocument((ICADDocumentProvider) new ExplicitCADDocumentProvider(document, documentFullName), this);
  }

  public virtual bool HasOpenFiles()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADSystemProxy.HasOpenFiles()");
    return this.GetActiveDocument() != null || this.GetOpenFiles(false).Count != 0;
  }

  public virtual ICollection<string> GetOpenFiles(bool visibleOnly)
  {
    List<string> pathList = new List<string>((IEnumerable<string>) this.RawGetOpenFiles(visibleOnly));
    this.FilterOpenFiles(pathList);
    return (ICollection<string>) pathList;
  }

  protected virtual void FilterOpenFiles(List<string> pathList)
  {
  }

  private string[] RawGetOpenFiles(bool visibleOnly)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<bool>("ICADSystem2.GetLoadedFiles()", visibleOnly);
    try
    {
      return this.rawCADSystem.GetLoadedFiles(visibleOnly) ?? new string[0];
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.GetLoadedFiles()");
    }
  }

  public virtual List<CADDocumentProxy> GetOpenDocuments(bool visibleOnly)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<bool>("CADSystemProxy.GetOpenDocuments()", visibleOnly);
    return CollectionUtils.ConvertAsList<string, CADDocumentProxy>(this.GetOpenFiles(visibleOnly), (Converter<string, CADDocumentProxy>) (openFilePath => this.builder.CreateDocument((ICADDocumentProvider) new LazyCADDocumentProvider(openFilePath, this), this)));
  }

  internal ICADDocument2 InternalOpenDocument(
    string fullName,
    bool openLoadedOnly,
    bool openVisible)
  {
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullName));
    CADDocumentOpenStatus openStatus = Path.IsPathRooted(fullName) ? this.RawGetDocumentStatus(fullName) : throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (fullName));
    bool alreadyOpen = openStatus != 0;
    if (alreadyOpen)
    {
      if (openVisible && this.apiResourceTracker != null)
        alreadyOpen = this.ValidateDocumentOpenStatus(fullName, openStatus) == CADDocumentOpenStatus.OpenVisible;
    }
    else if (openLoadedOnly)
      return (ICADDocument2) null;
    ICADDocument2 document2 = this.RawGetDocument2(fullName, openVisible);
    if (this.apiResourceTracker == null)
      return document2;
    this.apiResourceTracker.TrackOpenDocument(fullName, alreadyOpen);
    return document2;
  }

  private ICADDocument2 RawGetDocument2(string fullName, bool openVisible)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, bool>("ICADSystem2.GetDocument2()", fullName, openVisible);
    try
    {
      ICADDocument2 ppDocument;
      this.rawCADSystem.GetDocument2(fullName, openVisible, out ppDocument);
      return ppDocument;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.GetDocument2()");
    }
    catch (ArgumentException ex)
    {
      if (!File.Exists(fullName))
        throw this.WrapExternalException((Exception) ex, "ICADSystem2.GetDocument2()", $"Файл '{fullName}' не найден на диске.");
      throw this.WrapExternalException((Exception) ex, "ICADSystem2.GetDocument2()", $"Возможно, файл '{fullName}' поврежден или создан в другой версии приложения.");
    }
  }

  public virtual bool IsOpenDocument(string fullName)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("CADSystemProxy.IsOpenDocument()", fullName);
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullName));
    if (!Path.IsPathRooted(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (fullName));
    return this.RawGetDocumentStatus(fullName) != 0;
  }

  public virtual CADDocumentOpenStatus GetDocumentOpenStatus(string fullName)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("CADSystemProxy.GetDocumentOpenStatus()", fullName);
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (fullName));
    return Path.IsPathRooted(fullName) ? this.ValidateDocumentOpenStatus(fullName, this.RawGetDocumentStatus(fullName)) : throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (fullName));
  }

  private CADDocumentOpenStatus RawGetDocumentStatus(string fullName)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICADSystem2.GetDocumentStatus()", fullName);
    EOpenStatus documentStatus;
    try
    {
      documentStatus = this.rawCADSystem.GetDocumentStatus(fullName);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.GetDocumentStatus()");
    }
    switch (documentStatus)
    {
      case EOpenStatus.OS_NotOpen:
        return CADDocumentOpenStatus.NotOpen;
      case EOpenStatus.OS_OpenInvisible:
        return CADDocumentOpenStatus.OpenInvisible;
      case EOpenStatus.OS_OpenVisible:
        return CADDocumentOpenStatus.OpenVisible;
      default:
        throw new NotSupportedEnumException((Enum) documentStatus);
    }
  }

  protected virtual CADDocumentOpenStatus ValidateDocumentOpenStatus(
    string fullName,
    CADDocumentOpenStatus openStatus)
  {
    return openStatus;
  }

  public virtual ApplicationVisualState<CADSystemProxy> SaveVisualState(
    CADSystemVisualStateFlags flags)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CADSystemVisualStateFlags>("CADSystemProxy.SaveVisualState()", flags);
    return this.VisualStateBuilder.SaveState(this, flags);
  }

  public virtual void RestoreVisualState(
    ApplicationVisualState<CADSystemProxy> savedVisualState)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<ApplicationVisualState<CADSystemProxy>>("CADSystemProxy.RestoreVisualState()", savedVisualState);
    if (savedVisualState == null)
      throw new ArgumentNullException(nameof (savedVisualState));
    savedVisualState.RestoreState(this);
  }

  private CADSystemVisualStateBuilder VisualStateBuilder
  {
    [DebuggerStepThrough] get
    {
      if (this.visualStateBuilder == null)
        this.visualStateBuilder = new CADSystemVisualStateBuilder();
      return this.visualStateBuilder;
    }
  }

  public virtual IAttInterface GetAttInterface()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, RegistrationClassContext>("IAttInterface.ctor()", "AttributeInterface.AttInterface", RegistrationClassContext.LocalServer);
    try
    {
      IAttInterface obj = (IAttInterface) ComActivator.CreateInstance("AttributeInterface.AttInterface", RegistrationClassContext.LocalServer);
      DelayedInit.WaitReadyOrFail((Func<bool>) (() => obj.Ready), 120000, "Cadmech AttInterface");
      return obj;
    }
    catch (COMException ex)
    {
      throw new ApplicationProxyException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_337"), (object) this.Name, (object) ex.Message, this.rawCADSystem != null ? (object) (Environment.NewLine + this.rawCADSystem.GetLastErrorMessage()) : (object) string.Empty), (Exception) ex);
    }
  }

  public virtual void SwitchToApp()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem2.Activate()");
    if (!ForegroundWindowHelper.Default.AllowActionToComObject((object) this.rawCADSystem))
      ForegroundWindowHelper.Default.AllowActionToAnyProcess();
    try
    {
      this.rawCADSystem.Activate();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.Activate()");
    }
  }

  public string Name
  {
    [DebuggerStepThrough] get => this.RawGetName();
  }

  private string RawGetName()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem2.GetCADSystemName()");
    try
    {
      return this.rawCADSystem.GetCADSystemName();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADSystem2.GetCADSystemName()");
    }
  }

  public CadmechRootProxy GetCadmechRoot(bool throwIfNotFound)
  {
    CadmechRootProxy cadmechRoot;
    if (this.Cache != null && this.Cache.TryGetValue<CadmechRootProxy>((object) "CadmechRoot", out cadmechRoot))
      return cadmechRoot;
    cadmechRoot = CadmechRootProxy.Create(throwIfNotFound);
    if (cadmechRoot != null && this.Cache != null)
      this.Cache.SetValue((object) "CadmechRoot", (object) cadmechRoot);
    return cadmechRoot;
  }

  public ICADSystemResourceTracker ApiResourceTracker
  {
    [DebuggerStepThrough] get => this.apiResourceTracker;
    [DebuggerStepThrough] set
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace<ICADSystemResourceTracker>("CADSystemProxy.set_ApiResourceTracker()", value);
      this.apiResourceTracker = value;
    }
  }

  public TValue EvaluateCached<TValue>(object owner, string valueName, Func<TValue> valueFunction)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<object, string, Func<TValue>>("CADSystemProxy.EvaluateCached()", owner, valueName, valueFunction);
    if (owner == null)
      throw new ArgumentNullException(nameof (owner));
    if (string.IsNullOrEmpty(valueName))
      throw new ArgumentException("Не задано имя вычисляемого значения.", nameof (valueName));
    if (valueFunction == null)
      throw new ArgumentNullException(nameof (valueFunction));
    if (this.Cache == null)
      return valueFunction();
    Tuple<object, string> key = Tuple.Create<object, string>(owner, valueName);
    TValue cached;
    if (!this.Cache.TryGetValue<TValue>((object) key, out cached))
    {
      cached = valueFunction();
      this.Cache.SetValue((object) key, (object) cached);
    }
    return cached;
  }

  /// <summary>
  /// Возвращает или задает кэш объектов CAD-интерфейса и результатов вызовов "тяжелых" методов CAD-интерфейса. Он используется всеми компонентами <see cref="T:CADSystemProxy" />.
  /// По умолчанию значение этого свойства не задано.
  /// </summary>
  public CADSystemCache Cache
  {
    [DebuggerStepThrough] get => this.cache;
    [DebuggerStepThrough] set
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace<CADSystemCache>("CADSystemProxy.set_Cache()", value);
      if (this.cache == value)
        return;
      if (this.CacheChanging != null)
        this.CacheChanging((object) this, EventArgs.Empty);
      this.cache = value;
      if (this.CacheChanged == null)
        return;
      this.CacheChanged((object) this, EventArgs.Empty);
    }
  }

  /// <summary>
  /// Событие предстоящего изменения свойства Cache. Вызывается до изменения свойства <see cref="P:Cache" />.
  /// </summary>
  public event EventHandler CacheChanging;

  /// <summary>
  /// Событие изменения свойства Cache. Вызывается после изменения свойства <see cref="P:Cache" />.
  /// </summary>
  public event EventHandler CacheChanged;
}
