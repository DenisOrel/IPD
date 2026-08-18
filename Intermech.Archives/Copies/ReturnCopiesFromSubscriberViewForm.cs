// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.ReturnCopiesFromSubscriberViewForm
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Форма возвращения копий, вызываемая со вкладки Абонентов
/// </summary>
public class ReturnCopiesFromSubscriberViewForm : Form
{
  /// <summary>
  /// Верувший копии == получатель копии (берется из настроек ипса)
  /// </summary>
  private bool _isRecipientReturnsCopies;
  private List<MyElement> _copies = new List<MyElement>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnDelete;
  private Button btnAdd;
  private ListBox lbCopies;
  private Label label3;
  private Button btnCancel;
  private Button btnOK;
  private ButtonEdit beRecipient;
  private Label label2;
  private Label label1;
  private DateTimePicker dateTimePicker1;

  /// <summary>Дата возврата копий</summary>
  public DateTime ReturnDate { get; private set; }

  /// <summary>Какие копии возвращаем</summary>
  public List<long> CopiesToReturn { get; private set; }

  /// <summary>Кто возвращает копии</summary>
  public long WhoReturnsCopies { get; private set; }

  public ReturnCopiesFromSubscriberViewForm() => this.InitializeComponent();

  /// <summary>Инициализация формы</summary>
  /// <param name="copies">Копии документа</param>
  /// <param name="subsciberId">ИД абонента, которому была выслана копия</param>
  public void Init(List<MyElement> copies)
  {
    this.WhoReturnsCopies = 0L;
    this._copies = copies;
    this.InitCopiesListBox();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this._isRecipientReturnsCopies = sessionKeeper.Session.Configurations.ReadStringNoCache(ConstsHolder.MODULE_NAME, ConstsHolder.SETTINGS, ConstsHolder.RECIPIENT_RETURN_COPY, true) == "True";
    this.InitWhoReturnsCopies();
    this.dateTimePicker1.Value = DateTime.Now;
  }

  /// <summary>
  /// Инициализируем возвращателя копий, учитывая настройку на автозаполнение и одинаковость получателя копий
  /// </summary>
  private void InitWhoReturnsCopies()
  {
    if (!this._isRecipientReturnsCopies)
      return;
    if (this.IsCopiesRecipientsAreEqual())
    {
      long int64 = Convert.ToInt64(((MyElement) ((MyElement) this.lbCopies.Items[0]).Tag).Value);
      this.beRecipient.Text = ((MyElement) ((MyElement) this.lbCopies.Items[0]).Tag).Caption;
      this.WhoReturnsCopies = int64;
      this.btnOK.Enabled = true;
    }
    else
    {
      this.beRecipient.Text = string.Empty;
      this.WhoReturnsCopies = 0L;
      this.btnOK.Enabled = true;
    }
  }

  /// <summary>
  /// Определяет, одинаковы ли получатели у копий, отображенных в листбоксе
  /// </summary>
  /// <returns></returns>
  private bool IsCopiesRecipientsAreEqual()
  {
    if (this.lbCopies.Items.Count == 0)
      return false;
    MyElement[] array = this.lbCopies.Items.Cast<MyElement>().ToArray<MyElement>();
    for (int index = 0; index < ((IEnumerable<MyElement>) array).Count<MyElement>() - 1; ++index)
    {
      if (Convert.ToInt64(((MyElement) array[index].Tag).Value) != Convert.ToInt64(((MyElement) array[index + 1].Tag).Value))
        return false;
    }
    return true;
  }

