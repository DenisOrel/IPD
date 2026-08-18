// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.IMViewerFileConverterProxy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.IMViewer;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Прокси-класс для преобразования документов CAD-системы в файлы IMViewer.
/// </summary>
public class IMViewerFileConverterProxy : CADObjectProxy
{
  private static readonly Guid IMViewerFileConverterCLSID = new Guid("73E073BA-40A7-4A5E-9256-7D8B1F52778A");
  private CADSystemProxy cadSystem;
  private IIMViewerFileConverter2 rawObject;

  /// <summary>Создает объект.</summary>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <exception cref="T:Intermech.Runtime.ComInterop.Proxies.ApplicationProxyException">Ошибка создания COM-объекта преобразователя</exception>
  public IMViewerFileConverterProxy(CADSystemProxy cadSystem)
  {
    this.cadSystem = cadSystem != null ? cadSystem : throw new ArgumentNullException(nameof (cadSystem));
    this.rawObject = this.RawCreateIMViewerFileConverter();
  }

  /// <summary>Создает COM-объект преобразователя.</summary>
  /// <returns>COM-объект преобразователя</returns>
  /// <exception cref="T:Intermech.Runtime.ComInterop.Proxies.ApplicationProxyException">Ошибка создания COM-объекта преобразователя</exception>
  private IIMViewerFileConverter2 RawCreateIMViewerFileConverter()
  {
    try
    {
      return (IIMViewerFileConverter2) ComActivator.CreateInstance(IMViewerFileConverterProxy.IMViewerFileConverterCLSID, RegistrationClassContext.InProcessServer);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException((Exception) ex, "CreateIMViewerFileConverter()", "Убедитесь, что приложение IMViewer установлено и зарегистрировано в системе, а его битность соответствует битности IPS.");
    }
  }

  /// <summary>Преобразует документ CAD-системы в файл IMViewer.</summary>
  /// <param name="documentPath">Путь к файлу документа CAD-системы</param>
  /// <param name="outputDirectory">Путь к папке для файла IMViewer</param>
  /// <returns>Абсолютный путь файлу IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentPath" /> содержит некорректное значение; параметр <paramref name="outputDirectory" /> содержит некорректное значение</exception>
  public string CreateViewerFile(string documentPath, string outputDirectory)
  {
    if (CADInterfaceTracing.ProxyCallTracer.Enabled)
      CADInterfaceTracing.ProxyCallTracer.AddToTrace<string, string>("IMViewerFileConverterProxy.CreateViewerFile()", documentPath, outputDirectory);
    if (string.IsNullOrEmpty(documentPath))
      throw new ArgumentException("Не задан путь к файлу документа.", nameof (documentPath));
    if (string.IsNullOrEmpty(outputDirectory))
      throw new ArgumentException("Не задан путь к папке для IMV-файла.", nameof (outputDirectory));
    if (!Directory.Exists(outputDirectory))
      Directory.CreateDirectory(outputDirectory);
    string str = this.RawConvertNativeFile2(documentPath, outputDirectory);
    if (string.IsNullOrEmpty(str))
      throw new ApplicationProxyException("Имя результирующего IMV-файла не должно быть пусто.");
    if (!PathUtils.IsPlacedIn(str, outputDirectory))
      throw new ApplicationProxyException($"Результирующий IMV-файл '{str}' должен находиться в папке '{outputDirectory}'.");
    return File.Exists(str) ? str : throw new ApplicationProxyException($"Результирующий IMV-файл '{str}' не найден на диске.");
  }

  private string RawConvertNativeFile2(string documentPath, string outputDirectory)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("IIMViewerFileConverter2.ConvertNativeFile2()");
    string psViewerFilePath;
    try
    {
      this.rawObject.ConvertNativeFile2((object) this.cadSystem.RawObject, documentPath, outputDirectory, out psViewerFilePath);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalException((Exception) ex, "IIMViewerFileConverter2.ConvertNativeFile2()", this.RawGetLastError());
    }
    return psViewerFilePath;
  }

  private string RawGetLastError()
  {
    try
    {
      return this.rawObject.LastError;
    }
    catch
    {
      return (string) null;
    }
  }

  /// <summary>
  /// Возвращает "сырой" COM-объект конвертера. Это свойство должно использоваться в тех случаях,
  /// когда COM-объект требуется передать наружу в CADInterface, CADMECH или связанные с ними приложения.
  /// Внутри IPS должен использоваться только IMViewerFileConverterProxy.
  /// </summary>
  public IIMViewerFileConverter2 RawObject
  {
    [DebuggerStepThrough] get => this.rawObject;
  }
}
