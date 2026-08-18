
// Type: Intermech.Client.Core.TreeViewForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// 
/// </summary>
public class TreeViewForm : Form
{
  private TreeNode _selectedNode;
  private int _constraintGroupIndex;
  private List<int> _filter;
  private List<int> _filterForTree;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel pnlButtons;
  private Button btnCancel;
  private Button btnOK;
  private TreeView trvObj;

  /// <summary>
  /// 
  /// </summary>
  public List<int> SelectedNodeID
  {
    get
    {
      return this._constraintGroupIndex != 1 ? new List<int>() : this.SelectedNodeData.Keys.Cast<int>().ToList<int>();
    }
  }

  /// <summary>Хранит пару "Guid - текст" выбранного узла.</summary>
  public Hashtable SelectedNodeData { get; set; }

  /// <summary>Конструктор.</summary>
  public TreeViewForm()
  {
    this.InitializeComponent();
    this.SelectedNodeData = new Hashtable();
  }

  /// <summary>Конструктор.</summary>
  /// <param name="nConstraintGroupIndex">Индекс группы</param>
  /// <param name="arrFilter">Список идентификаторов типов объектов.
  /// В дерево будут загружаться только те типы объектов, идентификаторы которых содежутся в этом списке.</param>
  /// <param name="filterForTree">Фильтр для построение дерева</param>
  /// <remarks>filterForTree создавался прежде всего для групп атрибутов, чтобы в дереве отображались только указанные группы</remarks>
  public TreeViewForm(int nConstraintGroupIndex, List<int> arrFilter, List<int> filterForTree = null)
    : this()
  {
    this._constraintGroupIndex = nConstraintGroupIndex;
    this._filter = arrFilter ?? new List<int>();
    this._filterForTree = filterForTree;
    if (this._filterForTree != null)
      this._filterForTree.Sort();
    switch (nConstraintGroupIndex)
    {
      case 1:
        this.Text += LocalizationHolder.rm.GetString("Client.Core_1090");
        int num1 = Statics.IconSrv.IndexOf(3, 0);
        this.trvObj.Nodes.Add(new TreeNode(LocalizationHolder.rm.GetString("Client.Core_54"), num1, num1));
        break;
      case 2:
        this.Text += LocalizationHolder.rm.GetString("Client.Core_1091");
        int num2 = Statics.IconSrv.IndexOf(4, 0);
        this.trvObj.Nodes.Add(new TreeNode(LocalizationHolder.rm.GetString("Client.Core_88"), num2, num2));
        break;
      case 3:
        this.Text += LocalizationHolder.rm.GetString("Client.Core_1092");
        int num3 = Statics.IconSrv.IndexOf(6, 0);
        this.trvObj.Nodes.Add(new TreeNode(LocalizationHolder.rm.GetString("Client.Core_1093"), num3, num3));
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.Size = this.Owner != null ? new Size(this.Owner.Width, this.Owner.Height) : new Size(400, 500);
    this.trvObj.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    switch (this._constraintGroupIndex)
    {
      case 1:
        IDBAttributesGroupInfoCollection attributesGroupCollection = service.GetAttributesGroupCollection(-1, true);
        if (attributesGroupCollection != null)
        {
          DataTable source = attributesGroupCollection.Select(string.Empty);
          Dictionary<int, DataRow> dictionary1 = this._filterForTree == null ? source.AsEnumerable().ToDictionary<DataRow, int, DataRow>((System.Func<DataRow, int>) (x => Convert.ToInt32(x["F_GROUP_ID"])), (System.Func<DataRow, DataRow>) (y => y)) : source.AsEnumerable().Where<DataRow>((System.Func<DataRow, bool>) (x => this._filterForTree.BinarySearch(Convert.ToInt32(x["F_GROUP_ID"])) > -1)).ToDictionary<DataRow, int, DataRow>((System.Func<DataRow, int>) (x => Convert.ToInt32(x["F_GROUP_ID"])), (System.Func<DataRow, DataRow>) (y => y));
          Dictionary<int, TreeViewForm.TmpClass> dictionary2 = new Dictionary<int, TreeViewForm.TmpClass>();
          foreach (KeyValuePair<int, DataRow> keyValuePair in dictionary1)
          {
            int key = keyValuePair.Key;
            int int32 = Convert.ToInt32(keyValuePair.Value["F_PARENT_ID"]);
            TreeNode node = new TreeNode(Convert.ToString(keyValuePair.Value["F_GROUP_NAME"]))
            {
              Tag = (object) key
            };
            node.ImageIndex = node.SelectedImageIndex = Statics.IconSrv != null ? Statics.IconSrv.IndexOf(12, 0) : 0;
            if (this.SelectedNodeData.Contains(keyValuePair.Value["F_GROUP_ID"]))
              this._selectedNode = node;
            dictionary2.Add(key, new TreeViewForm.TmpClass(int32, node));
          }
          using (Dictionary<int, TreeViewForm.TmpClass>.Enumerator enumerator = dictionary2.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<int, TreeViewForm.TmpClass> current = enumerator.Current;
              int parentId = current.Value.ParentID;
              if (parentId == 0)
                this.trvObj.Nodes[0].Nodes.Add(current.Value.Node);
              else if (dictionary2.ContainsKey(parentId))
                dictionary2[parentId].Node.Nodes.Add(current.Value.Node);
            }
            break;
          }
        }
        break;
      case 2:
        bool bFilterEmpty = this._filter.Count == 0;
        List<int> intList = new List<int>();
        if (!bFilterEmpty)
        {
          foreach (int childTypeID in this._filter)
          {
            int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(childTypeID);
            while (this._filter.Contains(objectTypeParentId))
              objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypeParentId);
            if (!intList.Contains(objectTypeParentId))
              intList.Add(objectTypeParentId);
          }
        }
        else
          intList.Add(-1);
        for (int index = 0; index < intList.Count; ++index)
        {
          IDBObjectTypeInfoCollection objectTypeCollection = service.GetObjectTypeCollection(intList[index], true);
          if (objectTypeCollection != null)
          {
            DataTable dt = objectTypeCollection.Select(string.Empty);
            foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
            {
              int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
              if (bFilterEmpty || this._filter.Contains(int32))
              {
                TreeNode node = this.LoadChildNode(int32, dt, bFilterEmpty);
                if (node != null)
                  this.trvObj.Nodes[0].Nodes.Add(node);
              }
            }
          }
        }
        break;
      case 3:
        IDBRelationTypeInfoCollection relationTypeCollection = service.GetRelationTypeCollection(true);
        if (relationTypeCollection != null)
        {
          IEnumerator enumerator = relationTypeCollection.Select(string.Empty).Rows.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              DataRow current = (DataRow) enumerator.Current;
              int int32 = Convert.ToInt32(current["F_RELATION_TYPE"]);
              TreeNode node = new TreeNode(Convert.ToString(current["F_DESCRIPTION"]))
              {
                Tag = current["F_GUID"]
              };
              node.ImageIndex = node.SelectedImageIndex = Statics.IconSrv != null ? Statics.IconSrv.IndexOf(6, int32) : 0;
              if (this.SelectedNodeData.Contains(current["F_GUID"]))
                this._selectedNode = node;
              this.trvObj.Nodes[0].Nodes.Add(node);
            }
            break;
          }
          finally
          {
            if (enumerator is IDisposable disposable)
              disposable.Dispose();
          }
        }
        else
          break;
    }
    if (this.trvObj.Nodes.Count <= 0)
      return;
    this.trvObj.Nodes[0].Expand();
    this.trvObj.SelectedNode = this._selectedNode;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosing(CancelEventArgs e)
  {
    base.OnClosing(e);
    if (this.trvObj.SelectedNode != null && this.trvObj.SelectedNode.Tag != null)
    {
      this.SelectedNodeData.Clear();
      this.SelectedNodeData.Add(this.trvObj.SelectedNode.Tag, (object) this.trvObj.SelectedNode.Text);
    }
    else
    {
      if (this.DialogResult != DialogResult.OK)
        return;
      e.Cancel = true;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objType"></param>
  /// <param name="cache"></param>
  /// <param name="dt"></param>
  /// <param name="bFilterEmpty"></param>
  /// <returns></returns>
  private TreeNode LoadChildNode(int objType, DataTable dt, bool bFilterEmpty)
  {
    TreeNode treeNode = (TreeNode) null;
    DataRow dataRow = dt.AsEnumerable().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToInt32(x["F_OBJECT_TYPE"]) == objType));
    if (dataRow != null)
    {
      treeNode = new TreeNode(Convert.ToString(dataRow["F_OBJ_TYPE_NAME"]))
      {
        Tag = dataRow["F_GUID"]
      };
      treeNode.ImageIndex = treeNode.SelectedImageIndex = Statics.IconSrv != null ? Statics.IconSrv.IndexOf(4, objType) : 0;
      if (this.SelectedNodeData.Contains(dataRow["F_GUID"]))
        this._selectedNode = treeNode;
      List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objType);
      if (objectTypeChildrenId != null && objectTypeChildrenId.Count > 0)
      {
        IDBObjectTypeInfoCollection objectTypeCollection = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectTypeCollection(objType, true);
        if (objectTypeCollection != null)
        {
          dt = objectTypeCollection.Select(string.Empty);
          foreach (int objType1 in objectTypeChildrenId)
          {
            if (bFilterEmpty || this._filter.Contains(objType1))
            {
              TreeNode node = this.LoadChildNode(objType1, dt, bFilterEmpty);
              if (node != null)
                treeNode.Nodes.Add(node);
            }
          }
        }
      }
    }
    return treeNode;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TreeViewForm));
    this.pnlButtons = new Panel();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.trvObj = new TreeView();
    this.pnlButtons.SuspendLayout();
    this.SuspendLayout();
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.pnlButtons.Name = "pnlButtons";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.trvObj, "trvObj");
    this.trvObj.Name = "trvObj";
    this.trvObj.Sorted = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.trvObj);
    this.Controls.Add((Control) this.pnlButtons);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (TreeViewForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.pnlButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private class TmpClass
  {
    internal int ParentID;
    internal TreeNode Node;

    /// <summary>Конструктор</summary>
    /// <param name="parentID">Идентификатор родительского узла</param>
    /// <param name="node">Узел</param>
    internal TmpClass(int parentID, TreeNode node)
    {
      this.ParentID = parentID;
      this.Node = node;
    }
  }
}
