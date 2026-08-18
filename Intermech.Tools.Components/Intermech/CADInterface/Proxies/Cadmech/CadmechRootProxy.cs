// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.Cadmech.CadmechRootProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Localization;
using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.Cadmech;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies.Cadmech;

/// <summary>
/// Реализует обертку для COM-объекта CADMECH (интерфейс ICadmech).
/// </summary>
public sealed class CadmechRootProxy : CadmechObjectProxy
{
  private ICadmech rawCadmechRoot;
  private static readonly Guid CadmechRootCLSID = new Guid("57DB5525-6800-4A27-90A4-AA9E4CD9B33C");

  /// <summary>Создает прокси-объект.</summary>
  /// <param name="rawObject">COM-объект</param>
  /// <exception cref="T:ArgumentNullException">Параметр <paramref name="rawObject" /> не должен быть равен null</exception>
  private CadmechRootProxy(ICadmech rawObject)
  {
    this.rawCadmechRoot = rawObject != null ? rawObject : throw new ArgumentNullException(nameof (rawObject));
  }

  public static CadmechRootProxy Create(bool throwIfNotFound)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<bool>("CadmechRootProxy.Create()", throwIfNotFound);
    ICadmech cadmechRoot = CadmechRootProxy.RawCreateCadmechRoot(throwIfNotFound);
    return cadmechRoot == null ? (CadmechRootProxy) null : new CadmechRootProxy(cadmechRoot);
  }

  private static ICadmech RawCreateCadmechRoot(bool throwIfNotFound)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<Guid, RegistrationClassContext>("ICadmech.ctor()", CadmechRootProxy.CadmechRootCLSID, RegistrationClassContext.LocalServer);
    try
    {
      return (ICadmech) ComActivator.CreateInstance(CadmechRootProxy.CadmechRootCLSID, RegistrationClassContext.LocalServer);
    }
    catch (COMException ex)
    {
      if (throwIfNotFound)
        throw new ApplicationProxyException("Не удалось создать COM-объект ICadmech. Возможно, CADMECH не установлен на компьютере.", (Exception) ex);
      return (ICadmech) null;
    }
  }

  public IMTextDocumentProxy GetDocument(string documentFilePath)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string>("CadmechRootProxy.GetDocument()", documentFilePath);
    return !string.IsNullOrEmpty(documentFilePath) ? new IMTextDocumentProxy(this.RawGetDocument(documentFilePath)) : throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_282"), nameof (documentFilePath));
  }

  private IMDoc_COM RawGetDocument(string documentFilePath)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace<string>("ICadmech.GetDocument()", documentFilePath);
    try
    {
      return this.rawCadmechRoot.GetDocument(documentFilePath) ?? throw new ApplicationProxyException($"Не удалось получить объект COM-объект IMDoc_COM для документа '{Path.GetFileName(documentFilePath)}'. Метод ICadmech.GetDocument() вернул null.");
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException(ex, "ICadmech.GetDocument()");
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект ICadmech. Это свойство должно использоваться в тех случаях,
  /// когда текущий объект требуется передать в CADMECH или связанные с ним приложения.
  /// Внутри IPS должен использоваться только proxy-объект.
  /// </summary>
  public ICadmech RawObject
  {
    [DebuggerStepThrough] get => this.rawCadmechRoot;
  }
}
