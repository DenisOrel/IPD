
// Type: Intermech.Client.Core.Thumbnail.FullImageView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.Show.Net;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Map;
using Intermech.Redline;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>
/// Форма просмотра картинок с эскизов страниц и изображений на пользовательских формах
/// </summary>
public class FullImageView : Form
{
  private object _picture;
  private Stack _viewStack = new Stack();
  private bool _pushChanges;
  private static FullImageView _instance;
  private static PicturesCache _picturesCache;
  private static INamedImageList _namedImageList;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar toolBar;
  private ButtonItem btZoom1to1;
  private MenuBar menuBar1;
  private ContextMenuBarItem zoomContextMenu;
  private ButtonItem btZoomAll;
  private ButtonItem btZoomOut;
  private ButtonItem btZoomIn;
  private ButtonItem btZoomPrevious;
  private MenuButtonItem mnZoomPrevious;
  private MenuButtonItem mnZoomIn;
  private MenuButtonItem mnZoomOut;
  private MenuButtonItem mnZoom1to1;
  private MenuButtonItem mnZoomAll;
  private FullImageMapView _view;

  /// <summary>Показать изображение</summary>
  /// <param name="obj">id объекта или image или IThumbImageProvider</param>
  public static void ShowImage(object obj)
  {
    if (FullImageView._instance == null || FullImageView._instance.IsDisposed)
    {
      FullImageView fullImageView = new FullImageView();
      fullImageView.Text = LocalizationHolder.rm.GetString("Client.Core_378");
      FullImageView._instance = fullImageView;
    }
    if (!FullImageView._instance.LoadData(obj))
      return;
    FullImageView._instance.Show();
  }

  private static PicturesCache PicturesCache
  {
    get
    {
      return FullImageView._picturesCache ?? (FullImageView._picturesCache = ServicesManager.GetService(typeof (IPicturesCache)) as PicturesCache);
    }
  }

  private static INamedImageList NamedImageList
  {
    get
    {
      return FullImageView._namedImageList ?? (FullImageView._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList);
    }
  }

  private FullImageView()
  {
    this.InitializeComponent();
    this.Init();
    this.LoadSettings();
  }

  /// <summary>Загрузка настроек формы</summary>
  private void LoadSettings()
  {
    HybridDictionary hybridDictionary = new HybridDictionary(0, true);
    FormStorage.LoadLayout((Control) this, (IDictionary) hybridDictionary);
    Point point = Point.Empty;
    Size size = Size.Empty;
    if (hybridDictionary.Count > 0)
    {
      point = hybridDictionary.Contains((object) "Location") ? (Point) hybridDictionary[(object) "Location"] : Point.Empty;
      size = hybridDictionary.Contains((object) "Size") ? (Size) hybridDictionary[(object) "Size"] : Size.Empty;
    }
    this.Location = point != Point.Empty ? point : this.Location;
    this.Size = size != Size.Empty ? size : this.Size;
  }

  /// <summary>Сохрание настроек формы</summary>
  private void SaveSettings()
  {
    FormStorage.SaveLayout((Control) this, (IDictionary) new HybridDictionary(0, true)
    {
      {
        (object) "Location",
        (object) this.Location
      },
      {
        (object) "Size",
        (object) this.Size
      }
    });
  }

