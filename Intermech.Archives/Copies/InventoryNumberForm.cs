// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.InventoryNumberForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Copies;
using Intermech.Search.Interfaces.Copies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Форма для присвоения инвентарного номера документам.</summary>
public class InventoryNumberForm : Form
{
  /// <summary>Выделенные элементы грида</summary>
  private readonly List<IDBTypedObjectID> _items;
  /// <summary>
  /// Сгенерированный автоматически номер для первого элемента в списке итемов.
  /// Нужен для отображения в текстбоксе.
  /// </summary>
  private readonly string _firstAutoGenInventoryNumber = string.Empty;
  /// <summary>
  /// Инвентарный номер, сгенерированный по классификатору для первого элемента в списке итемов.
  /// Нужен для отображения в текстбоксе.
  /// </summary>
  private string _classifierGenInventoryNumber = string.Empty;
  /// <summary>
  /// Словарик со счётчиками, использованными в формулами.
  /// нужен для отката значений счётчика , если пользователь нажал Отмена в диалоге
  /// </summary>
  private readonly Dictionary<string, long> _counter = new Dictionary<string, long>();
  /// <summary>
  /// Определяет возможность использования кнопки выбора классификаторов.
  /// Активна, когда есть хотя бы один классификатор в списке настроек.
  /// </summary>
  private readonly bool _canUseClassifyBtn;
  /// <summary>ID выбранного классификатора.</summary>
  private long _classifierID;
  private bool _needRestoreCounter = true;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOk;
  private GroupBox groupBox1;
  private RadioButton rbManual;
  private RadioButton rbClassifierGeneration;
  private RadioButton rbAutoGeneration;
  private Panel panelOTD;
  private Button btnClassify;
  private Label label1;
  private TextBox textbox;
  private Panel panel1;
  private Label label2;
  private DateTimePicker _dtRegistrationDate;

