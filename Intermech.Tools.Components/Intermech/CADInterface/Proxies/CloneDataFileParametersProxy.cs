// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneDataFileParametersProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Реализует обертку для COM-объекта контейнера параметров клонируемого документа CAD-системы (интерфейс ICloneDataFileParameters).
/// </summary>
public class CloneDataFileParametersProxy : CADSystemComponentProxy
{
  private CloneDataFileParameters rawObject;
  private ParametersContainerConverter dataFormatConverter;

  /// <summary>Создает объект</summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null</exception>
  public CloneDataFileParametersProxy(CADSystemProxy cadSystem)
    : base(cadSystem)
  {
    this.rawObject = (CloneDataFileParameters) new CloneDataFileParametersClass();
    this.dataFormatConverter = ParametersContainerConverter.Default;
  }

  /// <summary>
  /// Возвращает признак, указывающий, к чему относятся параметры - к документу или к конфигурации документа.
  /// </summary>
  public bool IsDocumentParameters
  {
    [DebuggerStepThrough] get => this.RawGetIsDocumentParameters();
  }

  private bool RawGetIsDocumentParameters()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFileParameters.get_IsDocumentParameters()");
    try
    {
      return this.rawObject.IsDocumentParameters;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneDataFileParameters.get_IsDocumentParameters()");
    }
  }

  /// <summary>
  /// Возвращает путь к мастер-файлу документа.
  /// Значение свойства должно соответствовать значению <see cref="P:Intermech.CADInterface.Proxies.CloneDataFileProxy.NewPath" />.
  /// </summary>
  public string MasterPath
  {
    [DebuggerStepThrough] get => this.RawGetMasterPath();
  }

  private string RawGetMasterPath()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFileParameters.get_MasterPath()");
    try
    {
      return this.rawObject.MasterPath;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneDataFileParameters.get_MasterPath()");
    }
  }

  /// <summary>
  /// Возвращает имя конфигурации документа, к которой относятся параметры.
  /// Значение свойства может быть равно null, если параметры относятся к самому документу.
  /// </summary>
  public string ConfigurationName
  {
    [DebuggerStepThrough] get
    {
      return this.CADSystem.Builder.ConfigurationNameMangler.ToSafeName(this.RawGetMasterPath(), this.RawGetConfigurationName());
    }
  }

  private string RawGetConfigurationName()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICloneDataFileParameters.get_ConfigurationName()");
    try
    {
      return this.rawObject.ConfigurationName;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneDataFileParameters.get_ConfigurationName()");
    }
  }

  /// <summary>Задает принадлежность параметров.</summary>
  /// <param name="originalPath">Исходный путь к мастер-файлу документа. Значение параметра должно соответствовать свойству <see cref="P:Intermech.CADInterface.Proxies.CloneDataFileProxy.OriginalPath" /></param>
  /// <param name="newPath">Новый путь к мастер-файлу документа. Значение параметра должно соответствовать свойству <see cref="P:Intermech.CADInterface.Proxies.CloneDataFileProxy.NewPath" /></param>
  /// <param name="configurationName">Имя конфигурации документа, к которой относятся параметры. Значение параметра может быть равно null, если это параметры документа, а не конфигурации</param>
  public void SetDestination(string originalPath, string newPath, string configurationName = null)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string, string, string>("ICloneDataFileParameters.SetDestination()", originalPath, newPath, configurationName);
    if (string.IsNullOrEmpty(originalPath))
      throw new ArgumentException("Не задан путь к мастер-файлу документа, в который записываются параметры", nameof (originalPath));
    if (string.IsNullOrEmpty(newPath))
      throw new ArgumentException("Не задан путь к мастер-файлу документа, в который записываются параметры", nameof (newPath));
    string rawName = configurationName != null ? this.CADSystem.Builder.ConfigurationNameMangler.ToRawName(originalPath, configurationName) : (string) null;
    try
    {
      this.rawObject.SetDestination(newPath, rawName);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneDataFileParameters.SetDestination()");
    }
  }

  /// <summary>Записывает значения параметров в контейнер.</summary>
  /// <param name="parameters">Список значений параметров</param>
  public void AddOrUpdateParameters(ICollection<ValueRecord> parameters)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<ICollection<ValueRecord>>("CloneDataFileParametersProxy.AddOrUpdateParameters()", parameters);
    if (parameters == null)
      throw new ArgumentNullException(nameof (parameters));
    if (parameters.Count == 0)
      return;
    string[] namesArray;
    object[] valuesArray;
    this.dataFormatConverter.ToNamesAndValues(parameters, out namesArray, out valuesArray);
    this.RawSetParameters(namesArray, valuesArray);
  }

  private void RawSetParameters(string[] namesArray, object[] valuesArray)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string[], object[]>("ICloneDataFileParameters.SetParameters()", namesArray, valuesArray);
    try
    {
      ((ICloneDataFileParameters) this.rawObject).SetParameters(namesArray, valuesArray, true);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneDataFileParameters.SetParameters()");
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект-обертку требуется передать наружу во внешнее приложение
  /// через COM-интерфейс. Внутри IPS должен использоваться только текущий объект-обертка.
  /// </summary>
  public CloneDataFileParameters RawObject
  {
    [DebuggerStepThrough] get => this.rawObject;
  }
}
