// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Common_Dialogs.UCKeywordReplacementDict
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using ImSSP;
using Intermech.Controls.Grid;
using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Common_Dialogs;

public class UCKeywordReplacementDict : UserControl
{
  private long _settingsObjectId = -1;
  private bool _isReadOnly = true;
  private readonly INotificationService _notificationSvc;
  private readonly KeywordReplacementScheme _scheme = new KeywordReplacementScheme();
  private bool _isChanged;
  /// <summary>
  /// Флажок будет установлен в true, если пользователь возьмёт объект на редактирование нажатием кнопки
  /// </summary>
  public bool AutoCheckedOut;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBody;
  public ListGrid lgKeywords;
  private Panel panelBottom;
  protected Panel panelInfo;
  private TextBox textInfo;
  private Button btnCheckOut;
  private PictureBox pictureInfo;

  public long SettingsObjectID
  {
    get => this._settingsObjectId;
    set
    {
      if (this._settingsObjectId == value)
        return;
      this.AssignSettingsObjId(value);
    }
  }

  public bool IsReadonly
  {
    get => this._isReadOnly;
    private set
    {
      this._isReadOnly = value;
      if (this._isReadOnly)
      {
        foreach (ListColumn column in (CollectionBase) this.lgKeywords.Columns)
          column.ActivatedEmbeddedType = ActivatedEmbeddedType.None;
      }
      this.lgKeywords.ActivatedEmbeddedControl = this._isReadOnly ? (Control) null : this.lgKeywords.ActivatedEmbeddedControl;
    }
  }

  public bool IsChanged
  {
    get => this._isChanged;
    private set
    {
      this._isChanged = value;
      EventHandler changedStateChanged = this.IsChangedStateChanged;
      if (changedStateChanged == null)
        return;
      changedStateChanged((object) this, (EventArgs) null);
    }
  }

  public event EventHandler IsChangedStateChanged;

  /// <summary>
  /// Текстовая расшифровка причины невозможности редактирования
  /// </summary>
  public string ReadonlyReason => this.textInfo.Text;

  /// <summary>Конструктор</summary>
  public UCKeywordReplacementDict()
  {
    this.InitializeComponent();
    this.lgKeywords.ClientSizeChanged += (EventHandler) ((s, e) => this.AdjustGridColumnWidths());
    this._notificationSvc = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this.IsChanged = false;
  }

  /// <summary>
  /// Проверка объекта шаблона перед попыткой модификации данных
  /// </summary>
  /// <returns> true если редактирование разрешено </returns>
  public bool IsSettingsObjectReadyToEdit()
  {
    if (this._settingsObjectId.IsUndefinedId())
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._settingsObjectId);
      if (dbObject1 == null)
        return false;
      if (dbObject1.ObjectID < 0L)
      {
        if (dbObject1.CheckoutBy == sessionKeeper.Session.UserID)
          return true;
        int num = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', взят на редактирование пользователем '{sessionKeeper.Session.GetObject(dbObject1.CheckoutBy).Caption}', редактирование недоступно", "Редактирование словаря замен", MessageBoxButtons.OK);
        return false;
      }
      switch (dbObject1.ObjectModifyMode)
      {
        case ObjectModifyModes.InBase:
        case ObjectModifyModes.CreateVersion:
          return true;
        case ObjectModifyModes.Checkout:
          if (MessageBox.Show($"Взять на редактирование объект '{dbObject1.Caption}'? (После завершения редактирования объект будет возвращен в архив)", "Редактирование словаря замен", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return false;
          IDBObject dbObject2 = dbObject1.CheckOut();
          if (dbObject2 == null || dbObject2.CheckoutBy != sessionKeeper.Session.UserID)
            return false;
          this.AutoCheckedOut = true;
          return true;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', в атрибутах которого хранится схема пропуска строк недоступен для редактирования", "Редактирование словаря замен", MessageBoxButtons.OK);
          return false;
        default:
          return false;
      }
    }
  }

  public bool AddItem(string keyword, string replacement, bool updateScheme = false)
  {
    bool flag = keyword == null && replacement == null;
    if (flag)
    {
      AddKeywordReplacementDlg keywordReplacementDlg = new AddKeywordReplacementDlg()
      {
        Keyword = keyword,
        Replacement = replacement
      };
      if (keywordReplacementDlg.ShowDialog() != DialogResult.OK)
        return false;
      keyword = keywordReplacementDlg.Keyword;
      replacement = keywordReplacementDlg.Replacement;
    }
    if (!this._scheme.Validate(keyword, replacement) && updateScheme)
      return false;
    this.lgKeywords.Items.Add(new Intermech.Controls.Grid.ListItem()
    {
      SubItems = {
        keyword,
        replacement
      }
    });
    if (updateScheme)
      this._scheme.Data[keyword] = replacement;
    this.IsChanged = flag;
    return true;
  }

