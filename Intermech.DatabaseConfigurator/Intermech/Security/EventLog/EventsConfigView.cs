// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.EventsConfigView
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

public class EventsConfigView : UserControl, IView
{
  private const int lvTypes4Objects = 1;
  private const int lvObjects = 2;
  private EventlogSettings settings;
  private ArrayList notLoggedObjects = new ArrayList();
  private AdvArrayList notLoggedTypes = new AdvArrayList();
  private bool blockOnChange;
  private TreeListNode objTypesNode;
  private TreeListNode objectsNode;
  private DataTable hierarchy;
  private DataTable allObjTypes;
  private Button _acceptButton;
  private Button _cancelButton;
  private GroupBox groupBox;
  private CheckBox cbRegistrate;
  private Label label2;
  private CheckBox _clearCheckBox;
  private NumericUpDown _clearNumericUpDown;
  private TreeList treeList;
  private Button deleteButton;
  private Button addButton;
  private TreeListColumn NameColumn;
  private Label label3;
  private IContainer components;
  private Label label4;
  private NumericUpDown _archiveNumericUpDown;
  private CheckBox _archiveCheckBox;
  private bool isChanged;

  private bool IsChanged
  {
    get => this.isChanged;
    set
    {
      this.isChanged = value;
      this.UpdateControls();
    }
  }

