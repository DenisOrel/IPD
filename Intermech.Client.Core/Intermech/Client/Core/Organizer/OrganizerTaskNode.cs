
// Type: Intermech.Client.Core.Organizer.OrganizerTaskNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>Класс для узел "Задачи органайзера".</summary>
public class OrganizerTaskNode : ObjectTypeNode, IContextAware, IOrganizerConditionNode
{
  private IServiceProvider _services;
  private ConditionStructure[] _conditions;

  /// <summary>Конструктор.</summary>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="accessRights">Права доступа к списку объектов</param>
  public OrganizerTaskNode(int objTypeID, AccessRights accessRights)
    : base(objTypeID, accessRights)
  {
    this._showClassifiers = false;
  }

  /// <summary>Контейнер сервисов.</summary>
  public new IServiceProvider Services
  {
    get => this._services;
    set => this._services = value;
  }

  /// <summary>Формирование слотов-папок.</summary>
  /// <returns>Коллекция слотов-папок</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    IOrganizerNode service = this._services.GetService(typeof (IOrganizerNode)) as IOrganizerNode;
    DescriptorCollection descriptors = new DescriptorCollection();
    descriptors.Add(Intermech.Navigator.Selections.Consts.SelectionsDescriptorGuid, (IDescriptor) new HiveDescriptor(Intermech.Navigator.Selections.Consts.SelectionTypeID, (ITopBinding) new OrganizerTaskNodeBinding(service != null)));
    List<PartSlot> folderSlots = new List<PartSlot>(1);
    folderSlots.Insert(0, new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(descriptors)));
    return folderSlots;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return OrganizerTaskNode.GetNonFolderSlots(this.Services, this._conditions);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="content"></param>
  /// <returns></returns>
  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return base.GetDefaultColumns(content);
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого можно прочитать список дочерних элементов.
  /// Если у данного элемента нет дочерних, то метод вернет null.
  /// </summary>
  /// <param name="content">Набор флагов, описывающих тип читаемых дочерних элементов</param>
  /// <returns>Интерфейс запроса</returns>
  public override INodeQuery GetQuery(ContentType content)
  {
    this.nonFolderSlots = (List<PartSlot>) null;
    return base.GetQuery(content);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="content"></param>
  /// <param name="ColumnSetName"></param>
  /// <returns></returns>
  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    return base.GetSupportedColumns(content, ColumnSetName);
  }

  /// <summary>Получение условий выбора объектов по умолчанию.</summary>
  public static ConditionStructure[] DefaultConditions
  {
    get
    {
      int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad00628-306c-11d8-b4e9-00304f19f545");
      int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad0002f-306c-11d8-b4e9-00304f19f545");
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        long userId = sessionKeeper.Session.UserID;
        return new ConditionStructure[2]
        {
          new ConditionStructure(attributeTypeId1, RelationalOperators.Equal, (object) userId, (object) null, LogicalOperators.OR, 1, false, AttributeSourceTypes.Auto, ColumnContents.ID),
          new ConditionStructure(attributeTypeId2, RelationalOperators.Equal, (object) userId, LogicalOperators.NONE, -1, false)
        };
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="services"></param>
  /// <param name="conditions"></param>
  /// <returns></returns>
  public static List<PartSlot> GetNonFolderSlots(
    IServiceProvider services,
    ConditionStructure[] conditions)
  {
    List<PartSlot> nonFolderSlots = new List<PartSlot>(1);
    ConditionStructure[] joinedConditions = (ConditionStructure[]) null;
    int objectTypeId = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (services.GetService(typeof (IOrganizerNode)) as IOrganizerNode == null)
      {
        if (sessionKeeper.Session.ShowPersonalObjects)
          goto label_7;
      }
      joinedConditions = OrganizerTaskNode.DefaultConditions;
    }
label_7:
    ConditionStructure[] conditions1 = ConditionStructure.Join(joinedConditions, conditions);
    ObjectsPart part = new ObjectsPart(objectTypeId, conditions1, services);
    nonFolderSlots.Add(new PartSlot(new Guid("cad015bc-306c-11d8-b4e9-00304f19f545"), (INodePart) part));
    return nonFolderSlots;
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
}
