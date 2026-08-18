
// Type: Intermech.Navigator.Selections.Implementation.SelectionNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>
/// Реализует элемент навигации "Выборка" или "Классификатор"
/// </summary>
public class SelectionNode : ObjectNode, IConditionsProvider
{
  /// <summary>Привязки</summary>
  protected IBinding _binding;
  /// <summary>Внешние условия</summary>
  protected IConditionsProvider _externalConditions;
  /// <summary>Ручная выборка</summary>
  private bool _handSelection;
  /// <summary>Идентификатор типа объекта, с которым связана выборка</summary>
  private int _bindedObjectTypeID = -1;
  /// <summary>Назначение выборки</summary>
  private int _sampleFunction;
  /// <summary>Искать среди объектов глобальных и локальных типов</summary>
  private bool _searchInLocalTypes;

  /// <summary>Тип выбранной выборки/классификатора</summary>
  public int SelTypeID
  {
    [DebuggerStepThrough] get => this._objTypeID;
  }

  /// <summary>Идентификатор версии выбранной выборки</summary>
  public long SelID
  {
    [DebuggerStepThrough] get => this._objID;
  }

  /// <summary>Привязки</summary>
  public IBinding Binding
  {
    [DebuggerStepThrough] get => this._binding;
  }

  /// <summary>Внешние условия</summary>
  public IConditionsProvider ExternalConditions
  {
    [DebuggerStepThrough] get => this._externalConditions;
  }

