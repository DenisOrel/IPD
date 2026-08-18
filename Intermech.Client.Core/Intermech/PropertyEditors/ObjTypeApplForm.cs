
// Type: Intermech.PropertyEditors.ObjTypeApplForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Client.Core.FormDesigner.TabPages;
using Intermech.Controls;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class ObjTypeApplForm : TabPageForm
{
  private Panel panel;
  private GroupBox groupBox;
  private Splitter splitter1;
  private Panel panel1;
  private PropertyGrid propertyGrid;
  private ComboBox relationsCB;
  private IContainer components;
  private RelationTypeList relationTypeList;
  private RelationTypeMember currentRelationTypeMember;
  private ObjTypeApplPGClass objTypeApplPGClass;
  private bool blockOnIndexChange;
  private int lastIndexCB = -1;
  private int lastHasApplIndexCB = -1;
  private bool lastRelAssigned;
  private int lastRelType;
  private bool lastIsReversed;
  private bool _blocktvchange;
  private ArrayList objectTypes = new ArrayList();
  private ArrayList allChilds = new ArrayList();
  private ArrayList rootObjectTypes = new ArrayList();
  private TreeView treeView;
  private ContextMenu contextMenu;
  private MenuItem addMenuItem;
  private MenuItem removeMenuItem;

  public ObjTypeApplForm(Guid aInstGuid)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this.relationsCB.MaxDropDownItems = 24;
    this.treeView.TreeViewNodeSorter = (IComparer) new Forms4TypeForm.TreeNodeComparer();
    this.treeView.Sorted = true;
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjTypeApplForm));
    this.contextMenu = new ContextMenu();
    this.addMenuItem = new MenuItem();
    this.removeMenuItem = new MenuItem();
    this.panel1 = new Panel();
    this.propertyGrid = new PropertyGrid();
    this.splitter1 = new Splitter();
    this.panel = new Panel();
    this.treeView = new TreeView();
    this.groupBox = new GroupBox();
    this.relationsCB = new ComboBox();
    this.panel1.SuspendLayout();
    this.panel.SuspendLayout();
    this.groupBox.SuspendLayout();
    this.SuspendLayout();
    this.contextMenu.MenuItems.AddRange(new MenuItem[2]
    {
      this.addMenuItem,
      this.removeMenuItem
    });
    componentResourceManager.ApplyResources((object) this.contextMenu, "contextMenu");
    this.contextMenu.Popup += new EventHandler(this.contextMenu_Popup);
    componentResourceManager.ApplyResources((object) this.addMenuItem, "addMenuItem");
    this.addMenuItem.Index = 0;
    this.addMenuItem.Click += new EventHandler(this.addMenuItem_Click);
    componentResourceManager.ApplyResources((object) this.removeMenuItem, "removeMenuItem");
    this.removeMenuItem.Index = 1;
    this.removeMenuItem.Click += new EventHandler(this.removeMenuItem_Click);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this.propertyGrid);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.PropertySort = PropertySort.Alphabetical;
    this.propertyGrid.Tag = (object) "    ";
    this.propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
    componentResourceManager.ApplyResources((object) this.splitter1, "splitter1");
    this.splitter1.Name = "splitter1";
    this.splitter1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.panel, "panel");
    this.panel.Controls.Add((Control) this.treeView);
    this.panel.Controls.Add((Control) this.groupBox);
    this.panel.Name = "panel";
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.ContextMenu = this.contextMenu;
    this.treeView.HideSelection = false;
    this.treeView.Name = "treeView";
    this.treeView.BeforeExpand += new TreeViewCancelEventHandler(this.treeView_BeforeExpand);
    this.treeView.BeforeSelect += new TreeViewCancelEventHandler(this.treeView_BeforeSelect);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    componentResourceManager.ApplyResources((object) this.groupBox, "groupBox");
    this.groupBox.Controls.Add((Control) this.relationsCB);
    this.groupBox.Name = "groupBox";
    this.groupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.relationsCB, "relationsCB");
    this.relationsCB.DrawMode = DrawMode.OwnerDrawFixed;
    this.relationsCB.DropDownStyle = ComboBoxStyle.DropDownList;
    this.relationsCB.Name = "relationsCB";
    this.relationsCB.DrawItem += new DrawItemEventHandler(this.relationsCB_DrawItem);
    this.relationsCB.SelectedIndexChanged += new EventHandler(this.relationsCB_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.splitter1);
    this.Controls.Add((Control) this.panel);
    this.Name = nameof (ObjTypeApplForm);
    this.Tag = (object) "  ";
    this.panel1.ResumeLayout(false);
    this.panel.ResumeLayout(false);
    this.groupBox.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public override void FillForm(IFolder folder)
  {
    this.treeView.ImageList = Statics.IconSrv != null ? Statics.IconSrv.ImageList : (ImageList) null;
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage))
      return;
    this.lastIndexCB = -1;
    this.lastHasApplIndexCB = -1;
    this.relationTypeList = new RelationTypeList((int) this._folder.Id);
    this.relationTypeList.Fill();
    this.FillRelationsCB();
    RelationTypeMember relationTypeMember = (RelationTypeMember) null;
    if (this.lastRelAssigned)
      relationTypeMember = this.relationTypeList.GetMemberByRel(this.lastRelType, this.lastIsReversed);
    if (relationTypeMember != null)
      this.relationsCB.SelectedItem = (object) relationTypeMember;
    else if (this.relationsCB.Items.Count > 0)
      this.relationsCB.SelectedIndex = 0;
    StatesController.SetLoadState((object) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage, true);
  }

  public override bool SaveForm(IFolder folder)
  {
    if (StatesController.GetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage))
    {
      if (this.propertyGrid.SelectedObject is ObjTypeApplPGClass && ((ObjTypeApplPGClass) this.propertyGrid.SelectedObject).isModified)
      {
        ObjTypeApplPGClass selectedObject = (ObjTypeApplPGClass) this.propertyGrid.SelectedObject;
        if (!selectedObject.SaveValues())
          return false;
        ApplicabilityChangedEventArgs e = new ApplicabilityChangedEventArgs("ApplicabilityChanged", selectedObject.relType, selectedObject.objType, selectedObject.inObjType);
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          service.FireEvent((object) null, (NotificationEventArgs) e);
        this.FillData(this.currentRelationTypeMember, true);
      }
      StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage, false);
    }
    return true;
  }

  public override bool RefreshAfterCanceling()
  {
    this.objTypeApplPGClass.FillValuesWithRevert();
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage, false);
    return false;
  }

  private void FillRelationsCB()
  {
    this.relationsCB.Items.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
      DataTable applicabilitiesList1 = applicabilityCollection.GetApplicabilitiesList(-1, (int) this._folder.Id, -1);
      DataTable applicabilitiesList2 = applicabilityCollection.GetApplicabilitiesList(-1, -1, (int) this._folder.Id);
      for (int index = 0; index < this.relationTypeList.Count; ++index)
      {
        RelationTypeMember relationType = (RelationTypeMember) this.relationTypeList[index];
        DataRow[] dataRowArray = (relationType.isReversed ? applicabilitiesList1 : applicabilitiesList2).Select("F_RELATION_TYPE=" + relationType.relType.ToString());
        if (dataRowArray != null && dataRowArray.Length != 0)
          arrayList1.Add((object) relationType);
        else
          arrayList2.Add((object) relationType);
      }
      arrayList1.Sort();
      arrayList2.Sort();
      for (int index = 0; index < arrayList1.Count; ++index)
        this.relationsCB.Items.Add(arrayList1[index]);
      this.lastHasApplIndexCB = this.relationsCB.Items.Count - 1;
      for (int index = 0; index < arrayList2.Count; ++index)
        this.relationsCB.Items.Add(arrayList2[index]);
    }
  }

  private void relationsCB_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.blockOnIndexChange || this.lastIndexCB == this.relationsCB.SelectedIndex)
      return;
    this.lastIndexCB = this.relationsCB.SelectedIndex;
    this.lastRelAssigned = true;
    this.lastRelType = ((RelationTypeMember) this.relationsCB.Items[this.relationsCB.SelectedIndex]).relType;
    this.lastIsReversed = ((RelationTypeMember) this.relationsCB.Items[this.relationsCB.SelectedIndex]).isReversed;
    this.currentRelationTypeMember = (RelationTypeMember) this.relationsCB.Items[this.relationsCB.SelectedIndex];
    this.FillTreeView(this.currentRelationTypeMember);
  }

  private void FillData(RelationTypeMember rtm, bool reload)
  {
    this.relationTypeList.CheckRelationInfo(rtm, reload);
    this.objectTypes.Clear();
    this.allChilds.Clear();
    this.rootObjectTypes.Clear();
    for (int index = 0; index < rtm.objTypeApplList.Count; ++index)
    {
      int id = !rtm.isReversed ? ((ObjTypeApplMember) rtm.objTypeApplList[index]).ObjType : ((ObjTypeApplMember) rtm.objTypeApplList[index]).InObjType;
      this.objectTypes.Add((object) id);
      ArrayList ch = new ArrayList();
      this.GetAllChilds(id, ch);
      this.allChilds.Add((object) ch);
    }
    for (int index1 = 0; index1 < this.objectTypes.Count; ++index1)
    {
      bool flag = false;
      for (int index2 = 0; index2 < this.objectTypes.Count; ++index2)
      {
        if (index2 != index1 && ((ArrayList) this.allChilds[index2]).IndexOf((object) (int) this.objectTypes[index1]) != -1)
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        this.rootObjectTypes.Add((object) (int) this.objectTypes[index1]);
    }
  }

  private void FillTreeView(RelationTypeMember rtm)
  {
    this.FillData(rtm, true);
    this._blocktvchange = true;
    try
    {
      this.treeView.Nodes.Clear();
      for (int index = 0; index < this.rootObjectTypes.Count; ++index)
        this.AddToTreeView((int) this.rootObjectTypes[index]);
    }
    finally
    {
      this._blocktvchange = false;
    }
    if (this.treeView.Nodes.Count > 0)
      this.treeView.SelectedNode = this.treeView.Nodes[0];
    else
      this.propertyGrid.SelectedObject = (object) null;
  }

  private void AddToTreeView(int objType)
  {
    this.treeView.BeginUpdate();
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objType, false);
        if (objectType == null)
          return;
        ObjectTypeFolder objectTypeFolder = new ObjectTypeFolder(this.instGuid, objectType.ObjectTypeName, (object) this.treeView, objectType.ObjectType, false, objectType.ObjectInstanceName, objectType.Versionable, objectType.Note, objectType.DefaultRelation, (objectType as IDBGuid).GUID, (objectType as IDBSubjectArea).SubjectAreas, objectType.CaptionAttribute, objectType.AnyAttributes, objectType.ObjectTypeShortName, objectType.LifetimeReserve, objectType.Options, objectType.SchemaID);
      }
    }
    finally
    {
      this.treeView.EndUpdate();
    }
  }

  private void GetAllChilds(int id, ArrayList ch)
  {
    foreach (DataRow row in (InternalDataCollectionBase) DataHolders.ObjectTypesHolder.LoadData(false, (object) id).Rows)
    {
      int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      ch.Add((object) int32);
      this.GetAllChilds(int32, ch);
    }
  }

  private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    if (e.Action != TreeViewAction.Expand)
      return;
    TreeNode node = e.Node;
    if (node == null || !ClientConsts.IsFakeNode(node))
      return;
    ((IFolder) node.Tag).Populate(false);
    node.Expand();
  }

  private void contextMenu_Popup(object sender, EventArgs e)
  {
    this.removeMenuItem.Enabled = this.treeView.SelectedNode != null && this.propertyGrid.SelectedObject != null && this.propertyGrid.SelectedObject is ObjTypeApplPGClass && !((ObjTypeApplPGClass) this.propertyGrid.SelectedObject).isModified && ((ObjTypeApplPGClass) this.propertyGrid.SelectedObject).InheritModePropertyClass != null && ((ObjTypeApplPGClass) this.propertyGrid.SelectedObject).InheritModePropertyClass.InheritMode != InheritModes.Inherited;
  }

  private void addMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode treeNode1 = (TreeNode) null;
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), LocalizationHolder.rm.GetString("Client.Core_88"), typeof (ObjectTypeFolder), false);
    if (selectorForm.ShowDialog() != DialogResult.OK || selectorForm.IDList.Count == 0)
      return;
    int id = (int) selectorForm.IDList[0];
    int index1 = -1;
    for (int index2 = 0; index2 < this.rootObjectTypes.Count; ++index2)
    {
      if ((int) this.rootObjectTypes[index2] == id)
      {
        this.treeView.SelectedNode = this.GetTreeNodeByObjType(id, this.treeView.Nodes);
        int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("Client.Core_131"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
        return;
      }
      int index3 = this.objectTypes.IndexOf((object) (int) this.rootObjectTypes[index2]);
      if (index3 != -1 && ((ArrayList) this.allChilds[index3]).IndexOf((object) id) != -1)
      {
        index1 = index2;
        break;
      }
    }
    ArrayList ch = new ArrayList();
    this.GetAllChilds(id, ch);
    if (index1 == -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
        int _inObjectType;
        int _objectType;
        if (this.currentRelationTypeMember.isReversed)
        {
          _inObjectType = id;
          _objectType = this.relationTypeList.ObjType;
        }
        else
        {
          _inObjectType = this.relationTypeList.ObjType;
          _objectType = id;
        }
        RelationsApplicabilityProperties applicabilityProperties1 = new RelationsApplicabilityProperties(0, _objectType, _inObjectType, this.currentRelationTypeMember.relType, false, int.MaxValue, ApplicabilityModes.Enabled, RelationConstraintModes.None, false, false, ApplicabilityOptions.None);
        RelationsApplicabilityProperties applicabilityProperties2 = applicabilityProperties1;
        applicabilityCollection.Create(applicabilityProperties2);
        for (int index4 = 0; index4 < this.rootObjectTypes.Count; ++index4)
        {
          if (ch.IndexOf((object) (int) this.rootObjectTypes[index4]) != -1)
            this.GetTreeNodeByObjType((int) this.rootObjectTypes[index4], this.treeView.Nodes)?.Remove();
        }
        this.AddToTreeView(id);
        this.FillData(this.currentRelationTypeMember, true);
        treeNode1 = this.GetTreeNodeByObjType(id, this.treeView.Nodes);
        ApplicabilityChangedEventArgs e1 = new ApplicabilityChangedEventArgs("ApplicabilityAdded", applicabilityProperties1.RelationType, applicabilityProperties1.ObjectType, applicabilityProperties1.InObjectType);
        if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
          service.FireEvent((object) null, (NotificationEventArgs) e1);
      }
    }
    else
    {
      TreeNode treeNode2 = (TreeNode) null;
      ArrayList arrayList = this.GiveAllParents(id);
      for (int index5 = 0; index5 < arrayList.Count; ++index5)
      {
        if ((int) arrayList[index5] == (int) this.rootObjectTypes[index1])
        {
          TreeNodeCollection nodes = this.treeView.Nodes;
          for (int index6 = index5; index6 >= 0; --index6)
          {
            treeNode2 = this.GetTreeNodeByObjType((int) arrayList[index6], nodes);
            treeNode2.Expand();
            nodes = treeNode2.Nodes;
          }
        }
      }
      treeNode1 = this.GetTreeNodeByObjType(id, treeNode2.Nodes);
      int num = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("Client.Core_131"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
    }
    this.treeView.SelectedNode = treeNode1;
    if (this.treeView.Nodes.Count <= 0 || this.relationsCB.SelectedIndex <= this.lastHasApplIndexCB)
      return;
    this.blockOnIndexChange = true;
    try
    {
      if (this.relationsCB.SelectedIndex - this.lastHasApplIndexCB > 1)
      {
        object obj = this.relationsCB.Items[this.relationsCB.SelectedIndex];
        this.relationsCB.Items.RemoveAt(this.relationsCB.SelectedIndex);
        this.relationsCB.Items.Insert(this.lastHasApplIndexCB + 1, obj);
        this.relationsCB.SelectedIndex = this.lastHasApplIndexCB + 1;
      }
      ++this.lastHasApplIndexCB;
      this.relationsCB.Refresh();
    }
    finally
    {
      this.blockOnIndexChange = false;
    }
    this.relationsCB_SelectedIndexChanged((object) this, new EventArgs());
  }

  private void removeMenuItem_Click(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null || selectedNode.Tag == null || selectedNode.Tag.GetType() != typeof (ObjectTypeFolder) || IMMessageBox.Show(MessageDialogs.msgConfirmDelete, MessageDialogs.msgReallyDelete + "\nВнимание! Также будут удалены соответствующие связи между объектами указанных типов", MessageBoxButtons.YesNo, IMMessageBoxImage.Question) != DialogResult.Yes)
      return;
    int id = (int) (selectedNode.Tag as IFolder).Id;
    int num1 = this.rootObjectTypes.IndexOf((object) id);
    int index1 = this.objectTypes.IndexOf((object) id);
    int _relationType = 0;
    int _objectType = 0;
    int _inObjectType = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (num1 != -1)
      {
        if (index1 == -1)
          return;
        ArrayList arrayList = new ArrayList();
        for (int index2 = 0; index2 < this.objectTypes.Count; ++index2)
        {
          if (index2 != index1 && ((ArrayList) this.allChilds[index1]).IndexOf((object) (int) this.objectTypes[index2]) != -1)
            arrayList.Add((object) index2);
        }
        if (arrayList.Count > 0)
        {
          for (int index3 = 0; index3 < arrayList.Count; ++index3)
          {
            for (int index4 = 0; index4 < arrayList.Count; ++index4)
            {
              if (index4 != index3 && ((ArrayList) this.allChilds[(int) arrayList[index4]]).IndexOf(this.objectTypes[(int) arrayList[index3]]) != -1)
                arrayList.RemoveAt(index3);
            }
          }
        }
        IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(((ObjTypeApplMember) this.currentRelationTypeMember.objTypeApplList[index1]).ApplId);
        if (applicability.RelationsCount != 0 && applicability.IsContent)
          throw new KernelExceptionID(312);
        _relationType = applicability.RelationType;
        _objectType = applicability.ObjectType;
        _inObjectType = applicability.InObjectType;
        applicability.Delete();
        for (int index5 = 0; index5 < arrayList.Count; ++index5)
          this.AddToTreeView((int) this.objectTypes[(int) arrayList[index5]]);
        selectedNode.Remove();
      }
      else
      {
        if (index1 == -1)
        {
          int num2 = (int) IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_132"), LocalizationHolder.rm.GetString("Client.Core_133"), MessageBoxButtons.OK, IMMessageBoxImage.Warning);
          return;
        }
        IDBRelationsApplicability applicability = sessionKeeper.Session.GetRelationsApplicabilityCollection().GetApplicability(((ObjTypeApplMember) this.currentRelationTypeMember.objTypeApplList[index1]).ApplId);
        if (applicability.RelationsCount != 0 && applicability.IsContent)
          throw new KernelExceptionID(312);
        _relationType = applicability.RelationType;
        _objectType = applicability.ObjectType;
        _inObjectType = applicability.InObjectType;
        applicability.Delete();
      }
    }
    this.FillData(this.currentRelationTypeMember, true);
    this.LoadNodeAndDraw(this.treeView.SelectedNode);
    if (this.treeView.Nodes.Count == 0 && this.relationsCB.SelectedIndex <= this.lastHasApplIndexCB)
    {
      this.blockOnIndexChange = true;
      try
      {
        if (this.lastHasApplIndexCB - this.relationsCB.SelectedIndex > 0)
        {
          object obj = this.relationsCB.Items[this.relationsCB.SelectedIndex];
          this.relationsCB.Items.RemoveAt(this.relationsCB.SelectedIndex);
          this.relationsCB.Items.Add(obj);
          this.relationsCB.SelectedIndex = this.relationsCB.Items.Count - 1;
        }
        --this.lastHasApplIndexCB;
        this.relationsCB.Refresh();
      }
      finally
      {
        this.blockOnIndexChange = false;
      }
      this.relationsCB_SelectedIndexChanged((object) this, new EventArgs());
    }
    ApplicabilityChangedEventArgs e1 = new ApplicabilityChangedEventArgs("ApplicabilityRemoved", _relationType, _objectType, _inObjectType);
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.FireEvent((object) null, (NotificationEventArgs) e1);
  }

  private TreeNode GetTreeNodeByObjType(int rootObjectType, TreeNodeCollection tnc)
  {
    TreeNode treeNodeByObjType = (TreeNode) null;
    for (int index = 0; index < tnc.Count; ++index)
    {
      if (tnc[index].Tag.GetType() == typeof (ObjectTypeFolder) && (int) (tnc[index].Tag as IFolder).Id == rootObjectType)
      {
        treeNodeByObjType = tnc[index];
        break;
      }
    }
    return treeNodeByObjType;
  }

  private ArrayList GiveAllParents(int aObjType)
  {
    ArrayList arrayList = new ArrayList();
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    for (IDBObjectTypeInfo objectType = service.GetObjectType(aObjType, false); objectType != null && objectType.ParentTypeID != -1; objectType = service.GetObjectType(objectType.ParentTypeID, false))
      arrayList.Add((object) objectType.ParentTypeID);
    return arrayList;
  }

  private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    if (!(this.propertyGrid.SelectedObject is ObjTypeApplPGClass) || !(this.propertyGrid.SelectedObject as ObjTypeApplPGClass).ChangePropertyEventProcessing(s, e))
      return;
    StatesController.SetModifiedState((object) TabPagesHolder.TabPages(this.instGuid).ObjTypeApplTabPage, true);
    EventsHolder.FireWasChanged(s, this.instGuid, (EventArgs) e);
  }

  private void LoadNodeAndDraw(TreeNode tn)
  {
    if (tn == null)
    {
      this.propertyGrid.SelectedObject = (object) null;
    }
    else
    {
      int id1 = (int) this._folder.Id;
      int id2 = (int) (tn.Tag as IFolder).Id;
      int aInObjType;
      int aObjType;
      if (((RelationTypeMember) this.relationsCB.SelectedItem).isReversed)
      {
        aInObjType = id2;
        aObjType = id1;
      }
      else
      {
        aInObjType = id1;
        aObjType = id2;
      }
      this.objTypeApplPGClass = new ObjTypeApplPGClass(((RelationTypeMember) this.relationsCB.SelectedItem).relType, aObjType, aInObjType, this.propertyGrid);
      this.propertyGrid.SelectedObject = (object) this.objTypeApplPGClass;
      this.objTypeApplPGClass.FillValues();
    }
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    if (e.Action == TreeViewAction.Collapse || e.Action == TreeViewAction.Expand || this._blocktvchange)
      return;
    this.LoadNodeAndDraw(e.Node);
  }

  private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    if (!this._folder.InChange)
      return;
    switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_135"), LocalizationHolder.rm.GetString("Client.Core_134"), MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
    {
      case DialogResult.Yes:
        if (!this._folder.ApplyData())
        {
          e.Cancel = true;
          break;
        }
        EventsHolder.FireApply(sender, this.instGuid, new EventsHolder.BoolArgs(true));
        break;
      case DialogResult.No:
        this._folder.Cancel(false);
        EventsHolder.FireCancel(sender, this.instGuid, (EventArgs) e);
        break;
      default:
        e.Cancel = true;
        break;
    }
  }

  private void relationsCB_DrawItem(object sender, DrawItemEventArgs e)
  {
    e.DrawBackground();
    if (e.Index == -1)
      return;
    Brush brush = SystemBrushes.ControlText;
    FontStyle newStyle = FontStyle.Regular;
    if (e.Index <= this.lastHasApplIndexCB)
      newStyle = FontStyle.Bold;
    using (Font font = new Font(this.relationsCB.Font, newStyle))
    {
      if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
        brush = SystemBrushes.HighlightText;
      e.Graphics.DrawString(this.relationsCB.Items[e.Index].ToString(), font, brush, (float) (e.Bounds.Left + 2), (float) (e.Bounds.Top + 2));
    }
  }

  private void ObjTypeApplForm_Enter(object sender, EventArgs e)
  {
  }

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1025";
}
