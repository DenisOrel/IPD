
// Type: Intermech.Client.Core.LibraryImagePopupControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Компонент для выбора библиотечного изображения</summary>
public class LibraryImagePopupControl : BasePopupControl
{
  /// <summary>Кэш библиотечных изображений</summary>
  private IPicturesCache _cache;
  /// <summary>Идентификатор версии изображения</summary>
  private long _imageID;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private HeaderControl headerControl;
  private MenuBar menuBar;
  private ContextMenuBarItem contextMenuBarItem;
  private MenuButtonItem mnpAddCriterion;
  private MenuButtonItem mnpDeleteCriterion;
  private MenuButtonItem mnpAddValue;
  private MenuButtonItem mnpDelValue;
  private MenuButtonItem mnpMoveUp;
  private MenuButtonItem mnpMoveDown;
  private PictureBox picture;
  private Bevel bevelImage;
  private Button btnSelect;
  private Button btnClear;
  private Button btnOK;
  private Button btnCancel;
  private Bevel bevelButtons;
  private PictureBox pictureLogo;

  /// <summary>Создать экземпляр класса</summary>
  public LibraryImagePopupControl()
  {
    this.InitializeComponent();
    this._cache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache;
    if (this._cache != null)
    {
      this._cache.LoadComplete += new LoadCompleteEventHandler(this.Cache_LoadComplete);
      this._cache.CacheChanged += new CacheChangedEventHandler(this.Cache_Changed);
    }
    this.UpdateControls();
  }

