
// Type: Intermech.Navigator.DeleteObjectRelationForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator;

/// <summary>Форма для удаления объектов(заготовок)/связей</summary>
public class DeleteObjectRelationForm : Form
{
  /// <summary>
  /// Источник идентификатора - объект/связь (остальные значения недопустимы)
  /// </summary>
  protected AttributeSourceTypes idSource = AttributeSourceTypes.Object;
  /// <summary>Идентификатор версии объекта/связи</summary>
  protected long id;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected Panel panelBottom;
  protected Button btnClose;
  protected Button btnDelete;
  private Label lbHint;
  private NumericUpDown edID;
  private RadioButton rdObject;
  private RadioButton rdRelation;
  protected Button btInfo;
  private Label lbInfo;
  private TextBox edInfo;

  /// <summary>Стандартный конструктор</summary>
  public DeleteObjectRelationForm()
  {
    this.InitializeComponent();
    this.Init(AttributeSourceTypes.Object, 0L);
  }

  /// <summary>Расширенный конструктор</summary>
  /// <param name="idSource">Источник идентификатора - объект/связь (остальные значения недопустимы)</param>
  /// <param name="id">Идентификатор версии объекта/связи</param>
  public DeleteObjectRelationForm(AttributeSourceTypes idSource, long id)
  {
    this.InitializeComponent();
    this.Init(idSource, id);
  }

  /// <summary>Вызвать форму для удаления объекта(заготовки)/связи</summary>
  /// <returns>Результат вызова формы как модального окна</returns>
  [STAThread]
  public static DialogResult Execute()
  {
    using (DeleteObjectRelationForm objectRelationForm = new DeleteObjectRelationForm())
      return objectRelationForm.ShowDialog();
  }

  /// <summary>Вызвать форму для удаления объекта(заготовки)/связи</summary>
  /// <param name="idSource">Источник идентификатора - объект/связь (остальные значения недопустимы)</param>
  /// <param name="id">Идентификатор версии объекта/связи</param>
  /// <returns>Результат вызова формы как модального окна</returns>
  [STAThread]
  public static DialogResult Execute(AttributeSourceTypes idSource, long id)
  {
    using (DeleteObjectRelationForm objectRelationForm = new DeleteObjectRelationForm(idSource, id))
      return objectRelationForm.ShowDialog();
  }

