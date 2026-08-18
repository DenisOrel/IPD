// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.AdvancedDialog
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class AdvancedDialog : 
  AdvancedForm,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable
{
  [CanBeNull]
  private readonly Form _centerOnForm;
  private IContainer components;
  private Panel _pnlDialogButtons;
  private Button _dialogCancelButton;
  private Button _dialogAcceptButton;
  private Panel _panelBtns;

  [NotNull]
  protected Panel PnlDialogButtons
  {
    get => this._pnlDialogButtons.CheckInitializedIn<Panel>((object) this);
  }

  [NotNull]
  protected Button DialogCancelButton
  {
    get => this._dialogCancelButton.CheckInitializedIn<Button>((object) this);
  }

  [NotNull]
  protected Button DialogAcceptButton
  {
    get => this._dialogAcceptButton.CheckInitializedIn<Button>((object) this);
  }

  [NotNull]
  protected Panel PanelBtns => this._panelBtns.CheckInitializedIn<Panel>((object) this);

  public AdvancedDialog()
    : this((Form) null)
  {
  }

  public AdvancedDialog([CanBeNull] Form centerOnForm)
  {
    this.InitializeComponent();
    this._centerOnForm = centerOnForm;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.components?.Dispose();
    base.Dispose(disposing);
  }

  protected override void OnLoad([NotNull] EventArgs e)
  {
    base.OnLoad(e);
    if (this._centerOnForm == null)
      return;
    this.CenterOnParentForm(this._centerOnForm);
  }

  private void InitializeComponent()
  {
    this._pnlDialogButtons = new Panel();
    this._panelBtns = new Panel();
    this._dialogAcceptButton = new Button();
    this._dialogCancelButton = new Button();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.BackColor = Color.Transparent;
    this._pnlDialogButtons.Controls.Add((Control) this._panelBtns);
    this._pnlDialogButtons.Dock = DockStyle.Bottom;
    this._pnlDialogButtons.Location = new Point(0, 343);
    this._pnlDialogButtons.Name = "_pnlDialogButtons";
    this._pnlDialogButtons.Size = new Size(550, 36);
    this._pnlDialogButtons.TabIndex = 9999;
    this._panelBtns.Controls.Add((Control) this._dialogAcceptButton);
    this._panelBtns.Controls.Add((Control) this._dialogCancelButton);
    this._panelBtns.Dock = DockStyle.Right;
    this._panelBtns.Location = new Point(377, 0);
    this._panelBtns.Name = "_panelBtns";
    this._panelBtns.Size = new Size(173, 36);
    this._panelBtns.TabIndex = 0;
    this._dialogAcceptButton.DialogResult = DialogResult.OK;
    this._dialogAcceptButton.ImeMode = ImeMode.NoControl;
    this._dialogAcceptButton.Location = new Point(5, 6);
    this._dialogAcceptButton.Name = "_dialogAcceptButton";
    this._dialogAcceptButton.Size = new Size(75, 23);
    this._dialogAcceptButton.TabIndex = 0;
    this._dialogAcceptButton.Text = "OK";
    this._dialogCancelButton.DialogResult = DialogResult.Cancel;
    this._dialogCancelButton.ImeMode = ImeMode.NoControl;
    this._dialogCancelButton.Location = new Point(86, 6);
    this._dialogCancelButton.Name = "_dialogCancelButton";
    this._dialogCancelButton.Size = new Size(75, 23);
    this._dialogCancelButton.TabIndex = 1;
    this._dialogCancelButton.Text = "Отмена";
    this.AcceptButton = (IButtonControl) this._dialogAcceptButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._dialogCancelButton;
    this.ClientSize = new Size(550, 379);
    this.Controls.Add((Control) this._pnlDialogButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AdvancedDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = "Диалог";
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