  /// <summary>Конструктор</summary>
  public InventoryNumberForm(List<IDBTypedObjectID> items)
  {
    this.InitializeComponent();
    this._items = items;
    IMServerService service1 = ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService;
    IDBConfigurations service2 = ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations;
    bool flag1 = service2.ReadBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.AUTOGENERATION, false, DBConfigMode.GlobalOnly);
    bool flag2 = service2.ReadBool(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.USE_CLASSIFIERS, false, DBConfigMode.GlobalOnly);
    this._canUseClassifyBtn = service1.GetCustomService(typeof (ICopiesService)) is ICopiesService customService1 && customService1.Classifiers.Count > 0;
    this.btnClassify.Enabled = flag2 && this._canUseClassifyBtn;
    this.rbAutoGeneration.Enabled = flag1;
    this.rbClassifierGeneration.Enabled = this.btnClassify.Enabled;
    if (flag1)
    {
      this.rbAutoGeneration.Checked = true;
      this.btnClassify.Enabled = false;
      if (service1.GetCustomService(typeof (IInventoryNumberGenerator)) is IInventoryNumberGenerator customService2)
      {
        this._counter = customService2.GenerateNumber(this._items[0].ObjectID, this._items[0].ObjectType, out this._firstAutoGenInventoryNumber);
        this.textbox.Text = this._firstAutoGenInventoryNumber;
        this.textbox.Enabled = false;
      }
    }
    else if (this.rbClassifierGeneration.Enabled)
    {
      this.rbClassifierGeneration.Checked = true;
      this.textbox.Enabled = false;
    }
    else
    {
      this.rbManual.Checked = true;
      this.textbox.Enabled = true;
      if (this._items.Count > 1)
      {
        this.panelOTD.Enabled = false;
        this.btnOk.Text = "Далее";
      }
      this.textbox.Enabled = true;
      this.btnClassify.Enabled = this._canUseClassifyBtn;
    }
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2649);
  }

  /// <summary>Присвоить инвентарные номера.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this._items.Count > 1 && this.btnOk.Text == "Далее")
      this.DialogResult = DialogResult.Retry;
    else if (this.textbox.Text == string.Empty)
    {
      int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_163"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK);
    }
    else
    {
      this.SetInventoryNumberAndRegistrationDate();
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>Нажатие кнопки Отмена</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnCancel_Click(object sender, EventArgs e) => this.Close();

  /// <summary>Классифицировать.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnClassify_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (!(sk.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService1))
        return;
      ISelectionsService customService2 = sk.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      this._classifierID = this.ChooseClassifier(customService1);
      if (this._classifierID == 0L)
        return;
      this._classifierGenInventoryNumber = this.GetObjectClassificationNumber(sk, this._items[0].ObjectID, this._classifierID, customService2);
      this.textbox.Text = this._classifierGenInventoryNumber;
    }
  }

  /// <summary>Радио-кнопка "Автоматически сгенерировать".</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void rbAutoGeneration_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.rbAutoGeneration.Checked)
      return;
    this.btnClassify.Enabled = false;
    this.textbox.Enabled = false;
    this.textbox.Text = this._firstAutoGenInventoryNumber;
  }

  /// <summary>Радио-кнопка "Сгенерировать по классификатору"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void rbClassifierGeneration_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.rbClassifierGeneration.Checked)
      return;
    this.btnClassify.Enabled = true;
    this.textbox.Enabled = false;
    this.textbox.Text = this._classifierGenInventoryNumber;
  }

  /// <summary>Радио-кнопка "Присвоить вручную"</summary>
  /// <param name="sender">The source of the event.</param>
  /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
  private void rbManual_CheckedChanged(object sender, EventArgs e)
  {
    if (this.rbManual.Checked)
    {
      if (this._items.Count > 1)
      {
        this.textbox.Enabled = false;
        this.btnClassify.Enabled = false;
        this.btnOk.Text = "Далее";
      }
      else
      {
        this.textbox.Enabled = true;
        this.btnClassify.Enabled = this._canUseClassifyBtn;
        this.btnOk.Text = "Применить";
      }
      this.textbox.Text = string.Empty;
    }
    else
    {
      this.textbox.Enabled = false;
      this.btnOk.Text = "Применить";
    }
  }

  /// <summary>Присвоение инвентарного номера и даты регистрации.</summary>
  private void SetInventoryNumberAndRegistrationDate()
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      INotificationService service = ApplicationServices.Container.GetService<INotificationService>();
      if (!(sk.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService1))
        throw new Exception(ServiceHolder.rm.GetString("Archives_CopiesClientServiceNotFind"));
      if (this.rbAutoGeneration.Checked && sk.Session.GetCustomService(typeof (IInventoryNumberGenerator)) is IInventoryNumberGenerator customService2)
      {
        for (int index = 0; index < this._items.Count; ++index)
        {
          if (index == 0)
          {
            AttributeValues invNumberAttrValues;
            customService1.SetInventoryNumberAttributes(sk.Session.SessionGUID, this._items[index].ObjectID, this._firstAutoGenInventoryNumber, this._dtRegistrationDate.Value, out invNumberAttrValues);
            service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", this._items[index].ObjectID, this._items[index].ObjectType, new AttributeValues[0], new AttributeValues[1]
            {
              invNumberAttrValues
            }));
          }
          else
          {
            string formula;
            customService2.GenerateNumber(this._items[index].ObjectID, this._items[index].ObjectType, out formula);
            AttributeValues invNumberAttrValues;
            customService1.SetInventoryNumberAttributes(sk.Session.SessionGUID, this._items[index].ObjectID, formula, this._dtRegistrationDate.Value, out invNumberAttrValues);
            service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", this._items[index].ObjectID, this._items[index].ObjectType, new AttributeValues[0], new AttributeValues[1]
            {
              invNumberAttrValues
            }));
          }
        }
        this._needRestoreCounter = false;
      }
      if (this.rbClassifierGeneration.Checked)
      {
        this._needRestoreCounter = true;
        ISelectionsService customService3 = sk.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
        foreach (IDBTypedObjectID dbTypedObjectId in this._items)
        {
          string classificationNumber = this.GetObjectClassificationNumber(sk, dbTypedObjectId.ObjectID, this._classifierID, customService3);
          AttributeValues invNumberAttrValues;
          customService1.SetInventoryNumberAttributes(sk.Session.SessionGUID, dbTypedObjectId.ObjectID, classificationNumber, this._dtRegistrationDate.Value, out invNumberAttrValues);
          service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType, new AttributeValues[0], new AttributeValues[1]
          {
            invNumberAttrValues
          }));
          this.IncludeObjectToClassifier(dbTypedObjectId.ObjectID, sk, customService3);
        }
      }
      if (!this.rbManual.Checked)
        return;
      this._needRestoreCounter = this.textbox.Text != this._firstAutoGenInventoryNumber;
      foreach (IDBTypedObjectID dbTypedObjectId in this._items)
      {
        AttributeValues invNumberAttrValues;
        customService1.SetInventoryNumberAttributes(sk.Session.SessionGUID, dbTypedObjectId.ObjectID, this.textbox.Text, this._dtRegistrationDate.Value, out invNumberAttrValues);
        service?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", dbTypedObjectId.ObjectID, dbTypedObjectId.ObjectType, new AttributeValues[0], new AttributeValues[1]
        {
          invNumberAttrValues
        }));
      }
    }
  }

  /// <summary>Включить объект в классификатор.</summary>
  /// <param name="objectId">Ид версии объекта.</param>
  /// <param name="sk">SessionKeeper.</param>
  /// <param name="selectionsService">Сервис выборок и классификаторов.</param>
  private void IncludeObjectToClassifier(
    long objectId,
    SessionKeeper sk,
    ISelectionsService selectionsService)
  {
    if (selectionsService.ExistsObject((object) sk.Session.SessionGUID, this._classifierID, objectId))
      return;
    selectionsService.IncludeObjects((object) sk.Session.SessionGUID, this._classifierID, new long[1]
    {
      objectId
    });
  }

  /// <summary>
  /// Установить дату регистрации объектов в отд на время, указанное на форме.
  /// </summary>
  /// <exception cref="T:System.NotImplementedException"></exception>
  private void SetRegistrationDate(IDBObject currObject)
  {
    DateTime dateTime = this._dtRegistrationDate.Value;
    currObject.GetAttributeByID(ConstsHolder.OTDRegisteredDateID).AsDateTime = dateTime;
  }

  /// <summary>Выбор классификатора</summary>
  /// <param name="service">Настройки</param>
  /// <returns></returns>
  private long ChooseClassifier(ICopiesService service)
  {
    long num = 0;
    using (ClassifySelectionForm classifySelectionForm = new ClassifySelectionForm(service.Classifiers.ToArray()))
    {
      if (classifySelectionForm.ShowDialog() == DialogResult.OK)
      {
        if (classifySelectionForm.SelectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
          num = itemData.Value;
      }
    }
    return num;
  }

  /// <summary>Получить классификационный номер объекта.</summary>
  /// <param name="sk">Хранитель сессии</param>
  /// <param name="objectId">ID объекта</param>
  /// <param name="classifierId">ID классификатора</param>
  /// <param name="selectionsService">Сервис выборок и классификаторов</param>
  /// <returns>Инвентарный номер, полученный с помощью классификатора</returns>
  private string GetObjectClassificationNumber(
    SessionKeeper sk,
    long objectId,
    long classifierId,
    ISelectionsService selectionsService)
  {
    string empty = string.Empty;
    if (selectionsService == null)
      return empty;
    IObjectClassificator objectClassificator = selectionsService.GetObjectClassificator((object) sk.Session.SessionGUID, classifierId);
    if (objectClassificator == null)
      return empty;
    AttributeValues[] clasificatorAttributes = objectClassificator.GetClasificatorAttributes(objectId);
    if (clasificatorAttributes != null)
    {
      foreach (AttributeValues attributeValues in clasificatorAttributes)
      {
        if (attributeValues.AttributeID == ConstsHolder.InventoryNumberID)
        {
          empty = attributeValues.Values[0].ToString();
          break;
        }
      }
    }
    return empty;
  }

  /// <summary>Откатывает счетчик автогенерации номера.</summary>
  private void RestoreCounter()
  {
    if (this._counter == null || this._counter.Count <= 0 || !((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IInventoryNumberGenerator)) is IInventoryNumberGenerator customService))
      return;
    customService.RestoreCounters(this._counter);
  }

  /// <summary>Восстановление размеров и положения формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void InventoryNumberForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохранение размеров и положения формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void InventoryNumberForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this._needRestoreCounter)
      this.RestoreCounter();
    FormStorage.SaveLayout((Control) this);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InventoryNumberForm));
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.btnClassify = new Button();
    this.groupBox1 = new GroupBox();
    this.rbManual = new RadioButton();
    this.rbClassifierGeneration = new RadioButton();
    this.rbAutoGeneration = new RadioButton();
    this.panelOTD = new Panel();
    this.label1 = new Label();
    this.textbox = new TextBox();
    this.panel1 = new Panel();
    this._dtRegistrationDate = new DateTimePicker();
    this.label2 = new Label();
    this.groupBox1.SuspendLayout();
    this.panelOTD.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    componentResourceManager.ApplyResources((object) this.btnClassify, "btnClassify");
    this.btnClassify.Name = "btnClassify";
    this.btnClassify.UseVisualStyleBackColor = true;
    this.btnClassify.Click += new EventHandler(this.btnClassify_Click);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.rbManual);
    this.groupBox1.Controls.Add((Control) this.rbClassifierGeneration);
    this.groupBox1.Controls.Add((Control) this.rbAutoGeneration);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbManual, "rbManual");
    this.rbManual.Name = "rbManual";
    this.rbManual.TabStop = true;
    this.rbManual.UseVisualStyleBackColor = true;
    this.rbManual.CheckedChanged += new EventHandler(this.rbManual_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbClassifierGeneration, "rbClassifierGeneration");
    this.rbClassifierGeneration.Name = "rbClassifierGeneration";
    this.rbClassifierGeneration.TabStop = true;
    this.rbClassifierGeneration.UseVisualStyleBackColor = true;
    this.rbClassifierGeneration.CheckedChanged += new EventHandler(this.rbClassifierGeneration_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbAutoGeneration, "rbAutoGeneration");
    this.rbAutoGeneration.Name = "rbAutoGeneration";
    this.rbAutoGeneration.TabStop = true;
    this.rbAutoGeneration.UseVisualStyleBackColor = true;
    this.rbAutoGeneration.CheckedChanged += new EventHandler(this.rbAutoGeneration_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.panelOTD, "panelOTD");
    this.panelOTD.Controls.Add((Control) this.btnClassify);
    this.panelOTD.Controls.Add((Control) this.label1);
    this.panelOTD.Controls.Add((Control) this.textbox);
    this.panelOTD.Name = "panelOTD";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.textbox, "textbox");
    this.textbox.Name = "textbox";
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Controls.Add((Control) this._dtRegistrationDate);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this._dtRegistrationDate, "_dtRegistrationDate");
    this._dtRegistrationDate.Format = DateTimePickerFormat.Custom;
    this._dtRegistrationDate.Name = "_dtRegistrationDate";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.AcceptButton = (IButtonControl) this.btnOk;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panelOTD);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.btnOk);
    this.Controls.Add((Control) this.btnCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (InventoryNumberForm);
    this.FormClosing += new FormClosingEventHandler(this.InventoryNumberForm_FormClosing);
    this.Load += new EventHandler(this.InventoryNumberForm_Load);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.panelOTD.ResumeLayout(false);
    this.panelOTD.PerformLayout();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
