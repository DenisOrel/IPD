// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechcardBaseObjectCommandUtils
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;
using Intermech.TechCard.Client.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes;

/// <summary>Commands utils for TechCard objects</summary>
public static class TechcardBaseObjectCommandUtils
{
  /// <summary>Проверка на допустимость команд перемещения</summary>
  /// <param name="commandsInfo"></param>
  /// <param name="items"></param>
  /// <param name="treeView"></param>
  public static void MoveCommandsValidate(
    CommandsInfo commandsInfo,
    ISelectedItems items,
    NavigatorTreeView treeView)
  {
    if (commandsInfo == null || commandsInfo == CommandsInfo.Empty)
      return;
    bool flag1 = false;
    bool flag2 = false;
    try
    {
      if (items == null || items.Count != 1 || treeView == null || treeView.SelectedNodes.Length != 1 || !treeView.ManualSort)
        return;
      NavigatorTreeNode selectedNode = treeView.SelectedNodes[0];
      IDBTypedObjectID dbTypedObjectId1;
      IDBRelationID dbRelationId1;
      if (selectedNode?.Parent == null || !TechcardClientControlsUtils.IsSelectedItemsFromTree(items, treeView) || !TechcardClientControlsUtils.GetObjectInfo(selectedNode, out dbTypedObjectId1, out dbRelationId1, false))
        return;
      NavigatorTreeNode parent = selectedNode.Parent;
      int num = parent.Children.IndexOf(selectedNode);
      if (num == -1)
        return;
      IDBTypedObjectID dbTypedObjectId2;
      IDBRelationID dbRelationId2;
      if (num > 0 && TechcardClientControlsUtils.GetObjectInfo(parent.Children[num - 1], out dbTypedObjectId2, out dbRelationId2, false) && dbTypedObjectId2 != null && dbRelationId2 != null && dbTypedObjectId2.ObjectType == dbTypedObjectId1.ObjectType && dbRelationId2.RelationType == dbRelationId1.RelationType)
        flag1 = true;
      IDBTypedObjectID dbTypedObjectId3;
      IDBRelationID dbRelationId3;
      if (num >= parent.Children.Count - 1 || !TechcardClientControlsUtils.GetObjectInfo(parent.Children[num + 1], out dbTypedObjectId3, out dbRelationId3, false) || dbTypedObjectId3 == null || dbRelationId3 == null || dbTypedObjectId3.ObjectType != dbTypedObjectId1.ObjectType || dbRelationId3.RelationType != dbRelationId1.RelationType)
        return;
      flag2 = true;
    }
    finally
    {
      if (!flag2 && !flag1)
      {
        commandsInfo.Suppress("moveObjectNode", 0);
      }
      else
      {
        if (flag1)
        {
          commandsInfo.Add("moveTop", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.MoveTopCommand)));
          commandsInfo.Add("moveUp", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.MoveUpCommand)));
        }
        if (flag2)
        {
          commandsInfo.Add("moveDown", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.MoveDownCommand)));
          commandsInfo.Add("moveBottom", new CommandInfo(0, new ClickEventHandler(TechCardBaseObjectContextCommandProvider.MoveBottomCommand)));
        }
      }
    }
  }

  /// <summary>Выполнение команды перемещения</summary>
  /// <param name="moveMode"></param>
  /// <param name="items"></param>
  /// <param name="treeView"></param>
  public static void MoveCommandsExecute(
    TechcardBaseObjectCommandUtils.MoveCommandMode moveMode,
    ISelectedItems items,
    NavigatorTreeView treeView)
  {
    if (items == null || items.Count != 1 || treeView == null || !treeView.ManualSort)
      return;
    NavigatorTreeNode selectedNode = treeView.SelectedNodes[0];
    IDBTypedObjectID dbTypedObjectId1;
    IDBRelationID dbRelationId1;
    if (selectedNode?.Parent == null || !TechcardClientControlsUtils.IsSelectedItemsFromTree(items, treeView) || !TechcardClientControlsUtils.GetObjectInfo(selectedNode, out dbTypedObjectId1, out dbRelationId1, false))
      return;
    NavigatorTreeNode parent = selectedNode.Parent;
    int num1 = parent.Children.IndexOf(selectedNode);
    if (num1 == -1)
      return;
    List<RelObjInfoItem> source = new List<RelObjInfoItem>();
    int num2 = 0;
    int index1 = -1;
    int index2 = -1;
    switch (moveMode)
    {
      case TechcardBaseObjectCommandUtils.MoveCommandMode.First:
      case TechcardBaseObjectCommandUtils.MoveCommandMode.MoveUp:
        num2 = -1;
        index1 = num1 - 1;
        break;
      case TechcardBaseObjectCommandUtils.MoveCommandMode.Down:
      case TechcardBaseObjectCommandUtils.MoveCommandMode.Last:
        num2 = 1;
        index1 = num1 + 1;
        break;
    }
    if (moveMode == TechcardBaseObjectCommandUtils.MoveCommandMode.First || moveMode == TechcardBaseObjectCommandUtils.MoveCommandMode.Last)
    {
      IDBTypedObjectID dbTypedObjectId2;
      IDBRelationID dbRelationId2;
      for (int index3 = index1 + num2; index3 >= 0 && index3 < parent.Children.Count && TechcardClientControlsUtils.GetObjectInfo(parent.Children[index3], out dbTypedObjectId2, out dbRelationId2, false) && dbTypedObjectId2 != null && dbRelationId2 != null && dbTypedObjectId2.ObjectType == dbTypedObjectId1.ObjectType && dbRelationId2.RelationType == dbRelationId1.RelationType; index3 += num2)
        index1 = index3;
    }
    switch (moveMode)
    {
      case TechcardBaseObjectCommandUtils.MoveCommandMode.First:
      case TechcardBaseObjectCommandUtils.MoveCommandMode.MoveUp:
        index2 = index1 > 0 ? index1 - 1 : index2;
        break;
      case TechcardBaseObjectCommandUtils.MoveCommandMode.Down:
      case TechcardBaseObjectCommandUtils.MoveCommandMode.Last:
        index2 = index1 < parent.Children.Count - 1 ? index1 + 1 : index2;
        break;
    }
    IDBTypedObjectID dbTypedObjectId3;
    IDBRelationID dbRelationId3;
    if (!TechcardClientControlsUtils.GetObjectInfo(parent.Children[index1], out dbTypedObjectId3, out dbRelationId3, false) || dbRelationId3 == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long result1 = dbRelationId3.Sorting;
      if (result1 == 0L)
      {
        IDBRelation relation = session.GetRelation(dbRelationId3.Value, false);
        if (relation == null)
          return;
        IDBAttribute attributeByGuid = relation.GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"), false);
        if (attributeByGuid != null)
          long.TryParse(attributeByGuid.AsString, out result1);
      }
      if (index2 != -1)
      {
        long result2 = 0;
        IDBRelationID dbRelationId4;
        if (TechcardClientControlsUtils.GetObjectInfo(parent.Children[index2], out dbTypedObjectId3, out dbRelationId4, false) && dbRelationId4 != null)
        {
          result2 = dbRelationId4.Sorting;
          if (result2 == 0L)
          {
            IDBRelation relation = session.GetRelation(dbRelationId4.Value, false);
            if (relation != null)
            {
              IDBAttribute attributeByGuid = relation.GetAttributeByGuid(new Guid("cad00202-306c-11d8-b4e9-00304f19f545"), false);
              if (attributeByGuid != null)
                long.TryParse(attributeByGuid.AsString, out result2);
            }
          }
        }
        if (result1 > 0L && result2 == 0L)
        {
          long num3 = 1000000;
          result1 += (long) num2 * num3;
        }
        else
          result1 = (long) Math.Round((double) (result1 + result2) / 2.0);
      }
      else
      {
        long num4 = 1000000;
        result1 += (long) num2 * num4;
      }
      IDBRelation relation1 = session.GetRelation(dbRelationId1.Value, false);
      if (relation1 != null)
      {
        List<AttributeValues> attributeValuesList = new List<AttributeValues>()
        {
          new AttributeValues(MetaDataHelper.GetAttributeTypeID(new Guid("cad00202-306c-11d8-b4e9-00304f19f545")), (object) result1)
        };
        relation1.SetAttributesValues(attributeValuesList.ToArray());
        source.Add(new RelObjInfoItem(relation1)
        {
          ProjInfo = new ObjInfoItem(relation1.ProjID)
        });
      }
    }
    if (source.Count == 0)
      return;
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsRemoved", (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToArray<long>(), (IList<int>) null, (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>()));
    service.FireEvent((object) null, (NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.RelationID)).ToArray<long>(), (IList<long>) source.Select<RelObjInfoItem, long>((Func<RelObjInfoItem, long>) (item => item.ProjInfo.ObjectID)).ToArray<long>(), (IList<int>) null, (IList<int>) source.Select<RelObjInfoItem, int>((Func<RelObjInfoItem, int>) (item => item.RelTypeID)).ToArray<int>()));
  }

  /// <summary>Нумерация элемента</summary>
  /// <param name="objectNode">Нумеруемый узел навигатора</param>
  /// <param name="fixedObjMode">Признак является ли область нумерации фиксированной</param>
  /// <param name="treeView">Дерево навигатора</param>
  public static bool NumerateCommand(
    NavigatorTreeNode objectNode,
    bool fixedObjMode,
    NavigatorTreeView treeView)
  {
    IDBTypedObjectID dbTypedObjectId1;
    IDBRelationID dbRelationId;
    IDBTypedObjectID dbTypedObjectId2;
    if (objectNode == null || treeView == null || !TechcardClientControlsUtils.GetObjectInfo(objectNode, out dbTypedObjectId1, out dbRelationId, false) || !TechcardClientControlsUtils.GetObjectInfo(objectNode.Parent, out dbTypedObjectId2, out IDBRelationID _, false))
      return false;
    long objectId1 = dbTypedObjectId1 != null ? dbTypedObjectId1.ObjectID : 0L;
    long objectId2 = dbTypedObjectId2 != null ? dbTypedObjectId2.ObjectID : 0L;
    if (objectId1 == 0L)
      return false;
    int objTypeId1 = dbTypedObjectId1 != null ? dbTypedObjectId1.ObjectType : -1;
    int objTypeId2 = dbTypedObjectId2 != null ? dbTypedObjectId2.ObjectType : -1;
    if (objTypeId1 == -1)
      return false;
    ObjInfoItem objInfoItem = new ObjInfoItem(objectId1, objTypeId1);
    RelInfoItem relInfoItem = new RelInfoItem(dbRelationId != null ? dbRelationId.Value : 0L);
    relInfoItem.RelTypeID = dbRelationId != null ? dbRelationId.RelationType : -1;
    ObjInfoItem projInfoItem = new ObjInfoItem(objectId2, objTypeId2);
    int num = fixedObjMode ? 1 : 0;
    NavigatorTreeNode objItemTreeNode = objectNode;
    return TechCardBaseNumerateCommandUtils.NumerateObject(objInfoItem, relInfoItem, projInfoItem, num != 0, objItemTreeNode);
  }

  /// <summary>Move modes</summary>
  public enum MoveCommandMode
  {
    /// <summary>Move first</summary>
    First,
    /// <summary>Move ups</summary>
    MoveUp,
    /// <summary>Move downs</summary>
    Down,
    /// <summary>Move last</summary>
    Last,
  }
}
