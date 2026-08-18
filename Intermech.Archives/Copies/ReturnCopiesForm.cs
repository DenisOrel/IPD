// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.ReturnCopiesForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Archives.Common;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Форма для возврата копий</summary>
public class ReturnCopiesForm : Form
{
  /// <summary>Кто вернул копии</summary>
  private long _recID;
  /// <summary>Дата возврата копий</summary>
  private DateTime _returnDate = DateTime.Now;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DateTimePicker dateTimePicker1;
  private Label label1;
  private Label label2;
  private ButtonEdit beRecipient;
  private Button btnOK;
  private Button btnCancel;

  /// <summary>Кто вернул копии</summary>
  public long RecID
  {
    get => this._recID;
    set => this._recID = value;
  }

  /// <summary>Дата возврата копий</summary>
  public DateTime ReturnDate
  {
    get => this._returnDate;
    set => this._returnDate = value;
  }

  /// <summary>Конструктор</summary>
  /// <param name="copyID">ИД копии. Если форма вызывается для нескольких копий ИД = NavigatorUndefinedObjectID</param>
  public ReturnCopiesForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2650);
  }

  public void Init(long copyID, long subscriberID)
  {
    this.dateTimePicker1.Value = DateTime.Now;
    if (copyID == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.Configurations.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.RECIPIENT_RETURN_COPY, true) == "True"))
        return;
      IDBAttribute objectAttributeById = sessionKeeper.Session.GetObjectAttributeByID(copyID, ConstsHolder.RecipientID);
      if (objectAttributeById == null)
        return;
      this._recID = objectAttributeById.AsInteger;
      if (this._recID == 0L)
        return;
      this.beRecipient.Text = objectAttributeById.AsString;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.beRecipient.Text == string.Empty)
    {
      int num = (int) IMMessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_113"), MessageBoxButtonsAdv.OK, IMMessageBoxImage.Warning);
    }
    else
    {
      this.ReturnDate = this.dateTimePicker1.Value;
      this.DialogResult = DialogResult.OK;
    }
  }

  /// <summary>выбор пользователя, вернувшего копии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void beRecipient_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_114"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1 || !(objArray[0] is IDBTypedObjectID dbTypedObjectId))
      return;
    this._recID = dbTypedObjectId.ObjectID;
    this.beRecipient.Text = dbTypedObjectId.Caption;
  }

  /// <summary>Загрузка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ReturnCopiesForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохранение</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ReturnCopiesForm_FormClosing(object sender, FormClosingEventArgs e)
  {
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
    this.dateTimePicker1 = new DateTimePicker();
    this.label1 = new Label();
    this.label2 = new Label();
    this.beRecipient = new ButtonEdit();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.beRecipient.Properties.BeginInit();
    this.SuspendLayout();
    this.dateTimePicker1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.dateTimePicker1.Location = new Point(12, 76);
    this.dateTimePicker1.Name = "dateTimePicker1";
    this.dateTimePicker1.Size = new Size(245, 20);
    this.dateTimePicker1.TabIndex = 0;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 57);
    this.label1.Name = "label1";
    this.label1.Size = new Size(83, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Дата возврата";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(9, 9);
    this.label2.Name = "label2";
    this.label2.Size = new Size(98, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "Кто вернул копию";
    this.beRecipient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.beRecipient.EditValue = (object) "";
    this.beRecipient.Location = new Point(12, 28);
    this.beRecipient.Name = "beRecipient";
    this.beRecipient.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beRecipient.Properties.ReadOnly = true;
    this.beRecipient.Size = new Size(245, 20);
    this.beRecipient.TabIndex = 3;
    this.beRecipient.ButtonClick += new ButtonPressedEventHandler(this.beRecipient_ButtonClick);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.Location = new Point(72, 115);
    this.btnOK.MinimumSize = new Size(75, 23);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(90, 27);
    this.btnOK.TabIndex = 4;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(168, 115);
    this.btnCancel.MinimumSize = new Size(75, 23);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(90, 27);
    this.btnCancel.TabIndex = 5;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(269, 153);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.beRecipient);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.dateTimePicker1);
    this.MaximizeBox = false;
    this.MaximumSize = new Size(600, 192 /*0xC0*/);
    this.MinimizeBox = false;
    this.MinimumSize = new Size(277, 192 /*0xC0*/);
    this.Name = nameof (ReturnCopiesForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Вернуть копии";
    this.FormClosing += new FormClosingEventHandler(this.ReturnCopiesForm_FormClosing);
    this.Load += new EventHandler(this.ReturnCopiesForm_Load);
    this.beRecipient.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
