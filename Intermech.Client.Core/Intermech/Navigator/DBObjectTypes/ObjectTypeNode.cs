
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.LifeCycle;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Класс, реализующий элемент "Тип объекта" из пространства навигации. Вся
/// функциональность наследуется от элемента "Контейнер типов объектов".
/// Дополнительно реализован интерфейс для инициализации элемента.
/// </summary>
public class ObjectTypeNode : CompositeNode, IContextAware, INodeNotifications
{
  /// <summary>
  /// Идентификатор типа объектов, производные от которого будет
  /// обрабатывать этот элемент.
  /// </summary>
  protected int objTypeID;
  /// <summary>Контейнер сервисов для узла</summary>
  private AdvancedServiceContainer _serviceProvider = new AdvancedServiceContainer();
  /// <summary>Права доступа к списку объектов</summary>
  protected AccessRights _accessRights;
  /// <summary>Флаг того, показывать ли элементы "Классификаторы"</summary>
  protected bool _showClassifiers = true;
  private Intermech.Navigator.DBObjectTypes.Implementation.NodeID _nodeID;

  /// <summary>Конструктор</summary>
  /// <param name="objTypeID">Тип объекта</param>
  /// <param name="accessRights">Права доступа к списку объектов</param>
  public ObjectTypeNode(int objTypeID, AccessRights accessRights)
  {
    this.objTypeID = objTypeID;
    this._accessRights = accessRights;
    this.options = NodeOptions.CanContainsObjectsList;
  }

