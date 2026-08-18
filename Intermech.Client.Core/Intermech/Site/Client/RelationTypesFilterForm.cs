
// Type: Intermech.Site.Client.RelationTypesFilterForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Site.Client;

public class RelationTypesFilterForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TreeView treeView1;
  private Button bOK;
  private Button bCancel;
  private CheckBox checkBox1;

  public RelationTypesFilterForm()
  {
    this.InitializeComponent();
    this.treeView1.ImageList = ((ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService))).ImageList;
  }

  public void LoadData(List<int> enabledTypes)
  {
    this.treeView1.Nodes.Clear();
    this.checkBox1.Checked = true;
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    IPublishTypesConfiguration customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IPublishTypesConfiguration)) as IPublishTypesConfiguration;
    foreach (DataRow row in (InternalDataCollectionBase) (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetRelationTypeCollection().Select("F_DESCRIPTION").Rows)
    {
      int int32 = Convert.ToInt32(row["F_RELATION_TYPE"]);
      if (customService.GetRelationMigrateType(new Guid(Convert.ToString(row["F_GUID"]))) == RelationMigrateType.DependsSetting)
      {
        TreeNode treeNode = this.treeView1.Nodes.Add(Convert.ToString(row["F_DESCRIPTION"]));
        int num1;
        int num2 = num1 = service.IndexOf(6, int32);
        treeNode.SelectedImageIndex = num1;
        treeNode.ImageIndex = num2;
        treeNode.Checked = enabledTypes == null || enabledTypes.Contains(int32);
        treeNode.Tag = (object) int32;
      }
    }
  }

  public void SetEnabledRelationTypes(List<int> enabledTypes)
  {
    foreach (TreeNode node in this.treeView1.Nodes)
      node.Checked = enabledTypes.Contains((int) node.Tag);
  }

  public List<int> FilteredRelationTypes
  {
    get
    {
      List<int> intList = new List<int>();
      foreach (TreeNode node in this.treeView1.Nodes)
      {
        if (!node.Checked)
          intList.Add((int) node.Tag);
      }
      return intList.Count <= 0 ? (List<int>) null : intList;
    }
  }

  private void RelationTypesFilterForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void RelationTypesFilterForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void checkBox1_CheckedChanged(object sender, EventArgs e)
  {
    foreach (TreeNode node in this.treeView1.Nodes)
      node.Checked = this.checkBox1.Checked;
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
    this.treeView1 = new TreeView();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.checkBox1 = new CheckBox();
    this.SuspendLayout();
    this.treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.treeView1.CheckBoxes = true;
    this.treeView1.Location = new Point(12, 12);
    this.treeView1.Name = "treeView1";
    this.treeView1.Size = new Size(338, 206);
    this.treeView1.TabIndex = 0;
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(102, 234);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 4;
    this.bOK.Text = "ОК";
    this.bOK.UseVisualStyleBackColor = true;
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(229, 234);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.checkBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.checkBox1.AutoSize = true;
    this.checkBox1.Location = new Point(12, 240 /*0xF0*/);
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.Size = new Size(91, 17);
    this.checkBox1.TabIndex = 6;
    this.checkBox1.Text = "Выбрать все";
    this.checkBox1.UseVisualStyleBackColor = true;
    this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(362, 273);
    this.Controls.Add((Control) this.checkBox1);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.treeView1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(380, 239);
    this.Name = nameof (RelationTypesFilterForm);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Типы связей для поиска публикуемого состава";
    this.FormClosing += new FormClosingEventHandler(this.RelationTypesFilterForm_FormClosing);
    this.Load += new EventHandler(this.RelationTypesFilterForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
