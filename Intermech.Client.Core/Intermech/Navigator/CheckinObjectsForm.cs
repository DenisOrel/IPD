
// Type: Intermech.Navigator.CheckinObjectsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Commands;
using Intermech.Docking;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>
/// Форма, отображаемая перед завершением изменений в объектах
/// </summary>
public sealed class CheckinObjectsForm : Form
{
  private IServiceContainer _contextServices;
  private ObjectCommandsOptionsHolder _commonOptionsHolder;
  private ICollection<WorkCopyCommandOptionsEditor> _contextServicesEditors;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox picture;
  private Panel panel1;
  private Button btnNo;
  private Button btnYes;
  private CheckBox cbPreserveWorkingCopy;
  protected PageControl pages;
  private Intermech.Docking.TabPage pageMain;
  private Label lbText;

  /// <summary>Конструктор</summary>
  public CheckinObjectsForm() => this.InitializeComponent();

  /// <summary>Конструктор</summary>
  /// <param name="contextServices">Контекст выполнения команды</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="text">Текст в окне</param>
  /// <param name="firstTabText">Заголовок первой закладки</param>
  /// <param name="extraControls">Дополнительные контролы, которые можно разместить на форме на дополнительных закладках</param>
  public CheckinObjectsForm(
    IServiceContainer contextServices,
    string caption,
    string text,
    string firstTabText,
    ICollection<WorkCopyCommandOptionsEditor> extraControls)
    : this()
  {
    if (contextServices == null)
      throw new ArgumentNullException(nameof (contextServices));
    if (caption == null)
      throw new ArgumentNullException(nameof (caption));
    if (text == null)
      throw new ArgumentNullException(nameof (text));
    if (firstTabText == null)
      throw new ArgumentNullException(nameof (firstTabText));
    if (extraControls == null)
      throw new ArgumentNullException(nameof (extraControls));
    this._contextServices = contextServices;
    this._contextServicesEditors = extraControls;
    this._commonOptionsHolder = (ObjectCommandsOptionsHolder) contextServices.GetService(typeof (ObjectCommandsOptionsHolder));
    this.Text = caption;
    this.lbText.Text = text;
    this.pageMain.Text = firstTabText;
    if (this._contextServicesEditors.Count == 0)
      return;
    this.AddExtraEditors();
  }

  /// <summary>Добавить контролы на закладки формы</summary>
  private void AddExtraEditors()
  {
    foreach (WorkCopyCommandOptionsEditor contextServicesEditor in (IEnumerable<WorkCopyCommandOptionsEditor>) this._contextServicesEditors)
    {
      if (contextServicesEditor != null)
      {
        contextServicesEditor.Dock = DockStyle.Fill;
        Intermech.Docking.TabPage tabPage = new Intermech.Docking.TabPage();
        tabPage.BorderStyle = Intermech.Docking.Rendering.BorderStyle.None;
        tabPage.Text = contextServicesEditor.Text;
        this.pages.TabPages.Add(tabPage);
        tabPage.Controls.Add((Control) contextServicesEditor);
      }
    }
  }

  /// <summary>Установить статус всех контролов формы</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void CheckinObjectsForm_Shown(object sender, EventArgs e)
  {
    OperatingSystem osVersion = Environment.OSVersion;
    bool flag = false;
    if (osVersion.Platform == PlatformID.Win32NT)
      flag = osVersion.Version.Major >= 6;
    if (!flag)
      this.BackColor = SystemColors.Control;
    if (this._commonOptionsHolder != null && this.ShowPreserveWorkingCopiesBox)
    {
      this.cbPreserveWorkingCopy.Visible = true;
      this.cbPreserveWorkingCopy.Checked = (this._commonOptionsHolder.Value & ObjectCommandsOptions.PreserveWorkingCopies) == ObjectCommandsOptions.PreserveWorkingCopies;
    }
    else
    {
      this.cbPreserveWorkingCopy.Visible = false;
      this.cbPreserveWorkingCopy.Checked = false;
    }
  }

  private void CheckinObjectsForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this.DialogResult != DialogResult.Yes)
      return;
    if (this._commonOptionsHolder != null && this.ShowPreserveWorkingCopiesBox)
    {
      if (this.cbPreserveWorkingCopy.Checked)
        this._commonOptionsHolder.Value |= ObjectCommandsOptions.PreserveWorkingCopies;
      else
        this._commonOptionsHolder.Value &= ~ObjectCommandsOptions.PreserveWorkingCopies;
    }
    foreach (WorkCopyCommandOptionsEditor contextServicesEditor in (IEnumerable<WorkCopyCommandOptionsEditor>) this._contextServicesEditors)
      contextServicesEditor.ApplyChanges();
  }

  /// <summary>
  /// Включает и выключает отображения флажка "Не удалять рабочие копии".
  /// </summary>
  [Browsable(true)]
  [DefaultValue(false)]
  public bool ShowPreserveWorkingCopiesBox { get; set; }

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CheckinObjectsForm));
    this.picture = new PictureBox();
    this.panel1 = new Panel();
    this.btnNo = new Button();
    this.btnYes = new Button();
    this.cbPreserveWorkingCopy = new CheckBox();
    this.pages = new PageControl();
    this.pageMain = new Intermech.Docking.TabPage();
    this.lbText = new Label();
    ((ISupportInitialize) this.picture).BeginInit();
    this.panel1.SuspendLayout();
    this.pages.SuspendLayout();
    this.pageMain.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.picture, "picture");
    this.picture.Name = "picture";
    this.picture.TabStop = false;
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.btnNo);
    this.panel1.Controls.Add((Control) this.btnYes);
    this.panel1.Controls.Add((Control) this.cbPreserveWorkingCopy);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.btnNo, "btnNo");
    this.btnNo.DialogResult = DialogResult.No;
    this.btnNo.Name = "btnNo";
    this.btnNo.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnYes, "btnYes");
    this.btnYes.DialogResult = DialogResult.Yes;
    this.btnYes.Name = "btnYes";
    this.btnYes.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbPreserveWorkingCopy, "cbPreserveWorkingCopy");
    this.cbPreserveWorkingCopy.Name = "cbPreserveWorkingCopy";
    this.cbPreserveWorkingCopy.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.pages, "pages");
    this.pages.CausesValidation = false;
    this.pages.Controls.Add((Control) this.pageMain);
    this.pages.Name = "pages";
    this.pageMain.Controls.Add((Control) this.lbText);
    this.pageMain.Index = 0;
    componentResourceManager.ApplyResources((object) this.pageMain, "pageMain");
    this.pageMain.Name = "pageMain";
    componentResourceManager.ApplyResources((object) this.lbText, "lbText");
    this.lbText.Name = "lbText";
    this.AcceptButton = (IButtonControl) this.btnYes;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.BackColor = SystemColors.Window;
    this.CancelButton = (IButtonControl) this.btnNo;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.pages);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.picture);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CheckinObjectsForm);
    this.ShowIcon = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.CheckinObjectsForm_FormClosed);
    this.Shown += new EventHandler(this.CheckinObjectsForm_Shown);
    ((ISupportInitialize) this.picture).EndInit();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.pages.ResumeLayout(false);
    this.pageMain.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
