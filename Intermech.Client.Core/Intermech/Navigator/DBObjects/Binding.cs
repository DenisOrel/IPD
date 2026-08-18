
// Type: Intermech.Navigator.DBObjects.Binding
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

internal sealed class Binding : ITopBinding, IBinding
{
  private readonly int _objTypeID;
  private long _objID;
  private ConditionStructure[] _topConditions;

  public Binding(int objTypeID, long objID, BindingType bindingType)
  {
    this._objTypeID = objTypeID;
    this._objID = objID;
    this._topConditions = (ConditionStructure[]) null;
    this.BindingType = bindingType;
    INotificationService service = ServicesManager.GetService<INotificationService>();
    service.Subscribe("ObjectsCheckedIn", new NotificationEventHandler(this.ObjectsWasChangedHandler));
    service.Subscribe("ObjectsCheckedOut", new NotificationEventHandler(this.ObjectsWasChangedHandler));
    service.Subscribe("ObjectsChangesCancelled", new NotificationEventHandler(this.ObjectsWasChangedHandler));
  }

  private void ObjectsWasChangedHandler(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null || !objectsEventArgs.ObjectIDs.Contains(this._objID))
      return;
    if (objectsEventArgs.EventName.Equals("ObjectsCheckedIn") || objectsEventArgs.EventName.Equals("ObjectsChangesCancelled"))
    {
      this._objID = Math.Abs(this._objID);
    }
    else
    {
      if (!objectsEventArgs.EventName.Equals("ObjectsCheckedOut"))
        return;
      this._objID = -1L * this._objID;
    }
  }

  public ConditionStructure[] TopConditions
  {
    get
    {
      if (this._topConditions == null)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(this._objTypeID);
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(8);
        switch (this.BindingType)
        {
          case BindingType.Selections:
          case BindingType.CommonSelections:
          case BindingType.PersonalSelections:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this.BindingType, 5));
            break;
          case BindingType.Classificators:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4ClassifierNode(5));
            break;
        }
        List<int> objectTypeParentsId = MetaDataHelper.GetObjectTypeParentsID(objectType.ObjectTypeID);
        objectTypeParentsId.Insert(0, objectType.ObjectTypeID);
        conditionStructureList.Add(new ConditionStructure(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, RelationalOperators.Empty, (object) null, LogicalOperators.OR, 1, false));
        for (int index = 0; index < objectTypeParentsId.Count; ++index)
        {
          LogicalOperators logicalOperator = index < objectTypeParentsId.Count - 1 ? LogicalOperators.OR : LogicalOperators.AND;
          int groupID = index < objectTypeParentsId.Count - 1 ? 0 : -1;
          conditionStructureList.Add(new ConditionStructure(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, RelationalOperators.Equal, (object) MetaDataHelper.GetObjectTypeGuid(objectTypeParentsId[index]).ToString(), logicalOperator, groupID, false));
        }
        this._topConditions = conditionStructureList.ToArray();
      }
      return this._topConditions;
    }
  }

  public void BindSelection(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(selObjectID);
      dbObject.GetAttributeByID(Intermech.Navigator.Selections.Consts.GetKindAttributeID(dbObject.ObjectType)).Value = (object) 5;
    }
  }

  public string GetCaption(int selTypeID) => Helper.GetAddress(this._objID);

  public object GetData(Type dataFormat)
  {
    if (dataFormat == typeof (IDBObjectID))
      return (object) new DBObjectID(this._objID, 0L, string.Empty, 0L);
    return dataFormat == typeof (IDBTypedObjectID) ? (object) new DBTypedObjectID(this._objTypeID, this._objID, 0L, string.Empty, 0L, 0L, 0L, string.Empty, 0L) : (object) null;
  }

  public BindingType BindingType { get; }

  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServicesManager.GetService<ISelectionsService>().GetConditionStructures((object) sessionKeeper.Session, selObjectID, this._objID);
  }

  /// <summary>
  /// Возвращает название закладки, на которой будут отображаться объекты,
  /// найденные с помощью выборки.
  /// </summary>
  public string ViewCaption => LocalizationHolder.rm.GetString("Client.Core_277");

  /// <summary>
  /// Возвращает часть, которая будет работать с объектами, найденными с
  /// помощью условий выборки.
  /// </summary>
  /// <param name="conditionProvider">Провайдер условий, предоставляющий условия выборки.</param>
  /// <returns>Ссылка на интерфейс части элемента навигации.</returns>
  public INodePart GetPart(IConditionsProvider conditionProvider)
  {
    bool flag1 = false;
    bool flag2 = false;
    ConditionStructure[] conditions = conditionProvider.GetConditions();
    if (conditions != null)
    {
      for (int index = 0; index < conditions.Length; ++index)
      {
        object attribute = conditions[index].Attribute;
        int num = 0;
        switch (attribute)
        {
          case ObligatoryObjectAttributes _:
          case int _:
            num = (int) attribute;
            break;
          case Guid guid:
            if (guid == new Guid("cad00034-306c-11d8-b4e9-00304f19f545"))
            {
              num = -21;
              break;
            }
            if ((Guid) attribute == new Guid("cad00035-306c-11d8-b4e9-00304f19f545"))
            {
              num = -22;
              break;
            }
            break;
        }
        if (num == -21)
          flag1 = true;
        if (num == -22)
          flag2 = true;
        if (flag1 & flag2)
          break;
      }
    }
    if (flag1 | flag2)
    {
      RelatedObjectsPart part = new RelatedObjectsPart(conditionProvider, (IServiceProvider) null);
      part.AcceptManagedEvents = false;
      return (INodePart) part;
    }
    ObjectsPart part1 = new ObjectsPart(conditionProvider, (IServiceProvider) null);
    part1.AcceptManagedEvents = false;
    return (INodePart) part1;
  }
}