  public ObjectTypeNode(Intermech.Navigator.DBObjectTypes.Implementation.NodeID nodeID)
  {
    this.objTypeID = nodeID.TypeID;
    this._nodeID = nodeID;
    this.options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>
  /// Идентификатор типа объектов, производные от которого будет
  /// обрабатывать этот элемент.
  /// </summary>
  public int ObjTypeID => this.objTypeID;

  /// <summary>Права доступа к списку объектов</summary>
  public AccessRights AccessRights
  {
    get
    {
      if (this._accessRights == AccessRights.NotDefined)
        this._accessRights = this._nodeID != null ? this._nodeID.AccessRights : AccessRights.NotDefined;
      return this._accessRights;
    }
    set => this._accessRights = value;
  }

  public override INodeQuery GetQuery(ContentType content) => base.GetQuery(content);

  /// <summary>Вернуть слоты-папки</summary>
  /// <returns>Слоты-папки</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    IObjectTypeNodeOptionsHolder service1 = this.Services != null ? this.Services.GetService(typeof (IObjectTypeNodeOptionsHolder)) as IObjectTypeNodeOptionsHolder : (IObjectTypeNodeOptionsHolder) null;
    ObjectTypeNodeOptions objectTypeNodeOptions = ObjectTypeNodeOptions.None;
    if (service1 != null)
      objectTypeNodeOptions = service1.Options;
    DescriptorCollection descriptors = (DescriptorCollection) null;
    CurrentUserAndRole service2 = ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) as CurrentUserAndRole;
    bool enableSelections = true;
    if (this.AccessRights == AccessRights.Enabled && (objectTypeNodeOptions & ObjectTypeNodeOptions.OnlyTypesMode) == ObjectTypeNodeOptions.None)
    {
      if (service2 != null && MetaDataHelper.ExistsObjectType(this.objTypeID) && !service2.InternalRule.AreSelectionsAndClassifiersEnabled(this.objTypeID, true))
        enableSelections = false;
      descriptors = this.GetSpecialDescriptors(enableSelections, this._showClassifiers & enableSelections);
    }
    List<PartSlot> folderSlots = new List<PartSlot>();
    if (((this.AccessRights != AccessRights.Enabled ? 0 : ((objectTypeNodeOptions & ObjectTypeNodeOptions.OnlyTypesMode) == ObjectTypeNodeOptions.None ? 1 : 0)) & (enableSelections ? 1 : 0)) != 0)
      folderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(descriptors, false)));
    folderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new ObjectTypesPart(this.objTypeID)));
    return folderSlots;
  }

  protected override ITopBinding GetBinding(BindingType bindingType)
  {
    return (ITopBinding) new ObjectTypeBinding(this.objTypeID, bindingType);
  }

  /// <summary>Вернуть слоты-не-папки</summary>
  /// <returns>Слоты-не-папки</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    IObjectTypeNodeOptionsHolder service = this.Services != null ? this.Services.GetService(typeof (IObjectTypeNodeOptionsHolder)) as IObjectTypeNodeOptionsHolder : (IObjectTypeNodeOptionsHolder) null;
    ObjectTypeNodeOptions objectTypeNodeOptions = ObjectTypeNodeOptions.None;
    if (service != null)
      objectTypeNodeOptions = service.Options;
    if (this.AccessRights != AccessRights.Enabled)
    {
      if (service != null)
        service.Options |= ObjectTypeNodeOptions.EmptyQuery;
      else
        this._serviceProvider.AddService(typeof (IObjectTypeNodeOptionsHolder), (object) new ObjectTypeNodeOptionsHolder(ObjectTypeNodeOptions.EmptyQuery));
      return this.SlotsFromSinglePart((INodePart) this.GetObjectsPart());
    }
    if (service != null)
    {
      int num = (objectTypeNodeOptions & ObjectTypeNodeOptions.OnlyTypesMode) == ObjectTypeNodeOptions.OnlyTypesMode ? 1 : 0;
      service.Options = ObjectTypeNodeOptions.None;
      if (num != 0)
        service.Options |= ObjectTypeNodeOptions.OnlyTypesMode;
      if ((objectTypeNodeOptions & ObjectTypeNodeOptions.ShowLCSteps) == ObjectTypeNodeOptions.ShowLCSteps)
        service.Options |= ObjectTypeNodeOptions.ShowLCSteps;
    }
    else
      this._serviceProvider.AddService(typeof (IObjectTypeNodeOptionsHolder), (object) new ObjectTypeNodeOptionsHolder(ObjectTypeNodeOptions.None));
    if ((objectTypeNodeOptions & ObjectTypeNodeOptions.OnlyTypesMode) != ObjectTypeNodeOptions.OnlyTypesMode)
      return this.SlotsFromSinglePart((INodePart) this.GetObjectsPart());
    IMSObjectType objectType = MetaDataHelper.GetObjectType(this.objTypeID);
    if (objectType == null)
      return (List<PartSlot>) null;
    List<PartSlot> nonFolderSlots = new List<PartSlot>();
    if ((objectTypeNodeOptions & ObjectTypeNodeOptions.ShowLCSteps) == ObjectTypeNodeOptions.ShowLCSteps)
    {
      nonFolderSlots.Add(new PartSlot(Intermech.Navigator.Consts.CategoryLifeCycleSchemeNodeGuid, (INodePart) new LifeCycleSchemeStepsPart(objectType.SchemaID, this.Services)));
      return nonFolderSlots;
    }
    nonFolderSlots.Add(new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new ObjectTypesPart(this.objTypeID)));
    return nonFolderSlots;
  }

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (!(dataFormat == typeof (IObjectTypeNodeOptionsHolder)))
      return base.GetData(nodeID, dataFormat);
    return this.Services == null ? (object) null : (object) (this.Services.GetService(typeof (IObjectTypeNodeOptionsHolder)) as IObjectTypeNodeOptionsHolder);
  }

  /// <summary>Возвращает часть со списком объектов</summary>
  /// <returns></returns>
  protected virtual ObjectsPart GetObjectsPart() => new ObjectsPart(this.objTypeID, this.Services);

  /// <summary>Контейнер сервисов для узла</summary>
  public IServiceProvider Services
  {
    get => (IServiceProvider) this._serviceProvider;
    set => this._serviceProvider.AdvancedProvider = value;
  }

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    if (e.EventName == "ObjectTypesChanged" && e is DBObjectTypesEventArgs && (e as DBObjectTypesEventArgs).ObjectTypeIDs.Contains(this.objTypeID))
      return ProcessResult.RefreshNode;
    if ((e.EventName == "AttributeChanged" || e.EventName == "AttributeRemoved") && AdditionalInfo != null && AdditionalInfo is NodeColumnCollection && e is DBAttributesEventArgs && (AdditionalInfo as NodeColumnCollection).ColumnIDsExists((e as DBAttributesEventArgs).AttributeIDs))
      return ProcessResult.RefreshNodeAndColumns;
    if (e.EventName == "Attribute4ObjTypeEvent")
    {
      DBAttributes4TypeEventArgs attributes4TypeEventArgs = e as DBAttributes4TypeEventArgs;
      NodeColumnCollection columnCollection = AdditionalInfo as NodeColumnCollection;
      if (attributes4TypeEventArgs != null && columnCollection != null && columnCollection.Count > 0 && (columnCollection.ColumnIDsExists(attributes4TypeEventArgs.ChangedIDs) || columnCollection.ColumnIDsExists(attributes4TypeEventArgs.RemovedIDs)))
        return ProcessResult.RefreshNodeAndColumns;
    }
    return ProcessResult.None;
  }
}
