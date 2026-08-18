
// Type: Intermech.Client.Core.Organizer.OrganizerRootNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>Узел "Органайзер".</summary>
public class OrganizerRootNode : 
  CompositeNode,
  IContextAware,
  IOrganizerNode,
  IOrganizerConditionNode
{
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();
  private ConditionStructure[] _conditions;

  /// <summary>Конструктор.</summary>
  public OrganizerRootNode()
  {
    this._services.AddService(typeof (IOrganizerNode), (object) new OrganizerNode());
  }

  /// <summary>Контейнер сервисов.</summary>
  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditions"></param>
  public void SetCondition(ConditionStructure[] conditions)
  {
    this._conditions = conditions;
    this.nonFolderSlots = (List<PartSlot>) null;
  }

  /// <summary>Формирование слотов-папок.</summary>
  /// <returns>Коллекция слотов-папок</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection descriptors = ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service ? service.Descriptors : new DescriptorCollection();
    descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545")));
    return new List<PartSlot>(1)
    {
      new PartSlot(new Guid("cad015bc-306c-11d8-b4e9-00304f19f545"), (INodePart) new DescriptorsPart(descriptors))
    };
  }

  /// <summary>Формирование слотов-непапок.</summary>
  /// <returns>Коллекция слотов-непапок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    List<PartSlot> nonFolderSlots = new List<PartSlot>(1);
    if (ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service)
    {
      DescriptorCollection descriptors = service.Descriptors;
      for (int index = 0; index < descriptors.Count; ++index)
      {
        if (descriptors[index] is OrganizerChildNodeDescriptor childNodeDescriptor && !(childNodeDescriptor.Guid == Guid.Empty))
        {
          INodePart part = childNodeDescriptor.GetPart(this.Services);
          nonFolderSlots.Add(new PartSlot(childNodeDescriptor.Guid, part));
        }
      }
    }
    nonFolderSlots.AddRange((IEnumerable<PartSlot>) OrganizerTaskNode.GetNonFolderSlots(this.Services, this._conditions));
    return nonFolderSlots;
  }

  /// <summary>Формирование дочерних узлов.</summary>
  /// <param name="nodeID">Данные создаваемого узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    if (nodeID == null)
      return (INode) null;
    return nodeID.TypeID == MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545") ? (INode) new OrganizerTaskNode(nodeID.TypeID, AccessRights.Enabled) : (INode) new OrganizerChildNode(nodeID.CategoryID, nodeID.TypeID);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return dataFormat == typeof (IOrganizerNode) ? (object) new OrganizerNode() : base.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Вернуть список колонок по умолчанию для корневого узла.
  /// </summary>
  /// <param name="content">Содержание</param>
  /// <returns>Список по умолчанию для корневого узла</returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    this.AddObligatoryColumns(columns);
    return columns.Count > 0 ? columns : Utils.DefaultColumnsObjects();
  }

  /// <summary>
  /// Вернуть список поддерживаемых колонок для корневого узла.
  /// </summary>
  /// <param name="content">Содержание</param>
  /// <param name="ColumnSetName">Имя набора колонок</param>
  /// <returns>Список поддерживаемых колонок для корневого узла</returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return Utils.DefaultSupportedColumnsObjects();
  }

  /// <summary>Добавление обязательных солонок.</summary>
  /// <param name="columns"></param>
  private void AddObligatoryColumns(NodeColumnCollection columns)
  {
    if (columns == null)
      columns = new NodeColumnCollection();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    if (service == null)
      return;
    if (!columns.ColumnIDExists((object) ObligatoryObjectAttributes.CAPTION))
    {
      NodeColumn column = service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION, NodeColumnSortOrder.Ascending, 0);
      columns.Insert(0, column);
    }
    int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeStart);
    if (!columns.ColumnIDExists((object) attributeTypeId1))
      columns.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attributeTypeId1));
    int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID(SystemGUIDs.attributeDueDate);
    if (columns.ColumnIDExists((object) attributeTypeId2))
      return;
    columns.Add(service.CreateColumn(Intermech.Navigator.Consts.ObjectColumnSchemeGuid, (object) attributeTypeId2));
  }
}
