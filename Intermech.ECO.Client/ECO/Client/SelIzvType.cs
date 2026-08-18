// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.SelIzvType
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class SelIzvType : Form
{
  private long ecoObjectID = -1;
  private int ecoTypeID = RevHelper.idObj_II;
  public RevType rt;
  private IContainer components;
  private Label label5;
  private Label label4;
  private Label label3;
  private RadioButton rbPR;
  private RadioButton rbPI;
  private RadioButton rbII;
  private ImageList imageList1;
  private Panel panel1;
  private Button button2;
  private Button btnOK;
  private Label label1;
  private RadioButton rbSN;

  public int EcoTypeID
  {
    get => this.ecoTypeID;
    set => this.ecoTypeID = value;
  }

  public long EcoObjectID
  {
    get => this.ecoObjectID;
    set => this.ecoObjectID = value;
  }

  public SelIzvType() => this.InitializeComponent();

  private void rbII_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbII.Checked)
    {
      this.rt = RevType.II;
      this.ecoTypeID = RevHelper.idObj_II;
    }
    if (this.rbPI.Checked)
    {
      this.rt = RevType.PI;
      this.ecoTypeID = RevHelper.idObj_PI;
    }
    if (this.rbPR.Checked)
    {
      this.rt = RevType.PR;
      this.ecoTypeID = RevHelper.idObj_PR;
    }
    if (!this.rbSN.Checked)
      return;
    this.rt = RevType.PI;
    this.ecoTypeID = RevHelper.idObj_SN;
  }

  private void SelIzvType_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void SelIzvType_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    IObjectCreatorService service = ECOPlugin.serviceProvider.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    ECOPlugin.ForceECOOpening = true;
    try
    {
      this.ecoObjectID = service.CreateObjectByTypeDialog(this.ecoTypeID);
      if (this.ecoObjectID != -1L)
        this.DialogResult = DialogResult.OK;
      else
        this.DialogResult = DialogResult.Cancel;
    }
    finally
    {
      ECOPlugin.ForceECOOpening = false;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelIzvType));
    this.label5 = new Label();
    this.imageList1 = new ImageList(this.components);
    this.label4 = new Label();
    this.label3 = new Label();
    this.rbPR = new RadioButton();
    this.rbPI = new RadioButton();
    this.rbII = new RadioButton();
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.btnOK = new Button();
    this.label1 = new Label();
    this.rbSN = new RadioButton();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.ImageList = this.imageList1;
    this.label5.Name = "label5";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "r1.bmp");
    this.imageList1.Images.SetKeyName(1, "r2.bmp");
    this.imageList1.Images.SetKeyName(2, "r3.bmp");
    this.imageList1.Images.SetKeyName(3, "служебные_записи.bmp");
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.ImageList = this.imageList1;
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.ImageList = this.imageList1;
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.rbPR, "rbPR");
    this.rbPR.Name = "rbPR";
    this.rbPR.Tag = (object) "2";
    this.rbPR.UseVisualStyleBackColor = true;
    this.rbPR.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbPI, "rbPI");
    this.rbPI.Name = "rbPI";
    this.rbPI.Tag = (object) "1";
    this.rbPI.UseVisualStyleBackColor = true;
    this.rbPI.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbII, "rbII");
    this.rbII.Checked = true;
    this.rbII.Name = "rbII";
    this.rbII.TabStop = true;
    this.rbII.Tag = (object) "0";
    this.rbII.UseVisualStyleBackColor = true;
    this.rbII.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.btnOK);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.Name = "btnOK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.ImageList = this.imageList1;
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.rbSN, "rbSN");
    this.rbSN.Name = "rbSN";
    this.rbSN.Tag = (object) "3";
    this.rbSN.UseVisualStyleBackColor = true;
    this.rbSN.CheckedChanged += new EventHandler(this.rbII_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.btnOK;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.button2;
    this.Controls.Add((Control) this.rbSN);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.label5);
    this.Controls.Add((Control) this.label4);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.rbPR);
    this.Controls.Add((Control) this.rbPI);
    this.Controls.Add((Control) this.rbII);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelIzvType);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.SelIzvType_FormClosing);
    this.Load += new EventHandler(this.SelIzvType_Load);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
