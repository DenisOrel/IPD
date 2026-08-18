
// Type: Intermech.Navigator.Selections.Implementation.DeleteCommandQuestion
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.Selections.Implementation;

public class DeleteCommandQuestion : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Panel panel2;
  private Button bCancel;
  private Button bNo;
  private Button bYes;
  private Panel panel3;
  private PictureBox pictureBox1;
  private TreeView treeView;
  private Label label1;

  public DeleteCommandQuestion() => this.InitializeComponent();

  internal DialogResult ShowQuestion(List<DeletedSelection> selection)
  {
    ICategoryTypeIconService service = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    if (service != null)
    {
      this.treeView.StateImageList = service.ImageList;
      this.treeView.ImageList = service.ImageList;
    }
    this.label1.Text = selection.Count > 1 ? LocalizationHolder.rm.GetString("Client.Core_680") : LocalizationHolder.rm.GetString("Client.Core_681");
    foreach (DeletedSelection deletedSelection in selection)
    {
      TreeNode node = new TreeNode(deletedSelection.SelectionName, deletedSelection.Icon, deletedSelection.Icon);
      if (deletedSelection.ObjectTypes.Count > 1)
      {
        TreeNode treeNode = node.Nodes.Add(LocalizationHolder.rm.GetString("Client.Core_682"));
        foreach (AttachedObject objectType in deletedSelection.ObjectTypes)
          treeNode.Nodes.Add(new TreeNode(objectType.Name, objectType.Icon, objectType.Icon));
      }
      if (deletedSelection.ParentSelections.Count > 1)
      {
        TreeNode treeNode = node.Nodes.Add(LocalizationHolder.rm.GetString("Client.Core_683"));
        foreach (AttachedObject parentSelection in deletedSelection.ParentSelections)
          treeNode.Nodes.Add(new TreeNode(parentSelection.Name, parentSelection.Icon, parentSelection.Icon));
      }
      this.treeView.Nodes.Add(node);
      node.ExpandAll();
    }
    return this.ShowDialog();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeleteCommandQuestion));
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bNo = new Button();
    this.bYes = new Button();
    this.panel2 = new Panel();
    this.treeView = new TreeView();
    this.panel3 = new Panel();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bNo);
    this.panel1.Controls.Add((Control) this.bYes);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bNo, "bNo");
    this.bNo.DialogResult = DialogResult.No;
    this.bNo.Name = "bNo";
    this.bNo.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bYes, "bYes");
    this.bYes.DialogResult = DialogResult.Yes;
    this.bYes.Name = "bYes";
    this.bYes.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.treeView);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.treeView, "treeView");
    this.treeView.Name = "treeView";
    this.panel3.Controls.Add((Control) this.label1);
    this.panel3.Controls.Add((Control) this.pictureBox1);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.pictureBox1.Image = (Image) Intermech.Client.Core.Properties.Resources.button_info;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.AcceptButton = (IButtonControl) this.bYes;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (DeleteCommandQuestion);
    this.ShowInTaskbar = false;
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
  }
}
