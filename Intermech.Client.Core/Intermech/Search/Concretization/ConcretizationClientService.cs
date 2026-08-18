
// Type: Intermech.Search.Concretization.ConcretizationClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Search.UI;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.Concretization;

public sealed class ConcretizationClientService : IConcretizationClientService
{
  public bool CanAbstract(NodeID objectNodeID)
  {
    if (objectNodeID == null)
      throw new ArgumentNullException(nameof (objectNodeID));
    return !RelationHelper.IsUnknownRelationID(objectNodeID.PrjLinkID) && objectNodeID.RelationTypeID != ConcretizationConstants.ProductDocumentationRelationTypeID && objectNodeID.State == ObjectFiltrationState.fsSoftConcretised;
  }

  public void AbstractCurrentVersion(long relationID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (!this.CanSetObjectVersionIDInComposition(relationID))
      return;
    this.SetObjectVersionIDInComposition(new Tuple<long, long>[1]
    {
      new Tuple<long, long>(relationID, 0L)
    });
  }

  public void AbstractCurrentVersionInComposition(
    long relationID,
    NavigatorTreeView navigatorTreeView)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (navigatorTreeView == null || navigatorTreeView.RootNode == null)
      throw new ArgumentException();
    this.SetObjectVersionIDInComposition(relationID, 0L, navigatorTreeView, (Predicate<Tuple<int, NodeID>>) (o => this.CanAbstract(o.Item2)));
  }

  public void AbstractEntireComposition(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    if (!ConcretizationClientService.CanExpandComposition())
      return;
    this.SetObjectVersionIDInComposition(((IEnumerable<NodeID>) this.GetObjectNodeIdsFromComposition(navigatorTreeNode, (Predicate<Tuple<int, NodeID>>) (o => this.CanAbstract(o.Item2)))).Select<NodeID, long>((Func<NodeID, long>) (o => o.PrjLinkID)).Distinct<long>().ToArray<long>(), 0L);
  }

  public bool CanConcretize(int projectTypeID, NodeID objectNodeID)
  {
    if (objectNodeID == null)
      throw new ArgumentNullException(nameof (objectNodeID));
    return !ObjectTypeHelper.IsUnknownObjectTypeID(projectTypeID) && !RelationHelper.IsUnknownRelationID(objectNodeID.PrjLinkID) && !RelationTypeHelper.IsUnknownRelationTypeID(objectNodeID.RelationTypeID) && !ObjectTypeHelper.IsUnknownObjectTypeID(objectNodeID.ObjectTypeID) && RelationTypeHelper.IsObjectVersionIDInCompositionExists(objectNodeID.RelationTypeID) && ObjectTypeApplicabilityHelper.IsSoftConcretizationMode(projectTypeID, objectNodeID.RelationTypeID, objectNodeID.ObjectTypeID) && objectNodeID.RelationTypeID != ConcretizationConstants.ProductDocumentationRelationTypeID && objectNodeID.State != ObjectFiltrationState.fsNonVersionable && objectNodeID.State != ObjectFiltrationState.fsCompositeVersion;
  }

  public void ConcretizeCurrentVersion(long relationID, long objectVersionID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    if (!this.CanSetObjectVersionIDInComposition(relationID))
      return;
    this.SetObjectVersionIDInComposition(new Tuple<long, long>[1]
    {
      new Tuple<long, long>(relationID, objectVersionID)
    });
  }

  public void ConcretizeCurrentVersionInComposition(
    long relationID,
    long objectVersionID,
    NavigatorTreeView navigatorTreeView)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      throw new ArgumentException();
    if (navigatorTreeView == null || navigatorTreeView.RootNode == null)
      throw new ArgumentException();
    this.SetObjectVersionIDInComposition(relationID, objectVersionID, navigatorTreeView, (Predicate<Tuple<int, NodeID>>) (o => this.CanConcretize(o.Item1, o.Item2)));
  }

  public void ConcretizeEntireComposition(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    if (!ConcretizationClientService.CanExpandComposition())
      return;
    this.SetObjectVersionIDInComposition(((IEnumerable<NodeID>) this.GetObjectNodeIdsFromComposition(navigatorTreeNode, (Predicate<Tuple<int, NodeID>>) (o => this.CanConcretize(o.Item1, o.Item2) && !ObjectHelper.IsUnknownObjectVersionID(o.Item2.ObjectID)))).Select<NodeID, Tuple<long, long>>((Func<NodeID, Tuple<long, long>>) (o => new Tuple<long, long>(o.PrjLinkID, o.ObjectID))).Distinct<Tuple<long, long>>().ToArray<Tuple<long, long>>());
  }

  public void ConcretizeSelectedVersion(long relationID, long objectID)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    long num = !ObjectHelper.IsUnknownObjectID(objectID) ? this.SelectVersion(objectID) : throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectVersionID(num))
      return;
    this.ConcretizeCurrentVersion(relationID, num);
  }

  public void ConcretizeSelectedVersionInComposition(
    long relationID,
    long objectID,
    NavigatorTreeView navigatorTreeView)
  {
    if (RelationHelper.IsUnknownRelationID(relationID))
      throw new ArgumentException();
    if (ObjectHelper.IsUnknownObjectID(objectID))
      throw new ArgumentException();
    if (navigatorTreeView == null || navigatorTreeView.RootNode == null)
      throw new ArgumentException();
    long num = this.SelectVersion(objectID);
    if (ObjectHelper.IsUnknownObjectVersionID(num))
      return;
    this.ConcretizeCurrentVersionInComposition(relationID, num, navigatorTreeView);
  }

  public void CheckVersion(NodeID objectNodeID, NavigatorTreeView navigatorTreeView)
  {
    if (objectNodeID == null)
      throw new ArgumentNullException(nameof (objectNodeID));
    if (navigatorTreeView == null || navigatorTreeView.RootNode == null)
      throw new ArgumentException();
    using (VersionCheckingForm versionCheckingForm = new VersionCheckingForm())
    {
      versionCheckingForm.ObjectNodeID = objectNodeID;
      versionCheckingForm.NavigatorTreeView = navigatorTreeView;
      int num = (int) versionCheckingForm.ShowDialog();
    }
  }

  public void AbstractComposition(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    this.SetObjectVersionIDInComposition(((IEnumerable<NodeID>) this.GetObjectNodeIdsFromCompositionFirstLevel(navigatorTreeNode, (Predicate<Tuple<int, NodeID>>) (o => this.CanAbstract(o.Item2)))).Select<NodeID, long>((Func<NodeID, long>) (o => o.PrjLinkID)).Distinct<long>().ToArray<long>(), 0L);
  }

  public void ConcretizeComposition(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    this.SetObjectVersionIDInComposition(((IEnumerable<NodeID>) this.GetObjectNodeIdsFromCompositionFirstLevel(navigatorTreeNode, (Predicate<Tuple<int, NodeID>>) (o => this.CanConcretize(o.Item1, o.Item2) && !ObjectHelper.IsUnknownObjectVersionID(o.Item2.ObjectID)))).Select<NodeID, Tuple<long, long>>((Func<NodeID, Tuple<long, long>>) (o => new Tuple<long, long>(o.PrjLinkID, o.ObjectID))).Distinct<Tuple<long, long>>().ToArray<Tuple<long, long>>());
  }

  private bool CanSetObjectVersionIDInComposition(long relationID)
  {
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      flag = ((IConcretizationServerService) sessionKeeper.Session.GetCustomService(typeof (IConcretizationServerService))).CanSetObjectVersionIDInComposition(sessionKeeper.Session.SessionGUID, relationID);
    if (!flag)
    {
      int num = (int) MessageBox.Show("Невозможно выполнить команду т.к. родительский объект не взят на редактирование, его редактирование запрещено или запрещена конкретизация данного типа связи.", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    return flag;
  }

  private void SetObjectVersionIDInComposition(
    Tuple<long, long>[] relationIDObjectVersionIDTuples)
  {
    if (relationIDObjectVersionIDTuples.Length != 0)
    {
      string report = (string) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        using (NotificationContext.Create(sessionKeeper.Session, (object) this))
          report = ((IConcretizationServerService) sessionKeeper.Session.GetCustomService(typeof (IConcretizationServerService))).SetObjectVersionIDInComposition(sessionKeeper.Session.SessionGUID, relationIDObjectVersionIDTuples);
      }
      this.ShowReport(report);
    }
    else
      ConcretizationClientService.ShowNotSuitableRelationsMessage();
  }

  private void SetObjectVersionIDInComposition(
    long relationID,
    long objectVersionID,
    NavigatorTreeView navigatorTreeView,
    Predicate<Tuple<int, NodeID>> predicate)
  {
    if (!this.CanSetObjectVersionIDInComposition(relationID) || !ConcretizationClientService.CanExpandComposition())
      return;
    long partID = 0;
    int relationTypeID = -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(relationID);
      partID = relation.PartID;
      relationTypeID = relation.RelationType;
    }
    this.SetObjectVersionIDInComposition(((IEnumerable<NodeID>) this.GetObjectNodeIdsFromComposition(navigatorTreeView.RootNode, (Predicate<Tuple<int, NodeID>>) (o => predicate(o) && o.Item2.ID == partID && o.Item2.RelationTypeID == relationTypeID))).Select<NodeID, long>((Func<NodeID, long>) (o => o.PrjLinkID)).Distinct<long>().ToArray<long>(), objectVersionID);
  }

  private NodeID[] GetObjectNodeIdsFromComposition(
    NavigatorTreeNode navigatorTreeNode,
    Predicate<Tuple<int, NodeID>> predicate)
  {
    return navigatorTreeNode.GetDescendants(true).Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (o => o.NodeID is NodeID)).Select<NavigatorTreeNode, Tuple<int, NodeID>>((Func<NavigatorTreeNode, Tuple<int, NodeID>>) (o => new Tuple<int, NodeID>(this.GetProjectTypeID(o), (NodeID) o.NodeID))).Where<Tuple<int, NodeID>>((Func<Tuple<int, NodeID>, bool>) (o => predicate(o))).Select<Tuple<int, NodeID>, NodeID>((Func<Tuple<int, NodeID>, NodeID>) (o => o.Item2)).Distinct<NodeID>().ToArray<NodeID>();
  }

  private int GetProjectTypeID(NavigatorTreeNode navigatorTreeNode)
  {
    return navigatorTreeNode.Parent != null && navigatorTreeNode.Parent.NodeID is NodeID ? ((NodeID) navigatorTreeNode.Parent.NodeID).ObjectTypeID : -1;
  }

  private static void ShowNotSuitableRelationsMessage()
  {
    int num = (int) MessageBox.Show("Невозможно выполнить команду. Не удалось найти подходящие связи", "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void ShowReport(string report)
  {
    int num = (int) MessageBox.Show(report, "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private static bool CanExpandComposition()
  {
    return MessageBox.Show("Внимание! Выполнение данной команды потребует разворота состава объекта на все уровни. Продолжить?", "Intermech Professional Solution", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK;
  }

  private void SetObjectVersionIDInComposition(long[] relationIds, long objectVersionID)
  {
    this.SetObjectVersionIDInComposition(((IEnumerable<long>) relationIds).Select<long, Tuple<long, long>>((Func<long, Tuple<long, long>>) (o => new Tuple<long, long>(o, objectVersionID))).ToArray<Tuple<long, long>>());
  }

  private long SelectVersion(long objectID)
  {
    return ObjectVersionSelection.SelectVersion(objectID, true, new List<long>(0));
  }

  private NodeID[] GetObjectNodeIdsFromCompositionFirstLevel(
    NavigatorTreeNode navigatorTreeNode,
    Predicate<Tuple<int, NodeID>> predicate)
  {
    navigatorTreeNode.Fetch();
    return navigatorTreeNode.Children.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (o => o.NodeID is NodeID)).Select<NavigatorTreeNode, Tuple<int, NodeID>>((Func<NavigatorTreeNode, Tuple<int, NodeID>>) (o => new Tuple<int, NodeID>(this.GetProjectTypeID(o), (NodeID) o.NodeID))).Where<Tuple<int, NodeID>>((Func<Tuple<int, NodeID>, bool>) (o => predicate(o))).Select<Tuple<int, NodeID>, NodeID>((Func<Tuple<int, NodeID>, NodeID>) (o => o.Item2)).Distinct<NodeID>().ToArray<NodeID>();
  }
}
