// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ModelConfigurationProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies.Cadmech;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools.Data;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>Конфигурация изделия из CAD-системы</summary>
public class ModelConfigurationProxy : 
  CADSystemComponentProxy,
  IModelConfigurationsContainer,
  IParametersContainerProxy,
  IParametersContainerProvider,
  IIMTextDocumentProvider
{
  private IModelConfigurationProvider configurationProvider;
  private CADDocumentProxy document;
  private IModelConfigurationCreationContext creationContext;
  private ParametersContainerProxy parametersProxy;
  private IModelConfiguration cachedRawModelConfiguration;
  private StringKey name;
  private string fullPath;
  private bool? isInMemory;

  /// <summary>Создать описание конфигурации изделия из CAD-системы</summary>
  /// <param name="configurationProvider">Провайдер конфигурации документа</param>
  /// <param name="document">Документ CAD-системы</param>
  /// <param name="cadSystem">CAD-система</param>
  /// <param name="creationContext">Контекст получения конфигурации документа</param>
  public ModelConfigurationProxy(
    IModelConfigurationProvider configurationProvider,
    CADDocumentProxy document,
    CADSystemProxy cadSystem,
    IModelConfigurationCreationContext creationContext)
    : base(cadSystem)
  {
    if (configurationProvider == null)
      throw new ArgumentNullException(nameof (configurationProvider));
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    if (creationContext == null)
      throw new ArgumentNullException(nameof (creationContext));
    this.configurationProvider = configurationProvider;
    this.document = document;
    this.creationContext = creationContext;
    this.parametersProxy = new ParametersContainerProxy((IParametersContainerProvider) this);
  }

  /// <summary>
  /// Возвращает контекст получения конфигурации документа.
  /// </summary>
  public IModelConfigurationCreationContext CreationContext
  {
    [DebuggerStepThrough] get => this.creationContext;
  }

  /// <summary>
  /// Возвращает документ CAD-системы, которому принадлежит конфигурация.
  /// </summary>
  public CADDocumentProxy Document
  {
    [DebuggerStepThrough] get => this.document;
  }

  /// <summary>
  /// Возвращает документ IMTEXT для текущей конфигурации документа CAD-системы.
  /// </summary>
  /// <param name="throwIfNoCadmechFound">Признак, что нужно бросать исключение, если CADMECH не установлен</param>
  /// <returns>Документ IMTEXT или null, если CADMECH не установлен</returns>
  /// <exception cref="T:System.ArgumentNullException">CADMECH не установлен и флаг throwIfNoCadmechFound = true</exception>
  public virtual IMTextDocumentProxy GetIMTextDocument(bool throwIfNoCadmechFound)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<bool>("ModelConfigurationProxy.GetIMTextDocument()", throwIfNoCadmechFound);
    if (string.IsNullOrEmpty(this.FullPath))
      throw new ApplicationProxyException($"Невозможно получить объект IMTEXT для конфигурации '{this.Name}' документа '{this.Document.Title}', так как конфигурация не сохранена в файл.");
    this.CADSystem.OpenDocument(this.FullPath, false).ForceLoad();
    return this.CADSystem.GetCadmechRoot(throwIfNoCadmechFound)?.GetDocument(this.FullPath);
  }

  /// <summary>
  /// Очищает кэш значений свойств конфигурации, который используется для оптимизации доступа к медленным, но редко изменяющимся свойствам.
  /// </summary>
  protected virtual void ResetPropertyCache()
  {
    this.cachedRawModelConfiguration = (IModelConfiguration) null;
    this.name = (StringKey) null;
    this.isInMemory = new bool?();
    this.fullPath = (string) null;
  }

  /// <summary>
  /// Возвращает true, если конфигурация и ее документ создаются CAD-системой "на лету" и не имеют файла.
  /// </summary>
  public bool IsInMemory
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("ModelConfigurationProxy.get_IsInMemory()");
      if (!this.isInMemory.HasValue)
        this.isInMemory = new bool?(this.DetectIsInMemory());
      return this.isInMemory.Value;
    }
  }

  /// <summary>
  /// Реализует определение случаев, когда конфигурация и ее документ создаются CAD-системой "на лету" и не имеют файла.
  /// </summary>
  protected virtual bool DetectIsInMemory() => false;

  /// <summary>Возвращает имя конфигурации.</summary>
  public virtual StringKey Name
  {
    get
    {
      if (this.name == (StringKey) null)
        this.name = (StringKey) this.CADSystem.Builder.ConfigurationNameMangler.ToSafeName(this.Document.FullName, (string) this.RawName);
      return this.name;
    }
  }

  /// <summary>
  /// Возвращает имя конфигурации, полученное непосредственно от CAD-интерфейса.
  /// </summary>
  public StringKey RawName
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.get_Name()");
      try
      {
        string name = this.RawObject.Name;
        if (CADInterfaceTracing.General.TraceVerbose)
          Trace.WriteLine($"IModelConfiguration.get_Name() returns '{name}'");
        return (StringKey) name;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelConfiguration.get_Name()");
      }
    }
  }

  /// <summary>Признак "только чтение"</summary>
  public virtual bool ReadOnly
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.get_ReadOnly()");
      try
      {
        return this.RawObject.ReadOnly;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelConfiguration.get_ReadOnly()");
      }
    }
  }

  /// <summary>
  /// Возвращает абсолютный путь к файлу конфигурации документа. Если такой файл не был сгенерирован или отсутствует на диске, то метод вернет пустую строку
  /// </summary>
  public string FullPath
  {
    get
    {
      if (this.fullPath == null)
      {
        this.fullPath = this.GetFullPath();
        if (CADInterfaceTracing.Proxies.TraceVerbose)
          Trace.WriteLine($"ModelConfigurationProxy.FullPath = {this.fullPath}");
      }
      return this.fullPath;
    }
  }

  protected virtual string GetFullPath()
  {
    string rawFullPath = this.RawFullPath;
    if (string.IsNullOrEmpty(rawFullPath) || !Path.IsPathRooted(rawFullPath))
      return string.Empty;
    string masterFile = this.Document.MasterFile;
    if (PathUtils.IsSamePath(rawFullPath, masterFile))
      return string.Empty;
    string firstPath = Path.GetExtension(rawFullPath);
    string secondPath = Path.GetExtension(masterFile);
    if (!PathUtils.IsSamePath(firstPath, secondPath))
      throw new ApplicationProxyException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_295"), (object) Path.GetFileName(masterFile), (object) firstPath, (object) secondPath));
    return !File.Exists(rawFullPath) ? string.Empty : rawFullPath;
  }

  public string RawFullPath
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.get_FullPath()");
      try
      {
        string fullPath = this.RawObject.FullPath;
        if (CADInterfaceTracing.General.TraceVerbose)
          Trace.WriteLine($"IModelConfiguration.get_FullPath() returns '{fullPath}'");
        return fullPath;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelConfiguration.get_FullPath()");
      }
    }
  }

  /// <summary>Была ли изменена конфигурация изделия</summary>
  public virtual bool Modified
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.get_IsModified()");
      try
      {
        return this.RawObject.IsModified;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelConfiguration.get_IsModified()");
      }
    }
  }

  /// <summary>
  /// Возвращает или задает массу конфигурации как физическое свойство.
  /// Значение свойства может быть null, если оно не поддерживается CAD-системой.
  /// </summary>
  public virtual MeasuredValue Mass
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.get_Mass()");
      try
      {
        IPhysicalQuantity mass = (IPhysicalQuantity) this.RawObject.Mass;
        return mass == null ? (MeasuredValue) null : this.CADSystem.PhysicalValues.ToMeasuredValue(mass);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelConfiguration.get_Mass()");
      }
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value), LocalizationHolder.rm.GetString("Tools.Components_300"));
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace<MeasuredValue>("IModelConfiguration.set_Mass()", value);
      try
      {
        this.RawObject.Mass = this.CADSystem.PhysicalValues.ToPhysicalQuantity(value);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelConfiguration.set_Mass()");
      }
    }
  }

  /// <summary>
  /// Преобразовать единицу измерения из типа EMainUnits в идентификатор соответствующей версии объекта Int64
  /// </summary>
  /// <param name="mainUnits">Текущее значение</param>
  /// <returns>Идентификатор соответствующей версии объекта Int64</returns>
  private long AppMainUnitToMeasure(EMainUnits mainUnits)
  {
    if (mainUnits == EMainUnits.UNIT_Undefined)
      mainUnits = EMainUnits.UNIT_Kilogram;
    if (mainUnits == EMainUnits.UNIT_Kilogram)
      return IDCache.Default.KilogramMeasure.Id;
    if (mainUnits == EMainUnits.UNIT_Pound)
      return IDCache.Default.PoundMeasure.Id;
    throw new NotSupportedEnumException((Enum) mainUnits, string.Format(LocalizationHolder.rm.GetString("Tools.Components_302"), (object) mainUnits));
  }

  /// <summary>
  /// Преобразовать идентификатор версии объекта единицы измерения в значение типа EMainUnits
  /// </summary>
  /// <param name="measureId">Идентификатор версии объекта единицы измерения</param>
  /// <returns>Значение типа EMainUnits</returns>
  private EMainUnits MeasureToAppMainUnits(long measureId)
  {
    if (measureId == IDCache.Default.KilogramMeasure.Id)
      return EMainUnits.UNIT_Kilogram;
    if (measureId == IDCache.Default.PoundMeasure.Id)
      return EMainUnits.UNIT_Pound;
    throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("Tools.Components_302"), (object) measureId));
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

  /// <summary>Получить описание конфигурации с указанным именем</summary>
  /// <param name="name">Имя конфигурации</param>
  /// <param name="openVisible">Признак, что конфигурация должна быть открыта в видимом режиме</param>
  /// <returns>Описание конфигурации с указанным именем</returns>
  public virtual ModelConfigurationProxy GetConfiguration(string name, bool openVisible = false)
  {
    if (string.IsNullOrEmpty(name))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_277"), nameof (name));
    Tuple<IModelConfiguration, bool> configuration = this.RawGetConfiguration(this.CADSystem.Builder.ConfigurationNameMangler.ToRawName(this.Document.FullName, name), openVisible);
    IModelConfiguration modelConfiguration = configuration.Item1;
    bool alreadyOpen = configuration.Item2;
    if (modelConfiguration == null)
      throw new ApplicationProxyException($"Не удалось получить конфигурацию '{name}' в документе '{this.Document.FullName}'. Возможно, конфигурация с указанным именем не существует.");
    if (this.CADSystem.ApiResourceTracker != null && !openVisible)
      this.CADSystem.ApiResourceTracker.TrackOpenConfiguration(modelConfiguration, alreadyOpen);
    if (openVisible)
      this.Document.ResetCachedDefaultConfiguration();
    return this.CADSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) new ExplicitModelConfigurationProvider(modelConfiguration), this.Document, this.CADSystem, (IModelConfigurationCreationContext) CADDocumentConfigurationContext.Default);
  }

  private Tuple<IModelConfiguration, bool> RawGetConfiguration(
    string rawConfigurationName,
    bool openVisible)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<IModelConfiguration, string, bool>("ICADDocument.GetConfiguration()", this.RawObject, rawConfigurationName, openVisible);
    try
    {
      IModelConfiguration ppConfiguration;
      bool configuration = this.Document.RawObject.GetConfiguration(this.RawObject, rawConfigurationName, openVisible, out ppConfiguration);
      return Tuple.Create<IModelConfiguration, bool>(ppConfiguration, configuration);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetConfiguration()");
    }
  }

  /// <summary>Получить список существующих конфигураций</summary>
  /// <returns>Список существующих конфигураций</returns>
  public virtual List<ModelConfigurationProxy> GetConfigurations()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ModelConfigurationProxy.GetConfigurations()");
    string[] strArray = this.RawGetConfigurationNames() ?? new string[0];
    List<ModelConfigurationProxy> configurations = new List<ModelConfigurationProxy>(strArray.Length);
    for (int index = 0; index < strArray.Length; ++index)
      configurations.Add(this.CADSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) new LazyModelConfigurationProvider(this.CADSystem.Builder.ConfigurationNameMangler.ToSafeName(this.Document.FullName, strArray[index]), this.RawObject, this.Document), this.Document, this.CADSystem, (IModelConfigurationCreationContext) CADDocumentConfigurationContext.Default));
    return configurations;
  }

  /// <summary>Получить список имен существующих конфигураций.</summary>
  /// <returns>Список имен существующих конфигураций</returns>
  public virtual List<string> GetConfigurationNames()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ModelConfigurationProxy.GetConfigurationNames()");
    string[] collection = this.RawGetConfigurationNames() ?? new string[0];
    for (int index = 0; index < collection.Length; ++index)
      collection[index] = this.CADSystem.Builder.ConfigurationNameMangler.ToSafeName(this.Document.FullName, collection[index]);
    return new List<string>((IEnumerable<string>) collection);
  }

  private string[] RawGetConfigurationNames()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<IModelConfiguration>("ICADDocument.GetConfigurationNames()", this.RawObject);
    try
    {
      string[] configurationNames = this.Document.RawObject.GetConfigurationNames(this.RawObject);
      if (configurationNames != null && configurationNames.Length == 1 && configurationNames[0] == this.RawObject.Name)
        configurationNames = (string[]) null;
      return configurationNames;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetConfigurationNames()");
    }
  }

  public virtual ModelComponentProxy AddComponent(ModelConfigurationProxy modelConfiguration)
  {
    if (modelConfiguration == null)
      throw new ArgumentNullException(nameof (modelConfiguration));
    return this.CADSystem.Builder.CreateModelComponent(this.RawObject.AddComponent(modelConfiguration.Document.MasterFile, this.CADSystem.Builder.ConfigurationNameMangler.ToRawName(modelConfiguration.Document.FullName, (string) modelConfiguration.Name)), this.CADSystem);
  }

  public virtual ModelComponentProxy GetComponent(string componentKey)
  {
    if (componentKey == null)
      throw new ArgumentNullException(nameof (componentKey));
    return this.CADSystem.Builder.CreateModelComponent(this.RawGetComponent(componentKey), this.CADSystem);
  }

  private IModelComponent RawGetComponent(string componentKey)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.GetComponent()");
    try
    {
      return this.RawObject.GetComponent(componentKey);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IModelConfiguration.GetComponent()");
    }
  }

  /// <summary>
  /// Возвращает состав конфигурации изделия на один уровень вниз.
  /// </summary>
  /// <returns>Список компонентов в составе конфигурации изделия</returns>
  public virtual List<ModelComponentProxy> GetStructure()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.GetStructure()");
    IModelComponent[] structure1;
    try
    {
      structure1 = this.RawObject.GetStructure();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IModelConfiguration.GetStructure()");
    }
    if (structure1 == null || structure1.Length == 0)
      return new List<ModelComponentProxy>(0);
    List<ModelComponentProxy> structure2 = new List<ModelComponentProxy>(structure1.Length);
    for (int index = 0; index < structure1.Length; ++index)
      structure2.Add(this.CADSystem.Builder.CreateModelComponent(structure1[index], this.CADSystem));
    return structure2;
  }

  /// <summary>Закрывает конфигурацию.</summary>
  public virtual void Close()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.Close()");
    try
    {
      this.RawObject.Close();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IModelConfiguration.Close()");
    }
  }

  /// <summary>Возвращает провайдер COM-объекта.</summary>
  public IModelConfigurationProvider RawObjectProvider
  {
    [DebuggerStepThrough] get => this.configurationProvider;
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект конфигурации документа. Это свойство должно использоваться в тех случаях,
  /// когда объект конфигурации документа CAD-системы требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IModelConfiguration RawObject
  {
    [DebuggerStepThrough] get
    {
      if (this.cachedRawModelConfiguration == null)
        this.cachedRawModelConfiguration = this.configurationProvider.RawConfiguration;
      return this.cachedRawModelConfiguration;
    }
  }
}
