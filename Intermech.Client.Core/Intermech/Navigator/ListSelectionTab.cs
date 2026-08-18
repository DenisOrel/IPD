
// Type: Intermech.Navigator.ListSelectionTab
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

public class ListSelectionTab : UserControl
{
  private List<long> _deletedObjects = new List<long>();
  /// <summary>Флаг персональной выборки</summary>
  protected bool isPersonal;
  protected string caption = string.Empty;
  /// <summary>Флаг того, что изменялись данные на закладке</summary>
  private bool _changed;
  protected int index;
  protected List<int> objectTypes;
  private string _message = string.Empty;
  private bool _inited;
  private long _selectionID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button bDelete;
  private Button bChange;
  private Button bAdd;
  private Label lDescription;
  private TreeList treeList1;
  private TreeListColumn treeListColumn1;

  public ListSelectionTab(
    string tabCaption,
    int tabIndex,
    List<int> objTypesInTab,
    string description,
    string selectMessage)
  {
    this.InitializeComponent();
    this.caption = tabCaption;
    this.index = tabIndex;
    this.objectTypes = objTypesInTab;
    this.lDescription.Text = description;
    this._message = selectMessage;
    this.treeList1.StateImageList = (ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService)?.ImageList;
  }

  public string Caption => this.caption;

  public int Index => this.index;

  public void Initialize(IUserSession session, long selectionID, bool isPersonal)
  {
    if (this._selectionID == selectionID && this._inited)
      return;
    this._changed = false;
    this._selectionID = selectionID;
    this.isPersonal = isPersonal;
    this.treeList1.Nodes.Clear();
    IAttachedSelectionsService customService = (IAttachedSelectionsService) session.GetCustomService(typeof (IAttachedSelectionsService));
    if (customService != null)
    {
      AttachedSelObjectInfo[] objectsForSelection = customService.GetObjectsForSelection(selectionID, this.objectTypes.ToArray());
      if (objectsForSelection != null && objectsForSelection.Length != 0)
      {
        ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
        for (int index = 0; index < objectsForSelection.Length; ++index)
        {
          QuickObjectInfo objectInfo = session.GetObjectInfo(objectsForSelection[index].ObjectID);
          TreeListNode treeListNode = this.treeList1.AppendNode((object) new object[1]
          {
            (object) objectInfo.Caption
          }, (TreeListNode) null);
          treeListNode.StateImageIndex = service != null ? service.IndexOf(4, objectInfo.ObjectTypeID) : -1;
          treeListNode.Tag = (object) objectsForSelection[index];
        }
      }
    }
    this._inited = true;
    this.ButtonUpdate();
  }

  public void Save(IUserSession session, long selectionID)
  {
    if (!this._changed)
      return;
    IAttachedSelectionsService customService = (IAttachedSelectionsService) session.GetCustomService(typeof (IAttachedSelectionsService));
    if (customService != null)
    {
      if (this._deletedObjects.Count > 0)
      {
        customService.ExcludeObjects(session.SessionGUID, selectionID, this._deletedObjects.ToArray());
        this._deletedObjects.Clear();
      }
      if (this.treeList1.Nodes.Count > 0)
      {
        List<long> longList1 = new List<long>(this.treeList1.Nodes.Count);
        for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
          longList1.Add(((AttachedSelObjectInfo) this.treeList1.Nodes[index].Tag).ObjectID);
        AttachedSelObjectInfo[] objectsForSelection = customService.GetObjectsForSelection(selectionID, this.objectTypes.ToArray());
        List<long> longList2 = new List<long>();
        if (objectsForSelection != null)
        {
          for (int index = 0; index < objectsForSelection.Length; ++index)
            longList2.Add(objectsForSelection[index].ObjectID);
        }
        if (!CompareValuesHelper.CompareCollections<long>((ICollection<long>) longList1, (ICollection<long>) longList2))
        {
          if (longList1.Count == 0)
          {
            customService.SetObjectsForSelection(session.SessionGUID, selectionID, this.objectTypes.ToArray(), (AttachedSelObjectInfo[]) null);
          }
          else
          {
            List<AttachedSelObjectInfo> attachedSelObjectInfoList = new List<AttachedSelObjectInfo>(this.treeList1.Nodes.Count);
            for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
              attachedSelObjectInfoList.Add((AttachedSelObjectInfo) this.treeList1.Nodes[index].Tag);
            customService.SetObjectsForSelection(session.SessionGUID, selectionID, this.objectTypes.ToArray(), attachedSelObjectInfoList.ToArray());
          }
        }
      }
      else
        customService.SetObjectsForSelection(session.SessionGUID, selectionID, this.objectTypes.ToArray(), (AttachedSelObjectInfo[]) null);
    }
    this._inited = false;
  }

  public Control TabControl => (Control) this;

  public event EventHandler OnChanged;

  private void FireOnChanged()
  {
    this._changed = true;
    EventHandler onChanged = this.OnChanged;
    if (onChanged != null)
      onChanged((object) this, new EventArgs());
    this.ButtonUpdate();
  }