  /// <summary>
  /// Загрузить информацию в контрол на основе данных из свойства Value
  /// </summary>
  private void LoadInfo()
  {
    if (this.Value == null || this.Value.Equals((object) Guid.Empty) || this.Value.Equals((object) 0L))
    {
      this.picture.Image = (Image) null;
      this.UpdateControls();
    }
    else
    {
      if (this._imageID == 0L && this.Value is Guid)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObject dbObject = sessionKeeper.Session.GetObject((Guid) this.Value, false);
          if (dbObject != null)
            this._imageID = dbObject.ObjectID;
        }
      }
      object obj = this._cache.GetPicture(this._imageID);
      if (obj is Icon icon)
        obj = (object) icon.ToBitmap();
      this.picture.Image = obj as Image;
    }
  }

  /// <summary>Обновить состояние элементов компонента</summary>
  public override void UpdateControls() => base.UpdateControls();

  /// <summary>
  /// Редактируемое значение (Guid-идентификатор версии объекта типа "Библиотечные изображения")
  /// </summary>
  public override object Value
  {
    [DebuggerStepThrough] get => base.Value ?? (object) Guid.Empty;
    set
    {
      this._imageID = 0L;
      switch (value)
      {
        case Guid _:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject((Guid) value, false);
            if (dbObject == null)
              break;
            this._imageID = dbObject.ObjectID;
            this.value = (object) (Guid) value;
            break;
          }
        case long _:
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject((long) value, false);
            if (dbObject == null)
              break;
            this._imageID = dbObject.ObjectID;
            this.value = (object) dbObject.ObjectGUID;
            break;
          }
        default:
          value = (object) Guid.Empty;
          break;
      }
    }
  }

  /// <summary>
  /// Отобразить элемент управления в указанной точке, с указанными размерами
  /// </summary>
  /// <param name="location">Положение левого верхнего угла компонента</param>
  /// <param name="size">Размеры компонента</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="value">Редактируемое значение</param>
  /// <returns>Результат вызова элемента управления</returns>
  public override DialogResult Execute(
    Point location,
    Size size,
    System.IServiceProvider services,
    object value)
  {
    try
    {
      this.Location = location;
      if (!size.IsEmpty)
        this.Size = size;
      this.Services = services;
      this.Value = value;
      this.DialogResult = DialogResult.None;
      this.LoadInfo();
      int num = (int) this.ShowDialog();
    }
    finally
    {
      this.Hide();
    }
    return this.DialogResult;
  }

  /// <summary>Компонент деактивирован</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected override void LeaveOrDeactivate(object sender, EventArgs e)
  {
  }

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoOK(object sender, EventArgs e) => this.DialogResult = DialogResult.OK;

  /// <summary>Нажата кнопка "Отмена"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoCancel(object sender, EventArgs e) => this.DialogResult = DialogResult.Cancel;

  /// <summary>Нажата кнопка "Выбрать"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoSelect(object sender, EventArgs e)
  {
    object[] objArray = SelectionWindow.Select(LocalizationHolder.rm.GetString("Client.Core_1400"), LocalizationHolder.rm.GetString("Client.Core_1401"), (IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(MetaDataHelper.GetObjectTypeID("cad00140-306c-11d8-b4e9-00304f19f545")), typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.DisableMultiselect);
    if (objArray == null || objArray.Length == 0 || !(objArray[0] is IDBObjectID dbObjectId))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(dbObjectId.Value, false);
      if (dbObject == null)
      {
        this.value = (object) Guid.Empty;
        this._imageID = 0L;
      }
      else
      {
        this.value = (object) dbObject.ObjectGUID;
        this._imageID = dbObjectId.Value;
      }
    }
    this.LoadInfo();
  }

  /// <summary>Нажата кнопка "Очистить"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoClear(object sender, EventArgs e)
  {
    this.value = (object) Guid.Empty;
    this._imageID = 0L;
    this.LoadInfo();
  }

  /// <summary>В кэше библиотечных изображений изменились данные</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="objectId">Идентификатор версии объекта</param>
  private void Cache_Changed(object sender, long objectId)
  {
  }

  /// <summary>Выполнена загрузка изображения</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void Cache_LoadComplete(object sender, PictureEventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this._cache != null)
      {
        this._cache.LoadComplete -= new LoadCompleteEventHandler(this.Cache_LoadComplete);
        this._cache.CacheChanged -= new CacheChangedEventHandler(this.Cache_Changed);
      }
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LibraryImagePopupControl));
    this.headerControl = new HeaderControl();
    this.pictureLogo = new PictureBox();
    this.menuBar = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.mnpAddCriterion = new MenuButtonItem();
    this.mnpDeleteCriterion = new MenuButtonItem();
    this.mnpAddValue = new MenuButtonItem();
    this.mnpDelValue = new MenuButtonItem();
    this.mnpMoveUp = new MenuButtonItem();
    this.mnpMoveDown = new MenuButtonItem();
    this.picture = new PictureBox();
    this.bevelImage = new Bevel();
    this.btnSelect = new Button();
    this.btnClear = new Button();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.bevelButtons = new Bevel();
    this.headerControl.SuspendLayout();
    ((ISupportInitialize) this.pictureLogo).BeginInit();
    ((ISupportInitialize) this.picture).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.headerControl, "headerControl");
    this.headerControl.BackColor = SystemColors.Control;
    this.headerControl.Controls.Add((Control) this.pictureLogo);
    this.headerControl.Controls.Add((Control) this.menuBar);
    this.headerControl.ForeColor = SystemColors.ControlText;
    this.headerControl.HeaderFont = new Font("Tahoma", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.headerControl.Name = "headerControl";
    componentResourceManager.ApplyResources((object) this.pictureLogo, "pictureLogo");
    this.pictureLogo.Name = "pictureLogo";
    this.pictureLogo.TabStop = false;
    componentResourceManager.ApplyResources((object) this.menuBar, "menuBar");
    this.menuBar.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuBar.Hidden = false;
    this.menuBar.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar.Name = "menuBar";
    this.menuBar.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem, "contextMenuBarItem");
    this.contextMenuBarItem.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpAddCriterion,
      (ToolbarItemBase) this.mnpDeleteCriterion,
      (ToolbarItemBase) this.mnpAddValue,
      (ToolbarItemBase) this.mnpDelValue,
      (ToolbarItemBase) this.mnpMoveUp,
      (ToolbarItemBase) this.mnpMoveDown
    });
    this.contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpAddCriterion, "mnpAddCriterion");
    this.mnpAddCriterion.ImageIndex = 0;
    this.mnpAddCriterion.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDeleteCriterion, "mnpDeleteCriterion");
    this.mnpDeleteCriterion.ImageIndex = 1;
    this.mnpDeleteCriterion.ShowText = true;
    this.mnpAddValue.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpAddValue, "mnpAddValue");
    this.mnpAddValue.ImageIndex = 2;
    this.mnpAddValue.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpDelValue, "mnpDelValue");
    this.mnpDelValue.ImageIndex = 3;
    this.mnpDelValue.ShowText = true;
    this.mnpMoveUp.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveUp, "mnpMoveUp");
    this.mnpMoveUp.ImageIndex = 4;
    this.mnpMoveUp.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpMoveDown, "mnpMoveDown");
    this.mnpMoveDown.ImageIndex = 5;
    this.mnpMoveDown.ShowText = true;
    componentResourceManager.ApplyResources((object) this.picture, "picture");
    this.picture.Name = "picture";
    this.picture.TabStop = false;
    componentResourceManager.ApplyResources((object) this.bevelImage, "bevelImage");
    this.bevelImage.Name = "bevelImage";
    componentResourceManager.ApplyResources((object) this.btnSelect, "btnSelect");
    this.btnSelect.MaximumSize = new Size(104, 27);
    this.btnSelect.MinimumSize = new Size(104, 27);
    this.btnSelect.Name = "btnSelect";
    this.btnSelect.TabStop = false;
    this.btnSelect.UseVisualStyleBackColor = true;
    this.btnSelect.Click += new EventHandler(this.DoSelect);
    componentResourceManager.ApplyResources((object) this.btnClear, "btnClear");
    this.btnClear.MaximumSize = new Size(104, 27);
    this.btnClear.MinimumSize = new Size(104, 27);
    this.btnClear.Name = "btnClear";
    this.btnClear.TabStop = false;
    this.btnClear.UseVisualStyleBackColor = true;
    this.btnClear.Click += new EventHandler(this.DoClear);
    componentResourceManager.ApplyResources((object) this.btnOK, "btnOK");
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.MaximumSize = new Size(104, 27);
    this.btnOK.MinimumSize = new Size(104, 27);
    this.btnOK.Name = "btnOK";
    this.btnOK.TabStop = false;
    this.btnOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.MaximumSize = new Size(104, 27);
    this.btnCancel.MinimumSize = new Size(104, 27);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.TabStop = false;
    this.btnCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bevelButtons, "bevelButtons");
    this.bevelButtons.Name = "bevelButtons";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.bevelButtons);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnClear);
    this.Controls.Add((Control) this.btnSelect);
    this.Controls.Add((Control) this.picture);
    this.Controls.Add((Control) this.headerControl);
    this.Controls.Add((Control) this.bevelImage);
    this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
    this.Name = nameof (LibraryImagePopupControl);
    this.headerControl.ResumeLayout(false);
    this.headerControl.PerformLayout();
    ((ISupportInitialize) this.pictureLogo).EndInit();
    ((ISupportInitialize) this.picture).EndInit();
    this.ResumeLayout(false);
  }
}
