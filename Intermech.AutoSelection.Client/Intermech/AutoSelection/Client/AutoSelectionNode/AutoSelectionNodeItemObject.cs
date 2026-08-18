// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeItemObject
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.AutoSelection.Client.Converters_Editors;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

[TypeConverter(typeof (AutoSelectionNodeItemObjectConverter))]
public class AutoSelectionNodeItemObject : AutoSelectionNodeItemCommon
{
  private AS_Long _itemObjectId;
  private string _itemObjectCaption = string.Empty;
  private AutoSelectonItemObjectMode _itemObjectMode;

  private void InitializeData() => this._type = AutoSelectionNodeType.ItemObject;

  private void SetItemObjectID(AS_Long value, bool updateLinkMode)
  {
    if (this._itemObjectId.Equals((object) value))
      return;
    this._itemObjectId = value;
    if (!updateLinkMode)
      return;
    AutoSelectionUtils.Common.UpdateNodesLinkCaptions(new List<AutoSelectionNodeBase>()
    {
      (AutoSelectionNodeBase) this
    });
  }

  protected internal override void CollectLinks(
    Dictionary<long, int> id2Types,
    Dictionary<Guid, int> objGuid2Types)
  {
    if (this.ItemObjectID == null || this.ItemObjectID.Value == 0L)
      return;
    id2Types[this.ItemObjectID.Value] = -1;
  }

  protected internal override void UpdateLinks(
    Dictionary<long, string> id2Caption,
    Dictionary<Guid, string> guid2Caption)
  {
    if (this.ItemObjectID == null || !id2Caption.ContainsKey(this.ItemObjectID.Value))
      return;
    this._itemObjectCaption = id2Caption[this.ItemObjectID.Value];
  }

  public AutoSelectionNodeItemObject(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._itemObjectId = new AS_Long();
    this.InitializeData();
  }

  public override XmlNode SaveData(XmlDocument doc)
  {
    XmlNode xmlNode = base.SaveData(doc);
    XmlAttribute attribute1 = doc.CreateAttribute("ItemObjectID");
    attribute1.Value = this._itemObjectId.ToString();
    xmlNode.Attributes.Append(attribute1);
    XmlAttribute attribute2 = doc.CreateAttribute("ItemObjectCaption");
    attribute2.Value = this._itemObjectCaption;
    xmlNode.Attributes.Append(attribute2);
    xmlNode.AppendChild(AutoSelEnumUtils.Save("ItemObjectMode", (int) this._itemObjectMode, EnumTypeHelper.GetCaption((Enum) this._itemObjectMode), doc));
    return xmlNode;
  }