  public EventsConfigView()
  {
    this.InitializeComponent();
    this.treeList.SelectImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._archiveNumericUpDown.Maximum = 2147483647M;
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EventsConfigView));
    this._acceptButton = new Button();
    this._cancelButton = new Button();
    this.groupBox = new GroupBox();
    this.label4 = new Label();
    this._archiveNumericUpDown = new NumericUpDown();
    this._archiveCheckBox = new CheckBox();
    this.label3 = new Label();
    this.treeList = new TreeList();
    this.NameColumn = new TreeListColumn();
    this.deleteButton = new Button();
    this.addButton = new Button();
    this._clearNumericUpDown = new NumericUpDown();
    this.label2 = new Label();
    this._clearCheckBox = new CheckBox();
    this.cbRegistrate = new CheckBox();
    this.groupBox.SuspendLayout();
    this._archiveNumericUpDown.BeginInit();
    this.treeList.BeginInit();
    this._clearNumericUpDown.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._acceptButton, "_acceptButton");
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Click += new EventHandler(this.btnApply_Click);
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.groupBox, "groupBox");
    this.groupBox.Controls.Add((Control) this.label4);
    this.groupBox.Controls.Add((Control) this._archiveNumericUpDown);
    this.groupBox.Controls.Add((Control) this._archiveCheckBox);
    this.groupBox.Controls.Add((Control) this.label3);
    this.groupBox.Controls.Add((Control) this.treeList);
    this.groupBox.Controls.Add((Control) this.deleteButton);
    this.groupBox.Controls.Add((Control) this.addButton);
    this.groupBox.Controls.Add((Control) this._clearNumericUpDown);
    this.groupBox.Controls.Add((Control) this.label2);
    this.groupBox.Controls.Add((Control) this._clearCheckBox);
    this.groupBox.Name = "groupBox";
    this.groupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this._archiveNumericUpDown, "_archiveNumericUpDown");
    this._archiveNumericUpDown.Name = "_archiveNumericUpDown";
    this._archiveNumericUpDown.ValueChanged += new EventHandler(this.ArchiveNumericUpDown_ValueChanged);
    componentResourceManager.ApplyResources((object) this._archiveCheckBox, "_archiveCheckBox");
    this._archiveCheckBox.Name = "_archiveCheckBox";
    this._archiveCheckBox.UseVisualStyleBackColor = true;
    this._archiveCheckBox.CheckedChanged += new EventHandler(this.ArchiveCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.treeList, "treeList");
    this.treeList.Columns.AddRange(new TreeListColumn[1]
    {
      this.NameColumn
    });
    this.treeList.Name = "treeList";
    this.treeList.AfterFocusNode += new NodeEventHandler(this.treeList_AfterFocusNode);
    componentResourceManager.ApplyResources((object) this.NameColumn, "NameColumn");
    this.NameColumn.Name = "NameColumn";
    componentResourceManager.ApplyResources((object) this.deleteButton, "deleteButton");
    this.deleteButton.Name = "deleteButton";
    this.deleteButton.Click += new EventHandler(this.deleteButton_Click);
    componentResourceManager.ApplyResources((object) this.addButton, "addButton");
    this.addButton.Name = "addButton";
    this.addButton.Click += new EventHandler(this.addButton_Click);
    componentResourceManager.ApplyResources((object) this._clearNumericUpDown, "_clearNumericUpDown");
    this._clearNumericUpDown.Maximum = new Decimal(new int[4]
    {
      20000,
      0,
      0,
      0
    });
    this._clearNumericUpDown.Name = "_clearNumericUpDown";
    this._clearNumericUpDown.ValueChanged += new EventHandler(this.daysUpDown_ValueChanged);
    this._clearNumericUpDown.KeyPress += new KeyPressEventHandler(this.daysUpDown_KeyPress);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this._clearCheckBox, "_clearCheckBox");
    this._clearCheckBox.Name = "_clearCheckBox";
    this._clearCheckBox.CheckedChanged += new EventHandler(this.ClearCheckBox_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbRegistrate, "cbRegistrate");
    this.cbRegistrate.Name = "cbRegistrate";
    this.cbRegistrate.CheckedChanged += new EventHandler(this.cbRegistrate_CheckedChanged);
    this.Controls.Add((Control) this.cbRegistrate);
    this.Controls.Add((Control) this.groupBox);
    this.Controls.Add((Control) this._cancelButton);
    this.Controls.Add((Control) this._acceptButton);
    this.Name = nameof (EventsConfigView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Load += new EventHandler(this.EventsConfigView_Load);
    this.Leave += new EventHandler(this.EventsConfigView_Leave);
    this.groupBox.ResumeLayout(false);
    this.groupBox.PerformLayout();
    this._archiveNumericUpDown.EndInit();
    this.treeList.EndInit();
    this._clearNumericUpDown.EndInit();
    this.ResumeLayout(false);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
  }

  public void Activate(IView previousView)
  {
    this.LoadSettings();
    this.FillControl();
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => LocalizationHolder.rm.GetString("DatabaseConfigurator_92");

  public int OrderID => 30;

  public int ImageIndex => -1;

  private void EventsConfigView_Leave(object sender, EventArgs e) => this.SaveSettings();

  private void FillControl()
  {
    this.blockOnChange = true;
    try
    {
      this.cbRegistrate.Checked = this.settings.LogOn;
      this.cbRegistrate_CheckedChanged((object) this, (EventArgs) null);
      this._clearCheckBox.CheckedChanged -= new EventHandler(this.ClearCheckBox_CheckedChanged);
      try
      {
        this._clearCheckBox.Checked = this.settings.AutoClear;
      }
      finally
      {
        this._clearCheckBox.CheckedChanged += new EventHandler(this.ClearCheckBox_CheckedChanged);
      }
      this._clearNumericUpDown.Value = (Decimal) this.settings.RecordsKeepDays;
      this.FillTreeList();
    }
    finally
    {
      this.blockOnChange = false;
    }
    this.UpdateControls();
  }

  private void FillTreeList()
  {
    this.treeList.ClearNodes();
    this.objTypesNode = this.treeList.AppendNode((object) new object[1]
    {
      (object) LocalizationHolder.rm.GetString("DatabaseConfigurator_93")
    }, (TreeListNode) null);
    this.objTypesNode.Tag = (object) 4;
    this.objTypesNode.ImageIndex = Statics.IconSrv.IndexOf(Intermech.Navigator.Consts.CategoryObjectTypes, 0);
    this.objTypesNode.SelectImageIndex = this.objTypesNode.ImageIndex;
    this.objectsNode = this.treeList.AppendNode((object) new object[1]
    {
      (object) LocalizationHolder.rm.GetString("DatabaseConfigurator_94")
    }, (TreeListNode) null);
    this.objectsNode.Tag = (object) 1;
    this.hierarchy = DataHolders.ObjectTypesHolder.GetHierarchy(false, false);
    this.allObjTypes = DataHolders.ObjectTypesHolder.GetAllObjectTypes(false, false);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = (DataTable) null;
      if (this.notLoggedObjects.Count > 0)
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
        {
          new ConditionStructure(-2, RelationalOperators.In, (object) (object[]) this.notLoggedObjects.ToArray(typeof (object)), LogicalOperators.NONE, 0, true)
        }, new object[3]
        {
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          (object) ObligatoryObjectAttributes.CAPTION
        });
        dataTable = sessionKeeper.Session.ObjectsSelect(-1, dbRecordSetParams);
      }
      if (dataTable != null)
      {
        this.treeList.BeginSort();
        try
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            TreeListNode parentNode = this.GetNodeByData(this.objectsNode, (object) Convert.ToInt32(row[1]));
            if (parentNode == null)
            {
              parentNode = this.treeList.AppendNode((object) new object[1]
              {
                (object) this.GetObjectTypeName(Convert.ToInt32(row[1]), this.allObjTypes)
              }, this.objectsNode);
              parentNode.Tag = (object) Convert.ToInt32(row[1]);
              parentNode.ImageIndex = Statics.IconSrv.IndexOf(4, Convert.ToInt32(row[1]));
            }
            this.treeList.AppendNode((object) new object[1]
            {
              (object) row[2].ToString()
            }, parentNode).Tag = (object) Convert.ToInt64(row[0]);
            parentNode.ImageIndex = Statics.IconSrv.IndexOf(4, Convert.ToInt32(row[1]));
          }
        }
        finally
        {
          this.treeList.EndSort();
        }
      }
      ArrayList rootObjTypeNodes = this.GetRootObjTypeNodes(this.hierarchy);
      for (int index = 0; index < rootObjTypeNodes.Count; ++index)
      {
        TreeListNode treeListNode = this.objTypesNode.TreeList.AppendNode((object) new object[1]
        {
          (object) this.GetObjectTypeName((int) rootObjTypeNodes[index], this.allObjTypes)
        }, this.objTypesNode);
        treeListNode.Tag = (object) (int) rootObjTypeNodes[index];
        treeListNode.ImageIndex = Statics.IconSrv.IndexOf(4, (int) rootObjTypeNodes[index]);
      }
      for (int index = 0; index < this.objTypesNode.Nodes.Count; ++index)
        this.ExpandNode(this.objTypesNode.Nodes[index], this.hierarchy, this.allObjTypes, true);
    }
    this.objTypesNode.Expanded = true;
    this.objectsNode.Expanded = true;
  }

  private void ExpandNode(
    TreeListNode parentNode,
    DataTable hierarhy,
    DataTable allObjTypes,
    bool byNotLoggedTypeList)
  {
    parentNode.Nodes.Clear();
    DataRow[] dataRowArray = hierarhy.Select("F_PARENT_ID=" + parentNode.Tag.ToString());
    if (dataRowArray == null || dataRowArray.Length == 0)
      return;
    foreach (DataRow dataRow in dataRowArray)
    {
      int int32 = Convert.ToInt32(dataRow["F_OBJECT_TYPE"]);
      if (!byNotLoggedTypeList || byNotLoggedTypeList && this.notLoggedTypes.IndexOf((object) int32) != -1)
      {
        TreeListNode parentNode1 = parentNode.TreeList.AppendNode((object) new object[1]
        {
          (object) this.GetObjectTypeName(int32, allObjTypes)
        }, parentNode);
        parentNode1.Tag = (object) int32;
        parentNode1.ImageIndex = Statics.IconSrv.IndexOf(4, int32);
        this.ExpandNode(parentNode1, hierarhy, allObjTypes, byNotLoggedTypeList);
      }
    }
  }

  private string GetObjectTypeName(int id, DataTable allObjTypes)
  {
    DataRow[] dataRowArray = allObjTypes.Select("F_OBJECT_TYPE=" + id.ToString());
    return dataRowArray == null || dataRowArray.Length == 0 ? string.Empty : dataRowArray[0]["F_OBJ_TYPE_NAME"].ToString();
  }

  private ArrayList GetRootObjTypeNodes(DataTable hierarchy)
  {
    ArrayList rootObjTypeNodes = new ArrayList();
    for (int index1 = 0; index1 < this.notLoggedTypes.Count; ++index1)
    {
      int notLoggedType = (int) this.notLoggedTypes[index1];
      ArrayList allParents = ObjectTypesHolder.GetAllParents((int) this.notLoggedTypes[index1], hierarchy);
      for (int index2 = 0; index2 < allParents.Count && this.notLoggedTypes.IndexOf((object) (int) allParents[index2]) != -1; ++index2)
        notLoggedType = (int) allParents[index2];
      if (rootObjTypeNodes.IndexOf((object) notLoggedType) == -1)
        rootObjTypeNodes.Add((object) notLoggedType);
    }
    return rootObjTypeNodes;
  }

  private void LoadSettings()
  {
    this.IsChanged = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.settings = sessionKeeper.Session.EventLog.Settings;
      this.notLoggedObjects.Clear();
      this.notLoggedObjects.AddRange((ICollection) this.settings.NotLoggedObjects);
      this.notLoggedTypes.Clear();
      this.notLoggedTypes.AddRange((ICollection) this.settings.NotLoggedTypes);
      this._archiveCheckBox.CheckedChanged -= new EventHandler(this.ArchiveCheckBox_CheckedChanged);
      try
      {
        this._archiveCheckBox.Checked = sessionKeeper.Session.Configurations.ReadBool("KERNEL", "EVENTS", "ARCHIVE", false, DBConfigMode.GlobalOnly);
      }
      finally
      {
        this._archiveCheckBox.CheckedChanged += new EventHandler(this.ArchiveCheckBox_CheckedChanged);
      }
      this._archiveNumericUpDown.ValueChanged -= new EventHandler(this.ArchiveNumericUpDown_ValueChanged);
      try
      {
        this._archiveNumericUpDown.Value = (Decimal) sessionKeeper.Session.Configurations.ReadInteger("KERNEL", "EVENTS", "ARC_DAYS", 90L, DBConfigMode.GlobalOnly);
      }
      finally
      {
        this._archiveNumericUpDown.ValueChanged += new EventHandler(this.ArchiveNumericUpDown_ValueChanged);
      }
    }
  }

  private void SaveSettings()
  {
    if (!this.isChanged || MessageBox.Show(MessageDialogs.msgReallySave, MessageDialogs.msgQuery, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.settings.NotLoggedObjects = (long[]) this.notLoggedObjects.ToArray(typeof (long));
      this.settings.NotLoggedTypes = (int[]) this.notLoggedTypes.ToArray(typeof (int));
      sessionKeeper.Session.EventLog.Settings = this.settings;
      sessionKeeper.Session.Configurations.WriteBool("KERNEL", "EVENTS", "ARCHIVE", this._archiveCheckBox.Checked, 0L);
      sessionKeeper.Session.Configurations.WriteInteger("KERNEL", "EVENTS", "ARC_DAYS", (long) Convert.ToInt32(this._archiveNumericUpDown.Value), 0L);
    }
    this.IsChanged = false;
  }

  private void btnApply_Click(object sender, EventArgs e) => this.SaveSettings();

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.LoadSettings();
    this.FillControl();
  }

  private void FireChanges() => this.IsChanged = true;

  private void cbRegistrate_CheckedChanged(object sender, EventArgs e)
  {
    this.settings.LogOn = this.cbRegistrate.Checked;
    this.groupBox.Enabled = this.cbRegistrate.Checked;
    if (this.blockOnChange)
      return;
    this.FireChanges();
  }

  private void daysUpDown_ValueChanged(object sender, EventArgs e) => this.daysChanged();

  private void daysUpDown_KeyPress(object sender, KeyPressEventArgs e) => this.daysChanged();

  private void daysChanged()
  {
    this.settings.RecordsKeepDays = Convert.ToInt32(this._clearNumericUpDown.Value);
    if (this.blockOnChange)
      return;
    this.FireChanges();
  }

  private void treeList_AfterFocusNode(object sender, NodeEventArgs e)
  {
    e.Node.SelectImageIndex = e.Node.ImageIndex;
    this.SetControlsState(this.treeList.Selection);
  }

  private TreeListNode GetRootNode(TreeListNode tln)
  {
    TreeListNode rootNode = (TreeListNode) null;
    if (tln != null)
    {
      rootNode = tln;
      while (rootNode.ParentNode != null)
        rootNode = rootNode.ParentNode;
    }
    return rootNode;
  }

  private TreeListNode GetFocusedNode()
  {
    return this.treeList.GetHitInfo(this.treeList.PointToClient(Control.MousePosition)).Node ?? this.treeList.FocusedNode;
  }

  private void CollectChildIdCustom(TreeListNode node, ArrayList a)
  {
    foreach (TreeListNode node1 in node.Nodes)
    {
      a.Add(node1.Tag);
      this.CollectChildIdCustom(node1, a);
    }
  }

  private ArrayList CollectChildId(TreeListNode tln)
  {
    ArrayList a = new ArrayList();
    this.CollectChildIdCustom(tln, a);
    return a;
  }

  private TreeListNode GetNodeByData(TreeListNode node, object data)
  {
    return this.GetNodeByData(node, data, false);
  }

  private TreeListNode GetNodeByData(TreeListNode node, object data, bool recursive)
  {
    TreeListNode nodeByData = (TreeListNode) null;
    for (int index = 0; index < node.Nodes.Count; ++index)
    {
      if (node.Nodes[index].Tag.Equals(data))
      {
        nodeByData = node.Nodes[index];
        break;
      }
      if (recursive)
      {
        nodeByData = this.GetNodeByData(node.Nodes[index], data, recursive);
        if (nodeByData != null)
          break;
      }
    }
    return nodeByData;
  }

  private void SetControlsState(TreeListMultiSelection ms)
  {
    this.addButton.Enabled = ms.Count == 1;
    this.deleteButton.Enabled = ms.Count >= 1;
  }

  private void addButton_Click(object sender, EventArgs e)
  {
    if (this.treeList.FocusedNode == null)
      return;
    TreeListNode rootNode = this.GetRootNode(this.treeList.FocusedNode);
    if (rootNode == this.objectsNode)
    {
      IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects(new int[1]
      {
        -1
      });
      if (dbObjectIdArray == null || dbObjectIdArray.Length == 0)
        return;
      ArrayList arrayList = new ArrayList();
      for (int index = 0; index < dbObjectIdArray.Length; ++index)
      {
        if (this.notLoggedObjects.IndexOf((object) dbObjectIdArray[index].Value) == -1)
          arrayList.Add((object) dbObjectIdArray[index].Value);
      }
      if (arrayList.Count == 0)
        return;
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) (object[]) arrayList.ToArray(typeof (object)), LogicalOperators.NONE, 0, true)
      }, new object[3]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
        (object) ObligatoryObjectAttributes.CAPTION
      });
      DataTable dataTable = (DataTable) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        dataTable = sessionKeeper.Session.ObjectsSelect(-1, dbRecordSetParams);
      this.treeList.BeginSort();
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          TreeListNode parentNode = this.GetNodeByData(this.objectsNode, (object) Convert.ToInt32(row[1]));
          if (parentNode == null)
          {
            parentNode = this.objectsNode.TreeList.AppendNode((object) new object[1]
            {
              (object) this.GetObjectTypeName(Convert.ToInt32(row[1]), DataHolders.ObjectTypesHolder.GetAllObjectTypes(false, false))
            }, this.objectsNode);
            parentNode.Tag = (object) Convert.ToInt32(row[1]);
            parentNode.ImageIndex = Statics.IconSrv.IndexOf(4, Convert.ToInt32(row[1]));
          }
          TreeListNode node = parentNode.TreeList.AppendNode((object) new object[1]
          {
            (object) row[2].ToString()
          }, parentNode);
          node.Tag = (object) Convert.ToInt64(row[0]);
          node.ImageIndex = Statics.IconSrv.IndexOf(4, Convert.ToInt32(row[1]));
          this.OpenParentNodes(node);
          this.notLoggedObjects.Add((object) Convert.ToInt64(row[0]));
        }
      }
      finally
      {
        this.treeList.EndSort();
      }
    }
    if (rootNode == this.objTypesNode)
    {
      SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("DatabaseConfigurator_95"), typeof (ObjectTypeFolder), true);
      if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
        return;
      this.treeList.BeginSort();
      try
      {
        ArrayList arrayList1 = new ArrayList((ICollection) selectorForm.IDList);
        for (int index = 0; index < arrayList1.Count; ++index)
        {
          TreeListNode nodeByData1 = this.GetNodeByData(this.objTypesNode, arrayList1[index], true);
          if (nodeByData1 != null)
          {
            this.notLoggedTypes.RemoveList(this.CollectChildId(nodeByData1));
            this.ExpandNode(nodeByData1, this.hierarchy, this.allObjTypes, false);
            this.OpenParentNodes(nodeByData1);
            ArrayList arrayList2 = this.CollectChildId(nodeByData1);
            this.notLoggedTypes.AddList(arrayList2);
            this.RemoveRootByArray(arrayList2);
          }
          else
          {
            ArrayList allParents = ObjectTypesHolder.GetAllParents((int) arrayList1[index], this.hierarchy);
            TreeListNode parentNode = this.objTypesNode;
            if (allParents.Count > 0)
            {
              TreeListNode nodeByData2 = this.GetNodeByData(this.objTypesNode, (object) (int) allParents[0], true);
              if (nodeByData2 != null)
                parentNode = nodeByData2;
            }
            TreeListNode treeListNode = this.treeList.AppendNode((object) new object[1]
            {
              (object) this.GetObjectTypeName((int) arrayList1[index], this.allObjTypes)
            }, parentNode);
            treeListNode.Tag = (object) (int) arrayList1[index];
            treeListNode.ImageIndex = Statics.IconSrv.IndexOf(4, (int) arrayList1[index]);
            this.ExpandNode(treeListNode, this.hierarchy, this.allObjTypes, false);
            this.OpenParentNodes(treeListNode);
            ArrayList arrayList3 = this.CollectChildId(treeListNode);
            this.notLoggedTypes.AddList(arrayList3);
            this.notLoggedTypes.Add((object) (int) arrayList1[index]);
            this.RemoveRootByArray(arrayList3);
          }
        }
      }
      finally
      {
        this.treeList.EndSort();
      }
    }
    this.FireChanges();
  }

  private void RemoveRootByArray(ArrayList c)
  {
    ArrayList arrayList = new ArrayList();
    for (int index = 0; index < this.objTypesNode.Nodes.Count; ++index)
    {
      if (c.IndexOf(this.objTypesNode.Nodes[index].Tag) != -1)
        arrayList.Add((object) this.objTypesNode.Nodes[index]);
    }
    for (int index = 0; index < arrayList.Count; ++index)
      this.treeList.DeleteNode((TreeListNode) arrayList[index]);
  }

  private void deleteButton_Click(object sender, EventArgs e)
  {
    if (this.treeList.Selection == null || this.treeList.Selection.Count == 0 || MessageBox.Show(MessageDialogs.msgReallyDelete, MessageDialogs.msgQuery, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    foreach (TreeListNode tln in (CollectionBase) this.treeList.Selection)
    {
      if (tln != this.objTypesNode && tln != this.objectsNode)
      {
        TreeListNode rootNode = this.GetRootNode(tln);
        if (rootNode == this.objectsNode)
        {
          if (tln.Level == 2)
          {
            this.notLoggedObjects.Remove(tln.Tag);
          }
          else
          {
            ArrayList arrayList = this.CollectChildId(tln);
            for (int index = 0; index < arrayList.Count; ++index)
              this.notLoggedObjects.Remove(arrayList[index]);
          }
        }
        if (rootNode == this.objTypesNode)
        {
          ArrayList arrayList = this.CollectChildId(tln);
          arrayList.Add(tln.Tag);
          for (int index = 0; index < arrayList.Count; ++index)
            this.notLoggedTypes.Remove(arrayList[index]);
        }
      }
    }
    ArrayList arrayList1 = new ArrayList((ICollection) this.treeList.Selection);
    for (int index = 0; index < arrayList1.Count; ++index)
    {
      if (arrayList1[index] != this.objTypesNode && arrayList1[index] != this.objectsNode)
        ((TreeListNode) arrayList1[index]).TreeList.DeleteNode((TreeListNode) arrayList1[index]);
    }
    this.FireChanges();
  }

  private void OpenParentNodes(TreeListNode node)
  {
    if (node == null)
      return;
    while (node.ParentNode != null)
    {
      node = node.ParentNode;
      node.Expanded = true;
    }
  }

  private void EventsConfigView_Load(object sender, EventArgs e)
  {
  }

  private void ArchiveCheckBox_CheckedChanged(object sender, EventArgs e) => this.IsChanged = true;

  private void ArchiveNumericUpDown_ValueChanged(object sender, EventArgs e)
  {
    this.IsChanged = true;
  }

  private void ClearCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.settings.AutoClear = this._clearCheckBox.Checked;
    this.IsChanged = true;
  }

  private void UpdateControls()
  {
    this._archiveNumericUpDown.Enabled = this._archiveCheckBox.Checked;
    this._clearNumericUpDown.Enabled = this._clearCheckBox.Checked;
    this._acceptButton.Enabled = this.IsChanged;
    this._cancelButton.Enabled = this.IsChanged;
  }
}
