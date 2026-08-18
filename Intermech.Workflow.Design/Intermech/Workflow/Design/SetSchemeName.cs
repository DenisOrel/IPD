// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.SetSchemeName
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class SetSchemeName : Form
{
  private long _categoryID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox schemeName;
  private Button okBtn;
  private Button cancelBtn;
  private SchemesTreeView schemesTreeView1;
  private GroupBox schemesGroup;
  private GroupBox nameGroup;
  private GroupBox btnGroup;
  private SplitContainer splitContainer1;

  public SetSchemeName(bool visibleSchemesCat = true)
  {
    this.InitializeComponent();
    this.schemesGroup.Visible = visibleSchemesCat;
    this.splitContainer1.Panel1Collapsed = !visibleSchemesCat;
  }

  public string SchemeName => this.schemeName.Text;

  public long CategoryID => this._categoryID;

  private void okBtn_Click(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(this.SchemeName))
    {
      int num = (int) MessageBox.Show("Введите название шаблона.");
    }
    else
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }

  private void SetSchemeName_FormClosing(object sender, FormClosingEventArgs e)
  {
    ISelectedItems selectedItems = this.schemesTreeView1.SelectedItems;
    if (this.schemesGroup.Visible && selectedItems != null && selectedItems.Count > 0 && selectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      this._categoryID = itemData.Value;
    if (!this.splitContainer1.Panel1Collapsed)
      wfFunx.SaveTreePath((NavigatorTreeView) this.schemesTreeView1);
    FormStorage.SaveLayout((Control) this);
  }

  private void SetSchemeName_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    if (this.splitContainer1.Panel1Collapsed)
    {
      this.MinimumSize = new Size(540, 155);
      this.MaximumSize = new Size(1920, 155);
      this.Invalidate();
    }
    else
    {
      if (this.Size.Height > 200)
        return;
      this.Size = new Size(this.Size.Width, 300);
      this.MinimumSize = new Size(540, 300);
      this.MaximumSize = this.DefaultMaximumSize;
      this.Invalidate();
    }
  }

  private void SetSchemeName_Shown(object sender, EventArgs e)
  {
    if (this.splitContainer1.Panel1Collapsed)
      return;
    IDescriptor rootDescriptor = (IDescriptor) new TopObjectsDescriptor(Holder.CategorySchemesID, 0, LocalizationHolder.rm.GetString("Workflow.Editor_18"), wfConsts.SchemeCategoriesID);
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog));
    serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    serviceContainer.AddService(typeof (VersionsRule), (object) Holder.AllVersionsRule);
    this.schemesTreeView1.Services = (System.IServiceProvider) serviceContainer;
    this.schemesTreeView1.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    if (wfFunx.RestoreTreePath((NavigatorTreeView) this.schemesTreeView1))
      return;
    this.schemesTreeView1.Build(rootDescriptor);
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
    this.schemeName = new TextBox();
    this.okBtn = new Button();
    this.cancelBtn = new Button();
    this.schemesGroup = new GroupBox();
    this.schemesTreeView1 = new SchemesTreeView();
    this.nameGroup = new GroupBox();
    this.btnGroup = new GroupBox();
    this.splitContainer1 = new SplitContainer();
    this.schemesGroup.SuspendLayout();
    this.schemesTreeView1.BeginInit();
    this.nameGroup.SuspendLayout();
    this.btnGroup.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this.schemeName.Dock = DockStyle.Fill;
    this.schemeName.Location = new Point(7, 21);
    this.schemeName.Margin = new Padding(4);
    this.schemeName.Name = "schemeName";
    this.schemeName.Size = new Size(663, 22);
    this.schemeName.TabIndex = 0;
    this.okBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.okBtn.Location = new Point(461, 14);
    this.okBtn.Margin = new Padding(4);
    this.okBtn.Name = "okBtn";
    this.okBtn.Size = new Size(100, 28);
    this.okBtn.TabIndex = 2;
    this.okBtn.Text = "OK";
    this.okBtn.UseVisualStyleBackColor = true;
    this.okBtn.Click += new EventHandler(this.okBtn_Click);
    this.cancelBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.cancelBtn.DialogResult = DialogResult.Cancel;
    this.cancelBtn.Location = new Point(569, 14);
    this.cancelBtn.Margin = new Padding(4);
    this.cancelBtn.Name = "cancelBtn";
    this.cancelBtn.Size = new Size(100, 28);
    this.cancelBtn.TabIndex = 2;
    this.cancelBtn.Text = "Отмена";
    this.cancelBtn.UseVisualStyleBackColor = true;
    this.schemesGroup.Controls.Add((Control) this.schemesTreeView1);
    this.schemesGroup.Dock = DockStyle.Fill;
    this.schemesGroup.Location = new Point(0, 0);
    this.schemesGroup.Margin = new Padding(4);
    this.schemesGroup.Name = "schemesGroup";
    this.schemesGroup.Padding = new Padding(7, 6, 7, 6);
    this.schemesGroup.Size = new Size(677, 207);
    this.schemesGroup.TabIndex = 5;
    this.schemesGroup.TabStop = false;
    this.schemesGroup.Text = "Выберите группу шаблонов куда нужно сохранить: ";
    this.schemesTreeView1.AllowDrop = true;
    this.schemesTreeView1.AllowMultiSelect = false;
    this.schemesTreeView1.AllowUserPinnedColumns = false;
    this.schemesTreeView1.DisableCheckedOutColumn = true;
    this.schemesTreeView1.Dock = DockStyle.Fill;
    this.schemesTreeView1.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.schemesTreeView1.ImageList = (ImageList) null;
    this.schemesTreeView1.LineStyle = LineStyle.Dot;
    this.schemesTreeView1.Location = new Point(7, 21);
    this.schemesTreeView1.Margin = new Padding(4);
    this.schemesTreeView1.Name = "schemesTreeView1";
    this.schemesTreeView1.RowEvenStyle.WordWrap = false;
    this.schemesTreeView1.RowOddStyle.WordWrap = false;
    this.schemesTreeView1.RowSelectedStyle.WordWrap = false;
    this.schemesTreeView1.RowStyle.BorderColor = SystemColors.Control;
    this.schemesTreeView1.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.schemesTreeView1.RowStyle.BorderWidth = 1;
    this.schemesTreeView1.RowStyle.WordWrap = false;
    this.schemesTreeView1.SelectBeforeEdit = true;
    this.schemesTreeView1.ShowRootRow = false;
    this.schemesTreeView1.Size = new Size(663, 180);
    this.schemesTreeView1.SuppressErrorMessages = true;
    this.schemesTreeView1.TabIndex = 3;
    this.nameGroup.Controls.Add((Control) this.schemeName);
    this.nameGroup.Dock = DockStyle.Fill;
    this.nameGroup.Location = new Point(0, 0);
    this.nameGroup.Margin = new Padding(4);
    this.nameGroup.Name = "nameGroup";
    this.nameGroup.Padding = new Padding(7, 6, 7, 6);
    this.nameGroup.Size = new Size(677, 57);
    this.nameGroup.TabIndex = 6;
    this.nameGroup.TabStop = false;
    this.nameGroup.Text = "Название шаблона: ";
    this.btnGroup.Controls.Add((Control) this.cancelBtn);
    this.btnGroup.Controls.Add((Control) this.okBtn);
    this.btnGroup.Dock = DockStyle.Bottom;
    this.btnGroup.Location = new Point(11, 279);
    this.btnGroup.Margin = new Padding(4);
    this.btnGroup.Name = "btnGroup";
    this.btnGroup.Padding = new Padding(4);
    this.btnGroup.Size = new Size(677, 54);
    this.btnGroup.TabIndex = 7;
    this.btnGroup.TabStop = false;
    this.splitContainer1.Dock = DockStyle.Fill;
    this.splitContainer1.FixedPanel = FixedPanel.Panel2;
    this.splitContainer1.IsSplitterFixed = true;
    this.splitContainer1.Location = new Point(11, 10);
    this.splitContainer1.Margin = new Padding(4);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this.schemesGroup);
    this.splitContainer1.Panel2.Controls.Add((Control) this.nameGroup);
    this.splitContainer1.Size = new Size(677, 269);
    this.splitContainer1.SplitterDistance = 207;
    this.splitContainer1.SplitterWidth = 5;
    this.splitContainer1.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.cancelBtn;
    this.ClientSize = new Size(699, 343);
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this.btnGroup);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Margin = new Padding(4);
    this.MinimumSize = new Size(540, 200);
    this.Name = nameof (SetSchemeName);
    this.Padding = new Padding(11, 10, 11, 10);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Введите название шаблона";
    this.FormClosing += new FormClosingEventHandler(this.SetSchemeName_FormClosing);
    this.Load += new EventHandler(this.SetSchemeName_Load);
    this.Shown += new EventHandler(this.SetSchemeName_Shown);
    this.schemesGroup.ResumeLayout(false);
    this.schemesTreeView1.EndInit();
    this.nameGroup.ResumeLayout(false);
    this.nameGroup.PerformLayout();
    this.btnGroup.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
