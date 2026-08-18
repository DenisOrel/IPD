// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.SetupTextDirrectionDlg
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using MWCommon;
using MWControls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>SetupTextDirrectionDlg</summary>
public class SetupTextDirrectionDlg : Form
{
  private TextOrientation? _textOrientation = new TextOrientation?(TextOrientation.Normal);
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button _BtnCancel;
  private Button _BtnOK;
  private GroupBox groupBoxOrientation;
  private MWLabel _labelRight;
  private MWLabel _labelBottom;
  private MWLabel _labelLeft;
  private MWLabel _labelTop;
  private Panel _panelTopSelected;
  private Panel _panelBottomSelected;
  private Panel _panelLeftSelected;
  private Panel _panelRightSelected;

  /// <summary>Конструктор</summary>
  /// <param name="textOrientation"></param>
  public SetupTextDirrectionDlg(TextOrientation? textOrientation)
  {
    this.InitializeComponent();
    this.SelectedTextOrientation = textOrientation;
  }

  /// <summary>SelectedTextOrientation</summary>
  public TextOrientation? SelectedTextOrientation
  {
    [DebuggerStepThrough] get => this._textOrientation;
    set
    {
      TextOrientation? textOrientation1 = this._textOrientation;
      TextOrientation? nullable = value;
      if (textOrientation1.GetValueOrDefault() == nullable.GetValueOrDefault() & textOrientation1.HasValue == nullable.HasValue)
        return;
      this._textOrientation = value;
      if (!this._textOrientation.HasValue)
      {
        this._panelTopSelected.Visible = false;
        this._panelLeftSelected.Visible = false;
        this._panelRightSelected.Visible = false;
        this._panelBottomSelected.Visible = false;
        this._labelTop.Cursor = Cursors.Hand;
        this._labelLeft.Cursor = Cursors.Hand;
        this._labelRight.Cursor = Cursors.Hand;
        this._labelBottom.Cursor = Cursors.Hand;
        this._BtnOK.Enabled = false;
        this._BtnCancel.Focus();
      }
      TextOrientation? textOrientation2 = this._textOrientation;
      if (!textOrientation2.HasValue)
        return;
      switch (textOrientation2.GetValueOrDefault())
      {
        case TextOrientation.Normal:
          this._panelTopSelected.Visible = true;
          this._panelLeftSelected.Visible = false;
          this._panelRightSelected.Visible = false;
          this._panelBottomSelected.Visible = false;
          this._labelTop.Cursor = Cursors.Arrow;
          this._labelLeft.Cursor = Cursors.Hand;
          this._labelRight.Cursor = Cursors.Hand;
          this._labelBottom.Cursor = Cursors.Hand;
          this._BtnOK.Enabled = true;
          this._BtnOK.Focus();
          break;
        case TextOrientation.DownTop:
          this._panelTopSelected.Visible = false;
          this._panelLeftSelected.Visible = true;
          this._panelRightSelected.Visible = false;
          this._panelBottomSelected.Visible = false;
          this._labelTop.Cursor = Cursors.Hand;
          this._labelLeft.Cursor = Cursors.Arrow;
          this._labelRight.Cursor = Cursors.Hand;
          this._labelBottom.Cursor = Cursors.Hand;
          this._BtnOK.Enabled = true;
          this._BtnOK.Focus();
          break;
        case TextOrientation.UpsideDown:
          this._panelTopSelected.Visible = false;
          this._panelLeftSelected.Visible = false;
          this._panelRightSelected.Visible = false;
          this._panelBottomSelected.Visible = true;
          this._labelTop.Cursor = Cursors.Hand;
          this._labelLeft.Cursor = Cursors.Hand;
          this._labelRight.Cursor = Cursors.Hand;
          this._labelBottom.Cursor = Cursors.Arrow;
          this._BtnOK.Enabled = true;
          this._BtnOK.Focus();
          break;
        case TextOrientation.TopDown:
          this._panelTopSelected.Visible = false;
          this._panelLeftSelected.Visible = false;
          this._panelRightSelected.Visible = true;
          this._panelBottomSelected.Visible = false;
          this._labelTop.Cursor = Cursors.Hand;
          this._labelLeft.Cursor = Cursors.Hand;
          this._labelRight.Cursor = Cursors.Arrow;
          this._labelBottom.Cursor = Cursors.Hand;
          this._BtnOK.Enabled = true;
          this._BtnOK.Focus();
          break;
      }
    }
  }

