// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ModelComponentProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class ModelComponentProxy : 
  CADSystemComponentProxy,
  IParametersContainerProvider,
  IParametersContainerProxy
{
  private IModelComponent rawObject;
  private ParametersContainerProxy parametersProxy;

  public ModelComponentProxy(IModelComponent rawModelComponent, CADSystemProxy cadSystem)
    : base(cadSystem)
  {
    this.rawObject = rawModelComponent != null ? rawModelComponent : throw new ArgumentNullException(nameof (rawModelComponent));
    this.parametersProxy = new ParametersContainerProxy((IParametersContainerProvider) this);
  }

  public virtual string FullName
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelComponent.get_FullName()");
      try
      {
        return this.RawObject.FullName;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelComponent.get_FullName()");
      }
    }
  }

  public virtual string Name
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelComponent.get_Name()");
      try
      {
        return this.RawObject.Name;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelComponent.get_Name()");
      }
    }
  }

  /// <summary>
  /// Уникальный ключ компонента в составе конфигурации изделия, в которую входит компонент.
  /// </summary>
  public virtual string Key
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelComponent.get_Key()");
      try
      {
        return this.RawObject.Key;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelComponent.get_Key()");
      }
    }
  }

  public virtual bool Selected
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelComponent.get_Selected()");
      try
      {
        return this.RawObject.Selected;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelComponent.get_Selected()");
      }
    }
  }

  public virtual bool Visible
  {
    get
    {
      if (CADInterfaceTracing.ExternalCallTracer.Enabled)
        CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelComponent.get_Visible()");
      try
      {
        return this.RawObject.Visible;
      }
      catch (COMException ex)
      {
        throw this.WrapExternalException(ex, "IModelComponent.get_Visible()");
      }
    }
  }

  public virtual ModelConfigurationProxy GetConfiguration()
  {
    ExplicitModelConfigurationProvider configurationProvider = new ExplicitModelConfigurationProvider(this.RawGetConfiguration());
    CADDocumentProxy document = this.CADSystem.Builder.CreateDocument((ICADDocumentProvider) new LinkedCADDocumentProvider((IModelConfigurationProvider) configurationProvider), this.CADSystem);
    return this.CADSystem.Builder.CreateModelConfiguration((IModelConfigurationProvider) configurationProvider, document, this.CADSystem, (IModelConfigurationCreationContext) ExternalModelConfigurationContext.Default);
  }

  private IModelConfiguration RawGetConfiguration()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelComponent.GetConfiguration()");
    try
    {
      return this.RawObject.GetConfiguration();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IModelComponent.GetConfiguration()");
    }
  }

  private ICADDocument RawGetDocument(IModelConfiguration rawModelConfiguration)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IModelConfiguration.GetCADDocument()");
    try
    {
      return rawModelConfiguration.GetCADDocument();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IModelConfiguration.GetCADDocument()");
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

  /// <summary>
  /// Возвращает "сырой" COM-объект компонента конфигурации. Это свойство должно использоваться в тех случаях,
  /// когда объект компонента конфигурации требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IModelComponent RawObject => this.rawObject;
}
