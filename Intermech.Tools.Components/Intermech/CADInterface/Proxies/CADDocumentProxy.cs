// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADDocumentProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Data;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.UI;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>Документ CAD-системы</summary>
[DebuggerDisplay("{FullName} : {DocumentType}")]
public class CADDocumentProxy : 
  CADSystemComponentProxy,
  IModelConfigurationsContainer,
  IParametersContainerProxy,
  IParametersContainerProvider,
  IIMTextDocumentProvider
{
  private ICADDocumentProvider documentProvider;
  private ParametersContainerProxy parametersProxy;
  private ICADDocument cachedRawDocument;
  private CADDocumentType? docType;
  private bool? hasConfigurations;
  private string fullName;
  private string masterFile;
  private bool? isInMemory;
  private bool? hasDefaultConfiguration;
  private WeakReference defaultConfigurationRef;

  public CADDocumentProxy(ICADDocumentProvider documentProvider, CADSystemProxy cadSystem)
    : base(cadSystem)
  {
    this.documentProvider = documentProvider != null ? documentProvider : throw new ArgumentNullException(nameof (documentProvider));
    this.parametersProxy = new ParametersContainerProxy((IParametersContainerProvider) this);
  }

  /// <summary>
  /// Очищает кэш значений свойств документа, который используется для оптимизации доступа к медленным, но редко изменяющимся свойствам.
  /// </summary>
  protected virtual void ResetPropertyCache()
  {
    this.cachedRawDocument = (ICADDocument) null;
    this.isInMemory = new bool?();
    this.fullName = (string) null;
    this.masterFile = (string) null;
    this.docType = new CADDocumentType?();
    this.hasConfigurations = new bool?();
    this.hasDefaultConfiguration = new bool?();
    this.defaultConfigurationRef = (WeakReference) null;
  }

  /// <summary>
  /// Возвращает документ IMTEXT для текущего документа CAD-системы.
  /// </summary>
  /// <param name="throwIfNoCadmechFound">Признак, что нужно бросать исключение, если CADMECH не установлен</param>
  /// <returns>Документ IMTEXT или null, если CADMECH не установлен</returns>
  /// <exception cref="T:System.ArgumentNullException">CADMECH не установлен и флаг throwIfNoCadmechFound = true</exception>
  public virtual IMTextDocumentProxy GetIMTextDocument(bool throwIfNoCadmechFound)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<bool>("CADDocumentProxy.GetIMTextDocument()", throwIfNoCadmechFound);
    if (string.IsNullOrEmpty(this.FullName))
      throw new ApplicationProxyException($"Невозможно получить объект IMTEXT для документа '{this.Title}', так как он не сохранен в файл.");
    this.ForceLoad();
    return this.CADSystem.GetCadmechRoot(throwIfNoCadmechFound)?.GetDocument(this.FullName);
  }

  /// <summary>Вынуждает CAD-систему загрузить документ в память.</summary>
  /// <remarks>
  /// Некоторые CAD-системы способны выполнять некоторые операции непосредственно в файле документа на диске, не загружая его в память CAD-системы.
  /// В большинстве случаев это хорошо, но иногда дополнительному API (например, CADMECH) требуется присутствие документа в памяти CAD-системы.
  /// </remarks>
  public void ForceLoad()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.ForceLoad()");
    this.DoForceLoad();
  }

  /// <summary>Вынуждает CAD-систему загрузить документ в память.</summary>
  protected virtual void DoForceLoad()
  {
  }

  /// <summary>
  /// Возвращает true, если документ создается CAD-системой "на лету" и не имеет файла.
  /// </summary>
  public virtual bool IsInMemory
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.get_IsInMemory()");
      if (!this.isInMemory.HasValue)
        this.isInMemory = new bool?(this.DetectIsInMemory());
      return this.isInMemory.Value;
    }
  }

  /// <summary>
  /// Реализует определение случаев, когда документ создается CAD-системой "на лету" и не имеет файла.
  /// </summary>
  protected virtual bool DetectIsInMemory() => false;

  public string FullName
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.get_FullName()");
      if (this.fullName == null)
        this.fullName = this.DetectFullName();
      return this.fullName;
    }
  }

  protected virtual string DetectFullName() => this.RawFullName;

  public string RawFullName
  {
    get
    {
      string fullPath1 = this.RawObjectProvider.TryGetFullPath();
      if (fullPath1 != null)
        return fullPath1;
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.get_FullPath()");
      string fullPath2;
      try
      {
        fullPath2 = this.RawObject.FullPath;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICADDocument.get_FullPath()");
      }
      return string.IsNullOrEmpty(fullPath2) || Path.IsPathRooted(fullPath2) ? fullPath2 : throw new ApplicationProxyException($"Не удалось определить путь к файлу '{fullPath2}'.");
    }
  }

  public virtual string Title
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.get_Title()");
      try
      {
        return this.RawObject.Title;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICADDocument.get_Title()");
      }
    }
  }

  public string MasterFile
  {
    get
    {
      if (this.masterFile == null)
        this.masterFile = this.DetectMasterFile();
      return this.masterFile;
    }
  }

  protected virtual string DetectMasterFile()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.GetMasterFileName()");
    string str;
    try
    {
      str = this.RawObject.GetMasterFileName();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetMasterFileName()");
    }
    if (!string.IsNullOrEmpty(str) && !Path.IsPathRooted(str))
      str = Path.Combine(Path.GetDirectoryName(this.FullName), str);
    return str;
  }

  public virtual bool ReadOnly
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.get_ReadOnly()");
      try
      {
        return this.RawObject.ReadOnly;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICADDocument.get_ReadOnly()");
      }
    }
    set
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<bool>("ICADDocument.set_ReadOnly()", value);
      try
      {
        this.RawObject.ReadOnly = value;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICADDocument.set_ReadOnly()");
      }
    }
  }

  public virtual bool Reloadable
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.IsReloadable()");
      try
      {
        bool pvbResult = false;
        this.RawObject.IsReloadable(ref pvbResult);
        return pvbResult;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICADDocument.IsReloadable()");
      }
    }
  }

  public virtual CADDocumentType DocumentType
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.get_DocType()");
      try
      {
        if (!this.docType.HasValue)
          this.docType = new CADDocumentType?(CADDocumentTypeConverter.ToProxyDocumentType(this.RawObject.DocType));
        return this.docType.Value;
      }
      catch (COMException ex)
      {
        if (ex.ErrorCode != -2147467259 /*0x80004005*/)
          throw this.WrapExternalException(ex, "ICADDocument.get_DocType()");
        this.docType = new CADDocumentType?(CADDocumentType.Undefined);
        return this.docType.Value;
      }
    }
  }

  public virtual bool IsMasterDocument
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.get_IsMasterDocument()");
      return PathUtils.IsSamePath(this.FullName, this.MasterFile);
    }
  }

  public virtual bool HasConfigurations
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.get_HasConfigurations()");
      if (!this.hasConfigurations.HasValue)
        this.hasConfigurations = new bool?(this.IsMasterDocument && this.DetectHasConfigurations());
      return this.hasConfigurations.Value;
    }
  }

  /// <summary>
  /// Реализует определение наличия у документа конфигураций. Они есть у моделей деталей и сборок.
  /// </summary>
  /// <returns>true, если у документа есть конфигурации, false - если конфигураций нет</returns>
  protected virtual bool DetectHasConfigurations()
  {
    CADDocumentType documentType = this.DocumentType;
    return documentType == CADDocumentType.Assembly || documentType == CADDocumentType.Part;
  }

  public virtual bool Modified
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument2.get_IsModified()");
      try
      {
        return ((ICADDocument2) this.RawObject).IsModified;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "ICADDocument2.get_IsModified()");
      }
    }
  }

  /// <summary>
  /// Возвращает конфигурацию по умолчанию, если она есть, или null.
  /// </summary>
  public virtual ModelConfigurationProxy DefaultConfiguration
  {
    get
    {
      if (this.HasConfigurations)
      {
        IModelConfiguration defaultConfiguration = this.GetDefaultConfiguration();
        if (defaultConfiguration != null)
          return this.CADSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) new ExplicitModelConfigurationProvider(defaultConfiguration), this, this.CADSystem, (IModelConfigurationCreationContext) CADDocumentConfigurationContext.Default);
      }
      return (ModelConfigurationProxy) null;
    }
  }

  protected virtual IModelConfiguration GetDefaultConfiguration()
  {
    if (this.hasDefaultConfiguration.HasValue)
    {
      if (!this.hasDefaultConfiguration.Value)
        return (IModelConfiguration) null;
      IModelConfiguration target = (IModelConfiguration) this.defaultConfigurationRef.Target;
      if (target != null)
        return target;
      this.hasDefaultConfiguration = new bool?();
      this.defaultConfigurationRef = (WeakReference) null;
    }
    IModelConfiguration defaultConfiguration = this.RawGetDefaultConfiguration();
    if (defaultConfiguration != null)
    {
      this.hasDefaultConfiguration = new bool?(true);
      this.defaultConfigurationRef = new WeakReference((object) defaultConfiguration);
      return defaultConfiguration;
    }
    this.hasDefaultConfiguration = new bool?(false);
    return (IModelConfiguration) null;
  }

  internal void ResetCachedDefaultConfiguration()
  {
    this.hasDefaultConfiguration = new bool?();
    this.defaultConfigurationRef = (WeakReference) null;
  }

  private IModelConfiguration RawGetDefaultConfiguration()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.GetDefaultConfiguration()");
    try
    {
      return this.RawObject.GetDefaultConfiguration();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetDefaultConfiguration()");
    }
  }

  public virtual void Activate()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument2.Activate()");
    try
    {
      ((ICADDocument2) this.RawObject).Activate();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument2.Activate()");
    }
  }

  public virtual void Reload()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.Reload()");
    try
    {
      this.RawObject.Reload(this.FullName);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.Reload()");
    }
  }

  public virtual void Save() => this.SaveCore(false);

  private void SaveCore(bool throwExceptions) => this.RawSave(throwExceptions);

  private void RawSave(bool throwExceptions)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.Save()");
    try
    {
      this.RawObject.Save();
    }
    catch (COMException ex)
    {
      if (throwExceptions)
        throw this.WrapExternalException(ex, "ICADDocument.Save()");
      if (!UIReport.Enabled)
        return;
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_270"), (object) ex.Message), TraceLevel.Error);
    }
  }

  public virtual void Export(string newFullName)
  {
    if (string.IsNullOrEmpty(newFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (newFullName));
    if (!Path.IsPathRooted(newFullName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_325"), nameof (newFullName));
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICADDocument.SaveAs()", newFullName);
    try
    {
      this.RawObject.SaveAs(newFullName, false);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.SaveAs()");
    }
    if (!File.Exists(newFullName))
      throw new ApplicationProxyException($"Экспорт документа в файл '{newFullName}' не поддерживается CAD-системой.");
  }

  public virtual void Close() => this.CloseCore(false);

  private void CloseCore(bool throwExceptions)
  {
    try
    {
      this.RawClose(throwExceptions);
    }
    finally
    {
      this.ResetPropertyCache();
      this.documentProvider = (ICADDocumentProvider) ClosedCADDocumentProvider.Default;
    }
  }

  private void RawClose(bool throwExceptions)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument.Close()");
    try
    {
      this.RawObject.Close();
    }
    catch (COMException ex)
    {
      if (throwExceptions)
        throw this.WrapExternalException(ex, "ICADDocument.Close()");
      if (!UIReport.Enabled)
        return;
      UIReport.ReportEvent(string.Format(LocalizationHolder.rm.GetString("Tools.Components_271"), (object) ex.Message), TraceLevel.Error);
    }
  }

  public virtual string MakePreview()
  {
    ICADDocument2 rawObject = (ICADDocument2) this.RawObject;
    string str = this.RawMakePreview(rawObject, EPreviewType.PT_DWF);
    if (string.IsNullOrEmpty(str))
      str = this.RawMakePreview(rawObject, EPreviewType.PT_Picture);
    return str;
  }

  private string RawMakePreview(ICADDocument2 rawDocument2, EPreviewType previewType)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument2.MakePreview()");
    try
    {
      return rawDocument2.MakePreview(EPreviewType.PT_DWF);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument2.MakePreview()");
    }
  }

  /// <summary>
  /// Возвращает COM-объект контейнера именованных значений. Метод используется для ленивого получения COM-объекта контейнера при
  /// первом обращении к нему.
  /// </summary>
  /// <returns>COM-объект контейнера</returns>
  IParametersContainer IParametersContainerProvider.GetContainer()
  {
    return (IParametersContainer) this.RawObject;
  }

  /// <summary>Получить список имён значений</summary>
  /// <returns>Список имён значений</returns>
  public virtual IList<string> GetParameterNames() => this.parametersProxy.GetParameterNames();

  /// <summary>Получить список именованных значений</summary>
  /// <returns>Список именованных значений</returns>
  public virtual List<ValueRecord> GetParameters() => this.parametersProxy.GetParameters();

  /// <summary>Получить список указанных именованных значений</summary>
  /// <param name="parameterNames">Имена значений</param>
  /// <returns>Список указанных именованных значений</returns>
  /// <exception cref="T:ArgumentNullException">parameterNames</exception>
  public virtual List<ValueRecord> GetParameters(IList<string> parameterNames)
  {
    return this.parametersProxy.GetParameters(parameterNames);
  }

  /// <summary>Внести в коллекцию указанные именованные значения</summary>
  /// <param name="parameters">Список именованных значений</param>
  /// <exception cref="T:ArgumentNullException">parameters</exception>
  public virtual void SetParameters(IList<ValueRecord> parameters)
  {
    this.parametersProxy.SetParameters(parameters);
  }

  /// <summary>Получить указанное именованное значение</summary>
  /// <param name="parameterName">Имя значения</param>
  /// <returns>Указанное именованное значение или null</returns>
  public virtual ValueRecord TryGetParameter(string parameterName)
  {
    return this.parametersProxy.TryGetParameter(parameterName);
  }

  /// <summary>Получить указанное именованное значение</summary>
  /// <param name="parameterName">Имя значения</param>
  /// <returns>Указанное именованное значение</returns>
  /// <exception cref="T:ArgumentProxyException">Не удалось найти указанный параметр</exception>
  public virtual ValueRecord GetParameter(string parameterName)
  {
    return this.parametersProxy.GetParameter(parameterName);
  }

  /// <summary>Внести в коллекцию указанное именованное значение</summary>
  /// <param name="parameter">Именованное значение</param>
  /// <exception cref="T:ArgumentNullException">parameter</exception>
  public virtual void SetParameter(ValueRecord parameter)
  {
    this.parametersProxy.SetParameter(parameter);
  }

  public virtual List<CADDocumentProxy> GetDependencies()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.GetDependencies()");
    return MasterDocumentsMapping.OpenMasterDocuments(this.CADSystem, (ICollection<string>) this.GetDependencyFiles(false).Item1);
  }

  public virtual Tuple<PathCollection, PathCollection> GetDependencyFiles(bool returnUnresolved)
  {
    string[] collection = this.RawGetAllDependencies(true) ?? new string[0];
    if (CADInterfaceTracing.General.TraceVerbose)
    {
      Trace.Indent();
      for (int index = 0; index < collection.Length; ++index)
        Trace.WriteLine(collection[index]);
      Trace.Unindent();
    }
    List<string> stringList = new List<string>((IEnumerable<string>) collection);
    if (stringList.Count != 0)
      this.FilterDependencyFiles(stringList);
    return this.ProcessRawDocumentFiles(stringList, returnUnresolved);
  }

  /// <summary>
  /// Позволяет отфильтровать список файловых зависимостей, полученных от CAD-системы
  /// </summary>
  /// <param name="dependencyFiles">Список путей к файлам зависимостей</param>
  protected virtual void FilterDependencyFiles(List<string> dependencyFiles)
  {
    this.RemoveSelfFileReferences(dependencyFiles);
  }

  private void RemoveSelfFileReferences(List<string> dependencyFiles)
  {
    dependencyFiles.RemoveAll((Predicate<string>) (x => PathUtils.IsSamePath(x, this.FullName)));
  }

  private string[] RawGetAllDependencies(bool topLevelOnly)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<bool>("ICADDocument2.GetAllDependencies()", topLevelOnly);
    try
    {
      return ((ICADDocument2) this.RawObject).GetAllDependencies(topLevelOnly);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument2.GetAllDependencies()");
    }
  }

  private Tuple<PathCollection, PathCollection> ProcessRawDocumentFiles(
    List<string> files,
    bool returnUnresolved)
  {
    PathCollection pathCollection = new PathCollection(files.Count);
    PathCollection unresolvedFiles = returnUnresolved ? new PathCollection(files.Count) : (PathCollection) null;
    string path1 = (string) null;
    for (int index = 0; index < files.Count; ++index)
    {
      if (!string.IsNullOrEmpty(files[index]))
      {
        if (files[index].IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
          CADDocumentProxy.CollectUnresolvedFile(unresolvedFiles, files[index]);
        }
        else
        {
          if (!Path.IsPathRooted(files[index]))
          {
            if (path1 == null)
            {
              string fullName = this.FullName;
              path1 = string.IsNullOrEmpty(fullName) || !Path.IsPathRooted(fullName) ? string.Empty : Path.GetDirectoryName(fullName);
            }
            if (!string.IsNullOrEmpty(path1))
            {
              string path = Path.Combine(path1, Path.GetFileName(files[index]));
              if (File.Exists(path))
              {
                if (CADInterfaceTracing.ExternalCallTracer.Enabled)
                  CADInterfaceTracing.ExternalCallTracer.AddToTrace($"File '{files[index]}' resolved to '{path}'.");
                files[index] = path;
              }
            }
          }
          if (!Path.IsPathRooted(files[index]))
            CADDocumentProxy.CollectUnresolvedFile(unresolvedFiles, files[index]);
          else if (!File.Exists(files[index]) && !this.CADSystem.IsOpenDocument(files[index]))
            CADDocumentProxy.CollectUnresolvedFile(unresolvedFiles, files[index]);
          else
            pathCollection.Add(files[index]);
        }
      }
    }
    return Tuple.Create<PathCollection, PathCollection>(pathCollection, unresolvedFiles);
  }

  public virtual Tuple<PathCollection, PathCollection> GetAssociativeFiles(bool returnUnresolved)
  {
    string[] collection = this.RawGetDependencies(true) ?? new string[0];
    if (CADInterfaceTracing.General.TraceVerbose)
    {
      Trace.Indent();
      for (int index = 0; index < collection.Length; ++index)
        Trace.WriteLine(collection[index]);
      Trace.Unindent();
    }
    return this.ProcessRawDocumentFiles(new List<string>((IEnumerable<string>) collection), returnUnresolved);
  }

  private string[] RawGetDependencies(bool topLevelOnly)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<bool>("ICADDocument.GetDependencies()", topLevelOnly);
    try
    {
      return this.RawObject.GetDependencies(topLevelOnly);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetDependencies()");
    }
  }

  private static void CollectUnresolvedFile(PathCollection unresolvedFiles, string file)
  {
    unresolvedFiles?.Add(file);
  }

  public virtual List<string> GetSatelliteFiles()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.GetSatelliteFiles()");
    List<string> satelliteFiles = new List<string>();
    if (this.HasConfigurations)
      this.CollectModelConfigurationFiles(satelliteFiles);
    return satelliteFiles;
  }

  private void CollectModelConfigurationFiles(List<string> satelliteFiles)
  {
    ICollection<ModelConfigurationProxy> allConfigurations = this.GetAllConfigurations();
    satelliteFiles.Capacity = satelliteFiles.Count + allConfigurations.Count;
    foreach (ModelConfigurationProxy configurationProxy in (IEnumerable<ModelConfigurationProxy>) allConfigurations)
    {
      string fullPath = configurationProxy.FullPath;
      if (!string.IsNullOrEmpty(fullPath))
        satelliteFiles.Add(fullPath);
    }
  }

  public virtual Tuple<PathCollection, PathCollection> GetMiscFiles(bool returnUnresolved)
  {
    return this.ProcessRawDocumentFiles(new List<string>((IEnumerable<string>) (this.RawGetMiscFiles() ?? new string[0])), returnUnresolved);
  }

  private string[] RawGetMiscFiles()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADDocument2.GetMiscellaneousReferences()");
    try
    {
      return ((ICADDocument2) this.RawObject).GetMiscellaneousReferences();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument2.GetMiscellaneousReferences()");
    }
  }

  /// <summary>
  /// Возвращает список всех файлов документа в родном формате CAD-системы (т.е.
  /// со всеми этими файлами можно работать через API CAD-системы)
  /// </summary>
  /// <returns>Список всех файлов документа в родном формате CAD-системы</returns>
  public virtual List<string> GetAllFiles()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.GetAllFiles()");
    List<string> dependencyFiles = new List<string>(128 /*0x80*/);
    dependencyFiles.AddRange((IEnumerable<string>) this.GetSatelliteFiles());
    string[] allDependencies = this.RawGetAllDependencies(true);
    if (allDependencies != null)
      dependencyFiles.AddRange((IEnumerable<string>) allDependencies);
    if (dependencyFiles.Count != 0)
      this.FilterDependencyFiles(dependencyFiles);
    dependencyFiles.Add(this.FullName);
    return dependencyFiles;
  }

  public virtual ModelConfigurationProxy TryGetConfiguration(string name)
  {
    try
    {
      return this.GetConfiguration(name, false);
    }
    catch (Exception ex)
    {
      switch (ex)
      {
        case ApplicationProxyException _:
        case COMException _:
          return (ModelConfigurationProxy) null;
        default:
          throw;
      }
    }
  }

  /// <summary>
  /// Возвращает конфигурацию документа по имени. Работает для любых конфигураций, а не только для верхнего уровня.
  /// </summary>
  /// <param name="name">Имя конфигурации документа</param>
  /// <param name="openVisible">Признак, что конфигурация должна быть открыта в видимом режиме</param>
  /// <returns>Объект конфигурации документа</returns>
  /// <remarks> Такое поведение метода противоречит комментарию к ICADDocument.GetConfiguration, но работает все именно так.</remarks>
  public virtual ModelConfigurationProxy GetConfiguration(string name, bool openVisible = false)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_277"), nameof (name));
    Tuple<IModelConfiguration, bool> configuration = this.RawGetConfiguration(this.CADSystem.Builder.ConfigurationNameMangler.ToRawName(this.FullName, name), openVisible);
    IModelConfiguration modelConfiguration = configuration.Item1;
    bool alreadyOpen = configuration.Item2;
    if (modelConfiguration == null)
      throw new ApplicationProxyException($"Не удалось получить конфигурацию '{name}' в документе '{this.FullName}'. Возможно, конфигурация с указанным именем не существует.");
    if (this.CADSystem.ApiResourceTracker != null && !openVisible)
      this.CADSystem.ApiResourceTracker.TrackOpenConfiguration(modelConfiguration, alreadyOpen);
    if (openVisible)
      this.ResetCachedDefaultConfiguration();
    return this.CADSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) new ExplicitModelConfigurationProvider(modelConfiguration), this, this.CADSystem, (IModelConfigurationCreationContext) CADDocumentConfigurationContext.Default);
  }

  private Tuple<IModelConfiguration, bool> RawGetConfiguration(
    string rawConfigurationName,
    bool openVisible)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<IModelConfiguration, string, bool>("ICADDocument.GetConfiguration()", (IModelConfiguration) null, rawConfigurationName, openVisible);
    try
    {
      IModelConfiguration ppConfiguration;
      bool configuration = this.RawObject.GetConfiguration((IModelConfiguration) null, rawConfigurationName, openVisible, out ppConfiguration);
      return Tuple.Create<IModelConfiguration, bool>(ppConfiguration, configuration);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetConfiguration()");
    }
  }

  public virtual List<ModelConfigurationProxy> GetConfigurations()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("CADDocumentProxy.GetConfigurations()");
    string[] strArray = (this.HasConfigurations ? this.RawGetConfigurationNames() : (string[]) null) ?? new string[0];
    List<ModelConfigurationProxy> configurations = new List<ModelConfigurationProxy>(strArray.Length);
    for (int index = 0; index < strArray.Length; ++index)
      configurations.Add(this.CADSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) new LazyModelConfigurationProvider(this.CADSystem.Builder.ConfigurationNameMangler.ToSafeName(this.FullName, strArray[index]), (IModelConfiguration) null, this), this, this.CADSystem, (IModelConfigurationCreationContext) CADDocumentConfigurationContext.Default));
    return configurations;
  }

  /// <summary>Получить список имен существующих конфигураций.</summary>
  /// <returns>Список имен существующих конфигураций</returns>
  public virtual List<string> GetConfigurationNames()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("CADDocumentProxy.GetConfigurationNames()");
    string[] collection = (this.HasConfigurations ? this.RawGetConfigurationNames() : (string[]) null) ?? new string[0];
    for (int index = 0; index < collection.Length; ++index)
      collection[index] = this.CADSystem.Builder.ConfigurationNameMangler.ToSafeName(this.FullName, collection[index]);
    return new List<string>((IEnumerable<string>) collection);
  }

  private string[] RawGetConfigurationNames()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<IModelConfiguration>("ICADDocument.GetConfigurationNames()", (IModelConfiguration) null);
    try
    {
      return this.RawObject.GetConfigurationNames((IModelConfiguration) null);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetConfigurationNames()");
    }
  }

  public virtual ICollection<ModelConfigurationProxy> GetAllConfigurations()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CADDocumentProxy.GetAllConfigurations()");
    LinkedList<ModelConfigurationProxy> result = new LinkedList<ModelConfigurationProxy>();
    this.WalkAllConfigurations((Action<ModelConfigurationProxy>) (cfg => result.AddLast(cfg)));
    return (ICollection<ModelConfigurationProxy>) result;
  }

  public virtual void WalkAllConfigurations(Action<ModelConfigurationProxy> method)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<Action<ModelConfigurationProxy>>("CADDocumentProxy.WalkAllConfigurations()", method);
    ModelConfigurationProxy defaultCfg = this.DefaultConfiguration;
    if (defaultCfg == null)
      return;
    method(defaultCfg);
    this.WalkAllConfigurations((IModelConfigurationsContainer) this, (Predicate<ModelConfigurationProxy>) (cfg => cfg.Name != defaultCfg.Name), method);
  }

  private void WalkAllConfigurations(
    IModelConfigurationsContainer provider,
    Predicate<ModelConfigurationProxy> filter,
    Action<ModelConfigurationProxy> method)
  {
    foreach (ModelConfigurationProxy configuration in provider.GetConfigurations())
    {
      if (filter == null || filter(configuration))
        method(configuration);
      this.WalkAllConfigurations((IModelConfigurationsContainer) configuration, filter, method);
    }
  }

  /// <summary>Записать кастомные данные</summary>
  /// <param name="blockName">Имя блока, максимальный размер 31 символ</param>
  /// <param name="data">Данные или null</param>
  public void WriteCustomData(string blockName, byte[] data)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, byte[]>("ICADDocument2.WriteCustomData()", blockName, data);
    CADDocumentProxy.CheckCustomDataBlockName(blockName);
    try
    {
      ((ICADDocument2) this.RawObject).WriteCustomData(blockName, data);
    }
    catch (InvalidCastException ex)
    {
      throw this.WrapExternalException((Exception) ex, "ICADDocument5.WriteCustomData()", (string) null);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument5.WriteCustomData()");
    }
  }

  /// <summary>Записать кастомные данные</summary>
  /// <param name="blockName">Имя блока, максимальный размер 31 символ</param>
  /// <returns>Данные или null</returns>
  public byte[] ReadCustomData(string blockName)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICADDocument2.ReadCustomData()", blockName);
    CADDocumentProxy.CheckCustomDataBlockName(blockName);
    try
    {
      return ((ICADDocument2) this.RawObject).ReadCustomData(blockName);
    }
    catch (ArgumentException ex)
    {
      return (byte[]) null;
    }
    catch (InvalidCastException ex)
    {
      throw this.WrapExternalException((Exception) ex, "ICADDocument2.ReadCustomData()", (string) null);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument2.ReadCustomData()");
    }
  }

  /// <summary>Удалить кастомные данные</summary>
  /// <param name="blockName">Имя блока, максимальный размер 31 символ</param>
  public void DeleteCustomData(string blockName)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICADDocument2.DeleteCustomData()", blockName);
    CADDocumentProxy.CheckCustomDataBlockName(blockName);
    try
    {
      ((ICADDocument2) this.RawObject).DeleteCustomData(blockName);
    }
    catch (InvalidCastException ex)
    {
      throw this.WrapExternalException((Exception) ex, "ICADDocument2.DeleteCustomData()", (string) null);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument2.DeleteCustomData()");
    }
  }

  private static void CheckCustomDataBlockName(string blockName)
  {
    if (blockName == null)
      throw new ArgumentNullException(nameof (blockName));
    if (blockName.Length > 31 /*0x1F*/)
      throw new ArgumentException("Имя блока не должно быть длинее 31 символа.", nameof (blockName));
  }

  /// <summary>Возвращает провайдер COM-объекта.</summary>
  public ICADDocumentProvider RawObjectProvider
  {
    [DebuggerStepThrough] get => this.documentProvider;
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект документа. Это свойство должно использоваться в тех случаях,
  /// когда объект документа CAD-системы требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public ICADDocument RawObject
  {
    [DebuggerStepThrough] get
    {
      if (this.cachedRawDocument == null)
        this.cachedRawDocument = this.documentProvider.Document;
      return this.cachedRawDocument;
    }
  }
}
