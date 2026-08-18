
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesBinding
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


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>ObjectTypesBinding: привязки к узлу</summary>
internal sealed class ObjectTypesBinding : ITopBinding, IBinding
{
  /// <summary>Условия</summary>
  private ConditionStructure[] _topConditions;

  public ObjectTypesBinding(BindingType bindingType) => this.BindingType = bindingType;

  public ConditionStructure[] TopConditions
  {
    get
    {
      if (this._topConditions == null)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(2);
        switch (this.BindingType)
        {
          case BindingType.Selections:
          case BindingType.CommonSelections:
          case BindingType.PersonalSelections:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this.BindingType, 4));
            break;
          case BindingType.Classificators:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4ClassifierNode(4));
            conditionStructureList.Add(new ConditionStructure(-7, RelationalOperators.NotEqual, (object) MetaDataHelper.GetObjectTypeID("cad00150-306c-11d8-b4e9-00304f19f545"), LogicalOperators.AND, 0, false));
            break;
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
      int kindAttributeId = Intermech.Navigator.Selections.Consts.GetKindAttributeID(dbObject.ObjectType);
      (dbObject.GetAttributeByID(kindAttributeId) ?? dbObject.Attributes.AddAttribute(kindAttributeId, false)).Value = (object) 4;
    }
  }

  public string GetCaption(int selTypeID) => Helper.GetObjectTypeName(selTypeID);

  public object GetData(Type dataFormat) => (object) null;

  public BindingType BindingType { get; }

  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID);
  }

  public string ViewCaption => LocalizationHolder.rm.GetString("Client.Core_307");

  public INodePart GetPart(IConditionsProvider conditionProvider)
  {
    ObjectsPart part = new ObjectsPart(conditionProvider, (IServiceProvider) null);
    part.AcceptManagedEvents = false;
    return (INodePart) part;
  }
}
