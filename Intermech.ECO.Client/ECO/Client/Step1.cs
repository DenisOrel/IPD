// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.Step1
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class Step1 : UserControl
{
  private RevType rt;
  public int objectTypeId = RevHelper.idObj_II;
  private int childObjType = -1;
  private IContainer components;
  private Label label4;
  private Label label3;
  private Label label5;
  private GroupBox gbKind;
  public RadioButton rbPR;
  public RadioButton rbPI;
  public RadioButton rbII;
  private Label hintLabel;
  private Panel panel1;
  private ImageList imageList1;
  private Panel hintPanel;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
  private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
  private Label label1;
  public RadioButton rbSN;
  public RadioButton rbChild;

  public Step1(RequireClass rc)
  {
    this.InitializeComponent();
    this.Dock = DockStyle.Fill;
    this.Visible = true;
    switch (rc)
    {
      case RequireClass.NoRequire:
        this.hintPanel.Visible = false;
        break;
      case RequireClass.Suggest:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("ECO.Client_141");
        this.Text = LocalizationHolder.rm.GetString("ECO.Client_142");
        break;
      case RequireClass.Require:
        this.hintLabel.Text = LocalizationHolder.rm.GetString("ECO.Client_139");
        this.Text = LocalizationHolder.rm.GetString("ECO.Client_140");
        break;
    }
  }

  public void SetOnlyObjType(int newObjectTypeID)
  {
    if (newObjectTypeID == RevHelper.idObj_II)
    {
      this.rbII.Checked = true;
      this.rbPI.Enabled = false;
      this.rbPR.Enabled = false;
      this.rbSN.Enabled = false;
    }
    if (newObjectTypeID == RevHelper.idObj_PI)
    {
      this.rbPI.Checked = true;
      this.rbII.Enabled = false;
      this.rbPR.Enabled = false;
      this.rbSN.Enabled = false;
    }
    if (newObjectTypeID == RevHelper.idObj_PR)
    {
      this.rbPR.Checked = true;
      this.rbPI.Enabled = false;
      this.rbII.Enabled = false;
      this.rbSN.Enabled = false;
    }
    if (newObjectTypeID == RevHelper.idObj_IPV)
    {
      this.rbII.Checked = true;
      this.rbII.Text = "ИПВ";
      this.objectTypeId = RevHelper.idObj_IPV;
      this.rbPI.Enabled = false;
      this.rbPR.Enabled = false;
      this.rbSN.Enabled = false;
    }
    if (newObjectTypeID != RevHelper.idObj_SN)
      return;
    this.rbII.Checked = false;
    this.objectTypeId = RevHelper.idObj_SN;
    this.rbPI.Enabled = false;
    this.rbPR.Enabled = false;
    this.rbSN.Enabled = true;
  }

  public void SetObjTypes(List<int> objTypes)
  {
    this.rbII.Enabled = objTypes.Contains(RevHelper.idObj_II);
    this.rbPI.Enabled = objTypes.Contains(RevHelper.idObj_PI);
    this.rbPR.Enabled = objTypes.Contains(RevHelper.idObj_PR);
    this.rbSN.Enabled = objTypes.Contains(RevHelper.idObj_PI);
    if (this.rbII.Enabled)
      this.rbII.Checked = true;
    else if (this.rbPI.Enabled)
      this.rbPI.Checked = true;
    else
      this.rbPR.Checked = true;
  }

  private void rbII_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.rbII.Checked)
      return;
    this.objectTypeId = RevHelper.idObj_II;
  }

  private void rbPI_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.rbPI.Checked)
      return;
    this.objectTypeId = RevHelper.idObj_PI;
  }

  private void rbPR_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.rbPR.Checked)
      return;
    this.objectTypeId = RevHelper.idObj_PR;
  }

  private void rbSN_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.rbSN.Checked)
      return;
    this.objectTypeId = RevHelper.idObj_SN;
  }

  public void SetChildType(int childTypeId, string childTypeName)
  {
    this.childObjType = childTypeId;
    this.rbChild.Text = childTypeName;
    this.rbChild.Visible = true;
    this.rbChild.Checked = true;
  }

  private void rbChild_CheckedChanged(object sender, EventArgs e)
  {
    if (!((RadioButton) sender).Checked)
      return;
    this.objectTypeId = this.childObjType;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Step1));
    this.label4 = new Label();
    this.imageList1 = new ImageList(this.components);
    this.label3 = new Label();
    this.label5 = new Label();
    this.gbKind = new GroupBox();
    this.rbSN = new RadioButton();
    this.label1 = new Label();
    this.rbPR = new RadioButton();
    this.rbPI = new RadioButton();
    this.rbII = new RadioButton();
    this.hintLabel = new Label();
    this.panel1 = new Panel();
    this.hintPanel = new Panel();
    this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
    this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
    this.rbChild = new RadioButton();
    this.gbKind.SuspendLayout();
    this.panel1.SuspendLayout();
    this.hintPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.ImageList = this.imageList1;
    this.label4.Name = "label4";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "r1.bmp");
    this.imageList1.Images.SetKeyName(1, "r2.bmp");
    this.imageList1.Images.SetKeyName(2, "r3.bmp");
    this.imageList1.Images.SetKeyName(3, "служебные_записи.bmp");
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.ImageList = this.imageList1;
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.ImageList = this.imageList1;
    this.label5.Name = "label5";
    this.gbKind.Controls.Add((Control) this.rbChild);
    this.gbKind.Controls.Add((Control) this.rbSN);
    this.gbKind.Controls.Add((Control) this.label1);
    this.gbKind.Controls.Add((Control) this.label5);
    this.gbKind.Controls.Add((Control) this.label4);
    this.gbKind.Controls.Add((Control) this.label3);
    this.gbKind.Controls.Add((Control) this.rbPR);
    this.gbKind.Controls.Add((Control) this.rbPI);
    this.gbKind.Controls.Add((Control) this.rbII);
    componentResourceManager.ApplyResources((object) this.gbKind, "gbKind");
    this.gbKind.Name = "gbKind";
    this.gbKind.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbSN, "rbSN");
    this.rbSN.Name = "rbSN";
    this.rbSN.Tag = (object) "3";
    this.rbSN.UseVisualStyleBackColor = true;
    this.rbSN.CheckedChanged += new EventHandler(this.rbSN_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ImageList = this.imageList1;
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.rbPR, "rbPR");
    this.rbPR.Name = "rbPR";
    this.rbPR.Tag = (object) "2";
    this.rbPR.UseVisualStyleBackColor = true;
    this.rbPR.CheckedChanged += new EventHandler(this.rbPR_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbPI, "rbPI");
    this.rbPI.Name = "rbPI";
    this.rbPI.Tag = (object) "1";
    this.rbPI.UseVisualStyleBackColor = true;
    this.rbPI.CheckedChanged += new EventHandler(this.rbPI_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbII, "rbII");
    this.rbII.Checked = true;
    this.rbII.Name = "rbII";
    this.rbII.TabStop = true;
    this.rbII.Tag = (object) "0";
    this.rbII.UseVisualStyleBackColor = true;
    this.rbII.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.hintLabel, "hintLabel");
    this.hintLabel.ForeColor = Color.Purple;
    this.hintLabel.Name = "hintLabel";
    this.panel1.Controls.Add((Control) this.gbKind);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.hintPanel.Controls.Add((Control) this.hintLabel);
    componentResourceManager.ApplyResources((object) this.hintPanel, "hintPanel");
    this.hintPanel.Name = "hintPanel";
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.ReadOnly = true;
    componentResourceManager.ApplyResources((object) this.rbChild, "rbChild");
    this.rbChild.Name = "rbChild";
    this.rbChild.Tag = (object) "3";
    this.rbChild.UseVisualStyleBackColor = true;
    this.rbChild.CheckedChanged += new EventHandler(this.rbChild_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.hintPanel);
    this.Name = nameof (Step1);
    this.gbKind.ResumeLayout(false);
    this.gbKind.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.hintPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
