// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.HelperClasses.AVSSelectedItemsHelper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS.HelperClasses;

/// <summary>
/// Вспомогательный класс, позволяющий AVS создавать коллекции ISelectedItems
/// для обращения к функционалу "Навигатора"
/// </summary>
internal static class AVSSelectedItemsHelper
{
  private static ReferenceToDBObject GetRowReference(DocumentTreeNode node)
  {
    INodeWithReference nodeWithReference = node as INodeWithReference;
    ReferenceToDBObject rowReference = (ReferenceToDBObject) null;
    if (nodeWithReference != null)
      rowReference = nodeWithReference.Reference as ReferenceToDBObject;
    return rowReference;
  }

  private static long GetProductId(AVSWindow avsWindow, DocumentTreeNode node)
  {
    if (AVSDocument.IsProductVariableDocNode(node))
      return -1;
    if (avsWindow.AVSDocument.AvsDocumentForm == AVSDocumentForm.A)
    {
      DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(node);
      if (productVariableDocNode != null)
      {
        ReferenceToDBObject rowReference = AVSSelectedItemsHelper.GetRowReference(productVariableDocNode);
        if (rowReference != null)
          return rowReference.DBObjectID;
      }
    }
    if (avsWindow.AVSDocument.IsFormB && avsWindow.AVSDocument.AvsDocumentForm == AVSDocumentForm.V)
    {
      List<long> productIds = avsWindow.AVSDocument.ProductIds;
      if (productIds != null)
        return productIds.Count == 1 ? productIds[0] : -1L;
    }
    return avsWindow.AVSDocument.ProductId;
  }

  public static List<DocumentTreeNode> GetSelectedNodes(
    AVSWindow avsWindow,
    bool onlyRows,
    bool onlyProducts)
  {
    List<DocumentTreeNode> selectedNodes = new List<DocumentTreeNode>();
    if (avsWindow.AVSDocument != null && avsWindow.ReadOnly && !avsWindow.AVSDocument.DataLoaded)
    {
      List<DocumentTreeNode> documentTreeNodeList = (List<DocumentTreeNode>) null;
      if (avsWindow.DocumentControl != null)
        documentTreeNodeList = avsWindow.DocumentControl.SelectedNodes;
      if (documentTreeNodeList != null && documentTreeNodeList.Count > 0)
      {
        foreach (DocumentTreeNode docNode in documentTreeNodeList)
        {
          DocumentTreeNode parentSpecRowDocNode = AVSDocument.FindParentSpecRowDocNode(docNode);
          if (parentSpecRowDocNode != null)
          {
            if (!onlyProducts)
              selectedNodes.Add(parentSpecRowDocNode);
          }
          else
          {
            DocumentTreeNode productVariableDocNode = AVSDocument.FindParentProductVariableDocNode(docNode);
            if (!onlyRows && productVariableDocNode != null)
              selectedNodes.Add(productVariableDocNode);
          }
        }
      }
    }
    return selectedNodes;
  }

  public static List<long> GetSelectedIds(AVSWindow avsWindow)
  {
    List<long> selectedIds = new List<long>();
    if (avsWindow.AVSDocument != null && avsWindow.AVSDocument.DataLoaded)
    {
      List<AVSRow> selectedSpecRows = avsWindow.GetSelectedSpecRows(true);
      if (selectedSpecRows.Count > 0)
      {
        foreach (AVSRow avsRow in selectedSpecRows)
        {
          if (avsRow.ObjectId != -1L && !selectedIds.Contains(avsRow.ObjectId))
            selectedIds.Add(avsRow.ObjectId);
        }
      }
      else
        selectedIds.AddRange((IEnumerable<long>) avsWindow.GetSelectedProducts(true));
    }
    return selectedIds;
  }

