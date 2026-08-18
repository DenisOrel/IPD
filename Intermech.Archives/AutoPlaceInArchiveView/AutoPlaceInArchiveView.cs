// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AutoPlaceInArchiveView.AutoPlaceInArchiveView
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Archives.AutoPlaceInArchiveView;

/// <summary>Вкладка Автоматическое размещение</summary>
[ViewDescriptionProvider(typeof (Intermech.Archives.AutoPlaceInArchiveView.AutoPlaceInArchiveView.AutoPlaceInArchiveViewDescriptionProvider))]
public class AutoPlaceInArchiveView : UserControl, IView
{
  /// <summary>ИД архива</summary>
  private long _archiveID;
  /// <summary>Закладка открыта в первый раз</summary>
  private bool _isFirst;
  /// <summary>Изменялась ли закладка</summary>
  private bool _isModified;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _bottom;
  private Panel _buttons;
  private Button _bCancel;
  private Button _bApply;
  private Panel _controlPanel;
  private AutoPlaceControl _autoPlaceControl;
  private Label _lblDisable;

  /// <summary>Изменялась ли закладка</summary>
  public bool IsModified
  {
    get => this._isModified;
    set
    {
      this._isModified = value;
      this._buttons.Enabled = value;
      if (!value)
        return;
      if (((!this._autoPlaceControl.UsersIDs.Any<long>() ? 0 : (this._autoPlaceControl.AutoPlaceDocTypesIDs.Any<int>() ? 1 : 0)) | (this._autoPlaceControl.UsersIDs.Any<long>() ? (false ? 1 : 0) : (!this._autoPlaceControl.AutoPlaceDocTypesIDs.Any<int>() ? 1 : 0))) != 0)
        this._bApply.Enabled = true;
      else
        this._bApply.Enabled = false;
    }
  }

