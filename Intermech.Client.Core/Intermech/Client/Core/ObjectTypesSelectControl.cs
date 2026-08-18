
// Type: Intermech.Client.Core.ObjectTypesSelectControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>дерево типов объектов</summary>
internal class ObjectTypesSelectControl : UserControl, IComparer
{
  private ImageList _imageList1;
  private bool _sortByCode;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeView tvObjectTypes;

  public event SelectObjectTypeHandler OnSelectObjectType;

  public int ObjectTypeID
  {
    get
    {
      return this.tvObjectTypes.SelectedNode == null || !(this.tvObjectTypes.SelectedNode.Tag is ObjectTypeTreeNode) ? -1 : ((ObjectTypeTreeNode) this.tvObjectTypes.SelectedNode.Tag).ObjectTypeID;
    }
  }

  public ObjectTypesSelectControl() => this.InitializeComponent();

  private void CreateCheckBoxes()
  {
    if (this._imageList1 != null)
      return;
    this._imageList1 = new ImageList();
    this._imageList1.Images.Add(Resources.Unchecked);
    this._imageList1.Images.Add(Resources.Checked1);
    this._imageList1.Images.Add(Resources.Indeterminate);
    this.tvObjectTypes.StateImageList = this._imageList1;
  }

  private void BuildTree(IUserSession session, IList<int> enabledObjTypes)
  {
    this.tvObjectTypes.TreeViewNodeSorter = (IComparer) null;
    if (enabledObjTypes != null && enabledObjTypes.Count > 0)
    {
      List<ObjectTypeTreeNode> nodes1 = new List<ObjectTypeTreeNode>();
      for (int index1 = 0; index1 < enabledObjTypes.Count; ++index1)
      {
        int objectTypeID = enabledObjTypes[index1];
        List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(objectTypeID);
        List<int> parentIds = new List<int>();
        for (int index2 = parentsIdReverse.Count - 1; index2 >= 0; --index2)
        {
          int objTypeID = parentsIdReverse[index2];
          if (enabledObjTypes.IndexOf(objTypeID) >= 0 || MetaDataHelper.GetObjectType(objTypeID).VersionsMode == ObjectVersionModes.Abstract)
            parentIds.Insert(0, objTypeID);
          else
            break;
        }
        if (parentIds.Count == 0 && !nodes1.Exists((Predicate<ObjectTypeTreeNode>) (x => x.ObjectTypeID == objectTypeID)))
        {
          nodes1.Add(new ObjectTypeTreeNode(objectTypeID, -1));
        }
        else
        {
          List<ObjectTypeTreeNode> typeTreeNodesList = this.CreateObjectTypeTreeNodesList(parentIds, objectTypeID, nodes1);
          nodes1.AddRange((IEnumerable<ObjectTypeTreeNode>) typeTreeNodesList);
        }
      }
      Dictionary<int, TreeNode> dictionary = new Dictionary<int, TreeNode>(nodes1.Count);
      while (nodes1.Exists((Predicate<ObjectTypeTreeNode>) (x => !x.Handled)))
      {
        foreach (ObjectTypeTreeNode objType in nodes1)
        {
          if (!objType.Handled)
          {
            if (objType.ParentTypeID == -1)
            {
              dictionary.Add(objType.ObjectTypeID, this.AddNode(this.tvObjectTypes.Nodes, objType, MetaDataHelper.GetObjectTypeName(objType.ObjectTypeID), false));
              objType.Handled = true;
            }
            else
            {
              TreeNode treeNode;
              if (dictionary.TryGetValue(objType.ParentTypeID, out treeNode))
              {
                if (!dictionary.ContainsKey(objType.ObjectTypeID))
                  dictionary.Add(objType.ObjectTypeID, this.AddNode(treeNode.Nodes, objType, MetaDataHelper.GetObjectTypeName(objType.ObjectTypeID), false));
                objType.Handled = true;
              }
            }
          }
        }
      }
      for (TreeNodeCollection nodes2 = this.tvObjectTypes.Nodes; nodes2.Count == 1; nodes2 = nodes2[0].Nodes)
        nodes2[0].Expand();
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectTypeCollection(-1, true).Select(string.Empty, (object[]) null).Rows)
      {
        if ((Convert.ToInt32(row["F_OPTIONS"]) & 32 /*0x20*/) == 0)
          this.AddNode(this.tvObjectTypes.Nodes, new ObjectTypeTreeNode(Convert.ToInt32(row["F_OBJECT_TYPE"]), -1), Convert.ToString(row["F_OBJ_TYPE_NAME"]), true);
      }
    }
    this.tvObjectTypes.Sort();
  }

  public int Compare(object x, object y)
  {
    if (x is TreeNode treeNode1 && y is TreeNode treeNode2)
    {
      if (!this._sortByCode)
        return string.Compare(treeNode1.Text, treeNode2.Text);
      if (treeNode1.Tag is ObjectTypeTreeNode tag1 && treeNode2.Tag is ObjectTypeTreeNode tag2)
        return string.Compare(tag1.Code, tag2.Code);
    }
    return 0;
  }

  private string DocumentTypeName(
    Guid sessionGuid,
    int documentTypeID,
    string documentTypeName,
    IDocumentTypeSettingsService docTypeService,
    out string documentTypeCode)
  {
    DocumentTypeSettings settings = docTypeService.GetSettings(sessionGuid, documentTypeID);
    documentTypeCode = string.IsNullOrEmpty(settings.DocumentTypeCode) ? (string) null : settings.DocumentTypeCode;
    return string.IsNullOrEmpty(documentTypeCode) ? documentTypeName : $"{documentTypeName} ({documentTypeCode})";
  }

  private void BuildList(IUserSession session, IList<int> enabledObjTypes, bool sortByCode)
  {
    this.tvObjectTypes.TreeViewNodeSorter = (IComparer) this;
    List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00070-306c-11d8-b4e9-00304f19f545"));
    IDocumentTypeSettingsService customService = (IDocumentTypeSettingsService) session.GetCustomService(typeof (IDocumentTypeSettingsService));
    if (enabledObjTypes != null && enabledObjTypes.Count > 0)
    {
      for (int index = 0; index < enabledObjTypes.Count; ++index)
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(enabledObjTypes[index]);
        if (objectType.VersionsMode != ObjectVersionModes.Abstract && (objectType.Options & ObjectTypeOptions.DisableManualCreate) != ObjectTypeOptions.DisableManualCreate)
        {
          string documentTypeCode = (string) null;
          string objTypeName = childrenIdRecursive.Contains(objectType.ObjectTypeID) ? this.DocumentTypeName(session.SessionGUID, objectType.ObjectTypeID, objectType.ObjectTypeName, customService, out documentTypeCode) : objectType.ObjectTypeName;
          this.AddNode(this.tvObjectTypes.Nodes, new ObjectTypeTreeNode(objectType.ObjectTypeID, -1, documentTypeCode), objTypeName, false);
        }
      }
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectTypeCollection(-2, true).Select(string.Empty, (object[]) null).Rows)
      {
        if ((Convert.ToInt32(row["F_OPTIONS"]) & 32 /*0x20*/) != 32 /*0x20*/ && Convert.ToInt32(row["F_VERSIONABLE"]) != 0)
        {
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          string documentTypeName = Convert.ToString(row["F_OBJ_TYPE_NAME"]);
          string documentTypeCode = (string) null;
          string objTypeName = childrenIdRecursive.Contains(int32) ? this.DocumentTypeName(session.SessionGUID, int32, documentTypeName, customService, out documentTypeCode) : documentTypeName;
          this.AddNode(this.tvObjectTypes.Nodes, new ObjectTypeTreeNode(int32, -1, documentTypeCode), objTypeName, false);
        }
      }
    }
  }

  public void BuildTree(IList<int> enabledObjTypes, int selectedType, bool checkBoxes)
  {
    this.Build(enabledObjTypes, selectedType, true, false, checkBoxes);
  }

  public void BuildList(
    IList<int> enabledObjTypes,
    int selectedType,
    bool sortByCode,
    bool checkBoxes)
  {
    this.Build(enabledObjTypes, selectedType, false, sortByCode, checkBoxes);
  }

  /// <summary>Инициализация контрола данными</summary>
  /// <param name="enabledObjTypes">Разрешенные типы объектов</param>
  /// <param name="selectedType">Выделенный тип</param>
  /// <param name="inTree">Данные ввиде дерева</param>
  /// <param name="checkBoxes">Отобразить checkBox-ы</param>
  private void Build(
    IList<int> enabledObjTypes,
    int selectedType,
    bool inTree,
    bool sortByCode,
    bool checkBoxes)
  {
    if (checkBoxes)
      this.CreateCheckBoxes();
    this._sortByCode = sortByCode;
    this.tvObjectTypes.ImageList = (ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService).ImageList;
    this.tvObjectTypes.TreeViewNodeSorter = (IComparer) new NodesComparer();
    this.tvObjectTypes.Nodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (inTree)
      {
        this.BuildTree(sessionKeeper.Session, enabledObjTypes);
        this.tvObjectTypes.ShowPlusMinus = true;
      }
      else
      {
        this.BuildList(sessionKeeper.Session, enabledObjTypes, sortByCode);
        this.tvObjectTypes.ShowPlusMinus = false;
      }
      bool flag = false;
      if (selectedType != -1)
      {
        TreeNode node1 = this.FindNode(this.tvObjectTypes.Nodes, selectedType);
        if (node1 != null)
        {
          this.tvObjectTypes.SelectedNode = node1;
          node1.Expand();
        }
        else
        {
          List<int> parentsIdReverse = MetaDataHelper.GetObjectTypeParentsIDReverse(selectedType);
          TreeNodeCollection nodes = this.tvObjectTypes.Nodes;
          foreach (int objectTypeID in parentsIdReverse)
          {
            TreeNode node2 = this.FindNode(nodes, objectTypeID);
            if (node2 != null)
            {
              this.LoadLevel(node2);
              nodes = node2.Nodes;
            }
            else
              break;
          }
          this.SelectNode(nodes, selectedType);
        }
        flag = true;
      }
      if (flag)
        return;
      bool isAdmin = sessionKeeper.Session.IsAdmin;
      Guid g = new Guid("cad00049-306c-11d8-b4e9-00304f19f545");
      foreach (TreeNode node in this.tvObjectTypes.Nodes)
      {
        if (node.Tag != null)
        {
          IDBLifecycleStepCollection lifecycleStepCollection = sessionKeeper.Session.GetLifecycleStepCollection(((ObjectTypeTreeNode) node.Tag).ObjectTypeID);
          if (lifecycleStepCollection != null)
          {
            DataSet schema = lifecycleStepCollection.GetSchema();
            if (schema != null && schema.Tables.Count != 0 && schema.Tables["IMS_LC_STEPS"] != null && schema.Tables["IMS_LC_STEPS"].Rows.Count != 0)
            {
              int firstStep = lifecycleStepCollection.GetFirstStep();
              IDBLifecycleStep lifecycleStep = sessionKeeper.Session.GetLifecycleStep(firstStep);
              if (lifecycleStep != null && isAdmin ^ sessionKeeper.Session.GetLifecycleLevel(lifecycleStep.LevelID).GUID.Equals(g))
              {
                this.tvObjectTypes.SelectedNode = node;
                break;
              }
            }
          }
        }
      }
    }
  }

  public void CheckNodes(bool check, List<int> uncheckedTypes)
  {
    this.CheckNodes(this.tvObjectTypes.Nodes, uncheckedTypes, check);
    foreach (TreeNode node in this.tvObjectTypes.Nodes)
      this.SetCheckedParent(node);
    this.UpdateStateIndexes(true);
  }

  public List<int> UncheckedObjectTypes
  {
    get
    {
      List<int> uncheckedNodes = this.GetUncheckedNodes(this.tvObjectTypes.Nodes);
      return uncheckedNodes.Count != 0 ? uncheckedNodes : (List<int>) null;
    }
  }

  public TreeView TreeView => this.tvObjectTypes;

  private List<int> GetUncheckedNodes(TreeNodeCollection nodes)
  {
    List<int> uncheckedNodes1 = new List<int>();
    foreach (TreeNode node in nodes)
    {
      ObjectTypeTreeNode tag = (ObjectTypeTreeNode) node.Tag;
      if (tag != null && tag.Checked != CheckState.Checked)
      {
        uncheckedNodes1.Add(((ObjectTypeTreeNode) node.Tag).ObjectTypeID);
        if (node.Nodes != null && node.Nodes.Count > 0)
        {
          List<int> uncheckedNodes2 = this.GetUncheckedNodes(node.Nodes);
          if (uncheckedNodes2.Count > 0)
            uncheckedNodes1.AddRange((IEnumerable<int>) uncheckedNodes2);
        }
      }
    }
    return uncheckedNodes1;
  }

  private void CheckNodes(TreeNodeCollection nodes, List<int> uncheckedTypes, bool check)
  {
    foreach (TreeNode node in nodes)
    {
      ObjectTypeTreeNode tag = (ObjectTypeTreeNode) node.Tag;
      if (tag != null)
      {
        CheckState checkState = check ? CheckState.Checked : CheckState.Unchecked;
        if (uncheckedTypes != null && uncheckedTypes.Contains(tag.ObjectTypeID))
        {
          checkState = CheckState.Unchecked;
          uncheckedTypes.Remove(tag.ObjectTypeID);
          if (uncheckedTypes.Count == 0)
            uncheckedTypes = (List<int>) null;
        }
        tag.Checked = checkState;
        this.LoadLevel(node);
        if (node.Nodes.Count > 0)
          this.CheckNodes(node.Nodes, uncheckedTypes, check);
      }
    }
  }

  private List<int> LoadLevel(TreeNode rootNode)
  {
    if (rootNode.Nodes.Count != 1 || rootNode.Nodes[0].Tag != null)
      return (List<int>) null;
    rootNode.Nodes.Clear();
    ObjectTypeTreeNode tag = (ObjectTypeTreeNode) rootNode.Tag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectTypeCollection(tag.ObjectTypeID, true).Select(string.Empty);
      List<int> intList = new List<int>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if ((Convert.ToInt32(row["F_OPTIONS"]) & 32 /*0x20*/) == 0)
        {
          int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
          intList.Add(int32);
          this.AddNode(rootNode.Nodes, new ObjectTypeTreeNode(int32, tag.ObjectTypeID, true), Convert.ToString(row["F_OBJ_TYPE_NAME"]), true);
        }
      }
      return intList.Count > 0 ? intList : (List<int>) null;
    }
  }

  private void SelectNode(TreeNodeCollection nodes, int objTypeID)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Tag != null && ((ObjectTypeTreeNode) node.Tag).ObjectTypeID == objTypeID)
      {
        this.tvObjectTypes.SelectedNode = node;
        node.Expand();
        break;
      }
    }
  }

  private TreeNode FindNode(TreeNodeCollection nodes, int objectTypeID)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Tag != null && ((ObjectTypeTreeNode) node.Tag).ObjectTypeID == objectTypeID)
        return node;
    }
    return (TreeNode) null;
  }

  private TreeNode AddNode(
    TreeNodeCollection tnc,
    ObjectTypeTreeNode objType,
    string objTypeName,
    bool createTempNode)
  {
    TreeNode node1 = new TreeNode()
    {
      Tag = (object) objType,
      Text = objTypeName
    };
    node1.ImageIndex = node1.SelectedImageIndex = Statics.IconSrv.IndexOf(4, objType.ObjectTypeID);
    tnc.Add(node1);
    if (createTempNode)
    {
      List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objType.ObjectTypeID);
      if (objectTypeChildrenId != null && objectTypeChildrenId.Count > 0)
      {
        TreeNode node2 = new TreeNode()
        {
          Tag = (object) null
        };
        node1.Nodes.Add(node2);
      }
    }
    return node1;
  }

  private List<ObjectTypeTreeNode> CreateObjectTypeTreeNodesList(
    List<int> parentIds,
    int child,
    List<ObjectTypeTreeNode> nodes)
  {
    List<ObjectTypeTreeNode> typeTreeNodesList = new List<ObjectTypeTreeNode>();
    for (int index = parentIds.Count - 1; index >= 0; --index)
    {
      int parentID = parentIds[index];
      typeTreeNodesList.Add(new ObjectTypeTreeNode(child, parentID));
      if (!nodes.Exists((Predicate<ObjectTypeTreeNode>) (x => x.ObjectTypeID == parentID)))
      {
        if (index == 0)
          typeTreeNodesList.Add(new ObjectTypeTreeNode(parentID, -1));
        child = parentID;
      }
      else
        break;
    }
    return typeTreeNodesList;
  }

  private void TvObjectTypes_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (this.OnSelectObjectType == null || e.Node.Tag == null)
      return;
    ObjectTypeTreeNode tag = (ObjectTypeTreeNode) e.Node.Tag;
    if (tag.Enabled && MetaDataHelper.GetObjectType(tag.ObjectTypeID).VersionsMode == ObjectVersionModes.Abstract)
      tag.Enabled = false;
    this.OnSelectObjectType((object) this, new SelectObjectTypeEventArgs(tag.ObjectTypeID, tag.Enabled));
  }

  private void TvObjectTypes_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    this.LoadLevel(e.Node);
  }

  private void SetCheckedParent(TreeNode node)
  {
    ObjectTypeTreeNode tag1 = (ObjectTypeTreeNode) node.Tag;
    bool flag1 = false;
    bool flag2 = false;
    bool flag3 = false;
    foreach (TreeNode node1 in node.Nodes)
    {
      ObjectTypeTreeNode tag2 = (ObjectTypeTreeNode) node1.Tag;
      if (tag2.Checked == CheckState.Unchecked)
        flag2 = true;
      else if (tag2.Checked == CheckState.Indeterminate)
        flag3 = true;
      else if (tag2.Checked == CheckState.Checked)
        flag1 = true;
      if ((flag2 | flag3) & flag1)
        break;
    }
    if (flag1 && !flag2 && !flag3)
      tag1.Checked = CheckState.Checked;
    else if (!flag1 & flag2 && !flag3)
      tag1.Checked = CheckState.Unchecked;
    else if (flag1 && flag2 | flag3 || ((flag1 ? 0 : (!flag2 ? 1 : 0)) & (flag3 ? 1 : 0)) != 0)
      tag1.Checked = CheckState.Indeterminate;
    if (node.Parent != null)
      this.SetCheckedParent(node.Parent);
    else
      this.UpdateNodeStateIndex(node, true);
  }

  /// <summary>Обновление StateIndex'ов у нодов дерева.</summary>
  private void UpdateStateIndexes(bool recursiveMode)
  {
    foreach (TreeNode node in this.tvObjectTypes.Nodes)
      this.UpdateNodeStateIndex(node, recursiveMode);
  }

  /// <summary>Обновление StateIndex'а у нода.</summary>
  /// <param name="node"></param>
  /// <param name="recursiveMode"></param>
  private void UpdateNodeStateIndex(TreeNode node, bool recursiveMode)
  {
    ObjectTypeTreeNode tag = (ObjectTypeTreeNode) node.Tag;
    node.StateImageIndex = (int) tag.Checked;
    if (!recursiveMode || node.Nodes.Count <= 0)
      return;
    foreach (TreeNode node1 in node.Nodes)
      this.UpdateNodeStateIndex(node1, recursiveMode);
  }

  private void TvObjectTypes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    if ((sender as TreeView).HitTest(e.X, e.Y).Location != TreeViewHitTestLocations.StateImage)
      return;
    TreeNode node = e.Node;
    ObjectTypeTreeNode tag = (ObjectTypeTreeNode) e.Node.Tag;
    if (tag == null)
      return;
    if (tag.Checked == CheckState.Checked)
      tag.Checked = CheckState.Unchecked;
    else if (tag.Checked != CheckState.Checked)
      tag.Checked = CheckState.Checked;
    this.LoadLevel(e.Node);
    this.CheckNodes(e.Node.Nodes, (List<int>) null, tag.Checked == CheckState.Checked);
    if (e.Node.Parent != null)
      this.SetCheckedParent(e.Node.Parent);
    else
      this.UpdateNodeStateIndex(e.Node, true);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.tvObjectTypes = new TreeView();
    this.SuspendLayout();
    this.tvObjectTypes.Dock = DockStyle.Fill;
    this.tvObjectTypes.Location = new Point(0, 0);
    this.tvObjectTypes.Name = "tvObjectTypes";
    this.tvObjectTypes.Size = new Size(856, 493);
    this.tvObjectTypes.TabIndex = 0;
    this.tvObjectTypes.BeforeExpand += new TreeViewCancelEventHandler(this.TvObjectTypes_BeforeExpand);
    this.tvObjectTypes.AfterSelect += new TreeViewEventHandler(this.TvObjectTypes_AfterSelect);
    this.tvObjectTypes.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.TvObjectTypes_NodeMouseClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tvObjectTypes);
    this.Name = "ObjectTypesSelectTree";
    this.Size = new Size(856, 493);
    this.ResumeLayout(false);
  }
}