  /// <summary>Сгенерировать коллекцию ISelectedItems для списка версий объектов</summary>
  /// <param name="rows">Список строк спецификации</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Коллекция ISelectedItems для списка версий объектов</returns>
  public static ISelectedItems GetSelectedItems(
    AVSWindow avsWindow,
    List<DocumentTreeNode> nodes,
    IServiceProvider services,
    bool objectItems)
  {
    if (nodes == null || nodes.Count == 0)
      return (ISelectedItems) null;
    List<long> longList = new List<long>();
    List<long> objectIDs = new List<long>();
    Dictionary<long, CreateObjectNodeParams> dictionary = new Dictionary<long, CreateObjectNodeParams>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < nodes.Count; ++index)
      {
        ReferenceToDBObject rowReference = AVSSelectedItemsHelper.GetRowReference(nodes[index]);
        if (rowReference != null)
        {
          if (rowReference.DBObjectID != -1L & objectItems)
            objectIDs.Add(rowReference.DBObjectID);
          else if (rowReference.DBObjectGuid != Guid.Empty)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(rowReference.DBObjectGuid, false);
            if (dbObject != null)
            {
              long objectId = dbObject.ObjectID;
              if (!objectItems)
              {
                CreateObjectNodeParams objectNodeParams = new CreateObjectNodeParams();
                objectNodeParams.ID = objectId;
                objectNodeParams.Caption = dbObject.Caption;
                objectNodeParams.CheckedOutBy = dbObject.CheckoutBy;
                objectNodeParams.LCStepID = dbObject.LCStep;
                objectNodeParams.ObjectID = dbObject.ObjectID;
                objectNodeParams.ObjectTypeID = dbObject.ObjectType;
                objectNodeParams.Owner = dbObject.OwnerID;
                IDBAttribute attributeById = dbObject.GetAttributeByID(AvsIDCache.Attr_SortIndex);
                if (attributeById != null)
                  objectNodeParams.Sorting = attributeById.AsInteger;
                objectNodeParams.State = ObjectFiltrationState.fsCorresponding;
                objectNodeParams.Version = (long) dbObject.VersionID;
                long productId = AVSSelectedItemsHelper.GetProductId(avsWindow, nodes[index]);
                long num1 = rowReference.DBRelationID;
                int num2 = rowReference.DBRelationType;
                if (num1 == -1L && productId != -1L && rowReference.DBRelationGuid != Guid.Empty)
                {
                  IDBRelation relation = sessionKeeper.Session.GetRelation(rowReference.DBRelationGuid, productId, false);
                  if (relation != null)
                  {
                    num1 = relation.RelationID;
                    num2 = relation.RelationType;
                  }
                }
                objectNodeParams.PrjLinkID = num1;
                objectNodeParams.RelationTypeID = num2;
                if (longList.IndexOf(productId) < 0)
                  longList.Add(productId);
                dictionary[objectNodeParams.ID] = objectNodeParams;
              }
              if (!objectIDs.Contains(objectId))
                objectIDs.Add(objectId);
            }
          }
        }
      }
    }
    if (objectIDs.Count == 0)
      return (ISelectedItems) null;
    if (objectItems)
      return ObjectExtensions.GetItems(objectIDs.ToArray());
    ListDescriptor rootDescriptor = new ListDescriptor(ObjectExtensions.CategoryID, 0, string.Empty, (IList) objectIDs);
    NodeIDPath handlerPath = new NodeIDPath((IDescriptor) rootDescriptor);
    EtherealNode etherealNode = new EtherealNode((IDescriptor) rootDescriptor);
    INodeQuery query = etherealNode.GetQuery(ContentType.Folders);
    query.Execute((object) null, 1);
    INodeID recordNodeId = query.GetRecordNodeID(0);
    handlerPath.Add(recordNodeId);
    INode child = etherealNode.GetChild(handlerPath[0]);
    if (child is IContextAware contextAware)
      contextAware.Services = services;
    NodeIDCollection nodeIDs = new NodeIDCollection();
    for (int index = 0; index < objectIDs.Count; ++index)
      nodeIDs.Add((INodeID) new NodeID(dictionary[objectIDs[index]])
      {
        Cookie = (object) new PartCookie(etherealNode.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/)
      });
    return (ISelectedItems) new NodeItems(handlerPath, child, nodeIDs, services, longList.Count > 1);
  }

  /// <summary>Сгенерировать коллекцию ISelectedItems для списка версий объектов</summary>
  /// <param name="rows">Список строк спецификации</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Коллекция ISelectedItems для списка версий объектов</returns>
  public static ISelectedItems GetSelectedItems(
    List<AVSRow> rows,
    IServiceProvider services,
    List<RelationAttributeValuesCache> relationIds)
  {
    if (rows == null || rows.Count == 0)
      return (ISelectedItems) null;
    Dictionary<long, List<long>> objIDs = new Dictionary<long, List<long>>();
    List<long> objectIDs = new List<long>(rows.Count);
    for (int index = 0; index < rows.Count; ++index)
    {
      if (rows[index].ObjectId != -1L && !objectIDs.Contains(rows[index].ObjectId))
        objectIDs.Add(rows[index].ObjectId);
    }
    if (objectIDs.Count == 0)
      return (ISelectedItems) null;
    if (rows.Count == 1)
    {
      if (rows[0].Relations != null && rows[0].Relations.Count == 1 && !rows[0].IsFormB)
      {
        relationIds = rows[0].Relations;
        long projectId = rows[0].Relations[0].ProjectId;
        objIDs[rows[0].Relations[0].ProjectId] = new List<long>((IEnumerable<long>) new long[1]
        {
          rows[0].Relations[0].RelationId
        });
      }
      else if (relationIds.Count == 1)
      {
        long projectId = relationIds[0].ProjectId;
        objIDs[relationIds[0].ProjectId] = new List<long>((IEnumerable<long>) new long[1]
        {
          relationIds[0].RelationId
        });
      }
    }
    return objIDs.Count <= 0 ? AVSSelectedItemsHelper.GetObjectSelectedItems(rows, objectIDs, services) : RelationExtensions.GetItems(objIDs, services);
  }

  private static ISelectedItems GetObjectSelectedItems(
    List<AVSRow> rows,
    List<long> objectIDs,
    IServiceProvider services)
  {
    if (rows == null)
      return ObjectExtensions.GetItems(objectIDs.ToArray(), services);
    DescriptorCollection descriptors = new DescriptorCollection();
    for (int index = 0; index < rows.Count; ++index)
    {
      if (rows[index].ObjGuid != Guid.Empty)
        descriptors.Add((IDescriptor) new AVSRowDescriptor(rows[index]));
    }
    return ObjectExtensions.GetItems((IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(Intermech.Navigator.Consts.CategoryCustomNode, 0, "Список объектов ", descriptors), services);
  }

  /// <summary>Сгенерировать коллекцию ISelectedItems для списка строк спецификации</summary>
  /// <param name="firstArticle">Идентификатор версии исполнения, информация о котором
  /// должна попасть в список-результат в первую очередь, или Intermech.Consts.UnknownObjectID</param>
  /// <param name="rows">Список строк спецификации</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <returns>Коллекция ISelectedItems для списка строк спецификации</returns>
  public static ISelectedItems GetRelationsSelectedItems(
    long firstArticle,
    List<AVSRow> rows,
    IServiceProvider services)
  {
    if (rows == null || rows.Count == 0)
      return (ISelectedItems) null;
    List<long> longList1 = new List<long>(rows.Count);
    List<long> longList2 = new List<long>();
    Dictionary<long, AVSRow> dictionary1 = new Dictionary<long, AVSRow>();
    Dictionary<long, List<long>> dictionary2 = new Dictionary<long, List<long>>();
    Dictionary<long, List<AVSRow>> dictionary3 = new Dictionary<long, List<AVSRow>>();
    for (int index1 = 0; index1 < rows.Count; ++index1)
    {
      if (rows[index1].Relations != null)
      {
        for (int index2 = 0; index2 < rows[index1].Relations.Count; ++index2)
        {
          long projectId = rows[index1].Relations[index2].ProjectId;
          long relationId = rows[index1].Relations[index2].RelationId;
          if (longList2.IndexOf(projectId) < 0)
            longList2.Add(projectId);
          if (!dictionary2.ContainsKey(projectId))
            dictionary2[projectId] = new List<long>();
          dictionary2[projectId].Add(relationId);
          if (!dictionary3.ContainsKey(projectId))
            dictionary3[projectId] = new List<AVSRow>();
          dictionary3[projectId].Add(rows[index1]);
        }
      }
      if (!longList1.Contains(rows[index1].ObjectId))
      {
        longList1.Add(rows[index1].ObjectId);
        dictionary1[rows[index1].ObjectId] = rows[index1];
      }
    }
    if (longList1.Count == 0)
      return (ISelectedItems) null;
    Dictionary<INodeID, NodeIDPath> handlerPaths = new Dictionary<INodeID, NodeIDPath>();
    Dictionary<INodeID, INode> handlers = new Dictionary<INodeID, INode>();
    NodeIDCollection nodeIDs = new NodeIDCollection();
    int index3 = longList2.IndexOf(firstArticle);
    if (index3 > 0)
    {
      longList2.RemoveAt(index3);
      longList2.Insert(0, firstArticle);
    }
    for (int index4 = 0; index4 < longList2.Count; ++index4)
    {
      long objID = longList2[index4];
      NodeIDPath handlerPath = new NodeIDPath((IDescriptor) new RelationsDescriptor(objID, (IList) dictionary2[longList2[index4]], true));
      EtherealNode handler = new EtherealNode(handlerPath.RootDescriptor);
      if (((Intermech.Navigator.DBObjects.Descriptor) handlerPath.RootDescriptor).InvalidDescriptor)
        return (ISelectedItems) new NodeItems(handlerPath, (INode) handler, new NodeIDCollection(), services);
      NodeID NodeID = new NodeID(new CreateObjectNodeParams()
      {
        ObjectID = objID,
        ObjectTypeID = rows[0].avsDocument.ProductType
      });
      NodeID.Cookie = (object) new DescriptorCookie(handler.descriptors.GetUniqueId(0));
      ((PartCookie) NodeID.Cookie).PartId = handler.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/;
      handlerPath.Add((INodeID) NodeID);
      INode child = handler.GetChild(handlerPath[0]);
      if (child is IContextAware contextAware)
        contextAware.Services = services;
      List<AVSRow> avsRowList = dictionary3[longList2[index4]];
      for (int index5 = 0; index5 < avsRowList.Count; ++index5)
      {
        AVSRow avsRow = avsRowList[index5];
        if (avsRow.Relations != null)
        {
          CreateObjectNodeParams e = new CreateObjectNodeParams()
          {
            Caption = AVSSelectedItemsHelper.ToString(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -50), 0, -1, true, false), string.Empty),
            CheckedOutBy = AVSSelectedItemsHelper.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -6), 0, -1, true, false), 0L),
            ID = avsRow.Object_F_ID,
            LCStepID = AVSSelectedItemsHelper.ToInt32(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -4), 0, -1, true, false), -1),
            ObjectID = avsRow.ObjectId,
            ObjectTypeID = avsRow.ObjType,
            Owner = AVSSelectedItemsHelper.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -8), 0, -1, true, false), 0L),
            PrjLinkID = 0,
            RelationTypeID = -1,
            Sorting = avsRow.SortIndex,
            State = ObjectFiltrationState.fsCorresponding,
            Version = AVSSelectedItemsHelper.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -5), 0, -1, true, false), 0L)
          };
          e.PrjLinkID = 0L;
          e.RelationTypeID = -1;
          e.BaseVersion = 0L;
          List<RelationAttributeValuesCache> attributeValuesCacheList = new List<RelationAttributeValuesCache>((IEnumerable<RelationAttributeValuesCache>) avsRow.Relations);
          if (firstArticle != 0L && attributeValuesCacheList.Count > 1 && attributeValuesCacheList[0].ProjectId != firstArticle)
          {
            int index6 = -1;
            for (int index7 = 0; index7 < attributeValuesCacheList.Count; ++index7)
            {
              if (attributeValuesCacheList[index7].ProjectId == firstArticle)
              {
                index6 = index7;
                break;
              }
            }
            if (index6 > 0)
            {
              RelationAttributeValuesCache attributeValuesCache = attributeValuesCacheList[index6];
              attributeValuesCacheList.RemoveAt(index6);
              attributeValuesCacheList.Insert(0, attributeValuesCache);
            }
          }
          for (int index8 = 0; index8 < attributeValuesCacheList.Count; ++index8)
          {
            if (attributeValuesCacheList[index8].ProjectId == objID)
            {
              e.PrjLinkID = attributeValuesCacheList[index8].RelationId;
              e.RelationTypeID = attributeValuesCacheList[index8].RelationType;
              NodeID key = new NodeID(e);
              key.Cookie = (object) new PartCookie(handler.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/);
              nodeIDs.Add((INodeID) key);
              handlerPaths[(INodeID) key] = handlerPath;
              handlers[(INodeID) key] = child;
            }
          }
        }
      }
    }
    return (ISelectedItems) new CompositeNodeItems(handlerPaths, handlers, nodeIDs, services, longList2.Count > 1);
  }

  public static ISelectedItems GetRelationsSelectedItems(
    List<AVSRow> rows,
    List<RelationAttributeValuesCache> relationIds,
    IServiceProvider services)
  {
    if (rows == null || rows.Count == 0)
      return (ISelectedItems) null;
    List<long> longList1 = new List<long>(rows.Count);
    List<long> longList2 = new List<long>();
    Dictionary<long, AVSRow> dictionary1 = new Dictionary<long, AVSRow>();
    Dictionary<long, List<long>> dictionary2 = new Dictionary<long, List<long>>();
    Dictionary<long, List<AVSRow>> dictionary3 = new Dictionary<long, List<AVSRow>>();
    for (int index1 = 0; index1 < rows.Count; ++index1)
    {
      List<RelationAttributeValuesCache> attributeValuesCacheList = relationIds ?? rows[index1].Relations;
      if (attributeValuesCacheList != null)
      {
        for (int index2 = 0; index2 < attributeValuesCacheList.Count; ++index2)
        {
          long projectId = attributeValuesCacheList[index2].ProjectId;
          long relationId = attributeValuesCacheList[index2].RelationId;
          if (longList2.IndexOf(projectId) < 0)
            longList2.Add(projectId);
          if (!dictionary2.ContainsKey(projectId))
            dictionary2[projectId] = new List<long>();
          dictionary2[projectId].Add(relationId);
          if (!dictionary3.ContainsKey(projectId))
            dictionary3[projectId] = new List<AVSRow>();
          dictionary3[projectId].Add(rows[index1]);
        }
      }
      if (!longList1.Contains(rows[index1].ObjectId))
      {
        longList1.Add(rows[index1].ObjectId);
        dictionary1[rows[index1].ObjectId] = rows[index1];
      }
    }
    if (longList1.Count == 0)
      return (ISelectedItems) null;
    Dictionary<INodeID, NodeIDPath> handlerPaths = new Dictionary<INodeID, NodeIDPath>();
    Dictionary<INodeID, INode> handlers = new Dictionary<INodeID, INode>();
    NodeIDCollection nodeIDs = new NodeIDCollection();
    for (int index3 = 0; index3 < longList2.Count; ++index3)
    {
      long objID = longList2[index3];
      NodeIDPath handlerPath = new NodeIDPath((IDescriptor) new RelationsDescriptor(objID, (IList) dictionary2[longList2[index3]], true));
      EtherealNode handler = new EtherealNode(handlerPath.RootDescriptor);
      if (((Intermech.Navigator.DBObjects.Descriptor) handlerPath.RootDescriptor).InvalidDescriptor)
        return (ISelectedItems) new NodeItems(handlerPath, (INode) handler, new NodeIDCollection(), services);
      NodeID NodeID = new NodeID(new CreateObjectNodeParams()
      {
        ObjectID = objID,
        ObjectTypeID = rows[0].avsDocument.ProductType
      });
      NodeID.Cookie = (object) new DescriptorCookie(handler.descriptors.GetUniqueId(0));
      ((PartCookie) NodeID.Cookie).PartId = handler.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/;
      handlerPath.Add((INodeID) NodeID);
      INode child = handler.GetChild(handlerPath[0]);
      if (child is IContextAware contextAware)
        contextAware.Services = services;
      List<AVSRow> avsRowList = dictionary3[longList2[index3]];
      for (int index4 = 0; index4 < avsRowList.Count; ++index4)
      {
        AVSRow avsRow = avsRowList[index4];
        List<RelationAttributeValuesCache> attributeValuesCacheList = relationIds ?? avsRow.Relations;
        if (attributeValuesCacheList != null)
        {
          CreateObjectNodeParams e = new CreateObjectNodeParams()
          {
            Caption = AVSSelectedItemsHelper.ToString(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -50), 0, -1, true, false), string.Empty),
            CheckedOutBy = AVSSelectedItemsHelper.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -6), 0, -1, true, false), 0L),
            ID = avsRow.Object_F_ID,
            LCStepID = AVSSelectedItemsHelper.ToInt32(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -4), 0, -1, true, false), -1),
            ObjectID = avsRow.ObjectId,
            ObjectTypeID = avsRow.ObjType,
            Owner = AVSSelectedItemsHelper.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -8), 0, -1, true, false), 0L),
            PrjLinkID = 0,
            RelationTypeID = -1,
            Sorting = avsRow.SortIndex,
            State = ObjectFiltrationState.fsCorresponding,
            Version = AVSSelectedItemsHelper.ToInt64(avsRow.GetFieldValue(new AvsRowAttributeInfo(false, -5), 0, -1, true, false), 0L)
          };
          e.PrjLinkID = 0L;
          e.RelationTypeID = -1;
          e.BaseVersion = 0L;
          for (int index5 = 0; index5 < attributeValuesCacheList.Count; ++index5)
          {
            if (attributeValuesCacheList[index5].ProjectId == objID)
            {
              e.PrjLinkID = attributeValuesCacheList[index5].RelationId;
              e.RelationTypeID = attributeValuesCacheList[index5].RelationType;
              NodeID key = new NodeID(e);
              key.Cookie = (object) new PartCookie(handler.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/);
              nodeIDs.Add((INodeID) key);
              handlerPaths[(INodeID) key] = handlerPath;
              handlers[(INodeID) key] = child;
            }
          }
        }
      }
    }
    return (ISelectedItems) new CompositeNodeItems(handlerPaths, handlers, nodeIDs, services, longList2.Count > 1);
  }

  public static ISelectedItems GetRelationsSelectedItems(
    AVSDocument avsDocument,
    List<RelationAttributeValuesCache> relationIds,
    IServiceProvider services)
  {
    List<long> longList1 = new List<long>(relationIds.Count);
    List<long> longList2 = new List<long>();
    Dictionary<long, RelationAttributeValuesCache> dictionary1 = new Dictionary<long, RelationAttributeValuesCache>();
    Dictionary<long, List<long>> dictionary2 = new Dictionary<long, List<long>>();
    Dictionary<long, List<RelationAttributeValuesCache>> dictionary3 = new Dictionary<long, List<RelationAttributeValuesCache>>();
    for (int index = 0; index < relationIds.Count; ++index)
    {
      long projectId = relationIds[index].ProjectId;
      long relationId = relationIds[index].RelationId;
      if (longList2.IndexOf(projectId) < 0)
        longList2.Add(projectId);
      if (!dictionary2.ContainsKey(projectId))
        dictionary2[projectId] = new List<long>();
      dictionary2[projectId].Add(relationId);
      if (!dictionary3.ContainsKey(projectId))
        dictionary3[projectId] = new List<RelationAttributeValuesCache>();
      dictionary3[projectId].Add(relationIds[index]);
      if (!longList1.Contains(relationIds[index].ObjectId))
      {
        longList1.Add(relationIds[index].ObjectId);
        dictionary1[relationIds[index].ObjectId] = relationIds[index];
      }
    }
    if (longList1.Count == 0)
      return (ISelectedItems) null;
    Dictionary<INodeID, NodeIDPath> handlerPaths = new Dictionary<INodeID, NodeIDPath>();
    Dictionary<INodeID, INode> handlers = new Dictionary<INodeID, INode>();
    NodeIDCollection nodeIDs = new NodeIDCollection();
    for (int index1 = 0; index1 < relationIds.Count; ++index1)
    {
      long projectId = relationIds[index1].ProjectId;
      NodeIDPath handlerPath = new NodeIDPath((IDescriptor) new RelationsDescriptor(projectId, (IList) dictionary2[longList2[index1]], true));
      EtherealNode handler = new EtherealNode(handlerPath.RootDescriptor);
      if (((Intermech.Navigator.DBObjects.Descriptor) handlerPath.RootDescriptor).InvalidDescriptor)
        return (ISelectedItems) new NodeItems(handlerPath, (INode) handler, new NodeIDCollection(), services);
      NodeID NodeID = new NodeID(new CreateObjectNodeParams()
      {
        ObjectID = projectId,
        ObjectTypeID = avsDocument.ProductType
      });
      NodeID.Cookie = (object) new DescriptorCookie(handler.descriptors.GetUniqueId(0));
      ((PartCookie) NodeID.Cookie).PartId = handler.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/;
      handlerPath.Add((INodeID) NodeID);
      INode child = handler.GetChild(handlerPath[0]);
      if (child is IContextAware contextAware)
        contextAware.Services = services;
      List<RelationAttributeValuesCache> attributeValuesCacheList = dictionary3[longList2[index1]];
      for (int index2 = 0; index2 < attributeValuesCacheList.Count; ++index2)
      {
        RelationAttributeValuesCache attributeValuesCache = attributeValuesCacheList[index2];
        CreateObjectNodeParams e = new CreateObjectNodeParams()
        {
          Caption = attributeValuesCache.ObjectCaption,
          CheckedOutBy = AVSSelectedItemsHelper.ToInt64(attributeValuesCache.GetValue(new AvsRowAttributeInfo(false, -6), false), 0L),
          ID = attributeValuesCache.F_ID,
          LCStepID = AVSSelectedItemsHelper.ToInt32(attributeValuesCache.GetValue(new AvsRowAttributeInfo(false, -4), false), -1),
          ObjectID = attributeValuesCache.ObjectId,
          ObjectTypeID = attributeValuesCache.ObjectType,
          Owner = AVSSelectedItemsHelper.ToInt64(attributeValuesCache.GetValue(new AvsRowAttributeInfo(false, -8), false), 0L),
          PrjLinkID = 0,
          RelationTypeID = -1,
          Sorting = attributeValuesCache.SortIndex,
          State = ObjectFiltrationState.fsCorresponding,
          Version = AVSSelectedItemsHelper.ToInt64(attributeValuesCache.GetValue(new AvsRowAttributeInfo(false, -5), false), 0L)
        };
        e.PrjLinkID = 0L;
        e.RelationTypeID = -1;
        e.BaseVersion = 0L;
        e.PrjLinkID = attributeValuesCache.RelationId;
        e.RelationTypeID = attributeValuesCache.RelationType;
        NodeID key = new NodeID(e);
        key.Cookie = (object) new PartCookie(handler.FolderSlots[0].UniqueId | 1073741824 /*0x40000000*/);
        nodeIDs.Add((INodeID) key);
        handlerPaths[(INodeID) key] = handlerPath;
        handlers[(INodeID) key] = child;
      }
    }
    return (ISelectedItems) new CompositeNodeItems(handlerPaths, handlers, nodeIDs, services, longList2.Count > 1);
  }

  /// <summary>Метод, позволяющий преобразовать object в string</summary>
  /// <param name="value">Преобразуемое значение</param>
  /// <param name="defValue">Значение по умолчанию</param>
  /// <returns>Результат</returns>
  internal static string ToString(object value, string defValue)
  {
    return value == null || value == DBNull.Value ? defValue : Convert.ToString(value);
  }

  /// <summary>Метод, позволяющий преобразовать object в Int32</summary>
  /// <param name="value">Преобразуемое значение</param>
  /// <param name="defValue">Значение по умолчанию</param>
  /// <returns>Результат</returns>
  internal static int ToInt32(object value, int defValue)
  {
    return value == null || value == DBNull.Value ? defValue : Convert.ToInt32(value);
  }

  /// <summary>Метод, позволяющий преобразовать object в Int64</summary>
  /// <param name="value">Преобразуемое значение</param>
  /// <param name="defValue">Значение по умолчанию</param>
  /// <returns>Результат</returns>
  internal static long ToInt64(object value, long defValue)
  {
    return value == null || value == DBNull.Value ? defValue : Convert.ToInt64(value);
  }
}
