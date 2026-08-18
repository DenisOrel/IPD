
// Type: Intermech.Navigator.ContextMenu.RelationExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует расширение системы команд контекстного меню для
/// связей базы данных.
/// </summary>
public sealed class RelationExtensions
{
  public static readonly Guid CategoryGuid = new Guid("{76ACD271-D526-40CB-AD38-5478FC00DFEE}");
  /// <summary>ID категории связей</summary>
  public static int CategoryID = -1;
  private static bool _initialized = false;

  /// <summary>Выполняет инициализацию расширения системы команд.</summary>
  public static void Start()
  {
    try
    {
      RelationExtensions.CategoryID = Holder.GuidMapper.Register(RelationExtensions.CategoryGuid);
      Holder.Factory.AddNodeType(RelationExtensions.CategoryID, typeof (RelationsListNode));
      RelationExtensions._initialized = true;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_445"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      RelationExtensions._initialized = false;
    }
  }

  /// <summary>
  /// Завершает работу расширения и освобождает все используемые ресурсы
  /// </summary>
  public static void Stop()
  {
  }

  /// <summary>
  /// Создает по списку идентификаторов связей базы данных коллекцию
  /// элементов навигации.
  /// </summary>
  /// <param name="objIDs">[(Int64)ID версии родительского объекта] =&gt; [(List(64))Список связей]</param>
  /// <returns>Коллекция элементов навигации</returns>
  public static ISelectedItems GetItems(Dictionary<long, List<long>> objIDs)
  {
    return RelationExtensions.GetItems(objIDs, (System.IServiceProvider) null);
  }

  /// <summary>
  /// Создает по списку идентификаторов связей базы данных коллекцию
  /// элементов навигации.
  /// </summary>
  /// <param name="objIDs">[(Int64)ID версии родительского объекта] =&gt; [(List(64))Список связей]</param>
  /// <param name="services">Дополнительные сервисы</param>
  /// <returns>Коллекция элементов навигации</returns>
  public static ISelectedItems GetItems(
    Dictionary<long, List<long>> objIDs,
    System.IServiceProvider services)
  {
    if (objIDs == null || objIDs.Count == 0)
      throw new ArgumentNullException(sc_3805.ssp_imclient_3809(), LocalizationHolder.rm.GetString("Client.Core_448"));
    if (!RelationExtensions._initialized)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3805.ssp_imclient_3810()));
    Dictionary<INodeID, NodeIDPath> handlerPaths = new Dictionary<INodeID, NodeIDPath>();
    Dictionary<INodeID, INode> handlers = new Dictionary<INodeID, INode>();
    NodeIDCollection nodeIDs = new NodeIDCollection();
    List<long> longList = new List<long>((IEnumerable<long>) objIDs.Keys);
    for (int index1 = 0; index1 < longList.Count; ++index1)
    {
      NodeIDPath handlerPath = new NodeIDPath((IDescriptor) new RelationsDescriptor(longList[index1], (IList) objIDs[longList[index1]], true));
      INode handler = (INode) new EtherealNode(handlerPath.RootDescriptor);
      if (((Descriptor) handlerPath.RootDescriptor).InvalidDescriptor)
        return (ISelectedItems) new NodeItems(handlerPath, handler, new NodeIDCollection(), services);
      INodeQuery query1 = handler.GetQuery(ContentType.Folders);
      query1.Execute((object) null, 1);
      handlerPath.Add(query1.GetRecordNodeID(0));
      INode child = handler.GetChild(handlerPath[0]);
      if (child is IContextAware contextAware)
        contextAware.Services = services;
      INodeQuery query2 = child.GetQuery(ContentType.Folders);
      query2.Execute((object) null, objIDs[longList[index1]].Count);
      for (int index2 = 0; index2 < query2.RecordCount; ++index2)
      {
        INodeID recordNodeId = query2.GetRecordNodeID(index2);
        nodeIDs.Add(recordNodeId, index2.ToString());
        handlerPaths[recordNodeId] = handlerPath;
        handlers[recordNodeId] = child;
      }
    }
    return (ISelectedItems) new CompositeNodeItems(handlerPaths, handlers, nodeIDs, services, objIDs.Count > 1);
  }
}
