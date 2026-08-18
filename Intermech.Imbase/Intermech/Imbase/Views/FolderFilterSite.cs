// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.FolderFilterSite
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Docking;
using Intermech.Imbase.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

internal class FolderFilterSite : DockControl
{
  private Button button2;
  private TextBox textBox1;
  private TextBox textBox2;
  private Button button1;
  private Button button3;
  private System.Windows.Forms.TabControl tabControl1;
  private System.Windows.Forms.TabPage tabPage1;
  private System.Windows.Forms.TabPage tabPage2;
  private Button button4;
  private TextBox textBox3;
  private TreeView treeView1;
  private TreeBuilder treeBuilder;
  private IContainer components;
  private OwnerGuidTune ownerTune;
  private OwnerGuidSelect ownerCheck;
  private Label label1;
  private FolderFilterTune tuner;

  internal FolderFilterSite() => this.InitializeComponent();

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FolderFilterSite));
    this.button2 = new Button();
    this.textBox1 = new TextBox();
    this.textBox2 = new TextBox();
    this.button1 = new Button();
    this.button3 = new Button();
    this.tuner = new FolderFilterTune();
    this.tabControl1 = new System.Windows.Forms.TabControl();
    this.tabPage1 = new System.Windows.Forms.TabPage();
    this.tabPage2 = new System.Windows.Forms.TabPage();
    this.label1 = new Label();
    this.ownerTune = new OwnerGuidTune();
    this.ownerCheck = new OwnerGuidSelect();
    this.treeView1 = new TreeView();
    this.button4 = new Button();
    this.textBox3 = new TextBox();
    this.treeBuilder = new TreeBuilder(this.components);
    this.tabControl1.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    componentResourceManager.ApplyResources((object) this.textBox1, "textBox1");
    this.textBox1.Name = "textBox1";
    componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
    this.textBox2.Name = "textBox2";
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tuner, "tuner");
    this.tuner.Dirty = false;
    this.tuner.MasterCatalog = -1L;
    this.tuner.Name = "tuner";
    this.tuner.OwnerGuid = "";
    this.tuner.ReadOnly = false;
    this.tuner.SlaveCatalog = -1L;
    this.tuner.DirtyChanged += new EventHandler(this.tuner_DirtyChanged);
    this.tuner.MasterNodeChanged += new EventHandler(this.Tuner_MasterNodeChanged);
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Controls.Add((Control) this.tabPage1);
    this.tabControl1.Controls.Add((Control) this.tabPage2);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Controls.Add((Control) this.button2);
    this.tabPage1.Controls.Add((Control) this.tuner);
    this.tabPage1.Controls.Add((Control) this.button3);
    this.tabPage1.Controls.Add((Control) this.textBox1);
    this.tabPage1.Controls.Add((Control) this.textBox2);
    this.tabPage1.Controls.Add((Control) this.button1);
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.tabPage2, "tabPage2");
    this.tabPage2.Controls.Add((Control) this.label1);
    this.tabPage2.Controls.Add((Control) this.ownerTune);
    this.tabPage2.Controls.Add((Control) this.ownerCheck);
    this.tabPage2.Controls.Add((Control) this.treeView1);
    this.tabPage2.Controls.Add((Control) this.button4);
    this.tabPage2.Controls.Add((Control) this.textBox3);
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.ownerTune, "ownerTune");
    this.ownerTune.Caption = "Тип фильтра";
    this.ownerTune.Name = "ownerTune";
    this.ownerTune.OwnerChanged += new EventHandler(this.ownerTune_OwnerChanged);
    componentResourceManager.ApplyResources((object) this.ownerCheck, "ownerCheck");
    this.ownerCheck.Caption = "Тип фильтра";
    this.ownerCheck.Name = "ownerCheck";
    this.ownerCheck.OwnerChanged += new EventHandler(this.ownerCheck_OwnerChanged);
    componentResourceManager.ApplyResources((object) this.treeView1, "treeView1");
    this.treeView1.Name = "treeView1";
    this.treeView1.Sorted = true;
    componentResourceManager.ApplyResources((object) this.button4, "button4");
    this.button4.Name = "button4";
    this.button4.UseVisualStyleBackColor = true;
    this.button4.Click += new EventHandler(this.button4_Click);
    componentResourceManager.ApplyResources((object) this.textBox3, "textBox3");
    this.textBox3.Name = "textBox3";
    this.treeBuilder.Catalogs = new long[0];
    this.treeBuilder.Checked = new long[0];
    this.treeBuilder.TreeView = this.treeView1;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AllowedStates = DockLocation.Document;
    this.BackColor = SystemColors.Control;
    this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
    this.Controls.Add((Control) this.tabControl1);
    this.Name = nameof (FolderFilterSite);
    this.PersistState = false;
    this.ShowHint = DockState.Document;
    this.tabControl1.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.tabPage1.PerformLayout();
    this.tabPage2.ResumeLayout(false);
    this.tabPage2.PerformLayout();
    this.ResumeLayout(false);
  }

  private void button2_Click(object sender, EventArgs e)
  {
    this.tuner.MasterCatalog = Convert.ToInt64(this.textBox1.Text);
  }

  private void button1_Click(object sender, EventArgs e)
  {
    this.tuner.SlaveCatalog = Convert.ToInt64(this.textBox2.Text);
  }

  private void tuner_DirtyChanged(object sender, EventArgs e)
  {
    this.button3.Enabled = this.tuner.Dirty;
  }

  private void button4_Click(object sender, EventArgs e)
  {
    this.treeBuilder.CreateFilterTree(long.Parse(this.textBox3.Text), (string) null, long.Parse(this.textBox2.Text));
  }

  private void Tuner_MasterNodeChanged(object sender, EventArgs e)
  {
    if (!(sender is TreeNode treeNode) || !(treeNode.Tag is NodeInfo tag))
      return;
    this.textBox3.Text = tag._objectId.ToString();
  }

  private void tuner_MasterNodeChanged(object sender, EventArgs e)
  {
  }

  private void ownerCheck_OwnerChanged(object sender, EventArgs e)
  {
    this.label1.Text = this.ownerCheck.OwnerGuid;
  }

  private void ownerTune_OwnerChanged(object sender, EventArgs e)
  {
    this.label1.Text = this.ownerTune.OwnerGuid;
  }
}
