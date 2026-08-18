// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeItemCommon
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Design;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public class AutoSelectionNodeItemCommon : AutoSelectionNodeItemFillAttributes, IItemCommon
{
  private AS_Guid _objTypeGuid;
  private AS_Guid _relTypeGuid;
  private AutoSelectionExecObjMode _execObjMode;
  private bool _editCreatedObject;
  private AutoSelectionMandatoryMode _mandatoryMode;

  protected void CreatedSelectionObject_Edit(
    AutoSelectionSession asSession,
    AutoSelectionObject asObject)
  {
    if (!this.EditCreatedObject)
      return;
    ISelectedItems items = RelationExtensions.GetItems(new Dictionary<long, List<long>>()
    {
      {
        asObject.CreatedRelnfo.ProjInfo.ObjectID,
        new List<long>() { asObject.CreatedRelnfo.RelationID }
      }
    });
    ServiceContainer viewServices1 = new ServiceContainer();
    viewServices1.AddService(typeof (AutoSelectionMode), (object) asSession.Params.Mode);
    ServiceContainer viewServices2 = viewServices1;
    CommandsTable commandsTable = Services.GetCommandsTable(items, (IServiceProvider) viewServices2);
    if (!commandsTable.Contains("ParametersCard"))
      return;
    Services.InvokeCommand("ParametersCard", commandsTable, (IServiceProvider) viewServices1);
  }

  protected void CreatedSelectionObject_RunAutoSelection(
    AutoSelectionSession asSession,
    AutoSelectionObject asObject)
  {
    if (asSession == null)
      throw new ArgumentNullException(nameof (asSession));
    if (asObject == null)
      throw new ArgumentNullException(nameof (asObject));
    if (AutosSelectConsts.Config.DelayedObjectCreation || !asObject.NeedAutoSelection)
      return;
    RelObjInfoItem createdRelnfo = asObject.CreatedRelnfo;
    if ((TypedInfoItem) createdRelnfo == (TypedInfoItem) null || createdRelnfo.RelationID == 0L)
      return;
    List<AutoSelectionObject> createdObjList = new List<AutoSelectionObject>()
    {
      asObject
    };
    asSession.ExecuteRules_AutoSelect4CreatedData(createdObjList);
  }

  public AutoSelectionNodeItemCommon(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._objTypeGuid = new AS_Guid();
    this._relTypeGuid = new AS_Guid();
  }

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    if (!this._objTypeGuid.Equals((object) Guid.Empty))
    {
      XmlAttribute attribute = doc.CreateAttribute("ObjectTypeGuid");
      attribute.Value = this._objTypeGuid.ToString();
      xmlNode.Attributes?.Append(attribute);
    }
    if (!this._relTypeGuid.Equals((object) Guid.Empty))
    {
      XmlAttribute attribute = doc.CreateAttribute("RelationTypeGuid");
      attribute.Value = this._relTypeGuid.ToString();
      xmlNode.Attributes?.Append(attribute);
    }
    XmlNode newChild1 = AutoSelEnumUtils.Save("Mandatory", (int) this._mandatoryMode, EnumTypeHelper.GetCaption((Enum) this._mandatoryMode), doc);
    xmlNode.AppendChild(newChild1);
    XmlNode newChild2 = AutoSelEnumUtils.Save("ExecObjMode", (int) this._execObjMode, EnumTypeHelper.GetCaption((Enum) this._execObjMode), doc);
    xmlNode.AppendChild(newChild2);
    XmlAttribute attribute1 = doc.CreateAttribute("EditCreatedObject");
    attribute1.Value = Convert.ToInt32(this._editCreatedObject).ToString();
    xmlNode.Attributes?.Append(attribute1);
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node?.Attributes == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    XmlAttribute attribute1 = node.Attributes["ObjectTypeGuid"];
    if (attribute1 != null)
    {
      this._objTypeGuid = new AS_Guid(new Guid(attribute1.Value));
      MetaDataHelper.GetObjectType(this._objTypeGuid.Value);
    }
    XmlAttribute attribute2 = node.Attributes["RelationTypeGuid"];
    if (attribute2 != null)
    {
      this._relTypeGuid = new AS_Guid(new Guid(attribute2.Value));
      MetaDataHelper.GetRelationType(this._relTypeGuid.Value);
    }
    int id1;
    AutoSelEnumUtils.Load("Mandatory", node, out id1);
    this._mandatoryMode = (AutoSelectionMandatoryMode) id1;
    int id2;
    AutoSelEnumUtils.Load("ExecObjMode", node, out id2);
    this._execObjMode = (AutoSelectionExecObjMode) id2;
    XmlAttribute attribute3 = node.Attributes["EditCreatedObject"];
    if (attribute3 != null)
      this._editCreatedObject = Convert.ToInt32(attribute3.Value) == 1;
    return (AutoSelectionNodeCommon) this;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_22")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_23")]
  [TypeConverter(typeof (ObjectTypeConverter))]
  [Editor(typeof (SelectionObjectTypeEditor), typeof (UITypeEditor))]
  [RefreshProperties(RefreshProperties.All)]
  public AS_Guid ObjTypeGuid
  {
    get => this._objTypeGuid;
    set
    {
      if (this._objTypeGuid.CompareTo((object) value) == 0)
        return;
      this._objTypeGuid = value;
      this.Name = MetaDataHelper.GetObjectTypeName(this._objTypeGuid.Value);
    }
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_24")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_25")]
  [TypeConverter(typeof (RelationTypeConverter))]
  [Editor(typeof (SelectionRelationTypeEditor), typeof (UITypeEditor))]
  public AS_Guid RelTypeGuid
  {
    get => this._relTypeGuid;
    set => this._relTypeGuid = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_26")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_27")]
  public AutoSelectionMandatoryMode MandatoryMode
  {
    get => this._mandatoryMode;
    set => this._mandatoryMode = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_91")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_92")]
  public AutoSelectionExecObjMode ExecObjMode
  {
    get => this._execObjMode;
    set => this._execObjMode = value;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_94")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_95")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool EditCreatedObject
  {
    [DebuggerStepThrough] get => this._editCreatedObject;
    [DebuggerStepThrough] set => this._editCreatedObject = value;
  }

  public override ObjInfoItem GetProjectObjInfo(AutoSelectionSession asSession)
  {
    if (asSession == null)
      throw new ArgumentNullException(nameof (asSession));
    ObjInfoItem projectObjInfo = (ObjInfoItem) null;
    switch (this.ExecObjMode)
    {
      case AutoSelectionExecObjMode.CurrentObject:
        projectObjInfo = asSession.TargetObjInfo;
        break;
      case AutoSelectionExecObjMode.ParentObject:
        projectObjInfo = asSession.TargetProjInfo;
        break;
    }
    return projectObjInfo;
  }

  public override bool AnalyzeObject(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec,
    ObjInfoItem targetObject)
  {
    if (!base.AnalyzeObject(asSession, logRec, targetObject))
      return false;
    IMSObjectType objectType1 = MetaDataHelper.GetObjectType(this.ObjTypeGuid.Value);
    if (objectType1 == null)
    {
      string data = string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_80"), (object) this.ObjTypeGuid.Value);
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data);
      return false;
    }
    IMSRelationType relationType = MetaDataHelper.GetRelationType(this.RelTypeGuid.Value);
    if (relationType == null)
    {
      string data = string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_81"), (object) this.RelTypeGuid.Value);
      asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data);
      return false;
    }
    if (MetaDataHelper.GetApplicability(targetObject.ObjTypeID, objectType1.ObjectTypeID, relationType.RelationTypeID) != null)
      return true;
    IMSObjectType objectType2 = MetaDataHelper.GetObjectType(targetObject.ObjTypeID);
    string data1 = string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_82"), (object) relationType.TypeName, (object) relationType.RelationTypeID, (object) objectType2.ObjectTypeName, (object) objectType2.ObjectTypeID, (object) objectType1.ObjectTypeName, (object) objectType1.ObjectTypeID);
    asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data1);
    return false;
  }

  protected override bool GetRelationType(int projTypeId, out int relationTypeId)
  {
    relationTypeId = -1;
    if (this.RelTypeGuid == null || !this.RelTypeGuid.Value.Equals(Guid.Empty))
    {
      relationTypeId = MetaDataHelper.GetRelationTypeID(this.RelTypeGuid?.Value.ToString());
    }
    else
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(projTypeId);
      if (objectType == null)
        return false;
      relationTypeId = objectType.DefaultRelation;
    }
    return true;
  }

  public override IDBRelation CreateRelation(
    AutoSelectionSession asSession,
    IDBObject projObject,
    ObjInfoItem partInfo,
    AttributeValues[] sortAttrValues = null)
  {
    IUserSession userSession = projObject != null ? projObject.Session : throw new ArgumentNullException(nameof (projObject));
    int relationTypeId;
    if (!this.GetRelationType(projObject.ObjectType, out relationTypeId))
      return (IDBRelation) null;
    IDBRelationCollection relationCollection = userSession.GetRelationCollection(relationTypeId);
    if (relationCollection == null)
      return (IDBRelation) null;
    IDBObject objectActualCopy = userSession.GetObjectActualCopy(partInfo.ObjectID, false);
    if (objectActualCopy == null)
    {
      AutoSelectionUtils.Output.WriteString(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_87"), (object) MetaDataHelper.GetRelationTypeName(relationTypeId), (object) projObject.Caption, (object) projObject.ObjectID, (object) MetaDataHelper.GetObjectTypeName(partInfo.ObjTypeID), (object) partInfo.ObjectID));
      return (IDBRelation) null;
    }
    partInfo = new ObjInfoItem(objectActualCopy);
    if (!this.ValidateRelation(ref projObject, ref partInfo, relationTypeId))
    {
      AutoSelectionUtils.Output.WriteString(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_89"), (object) MetaDataHelper.GetRelationTypeName(relationTypeId), (object) projObject.Caption, (object) projObject.ObjectID, (object) objectActualCopy.Caption, (object) objectActualCopy.ObjectID, (object) MetaDataHelper.GetObjectTypeName(projObject.ObjectType), (object) MetaDataHelper.GetObjectTypeName(objectActualCopy.ObjectType)));
      return (IDBRelation) null;
    }
    IDBRelation dbRelation = relationCollection.Create(projObject.ObjectID, partInfo.ObjectID, MetaDataHelper.GetAttribute4RelationType(relationTypeId, Intermech.Imbase.Consts.ObjectSortOrderAttID) != null ? sortAttrValues : (AttributeValues[]) null);
    this.AttributesRelationSetDefault(asSession, dbRelation, (List<AutoSelAttrVal>) this._defRelAttrList);
    this.AttributesCalc(asSession, (IDBAttributable) dbRelation, (List<AutoSelAttr>) this._calcRelAttrList);
    return dbRelation;
  }

  public override AutoSelExecuteStatus Execute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.Execute(asSession, logRec);
    return selExecuteStatus != AutoSelExecuteStatus.Skipped || this.MandatoryMode != AutoSelectionMandatoryMode.Mandatory ? selExecuteStatus : AutoSelExecuteStatus.SkipOwnerLevel;
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    return selExecuteStatus != AutoSelExecuteStatus.Applied ? selExecuteStatus : AutoSelExecuteStatus.Applied;
  }

  protected override AutoSelExecuteStatus DoExecuteCondition(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecuteCondition(asSession, logRec);
    return selExecuteStatus == AutoSelExecuteStatus.Applied || this.MandatoryMode != AutoSelectionMandatoryMode.Mandatory ? selExecuteStatus : AutoSelExecuteStatus.SkipOwnerLevel;
  }

  public override ICollection<Guid> CollectMetaDataGuids(
    IMSGlobals type,
    ICollection<Guid> collector)
  {
    base.CollectMetaDataGuids(type, collector);
    if (collector.IsReadOnly)
      return collector;
    if (type == IMSGlobals.IMSObjectType || type == IMSGlobals.Unknown)
      AddIf(this._objTypeGuid);
    if (type == IMSGlobals.IMSRelationType || type == IMSGlobals.Unknown)
      AddIf(this._relTypeGuid);
    return collector;

    void AddIf(AS_Guid asg)
    {
      if (asg.Value.Equals(Guid.Empty) || collector.Contains(asg.Value))
        return;
      collector.Add(asg.Value);
    }
  }
}
