
// Type: Intermech.Client.Core.Navigator.Classes.Providers.NavigatorVirtualColumnProviderExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Data;


namespace Intermech.Client.Core.Navigator.Classes.Providers;

/// <summary>
/// Класс для методов расширения поддержки провайдера, для базовых классов навигатора
/// </summary>
public static class NavigatorVirtualColumnProviderExtensions
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeItems"></param>
  /// <param name="column"></param>
  /// <returns></returns>
  private static object MapVirtualColumnToField(INodeItems nodeItems, NodeColumn column)
  {
    if (!(nodeItems is IContextAware contextAware))
      return (object) null;
    return ServiceUtils.GetService<INavigatorVirtualColumnProvider>((object) contextAware.Services, false)?.MapColumnToField(nodeItems, column);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodePart"></param>
  /// <param name="column"></param>
  /// <returns></returns>
  public static object MapVirtualColumnToField(this INodePart nodePart, NodeColumn column)
  {
    return NavigatorVirtualColumnProviderExtensions.MapVirtualColumnToField((INodeItems) nodePart, column);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="descriptor"></param>
  /// <param name="column"></param>
  /// <returns></returns>
  public static object MapVirtualColumnToField(this IDescriptor descriptor, NodeColumn column)
  {
    return NavigatorVirtualColumnProviderExtensions.MapVirtualColumnToField((INodeItems) descriptor, column);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dataTable"></param>
  /// <returns></returns>
  public static DataTable GetVirtualDataTable(
    this INodeQuery nodeQuery,
    NavigatorVirtualColumnProviderArgs args)
  {
    if (!(nodeQuery is IContextAware contextAware))
      return args?.SourceTable;
    INavigatorVirtualColumnProvider service = ServiceUtils.GetService<INavigatorVirtualColumnProvider>((object) contextAware.Services, false);
    if (service != null)
      return service.GetDataTable(nodeQuery, args);
    return args?.SourceTable;
  }
}
