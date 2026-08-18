// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.InventoryNumberPerItem
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
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

public class InventoryNumberPerItem : Form
{
  private IDBTypedObjectID _docItem;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private DateTimePicker _dtRegistrationDate;
  private Label label2;
  private Panel panelOTD;
  private Button btnClassify;
  private Label label1;
  private TextBox textbox;
  private Button btnOk;
  private Button btnCancel;
  private Label labelDocCaption;
  private Panel panel2;
  private Panel panel3;

  public InventoryNumberPerItem() => this.InitializeComponent();

  public void Init(IDBTypedObjectID item)
  {
    this._docItem = item;
    this.labelDocCaption.Text = $"{MetaDataHelper.GetObjectType(item.ObjectType).ObjectName} {item.Caption}";
    ICopiesService customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (ICopiesService)) as ICopiesService;
    this.btnClassify.Enabled = customService != null && customService.Classifiers.Count > 0;
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.textbox.Text == string.Empty)
    {
      int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_163"), ServiceHolder.rm.GetString("Archives_111"), MessageBoxButtons.OK);
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (ICopiesClientService)) is ICopiesClientService))
        throw new Exception(ServiceHolder.rm.GetString("Archives_CopiesClientServiceNotFind"));
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!(sessionKeeper.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService))
          throw new KernelException("Не найден ICopiesService");
        AttributeValues invNumberAttrValues;
        customService.SetInventoryNumberAttributes(sessionKeeper.Session.SessionGUID, this._docItem.ObjectID, this.textbox.Text, this._dtRegistrationDate.Value, out invNumberAttrValues);
        ApplicationServices.Container.GetService<INotificationService>()?.FireEvent((object) this, (NotificationEventArgs) new DBObjectsExtendedEventArgs("ObjectsChanged", this._docItem.ObjectID, this._docItem.ObjectType, new AttributeValues[0], new AttributeValues[1]
        {
          invNumberAttrValues
        }));
      }
      this.Close();
    }
  }

  private void btnClassify_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sk = new SessionKeeper())
    {
      if (!(sk.Session.GetCustomService(typeof (ICopiesService)) is ICopiesService customService1))
        return;
      ISelectionsService customService2 = sk.Session.GetCustomService(typeof (ISelectionsService)) as ISelectionsService;
      long classifierId = this.ChooseClassifier(customService1);
      if (classifierId == 0L)
        return;
      this.textbox.Text = this.GetObjectClassificationNumber(sk, this._docItem.ObjectID, classifierId, customService2);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (InventoryNumberPerItem));
    this.panel1 = new Panel();
    this._dtRegistrationDate = new DateTimePicker();
    this.label2 = new Label();
    this.panelOTD = new Panel();
    this.btnClassify = new Button();
    this.label1 = new Label();
    this.textbox = new TextBox();
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.labelDocCaption = new Label();
    this.panel2 = new Panel();
    this.panel3 = new Panel();
    this.panel1.SuspendLayout();
    this.panelOTD.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel3.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panel1.Controls.Add((Control) this._dtRegistrationDate);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Location = new Point(2, 175);
    this.panel1.Margin = new Padding(6);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(619, 94);
    this.panel1.TabIndex = 11;
    this._dtRegistrationDate.CustomFormat = " dd MMMM yyyy   H:mm";
    this._dtRegistrationDate.Format = DateTimePickerFormat.Custom;
    this._dtRegistrationDate.Location = new Point(18, 43);
    this._dtRegistrationDate.Margin = new Padding(6);
    this._dtRegistrationDate.Name = "_dtRegistrationDate";
    this._dtRegistrationDate.Size = new Size(350, 31 /*0x1F*/);
    this._dtRegistrationDate.TabIndex = 6;
    this.label2.AutoSize = true;
    this.label2.ImeMode = ImeMode.NoControl;
    this.label2.Location = new Point(13, 12);
    this.label2.Margin = new Padding(6, 0, 6, 0);
    this.label2.Name = "label2";
    this.label2.Size = new Size(193, 25);
    this.label2.TabIndex = 3;
    this.label2.Text = "Дата регистрации";
    this.panelOTD.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panelOTD.Controls.Add((Control) this.btnClassify);
    this.panelOTD.Controls.Add((Control) this.label1);
    this.panelOTD.Controls.Add((Control) this.textbox);
    this.panelOTD.Location = new Point(2, 82);
    this.panelOTD.Margin = new Padding(6);
    this.panelOTD.Name = "panelOTD";
    this.panelOTD.Size = new Size(619, 93);
    this.panelOTD.TabIndex = 10;
    this.btnClassify.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnClassify.Enabled = false;
    this.btnClassify.Image = (Image) componentResourceManager.GetObject("btnClassify.Image");
    this.btnClassify.ImeMode = ImeMode.NoControl;
    this.btnClassify.Location = new Point(567, 39);
    this.btnClassify.Margin = new Padding(6);
    this.btnClassify.Name = "btnClassify";
    this.btnClassify.Size = new Size(48 /*0x30*/, 42);
    this.btnClassify.TabIndex = 5;
    this.btnClassify.UseVisualStyleBackColor = true;
    this.btnClassify.Click += new EventHandler(this.btnClassify_Click);
    this.label1.AutoSize = true;
    this.label1.ImeMode = ImeMode.NoControl;
    this.label1.Location = new Point(13, 0);
    this.label1.Margin = new Padding(6, 0, 6, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(216, 25);
    this.label1.TabIndex = 3;
    this.label1.Text = "Инвентарный номер";
    this.textbox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textbox.Location = new Point(18, 42);
    this.textbox.Margin = new Padding(6);
    this.textbox.Name = "textbox";
    this.textbox.Size = new Size(527, 31 /*0x1F*/);
    this.textbox.TabIndex = 1;
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.ImeMode = ImeMode.NoControl;
    this.btnOk.Location = new Point(161, 14);
    this.btnOk.Margin = new Padding(6);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(220, 53);
    this.btnOk.TabIndex = 9;
    this.btnOk.Text = "Применить";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(393, 15);
    this.btnCancel.Margin = new Padding(6);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(220, 52);
    this.btnCancel.TabIndex = 8;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.labelDocCaption.AutoSize = true;
    this.labelDocCaption.Location = new Point(13, 3);
    this.labelDocCaption.Name = "labelDocCaption";
    this.labelDocCaption.Size = new Size(170, 25);
    this.labelDocCaption.TabIndex = 12;
    this.labelDocCaption.Text = "labelDocCaption";
    this.panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.panel2.AutoScroll = true;
    this.panel2.Controls.Add((Control) this.labelDocCaption);
    this.panel2.Location = new Point(2, 6);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(619, 77);
    this.panel2.TabIndex = 13;
    this.panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panel3.AutoScroll = true;
    this.panel3.Controls.Add((Control) this.btnOk);
    this.panel3.Controls.Add((Control) this.btnCancel);
    this.panel3.Location = new Point(2, 278);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(619, 74);
    this.panel3.TabIndex = 14;
    this.AcceptButton = (IButtonControl) this.btnOk;
    this.AutoScaleDimensions = new SizeF(192f, 192f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.AutoScroll = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(624, 354);
    this.Controls.Add((Control) this.panel3);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panelOTD);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.MaximizeBox = false;
    this.MaximumSize = new Size(1500, 425);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(650, 425);
    this.Name = nameof (InventoryNumberPerItem);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Зарегистрировать в ОТД";
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panelOTD.ResumeLayout(false);
    this.panelOTD.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
