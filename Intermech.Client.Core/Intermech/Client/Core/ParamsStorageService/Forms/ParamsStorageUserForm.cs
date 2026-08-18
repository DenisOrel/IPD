
// Type: Intermech.Client.Core.ParamsStorageService.Forms.ParamsStorageUserForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.ParamsStorage;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.ParamsStorageService.Forms;

/// <summary>Диалог создания технологических объектов</summary>
/// <summary>
/// Форма создания объекта на основе данных пользовательских форм
/// </summary>
public class ParamsStorageUserForm : Form
{
  /// <summary>Раздел справки для формы ( пока не определен)</summary>
  private int helpTopicID = -1;
  /// <summary>Объект - контейнер параметров</summary>
  private IParamsStorageObject _paramsStorage;
  /// <summary>Список пользовательских форм</summary>
  protected List<long> _formControls;
  /// <summary>Индекс текущей закрадки</summary>
  protected int _tabIndex = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button buttonCancel;
  /// <summary>
  /// 
  /// </summary>
  protected Button buttonOK;
  private TabControl tcForms;
  private Panel pnlTop;
  private Label lblCaption;
  private PictureBox pictCaption;

  /// <summary>Инициализация параметров формы</summary>
  protected virtual void InitializeData()
  {
    this._formControls = new List<long>();
    if (this.ParamsStorage == null)
      return;
    this.Name += this.ParamsStorage.StorageName;
    this._formControls.AddRange((IEnumerable<long>) this.ParamsStorage.GetFormDesignIDs());
    this.LoadFormContols();
  }

  /// <summary>Загрузка параметов формы</summary>
  protected virtual void LoadSettings()
  {
    HybridDictionary hybridDictionary = new HybridDictionary();
    FormStorage.LoadLayout((Control) this, (IDictionary) hybridDictionary);
    long num1 = 0;
    if (hybridDictionary.Contains((object) "formID"))
      num1 = Convert.ToInt64(hybridDictionary[(object) "formID"]);
    if (num1 == 0L)
      return;
    int num2 = -1;
    foreach (TabPage tabPage in this.tcForms.TabPages)
    {
      if (this.tcForms.TabPages[this._tabIndex].Tag is FormDesignerView tag && tag.FormID == num1)
      {
        num2 = tabPage.TabIndex;
        break;
      }
    }
    if (num2 == -1)
      return;
    this.tcForms.TabIndex = num2;
  }

  /// <summary>Сохранение параметров формы</summary>
  protected virtual void SaveSettings()
  {
    long num = 0;
    if (this._tabIndex != -1 && this.tcForms.TabPages[this._tabIndex].Tag is FormDesignerView tag)
      num = tag.FormID;
    FormStorage.SaveLayout((Control) this, (IDictionary) new HybridDictionary(1)
    {
      {
        (object) "formID",
        (object) num
      }
    });
  }