  public override AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node?.Attributes == null || base.LoadData(node) == null)
      return (AutoSelectionNodeCommon) null;
    this._itemObjectId = new AS_Long(Convert.ToInt64(node.Attributes["ItemObjectID"].Value));
    XmlAttribute attribute = node.Attributes["ItemObjectCaption"];
    if (attribute != null)
      this._itemObjectCaption = attribute.Value;
    int id;
    AutoSelEnumUtils.Load("ItemObjectMode", node, out id);
    this._itemObjectMode = (AutoSelectonItemObjectMode) id;
    return (AutoSelectionNodeCommon) this;
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_39")]
  [CustomDescription("Attribute.AutoSelection.Client_40")]
  [TypeConverter(typeof (SelectionLongObjectConverter))]
  [Editor(typeof (SelectionObjectEditor), typeof (UITypeEditor))]
  public AS_Long ItemObjectID
  {
    get => this._itemObjectId;
    set => this.SetItemObjectID(value, true);
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_41")]
  [CustomDescription("Attribute.AutoSelection.Client_42")]
  [RefreshProperties(RefreshProperties.All)]
  public AutoSelectonItemObjectMode ItemObjectMode
  {
    get => this._itemObjectMode;
    set => this._itemObjectMode = value;
  }

  protected override string GetShortInfo()
  {
    return this._itemObjectCaption != string.Empty ? $"{this.Name}:{this._itemObjectCaption})" : base.GetShortInfo();
  }

  public override string ToString()
  {
    if (!(this._itemObjectCaption != string.Empty))
      return base.ToString();
    return $"{EnumDescConverter.GetEnumDescription((Enum) this.Type)}({this.Name}:{this._itemObjectCaption})";
  }

  protected internal override IList<AutoSelectionObject> CreateObject(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject)
  {
    if (MetaDataHelper.GetObjectTypeID(this.ObjTypeGuid.ToString()) == -1)
      return (IList<AutoSelectionObject>) null;
    object obj = selectionObject?.Value;
    if (obj == null || obj.GetType() != typeof (AS_Long))
      return (IList<AutoSelectionObject>) null;
    switch (this.ItemObjectMode)
    {
      case AutoSelectonItemObjectMode.CreateNew:
        return this.CreateObject_New(asSession, selectionObject);
      case AutoSelectonItemObjectMode.LinkToObjectOnly:
        return this.CreateObject_LinkOnly(asSession, selectionObject);
      case AutoSelectonItemObjectMode.CreateByDialog:
        return this.CreateObject_ByDialog(asSession, selectionObject);
      default:
        return (IList<AutoSelectionObject>) null;
    }
  }

  private IList<AutoSelectionObject> CreateObject_LinkOnly(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID(this.ObjTypeGuid.ToString());
    if (objectTypeId == -1)
      return (IList<AutoSelectionObject>) null;
    AutoSelectionObject autoSelectionObject = (AutoSelectionObject) selectionObject.Clone();
    autoSelectionObject.CreatedObjInfo = new ObjInfoItem(this.ItemObjectID.Value, objectTypeId);
    return (IList<AutoSelectionObject>) new AutoSelectionObject[1]
    {
      autoSelectionObject
    };
  }

  private IList<AutoSelectionObject> CreateObject_New(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID(this.ObjTypeGuid.ToString());
    if (objectTypeId == -1)
      return (IList<AutoSelectionObject>) null;
    List<AutoSelectionObject> objectNew = new List<AutoSelectionObject>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBObjectCollection objectCollection = session.GetObjectCollection(objectTypeId);
      IDBObject dbObject;
      if (this.ItemObjectID.Value == 0L)
      {
        dbObject = objectCollection.Create();
      }
      else
      {
        if (session.GetObject(this.ItemObjectID.Value, false) == null)
        {
          AutoSelectionUtils.Output.WriteString(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_86"), (object) MetaDataHelper.GetObjectTypeName(objectTypeId), (object) this.ItemObjectID.Value));
          return (IList<AutoSelectionObject>) null;
        }
        dbObject = objectCollection.Create(this.ItemObjectID.Value);
      }
      this.AttributesObjectSetDefault(asSession, dbObject, (List<AutoSelAttrVal>) this._defObjAttrList);
      AutoSelectionObject autoSelectionObject = (AutoSelectionObject) selectionObject.Clone();
      autoSelectionObject.CreatedObjInfo = new ObjInfoItem(dbObject);
      this.AttributesCalc(asSession, (IDBAttributable) dbObject, (List<AutoSelAttr>) this._calcObjAttrList);
      objectNew.Add(autoSelectionObject);
    }
    return (IList<AutoSelectionObject>) objectNew;
  }

  private IList<AutoSelectionObject> CreateObject_ByDialog(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject)
  {
    int objectTypeId = MetaDataHelper.GetObjectTypeID(this.ObjTypeGuid.ToString());
    if (objectTypeId == -1)
      return (IList<AutoSelectionObject>) null;
    IObjectCreatorService service = ServiceUtils.GetService<IObjectCreatorService>((object) ApplicationServices.Container, true);
    ObjInfoItem projObjInfo = this.GetProjectObjInfo(asSession);
    int relationTypeId;
    if ((TypedInfoItem) projObjInfo == (TypedInfoItem) null || !this.GetRelationType(projObjInfo.ObjTypeID, out relationTypeId))
      return (IList<AutoSelectionObject>) null;
    ObjectRelationLink[] aObjRelations = new ObjectRelationLink[1]
    {
      new ObjectRelationLink(projObjInfo.ObjectID, relationTypeId)
    };
    OpenEditorMode openEditor = OpenEditorMode.Open;
    long objectByTypeDialog = service.CreateObjectByTypeDialog(objectTypeId, -1L, aObjRelations, DateTime.Now, false, ref openEditor, (IObjectCreatorParams) null);
    List<ObjectCreatedInfo> objectCreatedInfoList = new List<ObjectCreatedInfo>((IEnumerable<ObjectCreatedInfo>) service.GetObjectCreatedInfo());
    if (objectCreatedInfoList.Count == 0 && objectByTypeDialog != 0L && objectByTypeDialog != -1L)
      objectCreatedInfoList.Add(new ObjectCreatedInfo()
      {
        ObjectId = objectByTypeDialog
      });
    if (objectCreatedInfoList.Count == 0)
      return (IList<AutoSelectionObject>) null;
    List<AutoSelectionObject> objectByDialog = new List<AutoSelectionObject>();
    selectionObject.NeedAutoSelection = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (ObjectCreatedInfo objectCreatedInfo in objectCreatedInfoList)
      {
        if (objectCreatedInfo.ObjectTypeId == -1 || objectCreatedInfo.ObjectTypeId == objectTypeId)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(objectCreatedInfo.ObjectId, false);
          if (dbObject != null && dbObject.TypeID == objectTypeId)
          {
            this.AttributesObjectSetDefault(asSession, dbObject, (List<AutoSelAttrVal>) this._defObjAttrList);
            AutoSelectionObject autoSelectionObject = (AutoSelectionObject) selectionObject.Clone();
            autoSelectionObject.CreatedObjInfo = new ObjInfoItem(dbObject);
            this.AttributesCalc(asSession, (IDBAttributable) dbObject, (List<AutoSelAttr>) this._calcObjAttrList);
            ObjectRelationLink objectRelationLink = objectCreatedInfo.RelationLinks != null ? ((IEnumerable<ObjectRelationLink>) objectCreatedInfo.RelationLinks).FirstOrDefault<ObjectRelationLink>((Func<ObjectRelationLink, bool>) (item => item.ObjectID == projObjInfo.ObjectID && item.RelationTypeID == relationTypeId)) : (ObjectRelationLink) null;
            if (objectRelationLink != null)
              autoSelectionObject.CreatedRelnfo = new RelObjInfoItem(objectRelationLink.LinkID, objectRelationLink.RelationTypeID)
              {
                ProjInfo = projObjInfo,
                PartInfo = new ObjInfoItem(dbObject)
              };
            objectByDialog.Add(autoSelectionObject);
          }
        }
      }
    }
    return (IList<AutoSelectionObject>) objectByDialog;
  }

  protected override AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    AutoSelExecuteStatus selExecuteStatus = base.DoExecute(asSession, logRec);
    if (selExecuteStatus != AutoSelExecuteStatus.Applied)
      return selExecuteStatus;
    if (this._itemObjectMode == AutoSelectonItemObjectMode.LinkToObjectOnly && this._itemObjectId.Value == 0L)
      return AutoSelExecuteStatus.Skipped;
    AutoSelectionObject prototypeSelectionObject = new AutoSelectionObject((AutoSelectionNodeCommon) this, (object) this._itemObjectId);
    if (!this.AnalyzeObject(asSession, logRec))
      return AutoSelExecuteStatus.Skipped;
    if (asSession.TestMode)
    {
      asSession.CreatedObjectList.Add(prototypeSelectionObject);
      return AutoSelExecuteStatus.Applied;
    }
    IList<AutoSelectionObject> createdSelectionObjects;
    if (!this.CreateSelectionObject(asSession, prototypeSelectionObject, out createdSelectionObjects) || createdSelectionObjects == null)
      return AutoSelExecuteStatus.Skipped;
    foreach (AutoSelectionObject asObject in (IEnumerable<AutoSelectionObject>) createdSelectionObjects)
    {
      this.CreatedSelectionObject_Edit(asSession, asObject);
      asSession.CreatedObjectList.Add(asObject);
      this.CreatedSelectionObject_RunAutoSelection(asSession, asObject);
    }
    return AutoSelExecuteStatus.Applied;
  }
}