  /// <summary>Создать узел</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="selObjID">Идентификатор версии объекта</param>
  public SelectionNode(int selTypeID, long selObjID)
    : this(selTypeID, selObjID, FreeBinding.Value, (IConditionsProvider) null)
  {
    this._handSelection = false;
    this.options |= NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Создать узел</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="selObjID">Идентификатор версии объекта</param>
  /// <param name="handSelection">Ручная ли выборка</param>
  /// <param name="sampleFunction"></param>
  /// <param name="searchInLocalTypes"></param>
  public SelectionNode(
    int selTypeID,
    long selObjID,
    bool handSelection,
    int sampleFunction,
    bool searchInLocalTypes)
    : this(selTypeID, selObjID, sampleFunction == 3 ? FilteredFreeBinding.Value : FreeBinding.Value, (IConditionsProvider) null, handSelection, -1, sampleFunction, searchInLocalTypes)
  {
    this.options |= NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Создать узел</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="selObjID">Идентификатор версии объекта</param>
  /// <param name="binding">Привязки</param>
  public SelectionNode(int selTypeID, long selObjID, IBinding binding)
    : this(selTypeID, selObjID, binding, (IConditionsProvider) null)
  {
    this._handSelection = false;
    this.options |= NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Создать узел</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="selObjID">Идентификатор версии объекта</param>
  /// <param name="binding">Привязки</param>
  /// <param name="externalConditions">Внешние условия</param>
  /// <param name="propagateConditions">Наследовать условия</param>
  public SelectionNode(
    int selTypeID,
    long selObjID,
    IBinding binding,
    IConditionsProvider externalConditions)
    : base(selTypeID, selObjID)
  {
    this._binding = binding;
    this._externalConditions = externalConditions;
    this._handSelection = false;
    this.options |= NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Создать узел</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="selObjID">Идентификатор версии объекта</param>
  /// <param name="binding">Привязки</param>
  /// <param name="externalConditions">Внешние условия</param>
  /// <param name="propagateConditions">Наследовать условия</param>
  /// <param name="handSelection">Флажок "Ручная сортировка"</param>
  /// <param name="bindedObjectTypeID">Идентификатор типа объекта, с которым связана выборка</param>
  public SelectionNode(
    int selTypeID,
    long selObjID,
    IBinding binding,
    IConditionsProvider externalConditions,
    bool handSelection,
    int bindedObjectTypeID,
    int sampleFunction,
    bool searchInLocalTypes)
    : base(selTypeID, selObjID)
  {
    this._binding = binding;
    this._externalConditions = externalConditions;
    this._handSelection = handSelection;
    this._bindedObjectTypeID = bindedObjectTypeID;
    this._sampleFunction = sampleFunction;
    this._searchInLocalTypes = searchInLocalTypes;
    this.options |= NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Получить список условий</summary>
  /// <returns></returns>
  public ConditionStructure[] GetConditions()
  {
    IContextAware contextAware = (IContextAware) this;
    long conditionValue = 0;
    if (contextAware != null && contextAware.Services.GetService(typeof (ProjectObjectID)) != null)
      conditionValue = (contextAware.Services.GetService(typeof (ProjectObjectID)) as ProjectObjectID).ProjectID;
    ConditionStructure[] joinedConditions = (ConditionStructure[]) null;
    if (SelectionCommands.IsSelection(this._objTypeID) && this._externalConditions != null)
    {
      joinedConditions = this._externalConditions.GetConditions();
      if (joinedConditions != null && joinedConditions.Length == 0)
        joinedConditions = (ConditionStructure[]) null;
    }
    ConditionStructure[] conditions = ConditionStructure.Join(joinedConditions, this._binding.GetConditions(this._objID));
    if (conditionValue != 0L)
    {
      ConditionStructure conditionStructure = new ConditionStructure(-14, RelationalOperators.Equal, (object) conditionValue, LogicalOperators.AND, 0, true);
      conditions = ConditionStructure.Join(conditions, new ConditionStructure[1]
      {
        conditionStructure
      });
    }
    if (this._searchInLocalTypes)
      conditions = ConditionStructure.Join(new ConditionStructure[1]
      {
        new ConditionStructure(0, RelationalOperators.LocalObjectTypes, (object) null, LogicalOperators.NONE, 0, false)
      }, conditions);
    return conditions;
  }

  /// <summary>Изменились ли условия выборки</summary>
  public bool ConditionsChanged => true;

  /// <summary>Вернуть папки узла</summary>
  /// <param name="relTypeId">Тип связи</param>
  /// <returns>Папки узла</returns>
  protected override INodePart CreateFolderPart(int relTypeId)
  {
    if (this._binding == null)
      return (INodePart) null;
    return ApplicationServices.Container.GetService(typeof (ICurrentUserAndRole)) is CurrentUserAndRole service && MetaDataHelper.ExistsObjectType(this._bindedObjectTypeID) && !service.InternalRule.AreSelectionsAndClassifiersEnabled(this._bindedObjectTypeID, true) ? (INodePart) null : (INodePart) new SelectionsPart(this._objTypeID, this._objID, relTypeId, this._binding, (IConditionsProvider) this, this._sampleFunction, this.Services);
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    if (this._binding == null)
      return (List<PartSlot>) null;
    return this._binding is IBindingEx ? ((IBindingEx) this._binding).CreateNonFolderSlots((IConditionsProvider) this) : this.SlotsFromSinglePart(this._binding.GetPart((IConditionsProvider) this));
  }

  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  public override ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    ProcessResult processResult = base.Process(e, AdditionalInfo);
    if (processResult == ProcessResult.None && e.EventName == "ObjectsChanged" && e is DBObjectsEventArgs objectsEventArgs && objectsEventArgs.ObjectIDs.Contains(this._objID))
      processResult = ProcessResult.RefreshNode;
    return processResult;
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (nodeID is SelectionNodeID selectionNodeId && dataFormat == typeof (IDBObjectTypeSelectionID) && selectionNodeId.BindedObjectTypeID <= 0)
    {
      int filterObjectType = this.FilterObjectType;
      if (filterObjectType != -1)
        return (object) new DBObjectTypeSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType, filterObjectType);
    }
    return base.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Идентификатор типа объекта, указанный в условиях выборки для оператора "Искать среди объектов типа"
  /// </summary>
  internal int FilterObjectType
  {
    [DebuggerStepThrough] get
    {
      ConditionStructure[] conditions = this.GetConditions();
      if (conditions != null && conditions.Length != 0)
      {
        for (int index = 0; index < conditions.Length; ++index)
        {
          if (conditions[index].RelationalOperator == RelationalOperators.ObjectTypeFilter && conditions[index].Value != null)
            return (int) conditions[index].Value;
        }
      }
      return -1;
    }
  }

  public override NodeColumnCollection GetSupportedColumns(
    ContentType content,
    string ColumnSetName)
  {
    if (content == ContentType.NonFolders)
    {
      int filterObjectType = this.FilterObjectType;
      if (filterObjectType != -1)
      {
        Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
        IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
        NodeColumnCollection columns = new NodeColumnCollection();
        Intermech.Navigator.DBObjects.Helper.AddObligatoryColumns(columns, true, true);
        Intermech.Navigator.DBObjects.Helper.AddObligatoryColumnsAdv(columns);
        Intermech.Navigator.DBObjects.Helper.AddObjectTypeColumns(columns, filterObjectType);
        Intermech.Navigator.DBObjects.Helper.AddAllColumns(columns);
        return columns;
      }
    }
    return base.GetSupportedColumns(content, ColumnSetName);
  }
}
