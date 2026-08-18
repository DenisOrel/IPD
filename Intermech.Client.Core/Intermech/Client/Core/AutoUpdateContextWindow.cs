
// Type: Intermech.Client.Core.AutoUpdateContextWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Contexts;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>
/// Окно, позволяющее включить режим автоматического пополнения текущего контекста редактирования
/// </summary>
public class AutoUpdateContextWindow : Form
{
  /// <summary>Можно ли отображать данное окно</summary>
  private static bool CanShow = true;
  /// <summary>
  /// Последний результат работы диалога. Используется, когда пользователь отказывается видеть диалог.
  /// </summary>
  private static DialogResult LastResult = DialogResult.Abort;
  /// <summary>Закрыта ли подсказка</summary>
  private bool _closed = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  public CheckBox cbDontShowAgain;
  public Button btOK;
  public Button btCancel;
  public PictureBox pbObject;
  private Label labelInfo;
  public Button btHint;
  private TextBox edHint;
  public Button btDefault;

  /// <summary>Создать экземпляр окна</summary>
  public AutoUpdateContextWindow()
  {
    this.InitializeComponent();
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.MinimumSize = new Size(600, 200);
    this.MaximumSize = new Size(600, 200);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.UpdateControls();
  }

  /// <summary>Обновить статус контролов</summary>
  protected virtual void UpdateControls()
  {
    this.cbDontShowAgain.Checked = !AutoUpdateContextWindow.CanShow;
    this.cbDontShowAgain.Visible = true;
    this.cbDontShowAgain.Enabled = true;
  }

  /// <summary>
  /// Отобразить (если можно) окно, предлагающее включить режим автоматического пополнения указанного контекста редактирования
  /// </summary>
  /// <param name="contextID">Идентификатор версии объекта контекста редактирования</param>
  /// <returns>DialogResult.OK - включить, DialogResult.Cancel - отключить либо показ окна был запрещён ранее, DialogResult.Abort - отмена операции</returns>
  public static DialogResult Execute(long contextID)
  {
    if (!AutoUpdateContextWindow.CanShow)
      return AutoUpdateContextWindow.LastResult;
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || service.CanSetContextAutoUpdateMode(contextID) != CanSetContextModeCode.CanSetAutoUpdate || service.CachedEditingContextID == contextID && service.CachedContextMode == EditingContextMode.AutoUpdate)
      return DialogResult.Cancel;
    string str = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(contextID);
      if (objectInfo.Empty)
        return DialogResult.Cancel;
      str = $"[{objectInfo.ObjectID}] \"{objectInfo.Caption}\"";
    }
    using (AutoUpdateContextWindow updateContextWindow = new AutoUpdateContextWindow())
    {
      updateContextWindow.labelInfo.Text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1444"), (object) str);
      updateContextWindow.UpdateControls();
      DialogResult dialogResult = updateContextWindow.ShowDialog();
      AutoUpdateContextWindow.CanShow = !updateContextWindow.cbDontShowAgain.Checked;
      if (!AutoUpdateContextWindow.CanShow)
        AutoUpdateContextWindow.LastResult = dialogResult;
      return dialogResult;
    }
  }

  /// <summary>Отобразить или скрыть подсказку</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btHint_Click(object sender, EventArgs e)
  {
    if (!this._closed)
    {
      this.edHint.Visible = false;
      this.btHint.Text = LocalizationHolder.rm.GetString("Client.Core_1317");
      this.MinimumSize = new Size(600, 200);
      this.MaximumSize = new Size(600, 200);
      this._closed = true;
    }
    else
    {
      this.edHint.Visible = true;
      this.btHint.Text = LocalizationHolder.rm.GetString("Client.Core_1445");
      this.MaximumSize = new Size(600, 550);
      this.MinimumSize = new Size(600, 550);
      this._closed = false;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoUpdateContextWindow));
    this.cbDontShowAgain = new CheckBox();
    this.btOK = new Button();
    this.btCancel = new Button();
    this.pbObject = new PictureBox();
    this.labelInfo = new Label();
    this.btHint = new Button();
    this.edHint = new TextBox();
    this.btDefault = new Button();
    ((ISupportInitialize) this.pbObject).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cbDontShowAgain, "cbDontShowAgain");
    this.cbDontShowAgain.Name = "cbDontShowAgain";
    this.cbDontShowAgain.UseVisualStyleBackColor = true;
    this.btOK.Cursor = Cursors.Default;
    this.btOK.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.Name = "btOK";
    this.btCancel.Cursor = Cursors.Default;
    this.btCancel.DialogResult = DialogResult.Abort;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.Name = "btCancel";
    componentResourceManager.ApplyResources((object) this.pbObject, "pbObject");
    this.pbObject.Name = "pbObject";
    this.pbObject.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelInfo, "labelInfo");
    this.labelInfo.Name = "labelInfo";
    this.btHint.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this.btHint, "btHint");
    this.btHint.Name = "btHint";
    this.btHint.Click += new EventHandler(this.btHint_Click);
    componentResourceManager.ApplyResources((object) this.edHint, "edHint");
    this.edHint.Name = "edHint";
    this.edHint.ReadOnly = true;
    this.btDefault.Cursor = Cursors.Default;
    this.btDefault.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btDefault, "btDefault");
    this.btDefault.Name = "btDefault";
    this.AcceptButton = (IButtonControl) this.btOK;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.btDefault);
    this.Controls.Add((Control) this.edHint);
    this.Controls.Add((Control) this.btHint);
    this.Controls.Add((Control) this.labelInfo);
    this.Controls.Add((Control) this.pbObject);
    this.Controls.Add((Control) this.cbDontShowAgain);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.btCancel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoUpdateContextWindow);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.TopMost = true;
    ((ISupportInitialize) this.pbObject).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
