// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.Client.BaseSettingsView
// Assembly: Intermech.XmlExchange.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 60313882-D426-47E0-8CD2-E15037D75FF2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.XmlExchange.Client.xml

using Intermech.Bars;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.XmlExchange.Client;

/// <summary>Базовая закладка настроек</summary>
/// <summary>Базовая закладка настроек</summary>
internal class BaseSettingsView : UserControl, IView
{
  /// <summary>Объект для синхронизации</summary>
  protected object syncRoot = new object();
  /// <summary>Есть ли изменения в редакторе</summary>
  protected bool isChanged;
  /// <summary>Запрет на обработку некоторых событий</summary>
  protected bool inEvents;
  /// <summary>Сервис именованных значков</summary>
  protected INamedImageList _images;
  /// <summary>Текущий пользователь</summary>
  protected ICurrentUserAndRole _userAndRole;
  /// <summary>Сервис значков для категорий и типов</summary>
  protected ICategoryTypeIconService _categoryImages;
  /// <summary>Кэш графических элементов Навигатора</summary>
  protected INavGraphicsCache _navGraphicsCache;
  /// <summary>Индекс изображения закладка</summary>
  protected int _imgView = -1;
  /// <summary>Обработчик событий от службы уведомлений</summary>
  protected NotificationEventHandler _notifyHandler;
  /// <summary>
  /// Коллекция выделенных элементов пространства навигации, на основании данных которых работает закладка
  /// </summary>
  protected ISelectedItems _items;
  /// <summary>Служба уведомлений</summary>
  protected INotificationService _notifications;
  /// <summary>
  /// Контейнер сервисов (контекст) для выделенных элементов пространства навигации
  /// </summary>
  protected System.IServiceProvider _services;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  /// <summary>Контейнер сервисов</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual System.IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  /// <summary>Объект для синхронизации</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual object SyncRoot
  {
    [DebuggerStepThrough] get => this.syncRoot;
  }

  /// <summary>Есть ли изменения в редакторе настройке</summary>
  [Category("Appearance")]
  [Browsable(true)]
  public virtual bool IsChanged
  {
    [DebuggerStepThrough] get => this.isChanged;
  }

  /// <summary>
  /// Событие возникает, если в редакторе произошли изменения
  /// </summary>
  [Description("Событие возникает, если в редакторе произошли изменения ")]
  public event EventHandler OnChanged;

  /// <summary>Сгенерировать событие "OnChanged"</summary>
  public virtual void RaiseOnChanged()
  {
    if (this.inEvents)
      return;
    EventHandler onChanged = this.OnChanged;
    if (onChanged == null)
      return;
    onChanged((object) this, EventArgs.Empty);
  }

  /// <summary>Создать экземпляр класса</summary>
  public BaseSettingsView()
  {
    this.InitializeComponent();
    this.InitViewResources();
    BarManager service = ServiceUtils.GetService<BarManager>((object) ApplicationServices.Container, false);
    if (service == null)
      return;
    service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
  }

  /// <summary>Инициализация ресурсов закладки</summary>
  private void InitViewResources()
  {
    if (ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false) == null || this.DesignMode)
      return;
    this._images = ServiceUtils.GetService<INamedImageList>((object) ApplicationServices.Container, false);
    this._userAndRole = ServiceUtils.GetService<ICurrentUserAndRole>((object) ApplicationServices.Container, false);
    this._navGraphicsCache = ServiceUtils.GetService<INavGraphicsCache>((object) ApplicationServices.Container, false);
    this._categoryImages = ServiceUtils.GetService<ICategoryTypeIconService>((object) ApplicationServices.Container, false);
    this._notifications = ServiceUtils.GetService<INotificationService>((object) ApplicationServices.Container, false);
    this._imgView = -1;
  }

  /// <summary>Освобождение ресурсов закладки</summary>
  public virtual void DisposeViewResources()
  {
    this._images = (INamedImageList) null;
    this._categoryImages = (ICategoryTypeIconService) null;
    this._items = (ISelectedItems) null;
    this._services = (System.IServiceProvider) null;
    this._notifications = (INotificationService) null;
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Заголовок закладки</summary>
  public virtual string Caption
  {
    [DebuggerStepThrough] get => "Настройка импорта из XML";
  }

  /// <summary>Индекс изображения</summary>
  public virtual int ImageIndex => this._imgView;

  /// <summary>Порядковый номер закладки</summary>
  public virtual int OrderID => 0;

  /// <summary>Инициализировать закладку</summary>
  /// <param name="items">Коллекция выделенных элементов пространства навигации</param>
  /// <param name="provider">Контейнер сервисов</param>
  public virtual void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._items = items;
    this._services = provider;
  }

  /// <summary>
  /// Активировать закладку (чтение из базы данных, загрузка информации и т.п.)
  /// </summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public virtual void Activate(IView previousView)
  {
    if (previousView == PageViewsManager.BlackHoleView)
      return;
    this.LoadViewData();
  }

  /// <summary>Деактивировать закладку</summary>
  /// <param name="nextView">Следующая закладка</param>
  public virtual void Deactivate(IView nextView)
  {
  }

  /// <summary>
  /// Заполнить элементы управления закладки данными, полученными в методе Initialize
  /// </summary>
  protected virtual void LoadViewData()
  {
    this.Clear();
    if (this._items == null || this._items.Count == 0)
      return;
    this.UpdateControls();
  }

  /// <summary>Выполнить очистку элементов управления в закладке</summary>
  protected virtual void Clear() => this.UpdateControls();

  /// <summary>Управление контролами на закладке</summary>
  protected virtual void UpdateControls()
  {
  }

  /// <summary>Забрать изменения из закладки в контейнер настроек</summary>
  protected virtual void CaptureChanges()
  {
  }

  /// <summary>Удаление используемых ресурсов</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      BarManager service = ServiceUtils.GetService<BarManager>((object) ApplicationServices.Container, false);
      if (service != null)
        service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BaseSettingsView));
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.ForeColor = SystemColors.ControlText;
    this.Name = "ImportSettingsView";
    this.ResumeLayout(false);
  }
}
