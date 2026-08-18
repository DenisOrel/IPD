// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.TreeBuilder
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Comparers;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

[ToolboxItem(typeof (TreeView))]
public class TreeBuilder : Component
{
  private static string DummyNodeText = "\u0001";
  private static ICategoryTypeIconService _iconService;
  private List<long> _catalogs = new List<long>();
  private List<int> _additionalTypes = new List<int>(4);
  private Dictionary<long, TreeNode> _nodes = new Dictionary<long, TreeNode>(0);
  private static ImageList _imageList = new ImageList()
  {
    ColorDepth = ColorDepth.Depth24Bit
  };
  internal TreeView _tree;
  private IContainer components;

  private void RebuildTree()
  {
    if (this.DesignMode || this._tree == null)
      return;
    this._tree.BeginUpdate();
    try
    {
      this._tree.Nodes.Clear();
      this._nodes.Clear();
      if (this._catalogs == null)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        foreach (long catalog in this._catalogs)
        {
          IDBObject dbObject = session.GetObject(catalog);
          if (dbObject.TypeID != Intermech.Imbase.Consts.ImbaseCatalogTypeID)
            throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString("Imbase.Client_30"), (object) catalog));
          NodeInfo nodeInfo = new NodeInfo(dbObject.ObjectID, dbObject.TypeID);
          if (this.OnFilterNode(nodeInfo))
          {
            int num = nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? TreeBuilder.GetIconIndex(nodeInfo._typeId, (object) ApplicabilityStatusHelper.GetStatus(nodeInfo.Applicability)) : TreeBuilder.GetIconIndex(nodeInfo._typeId);
            TreeNode node = new TreeNode(dbObject.Caption, num, num)
            {
              Tag = (object) nodeInfo
            };
            this.AddDummyNode(node);
            this._tree.Nodes.Add(node);
            if (!this._nodes.ContainsKey(catalog))
              this._nodes.Add(catalog, node);
          }
        }
      }
    }
    finally
    {
      this._tree.EndUpdate();
    }
  }

  private void ExploreBranch(TreeNode node, List<long> objIds)
  {
    if (node.Tag is NodeInfo tag && objIds.Contains(tag.ObjectId))
    {
      node.Checked = true;
      this._tree.SelectedNode = node;
      node.EnsureVisible();
    }
    if (this.UnexploredNode(node))
      this.ExploreNode(node);
    for (int index = 0; index < node.Nodes.Count; ++index)
      this.ExploreBranch(node.Nodes[index], objIds);
  }

  private void GetCheckedNodes(TreeNodeCollection nodes, List<long> result)
  {
    int count = nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      TreeNode node = nodes[index];
      if (node.Checked && node.Tag is NodeInfo tag && !result.Contains(tag._objectId))
        result.Add(tag._objectId);
      if (node.Nodes.Count > 0)
        this.GetCheckedNodes(node.Nodes, result);
    }
  }

  private void SetCheckedNodes(TreeNodeCollection nodes, List<long> result)
  {
    int count = nodes.Count;
    for (int index = 0; index < count; ++index)
    {
      TreeNode node = nodes[index];
      bool flag = false;
      if (result != null && node.Tag is NodeInfo tag)
        flag = result.Contains(tag._objectId);
      node.Checked = flag;
      if (node.Nodes.Count > 0)
        this.SetCheckedNodes(node.Nodes, result);
    }
  }

  private bool ToBoolean(object value)
  {
    return value != null && !DBNull.Value.Equals(value) && Convert.ToBoolean(value);
  }

  private bool OnFilterNode(NodeInfo nodeInfo)
  {
    if (this.FilterObject == null)
      return true;
    NodeInfoEventArgs e = new NodeInfoEventArgs(nodeInfo);
    NodeFilterEventHandler filterObject = this.FilterObject;
    if (filterObject != null)
      filterObject((object) this, e);
    return !e.Cancel;
  }

  private DataView GetParentPathView(DataTable dataTable)
  {
    if (dataTable == null)
      return (DataView) null;
    if (dataTable.Columns.IndexOf("F_PARENT_PATH") == -1)
    {
      dataTable.Columns.Add("F_PARENT_PATH", typeof (string));
      int columnIndex1 = dataTable.Columns.IndexOf("F_PARENT_PATH");
      int columnIndex2 = dataTable.Columns.IndexOf("F_PATH");
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        string str = row[columnIndex2].ToString();
        if (str.Length > 2)
          row[columnIndex1] = (object) str.Substring(0, str.Length - 2);
      }
    }
    return new DataView(dataTable)
    {
      Sort = "F_PARENT_PATH ASC, F_SORT ASC"
    };
  }

  private List<Tuple<string, NodeInfo>> CreateCatalogNodes(
    DataTable dt,
    IDictionary<long, TreeNode> nodeIds,
    Dictionary<string, List<TreeNode>> nodes,
    Dictionary<long, TreeNode> favoriteNodeIds)
  {
    int icSort = dt.Columns.IndexOf("F_SORT");
    List<Tuple<string, NodeInfo>> list = dt.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (row => Convert.ToInt32(row["F_OBJECT_TYPE"]) == Intermech.Imbase.Consts.ImbaseCatalogTypeID && !string.IsNullOrEmpty(Convert.ToString(row["F_PATH"])))).Select<DataRow, Tuple<string, NodeInfo>>((System.Func<DataRow, Tuple<string, NodeInfo>>) (row => new Tuple<string, NodeInfo>(Convert.ToString(row["CAPTION"]), new NodeInfo(Convert.ToInt64(row["F_OBJECT_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]))
    {
      Path = Convert.ToString(row["F_PATH"]),
      Order = icSort != -1 ? (!Convert.IsDBNull(row[icSort]) ? Convert.ToInt32(row[icSort]) : 0) : 0
    }))).Where<Tuple<string, NodeInfo>>((System.Func<Tuple<string, NodeInfo>, bool>) (catalogInfo => this.OnFilterNode(catalogInfo.Item2))).ToList<Tuple<string, NodeInfo>>();
    if (list.Count == 0)
      return list;
    foreach (Tuple<string, NodeInfo> tuple in list)
    {
      int iconIndex = TreeBuilder.GetIconIndex(tuple.Item2.TypeId);
      TreeNode node = new TreeNode(tuple.Item1, iconIndex, iconIndex)
      {
        Tag = (object) tuple.Item2
      };
      this._tree.Nodes.Add(node);
      if (!this._catalogs.Contains(tuple.Item2.ObjectId))
        this._catalogs.Add(tuple.Item2.ObjectId);
      if (nodeIds != null && !nodeIds.ContainsKey(tuple.Item2.ObjectId))
        nodeIds.Add(tuple.Item2.ObjectId, node);
      if (!favoriteNodeIds.ContainsKey(tuple.Item2.ObjectId))
        favoriteNodeIds.Add(tuple.Item2.ObjectId, node);
      if (!nodes.ContainsKey(tuple.Item2.Path))
        nodes.Add(tuple.Item2.Path, new List<TreeNode>()
        {
          node
        });
      else
        nodes[tuple.Item2.Path].Add(node);
    }
    return list;
  }

  private void PopulateFavoriteNodes(
    DataTable dt,
    Dictionary<long, TreeNode> favoriteNodes,
    Dictionary<string, List<TreeNode>> nodes,
    List<Tuple<string, NodeInfo>> catalogsInfo)
  {
    DataTable foldersForCatalogs;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IImbaseServer server = TreeBuilder.GetServer(session);
      if (server == null)
        return;
      long[] array = catalogsInfo.Select<Tuple<string, NodeInfo>, long>((System.Func<Tuple<string, NodeInfo>, long>) (x => x.Item2.ObjectId)).ToArray<long>();
      foldersForCatalogs = server.GetFavoriteFoldersForCatalogs(session.SessionGUID, array, true);
    }
    if (foldersForCatalogs == null)
      return;
    HashSet<long> imbaseObjHashSet = dt.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x["F_OBJECT_ID"]))).ToHashSet<long>();
    int iSort = foldersForCatalogs.Columns.IndexOf("F_SORT");
    List<\u003C\u003Ef__AnonymousType1<string, NodeInfo, long>> list = foldersForCatalogs.AsEnumerable().Select(row =>
    {
      string str = Convert.ToString(row["CAPTION"]);
      NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row["F_OBJECT_ID"]), Convert.ToInt32(row["F_OBJECT_TYPE"]));
      nodeInfo.Path = Convert.ToString(row["F_PATH"]);
      nodeInfo.Order = iSort != -1 ? (!Convert.IsDBNull(row[iSort]) ? Convert.ToInt32(row[iSort]) : 0) : 0;
      long int64 = Convert.ToInt64(row["F_PROJ_ID"]);
      return new
      {
        Caption = str,
        NodeInfo = nodeInfo,
        ParentNodeId = int64
      };
    }).Where(catalogInfo => this.OnFilterNode(catalogInfo.NodeInfo)).ToList();
    List<\u003C\u003Ef__AnonymousType1<string, NodeInfo, long>> filteredFavoritesInfo = list.Where(x =>
    {
      if (x.NodeInfo.TypeId == Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
        return false;
      return !imbaseObjHashSet.Contains(x.NodeInfo.ObjectId) || string.IsNullOrEmpty(x.NodeInfo.Path);
    }).ToList();
    if (filteredFavoritesInfo.Count != 0)
      list.RemoveAll(x => filteredFavoritesInfo.Contains(x));
    bool flag = true;
    while (flag)
    {
      flag = false;
      foreach (var data in list.ToArray())
      {
        TreeNode treeNode;
        if (favoriteNodes.TryGetValue(data.ParentNodeId, out treeNode))
        {
          flag = true;
          int iconIndex = TreeBuilder.GetIconIndex(data.NodeInfo.TypeId);
          TreeNode node = new TreeNode(data.Caption, iconIndex, iconIndex)
          {
            Tag = (object) data.NodeInfo
          };
          treeNode.Nodes.Add(node);
          if (!favoriteNodes.ContainsKey(data.NodeInfo.ObjectId))
            favoriteNodes.Add(data.NodeInfo.ObjectId, node);
          if (data.NodeInfo.TypeId != Intermech.Imbase.Consts.ImbaseFavoritesTypeID)
          {
            if (!nodes.ContainsKey(data.NodeInfo.Path))
              nodes.Add(data.NodeInfo.Path, new List<TreeNode>()
              {
                node
              });
            else
              nodes[data.NodeInfo.Path].Add(node);
          }
          list.Remove(data);
        }
      }
    }
  }

  internal static IImbaseServer GetServer(IUserSession session)
  {
    return session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
  }

  public TreeBuilder()
  {
    this.InitializeComponent();
    this.AdditionalTypes = new int[2]
    {
      Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID,
      Intermech.Imbase.Consts.ImbaseTableRefTypeID
    };
  }

  public TreeBuilder(IContainer container)
    : this()
  {
    container.Add((IComponent) this);
  }

  public static int GetIconIndex(int objectType, object data = null)
  {
    return TreeBuilder.GetIconIndex(4, objectType, data);
  }

  public static int GetIconIndex(int category, int objectType, object data = null)
  {
    ImageList imageList = TreeBuilder._imageList;
    string key = data != null ? $"{objectType}{data}" : objectType.ToString();
    int iconIndex = imageList.Images.IndexOfKey(key);
    if (iconIndex != -1)
      return iconIndex;
    int index = TreeBuilder.IconService.IndexOf(category, objectType, data);
    if (index == -1)
      index = TreeBuilder.IconService.IndexOf(category, objectType);
    using (Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/))
    {
      using (Graphics g = Graphics.FromImage((Image) bitmap))
      {
        TreeBuilder.IconService.ImageList.Draw(g, 0, 0, index);
        imageList.Images.Add(key, (Image) bitmap);
        imageList.Draw(g, 0, 0, 0);
        return imageList.Images.Count - 1;
      }
    }
  }

  public void AddDummyNode(TreeNode node) => node.Nodes.Add(TreeBuilder.DummyNodeText);

  public bool IsDummyNode(TreeNode node)
  {
    return node != null && node.Text.Equals(TreeBuilder.DummyNodeText);
  }

  public void LoadFullTree(List<long> objIds)
  {
    this._tree.BeginUpdate();
    try
    {
      List<TreeNode> treeNodeList = new List<TreeNode>();
      foreach (TreeNode node in this._tree.Nodes)
      {
        if (this.UnexploredNode_Ex(node))
          treeNodeList.Add(node);
      }
      if (treeNodeList.Count != 0)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (sessionKeeper.Session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService)
          {
            List<long> longList = new List<long>();
            Dictionary<long, string> dictionary1 = new Dictionary<long, string>();
            foreach (TreeNode treeNode in treeNodeList)
            {
              NodeInfo tag = (NodeInfo) treeNode.Tag;
              if (!longList.Contains(tag._objectId))
                longList.Add(tag._objectId);
            }
            List<ColumnDescriptor> columnDescriptorList = new List<ColumnDescriptor>()
            {
              new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
              new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
            };
            DBRecordSetParams rParams = new DBRecordSetParams(new List<ConditionStructure>()
            {
              new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), (object) null, LogicalOperators.NONE, 0, false, AttributeSourceTypes.Auto, ColumnContents.Text)
            }.ToArray(), columnDescriptorList.ToArray())
            {
              Tags = new HybridDictionary()
              {
                [(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true
              }
            };
            DataTable dataTable1 = ImbaseHelper.SelectObjects(sessionKeeper.Session, rParams, Intermech.Imbase.Consts.Imbase_NavTree_ObjectTypeIDS);
            if (dataTable1 != null && dataTable1.Rows.Count > 0)
            {
              foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
              {
                long int64 = Convert.ToInt64(row[Intermech.Imbase.Consts.F_OBJECT_ID]);
                string str = row[Intermech.Imbase.Consts.ClassifFolderKeyAttId.ToString()].ToString();
                if (int64 != 0L && !dictionary1.ContainsKey(int64))
                  dictionary1.Add(int64, str);
              }
            }
            foreach (TreeNode treeNode1 in treeNodeList)
            {
              NodeInfo tag = (NodeInfo) treeNode1.Tag;
              Dictionary<string, TreeNode> dictionary2 = new Dictionary<string, TreeNode>();
              string key1 = dictionary1.ContainsKey(tag._objectId) ? dictionary1[tag._objectId] : string.Empty;
              dictionary2.Add(key1, treeNode1);
              treeNode1.Nodes.Clear();
              DataTable dataTable2 = customService.LoadAllCatalogTable(sessionKeeper.Session.SessionGUID, tag._objectId, false);
              if (dataTable2 != null && dataTable2.Rows.Count > 0)
              {
                DataView parentPathView = this.GetParentPathView(dataTable2);
                int count = parentPathView.Count;
                int columnIndex1 = dataTable2.Columns.IndexOf("F_OBJECT_ID");
                int columnIndex2 = dataTable2.Columns.IndexOf("CAPTION");
                int columnIndex3 = dataTable2.Columns.IndexOf("F_OBJECT_TYPE");
                int columnIndex4 = dataTable2.Columns.IndexOf("F_PATH");
                dataTable2.Columns.IndexOf("F_GUID");
                int columnIndex5 = dataTable2.Columns.IndexOf("F_SORT");
                for (int recordIndex = 0; recordIndex < count; ++recordIndex)
                {
                  DataRow row = parentPathView[recordIndex].Row;
                  string key2 = Convert.ToString(row[columnIndex4]);
                  if (!dictionary2.ContainsKey(key2))
                  {
                    NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]));
                    int num = nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? TreeBuilder.GetIconIndex(nodeInfo._typeId, (object) ApplicabilityStatusHelper.GetStatus(nodeInfo.Applicability)) : TreeBuilder.GetIconIndex(nodeInfo._typeId);
                    TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), num, num);
                    if (!DBNull.Value.Equals(row[columnIndex5]))
                      nodeInfo.Order = Convert.ToInt32(row[columnIndex5]);
                    node.Tag = (object) nodeInfo;
                    int length = key2.Length - 2;
                    string key3 = key2.Substring(0, length);
                    TreeNode treeNode2 = dictionary2.ContainsKey(key3) ? dictionary2[key3] : (TreeNode) null;
                    if (treeNode2 != null)
                    {
                      treeNode2.Nodes.Add(node);
                      if (!this._nodes.ContainsKey(nodeInfo.ObjectId))
                        this._nodes.Add(nodeInfo.ObjectId, node);
                      dictionary2.Add(key2, node);
                    }
                  }
                }
                foreach (TreeNode node in this._tree.Nodes)
                  node.Collapse(true);
              }
            }
          }
        }
      }
      for (int index = 0; index < this._tree.Nodes.Count; ++index)
        this.ExploreBranch(this._tree.Nodes[index], objIds);
    }
    finally
    {
      this._tree.EndUpdate();
      TreeNode selectedNode = this._tree.SelectedNode;
      this._tree.Sort();
      if (selectedNode != null)
        this._tree.SelectedNode = selectedNode;
    }
  }

  public void ExploreNode(TreeNode rootNode)
  {
    rootNode.TreeView.BeginUpdate();
    try
    {
      if (!(rootNode.Tag is NodeInfo tag))
        return;
      rootNode.Nodes.Clear();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IImbaseServer server = TreeBuilder.GetServer(session);
        if (server == null)
          return;
        DataTable subfolders = server.GetSubfolders(session.SessionGUID, tag._objectId, this.AdditionalTypes);
        if (subfolders == null)
          return;
        List<TreeNode> treeNodeList = new List<TreeNode>();
        int columnIndex1 = subfolders.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex2 = subfolders.Columns.IndexOf("CAPTION");
        int columnIndex3 = subfolders.Columns.IndexOf("F_OBJECT_TYPE");
        int columnIndex4 = subfolders.Columns.IndexOf("F_EXP");
        int columnIndex5 = subfolders.Columns.IndexOf("F_SORT");
        int columnIndex6 = subfolders.Columns.IndexOf("F_APPLICABILITY");
        foreach (DataRow row in (InternalDataCollectionBase) subfolders.Rows)
        {
          NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]));
          if (this.OnFilterNode(nodeInfo))
          {
            int num = nodeInfo.TypeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? TreeBuilder.GetIconIndex(nodeInfo._typeId, (object) ApplicabilityStatusHelper.GetStatus(nodeInfo.Applicability)) : TreeBuilder.GetIconIndex(nodeInfo._typeId);
            bool boolean = this.ToBoolean(row[columnIndex4]);
            TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), num, num);
            if (columnIndex5 != -1)
              nodeInfo._order = Convert.ToInt32(row[columnIndex5]);
            node.Tag = (object) nodeInfo;
            if (nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID)
            {
              if (columnIndex6 != -1)
                nodeInfo.Applicability = Convert.ToString(row[columnIndex6]);
              if (boolean)
                this.AddDummyNode(node);
              rootNode.Nodes.Add(node);
            }
            else
              treeNodeList.Add(node);
            if (!this._nodes.ContainsKey(nodeInfo.ObjectId))
              this._nodes.Add(nodeInfo.ObjectId, node);
          }
        }
        if (treeNodeList.Count == 0)
          return;
        rootNode.Nodes.AddRange(treeNodeList.ToArray());
      }
    }
    finally
    {
      rootNode.TreeView.EndUpdate();
    }
  }

  public void FullExploreNode(TreeNode rootNode)
  {
    rootNode.TreeView.BeginUpdate();
    try
    {
      if (!(rootNode.Tag is NodeInfo tag))
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        Dictionary<string, TreeNode> dictionary = new Dictionary<string, TreeNode>();
        rootNode.Nodes.Clear();
        IUserSession session = sessionKeeper.Session;
        IImbaseServer server = TreeBuilder.GetServer(session);
        if (server == null)
          return;
        List<int> intList = new List<int>((IEnumerable<int>) this.AdditionalTypes);
        if (tag.TypeId == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
          intList.Add(tag.TypeId);
        DataTable allSubfolders = server.GetAllSubfolders(session.SessionGUID, tag._objectId, intList.ToArray());
        if (allSubfolders == null)
          return;
        string empty = string.Empty;
        string columnName1 = "F_OBJECT_ID";
        if (allSubfolders.Columns.IndexOf(columnName1) == -1)
        {
          string columnName2 = -2.ToString();
          columnName1 = allSubfolders.Columns.IndexOf(columnName2) != -1 ? $"[{columnName2}]" : string.Empty;
        }
        DataRow[] dataRowArray = string.IsNullOrEmpty(columnName1) ? (DataRow[]) null : allSubfolders.Select($"{"F_OBJECT_ID"} = {tag.ObjectId}");
        if (dataRowArray != null && dataRowArray.Length != 0)
          empty = dataRowArray[0]["F_PATH"].ToString();
        dictionary.Add(empty, rootNode);
        DataView parentPathView = this.GetParentPathView(allSubfolders);
        int count = parentPathView.Count;
        int columnIndex1 = allSubfolders.Columns.IndexOf("F_OBJECT_ID");
        int columnIndex2 = allSubfolders.Columns.IndexOf("CAPTION");
        int columnIndex3 = allSubfolders.Columns.IndexOf("F_OBJECT_TYPE");
        int columnIndex4 = allSubfolders.Columns.IndexOf("F_PATH");
        int columnIndex5 = allSubfolders.Columns.IndexOf("F_SORT");
        int columnIndex6 = allSubfolders.Columns.IndexOf("F_APPLICABILITY");
        for (int recordIndex = 0; recordIndex < count; ++recordIndex)
        {
          DataRow row = parentPathView[recordIndex].Row;
          string key1 = Convert.ToString(row[columnIndex4]);
          if (!dictionary.ContainsKey(key1))
          {
            NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]));
            nodeInfo.Applicability = Convert.ToString(row[columnIndex6]);
            if (this.OnFilterNode(nodeInfo))
            {
              int num = nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? TreeBuilder.GetIconIndex(nodeInfo._typeId, (object) ApplicabilityStatusHelper.GetStatus(nodeInfo.Applicability)) : TreeBuilder.GetIconIndex(nodeInfo._typeId);
              TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), num, num);
              if (!DBNull.Value.Equals(row[columnIndex5]))
                nodeInfo.Order = Convert.ToInt32(row[columnIndex5]);
              node.Tag = (object) nodeInfo;
              int length = key1.Length - 2;
              string key2 = key1.Substring(0, length);
              TreeNode treeNode = dictionary.ContainsKey(key2) ? dictionary[key2] : (TreeNode) null;
              if (treeNode != null)
              {
                treeNode.Nodes.Add(node);
                if (!this._nodes.ContainsKey(nodeInfo.ObjectId))
                  this._nodes.Add(nodeInfo.ObjectId, node);
                dictionary.Add(key1, node);
              }
            }
          }
        }
        rootNode.Collapse(false);
        rootNode.Expand();
      }
    }
    finally
    {
      rootNode.TreeView.EndUpdate();
    }
  }

  public bool UnexploredNode(TreeNode node)
  {
    if (node != null && node.Nodes.Count > 0)
    {
      int count = node.Nodes.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.IsDummyNode(node.Nodes[index]))
          return true;
      }
    }
    return false;
  }

  public bool UnexploredNode_Ex(TreeNode node)
  {
    if (node != null)
    {
      if (this.IsDummyNode(node))
        return true;
      int count = node.Nodes.Count;
      for (int index = 0; index < count; ++index)
      {
        if (this.UnexploredNode_Ex(node.Nodes[index]))
          return true;
      }
    }
    return false;
  }

  public void ShowTreeForType(int needType)
  {
    this._tree.BeginUpdate();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IImbaseServer server = TreeBuilder.GetServer(session);
        if (server == null)
          return;
        this.CreateTree(server.GetFoldersForCreateType(session.SessionGUID, (object) needType, (long[]) null, true));
      }
    }
    finally
    {
      this._tree.EndUpdate();
    }
  }

  public void CreateTree(DataTable dt) => this.CreateTree(dt, (IDictionary<long, TreeNode>) null);

  public void CreateTree(DataTable dt, IDictionary<long, TreeNode> nodeIds)
  {
    this._tree.BeginUpdate();
    try
    {
      IntPtr handle = this._tree.Handle;
      this._tree.Nodes.Clear();
      this._catalogs.Clear();
      this._nodes.Clear();
      nodeIds?.Clear();
      if (dt == null || dt.Rows.Count == 0)
        return;
      Dictionary<string, List<TreeNode>> nodes = new Dictionary<string, List<TreeNode>>();
      Dictionary<long, TreeNode> dictionary = new Dictionary<long, TreeNode>();
      if (this.AllowFavourites)
      {
        List<Tuple<string, NodeInfo>> catalogNodes = this.CreateCatalogNodes(dt, nodeIds, nodes, dictionary);
        this.PopulateFavoriteNodes(dt, dictionary, nodes, catalogNodes);
      }
      DataView parentPathView = this.GetParentPathView(dt);
      int columnIndex1 = dt.Columns.IndexOf("F_OBJECT_ID");
      int columnIndex2 = dt.Columns.IndexOf("CAPTION");
      int columnIndex3 = dt.Columns.IndexOf("F_OBJECT_TYPE");
      int columnIndex4 = dt.Columns.IndexOf("#FLT");
      int columnIndex5 = dt.Columns.IndexOf("F_PATH");
      int columnIndex6 = dt.Columns.IndexOf("F_SORT");
      int columnIndex7 = dt.Columns.IndexOf("F_APPLICABILITY");
      int count = parentPathView.Count;
      List<TreeNode> treeNodeList1 = new List<TreeNode>(count);
      for (int recordIndex = 0; recordIndex < count; ++recordIndex)
      {
        DataRow row = parentPathView[recordIndex].Row;
        string str = Convert.ToString(row[columnIndex5]);
        int int32 = Convert.ToInt32(row[columnIndex3]);
        if (!string.IsNullOrEmpty(str) && (!this.AllowFavourites || int32 != Intermech.Imbase.Consts.ImbaseCatalogTypeID))
        {
          NodeInfo nodeInfo = new NodeInfo(Convert.ToInt64(row[columnIndex1]), Convert.ToInt32(row[columnIndex3]))
          {
            Path = str
          };
          nodeInfo.Applicability = Convert.ToString(row[columnIndex7]);
          if (this.OnFilterNode(nodeInfo))
          {
            int num = nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID ? TreeBuilder.GetIconIndex(nodeInfo._typeId, (object) ApplicabilityStatusHelper.GetStatus(nodeInfo.Applicability)) : TreeBuilder.GetIconIndex(nodeInfo._typeId);
            if (columnIndex6 != -1)
              nodeInfo.Order = Convert.ToInt32(row[columnIndex6]);
            TreeNode node = new TreeNode(Convert.ToString(row[columnIndex2]), num, num)
            {
              Tag = (object) nodeInfo
            };
            if (nodeIds != null && !nodeIds.ContainsKey(nodeInfo.ObjectId))
              nodeIds.Add(nodeInfo.ObjectId, node);
            bool flag = false;
            if (columnIndex4 > -1)
            {
              flag = this.ToBoolean(row[columnIndex4]);
              if (flag)
                treeNodeList1.Add(node);
            }
            if ((nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseFolderTypeID || nodeInfo._typeId == Intermech.Imbase.Consts.ImbaseCatalogTypeID) && flag)
              this.AddDummyNode(node);
            string key1 = Convert.ToString(row[columnIndex5]);
            int length = key1.Length - 2;
            string key2 = key1.Substring(0, length);
            List<TreeNode> treeNodeList2;
            if (nodes.TryGetValue(key2, out treeNodeList2))
            {
              foreach (TreeNode treeNode in treeNodeList2)
              {
                if (treeNode != null)
                {
                  if (treeNode.Nodes.Count == 1 && this.IsDummyNode(treeNode.Nodes[0]))
                    treeNode.Nodes.Clear();
                  if (nodes.ContainsKey(key1))
                    node = (TreeNode) node.Clone();
                  treeNode.Nodes.Add(node);
                  if (!nodes.ContainsKey(key1))
                    nodes.Add(key1, new List<TreeNode>()
                    {
                      node
                    });
                  else
                    nodes[key1].Add(node);
                }
              }
            }
            else
            {
              this._tree.Nodes.Add(node);
              if (!nodes.ContainsKey(key1))
                nodes.Add(key1, new List<TreeNode>()
                {
                  node
                });
              else
                nodes[key1].Add(node);
            }
            if (!this._nodes.ContainsKey(nodeInfo.ObjectId))
              this._nodes.Add(nodeInfo.ObjectId, node);
          }
        }
      }
      foreach (TreeNode treeNode in treeNodeList1)
      {
        for (TreeNode parent = treeNode.Parent; parent != null; parent = parent.Parent)
          parent.Expand();
      }
    }
    finally
    {
      this._tree.EndUpdate();
    }
  }

  public void ShowList(long[] foldersList)
  {
    this._tree.BeginUpdate();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IImbaseServer server = TreeBuilder.GetServer(session);
        if (server == null)
          return;
        if (foldersList.Length == 0)
          foldersList = new long[1];
        this.CreateTree(server.GetFoldersForObjects(session.SessionGUID, foldersList, (long[]) null));
      }
    }
    finally
    {
      this._tree.EndUpdate();
    }
  }

  public void CreateFilterTree(long folderId, string ownerGuid, long catalogId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (!(sessionKeeper.Session.GetCustomService(typeof (IFolderFilterService)) is IFolderFilterService customService))
        return;
      this.CreateTree(customService.LoadFoldersFor(session.SessionGUID, folderId, ownerGuid, catalogId));
    }
  }

  public bool SetCheckedNodes(List<long> objIDs)
  {
    if (this._nodes == null || objIDs == null || objIDs.Count == 0)
      return false;
    foreach (long objId in objIDs)
    {
      if (objId != 0L)
      {
        if (!this._nodes.ContainsKey(objId))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
              return false;
            Guid sessionGuid = sessionKeeper.Session.SessionGUID;
            long[] objectList = new long[1]{ objId };
            DataTable foldersForObjects = customService.GetFoldersForObjects(sessionGuid, objectList, (long[]) null);
            if (foldersForObjects == null || foldersForObjects.Rows.Count == 0)
              return false;
            string str = foldersForObjects.Select($"F_OBJECT_ID = {objId}")[0]["F_PATH"].ToString();
            for (int length = 2; length < str.Length; length += 2)
            {
              long int64 = Convert.ToInt64(foldersForObjects.Select($"F_PATH = '{str.Substring(0, length)}'")[0]["F_OBJECT_ID"]);
              if (this._nodes.ContainsKey(int64) && !this._nodes[int64].IsExpanded)
                this._nodes[int64].Expand();
            }
          }
        }
        this._nodes[objId].Checked = true;
        this.ExpandNode(this._nodes[objId]);
      }
    }
    return true;
  }

  private void ExpandNode(TreeNode node)
  {
    for (TreeNode parent = node.Parent; parent != null; parent = parent.Parent)
      parent.Expand();
  }

  public bool SetSelectedNode(long objID)
  {
    if (this._nodes == null || objID == 0L)
      return false;
    if (!this._nodes.ContainsKey(objID))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable1;
        if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        {
          dataTable1 = (DataTable) null;
        }
        else
        {
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          long[] objectList = new long[1]{ objID };
          dataTable1 = customService.GetFoldersForObjects(sessionGuid, objectList, (long[]) null);
        }
        DataTable dataTable2 = dataTable1;
        if (dataTable2 == null || dataTable2.Rows.Count == 0)
          return false;
        string str = dataTable2.Select($"F_OBJECT_ID = {objID}")[0]["F_PATH"].ToString();
        for (int length = 2; length < str.Length; length += 2)
        {
          DataRow[] dataRowArray = dataTable2.Select($"F_PATH = '{str.Substring(0, length)}'");
          if (dataRowArray.Length != 0)
          {
            long int64 = Convert.ToInt64(dataRowArray[0]["F_OBJECT_ID"]);
            if (this._nodes.ContainsKey(int64) && !this._nodes[int64].IsExpanded)
              this._nodes[int64].Expand();
          }
        }
      }
    }
    if (!this._nodes.ContainsKey(objID))
      return false;
    this._tree.SelectedNode = this._nodes[objID];
    return true;
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  private int[] AdditionalTypes
  {
    get => this._additionalTypes.ToArray();
    set
    {
      this._additionalTypes.Clear();
      if (value == null)
        return;
      int length = value.Length;
      for (int index = 0; index < length; ++index)
      {
        int num = value[index];
        if (!this._additionalTypes.Contains(num))
          this._additionalTypes.Add(num);
      }
    }
  }

  private static ICategoryTypeIconService IconService
  {
    get
    {
      return TreeBuilder._iconService ?? (TreeBuilder._iconService = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService);
    }
  }

  internal static ImageList ImageList => TreeBuilder._imageList;

  public TreeView TreeView
  {
    get => this._tree;
    set
    {
      if (this._tree == value)
        return;
      if (this._tree != null)
      {
        this._tree.AfterSelect -= new TreeViewEventHandler(this.TreeNode_AfterSelect);
        this._tree.BeforeExpand -= new TreeViewCancelEventHandler(this.TreeNode_BeforeExpand);
      }
      this._tree = value;
      if (this._tree == null)
        return;
      this._tree.AfterSelect += new TreeViewEventHandler(this.TreeNode_AfterSelect);
      this._tree.BeforeExpand += new TreeViewCancelEventHandler(this.TreeNode_BeforeExpand);
      this._tree.ImageList = TreeBuilder._imageList;
      if (this._tree.TreeViewNodeSorter != null)
        return;
      this._tree.TreeViewNodeSorter = (IComparer) new NodeComparer();
    }
  }

  [Browsable(false)]
  public long[] Catalogs
  {
    get => this._catalogs.ToArray();
    set
    {
      this._catalogs.Clear();
      if (value != null)
      {
        foreach (long num in value)
        {
          if (!this._catalogs.Contains(num))
            this._catalogs.Add(num);
        }
      }
      this.RebuildTree();
    }
  }

  [Browsable(false)]
  public long[] Checked
  {
    get
    {
      if (this._tree == null)
        return (long[]) null;
      List<long> result = new List<long>(32 /*0x20*/);
      this.GetCheckedNodes(this._tree.Nodes, result);
      return result.ToArray();
    }
    set
    {
      List<long> result = new List<long>();
      if (value != null)
        result.AddRange((IEnumerable<long>) value);
      if (this._tree == null)
        return;
      this.SetCheckedNodes(this._tree.Nodes, result);
    }
  }

  [DefaultValue(true)]
  public bool ShowCatalogRecords
  {
    get => this._additionalTypes.Contains(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
    set
    {
      bool flag = this._additionalTypes.Contains(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
      if (value)
      {
        if (flag)
          return;
        this._additionalTypes.Add(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
      }
      else
      {
        if (!flag)
          return;
        this._additionalTypes.Remove(Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID);
      }
    }
  }

  [DefaultValue(true)]
  public bool ShowTableReferences
  {
    get => this._additionalTypes.Contains(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
    set
    {
      bool flag = this._additionalTypes.Contains(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      if (value)
      {
        if (flag)
          return;
        this._additionalTypes.Add(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      }
      else
      {
        if (!flag)
          return;
        this._additionalTypes.Remove(Intermech.Imbase.Consts.ImbaseTableRefTypeID);
      }
    }
  }

  public Dictionary<long, TreeNode> NodeCache
  {
    [DebuggerStepThrough] get => this._nodes;
  }

  public bool AllowFavourites { get; set; }

  public event SelectEventHandler Selected;

  public event NodeFilterEventHandler FilterObject;

  private void TreeNode_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    TreeNode node = e.Node;
    if (!this.UnexploredNode(node))
      return;
    if (node.Level == 0)
      this.ExploreNode(node);
    else
      this.FullExploreNode(node);
  }

  private void TreeNode_AfterSelect(object sender, TreeViewEventArgs e)
  {
    SelectEventHandler selected = this.Selected;
    if (selected == null)
      return;
    selected((object) this, new TreeViewSelectEventArgs(e, e.Node.Tag as NodeInfo));
  }

  public void UpdateUnExploreRows(DataTable dbTable, IUserSession session)
  {
    if (dbTable == null)
      throw new ArgumentNullException(nameof (dbTable));
    IImbaseServer server = TreeBuilder.GetServer(session);
    if (server == null || dbTable.Rows.Count == 0)
      return;
    int columnIndex1 = dbTable.Columns.IndexOf("F_OBJECT_ID");
    int columnIndex2 = dbTable.Columns.IndexOf("#FLT");
    int columnIndex3 = dbTable.Columns.IndexOf("F_PATH");
    if (columnIndex2 == -1)
      return;
    Dictionary<long, DataRow> dictionary = new Dictionary<long, DataRow>(dbTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dbTable.Rows)
    {
      if (this.ToBoolean(row[columnIndex2]))
        dictionary[Convert.ToInt64(row[columnIndex1])] = row;
    }
    if (dictionary.Count == 0)
      return;
    List<long> longList = new List<long>((IEnumerable<long>) dictionary.Keys);
    DataTable subfolders = server.GetSubfolders(session.SessionGUID, longList.ToArray(), this.AdditionalTypes);
    List<string> list = new List<string>(subfolders != null ? subfolders.Rows.Count : 0);
    if (subfolders != null)
    {
      int columnIndex4 = subfolders.Columns.IndexOf("F_PATH");
      foreach (DataRow row in (InternalDataCollectionBase) subfolders.Rows)
      {
        string str = row[columnIndex4].ToString();
        if (str.Length >= 2)
          list.Add(str.Remove(str.Length - 2));
      }
      GenericListHelper.MakeUnique<string>(list);
    }
    foreach (DataRow dataRow in dictionary.Values)
    {
      string str = dataRow[columnIndex3].ToString();
      if (list.BinarySearch(str) < 0)
        dataRow[columnIndex2] = (object) false;
    }
    dbTable.AcceptChanges();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
  }
}
