// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailNodeBinding
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

internal class MailNodeBinding : ITopBinding, IBinding
{
  private ConditionStructure[] _topConditions;
  private BindingType _bindingType;
  private MailBoxNode _node;

  public MailNodeBinding(MailBoxNode node, BindingType bindingType)
  {
    this._node = node;
    this._bindingType = bindingType;
  }

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
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4SelectionNode(this._bindingType, 7));
            break;
          case BindingType.Classificators:
            conditionStructureList.AddRange((IEnumerable<ConditionStructure>) BindingHelper.GetBindingConditions4ClassifierNode(7));
            break;
        }
        conditionStructureList.Add(new ConditionStructure(wfConsts.AttrMailFolderID, RelationalOperators.Equal, (object) (int) this._node.MailType, (object) null, LogicalOperators.NONE, 0, false));
        this._topConditions = conditionStructureList.ToArray();
      }
      return this._topConditions;
    }
  }

  public void BindSelection(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(selObjectID);
      if (objectInfo.Empty)
        return;
      List<AttributeValues> attributeValuesList = new List<AttributeValues>();
      int kindAttributeId = Intermech.Navigator.Selections.Consts.GetKindAttributeID(objectInfo.ObjectTypeID);
      int[] attributesID = new int[3]
      {
        kindAttributeId,
        wfConsts.AttrMailFolderID,
        wfConsts.AttrObjectTypesID
      };
      AttributeValues[] attributesValues = sessionKeeper.Session.GetObjectAttributesValues(selObjectID, attributesID, GetAttributeValuesModes.None, false);
      for (int index = 0; index < attributesValues.Length; ++index)
      {
        AttributeValues attributeValues1 = attributesValues[index];
        int attributeID = attributeValues1 != null ? attributeValues1.AttributeID : attributesID[index];
        if (attributeID == kindAttributeId)
        {
          AttributeValues attributeValues2 = new AttributeValues(attributeID, (object) 7);
          attributeValuesList.Add(attributeValues2);
        }
        else if (attributeID == wfConsts.AttrMailFolderID)
        {
          AttributeValues attributeValues3 = new AttributeValues(attributeID, (object) (int) this._node.MailType);
          attributeValuesList.Add(attributeValues3);
        }
        else if (attributeID == wfConsts.AttrObjectTypesID)
        {
          IMSObjectType objectType = MetaDataHelper.GetObjectType(this._node.ElementsTypeID);
          if (objectType != null)
          {
            AttributeValues attributeValues4 = new AttributeValues(attributeID, (object) new object[1]
            {
              (object) objectType.Guid
            });
            attributeValuesList.Add(attributeValues4);
          }
        }
      }
      sessionKeeper.Session.SetObjectAttributesValues(selObjectID, false, attributeValuesList.ToArray());
    }
  }

  public string GetCaption(int selTypeID) => Intermech.Navigator.DBObjectTypes.Helper.GetObjectTypeName(selTypeID);

  public object GetData(Type dataFormat) => (object) null;

  public BindingType BindingType => this._bindingType;

  public ConditionStructure[] GetConditions(long selObjectID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((ISelectionsService) ApplicationServices.Container.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, selObjectID);
  }

  public INodePart GetPart(IConditionsProvider conditionProvider)
  {
    return this._node.GetPart(conditionProvider);
  }

  public string ViewCaption => LocalizationHolder.rm.GetString("Workflow.Client_11");
}