  public void DeleteItem(string keyword = null)
  {
    int num = string.IsNullOrWhiteSpace(keyword) ? this.lgKeywords.Items.GetNextSelectedItemIndex(0) : this.lgKeywords.Items.FindItemIndex(this.lgKeywords.Items.OfType<Intermech.Controls.Grid.ListItem>().FirstOrDefault<Intermech.Controls.Grid.ListItem>((Func<Intermech.Controls.Grid.ListItem, bool>) (li => li.SubItems[0].Text.Equals(keyword, StringComparison.CurrentCulture))));
    if (num < 0)
      return;
    keyword = keyword ?? this.lgKeywords.Items[num].SubItems[0].Text;
    this.lgKeywords.Items.Remove(num);
    this._scheme.Remove(keyword);
    this.IsChanged = true;
  }

  public void SetToDefault()
  {
    this._scheme.SetDefault();
    this.UpdateGrid();
    this.IsChanged = true;
  }

  public void SaveData()
  {
    if (!this.IsChanged)
      return;
    this.UpdateScheme();
    this._scheme.SaveToDBObjectAttribute(this._settingsObjectId, AvsIDCache.Attr_DynamicHeaderKeywordReplacementSchema);
    this.IsChanged = false;
  }

  /// <summary>
  /// Взять на изменение объект "Основной шаблон спецификаций"
  /// </summary>
  internal void CheckOutMainSpecTemplatePressed()
  {
    if (this._settingsObjectId <= 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(Math.Abs(this._settingsObjectId), true);
      if ((objectActual.ObjectModifyMode == ObjectModifyModes.InBase ? 1 : (objectActual.ObjectModifyMode != ObjectModifyModes.Checkout ? 0 : (objectActual.CheckoutBy == sessionKeeper.Session.UserID ? 1 : 0))) == 0 && objectActual.CheckoutBy != sessionKeeper.Session.UserID && objectActual.CheckoutBy != 0L)
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectActual.CheckoutBy);
        int num = (int) MessageBox.Show($"Редактировать список атрибутов и изменять параметры нельзя. Объект \"{objectActual.Caption}\" взят на редактирование пользователем \"{objectInfo.Caption}\".", sc_967.ssp_avs_968(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
      else
      {
        long objectId1 = objectActual.ObjectID;
        IDBObject dbObject = objectActual.CheckOut(true);
        this._settingsObjectId = dbObject.ObjectID;
        long objectId2 = dbObject.ObjectID;
        List<long> objectIDs = new List<long>(1);
        List<long> newObjectIDs = new List<long>(1);
        objectIDs.Add(objectId1);
        newObjectIDs.Add(objectId2);
        this.AutoCheckedOut = true;
        this.IsReadonly = false;
        this.btnCheckOut.Enabled = false;
        this.textInfo.Text = "";
        if (objectId1 == 0L)
          return;
        this._notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsCheckOutEventArgs("ObjectsCheckedOut", (IList<long>) objectIDs, (IList<long>) newObjectIDs));
      }
    }
  }

