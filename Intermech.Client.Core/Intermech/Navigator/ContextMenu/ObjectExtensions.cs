
// Type: Intermech.Navigator.ContextMenu.ObjectExtensions
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Windows.Forms;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует расширение системы команд контекстного меню для
/// объектов базы данных.
/// </summary>
public sealed class ObjectExtensions
{
  public static readonly Guid CategoryGuid = new Guid("{115571D5-8DB9-4bb2-941F-151503A2706B}");
  /// <summary>ID категории версий объектов</summary>
  public static int CategoryID = -1;
  private static bool _initialized = false;

  /// <summary>Выполняет инициализацию расширения системы команд.</summary>
  public static void Start()
  {
    try
    {
      ObjectExtensions.CategoryID = Holder.GuidMapper.Register(ObjectExtensions.CategoryGuid);
      Holder.Factory.AddNodeType(ObjectExtensions.CategoryID, typeof (ObjectsListNode));
      ObjectExtensions._initialized = true;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, LocalizationHolder.rm.GetString("Client.Core_445"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      ObjectExtensions._initialized = false;
    }
    RelationExtensions.Start();
  }

  /// <summary>
  /// Завершает работу расширения и освобождает все используемые ресурсы
  /// </summary>
  public static void Stop() => RelationExtensions.Stop();

  /// <summary>
  /// Создает по списку идентификаторов версий объектов базы данных коллекцию
  /// элементов навигации.
  /// </summary>
  /// <param name="objectIDs">Массив идентификаторов версий объектов</param>
  /// <returns>Коллекция элементов навигации</returns>
  public static ISelectedItems GetItems(params long[] objectIDs)
  {
    return ObjectExtensions.GetItems(objectIDs, (System.IServiceProvider) null);
  }

  /// <summary>
  /// Создает по списку идентификаторов версий объектов базы данных коллекцию
  /// элементов навигации.
  /// </summary>
  /// <param name="objectIDs">Массив идентификаторов версий объектов</param>
  /// <param name="services">Дополнительные сервисы</param>
  /// <returns>Коллекция элементов навигации</returns>
  public static ISelectedItems GetItems(long[] objectIDs, System.IServiceProvider services)
  {
    if (objectIDs == null || objectIDs.Length == 0)
      throw new ArgumentNullException(sc_3805.ssp_imclient_3806(), LocalizationHolder.rm.GetString("Client.Core_446"));
    if (!ObjectExtensions._initialized)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3805.ssp_imclient_3807()));
    IDescriptor objectsDescriptor;
    if (objectIDs.Length == 1)
    {
      int objectTypeId = (ServicesManager.GetService(typeof (IObjectsInfoCache)) as IObjectsInfoCache).GetObjectInfo(objectIDs[0]).ObjectTypeID;
      objectsDescriptor = (IDescriptor) new ListDescriptor(ObjectExtensions.CategoryID, objectTypeId, string.Empty, (IList) objectIDs);
    }
    else
    {
      DescriptorCollection descriptors = new DescriptorCollection();
      for (int index = 0; index < objectIDs.Length; ++index)
        descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectIDs[index]));
      objectsDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategoryCustomNode, 0, LocalizationHolder.rm.GetString("Client.Core_1377"), descriptors);
    }
    return ObjectExtensions.GetItems(objectsDescriptor, services);
  }

  /// <summary>
  /// Создает по списку идентификаторов версий объектов базы данных коллекцию
  /// элементов навигации.
  /// </summary>
  /// <param name="objectsDescriptor">Дескриптор версий объектов</param>
  /// <param name="services">Дополнительные сервисы</param>
  /// <returns>Коллекция элементов навигации</returns>
  public static ISelectedItems GetItems(IDescriptor objectsDescriptor, System.IServiceProvider services)
  {
    if (objectsDescriptor == null)
      throw new ArgumentNullException(nameof (objectsDescriptor));
    if (!ObjectExtensions._initialized)
      throw new InvalidOperationException(LocalizationHolder.rm.GetString(sc_3805.ssp_imclient_3808()));
    NodeIDPath handlerPath = new NodeIDPath(objectsDescriptor);
    EtherealNode etherealNode = new EtherealNode(handlerPath.RootDescriptor);
    INodeQuery query1 = etherealNode.GetQuery(ContentType.Folders);
    query1.Execute((object) null, 1);
    handlerPath.Add(query1.GetRecordNodeID(0));
    INode child = etherealNode.GetChild(handlerPath[0]);
    if (child is IContextAware contextAware)
    {
      AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer(services);
      serviceContainer.AddService(typeof (ObjectsSelectionOptionsHolder), (object) new ObjectsSelectionOptionsHolder(ObjectsSelectionOptions.ShowAllModifications));
      contextAware.Services = (System.IServiceProvider) serviceContainer;
    }
    INodeQuery query2 = child.GetQuery(ContentType.Folders);
    query2.Execute((object) null, 2147483646);
    NodeIDCollection nodeIDs = new NodeIDCollection();
    for (int index = 0; index < query2.RecordCount; ++index)
      nodeIDs.Add(query2.GetRecordNodeID(index), index.ToString());
    return (ISelectedItems) new NodeItems(handlerPath, child, nodeIDs, services);
  }
}
