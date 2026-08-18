
// Type: Intermech.Navigator.Snapshots.SnapshotsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Snapshots;

/// <summary>Закладка для итераций</summary>
[ViewDescriptionProvider(typeof (SnapshotsView.SnapshotsViewDescriptionProvider))]
public class SnapshotsView : ChildrenView
{
  /// <summary>id объекта, для которого показываем итерации</summary>
  private long id;
  /// <summary>
  /// версия для которой показываем итерации
  /// (не обязательно должна совпадать с той что выбрана в дереве)
  /// </summary>
  private long objectID;
  /// <summary>Активная итерация выбранной версии объекта</summary>
  private long _activeSnapshotID;
  private INotificationService ns;
  private int imageIndex = -1;
  /// <summary>
  /// показывать итреации для всех версии или для конкретной
  /// </summary>
  private static SnapshotMode mode;
  /// <summary>
  /// версия, для которой показываем итерации
  /// (может не совпадать с той, что выбрана в дереве)
  /// </summary>
  private static long selObjectID;
  /// <summary>Вкладка инициализирована</summary>
  private bool isActivated;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ComboBoxItem cbVersions;
  private ButtonItem btnRestore;

  internal static SnapshotMode Mode => SnapshotsView.mode;

  /// <summary>
  /// версия, для которой показываем итерации
  /// (может не совпадать с той, что выбрана в дереве)
  /// </summary>
  internal static long SelObjectID => SnapshotsView.selObjectID;

  public override int OrderID => 12;

  /// <summary>иконка для закладки</summary>
  public override int ImageIndex
  {
    get
    {
      if (this.imageIndex >= 0)
        return this.imageIndex;
      this.imageIndex = (ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList).ImageIndex("imgSnapshot");
      return this.imageIndex;
    }
  }

  public override string Caption => LocalizationHolder.rm.GetString("Client.Core_1410");

  /// <summary>
  /// Важная штука, нужная для того что бы набор колонок не наследовался
  /// </summary>
  protected override bool UseInheritedNavViews
  {
    get => false;
    set => base.UseInheritedNavViews = false;
  }

  /// <summary>Название потока, в котором будут сохранены настройки</summary>
  public override string StateStreamPrefix => "SnapshotsColumns";

