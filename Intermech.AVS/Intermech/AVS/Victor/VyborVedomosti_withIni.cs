// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.VyborVedomosti_withIni
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class VyborVedomosti_withIni : Form
{
  public Vedomost_VB.TypeVed typeVed_result;
  public List<Vedomost_VB_Static.TypeVedOrTabl_Systems> List_Type_Systems;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  private ListBox listBoxVedomostName;
  private ToolTip toolTip1;
  private ImageList imageList1;
  private ImageList imagesToolbars;

  public VyborVedomosti_withIni() => this.InitializeComponent();

  private void VyborVedomosti_withIni_Load(object sender, EventArgs e)
  {
    if (this.List_Type_Systems == null || this.List_Type_Systems.Count < 1)
      this.Close();
    for (int index = 0; index < this.List_Type_Systems.Count; ++index)
      this.listBoxVedomostName.Items.Add((object) this.List_Type_Systems[index].name);
    if (this.List_Type_Systems.Count <= 0)
      return;
    this.listBoxVedomostName.SelectedIndex = 0;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    if (this.listBoxVedomostName.Items.Count <= 0)
      return;
    this.typeVed_result = this.List_Type_Systems[this.listBoxVedomostName.SelectedIndex].typeVed;
  }

  private void listBoxVedomostName_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    this.bOK_Click(sender, (EventArgs) e);
    this.DialogResult = DialogResult.OK;
    this.Close();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VyborVedomosti_withIni));
    this.panelForButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.listBoxVedomostName = new ListBox();
    this.toolTip1 = new ToolTip(this.components);
    this.imageList1 = new ImageList(this.components);
    this.imagesToolbars = new ImageList(this.components);
    this.panelForButtons.SuspendLayout();
    this.SuspendLayout();
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 342);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(522, 42);
    this.panelForButtons.TabIndex = 12;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(393, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(262, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.listBoxVedomostName.Dock = DockStyle.Fill;
    this.listBoxVedomostName.Font = new Font("Microsoft Sans Serif", 10f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.listBoxVedomostName.FormattingEnabled = true;
    this.listBoxVedomostName.ItemHeight = 16 /*0x10*/;
    this.listBoxVedomostName.Location = new Point(0, 0);
    this.listBoxVedomostName.Name = "listBoxVedomostName";
    this.listBoxVedomostName.Size = new Size(522, 342);
    this.listBoxVedomostName.TabIndex = 13;
    this.listBoxVedomostName.MouseDoubleClick += new MouseEventHandler(this.listBoxVedomostName_MouseDoubleClick);
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "Not.ico");
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "");
    this.imagesToolbars.Images.SetKeyName(2, "");
    this.imagesToolbars.Images.SetKeyName(3, "");
    this.imagesToolbars.Images.SetKeyName(4, "");
    this.imagesToolbars.Images.SetKeyName(5, "");
    this.imagesToolbars.Images.SetKeyName(6, "");
    this.imagesToolbars.Images.SetKeyName(7, "Связь.ico");
    this.imagesToolbars.Images.SetKeyName(8, "object_16x16.ico");
    this.imagesToolbars.Images.SetKeyName(9, "WithoutDrawing.ico");
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(522, 384);
    this.Controls.Add((Control) this.listBoxVedomostName);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "VyborVedomosti_SystemswithIni";
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Ведомости, заложенные в программе";
    this.Load += new EventHandler(this.VyborVedomosti_withIni_Load);
    this.panelForButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