  /// <summary>Инициализация формы данными</summary>
  /// <param name="idSource">Источник идентификатора - объект/связь (остальные значения недопустимы)</param>
  /// <param name="id">Идентификатор версии объекта/связи</param>
  protected virtual void Init(AttributeSourceTypes idSource, long id)
  {
    this.idSource = idSource;
    this.id = id;
    this.edID.Value = (Decimal) id;
    this.rdObject.Checked = idSource == AttributeSourceTypes.Object;
    this.rdRelation.Checked = idSource == AttributeSourceTypes.Relation;
    this.UpdateControls();
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DeleteObjectRelationForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DeleteObjectRelationForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Обновить состояние контролов</summary>
  protected virtual void UpdateControls()
  {
    this.btInfo.Enabled = true;
    this.btnDelete.Enabled = true;
    this.btnClose.Enabled = true;
  }

  /// <summary>Удаление объекта (заготовки)/связи</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    this.id = Convert.ToInt64(this.edID.Value);
    this.idSource = this.rdObject.Checked ? AttributeSourceTypes.Object : AttributeSourceTypes.Relation;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1547"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      switch (this.idSource)
      {
        case AttributeSourceTypes.Object:
          IDBObject dbObject1 = sessionKeeper.Session.GetObject(this.id);
          int num = dbObject1.ObjectID < 0L ? 1 : 0;
          dbObject1.Delete(0L);
          if (num == 0)
            break;
          IDBObject dbObject2 = sessionKeeper.Session.GetObject(-this.id, false);
          if (dbObject2 == null)
            break;
          dbObject2.Delete(0L);
          break;
        case AttributeSourceTypes.Relation:
          sessionKeeper.Session.GetRelation(this.id).Delete(0L);
          break;
      }
    }
  }

  /// <summary>Получение информации об объекте / связи</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void btInfo_Click(object sender, EventArgs e)
  {
    this.id = Convert.ToInt64(this.edID.Value);
    this.idSource = this.rdObject.Checked ? AttributeSourceTypes.Object : AttributeSourceTypes.Relation;
    string str = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      switch (this.idSource)
      {
        case AttributeSourceTypes.Object:
          IDBObject dbObject = sessionKeeper.Session.GetObject(this.id, false);
          if (dbObject == null)
          {
            str = LocalizationHolder.rm.GetString("Client.Core_1548");
            break;
          }
          str = string.Format(LocalizationHolder.rm.GetString("Client.Core_1549"), dbObject.IsCreationMode ? (object) LocalizationHolder.rm.GetString("Client.Core_1550") : (object) LocalizationHolder.rm.GetString("Client.Core_1356"), (object) dbObject.Caption, (object) dbObject.OwnerID.ToString(), (object) MetaDataHelper.GetObjectTypeName(dbObject.TypeID), dbObject.IsBaseVersion ? (object) LocalizationHolder.rm.GetString("Client.Core_1322") : (object) LocalizationHolder.rm.GetString("Client.Core_1321"));
          break;
        case AttributeSourceTypes.Relation:
          IDBRelation relation = sessionKeeper.Session.GetRelation(this.id, false);
          str = relation != null ? string.Format(LocalizationHolder.rm.GetString("Client.Core_1552"), (object) MetaDataHelper.GetRelationTypeName(relation.TypeID)) : LocalizationHolder.rm.GetString("Client.Core_1551");
          break;
      }
    }
    this.edInfo.Text = str;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DeleteObjectRelationForm));
    this.panelBottom = new Panel();
    this.btInfo = new Button();
    this.btnDelete = new Button();
    this.btnClose = new Button();
    this.lbHint = new Label();
    this.edID = new NumericUpDown();
    this.rdObject = new RadioButton();
    this.rdRelation = new RadioButton();
    this.lbInfo = new Label();
    this.edInfo = new TextBox();
    this.panelBottom.SuspendLayout();
    this.edID.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Controls.Add((Control) this.btInfo);
    this.panelBottom.Controls.Add((Control) this.btnDelete);
    this.panelBottom.Controls.Add((Control) this.btnClose);
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btInfo, "btInfo");
    this.btInfo.Cursor = Cursors.Default;
    this.btInfo.Name = "btInfo";
    this.btInfo.Click += new EventHandler(this.btInfo_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Cursor = Cursors.Default;
    this.btnDelete.DialogResult = DialogResult.OK;
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.btnClose, "btnClose");
    this.btnClose.Cursor = Cursors.Default;
    this.btnClose.DialogResult = DialogResult.Cancel;
    this.btnClose.Name = "btnClose";
    componentResourceManager.ApplyResources((object) this.lbHint, "lbHint");
    this.lbHint.Name = "lbHint";
    componentResourceManager.ApplyResources((object) this.edID, "edID");
    this.edID.Maximum = new Decimal(new int[4]
    {
      -1,
      int.MaxValue,
      0,
      0
    });
    this.edID.Minimum = new Decimal(new int[4]
    {
      0,
      int.MinValue,
      0,
      int.MinValue
    });
    this.edID.Name = "edID";
    componentResourceManager.ApplyResources((object) this.rdObject, "rdObject");
    this.rdObject.Checked = true;
    this.rdObject.Name = "rdObject";
    this.rdObject.TabStop = true;
    this.rdObject.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.rdRelation, "rdRelation");
    this.rdRelation.Name = "rdRelation";
    this.rdRelation.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lbInfo, "lbInfo");
    this.lbInfo.Name = "lbInfo";
    componentResourceManager.ApplyResources((object) this.edInfo, "edInfo");
    this.edInfo.Name = "edInfo";
    this.edInfo.ReadOnly = true;
    this.AcceptButton = (IButtonControl) this.btnDelete;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.btnClose;
    this.Controls.Add((Control) this.edInfo);
    this.Controls.Add((Control) this.lbInfo);
    this.Controls.Add((Control) this.rdRelation);
    this.Controls.Add((Control) this.rdObject);
    this.Controls.Add((Control) this.edID);
    this.Controls.Add((Control) this.lbHint);
    this.Controls.Add((Control) this.panelBottom);
    this.MaximizeBox = false;
    this.Name = nameof (DeleteObjectRelationForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.FormClosed += new FormClosedEventHandler(this.DeleteObjectRelationForm_FormClosed);
    this.Load += new EventHandler(this.DeleteObjectRelationForm_Load);
    this.panelBottom.ResumeLayout(false);
    this.edID.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
