// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseObjectCreatorUserForm
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using ImSSP;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.TechCard.Client.UI.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>Диалог создания технологических объектов</summary>
/// <summary>
/// Форма создания объекта на основе данных пользовательских форм
/// </summary>
[Obsolete]
public class TechCardBaseObjectCreatorUserForm : Form
{
  /// <summary>раздел справки для формы</summary>
  private int _helpTopicId = 1414;
  /// <summary>Тип создаваемого объекта</summary>
  private int _objTypeID = -1;
  /// <summary>Идентификатор созданного объекта</summary>
  protected long _objID;
  /// <summary>Флаг закрытия формы по "OK"</summary>
  private bool _okPressed;
  /// <summary>
  /// Признак необходимости удаления объекта
  /// (если пользователь нажал отмену)
  /// </summary>
  protected bool _objNeedDelete = true;
  /// <summary>Индекс текущей закладки</summary>
  protected int _tabIndex = -1;
  /// <summary>Список пользовательских форм</summary>
  protected ICollection<long> _formControls;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button buttonCancel;
  /// <summary>
  /// 
  /// </summary>
  protected Button buttonFinish;
  private TabControl tcForms;
  private Panel pnlTop;
  private Label lblCaption;
  private PictureBox pictCaption;

  /// <summary>Инициализация параметров формы</summary>
  private void InitData()
  {
    this._formControls = (ICollection<long>) null;
    if (this._objTypeID == 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectType objectType1 = sessionKeeper.Session.GetObjectType(this._objTypeID);
      int? objectType2 = objectType1?.ObjectType;
      int elemRouteId = TechCardConsts.ObjectTypes.ElemRouteID;
      if (objectType2.GetValueOrDefault() == elemRouteId & objectType2.HasValue)
        this._helpTopicId = 1462;
      if (objectType1 != null)
        this.Text = string.Format(this.Text, (object) objectType1.ObjectTypeName);
      if (this._objID != 0L)
        return;
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(this._objTypeID);
      if (objectCollection == null)
        return;
      IDBObject dbObject = objectCollection.Create();
      if (dbObject == null)
        return;
      this._objID = dbObject.ObjectID;
    }
  }

  /// <summary>Загрузка пользовательских форм</summary>
  protected virtual void FormContols_Load()
  {
    this.tcForms.TabPages.Clear();
    try
    {
      if (this._objID == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        int index = 0;
        foreach (long formControl in (IEnumerable<long>) this.FormControls)
        {
          FormDesignerView formDesignerView = new FormDesignerView(this._objID, formControl);
          string errorMsg;
          if (!formDesignerView.LoadForm(sessionKeeper.Session.GetObject(formControl), out errorMsg))
          {
            errorMsg = errorMsg + sc_19614.ssp_techcard_19615() + string.Format(LocalizationHolder.rm.GetString("TechCard.Client_371"), (object) formControl);
            int num = (int) MessageBox.Show(errorMsg, LocalizationHolder.rm.GetString(sc_19614.ssp_techcard_19616()), MessageBoxButtons.OK, MessageBoxIcon.Hand);
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
  /// <param name="objTypeId">Тип создаваемого объекта</param>
  /// <param name="objId"> Идентификатор созданного объекта</param>
  public TechCardBaseObjectCreatorUserForm(int objTypeId, long objId)
  {
    this._objTypeID = objTypeId;
    this._objID = objId;
    this.InitializeComponent();
    this.InitData();
    this.Name += this._objTypeID.ToString();
    TechCardFormUtils.LoadSettings((Control) this);
  }

  /// <summary>Список пользовательских форм</summary>
  public ICollection<long> FormControls
  {
    get => this._formControls;
    set
    {
      this._formControls = value;
      this.FormContols_Load();
    }
  }

  /// <summary>Идентификатор созданного объекта</summary>
  public long ObjID => this._objID;

  /// <summary>Флаг удаления объекта по отмене</summary>
  public bool ObjNeedDelete
  {
    get => this._objNeedDelete;
    set => this._objNeedDelete = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechCardBaseObjectCreatorImbase_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this._objNeedDelete && !this._okPressed)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        sessionKeeper.Session.GetObject(this._objID, false)?.Delete(0L);
        this._objID = 0L;
      }
    }
    TechCardFormUtils.SaveSettings((Control) this);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonFinish_Click(object sender, EventArgs e)
  {
    this._okPressed = true;
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
    if (this._tabIndex != -1 && this.tcForms.TabPages[this._tabIndex].Tag is FormDesignerView tag)
      tag.SaveForm();
    this._tabIndex = this.tcForms.TabIndex;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void buttonCancel_Click(object sender, EventArgs e) => this._okPressed = false;

  /// <summary>нажата f1 - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="hlpevent"></param>
  private void TechCardBaseObjectCreatorUserForm_HelpRequested(
    object sender,
    HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(this._helpTopicId);
  }

  /// <summary>нажата кнопка вызова помощи - показать справку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void TechCardBaseObjectCreatorUserForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(this._helpTopicId);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (TechCardBaseObjectCreatorUserForm));
    this.panelBottom = new Panel();
    this.buttonCancel = new Button();
    this.buttonFinish = new Button();
    this.tcForms = new TabControl();
    this.pnlTop = new Panel();
    this.lblCaption = new Label();
    this.pictCaption = new PictureBox();
    this.panelBottom.SuspendLayout();
    this.pnlTop.SuspendLayout();
    ((ISupportInitialize) this.pictCaption).BeginInit();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this.buttonCancel);
    this.panelBottom.Controls.Add((Control) this.buttonFinish);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.buttonCancel, "buttonCancel");
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
    componentResourceManager.ApplyResources((object) this.buttonFinish, "buttonFinish");
    this.buttonFinish.DialogResult = DialogResult.OK;
    this.buttonFinish.Name = "buttonFinish";
    this.buttonFinish.Click += new EventHandler(this.buttonFinish_Click);
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
    this.Name = nameof (TechCardBaseObjectCreatorUserForm);
    this.ShowInTaskbar = false;
    this.HelpButtonClicked += new CancelEventHandler(this.TechCardBaseObjectCreatorUserForm_HelpButtonClicked);
    this.FormClosed += new FormClosedEventHandler(this.TechCardBaseObjectCreatorImbase_FormClosed);
    this.HelpRequested += new HelpEventHandler(this.TechCardBaseObjectCreatorUserForm_HelpRequested);
    this.panelBottom.ResumeLayout(false);
    this.pnlTop.ResumeLayout(false);
    ((ISupportInitialize) this.pictCaption).EndInit();
    this.ResumeLayout(false);
  }
}
