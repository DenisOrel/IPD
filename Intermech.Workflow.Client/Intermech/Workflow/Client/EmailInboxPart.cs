// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailInboxPart
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

internal class EmailInboxPart : ObjectsPart
{
  private string _accauntEmail;
  private IServiceProvider _services;
  private IConditionsProvider _conditionsProvider;
  private int _emailTypeID;
  private ConditionStructure[] _conditionsCache;

  public EmailInboxPart(IServiceProvider services, string accauntEmail)
    : base(services)
  {
    this._services = services;
    this._accauntEmail = accauntEmail;
    this._emailTypeID = MetaDataHelper.GetObjectTypeID(wfConsts.objtypeEmailMessages);
  }

  public EmailInboxPart(
    IServiceProvider services,
    IConditionsProvider conditionsProvider,
    string accauntEmail)
    : this(services, accauntEmail)
  {
    this._conditionsProvider = conditionsProvider;
  }

  protected new ConditionStructure[] Conditions
  {
    get
    {
      if (this._conditionsProvider != null && this._conditionsProvider.ConditionsChanged)
        this._conditionsCache = (ConditionStructure[]) null;
      if (this._conditionsCache == null && this._conditionsProvider != null)
        this._conditionsCache = this._conditionsProvider.GetConditions();
      return this._conditionsCache;
    }
  }

  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    conditions = ConditionStructure.Join(new ConditionStructure(wfConsts.attributeEmail, RelationalOperators.Equal, (object) this._accauntEmail, LogicalOperators.AND, 0), conditions);
    ConditionStructure[] conditions1 = this.Conditions;
    if (conditions1 != null)
      conditions = ConditionStructure.Join(conditions1, conditions);
    IServiceProvider services = this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null;
    return (INodeQuery) new ObjectsQuery((INodeQuerySupport) this, this._emailTypeID, conditions, services);
  }

  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    NodeID nodeId = base.CreateNodeId(fieldValues, adapter) as NodeID;
    string messageID = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) wfConsts.attributeMessageID, AttributeSourceTypes.Object))]);
    string inReplyTo = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) wfConsts.attributeInReplyToID, AttributeSourceTypes.Object))]);
    object fieldValue = fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) wfConsts.attributeOfficeDocumentID, AttributeSourceTypes.Object))];
    return (INodeID) new EmailMessageNodeID(nodeId.ObjectTypeID, nodeId.ObjectID, nodeId.ID, nodeId.CheckedOutBy, nodeId.PrjLinkID, nodeId.LCStepID, nodeId.Caption, nodeId.RelationTypeID, nodeId.Owner, nodeId.Sorting, nodeId.State, nodeId.Version, nodeId.BaseVersion, nodeId.SiteID, nodeId.ModificationID, messageID, inReplyTo, fieldValue != DBNull.Value ? Convert.ToInt64(fieldValue) : 0L);
  }

  public override object CreateRecordId(INodeID nodeId)
  {
    switch (nodeId)
    {
      case EmailMessageNodeID _:
        return (object) ((NodeID) nodeId).ObjectID;
      case NodeID _:
        return (object) ((NodeID) nodeId).ObjectID;
      default:
        return (object) null;
    }
  }

  internal static NodeColumnCollection DefaultColumns
  {
    get
    {
      IColumnSchemes service = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
      return new NodeColumnCollection()
      {
        service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID),
        service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) wfConsts.AttrSubjectID),
        service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) wfConsts.attributeEmailDataID),
        service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) wfConsts.attributeSenderID),
        service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) wfConsts.attributeEmailSenderID)
      };
    }
  }

  public override NodeColumnCollection GetDefaultColumns() => EmailInboxPart.DefaultColumns;

  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    specialFields.Add((object) new NodeColumnID((object) wfConsts.attributeMessageID, AttributeSourceTypes.Object));
    specialFields.Add((object) new NodeColumnID((object) wfConsts.attributeInReplyToID, AttributeSourceTypes.Object));
    specialFields.Add((object) new NodeColumnID((object) wfConsts.attributeOfficeDocumentID, AttributeSourceTypes.Object));
    return specialFields;
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return base.GetData(nodeID, dataFormat);
  }
}
