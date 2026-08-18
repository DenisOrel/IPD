
// Type: Intermech.Client.Core.Organizer.OrganizerTaskNodeBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
internal class OrganizerTaskNodeBinding : ITopBinding, IBinding
{
  private int _categoryID = -1;
  private string _caption = string.Empty;
  private ConditionStructure[] _topConditions;
  private bool _isOrganizerNode;

  /// <summary>Конструктор.</summary>
  public OrganizerTaskNodeBinding(bool isOrganizerNode)
  {
    this._isOrganizerNode = isOrganizerNode;
    this._categoryID = MetaDataHelper.GetObjectTypeID("cad015bc-306c-11d8-b4e9-00304f19f545");
    this._caption = LocalizationHolder.rm.GetString("Organaizer_TaskCaption");
  }

  /// <summary>
  /// Возвращает набор условий, для нахождения выборок, верхнего уровня дерева выборок.
  /// </summary>
  public ConditionStructure[] TopConditions
  {
    get
    {
      if (this._topConditions == null)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(3);
        if (this._isOrganizerNode)
        {
          ConditionStructure conditionStructure1 = new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.AND, 1, false);
          ConditionStructure conditionStructure2 = new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.Equal, (object) 8, LogicalOperators.AND, -1, false);
          conditionStructureList.AddRange((IEnumerable<ConditionStructure>) new ConditionStructure[2]
          {
            conditionStructure1,
            conditionStructure2
          });
          int attributeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(new Guid("cad015d1-306c-11d8-b4e9-00304f19f545"), true).AttributeID;
          conditionStructureList.Add(new ConditionStructure(attributeId, RelationalOperators.Equal, (object) this._categoryID, (object) null, LogicalOperators.NONE, 0, false));
        }
        else
        {
          ConditionStructure conditionStructure3 = new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.AND, 1, false);
          ConditionStructure conditionStructure4 = new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.Equal, (object) 3, LogicalOperators.AND, -1, false);
          ConditionStructure conditionStructure5 = new ConditionStructure(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeGuid(this._categoryID).ToString(), LogicalOperators.NONE, 0, false);
          conditionStructureList.AddRange((IEnumerable<ConditionStructure>) new ConditionStructure[3]
          {
            conditionStructure3,
            conditionStructure4,
            conditionStructure5
          });
        }
        this._topConditions = conditionStructureList.ToArray();
      }
      return this._topConditions;
    }
  }

  /// <summary>
  /// Выполняет вставку выборки в верхний уровень дерева выборок.
  /// Вызывается сразу после создания новой выборки.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  public void BindSelection(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(selObjectID);
      dbObject.Attributes.FindByID(Intermech.Navigator.Selections.Consts.GetKindAttributeID(dbObject.ObjectType)).Value = (object) 8;
      int attributeId = sessionKeeper.Session.GetAttributeType(new Guid("cad015d1-306c-11d8-b4e9-00304f19f545")).AttributeID;
      (dbObject.Attributes.FindByID(attributeId) ?? dbObject.Attributes.AddAttribute(attributeId, false)).Value = (object) this._categoryID;
    }
  }

  /// <summary>Возвращает название корня дерева выборок.</summary>
  /// <param name="selTypeID">Идентификатор базового типа выборок в дереве</param>
  /// <returns>Название корня дерева выборок</returns>
  public string GetCaption(int selTypeID) => Intermech.Navigator.DBObjectTypes.Helper.GetObjectTypeName(selTypeID);

  /// <summary>
  /// Возвращает для корня дерева выборок данные в указанном формате.
  /// </summary>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Данные в запрошенном формате</returns>
  public object GetData(Type dataFormat) => (object) null;

  /// <summary>
  /// 
  /// </summary>
  public BindingType BindingType => BindingType.Selections;

  /// <summary>
  /// Возвращает набор условий для выборки с указанным идентификатором.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  /// <returns>Массив условий, которые позволяют найти в базе данных объекты, удовлетворяющие условиям выборки</returns>
  public ConditionStructure[] GetConditions(long selObjectID)
  {
    ConditionStructure[] joinedConditions = OrganizerTaskNode.DefaultConditions;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ISelectionsService service = (ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService));
      if (service != null)
        joinedConditions = ConditionStructure.Join(joinedConditions, service.GetConditionStructures((object) sessionKeeper.Session, selObjectID));
    }
    return joinedConditions;
  }

  /// <summary>
  /// Возвращает часть элемента навигации, которая будет работать с объектами, найденными с помощью условий выборки.
  /// </summary>
  /// <param name="conditionProvider">Провайдер, предоставляющий условия выборки</param>
  /// <returns>Часть элемента навигации</returns>
  public INodePart GetPart(IConditionsProvider conditionProvider)
  {
    return (INodePart) new ObjectsPart(this._categoryID, conditionProvider, (IServiceProvider) null);
  }

  /// <summary>
  /// Возвращает название закладки, на которой будут отображаться объекты, найденные с помощью условий выборки.
  /// </summary>
  public string ViewCaption => this._caption;
}