  protected virtual int ObjectTypesInSelectDialog => -1;

  private void bAdd_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(this._message, string.Empty, this.ObjectTypesInSelectDialog, SelectionOptions.Default);
    if (numArray == null)
      return;
    bool flag1 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index1 = 0; index1 < numArray.Length; ++index1)
      {
        bool flag2 = false;
        for (int index2 = 0; index2 < this.treeList1.Nodes.Count; ++index2)
        {
          if (((AttachedSelObjectInfo) this.treeList1.Nodes[index2].Tag).ObjectID == numArray[index1])
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[index1]);
          TreeListNode treeListNode = this.treeList1.AppendNode((object) new object[1]
          {
            (object) dbObject.Caption
          }, (TreeListNode) null);
          ICategoryTypeIconService service = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
          treeListNode.StateImageIndex = service != null ? service.IndexOf(4, dbObject.ObjectType) : -1;
          treeListNode.Tag = (object) new AttachedSelObjectInfo(dbObject.ObjectID, dbObject.ObjectType);
          this.treeList1.FocusedNode = treeListNode;
          if (this._deletedObjects.Contains(dbObject.ObjectID))
            this._deletedObjects.Add(dbObject.ObjectID);
          if (!flag1)
            flag1 = true;
        }
      }
    }
    if (!flag1)
      return;
    this.FireOnChanged();
  }

  private void bChange_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(this._message, string.Empty, this.ObjectTypesInSelectDialog, SelectionOptions.Default | SelectionOptions.DisableMultiselect);
    if (numArray == null || numArray.Length == 0)
      return;
    bool flag1 = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag2 = false;
      for (int index = 0; index < this.treeList1.Nodes.Count; ++index)
      {
        if (((AttachedSelObjectInfo) this.treeList1.Nodes[index].Tag).ObjectID == numArray[0])
        {
          flag2 = true;
          break;
        }
      }
      if (!flag2)
      {
        if (!this._deletedObjects.Contains(((AttachedSelObjectInfo) this.treeList1.FocusedNode.Tag).ObjectID))
          this._deletedObjects.Add(((AttachedSelObjectInfo) this.treeList1.FocusedNode.Tag).ObjectID);
        IDBObject dbObject = sessionKeeper.Session.GetObject(numArray[0]);
        this.treeList1.FocusedNode.SetValue((object) 0, (object) dbObject.Caption);
        this.treeList1.FocusedNode.Tag = (object) new AttachedSelObjectInfo(dbObject.ObjectID, dbObject.ObjectType);
        if (this._deletedObjects.Contains(dbObject.ObjectID))
          this._deletedObjects.Add(dbObject.ObjectID);
        flag1 = true;
      }
    }
    if (!flag1)
      return;
    this.FireOnChanged();
  }

  private void bDelete_Click(object sender, EventArgs e)
  {
    if (this.treeList1.FocusedNode == null || this.treeList1.FocusedNode.Tag == null)
      return;
    if (!this._deletedObjects.Contains(((AttachedSelObjectInfo) this.treeList1.FocusedNode.Tag).ObjectID))
      this._deletedObjects.Add(((AttachedSelObjectInfo) this.treeList1.FocusedNode.Tag).ObjectID);
    this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);
    this.FireOnChanged();
  }

  private void treeList1_AfterFocusNode(object sender, NodeEventArgs e) => this.ButtonUpdate();

  private void ButtonUpdate()
  {
    this.bChange.Enabled = this.bDelete.Enabled = this.treeList1.FocusedNode != null && this.treeList1.FocusedNode.Tag != null;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ListSelectionTab));
    this.bDelete = new Button();
    this.bChange = new Button();
    this.bAdd = new Button();
    this.lDescription = new Label();
    this.treeList1 = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.treeList1.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.bDelete, "bDelete");
    this.bDelete.Name = "bDelete";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    componentResourceManager.ApplyResources((object) this.bChange, "bChange");
    this.bChange.Name = "bChange";
    this.bChange.UseVisualStyleBackColor = true;
    this.bChange.Click += new EventHandler(this.bChange_Click);
    componentResourceManager.ApplyResources((object) this.bAdd, "bAdd");
    this.bAdd.Name = "bAdd";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    componentResourceManager.ApplyResources((object) this.lDescription, "lDescription");
    this.lDescription.Name = "lDescription";
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.treeList1.Name = "treeList1";
    this.treeList1.AfterFocusNode += new NodeEventHandler(this.treeList1_AfterFocusNode);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.bDelete);
    this.Controls.Add((Control) this.bChange);
    this.Controls.Add((Control) this.bAdd);
    this.Controls.Add((Control) this.lDescription);
    this.Controls.Add((Control) this.treeList1);
    this.MinimumSize = new Size(422, 128 /*0x80*/);
    this.Name = nameof (ListSelectionTab);
    this.treeList1.EndInit();
    this.ResumeLayout(false);
  }
}
