
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypeBinding
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
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Реализует привязку дерева выборок к элементу навигации "Тип объектов".
/// </summary>
public class ObjectTypeBinding : ITopBinding, IBinding, IBindingStateStream
{
  protected int objTypeID;
  protected BindingType bindingType;
  protected ConditionStructure[] topConditions;

  public ObjectTypeBinding(int objTypeID, BindingType bindingType)
  {
    this.objTypeID = objTypeID;
    this.bindingType = bindingType;
    this.topConditions = (ConditionStructure[]) null;
  }

  public int ObjTypeID
  {
    [DebuggerStepThrough] get => this.objTypeID;
  }

  public BindingType BindingType
  {
    [DebuggerStepThrough] get => this.bindingType;
  }

  public ConditionStructure[] TopConditions
  {
    get
    {
      if (this.topConditions == null)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(this.objTypeID);
        if (objectType != null)
        {
          List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(3);
          switch (this.bindingType)
          {
            case BindingType.Selections:
            case BindingType.CommonSelections:
            case BindingType.PersonalSelections:
              conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this.bindingType, 3));
              break;
            case BindingType.Classificators:
              conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4ClassifierNode(3));
              break;
          }
          conditionStructureList.Add(new ConditionStructure(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, RelationalOperators.Equal, (object) objectType.Guid.ToString(), LogicalOperators.NONE, 0, false));
          this.topConditions = conditionStructureList.ToArray();
        }
      }
      return this.topConditions;
    }
  }

  public void BindSelection(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(selObjectID);
      dbObject.Attributes.AddAttribute(Intermech.Navigator.Selections.Consts.GetKindAttributeID(dbObject.ObjectType), false).Value = (object) 3;
      Guid guid = ((IDBGuid) sessionKeeper.Session.GetObjectType(this.objTypeID)).GUID;
      IDBAttribute byId = dbObject.Attributes.FindByID(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID);
      if (byId == null)
      {
        dbObject.Attributes.AddAttribute(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, false, new object[1]
        {
          (object) guid
        });
      }
      else
      {
        object[] values = byId.Values;
        if (values != null && values.Length == 1 && !GuidHelper.IsGuid(values[0].ToString()))
        {
          byId.Value = (object) guid;
        }
        else
        {
          if (values != null)
          {
            for (int index = 0; index < values.Length; ++index)
            {
              if (values[index].Equals((object) guid))
                return;
            }
          }
          byId.AddValue((object) guid);
        }
      }
    }
  }

  public string GetCaption(int selTypeID) => Helper.GetObjectTypeName(selTypeID);

  public object GetData(Type dataFormat)
  {
    return dataFormat == typeof (IDBTypedObjectID) ? (object) new DBTypedObjectID(this.objTypeID, 0L, 0L, string.Empty, 0L, 0L, 0L, string.Empty, 0L) : (object) null;
  }

  public virtual ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID);
  }

  public INodePart GetPart(IConditionsProvider conditionsProvider)
  {
    ObjectsPart part = new ObjectsPart(this.objTypeID, conditionsProvider, (IServiceProvider) null);
    part.AcceptManagedEvents = false;
    return (INodePart) part;
  }

  public string ViewCaption => Helper.GetObjectTypeName(this.objTypeID);

  public int CategoryID => 4;

  public int CategoryType => this.objTypeID;

  public string Prefix => (string) null;
}
