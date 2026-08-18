
// Type: Intermech.Client.Core.Organizer.OrganizerChildNodeBinding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
internal class OrganizerChildNodeBinding : ITopBinding, IBinding
{
  /// <summary>Идентификатор категории подузла органайзера</summary>
  private int _categoryID = -1;
  /// <summary>Наименование узла</summary>
  private string _caption = string.Empty;
  /// <summary>Часть элемента навигации</summary>
  private INodePart _part;
  /// <summary>
  /// Набор условий, для нахождения выборок, верхнего уровня дерева выборок
  /// </summary>
  private ConditionStructure[] _topConditions;
  /// <summary>Набор условий для выбора данных</summary>
  private ConditionStructure[] _conditions;
  /// <summary>
  /// Тип привязки к элементу навигации (реализовано для "Выборки")
  /// </summary>
  private BindingType _bindingType;

  /// <summary>Конструктор.</summary>
  /// <param name="categoryID">Идентификатор категории подузла органайзера</param>
  /// <param name="part">Часть элемента навигации</param>
  /// <param name="bindingType">Тип привязки к элементу навигации (реализовано для "Выборки")</param>
  public OrganizerChildNodeBinding(int categoryID, INodePart part, BindingType bindingType)
  {
    this._categoryID = categoryID;
    this._part = part;
    this._bindingType = bindingType;
    if (!(ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service))
      return;
    OrganizerChildNodeDescriptor descriptor = service.GetDescriptor(categoryID);
    if (descriptor == null)
      return;
    this._caption = descriptor.Caption;
    this._conditions = descriptor.Conditions;
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
        switch (this._bindingType)
        {
          case BindingType.Selections:
          case BindingType.CommonSelections:
          case BindingType.PersonalSelections:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this._bindingType, 8));
            break;
        }
        int attributeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetAttributeType(new Guid("cad015d1-306c-11d8-b4e9-00304f19f545"), true).AttributeID;
        conditionStructureList.Add(new ConditionStructure(attributeId, RelationalOperators.Equal, (object) this._categoryID, (object) null, LogicalOperators.NONE, 0, false));
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
  public BindingType BindingType => this._bindingType;

  /// <summary>
  /// Возвращает набор условий для выборки с указанным идентификатором.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  /// <returns>Массив условий, которые позволяют найти в базе данных объекты, удовлетворяющие условиям выборки</returns>
  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ConditionStructure.Join(this._conditions, ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID));
  }

  /// <summary>
  /// Возвращает часть элемента навигации, которая будет работать с объектами, найденными с помощью условий выборки.
  /// </summary>
  /// <param name="conditionProvider">Провайдер, предоставляющий условия выборки</param>
  /// <returns>Часть элемента навигации</returns>
  public INodePart GetPart(IConditionsProvider conditionProvider) => this._part;

  /// <summary>
  /// Возвращает название закладки, на которой будут отображаться объекты, найденные с помощью условий выборки.
  /// </summary>
  public string ViewCaption => this._caption;
}
