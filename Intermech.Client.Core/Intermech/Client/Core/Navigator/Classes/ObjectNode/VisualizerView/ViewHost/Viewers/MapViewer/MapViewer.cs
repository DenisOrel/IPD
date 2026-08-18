
// Type: Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.Interfaces;
using Intermech.Client.Core.PropertyEditors;
using Intermech.Client.Core.Redline;
using Intermech.Client.Core.Visualizers;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Show;
using Intermech.Localization;
using Intermech.Map;
using Intermech.Navigator.Interfaces;
using Intermech.Redline;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer;

/// <summary>Просмотрщик MapObject</summary>
public class MapViewer : 
  UserControl,
  IViewer,
  IPagerSupport,
  IRedlinerSupport,
  IZoomSupport,
  IDistanceMeasureSupport,
  IColorDwgSupport,
  IOverviewSupport
{
  /// <summary>
  /// Режим отображения пометок по всем доступным документам
  /// </summary>
  private ShowDocsMode _showRedLine4AllDocs;
  /// <summary>
  /// </summary>
  private readonly List<Redliner> _redliners = new List<Redliner>();
  /// <summary>признак можно ли редактировать пометки</summary>
  private bool _isRedlineEdit = true;
  /// <summary>
  /// 
  /// </summary>
  private EStatusRemark FilterFlags = EStatusRemark.eAll;
  /// <summary>
  /// 
  /// </summary>
  private bool _isRedFormExpand = true;
  /// <summary>
  /// 
  /// </summary>
  private RedProperty _redMapProperty = new RedProperty();
  /// <summary>
  /// 
  /// </summary>
  private Control _owner;
  /// <summary>
  /// 
  /// </summary>
  private MapObject _mapObject;
  /// <summary>
  /// 
  /// </summary>
  private IList<FileItem> _fileItems;
  /// <summary>
  /// 
  /// </summary>
  private Stack _viewStack = new Stack();
  /// <summary>
  /// 
  /// </summary>
  private PointF _notePropsPoint = PointF.Empty;
  /// <summary>Первая страница уже добавлена,
  /// флаг нужен для избежания путаницы в загрузке первой партии страниц и добавленных в фоновом потоке</summary>
  private bool _firstPageAdded;
  /// <summary>
  /// 
  /// </summary>
  private List<object> _pageBuff = new List<object>();
  /// <summary>
  /// 
  /// </summary>
  private List<object> _delayedBuffer = new List<object>();
  /// <summary>
  /// 
  /// </summary>
  private bool _distanceToolActivated;
  /// <summary>Выбранный отображаемый файл</summary>
  private FileItem _selectedFileItem;
  /// <summary>Поддержка страниц</summary>
  private IPager _pager;
  /// <summary>
  /// 
  /// </summary>
  private Redliner _redliner;
  /// <summary>Коэффициент растяжения графики (DpiX / 96)</summary>
  private float _factorDpiX;
  /// <summary>
  /// Должность/графа подписи для замечаний. Запоминается после первого выбора и очищается после деактивации вкладки
  /// </summary>
  private string _rankSignature;
  private string _newLineTemplate = "\\line ";
  private string boldTemplate = "\\b {0}: \\b0 ";
  private bool _isBlack;
  /// <summary>Требуется переменная конструктора.</summary>
  private IContainer components;
  private RedlineSplitContainer splitContainer;
  private RedlineSplitContainer splitContainerRedObject;
  private TreeView treeView;
  private RichTextBox tBoxComment;
  private TextBox tBoxStep;
  private Label label5;
  private TextBox tBoxBusiness_process;
  private Label lbUser;
  private TextBox tBoxTime;
  private Label lbTime;
  private TextBox tBoxUser;
  private Label lbStep;
  private RedlineSplitContainer splitContainerView;
  private MenuBar menuBarTreeView;
  private ContextMenuBarItem contextMenuBarItemTree;
  private MenuButtonItem mBtItem_Corrected;
  private MenuButtonItem mBtItem_Agreed;
  private MenuButtonItem mBtItem_Inconsistent;
  private MenuButtonItem mBtItem_Rename;
  private MenuButtonItem mBtItem_Remove;
  private MenuButtonItem mBtItem_Rejected;
  private Intermech.Bars.ToolBar toolBarTreeView;
  private ButtonItem btnNew;
  private ButtonItem btnComments;
  private ButtonItem btnCheckFilter1;
  private ButtonItem btnCheckFilter2;
  private ButtonItem btnCheckFilter3;
  private ButtonItem btnCheckFilter4;
  private Intermech.Bars.ToolBar toolBarRed;
  private ButtonItem btnSave;
  private ButtonItem btnUndo;
  private ButtonItem btnRedo;
  private ComboBoxItem cbBoxRole;
  private ButtonItem btPointer;
  private ButtonItem btRedLine;
  private ButtonItem btRedPencil;
  private ButtonItem btRedNote;
  private ButtonItem btRedEllipse;
  private ButtonItem btRedEllipseFill;
  private ButtonItem btRedCircle;
  private ButtonItem btRedCircleFill;
  private ButtonItem btRedRectangle;
  private ButtonItem btRedRectangleFill;
  private ButtonItem btnBlank;
  private ButtonItem btnRed;
  private MenuButtonItem mbtnShowAll;
  private DropDownMenuItem ddCheckShowAll;
  private MenuButtonItem mbtnShowWithRemarkOnly;
  private RedlineView view;
  private ProgressBar pageLoadProgressBar;

  /// <summary>Сервис для работы с именованными иконками</summary>
  private INamedImageList NamedImageList { get; } = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);

  /// <summary>
  /// 
  /// </summary>
  private BarManager BarManager { get; } = ServiceUtils.GetService<BarManager>((object) ServicesManager.ServiceContainer, true);

  /// <summary>
  /// 
  /// </summary>
  private IRedService RedService { get; } = ServiceUtils.GetService<IRedService>((object) ServicesManager.ServiceContainer, true);

  /// <summary>
  /// Сервис для управления визуализаторами файлов через MapObject
  /// </summary>
  private IVisualizerService VisualizerService { get; } = ServiceUtils.GetService<IVisualizerService>((object) ServicesManager.ServiceContainer, true);

  /// <summary>переключает форму в режим просмотра пометок</summary>
  private bool RedlineForm
  {
    get => this._isRedFormExpand;
    set
    {
      if (this._isRedFormExpand == value)
        return;
      this._isRedFormExpand = value;
      this.splitContainer.Panel1Collapsed = !this._isRedFormExpand;
      this.splitContainerView.Panel2Collapsed = !this._isRedFormExpand;
      this.toolBarRed.Visible = this._isRedFormExpand;
      this.toolBarTreeView.Visible = this._isRedFormExpand;
      this._Resize((object) this.splitContainer.Panel1);
      this._Resize((object) this.splitContainer.Panel2);
      this.FilterFlags = EStatusRemark.eAll;
      this.Update_toolBarTreeView();
      this.UpdateFilter();
      this.FillTreeView();
      this.CheckAllRedView();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private bool IsRedlineEdit
  {
    get => this._isRedlineEdit;
    set
    {
      this._isRedlineEdit = value;
      this.UpdateButtons_RedLiner();
    }
  }

  /// <summary>Проверим наличие редлайнинга</summary>
  private bool IsRedEnabled => this._redliner != null;

  /// <summary>
  /// проверка есть ли и включен _view = true - включен _view
  /// </summary>
  private bool ViewEnabled => this.view.Visible;

  /// <summary>
  /// 
  /// </summary>
  private bool PageLoadProgressBarEnabled
  {
    get => this.pageLoadProgressBar.Visible;
    set => this.SetProgressBarVisible(value);
  }

  public MapViewer()
  {
    this.InitializeComponent();
    using (Graphics graphics = this.CreateGraphics())
      this._factorDpiX = graphics.DpiX / 96f;
  }

  public event SetFileItemEventHandler SetFileItemCurentEvent;

  /// <summary>получить расширение файла без точки</summary>
  /// <param name="fileName">имя файла</param>
  /// <returns>расширение файла без точки</returns>
  private string GetExtension(string fileName)
  {
    string str = Path.GetExtension(fileName);
    return !string.IsNullOrEmpty(str) ? str.ToLower().Replace(".", string.Empty) : throw new ArgumentException("Value must be not null or empty", "extension");
  }

  /// <summary>
  /// 
  /// </summary>
  private void InitializeRedLineToolBar()
  {
    this.toolBarRed.ImageList = this.NamedImageList.ImageList;
    this.btnSave.ImageIndex = this.NamedImageList.ImageIndex("imgSave");
    this.btnSave.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_935");
    this.btnUndo.ImageIndex = this.NamedImageList.ImageIndex("imgUndo");
    this.btnUndo.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_524");
    this.btnRedo.ImageIndex = this.NamedImageList.ImageIndex("imgRedo");
    this.btnRedo.ToolTipText = LocalizationHolder.rm.GetString("Redo");
    this.cbBoxRole.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbBoxRole.ComboBox.FlatStyle = FlatStyle.Flat;
    this.cbBoxRole.Text = this.cbBoxRole.ToolTipText = LocalizationHolder.rm.GetString("Grafa");
    this.SubscribeCbBoxRoleEvents();
    this.ClearRoleText();
    this.btnRed.ImageIndex = this.NamedImageList.ImageIndex("imgRedEdit");
    this.btPointer.ImageIndex = this.NamedImageList.ImageIndex("imgPointer");
    this.btPointer.ToolTipText = LocalizationHolder.rm.GetString("Pointer");
    this.btRedLine.ImageIndex = this.NamedImageList.ImageIndex("imgRedLine");
    this.btRedLine.ToolTipText = LocalizationHolder.rm.GetString("Line");
    this.btRedPencil.ImageIndex = this.NamedImageList.ImageIndex("imgRedPencil");
    this.btRedPencil.ToolTipText = LocalizationHolder.rm.GetString("Pencil");
    this.btRedNote.ImageIndex = this.NamedImageList.ImageIndex("imgRedNote");
    this.btRedNote.ToolTipText = LocalizationHolder.rm.GetString("Note");
    this.btRedEllipse.ImageIndex = this.NamedImageList.ImageIndex("imgRedEllipse");
    this.btRedEllipse.ToolTipText = LocalizationHolder.rm.GetString("Ellipse");
    this.btRedEllipseFill.ImageIndex = this.NamedImageList.ImageIndex("imgRedEllipseFill");
    this.btRedEllipseFill.ToolTipText = LocalizationHolder.rm.GetString("EllipseFill");
    this.btRedCircle.ImageIndex = this.NamedImageList.ImageIndex("imgRedCircle");
    this.btRedCircle.ToolTipText = LocalizationHolder.rm.GetString("Circle");
    this.btRedCircleFill.ImageIndex = this.NamedImageList.ImageIndex("imgRedCircleFill");
    this.btRedCircleFill.ToolTipText = LocalizationHolder.rm.GetString("CircleFill");
    this.btRedRectangle.ImageIndex = this.NamedImageList.ImageIndex("imgRedRectangle");
    this.btRedRectangle.ToolTipText = LocalizationHolder.rm.GetString("Rectangle");
    this.btRedRectangleFill.ImageIndex = this.NamedImageList.ImageIndex("imgRedRectangleFill");
    this.btRedRectangleFill.ToolTipText = LocalizationHolder.rm.GetString("RectangleFill");
    this.ChangeRedPropertyButton();
  }

  private void SubscribeCbBoxRoleEvents()
  {
    this.cbBoxRole.SelectedValueChanged += new EventHandler(this.CbBoxRole_SelectedValueChanged);
    this.cbBoxRole.ComboBox.DropDown += new EventHandler(this.CbBoxRole_DropDown);
    this.cbBoxRole.ComboBox.DropDownClosed += new EventHandler(this.CbBoxRole_DropDownClosed);
  }

  private void UnsubscribeCbBoxRoleEvents()
  {
    this.cbBoxRole.SelectedValueChanged -= new EventHandler(this.CbBoxRole_SelectedValueChanged);
    this.cbBoxRole.ComboBox.DropDown -= new EventHandler(this.CbBoxRole_DropDown);
    this.cbBoxRole.ComboBox.DropDownClosed -= new EventHandler(this.CbBoxRole_DropDownClosed);
  }

  /// <summary>создать меню свойств пометок</summary>
  private void ChangeRedPropertyButton()
  {
    ButtonItem buttonItem1 = new ButtonItem();
    buttonItem1.ImageIndex = this.NamedImageList.ImageIndex("imgLinecolor");
    ButtonItem buttonItem2 = buttonItem1;
    buttonItem2.Image = this.NamedImageList.ImageList.Images[buttonItem2.ImageIndex];
    buttonItem2.Text = string.Empty;
    buttonItem2.ToolTipText = LocalizationHolder.rm.GetString("Client.Core_334");
    buttonItem2.CommandName = "RedLineColor";
    buttonItem2.Click += new EventHandler(this.BtRedLineColor_Click);
    buttonItem2.Importance = ToolBarItemImportance.High;
    this.toolBarRed.Items.Insert(this.toolBarRed.Items.IndexOf((ToolbarItemBase) this.btPointer), (ToolbarItemBase) buttonItem2);
  }

  private void cbBoxRole_MinimumControlWidth(string sRole)
  {
    if (string.IsNullOrEmpty(sRole))
      return;
    this.cbBoxRole.MinimumControlWidth = TextRenderer.MeasureText(sRole, this.cbBoxRole.ComboBox.Font).Width + SystemInformation.VerticalScrollBarWidth;
  }

  /// <summary>слой, соотв. текущему замечанию</summary>
  /// <param name="checkEditable"></param>
  /// <returns></returns>
  private RedlineLayer GetCurrentRedLayer(bool checkEditable = false)
  {
    return this.GetCurrentRedLayer(this.treeView.SelectedNode, checkEditable);
  }

  private void RedLiner_Click(object sender, EventArgs e)
  {
    if (!this.ViewEnabled || !this.IsRedEnabled || !(sender is ButtonItem buttonItem) || !buttonItem.Enabled)
      return;
    IMapTool tool = this._redliner.View.Tool;
    string commandName = buttonItem.CommandName;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
    {
      case 50440834:
        if (!(commandName == "RedEllipse"))
          break;
        buttonItem.Checked = !(tool is RedLineEllipseTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawEllipse();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 408404360:
        if (!(commandName == "RedNote"))
          break;
        buttonItem.Checked = !(tool is RedLineNoteTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawNote();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 1129569617:
        if (!(commandName == "RedCircleFill"))
          break;
        buttonItem.Checked = !(tool is RedLineCircleFillTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawCircleFill();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 1296547422:
        if (!(commandName == "RedLine"))
          break;
        buttonItem.Checked = !(tool is RedLineStrokeTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawLine();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 1355754345:
        if (!(commandName == "RedPencil"))
          break;
        buttonItem.Checked = !(tool is RedLinePencilTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawPencil();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 1815186992:
        if (!(commandName == "RedCircle"))
          break;
        buttonItem.Checked = !(tool is RedLineCircleTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawCircle();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 2440558068:
        if (!(commandName == "RedRectangleFill"))
          break;
        buttonItem.Checked = !(tool is RedLineRectangleFillTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawRectangleFill();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 2610012873:
        if (!(commandName == "RedRectangle"))
          break;
        buttonItem.Checked = !(tool is RedLineRectangleTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawRectangle();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 2801444447:
        if (!(commandName == "RedEllipseFill"))
          break;
        buttonItem.Checked = !(tool is RedLineEllipseFillTool);
        if (buttonItem.Checked)
        {
          this._redliner.DrawEllipseFill();
          break;
        }
        this._redliner.CancelDraw();
        break;
      case 4109056613:
        if (!(commandName == "RedPointer"))
          break;
        this._redliner.CancelDraw();
        buttonItem.Checked = !buttonItem.Checked;
        break;
    }
  }

  private void UpdateButtons_RedLiner()
  {
    bool flag1 = this.ViewEnabled && this.IsRedEnabled;
    bool flag2 = this.IsRedEnabled && this.IsRedlineEdit && this._redliner.CurrentRedLayer != null && this._redliner.CurrentRedLayer.AllowEdit;
    if (this._redliner?.Relative is IPager relative)
    {
      object redPage = this._redliner.GetRedPage(this._redliner.CurrentRedLayer);
      int num = redPage == null ? 1 : (relative.Current == redPage ? 1 : 0);
    }
    bool flag3 = flag1 & flag2;
    this.btnNew.Enabled = flag1 && this.IsRedlineEdit;
    this.btnRed.Visible = this.btnRed.Enabled = false;
    this.btnRed.Checked = this.IsRedlineEdit;
    if (this.IsRedlineEdit)
    {
      this.btnRed.ImageIndex = this.NamedImageList.ImageIndex("imgRedEdit");
      this.btnRed.ToolTipText = LocalizationHolder.rm.GetString("EditNotes");
    }
    else
    {
      this.btnRed.ImageIndex = this.NamedImageList.ImageIndex("imgRedViewOnly");
      this.btnRed.ToolTipText = LocalizationHolder.rm.GetString("ViewNotes");
    }
    IMapTool tool = this._redliner?.View.Tool;
    foreach (ToolbarItemBase toolbarItemBase in (CollectionBase) this.toolBarRed.Items)
    {
      if (toolbarItemBase is ButtonItem buttonItem)
      {
        switch (buttonItem.CommandName)
        {
          case "RedCircle":
            buttonItem.Checked = tool is RedLineCircleTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedCircleFill":
            buttonItem.Checked = tool is RedLineCircleFillTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedDistance":
            buttonItem.Checked = tool is DistanceTool & flag3;
            buttonItem.Enabled = flag1;
            buttonItem.Visible = flag1;
            continue;
          case "RedEllipse":
            buttonItem.Checked = tool is RedLineEllipseTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedEllipseFill":
            buttonItem.Checked = tool is RedLineEllipseFillTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedLine":
            buttonItem.Checked = tool is RedLineStrokeTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedLineColor":
            buttonItem.Enabled = flag1;
            if (this._redMapProperty == null)
              buttonItem.Enabled = false;
            buttonItem.Visible = flag1;
            continue;
          case "RedNote":
            buttonItem.Checked = tool is RedLineNoteTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedPencil":
            buttonItem.Checked = tool is RedLinePencilTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedPointer":
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedRectangle":
            buttonItem.Checked = tool is RedLineRectangleTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "RedRectangleFill":
            buttonItem.Checked = tool is RedLineRectangleFillTool & flag3;
            buttonItem.Enabled = flag3;
            buttonItem.Visible = flag1;
            continue;
          case "Redo":
            buttonItem.Enabled = flag1 && this._redliner.CanRedo;
            continue;
          case "Save":
            buttonItem.Enabled = flag1 && this._redliner.Dirty;
            continue;
          case "Undo":
            buttonItem.Enabled = flag1 && this._redliner.CanUndo;
            continue;
          default:
            continue;
        }
      }
    }
  }

  private void Update_toolBarTreeView() => this.btnComments.Visible = this.ddCheckShowAll.Checked;

  private void Initialize_View()
  {
    using (Graphics graphics = this.splitContainer.CreateGraphics())
      this.splitContainer.Panel1MinSize = Math.Max((int) graphics.MeasureString(LocalizationHolder.rm.GetString("ActionNote"), this.Font).Width, this.splitContainer.Panel1MinSize);
  }

  private void InitializeToolbarTreeView()
  {
    this.toolBarTreeView.ImageList = this.NamedImageList.ImageList;
    this.btnNew.ImageIndex = this.NamedImageList.ImageIndex("imgNewRedDoc");
    this.btnNew.ToolTipText = LocalizationHolder.rm.GetString("NewNote");
    this.ddCheckShowAll.ImageIndex = this.NamedImageList.ImageIndex("imgListView");
    this.ddCheckShowAll.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllRemarks");
    this.ddCheckShowAll.Tag = (object) ShowDocsMode.All;
    this.mbtnShowAll.ImageIndex = this.NamedImageList.ImageIndex("imgListView");
    this.mbtnShowAll.Text = this.mbtnShowAll.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllDoc");
    this.mbtnShowAll.Tag = (object) ShowDocsMode.All;
    this.mbtnShowWithRemarkOnly.ImageIndex = this.NamedImageList.ImageIndex("imgCopyListFromDoc");
    this.mbtnShowWithRemarkOnly.Text = this.mbtnShowWithRemarkOnly.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowDocWithRemarkOnly");
    this.mbtnShowWithRemarkOnly.Tag = (object) ShowDocsMode.WithRemarksOnly;
    this.btnComments.ImageIndex = this.NamedImageList.ImageIndex("imgOutput");
    this.btnComments.Text = string.Empty;
    this.btnComments.ToolTipText = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.ShowAllComments");
    ReportAttribute attribute1 = EStatusRemark.eAgreed.GetAttribute<ReportAttribute>();
    this.btnCheckFilter1.Tag = (object) EStatusRemark.eAgreed.GetName<EStatusRemark>();
    this.btnCheckFilter1.ImageIndex = this.NamedImageList.ImageIndex(attribute1.ImgName);
    this.btnCheckFilter1.Text = string.Empty;
    this.btnCheckFilter1.ToolTipText = attribute1.TipText;
    this.btnCheckFilter1.Click += new EventHandler(this.btnCheckFilter_Click);
    ReportAttribute attribute2 = EStatusRemark.eCorrected.GetAttribute<ReportAttribute>();
    this.btnCheckFilter2.Tag = (object) EStatusRemark.eCorrected.GetName<EStatusRemark>();
    this.btnCheckFilter2.ImageIndex = this.NamedImageList.ImageIndex(attribute2.ImgName);
    this.btnCheckFilter2.Text = string.Empty;
    this.btnCheckFilter2.ToolTipText = attribute2.TipText;
    this.btnCheckFilter2.Click += new EventHandler(this.btnCheckFilter_Click);
    ReportAttribute attribute3 = EStatusRemark.eInconsistent.GetAttribute<ReportAttribute>();
    this.btnCheckFilter3.Tag = (object) EStatusRemark.eInconsistent.GetName<EStatusRemark>();
    this.btnCheckFilter3.ImageIndex = this.NamedImageList.ImageIndex(attribute3.ImgName);
    this.btnCheckFilter3.Text = string.Empty;
    this.btnCheckFilter3.ToolTipText = attribute3.TipText;
    this.btnCheckFilter3.Click += new EventHandler(this.btnCheckFilter_Click);
    ReportAttribute attribute4 = EStatusRemark.eRejected.GetAttribute<ReportAttribute>();
    this.btnCheckFilter4.Tag = (object) EStatusRemark.eRejected.GetName<EStatusRemark>();
    this.btnCheckFilter4.ImageIndex = this.NamedImageList.ImageIndex(attribute4.ImgName);
    this.btnCheckFilter4.Text = string.Empty;
    this.btnCheckFilter4.ToolTipText = attribute4.TipText;
    this.btnCheckFilter4.Click += new EventHandler(this.btnCheckFilter_Click);
  }

  public void PageChanged_(object sender, EventArgs e)
  {
    this.TrySaveRedline();
    if (this._redliner == null || !this._isRedFormExpand)
      return;
    List<RedlineLayer> list = this.treeView.Nodes.Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).OfType<RedlineLayer>().ToList<RedlineLayer>().Where<RedlineLayer>(new Func<RedlineLayer, bool>(this.IsPageRemark)).ToList<RedlineLayer>();
    object currentPage = ((IPager) this._redliner.Relative).Current;
    Func<RedlineLayer, bool> predicate = (Func<RedlineLayer, bool>) (x =>
    {
      List<object> remarkPages = this.GetRemarkPages(this._redliner, x);
      // ISSUE: explicit non-virtual call
      return remarkPages != null && __nonvirtual (remarkPages.Contains(currentPage));
    });
    IEnumerable<RedlineLayer> source = list.Where<RedlineLayer>(predicate);
    if (this.treeView.SelectedNode?.Tag is RedlineLayer tag)
    {
      source.Contains<RedlineLayer>(tag);
      this.SetNodeRemarkForeColor(this.treeView.SelectedNode);
      this._redliner.ChangeVisibleLayers(new List<object>()
      {
        (object) tag
      });
      this._redliner.ChangeVisibleLayer(tag, this.IsRedlineEdit);
    }
    else
      this._redliner.ChangeVisibleLayers(source.Cast<object>().ToList<object>());
  }

  private void InitializeMenuBarTreeView()
  {
    this.menuBarTreeView.ImageList = this.NamedImageList.ImageList;
    string str = string.Format(LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.Item_CorrectedOrRejected") ?? string.Empty, (object) Redliner.Developed, (object) Redliner.Made);
    this.mBtItem_Corrected.ImageIndex = this.NamedImageList.ImageIndex(EStatusRemark.eCorrected.GetAttribute<ReportAttribute>().ImgName);
    this.mBtItem_Corrected.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Corrected");
    this.mBtItem_Corrected.ToolTipText = str;
    this.mBtItem_Agreed.ImageIndex = this.NamedImageList.ImageIndex(EStatusRemark.eAgreed.GetAttribute<ReportAttribute>().ImgName);
    this.mBtItem_Agreed.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Agreed");
    this.mBtItem_Inconsistent.ImageIndex = this.NamedImageList.ImageIndex(EStatusRemark.eInconsistent.GetAttribute<ReportAttribute>().ImgName);
    this.mBtItem_Inconsistent.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Inconsistent");
    this.mBtItem_Rejected.ImageIndex = this.NamedImageList.ImageIndex(EStatusRemark.eRejected.GetAttribute<ReportAttribute>().ImgName);
    this.mBtItem_Rejected.ToolTipText = str;
    this.mBtItem_Rejected.Text = LocalizationHolder.rm.GetString("Client.Core.EStatusRemark.Rejected");
    this.mBtItem_Remove.ImageIndex = this.NamedImageList.ImageIndex("imgDelete");
    this.mBtItem_Remove.Text = LocalizationHolder.rm.GetString("Client.Core_1127");
    this.mBtItem_Rename.Text = LocalizationHolder.rm.GetString("Client.Core_1625");
  }

  private void _Resize(object sender)
  {
    if (!(sender is Control control))
      return;
    Intermech.Bars.ToolBar toolBar = control.Controls.OfType<Intermech.Bars.ToolBar>().FirstOrDefault<Intermech.Bars.ToolBar>();
    SplitContainer splitContainer = toolBar != null ? toolBar.Parent.Controls.OfType<SplitContainer>().FirstOrDefault<SplitContainer>() : (SplitContainer) null;
    if (splitContainer != null)
    {
      Rectangle bounds = toolBar.Parent.Bounds;
      int y = !toolBar.Visible || !toolBar.Parent.Visible ? 0 : toolBar.Height + 1;
      splitContainer.SetBounds(0, y, bounds.Width, bounds.Height - y);
    }
    control.Invalidate();
  }

  /// <summary>очистить поля</summary>
  private void ClearBoxView()
  {
    this.tBoxUser.Text = "";
    this.tBoxTime.Text = "";
    this.tBoxBusiness_process.Text = "";
    this.tBoxStep.Text = "";
    this.tBoxComment.Text = "";
    this.tBoxComment.ReadOnly = true;
    this.ClearRoleText();
  }

  private void UpdateInfoText(RedlineLayer redLayer)
  {
    if (redLayer == null)
    {
      this.ClearBoxView();
    }
    else
    {
      this.tBoxUser.Text = redLayer.UserID.Split('|')[0];
      this.tBoxTime.Text = redLayer.Time.ToString("dd.M.yyyy H.mm");
      this.tBoxBusiness_process.Text = redLayer.NameBusiness;
      this.tBoxStep.Text = redLayer.StepBusiness;
      bool flag = redLayer.LockRemark || redLayer.UserID != Redliner.UserNameID || !this.IsRedlineEdit;
      this.tBoxComment.Text = redLayer.Comment;
      this.tBoxComment.ReadOnly = flag;
      string signature = redLayer.Signature;
      List<string> signatures = this._redliner.GenerateSignatures(redLayer.StatusRemark);
      if (flag)
        signatures.Clear();
      if (!signatures.Contains(signature))
        signatures.Insert(0, signature);
      this.cbBoxRole.ComboBox.BeginUpdate();
      this.cbBoxRole.Items.Clear();
      this.cbBoxRole.Items.AddRange(signatures.Cast<object>().ToArray<object>());
      this.cbBoxRole.ComboBox.EndUpdate();
      this.cbBoxRole.ComboBox.SelectedItem = (object) signature;
      this.cbBoxRole_MinimumControlWidth(signature);
    }
  }

  private void ClearRoleText()
  {
    this.cbBoxRole.ComboBox.DropDownHeight = 1;
    this.cbBoxRole.ComboBox.BeginUpdate();
    this.cbBoxRole.ComboBox.Items.Clear();
    this.cbBoxRole.ComboBox.SelectedIndex = -1;
    this.cbBoxRole.ComboBox.EndUpdate();
  }

  private TreeNode SearchTree(TreeNodeCollection nodes, RedlineLayer tag)
  {
    return nodes.SearchTree((object) tag, (Func<object, object, bool>) ((o, o1) => o is RedlineLayer redlineLayer1 && o1 is RedlineLayer redlineLayer2 && (long) redlineLayer2.RedObjectID == (long) redlineLayer1.RedObjectID));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="checkEditable"></param>
  /// <returns></returns>
  private RedlineLayer GetCurrentRedLayer(TreeNode node, bool checkEditable = false)
  {
    if (!(node?.Tag is RedlineLayer tag) || !checkEditable)
      return tag;
    if (!this.ViewEnabled || !this.IsRedEnabled)
      return (RedlineLayer) null;
    if (this._showRedLine4AllDocs != ShowDocsMode.Single)
    {
      FileItem fileItemByNode = this.GetFileItemByNode(this.treeView.SelectedNode);
      if (fileItemByNode == null || fileItemByNode.BlobID != this._selectedFileItem.BlobID || fileItemByNode.ObjectId != this._selectedFileItem.ObjectId)
        return (RedlineLayer) null;
    }
    return tag;
  }

  /// <summary>проверка видимости команды 'Исправлено' и 'Отклонено'</summary>
  private void CheckVisible_CorrectedOrRejected(MenuButtonItem mBtItem)
  {
    mBtItem.Visible = false;
    mBtItem.Enabled = false;
    if (!this.IsRedlineEdit || !this.IsRedEnabled || !this._redliner.isEditRedRole)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer(true);
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.StatusRemark == EStatusRemark.eCorrected || currentRedLayer.StatusRemark == EStatusRemark.eRejected || this._redliner.ListChainRedlineLayer(currentRedLayer.RedObjectID).Any<RedlineLayer>((Func<RedlineLayer, bool>) (u => u.StatusRemark == EStatusRemark.eAgreed)))
      return;
    mBtItem.Visible = true;
    if (currentRedLayer.UserID == Redliner.UserNameID || this._redliner.GenerateSignatures(mBtItem.CommandName.ToEnum<EStatusRemark>()).Count == 0)
      return;
    mBtItem.Enabled = true;
  }

  /// <summary>проверка видимости команды 'Не исправлено' и 'Согласовано' </summary>
  private void CheckVisible_InconsistentOrAgreed(MenuButtonItem mBtItem)
  {
    mBtItem.Visible = false;
    mBtItem.Enabled = false;
    if (!this.IsRedlineEdit || !this.IsRedEnabled || !this._redliner.isEditRedRole)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer(true);
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.StatusRemark == EStatusRemark.eInconsistent || this._redliner.ListChainRedlineLayer(currentRedLayer.RedObjectID).Any<RedlineLayer>((Func<RedlineLayer, bool>) (u => u.StatusRemark == EStatusRemark.eAgreed)))
      return;
    mBtItem.Visible = true;
    if (currentRedLayer.UserID == Redliner.UserNameID || this._redliner.GenerateSignatures(mBtItem.CommandName.ToEnum<EStatusRemark>()).Count == 0)
      return;
    mBtItem.Enabled = true;
  }

  /// <summary>проверка видимости команды переименование</summary>
  private void CheckVisible_Rename(MenuButtonItem mBtItem)
  {
    mBtItem.Visible = false;
    mBtItem.Enabled = false;
    mBtItem.BeginGroup = false;
    if (!this.IsRedlineEdit || !this.IsRedEnabled)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer(true);
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.ParentID != 0UL && !this._redliner.isEditRedRole)
      return;
    mBtItem.BeginGroup = true;
    mBtItem.Visible = true;
    if (currentRedLayer.UserID != Redliner.UserNameID)
      return;
    mBtItem.Enabled = true;
  }

  /// <summary>проверка видимости команды удалить</summary>
  private void CheckVisible_Remove()
  {
    this.mBtItem_Remove.Visible = false;
    if (!this.IsRedlineEdit || !this.IsRedEnabled)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer(true);
    if (currentRedLayer == null || currentRedLayer.LockRemark || currentRedLayer.ParentID != 0UL && !this._redliner.isEditRedRole || currentRedLayer.UserID != Redliner.UserNameID)
      return;
    this.mBtItem_Remove.Visible = true;
    this.mBtItem_Remove.BeginGroup = true;
  }

  /// <summary>команда удаление элемента дерева</summary>
  private void Command_Remove()
  {
    if (!this.IsRedEnabled)
      return;
    RedlineLayer redLayer = this.GetCurrentRedLayer(true);
    if (redLayer == null)
      return;
    this._redliner.CurrentRedLayer = (MapLayer) null;
    List<RedlineLayer> source = this._redliner.ListRedlineLayer();
    RedlineLayer node = source.SingleOrDefault<RedlineLayer>((Func<RedlineLayer, bool>) (x => (long) x.RedObjectID == (long) redLayer.ParentID)) ?? redLayer;
    RedlineLayer redlineLayer = source.SingleOrDefault<RedlineLayer>((Func<RedlineLayer, bool>) (x => (long) x.RedObjectID == (long) node.ParentID)) ?? node;
    List<RedlineLayer> list = source.Where<RedlineLayer>((Func<RedlineLayer, bool>) (x => (long) x.ParentID == (long) node.RedObjectID)).ToList<RedlineLayer>();
    list.Sort((Comparison<RedlineLayer>) ((x, y) => DateTime.Compare(x.Time, y.Time)));
    source.Clear();
    if (list.Count == 1)
    {
      node.LockRemark = false;
      redlineLayer.StatusRemark = node != redlineLayer ? node.StatusRemark : EStatusRemark.eInconsistent;
    }
    if (list.Count == 2)
    {
      if (node == redlineLayer)
      {
        redlineLayer.StatusRemark = (list[0] != redLayer ? list[0] : list[1]).StatusRemark;
      }
      else
      {
        node.LockRemark = false;
        redlineLayer.StatusRemark = node != redlineLayer ? node.StatusRemark : EStatusRemark.eInconsistent;
      }
    }
    this.treeView.SelectedNode.Tag = (object) null;
    this._redliner.DeleteRedLayer(redLayer);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="redlinerNodes"></param>
  /// <param name="redliner"></param>
  /// <param name="objectId"></param>
  /// <param name="blobId"></param>
  /// <param name="redlinerNode"></param>
  private void CreateRedLinerTree(TreeNodeCollection redlinerNodes, Redliner redliner)
  {
    if (redlinerNodes == null || redliner == null)
      return;
    this.UpdateFilter();
    this.treeView.DrawMode = TreeViewDrawMode.Normal;
    this.treeView.DrawMode = TreeViewDrawMode.OwnerDrawAll;
    this.treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
    List<RedlineLayer> redlineLayerList = redliner.ListRedlineLayer();
    List<RedlineLayer> list = redlineLayerList.Where<RedlineLayer>((Func<RedlineLayer, bool>) (e => e.ParentID > 0UL)).ToList<RedlineLayer>();
    Dictionary<string, List<RedlineLayer>> dictionary1 = redlineLayerList.Except<RedlineLayer>((IEnumerable<RedlineLayer>) list).GroupBy<RedlineLayer, string>((Func<RedlineLayer, string>) (e => e.Signature)).ToDictionary<IGrouping<string, RedlineLayer>, string, List<RedlineLayer>>((Func<IGrouping<string, RedlineLayer>, string>) (gr => gr.Key), (Func<IGrouping<string, RedlineLayer>, List<RedlineLayer>>) (gr => gr.ToList<RedlineLayer>()));
    redlineLayerList.Clear();
    Dictionary<string, List<RedlineLayer>> dictionary2 = dictionary1.OrderBy<KeyValuePair<string, List<RedlineLayer>>, string>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, List<RedlineLayer>>, string, List<RedlineLayer>>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key), (Func<KeyValuePair<string, List<RedlineLayer>>, List<RedlineLayer>>) (x => x.Value));
    for (int index1 = 0; index1 < dictionary2.Count; ++index1)
    {
      KeyValuePair<string, List<RedlineLayer>> keyValuePair1 = dictionary2.ElementAt<KeyValuePair<string, List<RedlineLayer>>>(index1);
      TreeNode node1 = new TreeNode(keyValuePair1.Key.Split('|')[0]);
      redlinerNodes.Add(node1);
      node1.ImageIndex = node1.SelectedImageIndex = this.NamedImageList.ImageIndex("imgUserRoles");
      Dictionary<string, List<RedlineLayer>> dictionary3 = keyValuePair1.Value.GroupBy<RedlineLayer, string>((Func<RedlineLayer, string>) (e => e.UserID)).ToDictionary<IGrouping<string, RedlineLayer>, string, List<RedlineLayer>>((Func<IGrouping<string, RedlineLayer>, string>) (gr => gr.Key), (Func<IGrouping<string, RedlineLayer>, List<RedlineLayer>>) (gr => gr.ToList<RedlineLayer>())).OrderBy<KeyValuePair<string, List<RedlineLayer>>, string>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, List<RedlineLayer>>, string, List<RedlineLayer>>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key), (Func<KeyValuePair<string, List<RedlineLayer>>, List<RedlineLayer>>) (x => x.Value));
      for (int index2 = 0; index2 < dictionary3.Count; ++index2)
      {
        KeyValuePair<string, List<RedlineLayer>> keyValuePair2 = dictionary3.ElementAt<KeyValuePair<string, List<RedlineLayer>>>(index2);
        TreeNode node2 = new TreeNode(keyValuePair2.Key.Split('|')[0]);
        node2.ImageIndex = node2.SelectedImageIndex = this.NamedImageList.ImageIndex("imgUser");
        node1.Nodes.Add(node2);
        Dictionary<string, List<RedlineLayer>> dictionary4 = this.ChangeFilter(keyValuePair2.Value).GroupBy<RedlineLayer, string>((Func<RedlineLayer, string>) (e => e.NameRemark)).ToDictionary<IGrouping<string, RedlineLayer>, string, List<RedlineLayer>>((Func<IGrouping<string, RedlineLayer>, string>) (gr => gr.Key), (Func<IGrouping<string, RedlineLayer>, List<RedlineLayer>>) (gr => gr.ToList<RedlineLayer>())).OrderBy<KeyValuePair<string, List<RedlineLayer>>, string>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key)).ToDictionary<KeyValuePair<string, List<RedlineLayer>>, string, List<RedlineLayer>>((Func<KeyValuePair<string, List<RedlineLayer>>, string>) (x => x.Key), (Func<KeyValuePair<string, List<RedlineLayer>>, List<RedlineLayer>>) (x => x.Value));
        for (int index3 = 0; index3 < dictionary4.Count; ++index3)
        {
          KeyValuePair<string, List<RedlineLayer>> keyValuePair3 = dictionary4.ElementAt<KeyValuePair<string, List<RedlineLayer>>>(index3);
          for (int index4 = 0; index4 < keyValuePair3.Value.Count; ++index4)
          {
            RedlineLayer redlineLayer = keyValuePair3.Value[index4];
            ReportAttribute attribute = redlineLayer.StatusRemark.GetAttribute<ReportAttribute>();
            TreeNode treeNode = new TreeNode(keyValuePair3.Key);
            node2.Nodes.Add(treeNode);
            treeNode.ImageIndex = treeNode.SelectedImageIndex = this.NamedImageList.ImageIndex(attribute.ImgName);
            treeNode.Tag = (object) redlineLayer;
            this.SetNodeRemarkForeColor(treeNode);
            this.CreateTree(treeNode, redlineLayer.RedObjectID, list);
          }
        }
        node2.Expand();
      }
      node1.Expand();
    }
  }

  /// <summary>
  /// Замечение относится к странице или целиком к документу
  /// </summary>
  /// <param name="itemRemark"></param>
  /// <returns></returns>
  private bool IsPageRemark(RedlineLayer itemRemark)
  {
    MapLayer source = this.view.Document.Layers.Find((object) itemRemark);
    return source != null && source.OfType<IMapRelativePosition>().Any<IMapRelativePosition>();
  }

  /// <summary>Найти страницу соответствующую замечанию</summary>
  /// <param name="redliner"></param>
  /// <param name="itemRemark"></param>
  /// <returns></returns>
  private object GetRemarkPage(Redliner redliner, RedlineLayer itemRemark)
  {
    MapLayer mapLayer = this.view.Document.Layers.Find((object) itemRemark);
    return redliner?.GetRedPage(mapLayer);
  }

  /// <summary>Получить список страницу, соответствующих замечанию</summary>
  /// <returns>Список объектов страниц или null</returns>
  private List<object> GetRemarkPages(Redliner redliner, RedlineLayer itemRemark)
  {
    MapLayer mapLayer = this.view.Document.Layers.Find((object) itemRemark);
    if (redliner == null)
      return (List<object>) null;
    List<object> redPagesForLayer = redliner.GetRedPagesForLayer(mapLayer);
    return redPagesForLayer == null ? (List<object>) null : redPagesForLayer.ToList<object>();
  }

  private void CreateTree(TreeNode root, ulong RedObjectID, List<RedlineLayer> listAllParents)
  {
    List<RedlineLayer> list = listAllParents.Where<RedlineLayer>((Func<RedlineLayer, bool>) (e => (long) e.ParentID == (long) RedObjectID)).ToList<RedlineLayer>();
    list.Sort((Comparison<RedlineLayer>) ((x, y) => DateTime.Compare(x.Time, y.Time)));
    for (int index = 0; index < list.Count; ++index)
    {
      RedlineLayer redlineLayer = list[index];
      TreeNode treeNode = new TreeNode(redlineLayer.NameRemark)
      {
        Tag = (object) redlineLayer
      };
      treeNode.ImageIndex = treeNode.SelectedImageIndex = this.treeView.ImageList.Images.Count + 1;
      root.Nodes.Add(treeNode);
      this.SetNodeRemarkForeColor(treeNode);
      this.CreateTree(treeNode, redlineLayer.RedObjectID, listAllParents);
    }
  }

  private void treeView_MouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right)
      return;
    this.treeView.SelectedNode = this.treeView.GetNodeAt(e.X, e.Y);
    if (this.treeView.SelectedNode == null)
      return;
    this.contextMenuBarItemTree.Show((Control) this.treeView, e.Location);
  }

  private List<RedlineLayer> ChangeFilter(List<RedlineLayer> list)
  {
    List<RedlineLayer> redlineLayerList = new List<RedlineLayer>();
    list.Sort((Comparison<RedlineLayer>) ((x, y) => DateTime.Compare(x.Time, y.Time)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eAgreed))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eAgreed)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eCorrected))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eCorrected)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eInconsistent))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eInconsistent)));
    if (this.FilterFlags.HasFlag((Enum) EStatusRemark.eRejected))
      redlineLayerList.AddRange(list.Where<RedlineLayer>((Func<RedlineLayer, bool>) (m => m.StatusRemark == EStatusRemark.eRejected)));
    return redlineLayerList;
  }

  /// <summary> </summary>
  /// <param name="Checked"></param>
  /// <param name="flag"></param>
  private void SetFilterFlags(bool Checked, EStatusRemark flag)
  {
    if (Checked)
      this.FilterFlags |= flag;
    else
      this.FilterFlags &= ~flag;
  }

  private void UpdateTreeView()
  {
    if (!this.IsRedEnabled)
      return;
    RedlineLayer redLayer = (RedlineLayer) null;
    if (this._redliner.CurrentRedLayer != null)
    {
      redLayer = this._redliner.CurrentRedLayer.Identifier as RedlineLayer;
      this._redliner.CurrentRedLayer = (MapLayer) null;
    }
    this.UpdateTreeView(redLayer);
  }

  /// <summary>переход на документ для активного замечания</summary>
  /// <param name="node">активное замечание</param>
  /// <returns>был ли переход на документ</returns>
  private void GoActiveDocument(TreeNode node)
  {
    if (this._showRedLine4AllDocs == ShowDocsMode.Single)
      return;
    FileItem fileItemByNode = this.GetFileItemByNode(node);
    if (fileItemByNode == null || fileItemByNode.ObjectId == this._selectedFileItem.ObjectId && fileItemByNode.BlobID == this._selectedFileItem.BlobID)
      return;
    object tag = node.Tag;
    bool flag = tag is RedlineLayer;
    SetFileItemEventHandler fileItemCurentEvent = this.SetFileItemCurentEvent;
    if (fileItemCurentEvent != null)
      fileItemCurentEvent((object) this, new SetFileItemEventArgs(fileItemByNode));
    TreeNode treeNode = flag ? this.SearchTree(this.treeView.Nodes, tag as RedlineLayer) : this.treeView.Nodes.SearchTree(tag, (Func<object, object, bool>) ((o, o1) => o.Equals(o1)));
    if (treeNode == null)
      return;
    treeNode.ForeColor = Color.Blue;
    this.treeView.SelectedNode = treeNode;
    this.treeView.Focus();
    if (!flag || !this.IsRedEnabled)
      return;
    RedlineLayer currentRedLayer1 = this.GetCurrentRedLayer();
    if (currentRedLayer1 == null)
      return;
    MapLayer currentRedLayer2 = this._redliner.CurrentRedLayer;
    if (currentRedLayer2 == null || currentRedLayer2.Identifier != currentRedLayer1)
      return;
    object redPage = this._redliner.GetRedPage(currentRedLayer2);
    if (redPage == null)
      return;
    ((IPager) this._redliner.Relative).Current = redPage;
  }

  /// <summary>переход на страницу активного замечания</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeView_DoubleClick(object sender, EventArgs e)
  {
    this.GoActiveDocument(this.treeView.SelectedNode);
    if (!this.IsRedEnabled)
      return;
    RedlineLayer currentRedLayer1 = this.GetCurrentRedLayer();
    if (currentRedLayer1 == null)
      return;
    MapLayer currentRedLayer2 = this._redliner.CurrentRedLayer;
    if (currentRedLayer2 == null || currentRedLayer2.Identifier != currentRedLayer1)
      return;
    object redPage = this._redliner.GetRedPage(currentRedLayer2);
    if (redPage == null)
      return;
    ((IPager) this._redliner.Relative).Current = redPage;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="node"></param>
  /// <returns></returns>
  private FileItem GetFileItemByNode(TreeNode node)
  {
    if (this._showRedLine4AllDocs == ShowDocsMode.Single || node == null)
      return (FileItem) null;
    for (node = node.Nodes.Count != 0 ? node.Nodes[0] : node; node != null; node = node.Parent)
    {
      if (node.Tag is FileItem tag && tag.IsFile)
        return tag;
    }
    return (FileItem) null;
  }

  private void SubscribeRenderChanged()
  {
    this.BarManager.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
    this.ToolbarRendererChanged((object) this.BarManager, EventArgs.Empty);
  }

  private void UnsubscribeRenderChanged()
  {
    this.menuBarTreeView.Renderer = this.toolBarRed.Renderer = this.toolBarTreeView.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
    this.BarManager.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
  }

  private void LoadRedlineData()
  {
    if (this._redliner == null)
      return;
    this._redliner.LoadData(this._selectedFileItem.ObjectId, this._selectedFileItem.BlobID, this._selectedFileItem.FileName);
    this._redliner.SetDirty(false);
  }

  /// <summary>
  /// 
  /// </summary>
  private void CheckAllRedView()
  {
    if (!this.IsRedEnabled)
      return;
    this.ClearBoxView();
    List<object> redLayers = (List<object>) null;
    if (this._isRedFormExpand)
    {
      List<RedlineLayer> list = this.treeView.Nodes.Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).OfType<RedlineLayer>().ToList<RedlineLayer>().Where<RedlineLayer>(new Func<RedlineLayer, bool>(this.IsPageRemark)).ToList<RedlineLayer>();
      object currentPage = ((IPager) this._redliner.Relative).Current;
      Func<RedlineLayer, bool> predicate = (Func<RedlineLayer, bool>) (x =>
      {
        List<object> remarkPages = this.GetRemarkPages(this._redliner, x);
        // ISSUE: explicit non-virtual call
        return remarkPages != null && __nonvirtual (remarkPages.Contains(currentPage));
      });
      redLayers = list.Where<RedlineLayer>(predicate).Cast<object>().ToList<object>();
    }
    this._redliner.ChangeVisibleLayers(redLayers);
    this._redliner.OnChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  private void UpdateFilter()
  {
    ButtonItem[] buttonItemArray = new ButtonItem[4]
    {
      this.btnCheckFilter1,
      this.btnCheckFilter2,
      this.btnCheckFilter3,
      this.btnCheckFilter4
    };
    foreach (ButtonItem buttonItem in buttonItemArray)
    {
      buttonItem.Checked = this.FilterFlags.HasFlag((Enum) (buttonItem.Tag as string).ToEnum<EStatusRemark>());
      buttonItem.Invalidate();
    }
  }

  private void SetProgressBarVisible(bool value)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) (() => this.SetProgressBarVisible(value)));
    }
    else
    {
      if (value)
      {
        this.pageLoadProgressBar.Style = ProgressBarStyle.Marquee;
        this.pageLoadProgressBar.MarqueeAnimationSpeed = 10;
      }
      else
      {
        this.pageLoadProgressBar.Style = ProgressBarStyle.Continuous;
        this.pageLoadProgressBar.MarqueeAnimationSpeed = 0;
      }
      this.pageLoadProgressBar.Visible = value;
    }
  }

  /// <summary>
  /// Обработчик события "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = sender is BarManager barManager ? barManager.Renderer : (IToolBarRenderer) null;
    if (renderer == null)
      return;
    this.menuBarTreeView.Renderer = this.toolBarRed.Renderer = this.toolBarTreeView.Renderer = renderer;
  }

  /// <summary>вызвать форму для изменения свойств пометок</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BtRedLineColor_Click(object sender, EventArgs e)
  {
    if (this._redMapProperty == null)
      return;
    using (RedPropertyView redPropertyView = new RedPropertyView())
    {
      redPropertyView.LoadSettings(this._redMapProperty);
      if (redPropertyView.ShowDialog() != DialogResult.OK)
        return;
      redPropertyView.Apply();
    }
  }

  private void RedLinerSaveRedoUndo_Click(object sender, EventArgs e)
  {
    if (!this.ViewEnabled || !this.IsRedEnabled || !(sender is ButtonItem buttonItem) || !buttonItem.Enabled)
      return;
    switch (buttonItem.CommandName)
    {
      case "Save":
        this._redliner.CancelDraw();
        this._redliner.WriteData(this._selectedFileItem.ObjectId, this._selectedFileItem.BlobID, this._selectedFileItem.FileName);
        this._redliner.SetDirty(false);
        this.UpdateButtons_RedLiner();
        break;
      case "Undo":
      case "Redo":
        this._redliner.CancelDraw();
        if (buttonItem.CommandName == "Undo")
          this._redliner.Undo();
        if (buttonItem.CommandName == "Redo")
          this._redliner.Redo();
        if (this._redliner.CurrentRedLayer?.Identifier is RedlineLayer identifier)
        {
          this._redliner.ChangeVisibleLayer(identifier, this.IsRedlineEdit);
          this.tBoxComment.Text = identifier.Comment = identifier.CommentText.Text;
          this.cbBoxRole.ComboBox.SelectedItem = (object) (identifier.Signature = identifier.SignatureText.Text);
          this.cbBoxRole_MinimumControlWidth(identifier.Signature);
          this.FillTreeView();
          this.UpdateTreeView(identifier);
        }
        this.UpdateButtons_RedLiner();
        break;
    }
  }

  private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
  {
    if (this.treeView.CheckBoxes || this.treeView.ImageList == null || e.Node.ImageIndex < this.treeView.ImageList.Images.Count)
      return;
    this.treeView.Invalidate(e.Node.Bounds);
  }

  private void treeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
  {
    if (this._showRedLine4AllDocs != ShowDocsMode.Single)
    {
      FileItem fileItemByNode = this.GetFileItemByNode(e.Node);
      if (fileItemByNode == null || fileItemByNode.ObjectId != this._selectedFileItem.ObjectId || fileItemByNode.BlobID != this._selectedFileItem.BlobID && fileItemByNode.IsFile)
        e.Node.ForeColor = Color.DarkGray;
    }
    if (this.treeView.ImageList == null || e.Node.ImageIndex < this.treeView.ImageList.Images.Count)
    {
      e.DrawDefault = true;
    }
    else
    {
      e.DrawDefault = false;
      int width1 = this.treeView.ImageList.ImageSize.Width;
      if (this.treeView.ShowLines)
      {
        int num1 = e.Node.Bounds.Left - 3 - width1 / 2;
        int num2 = (e.Node.Bounds.Top + e.Node.Bounds.Bottom) / 2;
        using (Pen pen = new Pen(this.treeView.LineColor, 1f))
        {
          pen.DashStyle = DashStyle.Dot;
          e.Graphics.DrawLine(pen, num1 - width1 / 2, num2, num1 + width1 / 2, num2);
          if (!this.treeView.CheckBoxes)
          {
            if (e.Node.IsExpanded)
              e.Graphics.DrawLine(pen, num1, num2, num1, num2 + width1 / 2);
          }
        }
      }
      Rectangle bounds1 = e.Bounds;
      e.Graphics.FillRectangle(Brushes.White, bounds1);
      using (StringFormat format = new StringFormat()
      {
        Alignment = StringAlignment.Near,
        LineAlignment = StringAlignment.Center
      })
      {
        if (e.Node.IsSelected)
        {
          using (Brush brush1 = (Brush) new SolidBrush(Color.FromKnownColor(KnownColor.Highlight)))
          {
            Graphics graphics = e.Graphics;
            Brush brush2 = brush1;
            int x = e.Bounds.X;
            Rectangle bounds2 = e.Bounds;
            int y = bounds2.Y;
            bounds2 = e.Bounds;
            int width2 = bounds2.Width;
            bounds2 = e.Bounds;
            int height = bounds2.Height;
            graphics.FillRectangle(brush2, x, y, width2, height);
          }
          Graphics graphics1 = e.Graphics;
          Pen black = Pens.Black;
          int x1 = e.Bounds.X;
          Rectangle bounds3 = e.Bounds;
          int y1 = bounds3.Y;
          bounds3 = e.Bounds;
          int width3 = bounds3.Width - 1;
          int height1 = e.Bounds.Height - 1;
          graphics1.DrawRectangle(black, x1, y1, width3, height1);
          e.Graphics.DrawString(e.Node.Text, this.treeView.Font, Brushes.White, (RectangleF) bounds1, format);
        }
        else
        {
          if (bounds1.Height == 0)
            return;
          e.Graphics.DrawString(e.Node.Text, this.treeView.Font, Brushes.Black, (RectangleF) bounds1, format);
        }
      }
    }
  }

  private void ViewMapPanel_SizeChanged(object sender, EventArgs e)
  {
    this.splitContainer.Size = this.Size;
    this._Resize((object) this.splitContainer.Panel1);
    this._Resize((object) this.splitContainer.Panel2);
  }

  private void Panel1_SizeChanged(object sender, EventArgs e)
  {
    this.view.Size = this.splitContainerView.Panel1.Size;
    this.view.Invalidate();
  }

  private void btnCheckFilter_Click(object sender, EventArgs e)
  {
    if (sender is ButtonItem buttonItem)
    {
      EStatusRemark flag = (buttonItem.Tag as string).ToEnum<EStatusRemark>();
      buttonItem.Checked = !buttonItem.Checked;
      buttonItem.Invalidate();
      this.SetFilterFlags(buttonItem.Checked, flag);
    }
    this.FillTreeView();
    this.UpdateTreeView();
  }

  private void ddCheckShowAll_Click(object sender, EventArgs e)
  {
    this.ddCheckShowAll.Checked = !this.ddCheckShowAll.Checked;
    this._showRedLine4AllDocs = !this.ddCheckShowAll.Checked || this.ddCheckShowAll.Tag == null ? ShowDocsMode.Single : (ShowDocsMode) this.ddCheckShowAll.Tag;
    this.Update_toolBarTreeView();
    this.FillTreeView();
    this.UpdateTreeView();
    this.ViewMapPanel_SizeChanged((object) this, (EventArgs) null);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mbtnShowDocs_Click(object sender, EventArgs e)
  {
    if (!(sender is MenuButtonItem menuButtonItem))
      return;
    this.ddCheckShowAll.ImageIndex = menuButtonItem.ImageIndex;
    this.ddCheckShowAll.ToolTipText = menuButtonItem.ToolTipText;
    this.ddCheckShowAll.Tag = menuButtonItem.Tag;
    this.ddCheckShowAll.Checked = false;
    this.ddCheckShowAll_Click((object) this.ddCheckShowAll, (EventArgs) null);
  }

  private string GetRtfUnicodeEscapedString(string s)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (char ch in s)
    {
      if (ch == '\\' || ch == '{' || ch == '}')
        stringBuilder.Append("\\" + ch.ToString());
      else if (ch <= '\u007F')
      {
        if (ch == '\n')
          stringBuilder.Append(this._newLineTemplate);
        else
          stringBuilder.Append(ch);
      }
      else
        stringBuilder.Append($"\\u{(object) Convert.ToUInt32(ch)}?");
    }
    return stringBuilder.ToString();
  }

  private void TreeNodesAction(TreeNodeCollection nodes, List<string> commendList)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Nodes.Count != 0)
      {
        this.TreeNodesAction(node.Nodes, commendList);
      }
      else
      {
        RedlineLayer currentRedLayer = this.GetCurrentRedLayer(node);
        if (currentRedLayer != null && !string.IsNullOrEmpty(currentRedLayer.Comment))
        {
          string s = currentRedLayer.Comment.TrimEnd(' ', '\n');
          if (!string.IsNullOrEmpty(s))
          {
            commendList.AddRange((IEnumerable<string>) node.Parent.GetNodePath());
            commendList.Add(node.Text.GetRtfUnicodeEscapedString(true));
            commendList.Add(s.GetRtfUnicodeEscapedString());
            commendList.Add("");
          }
        }
      }
    }
  }

  private void btnComments_Click(object sender, EventArgs e)
  {
    List<string> stringList = new List<string>()
    {
      "{\\rtf1 "
    };
    this.TreeNodesAction(this.treeView.Nodes, stringList);
    using (RTFEditorForm rtfEditorForm = new RTFEditorForm())
    {
      rtfEditorForm.Text = LocalizationHolder.rm.GetString("Client.Core.ViewMapPanel.CommentList");
      rtfEditorForm.RTFText = string.Join(this._newLineTemplate, (IEnumerable<string>) stringList);
      int num = (int) rtfEditorForm.ShowDialog();
    }
  }

  private void BtnItemNew_Click(object sender, EventArgs e)
  {
    if (!this.IsRedEnabled)
      return;
    RedlineLayer redlineLayer = this._redliner.CreateRedlineLayer(this._selectedFileItem.ObjectId, EStatusRemark.eInconsistent, true);
    if (string.IsNullOrEmpty(this._rankSignature))
      this._rankSignature = this._redliner.NewSignature(EStatusRemark.eInconsistent) ?? "Не выбрана";
    if (string.IsNullOrEmpty(this._rankSignature))
      return;
    redlineLayer.Signature = this._rankSignature;
    MapDocument document = this.view.Document;
    MapLayer newLayerAfter = document.Layers.CreateNewLayerAfter(document.Layers.Default);
    newLayerAfter.Identifier = (object) redlineLayer;
    newLayerAfter.Add((MapObject) redlineLayer.CreateCommentText());
    newLayerAfter.Add((MapObject) redlineLayer.CreateSignatureText());
    this.FillTreeView();
    this._redliner.CurrentRedLayer = newLayerAfter;
    this.UpdateTreeView();
    redlineLayer.UndoManager.Clear();
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
  }

  public void RedlinerChanged(object sender, EventArgs e)
  {
    this.UpdateButtons_RedLiner();
    EventHandler zoomChanging = this.ZoomChanging;
    if (zoomChanging != null)
      zoomChanging((object) this, new EventArgs());
    EventHandler measureStateChanged = this.DistanceMeasureStateChanged;
    if (measureStateChanged == null)
      return;
    measureStateChanged((object) this, new EventArgs());
  }

  private void btnRed_Click(object sender, EventArgs e)
  {
    this.IsRedlineEdit = !this.IsRedlineEdit;
    this.UpdateButtons_RedLiner();
  }

  private void CbBoxRole_SelectedValueChanged(object sender, EventArgs e)
  {
    if (!(this.cbBoxRole.ComboBox.SelectedItem is string selectedItem))
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || !(currentRedLayer.Signature != selectedItem))
      return;
    currentRedLayer.UndoManager.StartTransaction();
    currentRedLayer.SignatureText.Text = currentRedLayer.Signature = selectedItem;
    currentRedLayer.UndoManager.FinishTransaction("Signature");
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
    this.cbBoxRole_MinimumControlWidth(selectedItem);
    this.FillTreeView();
    this.UpdateTreeView(currentRedLayer);
  }

  private void CbBoxRole_DropDown(object sender, EventArgs e)
  {
    ComboBox myCombo = (ComboBox) sender;
    int verticalScrollBarWidth = myCombo.Items.Count > myCombo.MaxDropDownItems ? SystemInformation.VerticalScrollBarWidth : 0;
    int num = myCombo.Items.OfType<object>().Select<object, int>((Func<object, int>) (x => TextRenderer.MeasureText(x.ToString(), myCombo.Font).Width)).DefaultIfEmpty<int>(0).Max();
    myCombo.DropDownWidth = (int) ((double) (num + verticalScrollBarWidth + 3) * (double) this._factorDpiX);
    if (myCombo.Items.Count < 2)
      myCombo.DropDownHeight = 1;
    else
      myCombo.DropDownHeight = 100;
  }

  private void CbBoxRole_DropDownClosed(object sender, EventArgs e)
  {
    if (!this.IsRedEnabled)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || currentRedLayer.ParentID != 0UL || (currentRedLayer.LockRemark || currentRedLayer.UserID != Redliner.UserNameID ? 1 : (!this.IsRedlineEdit ? 1 : 0)) != 0)
      return;
    this._rankSignature = this.cbBoxRole.ComboBox.SelectedItem as string;
  }

  private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.treeView.Refresh();
    if (!this.IsRedEnabled)
      return;
    if (this.GetCurrentRedLayer() != null)
    {
      this.GoActiveDocument(e.Node);
      RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
      if (this.IsPageRemark(currentRedLayer))
      {
        object remarkPage = this.GetRemarkPage(this._redliner, currentRedLayer);
        if (remarkPage != null)
        {
          ((IPager) this._redliner.Relative).Current = remarkPage;
          this._redliner.ChangeVisibleLayers((List<object>) null);
          e.Node.ForeColor = Color.Blue;
          this._redliner.ChangeVisibleLayer(currentRedLayer, this.IsRedlineEdit);
          this.UpdateInfoText(currentRedLayer);
        }
        else
        {
          e.Node.ForeColor = SystemColors.GrayText;
          this.ClearBoxView();
          this.UpdateInfoText((RedlineLayer) null);
          this._redliner.ChangeVisibleLayers((List<object>) null);
        }
      }
      else
      {
        e.Node.ForeColor = Color.Blue;
        this._redliner.ChangeVisibleLayer(currentRedLayer, this.IsRedlineEdit);
        this.UpdateInfoText(currentRedLayer);
      }
    }
    else
    {
      this.ClearBoxView();
      List<RedlineLayer> list1 = (this.treeView.SelectedNode != null ? this.treeView.SelectedNode.Nodes : this.treeView.Nodes).Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).OfType<RedlineLayer>().ToList<RedlineLayer>();
      List<RedlineLayer> list2 = list1.Where<RedlineLayer>(new Func<RedlineLayer, bool>(this.IsPageRemark)).ToList<RedlineLayer>();
      List<object> list3 = list1.Where<RedlineLayer>((Func<RedlineLayer, bool>) (x => !this.IsPageRemark(x))).Cast<object>().ToList<object>();
      List<object> remarkPages = list2.SelectMany<RedlineLayer, object>((Func<RedlineLayer, IEnumerable<object>>) (x => (IEnumerable<object>) this.GetRemarkPages(this._redliner, x))).Where<object>((Func<object, bool>) (x => x != null)).ToList<object>();
      if (remarkPages != null && remarkPages.Count > 0)
      {
        List<object> list4 = list2.Where<RedlineLayer>((Func<RedlineLayer, bool>) (x =>
        {
          List<object> remarkPages1 = this.GetRemarkPages(this._redliner, x);
          return remarkPages1 != null && remarkPages1.Intersect<object>((IEnumerable<object>) remarkPages).Any<object>();
        })).Cast<object>().ToList<object>();
        ((IPager) this._redliner.Relative).Current = remarkPages[0];
        this._redliner.ChangeVisibleLayers(list4);
      }
      else
        this._redliner.ChangeVisibleLayers(list3);
    }
    this._redliner.OnChanged();
  }

  private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
  {
    if (!this.IsRedEnabled)
      return;
    this.SetNodeRemarkForeColor(this.treeView.SelectedNode, false);
  }

  private void SetNodeRemarkForeColor(TreeNode node, bool SelectedNodeBlue = true)
  {
    if (node == null)
      return;
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer(node);
    if (this.IsPageRemark(currentRedLayer))
      node.ForeColor = this.GetRemarkPage(this._redliner, currentRedLayer) != null ? (node.IsSelected & SelectedNodeBlue ? Color.Blue : SystemColors.ControlText) : SystemColors.GrayText;
    else
      node.ForeColor = node.IsSelected & SelectedNodeBlue ? Color.Blue : SystemColors.ControlText;
  }

  private void tBoxComment_TextChanged(object sender, EventArgs e)
  {
    RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
    if (currentRedLayer == null || !(currentRedLayer.Comment != this.tBoxComment.Text))
      return;
    currentRedLayer.UndoManager.StartTransaction();
    currentRedLayer.CommentText.Text = currentRedLayer.Comment = this.tBoxComment.Text;
    currentRedLayer.UndoManager.FinishTransaction("Comment");
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
    this._redliner.CancelDraw();
  }

  private void contextMenuBarItemTree_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    this.CheckVisible_CorrectedOrRejected(this.mBtItem_Corrected);
    this.CheckVisible_CorrectedOrRejected(this.mBtItem_Rejected);
    this.CheckVisible_InconsistentOrAgreed(this.mBtItem_Agreed);
    this.CheckVisible_InconsistentOrAgreed(this.mBtItem_Inconsistent);
    this.CheckVisible_Rename(this.mBtItem_Rename);
    this.CheckVisible_Remove();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void mBtItem_Command_Click(object sender, EventArgs e)
  {
    if (!this.ViewEnabled || !this.IsRedEnabled || !(sender is ButtonItemBase buttonItemBase))
      return;
    switch (buttonItemBase.CommandName)
    {
      case "eAgreed":
      case "eCorrected":
      case "eRejected":
      case "eInconsistent":
        EStatusRemark status = buttonItemBase.CommandName.ToEnum<EStatusRemark>();
        RedlineLayer currentRedLayer = this.GetCurrentRedLayer();
        MapLayer layer = this.view.Document.Layers.Find((object) currentRedLayer);
        string str = this._redliner.NewSignature(status);
        if (string.IsNullOrEmpty(str))
          break;
        RedlineLayer redlineLayer = this._redliner.CreateRedlineLayer(this._selectedFileItem.ObjectId, status);
        redlineLayer.Signature = str;
        redlineLayer.ParentID = currentRedLayer.RedObjectID;
        this._redliner.ListChainRedlineLayer(currentRedLayer.RedObjectID).Last<RedlineLayer>().StatusRemark = status;
        MapDocument document = this.view.Document;
        MapLayer newLayerAfter = document.Layers.CreateNewLayerAfter(document.Layers.Default);
        newLayerAfter.Identifier = (object) redlineLayer;
        newLayerAfter.Add((MapObject) redlineLayer.CreateCommentText());
        newLayerAfter.Add((MapObject) redlineLayer.CreateSignatureText());
        this._redliner.CopyLayerDark(layer, newLayerAfter);
        currentRedLayer.LockRemark = true;
        redlineLayer.UndoManager.Clear();
        this.FillTreeView();
        this._redliner.CurrentRedLayer = newLayerAfter;
        this.UpdateTreeView();
        this._redliner.SetDirty(true);
        this._redliner.OnChanged();
        this._redliner.CancelDraw();
        break;
      case "eRename":
        if (this.treeView.SelectedNode == null)
          break;
        this.treeView.LabelEdit = true;
        if (this.treeView.SelectedNode.IsEditing)
          break;
        this.treeView.SelectedNode.BeginEdit();
        break;
      case "eRemove":
        this.Command_Remove();
        this.FillTreeView();
        this._redliner.SetDirty(true);
        this._redliner.OnChanged();
        this._redliner.CancelDraw();
        break;
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
  {
    this.treeView.LabelEdit = false;
    if (string.IsNullOrEmpty(e.Label))
      return;
    string text = e.Node.Text;
    string label = e.Label;
    string str = label;
    if (text == str)
      return;
    e.CancelEdit = true;
    e.Node.Text = label;
    this.GetCurrentRedLayer().NameRemark = label;
    this._redliner.SetDirty(true);
    this._redliner.OnChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void splitContainerRedObject_Paint(object sender, PaintEventArgs e)
  {
    if (!(sender is SplitContainer splitContainer))
      return;
    if (splitContainer.SplitterWidth != this.Font.Height)
      splitContainer.SplitterWidth = this.Font.Height;
    using (StringFormat format = new StringFormat())
    {
      Rectangle splitterRectangle = splitContainer.SplitterRectangle;
      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;
      format.Trimming = StringTrimming.None;
      e.Graphics.DrawString(LocalizationHolder.rm.GetString("ActionNote"), this.Font, Brushes.Black, (RectangleF) splitterRectangle, format);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void splitContainerRedObject_Resize(object sender, EventArgs e)
  {
    ((Control) sender).Invalidate();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="redLayer"></param>
  private void UpdateTreeView(RedlineLayer redLayer)
  {
    if (!this.IsRedEnabled)
      return;
    TreeNode treeNode = this.SearchTree(this.treeView.Nodes, redLayer);
    if (treeNode != null)
    {
      this.treeView.SelectedNode = treeNode;
      this.treeView.Focus();
    }
    else
    {
      this.ClearBoxView();
      this._redliner.ChangeVisibleLayers((this.treeView.SelectedNode != null ? this.treeView.SelectedNode.Nodes : this.treeView.Nodes).Collect().Select<TreeNode, object>((Func<TreeNode, object>) (element => element.Tag)).ToList<object>());
    }
    this._redliner.OnChanged();
  }

  /// <summary>Очистка дерева</summary>
  public void ClearTreeView()
  {
    this.treeView.SelectedNode = (TreeNode) null;
    this._redliners.ForEach((Action<Redliner>) (item => item.Dispose()));
    this._redliners.Clear();
    this.treeView.Nodes.Clear();
    this.ClearBoxView();
  }

  /// <summary>
  /// 
  /// </summary>
  public void FillTreeView()
  {
    this.treeView.BeginUpdate();
    try
    {
      this.ClearTreeView();
      if (this._showRedLine4AllDocs == ShowDocsMode.Single)
      {
        this.CreateRedLinerTree(this.treeView.Nodes, this._redliner);
      }
      else
      {
        if (this._fileItems == null)
          return;
        TreeNode key = (TreeNode) null;
        Dictionary<TreeNode, bool> dictionary = new Dictionary<TreeNode, bool>();
        foreach (FileItem fileItem in (IEnumerable<FileItem>) this._fileItems)
        {
          if (!(Path.GetExtension(fileItem.FileName) == ExtensionsConsts.ExactSpecificationExtension))
          {
            TreeNode node = new TreeNode(fileItem.ToString())
            {
              Tag = (object) fileItem
            };
            if (!fileItem.IsFile)
            {
              this.treeView.Nodes.Add(node);
              key = node;
            }
            else
            {
              if (key != null)
              {
                key.Nodes.Add(node);
                key.Expand();
              }
              else
                this.treeView.Nodes.Add(node);
              Redliner redliner;
              if (fileItem.BlobID != this._selectedFileItem.BlobID)
              {
                redliner = new Redliner(new MapLayerCollection())
                {
                  EditRedRole = SignsClient.ShowUserGraphs(fileItem.ObjectId)
                };
                redliner.LoadData(fileItem.ObjectId, fileItem.BlobID, fileItem.FileName);
                this._redliners.Add(redliner);
              }
              else
                redliner = this._redliner;
              this.CreateRedLinerTree(node.Nodes, redliner);
              node.Expand();
              if (key != null)
                dictionary[key] = node.Nodes.Count > 0;
            }
          }
        }
        if (this._showRedLine4AllDocs != ShowDocsMode.WithRemarksOnly)
          return;
        for (int index = this.treeView.Nodes.Count - 1; index >= 0; --index)
        {
          TreeNode node = this.treeView.Nodes[index];
          bool flag;
          if (dictionary.TryGetValue(node, out flag))
          {
            if (!flag)
              this.treeView.Nodes.RemoveAt(index);
          }
          else
            this.treeView.Nodes.RemoveAt(index);
        }
      }
    }
    finally
    {
      this.treeView.EndUpdate();
    }
  }

  /// <summary>Обновить цвет узлов дерева</summary>
  public void UpdateNodesState()
  {
    this.treeView.Nodes.Collect().ToList<TreeNode>().ForEach((Action<TreeNode>) (x => this.SetNodeRemarkForeColor(x)));
  }

  /// <summary>Сохранение замечаний редйланера</summary>
  private void TrySaveRedline()
  {
    if (this._redliner == null)
      return;
    bool flag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      flag = !sessionKeeper.Session.GetObjectInfo(this._selectedFileItem.ObjectId).Empty;
    if (this._redliner.Dirty & flag)
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_376"), LocalizationHolder.rm.GetString("Client.Core_377"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
      {
        this._redliner.WriteData(this._selectedFileItem.ObjectId, this._selectedFileItem.BlobID, this._selectedFileItem.FileName);
      }
      else
      {
        this.LoadRedlineData();
        this.FillTreeView();
      }
    }
    this._redliner.OnChanged();
    IClientRedliningService crs;
    if ((crs = ServicesManager.GetService(typeof (IClientRedliningService)) as IClientRedliningService) == null || this._fileItems == null)
      return;
    List<Tuple<long, string>> list = this._fileItems.Where<FileItem>((Func<FileItem, bool>) (x => File.Exists(x.FileFullName))).Select<FileItem, Tuple<long, string>>((Func<FileItem, Tuple<long, string>>) (x => new Tuple<long, string>(x.ObjectId, x.FileFullName))).ToList<Tuple<long, string>>();
    if (!list.Any<Tuple<long, string>>())
      return;
    crs.Sync(list);
    list.ForEach((Action<Tuple<long, string>>) (item => crs.Remove(item.Item1, item.Item2)));
  }

  /// <summary>Инициализация рейдлайнера</summary>
  /// <param name="fileItemInfo"></param>
  public void InitRedlinerData()
  {
    if (this._redliner.Relative != null)
      this.LoadRedlineData();
    this.FillTreeView();
    this.CheckAllRedView();
  }

  public void Init(Control owner)
  {
    this._owner = owner;
    this._owner.SuspendLayout();
    this._owner.Controls.Add((Control) this);
    this._redMapProperty.Copy((IRedProperty) this.RedService);
    this.treeView.ImageList = this.NamedImageList.ImageList;
    this.InitializeRedLineToolBar();
    this.InitializeMenuBarTreeView();
    this.InitializeToolbarTreeView();
    this.Initialize_View();
    this.SubscribeRenderChanged();
    this.RedlineForm = false;
    this.UpdateButtons_RedLiner();
    this._owner.Resize += new EventHandler(this._owner_Resize);
    this.SetFileItemCurentEvent += new SetFileItemEventHandler(this.ViewMapPanel_SetFileItemCurentEvent);
    this.view.ViewChanging += new EventHandler(this.ViewChanging);
    this.BlackWidthService.Changed += new EventHandler(this.BlackWidthService_Changed);
    this.ActiveControl = (Control) this.view;
    this._owner.ResumeLayout(false);
    this.OnResize();
  }

  private void ViewMapPanel_SetFileItemCurentEvent(object sender, SetFileItemEventArgs e)
  {
    SetFileItemEventHandler fileItemSetCurent = this.FileItemSetCurent;
    if (fileItemSetCurent == null)
      return;
    fileItemSetCurent((object) this, e);
  }

  private void _owner_Resize(object sender, EventArgs e) => this.OnResize();

  private void OnResize()
  {
    this.Width = this.Parent.Width;
    this.Height = this.Parent.Height;
    Stack stack = this._viewStack.Clone() as Stack;
    this.view.ZoomToFit();
    this._viewStack = stack;
    EventHandler zoomChanging = this.ZoomChanging;
    if (zoomChanging == null)
      return;
    zoomChanging((object) this, new EventArgs());
  }

  public void Open(FileItem fileItemInfo, System.IServiceProvider serviceProvider)
  {
    this._selectedFileItem = fileItemInfo;
    this.PageLoadProgressBarEnabled = false;
    this._viewStack.Clear();
    RelationPair service1 = (RelationPair) null;
    ISelectedItems service2 = (ISelectedItems) null;
    if (serviceProvider != null)
    {
      serviceProvider.TryGetService<IList<FileItem>>(out this._fileItems);
      serviceProvider.TryGetService<RelationPair>(out service1);
      serviceProvider.TryGetService<ISelectedItems>(out service2);
    }
    this._mapObject = this.GetMapObject(this._selectedFileItem, service1);
    if (this._mapObject == null)
      return;
    ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, false)?.FireEvent((object) this, (NotificationEventArgs) new BeforeMapObjectViewEventArgs(this._mapObject, service2));
    this.view.Document.Add(this._mapObject);
    RectangleF bounds = this._mapObject.Bounds;
    this.view.Document.TopLeft = bounds.Location;
    this.view.Document.Size = bounds.Size;
    this.view.DocPosition = PointF.Empty;
    this.AttachPager(this._mapObject as IPager);
    this.AttachRedliner(this._selectedFileItem, this._mapObject);
    this.SetOverView();
    this.view.ZoomToFit();
    this._viewStack.Clear();
    this.Visible = true;
  }

  protected internal virtual MapObject GetMapObject(
    FileItem fileItemInfo,
    RelationPair relationPairKey)
  {
    IVisualizer visualizer1 = this.VisualizerService.GetVisualizer(this.GetExtension(fileItemInfo.FileName));
    IVisualizer visualizer2 = visualizer1;
    if (visualizer2 == null)
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("VisualizerForMapNotFound"), (object) fileItemInfo.FileName));
    MapObject viewObject;
    if (visualizer2 is IVisualizerEx visualizerEx)
    {
      VisualizerExParams visualizerExParams = new VisualizerExParams()
      {
        ObjectId = fileItemInfo.ObjectId,
        ValueIndex = fileItemInfo.ValueIndex,
        FileName = fileItemInfo.FileFullName,
        ObjectTypeId = fileItemInfo.ObjectType,
        RelationPair = relationPairKey
      };
      viewObject = visualizerEx.GetViewObject(visualizerExParams);
    }
    else
      viewObject = visualizer1.GetViewObject(fileItemInfo.ObjectId, fileItemInfo.ValueIndex, fileItemInfo.FileFullName, (byte[]) null);
    return viewObject;
  }

  public void Close()
  {
    this._viewStack.Clear();
    this.SetOverview((MapView) null);
    this.DetachPager();
    this.DetachRedliner();
    this.view.Document.Clear();
    this._mapObject?.Dispose();
    this.Visible = false;
  }

  public void Clear()
  {
    this.Close();
    this.UnsubscribeCbBoxRoleEvents();
    this.SetFileItemCurentEvent -= new SetFileItemEventHandler(this.ViewMapPanel_SetFileItemCurentEvent);
    this.view.ViewChanging -= new EventHandler(this.ViewChanging);
    this.BlackWidthService.Changed -= new EventHandler(this.BlackWidthService_Changed);
    this.UnsubscribeRenderChanged();
    if (this._owner == null)
      return;
    this._owner.Resize -= new EventHandler(this._owner_Resize);
    if (this._owner.Controls.Contains((Control) this))
      this._owner.Controls.Remove((Control) this);
    this.Dispose();
  }

  public event EventHandler PageChanged;

  public event PagesAddEventHandler PagesAdded;

  public void FirstPage() => this._pager?.First();

  public void PrevPage() => this._pager?.Prev();

  public void NextPage() => this._pager?.Next();

  public void LastPage() => this._pager?.Last();

  public object CurrentPage() => this._pager?.Current;

  public void SetCurrentPage(object page)
  {
    if (this._pager == null)
      return;
    this._pager.Current = page;
  }

  public void RaiseLoadedPages()
  {
    if (this.PagesAdded == null)
      throw new Exception("Control not subscribed for PagesAdded event");
    if (this._pager == null)
      return;
    this.AddPages(this._pager.Pages);
    EventHandler pageChanged = this.PageChanged;
    if (pageChanged == null)
      return;
    pageChanged((object) this._pager, new EventArgs());
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pager"></param>
  private void AttachPager(IPager pager)
  {
    if (this._pager == pager)
      return;
    this.DetachPager();
    this._pager = pager;
    if (this._pager == null)
      return;
    this._pager.Refit += new EventHandler(this.Pager_Refit);
    this._pager.Refresh += new EventHandler(this.Pager_Refresh);
    this._pager.PageChanged += new EventHandler(this.Pager_PageChanged);
    if (this._pager is IBackgroundPager pager1)
      pager1.NewPageAdded += new PageEventHandler(this.Pager_NewPageAdded);
    lock (this._delayedBuffer)
    {
      this._firstPageAdded = true;
      this.AddPages(this.OrderPages(this._pager.Pages, this._delayedBuffer));
    }
  }

  private object[] OrderPages(object[] firstPages, List<object> buffer)
  {
    ArrayList arrayList = new ArrayList((ICollection) this._pager.Pages);
    if (buffer.Count > 0)
    {
      int num = -1;
      if (arrayList.Count > 0)
        num = buffer.IndexOf(arrayList[arrayList.Count - 1]);
      if (num != -1)
        arrayList.AddRange((ICollection) buffer.Skip<object>(num + 1).ToArray<object>());
      else
        arrayList.AddRange((ICollection) buffer);
    }
    return arrayList.ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  private void LastPageBackGroundLoaded()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Action(this.LastPageBackGroundLoaded));
    }
    else
    {
      this.PageLoadProgressBarEnabled = false;
      EventHandler pageChanged = this.PageChanged;
      if (pageChanged != null)
        pageChanged((object) this._pager, new EventArgs());
      this.UpdateRedlineRemarksState();
    }
  }

  /// <summary>Очистка</summary>
  private void DetachPager()
  {
    if (this._pager == null)
      return;
    this._pager.Refit -= new EventHandler(this.Pager_Refit);
    this._pager.Refresh -= new EventHandler(this.Pager_Refresh);
    this._pager.PageChanged -= new EventHandler(this.Pager_PageChanged);
    if (this._pager is IBackgroundPager pager)
    {
      pager.NewPageAdded -= new PageEventHandler(this.Pager_NewPageAdded);
      pager.Abort();
    }
    this._pager = (IPager) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pages"></param>
  private void AddPages(object[] pages)
  {
    if (pages == null || pages.Length == 0)
      return;
    if (this.PagesAdded == null)
    {
      lock (this._pageBuff)
        this._pageBuff.AddRange((IEnumerable<object>) pages);
    }
    else
    {
      List<object> objectList = new List<object>();
      lock (this._pageBuff)
      {
        objectList.AddRange((IEnumerable<object>) this._pageBuff);
        objectList.AddRange((IEnumerable<object>) pages);
        this._pageBuff.Clear();
      }
      PagesAddEventHandler pagesAdded = this.PagesAdded;
      if (pagesAdded == null)
        return;
      pagesAdded((object) this, new PagerAddPagesEventArgs(objectList.ToArray()));
    }
  }

  private void Pager_Refresh(object sender, EventArgs e) => this.view.Refresh();

  private void Pager_Refit(object sender, EventArgs e) => this.view.ZoomToFit();

  private void Pager_PageChanged(object sender, EventArgs e)
  {
    this._viewStack.Clear();
    EventHandler pageChanged = this.PageChanged;
    if (pageChanged == null)
      return;
    pageChanged((object) this._pager, new EventArgs());
  }

  private void CheckPageLoadProgressBarEnabled_()
  {
    if (this.PageLoadProgressBarEnabled || !this.Visible)
      return;
    this.PageLoadProgressBarEnabled = true;
  }

  private void Pager_NewPageAdded(object sender, PagerEventArgs e)
  {
    object page = e.Page;
    bool flag = page == null;
    lock (this._delayedBuffer)
    {
      if (page != null)
        this._delayedBuffer.Add(page);
      if (this._firstPageAdded)
      {
        if (this._delayedBuffer.Count >= 100 | flag)
        {
          this.AddPages(this._delayedBuffer.ToArray());
          this._delayedBuffer.Clear();
          this.CheckPageLoadProgressBarEnabled_();
          this.UpdateRedlineRemarksState();
        }
      }
    }
    if (!flag)
      return;
    if (this._pager is IBackgroundPager pager)
      pager.NewPageAdded -= new PageEventHandler(this.Pager_NewPageAdded);
    this.LastPageBackGroundLoaded();
  }

  private void UpdateRedlineRemarksState()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new Action(this.UpdateRedlineRemarksState));
    }
    else
    {
      if (this._redliner == null)
        return;
      this.UpdateNodesState();
    }
  }

  public void ResetRankSignature() => this._rankSignature = string.Empty;

  public event SetFileItemEventHandler FileItemSetCurent;

  public bool RedLineEnabled() => this._redliner != null;

  public bool HasLayers() => this._redliner != null && this._redliner.ListRedlineLayer().Count != 0;

  public void ShowNoteProperties()
  {
    MapLayer currentRedLayer = this._redliner?.CurrentRedLayer;
    if (currentRedLayer == null)
      return;
    foreach (MapObject mapObject in currentRedLayer.GetEnumerator())
    {
      if (mapObject is MapRedNote note && note.ContainsPoint(this._notePropsPoint))
      {
        using (RedNoteEditForm redNoteEditForm = new RedNoteEditForm(note, this.view.DocScale))
        {
          redNoteEditForm.LoadSettgins((MapObject) note);
          if (redNoteEditForm.ShowDialog() != DialogResult.OK)
            break;
          this.view.StartTransaction();
          redNoteEditForm.Apply((MapObject) note);
          note.Text = redNoteEditForm.NoteText;
          this.view.FinishTransaction("SetProperties");
          this.view.OnViewChanged();
          break;
        }
      }
      if (mapObject is MapShape && mapObject.ContainsPoint(this._notePropsPoint))
      {
        using (RedPropertyShape redPropertyShape = new RedPropertyShape())
        {
          redPropertyShape.LoadSettgins(mapObject);
          if (redPropertyShape.ShowDialog() != DialogResult.OK)
            break;
          this.view.StartTransaction();
          redPropertyShape.Apply(mapObject);
          this.view.FinishTransaction("SetProperties");
          this.view.OnViewChanged();
          break;
        }
      }
    }
  }

  public bool GetViewNotesVisible() => this.RedlineForm;

  public void SetViewNotesVisible(bool value) => this.RedlineForm = value;

  public bool GetRedLineEdit() => this.IsRedlineEdit;

  public void SetRedLineEdit(bool value) => this.IsRedlineEdit = value;

  public bool GetRedNotePropertiesVisible()
  {
    MapLayer currentRedLayer = this._redliner?.CurrentRedLayer;
    if (currentRedLayer == null || !(currentRedLayer.Identifier is RedlineLayer identifier) || identifier.UserID != Redliner.UserNameID)
      return false;
    this._notePropsPoint = this.view.LastInput.DocPoint;
    foreach (MapObject mapObject in currentRedLayer.GetEnumerator())
    {
      if ((mapObject is MapRedNote || mapObject is MapShape) && mapObject.ContainsPoint(this._notePropsPoint))
        return true;
    }
    return false;
  }

  private void AttachRedliner(FileItem fileItemInfo, MapObject mapObject)
  {
    if (this.GetExtension(fileItemInfo.FileName) == ExtensionsConsts.ExactSpecificationExtension)
      return;
    this._redliner = Redliner.CreateRedLiner(mapObject, (MapView) this.view, ref this._redMapProperty);
    if (this._redliner == null)
      return;
    this._redliner.EditRedRole = SignsClient.ShowUserGraphs(fileItemInfo.ObjectId);
    this._redliner.Changed += new EventHandler(this.RedlinerChanged);
    this.VisibleChanged += new EventHandler(this.RedlinerChanged);
    if (mapObject is IPager pager)
      pager.PageChanged += new EventHandler(this.PageChanged_);
    this.InitRedlinerData();
  }

  private void DetachRedliner()
  {
    if (this._redliner == null)
      return;
    this.TrySaveRedline();
    if (this._mapObject is IPager mapObject)
      mapObject.PageChanged -= new EventHandler(this.PageChanged_);
    this._redliner.Changed -= new EventHandler(this.RedlinerChanged);
    this.VisibleChanged -= new EventHandler(this.RedlinerChanged);
    this.ClearTreeView();
    this._redliner.DeleteRedLayers();
    this._redliner.Dispose();
    this._redliner = (Redliner) null;
  }

  public event EventHandler ZoomChanging;

  public void ZoomToFit() => this.view.ZoomToFit();

  public void ZoomIn() => this.view.ZoomIn();

  public void ZoomOut() => this.view.ZoomOut();

  public void Zoom1to1()
  {
    this.AddPositionToStack();
    this.view.Zoom1to1();
  }

  public bool ZoomPrevious()
  {
    if (this._viewStack.Count > 0)
    {
      Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale posAndScale1 = (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale) this._viewStack.Peek();
      Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale posAndScale2 = new Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale(this.view.DocPosition, this.view.DocScale);
      if (posAndScale1.Equals((object) posAndScale2))
      {
        this._viewStack.Pop();
        this.ZoomPrevious();
        return this._viewStack.Count > 0;
      }
      this.view.SetPosAndScale(posAndScale1.Pos, posAndScale1.Scale);
      this._viewStack.Pop();
    }
    EventHandler zoomChanging = this.ZoomChanging;
    if (zoomChanging != null)
      zoomChanging((object) this, new EventArgs());
    return this._viewStack.Count > 0;
  }

  public void RedDistanceMeasure()
  {
    this._distanceToolActivated = !(this._redliner?.View.Tool is DistanceTool);
    if (this._distanceToolActivated)
      this._redliner?.Distance();
    else
      this._redliner?.CancelDraw();
    EventHandler measureStateChanged = this.DistanceMeasureStateChanged;
    if (measureStateChanged == null)
      return;
    measureStateChanged((object) this, new EventArgs());
  }

  public bool RedDistanceMeasureEnabled() => this._redliner?.CurrentRedLayer?.AllowEdit ?? false;

  public bool RedDistanceMeasureChecked() => this._distanceToolActivated;

  public event EventHandler DistanceMeasureStateChanged;

  public bool PreviousViewEnabled() => this._viewStack.Count > 0;

  public Control GetControlForContextMenu() => (Control) this.view;

  private void AddPositionToStack()
  {
    Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale posAndScale1 = new Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale(this.view.DocPosition, this.view.DocScale);
    if (this._viewStack.Count > 0)
    {
      Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale posAndScale2 = (Intermech.Client.Core.Navigator.Classes.ObjectNode.VisualizerView.ViewHost.Viewers.MapViewer.MapViewer.PosAndScale) this._viewStack.Peek();
      if (posAndScale1.Equals((object) posAndScale2))
        return;
      this._viewStack.Push((object) posAndScale1);
    }
    else
      this._viewStack.Push((object) posAndScale1);
    EventHandler zoomChanging = this.ZoomChanging;
    if (zoomChanging == null)
      return;
    zoomChanging((object) this, new EventArgs());
  }

  private void ViewChanging(object sender, EventArgs e) => this.AddPositionToStack();

  /// <summary>
  /// Служба для работы с настройками толщины для цвета в Acad
  /// </summary>
  private IBlackWidthService BlackWidthService { get; } = ServiceUtils.GetService<IBlackWidthService>((object) ServicesManager.ServiceContainer, true);

  public bool IsShowDwg() => this._pager is IShowDwg;

  public bool IsAllColorsToBlack() => this.BlackWidthService.AllColorToBlack;

  public bool ColorNotChanged() => this._isBlack == this.BlackWidthService.AllColorToBlack;

  public void SwitchColorToBlack()
  {
    if (!(this._pager is IShowDwg))
      return;
    this.BlackWidthService.AllColorToBlack = !this.BlackWidthService.AllColorToBlack;
    this.BlackWidthService.SaveSettings();
  }

  private void BlackWidthService_Changed(object sender, EventArgs e)
  {
    if (!(this._pager is IShowDwg))
      return;
    this._isBlack = this.BlackWidthService.AllColorToBlack;
    DwgColorChangedEventHandler dwgColorChanged = this.DwgColorChanged;
    if (dwgColorChanged != null)
      dwgColorChanged((object) this, new DwgColorChangedEventArgs(this._isBlack));
    this.view.Invalidate();
  }

  public event DwgColorChangedEventHandler DwgColorChanged;

  public void StartOverView()
  {
    ICommandManager service = ServiceUtils.GetService<ICommandManager>((object) ServicesManager.ServiceContainer, true);
    ICommandState command = service?.FindCommand("Overview");
    if (command == null || !command.Enabled)
      return;
    service.Execute(command);
  }

  private void SetOverview(MapView mapView)
  {
    ServiceUtils.GetService<IVisualizerOverview>((object) ServicesManager.ServiceContainer, false)?.Attach(mapView);
  }

  public void SetOverView() => this.SetOverview((MapView) this.view);

  /// <summary>Освободить все используемые ресурсы.</summary>
  /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Обязательный метод для поддержки конструктора - не изменяйте
  /// содержимое данного метода при помощи редактора кода.
  /// </summary>
  private void InitializeComponent()
  {
    this.splitContainer = new RedlineSplitContainer();
    this.splitContainerRedObject = new RedlineSplitContainer();
    this.treeView = new TreeView();
    this.menuBarTreeView = new MenuBar();
    this.contextMenuBarItemTree = new ContextMenuBarItem();
    this.mBtItem_Agreed = new MenuButtonItem();
    this.mBtItem_Inconsistent = new MenuButtonItem();
    this.mBtItem_Rejected = new MenuButtonItem();
    this.mBtItem_Corrected = new MenuButtonItem();
    this.mBtItem_Rename = new MenuButtonItem();
    this.mBtItem_Remove = new MenuButtonItem();
    this.toolBarTreeView = new Intermech.Bars.ToolBar();
    this.btnNew = new ButtonItem();
    this.ddCheckShowAll = new DropDownMenuItem();
    this.mbtnShowAll = new MenuButtonItem();
    this.mbtnShowWithRemarkOnly = new MenuButtonItem();
    this.btnComments = new ButtonItem();
    this.btnBlank = new ButtonItem();
    this.btnCheckFilter1 = new ButtonItem();
    this.btnCheckFilter2 = new ButtonItem();
    this.btnCheckFilter3 = new ButtonItem();
    this.btnCheckFilter4 = new ButtonItem();
    this.tBoxComment = new RichTextBox();
    this.splitContainerView = new RedlineSplitContainer();
    this.view = new RedlineView();
    this.lbUser = new Label();
    this.lbTime = new Label();
    this.tBoxTime = new TextBox();
    this.tBoxStep = new TextBox();
    this.tBoxUser = new TextBox();
    this.tBoxBusiness_process = new TextBox();
    this.label5 = new Label();
    this.lbStep = new Label();
    this.toolBarRed = new Intermech.Bars.ToolBar();
    this.btnSave = new ButtonItem();
    this.btnUndo = new ButtonItem();
    this.btnRedo = new ButtonItem();
    this.cbBoxRole = new ComboBoxItem();
    this.btnRed = new ButtonItem();
    this.btPointer = new ButtonItem();
    this.btRedLine = new ButtonItem();
    this.btRedPencil = new ButtonItem();
    this.btRedNote = new ButtonItem();
    this.btRedEllipse = new ButtonItem();
    this.btRedEllipseFill = new ButtonItem();
    this.btRedCircle = new ButtonItem();
    this.btRedCircleFill = new ButtonItem();
    this.btRedRectangle = new ButtonItem();
    this.btRedRectangleFill = new ButtonItem();
    this.pageLoadProgressBar = new ProgressBar();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.splitContainerRedObject.BeginInit();
    this.splitContainerRedObject.Panel1.SuspendLayout();
    this.splitContainerRedObject.Panel2.SuspendLayout();
    this.splitContainerRedObject.SuspendLayout();
    this.splitContainerView.BeginInit();
    this.splitContainerView.Panel1.SuspendLayout();
    this.splitContainerView.Panel2.SuspendLayout();
    this.splitContainerView.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.FixedPanel = FixedPanel.Panel1;
    this.splitContainer.Location = new Point(0, 0);
    this.splitContainer.Margin = new Padding(1);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Panel1.Controls.Add((Control) this.splitContainerRedObject);
    this.splitContainer.Panel1MinSize = 130;
    this.splitContainer.Panel2.Controls.Add((Control) this.splitContainerView);
    this.splitContainer.Panel2.Controls.Add((Control) this.toolBarRed);
    this.splitContainer.Panel2MinSize = 0;
    this.splitContainer.Size = new Size(875, 525);
    this.splitContainer.SplitterDistance = 204;
    this.splitContainer.SplitterWidth = 3;
    this.splitContainer.TabIndex = 0;
    this.splitContainerRedObject.Dock = DockStyle.Fill;
    this.splitContainerRedObject.FixedPanel = FixedPanel.Panel2;
    this.splitContainerRedObject.Location = new Point(0, 0);
    this.splitContainerRedObject.Name = "splitContainerRedObject";
    this.splitContainerRedObject.Orientation = Orientation.Horizontal;
    this.splitContainerRedObject.Panel1.Controls.Add((Control) this.treeView);
    this.splitContainerRedObject.Panel1.Controls.Add((Control) this.menuBarTreeView);
    this.splitContainerRedObject.Panel1.Controls.Add((Control) this.toolBarTreeView);
    this.splitContainerRedObject.Panel2.Controls.Add((Control) this.tBoxComment);
    this.splitContainerRedObject.Size = new Size(204, 525);
    this.splitContainerRedObject.SplitterDistance = 279;
    this.splitContainerRedObject.SplitterWidth = 24;
    this.splitContainerRedObject.TabIndex = 0;
    this.splitContainerRedObject.Paint += new PaintEventHandler(this.splitContainerRedObject_Paint);
    this.splitContainerRedObject.Resize += new EventHandler(this.splitContainerRedObject_Resize);
    this.treeView.Dock = DockStyle.Fill;
    this.treeView.Location = new Point(0, 46);
    this.treeView.Name = "treeView";
    this.treeView.Size = new Size(204, 233);
    this.treeView.TabIndex = 0;
    this.treeView.AfterLabelEdit += new NodeLabelEditEventHandler(this.treeView_AfterLabelEdit);
    this.treeView.AfterCollapse += new TreeViewEventHandler(this.treeView_AfterCollapse);
    this.treeView.DrawNode += new DrawTreeNodeEventHandler(this.treeView_DrawNode);
    this.treeView.BeforeSelect += new TreeViewCancelEventHandler(this.treeView_BeforeSelect);
    this.treeView.AfterSelect += new TreeViewEventHandler(this.treeView_AfterSelect);
    this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    this.treeView.MouseUp += new MouseEventHandler(this.treeView_MouseUp);
    this.menuBarTreeView.Guid = new Guid("3c93b2c5-40bd-44ce-9e42-ef00b3cd2ba8");
    this.menuBarTreeView.Hidden = false;
    this.menuBarTreeView.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItemTree
    });
    this.menuBarTreeView.Location = new Point(0, 20);
    this.menuBarTreeView.Name = "menuBarTreeView";
    this.menuBarTreeView.OwnerForm = (Form) null;
    this.menuBarTreeView.Size = new Size(204, 26);
    this.menuBarTreeView.TabIndex = 1;
    this.menuBarTreeView.Text = "menuBarTree";
    this.menuBarTreeView.Visible = false;
    this.contextMenuBarItemTree.CommandName = "contextMenuBarItemTree";
    this.contextMenuBarItemTree.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mBtItem_Agreed,
      (ToolbarItemBase) this.mBtItem_Inconsistent,
      (ToolbarItemBase) this.mBtItem_Rejected,
      (ToolbarItemBase) this.mBtItem_Corrected,
      (ToolbarItemBase) this.mBtItem_Rename,
      (ToolbarItemBase) this.mBtItem_Remove
    });
    this.contextMenuBarItemTree.ShowText = true;
    this.contextMenuBarItemTree.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.contextMenuBarItemTree_BeforePopup);
    this.mBtItem_Agreed.CommandName = "eAgreed";
    this.mBtItem_Agreed.ShowText = true;
    this.mBtItem_Agreed.Click += new EventHandler(this.mBtItem_Command_Click);
    this.mBtItem_Inconsistent.CommandName = "eInconsistent";
    this.mBtItem_Inconsistent.ShowText = true;
    this.mBtItem_Inconsistent.Click += new EventHandler(this.mBtItem_Command_Click);
    this.mBtItem_Rejected.CommandName = "eRejected";
    this.mBtItem_Rejected.ShowText = true;
    this.mBtItem_Rejected.Click += new EventHandler(this.mBtItem_Command_Click);
    this.mBtItem_Corrected.CommandName = "eCorrected";
    this.mBtItem_Corrected.ShowText = true;
    this.mBtItem_Corrected.Click += new EventHandler(this.mBtItem_Command_Click);
    this.mBtItem_Rename.CommandName = "eRename";
    this.mBtItem_Rename.ShowText = true;
    this.mBtItem_Rename.Click += new EventHandler(this.mBtItem_Command_Click);
    this.mBtItem_Remove.CommandName = "eRemove";
    this.mBtItem_Remove.ShowText = true;
    this.mBtItem_Remove.Click += new EventHandler(this.mBtItem_Command_Click);
    this.toolBarTreeView.FullMenus = true;
    this.toolBarTreeView.Guid = new Guid("2fba31fc-0191-4e51-b781-36a5e0f478a9");
    this.toolBarTreeView.Hidden = false;
    this.toolBarTreeView.Items.AddRange(new ToolbarItemBase[8]
    {
      (ToolbarItemBase) this.btnNew,
      (ToolbarItemBase) this.ddCheckShowAll,
      (ToolbarItemBase) this.btnComments,
      (ToolbarItemBase) this.btnBlank,
      (ToolbarItemBase) this.btnCheckFilter1,
      (ToolbarItemBase) this.btnCheckFilter2,
      (ToolbarItemBase) this.btnCheckFilter3,
      (ToolbarItemBase) this.btnCheckFilter4
    });
    this.toolBarTreeView.Location = new Point(0, 0);
    this.toolBarTreeView.Name = "toolBarTreeView";
    this.toolBarTreeView.Overflow = ToolBarOverflow.Wrap;
    this.toolBarTreeView.Size = new Size(204, 20);
    this.toolBarTreeView.TabIndex = 10;
    this.toolBarTreeView.Text = "toolBar1";
    this.toolBarTreeView.Visible = false;
    this.btnNew.CommandName = "btnNew";
    this.btnNew.Click += new EventHandler(this.BtnItemNew_Click);
    this.ddCheckShowAll.CommandName = "ddCheckShowAll";
    this.ddCheckShowAll.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.mbtnShowAll,
      (ToolbarItemBase) this.mbtnShowWithRemarkOnly
    });
    this.ddCheckShowAll.ShowText = true;
    this.ddCheckShowAll.Click += new EventHandler(this.ddCheckShowAll_Click);
    this.mbtnShowAll.CommandName = "menuButtonItem1";
    this.mbtnShowAll.ShowText = true;
    this.mbtnShowAll.Text = "menuButtonItem1";
    this.mbtnShowAll.Click += new EventHandler(this.mbtnShowDocs_Click);
    this.mbtnShowWithRemarkOnly.CommandName = "menuButtonItem2";
    this.mbtnShowWithRemarkOnly.ShowText = true;
    this.mbtnShowWithRemarkOnly.Text = "menuButtonItem2";
    this.mbtnShowWithRemarkOnly.Click += new EventHandler(this.mbtnShowDocs_Click);
    this.btnComments.CommandName = "btnComments";
    this.btnComments.Click += new EventHandler(this.btnComments_Click);
    this.btnBlank.CommandName = "btnBlank";
    this.btnBlank.Enabled = false;
    this.btnBlank.IconSize = new Size(1, 1);
    this.btnBlank.Importance = ToolBarItemImportance.Lowest;
    this.btnBlank.MinimumSize = 1;
    this.btnBlank.Padding.Bottom = 0;
    this.btnBlank.Padding.Left = 0;
    this.btnBlank.Padding.Right = 0;
    this.btnBlank.Padding.Top = 0;
    this.btnBlank.Stretch = true;
    this.btnCheckFilter1.CommandName = "btnCheckFilter1";
    this.btnCheckFilter2.CommandName = "btnCheckFilter2";
    this.btnCheckFilter3.CommandName = "btnCheckFilter3";
    this.btnCheckFilter4.CommandName = "btnCheckFilter4";
    this.tBoxComment.AcceptsTab = true;
    this.tBoxComment.Dock = DockStyle.Fill;
    this.tBoxComment.Location = new Point(0, 0);
    this.tBoxComment.MaxLength = 5000;
    this.tBoxComment.Name = "tBoxComment";
    this.tBoxComment.Size = new Size(204, 222);
    this.tBoxComment.TabIndex = 0;
    this.tBoxComment.Text = "";
    this.tBoxComment.TextChanged += new EventHandler(this.tBoxComment_TextChanged);
    this.splitContainerView.Dock = DockStyle.Fill;
    this.splitContainerView.FixedPanel = FixedPanel.Panel2;
    this.splitContainerView.Location = new Point(0, 24);
    this.splitContainerView.Name = "splitContainerView";
    this.splitContainerView.Orientation = Orientation.Horizontal;
    this.splitContainerView.Panel1.Controls.Add((Control) this.view);
    this.splitContainerView.Panel1.SizeChanged += new EventHandler(this.Panel1_SizeChanged);
    this.splitContainerView.Panel1MinSize = 0;
    this.splitContainerView.Panel2.Controls.Add((Control) this.lbUser);
    this.splitContainerView.Panel2.Controls.Add((Control) this.lbTime);
    this.splitContainerView.Panel2.Controls.Add((Control) this.tBoxTime);
    this.splitContainerView.Panel2.Controls.Add((Control) this.tBoxStep);
    this.splitContainerView.Panel2.Controls.Add((Control) this.tBoxUser);
    this.splitContainerView.Panel2.Controls.Add((Control) this.tBoxBusiness_process);
    this.splitContainerView.Panel2.Controls.Add((Control) this.label5);
    this.splitContainerView.Panel2.Controls.Add((Control) this.lbStep);
    this.splitContainerView.Panel2MinSize = 0;
    this.splitContainerView.Size = new Size(668, 501);
    this.splitContainerView.SplitterDistance = 438;
    this.splitContainerView.SplitterWidth = 1;
    this.splitContainerView.TabIndex = 15;
    this.view.AllowDrop = true;
    this.view.BackColor = Color.White;
    this.view.Dock = DockStyle.Fill;
    this.view.DragsRealtime = true;
    this.view.Location = new Point(0, 0);
    this.view.Name = "view";
    this.view.Size = new Size(668, 438);
    this.view.TabIndex = 0;
    this.lbUser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbUser.AutoSize = true;
    this.lbUser.Location = new Point(8, 13);
    this.lbUser.Name = "lbUser";
    this.lbUser.Size = new Size(37, 13);
    this.lbUser.TabIndex = 5;
    this.lbUser.Text = "ФИО:";
    this.lbTime.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbTime.AutoSize = true;
    this.lbTime.Location = new Point(8, 39);
    this.lbTime.Name = "lbTime";
    this.lbTime.Size = new Size(80 /*0x50*/, 13);
    this.lbTime.TabIndex = 6;
    this.lbTime.Text = "Дата и время:";
    this.lbTime.TextAlign = ContentAlignment.MiddleRight;
    this.tBoxTime.Location = new Point(94, 36);
    this.tBoxTime.Name = "tBoxTime";
    this.tBoxTime.ReadOnly = true;
    this.tBoxTime.Size = new Size(219, 20);
    this.tBoxTime.TabIndex = 10;
    this.tBoxStep.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tBoxStep.Location = new Point(350, 36);
    this.tBoxStep.Name = "tBoxStep";
    this.tBoxStep.ReadOnly = true;
    this.tBoxStep.Size = new Size(315, 20);
    this.tBoxStep.TabIndex = 12;
    this.tBoxUser.Location = new Point(50, 10);
    this.tBoxUser.Name = "tBoxUser";
    this.tBoxUser.ReadOnly = true;
    this.tBoxUser.Size = new Size(263, 20);
    this.tBoxUser.TabIndex = 9;
    this.tBoxBusiness_process.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tBoxBusiness_process.Location = new Point(350, 10);
    this.tBoxBusiness_process.Name = "tBoxBusiness_process";
    this.tBoxBusiness_process.ReadOnly = true;
    this.tBoxBusiness_process.Size = new Size(315, 20);
    this.tBoxBusiness_process.TabIndex = 11;
    this.label5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.label5.AutoSize = true;
    this.label5.Location = new Point(319, 13);
    this.label5.Name = "label5";
    this.label5.Size = new Size(25, 13);
    this.label5.TabIndex = 7;
    this.label5.Text = "БП:";
    this.lbStep.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.lbStep.AutoSize = true;
    this.lbStep.Location = new Point(319, 39);
    this.lbStep.Name = "lbStep";
    this.lbStep.Size = new Size(30, 13);
    this.lbStep.TabIndex = 8;
    this.lbStep.Text = "Шаг:";
    this.toolBarRed.FullMenus = true;
    this.toolBarRed.Guid = new Guid("c95020a5-1bad-437e-b8e6-9e29251590a1");
    this.toolBarRed.Hidden = false;
    this.toolBarRed.Items.AddRange(new ToolbarItemBase[15]
    {
      (ToolbarItemBase) this.btnSave,
      (ToolbarItemBase) this.btnUndo,
      (ToolbarItemBase) this.btnRedo,
      (ToolbarItemBase) this.cbBoxRole,
      (ToolbarItemBase) this.btnRed,
      (ToolbarItemBase) this.btPointer,
      (ToolbarItemBase) this.btRedLine,
      (ToolbarItemBase) this.btRedPencil,
      (ToolbarItemBase) this.btRedNote,
      (ToolbarItemBase) this.btRedEllipse,
      (ToolbarItemBase) this.btRedEllipseFill,
      (ToolbarItemBase) this.btRedCircle,
      (ToolbarItemBase) this.btRedCircleFill,
      (ToolbarItemBase) this.btRedRectangle,
      (ToolbarItemBase) this.btRedRectangleFill
    });
    this.toolBarRed.Location = new Point(0, 0);
    this.toolBarRed.Name = "toolBarRed";
    this.toolBarRed.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRed.Size = new Size(668, 24);
    this.toolBarRed.TabIndex = 0;
    this.toolBarRed.Text = "toolBarRed";
    this.toolBarRed.Visible = false;
    this.btnSave.CommandName = "Save";
    this.btnSave.Click += new EventHandler(this.RedLinerSaveRedoUndo_Click);
    this.btnUndo.BeginGroup = true;
    this.btnUndo.CommandName = "Undo";
    this.btnUndo.Click += new EventHandler(this.RedLinerSaveRedoUndo_Click);
    this.btnRedo.BeginGroup = true;
    this.btnRedo.CommandName = "Redo";
    this.btnRedo.Click += new EventHandler(this.RedLinerSaveRedoUndo_Click);
    this.cbBoxRole.BeginGroup = true;
    this.cbBoxRole.CommandName = "BoxRole";
    this.cbBoxRole.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbBoxRole.MinimumControlWidth = 227;
    this.cbBoxRole.Padding.Bottom = 0;
    this.cbBoxRole.Padding.Left = 1;
    this.cbBoxRole.Padding.Right = 1;
    this.cbBoxRole.Padding.Top = 0;
    this.btnRed.BeginGroup = true;
    this.btnRed.CommandName = "btnRed";
    this.btnRed.Click += new EventHandler(this.btnRed_Click);
    this.btPointer.AutoToggle = AutoToggleType.Radio;
    this.btPointer.BeginGroup = true;
    this.btPointer.CommandName = "RedPointer";
    this.btPointer.Click += new EventHandler(this.RedLiner_Click);
    this.btRedLine.AutoToggle = AutoToggleType.Radio;
    this.btRedLine.CommandName = "RedLine";
    this.btRedLine.Click += new EventHandler(this.RedLiner_Click);
    this.btRedPencil.AutoToggle = AutoToggleType.Radio;
    this.btRedPencil.CommandName = "RedPencil";
    this.btRedPencil.Click += new EventHandler(this.RedLiner_Click);
    this.btRedNote.AutoToggle = AutoToggleType.Radio;
    this.btRedNote.CommandName = "RedNote";
    this.btRedNote.Click += new EventHandler(this.RedLiner_Click);
    this.btRedEllipse.AutoToggle = AutoToggleType.Radio;
    this.btRedEllipse.CommandName = "RedEllipse";
    this.btRedEllipse.Click += new EventHandler(this.RedLiner_Click);
    this.btRedEllipseFill.AutoToggle = AutoToggleType.Radio;
    this.btRedEllipseFill.CommandName = "RedEllipseFill";
    this.btRedEllipseFill.Click += new EventHandler(this.RedLiner_Click);
    this.btRedCircle.AutoToggle = AutoToggleType.Radio;
    this.btRedCircle.CommandName = "RedCircle";
    this.btRedCircle.Click += new EventHandler(this.RedLiner_Click);
    this.btRedCircleFill.AutoToggle = AutoToggleType.Radio;
    this.btRedCircleFill.CommandName = "RedCircleFill";
    this.btRedCircleFill.Click += new EventHandler(this.RedLiner_Click);
    this.btRedRectangle.AutoToggle = AutoToggleType.Radio;
    this.btRedRectangle.CommandName = "RedRectangle";
    this.btRedRectangle.Click += new EventHandler(this.RedLiner_Click);
    this.btRedRectangleFill.AutoToggle = AutoToggleType.Radio;
    this.btRedRectangleFill.CommandName = "RedRectangleFill";
    this.btRedRectangleFill.Click += new EventHandler(this.RedLiner_Click);
    this.pageLoadProgressBar.Dock = DockStyle.Bottom;
    this.pageLoadProgressBar.Location = new Point(0, 525);
    this.pageLoadProgressBar.Name = "pageLoadProgressBar";
    this.pageLoadProgressBar.Size = new Size(875, 19);
    this.pageLoadProgressBar.TabIndex = 1;
    this.pageLoadProgressBar.Visible = false;
    this.AutoScaleDimensions = new SizeF(96f, 96f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.Controls.Add((Control) this.splitContainer);
    this.Controls.Add((Control) this.pageLoadProgressBar);
    this.Margin = new Padding(0);
    this.Name = nameof (MapViewer);
    this.Size = new Size(875, 544);
    this.SizeChanged += new EventHandler(this.ViewMapPanel_SizeChanged);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.splitContainerRedObject.Panel1.ResumeLayout(false);
    this.splitContainerRedObject.Panel2.ResumeLayout(false);
    this.splitContainerRedObject.EndInit();
    this.splitContainerRedObject.ResumeLayout(false);
    this.splitContainerView.Panel1.ResumeLayout(false);
    this.splitContainerView.Panel2.ResumeLayout(false);
    this.splitContainerView.Panel2.PerformLayout();
    this.splitContainerView.EndInit();
    this.splitContainerView.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private struct PosAndScale(PointF pos, float scale)
  {
    public PointF Pos = pos;
    public float Scale = scale;
  }
}
