
// Type: Intermech.Navigator.Controls.NavigatorTreeViewHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Controls;

public static class NavigatorTreeViewHelper
{
  /// <summary>Рекурсивно ищет все наиболее близкие к корню иерархии ноды дерева, для которых верно некоторое условие и выполняет с ними
  /// переданную процедуру</summary>
  /// <param name="condition">Условие, которому должны соответствовать наиболее близкие к корню иерархии ноды дерева</param>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей некоторому условию</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="treeNodes">Список нод, с которых идёт обработка. Если null, то обработка начнётся с корневой ноды. Используется для
  /// организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForAllClosestToRootTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [NotNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool autoPopulateNodes = false,
    [CanBeNull] IEnumerable<NavigatorTreeNode> treeNodes = null)
  {
    foreach (NavigatorTreeNode node in treeNodes ?? Enumeration.Create<NavigatorTreeNode>(navigatorTreeView.RootNode))
    {
      if (condition(node))
        predicate(node);
      else if (node.HasChildren && (node.Full || autoPopulateNodes && navigatorTreeView.PopulateNodeAndWaitForFull(node)))
        navigatorTreeView.InvokeForAllClosestToRootTreeNodes(condition, predicate, autoPopulateNodes, (IEnumerable<NavigatorTreeNode>) node.Children);
    }
  }

  /// <summary>Вызывает для всех дочерних нод переданной (или для корневых нод дерева, если параметр treeNodes == null) переданную обработку</summary>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей некоторому условию</param>
  /// <param name="recursive">Если true то обработка будет рекурсивной</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    (startTreeNode ?? navigatorTreeView.RootNode)?.InvokeWithChilds((Func<NavigatorTreeNode, bool>) null, predicate, recursive, autoPopulateNodes);
  }

  /// <summary>Вызывает для всех дочерних нод переданной (или для корневых нод дерева, если параметр treeNodes == null) переданную обработку</summary>
  /// <param name="condition">Условие, которому должны соответствовать наиболее близкие к корню иерархии ноды дерева</param>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей некоторому условию</param>
  /// <param name="recursive">Если true то обработка будет рекурсивной</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [NotNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    (startTreeNode ?? navigatorTreeView.RootNode)?.InvokeWithChilds(condition, predicate, recursive, autoPopulateNodes);
  }

  /// <summary>Вызывает для всех дочерних нод переданной (или для корневых нод дерева, если параметр treeNodes == null) переданную обработку</summary>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей некоторому условию
  /// Если вернёт false, то обработка прекращается</param>
  /// <param name="recursive">Если true то обработка будет рекурсивной</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [NotNull, InstantHandle] Func<NavigatorTreeNode, bool> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    (startTreeNode ?? navigatorTreeView.RootNode)?.InvokeWithChilds((Func<NavigatorTreeNode, bool>) null, predicate, recursive, autoPopulateNodes);
  }

  /// <summary>Вызывает для всех дочерних нод переданной (или для корневых нод дерева, если параметр treeNodes == null) переданную обработку</summary>
  /// <param name="condition">Условие, которому должны соответствовать наиболее близкие к корню иерархии ноды дерева</param>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей некоторому условию
  /// Если вернёт false, то обработка прекращается</param>
  /// <param name="recursive">Если true то обработка будет рекурсивной</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [CanBeNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] Func<NavigatorTreeNode, bool> predicate,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    (startTreeNode ?? navigatorTreeView.RootNode)?.InvokeWithChilds(condition, predicate, recursive, autoPopulateNodes);
  }

  /// <summary>Вызывает для всех дочерних нод переданной (или для корневых нод дерева, если параметр treeNodes == null) переданную обработку</summary>
  /// <param name="condition">Условие, которому должны соответствовать наиболее близкие к корню иерархии ноды дерева</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей условию condition</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [CanBeNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    [NotNull, InstantHandle] Func<NavigatorTreeNode, bool> invokeForChilds,
    [NotNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    (startTreeNode ?? navigatorTreeView.RootNode)?.InvokeWithChilds(condition, invokeForChilds, predicate, autoPopulateNodes);
  }

  /// <summary>Поиск первой ноды в дереве, соответствующей заданному условию</summary>
  /// <param name="condition">Условие, которому должна соответствовать разыскиваемая нода дерева</param>
  /// <param name="findInChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся поиск. Если null, то поиск начнётся с
  /// корневой ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  /// <returns>Первая найденная нода, соответствующая переданному условию, или null, если ни одна нода условию не соответствует</returns>
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static NavigatorTreeNode FindFirstNode(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [NotNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    [CanBeNull, InstantHandle] Func<NavigatorTreeNode, bool> findInChilds = null,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    return (startTreeNode ?? navigatorTreeView.RootNode)?.FindFirstNode(condition, findInChilds, autoPopulateNodes);
  }

  /// <summary>Вызывает для всех дочерних нод переданной (или для корневых нод дерева, если параметр treeNodes == null) переданную обработку</summary>
  /// <param name="condition">Условие, которому должны соответствовать наиболее близкие к корню иерархии ноды дерева</param>
  /// <param name="invokeForChilds">Перебирать ли дочерние ноды данной</param>
  /// <param name="predicate">Что делать с каждой наиболее близкой к корню иерархии нодой дерева, соответствующей условию condition</param>
  /// <param name="afterChildsProcessed">Метод обработки ноды после того, как обработаны все дочерние ноды</param>
  /// <param name="autoPopulateNodes">Загружать ли автоматически состав незагруженных нод если выше по иерархии не найдено ни одной ноды,
  /// удовлетворяющей переданному условию</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void InvokeForTreeNodes(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [CanBeNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    [CanBeNull, InstantHandle] Func<NavigatorTreeNode, bool> invokeForChilds,
    [CanBeNull, InstantHandle] Action<NavigatorTreeNode> predicate,
    [CanBeNull, InstantHandle] Action<NavigatorTreeNode> afterChildsProcessed,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    (startTreeNode ?? navigatorTreeView.RootNode)?.InvokeWithChilds(condition, invokeForChilds, afterChildsProcessed, predicate, autoPopulateNodes);
  }

  /// <summary>Последовательность нод (дочерних от указанной, или всех корневых нод), удовлетворяющих некому условию (которое необязательно
  /// указывать), рекурсивная или нет, с автоподгрузкой недогруженных узлов или нет</summary>
  /// <param name="condition">Условие, которому должны соответствовать ноды.</param>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> NodesEnumeration(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    [CanBeNull, InstantHandle] Func<NavigatorTreeNode, bool> condition,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    return (startTreeNode ?? navigatorTreeView.RootNode)?.EnumerationWithChilds(condition, recursive, autoPopulateNodes) ?? (IEnumerable<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
  }

  /// <summary>Последовательность нод (дочерних от указанной, или всех корневых нод), рекурсивная или нет, с автоподгрузкой недогруженных
  /// узлов или нет</summary>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<NavigatorTreeNode> NodesEnumeration(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    return (startTreeNode ?? navigatorTreeView.RootNode)?.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes) ?? (IEnumerable<NavigatorTreeNode>) Array.Empty<NavigatorTreeNode>();
  }

  /// <summary>Последовательность нод (дочерних от указанной, или всех корневых нод), рекурсивная или нет, с автоподгрузкой недогруженных
  /// узлов или нет</summary>
  /// <param name="recursive">Если true, то работает рекурсивно</param>
  /// <param name="autoPopulateNodes">Если true, то автоматом подгружает состав дочерних нод</param>
  /// <param name="startTreeNode">Начальная нода, с дочерних нод которой которых начнётся обработка. Если null, то обработка начнётся с
  /// корневых ноды. Используется для организации рекурсии, при внешних вызовах можно не передавать</param>
  /// <returns>Последовательность</returns>
  [NotNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static List<NavigatorTreeNode> NodesList(
    [NotNull] this NavigatorTreeView navigatorTreeView,
    bool recursive = true,
    bool autoPopulateNodes = false,
    [CanBeNull] NavigatorTreeNode startTreeNode = null)
  {
    NavigatorTreeNode navigatorTreeNode = startTreeNode ?? navigatorTreeView.RootNode;
    return (navigatorTreeNode != null ? navigatorTreeNode.EnumerationWithChilds((Func<NavigatorTreeNode, bool>) null, recursive, autoPopulateNodes).ToList<NavigatorTreeNode>() : (List<NavigatorTreeNode>) null) ?? new List<NavigatorTreeNode>();
  }

  /// <summary>
  /// Создать описание объекта или связи на основе указанных интерфейсов
  /// </summary>
  /// <param name="relID">Описание связи</param>
  /// <param name="objID">Описание версии объекта</param>
  /// <returns>Описание объекта или связи</returns>
  public static SimpleRelationPair GetSimplerelationPair(
    IDBRelationID relID,
    IDBTypedObjectID objID)
  {
    return new SimpleRelationPair(relID != null ? relID.Value : 0L, relID != null ? relID.RelationType : -1, objID != null ? objID.ObjectID : 0L, objID != null ? objID.ObjectType : -1);
  }

  /// <summary>
  /// Преобразовать порядок сортировки колонки дерева "Навигатора" в порядок сортировки колонки "Навигатора"
  /// </summary>
  /// <param name="value">Порядок сортировки колонки дерева "Навигатора"</param>
  /// <returns>Порядок сортировки колонки "Навигатора"</returns>
  public static NodeColumnSortOrder TreeToNavigatorSortDirection(ListSortDirection value)
  {
    return value == ListSortDirection.Ascending ? NodeColumnSortOrder.Ascending : NodeColumnSortOrder.Descending;
  }

  /// <summary>
  /// Метод позволяет отыскать для указанного узла (с объектом) в дереве
  /// путь к конфигурируемому родительскому узлу верхнего уровня,
  /// в составе которого содержится данный узел. Если узел не содержит
  /// объект, будет возвращено значение null
  /// </summary>
  /// <param name="node">Узел, содержащий объект</param>
  /// <returns>Путь к конфигурируемому родительскому узлу верхнего уровня,
  /// в составе которого содержится указанный узел</returns>
  public static RelationPath GetCompositionNodePath(NavigatorTreeNode node)
  {
    if (node == null)
      return (RelationPath) null;
    RelationPath compositionNodePath = new RelationPath();
    NavigatorTreeNode node1 = node;
    while (true)
    {
      INode nodeHandler = node.Tree.GetNodeHandler(node1);
      if (nodeHandler != null)
      {
        IDBRelationID data1 = nodeHandler.GetData(node1.NodeID, typeof (IDBRelationID)) as IDBRelationID;
        if (nodeHandler.GetData(node1.NodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data2 && data1 != null && data1.Value != 0L && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data1.RelationType) && node1.Parent != null && node1.Parent.InTree)
        {
          SimpleRelationPair simpleRelationPair = new SimpleRelationPair(data1.Value, data1.RelationType, data2.ObjectID, data2.ObjectType);
          compositionNodePath.Items.Insert(0, simpleRelationPair);
          node1 = node1.Parent;
        }
        else
          break;
      }
      else
        goto label_7;
    }
    return compositionNodePath;
label_7:
    return compositionNodePath;
  }

  /// <summary>
  /// Метод позволяет отыскать для указанного узла (с объектом) в дереве
  /// путь к конфигурируемому родительскому узлу верхнего уровня,
  /// в составе которого содержится данный узел. Если узел не содержит
  /// объект, будет возвращено значение null. В путь попадёт и указанный узел,
  /// если он является конфигурируемым типом объектов
  /// </summary>
  /// <param name="node">Узел, содержащий объект</param>
  /// <returns>Путь к конфигурируемому родительскому узлу верхнего уровня,
  /// в составе которого содержится указанный узел, а также указанный узел</returns>
  public static RelationPath GetConfiguredNodePath(NavigatorTreeNode node)
  {
    if (node == null)
      return (RelationPath) null;
    RelationPath configuredNodePath = new RelationPath();
    NavigatorTreeNode node1 = node;
    while (true)
    {
      INode nodeHandler = node.Tree.GetNodeHandler(node1);
      if (nodeHandler != null)
      {
        IDBRelationID data1 = nodeHandler.GetData(node1.NodeID, typeof (IDBRelationID)) as IDBRelationID;
        if (nodeHandler.GetData(node1.NodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data2)
        {
          if (data1 != null && data1.Value != 0L && MetaDataHelper.IsPdmPartiallyConfigurableRelationType(data1.RelationType) && node1.Parent != null && node1.Parent.InTree)
          {
            SimpleRelationPair simpleRelationPair = new SimpleRelationPair(data1.Value, data1.RelationType, data2.ObjectID, data2.ObjectType);
            configuredNodePath.Items.Insert(0, simpleRelationPair);
            node1 = node1.Parent;
          }
          else
            goto label_8;
        }
        else
          break;
      }
      else
        goto label_11;
    }
    return configuredNodePath;
label_8:
    if (MetaDataHelper.IsPdmConfigurableObjectType(data2.ObjectType))
    {
      SimpleRelationPair simpleRelationPair = new SimpleRelationPair(0L, -1, data2.ObjectID, data2.ObjectType);
      configuredNodePath.Items.Insert(0, simpleRelationPair);
    }
    return configuredNodePath;
label_11:
    return configuredNodePath;
  }

  /// <summary>
  /// Вернуть полный путь к текущей строке в гриде при условии, что она
  /// содержит связь. В путь попадёт также информация из дерева Навигатора, если
  /// его сервис доступен в контейнере. Путь будет рассчитан вверх до корневого
  /// узла в дереве Навигатора, содержащего родительский объект указанного типа
  /// </summary>
  /// <param name="node">Узел, содержащий объект</param>
  /// <param name="parentObjectTypeID">Идентификатор родительского типа объекта, который является корневым в составе.
  /// Если указать константу Intermech.Consts.UnknownObjectTypeId, то будет возвращён полный путь состава
  /// без учёта родительского типа</param>
  /// <param name="useInheritance">Если указать true, допускается прерывать поиск на объектах, тип которых унаследован от указанного родительского типа</param>
  /// <returns>Полный путь к найденному родительскому узлу, а также указанный узел, либо null</returns>
  public static RelationPath GetTypedParentObjectNodePath(
    NavigatorTreeNode node,
    int parentObjectTypeID,
    bool useInheritance)
  {
    if (node == null)
      return (RelationPath) null;
    RelationPath parentObjectNodePath = new RelationPath();
    NavigatorTreeNode node1 = node;
    do
    {
      INode nodeHandler = node.Tree.GetNodeHandler(node1);
      if (nodeHandler != null)
      {
        IDBRelationID data1 = nodeHandler.GetData(node1.NodeID, typeof (IDBRelationID)) as IDBRelationID;
        if (!(nodeHandler.GetData(node1.NodeID, typeof (IDBTypedObjectID)) is IDBTypedObjectID data2))
          return parentObjectNodePath;
        if (data1 != null && data1.Value != 0L && node1.Parent != null && node1.Parent.InTree)
        {
          SimpleRelationPair simpleRelationPair = new SimpleRelationPair(data1.Value, data1.RelationType, data2.ObjectID, data2.ObjectType);
          parentObjectNodePath.Items.Insert(0, simpleRelationPair);
          node1 = node1.Parent;
        }
        else
          goto label_9;
      }
      else
        goto label_12;
    }
    while (parentObjectTypeID == -1 || (!useInheritance || !MetaDataHelper.IsObjectTypeChildOf(data2.ObjectType, parentObjectTypeID)) && (useInheritance || data2.ObjectType != parentObjectTypeID));
    return parentObjectNodePath;
label_9:
    if (parentObjectTypeID == -1 || useInheritance && MetaDataHelper.IsObjectTypeChildOf(data2.ObjectType, parentObjectTypeID) || !useInheritance && data2.ObjectType == parentObjectTypeID)
    {
      SimpleRelationPair simpleRelationPair = new SimpleRelationPair(0L, -1, data2.ObjectID, data2.ObjectType);
      parentObjectNodePath.Items.Insert(0, simpleRelationPair);
    }
    return parentObjectNodePath;
label_12:
    return parentObjectNodePath;
  }
}
