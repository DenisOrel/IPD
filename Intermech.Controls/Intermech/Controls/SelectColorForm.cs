
// Type: Intermech.Controls.SelectColorForm
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class SelectColorForm : 
  Form,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Bevel _bevelDialogButtons;
  protected Panel _pnlDialogButtons;
  protected Panel _panelBtns;
  protected Button _cancelButton;
  private ColorSelectionUserControl _colorSelectionUserControl;
  private Panel _panelTansparentButton;
  private ColorButton _btnTransparentBrush;

  public SelectColorForm()
    : this(Color.Transparent)
  {
  }

  public SelectColorForm(Color color)
  {
    this.InitializeComponent();
    this._colorSelectionUserControl.Color = color;
    this._btnTransparentBrush.ForceDown = color == Color.Transparent || color == Color.Empty;
  }

  private void _colorSelectionUserControl_ColorWasSelected(Color color)
  {
    this.DialogResult = DialogResult.OK;
    this.Close();
    this.DialogResult = DialogResult.OK;
  }

  private void _btnTransparentBrush_Click(object sender, EventArgs e)
  {
    this._colorSelectionUserControl.Color = Color.Transparent;
    this._colorSelectionUserControl_ColorWasSelected(Color.Transparent);
  }

  public Color Color
  {
    get => this._colorSelectionUserControl.Color;
    set => this._colorSelectionUserControl.Color = value;
  }

  private void _colorSelectionUserControl_Resize(object sender, EventArgs e)
  {
    this.ClientSize = new Size(this.ClientSize.Width, this._panelTansparentButton.Height + this._colorSelectionUserControl.Height + this._pnlDialogButtons.Height);
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
    this._bevelDialogButtons = new Bevel();
    this._pnlDialogButtons = new Panel();
    this._panelBtns = new Panel();
    this._cancelButton = new Button();
    this._colorSelectionUserControl = new ColorSelectionUserControl();
    this._panelTansparentButton = new Panel();
    this._btnTransparentBrush = new ColorButton();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._panelTansparentButton.SuspendLayout();
    this.SuspendLayout();
    this._bevelDialogButtons.Dock = DockStyle.Bottom;
    this._bevelDialogButtons.Location = new Point(0, 279);
    this._bevelDialogButtons.Name = "_bevelDialogButtons";
    this._bevelDialogButtons.Size = new Size(290, 2);
    this._bevelDialogButtons.TabIndex = 5;
    this._pnlDialogButtons.BackColor = SystemColors.Control;
    this._pnlDialogButtons.Controls.Add((Control) this._panelBtns);
    this._pnlDialogButtons.Dock = DockStyle.Bottom;
    this._pnlDialogButtons.Location = new Point(0, 281);
    this._pnlDialogButtons.Name = "_pnlDialogButtons";
    this._pnlDialogButtons.Size = new Size(290, 36);
    this._pnlDialogButtons.TabIndex = 4;
    this._panelBtns.Controls.Add((Control) this._cancelButton);
    this._panelBtns.Dock = DockStyle.Right;
    this._panelBtns.Location = new Point(197, 0);
    this._panelBtns.Name = "_panelBtns";
    this._panelBtns.Size = new Size(93, 36);
    this._panelBtns.TabIndex = 0;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.ImeMode = ImeMode.NoControl;
    this._cancelButton.Location = new Point(11, 6);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 0;
    this._cancelButton.Text = "Закрыть";
    this._colorSelectionUserControl.BackColor = Color.White;
    this._colorSelectionUserControl.Color = Color.Transparent;
    this._colorSelectionUserControl.Dock = DockStyle.Top;
    this._colorSelectionUserControl.Location = new Point(0, 38);
    this._colorSelectionUserControl.Name = "_colorSelectionUserControl";
    this._colorSelectionUserControl.Size = new Size(290, 242);
    this._colorSelectionUserControl.TabIndex = 6;
    this._colorSelectionUserControl.ColorWasSelected += new ColorSelectionUserControl.ColorWasSelectedDelegate(this._colorSelectionUserControl_ColorWasSelected);
    this._colorSelectionUserControl.Resize += new EventHandler(this._colorSelectionUserControl_Resize);
    this._panelTansparentButton.Controls.Add((Control) this._btnTransparentBrush);
    this._panelTansparentButton.Dock = DockStyle.Top;
    this._panelTansparentButton.Location = new Point(0, 0);
    this._panelTansparentButton.Name = "_panelTansparentButton";
    this._panelTansparentButton.Padding = new Padding(3, 3, 3, 5);
    this._panelTansparentButton.Size = new Size(290, 38);
    this._panelTansparentButton.TabIndex = 8;
    this._btnTransparentBrush.Color = Color.White;
    this._btnTransparentBrush.Dock = DockStyle.Fill;
    this._btnTransparentBrush.Location = new Point(3, 3);
    this._btnTransparentBrush.Name = "_btnTransparentBrush";
    this._btnTransparentBrush.Size = new Size(284, 30);
    this._btnTransparentBrush.TabIndex = 0;
    this._btnTransparentBrush.Text = "Прозрачный";
    this._btnTransparentBrush.UseVisualStyleBackColor = false;
    this._btnTransparentBrush.Click += new EventHandler(this._btnTransparentBrush_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackColor = Color.White;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(290, 317);
    this.Controls.Add((Control) this._colorSelectionUserControl);
    this.Controls.Add((Control) this._panelTansparentButton);
    this.Controls.Add((Control) this._bevelDialogButtons);
    this.Controls.Add((Control) this._pnlDialogButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectColorForm);
    this.Text = "Выбор цвета";
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._panelTansparentButton.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
