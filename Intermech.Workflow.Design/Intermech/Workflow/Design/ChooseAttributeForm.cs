// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ChooseAttributeForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ChooseAttributeForm : FormEx
{
  private long _varsSource;
  private IObjectTypesInheritanceCache _inhCache;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private TreeView AttrView;

  public ChooseAttributeForm()
  {
    this.InitializeComponent();
    this.AttrView.TreeViewNodeSorter = (IComparer) new AttrTreeViewItemComparer();
  }

  public ChooseAttributeForm(long VarsSourceID)
    : this()
  {
    this._varsSource = VarsSourceID;
    this.FillTree();
  }

  private IObjectTypesInheritanceCache ObjInheritanceCache
  {
    get
    {
      if (this._inhCache == null)
        this._inhCache = CacheManager.Cache("ObjectTypeInheritanceCache") as IObjectTypesInheritanceCache;
      return this._inhCache;
    }
  }

  private void AddTypeNode(TreeNode root, int typeID)
  {
    string objectTypeName = MetaDataHelper.GetObjectTypeName(typeID);
    if (string.IsNullOrEmpty(objectTypeName))
      return;
    TreeNode node = (TreeNode) new ObjTypeNode(objectTypeName, typeID);
    root.Nodes.Add(node);
    node.ImageIndex = BaseHolder.IconService.IndexOf(4, typeID);
    node.SelectedImageIndex = node.ImageIndex;
    node.Nodes.Add((TreeNode) new EmptyNode());
  }

  private void AddAttributeNode(TreeNode root, string name, int AttributeID, object type)
  {
    AttributeNode node = new AttributeNode(name, AttributeID);
    node.ImageIndex = BaseHolder.IconService.IndexOf(3, -1, type);
    node.SelectedImageIndex = node.ImageIndex;
    root.Nodes.Add((TreeNode) node);
  }

  private void FillTree()
  {
    this.AttrView.ImageList = BaseHolder.IconService.ImageList;
    if (this._varsSource == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject src = sessionKeeper.Session.GetObject(this._varsSource, false);
      if (src != null)
      {
        VarList varList = new VarList(src, false, false);
        TreeNode root1 = this.AttrView.Nodes.Add(LocalizationHolder.rm.GetString("Workflow.Design_24"));
        foreach (Variable variable in varList)
          this.AddAttributeNode(root1, variable.Name, variable.AttrTypeID, (object) variable.VarType);
        varList.Clear();
        varList.AddSystemVariables(src);
        TreeNode root2 = this.AttrView.Nodes.Add(LocalizationHolder.rm.GetString("Workflow.Design_25"));
        foreach (Variable variable in varList)
          this.AddAttributeNode(root2, variable.Name, variable.AttrTypeID, (object) variable.VarType);
      }
    }
    TreeNode root3 = this.AttrView.Nodes.Add(LocalizationHolder.rm.GetString("Workflow.Design_26"));
    root3.ImageIndex = BaseHolder.IconService.IndexOf(3, 0);
    root3.SelectedImageIndex = root3.ImageIndex;
    foreach (ObligatoryObjectAttributes objectAttributes in Enum.GetValues(typeof (ObligatoryObjectAttributes)))
    {
      string enumDescription = SimpleFuncs.GetEnumDescription((Enum) objectAttributes);
      if (enumDescription != "")
        this.AddAttributeNode(root3, enumDescription, (int) objectAttributes, (object) ObligatoryObjectAttributesHelper.GetDataType(objectAttributes));
    }
    List<int> applicableAttachmentTypes = wfFunx.GetApplicableAttachmentTypes();
    TreeNode root4 = this.AttrView.Nodes.Add(LocalizationHolder.rm.GetString("Workflow.Design_27"));
    root4.ImageIndex = BaseHolder.IconService.IndexOf(3, 0);
    root4.SelectedImageIndex = root4.ImageIndex;
    DataHolders.AttributesHolder.LoadData(false, (object) -1);
    foreach (int typeID in applicableAttachmentTypes)
      this.AddTypeNode(root4, typeID);
  }

  private void AttrView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.OkButton.Enabled = this.AttrView.SelectedNode is AttributeNode;
  }

  public int SelectedAttributeType
  {
    get
    {
      return !(this.AttrView.SelectedNode is AttributeNode selectedNode) ? 0 : selectedNode.AttributeID;
    }
  }

  private void AttrView_DoubleClick(object sender, EventArgs e)
  {
    if (this.SelectedAttributeType == 0)
      return;
    this.DialogResult = DialogResult.OK;
  }

  private void AttrView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    if (!(e.Node is ObjTypeNode node) || node.Nodes.Count != 1 || !(node.Nodes[0] is EmptyNode))
      return;
    node.Nodes.Clear();
    int typeId = node.TypeID;
    int[] childrenTypes = this.ObjInheritanceCache.GetChildrenTypes(typeId);
    if (childrenTypes != null)
    {
      foreach (int typeID in childrenTypes)
        this.AddTypeNode((TreeNode) node, typeID);
    }
    foreach (IMSAttribute4ObjectType attribute4ObjectType in MetaDataHelper.GetAttribute4ObjectTypeList(typeId))
    {
      DataRow attribute = DataHolders.AttributesHolder.GetAttribute(attribute4ObjectType.AttributeID);
      if (attribute != null)
        this.AddAttributeNode((TreeNode) node, attribute["F_NAME"].ToString(), Convert.ToInt32(attribute["F_ATTRIBUTE_ID"]), (object) (FieldTypes) Convert.ToInt32(attribute["F_ATTRIBUTE_TYPE"]));
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ChooseAttributeForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.AttrView = new TreeView();
    this.Panel2.SuspendLayout();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    componentResourceManager.ApplyResources((object) this.AttrView, "AttrView");
    this.AttrView.Name = "AttrView";
    this.AttrView.BeforeExpand += new TreeViewCancelEventHandler(this.AttrView_BeforeExpand);
    this.AttrView.AfterSelect += new TreeViewEventHandler(this.AttrView_AfterSelect);
    this.AttrView.DoubleClick += new EventHandler(this.AttrView_DoubleClick);
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.AttrView);
    this.Controls.Add((Control) this.Panel2);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChooseAttributeForm);
    this.ShowInTaskbar = false;
    this.Panel2.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
