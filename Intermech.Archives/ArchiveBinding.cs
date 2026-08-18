// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchiveBinding
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using Intermech.Search;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Привязка выборок и классификаторов к узлу, связанному с архивом документов
/// </summary>
internal class ArchiveBinding : ITopBinding, IBinding, IBindingStateStream
{
  /// <summary>Тип объекта</summary>
  private int _arcTypeID;
  /// <summary>Идентификатор архива</summary>
  private long _arcID;
  /// <summary>Тип привязки</summary>
  private BindingType _bindingType;
  /// <summary>Условия выборок</summary>
  private ConditionStructure[] _topConditions;

  /// <summary>Создать привязки для узла архива</summary>
  /// <param name="arcTypeID">Тип архива</param>
  /// <param name="arcID">Идентификатор архива</param>
  /// <param name="bindingType">Тип привязки</param>
  public ArchiveBinding(int arcTypeID, long arcID, BindingType bindingType)
  {
    this._arcTypeID = arcTypeID;
    this._arcID = arcID;
    this._topConditions = (ConditionStructure[]) null;
    this._bindingType = bindingType;
  }

  /// <summary>
  /// Возвращает набор условий, с помощью которых можно найти выборки,
  /// находящиеся на верхнем уровне дерева выборок.
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
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this._bindingType, 1));
            break;
          case BindingType.Classificators:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4ClassifierNode(1));
            break;
        }
        conditionStructureList.Add(new ConditionStructure(ConstsHolder.ArchivesForSelectionID, RelationalOperators.Equal, (object) this._arcID, (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Object, ColumnContents.ID));
        this._topConditions = conditionStructureList.ToArray();
      }
      return this._topConditions;
    }
  }

  /// <summary>
  /// Выполняет вставку выборки в верхний уровень дерева выборок. Вызывается сразу
  /// после создания новой выборки.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  public void BindSelection(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(selObjectID);
      AttributeValues[] valuesList = new AttributeValues[2]
      {
        new AttributeValues(Intermech.Navigator.Selections.Consts.GetKindAttributeID(dbObject.ObjectType), (object) 1),
        new AttributeValues(ConstsHolder.ArchivesForSelectionID, (object) new object[1]
        {
          (object) this._arcID
        })
      };
      dbObject.SetAttributesValues(valuesList);
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

  /// <summary>Возвращает тип привязки</summary>
  public BindingType BindingType
  {
    [DebuggerStepThrough] get => this._bindingType;
  }

  /// <summary>
  /// Возвращает набор условий для выборки с указанным идентификатором.
  /// </summary>
  /// <param name="selObjectID">Идентификатор объекта-выборки</param>
  /// <returns>
  /// Массив условий, которые позволяют найти в базе данных объекты,
  /// удовлетворяющие условиям выборки.
  /// </returns>
  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID);
  }

  /// <summary>
  /// Возвращает часть элемента навигации, которая будет работать с объектами,
  /// найденными с помощью условий выборки.
  /// </summary>
  /// <param name="conditionProvider">Провайдер, предоставляющий условия выборки</param>
  /// <returns>Часть элемента навигации</returns>
  public INodePart GetPart(IConditionsProvider conditionsProvider)
  {
    DocumsPart part = new DocumsPart(this._arcID, conditionsProvider, (IServiceProvider) null);
    part.AcceptManagedEvents = false;
    return (INodePart) part;
  }

  /// <summary>
  /// Возвращает название закладки, на которой будут отображаться объекты,
  /// найденные с помощью условий выборки.
  /// </summary>
  public string ViewCaption
  {
    [DebuggerStepThrough] get => ServiceHolder.rm.GetString("Archives_4");
  }

  public int CategoryID => 1;

  public int CategoryType => this._arcTypeID;

  public string Prefix
  {
    get => Constants.ArchiveStateStreamPrefix + Convert.ToString(Math.Abs(this._arcID));
  }
}
