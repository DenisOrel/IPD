// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.IMTextDocumentProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Runtime.ComInterop.Proxies;
using Interop.Cadmech;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies.Cadmech;

/// <summary>
/// Реализует обертку для COM-объекта документа IMTEXT (интерфейс IMDoc_COM).
/// </summary>
public sealed class IMTextDocumentProxy : CadmechObjectProxy
{
  private IMDoc_COM rawDocument;

  /// <summary>Создает прокси-объект.</summary>
  /// <param name="rawObject">COM-объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="rawObject" /> не должен быть равен null</exception>
  internal IMTextDocumentProxy(IMDoc_COM rawObject)
  {
    this.rawDocument = rawObject != null ? rawObject : throw new ArgumentNullException(nameof (rawObject));
  }

  public void Activate()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMDoc_COM.Activate()");
    try
    {
      this.rawDocument.Activate();
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMDoc_COM.Activate()");
    }
  }

  /// <summary>Получение менеджера атрибутов</summary>
  /// <returns></returns>
  public IMTextAttributeManagerProxy GetAttrManager()
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IMDoc_COM.GetAttrManager()");
    try
    {
      IMAttrManager_COM attrManager = this.rawDocument.GetAttrManager();
      return attrManager == null ? (IMTextAttributeManagerProxy) null : new IMTextAttributeManagerProxy(attrManager);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IMDoc_COM.GetAttrManager()");
    }
  }

  [Obsolete("Do not use!", true)]
  public string CreateViewerFile(string folderPath)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("IMTextDocumentProxy.CreateViewerFile()", folderPath);
    string path2 = this.RawSaveViewerFile(folderPath);
    string path = !string.IsNullOrEmpty(path2) ? Path.GetFullPath(Path.Combine(folderPath, path2)) : throw new ApplicationProxyException("Имя результирующего IMV-файла не должно быть пусто.");
    return File.Exists(path) ? path : throw new ApplicationProxyException($"Результирующий IMV-файл не найден на диске по пути '{path}'.");
  }

  [Obsolete("Do not use!", true)]
  private string RawSaveViewerFile(string folderPath)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IIMDoc3.SaveViewerFile()");
    if (string.IsNullOrEmpty(folderPath))
      throw new ArgumentException("Не задан путь к папке для IMV-файла.", nameof (folderPath));
    string psViewerFileName;
    try
    {
      ((IIMDoc3) this.rawDocument).SaveViewerFile(folderPath, false, false, out psViewerFileName);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "IIMDoc3.SaveViewerFile()");
    }
    return psViewerFileName;
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект документа IMTEXT. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public IMDoc_COM RawObject
  {
    [DebuggerStepThrough] get => this.rawDocument;
  }
}