  /// <summary>Конструктор</summary>
  public AutoPlaceInArchiveView()
  {
    this.InitializeComponent();
    this._autoPlaceControl.OnModified += new EventHandler(this._control_OnModified);
  }

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._archiveID = items.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData ? itemData.Value : throw new Exception(string.Format(ServiceHolder.rm.GetString("Archives_159")));
    this._isFirst = true;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она стала видима на экране. Этот метод вызывается при
  /// первом показе закладки, а также при переключении на нее с другой закладки.
  /// </summary>
  /// <param name="previousView">Закладка, с которой осуществляется переключение. Может быть null для самой первой
  /// показываемой на экране закладки.</param>
  public void Activate(IView previousView)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if ((int) sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, MetaDataHelper.GetAttributeTypeID(ConstsHolder.ArchiveTypesUsingModeGuid)).AsInteger == 2)
      {
        this._lblDisable.Visible = true;
        this._autoPlaceControl.Visible = false;
      }
      else
      {
        this._lblDisable.Visible = false;
        this._autoPlaceControl.Visible = true;
      }
    }
    if (this._isFirst)
      this._autoPlaceControl.ArchiveID = this._archiveID;
    this._autoPlaceControl.UpdateControl();
    this._isFirst = false;
    this.IsModified = false;
  }

  /// <summary>
  /// Уведомляет закладку о том, что она перестала быть видима на экране. Этот метод
  /// вызывается при переключении на другую закладку, а также удалении всех закладок.
  /// </summary>
  /// <param name="nextView">Закладка, на которую осуществляется переключение. Может быть null, если выполяется
  /// не переключение, а удаление закладок.</param>
  public void Deactivate(IView nextView)
  {
    if (!this._bApply.Enabled || !this.IsModified)
      return;
    if (MessageBox.Show(ServiceHolder.rm.GetString("Archives_156"), ServiceHolder.rm.GetString("Archives_184"), MessageBoxButtons.YesNo, MessageBoxIcon.Question).Equals((object) DialogResult.Yes))
      this._bApply_Click((object) null, (EventArgs) null);
    else
      this._bCancel_Click((object) null, (EventArgs) null);
  }

  /// <summary>
  /// Возвращает название закладки, которое будет отображаться на экране. Навигатор
  /// получает значение этого свойства после того, как закладка будет проинициализирована
  /// в методе Initialize.
  /// </summary>
  public string Caption => ServiceHolder.rm.GetString("Archives_184");

  /// <summary>
  /// Возвращает индекс иконки, которая будет отображаться на экране,
  /// в именованном списке иконок. Навигатор получает значение этого свойства после того,
  /// как закладка будет проинициализирована в методе Initialize.
  /// </summary>
  public int ImageIndex
  {
    get
    {
      return ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service ? service.ImageIndex("imgArchAutoPlace") : -1;
    }
  }

  /// <summary>
  /// Возвращает индекс расположения закладки среди других закладок
  /// при выводе на экран. Навигатор сортирует отображаемые закладки в
  /// порядке возрастания этого значения. Значение этого свойства
  /// навигатор получает после того, как закладка будет проинициализирована в
  /// методе Initialize.
  /// </summary>
  public int OrderID => 29;

  /// <summary>Изменение контрола.</summary>
  /// <param name="sender">Источник события</param>
  /// <param name="e">Аргументы</param>
  private void _control_OnModified(object sender, EventArgs e)
  {
    this.IsModified = this._autoPlaceControl.IsModified;
  }

  /// <summary>
  /// Нажатие кнопки Применить.
  /// Все проверки на адекватность настроек производятся на контроле при добавлении типов и юзеров.
  /// Здесь только сохранение атрибутов.
  /// </summary>
  private void _bApply_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute objectAttributeById1 = sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.AutoPlaceDocTypesAttrID);
      IDBAttribute objectAttributeById2 = sessionKeeper.Session.GetObjectAttributeByID(this._archiveID, ConstsHolder.UsersCanAutoPlaceDocsAttrID);
      if (objectAttributeById1 == null || objectAttributeById2 == null)
        return;
      objectAttributeById1.ClearValues();
      List<int> placeDocTypesIds = this._autoPlaceControl.AutoPlaceDocTypesIDs;
      foreach (int objTypeID in placeDocTypesIds)
      {
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(objTypeID);
        if (objectAttributeById1.ValuesCount == 1 && objectAttributeById1.IsNull)
        {
          objectAttributeById1.Index = 0;
          objectAttributeById1.Value = (object) objectTypeGuid;
        }
        else
          objectAttributeById1.AddValue((object) objectTypeGuid);
      }
      objectAttributeById2.ClearValues();
      List<long> usersIds = this._autoPlaceControl.UsersIDs;
      foreach (long newValue in usersIds)
      {
        if (objectAttributeById2.ValuesCount == 1 && objectAttributeById2.IsNull)
        {
          objectAttributeById2.Index = 0;
          objectAttributeById2.Value = (object) newValue;
        }
        else
          objectAttributeById2.AddValue((object) newValue);
      }
      if (sessionKeeper.Session.GetCustomService(typeof (IArchiveAutoPlaceCacheService)) is IArchiveAutoPlaceCacheService customService)
        customService.SaveAutoPlaceSettingsInCache(this._archiveID, placeDocTypesIds, usersIds);
    }
    this.IsModified = false;
  }

  private void _bCancel_Click(object sender, EventArgs e)
  {
    this._autoPlaceControl.UpdateControl();
    this.IsModified = false;
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
    this._bottom = new Panel();
    this._buttons = new Panel();
    this._bCancel = new Button();
    this._bApply = new Button();
    this._controlPanel = new Panel();
    this._lblDisable = new Label();
    this._autoPlaceControl = new AutoPlaceControl();
    this._bottom.SuspendLayout();
    this._buttons.SuspendLayout();
    this._controlPanel.SuspendLayout();
    this.SuspendLayout();
    this._bottom.Controls.Add((Control) this._buttons);
    this._bottom.Dock = DockStyle.Bottom;
    this._bottom.Location = new Point(0, 476);
    this._bottom.Name = "_bottom";
    this._bottom.Size = new Size(879, 40);
    this._bottom.TabIndex = 4;
    this._buttons.Controls.Add((Control) this._bCancel);
    this._buttons.Controls.Add((Control) this._bApply);
    this._buttons.Dock = DockStyle.Right;
    this._buttons.Location = new Point(612, 0);
    this._buttons.Name = "_buttons";
    this._buttons.Size = new Size(267, 40);
    this._buttons.TabIndex = 0;
    this._bCancel.FlatStyle = FlatStyle.System;
    this._bCancel.ImeMode = ImeMode.NoControl;
    this._bCancel.Location = new Point(139, 6);
    this._bCancel.Name = "_bCancel";
    this._bCancel.Size = new Size(110, 27);
    this._bCancel.TabIndex = 2;
    this._bCancel.Text = "Отмена";
    this._bCancel.Click += new EventHandler(this._bCancel_Click);
    this._bApply.FlatStyle = FlatStyle.System;
    this._bApply.ImeMode = ImeMode.NoControl;
    this._bApply.Location = new Point(23, 6);
    this._bApply.Name = "_bApply";
    this._bApply.Size = new Size(110, 27);
    this._bApply.TabIndex = 1;
    this._bApply.Text = "Применить";
    this._bApply.Click += new EventHandler(this._bApply_Click);
    this._controlPanel.Controls.Add((Control) this._lblDisable);
    this._controlPanel.Controls.Add((Control) this._autoPlaceControl);
    this._controlPanel.Dock = DockStyle.Fill;
    this._controlPanel.Location = new Point(0, 0);
    this._controlPanel.Name = "_controlPanel";
    this._controlPanel.Size = new Size(879, 476);
    this._controlPanel.TabIndex = 5;
    this._lblDisable.AutoSize = true;
    this._lblDisable.Location = new Point(3, 15);
    this._lblDisable.Name = "_lblDisable";
    this._lblDisable.Size = new Size(834, 13);
    this._lblDisable.TabIndex = 1;
    this._lblDisable.Text = "Настройка недоступна при включенном режиме \"Архив не может содержать документы перечисленных ниже типов\" на вкладке Разрешенные типы документов.";
    this._autoPlaceControl.ArchiveID = 0L;
    this._autoPlaceControl.Dock = DockStyle.Fill;
    this._autoPlaceControl.IsModified = false;
    this._autoPlaceControl.Location = new Point(0, 0);
    this._autoPlaceControl.Name = "_autoPlaceControl";
    this._autoPlaceControl.Size = new Size(879, 476);
    this._autoPlaceControl.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._controlPanel);
    this.Controls.Add((Control) this._bottom);
    this.Name = nameof (AutoPlaceInArchiveView);
    this.Size = new Size(879, 516);
    this._bottom.ResumeLayout(false);
    this._buttons.ResumeLayout(false);
    this._controlPanel.ResumeLayout(false);
    this._controlPanel.PerformLayout();
    this.ResumeLayout(false);
  }

  private sealed class AutoPlaceInArchiveViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      if (!(serviceProvider.GetService(typeof (INamedImageList)) is INamedImageList service))
        service = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
      INamedImageList namedImageList = service;
      return new ViewDescription()
      {
        Caption = ServiceHolder.rm.GetString("Archives_184"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgArchAutoPlace") : -1,
        OrderID = 29
      };
    }
  }
}
