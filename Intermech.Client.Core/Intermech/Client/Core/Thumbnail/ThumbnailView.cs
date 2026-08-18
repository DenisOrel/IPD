
// Type: Intermech.Client.Core.Thumbnail.ThumbnailView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Controls;
using Intermech.Controls.Thumbnail;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextCommands;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>
/// Вид отображает изображения, привязанные к Каталогам, папкам и таблицам
/// </summary>
[ViewDescriptionProvider(typeof (ThumbnailView.ThumbnailViewDescriptionProvider))]
public class ThumbnailView : 
  UserControl,
  IFoldersView,
  IView,
  ICommandTarget,
  ISelectedItemsHost,
  IIOSource
{
  internal static int _imageIndex = -1;
  internal AdvancedServiceContainer _services;
  protected IIODispatcher _dispatcher;
  protected INode _parentNode;
  protected INode _node;
  protected INodeID _nodeID;
  protected NodeIDPath _path;
  protected List<ThumbnailItem> _items;
  protected object _bookmark;
  private bool _eof;
  protected int _readedCount;
  /// <summary>
  /// Пока значение этого флажка равно true, всегда читаются все данные (пакетное чтение отключается)
  /// </summary>
  private bool _readAllMode;
  protected ThumbnailGrid _thumbnails;
  protected ThumbnailRenderer _renderer;
  private IPicturesCache _cache;
  private int _sessionId;
  private ThumbnailSelectedItems _selectedItems;
  private ContextMenuBarItem _contextMenu;
  private ThumbnailItem _item;
  private int _updateIndex;
  private IContainer components;
  private Timer timer;
  protected ToolStrip _ts;
  protected ToolStripTextBox _tsTxtSearch;
  protected ToolStripButton _tsBtnSearch;
  protected ToolStripSeparator _tsSeparator;
  protected ToolStripButton _tsBtnAlphabet;
  protected ToolStripButton _tsBtnNumber;
  private ImageList _imgList;
  private GroupBox _grp;
  protected StatusStrip _statusBar;
  protected ToolStripDropDownButton _btnReadNext;
  protected ToolStripDropDownButton _btnReadAll;
  private ToolStripStatusLabel _lbDivider;
  protected ToolStripStatusLabel _pnlReaded;
  private static int _fetchCount = 0;
  protected static NodeColumnCollection _columns;
  private static Size _panelSize = Size.Empty;
  private static IPopupMenuHost _host;
  private static INamedImageList _nil;
  protected ToolStripSeparator _tsSeparator1;
  protected ToolStripLabel _tsFltLabel;
  protected ToolStripComboBox _tsFltComboBox;
  protected ToolStripSeparator _tsSeparator2;
  private static Icon _waitIcon;
  /// <summary>Нажата ли клавиша "Return" ("Enter").</summary>
  private bool returnKeyPressed;

  /// <summary>
  /// 
  /// </summary>
  protected virtual ContentType ContentType => ContentType.Folders | ContentType.NonFolders;

  /// <summary>
  /// 
  /// </summary>
  private INode Node
  {
    get
    {
      if (this._node == null)
      {
        this._node = this._parentNode.GetChild(this._nodeID);
        if (this._node == null)
          this._node = this._parentNode.GetData(this._nodeID, typeof (INode)) as INode;
        if (this._node is IContextAware node)
          node.Services = (System.IServiceProvider) this._services;
      }
      return this._node;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  internal ThumbnailItem SelectedItem
  {
    get
    {
      if (this._items != null && this._items.Count > 0)
      {
        int itemIndex = this._thumbnails.ItemIndex;
        if (itemIndex < this._items.Count && itemIndex != -1)
          return this._items[itemIndex];
      }
      return (ThumbnailItem) null;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private static void ThumbnailViewInit()
  {
    if (ThumbnailView._columns != null)
      return;
    IColumnSchemes service1 = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    IFactory service2 = (IFactory) ServicesManager.GetService(typeof (IFactory));
    ThumbnailView._columns = new NodeColumnCollection();
    ThumbnailView._columns.Add(service1.CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_CAPTION"));
    ThumbnailView._columns.Add(service1.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID));
    ThumbnailView._columns.Add(service1.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE));
    if (ServicesManager.GetService(typeof (IConfigurationManager)) is IConfigurationManager service3)
    {
      service3.ConfigurationBeforeSave += new ConfigurationBeforeSaveEventHandler(ThumbnailView.Configuration_BeforeSave);
      IConfiguration configuration = service3.Open("Thumbnails");
      if (configuration != null)
      {
        string property = configuration.GetProperty("PanelSize");
        if (property != null)
        {
          if (property.Length > 0)
          {
            try
            {
              ThumbnailView._panelSize = (Size) TypeDescriptor.GetConverter(typeof (Size)).ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) property);
            }
            catch
            {
            }
          }
        }
      }
    }
    if (ServicesManager.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service4)
      service4.Changed += new EventHandler(ThumbnailView.PropPages_Changed);
    ThumbnailView._host = ServicesManager.GetService(typeof (IPopupMenuHost)) as IPopupMenuHost;
    ThumbnailView._nil = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    using (Stream manifestResourceStream = typeof (ThumbnailView).Assembly.GetManifestResourceStream("Intermech.Client.Core.Resources.WaitImage.ico"))
      ThumbnailView._waitIcon = new Icon(manifestResourceStream);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="query"></param>
  protected virtual void ApplyColumns(INodeQuery query)
  {
    for (int index = 0; index < ThumbnailView._columns.Count; ++index)
      query.AddColumn(ThumbnailView._columns[index], (INodeColumnTransform) null);
  }

  /// <summary>
  /// 
  /// </summary>
  private static int FetchCount
  {
    get
    {
      if (ThumbnailView._fetchCount == 0)
        ThumbnailView._fetchCount = (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).MaxRows;
      return ThumbnailView._fetchCount;
    }
  }

  /// <summary>Конструктор.</summary>
  public ThumbnailView()
  {
    if (!this.DesignMode)
      ThumbnailView.ThumbnailViewInit();
    this._services = new AdvancedServiceContainer();
    this._updateIndex = -1;
    this._cache = ServicesManager.GetService(typeof (IPicturesCache)) as IPicturesCache;
    if (this._cache != null)
    {
      this._cache.LoadComplete += new LoadCompleteEventHandler(this.Cache_LoadComplete);
      this._cache.CacheChanged += new CacheChangedEventHandler(this.Cache_Changed);
    }
    this.InitializeComponent();
    this._renderer = new ThumbnailRenderer(this.Font, new GetImageHandler(this.OnGetImage));
    this._thumbnails.Renderer = (IThumbnailRenderer) this._renderer;
    if (ThumbnailView._panelSize == Size.Empty)
      ThumbnailView._panelSize = this._thumbnails.PanelSize;
    else
      this._thumbnails.PanelSize = ThumbnailView._panelSize;
    ThumbnailView.DragAcceptFiles(this.Handle, true);
    this.Subscribe();
    this._tsBtnAlphabet.Image = this._imgList.Images["SortAlphabetAsc.png"];
    this._tsBtnAlphabet.Tag = (object) ThumbnailView.SortMethods.NameAsc;
    this._tsBtnNumber.Image = this._imgList.Images["SortNumberAsc.png"];
    this._tsBtnNumber.Tag = (object) ThumbnailView.SortMethods.NumAsc;
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ThumbnailView));
    this.timer = new Timer(this.components);
    this._ts = new ToolStrip();
    this._tsTxtSearch = new ToolStripTextBox();
    this._tsBtnSearch = new ToolStripButton();
    this._tsSeparator = new ToolStripSeparator();
    this._tsBtnAlphabet = new ToolStripButton();
    this._tsBtnNumber = new ToolStripButton();
    this._tsSeparator1 = new ToolStripSeparator();
    this._tsFltLabel = new ToolStripLabel();
    this._tsFltComboBox = new ToolStripComboBox();
    this._tsSeparator2 = new ToolStripSeparator();
    this._imgList = new ImageList(this.components);
    this._grp = new GroupBox();
    this._thumbnails = new ThumbnailGrid();
    this._statusBar = new StatusStrip();
    this._btnReadNext = new ToolStripDropDownButton();
    this._btnReadAll = new ToolStripDropDownButton();
    this._lbDivider = new ToolStripStatusLabel();
    this._pnlReaded = new ToolStripStatusLabel();
    this._ts.SuspendLayout();
    this._statusBar.SuspendLayout();
    this.SuspendLayout();
    this._ts.GripStyle = ToolStripGripStyle.Hidden;
    this._ts.Items.AddRange(new ToolStripItem[9]
    {
      (ToolStripItem) this._tsTxtSearch,
      (ToolStripItem) this._tsBtnSearch,
      (ToolStripItem) this._tsSeparator,
      (ToolStripItem) this._tsBtnAlphabet,
      (ToolStripItem) this._tsBtnNumber,
      (ToolStripItem) this._tsSeparator1,
      (ToolStripItem) this._tsFltLabel,
      (ToolStripItem) this._tsFltComboBox,
      (ToolStripItem) this._tsSeparator2
    });
    this._ts.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
    componentResourceManager.ApplyResources((object) this._ts, "_ts");
    this._ts.Name = "_ts";
    this._tsTxtSearch.BorderStyle = BorderStyle.FixedSingle;
    this._tsTxtSearch.Margin = new Padding(0);
    this._tsTxtSearch.Name = "_tsTxtSearch";
    componentResourceManager.ApplyResources((object) this._tsTxtSearch, "_tsTxtSearch");
    this._tsTxtSearch.KeyDown += new KeyEventHandler(this.On_tsTxtSearch_KeyDown);
    this._tsBtnSearch.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsBtnSearch, "_tsBtnSearch");
    this._tsBtnSearch.Name = "_tsBtnSearch";
    this._tsBtnSearch.Click += new EventHandler(this.On_tsBtnSearch_Click);
    this._tsSeparator.Name = "_tsSeparator";
    componentResourceManager.ApplyResources((object) this._tsSeparator, "_tsSeparator");
    this._tsBtnAlphabet.Checked = true;
    this._tsBtnAlphabet.CheckState = CheckState.Checked;
    this._tsBtnAlphabet.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsBtnAlphabet, "_tsBtnAlphabet");
    this._tsBtnAlphabet.Name = "_tsBtnAlphabet";
    this._tsBtnAlphabet.Click += new EventHandler(this.On_tsBtnAlphabet_Click);
    this._tsBtnNumber.DisplayStyle = ToolStripItemDisplayStyle.Image;
    componentResourceManager.ApplyResources((object) this._tsBtnNumber, "_tsBtnNumber");
    this._tsBtnNumber.Name = "_tsBtnNumber";
    this._tsBtnNumber.Click += new EventHandler(this.On_tsBtnNumber_Click);
    this._tsSeparator1.Name = "_tsSeparator1";
    componentResourceManager.ApplyResources((object) this._tsSeparator1, "_tsSeparator1");
    this._tsFltLabel.Name = "_tsFltLabel";
    componentResourceManager.ApplyResources((object) this._tsFltLabel, "_tsFltLabel");
    this._tsFltComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._tsFltComboBox.Name = "_tsFltComboBox";
    componentResourceManager.ApplyResources((object) this._tsFltComboBox, "_tsFltComboBox");
    this._tsSeparator2.Name = "_tsSeparator2";
    componentResourceManager.ApplyResources((object) this._tsSeparator2, "_tsSeparator2");
    this._imgList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("_imgList.ImageStream");
    this._imgList.TransparentColor = Color.Transparent;
    this._imgList.Images.SetKeyName(0, "SortAlphabetAsc.png");
    this._imgList.Images.SetKeyName(1, "SortAlphabetDesc.png");
    this._imgList.Images.SetKeyName(2, "SortNumberAsc.png");
    this._imgList.Images.SetKeyName(3, "SortNumberDesc.png");
    componentResourceManager.ApplyResources((object) this._grp, "_grp");
    this._grp.Name = "_grp";
    this._grp.TabStop = false;
    componentResourceManager.ApplyResources((object) this._thumbnails, "_thumbnails");
    this._thumbnails.ItemIndex = 0;
    this._thumbnails.Name = "_thumbnails";
    this._thumbnails.PanelSize = new Size(150, 120);
    this._thumbnails.PanelSpacing = 16 /*0x10*/;
    this._thumbnails.Renderer = (IThumbnailRenderer) null;
    this._thumbnails.ShowContextMenu += new ThumbnailEventHandler(this.Thumbnails_ShowContextMenu);
    this._thumbnails.SelectionChanged += new Intermech.Controls.Thumbnail.SelectionChangedEventHandler(this.Thumbnails_SelectionChanged);
    this._thumbnails.StopResize += new EventHandler(this.Thumbnails_StopResize);
    this._thumbnails.DoubleClick += new EventHandler(this._thumbnails_DoubleClick);
    this._thumbnails.Enter += new EventHandler(this._thumbnails_Enter);
    this._thumbnails.KeyDown += new KeyEventHandler(this.ThumbnailView_KeyDown);
    this._thumbnails.KeyUp += new KeyEventHandler(this.ThumbnailView_KeyUp);
    this._thumbnails.Leave += new EventHandler(this._thumbnails_Enter);
    this._statusBar.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this._btnReadNext,
      (ToolStripItem) this._btnReadAll,
      (ToolStripItem) this._lbDivider,
      (ToolStripItem) this._pnlReaded
    });
    componentResourceManager.ApplyResources((object) this._statusBar, "_statusBar");
    this._statusBar.Name = "_statusBar";
    this._statusBar.ShowItemToolTips = true;
    this._statusBar.SizingGrip = false;
    componentResourceManager.ApplyResources((object) this._btnReadNext, "_btnReadNext");
    this._btnReadNext.ForeColor = Color.Red;
    this._btnReadNext.Name = "_btnReadNext";
    this._btnReadNext.Overflow = ToolStripItemOverflow.Never;
    this._btnReadNext.ShowDropDownArrow = false;
    this._btnReadNext.Click += new EventHandler(this.On_btnReadNext_Click);
    this._btnReadAll.ForeColor = Color.Red;
    componentResourceManager.ApplyResources((object) this._btnReadAll, "_btnReadAll");
    this._btnReadAll.Name = "_btnReadAll";
    this._btnReadAll.Overflow = ToolStripItemOverflow.Never;
    this._btnReadAll.ShowDropDownArrow = false;
    this._btnReadAll.Click += new EventHandler(this.On_btnReadAll_Click);
    componentResourceManager.ApplyResources((object) this._lbDivider, "_lbDivider");
    this._lbDivider.Name = "_lbDivider";
    this._pnlReaded.Name = "_pnlReaded";
    this._pnlReaded.Overflow = ToolStripItemOverflow.Never;
    componentResourceManager.ApplyResources((object) this._pnlReaded, "_pnlReaded");
    this.Controls.Add((System.Windows.Forms.Control) this._thumbnails);
    this.Controls.Add((System.Windows.Forms.Control) this._statusBar);
    this.Controls.Add((System.Windows.Forms.Control) this._grp);
    this.Controls.Add((System.Windows.Forms.Control) this._ts);
    this.Name = nameof (ThumbnailView);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.KeyDown += new KeyEventHandler(this.ThumbnailView_KeyDown);
    this.KeyUp += new KeyEventHandler(this.ThumbnailView_KeyUp);
    this._ts.ResumeLayout(false);
    this._ts.PerformLayout();
    this._statusBar.ResumeLayout(false);
    this._statusBar.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  /// <summary>Двойной клик мышью.</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void _thumbnails_DoubleClick(object sender, EventArgs e)
  {
    if (this._dispatcher == null)
      return;
    NodeIDPath selectedNodeIdPath = this.GetSelectedNodeIDPath();
    this._dispatcher.ProcessEvent((IIOEvent) new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evMouseDoubleClick, (object) e, (object) selectedNodeIdPath));
  }

  /// <summary>Нажата клавиша.</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ThumbnailView_KeyDown(object sender, KeyEventArgs e)
  {
    this.returnKeyPressed = e.KeyCode == Keys.Return;
    if (this._dispatcher == null)
      return;
    this._dispatcher.ProcessEvent((IIOEvent) new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evKeyDown, (object) e, (object) null));
  }

  /// <summary>Отпущена клавиша.</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ThumbnailView_KeyUp(object sender, KeyEventArgs e)
  {
    if (this._dispatcher == null)
      return;
    NodeIDPath ATag = this.GetSelectedNodeIDPath();
    if (e.KeyCode == Keys.Back || e.KeyCode == Keys.BrowserBack)
      ATag = this._path;
    if (e.KeyCode == Keys.Return && (e.KeyCode != Keys.Return || !this.returnKeyPressed))
      return;
    this._dispatcher.ProcessEvent((IIOEvent) new IOEvent((IIOSource) this, IOEventFlags.efNone, IOEventType.evKeyUp, (object) e, (object) ATag));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void _thumbnails_Enter(object sender, EventArgs e)
  {
    this._thumbnails.RepaintItem(this._thumbnails.ItemIndex);
  }

  /// <summary>
  /// В кэше картинок изменились данные. Надо проверить картинки и перечитать для них кэш.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="objectId"></param>
  private void Cache_Changed(object sender, long objectId)
  {
    if (this._items == null)
      return;
    bool flag = false;
    int count = this._items.Count;
    for (int index = 0; index < count; ++index)
    {
      if (index != this._updateIndex && this._items[index].PictureObjectId == objectId)
      {
        this._items[index].CleanCache();
        flag = true;
      }
    }
    if (!flag)
      return;
    this._thumbnails.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Cache_LoadComplete(object sender, PictureEventArgs e)
  {
    if (this._items == null || this._items.Count <= 0)
      return;
    int session = e.Session;
    int sessionId = this._sessionId;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="configurationManager"></param>
  private static void Configuration_BeforeSave(IConfigurationManager configurationManager)
  {
    configurationManager.Create("Thumbnails").SetProperty("PanelSize", (string) TypeDescriptor.GetConverter(typeof (Size)).ConvertTo((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, (object) ThumbnailView._panelSize, typeof (string)));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnReadAll_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString(sc_5187.ssp_imclient_5188()), LocalizationHolder.rm.GetString("Client.Core_132"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this._readAllMode = true;
    this._tsBtnAlphabet.Image = this._imgList.Images["SortAlphabetAsc.png"];
    this._tsBtnAlphabet.Checked = true;
    this._tsBtnAlphabet.Tag = (object) ThumbnailView.SortMethods.NameAsc;
    this._tsBtnNumber.Image = this._imgList.Images["SortNumberAsc.png"];
    this._tsBtnNumber.Checked = false;
    this._tsBtnNumber.Tag = (object) ThumbnailView.SortMethods.NumAsc;
    this.GetDataPacket();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_btnReadNext_Click(object sender, EventArgs e)
  {
    this._tsBtnAlphabet.Image = this._imgList.Images["SortAlphabetAsc.png"];
    this._tsBtnAlphabet.Checked = true;
    this._tsBtnAlphabet.Tag = (object) ThumbnailView.SortMethods.NameAsc;
    this._tsBtnNumber.Image = this._imgList.Images["SortNumberAsc.png"];
    this._tsBtnNumber.Checked = false;
    this._tsBtnNumber.Tag = (object) ThumbnailView.SortMethods.NumAsc;
    this.GetDataPacket();
  }

  /// <summary>Сортировка по алфавиту.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_tsBtnAlphabet_Click(object sender, EventArgs e)
  {
    if (this._tsBtnAlphabet.Checked)
    {
      if ((ThumbnailView.SortMethods) this._tsBtnAlphabet.Tag == ThumbnailView.SortMethods.NameAsc)
      {
        this._tsBtnAlphabet.Image = this._imgList.Images["SortAlphabetDesc.png"];
        this._tsBtnAlphabet.Tag = (object) ThumbnailView.SortMethods.NameDesc;
      }
      else
      {
        this._tsBtnAlphabet.Image = this._imgList.Images["SortAlphabetAsc.png"];
        this._tsBtnAlphabet.Tag = (object) ThumbnailView.SortMethods.NameAsc;
      }
    }
    else
    {
      this._tsBtnAlphabet.Checked = true;
      this._tsBtnNumber.Checked = false;
    }
    this._items.Sort((IComparer<ThumbnailItem>) new ThumbnailView.ComparerThumbnailItem((ThumbnailView.SortMethods) this._tsBtnAlphabet.Tag));
    this.UpdateView();
  }

  /// <summary>Сортировка по идентификатору объекта.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_tsBtnNumber_Click(object sender, EventArgs e)
  {
    if (this._tsBtnNumber.Checked)
    {
      if ((ThumbnailView.SortMethods) this._tsBtnNumber.Tag == ThumbnailView.SortMethods.NumAsc)
      {
        this._tsBtnNumber.Image = this._imgList.Images["SortNumberDesc.png"];
        this._tsBtnNumber.Tag = (object) ThumbnailView.SortMethods.NumDesc;
      }
      else
      {
        this._tsBtnNumber.Image = this._imgList.Images["SortNumberAsc.png"];
        this._tsBtnNumber.Tag = (object) ThumbnailView.SortMethods.NumAsc;
      }
    }
    else
    {
      this._tsBtnAlphabet.Checked = false;
      this._tsBtnNumber.Checked = true;
    }
    this._items.Sort((IComparer<ThumbnailItem>) new ThumbnailView.ComparerThumbnailItem((ThumbnailView.SortMethods) this._tsBtnNumber.Tag));
    this.UpdateView();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_tsBtnSearch_Click(object sender, EventArgs e)
  {
    int num = this.SearchItem(this._thumbnails.ItemIndex + 1, this._items.Count, this._tsTxtSearch.Text);
    if (num == -1 && this._thumbnails.ItemIndex > -1)
      num = this.SearchItem(0, this._thumbnails.ItemIndex, this._tsTxtSearch.Text);
    if (num == -1)
      return;
    this._thumbnails.ItemIndex = num;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void On_tsTxtSearch_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.On_tsBtnSearch_Click(sender, new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="imageIndex"></param>
  /// <returns></returns>
  protected virtual object OnGetImage(int imageIndex)
  {
    ThumbnailItem thumbnailItem = this._items[imageIndex];
    object image = thumbnailItem.Image;
    if (image != null || this._cache == null)
      return image;
    long newObjectId;
    object picture = this._cache.GetPicture(thumbnailItem.TypeId, thumbnailItem.PictureObjectId, out newObjectId);
    if (thumbnailItem.PictureObjectId != newObjectId)
      thumbnailItem.PictureObjectId = newObjectId;
    thumbnailItem.Image = picture;
    return picture;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnNotificationService_Notify(object sender, NotificationEventArgs e)
  {
    if (e == null || !(e.EventName == "ObjectsRemoved") || !(e is DBObjectsEventArgs objectsEventArgs) || objectsEventArgs.ObjectIDs == null)
      return;
    List<ThumbnailItem> thumbnailItemList = new List<ThumbnailItem>(objectsEventArgs.ObjectIDs.Count);
    int count1 = this._items != null ? this._items.Count : 0;
    for (int index = 0; index < count1; ++index)
    {
      ThumbnailItem thumbnailItem = this._items[index];
      if (objectsEventArgs.ObjectIDs.Contains(thumbnailItem.ObjectId))
        thumbnailItemList.Add(thumbnailItem);
    }
    int count2 = thumbnailItemList.Count;
    for (int index1 = 0; index1 < count2; ++index1)
    {
      int index2 = this._items.IndexOf(thumbnailItemList[index1]);
      if (index2 != -1)
        this._items.RemoveAt(index2);
    }
    if (count2 <= 0 || this._thumbnails == null)
      return;
    this._thumbnails.Count = this._items.Count;
    this._thumbnails.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private static void PropPages_Changed(object sender, EventArgs e)
  {
    ThumbnailView._fetchCount = 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="oldIndex"></param>
  /// <param name="newIndex"></param>
  private void Thumbnails_SelectionChanged(object sender, int oldIndex, int newIndex)
  {
    this._selectedItems.Invalidate();
    this.OnSelectedItemsChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Thumbnails_ShowContextMenu(object sender, ThumbnailEventArgs e)
  {
    int itemIndex = e.ItemIndex;
    ThumbnailItem thumbnailItem = (ThumbnailItem) null;
    if (itemIndex != -1 && itemIndex < this._items.Count)
      thumbnailItem = this._items[itemIndex];
    if (thumbnailItem != null && thumbnailItem.ObjectId != 0L)
    {
      ContextMenuBarItem contextMenu = this.GetContextMenu(thumbnailItem);
      try
      {
        this._updateIndex = itemIndex;
        contextMenu.Show((System.Windows.Forms.Control) this, e.Pos);
      }
      finally
      {
        this._updateIndex = -1;
        this._item = (ThumbnailItem) null;
      }
    }
    else
    {
      ServiceContainer serviceContainer = new ServiceContainer();
      serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.ReadOnly));
      Intermech.Navigator.ContextMenu.Services.GetMenu((ISelectedItems) new NodeItems(this._path, this.Node, new NodeIDCollection(), (System.IServiceProvider) serviceContainer), (System.IServiceProvider) serviceContainer)?.Show((System.Windows.Forms.Control) this, e.Pos);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Thumbnails_StopResize(object sender, EventArgs e)
  {
    ThumbnailView._panelSize = this._thumbnails.PanelSize;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="item"></param>
  /// <param name="imageID"></param>
  private void AssignImage(ThumbnailItem item, long imageID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(item.ObjectId);
      if (dbObject == null)
        return;
      IDBAttribute dbAttribute = dbObject.GetAttributeByID(Consts.ImageAttTypeID) ?? dbObject.Attributes.AddAttribute(Consts.ImageAttTypeID, false);
      if (dbAttribute == null)
        return;
      dbAttribute.AsInteger = imageID;
      item.Image = (object) null;
      item.PictureObjectId = item.ObjectId;
      this._thumbnails.RepaintItem(this._updateIndex);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="item"></param>
  /// <returns></returns>
  protected virtual ContextMenuBarItem GetContextMenu(ThumbnailItem item)
  {
    this._item = item;
    bool flag = item.TypeId == Consts.ImageLibraryItemTypeID;
    if (this._contextMenu == null)
    {
      this._contextMenu = (ContextMenuBarItem) new PopupMenuBarItem();
      ((PopupMenuBarItem) this._contextMenu).PopupHost = ThumbnailView._host;
      int imageIndex1 = -1;
      INamedImageList nil = ThumbnailView._nil;
      if (nil != null)
        imageIndex1 = ThumbnailView._nil.ImageIndex("imgOpenItem");
      this._contextMenu.Items.Add((ToolbarItemBase) new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_1020"), new EventHandler(this.OnLoadImage), imageIndex1));
      int imageIndex2 = nil == null ? -1 : nil.ImageIndex("imgDelete");
      this._contextMenu.Items.Add((ToolbarItemBase) new MenuButtonItem(LocalizationHolder.rm.GetString("ThumbnailView_RemoveImage"), new EventHandler(this.OnDeleteImage), imageIndex2));
      int imageIndex3 = nil == null ? -1 : nil.ImageIndex("imgImageLib");
      this._contextMenu.Items.Add((ToolbarItemBase) new MenuButtonItem(LocalizationHolder.rm.GetString("ThumbnailView_SelectFromLibrary"), new EventHandler(this.OnAssignImage), imageIndex3));
      int imageIndex4 = nil == null ? -1 : nil.ImageIndex("imgClean");
      this._contextMenu.Items.Add((ToolbarItemBase) new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_1128"), new EventHandler(this.OnCleanImage), imageIndex4));
      MenuButtonItem menuButtonItem = new MenuButtonItem(LocalizationHolder.rm.GetString("Client.Core_378"), nil != null ? nil.ImageIndex("imgView") : -1);
      menuButtonItem.Click += (EventHandler) ((sender, e) => this.ShowImageEvent(sender, e, this._item));
      this._contextMenu.Items.Add((ToolbarItemBase) menuButtonItem);
    }
    this._contextMenu.Items[0].Enabled = true;
    this._contextMenu.Items[1].Visible = flag;
    this._contextMenu.Items[2].Visible = !flag;
    this._contextMenu.Items[3].Visible = !flag;
    return this._contextMenu;
  }

  /// <summary>
  /// 
  /// </summary>
  protected void GetDataPacket()
  {
    INodeQuery query = this.Node.GetQuery(this.ContentType);
    if (query == null)
      return;
    this.ApplyColumns(query);
    query.Execute(this._bookmark, this._readAllMode ? 2147483646 : ThumbnailView.FetchCount);
    this._bookmark = query.Bookmark;
    this._eof = this._bookmark == null;
    this._readedCount += query.RecordCount;
    try
    {
      if (this._items == null)
        this._items = new List<ThumbnailItem>(this._readedCount);
      if (this._items.Capacity < this._readedCount)
        this._items.Capacity = this._readedCount;
      for (int index = 0; index < query.RecordCount; ++index)
        this._items.Add(this.CreateThumbnailItem(query.GetRecordNodeID(index), query.GetRecordValues(index)));
    }
    finally
    {
      ThumbnailView.SortMethods sortMethod = ThumbnailView.SortMethods.NameAsc;
      if (this._tsBtnAlphabet.Checked)
        sortMethod = (ThumbnailView.SortMethods) this._tsBtnAlphabet.Tag;
      else if (this._tsBtnNumber.Checked)
        sortMethod = (ThumbnailView.SortMethods) this._tsBtnNumber.Tag;
      this._items.Sort((IComparer<ThumbnailItem>) new ThumbnailView.ComparerThumbnailItem(sortMethod));
      this.UpdateView();
      this.UpdateStatusbar();
    }
  }

  protected virtual ThumbnailItem CreateThumbnailItem(INodeID nodeID, object[] record)
  {
    return new ThumbnailItem(nodeID, Convert.ToString(record[0]), Convert.ToInt64(record[1]), Convert.ToInt32(record[2]));
  }

  /// <summary>
  /// 
  /// </summary>
  private void OnSelectedItemsChanged()
  {
    if (this.SelectedItemsChanged == null)
      return;
    this.SelectedItemsChanged((object) this, EventArgs.Empty);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="startIndex"></param>
  /// <param name="endIndex"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  protected virtual int SearchItem(int startIndex, int endIndex, string text)
  {
    if (string.IsNullOrWhiteSpace(text) || this._items.Count == 0 || startIndex < 0 || endIndex < 0 || endIndex < startIndex)
      return -1;
    for (int index = startIndex; index < endIndex; ++index)
    {
      if (this._items[index].Name.IndexOf(text, 0, StringComparison.InvariantCultureIgnoreCase) >= 0)
        return index;
    }
    return -1;
  }

  /// <summary>
  /// 
  /// </summary>
  private void Subscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe(new NotificationEventHandler(this.OnNotificationService_Notify));
  }

  /// <summary>
  /// 
  /// </summary>
  private void UnSubscribe()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Unsubscribe(new NotificationEventHandler(this.OnNotificationService_Notify));
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateStatusbar()
  {
    if (this._items == null)
      return;
    string str = string.Format(LocalizationHolder.rm.GetString("Client.Core_1175"), (object) this._items.Count);
    if (this._pnlReaded.Text != str)
      this._pnlReaded.Text = str;
    this._btnReadAll.Visible = this._btnReadNext.Visible = !this._eof;
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateView()
  {
    if (this._renderer != null)
      this._renderer.Items = this._items;
    this._thumbnails.Count = this._items.Count;
    this._thumbnails.Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="disposing"></param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this._cache != null)
    {
      this._cache.LoadComplete -= new LoadCompleteEventHandler(this.Cache_LoadComplete);
      this._cache.CacheChanged -= new CacheChangedEventHandler(this.Cache_Changed);
      this._cache = (IPicturesCache) null;
      this.UnSubscribe();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// 
  /// </summary>
  public int ImageIndex => ThumbnailView._imageIndex;

  /// <summary>
  /// 
  /// </summary>
  public virtual int OrderID => int.MinValue;

  /// <summary>
  /// 
  /// </summary>
  public virtual string Caption => LocalizationHolder.rm.GetString("Client.Core_720");

  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  public virtual void Initialize(ISelectedItems items, System.IServiceProvider services)
  {
    NodeIDPath parentPath = items.GetParentPath(0);
    this._services.AdvancedProvider = services;
    this._dispatcher = services.GetService(typeof (IIODispatcher)) as IIODispatcher;
    this._parentNode = (INode) items.GetItemData(0, typeof (INode));
    this._nodeID = items.GetItemID(0);
    this._path = new NodeIDPath(parentPath, this._nodeID);
    this._node = (INode) null;
    this._items = (List<ThumbnailItem>) null;
    this._thumbnails.Count = 0;
    this._selectedItems = new ThumbnailSelectedItems(this._path, this.Node, this);
    this._readedCount = 0;
    this._readAllMode = false;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nextView"></param>
  public virtual void Deactivate(IView nextView)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="previousView"></param>
  public virtual void Activate(IView previousView)
  {
    if (this._items != null)
      return;
    if (this._cache != null)
      this._sessionId = this._cache.Session;
    this.GetDataPacket();
  }

  /// <summary>Если true, то оставлять закладку активной.</summary>
  public bool RemainActiveView
  {
    get
    {
      return ((this.IsDisposed ? 1 : 0) | (this._items == null ? 0 : (this._items.Count > 0 ? 1 : 0))) != 0;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  public bool Execute(ICommandState commandState) => false;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="commandState"></param>
  /// <returns></returns>
  public bool QueryStatus(ICommandState commandState) => false;

  /// <summary>
  /// 
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ISelectedItems SelectedItems => (ISelectedItems) this._selectedItems;

  /// <summary>
  /// 
  /// </summary>
  public event EventHandler SelectedItemsChanged;

  /// <summary>Найти путь к сфокусированной записи (если она папка).</summary>
  /// <returns>Путь к сфокусированной записи</returns>
  internal NodeIDPath GetSelectedNodeIDPath()
  {
    if (this.SelectedItem == null)
      return (NodeIDPath) null;
    NodeIDPath selectedNodeIdPath = (NodeIDPath) null;
    INodeID nodeId = this.SelectedItem.NodeID;
    bool flag = nodeId != null && nodeId.CategoryID == 1 && MetaDataHelper.IsObjectTypeChildOf(nodeId.TypeID, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545"));
    if (nodeId != null && (this.Node.GetAttributesOf(nodeId) & ContentAttributes.Folder) == ContentAttributes.Folder | flag)
    {
      if (nodeId is INodeIDExtended)
        this._path = (nodeId as INodeIDExtended).CorrectPath(this._path, nodeId);
      selectedNodeIdPath = new NodeIDPath(this._path, nodeId);
    }
    return selectedNodeIdPath;
  }

  /// <summary>
  /// Элемент управления, который является источником событий.
  /// </summary>
  public object Control
  {
    get => (object) this;
    set
    {
    }
  }

  /// <summary>Контейнер сервисов.</summary>
  public System.IServiceProvider Services
  {
    get => (System.IServiceProvider) this._services;
    set
    {
    }
  }

  /// <summary>Коллекция выделенных в элементе управления элементов.</summary>
  ISelectedItems IIOSource.SelectedItems
  {
    get => this.SelectedItems;
    set
    {
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnAssignImage(object sender, EventArgs e)
  {
    if (this._item == null)
      return;
    IDBObjectID[] dbObjectIdArray = SelectorForm.SelectObjects(new int[1]
    {
      Consts.ImageLibraryItemTypeID
    });
    if (dbObjectIdArray == null || dbObjectIdArray.Length == 0)
      return;
    this.AssignImage(this._item, dbObjectIdArray[0].Value);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnCleanImage(object sender, EventArgs e)
  {
    if (this._item == null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._item.ObjectId);
      if (dbObject == null)
        return;
      IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.ImageAttTypeID);
      if (attributeById == null)
        return;
      if (attributeById.AttributeType is IDBAttributeType4Object attributeType)
      {
        if (attributeType.Required == RequiredModes.AutoRequired)
          attributeById.Value = attributeType.DefaultValue;
        else
          attributeById.Delete(0L);
      }
      else
        attributeById.Delete(0L);
      this._item.Image = (object) null;
      this._item.PictureObjectId = this._item.ObjectId;
      this._thumbnails.RepaintItem(this._updateIndex);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnDeleteImage(object sender, EventArgs e)
  {
    if (this._item == null)
      return;
    ObjectCommands.DeleteCommand((ISelectedItems) this._selectedItems, (System.IServiceProvider) this._services, (object) null);
  }

  /// <summary>Обработчик события Показать изображение</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// <param name="thItem"></param>
  private void ShowImageEvent(object sender, EventArgs e, ThumbnailItem thItem)
  {
    FullImageView.ShowImage(thItem.Image);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void OnLoadImage(object sender, EventArgs e)
  {
    if (this._item == null)
      return;
    if (this._item.TypeId == Consts.ImageLibraryItemTypeID)
    {
      if (!this._cache.UpdateItem(this._item.TypeId, this._item.ObjectId))
        return;
      this._item.CleanCache();
      this._thumbnails.RepaintItem(this._updateIndex);
    }
    else
    {
      if (!(ServicesManager.GetService(typeof (IObjectCreatorService)) is IObjectCreatorService service))
        return;
      long objectByTypeDialog = service.CreateObjectByTypeDialog(Consts.ImageLibraryItemTypeID);
      switch (objectByTypeDialog)
      {
        case -1:
          break;
        case 0:
          break;
        default:
          Holder.NotificationService.FireEvent((object) null, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", objectByTypeDialog));
          this.AssignImage(this._item, objectByTypeDialog);
          break;
      }
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="hWnd"></param>
  /// <param name="fAccept"></param>
  [DllImport("shell32.dll", CharSet = CharSet.Ansi)]
  public static extern void DragAcceptFiles(IntPtr hWnd, bool fAccept);

  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 563)
    {
      m.Result = new IntPtr(1);
      IntPtr wparam = m.WParam;
    }
    base.WndProc(ref m);
  }

  protected override void OnSizeChanged(EventArgs e)
  {
    base.OnSizeChanged(e);
    if (this.Parent == null || !this.IsHandleCreated)
      return;
    Size clientSize = this._thumbnails.ClientSize;
    if (clientSize.IsEmpty)
      return;
    clientSize.Height -= 16 /*0x10*/;
    clientSize.Width -= 16 /*0x10*/;
    if (this._thumbnails == null || this._thumbnails.PanelSize.Height >= clientSize.Height && this._thumbnails.PanelSize.Width >= clientSize.Width)
      return;
    clientSize.Height = Math.Max(Math.Min(clientSize.Height, this._thumbnails.PanelSize.Height), 32 /*0x20*/);
    clientSize.Width = Math.Max(Math.Min(clientSize.Width, this._thumbnails.PanelSize.Width), 32 /*0x20*/);
  }

  private class ComparerThumbnailItem : IComparer<ThumbnailItem>
  {
    private ThumbnailView.SortMethods _sortMethod;

    /// <summary>Конструктор.</summary>
    /// <param name="sortMethod">Способ сортировки</param>
    internal ComparerThumbnailItem(ThumbnailView.SortMethods sortMethod)
    {
      this._sortMethod = sortMethod;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item1"></param>
    /// <param name="item2"></param>
    /// <returns></returns>
    public int Compare(ThumbnailItem item1, ThumbnailItem item2)
    {
      switch (this._sortMethod)
      {
        case ThumbnailView.SortMethods.NameDesc:
          return item2.Name.CompareTo(item1.Name);
        case ThumbnailView.SortMethods.NumAsc:
          return item1.ObjectId.CompareTo(item2.ObjectId);
        case ThumbnailView.SortMethods.NumDesc:
          return item2.ObjectId.CompareTo(item1.ObjectId);
        default:
          return item1.Name.CompareTo(item2.Name);
      }
    }
  }

  /// <summary>
  /// Перечисление для указания способа сортировки элементов.
  /// </summary>
  protected enum SortMethods
  {
    NameAsc,
    NameDesc,
    NumAsc,
    NumDesc,
  }

  protected class ThumbnailViewDescriptionProvider : BaseViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      System.IServiceProvider serviceProvider)
    {
      return new ViewDescription()
      {
        Caption = LocalizationHolder.rm.GetString("Client.Core_720"),
        ImageIndex = -1,
        OrderID = int.MinValue
      };
    }
  }
}