  /// <summary>Отменить изменения в объекте</summary>
  internal void RollbackChangesInSettingsObject()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this._settingsObjectId, true);
      long objectID = 0;
      if (objectActual.CheckoutBy == sessionKeeper.Session.UserID)
      {
        objectID = objectActual.ObjectID;
        objectActual.CancelChanges();
      }
      this._settingsObjectId = objectActual.ObjectID;
      this.AutoCheckedOut = false;
      this.IsReadonly = true;
      this.btnCheckOut.Enabled = true;
      if (objectID == 0L)
        return;
      this._notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsChangesCancelled", objectID));
    }
  }

  /// <summary>
  /// Завершить изменения в объекте "Основной шаблон спецификаций"
  /// </summary>
  internal void CheckInSettingsObject()
  {
    if (this._settingsObjectId <= 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this._settingsObjectId, true);
      long objectID = 0;
      if (objectActual.CheckoutBy == sessionKeeper.Session.UserID)
      {
        objectID = objectActual.ObjectID;
        objectActual.CheckIn();
      }
      this._settingsObjectId = objectActual.ObjectID;
      this.AutoCheckedOut = false;
      this.IsReadonly = true;
      this.btnCheckOut.Enabled = true;
      if (objectID == 0L)
        return;
      this._notificationSvc.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCheckedIn", objectID));
    }
  }

  private void UpdateGrid()
  {
    this.lgKeywords.Items.Clear();
    foreach (string key in this._scheme.Data.Keys)
      this.AddItem(key, this._scheme.Data[key]);
  }

  private void AdjustGridColumnWidths()
  {
    this.lgKeywords.Columns[0].Width = this.lgKeywords.ClientSize.Width / 2 - 5;
    this.lgKeywords.Columns[1].Width = this.lgKeywords.ClientSize.Width / 2;
  }

  private void AssignSettingsObjId(long value)
  {
    if (value.IsUndefinedId())
      return;
    this._settingsObjectId = value;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActual = sessionKeeper.Session.GetObjectActual(this._settingsObjectId, true);
      this._settingsObjectId = objectActual.ObjectID;
      this.LoadData(sessionKeeper.Session);
      long checkoutBy = objectActual.CheckoutBy;
      this.IsReadonly = objectActual.ObjectModifyMode == ObjectModifyModes.Checkout && checkoutBy > 0L && objectActual.CheckoutBy != sessionKeeper.Session.UserID;
      if (!this.IsReadonly)
        return;
      this.btnCheckOut.Enabled = false;
      QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(objectActual.CheckoutBy);
      this.textInfo.Text = $"Редактировать список и изменять параметры нельзя. Объект \"{objectActual.Caption}\" взят на редактирование пользователем \"{objectInfo.Caption}\".";
      this.textInfo.SelectionLength = 0;
    }
  }

  private void LoadData(IUserSession session)
  {
    this._scheme.LoadFromDBObjectAttribute(this._settingsObjectId, AvsIDCache.Attr_DynamicHeaderKeywordReplacementSchema, session);
    if (this._scheme.Data.Count == 0)
      this._scheme.SetDefault();
    this.UpdateGrid();
    this.IsChanged = false;
  }

  private void UpdateScheme()
  {
    this._scheme.Data = this.lgKeywords.Items.OfType<Intermech.Controls.Grid.ListItem>().Select<Intermech.Controls.Grid.ListItem, KeyValuePair<string, string>>((Func<Intermech.Controls.Grid.ListItem, KeyValuePair<string, string>>) (li => new KeyValuePair<string, string>(li.SubItems[0].Text, li.SubItems[1].Text))).ToList<KeyValuePair<string, string>>().ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (kvp => kvp.Key), (Func<KeyValuePair<string, string>, string>) (kvp => kvp.Value));
  }

  private void btnCheckOut_Click(object sender, EventArgs e)
  {
    this.CheckOutMainSpecTemplatePressed();
  }

  private void lgKeywords_ItemChanged(object source, ChangedEventArgs e)
  {
    if (e.Item == null || e.ChangedType != ChangedType.SubItemChanged || !this.IsSettingsObjectReadyToEdit())
      return;
    this._scheme.AddOrUpdate(e.Item.SubItems[0].Text, e.Item.SubItems[1].Text);
    this.IsChanged = true;
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
    ListColumn listColumn1 = new ListColumn();
    ListColumn listColumn2 = new ListColumn();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UCKeywordReplacementDict));
    this.panelBody = new Panel();
    this.lgKeywords = new ListGrid();
    this.panelBottom = new Panel();
    this.panelInfo = new Panel();
    this.textInfo = new TextBox();
    this.btnCheckOut = new Button();
    this.pictureInfo = new PictureBox();
    this.panelBody.SuspendLayout();
    this.panelBottom.SuspendLayout();
    this.panelInfo.SuspendLayout();
    ((ISupportInitialize) this.pictureInfo).BeginInit();
    this.SuspendLayout();
    this.panelBody.Controls.Add((Control) this.lgKeywords);
    this.panelBody.Dock = DockStyle.Fill;
    this.panelBody.Location = new Point(0, 0);
    this.panelBody.Name = "panelBody";
    this.panelBody.Padding = new Padding(17, 5, 17, 5);
    this.panelBody.Size = new Size(461, 385);
    this.panelBody.TabIndex = 2;
    this.lgKeywords.AlternateBackground = Color.DarkGreen;
    this.lgKeywords.BackColor = SystemColors.ControlLightLight;
    listColumn1.ActivatedEmbeddedType = ActivatedEmbeddedType.TextBox;
    listColumn1.Name = "KeyWord";
    listColumn1.Text = "Ключевое слово";
    listColumn1.Width = 120;
    listColumn2.ActivatedEmbeddedType = ActivatedEmbeddedType.TextBox;
    listColumn2.Name = "Replacement";
    listColumn2.Text = "Заменитель";
    listColumn2.Width = 120;
    this.lgKeywords.Columns.AddRange(new ListColumn[2]
    {
      listColumn1,
      listColumn2
    });
    this.lgKeywords.Dock = DockStyle.Fill;
    this.lgKeywords.FullRowSelect = false;
    this.lgKeywords.GridColor = Color.LightGray;
    this.lgKeywords.HeaderHeight = 22;
    this.lgKeywords.HeaderStyle = HeaderStyle.Flat;
    this.lgKeywords.HotTrackingColor = Color.LightGray;
    this.lgKeywords.ImageList = (ImageList) null;
    this.lgKeywords.ItemHeight = 17;
    this.lgKeywords.ItemWordWrap = true;
    this.lgKeywords.Location = new Point(17, 5);
    this.lgKeywords.Margin = new Padding(17, 3, 17, 3);
    this.lgKeywords.Name = "lgKeywords";
    this.lgKeywords.Padding = new Padding(17, 0, 50, 0);
    this.lgKeywords.SelectedTextColor = Color.White;
    this.lgKeywords.SelectionColor = Color.DarkBlue;
    this.lgKeywords.Size = new Size(427, 375);
    this.lgKeywords.SuperFlatHeaderColor = Color.White;
    this.lgKeywords.TabIndex = 2;
    this.lgKeywords.Text = "RowList";
    this.lgKeywords.ItemChanged += new ChangedEventHandler(this.lgKeywords_ItemChanged);
    this.panelBottom.Controls.Add((Control) this.panelInfo);
    this.panelBottom.Dock = DockStyle.Bottom;
    this.panelBottom.Location = new Point(0, 380);
    this.panelBottom.Name = "panelBottom";
    this.panelBottom.Padding = new Padding(17, 3, 17, 3);
    this.panelBottom.Size = new Size(461, 5);
    this.panelBottom.TabIndex = 3;
    this.panelBottom.Visible = false;
    this.panelInfo.BackColor = SystemColors.Info;
    this.panelInfo.BorderStyle = BorderStyle.Fixed3D;
    this.panelInfo.Controls.Add((Control) this.textInfo);
    this.panelInfo.Controls.Add((Control) this.btnCheckOut);
    this.panelInfo.Controls.Add((Control) this.pictureInfo);
    this.panelInfo.Dock = DockStyle.Bottom;
    this.panelInfo.ForeColor = SystemColors.InfoText;
    this.panelInfo.Location = new Point(17, -70);
    this.panelInfo.Name = "panelInfo";
    this.panelInfo.Size = new Size(427, 72);
    this.panelInfo.TabIndex = 1;
    this.textInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textInfo.BackColor = SystemColors.Info;
    this.textInfo.ForeColor = SystemColors.InfoText;
    this.textInfo.Location = new Point(30, 3);
    this.textInfo.Multiline = true;
    this.textInfo.Name = "textInfo";
    this.textInfo.ReadOnly = true;
    this.textInfo.ScrollBars = ScrollBars.Vertical;
    this.textInfo.Size = new Size(256 /*0x0100*/, 65);
    this.textInfo.TabIndex = 2;
    this.btnCheckOut.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnCheckOut.Cursor = Cursors.Default;
    this.btnCheckOut.Enabled = false;
    this.btnCheckOut.FlatStyle = FlatStyle.System;
    this.btnCheckOut.ImageAlign = ContentAlignment.MiddleLeft;
    this.btnCheckOut.ImeMode = ImeMode.NoControl;
    this.btnCheckOut.Location = new Point(292, 9);
    this.btnCheckOut.Name = "btnCheckOut";
    this.btnCheckOut.Size = new Size(121, 54);
    this.btnCheckOut.TabIndex = 0;
    this.btnCheckOut.Text = "Взять на редактирование";
    this.btnCheckOut.Click += new EventHandler(this.btnCheckOut_Click);
    this.pictureInfo.BackColor = SystemColors.Info;
    this.pictureInfo.Dock = DockStyle.Left;
    this.pictureInfo.Image = (Image) componentResourceManager.GetObject("pictureInfo.Image");
    this.pictureInfo.ImeMode = ImeMode.NoControl;
    this.pictureInfo.Location = new Point(0, 0);
    this.pictureInfo.Name = "pictureInfo";
    this.pictureInfo.Size = new Size(28, 68);
    this.pictureInfo.SizeMode = PictureBoxSizeMode.CenterImage;
    this.pictureInfo.TabIndex = 1;
    this.pictureInfo.TabStop = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.panelBody);
    this.Name = nameof (UCKeywordReplacementDict);
    this.Size = new Size(461, 385);
    this.panelBody.ResumeLayout(false);
    this.panelBottom.ResumeLayout(false);
    this.panelInfo.ResumeLayout(false);
    this.panelInfo.PerformLayout();
    ((ISupportInitialize) this.pictureInfo).EndInit();
    this.ResumeLayout(false);
  }
}
