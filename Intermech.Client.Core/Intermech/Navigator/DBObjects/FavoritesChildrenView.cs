
// Type: Intermech.Navigator.DBObjects.FavoritesChildrenView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Navigator.DBObjects;

/// <summary>Чилдрен вью для корневого узла Избранное</summary>
public class FavoritesChildrenView : ChildrenView, ICanCloseViews, ICanDeactivateView
{
  private int _imageIndex = -1;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public FavoritesChildrenView() => this.InitializeComponent();

  /// <summary>Наименование закладки.</summary>
  public override string Caption => "Объекты";

  /// <summary>Идентификатор иконки.</summary>
  public override int ImageIndex => this._imageIndex;

  /// <summary>
  /// 
  /// </summary>
  public override int OrderID => 1;

  /// <summary>
  /// 
  /// </summary>
  public override ContentType ViewContentType => ContentType.NonFolders;

  /// <summary>
  /// Категория для названия потока с сохранёнными настройками
  /// </summary>
  public override string StateStreamPrefix => "Favorites_";

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    base.Initialize(items, services);
    this._notificationService = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    if (this._notificationService != null)
    {
      this._notificationService.Unsubscribe("FavoritesChanged", new NotificationEventHandler(this.FavoritesChangedEventHandler));
      this._notificationService.Subscribe("FavoritesChanged", new NotificationEventHandler(this.FavoritesChangedEventHandler));
      this._notificationService.Unsubscribe("FavoritesRemoveType", new NotificationEventHandler(this.FavoritesChangedEventHandler));
      this._notificationService.Subscribe("FavoritesRemoveType", new NotificationEventHandler(this.FavoritesChangedEventHandler));
    }
    this._imageIndex = ChildrenView._namedImageList.ImageIndex("imgContains");
  }

  protected override void Dispose(bool disposing)
  {
    this._notificationService.Unsubscribe("FavoritesChanged", new NotificationEventHandler(this.FavoritesChangedEventHandler));
    this._notificationService.Unsubscribe("FavoritesRemoveType", new NotificationEventHandler(this.FavoritesChangedEventHandler));
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void FavoritesChangedEventHandler(object sender, NotificationEventArgs e)
  {
    this.ReloadItems();
  }

  public bool CanClose(object sender) => true;

  public bool CanDeactivate(object sender) => this.CanClose(sender);

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }
}
