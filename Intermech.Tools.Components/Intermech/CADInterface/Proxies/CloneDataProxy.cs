// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CloneDataProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using Interop.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Реализует обертку для COM-объекта задания клонирования файлов (интерфейс ICloneData).
/// </summary>
public class CloneDataProxy : CADSystemComponentProxy
{
  private List<CloneDataFileProxy> files;
  private List<CloneDataFileParametersProxy> fileParameters;
  private CloneProgressSink progressSink;
  private CloneData rawObject;
  private bool isRebuildRequired;

  /// <summary>Создает объект.</summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null</exception>
  public CloneDataProxy(CADSystemProxy cadSystem)
    : base(cadSystem)
  {
    this.files = new List<CloneDataFileProxy>();
    this.fileParameters = new List<CloneDataFileParametersProxy>();
    this.rawObject = (CloneData) new CloneDataClass();
    this.isRebuildRequired = true;
  }

  /// <summary>
  /// Возвращает признак, что в задании есть файлы для клонирования.
  /// </summary>
  public bool HasFiles
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CloneDataProxy.get_HasFiles()");
      return this.files.Count != 0;
    }
  }

  /// <summary>Создает описатель файла документа.</summary>
  /// <returns>Описатель файла документа</returns>
  public CloneDataFileProxy CreateFile()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CloneDataProxy.CreateFile()");
    return new CloneDataFileProxy(this.CADSystem);
  }

  /// <summary>Добавляет в текущее задание файл документа.</summary>
  /// <param name="file">Описатель файла документа</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="file" /> содержит null</exception>
  public void AddFile(CloneDataFileProxy file)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CloneDataFileProxy>("CloneDataProxy.AddFile()", file);
    if (file == null)
      throw new ArgumentNullException(nameof (file));
    this.files.Add(file);
    this.SetRebuildRequired();
  }

  /// <summary>Возвращает список файлов, уже добавленных в задание.</summary>
  /// <returns>Список описателей файлов документов</returns>
  public List<CloneDataFileProxy> GetFiles()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CloneDataProxy.GetFiles()");
    return new List<CloneDataFileProxy>((IEnumerable<CloneDataFileProxy>) this.files);
  }

  /// <summary>
  /// Создает контейнер для параметров документа или конфигурации документа.
  /// </summary>
  /// <returns>Контейнер для параметров документа или конфигурации документа</returns>
  public CloneDataFileParametersProxy CreateFileParameters()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CloneDataProxy.CreateFileParameters()");
    return new CloneDataFileParametersProxy(this.CADSystem);
  }

  /// <summary>
  /// Добавляет в текущее задание контейнер с параметрами документа или конфигурации документа.
  /// </summary>
  /// <param name="fileParameters">Контейнер с параметрами документа или конфигурации документа</param>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="fileParameters" /> содержит null</exception>
  public void AddFileParameters(CloneDataFileParametersProxy fileParameters)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<CloneDataFileParametersProxy>("CloneDataProxy.AddFileParameters()", fileParameters);
    if (fileParameters == null)
      throw new ArgumentNullException(nameof (fileParameters));
    this.fileParameters.Add(fileParameters);
    this.SetRebuildRequired();
  }

  /// <summary>
  /// Возвращает список контейнеров с параметрами документов, уже добавленных в задание.
  /// </summary>
  /// <returns>Список контейнеров с параметрами документов</returns>
  public List<CloneDataFileParametersProxy> GetFileParameters()
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace("CloneDataProxy.GetFileParameters()");
    return new List<CloneDataFileParametersProxy>((IEnumerable<CloneDataFileParametersProxy>) this.fileParameters);
  }

  /// <summary>
  /// Возвращает или задает callback-объект PDM-системы.
  /// Он используется PDM-системой для отображения хода выполнения операции клонирования,
  /// прерывания операции клонирования, реагирования на ошибки в процессе клонирования.
  /// </summary>
  public CloneProgressSink ProgressSink
  {
    get
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace("CloneDataProxy.get_ProgressSink()");
      return this.progressSink;
    }
    set
    {
      if (CADInterfaceTracing.ProxyCallTracer.Enabled)
        CADInterfaceTracing.ProxyCallTracer.AddToTrace<CloneProgressSink>("CloneDataProxy.set_ProgressSink()", value);
      this.progressSink = value;
      this.SetRebuildRequired();
    }
  }

  private void SetRebuildRequired()
  {
    if (this.isRebuildRequired)
      return;
    this.isRebuildRequired = true;
  }

  private void LazyRebuildRawObject()
  {
    if (!this.isRebuildRequired)
      return;
    this.RebuildRawObject();
    this.isRebuildRequired = false;
  }

  private void RebuildRawObject()
  {
    CloneDataFile[] filesArray = this.files.Count != 0 ? this.BuildFileArray(this.files) : (CloneDataFile[]) null;
    CloneDataFileParameters[] fileParametersArray = this.fileParameters.Count != 0 ? this.BuildFileParametersArray(this.fileParameters) : new CloneDataFileParameters[0];
    CloneProgressSinkAdapter newProgressSink = new CloneProgressSinkAdapter(this, this.progressSink);
    this.RawSetFiles(filesArray);
    this.RawSetFileParameters(fileParametersArray);
    this.RawSetProgressSink((ICloneProgressSink) newProgressSink);
  }

  private CloneDataFile[] BuildFileArray(List<CloneDataFileProxy> filesList)
  {
    CloneDataFile[] cloneDataFileArray = new CloneDataFile[filesList.Count];
    for (int index = 0; index < filesList.Count; ++index)
      cloneDataFileArray[index] = filesList[index].RawObject;
    return cloneDataFileArray;
  }

  private CloneDataFileParameters[] BuildFileParametersArray(
    List<CloneDataFileParametersProxy> fileParametersList)
  {
    CloneDataFileParameters[] dataFileParametersArray = new CloneDataFileParameters[fileParametersList.Count];
    for (int index = 0; index < fileParametersList.Count; ++index)
      dataFileParametersArray[index] = fileParametersList[index].RawObject;
    return dataFileParametersArray;
  }

  private void RawSetFiles(CloneDataFile[] filesArray)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<CloneDataFile[]>("ICloneData.set_Files()", filesArray);
    try
    {
      this.rawObject.Files = filesArray;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneData.set_Files()");
    }
  }

  private void RawSetFileParameters(CloneDataFileParameters[] fileParametersArray)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<CloneDataFileParameters[]>("ICloneData.set_FileParameters()", fileParametersArray);
    try
    {
      this.rawObject.FileParameters = fileParametersArray;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneData.set_FileParameters()");
    }
  }

  private void RawSetProgressSink(ICloneProgressSink newProgressSink)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<ICloneProgressSink>("ICloneData.set_ProgressSink()", newProgressSink);
    try
    {
      this.rawObject.ProgressSink = newProgressSink;
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICloneData.set_ProgressSink()");
    }
  }

  /// <summary>Находит файл документа по исходному пути к файлу.</summary>
  /// <param name="originalPath">Исходный путь к файлу</param>
  /// <returns>Описатель файла или null</returns>
  /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="originalPath" /> содержит null</exception>
  public CloneDataFileProxy FindFileByOriginalPath(string originalPath)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("CloneDataProxy.FindFileByOriginalPath()", originalPath);
    if (originalPath == null)
      throw new ArgumentNullException(nameof (originalPath));
    return this.files.Find((Predicate<CloneDataFileProxy>) (x => PathUtils.IsSamePath(x.OriginalPath, originalPath)));
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект-обертку требуется передать наружу во внешнее приложение
  /// через COM-интерфейс. Внутри IPS должен использоваться только текущий объект-обертка.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public CloneData RawObject
  {
    [DebuggerStepThrough] get
    {
      this.LazyRebuildRawObject();
      return this.rawObject;
    }
  }

  internal CloneDataFileProxy TryMapToProxy(CloneDataFile rawObject)
  {
    if (rawObject == null)
      throw new ArgumentNullException(nameof (rawObject));
    return this.files.Find((Predicate<CloneDataFileProxy>) (x => x.RawObject == rawObject));
  }

  internal CloneDataFileParametersProxy TryMapToProxy(CloneDataFileParameters rawObject)
  {
    if (rawObject == null)
      throw new ArgumentNullException(nameof (rawObject));
    return this.fileParameters.Find((Predicate<CloneDataFileParametersProxy>) (x => x.RawObject == rawObject));
  }
}
