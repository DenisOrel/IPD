// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.AddSignGraphsForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class AddSignGraphsForm : FormEx
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private EnhListView GraphsView;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  public CheckBox NewGroupBox;

  public AddSignGraphsForm() => this.InitializeComponent();

  private void AddSignGraphsForm_Load(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.GraphsView.Items.Clear();
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(wfConsts.SignGraphID);
      if (attributeType == null || attributeType.MultipleValued == MultiValueModes.SingleValue)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) attributeType.GetPossibleValues().Rows)
      {
        ListViewItem listViewItem = this.GraphsView.Items.Add(row[2].ToString());
        GraphInfo graphInfo = new GraphInfo(row[1].ToString(), StrongSignMode.Default);
        listViewItem.Tag = (object) graphInfo;
        CheckBoxListViewSubItem boxListViewSubItem = new CheckBoxListViewSubItem();
        boxListViewSubItem.Tag = (object) graphInfo;
        boxListViewSubItem.Checked = graphInfo.StrongSign;
        boxListViewSubItem.OnClick += new EventHandler(this.si_OnClick);
        listViewItem.SubItems.Add((ListViewItem.ListViewSubItem) boxListViewSubItem);
      }
    }
  }

  private void si_OnClick(object sender, EventArgs e)
  {
    if (!(sender is CheckBoxListViewSubItem))
      return;
    CheckBoxListViewSubItem boxListViewSubItem = (CheckBoxListViewSubItem) sender;
    (boxListViewSubItem.Tag as GraphInfo).StrongSign = boxListViewSubItem.Checked;
  }

  public GraphInfoList Selected
  {
    get
    {
      GraphInfoList selected = new GraphInfoList();
      foreach (int checkedIndex in this.GraphsView.CheckedIndices)
      {
        ListViewItem listViewItem = this.GraphsView.Items[checkedIndex];
        selected.Add((GraphInfo) listViewItem.Tag);
      }
      return selected;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AddSignGraphsForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.GraphsView = new EnhListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.NewGroupBox = new CheckBox();
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
    this.GraphsView.CheckBoxes = true;
    this.GraphsView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    componentResourceManager.ApplyResources((object) this.GraphsView, "GraphsView");
    this.GraphsView.FullRowSelect = true;
    this.GraphsView.MultiSelect = false;
    this.GraphsView.Name = "GraphsView";
    this.GraphsView.OwnerDraw = true;
    this.GraphsView.RadioGroups = false;
    this.GraphsView.SortColumn = 0;
    this.GraphsView.Sorting = SortOrder.Ascending;
    this.GraphsView.SubitemImages = (ImageList) null;
    this.GraphsView.UseCompatibleStateImageBehavior = false;
    this.GraphsView.View = View.Details;
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.NewGroupBox, "NewGroupBox");
    this.NewGroupBox.Name = "NewGroupBox";
    this.NewGroupBox.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.GraphsView);
    this.Controls.Add((Control) this.NewGroupBox);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AddSignGraphsForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.Load += new EventHandler(this.AddSignGraphsForm_Load);
    this.Panel2.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
