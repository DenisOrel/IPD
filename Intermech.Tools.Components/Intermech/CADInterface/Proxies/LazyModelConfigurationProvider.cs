// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.LazyModelConfigurationProvider
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.CADInterface;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public sealed class LazyModelConfigurationProvider : 
  CADInterfaceObjectProxy,
  IModelConfigurationProvider
{
  private string configurationName;
  private IModelConfiguration rawParentConfiguration;
  private CADDocumentProxy document;
  private IModelConfiguration cachedRawModelConfiguration;

  public LazyModelConfigurationProvider(
    string configurationName,
    IModelConfiguration rawParentConfiguration,
    CADDocumentProxy document)
  {
    if (string.IsNullOrEmpty(configurationName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_309"), nameof (configurationName));
    if (document == null)
      throw new ArgumentNullException(nameof (document), LocalizationHolder.rm.GetString("Tools.Components_292"));
    this.configurationName = configurationName;
    this.rawParentConfiguration = rawParentConfiguration;
    this.document = document;
  }

  /// <summary>
  /// Находит и возвращает COM-объект конфигурации документа CAD-системы. Поиск выполняется при первом обращении к свойству, результат поиска кэшируется.
  /// </summary>
  public IModelConfiguration RawConfiguration
  {
    [DebuggerStepThrough] get
    {
      if (this.cachedRawModelConfiguration == null)
        this.cachedRawModelConfiguration = this.FindModelConfiguration();
      return this.cachedRawModelConfiguration;
    }
  }

  private IModelConfiguration FindModelConfiguration()
  {
    Tuple<IModelConfiguration, bool> configuration = this.RawGetConfiguration(this.document.CADSystem.Builder.ConfigurationNameMangler.ToRawName(this.document.FullName, this.configurationName), false);
    IModelConfiguration modelConfiguration = configuration.Item1;
    bool alreadyOpen = configuration.Item2;
    if (modelConfiguration == null)
      throw new ApplicationProxyException($"Не удалось получить конфигурацию '{this.configurationName}' в документе '{this.document.FullName}'. Возможно, конфигурация с указанным именем не существует.");
    if (this.document.CADSystem.ApiResourceTracker != null)
      this.document.CADSystem.ApiResourceTracker.TrackOpenConfiguration(modelConfiguration, alreadyOpen);
    return modelConfiguration;
  }

  private Tuple<IModelConfiguration, bool> RawGetConfiguration(
    string rawConfigurationName,
    bool openVisible)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<IModelConfiguration, string, bool>("ICADDocument.GetConfiguration()", this.rawParentConfiguration, rawConfigurationName, openVisible);
    try
    {
      IModelConfiguration ppConfiguration;
      bool configuration = this.document.RawObject.GetConfiguration(this.rawParentConfiguration, rawConfigurationName, openVisible, out ppConfiguration);
      return Tuple.Create<IModelConfiguration, bool>(ppConfiguration, configuration);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICADDocument.GetConfiguration()");
    }
  }
}
