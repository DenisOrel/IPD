// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchivesBinding
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Привязки выборок и классификаторов для узла "Архивы документов"
/// </summary>
internal class ArchivesBinding : ITopBinding, IBinding
{
  /// <summary>Условия</summary>
  private ConditionStructure[] _topConditions;
  /// <summary>Тип привязки</summary>
  private BindingType _bindingType;

  /// <summary>Создать привязку</summary>
  /// <param name="bindingType">Тип привязки</param>
  public ArchivesBinding(BindingType bindingType) => this._bindingType = bindingType;

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
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(2);
        switch (this._bindingType)
        {
          case BindingType.Selections:
          case BindingType.CommonSelections:
          case BindingType.PersonalSelections:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this._bindingType, 2));
            break;
          case BindingType.Classificators:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4ClassifierNode(2));
            break;
        }
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
      dbObject.GetAttributeByID(Intermech.Navigator.Selections.Consts.GetKindAttributeID(dbObject.ObjectType)).Value = (object) 2;
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
    AllDocumsPart part = new AllDocumsPart(conditionsProvider, (IServiceProvider) null);
    part.AcceptManagedEvents = false;
    return (INodePart) part;
  }

  /// <summary>
  /// Возвращает название закладки, на которой будут отображаться объекты,
  /// найденные с помощью условий выборки.
  /// </summary>
  public string ViewCaption
  {
    [DebuggerStepThrough] get => ServiceHolder.rm.GetString("Archives_2");
  }
}
