
// Type: Intermech.Navigator.Controls.ObjectsCompositionNavigatorTree
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

/// <summary>Дерево для отображения структуры объекта</summary>
public class ObjectsCompositionNavigatorTree : 
  AdvNavigatorTreeView,
  IContextAware,
  ISelectedItemsHost,
  IIOSource,
  ICommandsProvider,
  ICommandTarget,
  ISupportInitialize,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  [CanBeNull]
  [ItemNotNull]
  private Lazy<IReadOnlyList<Descriptor>> _rootDbObjectDescriptors;
  /// <summary>Внешний метод создания дескриптора объекта</summary>
  [CanBeNull]
  public ObjectsCompositionNavigatorTree.OnCreateObjectDescriptorDelegate OnCreateObjectDescriptor;

  /// <summary>Идентификатор сфокусированной в дереве ноды</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public NodeID FocusedNodeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.FocusedNode?.NodeID as NodeID;
    }
  }

  /// <summary>Признак того, что в дереве сфокусирован объект</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ObjectIsFocused
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      NodeID focusedNodeId = this.FocusedNodeID;
      return focusedNodeId != null && focusedNodeId.IsObjectCategory();
    }
  }

  /// <summary>Идентификатор версии сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.FocusedNodeID?.ObjectID;
    }
  }

  /// <summary>Идентификатор сфокусированного в дереве объекта (!!! НЕ ВЕРСИИ !!!)</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.FocusedNodeID?.ID;
    }
  }

  /// <summary>Идентификатор типа сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int? FocusedObjectTypeID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetFocusedTypeID();
    }
  }

  /// <summary>Идентификатор связи сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectPrjLinkID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.FocusedNodeID?.PrjLinkID;
    }
  }

  /// <summary>Идентификатор владельца сфокусированного в дереве объекта</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long? FocusedObjectOwner
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.FocusedNodeID?.Owner;
    }
  }

  /// <summary>Заголовок сфокусированного в дереве объекта</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public string FocusedObjectCaption
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.FocusedNodeID?.Caption;
    }
  }

  /// <summary>Последовательность идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerableWithCapacity<NodeID> SelectedObjectNodeIDsEnumeration
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedNodesList.Map<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (treeNode => (NodeID) treeNode.NodeID)).Filter<NodeID>((Func<NodeID, bool>) (nodeID => nodeID != null && nodeID.IsObjectCategory() && nodeID.ObjectID != 0L));
    }
  }

  /// <summary>Список идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NodeID> SelectedObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedObjectNodeIDsEnumeration.AsList<NodeID>();
    }
  }

  /// <summary>Признак того, что в дереве выбран хотя 1 объект</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ObjectIsSelected
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedObjectNodeIDsEnumeration.Any<NodeID>();
    }
  }

  /// <summary>Список идентификаторов версий выбранных в дереве объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ObjectID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов выбранных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов типов выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> SelectedObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedObjectNodeIDsEnumeration.Map<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.ObjectTypeID)).DistinctWithCapacity<int>().AsList<int>();
    }
  }

  /// <summary>Список идентификаторов связей выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedObjectPrjLinkIDs
  {
    [DebuggerStepThrough] get
    {
      return this.SelectedObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.PrjLinkID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список заголовков выбранных в объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<string> SelectedObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedObjectNodeIDs.MapListReadOnly<NodeID, string>((Func<NodeID, string>) (nodeID => nodeID.Caption));
    }
  }

  /// <summary>Последовательность идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerableWithCapacity<NodeID> SelectedClosestToRootObjectNodeIDsEnumeration
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetSelectedNodesClosestToRoot().Map<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (treeNode => (NodeID) treeNode.NodeID)).Filter<NodeID>((Func<NodeID, bool>) (nodeID => nodeID != null && nodeID.IsObjectCategory() && nodeID.ObjectID != 0L));
    }
  }

  /// <summary>Список идентификаторов выбранных в дереве нод</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NodeID> SelectedClosestToRootObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedClosestToRootObjectNodeIDsEnumeration.AsList<NodeID>();
    }
  }

  /// <summary>Список идентификаторов версий выбранных в дереве объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedClosestToRootObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedClosestToRootObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ObjectID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов выбранных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedClosestToRootObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedClosestToRootObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов типов выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> SelectedClosestToRootObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedClosestToRootObjectNodeIDsEnumeration.Map<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.ObjectTypeID)).DistinctWithCapacity<int>().AsList<int>();
    }
  }

  /// <summary>Список идентификаторов связей выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> SelectedClosestToRootObjectPrjLinkIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedClosestToRootObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.PrjLinkID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список заголовков выбранных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<string> SelectedClosestToRootObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.SelectedClosestToRootObjectNodeIDs.MapListReadOnly<NodeID, string>((Func<NodeID, string>) (nodeID => nodeID.Caption));
    }
  }

  /// <summary>Список нод объектов без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NavigatorTreeNode> ObjectNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.NodesList().Filter<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
      {
        INodeID nodeId2 = node.NodeID;
        return (nodeId2 != null ? (nodeId2.IsObjectCategory() ? 1 : 0) : 0) != 0 && node.NodeID is NodeID nodeId3 && nodeId3.ObjectID != 0L;
      })).AsList<NavigatorTreeNode>();
    }
  }

  /// <summary>Последовательность интерфейсов идентификаторов нод объектов без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerableWithCapacity<NodeID> ObjectNodeIDsEnumeration
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.NodesList().Map<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (treeNode => (NodeID) treeNode.NodeID)).Filter<NodeID>((Func<NodeID, bool>) (nodeID => nodeID != null && nodeID.IsObjectCategory() && nodeID.ObjectID != 0L));
    }
  }

  /// <summary>Список интерфейсов идентификаторов нод объектов без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NodeID> ObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodeIDsEnumeration.AsList<NodeID>();
    }
  }

  /// <summary>Список идентификаторов версий объектов в дереве</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> ObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ObjectID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> ObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов типов объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> ObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodeIDsEnumeration.Map<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.ObjectTypeID)).DistinctWithCapacity<int>().AsList<int>();
    }
  }

  /// <summary>Список идентификаторов связей всех загруженных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> ObjectPrjLinkIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.PrjLinkID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список заголовков всех загруженных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<string> ObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.ObjectNodeIDs.MapListReadOnly<NodeID, string>((Func<NodeID, string>) (nodeID => nodeID.Caption));
    }
  }

  /// <summary>Список отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NavigatorTreeNode> CheckedObjectNodes
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetCheckedNodes().Filter<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (node =>
      {
        if (node != null)
        {
          INodeID nodeId2 = node.NodeID;
          bool? nullable = nodeId2 != null ? new bool?(nodeId2.IsObjectCategory()) : new bool?();
          bool flag = true;
          if (nullable.GetValueOrDefault() == flag & nullable.HasValue && node.NodeID is NodeID nodeId3)
            return nodeId3.ObjectID != 0L;
        }
        return false;
      })).AsList<NavigatorTreeNode>();
    }
  }

  /// <summary>Перечисление интерфейсов идентификаторов отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [ItemNotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IEnumerableWithCapacity<NodeID> CheckedObjectNodeIDsEnumeration
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetCheckedNodes().Map<NavigatorTreeNode, NodeID>((Func<NavigatorTreeNode, NodeID>) (treeNode => (NodeID) treeNode.NodeID)).Filter<NodeID>((Func<NodeID, bool>) (nodeID => nodeID != null && nodeID.IsObjectCategory() && nodeID.ObjectID != 0L));
    }
  }

  /// <summary>Список интерфейсов идентификаторов отмеченных нод без какой-либо фильтрации
  /// Метод рекурсивно перебирает только загруженные ноды</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<NodeID> CheckedObjectNodeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDsEnumeration.AsList<NodeID>();
    }
  }

  /// <summary>Признак того, что в дереве отмечен хотя бы 1 объект</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool ObjectIsChecked
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDs.Any<NodeID>();
    }
  }

  /// <summary>Список идентификаторов версий отмеченных в дереве объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> CheckedObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ObjectID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов отмеченных объектов (!!! НЕ ВЕРСИЙ !!!)</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> CheckedObjectIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.ID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список идентификаторов типов отмеченных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<int> CheckedObjectTypeIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDsEnumeration.Map<NodeID, int>((Func<NodeID, int>) (nodeID => nodeID.ObjectTypeID)).DistinctWithCapacity<int>().AsList<int>();
    }
  }

  /// <summary>Список идентификаторов связей отмеченных объектов</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public List<long> CheckedObjectPrjLinkIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDsEnumeration.Map<NodeID, long>((Func<NodeID, long>) (nodeID => nodeID.PrjLinkID)).DistinctWithCapacity<long>().AsList<long>();
    }
  }

  /// <summary>Список заголовков отмеченных в объекта</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IReadOnlyList<string> CheckedObjectCaptions
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.CheckedObjectNodeIDs.MapListReadOnly<NodeID, string>((Func<NodeID, string>) (nodeID => nodeID.Caption));
    }
  }

  /// <summary>Построить дерево на основе указанного дескриптора</summary>
  /// <param name="descriptor">Описание корневого узла дерева</param>
  protected override void BuildCore([NotNull] IDescriptor descriptor) => base.BuildCore(descriptor);

  /// <summary>Выполнить перестройку содержимого дерева (если есть изменения в коллекции Nodes)</summary>
  public override void RebuildTree()
  {
    base.RebuildTree();
    this._rootDbObjectDescriptors = new Lazy<IReadOnlyList<Descriptor>>(new Func<IReadOnlyList<Descriptor>>(this.GetRootDbObjectDescriptors));
    this.IsRootMultipleObjects = this.RootDescriptor is MultipleObjectsDescriptor;
  }

  /// <summary>Признак того, что в дерево загружено несколько объектов</summary>
  public bool IsRootMultipleObjects { get; private set; }

  /// <summary>Коллекция дескрипторов корневых объектов БД</summary>
  [CanBeNull]
  [ItemNotNull]
  public IReadOnlyList<Descriptor> RootDbObjectDescriptors
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._rootDbObjectDescriptors?.Value;
    }
  }

  [NotNull]
  [ItemNotNull]
  public IReadOnlyList<Descriptor> GetRootDbObjectDescriptors()
  {
    if (this.RootDescriptor == null)
      return (IReadOnlyList<Descriptor>) Array.Empty<Descriptor>();
    if (this.RootDescriptor is Descriptor rootDescriptor1)
      return (IReadOnlyList<Descriptor>) new Descriptor[1]
      {
        rootDescriptor1
      };
    return this.RootDescriptor is MultipleObjectsDescriptor rootDescriptor2 ? (IReadOnlyList<Descriptor>) rootDescriptor2.DbObjectDescriptors.ToArray<Descriptor>() : throw new InvalidOperationException($"Unsupported root descriptor type \"{this.RootDescriptor.GetType()}\"");
  }

  /// <summary>Коллекция дескрипторов корневых объектов БД</summary>
  [NotNull]
  public IReadOnlyList<long> RootDbObjectVersionIDs
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      IReadOnlyList<Descriptor> objectDescriptors = this.RootDbObjectDescriptors;
      return (objectDescriptors != null ? (IReadOnlyList<long>) objectDescriptors.Map<Descriptor, long>((Func<Descriptor, long>) (objectDescriptor => objectDescriptor.ObjectID)).AsList<long>() : (IReadOnlyList<long>) null) ?? (IReadOnlyList<long>) Array.Empty<long>();
    }
    set => this.Build((IReadOnlyCollection<long>) value);
  }

  /// <summary>Корневой объект БД (если объектов в корне несколько - свалит Exception)</summary>
  [CanBeNull]
  public Descriptor RootDbObjectDescriptor
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.RootDescriptor == null ? (Descriptor) null : (Descriptor) this.RootDescriptor;
    }
  }

  /// <summary>Идентификатор версии корневого объекта БД (если объектов в корне несколько - свалит Exception)</summary>
  public long RootDbObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      Descriptor objectDescriptor = this.RootDbObjectDescriptor;
      return objectDescriptor == null ? 0L : objectDescriptor.ObjectID;
    }
    set
    {
      if (value != 0L)
        this.Build((IDescriptor) new Descriptor(value));
      else
        this.Build((IDescriptor) null);
    }
  }

  /// <summary>Уровень вложенности ноды в структуре объекта. Не учитывает ноду "Несколько объектов" если она присутствует в корне</summary>
  public int GetObjectNodeLevel([NotNull] NavigatorTreeNode navigatorTreeNode)
  {
    int objectNodeLevel = 0;
    for (; navigatorTreeNode != null; navigatorTreeNode = navigatorTreeNode.Parent)
    {
      if (navigatorTreeNode.NodeID != null && navigatorTreeNode.NodeID.GetObjVerID(false) != 0L)
        ++objectNodeLevel;
    }
    return objectNodeLevel;
  }

  /// <summary>Является ли данная нода корневым объектом</summary>
  public bool IsNodeRootObject([NotNull] NavigatorTreeNode navigatorTreeNode)
  {
    return !this.IsRootMultipleObjects ? navigatorTreeNode.Level == 1 : navigatorTreeNode.Level == 2;
  }

  /// <summary>Загрузить в дерево объект с переданным идентификатором версии объекта</summary>
  /// <param name="newRootDbObjectVersionID">Идентификатор версии корневого объекта БД</param>
  public virtual void Build(long newRootDbObjectVersionID)
  {
    if (newRootDbObjectVersionID != 0L)
    {
      if (!this.IsRootMultipleObjects && this.RootDbObjectVersionID == newRootDbObjectVersionID)
        return;
      this.Build((IDescriptor) new Descriptor(newRootDbObjectVersionID));
    }
    else
      this.BuildEmpty();
  }

  /// <summary>Загрузить в дерево объекты с переданными идентификаторами версиями объектов</summary>
  /// <param name="rootDbObjectVersionID">Идентификатор версии корневого объекта БД</param>
  public virtual void Build(
    [NotNull] IReadOnlyCollection<long> newRootDbObjectVersionIDs,
    [CanBeNull] string multipleObjectsNodeCaption = null)
  {
    if ((newRootDbObjectVersionIDs != null ? (newRootDbObjectVersionIDs.IsEmpty<long>() ? 1 : 0) : 1) != 0)
    {
      this.BuildEmpty();
    }
    else
    {
      newRootDbObjectVersionIDs = (IReadOnlyCollection<long>) newRootDbObjectVersionIDs.DistinctWithCapacity<long>().AsList<long>();
      if (newRootDbObjectVersionIDs.MoreThanOne<long>())
      {
        if (newRootDbObjectVersionIDs.SequenceEqualIgnoreOrder<long>((IEnumerable<long>) this.RootDbObjectVersionIDs))
          return;
        this.Build((IDescriptor) new MultipleObjectsDescriptor(multipleObjectsNodeCaption ?? LocalizationHolder.GetString("Client.Core_1634"), (IEnumerable<IDescriptor>) newRootDbObjectVersionIDs.Select<long, Descriptor>((Func<long, Descriptor>) (objectVersionID => this.OnCreateObjectDescriptor != null ? this.OnCreateObjectDescriptor(objectVersionID) : new Descriptor(objectVersionID)))));
      }
      else
        this.Build(newRootDbObjectVersionIDs.First<long>());
    }
  }

  public void BuildEmpty()
  {
    if (this.RootDescriptor == null)
      return;
    this.Build((IDescriptor) null);
  }

  /// <summary>Функция проверки необходимости рекурсивной загрузки состава</summary>
  protected override bool DefaultCompositionLoadCheckFunc(NavigatorTreeNode node)
  {
    if (this.CheckBoxStyle == NavigatorTreeViewCheckBoxStyle.None || node.CheckState != CheckState.Unchecked)
      return true;
    if (node.ShowCheckState)
      return false;
    return !this.IsRootMultipleObjects ? node.Level <= 1 : node.Level <= 2;
  }

  public delegate Descriptor OnCreateObjectDescriptorDelegate(long objectVersionID);
}
