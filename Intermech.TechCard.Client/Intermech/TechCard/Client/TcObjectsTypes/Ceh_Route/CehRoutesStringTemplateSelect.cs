// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route.CehRoutesStringTemplateSelect
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Interfaces.TechCard.Ceh_Route;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.Ceh_Route;

/// <summary>Диалог выбора шаблона расцеховки</summary>
public class CehRoutesStringTemplateSelect : Form
{
  private readonly List<int> _templateIdList = new List<int>();
  private ICehRouteStringItem _routeStringItem;
  private ICehRouteStringTemplItem _routeStringTemplate;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOk;
  private TableLayoutPanel tableLayoutPanel1;
  private Button btnCancel;
  private TreeList tlTemplates;
  private TreeListColumn treeListColumn1;

  /// <summary>Init data</summary>
  private void InitData() => this.InitializeCustomSettings();

  /// <summary>
  /// 
  /// </summary>
  private void InitializeCustomSettings()
  {
    INamedImageList service1 = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    if (service1 != null)
    {
      this.tlTemplates.StateImageList = service1.ImageList;
      this.tlTemplates.CheckedStateIndex = service1.ImageIndex("imgChecked");
      this.tlTemplates.UncheckedStateIndex = service1.ImageIndex("imgUnchecked");
      this.tlTemplates.GrayedStateIndex = service1.ImageIndex("imgGrayed");
    }
    else
      this.tlTemplates.StateImageList = (ImageList) null;
    ICategoryTypeIconService service2 = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    if (service2 == null)
      return;
    Icon icon = service2.GetIcon(4, TechCardConsts.ObjectTypes.TemplRouteBaseID);
    if (icon == null)
      return;
    this.Icon = icon;
  }

  /// <summary>Update tree list node state</summary>
  /// <param name="treeNode"></param>
  private void UpdateNodeState(TreeListNode treeNode)
  {
    if (treeNode == null)
      return;
    int tag = (int) treeNode.Tag;
    if (this._templateIdList.Contains(tag))
    {
      if (this._routeStringTemplate != null && this._routeStringTemplate.ObjTypeID == tag)
        treeNode.CheckState = CheckState.Checked;
      else
        treeNode.CheckState = CheckState.Indeterminate;
    }
    else
      treeNode.CheckState = CheckState.Unchecked;
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateButtons() => this.btnOk.Enabled = this.tlTemplates.CheckedNodes.Count > 0;

  /// <summary>Конструктор</summary>
  public CehRoutesStringTemplateSelect()
  {
    this.InitializeComponent();
    this.InitData();
  }

  /// <summary>ICehRouteStringItem</summary>
  public ICehRouteStringItem RouteStringItem
  {
    get => this._routeStringItem;
    set
    {
      this._routeStringItem = value;
      this._templateIdList.Clear();
      if (this._routeStringItem == null)
        return;
      foreach (ICehRouteStringTemplItem routeStringTemplItem in (IEnumerable<ICehRouteStringTemplItem>) this._routeStringItem.Items)
        this._templateIdList.Add(routeStringTemplItem.ObjTypeID);
    }
  }

  /// <summary>Шаблон расцеховки</summary>
  public ICehRouteStringTemplItem RouteStringTemplate
  {
    get => this._routeStringTemplate;
    set => this._routeStringTemplate = value;
  }

  /// <summary>Заполнение списка</summary>
  public void LoadData()
  {
    this.tlTemplates.BeginUpdate();
    try
    {
      this.tlTemplates.CheckStateChanging -= new CheckStateChangingEventHandler(this.tlTemplates_CheckStateChanging);
      this.tlTemplates.Nodes.Clear();
      foreach (int objTypeID in MetaDataHelper.GetObjectTypeChildrenID(TechCardConsts.ObjectTypes.TemplRouteBaseID))
      {
        IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
        if (objectType != null)
        {
          TreeListNode treeNode = this.tlTemplates.AppendNode((object) null, (TreeListNode) null);
          treeNode.SetValue((object) 0, (object) objectType.ObjectTypeName);
          treeNode.Tag = (object) objTypeID;
          this.UpdateNodeState(treeNode);
        }
      }
    }
    finally
    {
      this.tlTemplates.CheckStateChanging += new CheckStateChangingEventHandler(this.tlTemplates_CheckStateChanging);
      this.tlTemplates.EndUpdate();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlTemplates_CheckStateChanging(object sender, CheckStateEventArgs e)
  {
    if (e.OldValue == CheckState.Indeterminate)
      e.NewValue = CheckState.Indeterminate;
    if (e.NewValue != CheckState.Checked)
      return;
    this.tlTemplates.CheckStateChanging -= new CheckStateChangingEventHandler(this.tlTemplates_CheckStateChanging);
    try
    {
      foreach (TreeListNode node in this.tlTemplates.Nodes)
      {
        if (node.CheckState == CheckState.Checked)
          node.CheckState = CheckState.Unchecked;
      }
    }
    finally
    {
      this.tlTemplates.CheckStateChanging += new CheckStateChangingEventHandler(this.tlTemplates_CheckStateChanging);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.tlTemplates.CheckedNodes.Count != 1)
      return;
    this._routeStringTemplate.ObjTypeID = (int) this.tlTemplates.CheckedNodes[0].Tag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tlTemplates_CheckStateChanged(object sender, NodeEventArgs e)
  {
    this.UpdateButtons();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CehRoutesStringTemplateSelect));
    this.btnOk = new Button();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.btnCancel = new Button();
    this.tlTemplates = new TreeList();
    this.treeListColumn1 = new TreeListColumn();
    this.tableLayoutPanel1.SuspendLayout();
    this.tlTemplates.BeginInit();
    this.SuspendLayout();
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel1, "tableLayoutPanel1");
    this.tableLayoutPanel1.Controls.Add((Control) this.btnOk, 1, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.btnCancel, 2, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tlTemplates, "tlTemplates");
    this.tlTemplates.CheckBoxes = CheckBoxesStyle.ThreeState;
    this.tlTemplates.Columns.AddRange(new TreeListColumn[1]
    {
      this.treeListColumn1
    });
    this.tlTemplates.Name = "tlTemplates";
    this.tlTemplates.CheckStateChanged += new NodeEventHandler(this.tlTemplates_CheckStateChanged);
    componentResourceManager.ApplyResources((object) this.treeListColumn1, "treeListColumn1");
    this.treeListColumn1.Name = "treeListColumn1";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlTemplates);
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (CehRoutesStringTemplateSelect);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tlTemplates.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
