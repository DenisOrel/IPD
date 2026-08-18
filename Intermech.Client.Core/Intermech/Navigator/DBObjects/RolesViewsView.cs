
// Type: Intermech.Navigator.DBObjects.RolesViewsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Вьюшка для редактирования закладок "Навигатора" для ролей
/// </summary>
[ViewDescriptionProvider(typeof (RolesViewsView.RolesViewsViewDescriptionProvider))]
public sealed class RolesViewsView : UserControl, IView
{
  private ArrayList _objectVersionIds = new ArrayList();
  private long _objectTypeID;
  private string _objectName = string.Empty;
  private string _baseObjectName = string.Empty;
  private RolesViewsForm _rolesViewsForm;
  private int _imageIndex = -1;
  private bool _initmode;
  private bool _loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Создать экземпляр панели</summary>
  public RolesViewsView()
  {
    this.InitializeComponent();
    this._initmode = false;
  }

  /// <summary>Инициализировать вьюшку</summary>
  /// <param name="items">Список выделенных объектов</param>
  /// <param name="provider"></param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._objectVersionIds.Clear();
    if (items.Count < 1)
      return;
    this._objectTypeID = 0L;
    for (int index = 0; index < items.Count; ++index)
    {
      IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(index, typeof (IDBTypedObjectID));
      if (itemData != null)
      {
        this._objectVersionIds.Add((object) itemData.ObjectID);
        if (this._objectTypeID == 0L)
          this._objectTypeID = itemData.ObjectID;
      }
    }
    this._initmode = true;
    this._loaded = false;
  }

  /// <summary>
  /// Вернуть номер изображения вьюшки из глобального списка
  /// </summary>
  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgRolesViews");
      return this._imageIndex;
    }
  }

  /// <summary>Вернуть порядковый номер вьюшки в списке всех вьюшек</summary>
  public int OrderID => 40;

  /// <summary>Вернуть заголовок вьюшки</summary>
  public string Caption => RolesViewsView.RolesViewsViewConsts.MenuCaption;

  /// <summary>
  /// Выполнить действия при активации объекта подходящего типа
  /// </summary>
  /// <param name="previousView">Предыдущая вьюшка</param>
  public void Activate(IView previousView)
  {
    if (this._initmode)
    {
      this._objectName = string.Empty;
      this._baseObjectName = string.Empty;
      long int64 = Convert.ToInt64(this._objectVersionIds[this._objectVersionIds.Count - 1]);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        this._objectName = sessionKeeper.Session.GetObject(int64).Caption;
        this._baseObjectName = this._objectName;
      }
      if (this._objectVersionIds.Count > 1)
        this._objectName = string.Format(RolesViewsView.RolesViewsViewConsts.MultiObjectCaption, (object) this._objectVersionIds.Count);
      if (this._rolesViewsForm == null)
      {
        this._rolesViewsForm = new RolesViewsForm();
        this._rolesViewsForm.SetParent((Control) this);
        this._rolesViewsForm.ParentMode = 2;
      }
      this._initmode = false;
    }
    if (this._loaded)
      return;
    this._rolesViewsForm.RoleObjectIDs = this._objectVersionIds;
    this._rolesViewsForm.RoleObjectName = this._objectName;
    this._rolesViewsForm.BaseRoleObjectName = this._baseObjectName;
    this._rolesViewsForm.LoadObjectData(0);
    this._loaded = true;
  }

  /// <summary>Переход на другой объект, деактивация вьюшки</summary>
  /// <param name="nextView">Следующая вьюшка</param>
  public void Deactivate(IView nextView)
  {
    if (!this._rolesViewsForm.IsChanged || MessageBox.Show(this._objectVersionIds.Count <= 1 ? string.Format(RolesViewsView.RolesViewsViewConsts.ViewDialog2, (object) this._objectName) : string.Format(RolesViewsView.RolesViewsViewConsts.ViewDialog1, (object) this._objectVersionIds.Count), RolesViewsView.RolesViewsViewConsts.MenuCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
      return;
    this._rolesViewsForm.SaveObjectData();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RolesViewsView));
    this.SuspendLayout();
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.BackgroundImage = (Image) null;
    this.Font = (Font) null;
    this.Name = nameof (RolesViewsView);
    this.ResumeLayout(false);
  }

  private sealed class RolesViewsViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = RolesViewsView.RolesViewsViewConsts.MenuCaption,
        ImageIndex = Holder.NamedImageList.ImageIndex("imgRolesViews"),
        OrderID = 40
      };
    }
  }

  /// <summary>Свалка констант для вьюшки</summary>
  private static class RolesViewsViewConsts
  {
    /// <summary>Закладки</summary>
    public static readonly string MenuCaption = LocalizationHolder.rm.GetString("Client.Core_673");
    /// <summary>объектов: {0}</summary>
    public static readonly string MultiObjectCaption = LocalizationHolder.rm.GetString("Client.Core_645");
    /// <summary>
    /// Сохранить настройки закладок для выделенных {0} ролей?
    /// </summary>
    public static readonly string ViewDialog1 = LocalizationHolder.rm.GetString("Client.Core_674");
    /// <summary>Сохранить настройки закладок для роли \"{0}\" ?</summary>
    public static readonly string ViewDialog2 = LocalizationHolder.rm.GetString("Client.Core_675");
  }
}
