// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.ParametersContainerProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data;
using Intermech.Localization;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

public class ParametersContainerProxy : CADInterfaceObjectProxy, IParametersContainerProxy
{
  private IParametersContainerProvider containerProvider;
  private ParametersContainerConverter dataFormatConverter;
  private IParametersContainer rawContainer;

  public ParametersContainerProxy(IParametersContainerProvider containerProvider)
  {
    this.containerProvider = containerProvider != null ? containerProvider : throw new ArgumentNullException(nameof (containerProvider));
    this.dataFormatConverter = ParametersContainerConverter.Default;
  }

  /// <summary>Получить список имён значений</summary>
  /// <returns>Список имён значений</returns>
  public IList<string> GetParameterNames()
  {
    string[] parameterNames = this.RawGetParameterNames();
    if (parameterNames == null)
      return (IList<string>) new OrderedList<string>(0, (IComparer<string>) StringKey.Comparer);
    OrderedList<string> collection = new OrderedList<string>(parameterNames.Length, (IComparer<string>) StringKey.Comparer);
    if (parameterNames.Length != 0)
      collection.AddRange<string>((IEnumerable<string>) parameterNames);
    return (IList<string>) collection;
  }

  private string[] RawGetParameterNames()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IParametersContainer.GetParameterNames()");
    try
    {
      return this.RawObject.GetParameterNames(true);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IParametersContainer.GetParameterNames()");
    }
  }

  /// <summary>Получить список именованных значений</summary>
  /// <returns>Список именованных значений</returns>
  public List<ValueRecord> GetParameters() => this.GetParameters(this.GetParameterNames());

  /// <summary>Получить список указанных именованных значений</summary>
  /// <param name="parameterNames">Имена значений</param>
  /// <returns>Список указанных именованных значений</returns>
  /// <exception cref="T:ArgumentNullException">parameterNames</exception>
  public List<ValueRecord> GetParameters(IList<string> parameterNames)
  {
    if (parameterNames == null)
      throw new ArgumentNullException(nameof (parameterNames));
    if (parameterNames.Count == 0)
      return new List<ValueRecord>(0);
    string[] strArray = new string[parameterNames.Count];
    parameterNames.CopyTo(strArray, 0);
    Tuple<object[], short[]> parameters = this.RawGetParameters(strArray);
    object[] valuesArray = parameters.Item1;
    short[] readOnlyFlagsArray = parameters.Item2;
    if (valuesArray == null)
      valuesArray = new object[strArray.Length];
    return this.dataFormatConverter.ToParameters(strArray, valuesArray, readOnlyFlagsArray, false);
  }

  private Tuple<object[], short[]> RawGetParameters(string[] namesArray)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string[]>("IParametersContainer.GetParameters()", namesArray);
    try
    {
      object[] ppValues;
      short[] ppIsReadOnly;
      this.RawObject.GetParameters(namesArray, true, out ppValues, out ppIsReadOnly);
      return Tuple.Create<object[], short[]>(ppValues, ppIsReadOnly);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IParametersContainer.GetParameters()");
    }
  }

  /// <summary>Внести в коллекцию указанные именованные значения</summary>
  /// <param name="parameters">Список именованных значений</param>
  /// <exception cref="T:ArgumentNullException">parameters</exception>
  public void SetParameters(IList<ValueRecord> parameters)
  {
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    if (parameters.Count == 0)
      return;
    string[] namesArray;
    object[] valuesArray;
    this.dataFormatConverter.ToNamesAndValues((ICollection<ValueRecord>) parameters, out namesArray, out valuesArray);
    this.RawSetParameters(namesArray, valuesArray);
  }

  /// <summary>Получить указанное именованное значение</summary>
  /// <param name="parameterName">Имя значения</param>
  /// <returns>Указанное именованное значение или null</returns>
  public ValueRecord TryGetParameter(string parameterName)
  {
    string[] namesArray = !string.IsNullOrEmpty(parameterName) ? new string[1]
    {
      parameterName
    } : throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_312"), "paramName");
    Tuple<object[], short[]> parameters1 = this.RawGetParameters(namesArray);
    object[] valuesArray = parameters1.Item1;
    short[] readOnlyFlagsArray = parameters1.Item2;
    if (valuesArray == null)
      return (ValueRecord) null;
    if (valuesArray.Length != 0)
    {
      List<ValueRecord> parameters2 = this.dataFormatConverter.ToParameters(namesArray, valuesArray, readOnlyFlagsArray, true);
      if (parameters2.Count != 0)
        return parameters2[0];
    }
    return (ValueRecord) null;
  }

  /// <summary>Получить указанное именованное значение</summary>
  /// <param name="parameterName">Имя значения</param>
  /// <returns>Указанное именованное значение</returns>
  /// <exception cref="T:ArgumentProxyException">Не удалось найти указанный параметр</exception>
  public ValueRecord GetParameter(string parameterName)
  {
    return this.TryGetParameter(parameterName) ?? throw new ApplicationProxyException($"Не удалось получить значение параметра '{parameterName}', так как он отсутствует у объекта CAD-интерфейса.");
  }

  /// <summary>Внести в коллекцию указанное именованное значение</summary>
  /// <param name="parameter">Именованное значение</param>
  /// <exception cref="T:ArgumentNullException">parameter</exception>
  public void SetParameter(ValueRecord parameter)
  {
    if (parameter == null)
      throw new ArgumentNullException(nameof (parameter));
    string[] namesArray;
    object[] valuesArray;
    this.dataFormatConverter.ToNamesAndValues(parameter, out namesArray, out valuesArray);
    this.RawSetParameters(namesArray, valuesArray);
  }

  private void RawSetParameters(string[] namesArray, object[] valuesArray)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string[], object[]>("IParametersContainer.SetParameters()", namesArray, valuesArray);
    try
    {
      this.RawObject.SetParameters(namesArray, valuesArray, true);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IParametersContainer.SetParameters()");
    }
  }

  /// <summary>Возвращает провайдер COM-объекта.</summary>
  public IParametersContainerProvider RawObjectProvider
  {
    [DebuggerStepThrough] get => this.containerProvider;
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект контейнера именованных значений. Это свойство должно использоваться в тех случаях,
  /// когда объект компонента конфигурации требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IParametersContainer RawObject
  {
    [DebuggerStepThrough] get
    {
      if (this.rawContainer == null)
        this.rawContainer = this.RawObjectProvider.GetContainer();
      return this.rawContainer;
    }
  }
}