  /// <summary>Инициализация контролов формы</summary>
  private void Init()
  {
    this.SuspendLayout();
    this.toolBar.ImageList = FullImageView.NamedImageList.ImageList;
    this.menuBar1.ImageList = FullImageView.NamedImageList.ImageList;
    this.btZoomAll.Text = this.btZoomAll.ToolTipText = this.mnZoomAll.Text = this.mnZoomAll.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1688");
    this.btZoomAll.ImageIndex = this.mnZoomAll.ImageIndex = FullImageView.NamedImageList.ImageIndex("imgZoomAll");
    this.btZoom1to1.Text = this.btZoom1to1.ToolTipText = this.mnZoom1to1.Text = this.mnZoom1to1.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1689");
    this.btZoom1to1.ImageIndex = this.mnZoom1to1.ImageIndex = FullImageView.NamedImageList.ImageIndex("imgZoom1to1");
    this.btZoomIn.Text = this.btZoomIn.ToolTipText = this.mnZoomIn.Text = this.mnZoomIn.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1690");
    this.btZoomIn.ImageIndex = this.mnZoomIn.ImageIndex = FullImageView.NamedImageList.ImageIndex("imgZoomIn");
    this.btZoomOut.Text = this.btZoomOut.ToolTipText = this.mnZoomOut.Text = this.mnZoomOut.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1691");
    this.btZoomOut.ImageIndex = this.mnZoomOut.ImageIndex = FullImageView.NamedImageList.ImageIndex("imgZoomOut");
    this.btZoomPrevious.ImageIndex = this.mnZoomPrevious.ImageIndex = FullImageView.NamedImageList.ImageIndex("imgZoomPrevious");
    this.btZoomPrevious.Text = this.btZoomPrevious.ToolTipText = this.mnZoomPrevious.Text = this.mnZoomPrevious.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_1692");
    this.zoomContextMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.ZoomContextMenu_BeforePopup);
    this.mnZoomPrevious.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoomIn.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoomOut.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoom1to1.Click += new EventHandler(this.ZoomButtons_Click);
    this.mnZoomAll.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomPrevious.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomIn.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomOut.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoom1to1.Click += new EventHandler(this.ZoomButtons_Click);
    this.btZoomAll.Click += new EventHandler(this.ZoomButtons_Click);
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.ResumeLayout(false);
  }

  /// <summary>Загрузить данные для просмотра</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  private bool LoadData(object obj)
  {
    this.CloseView();
    switch (obj)
    {
      case long objId:
        this.LoadPictureFromObject(objId);
        break;
      case Image _:
      case IThumbImageProvider _:
        this._picture = obj;
        break;
    }
    if (!(this._picture is Image image1))
      image1 = this._picture is IThumbImageProvider picture ? picture.Image : (Image) null;
    Image image2 = image1;
    if (image2 == null)
      return false;
    this.AttachMapObject((MapObject) new ImageObject(image2, false));
    this.UpdateButtons();
    return true;
  }

  /// <summary>Получить изображение по objectId</summary>
  /// <param name="objId"></param>
  private void LoadPictureFromObject(long objId)
  {
    if (objId == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(objId, false);
      if (dbObject == null)
        return;
      int objectType = dbObject.ObjectType;
      PicturesCache picturesCache = FullImageView.PicturesCache;
      if (picturesCache == null)
        return;
      if (picturesCache.IsImageLibraryItem(objectType))
      {
        this._picture = picturesCache.GetPicture(objId);
      }
      else
      {
        IDBAttribute attributeById = dbObject.GetAttributeByID(Consts.ImageAttTypeID);
        if (attributeById == null || attributeById.AsInteger < 0L)
          return;
        this._picture = picturesCache.LookInCache(attributeById.AsInteger);
      }
    }
  }

  /// <summary>Отобразить MapObject</summary>
  /// <param name="mapObject"></param>
  private void AttachMapObject(MapObject mapObject)
  {
    this._pushChanges = false;
    if (mapObject != null)
    {
      this._viewStack.Clear();
      this._view.Document.Add(mapObject);
      RectangleF bounds = mapObject.Bounds;
      this._view.Document.TopLeft = bounds.Location;
      this._view.Document.Size = bounds.Size;
      this._view.DocPosition = PointF.Empty;
      this._view.ZoomToFit();
    }
    this._pushChanges = true;
  }

  /// <summary>Очистка просмотрщика</summary>
  private void CloseView()
  {
    this._picture = (object) null;
    try
    {
      this._pushChanges = false;
      MapDocument document = this._view.Document;
      MapLayerCollectionEnumerator enumerator = document.Layers.GetEnumerator();
      while (enumerator.MoveNext())
      {
        MapLayer current = enumerator.Current;
        MapObject[] mapObjectArray = current.CopyArray();
        current.Clear();
        for (int index = 0; index < mapObjectArray.Length; ++index)
        {
          using (mapObjectArray[index])
            mapObjectArray[index] = (MapObject) null;
        }
      }
      document.Clear();
      this._view.InitializeLayersFromDocument();
      this._pushChanges = true;
    }
    catch (Exception ex)
    {
    }
    finally
    {
      this._pushChanges = true;
    }
  }

  /// <summary>Признак доступность view</summary>
  /// <returns></returns>
  private bool ViewEnabled() => this._view != null && this._view.Visible;

  /// <summary>Обносить состояние кнопок на панели</summary>
  private void UpdateButtons()
  {
    if (this.toolBar == null)
      return;
    this.toolBar.BeginUpdate();
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.toolBar.Items)
    {
      if (!(toolbarItemBase.CommandName != "ZoomPrevious"))
        toolbarItemBase.Enabled = this._viewStack.Count > 1;
    }
    this.toolBar.EndUpdate();
  }

  /// <summary>Рендер тулбара</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = sender is BarManager barManager ? barManager.Renderer : (IToolBarRenderer) null;
    this.menuBar1.Renderer = renderer;
    this.toolBar.Renderer = renderer;
  }

  /// <summary>Оработчик команд zoom</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ZoomButtons_Click(object sender, EventArgs e)
  {
    if (!this.ViewEnabled() || !(sender is ButtonItemBase buttonItemBase))
      return;
    switch (buttonItemBase.CommandName)
    {
      case "ZoomAll":
        this._view.ZoomToFit();
        break;
      case "ZoomIn":
        this._view.ZoomIn();
        break;
      case "ZoomOut":
        this._view.ZoomOut();
        break;
      case "Zoom1to1":
        this._view.Zoom1to1();
        break;
      case "ZoomPrevious":
        if (this._viewStack.Count > 0)
        {
          this._pushChanges = false;
          if (this._viewStack.Count > 1)
            this._viewStack.Pop();
          FullImageView.PosAndScale posAndScale = (FullImageView.PosAndScale) this._viewStack.Peek();
          this._view.SetPosAndScale(posAndScale.Pos, posAndScale.Scale);
          this._pushChanges = true;
        }
        buttonItemBase.Enabled = this._viewStack.Count > 1;
        break;
    }
  }

  /// <summary>
  /// Актуализация состояния команды контекстного меню Предыдущий вид
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ZoomContextMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.mnZoomPrevious.Enabled = this._viewStack.Count > 1;
  }

  /// <summary>Изменение вида просмотрщика</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void View_ViewChanging(object sender, EventArgs e)
  {
    if (!this._pushChanges)
      return;
    FullImageView.PosAndScale posAndScale1 = new FullImageView.PosAndScale(this._view.DocPosition, this._view.DocScale);
    if (this._viewStack.Count > 0)
    {
      FullImageView.PosAndScale posAndScale2 = (FullImageView.PosAndScale) this._viewStack.Peek();
      if (posAndScale1.Pos == posAndScale2.Pos && (double) posAndScale1.Scale == (double) posAndScale2.Scale)
        return;
    }
    this._viewStack.Push((object) posAndScale1);
    this.UpdateButtons();
  }

  /// <summary>Изменение размера формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FullImageView_SizeChanged(object sender, EventArgs e)
  {
    if (!this._view.IsZoomToFit)
      return;
    this._view.ZoomToFit();
  }

  /// <summary>Обработска закрытия формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FullImageView_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.SaveSettings();
    if (e.CloseReason != CloseReason.UserClosing)
      return;
    e.Cancel = true;
    this.Hide();
  }

  /// <summary>Показ формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FullImageView_Shown(object sender, EventArgs e) => this._view.ZoomToFit();

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
    this.toolBar = new Intermech.Bars.ToolBar();
    this.btZoomAll = new ButtonItem();
    this.btZoom1to1 = new ButtonItem();
    this.btZoomOut = new ButtonItem();
    this.btZoomIn = new ButtonItem();
    this.btZoomPrevious = new ButtonItem();
    this.menuBar1 = new MenuBar();
    this.zoomContextMenu = new ContextMenuBarItem();
    this.mnZoomPrevious = new MenuButtonItem();
    this.mnZoomIn = new MenuButtonItem();
    this.mnZoomOut = new MenuButtonItem();
    this.mnZoom1to1 = new MenuButtonItem();
    this.mnZoomAll = new MenuButtonItem();
    this._view = new FullImageMapView();
    this.SuspendLayout();
    this.toolBar.AllowRightToLeft = true;
    this.toolBar.FullMenus = true;
    this.toolBar.Guid = new Guid("4966cc2d-ca45-4b1e-ba30-ba5448c12f30");
    this.toolBar.Hidden = false;
    this.toolBar.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.btZoomAll,
      (ToolbarItemBase) this.btZoom1to1,
      (ToolbarItemBase) this.btZoomOut,
      (ToolbarItemBase) this.btZoomIn,
      (ToolbarItemBase) this.btZoomPrevious
    });
    this.toolBar.Location = new Point(0, 0);
    this.toolBar.Name = "toolBar";
    this.toolBar.RightToLeft = RightToLeft.Yes;
    this.toolBar.Size = new Size(749, 20);
    this.toolBar.TabIndex = 3;
    this.toolBar.Text = "toolBar";
    this.btZoomAll.BeginGroup = true;
    this.btZoomAll.CommandName = "ZoomAll";
    this.btZoom1to1.CommandName = "Zoom1to1";
    this.btZoomOut.CommandName = "ZoomOut";
    this.btZoomIn.CommandName = "ZoomIn";
    this.btZoomPrevious.CommandName = "ZoomPrevious";
    this.menuBar1.Guid = new Guid("4eb4344b-b3cc-4da6-91b3-b23dfe5ea092");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.zoomContextMenu
    });
    this.menuBar1.Location = new Point(0, 20);
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) this;
    this.menuBar1.Size = new Size(749, 26);
    this.menuBar1.TabIndex = 4;
    this.menuBar1.Text = "menuBar1";
    this.menuBar1.Visible = false;
    this.zoomContextMenu.CommandName = "zoomContextMenu";
    this.zoomContextMenu.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.mnZoomPrevious,
      (ToolbarItemBase) this.mnZoomIn,
      (ToolbarItemBase) this.mnZoomOut,
      (ToolbarItemBase) this.mnZoom1to1,
      (ToolbarItemBase) this.mnZoomAll
    });
    this.zoomContextMenu.ShowText = true;
    this.mnZoomPrevious.CommandName = "ZoomPrevious";
    this.mnZoomPrevious.ShowText = true;
    this.mnZoomPrevious.Text = "ZoomPrevious";
    this.mnZoomIn.CommandName = "ZoomIn";
    this.mnZoomIn.ShowText = true;
    this.mnZoomIn.Text = "ZoomIn";
    this.mnZoomOut.CommandName = "ZoomOut";
    this.mnZoomOut.ShowText = true;
    this.mnZoomOut.Text = "ZoomOut";
    this.mnZoom1to1.CommandName = "Zoom1to1";
    this.mnZoom1to1.ShowText = true;
    this.mnZoom1to1.Text = "Zoom1to1";
    this.mnZoomAll.CommandName = "ZoomAll";
    this.mnZoomAll.ShowText = true;
    this.mnZoomAll.Text = "ZoomAll";
    this._view.AllowCopy = false;
    this._view.AllowDelete = false;
    this._view.AllowDrop = true;
    this._view.AllowEdit = false;
    this._view.AllowInsert = false;
    this._view.BackColor = Color.White;
    this._view.Dock = DockStyle.Fill;
    this._view.DragsRealtime = true;
    this._view.Location = new Point(0, 46);
    this._view.Name = "_view";
    this._view.Size = new Size(749, 412);
    this._view.TabIndex = 5;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(749, 458);
    this.Controls.Add((Control) this._view);
    this.Controls.Add((Control) this.menuBar1);
    this.Controls.Add((Control) this.toolBar);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FullImageView);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.Manual;
    this.TopMost = true;
    this.FormClosing += new FormClosingEventHandler(this.FullImageView_FormClosing);
    this.Shown += new EventHandler(this.FullImageView_Shown);
    this.SizeChanged += new EventHandler(this.FullImageView_SizeChanged);
    this.ResumeLayout(false);
  }

  [DebuggerDisplay("Scale={Scale} (x={Pos.X}, y={Pos.Y})")]
  private struct PosAndScale(PointF pos, float scale)
  {
    public PointF Pos = pos;
    public float Scale = scale;
  }
}
