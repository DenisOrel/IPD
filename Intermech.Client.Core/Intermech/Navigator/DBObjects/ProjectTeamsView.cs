
// Type: Intermech.Navigator.DBObjects.ProjectTeamsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Вьюшка для управления участниками проектов</summary>
[ViewDescriptionProvider(typeof (ProjectTeamsView.ProjectTeamsViewDescriptionProvider))]
public class ProjectTeamsView : UserControl, IView
{
  /// <summary>форма</summary>
  private ProjectTeamsForm form;
  private bool _initmode;
  /// <summary>Загружен ли объект</summary>
  private bool _loaded;
  /// <summary>Индекс изображения "imgProject"</summary>
  internal static int _imageProject = -1;
  /// <summary>Сервис именованных значков</summary>
  internal INamedImageList _images;
  /// <summary>
  /// Контейнер сервисов (контекст) для выделенных элементов пространства навигации
  /// </summary>
  internal System.IServiceProvider _provider;
  /// <summary>Служба уведомлений</summary>
  internal INotificationService _notifications;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  internal NotificationEventHandler _notifyHandler;
  internal ISelectedItems _items;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolTip toolTip;
  private Column columnTeam;
  private ImageList imageList;
  private Column columnLevel;

  /// <summary>Создать экземпляр класса</summary>
  public ProjectTeamsView()
  {
    this.InitializeComponent();
    this._initmode = false;
  }

  /// <summary>Заголовок закладки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_627");

  /// <summary>Индекс изображения</summary>
  public int ImageIndex => ProjectTeamsView._imageProject;

  /// <summary>
  /// Порядковый номер закладки (прописан в файле Вьюшки.txt)
  /// </summary>
  public int OrderID => 27;

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="provider">Контейнер сервисов</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._items = items;
    this._provider = provider;
    this._images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    ProjectTeamsView._imageProject = ProjectTeamsView._imageProject < 0 ? this._images.ImageIndex("imgProjectTeam") : ProjectTeamsView._imageProject;
    this._initmode = true;
    this._loaded = false;
  }

  /// <summary>
  /// Активировать закладку (чтение из базы данных, загрузка информации и т.п.)
  /// </summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public void Activate(IView previousView)
  {
    if (this._initmode)
    {
      if (this.form == null)
      {
        this.form = new ProjectTeamsForm();
        this.form.SetParent((Control) this);
      }
      this._initmode = false;
    }
    if (!this._loaded)
    {
      this.form.ObjectID = (this._items.GetItemData(0, typeof (IDBTypedObjectID)) as IDBTypedObjectID).ObjectID;
      this.form.LoadViewData();
    }
    if (this._notifications != null && this._notifyHandler == null)
    {
      this._notifyHandler = new NotificationEventHandler(this.NotificationEventFired);
      this._notifications.Subscribe(this._notifyHandler);
    }
    this._loaded = true;
  }

  /// <summary>Деактивировать закладку</summary>
  /// <param name="nextView">Следующая закладка</param>
  public void Deactivate(IView nextView)
  {
    if (this._notifications != null && this._notifyHandler != null)
    {
      this._notifications.Unsubscribe(this._notifyHandler);
      this._notifyHandler = (NotificationEventHandler) null;
    }
    this._images = (INamedImageList) null;
    this._notifications = (INotificationService) null;
    this._provider = (System.IServiceProvider) null;
  }

  /// <summary>Получено очередное событие от службы уведомлений</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void NotificationEventFired(object sender, NotificationEventArgs e)
  {
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ProjectTeamsView));
    this.toolTip = new ToolTip(this.components);
    this.imageList = new ImageList(this.components);
    this.columnTeam = new Column();
    this.columnLevel = new Column();
    this.SuspendLayout();
    this.imageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList.ImageStream");
    this.imageList.TransparentColor = Color.Transparent;
    this.imageList.Images.SetKeyName(0, "users.ico");
    this.imageList.Images.SetKeyName(1, "adim.ico");
    this.imageList.Images.SetKeyName(2, "user.ico");
    this.columnTeam.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnTeam, "columnTeam");
    this.columnTeam.CellStyle.BorderColor = SystemColors.ControlDark;
    this.columnTeam.CellStyle.BorderStyle = Border3DStyle.Adjust;
    this.columnTeam.CellStyle.BorderWidth = 0;
    this.columnTeam.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("columnTeam.HeaderStyle.HorzAlignment");
    this.columnTeam.Movable = false;
    this.columnTeam.Name = "columnTeam";
    this.columnTeam.Sortable = false;
    this.columnTeam.SortDirection = ListSortDirection.Ascending;
    this.columnLevel.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    componentResourceManager.ApplyResources((object) this.columnLevel, "columnLevel");
    this.columnLevel.CellStyle.BorderColor = SystemColors.Control;
    this.columnLevel.CellStyle.BorderStyle = Border3DStyle.Adjust;
    this.columnLevel.CellStyle.BorderWidth = 1;
    this.columnLevel.Movable = false;
    this.columnLevel.Name = "columnLevel";
    this.columnLevel.Sortable = false;
    this.columnLevel.SortDirection = ListSortDirection.Ascending;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.MinimumSize = new Size(220, 120);
    this.Name = nameof (ProjectTeamsView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
  }

  private sealed class ProjectTeamsViewDescriptionProvider : BaseViewDescriptionProvider
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
        Caption = LocalizationHolder.rm.GetString("Client.Core_627"),
        ImageIndex = namedImageList != null ? namedImageList.ImageIndex("imgProjectTeam") : -1,
        OrderID = 27
      };
    }
  }
}