  /// <summary>Загрузка пользовательских форм</summary>
  protected virtual void LoadFormContols()
  {
    this.tcForms.TabPages.Clear();
    try
    {
      if (this.ParamsStorage == null || this.ParamsStorage.ObjectID == 0L || this.FormControls == null || this.FormControls.Count == 0)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int index = 0;
        foreach (long formControl in this.FormControls)
        {
          FormDesignerView formDesignerView = new FormDesignerView(this.ParamsStorage.ObjectID, formControl);
          string errorMsg = string.Empty;
          if (!formDesignerView.LoadForm(sessionKeeper.Session.GetObject(formControl), out errorMsg))
          {
            int num = (int) MessageBox.Show($"{errorMsg}. {string.Format(LocalizationHolder.rm.GetString("Client.Core_1142"), (object) formControl)}", LocalizationHolder.rm.GetString("Client.Core_1611"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
          }
          else
            formDesignerView.ButtonsVisible(false);
          IDBObject dbObject = sessionKeeper.Session.GetObject(formControl);
          if (dbObject != null)
            this.tcForms.TabPages.Add(dbObject.Caption);
          else
            this.tcForms.TabPages.Add(formDesignerView.Name);
          formDesignerView.Parent = (Control) this.tcForms.TabPages[index];
          formDesignerView.Dock = DockStyle.Fill;
          this.tcForms.TabPages[index++].Tag = (object) formDesignerView;
        }
      }
    }
    finally
    {
      if (this.tcForms.TabPages.Count != 0)
        this.tcForms.TabIndex = 0;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="paramsStorage">Объект контейнер</param>
  public ParamsStorageUserForm(IParamsStorageObject paramsStorage)
  {
    this._paramsStorage = paramsStorage;
    this.InitializeComponent();
    this.InitializeData();
    this.LoadSettings();
  }

  /// <summary>Объект - контейнер параметров</summary>
  public IParamsStorageObject ParamsStorage => this._paramsStorage;

  /// <summary>Список пользовательских форм</summary>
  public List<long> FormControls => this._formControls;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ParamsStorageUserForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this.SaveSettings();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonFinish_Click(object sender, EventArgs e)
  {
    for (int index = 0; index < this.tcForms.TabPages.Count; ++index)
    {
      if (this.tcForms.TabPages[index].Tag is FormDesignerView tag)
        tag.SaveForm();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tcForms_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._tabIndex != -1)
      (this.tcForms.TabPages[this._tabIndex].Tag as FormDesignerView).SaveForm();
    this._tabIndex = this.tcForms.TabIndex;
  }

  /// <summary>нажата f1 - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="hlpevent"></param>
  private void ParamsStorageUserForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(this.helpTopicID);
  }

  /// <summary>нажата кнопка вызова помощи - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ParamsStorageUserForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(this.helpTopicID);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ParamsStorageUserForm));
    this.panelBottom = new Panel();
    this.buttonCancel = new Button();
    this.buttonOK = new Button();
    this.tcForms = new TabControl();
    this.pnlTop = new Panel();
    this.lblCaption = new Label();
    this.pictCaption = new PictureBox();
    this.panelBottom.SuspendLayout();
    this.pnlTop.SuspendLayout();
    ((ISupportInitialize) this.pictCaption).BeginInit();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.buttonCancel);
    this.panelBottom.Controls.Add((Control) this.buttonOK);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    componentResourceManager.ApplyResources((object) this.buttonOK, "buttonOK");
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Click += new EventHandler(this.buttonFinish_Click);
    componentResourceManager.ApplyResources((object) this.tcForms, "tcForms");
    this.tcForms.Name = "tcForms";
    this.tcForms.SelectedIndex = 0;
    this.tcForms.SelectedIndexChanged += new EventHandler(this.tcForms_SelectedIndexChanged);
    this.pnlTop.Controls.Add((Control) this.lblCaption);
    this.pnlTop.Controls.Add((Control) this.pictCaption);
    componentResourceManager.ApplyResources((object) this.pnlTop, "pnlTop");
    this.pnlTop.Name = "pnlTop";
    componentResourceManager.ApplyResources((object) this.lblCaption, "lblCaption");
    this.lblCaption.ForeColor = SystemColors.GrayText;
    this.lblCaption.Name = "lblCaption";
    componentResourceManager.ApplyResources((object) this.pictCaption, "pictCaption");
    this.pictCaption.Name = "pictCaption";
    this.pictCaption.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tcForms);
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.pnlTop);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ParamsStorageUserForm);
    this.ShowInTaskbar = false;
    this.HelpButtonClicked += new CancelEventHandler(this.ParamsStorageUserForm_HelpButtonClicked);
    this.FormClosed += new FormClosedEventHandler(this.ParamsStorageUserForm_FormClosed);
    this.HelpRequested += new HelpEventHandler(this.ParamsStorageUserForm_HelpRequested);
    this.panelBottom.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    ((ISupportInitialize) this.pictCaption).EndInit();
    this.ResumeLayout(false);
  }
}
