// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.DwgCreator.SelectObject
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Cadmech.Integrator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Cadmech.Integrator.DwgCreator;

internal class SelectObject : Form
{
  private IContainer components;
  private Panel panel1;
  private Panel panel2;
  private ListView listView1;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private Button bCancel;
  private Button bOK;
  private PictureBox pictureBox1;
  private Label label2;
  private Label label1;
  private Label label3;
  private ColumnHeader columnHeader3;

  public SelectObject() => this.InitializeComponent();

  public long[] SelectedObjectIDs
  {
    get
    {
      List<long> longList = new List<long>(1);
      if (this.listView1.SelectedItems != null && this.listView1.SelectedItems.Count > 0)
      {
        foreach (ListViewItem selectedItem in this.listView1.SelectedItems)
          longList.Add((long) selectedItem.Tag);
      }
      return longList.Count > 0 ? longList.ToArray() : (long[]) null;
    }
  }

  public void SetData(string str1, string str2, string str3, ListViewItem[] items)
  {
    this.label1.Text = str1;
    this.label2.Text = str2;
    this.label3.Text = str3;
    this.listView1.BeginUpdate();
    this.listView1.Items.Clear();
    this.listView1.Items.AddRange(items);
    this.listView1.EndUpdate();
    this.bOK.Enabled = false;
  }

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.bOK.Enabled = this.listView1.SelectedItems != null && this.listView1.SelectedItems.Count > 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectObject));
    this.panel1 = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.panel2 = new Panel();
    this.label3 = new Label();
    this.label2 = new Label();
    this.label1 = new Label();
    this.pictureBox1 = new PictureBox();
    this.listView1 = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    ((ISupportInitialize) this.pictureBox1).BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bCancel);
    this.panel1.Controls.Add((Control) this.bOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOK, "bOK");
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Name = "bOK";
    this.bOK.UseVisualStyleBackColor = true;
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Controls.Add((Control) this.label2);
    this.panel2.Controls.Add((Control) this.label1);
    this.panel2.Controls.Add((Control) this.pictureBox1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.pictureBox1.Image = (Image) Resources.IR_Info_48x48;
    componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
    this.pictureBox1.Name = "pictureBox1";
    this.pictureBox1.TabStop = false;
    this.listView1.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3
    });
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.FullRowSelect = true;
    this.listView1.GridLines = true;
    this.listView1.Name = "listView1";
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.Details;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    this.AcceptButton = (IButtonControl) this.bOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.Controls.Add((Control) this.listView1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectObject);
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    ((ISupportInitialize) this.pictureBox1).EndInit();
    this.ResumeLayout(false);
  }
}
