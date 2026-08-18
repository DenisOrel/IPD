
// Type: Intermech.Windows.Forms.BaseDialog
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Windows.Forms;

/// <summary>База для диалога</summary>
public class BaseDialog : 
  BaseForm,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  [CanBeNull]
  private readonly Form _centerOnForm;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel _pnlDialogButtons;
  protected Button _cancelButton;
  protected Button _okButton;
  protected Bevel _bevelDialogButtons;
  protected Panel _panelBtns;

  public BaseDialog()
  {
    this.AddService<BaseDialog>(this);
    this.InitializeComponent();
  }

  public BaseDialog([CanBeNull] string contextName, [CanBeNull] Form centerOnForm = null)
    : this(centerOnForm, contextName: contextName)
  {
  }

  public BaseDialog([CanBeNull] System.IServiceProvider ownerServices, [CanBeNull] string contextName = null)
    : this((Form) null, ownerServices, contextName)
  {
  }

  public BaseDialog([CanBeNull] Form centerOnForm, [CanBeNull] System.IServiceProvider ownerServices = null, [CanBeNull] string contextName = null)
    : base(ownerServices, contextName)
  {
    this.AddService<BaseDialog>(this);
    this.InitializeComponent();
    this._centerOnForm = centerOnForm;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.RemoveService<BaseDialog>();
      this.components?.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
  /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    if (this._centerOnForm == null)
      return;
    this.CenterOnParentForm(this._centerOnForm);
  }

  /// <summary>Использовать ли FormStorage для хранения положения и размеров формы</summary>
  protected override bool SaveLoadFormSizeAndPosition()
  {
    return (this._centerOnForm == null || this.FormBorderStyle == FormBorderStyle.Sizable || this.FormBorderStyle == FormBorderStyle.SizableToolWindow) && base.SaveLoadFormSizeAndPosition();
  }

  /// <summary>Обновить статус доступности команд</summary>
  /// <returns>true если обновление прошло успешно, если обновление команд заблокировано, то false</returns>
  protected override bool UpdateCommands()
  {
    if (!base.UpdateCommands())
      return false;
    if (!this.InDesignMode)
    {
      if (this._okButton.Visible)
      {
        Button okButton = this._okButton;
        LocksManager saveLocker = this.SaveLocker;
        int num = (saveLocker != null ? (!saveLocker.IsLocked ? 1 : 0) : 1) == 0 || this.IsReadOnly ? 0 : (this.OkButtonCanBeEnabled() ? 1 : 0);
        okButton.Enabled = num != 0;
      }
    }
    else
      this._okButton.Enabled = !this.IsReadOnly && this.OkButtonCanBeEnabled();
    return true;
  }

  /// <summary>Дополнительная проверка (кроме IsReadOnly и блокировки сохранения - _saveLocker.IsLocked), должна ли быть включена кнопка OK</summary>
  /// <returns>true если кнопка может быть включена</returns>
  protected virtual bool OkButtonCanBeEnabled() => true;

  /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.</summary>
  /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
  protected override void OnShown(EventArgs e)
  {
    base.OnShown(e);
    if (this.InDesignMode)
      return;
    this.InitSaveLockService();
    LocksManager saveLocker = this.SaveLocker;
    this.LockSaveStatusChanged(saveLocker != null && saveLocker.IsLocked);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._pnlDialogButtons = new Panel();
    this._panelBtns = new Panel();
    this._okButton = new Button();
    this._cancelButton = new Button();
    this._bevelDialogButtons = new Bevel();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this.SuspendLayout();
    this._pnlDialogButtons.BackColor = Color.Transparent;
    this._pnlDialogButtons.Controls.Add((Control) this._panelBtns);
    this._pnlDialogButtons.Dock = DockStyle.Bottom;
    this._pnlDialogButtons.Location = new Point(0, 343);
    this._pnlDialogButtons.Name = "_pnlDialogButtons";
    this._pnlDialogButtons.Size = new Size(550, 36);
    this._pnlDialogButtons.TabIndex = 0;
    this._panelBtns.Controls.Add((Control) this._okButton);
    this._panelBtns.Controls.Add((Control) this._cancelButton);
    this._panelBtns.Dock = DockStyle.Right;
    this._panelBtns.Location = new Point(377, 0);
    this._panelBtns.Name = "_panelBtns";
    this._panelBtns.Size = new Size(173, 36);
    this._panelBtns.TabIndex = 0;
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.ImeMode = ImeMode.NoControl;
    this._okButton.Location = new Point(5, 6);
    this._okButton.Name = "_okButton";
    this._okButton.Size = new Size(75, 23);
    this._okButton.TabIndex = 0;
    this._okButton.Text = "OK";
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.ImeMode = ImeMode.NoControl;
    this._cancelButton.Location = new Point(86, 6);
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Size = new Size(75, 23);
    this._cancelButton.TabIndex = 1;
    this._cancelButton.Text = "Отмена";
    this._bevelDialogButtons.Dock = DockStyle.Bottom;
    this._bevelDialogButtons.Location = new Point(0, 341);
    this._bevelDialogButtons.Name = "_bevelDialogButtons";
    this._bevelDialogButtons.Size = new Size(550, 2);
    this._bevelDialogButtons.TabIndex = 3;
    this.AcceptButton = (IButtonControl) this._okButton;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancelButton;
    this.ClientSize = new Size(550, 379);
    this.Controls.Add((Control) this._bevelDialogButtons);
    this.Controls.Add((Control) this._pnlDialogButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (BaseDialog);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.Text = "Диалог";
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
