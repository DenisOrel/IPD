// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SetupNumberingSchemaControl
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary>Панель настройки схемы нумерации позиций (полная)</summary>
public class SetupNumberingSchemaControl : ExtUserControl, IView
{
  public Button btnCancel;
  public Button btnOK;
  private ToolTipController EditModeToolTip;
  private ToolTipController ReadModeToolTip;
  private SpecifNumberingControlFull specifNumberingControlFull;
  private IContainer components;
  public static int PageImageIndex = -1;
  private long _SchemaObjectID = -1;
  private int _SchemaObjectType = -1;
  private long _SchemaTemplateID = -1;
  public SpecifNumberingFull _SpecifNumberingFull;
  private bool _Loaded;
  private INotificationService _INotificationService;
  private NotificationEventHandler _ObjectWasCheckedOutHandler;
  private NotificationEventHandler _ObjectWasCheckedInHandler;
  private NotificationEventHandler _ObjectChangesWasCanceledHandler;
  private bool _NeedToAutoCheckIn;

  public SetupNumberingSchemaControl()
  {
    this.InitializeComponent();
    this._ObjectWasCheckedOutHandler = new NotificationEventHandler(this.ObjectWasCheckedOut);
    this._ObjectWasCheckedInHandler = new NotificationEventHandler(this.ObjectWasCheckedIn);
    this._ObjectChangesWasCanceledHandler = new NotificationEventHandler(this.ObjectChangesWasCanceled);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.btnCancel = new Button();
    this.btnOK = new Button();
    this.specifNumberingControlFull = new SpecifNumberingControlFull();
    this.EditModeToolTip = new ToolTipController(this.components);
    this.ReadModeToolTip = new ToolTipController(this.components);
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Enabled = false;
    this.btnCancel.FlatStyle = FlatStyle.System;
    this.btnCancel.Location = new Point(517, 227);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 2;
    this.btnCancel.Text = "Отмена";
    this.EditModeToolTip.SetToolTip((Control) this.btnCancel, "Отменить правки, произведенные в настройках нумерации");
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Enabled = false;
    this.btnOK.FlatStyle = FlatStyle.System;
    this.btnOK.Location = new Point(390, 227);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 1;
    this.btnOK.Text = "Применить";
    this.EditModeToolTip.SetToolTip((Control) this.btnOK, "Сохранить изменения настроек нумерации позиций в БД");
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.specifNumberingControlFull.AutoScroll = true;
    this.specifNumberingControlFull.AutoScrollMinSize = new Size(601, 230);
    this.specifNumberingControlFull.Dock = DockStyle.Fill;
    this.specifNumberingControlFull.Location = new Point(0, 0);
    this.specifNumberingControlFull.Name = "specifNumberingControlFull";
    this.specifNumberingControlFull.Size = new Size(651, (int) byte.MaxValue);
    this.specifNumberingControlFull.SpecificationTemplateObjectId = -1L;
    this.specifNumberingControlFull.TabIndex = 0;
    this.EditModeToolTip.Active = false;
    this.EditModeToolTip.Style = new ViewStyle("ToolTip style");
    this.ReadModeToolTip.Style = new ViewStyle("ToolTip style");
    this.AutoScrollMinSize = new Size(589, 230);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.specifNumberingControlFull);
    this.Name = nameof (SetupNumberingSchemaControl);
    this.Size = new Size(651, (int) byte.MaxValue);
    this.ResumeLayout(false);
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (this._INotificationService != null)
    {
      this._INotificationService.Unsubscribe("ObjectsCheckedOut", this._ObjectWasCheckedOutHandler);
      this._INotificationService.Unsubscribe("ObjectsCheckedIn", this._ObjectWasCheckedInHandler);
      this._INotificationService.Unsubscribe("ObjectsChangesCancelled", this._ObjectChangesWasCanceledHandler);
    }
    base.Dispose(disposing);
  }

  public int ImageIndex => SetupNumberingSchemaControl.PageImageIndex;

  public int OrderID => 0;

  public string Caption => "Схема нумерации позиций";

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    if (items.Count != 1)
      return;
    if (this._INotificationService != null)
    {
      this._INotificationService = (INotificationService) provider.GetService(typeof (INotificationService));
      if (this._INotificationService != null)
      {
        this._INotificationService.Subscribe("ObjectsCheckedOut", this._ObjectWasCheckedOutHandler);
        this._INotificationService.Subscribe("ObjectsCheckedIn", this._ObjectWasCheckedInHandler);
        this._INotificationService.Subscribe("ObjectsChangesCancelled", this._ObjectChangesWasCanceledHandler);
      }
    }
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    this._SchemaObjectID = itemData.ObjectID;
    this._SchemaObjectType = itemData.ObjectType;
    this._SchemaTemplateID = -1L;
    this._Loaded = false;
  }

  public void Deactivate(IView nextView)
  {
    if (this.Changed && MessageBox.Show("Применить изменения в схеме нумерации позиций?", "Схема нумерации позиций", MessageBoxButtons.YesNo) == DialogResult.Yes)
      this.SaveNumberingSchema();
    if (!this._NeedToAutoCheckIn)
      return;
    this.CheckInObj();
  }

  /// <summary> Вызывается после загрузки панели  </summary>
  /// <param name="previousView"></param>
  public void Activate(IView previousView)
  {
    if (this._Loaded)
      return;
    this.InitNumberingSchema();
    this._Loaded = true;
  }

  /// <summary> Инициализация данных </summary>
  public void InitNumberingSchema()
  {
    this.LockControls();
    try
    {
      this._SpecifNumberingFull = this.LoadNumberingSchema();
      this.specifNumberingControlFull.SpecifNumberingFull = this._SpecifNumberingFull;
      this._NeedToAutoCheckIn = false;
      this.Changed = false;
      this.RefreshReadOnly();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary>Создание объекта "Схема нумерации позиций в спецификации" применимую к объекту, для которого открыта данная панель</summary>
  /// <returns>Объект "Схема нумерации позиций в спецификации"</returns>
  public SpecifNumberingFull LoadNumberingSchema()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return (SpecifNumberingFull) SettingsStructure.StaticCreateSettingsStructureFromObject(sessionKeeper.Session, this._SchemaObjectID, this._SchemaObjectType, this._SchemaTemplateID, AvsIDCache.Attr_NumberingSchema, typeof (SpecifNumberingFull), (List<Triple>) null);
  }

  /// <summary> Обработка нажатия на кнопку "Применить" </summary>
  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.ReadOnly)
    {
      this.RefreshReadOnly();
      this.UpdateControls(false);
    }
    else
    {
      this.LockControls();
      try
      {
        this.SaveNumberingSchema();
        if (this._NeedToAutoCheckIn)
          this.CheckInObj();
        this.Changed = false;
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  /// <summary> Обработка нажатия на кнопку "Отменить" </summary>
  private void btnCancel_Click(object sender, EventArgs e)
  {
    if (this._NeedToAutoCheckIn)
      this.CheckInObj();
    else
      this.InitNumberingSchema();
  }

  /// <summary> Сохранение изменений </summary>
  public void SaveNumberingSchema()
  {
    if (!this._Loaded)
      return;
    this.LockControls();
    try
    {
      this._SpecifNumberingFull.SaveParams();
      this.UpdateControls(true);
    }
    finally
    {
      this.UnlockControls();
    }
  }

  /// <summary> Возвращает объект, содержащий в своём атрибуте настройки нумерации, в архив </summary>
  protected void CheckInObj()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._SchemaObjectID);
      if (dbObject == null || dbObject.CheckoutBy != sessionKeeper.Session.UserID)
        return;
      dbObject.CheckIn();
      this._SchemaObjectID = dbObject.ObjectID;
      this._SchemaObjectID = Math.Abs(this._SchemaObjectID);
      this.InitNumberingSchema();
    }
  }

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    this.btnOK.Enabled = this.Changed && !this.ReadOnly;
    this.btnCancel.Enabled = this.Changed && !this.ReadOnly;
    if (this.EditModeToolTip == null)
      return;
    if (this.ReadOnly)
    {
      if (!this.EditModeToolTip.Active)
        return;
      this.EditModeToolTip.Active = false;
      this.ReadModeToolTip.Active = true;
    }
    else
    {
      if (!this.ReadModeToolTip.Active)
        return;
      this.ReadModeToolTip.Active = false;
      this.EditModeToolTip.Active = true;
    }
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this._SpecifNumberingFull == null || this._SpecifNumberingFull.ReadOnly;
  }

  /// <summary>
  /// Вызывается, когда требуется проверка перед попыткой модификации данных
  /// Например, когда у пользователя необходимо запросить разрешение на взятие
  /// на редактирование некоторого объекта
  /// </summary>
  /// <returns> true если редактирование разрешено </returns>
  protected override bool BeforeObjectEditBegin(ref bool wasUpdated)
  {
    wasUpdated = false;
    if (this._SpecifNumberingFull == null)
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject1 = sessionKeeper.Session.GetObject(this._SchemaObjectID);
      if (dbObject1 == null || dbObject1.GetAttributeByID(AvsIDCache.Attr_NumberingSchema) == null)
        return false;
      if (dbObject1.ObjectID < 0L)
      {
        if (dbObject1.CheckoutBy == sessionKeeper.Session.UserID)
          return true;
        int num = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', взят на редактирование пользователем '{sessionKeeper.Session.GetObject(dbObject1.CheckoutBy).Caption}, редактирование недоступно", "Редактирование схемы нумерации позиций", MessageBoxButtons.OK);
        wasUpdated = true;
        this.InitNumberingSchema();
        return false;
      }
      switch (dbObject1.ObjectModifyMode)
      {
        case ObjectModifyModes.InBase:
        case ObjectModifyModes.CreateVersion:
          return true;
        case ObjectModifyModes.Checkout:
          if (MessageBox.Show($"Взять на редактирование объект '{dbObject1.Caption}'? (После завершения редактирования объект будет возвращен в архив)", "Редактирование схемы нумерации позиций", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return false;
          IDBObject dbObject2 = dbObject1.CheckOut();
          if (dbObject2 == null || dbObject2.CheckoutBy != sessionKeeper.Session.UserID)
            return false;
          this._SchemaObjectID = dbObject2.ObjectID;
          wasUpdated = true;
          this.InitNumberingSchema();
          this._NeedToAutoCheckIn = true;
          return true;
        case ObjectModifyModes.CantModify:
          int num1 = (int) MessageBox.Show($"Объект '{dbObject1.Caption}', в атрибутах которого хранится схема нумерации позиций недоступен для редактирования", "Редактирование схемы нумерации позиций", MessageBoxButtons.OK);
          return false;
        default:
          return false;
      }
    }
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      Size size = this.Size;
      int width1 = size.Width;
      int x = this.btnCancel.Location.X;
      size = this.btnCancel.Size;
      int width2 = size.Width;
      int num = x + width2;
      return width1 - num;
    }
  }

  /// <summary> Обработчик события "объект-владелец был взят на изменение" </summary>
  public void ObjectWasCheckedOut(object sender, NotificationEventArgs e)
  {
  }

  /// <summary> Обработчик события "объект-владелец был возвращён в архив" </summary>
  public void ObjectWasCheckedIn(object sender, NotificationEventArgs e)
  {
  }

  /// <summary> Обработчик события "Правки объекта-владелеца были отменены" </summary>
  public void ObjectChangesWasCanceled(object sender, NotificationEventArgs e)
  {
  }
}
