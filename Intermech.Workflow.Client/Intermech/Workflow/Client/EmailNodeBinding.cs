// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailNodeBinding
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

internal class EmailNodeBinding : ITopBinding, IBinding
{
  private ConditionStructure[] _topConditions;
  private string _accauntEmail = string.Empty;

  public EmailNodeBinding(string accauntEmail) => this._accauntEmail = accauntEmail;

  public ConditionStructure[] TopConditions
  {
    get
    {
      if (this._topConditions == null)
      {
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>();
        conditionStructureList.AddRange((IEnumerable<ConditionStructure>) new ConditionStructure[4]
        {
          new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.AND, 0, false),
          new ConditionStructure(Intermech.Navigator.Selections.Consts.KindSelectionAttrID, RelationalOperators.Equal, (object) 3, LogicalOperators.AND, 0, false),
          new ConditionStructure(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, RelationalOperators.Equal, (object) wfConsts.objtypeEmailMessages.ToString(), LogicalOperators.AND, 0, false),
          new ConditionStructure(wfConsts.attributeEmailID, RelationalOperators.Equal, (object) this._accauntEmail, LogicalOperators.NONE, 0, false)
        });
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
      dbObject.Attributes.FindByID(Intermech.Navigator.Selections.Consts.KindSelectionAttrID).Value = (object) 3;
      (dbObject.Attributes.FindByID(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID) ?? dbObject.Attributes.AddAttribute(Intermech.Navigator.Selections.Consts.ObjectTypesAttrID, false)).AsString = wfConsts.objtypeEmailMessages.ToString();
      (dbObject.Attributes.FindByID(wfConsts.attributeEmailID) ?? dbObject.Attributes.AddAttribute(wfConsts.attributeEmailID, false)).AsString = this._accauntEmail;
    }
  }

  public string GetCaption(int selTypeID) => Intermech.Navigator.DBObjectTypes.Helper.GetObjectTypeName(selTypeID);

  public object GetData(Type dataFormat) => (object) null;

  public BindingType BindingType => BindingType.Selections;

  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ISelectionsService) ApplicationServices.Container.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID);
  }

  public INodePart GetPart(IConditionsProvider conditionsProvider)
  {
    return (INodePart) new EmailInboxPart((IServiceProvider) null, conditionsProvider, this._accauntEmail);
  }

  public string ViewCaption => MetaDataHelper.GetObjectTypeName(wfConsts.objtypeEmailMessages);
}