  private void InitCopiesListBox()
  {
    foreach (object copy in this._copies)
      this.lbCopies.Items.Add(copy);
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.beRecipient.Text == string.Empty)
    {
      int num = (int) MessageBox.Show(ServiceHolder.rm.GetString("Archives_111"), ServiceHolder.rm.GetString("Archives_113"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      this.ReturnDate = this.dateTimePicker1.Value;
      this.CopiesToReturn = this.lbCopies.Items.Cast<MyElement>().Select<MyElement, long>((Func<MyElement, long>) (x => (long) x.Value)).ToList<long>();
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    IDescriptor rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, ConstsHolder.CopyOfDocumentID, ServiceHolder.rm.GetString("Archives_210"), (IList) this._copies.Select<MyElement, long>((Func<MyElement, long>) (x => (long) x.Value)).ToList<long>());
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_211"), rootDescriptor, typeof (IDBTypedObjectID), SelectionOptions.HideTree | SelectionOptions.SelectObjects | SelectionOptions.ForceRebuildNavTree);
    if (objArray == null || objArray.Length == 0)
      return;
    foreach (object obj in objArray)
    {
      IDBTypedObjectID curCopy;
      if ((curCopy = obj as IDBTypedObjectID) != null)
      {
        MyElement myElement = this._copies.First<MyElement>((Func<MyElement, bool>) (x => Convert.ToInt64(x.Value) == curCopy.ObjectID));
        if (!this.lbCopies.Items.Contains((object) myElement))
          this.lbCopies.Items.Add((object) myElement);
      }
    }
    this.InitWhoReturnsCopies();
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (this.lbCopies.SelectedItem == null)
      return;
    this.lbCopies.Items.RemoveAt(this.lbCopies.SelectedIndex);
  }

  private void lbCopies_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lbCopies.SelectedItem != null)
      this.btnDelete.Enabled = true;
    else
      this.btnDelete.Enabled = false;
    if (this.lbCopies.Items.Count == 0)
    {
      this.btnOK.Enabled = false;
    }
    else
    {
      if (this.WhoReturnsCopies == 0L)
        return;
      this.btnOK.Enabled = true;
    }
  }

  private void beRecipient_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    object[] objArray = SelectionWindow.Select(ServiceHolder.rm.GetString("Archives_114"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00002-306c-11d8-b4e9-00304f19f545")), typeof (IDBTypedObjectID), SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length != 1 || !(objArray[0] is IDBTypedObjectID dbTypedObjectId))
      return;
    this.beRecipient.Text = dbTypedObjectId.Caption;
    this.WhoReturnsCopies = dbTypedObjectId.ObjectID;
    this.btnOK.Enabled = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ReturnCopiesFromSubscriberViewForm));
    this.btnDelete = new Button();
    this.btnAdd = new Button();
    this.lbCopies = new ListBox();
    this.label3 = new Label();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.beRecipient = new ButtonEdit();
    this.label2 = new Label();
    this.label1 = new Label();
    this.dateTimePicker1 = new DateTimePicker();
    this.beRecipient.Properties.BeginInit();
    this.SuspendLayout();
    this.btnDelete.Enabled = false;
    this.btnDelete.Image = (Image) componentResourceManager.GetObject("btnDelete.Image");
    this.btnDelete.ImageAlign = ContentAlignment.BottomCenter;
    this.btnDelete.Location = new Point(45, 26);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(24, 24);
    this.btnDelete.TabIndex = 37;
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.btnAdd.Image = (Image) componentResourceManager.GetObject("btnAdd.Image");
    this.btnAdd.ImageAlign = ContentAlignment.BottomCenter;
    this.btnAdd.Location = new Point(15, 26);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(24, 24);
    this.btnAdd.TabIndex = 36;
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.lbCopies.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbCopies.FormattingEnabled = true;
    this.lbCopies.HorizontalScrollbar = true;
    this.lbCopies.Location = new Point(16 /*0x10*/, 54);
    this.lbCopies.Name = "lbCopies";
    this.lbCopies.Size = new Size(382, 134);
    this.lbCopies.TabIndex = 35;
    this.lbCopies.SelectedIndexChanged += new EventHandler(this.lbCopies_SelectedIndexChanged);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(13, 8);
    this.label3.Name = "label3";
    this.label3.Size = new Size(126, 13);
    this.label3.TabIndex = 34;
    this.label3.Text = "Возвращаемые копии :";
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(309, 306);
    this.btnCancel.MinimumSize = new Size(75, 23);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(90, 27);
    this.btnCancel.TabIndex = 33;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.Enabled = false;
    this.btnOK.Location = new Point(213, 306);
    this.btnOK.MinimumSize = new Size(75, 23);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(90, 27);
    this.btnOK.TabIndex = 32 /*0x20*/;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.beRecipient.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.beRecipient.EditValue = (object) "";
    this.beRecipient.Location = new Point(16 /*0x10*/, 215);
    this.beRecipient.Name = "beRecipient";
    this.beRecipient.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.beRecipient.Properties.ReadOnly = true;
    this.beRecipient.Size = new Size(382, 20);
    this.beRecipient.TabIndex = 31 /*0x1F*/;
    this.beRecipient.ButtonClick += new ButtonPressedEventHandler(this.beRecipient_ButtonClick);
    this.label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(13, 196);
    this.label2.Name = "label2";
    this.label2.Size = new Size(99, 13);
    this.label2.TabIndex = 30;
    this.label2.Text = "Кто вернул копии:";
    this.label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(13, 244);
    this.label1.Name = "label1";
    this.label1.Size = new Size(86, 13);
    this.label1.TabIndex = 29;
    this.label1.Text = "Дата возврата:";
    this.dateTimePicker1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.dateTimePicker1.Location = new Point(16 /*0x10*/, 263);
    this.dateTimePicker1.Name = "dateTimePicker1";
    this.dateTimePicker1.Size = new Size(382, 20);
    this.dateTimePicker1.TabIndex = 28;
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(410, 345);
    this.Controls.Add((Control) this.btnDelete);
    this.Controls.Add((Control) this.btnAdd);
    this.Controls.Add((Control) this.lbCopies);
    this.Controls.Add((Control) this.label3);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.beRecipient);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.dateTimePicker1);
    this.MinimumSize = new Size(316, 326);
    this.Name = nameof (ReturnCopiesFromSubscriberViewForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Возврат копий абоненту";
    this.beRecipient.Properties.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
