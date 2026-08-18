// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.VyborShablona
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class VyborShablona : Form
{
  public IMSObjectType _imsObjectTypeCurr;
  public QuickObjectInfo _quickObjectInfo_Result;
  private int i_Vydelit = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelForButtons;
  internal Button bCancel;
  internal Button bOK;
  private ToolTip toolTip1;
  private ListBox listBoxInfo;

  public VyborShablona() => this.InitializeComponent();

  private void VyborShablona_Load(object sender, EventArgs e)
  {
    for (int index = 0; index < Vedomost_VB_Static.List_TemplateVed_QuickObjectInfo.Count; ++index)
      this.listBoxInfo.Items.Add((object) Vedomost_VB_Static.List_TemplateVed_QuickObjectInfo[index].Caption);
    if (Vedomost_VB_Static.List_TemplateVed_QuickObjectInfo.Count <= 0)
      return;
    this.listBoxInfo.SelectedIndex = 0;
  }

  private void bOK_Click(object sender, EventArgs e)
  {
    if (this.listBoxInfo.Items.Count <= 0)
      return;
    int selectedIndex = this.listBoxInfo.SelectedIndex;
    this._quickObjectInfo_Result = Vedomost_VB_Static.List_TemplateVed_QuickObjectInfo[selectedIndex];
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
    this.panelForButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.listBoxInfo = new ListBox();
    this.panelForButtons.SuspendLayout();
    this.SuspendLayout();
    this.panelForButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelForButtons.Controls.Add((Control) this.bCancel);
    this.panelForButtons.Controls.Add((Control) this.bOK);
    this.panelForButtons.Dock = DockStyle.Bottom;
    this.panelForButtons.Location = new Point(0, 344);
    this.panelForButtons.Name = "panelForButtons";
    this.panelForButtons.Size = new Size(524, 42);
    this.panelForButtons.TabIndex = 13;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(395, 5);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(264, 5);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.listBoxInfo.Dock = DockStyle.Fill;
    this.listBoxInfo.FormattingEnabled = true;
    this.listBoxInfo.Location = new Point(0, 0);
    this.listBoxInfo.Name = "listBoxInfo";
    this.listBoxInfo.Size = new Size(524, 344);
    this.listBoxInfo.TabIndex = 14;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(524, 386);
    this.Controls.Add((Control) this.listBoxInfo);
    this.Controls.Add((Control) this.panelForButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (VyborShablona);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор шаблона для групповой формы Б";
    this.Load += new EventHandler(this.VyborShablona_Load);
    this.panelForButtons.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
