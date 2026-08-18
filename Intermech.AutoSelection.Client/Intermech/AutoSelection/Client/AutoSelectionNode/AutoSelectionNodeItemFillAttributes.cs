// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeItemFillAttributes
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.Expert;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public abstract class AutoSelectionNodeItemFillAttributes : AutoSelectionNodeCommon
{
  private protected readonly AutoSelAttrValList _defRelAttrList;
  private protected readonly AutoSelAttrList _calcRelAttrList;
  private protected readonly AutoSelAttrValList _defObjAttrList;
  private protected readonly AutoSelAttrList _calcObjAttrList;

  public AutoSelectionNodeItemFillAttributes(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._defRelAttrList = new AutoSelAttrValList(AutoSelAttrTypeMode.asatRelationType, this);
    this._defObjAttrList = new AutoSelAttrValList(AutoSelAttrTypeMode.asatObjectType, this);
    this._calcRelAttrList = new AutoSelAttrList(AutoSelAttrTypeMode.asatRelationType, this);
    this._calcObjAttrList = new AutoSelAttrList(AutoSelAttrTypeMode.asatObjectType, this);
  }

  private protected void AttributesObjectSetDefault(
    AutoSelectionSession asSession,
    IDBObject dbObject,
    List<AutoSelAttrVal> attrValList)
  {
    if (dbObject == null)
      return;
    IDBObjectType objectType = dbObject.Session.GetObjectType(dbObject.ObjectType, false);
    if (objectType == null)
      return;
    if (objectType.AnyAttributes)
    {
      this.AttributesSetDefault(asSession, (IDBAttributable) dbObject, attrValList);
    }
    else
    {
      List<AutoSelAttrVal> selAttrValList = new List<AutoSelAttrVal>();
      foreach (AutoSelAttrVal attrVal in attrValList)
      {
        if (attrVal != null)
        {
          int attributeId = MetaDataHelper.GetAttributeID((object) attrVal.AttrGuid.ToString());
          if (attributeId != 0 && objectType.HasAttribute(attributeId))
            selAttrValList.Add(attrVal);
        }
      }
      if (selAttrValList.Count == 0)
        return;
      this.AttributesSetDefault(asSession, (IDBAttributable) dbObject, selAttrValList);
    }
  }

  private protected void AttributesRelationSetDefault(
    AutoSelectionSession asSession,
    IDBRelation dbRelation,
    List<AutoSelAttrVal> attrValList)
  {
    if (dbRelation == null)
      return;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(dbRelation.RelationType);
    if (relationType == null)
      return;
    if (relationType.AnyAttributes)
    {
      this.AttributesSetDefault(asSession, (IDBAttributable) dbRelation, attrValList);
    }
    else
    {
      List<AutoSelAttrVal> selAttrValList = new List<AutoSelAttrVal>();
      foreach (AutoSelAttrVal attrVal in attrValList)
      {
        if (attrVal != null)
        {
          int attributeId = MetaDataHelper.GetAttributeID((object) attrVal.AttrGuid.ToString());
          if (attributeId != 0 && MetaDataHelper.GetAttribute4RelationType(dbRelation.RelationType, attributeId) != null)
            selAttrValList.Add(attrVal);
        }
      }
      if (selAttrValList.Count == 0)
        return;
      this.AttributesSetDefault(asSession, (IDBAttributable) dbRelation, selAttrValList);
    }
  }

  private void AttributesSetDefault(
    AutoSelectionSession asSession,
    IDBAttributable attributable,
    List<AutoSelAttrVal> selAttrValList)
  {
    if (attributable == null)
      throw new ArgumentNullException(nameof (attributable));
    if (selAttrValList == null || selAttrValList.Count == 0)
      return;
    List<AttributeValues> attributeValuesList = new List<AttributeValues>();
    foreach (AutoSelAttrVal selAttrVal in selAttrValList)
    {
      if (selAttrVal.AttrMode.Equals((object) AutoSelectionAttrMode.SkipExists))
      {
        IDBAttribute attributeByGuid = attributable.GetAttributeByGuid(selAttrVal.AttrGuid, false);
        if (attributeByGuid != null && !attributeByGuid.IsNull)
          continue;
      }
      int attributeId = MetaDataHelper.GetAttributeID((object) selAttrVal.AttrGuid.ToString());
      if (attributeId != 0 && selAttrVal.AttrValue != null)
      {
        object initValue = selAttrVal.AttrValue;
        if (initValue is ObjectPropertyClass)
          initValue = (object) ((ObjectPropertyClass) initValue).ObjectID;
        AttributeValues attributeValues = new AttributeValues(attributeId, initValue);
        attributeValuesList.Add(attributeValues);
      }
    }
    if (attributeValuesList.Count == 0)
      return;
    attributable.SetAttributesValues(attributeValuesList.ToArray());
  }

  private protected void AttributesCalc(
    AutoSelectionSession asSession,
    IDBAttributable dbAttributable,
    List<AutoSelAttr> selAttrList)
  {
    if (dbAttributable == null || selAttrList.Count == 0)
      return;
    long objId;
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        objId = dbObject.ObjectID;
        break;
      case IDBRelation dbRelation:
        objId = dbRelation.RelationID;
        break;
      default:
        return;
    }
    IExpertUser expertUserService = AutoSelectionUtils.ServiceKeeper.GetExpertUserService();
    if (expertUserService == null)
      return;
    foreach (AutoSelAttr selAttr in selAttrList)
    {
      int attributeId = MetaDataHelper.GetAttributeID((object) selAttr.AttrGuid);
      if (attributeId != 0)
      {
        IExpertTask expertTask = expertUserService.GetExpertTask();
        expertTask.EndCalculate += new EndCalculateEventHandler(this.EndCalculateEvent);
        expertTask.Calculate(dbAttributable.TypeID, attributeId, objId, asSession.ContextInfo.ObjectIds.ToArray<long>());
      }
    }
  }

  private void EndCalculateEvent(object sender, EndCalculateEventArgs e)
  {
    if (!(sender is IExpertTask expertTask))
      return;
    try
    {
      if (e.result == ExpertResult.OK)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (!ServiceUtils.GetService<IDBTransactions>((object) sessionKeeper.Session, true).InTransaction)
          {
            expertTask.ApplyCalcParms();
          }
          else
          {
            List<CalculatedAttr> list = expertTask.GetCalcParms().Values.Where<CalculatedAttr>((Func<CalculatedAttr, bool>) (item => item.ca_pair.objID != -1L)).ToList<CalculatedAttr>();
            if (list.Count != 0)
            {
              IExpertServer service = ServiceUtils.GetService<IExpertServer>((object) sessionKeeper.Session, true);
              int taskId = service.StartTask(sessionKeeper.Session.SessionGUID);
              try
              {
                service.ApplyCalcParms(taskId, list);
              }
              finally
              {
                service.EndTask(taskId);
              }
            }
          }
        }
      }
      if (AutoSelectionUtils.ServiceKeeper.GetExpertUserService().ShowTraceWindow)
      {
        lock (ExpertUser.rur)
          ExpertUser.rur.Execute(expertTask.GetTraceInfo(), true);
      }
      expertTask.EndCalculate -= new EndCalculateEventHandler(this.EndCalculateEvent);
    }
    finally
    {
      expertTask.Dispose();
    }
  }

  [CustomDisplayName("Attribute.AutoSelection.Client_28")]
  [CustomDescription("Attribute.AutoSelection.Client_29")]
  [CustomCategory("Attribute.AutoSelection.Client_30")]
  [TypeConverter(typeof (AutoSelAttrCollTypeConverter))]
  [Editor(typeof (AutoSelAttrCollEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  [ReadOnly(false)]
  public List<AutoSelAttrVal> DefRelAttrList => (List<AutoSelAttrVal>) this._defRelAttrList;

  [CustomDisplayName("Attribute.AutoSelection.Client_28")]
  [CustomDescription("Attribute.AutoSelection.Client_31")]
  [CustomCategory("Attribute.AutoSelection.Client_32")]
  [TypeConverter(typeof (AutoSelAttrCollTypeConverter))]
  [Editor(typeof (AutoSelAttrCollEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  [ReadOnly(false)]
  public List<AutoSelAttrVal> DefObjAttrList => (List<AutoSelAttrVal>) this._defObjAttrList;

  [CustomDisplayName("Attribute.AutoSelection.Client_33")]
  [CustomDescription("Attribute.AutoSelection.Client_34")]
  [CustomCategory("Attribute.AutoSelection.Client_30")]
  [TypeConverter(typeof (AutoSelAttrCollTypeConverter))]
  [Editor(typeof (AutoSelAttrCollEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  [ReadOnly(false)]
  public List<AutoSelAttr> CalcRelAttrList => (List<AutoSelAttr>) this._calcRelAttrList;

  [CustomDisplayName("Attribute.AutoSelection.Client_33")]
  [CustomDescription("Attribute.AutoSelection.Client_35")]
  [CustomCategory("Attribute.AutoSelection.Client_32")]
  [TypeConverter(typeof (AutoSelAttrCollTypeConverter))]
  [Editor(typeof (AutoSelAttrCollEditor), typeof (UITypeEditor))]
  [DefaultValue(null)]
  [ReadOnly(false)]
  public List<AutoSelAttr> CalcObjectAttrList => (List<AutoSelAttr>) this._calcObjAttrList;

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    XmlNode newChild1 = this._defRelAttrList.Save("DefRelAttrList", doc);
    if (newChild1 != null)
      xmlNode.AppendChild(newChild1);
    XmlNode newChild2 = this._defObjAttrList.Save("DefObjAttrList", doc);
    if (newChild2 != null)
      xmlNode.AppendChild(newChild2);
    XmlNode newChild3 = this._calcRelAttrList.Save("CalcRelAttrList", doc);
    if (newChild3 != null)
      xmlNode.AppendChild(newChild3);
    XmlNode newChild4 = this._calcObjAttrList.Save("CalcObjAttrList", doc);
    if (newChild4 != null)
      xmlNode.AppendChild(newChild4);
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node?.Attributes == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name.Equals("DefRelAttrList"))
        this._defRelAttrList.Load("DefRelAttrList", childNode);
      else if (childNode.Name.Equals("DefObjAttrList"))
        this._defObjAttrList.Load("DefObjAttrList", childNode);
      else if (childNode.Name.Equals("CalcRelAttrList"))
        this._calcRelAttrList.Load("CalcRelAttrList", childNode);
      else if (childNode.Name.Equals("CalcObjAttrList"))
        this._calcObjAttrList.Load("CalcObjAttrList", childNode);
    }
    return (AutoSelectionNodeCommon) this;
  }

  public override ICollection<Guid> CollectMetaDataGuids(
    IMSGlobals type,
    ICollection<Guid> collector)
  {
    base.CollectMetaDataGuids(type, collector);
    this._defObjAttrList.CollectMetaDataGuids(type, collector);
    this._defRelAttrList.CollectMetaDataGuids(type, collector);
    return collector;
  }
}