  public SnapshotsView()
  {
    this.InitializeComponent();
    this.cbVersions.Index = 9;
    this.btnRestore.Index = 10;
  }

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public override void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    IDBTypedObjectID itemData = (IDBTypedObjectID) items.GetItemData(0, typeof (IDBTypedObjectID));
    this.id = itemData.ID;
    this.objectID = itemData.ObjectID;
    base.Initialize(items, provider);
    this.ns = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    this.isActivated = false;
  }

  /// <summary>Активировать закладку</summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public override void Activate(IView previousView)
  {
    if (this.isActivated)
      return;
    base.Activate(previousView);
    if (this.ns != null)
    {
      this.ns.Unsubscribe("SnapshotsChanged", new NotificationEventHandler(this.SnapshotReload));
      this.ns.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectWasChanged));
      this.ns.Subscribe("SnapshotsChanged", new NotificationEventHandler(this.SnapshotReload));
      this.ns.Subscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectWasChanged));
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.cbVersions.Items.Clear();
      this.cbVersions.Items.Add((object) new MyElement((object) 0L, LocalizationHolder.rm.GetString("Client.Core_1411"), (object) null));
      foreach (long objectVersion in sessionKeeper.Session.GetObjectVersions(this.id))
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(objectVersion, false);
        if (dbObject != null)
          this.cbVersions.Items.Add((object) new MyElement((object) objectVersion, string.Format(LocalizationHolder.rm.GetString("Client.Core_1412"), (object) dbObject.Caption, (object) dbObject.VersionID), (object) null));
      }
    }
    this.cbVersions.ComboBox.SelectedIndex = 0;
    SnapshotsView.mode = SnapshotMode.Object;
    this.cbVersions.ComboBox.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this.UpdateView();
    this.isActivated = true;
  }

  /// <summary>Deactivates the specified next view.</summary>
  /// <param name="nextView">The next view.</param>
  public override void Deactivate(IView nextView)
  {
    this.cbVersions.ComboBox.SelectedIndexChanged -= new EventHandler(this.ComboBox_SelectedIndexChanged);
    if (this.ns != null)
    {
      this.ns.Unsubscribe("SnapshotsChanged", new NotificationEventHandler(this.SnapshotReload));
      this.ns.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.ObjectWasChanged));
    }
    base.Deactivate(nextView);
  }

  /// <summary>
  /// Создает или получает извне элемент навигации, чье содержимое отображается в гриде.
  /// </summary>
  /// <returns></returns>
  protected override INode GetNode()
  {
    SnapshotsNode node;
    IContextAware contextAware = (IContextAware) (node = new SnapshotsNode(this.id, this.objectID));
    IContextAware parentNode = this._parentNode as IContextAware;
    if (contextAware == null)
      return (INode) node;
    AdvancedServiceContainer serviceContainer = new AdvancedServiceContainer((System.IServiceProvider) this._services);
    if (parentNode != null)
      serviceContainer.AdvancedProvider = parentNode.Services;
    contextAware.Services = (System.IServiceProvider) serviceContainer;
    return (INode) node;
  }

  /// <summary>Обновление вьюшки.</summary>
  private void UpdateView()
  {
    this._activeSnapshotID = 0L;
    this.ReloadItems();
  }

  private void SnapshotReload(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs.Count == 0 || objectsEventArgs.ObjectIDs[0] != this.id)
      return;
    this.UpdateView();
  }

  /// <summary>
  /// Получает ИД последней использованной итерации из атрибута.
  /// </summary>
  /// <returns>ИД последней использованной итерации.</returns>
  private long GetActiveSnapshotID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this.objectID, false);
      if (dbObject == null)
        return 0;
      IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cadd94ce-306c-11d8-b4e9-00304f19f545"));
      long activeSnapshotId = 0;
      if (attributeByGuid != null)
        activeSnapshotId = attributeByGuid.AsInteger;
      return activeSnapshotId;
    }
  }

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!(this.cbVersions.ComboBox.Items[this.cbVersions.ComboBox.SelectedIndex] is MyElement myElement))
      return;
    long int64 = Convert.ToInt64(myElement.Value);
    if (int64 == 0L)
    {
      SnapshotsView.mode = SnapshotMode.Object;
    }
    else
    {
      SnapshotsView.mode = SnapshotMode.ObjectVersion;
      SnapshotsView.selObjectID = int64;
    }
    this.ReloadItems();
  }

  private void ObjectWasChanged(object sender, NotificationEventArgs e)
  {
    if (!(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs.Count == 0 || objectsEventArgs.ObjectIDs[0] != this.objectID)
      return;
    this.UpdateView();
  }

  /// <summary>активная итерация объекта</summary>
  private long ActiveSnapshotID
  {
    get
    {
      if (this._activeSnapshotID == 0L)
        this._activeSnapshotID = this.GetActiveSnapshotID();
      return this._activeSnapshotID;
    }
  }

  /// <summary>Динамическая подстановка шрифта</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_DynamicFont(object sender, iGDynamicFontEventArgs e)
  {
    if (!(this.GetNodeIDForRow(e.RowIndex) is SnapshotsNodeID nodeIdForRow) || nodeIdForRow.SnapshotID != this.ActiveSnapshotID)
      return;
    e.Font = new Font(this._grid.Font, FontStyle.Bold | FontStyle.Italic);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SnapshotsView));
    this.cbVersions = new ComboBoxItem();
    this.btnRestore = new ButtonItem();
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._toolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.cbVersions,
      (ToolbarItemBase) this.btnRestore
    });
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this._grid.DynamicFont += new iGDynamicFontEventHandler(this.grid_DynamicFont);
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Enabled = false;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this._gridHeaderMenuBar, "menuHeader");
    this.cbVersions.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.cbVersions, "cbVersions");
    this.cbVersions.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbVersions.MinimumControlWidth = 50;
    this.cbVersions.Padding.Bottom = 0;
    this.cbVersions.Padding.Left = 1;
    this.cbVersions.Padding.Right = 1;
    this.cbVersions.Padding.Top = 0;
    this.cbVersions.Stretch = true;
    componentResourceManager.ApplyResources((object) this.btnRestore, "btnRestore");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.DisableCheckedOutColumn = true;
    this.DisableFiltration = true;
    this.DisableHeaderContextMenu = true;
    this.DisableParentSelectedItems = true;
    this.Name = nameof (SnapshotsView);
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private sealed class SnapshotsViewDescriptionProvider : BaseViewDescriptionProvider
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
        Caption = LocalizationHolder.rm.GetString("Client.Core_1410"),
        ImageIndex = namedImageList.ImageIndex("imgSnapshot"),
        OrderID = 12
      };
    }
  }
}