  private void _labelTop_Click(object sender, EventArgs e)
  {
    if (sender == this._labelTop)
      this.SelectedTextOrientation = new TextOrientation?(TextOrientation.Normal);
    else if (sender == this._labelLeft)
      this.SelectedTextOrientation = new TextOrientation?(TextOrientation.DownTop);
    else if (sender == this._labelRight)
    {
      this.SelectedTextOrientation = new TextOrientation?(TextOrientation.TopDown);
    }
    else
    {
      if (sender != this._labelBottom)
        return;
      this.SelectedTextOrientation = new TextOrientation?(TextOrientation.UpsideDown);
    }
  }

  /// <summary>ProcessCmdKey</summary>
  /// <param name="msg"></param>
  /// <param name="keyData"></param>
  /// <returns></returns>
  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Left:
        this.SelectedTextOrientation = new TextOrientation?(TextOrientation.DownTop);
        return true;
      case Keys.Up:
        this.SelectedTextOrientation = new TextOrientation?(TextOrientation.Normal);
        return true;
      case Keys.Right:
        this.SelectedTextOrientation = new TextOrientation?(TextOrientation.TopDown);
        return true;
      case Keys.Down:
        this.SelectedTextOrientation = new TextOrientation?(TextOrientation.UpsideDown);
        return true;
      default:
        return base.ProcessCmdKey(ref msg, keyData);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SetupTextDirrectionDlg));
    this._BtnCancel = new Button();
    this._BtnOK = new Button();
    this.groupBoxOrientation = new GroupBox();
    this._labelRight = new MWLabel();
    this._labelBottom = new MWLabel();
    this._labelLeft = new MWLabel();
    this._labelTop = new MWLabel();
    this._panelTopSelected = new Panel();
    this._panelBottomSelected = new Panel();
    this._panelLeftSelected = new Panel();
    this._panelRightSelected = new Panel();
    this.groupBoxOrientation.SuspendLayout();
    this.SuspendLayout();
    this._BtnCancel.AccessibleDescription = (string) null;
    this._BtnCancel.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._BtnCancel, "_BtnCancel");
    this._BtnCancel.BackgroundImage = (Image) null;
    this._BtnCancel.DialogResult = DialogResult.Cancel;
    this._BtnCancel.Font = (Font) null;
    this._BtnCancel.Name = "_BtnCancel";
    this._BtnOK.AccessibleDescription = (string) null;
    this._BtnOK.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._BtnOK, "_BtnOK");
    this._BtnOK.BackgroundImage = (Image) null;
    this._BtnOK.DialogResult = DialogResult.OK;
    this._BtnOK.Font = (Font) null;
    this._BtnOK.Name = "_BtnOK";
    this.groupBoxOrientation.AccessibleDescription = (string) null;
    this.groupBoxOrientation.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.groupBoxOrientation, "groupBoxOrientation");
    this.groupBoxOrientation.BackgroundImage = (Image) null;
    this.groupBoxOrientation.Controls.Add((Control) this._labelRight);
    this.groupBoxOrientation.Controls.Add((Control) this._labelBottom);
    this.groupBoxOrientation.Controls.Add((Control) this._labelLeft);
    this.groupBoxOrientation.Controls.Add((Control) this._labelTop);
    this.groupBoxOrientation.Controls.Add((Control) this._panelTopSelected);
    this.groupBoxOrientation.Controls.Add((Control) this._panelBottomSelected);
    this.groupBoxOrientation.Controls.Add((Control) this._panelLeftSelected);
    this.groupBoxOrientation.Controls.Add((Control) this._panelRightSelected);
    this.groupBoxOrientation.FlatStyle = FlatStyle.System;
    this.groupBoxOrientation.Font = (Font) null;
    this.groupBoxOrientation.Name = "groupBoxOrientation";
    this.groupBoxOrientation.TabStop = false;
    this._labelRight.AccessibleDescription = (string) null;
    this._labelRight.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._labelRight, "_labelRight");
    this._labelRight.BackColor = Color.White;
    this._labelRight.BorderStyle = BorderStyle.FixedSingle;
    this._labelRight.Cursor = Cursors.Hand;
    this._labelRight.Font = (Font) null;
    this._labelRight.Name = "_labelRight";
    this._labelRight.StringFrmt = StringFormatEnum.GenericDefault;
    this._labelRight.TextDir = TextDir.Right;
    this._labelRight.Click += new EventHandler(this._labelTop_Click);
    this._labelBottom.AccessibleDescription = (string) null;
    this._labelBottom.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._labelBottom, "_labelBottom");
    this._labelBottom.BackColor = Color.White;
    this._labelBottom.BorderStyle = BorderStyle.FixedSingle;
    this._labelBottom.Cursor = Cursors.Hand;
    this._labelBottom.Font = (Font) null;
    this._labelBottom.Name = "_labelBottom";
    this._labelBottom.StringFrmt = StringFormatEnum.GenericDefault;
    this._labelBottom.TextDir = TextDir.UpsideDown;
    this._labelBottom.Click += new EventHandler(this._labelTop_Click);
    this._labelLeft.AccessibleDescription = (string) null;
    this._labelLeft.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._labelLeft, "_labelLeft");
    this._labelLeft.BackColor = Color.White;
    this._labelLeft.BorderStyle = BorderStyle.FixedSingle;
    this._labelLeft.Cursor = Cursors.Hand;
    this._labelLeft.Font = (Font) null;
    this._labelLeft.Name = "_labelLeft";
    this._labelLeft.StringFrmt = StringFormatEnum.GenericDefault;
    this._labelLeft.TextDir = TextDir.Left;
    this._labelLeft.Click += new EventHandler(this._labelTop_Click);
    this._labelTop.AccessibleDescription = (string) null;
    this._labelTop.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._labelTop, "_labelTop");
    this._labelTop.BackColor = Color.White;
    this._labelTop.BorderStyle = BorderStyle.FixedSingle;
    this._labelTop.Font = (Font) null;
    this._labelTop.Name = "_labelTop";
    this._labelTop.StringFrmt = StringFormatEnum.GenericDefault;
    this._labelTop.Click += new EventHandler(this._labelTop_Click);
    this._panelTopSelected.AccessibleDescription = (string) null;
    this._panelTopSelected.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._panelTopSelected, "_panelTopSelected");
    this._panelTopSelected.BackColor = Color.Black;
    this._panelTopSelected.BackgroundImage = (Image) null;
    this._panelTopSelected.Font = (Font) null;
    this._panelTopSelected.Name = "_panelTopSelected";
    this._panelBottomSelected.AccessibleDescription = (string) null;
    this._panelBottomSelected.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._panelBottomSelected, "_panelBottomSelected");
    this._panelBottomSelected.BackColor = Color.Black;
    this._panelBottomSelected.BackgroundImage = (Image) null;
    this._panelBottomSelected.Font = (Font) null;
    this._panelBottomSelected.Name = "_panelBottomSelected";
    this._panelLeftSelected.AccessibleDescription = (string) null;
    this._panelLeftSelected.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this._panelLeftSelected, "_panelLeftSelected");
    this._panelLeftSelected.BackColor = Color.Black;
    this._panelLeftSelected.BackgroundImage = (Image) null;
    this._panelLeftSelected.Font = (Font) null;
    this._panelLeftSelected.Name = "_panelLeftSelected";
    this._panelRightSelected.AccessibleDescription = (string) null;
    this._panelRightSelected.AccessibleName = (string) null;
    this._panelRightSelected.AccessibleRole = AccessibleRole.None;
    componentResourceManager.ApplyResources((object) this._panelRightSelected, "_panelRightSelected");
    this._panelRightSelected.BackColor = Color.Black;
    this._panelRightSelected.BackgroundImage = (Image) null;
    this._panelRightSelected.Font = (Font) null;
    this._panelRightSelected.Name = "_panelRightSelected";
    this.AcceptButton = (IButtonControl) this._BtnOK;
    this.AccessibleDescription = (string) null;
    this.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.CancelButton = (IButtonControl) this._BtnCancel;
    this.Controls.Add((Control) this.groupBoxOrientation);
    this.Controls.Add((Control) this._BtnCancel);
    this.Controls.Add((Control) this._BtnOK);
    this.Font = (Font) null;
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Icon = (Icon) null;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SetupTextDirrectionDlg);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Tag = (object) "";
    this.groupBoxOrientation.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
