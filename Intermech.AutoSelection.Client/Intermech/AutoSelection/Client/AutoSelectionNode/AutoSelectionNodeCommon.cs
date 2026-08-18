// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeCommon
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionNodeSupport;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.Expert;
using Intermech.Expert.User;
using Intermech.Interfaces;
using Intermech.Interfaces.AutoSelection;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Expert;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public abstract class AutoSelectionNodeCommon : AutoSelectionNodeBase, ICloneable
{
  private TempFormula _condition;
  protected AutoSelectionNodeType _type = AutoSelectionNodeType.None;

  private void InitializeData()
  {
  }

  protected bool ValidateRelation(
    ref IDBObject projObject,
    ref ObjInfoItem partInfo,
    int relTypeId)
  {
    if (projObject == null)
      throw new ArgumentNullException(nameof (projObject));
    if ((TypedInfoItem) partInfo == (TypedInfoItem) null)
      throw new ArgumentNullException(nameof (partInfo));
    IMSApplicability applicability = MetaDataHelper.GetApplicability(projObject.ObjectType, partInfo.ObjTypeID, relTypeId);
    if (applicability == null)
      return false;
    if ((applicability.ApplicabilityMode == ApplicabilityModes.AnyRequired || applicability.ApplicabilityMode == ApplicabilityModes.Required) && projObject.IsCreationMode)
      projObject.CommitCreation(true, applicability.IsContent | UISettings.AutoCheckOutNewObjects);
    return true;
  }

  public virtual ObjInfoItem GetProjectObjInfo(AutoSelectionSession asSession)
  {
    return asSession != null ? asSession.TargetObjInfo : throw new ArgumentNullException(nameof (asSession));
  }

  protected bool AnalyzeObject(AutoSelectionSession asSession, AutoSelectionLogRec logRec)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    return this.AnalyzeObject(asSession, logRec, this.GetProjectObjInfo(asSession));
  }

  protected AutoSelectionNodeCommon(AutoSelectionNodeBase ownerNode, string name)
    : base(ownerNode, name)
  {
    this._condition = new TempFormula();
    this.InitializeData();
  }

  public static byte[] Save(List<AutoSelectionNodeCommon> nodeList)
  {
    XmlDocument doc = new XmlDocument();
    XmlNode element = (XmlNode) doc.CreateElement("AutoSelectionNodes");
    foreach (AutoSelectionNodeCommon node in nodeList)
      element.AppendChild(node.SaveData(doc));
    doc.AppendChild(element);
    using (MemoryStream outStream = new MemoryStream())
    {
      doc.Save((Stream) outStream);
      return outStream.ToArray();
    }
  }

  public static AutoSelectionNodeCommon[] Load(AutoSelectionNodeBase ownerNode, byte[] data)
  {
    XmlDocument xmlDocument = new XmlDocument();
    using (MemoryStream inStream = new MemoryStream(data))
      xmlDocument.Load((Stream) inStream);
    XmlNode firstChild = xmlDocument.FirstChild;
    if (!firstChild.Name.Equals("AutoSelectionNodes"))
      return new AutoSelectionNodeCommon[0];
    List<AutoSelectionNodeCommon> selectionNodeCommonList = new List<AutoSelectionNodeCommon>();
    foreach (XmlNode childNode in firstChild.ChildNodes)
    {
      AutoSelectionNodeCommon selectionNodeCommon = AutoSelectionNodeCommon.Load(ownerNode, childNode);
      if (selectionNodeCommon != null)
        selectionNodeCommonList.Add(selectionNodeCommon);
    }
    return selectionNodeCommonList.ToArray();
  }

  public static AutoSelectionNodeCommon Load(AutoSelectionNodeBase ownerNode, XmlNode node)
  {
    if (node == null)
      throw new ArgumentNullException(nameof (node));
    if (!node.Name.Equals("Node") || node.Attributes == null)
      return (AutoSelectionNodeCommon) null;
    int id;
    if (!AutoSelEnumUtils.Load("ObjType", node, out id))
      return (AutoSelectionNodeCommon) null;
    System.Type nodeObjectType = AutoSelectionUtils.Common.GetNodeObjectType((AutoSelectionNodeType) id);
    if (nodeObjectType == (System.Type) null)
      return (AutoSelectionNodeCommon) null;
    return !(Activator.CreateInstance(nodeObjectType, (object) ownerNode, (object) node.Attributes["Name"].Value) is AutoSelectionNodeCommon instance) ? (AutoSelectionNodeCommon) null : instance.LoadData(node);
  }

  public virtual XmlNode SaveData(XmlDocument doc)
  {
    XmlNode element = (XmlNode) doc.CreateElement("Node");
    XmlNode newChild = AutoSelEnumUtils.Save("ObjType", (int) this._type, EnumTypeHelper.GetCaption((Enum) this._type), doc);
    element.AppendChild(newChild);
    XmlAttribute attribute1 = doc.CreateAttribute("Name");
    attribute1.Value = this.Name;
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = doc.CreateAttribute("Order");
    attribute2.Value = this.Order.ToString();
    element.Attributes.Append(attribute2);
    if (this._condition != null)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        new BinaryFormatter()
        {
          AssemblyFormat = FormatterAssemblyStyle.Simple
        }.Serialize((Stream) serializationStream, (object) this._condition);
        XmlAttribute attribute3 = doc.CreateAttribute("Condition");
        attribute3.Value = Convert.ToBase64String(serializationStream.ToArray());
        element.Attributes.Append(attribute3);
      }
    }
    foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
      element.AppendChild(childsNode.SaveData(doc));
    return element;
  }

  public virtual AutoSelectionNodeCommon LoadData(XmlNode node)
  {
    if (node == null || !node.Name.Equals("Node") || node.Attributes == null)
      return (AutoSelectionNodeCommon) null;
    this._name = node.Attributes["Name"].Value;
    int.TryParse(node.Attributes["Order"].Value, out this._order);
    XmlAttribute attribute = node.Attributes["Condition"];
    if (attribute != null && attribute.Value != null)
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      try
      {
        binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
        using (MemoryStream serializationStream = new MemoryStream(Convert.FromBase64String(attribute.Value)))
          this._condition = binaryFormatter.Deserialize((Stream) serializationStream) as TempFormula;
      }
      catch
      {
        int num = (int) MessageBox.Show(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_2"), Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_3"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
        return (AutoSelectionNodeCommon) null;
      }
    }
    foreach (XmlNode childNode in node.ChildNodes)
    {
      if (childNode.Name.Equals("Node"))
      {
        AutoSelectionNodeCommon selNode = AutoSelectionNodeCommon.Load((AutoSelectionNodeBase) this, childNode);
        if (selNode != null)
          this.ChildsNodes.Add(selNode);
      }
    }
    return this;
  }

  public virtual object Clone()
  {
    return (object) AutoSelectionNodeCommon.Load(this.OwnerNode, this.SaveData(new XmlDocument()));
  }

  public virtual bool AnalyzeObject(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec,
    ObjInfoItem targetObject)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    if (!((TypedInfoItem) targetObject == (TypedInfoItem) null) && targetObject.ObjectID != 0L)
      return true;
    string data = Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_79");
    asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, data);
    return false;
  }

  protected internal virtual IList<AutoSelectionObject> CreateObject(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject)
  {
    return (IList<AutoSelectionObject>) null;
  }

  protected virtual bool GetRelationType(int projTypeId, out int relationTypeId)
  {
    relationTypeId = -1;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(projTypeId);
    if (objectType == null)
      return false;
    relationTypeId = objectType.DefaultRelation;
    return true;
  }

  public virtual IDBRelation CreateRelation(
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
    if (!this.ValidateRelation(ref projObject, ref partInfo, relationTypeId))
      return (IDBRelation) null;
    if (userSession.GetObject(partInfo.ObjectID, false) != null)
      return relationCollection.Create(projObject.ObjectID, partInfo.ObjectID, MetaDataHelper.GetAttribute4RelationType(relationTypeId, Intermech.Imbase.Consts.ObjectSortOrderAttID) != null ? sortAttrValues : (AttributeValues[]) null);
    AutoSelectionUtils.Output.WriteString(string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_87"), (object) MetaDataHelper.GetRelationTypeName(relationTypeId), (object) projObject.Caption, (object) projObject.ObjectID, (object) MetaDataHelper.GetObjectTypeName(partInfo.ObjTypeID), (object) partInfo.ObjectID));
    return (IDBRelation) null;
  }

  protected bool CreateSelectionObject(
    AutoSelectionSession asSession,
    AutoSelectionObject prototypeSelectionObject,
    out IList<AutoSelectionObject> createdSelectionObjects)
  {
    createdSelectionObjects = (IList<AutoSelectionObject>) null;
    if (asSession == null)
      throw new ArgumentNullException(nameof (asSession));
    if (prototypeSelectionObject == null)
      throw new ArgumentNullException(nameof (prototypeSelectionObject));
    if (AutosSelectConsts.Config.DelayedObjectCreation)
      return true;
    IList<AutoSelectionObject> autoSelectionObjectList = this.CreateObject(asSession, prototypeSelectionObject);
    if (autoSelectionObjectList == null)
      return false;
    createdSelectionObjects = (IList<AutoSelectionObject>) new List<AutoSelectionObject>(autoSelectionObjectList.Count);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<CompositionSortingProjInfo> compositionSortingProjInfoList1 = new List<CompositionSortingProjInfo>();
      List<CompositionSortingProjInfo> compositionSortingProjInfoList2 = new List<CompositionSortingProjInfo>();
      IImbaseObjInfoService service1 = ServiceUtils.GetService<IImbaseObjInfoService>((object) session, false);
      foreach (AutoSelectionObject autoSelectionObject in (IEnumerable<AutoSelectionObject>) autoSelectionObjectList)
      {
        if (!ObjInfoItem.IsEmpty((ITypedInfoItem) autoSelectionObject.CreatedObjInfo))
        {
          if (RelInfoItem.IsEmpty((RelInfoItem) autoSelectionObject.CreatedRelnfo))
          {
            RelObjInfoItem relation1 = autoSelectionObject.CreatedRelnfo = new RelObjInfoItem((RelInfoItem) null, this.GetProjectObjInfo(asSession), autoSelectionObject.CreatedObjInfo);
            asSession.Service.DoBeforeCreateRelation((object) this, new RelationEventArgs(relation1));
            IDBObject objectActualCopy = session.GetObjectActualCopy(relation1.ProjInfo.ObjectID, true);
            relation1.ProjInfo.ObjectID = objectActualCopy.ObjectID;
            IDBRelation relation2 = this.CreateRelation(asSession, objectActualCopy, relation1.PartInfo);
            if (relation2 != null)
            {
              relation1.RelationID = relation2.RelationID;
              relation1.RelTypeID = relation2.RelationType;
              asSession.Service.DoAfterCreateRelation((object) this, new RelationEventArgs(relation1));
              CompositionSortingProjInfo compositionSortingProjInfo = new CompositionSortingProjInfo(autoSelectionObject.CreatedRelnfo.RelationID, autoSelectionObject.CreatedRelnfo.RelTypeID, autoSelectionObject.CreatedRelnfo.ProjInfo.ObjectID, autoSelectionObject.CreatedRelnfo.ProjInfo.ObjTypeID, autoSelectionObject.CreatedRelnfo.PartInfo.ObjTypeID);
              if ((TypedInfoItem) asSession.TargetProjInfo == (TypedInfoItem) autoSelectionObject.CreatedRelnfo.ProjInfo)
                compositionSortingProjInfoList2.Add(compositionSortingProjInfo);
              else
                compositionSortingProjInfoList1.Add(compositionSortingProjInfo);
              if (compositionSortingProjInfoList1.Count != 0 || compositionSortingProjInfoList2.Count != 0)
              {
                ICompositionsAutomaticSortingSession automaticSortingSession = (ICompositionsAutomaticSortingSession) null;
                ICompositionsAutomaticSortingService service2 = ServiceUtils.GetService<ICompositionsAutomaticSortingService>((object) session, true);
                if (service2 != null)
                  automaticSortingSession = service2.CreateSession((object) AutoSelectionSession.SortingSession);
                if (automaticSortingSession != null)
                {
                  try
                  {
                    if (compositionSortingProjInfoList2.Count != 0)
                    {
                      automaticSortingSession.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
                      {
                        asSession.TargetObjInfo
                      }, (object) session.SessionGUID);
                      long projectRelationId = asSession.Params.ProjectRelationIDs == null || asSession.Params.ProjectRelationIDs.Length == 0 ? 0L : asSession.Params.ProjectRelationIDs[0];
                      if (projectRelationId != 0L)
                        automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList2.ToArray(), CompositionTargetMode.InsertAfter, projectRelationId, (object) session.SessionGUID);
                      else
                        automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList2.ToArray(), (object) session.SessionGUID);
                      compositionSortingProjInfoList2.Clear();
                    }
                    if (compositionSortingProjInfoList1.Count != 0)
                    {
                      automaticSortingSession.PrefetchObjectComposition((IEnumerable<ObjInfoItem>) new ObjInfoItem[1]
                      {
                        asSession.TargetObjInfo
                      }, (object) session.SessionGUID);
                      automaticSortingSession.ProceedRelation((IEnumerable<CompositionSortingProjInfo>) compositionSortingProjInfoList1.ToArray(), (object) session.SessionGUID);
                      compositionSortingProjInfoList1.Clear();
                    }
                  }
                  finally
                  {
                    service2.DisposeSession((object) AutoSelectionSession.SortingSession);
                  }
                }
              }
            }
            else
              continue;
          }
          IDBObject dbObject = session.GetObjectActualCopy(autoSelectionObject.CreatedObjInfo.ObjectID, true);
          ObjInfoItem aObject = new ObjInfoItem(dbObject);
          ObjInfoItem newObject = (ObjInfoItem) null;
          IDBObject dbObjectLocalCopy = dbObject;
          Lazy<bool> lazy = new Lazy<bool>((Func<bool>) (() => ((IDBSecurity) dbObjectLocalCopy).CheckAccess(ActionType.Edit, true, false)));
          if (dbObject.IsCreationMode)
          {
            asSession.Service.DoBeforeCommitCreation((object) this, new ObjectEventArgs(aObject));
            dbObject.CommitCreation(true, lazy.Value);
            newObject = new ObjInfoItem(dbObject);
          }
          asSession.Service.DoAfterCommitCreation((object) this, (ObjectEventArgs) new ObjectCommitEventArgs(aObject, newObject));
          ImbaseObjCreateInfo objCreateInfo;
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy == 0L && lazy.Value && service1 != null && service1.GetCreationMode(dbObject.ObjectID, session.SessionGUID, out objCreateInfo) && objCreateInfo.CreateMode == ImbaseObjCreateMode.iocmCreateNew)
            dbObject = dbObject.CheckOut();
          autoSelectionObject.CreatedObjInfo.ObjectID = autoSelectionObject.CreatedRelnfo.PartInfo.ObjectID = dbObject.ObjectID;
          createdSelectionObjects.Add(autoSelectionObject);
        }
      }
    }
    return createdSelectionObjects.Count > 0;
  }

  public void DeleteSelectionObject(
    AutoSelectionSession asSession,
    AutoSelectionObject selectionObject,
    IUserSession session)
  {
  }

  protected virtual AutoSelExecuteStatus DoExecute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    return AutoSelExecuteStatus.Applied;
  }

  protected virtual AutoSelExecuteStatus DoExecuteCondition(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    AutoSelExecuteStatus selExecuteStatus = AutoSelExecuteStatus.Applied;
    if (this._condition == null || this._condition.Count == 0)
      return selExecuteStatus;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IExpertUser expertUserService = AutoSelectionUtils.ServiceKeeper.GetExpertUserService();
      IExpertServer service = ServiceUtils.GetService<IExpertServer>((object) sessionKeeper.Session, true);
      int taskId = service.StartTask(sessionKeeper.Session.SessionGUID, ExpertTraceFlags.None);
      try
      {
        service.SetDateTimeFormat(taskId, Thread.CurrentThread.CurrentCulture.DateTimeFormat);
        service.SetNumberFormat(taskId, Thread.CurrentThread.CurrentCulture.NumberFormat);
        service.SetTrace(taskId, expertUserService.ShowTraceWindow);
        service.SetLog(taskId, expertUserService.ReportLog);
        service.SetTraceFlags(taskId, ExpertTask.GetConfTraceFlags());
        bool flag = false;
        object obj;
        if (service.CalcFormula(taskId, (object) this._condition, asSession.ContextInfo.ObjectIds.ToArray<long>(), out obj, asSession.ContextInfo.RelationIds.FirstOrDefault<long>()) == ExpertResult.OK)
          flag = obj is bool && Convert.ToBoolean(obj);
        if (expertUserService.ShowTraceWindow)
          ExpertUser.rur.Execute(service.GetTraceInfo(taskId), true);
        selExecuteStatus = flag ? AutoSelExecuteStatus.Applied : AutoSelExecuteStatus.Skipped;
      }
      finally
      {
        service.EndTask(taskId);
      }
    }
    asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_77"), (object) EnumDescConverter.GetEnumDescription((Enum) selExecuteStatus)));
    return selExecuteStatus;
  }

  protected virtual AutoSelExecuteStatus DoExecuteChildNodes(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    foreach (AutoSelectionNodeBase childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
    {
      AutoSelExecuteStatus selExecuteStatus = childsNode.Execute(asSession, logRec);
      switch (selExecuteStatus)
      {
        case AutoSelExecuteStatus.SkipOwnerLevel:
          return AutoSelExecuteStatus.Skipped;
        case AutoSelExecuteStatus.AbortAll:
          return selExecuteStatus;
        default:
          continue;
      }
    }
    return AutoSelExecuteStatus.Applied;
  }

  protected override string GetShortInfo() => this.Name;

  public override string ToString()
  {
    return $"{EnumDescConverter.GetEnumDescription((Enum) this.Type)}({this.Name})";
  }

  public override AutoSelExecuteStatus Execute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec)
  {
    this.DoExecuteCheckArgs(asSession, logRec);
    AutoSelectionLogRec autoSelectionLogRec = asSession.SelectionLog.AddRec(logRec, (AutoSelectionNodeBase) this, this.ToString());
    AutoSelExecuteStatus selExecuteStatus1 = this.DoExecuteCondition(asSession, autoSelectionLogRec);
    if (selExecuteStatus1 != AutoSelExecuteStatus.Applied)
      return selExecuteStatus1;
    int count = asSession.CreatedObjectList.Count;
    AutoSelExecuteStatus selExecuteStatus2 = this.DoExecute(asSession, autoSelectionLogRec);
    if (selExecuteStatus2 == AutoSelExecuteStatus.Applied)
      selExecuteStatus2 = this.DoExecuteChildNodes(asSession, autoSelectionLogRec);
    if (selExecuteStatus2 != AutoSelExecuteStatus.Applied)
    {
      if ((uint) (selExecuteStatus2 - 1) <= 1U && asSession.CreatedObjectList.Count != count)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          for (int index = asSession.CreatedObjectList.Count - 1; index >= count; --index)
          {
            AutoSelectionObject createdObject = asSession.CreatedObjectList[index];
            createdObject?.Node?.DeleteSelectionObject(asSession, createdObject, sessionKeeper.Session);
            asSession.CreatedObjectList.RemoveAt(index);
          }
        }
      }
    }
    else
    {
      int num = asSession.CreatedObjectList.Count - count;
      asSession.SelectionLog.AddRec(autoSelectionLogRec, (AutoSelectionNodeBase) this, string.Format(Intermech.AutoSelection.Client.LocalizationHolder.rm.GetString("AutoSelection.Client_4"), (object) num));
    }
    return selExecuteStatus2;
  }

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_7")]
  [Intermech.AutoSelection.Client.CustomDescription("Attribute.AutoSelection.Client_8")]
  [ReadOnly(true)]
  public AutoSelectionNodeType Type => this._type;

  [Intermech.AutoSelection.Client.CustomCategory("Attribute.AutoSelection.Client_87")]
  [Intermech.AutoSelection.Client.CustomDisplayName("Attribute.AutoSelection.Client_9")]
  [Browsable(false)]
  public TempFormula Condition
  {
    get => this._condition;
    set => this._condition = value;
  }
}
