
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.ViewerProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost;

/// <summary>
/// Провайдер просмотровщиков для конкретного контрола (закладки)
/// </summary>
internal class ViewerProvider
{
  private IDictionary<string, IViewer> _viewersCache;
  private IExtensionsService _extensionsService;
  private string[] _openProperties;
  private string[] _openMethods;

  public ViewerProvider()
  {
    this._viewersCache = (IDictionary<string, IViewer>) new Dictionary<string, IViewer>();
  }

  public void InitializeServices()
  {
    this._extensionsService = ServiceUtils.GetService<IExtensionsService>((object) ServicesManager.ServiceContainer, true);
    string methods = this._extensionsService.Methods;
    string properties = this._extensionsService.Properties;
    string[] strArray1;
    if (properties == null)
      strArray1 = (string[]) null;
    else
      strArray1 = properties.Replace(" ", "").Split(';', ',', '|');
    this._openProperties = strArray1;
    string[] strArray2;
    if (methods == null)
      strArray2 = (string[]) null;
    else
      strArray2 = methods.Replace(" ", "").Split(';', ',', '|');
    this._openMethods = strArray2;
  }

  /// <summary>Пробуем найти подходящий просмотрщик в кэше</summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  public IViewer TryFindViewer(string @extension)
  {
    IViewer viewer;
    return !this._viewersCache.TryGetValue(@extension, out viewer) ? (IViewer) null : viewer;
  }

  /// <summary>
  /// Добавить в кэш вьювер, которым открылся файл с указанным расширением
  /// </summary>
  /// <param name="extension"></param>
  /// <param name="viewer"></param>
  public void AddView(string @extension, IViewer viewer) => this._viewersCache[@extension] = viewer;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="extension"></param>
  /// <returns></returns>
  public IViewer GetViewer(FileExtensionsInfo extensionInfo)
  {
    IViewer viewer = ViewerFactory.Instance.Create(extensionInfo.Style);
    this.InitializeSupportInterfaces(viewer, extensionInfo);
    return viewer;
  }

  /// <summary>Очистка всех просмотрщиков из кэша</summary>
  public void ClearViewersCache()
  {
    foreach (IViewer viewer in (IEnumerable<IViewer>) this._viewersCache.Values)
    {
      viewer.Clear();
      if (viewer is IDisposable disposable)
        disposable.Dispose();
    }
  }

  /// <summary>Инициализация поддверживаемых интерфейсов</summary>
  /// <param name="viewer"></param>
  /// <param name="fileExt"></param>
  private void InitializeSupportInterfaces(IViewer viewer, FileExtensionsInfo fileExt)
  {
    if (viewer is IClsidSupport clsidSupport)
      clsidSupport.SetClsid(fileExt.ID);
    if (viewer is IProgidSupport progidSupport)
      progidSupport.SetProdid(fileExt.ProgId);
    if (viewer is IOpenMethodsAndPropsSupport methodsAndPropsSupport)
    {
      methodsAndPropsSupport.SetOpenMethods(this._openMethods);
      methodsAndPropsSupport.SetOpenProps(this._openProperties);
    }
    if (viewer is IShellCommandLineSupport commandLineSupport1)
      commandLineSupport1.SetCommandLine(fileExt.ShellCommandLine);
    if (!(viewer is ICommandLineSupport commandLineSupport2))
      return;
    commandLineSupport2.SetCommandLine(fileExt.CommandLine);
  }
}
