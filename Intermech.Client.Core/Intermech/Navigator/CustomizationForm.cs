
// Type: Intermech.Navigator.CustomizationForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Client.Core.Navigator.Drawing;
using Intermech.DataFormats;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Search;
using Intermech.Search.ButtonBars;
using Intermech.Search.ContextMenus;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator;

/// <summary>
/// Форма "Настройка интерфейса пользователя" позволяет выполнять настройки команд меню и видимость панелей управления
/// </summary>
public sealed class CustomizationForm : Form
{
  private const string ImageViewsGridColumnKey = "IMAGE";
  private const string CheckBoxViewsGridColumnKey = "CHECK";
  private const string ModuleViewsGridColumnKey = "MODULE";
  private const string ViewViewsGridColumnKey = "VIEW";
  private const string ObjectTypeViewsGridColumnKey = "TYPE";
  private const string OrderIDColumnKey = "OrderID";
  private const string ViewDescriptionViewsGridColumnKey = "NOTE";
  private INamedImageList _namedImageList;
  private INavGraphicsCache _navGraphicsCache;
  private ICurrentUserAndRole _currentUserAndRole;
  /// <summary>Контейнер всех настроек интерфейса пользователя в XML</summary>
  private XMLSettingsStorage _xmlSettingsStorage = new XMLSettingsStorage();
  /// <summary>Для быстрого поиска узлов панелей управления</summary>
  private Dictionary<Intermech.Bars.ToolBar, TreeListNode> _toolbars = new Dictionary<Intermech.Bars.ToolBar, TreeListNode>(0);
  /// <summary>Сервис настраиваемых команд контекстных меню</summary>
  private AdjustableMenuCommands _adjustableMenuCommands;
  /// <summary>Сервис настраиваемых закладок "Навигатора"</summary>
  private AdjustableViews _adjustableViews;
  /// <summary>Для быстрого поиска узлов закладок "Навигатора"</summary>
  private Dictionary<AdjustableView, iGRow> _viewsCache = new Dictionary<AdjustableView, iGRow>(0);
  private List<ColorsSchemeProperties> _colorsSchemePropertiesList = new List<ColorsSchemeProperties>();
  private ColorsSchemeProperties _currentColorsSchemeProperties;
  private ColorsSchemeProperties _defaultColorsSchemeProperties;
  /// <summary>все схемы пользователя</summary>
  private AllUsersColors _allUsersColors;
  /// <summary>Флажок означает наличие текущей обработки события</summary>
  private bool _inEvent;
  private CustomizationForm.CurrentUserConfigurationSelectedItems _currentUserConfigurationSelectedItems;
  private bool _isButtonBarsEditorViewActivated;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button _closeButton;
  private ToolTip toolTip;
  private System.Windows.Forms.TabControl _tabControl;
  private System.Windows.Forms.TabPage page_Toolbars;
  private System.Windows.Forms.TabPage page_ContextMenus;
  private ImageList imagesState;
  private TreeListColumn columnToolbars;
  private Panel panelMenuCommands;
  private Button _setDefaultContextMenuButton;
  private Intermech.Bars.ToolBar toolBarTop;
  private ButtonItem _loadSettingsFormFileButtonItem;
  private ButtonItem _saveSettingsToFileButtonItem;
  private ImageList imagesToolbars;
  private OpenFileDialog _settingsOpenFileDialog;
  private SaveFileDialog _settingsSaveFileDialog;
  private System.Windows.Forms.TabPage page_Views;
  private Panel panel1;
  private Button _setDefaultViewsButton;
  private iGrid _viewsGrid;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private ContextMenuEditor _contextMenuEditor;
  private System.Windows.Forms.TabPage page_Color;
  private Panel panel2;
  private Button _deleteColorSchemeButton;
  private Button _addColorSchemeButton;
  private ComboBox _colorSchemeComboBox;
  private Label label1;
  private Panel panel3;
  private ListBox _colorSchemeElementsListBox;
  private Label label2;
  private Button _changeColorSchemeElementForeColorButton;
  private Label lbLetterColor;
  private ColorDialog _colorDialog;
  private Label lbSample;
  private ComboBox _colorSchemeElementGradientTypeComboBox;
  private Label label6;
  private Label label5;
  private Button _changeColorSchemeElementGradientEndColorButton;
  private Button _changeColorSchemeElementGradientStartColorButton;
  private Label label7;
  private Label _colorSchemeElementForeColorLabel;
  private Label _colorSchemeElementGradientEndColorLabel;
  private Label _colorSchemeElementGradientStartColorLabel;
  private Label label3;
  private System.Windows.Forms.TabPage page_NavColumns;
  private Panel panelNavColumns;
  private TableLayoutPanel tablePanelNavColumns;
  private Button _loadColumnsSettingsFromFileButton;
  private Button _setDefaultColumnsSettingsButton;
  private Button _saveColumnsSettingsToFileButton;
  private PictureBox pictureLoad;
  private PictureBox pictureSave;
  private PictureBox pictureReset;
  private Label lbNavColumnsInfo;
  private Label lbNavColumnsLoad;
  private Label lbNavColumnsSave;
  private Label lbNavColumnsReset;
  private OpenFileDialog _columnsSettingsOpenFileDialog;
  private SaveFileDialog _columnsSettingsSaveFileDialog;
  private LinkLabel _closeAllWindowsLinkLabel;
  private Button _setDefaultColorSchemeElementForeColor;
  private Button _setDefaultColorSchemeElementGradientEndColorButton;
  private Button _setDefaultColorSchemeElementGradientStartColorButton;
  private Button _setDefaultColorSchemeElementGradientTypeButton;
  private Intermech.Bars.ToolBar toolBarViews;
  private ButtonItem _checkAllViewsButtonItem;
  private ButtonItem _uncheckAllViewsButtonItem;
  private ImageList imagesTab;
  private TreeList _toolbarsTree;
  private System.Windows.Forms.TabPage _buttonBarsEditorViewTabPage;
  private ButtonBarsEditorView _buttonBarsEditorView;
  private System.Windows.Forms.TabPage _contextMenusTabPage;
  private ContextMenusForObjectEditorControl _contextMenusForObjectEditorControl;

  public CustomizationForm()
  {
    this.InitializeComponent();
    this.Init();
  }

  /// <summary>Вызвать форму "Настройка интерфейса пользователя"</summary>
  public static void Execute()
  {
    using (CustomizationForm customizationForm = new CustomizationForm())
    {
      int num = (int) customizationForm.ShowDialog();
    }
  }

  private void CustomizationForm_Load(object sender, EventArgs e)
  {
    if (this._currentUserConfigurationSelectedItems == null)
      this._currentUserConfigurationSelectedItems = new CustomizationForm.CurrentUserConfigurationSelectedItems();
    this._buttonBarsEditorView.Initialize((ISelectedItems) this._currentUserConfigurationSelectedItems, (System.IServiceProvider) ServicesManager.ServiceContainer);
    this._contextMenusForObjectEditorControl.ObjectVersionID = this._currentUserConfigurationSelectedItems.CurrentUserConfigurationVersionID;
    FormStorage.LoadLayout((Control) this);
  }

  private void CustomizationForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    (ServicesManager.GetService(typeof (IFactory)) as IFactory).ConfiguredContextMenuTemplate = AdjustableMenusHelper.BuildMenuTemplate(this._contextMenuEditor.AdjustableMenuCommands);
    FormStorage.SaveLayout((Control) this);
  }

  private void ToolbarsTree_CheckStateChanged(object sender, DevExpress.IM.XtraTreeList.NodeEventArgs e)
  {
    if (this._inEvent || e == null || e.Node == null || !(e.Node.Tag is Intermech.Bars.ToolBar))
      return;
    Intermech.Bars.ToolBar tag = e.Node.Tag as Intermech.Bars.ToolBar;
    if (!tag.Closable)
      return;
    tag.Hidden = e.Node.CheckState == CheckState.Unchecked;
    XmlNode nodeWithAttr = this._xmlSettingsStorage.FindNodeWithAttr(this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "Toolbars", true), "Toolbar", "guid", tag.Guid.ToString(), true);
    nodeWithAttr.InnerText = tag.Text;
    this._xmlSettingsStorage.SetAttributeValue(nodeWithAttr, "enabled", tag.IsOpen ? "1" : "0");
  }

  private void CheckAllViewsButtonItem_Click(object sender, EventArgs e)
  {
    if (this._inEvent)
      return;
    for (int index = 0; index < this._viewsGrid.Rows.Count; ++index)
    {
      iGRow row = this._viewsGrid.Rows[index];
      if (row.Type == iGRowType.Normal && row.Tag is AdjustableView tag)
        tag.Visible = true;
    }
    this._viewsGrid.Invalidate();
  }

  private void UncheckAllViewsButtonItem_Click(object sender, EventArgs e)
  {
    if (this._inEvent)
      return;
    for (int index = 0; index < this._viewsGrid.Rows.Count; ++index)
    {
      iGRow row = this._viewsGrid.Rows[index];
      if (row.Type == iGRowType.Normal && row.Tag is AdjustableView tag)
        tag.Visible = false;
    }
    this._viewsGrid.Invalidate();
  }

  private void ViewsGrid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (this._inEvent || e.RowIndex >= this._viewsGrid.Rows.Count || e.ColIndex != 1 || e.Button != MouseButtons.Left)
      return;
    AdjustableView tag = this._viewsGrid.Rows[e.RowIndex].Tag as AdjustableView;
    if (!new Rectangle(e.Bounds.Left + (e.Bounds.Width - this._namedImageList.ImageList.ImageSize.Width) / 2, e.Bounds.Top + (e.Bounds.Height - this._namedImageList.ImageList.ImageSize.Height) / 2, this._namedImageList.ImageList.ImageSize.Width, this._namedImageList.ImageList.ImageSize.Height).Contains(e.MousePos))
      return;
    tag.Visible = !tag.Visible;
    this._viewsGrid.Invalidate(e.Bounds);
  }

  private void SetDefaultContextMenuButton_Click(object sender, EventArgs e)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IFactory service = ServicesManager.GetService(typeof (IFactory)) as IFactory;
        this._adjustableMenuCommands.Assign(AdjustableMenusHelper.BuildFromMenuTemplate(service.ContextMenuTemplate));
        this._adjustableMenuCommands.BatchPropertiesSet((object) true);
        this._adjustableMenuCommands.SyncWithRoleSettings(sessionKeeper.Session.RoleID);
        service.ConfiguredContextMenuTemplate = AdjustableMenusHelper.BuildMenuTemplate(this._adjustableMenuCommands);
      }
    }
    finally
    {
      this.CreateMenusTree();
      this.UpdateControls();
    }
  }

  private void SetDefaultViewsButton_Click(object sender, EventArgs e)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AdjustableViews adjustableViews = this._adjustableViews;
        object[] objArray = new object[8];
        objArray[5] = (object) true;
        objArray[7] = (object) new List<int>();
        adjustableViews.BatchPropertiesSet(objArray);
        this._adjustableViews.Clear();
        AdjustableViews collection = new AdjustableViews();
        collection.SyncWithRoleSettings(sessionKeeper.Session.RoleID);
        if (collection.Count > 0)
          this._adjustableViews.AddRange((IEnumerable<AdjustableView>) collection);
        else
          this._adjustableViews.AddRange((IEnumerable<AdjustableView>) AdjustableViewsHelper.GetDefaultAdjustableViews());
      }
    }
    finally
    {
      this.CreateViewsGrid();
      this.UpdateControls();
    }
  }

  private void LoadSettingsFromFileButtonItem_Click(object sender, EventArgs e)
  {
    if (this._settingsOpenFileDialog.ShowDialog() != DialogResult.OK || !this._xmlSettingsStorage.Load(this._settingsOpenFileDialog.FileName))
      return;
    this.ApplyXMLSettings();
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_561"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  private void SaveSettingsToFileButtonItem_Click(object sender, EventArgs e)
  {
    this.GetXMLSettings();
    if (this._settingsSaveFileDialog.ShowDialog() != DialogResult.OK || !this._xmlSettingsStorage.Save(this._settingsSaveFileDialog.FileName))
      return;
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_562"), LocalizationHolder.rm.GetString("Client.Core_50"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  /// <summary>Отрисовка ячеек в гриде со списком закладок</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ViewsGrid_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (e.RowIndex >= this._viewsGrid.Rows.Count || e.ColIndex > 1 && e.ColIndex != 4)
      return;
    AdjustableView tag = this._viewsGrid.Rows[e.RowIndex].Tag as AdjustableView;
    Size imageSize;
    Rectangle bounds;
    if (e.ColIndex == 0)
    {
      int left = e.Bounds.Left;
      int width1 = e.Bounds.Width;
      imageSize = this._namedImageList.ImageList.ImageSize;
      int width2 = imageSize.Width;
      int num1 = (width1 - width2) / 2;
      int x = left + num1;
      int top = e.Bounds.Top;
      bounds = e.Bounds;
      int height1 = bounds.Height;
      imageSize = this._namedImageList.ImageList.ImageSize;
      int height2 = imageSize.Height;
      int num2 = (height1 - height2) / 2;
      int y = top + num2;
      int index = this._namedImageList.ImageIndex(tag.ImageName);
      if (index >= 0)
        this._namedImageList.ImageList.Draw(e.Graphics, new Point(x, y), index);
    }
    if (e.ColIndex == 1)
    {
      bounds = e.Bounds;
      int left = bounds.Left;
      bounds = e.Bounds;
      int width3 = bounds.Width;
      imageSize = this.imagesState.ImageSize;
      int width4 = imageSize.Width;
      int num3 = (width3 - width4) / 2;
      int x = left + num3;
      bounds = e.Bounds;
      int top = bounds.Top;
      bounds = e.Bounds;
      int height3 = bounds.Height;
      imageSize = this.imagesState.ImageSize;
      int height4 = imageSize.Height;
      int num4 = (height3 - height4) / 2;
      int y = top + num4;
      this.imagesState.Draw(e.Graphics, new Point(x, y), tag.Visible ? 1 : 0);
    }
    if (e.ColIndex != 4)
      return;
    ImageList imageList32x16 = Images32x16_Cache.GetImageList32x16();
    bounds = e.Bounds;
    int num5 = bounds.Left + 2;
    bounds = e.Bounds;
    int top1 = bounds.Top;
    bounds = e.Bounds;
    int height5 = bounds.Height;
    imageSize = imageList32x16.ImageSize;
    int height6 = imageSize.Height;
    int num6 = (height5 - height6) / 2;
    int num7 = top1 + num6;
    List<int> objectTypes = tag.ObjectTypes;
    if (objectTypes == null || objectTypes.Count == 0)
      return;
    for (int index1 = 0; index1 < objectTypes.Count; ++index1)
    {
      int num8 = num5 + 20;
      imageSize = imageList32x16.ImageSize;
      int num9 = imageSize.Width / 2;
      int num10 = num8 + num9;
      bounds = e.Bounds;
      int right = bounds.Right;
      if (num10 > right)
        break;
      int image32x16Index = Images32x16_Cache.GetImage32x16Index(4, objectTypes[index1], (NavigatorTreeNode) null);
      if (image32x16Index >= 0)
      {
        ImageList imageList = imageList32x16;
        Graphics graphics = e.Graphics;
        int x = num5;
        int y = num7;
        imageSize = imageList32x16.ImageSize;
        int width = imageSize.Width / 2;
        imageSize = imageList32x16.ImageSize;
        int height7 = imageSize.Height;
        int index2 = image32x16Index;
        imageList.Draw(graphics, x, y, width, height7, index2);
        int num11 = num5;
        imageSize = imageList32x16.ImageSize;
        int num12 = imageSize.Width / 2;
        num5 = num11 + num12 + 2;
      }
    }
  }

  private void ViewsGrid_Resize(object sender, EventArgs e)
  {
    int num = this._viewsGrid.ClientRectangle.Width - this._viewsGrid.Cols[0].Width - this._viewsGrid.Cols[1].Width - this._viewsGrid.Cols[2].Width - this._viewsGrid.Cols[3].Width - 30;
    if (num <= 0)
      return;
    this._viewsGrid.Cols[4].Width = num;
  }

  private void ToolbarsTree_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != ' ' || this._toolbarsTree.FocusedNode == null)
      return;
    this._toolbarsTree.FocusedNode.CheckState = this._toolbarsTree.FocusedNode.CheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked;
    e.Handled = true;
  }

  private void Editor_Changed(object sender, EventArgs e) => this.UpdateControls();

  private void ViewsGrid_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != ' ' || this._viewsGrid.SelectedCells.Count == 0 || !(this._viewsGrid.SelectedCells[0].Row.Tag is AdjustableView tag))
      return;
    tag.Visible = !tag.Visible;
    this._viewsGrid.Invalidate();
    e.Handled = true;
  }

  private void CustomizationForm_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    this.ShowHelpTopic();
  }

  private void CustomizationForm_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    this.ShowHelpTopic();
  }

  private void ColorSchemeElementsListBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.LoadSchemeElement();
  }

  private void ColorSchemeComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._currentColorsSchemeProperties != null)
      this.SaveCurrentScheme();
    UIColorsScheme scheme1 = this._defaultColorsSchemeProperties.Scheme;
    this._currentColorsSchemeProperties = this._colorSchemeComboBox.SelectedItem as ColorsSchemeProperties;
    UIColorsScheme scheme2 = this._currentColorsSchemeProperties.Scheme;
    object obj = this._colorSchemeElementsListBox.Items[0];
    ColorsElementProprties elementProprties1 = this._colorSchemeElementsListBox.Items[0] as ColorsElementProprties;
    Color background1;
    Color color1 = background1 = scheme2.Background;
    elementProprties1.BkEndColor = background1;
    Color color2;
    Color color3 = color2 = color1;
    elementProprties1.BkStartColor = color2;
    elementProprties1.Background = color3;
    elementProprties1.Foreground = scheme2.Foreground;
    elementProprties1.CanUseGradient = false;
    Color background2;
    Color color4 = background2 = scheme1.Background;
    elementProprties1.DefaultBkEndColor = background2;
    Color color5;
    Color color6 = color5 = color4;
    elementProprties1.DefaultBkStartColor = color5;
    elementProprties1.DefaultBackground = color6;
    elementProprties1.DefaultForeground = scheme1.Foreground;
    ColorsElementProprties elementProprties2 = this._colorSchemeElementsListBox.Items[1] as ColorsElementProprties;
    Color backgroundSelected1;
    Color color7 = backgroundSelected1 = scheme2.BackgroundSelected;
    elementProprties2.BkEndColor = backgroundSelected1;
    Color color8;
    Color color9 = color8 = color7;
    elementProprties2.BkStartColor = color8;
    elementProprties2.Background = color9;
    elementProprties2.Foreground = scheme2.ForegroundSelected;
    elementProprties2.CanUseGradient = false;
    Color backgroundSelected2;
    Color color10 = backgroundSelected2 = scheme1.BackgroundSelected;
    elementProprties2.DefaultBkEndColor = backgroundSelected2;
    Color color11;
    Color color12 = color11 = color10;
    elementProprties2.DefaultBkStartColor = color11;
    elementProprties2.DefaultBackground = color12;
    elementProprties2.DefaultForeground = scheme1.ForegroundSelected;
    ColorsElementProprties elementProprties3 = this._colorSchemeElementsListBox.Items[2] as ColorsElementProprties;
    Color selectedInactive1;
    Color color13 = selectedInactive1 = scheme2.BackgroundSelectedInactive;
    elementProprties3.BkEndColor = selectedInactive1;
    Color color14;
    Color color15 = color14 = color13;
    elementProprties3.BkStartColor = color14;
    elementProprties3.Background = color15;
    elementProprties3.Foreground = scheme2.ForegroundSelectedInactive;
    elementProprties3.CanUseGradient = false;
    Color selectedInactive2;
    Color color16 = selectedInactive2 = scheme1.BackgroundSelectedInactive;
    elementProprties3.DefaultBkEndColor = selectedInactive2;
    Color color17;
    Color color18 = color17 = color16;
    elementProprties3.DefaultBkStartColor = color17;
    elementProprties3.DefaultBackground = color18;
    elementProprties3.DefaultForeground = scheme1.ForegroundSelectedInactive;
    bool flag1 = (scheme2.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut;
    ColorsElementProprties elementProprties4 = this._colorSchemeElementsListBox.Items[3] as ColorsElementProprties;
    elementProprties4.Background = scheme2.CheckedOutBkColor;
    elementProprties4.Foreground = scheme2.ForegroundCheckedOut;
    elementProprties4.BkStartColor = scheme2.CheckedOutBkStartColor;
    elementProprties4.BkEndColor = scheme2.CheckedOutBkEndColor;
    elementProprties4.GradientMode = scheme2.CheckedOutGradientMode;
    elementProprties4.UseGradient = flag1;
    elementProprties4.DefaultForeground = scheme1.ForegroundCheckedOut;
    elementProprties4.DefaultGradientMode = scheme1.CheckedOutGradientMode;
    elementProprties4.DefaultBackground = scheme1.CheckedOutBkColor;
    elementProprties4.DefaultBkStartColor = scheme1.CheckedOutBkStartColor;
    elementProprties4.DefaultBkEndColor = scheme1.CheckedOutBkEndColor;
    bool flag2 = (scheme2.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther;
    ColorsElementProprties elementProprties5 = this._colorSchemeElementsListBox.Items[4] as ColorsElementProprties;
    elementProprties5.Background = scheme2.CheckedOutOtherBkColor;
    elementProprties5.Foreground = scheme2.ForegroundCheckedOutOther;
    elementProprties5.BkStartColor = scheme2.CheckedOutOtherBkStartColor;
    elementProprties5.BkEndColor = scheme2.CheckedOutOtherBkEndColor;
    elementProprties5.GradientMode = scheme2.CheckedOutOtherGradientMode;
    elementProprties5.UseGradient = flag2;
    elementProprties5.DefaultBackground = scheme1.CheckedOutOtherBkColor;
    elementProprties5.DefaultBkStartColor = scheme1.CheckedOutOtherBkStartColor;
    elementProprties5.DefaultBkEndColor = scheme1.CheckedOutOtherBkEndColor;
    elementProprties5.DefaultForeground = scheme1.ForegroundCheckedOutOther;
    elementProprties5.DefaultGradientMode = scheme1.CheckedOutOtherGradientMode;
    ColorsElementProprties elementProprties6 = this._colorSchemeElementsListBox.Items[5] as ColorsElementProprties;
    Color forumCaptionBkColor1;
    Color color19 = forumCaptionBkColor1 = scheme2.ForumCaptionBkColor;
    elementProprties6.BkEndColor = forumCaptionBkColor1;
    Color color20;
    Color color21 = color20 = color19;
    elementProprties6.BkStartColor = color20;
    elementProprties6.Background = color21;
    elementProprties6.Foreground = scheme2.ForumCaptionColor;
    elementProprties6.CanUseGradient = false;
    Color forumCaptionBkColor2;
    Color color22 = forumCaptionBkColor2 = scheme1.ForumCaptionBkColor;
    elementProprties6.DefaultBkEndColor = forumCaptionBkColor2;
    Color color23;
    Color color24 = color23 = color22;
    elementProprties6.DefaultBkStartColor = color23;
    elementProprties6.DefaultBackground = color24;
    elementProprties6.DefaultForeground = scheme1.ForumCaptionColor;
    ColorsElementProprties elementProprties7 = this._colorSchemeElementsListBox.Items[6] as ColorsElementProprties;
    Color forumMessageBkColor1;
    Color color25 = forumMessageBkColor1 = scheme2.ForumMessageBkColor;
    elementProprties7.BkEndColor = forumMessageBkColor1;
    Color color26;
    Color color27 = color26 = color25;
    elementProprties7.BkStartColor = color26;
    elementProprties7.Background = color27;
    elementProprties7.Foreground = scheme2.ForumMessageColor;
    elementProprties7.CanUseGradient = false;
    Color forumMessageBkColor2;
    Color color28 = forumMessageBkColor2 = scheme1.ForumMessageBkColor;
    elementProprties7.DefaultBkEndColor = forumMessageBkColor2;
    Color color29;
    Color color30 = color29 = color28;
    elementProprties7.DefaultBkStartColor = color29;
    elementProprties7.DefaultBackground = color30;
    elementProprties7.DefaultForeground = scheme1.ForumMessageColor;
    if (this._colorSchemeElementsListBox.SelectedIndex == -1)
      this._colorSchemeElementsListBox.SelectedIndex = 0;
    else
      this.LoadSchemeElement();
    this.UpdateSchemeControls();
  }

  private void ChangeColorSchemeElementGradientStartColorButton_Click(object sender, EventArgs e)
  {
    this._colorDialog.Color = this._colorSchemeElementGradientStartColorLabel.BackColor;
    if (this._colorDialog.ShowDialog() == DialogResult.OK)
    {
      this._colorSchemeElementGradientStartColorLabel.BackColor = this._colorDialog.Color;
      ColorsElementProprties selectedItem = this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties;
      if (!selectedItem.UseGradient)
        selectedItem.Background = selectedItem.BkStartColor = this._colorDialog.Color;
      else
        selectedItem.BkStartColor = this._colorDialog.Color;
    }
    this.CreateSample();
  }

  private void ChangeColorSchemeElementGradientEndColorButton_Click(object sender, EventArgs e)
  {
    this._colorDialog.Color = this._colorSchemeElementGradientEndColorLabel.BackColor;
    if (this._colorDialog.ShowDialog() == DialogResult.OK)
    {
      this._colorSchemeElementGradientEndColorLabel.BackColor = this._colorDialog.Color;
      (this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties).BkEndColor = this._colorDialog.Color;
    }
    this.CreateSample();
  }

  private void ChangeColorSchemeElementForeColorButton_Click(object sender, EventArgs e)
  {
    this._colorDialog.Color = this._colorSchemeElementForeColorLabel.BackColor;
    if (this._colorDialog.ShowDialog() == DialogResult.OK)
    {
      this._colorSchemeElementForeColorLabel.BackColor = this._colorDialog.Color;
      (this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties).Foreground = this._colorDialog.Color;
    }
    this.CreateSample();
  }

  private void AddColorSchemeButton_Click(object sender, EventArgs e)
  {
    string str = this.NextNumber();
    ColorsSchemeProperties schemeProperties = new ColorsSchemeProperties(LocalizationHolder.rm.GetString("UserColorsSheme") + str, Guid.NewGuid().ToString(), new UIColorsScheme());
    this._colorsSchemePropertiesList.Add(schemeProperties);
    this._colorSchemeComboBox.Items.Add((object) schemeProperties);
    this._colorSchemeComboBox.SelectedIndex = this._colorSchemeComboBox.Items.Count - 1;
  }

  private void ColorSchemeElementGradientTypeComboBox_SelectedIndexChanged(
    object sender,
    EventArgs e)
  {
    if (this._colorSchemeElementsListBox.SelectedItem is ColorsElementProprties selectedItem)
    {
      if (this._colorSchemeElementGradientTypeComboBox.SelectedIndex == 4)
      {
        selectedItem.UseGradient = false;
      }
      else
      {
        this._colorSchemeElementGradientStartColorLabel.BackColor = selectedItem.BkStartColor;
        selectedItem.UseGradient = true;
        selectedItem.GradientMode = (LinearGradientMode) Enum.GetValues(typeof (LinearGradientMode)).GetValue(this._colorSchemeElementGradientTypeComboBox.SelectedIndex);
      }
    }
    this.FillGradientCombo(this._colorSchemeElementGradientTypeComboBox.SelectedIndex);
    this.CreateSample();
  }

  private void lbSample_Paint(object sender, PaintEventArgs e)
  {
    int selectedIndex = this._colorSchemeElementGradientTypeComboBox.SelectedIndex;
    Rectangle clipRectangle = e.ClipRectangle;
    NavGradientBrush navGradientBrush = this._navGraphicsCache.GetNavGradientBrush(this._colorSchemeElementGradientStartColorLabel.BackColor, this._colorSchemeElementGradientEndColorLabel.BackColor, selectedIndex == 4 ? LinearGradientMode.Vertical : (LinearGradientMode) Enum.GetValues(typeof (LinearGradientMode)).GetValue(selectedIndex), clipRectangle, selectedIndex != 4);
    if (navGradientBrush != null)
    {
      try
      {
        e.Graphics.FillRectangle(navGradientBrush.Brush, clipRectangle);
      }
      finally
      {
        navGradientBrush.Dispose();
      }
    }
    SizeF sizeF = e.Graphics.MeasureString(LocalizationHolder.rm.GetString("Client.Core_1431"), this.lbSample.Font);
    PointF point = new PointF((float) (((double) this.lbSample.Width - (double) sizeF.Width) / 2.0), (float) (((double) this.lbSample.Height - (double) sizeF.Height) / 2.0));
    using (SolidBrush solidBrush = new SolidBrush(this._colorSchemeElementForeColorLabel.BackColor))
      e.Graphics.DrawString(LocalizationHolder.rm.GetString("Client.Core_1431"), this.lbSample.Font, (Brush) solidBrush, point);
  }

  private void DeleteColorSchemeButton_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DeleteUserColors"), LocalizationHolder.rm.GetString("ColorsSheme"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    this._colorsSchemePropertiesList.Remove(this._currentColorsSchemeProperties);
    int selectedIndex = this._colorSchemeComboBox.SelectedIndex;
    this._colorSchemeComboBox.Items.RemoveAt(selectedIndex);
    this._colorSchemeComboBox.SelectedIndex = selectedIndex - 1;
  }

  private void SetDefaultColorSchemeElementForeColorButton_Click(object sender, EventArgs e)
  {
    ColorsElementProprties selectedItem = this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties;
    this._colorSchemeElementForeColorLabel.BackColor = selectedItem.Foreground = selectedItem.DefaultForeground;
    this.CreateSample();
  }

  private void SetDefaultColorSchemeElementGradientTypeButton_Click(object sender, EventArgs e)
  {
    ColorsElementProprties selectedItem = this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties;
    selectedItem.GradientMode = selectedItem.DefaultGradientMode;
    this._colorSchemeElementGradientTypeComboBox.SelectedIndex = (int) selectedItem.GradientMode;
    this.CreateSample();
  }

  private void SetDefaultColorSchemeElementGradientStartColorButton_Click(
    object sender,
    EventArgs e)
  {
    ColorsElementProprties selectedItem = this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties;
    if (!selectedItem.UseGradient)
      selectedItem.Background = selectedItem.BkStartColor = this._colorSchemeElementGradientStartColorLabel.BackColor = selectedItem.DefaultBackground;
    else
      selectedItem.BkStartColor = this._colorSchemeElementGradientStartColorLabel.BackColor = selectedItem.DefaultBkStartColor;
    this.CreateSample();
  }

  private void SetDefaultColorSchemeElementGradientEndColorButton_Click(object sender, EventArgs e)
  {
    ColorsElementProprties selectedItem = this._colorSchemeElementsListBox.SelectedItem as ColorsElementProprties;
    this._colorSchemeElementGradientEndColorLabel.BackColor = selectedItem.BkEndColor = selectedItem.DefaultBkEndColor;
    this.CreateSample();
  }

  private void LoadColumnsSettingsFromFileButton_Click(object sender, EventArgs e)
  {
    if (this._columnsSettingsOpenFileDialog.ShowDialog() != DialogResult.OK)
      return;
    if ((ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService).LoadFromFile(this._columnsSettingsOpenFileDialog.FileName))
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1433"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1434"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void SaveColumnsSettingsToFileButton_Click(object sender, EventArgs e)
  {
    if (this._columnsSettingsSaveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    if ((ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService).SaveToFile(this._columnsSettingsSaveFileDialog.FileName))
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1435"), LocalizationHolder.rm.GetString("Client.Core_1317"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    else
    {
      int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1436"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void SetDefaultColumnsSettingsButton_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1437"), LocalizationHolder.rm.GetString("Client.Core_1261"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    (ServicesManager.GetService(typeof (INavigatorColumnsService)) as INavigatorColumnsService).Reset();
  }

  private void CloseAllWindowsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    DockControl[] dockControls = (ServicesManager.GetService(typeof (DockManager)) as DockManager).GetDockControls();
    if (dockControls == null || dockControls.Length == 0)
      return;
    for (int index = dockControls.Length - 1; index >= 0; --index)
    {
      if (dockControls[index].Closable)
        dockControls[index].Close();
    }
  }

  private void CloseButton_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.SaveCurrentScheme();
      this._colorsSchemePropertiesList.RemoveAt(0);
      this._allUsersColors.schemes = this._colorsSchemePropertiesList;
      this._allUsersColors.SaveToUserSettings(sessionKeeper.Session.UserID);
      if (!string.Equals(this._allUsersColors.CurrentColorsScheme.SchemeGuid, this._currentColorsSchemeProperties.SchemeGuid))
      {
        sessionKeeper.Session.Configurations.WriteString("CLIENT", "INTERFACE", "COLOR_SCHEME", this._currentColorsSchemeProperties.SchemeGuid, sessionKeeper.Session.UserID);
        this._allUsersColors.CurrentColorsScheme = this._currentColorsSchemeProperties;
      }
    }
    this._navGraphicsCache.Clear();
    this._navGraphicsCache.OnUserColorsSchemeChange();
    if (!(ServicesManager.GetService(typeof (ICurrentNavWindow)) is ICurrentNavWindow service))
      return;
    if (service.TreeView is NavigatorTreeView treeView)
      treeView.RebuildTree();
    if (!(service.NavWindow is NavWindowBase navWindow))
      return;
    navWindow.Refresh();
  }

  private void _viewsGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
    if (!(this._viewsGrid.Cols[e.ColIndex].Key == "OrderID"))
      return;
    AdjustableView tag = this._viewsGrid.Rows[e.RowIndex].Tag as AdjustableView;
    int result = 0;
    if (this._viewsGrid.Cells[e.RowIndex, e.ColIndex].Value != null && !int.TryParse(this._viewsGrid.Cells[e.RowIndex, e.ColIndex].Value.ToString(), out result))
      return;
    tag.OrderID = result;
    this._viewsGrid.Refresh();
  }

  private void ViewsGrid_EllipsisBtnClick(object sender, iGEllipsisBtnClickEventArgs e)
  {
    iGRow row = this._viewsGrid.Rows[e.RowIndex];
    AdjustableView view = row != null ? row.Tag as AdjustableView : (AdjustableView) null;
    if (view == null)
      return;
    List<int> objectTypes = view.ObjectTypes;
    if (SelectionListWindow.Execute((System.IServiceProvider) ServicesManager.ServiceContainer, objectTypes) != DialogResult.OK)
      return;
    AdjustableViewsHelper.UnregisterViewObjectTypes(view);
    objectTypes.ForEach((Action<int>) (typeID => AdjustableViewsHelper.RegisterView4ObjectType(typeID, view, true)));
    this._viewsGrid.Refresh();
  }

  private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._tabControl.SelectedTab == this._buttonBarsEditorViewTabPage && !this._isButtonBarsEditorViewActivated)
      this._buttonBarsEditorView.Activate((IView) null);
    this.UpdateSaveLoadSettingsButtons();
  }

  private void UpdateSaveLoadSettingsButtons()
  {
    this._loadSettingsFormFileButtonItem.Enabled = this._saveSettingsToFileButtonItem.Enabled = this._tabControl.SelectedTab != this.page_Toolbars && this._tabControl.SelectedTab != this.page_Views && this._tabControl.SelectedTab != this.page_NavColumns && this._tabControl.SelectedTab != this._buttonBarsEditorViewTabPage && this._tabControl.SelectedTab != this._contextMenusTabPage;
  }

  private void Init()
  {
    this._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._adjustableMenuCommands = ServicesManager.GetService(typeof (AdjustableMenuCommands)) as AdjustableMenuCommands;
    this._adjustableViews = ServicesManager.GetService(typeof (AdjustableViews)) as AdjustableViews;
    this._currentUserAndRole = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this._defaultColorsSchemeProperties = new ColorsSchemeProperties(AllUsersColors.defSchemeName, string.Empty, new UIColorsScheme());
    this.LoadCurrentColorScheme();
    this.Text = LocalizationHolder.rm.GetString("Client.Core_573");
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 70, primaryWorkingArea.Height / 100 * 60);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this.toolBarTop.ImageList = this._namedImageList != null ? this._namedImageList.ImageList : (ImageList) null;
    this._viewsGrid.ImageList = this._namedImageList != null ? this._namedImageList.ImageList : (ImageList) null;
    this._loadSettingsFormFileButtonItem.ImageIndex = this._namedImageList != null ? this._namedImageList.ImageIndex("imgOpenItem") : -1;
    this._saveSettingsToFileButtonItem.ImageIndex = this._namedImageList != null ? this._namedImageList.ImageIndex("imgSave") : -1;
    this.PrepareViewsGridsColumns();
    if (this._viewsGrid.SortObject.Count == 0)
    {
      this._viewsGrid.SortObject.Add(2);
      this._viewsGrid.SortObject.Add(3);
    }
    this.CreateToolbarsList();
    this.CreateMenusTree();
    this.CreateViewsGrid();
    this.UpdateControls();
    this._colorSchemeElementsListBox.HorizontalScrollbar = true;
  }

  private void UpdateControls()
  {
    if (this._currentUserAndRole == null || this._currentUserAndRole != null && this._currentUserAndRole.BlockedMenus)
    {
      this._tabControl.Controls.Remove((Control) this.page_ContextMenus);
      this._tabControl.Controls.Remove((Control) this._contextMenusTabPage);
    }
    if (this._currentUserAndRole == null || this._currentUserAndRole != null && this._currentUserAndRole.BlockedViews)
      this._tabControl.Controls.Remove((Control) this.page_Views);
    this._viewsGrid.Cols["CHECK"].Visible = this._checkAllViewsButtonItem.Enabled;
    this.UpdateSaveLoadSettingsButtons();
  }

  /// <summary>Создать в гриде колонки</summary>
  private void PrepareViewsGridsColumns()
  {
    iGCellStyle iGcellStyle1 = new iGCellStyle(true);
    iGcellStyle1.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    iGcellStyle1.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle1.SingleClickEdit = iGBool.False;
    iGcellStyle1.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle2 = new iGCellStyle(true);
    iGcellStyle2.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    iGcellStyle2.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle2.SingleClickEdit = iGBool.False;
    iGcellStyle2.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle3 = new iGCellStyle(true);
    iGcellStyle3.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle3.TextAlign = iGContentAlignment.MiddleLeft;
    iGcellStyle3.SingleClickEdit = iGBool.False;
    iGcellStyle3.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle4 = new iGCellStyle(true);
    iGcellStyle4.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    iGcellStyle4.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle4.ReadOnly = iGBool.False;
    iGcellStyle4.SingleClickEdit = iGBool.False;
    iGcellStyle4.TypeFlags = iGCellTypeFlags.HasEllipsisBtn;
    iGcellStyle4.Type = iGCellType.Check;
    iGcellStyle4.ValueType = typeof (object);
    (this._viewsGrid.Cols["IMAGE"] ?? this._viewsGrid.Cols.Add(new iGColPattern(32 /*0x20*/, true, true, 32 /*0x20*/, 32 /*0x20*/, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) string.Empty, "IMAGE", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle1;
    (this._viewsGrid.Cols["CHECK"] ?? this._viewsGrid.Cols.Add(new iGColPattern(32 /*0x20*/, true, true, 32 /*0x20*/, 32 /*0x20*/, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) string.Empty, "CHECK", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle2;
    iGCol iGcol = this._viewsGrid.Cols["MODULE"] ?? this._viewsGrid.Cols.Add(new iGColPattern(256 /*0x0100*/, true, true, 128 /*0x80*/, -1, true, false, true, iGSortType.ByTextNoCase, iGSortOrder.Ascending, false, (object) null, (object) "Модуль", "MODULE", -1, (object) null, (object) null, -1));
    iGcol.CellStyle = iGcellStyle3;
    iGcol.AllowGrouping = true;
    iGcol.AllowMoving = true;
    (this._viewsGrid.Cols["VIEW"] ?? this._viewsGrid.Cols.Add(new iGColPattern(256 /*0x0100*/, true, true, 128 /*0x80*/, -1, true, false, false, iGSortType.ByTextNoCase, iGSortOrder.Ascending, false, (object) null, (object) "Закладка", "VIEW", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle3;
    (this._viewsGrid.Cols["TYPE"] ?? this._viewsGrid.Cols.Add(new iGColPattern(164, true, true, 164, 164, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) "Типы объектов", "TYPE", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle4;
    (this._viewsGrid.Cols["OrderID"] ?? this._viewsGrid.Cols.Add(new iGColPattern(150, true, true, 150, 150, false, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Порядок", "OrderID", -1, (object) null, (object) null, -1))).CellStyle = new iGCellStyle(true)
    {
      Flags = iGCellFlags.DisplayText,
      ReadOnly = iGBool.False,
      SingleClickEdit = iGBool.True,
      Type = iGCellType.Text,
      ValueType = typeof (int)
    };
    (this._viewsGrid.Cols["NOTE"] ?? this._viewsGrid.Cols.Add(new iGColPattern(256 /*0x0100*/, true, true, 128 /*0x80*/, -1, true, false, false, iGSortType.ByTextNoCase, iGSortOrder.Ascending, false, (object) null, (object) "Описание закладки", "NOTE", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle3;
  }

  /// <summary>Добавить в список очередную панель управления</summary>
  /// <param name="toolbar">Панель управления</param>
  /// <returns>Добавленная панель управления или null</returns>
  private TreeListNode AddToolbar(Intermech.Bars.ToolBar toolbar)
  {
    if (toolbar == null)
      return (TreeListNode) null;
    if (this._toolbars.ContainsKey(toolbar))
      return this._toolbars[toolbar];
    TreeListNode treeListNode = this._toolbarsTree.AppendNode((object) new object[1]
    {
      (object) toolbar.Text
    }, (TreeListNode) null);
    treeListNode.ImageIndex = -1;
    treeListNode.SelectImageIndex = -1;
    treeListNode.Tag = (object) toolbar;
    treeListNode.CheckState = toolbar.IsOpen ? CheckState.Checked : CheckState.Unchecked;
    this._toolbars[toolbar] = treeListNode;
    return treeListNode;
  }

  /// <summary>Построить список доступных панелей управления</summary>
  private void CreateToolbarsList()
  {
    try
    {
      this._toolbarsTree.BeginUpdate();
      this._toolbarsTree.BeginSort();
      this._toolbars.Clear();
      this._toolbarsTree.ClearNodes();
      XmlNode node = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "Toolbars", true);
      node.RemoveAll();
      if (!(ServicesManager.GetService(typeof (BarManager)) is BarManager service))
        return;
      List<Intermech.Bars.ToolBar> toolbarsList = service.GetToolbarsList();
      for (int index = 0; index < toolbarsList.Count; ++index)
      {
        if (toolbarsList[index].Closable)
        {
          this.AddToolbar(toolbarsList[index]);
          XmlNode nodeWithAttr = this._xmlSettingsStorage.FindNodeWithAttr(node, "Toolbar", "guid", toolbarsList[index].Guid.ToString(), true);
          nodeWithAttr.InnerText = toolbarsList[index].Text;
          this._xmlSettingsStorage.SetAttributeValue(nodeWithAttr, "enabled", toolbarsList[index].IsOpen ? "1" : "0");
        }
      }
    }
    finally
    {
      this._toolbarsTree.EndSort();
      this._toolbarsTree.EndUpdate();
    }
  }

  /// <summary>Построить дерево команд меню</summary>
  private void CreateMenusTree()
  {
    if (this._adjustableMenuCommands == null)
      return;
    this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "ContextMenus", true).RemoveAll();
    this._contextMenuEditor.AdjustableMenuCommands = this._adjustableMenuCommands;
  }

  /// <summary>Добавить в список очередную закладку "Навигатора"</summary>
  /// <param name="view">Закладка "Навигатора"</param>
  /// <returns>Добавленная закладка в виде строки или null</returns>
  private iGRow AddView(AdjustableView view)
  {
    if (view == null)
      return (iGRow) null;
    if (this._viewsCache.ContainsKey(view))
      return this._viewsCache[view];
    iGRow iGrow = this._viewsGrid.Rows.Add();
    iGrow.Cells["MODULE"].Value = (object) view.Module;
    iGrow.Cells["VIEW"].Value = (object) view.Caption;
    iGrow.Cells["NOTE"].Value = (object) view.Hint;
    iGrow.Cells["OrderID"].Value = (object) view.OrderID;
    iGrow.Tag = (object) view;
    this._viewsCache[view] = iGrow;
    return iGrow;
  }

  /// <summary>Построить список закладок "Навигатора"</summary>
  private void CreateViewsGrid()
  {
    try
    {
      this._viewsGrid.BeginUpdate();
      this.PrepareViewsGridsColumns();
      this._viewsCache.Clear();
      this._viewsGrid.Rows.Clear();
      XmlNode node = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "NavigatorViews", true);
      node.RemoveAll();
      for (int index = 0; index < this._adjustableViews.Count; ++index)
      {
        this.AddView(this._adjustableViews[index]);
        XmlNode nodeWithAttr = this._xmlSettingsStorage.FindNodeWithAttr(node, "NavigatorView", "name", this._adjustableViews[index].Name, true);
        nodeWithAttr.InnerText = this._adjustableViews[index].Caption;
        this._xmlSettingsStorage.SetAttributeValue(nodeWithAttr, "enabled", this._adjustableViews[index].Visible ? "1" : "0");
      }
    }
    finally
    {
      this._viewsGrid.Sort();
      this._viewsGrid.Group();
      this._viewsGrid.EndUpdate();
      this._viewsGrid.Update();
    }
  }

  /// <summary>Применить настройки из документа XML</summary>
  private void ApplyXMLSettings()
  {
    if (this._xmlSettingsStorage == null || this._xmlSettingsStorage.document == null)
      return;
    if (this._xmlSettingsStorage.document.DocumentElement == null)
      return;
    try
    {
      this._inEvent = true;
      XmlNode node1 = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "Toolbars", true);
      BarManager service = ServicesManager.GetService(typeof (BarManager)) as BarManager;
      for (int i = 0; i < node1.ChildNodes.Count; ++i)
      {
        XmlNode childNode = node1.ChildNodes[i];
        if (!(childNode.Name != "Toolbar"))
        {
          string attributeValue = this._xmlSettingsStorage.GetAttributeValue(childNode, "guid", string.Empty);
          if (!(attributeValue == string.Empty))
          {
            Guid empty = Guid.Empty;
            Guid guid;
            try
            {
              guid = new Guid(attributeValue);
            }
            catch
            {
              continue;
            }
            Intermech.Bars.ToolBar toolbar = service.FindToolbar(guid);
            if (toolbar != null)
              toolbar.Hidden = toolbar.Closable && this._xmlSettingsStorage.GetAttributeValue(childNode, "enabled", "1") == "0";
          }
        }
      }
      XmlNode node2 = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "ContextMenus", true);
      for (int i = 0; i < node2.ChildNodes.Count; ++i)
      {
        XmlNode childNode = node2.ChildNodes[i];
        if (!(childNode.Name != "ContextMenu"))
        {
          string attributeValue = this._xmlSettingsStorage.GetAttributeValue(childNode, "command", string.Empty);
          if (!(attributeValue == string.Empty))
          {
            AdjustableMenuCommand commandFromRoot = this._adjustableMenuCommands.FindCommandFromRoot(attributeValue);
            if (commandFromRoot != null)
            {
              commandFromRoot.Visible = this._xmlSettingsStorage.GetAttributeValue(childNode, "enabled", "1") == "1";
              commandFromRoot.Group = this._xmlSettingsStorage.GetAttributeAsInt32(childNode, "groupID", commandFromRoot.Group);
              commandFromRoot.OrderBy = this._xmlSettingsStorage.GetAttributeAsInt32(childNode, "orderID", commandFromRoot.OrderBy);
            }
          }
        }
      }
      XmlNode node3 = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "NavigatorViews", true);
      for (int i = 0; i < node3.ChildNodes.Count; ++i)
      {
        XmlNode childNode = node3.ChildNodes[i];
        if (!(childNode.Name != "NavigatorView"))
        {
          string attributeValue = this._xmlSettingsStorage.GetAttributeValue(childNode, "name", string.Empty);
          if (!(attributeValue == string.Empty))
          {
            AdjustableView view = this._adjustableViews.FindView(attributeValue);
            if (view != null)
              view.Visible = this._xmlSettingsStorage.GetAttributeValue(childNode, "enabled", "1") == "1";
          }
        }
      }
      this.LoadSсhemeFromXml();
      this.CreateToolbarsList();
      this.CreateMenusTree();
      this.CreateViewsGrid();
    }
    finally
    {
      this._inEvent = false;
    }
  }

  /// <summary>Получить настройки в XML-документ</summary>
  private void GetXMLSettings()
  {
    if (this._xmlSettingsStorage == null || this._xmlSettingsStorage.document == null || this._xmlSettingsStorage.document.DocumentElement == null)
      return;
    XmlNode node = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "ContextMenus", true);
    node.RemoveAll();
    this._adjustableMenuCommands.Assign(this._contextMenuEditor.AdjustableMenuCommands);
    (ServicesManager.GetService(typeof (IFactory)) as IFactory).ConfiguredContextMenuTemplate = AdjustableMenusHelper.BuildMenuTemplate(this._adjustableMenuCommands);
    List<AdjustableMenuCommand> list = new List<AdjustableMenuCommand>();
    AdjustableMenuCommands.ExtractCommands(this._adjustableMenuCommands, ref list);
    for (int index = 0; index < list.Count; ++index)
    {
      AdjustableMenuCommand adjustableMenuCommand = list[index];
      XmlNode nodeWithAttr = this._xmlSettingsStorage.FindNodeWithAttr(node, "ContextMenu", "command", adjustableMenuCommand.Command, true);
      nodeWithAttr.InnerText = adjustableMenuCommand.Caption;
      this._xmlSettingsStorage.SetAttributeValue(nodeWithAttr, "enabled", adjustableMenuCommand.Visible ? "1" : "0");
      this._xmlSettingsStorage.SetAttributeValue(nodeWithAttr, "groupID", adjustableMenuCommand.Group.ToString());
      this._xmlSettingsStorage.SetAttributeValue(nodeWithAttr, "orderID", adjustableMenuCommand.OrderBy.ToString());
    }
    this.SaveSсhemeToXml();
  }

  private void ShowHelpTopic()
  {
    int topicID = 1906;
    if (this._tabControl.SelectedTab == this.page_ContextMenus)
      topicID = 1897;
    else if (this._tabControl.SelectedTab == this.page_Toolbars)
      topicID = 1910;
    HelpProvidersClass.ShowHelpTopic(topicID);
  }

  private void LoadCurrentColorScheme()
  {
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("Unmarked")));
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("Selected")));
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("SelectedInactive")));
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("CheckedOut")));
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("CheckedOutOther")));
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("Client.Core_1429")));
    this._colorSchemeElementsListBox.Items.Add((object) new ColorsElementProprties(LocalizationHolder.rm.GetString("Client.Core_1430")));
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._allUsersColors = this._navGraphicsCache.Schemes;
    this._colorsSchemePropertiesList = (this._allUsersColors.Clone() as AllUsersColors).schemes;
    this._colorsSchemePropertiesList.Insert(0, this._defaultColorsSchemeProperties);
    this.LoadSchemeInCombo();
  }

  public void LoadSchemeInCombo()
  {
    this._colorSchemeComboBox.Items.Clear();
    string schemeGuid = this._allUsersColors.CurrentColorsScheme.SchemeGuid;
    foreach (ColorsSchemeProperties schemeProperties in this._colorsSchemePropertiesList)
    {
      this._colorSchemeComboBox.Items.Add((object) schemeProperties);
      if (schemeGuid.Equals(schemeProperties.SchemeGuid))
        this._colorSchemeComboBox.SelectedItem = (object) schemeProperties;
    }
  }

  private void LoadSchemeElement()
  {
    if (!(this._colorSchemeElementsListBox.SelectedItem is ColorsElementProprties selectedItem))
      return;
    this._colorSchemeElementGradientStartColorLabel.BackColor = selectedItem.BkStartColor;
    this._colorSchemeElementGradientEndColorLabel.BackColor = selectedItem.BkEndColor;
    if (selectedItem.UseGradient)
      this.FillGradientCombo((int) selectedItem.GradientMode);
    else
      this.FillGradientCombo(4);
    this._colorSchemeElementForeColorLabel.BackColor = selectedItem.Foreground;
    this.UpdateElements(selectedItem);
    this.CreateSample();
  }

  private void FillGradientCombo(int gradientMode)
  {
    this.label7.Visible = this._colorSchemeElementGradientEndColorLabel.Visible = this._setDefaultColorSchemeElementGradientEndColorButton.Visible = this._changeColorSchemeElementGradientEndColorButton.Visible = gradientMode != 4;
    this.label6.Text = gradientMode != 4 ? LocalizationHolder.rm.GetString("StartGradientColor") : LocalizationHolder.rm.GetString("BackgroundColor");
    this._colorSchemeElementGradientTypeComboBox.SelectedIndex = gradientMode;
  }

  private void UpdateElements(ColorsElementProprties selEl)
  {
    this._colorSchemeElementGradientTypeComboBox.Enabled = this._setDefaultColorSchemeElementGradientTypeButton.Enabled = selEl.CanUseGradient && !string.IsNullOrEmpty(this._currentColorsSchemeProperties.SchemeGuid);
  }

  private void UpdateSchemeControls()
  {
    this._deleteColorSchemeButton.Enabled = this._changeColorSchemeElementGradientStartColorButton.Enabled = this._changeColorSchemeElementGradientEndColorButton.Enabled = this._changeColorSchemeElementForeColorButton.Enabled = this._setDefaultColorSchemeElementForeColor.Enabled = this._setDefaultColorSchemeElementGradientTypeButton.Enabled = this._setDefaultColorSchemeElementGradientStartColorButton.Enabled = this._setDefaultColorSchemeElementGradientEndColorButton.Enabled = !string.IsNullOrEmpty(this._currentColorsSchemeProperties.SchemeGuid);
  }

  private void CreateSample() => this.lbSample.Refresh();

  private string NextNumber()
  {
    List<int> intList = new List<int>();
    foreach (ColorsSchemeProperties schemeProperties in this._colorsSchemePropertiesList)
    {
      string schemeName = schemeProperties.SchemeName;
      if (schemeName.StartsWith(LocalizationHolder.rm.GetString("UserColorsSheme")))
      {
        int length = LocalizationHolder.rm.GetString("UserColorsSheme").Length;
        string str = schemeName.Substring(length);
        intList.Add(Convert.ToInt32(str));
      }
    }
    intList.Sort();
    if (intList.Count == 0 || intList[0] > 1)
      return "1";
    for (int index = 0; index < intList.Count - 1; ++index)
    {
      int num1 = intList[index];
      int num2 = intList[index + 1];
      if (num1 < num2 - 1)
        return (num1 + 1).ToString();
    }
    return (intList.Count + 1).ToString();
  }

  /// <summary>сохранить настройки сделанные для текущей схемы</summary>
  private void SaveCurrentScheme()
  {
    UIColorsScheme scheme = this._currentColorsSchemeProperties.Scheme;
    ColorsElementProprties elementProprties1 = this._colorSchemeElementsListBox.Items[0] as ColorsElementProprties;
    scheme.Background = elementProprties1.Background;
    scheme.Foreground = elementProprties1.Foreground;
    ColorsElementProprties elementProprties2 = this._colorSchemeElementsListBox.Items[1] as ColorsElementProprties;
    scheme.BackgroundSelected = elementProprties2.Background;
    scheme.ForegroundSelected = elementProprties2.Foreground;
    ColorsElementProprties elementProprties3 = this._colorSchemeElementsListBox.Items[2] as ColorsElementProprties;
    scheme.BackgroundSelectedInactive = elementProprties3.Background;
    scheme.ForegroundSelectedInactive = elementProprties3.Foreground;
    ColorsElementProprties elementProprties4 = this._colorSchemeElementsListBox.Items[3] as ColorsElementProprties;
    scheme.CheckedOutBkColor = elementProprties4.Background;
    scheme.CheckedOutBkStartColor = elementProprties4.BkStartColor;
    scheme.CheckedOutBkEndColor = elementProprties4.BkEndColor;
    scheme.CheckedOutGradientMode = elementProprties4.GradientMode;
    scheme.Gradient = elementProprties4.UseGradient ? GradientUsing.CheckOut : GradientUsing.None;
    scheme.ForegroundCheckedOut = elementProprties4.Foreground;
    ColorsElementProprties elementProprties5 = this._colorSchemeElementsListBox.Items[4] as ColorsElementProprties;
    scheme.CheckedOutOtherBkColor = elementProprties5.Background;
    scheme.CheckedOutOtherBkStartColor = elementProprties5.BkStartColor;
    scheme.CheckedOutOtherBkEndColor = elementProprties5.BkEndColor;
    scheme.CheckedOutOtherGradientMode = elementProprties5.GradientMode;
    if (elementProprties5.UseGradient)
      scheme.Gradient |= GradientUsing.CheckedOutOther;
    scheme.ForegroundCheckedOutOther = elementProprties5.Foreground;
    ColorsElementProprties elementProprties6 = this._colorSchemeElementsListBox.Items[5] as ColorsElementProprties;
    scheme.ForumCaptionBkColor = elementProprties6.Background;
    scheme.ForumCaptionColor = elementProprties6.Foreground;
    ColorsElementProprties elementProprties7 = this._colorSchemeElementsListBox.Items[6] as ColorsElementProprties;
    scheme.ForumMessageBkColor = elementProprties7.Background;
    scheme.ForumMessageColor = elementProprties7.Foreground;
  }

  /// <summary>сохранение всех пользовательских схем</summary>
  private void SaveSсhemeToXml()
  {
    this.SaveCurrentScheme();
    XmlNode node1 = this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "UserColorsScheme", true);
    node1.RemoveAll();
    foreach (ColorsSchemeProperties schemeProperties in this._colorsSchemePropertiesList)
    {
      if (schemeProperties.SchemeGuid != string.Empty)
      {
        XmlNode xmlNode = this._xmlSettingsStorage.AddNode(node1, "ColorScheme");
        this._xmlSettingsStorage.SetAttributeValue(xmlNode, "ColorSchemeName", schemeProperties.SchemeName);
        this._xmlSettingsStorage.SetAttributeValue(xmlNode, "ColorSchemeGuid", schemeProperties.SchemeGuid);
        UIColorsScheme scheme = schemeProperties.Scheme;
        XmlNode node2 = this._xmlSettingsStorage.AddNode(xmlNode, "SchemeElement");
        this._xmlSettingsStorage.SetAttributeValue(node2, "ElementId", "0");
        XMLSettingsStorage xmlSettingsStorage1 = this._xmlSettingsStorage;
        XmlNode node3 = node2;
        int num = scheme.Background.ToArgb();
        string str1 = num.ToString();
        xmlSettingsStorage1.SetAttributeValue(node3, "Background", str1);
        XMLSettingsStorage xmlSettingsStorage2 = this._xmlSettingsStorage;
        XmlNode node4 = node2;
        num = scheme.Foreground.ToArgb();
        string str2 = num.ToString();
        xmlSettingsStorage2.SetAttributeValue(node4, "Foreground", str2);
        XmlNode node5 = this._xmlSettingsStorage.AddNode(xmlNode, "SchemeElement");
        this._xmlSettingsStorage.SetAttributeValue(node5, "ElementId", "1");
        XMLSettingsStorage xmlSettingsStorage3 = this._xmlSettingsStorage;
        XmlNode node6 = node5;
        num = scheme.BackgroundSelected.ToArgb();
        string str3 = num.ToString();
        xmlSettingsStorage3.SetAttributeValue(node6, "Background", str3);
        XMLSettingsStorage xmlSettingsStorage4 = this._xmlSettingsStorage;
        XmlNode node7 = node5;
        num = scheme.ForegroundSelected.ToArgb();
        string str4 = num.ToString();
        xmlSettingsStorage4.SetAttributeValue(node7, "Foreground", str4);
        XmlNode node8 = this._xmlSettingsStorage.AddNode(xmlNode, "SchemeElement");
        this._xmlSettingsStorage.SetAttributeValue(node8, "ElementId", "2");
        XMLSettingsStorage xmlSettingsStorage5 = this._xmlSettingsStorage;
        XmlNode node9 = node8;
        num = scheme.BackgroundSelectedInactive.ToArgb();
        string str5 = num.ToString();
        xmlSettingsStorage5.SetAttributeValue(node9, "Background", str5);
        XMLSettingsStorage xmlSettingsStorage6 = this._xmlSettingsStorage;
        XmlNode node10 = node8;
        num = scheme.ForegroundSelectedInactive.ToArgb();
        string str6 = num.ToString();
        xmlSettingsStorage6.SetAttributeValue(node10, "Foreground", str6);
        XmlNode node11 = this._xmlSettingsStorage.AddNode(xmlNode, "SchemeElement");
        this._xmlSettingsStorage.SetAttributeValue(node11, "ElementId", "3");
        XMLSettingsStorage xmlSettingsStorage7 = this._xmlSettingsStorage;
        XmlNode node12 = node11;
        num = scheme.CheckedOutBkColor.ToArgb();
        string str7 = num.ToString();
        xmlSettingsStorage7.SetAttributeValue(node12, "Background", str7);
        XMLSettingsStorage xmlSettingsStorage8 = this._xmlSettingsStorage;
        XmlNode node13 = node11;
        num = scheme.ForegroundCheckedOut.ToArgb();
        string str8 = num.ToString();
        xmlSettingsStorage8.SetAttributeValue(node13, "Foreground", str8);
        XMLSettingsStorage xmlSettingsStorage9 = this._xmlSettingsStorage;
        XmlNode node14 = node11;
        num = scheme.CheckedOutBkStartColor.ToArgb();
        string str9 = num.ToString();
        xmlSettingsStorage9.SetAttributeValue(node14, "BkStartColor", str9);
        XMLSettingsStorage xmlSettingsStorage10 = this._xmlSettingsStorage;
        XmlNode node15 = node11;
        num = scheme.CheckedOutBkEndColor.ToArgb();
        string str10 = num.ToString();
        xmlSettingsStorage10.SetAttributeValue(node15, "BkEndColor", str10);
        XMLSettingsStorage xmlSettingsStorage11 = this._xmlSettingsStorage;
        XmlNode node16 = node11;
        num = (int) scheme.CheckedOutGradientMode;
        string str11 = num.ToString();
        xmlSettingsStorage11.SetAttributeValue(node16, "GradientMode", str11);
        string str12 = (scheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut ? "1" : "0";
        this._xmlSettingsStorage.SetAttributeValue(node11, "GradientUsing", str12);
        XmlNode node17 = this._xmlSettingsStorage.AddNode(xmlNode, "SchemeElement");
        this._xmlSettingsStorage.SetAttributeValue(node17, "ElementId", "4");
        XMLSettingsStorage xmlSettingsStorage12 = this._xmlSettingsStorage;
        XmlNode node18 = node17;
        num = scheme.CheckedOutOtherBkColor.ToArgb();
        string str13 = num.ToString();
        xmlSettingsStorage12.SetAttributeValue(node18, "Background", str13);
        XMLSettingsStorage xmlSettingsStorage13 = this._xmlSettingsStorage;
        XmlNode node19 = node17;
        num = scheme.ForegroundCheckedOutOther.ToArgb();
        string str14 = num.ToString();
        xmlSettingsStorage13.SetAttributeValue(node19, "Foreground", str14);
        XMLSettingsStorage xmlSettingsStorage14 = this._xmlSettingsStorage;
        XmlNode node20 = node17;
        num = scheme.CheckedOutOtherBkStartColor.ToArgb();
        string str15 = num.ToString();
        xmlSettingsStorage14.SetAttributeValue(node20, "BkStartColor", str15);
        XMLSettingsStorage xmlSettingsStorage15 = this._xmlSettingsStorage;
        XmlNode node21 = node17;
        num = scheme.CheckedOutOtherBkEndColor.ToArgb();
        string str16 = num.ToString();
        xmlSettingsStorage15.SetAttributeValue(node21, "BkEndColor", str16);
        XMLSettingsStorage xmlSettingsStorage16 = this._xmlSettingsStorage;
        XmlNode node22 = node17;
        num = (int) scheme.CheckedOutOtherGradientMode;
        string str17 = num.ToString();
        xmlSettingsStorage16.SetAttributeValue(node22, "GradientMode", str17);
        string str18 = (scheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther ? "1" : "0";
        this._xmlSettingsStorage.SetAttributeValue(node17, "GradientUsing", str18);
      }
    }
  }

  /// <summary>загружаем цветовые схемы из файла</summary>
  private void LoadSсhemeFromXml()
  {
    if (this._colorsSchemePropertiesList.Count > 1)
      this._colorsSchemePropertiesList.RemoveRange(1, this._colorsSchemePropertiesList.Count - 1);
    foreach (XmlNode childNode in this._xmlSettingsStorage.FindNode((XmlNode) this._xmlSettingsStorage.document.DocumentElement, "UserColorsScheme", true).ChildNodes)
    {
      if (!(childNode.Name != "ColorScheme"))
      {
        string attributeValue1 = this._xmlSettingsStorage.GetAttributeValue(childNode, "ColorSchemeName", LocalizationHolder.rm.GetString("Client.Core_1432"));
        string attributeValue2 = this._xmlSettingsStorage.GetAttributeValue(childNode, "ColorSchemeGuid", string.Empty);
        UIColorsScheme uiColorsScheme = new UIColorsScheme();
        string schemeGuid = attributeValue2;
        UIColorsScheme scheme = uiColorsScheme;
        ColorsSchemeProperties schemeProperties = new ColorsSchemeProperties(attributeValue1, schemeGuid, scheme);
        XmlNode nodeWithAttr1 = this._xmlSettingsStorage.FindNodeWithAttr(childNode, "SchemeElement", "ElementId", "0", false);
        string attributeValue3 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr1, "Background", "0");
        string attributeValue4 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr1, "Foreground", "0");
        uiColorsScheme.Background = Color.FromArgb(Convert.ToInt32(attributeValue3));
        uiColorsScheme.Foreground = Color.FromArgb(Convert.ToInt32(attributeValue4));
        XmlNode nodeWithAttr2 = this._xmlSettingsStorage.FindNodeWithAttr(childNode, "SchemeElement", "ElementId", "1", false);
        string attributeValue5 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr2, "Background", "0");
        string attributeValue6 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr2, "Foreground", "0");
        uiColorsScheme.BackgroundSelected = Color.FromArgb(Convert.ToInt32(attributeValue5));
        uiColorsScheme.ForegroundSelected = Color.FromArgb(Convert.ToInt32(attributeValue6));
        XmlNode nodeWithAttr3 = this._xmlSettingsStorage.FindNodeWithAttr(childNode, "SchemeElement", "ElementId", "2", false);
        string attributeValue7 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr3, "Background", "0");
        string attributeValue8 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr3, "Foreground", "0");
        uiColorsScheme.BackgroundSelectedInactive = Color.FromArgb(Convert.ToInt32(attributeValue7));
        uiColorsScheme.ForegroundSelectedInactive = Color.FromArgb(Convert.ToInt32(attributeValue8));
        XmlNode nodeWithAttr4 = this._xmlSettingsStorage.FindNodeWithAttr(childNode, "SchemeElement", "ElementId", "3", false);
        string attributeValue9 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr4, "Background", "0");
        string attributeValue10 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr4, "Foreground", "0");
        string attributeValue11 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr4, "BkEndColor", "0");
        string attributeValue12 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr4, "BkStartColor", "0");
        string attributeValue13 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr4, "GradientMode", "0");
        uiColorsScheme.CheckedOutBkColor = Color.FromArgb(Convert.ToInt32(attributeValue9));
        uiColorsScheme.ForegroundCheckedOut = Color.FromArgb(Convert.ToInt32(attributeValue10));
        uiColorsScheme.CheckedOutBkStartColor = Color.FromArgb(Convert.ToInt32(attributeValue11));
        uiColorsScheme.CheckedOutBkEndColor = Color.FromArgb(Convert.ToInt32(attributeValue12));
        uiColorsScheme.CheckedOutGradientMode = (LinearGradientMode) Enum.GetValues(typeof (LinearGradientMode)).GetValue(Convert.ToInt32(attributeValue13));
        if (this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr4, "GradientUsing", "0") == "0")
          uiColorsScheme.Gradient ^= GradientUsing.CheckOut;
        XmlNode nodeWithAttr5 = this._xmlSettingsStorage.FindNodeWithAttr(childNode, "SchemeElement", "ElementId", "4", false);
        string attributeValue14 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr5, "Background", "0");
        string attributeValue15 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr5, "Foreground", "0");
        string attributeValue16 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr5, "BkEndColor", "0");
        string attributeValue17 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr5, "BkStartColor", "0");
        string attributeValue18 = this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr5, "GradientMode", "0");
        uiColorsScheme.CheckedOutOtherBkColor = Color.FromArgb(Convert.ToInt32(attributeValue14));
        uiColorsScheme.ForegroundCheckedOutOther = Color.FromArgb(Convert.ToInt32(attributeValue15));
        uiColorsScheme.CheckedOutOtherBkStartColor = Color.FromArgb(Convert.ToInt32(attributeValue16));
        uiColorsScheme.CheckedOutOtherBkEndColor = Color.FromArgb(Convert.ToInt32(attributeValue17));
        uiColorsScheme.CheckedOutOtherGradientMode = (LinearGradientMode) Enum.GetValues(typeof (LinearGradientMode)).GetValue(Convert.ToInt32(attributeValue18));
        if (this._xmlSettingsStorage.GetAttributeValue(nodeWithAttr5, "GradientUsing", "0") == "0")
          uiColorsScheme.Gradient ^= GradientUsing.CheckedOutOther;
        this._colorsSchemePropertiesList.Add(schemeProperties);
      }
    }
    this._allUsersColors.CurrentColorsScheme.SchemeGuid = string.Empty;
    this.LoadSchemeInCombo();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomizationForm));
    ViewStyle viewStyle1 = new ViewStyle();
    ViewStyle viewStyle2 = new ViewStyle();
    ViewStyle viewStyle3 = new ViewStyle();
    ViewStyle viewStyle4 = new ViewStyle();
    ViewStyle viewStyle5 = new ViewStyle();
    this.panelBottom = new Panel();
    this._closeButton = new Button();
    this.toolTip = new ToolTip(this.components);
    this._setDefaultContextMenuButton = new Button();
    this._setDefaultViewsButton = new Button();
    this._loadColumnsSettingsFromFileButton = new Button();
    this._saveColumnsSettingsToFileButton = new Button();
    this._setDefaultColumnsSettingsButton = new Button();
    this._changeColorSchemeElementGradientEndColorButton = new Button();
    this.imagesToolbars = new ImageList(this.components);
    this._changeColorSchemeElementGradientStartColorButton = new Button();
    this._changeColorSchemeElementForeColorButton = new Button();
    this._setDefaultColorSchemeElementForeColor = new Button();
    this._setDefaultColorSchemeElementGradientStartColorButton = new Button();
    this._setDefaultColorSchemeElementGradientEndColorButton = new Button();
    this._setDefaultColorSchemeElementGradientTypeButton = new Button();
    this._tabControl = new System.Windows.Forms.TabControl();
    this.page_Toolbars = new System.Windows.Forms.TabPage();
    this._toolbarsTree = new TreeList();
    this.columnToolbars = new TreeListColumn();
    this.imagesState = new ImageList(this.components);
    this.page_ContextMenus = new System.Windows.Forms.TabPage();
    this._contextMenuEditor = new ContextMenuEditor();
    this.panelMenuCommands = new Panel();
    this.page_Views = new System.Windows.Forms.TabPage();
    this._viewsGrid = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.toolBarViews = new Intermech.Bars.ToolBar();
    this._checkAllViewsButtonItem = new ButtonItem();
    this._uncheckAllViewsButtonItem = new ButtonItem();
    this.panel1 = new Panel();
    this.page_Color = new System.Windows.Forms.TabPage();
    this.panel3 = new Panel();
    this.label3 = new Label();
    this._colorSchemeElementForeColorLabel = new Label();
    this._colorSchemeElementGradientEndColorLabel = new Label();
    this._colorSchemeElementGradientStartColorLabel = new Label();
    this._colorSchemeElementGradientTypeComboBox = new ComboBox();
    this.lbSample = new Label();
    this.label7 = new Label();
    this.label6 = new Label();
    this.label5 = new Label();
    this.lbLetterColor = new Label();
    this._colorSchemeElementsListBox = new ListBox();
    this.label2 = new Label();
    this.panel2 = new Panel();
    this._deleteColorSchemeButton = new Button();
    this._addColorSchemeButton = new Button();
    this._colorSchemeComboBox = new ComboBox();
    this.label1 = new Label();
    this.page_NavColumns = new System.Windows.Forms.TabPage();
    this.panelNavColumns = new Panel();
    this.tablePanelNavColumns = new TableLayoutPanel();
    this.pictureReset = new PictureBox();
    this.pictureSave = new PictureBox();
    this.pictureLoad = new PictureBox();
    this.lbNavColumnsInfo = new Label();
    this.lbNavColumnsLoad = new Label();
    this.lbNavColumnsSave = new Label();
    this.lbNavColumnsReset = new Label();
    this._closeAllWindowsLinkLabel = new LinkLabel();
    this._buttonBarsEditorViewTabPage = new System.Windows.Forms.TabPage();
    this._buttonBarsEditorView = new ButtonBarsEditorView();
    this._contextMenusTabPage = new System.Windows.Forms.TabPage();
    this._contextMenusForObjectEditorControl = new ContextMenusForObjectEditorControl();
    this.imagesTab = new ImageList(this.components);
    this.toolBarTop = new Intermech.Bars.ToolBar();
    this._loadSettingsFormFileButtonItem = new ButtonItem();
    this._saveSettingsToFileButtonItem = new ButtonItem();
    this._settingsOpenFileDialog = new OpenFileDialog();
    this._settingsSaveFileDialog = new SaveFileDialog();
    this._colorDialog = new ColorDialog();
    this._columnsSettingsOpenFileDialog = new OpenFileDialog();
    this._columnsSettingsSaveFileDialog = new SaveFileDialog();
    this.panelBottom.SuspendLayout();
    this._tabControl.SuspendLayout();
    this.page_Toolbars.SuspendLayout();
    this._toolbarsTree.BeginInit();
    this.page_ContextMenus.SuspendLayout();
    ((ISupportInitialize) this._contextMenuEditor).BeginInit();
    this.panelMenuCommands.SuspendLayout();
    this.page_Views.SuspendLayout();
    ((ISupportInitialize) this._viewsGrid).BeginInit();
    this.panel1.SuspendLayout();
    this.page_Color.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel2.SuspendLayout();
    this.page_NavColumns.SuspendLayout();
    this.panelNavColumns.SuspendLayout();
    this.tablePanelNavColumns.SuspendLayout();
    ((ISupportInitialize) this.pictureReset).BeginInit();
    ((ISupportInitialize) this.pictureSave).BeginInit();
    ((ISupportInitialize) this.pictureLoad).BeginInit();
    this._buttonBarsEditorViewTabPage.SuspendLayout();
    this._contextMenusTabPage.SuspendLayout();
    this._contextMenusForObjectEditorControl.BeginInit();
    this.SuspendLayout();
    this.panelBottom.Controls.Add((Control) this._closeButton);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this._closeButton, "_closeButton");
    this._closeButton.Cursor = Cursors.Default;
    this._closeButton.DialogResult = DialogResult.OK;
    this._closeButton.Name = "_closeButton";
    this._closeButton.Click += new EventHandler(this.CloseButton_Click);
    componentResourceManager.ApplyResources((object) this._setDefaultContextMenuButton, "_setDefaultContextMenuButton");
    this._setDefaultContextMenuButton.Cursor = Cursors.Default;
    this._setDefaultContextMenuButton.Name = "_setDefaultContextMenuButton";
    this.toolTip.SetToolTip((Control) this._setDefaultContextMenuButton, componentResourceManager.GetString("_setDefaultContextMenuButton.ToolTip"));
    this._setDefaultContextMenuButton.Click += new EventHandler(this.SetDefaultContextMenuButton_Click);
    componentResourceManager.ApplyResources((object) this._setDefaultViewsButton, "_setDefaultViewsButton");
    this._setDefaultViewsButton.Cursor = Cursors.Default;
    this._setDefaultViewsButton.Name = "_setDefaultViewsButton";
    this.toolTip.SetToolTip((Control) this._setDefaultViewsButton, componentResourceManager.GetString("_setDefaultViewsButton.ToolTip"));
    this._setDefaultViewsButton.Click += new EventHandler(this.SetDefaultViewsButton_Click);
    this._loadColumnsSettingsFromFileButton.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._loadColumnsSettingsFromFileButton, "_loadColumnsSettingsFromFileButton");
    this._loadColumnsSettingsFromFileButton.Name = "_loadColumnsSettingsFromFileButton";
    this.toolTip.SetToolTip((Control) this._loadColumnsSettingsFromFileButton, componentResourceManager.GetString("_loadColumnsSettingsFromFileButton.ToolTip"));
    this._loadColumnsSettingsFromFileButton.Click += new EventHandler(this.LoadColumnsSettingsFromFileButton_Click);
    this._saveColumnsSettingsToFileButton.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._saveColumnsSettingsToFileButton, "_saveColumnsSettingsToFileButton");
    this._saveColumnsSettingsToFileButton.Name = "_saveColumnsSettingsToFileButton";
    this.toolTip.SetToolTip((Control) this._saveColumnsSettingsToFileButton, componentResourceManager.GetString("_saveColumnsSettingsToFileButton.ToolTip"));
    this._saveColumnsSettingsToFileButton.Click += new EventHandler(this.SaveColumnsSettingsToFileButton_Click);
    this._setDefaultColumnsSettingsButton.Cursor = Cursors.Default;
    componentResourceManager.ApplyResources((object) this._setDefaultColumnsSettingsButton, "_setDefaultColumnsSettingsButton");
    this._setDefaultColumnsSettingsButton.Name = "_setDefaultColumnsSettingsButton";
    this.toolTip.SetToolTip((Control) this._setDefaultColumnsSettingsButton, componentResourceManager.GetString("_setDefaultColumnsSettingsButton.ToolTip"));
    this._setDefaultColumnsSettingsButton.Click += new EventHandler(this.SetDefaultColumnsSettingsButton_Click);
    componentResourceManager.ApplyResources((object) this._changeColorSchemeElementGradientEndColorButton, "_changeColorSchemeElementGradientEndColorButton");
    this._changeColorSchemeElementGradientEndColorButton.ImageList = this.imagesToolbars;
    this._changeColorSchemeElementGradientEndColorButton.Name = "_changeColorSchemeElementGradientEndColorButton";
    this.toolTip.SetToolTip((Control) this._changeColorSchemeElementGradientEndColorButton, componentResourceManager.GetString("_changeColorSchemeElementGradientEndColorButton.ToolTip"));
    this._changeColorSchemeElementGradientEndColorButton.UseVisualStyleBackColor = true;
    this._changeColorSchemeElementGradientEndColorButton.Click += new EventHandler(this.ChangeColorSchemeElementGradientEndColorButton_Click);
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "window_colors.ico");
    this.imagesToolbars.Images.SetKeyName(1, "check2.ico");
    this.imagesToolbars.Images.SetKeyName(2, "check_all.ico");
    this.imagesToolbars.Images.SetKeyName(3, "uncheck_all.ico");
    this.imagesToolbars.Images.SetKeyName(4, "asterisk.ico");
    componentResourceManager.ApplyResources((object) this._changeColorSchemeElementGradientStartColorButton, "_changeColorSchemeElementGradientStartColorButton");
    this._changeColorSchemeElementGradientStartColorButton.ImageList = this.imagesToolbars;
    this._changeColorSchemeElementGradientStartColorButton.Name = "_changeColorSchemeElementGradientStartColorButton";
    this.toolTip.SetToolTip((Control) this._changeColorSchemeElementGradientStartColorButton, componentResourceManager.GetString("_changeColorSchemeElementGradientStartColorButton.ToolTip"));
    this._changeColorSchemeElementGradientStartColorButton.UseVisualStyleBackColor = true;
    this._changeColorSchemeElementGradientStartColorButton.Click += new EventHandler(this.ChangeColorSchemeElementGradientStartColorButton_Click);
    componentResourceManager.ApplyResources((object) this._changeColorSchemeElementForeColorButton, "_changeColorSchemeElementForeColorButton");
    this._changeColorSchemeElementForeColorButton.ImageList = this.imagesToolbars;
    this._changeColorSchemeElementForeColorButton.Name = "_changeColorSchemeElementForeColorButton";
    this.toolTip.SetToolTip((Control) this._changeColorSchemeElementForeColorButton, componentResourceManager.GetString("_changeColorSchemeElementForeColorButton.ToolTip"));
    this._changeColorSchemeElementForeColorButton.UseVisualStyleBackColor = true;
    this._changeColorSchemeElementForeColorButton.Click += new EventHandler(this.ChangeColorSchemeElementForeColorButton_Click);
    componentResourceManager.ApplyResources((object) this._setDefaultColorSchemeElementForeColor, "_setDefaultColorSchemeElementForeColor");
    this._setDefaultColorSchemeElementForeColor.ImageList = this.imagesToolbars;
    this._setDefaultColorSchemeElementForeColor.Name = "_setDefaultColorSchemeElementForeColor";
    this.toolTip.SetToolTip((Control) this._setDefaultColorSchemeElementForeColor, componentResourceManager.GetString("_setDefaultColorSchemeElementForeColor.ToolTip"));
    this._setDefaultColorSchemeElementForeColor.UseVisualStyleBackColor = true;
    this._setDefaultColorSchemeElementForeColor.Click += new EventHandler(this.SetDefaultColorSchemeElementForeColorButton_Click);
    componentResourceManager.ApplyResources((object) this._setDefaultColorSchemeElementGradientStartColorButton, "_setDefaultColorSchemeElementGradientStartColorButton");
    this._setDefaultColorSchemeElementGradientStartColorButton.ImageList = this.imagesToolbars;
    this._setDefaultColorSchemeElementGradientStartColorButton.Name = "_setDefaultColorSchemeElementGradientStartColorButton";
    this.toolTip.SetToolTip((Control) this._setDefaultColorSchemeElementGradientStartColorButton, componentResourceManager.GetString("_setDefaultColorSchemeElementGradientStartColorButton.ToolTip"));
    this._setDefaultColorSchemeElementGradientStartColorButton.UseVisualStyleBackColor = true;
    this._setDefaultColorSchemeElementGradientStartColorButton.Click += new EventHandler(this.SetDefaultColorSchemeElementGradientStartColorButton_Click);
    componentResourceManager.ApplyResources((object) this._setDefaultColorSchemeElementGradientEndColorButton, "_setDefaultColorSchemeElementGradientEndColorButton");
    this._setDefaultColorSchemeElementGradientEndColorButton.ImageList = this.imagesToolbars;
    this._setDefaultColorSchemeElementGradientEndColorButton.Name = "_setDefaultColorSchemeElementGradientEndColorButton";
    this.toolTip.SetToolTip((Control) this._setDefaultColorSchemeElementGradientEndColorButton, componentResourceManager.GetString("_setDefaultColorSchemeElementGradientEndColorButton.ToolTip"));
    this._setDefaultColorSchemeElementGradientEndColorButton.UseVisualStyleBackColor = true;
    this._setDefaultColorSchemeElementGradientEndColorButton.Click += new EventHandler(this.SetDefaultColorSchemeElementGradientEndColorButton_Click);
    componentResourceManager.ApplyResources((object) this._setDefaultColorSchemeElementGradientTypeButton, "_setDefaultColorSchemeElementGradientTypeButton");
    this._setDefaultColorSchemeElementGradientTypeButton.ImageList = this.imagesToolbars;
    this._setDefaultColorSchemeElementGradientTypeButton.Name = "_setDefaultColorSchemeElementGradientTypeButton";
    this.toolTip.SetToolTip((Control) this._setDefaultColorSchemeElementGradientTypeButton, componentResourceManager.GetString("_setDefaultColorSchemeElementGradientTypeButton.ToolTip"));
    this._setDefaultColorSchemeElementGradientTypeButton.UseVisualStyleBackColor = true;
    this._setDefaultColorSchemeElementGradientTypeButton.Click += new EventHandler(this.SetDefaultColorSchemeElementGradientTypeButton_Click);
    this._tabControl.Controls.Add((Control) this.page_Toolbars);
    this._tabControl.Controls.Add((Control) this.page_ContextMenus);
    this._tabControl.Controls.Add((Control) this.page_Views);
    this._tabControl.Controls.Add((Control) this.page_Color);
    this._tabControl.Controls.Add((Control) this.page_NavColumns);
    this._tabControl.Controls.Add((Control) this._buttonBarsEditorViewTabPage);
    this._tabControl.Controls.Add((Control) this._contextMenusTabPage);
    componentResourceManager.ApplyResources((object) this._tabControl, "_tabControl");
    this._tabControl.ImageList = this.imagesTab;
    this._tabControl.Name = "_tabControl";
    this._tabControl.SelectedIndex = 0;
    this._tabControl.SelectedIndexChanged += new EventHandler(this.TabControl_SelectedIndexChanged);
    this.page_Toolbars.Controls.Add((Control) this._toolbarsTree);
    componentResourceManager.ApplyResources((object) this.page_Toolbars, "page_Toolbars");
    this.page_Toolbars.Name = "page_Toolbars";
    this.page_Toolbars.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._toolbarsTree, "_toolbarsTree");
    this._toolbarsTree.CheckBoxes = CheckBoxesStyle.ThreeState;
    this._toolbarsTree.Columns.AddRange(new TreeListColumn[1]
    {
      this.columnToolbars
    });
    this._toolbarsTree.Name = "treeTolbars";
    this._toolbarsTree.StateImageList = this.imagesState;
    viewStyle1.BackColor = SystemColors.Highlight;
    viewStyle1.ForeColor = SystemColors.HighlightText;
    viewStyle1.Options = StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage;
    viewStyle1.StyleName = "HideSelectionRow";
    viewStyle2.BackColor = Color.LightGreen;
    viewStyle2.Options = StyleOptions.None;
    viewStyle2.StyleName = "OddRow";
    viewStyle3.BackColor = SystemColors.Highlight;
    viewStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    viewStyle3.ForeColor = SystemColors.HighlightText;
    viewStyle3.StyleName = "GroupCellSelected";
    viewStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    viewStyle4.StyleName = "GroupCell";
    viewStyle5.BackColor = SystemColors.Highlight;
    viewStyle5.ForeColor = SystemColors.HighlightText;
    viewStyle5.Options = StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage;
    viewStyle5.StyleName = "FocusedCell";
    this._toolbarsTree.Styles.AddReplace("HideSelectionRow", (object) viewStyle1);
    this._toolbarsTree.Styles.AddReplace("OddRow", (object) viewStyle2);
    this._toolbarsTree.Styles.AddReplace("GroupCellSelected", (object) viewStyle3);
    this._toolbarsTree.Styles.AddReplace("GroupCell", (object) viewStyle4);
    this._toolbarsTree.Styles.AddReplace("FocusedCell", (object) viewStyle5);
    this._toolbarsTree.CheckStateChanged += new NodeEventHandler(this.ToolbarsTree_CheckStateChanged);
    this._toolbarsTree.KeyPress += new KeyPressEventHandler(this.ToolbarsTree_KeyPress);
    componentResourceManager.ApplyResources((object) this.columnToolbars, "columnToolbars");
    this.columnToolbars.Name = "columnToolbars";
    this.imagesState.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesState.ImageStream");
    this.imagesState.TransparentColor = Color.Transparent;
    this.imagesState.Images.SetKeyName(0, "unchecked.ico");
    this.imagesState.Images.SetKeyName(1, "checked.ico");
    this.imagesState.Images.SetKeyName(2, "grayed.ico");
    this.page_ContextMenus.Controls.Add((Control) this._contextMenuEditor);
    this.page_ContextMenus.Controls.Add((Control) this.panelMenuCommands);
    componentResourceManager.ApplyResources((object) this.page_ContextMenus, "page_ContextMenus");
    this.page_ContextMenus.Name = "page_ContextMenus";
    this.page_ContextMenus.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._contextMenuEditor, "_contextMenuEditor");
    this._contextMenuEditor.IsChanged = false;
    this._contextMenuEditor.Name = "_contextMenuEditor";
    this.panelMenuCommands.Controls.Add((Control) this._setDefaultContextMenuButton);
    componentResourceManager.ApplyResources((object) this.panelMenuCommands, "panelMenuCommands");
    this.panelMenuCommands.Name = "panelMenuCommands";
    this.page_Views.Controls.Add((Control) this._viewsGrid);
    this.page_Views.Controls.Add((Control) this.toolBarViews);
    this.page_Views.Controls.Add((Control) this.panel1);
    componentResourceManager.ApplyResources((object) this.page_Views, "page_Views");
    this.page_Views.Name = "page_Views";
    this.page_Views.UseVisualStyleBackColor = true;
    this._viewsGrid.BackColorEvenRows = Color.White;
    this._viewsGrid.DefaultAutoGroupRow.Height = 24;
    this._viewsGrid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this._viewsGrid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this._viewsGrid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._viewsGrid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    componentResourceManager.ApplyResources((object) this._viewsGrid, "_viewsGrid");
    this._viewsGrid.EllipsisBtnGlyph = (Image) componentResourceManager.GetObject("_viewsGrid.EllipsisBtnGlyph");
    this._viewsGrid.GroupBox.Text = componentResourceManager.GetString("_viewsGrid.GroupBox.Text");
    this._viewsGrid.GroupBox.Visible = true;
    this._viewsGrid.Header.Height = (int) componentResourceManager.GetObject("_viewsGrid.Header.Height");
    this._viewsGrid.Name = "_viewsGrid";
    this._viewsGrid.RowMode = true;
    this._viewsGrid.RowModeHasCurCell = true;
    this._viewsGrid.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this._viewsGrid.ShowControlsInAllCells = false;
    this._viewsGrid.SilentValidation = true;
    this._viewsGrid.UniqueKeys = true;
    this._viewsGrid.CellMouseUp += new iGCellMouseUpEventHandler(this.ViewsGrid_CellMouseUp);
    this._viewsGrid.EllipsisBtnClick += new iGEllipsisBtnClickEventHandler(this.ViewsGrid_EllipsisBtnClick);
    this._viewsGrid.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(this.ViewsGrid_CustomDrawCellForeground);
    this._viewsGrid.AfterCommitEdit += new iGAfterCommitEditEventHandler(this._viewsGrid_AfterCommitEdit);
    this._viewsGrid.KeyPress += new KeyPressEventHandler(this.ViewsGrid_KeyPress);
    this._viewsGrid.Resize += new EventHandler(this.ViewsGrid_Resize);
    this.toolBarViews.AddRemoveButtonsVisible = false;
    this.toolBarViews.AllowHorizontalDock = false;
    this.toolBarViews.DockLine = 3;
    this.toolBarViews.DrawActionsButton = false;
    this.toolBarViews.FullMenus = true;
    this.toolBarViews.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarViews.Hidden = true;
    this.toolBarViews.ImageList = this.imagesToolbars;
    this.toolBarViews.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this._checkAllViewsButtonItem,
      (ToolbarItemBase) this._uncheckAllViewsButtonItem
    });
    componentResourceManager.ApplyResources((object) this.toolBarViews, "toolBarViews");
    this.toolBarViews.MinimumFloatingSize = new Size(250, 30);
    this.toolBarViews.Name = "toolBarViews";
    this.toolBarViews.Overflow = ToolBarOverflow.Wrap;
    this.toolBarViews.Stretch = true;
    this.toolBarViews.Tearable = false;
    componentResourceManager.ApplyResources((object) this._checkAllViewsButtonItem, "_checkAllViewsButtonItem");
    this._checkAllViewsButtonItem.ImageIndex = 2;
    this._checkAllViewsButtonItem.ShowText = true;
    this._checkAllViewsButtonItem.Click += new EventHandler(this.CheckAllViewsButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._uncheckAllViewsButtonItem, "_uncheckAllViewsButtonItem");
    this._uncheckAllViewsButtonItem.ImageIndex = 3;
    this._uncheckAllViewsButtonItem.ShowText = true;
    this._uncheckAllViewsButtonItem.Click += new EventHandler(this.UncheckAllViewsButtonItem_Click);
    this.panel1.Controls.Add((Control) this._setDefaultViewsButton);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.page_Color.Controls.Add((Control) this.panel3);
    this.page_Color.Controls.Add((Control) this.panel2);
    componentResourceManager.ApplyResources((object) this.page_Color, "page_Color");
    this.page_Color.Name = "page_Color";
    this.page_Color.UseVisualStyleBackColor = true;
    this.panel3.BackColor = SystemColors.Control;
    this.panel3.Controls.Add((Control) this.label3);
    this.panel3.Controls.Add((Control) this._colorSchemeElementForeColorLabel);
    this.panel3.Controls.Add((Control) this._colorSchemeElementGradientEndColorLabel);
    this.panel3.Controls.Add((Control) this._colorSchemeElementGradientStartColorLabel);
    this.panel3.Controls.Add((Control) this._colorSchemeElementGradientTypeComboBox);
    this.panel3.Controls.Add((Control) this.lbSample);
    this.panel3.Controls.Add((Control) this._changeColorSchemeElementGradientEndColorButton);
    this.panel3.Controls.Add((Control) this._changeColorSchemeElementGradientStartColorButton);
    this.panel3.Controls.Add((Control) this._setDefaultColorSchemeElementGradientEndColorButton);
    this.panel3.Controls.Add((Control) this._setDefaultColorSchemeElementGradientTypeButton);
    this.panel3.Controls.Add((Control) this._setDefaultColorSchemeElementGradientStartColorButton);
    this.panel3.Controls.Add((Control) this._setDefaultColorSchemeElementForeColor);
    this.panel3.Controls.Add((Control) this._changeColorSchemeElementForeColorButton);
    this.panel3.Controls.Add((Control) this.label7);
    this.panel3.Controls.Add((Control) this.label6);
    this.panel3.Controls.Add((Control) this.label5);
    this.panel3.Controls.Add((Control) this.lbLetterColor);
    this.panel3.Controls.Add((Control) this._colorSchemeElementsListBox);
    this.panel3.Controls.Add((Control) this.label2);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.BorderStyle = BorderStyle.Fixed3D;
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this._colorSchemeElementForeColorLabel, "_colorSchemeElementForeColorLabel");
    this._colorSchemeElementForeColorLabel.BorderStyle = BorderStyle.Fixed3D;
    this._colorSchemeElementForeColorLabel.Name = "_colorSchemeElementForeColorLabel";
    componentResourceManager.ApplyResources((object) this._colorSchemeElementGradientEndColorLabel, "_colorSchemeElementGradientEndColorLabel");
    this._colorSchemeElementGradientEndColorLabel.BorderStyle = BorderStyle.Fixed3D;
    this._colorSchemeElementGradientEndColorLabel.Name = "_colorSchemeElementGradientEndColorLabel";
    componentResourceManager.ApplyResources((object) this._colorSchemeElementGradientStartColorLabel, "_colorSchemeElementGradientStartColorLabel");
    this._colorSchemeElementGradientStartColorLabel.BorderStyle = BorderStyle.Fixed3D;
    this._colorSchemeElementGradientStartColorLabel.Name = "_colorSchemeElementGradientStartColorLabel";
    componentResourceManager.ApplyResources((object) this._colorSchemeElementGradientTypeComboBox, "_colorSchemeElementGradientTypeComboBox");
    this._colorSchemeElementGradientTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._colorSchemeElementGradientTypeComboBox.FormattingEnabled = true;
    this._colorSchemeElementGradientTypeComboBox.Items.AddRange(new object[5]
    {
      (object) componentResourceManager.GetString("_colorSchemeElementGradientTypeComboBox.Items"),
      (object) componentResourceManager.GetString("_colorSchemeElementGradientTypeComboBox.Items1"),
      (object) componentResourceManager.GetString("_colorSchemeElementGradientTypeComboBox.Items2"),
      (object) componentResourceManager.GetString("_colorSchemeElementGradientTypeComboBox.Items3"),
      (object) componentResourceManager.GetString("_colorSchemeElementGradientTypeComboBox.Items4")
    });
    this._colorSchemeElementGradientTypeComboBox.Name = "_colorSchemeElementGradientTypeComboBox";
    this._colorSchemeElementGradientTypeComboBox.SelectedIndexChanged += new EventHandler(this.ColorSchemeElementGradientTypeComboBox_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.lbSample, "lbSample");
    this.lbSample.BorderStyle = BorderStyle.Fixed3D;
    this.lbSample.ForeColor = Color.Black;
    this.lbSample.Name = "lbSample";
    this.lbSample.Paint += new PaintEventHandler(this.lbSample_Paint);
    componentResourceManager.ApplyResources((object) this.label7, "label7");
    this.label7.Name = "label7";
    componentResourceManager.ApplyResources((object) this.label6, "label6");
    this.label6.Name = "label6";
    componentResourceManager.ApplyResources((object) this.label5, "label5");
    this.label5.Name = "label5";
    componentResourceManager.ApplyResources((object) this.lbLetterColor, "lbLetterColor");
    this.lbLetterColor.Name = "lbLetterColor";
    componentResourceManager.ApplyResources((object) this._colorSchemeElementsListBox, "_colorSchemeElementsListBox");
    this._colorSchemeElementsListBox.FormattingEnabled = true;
    this._colorSchemeElementsListBox.Name = "_colorSchemeElementsListBox";
    this._colorSchemeElementsListBox.SelectedIndexChanged += new EventHandler(this.ColorSchemeElementsListBox_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    this.panel2.BackColor = SystemColors.Control;
    this.panel2.Controls.Add((Control) this._deleteColorSchemeButton);
    this.panel2.Controls.Add((Control) this._addColorSchemeButton);
    this.panel2.Controls.Add((Control) this._colorSchemeComboBox);
    this.panel2.Controls.Add((Control) this.label1);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this._deleteColorSchemeButton, "_deleteColorSchemeButton");
    this._deleteColorSchemeButton.Name = "_deleteColorSchemeButton";
    this._deleteColorSchemeButton.UseVisualStyleBackColor = true;
    this._deleteColorSchemeButton.Click += new EventHandler(this.DeleteColorSchemeButton_Click);
    componentResourceManager.ApplyResources((object) this._addColorSchemeButton, "_addColorSchemeButton");
    this._addColorSchemeButton.Name = "_addColorSchemeButton";
    this._addColorSchemeButton.UseVisualStyleBackColor = true;
    this._addColorSchemeButton.Click += new EventHandler(this.AddColorSchemeButton_Click);
    componentResourceManager.ApplyResources((object) this._colorSchemeComboBox, "_colorSchemeComboBox");
    this._colorSchemeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._colorSchemeComboBox.FormattingEnabled = true;
    this._colorSchemeComboBox.Name = "_colorSchemeComboBox";
    this._colorSchemeComboBox.SelectedIndexChanged += new EventHandler(this.ColorSchemeComboBox_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    this.page_NavColumns.Controls.Add((Control) this.panelNavColumns);
    componentResourceManager.ApplyResources((object) this.page_NavColumns, "page_NavColumns");
    this.page_NavColumns.Name = "page_NavColumns";
    this.page_NavColumns.UseVisualStyleBackColor = true;
    this.panelNavColumns.Controls.Add((Control) this.tablePanelNavColumns);
    componentResourceManager.ApplyResources((object) this.panelNavColumns, "panelNavColumns");
    this.panelNavColumns.Name = "panelNavColumns";
    componentResourceManager.ApplyResources((object) this.tablePanelNavColumns, "tablePanelNavColumns");
    this.tablePanelNavColumns.Controls.Add((Control) this.pictureReset, 0, 9);
    this.tablePanelNavColumns.Controls.Add((Control) this.pictureSave, 0, 7);
    this.tablePanelNavColumns.Controls.Add((Control) this._setDefaultColumnsSettingsButton, 4, 9);
    this.tablePanelNavColumns.Controls.Add((Control) this._saveColumnsSettingsToFileButton, 4, 7);
    this.tablePanelNavColumns.Controls.Add((Control) this._loadColumnsSettingsFromFileButton, 4, 4);
    this.tablePanelNavColumns.Controls.Add((Control) this.pictureLoad, 0, 4);
    this.tablePanelNavColumns.Controls.Add((Control) this.lbNavColumnsInfo, 0, 0);
    this.tablePanelNavColumns.Controls.Add((Control) this.lbNavColumnsLoad, 2, 4);
    this.tablePanelNavColumns.Controls.Add((Control) this.lbNavColumnsSave, 2, 7);
    this.tablePanelNavColumns.Controls.Add((Control) this.lbNavColumnsReset, 2, 9);
    this.tablePanelNavColumns.Controls.Add((Control) this._closeAllWindowsLinkLabel, 0, 2);
    this.tablePanelNavColumns.Name = "tablePanelNavColumns";
    componentResourceManager.ApplyResources((object) this.pictureReset, "pictureReset");
    this.pictureReset.Name = "pictureReset";
    this.pictureReset.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureSave, "pictureSave");
    this.pictureSave.Name = "pictureSave";
    this.pictureSave.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pictureLoad, "pictureLoad");
    this.pictureLoad.Name = "pictureLoad";
    this.pictureLoad.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lbNavColumnsInfo, "lbNavColumnsInfo");
    this.tablePanelNavColumns.SetColumnSpan((Control) this.lbNavColumnsInfo, 5);
    this.lbNavColumnsInfo.Name = "lbNavColumnsInfo";
    componentResourceManager.ApplyResources((object) this.lbNavColumnsLoad, "lbNavColumnsLoad");
    this.lbNavColumnsLoad.Name = "lbNavColumnsLoad";
    componentResourceManager.ApplyResources((object) this.lbNavColumnsSave, "lbNavColumnsSave");
    this.lbNavColumnsSave.Name = "lbNavColumnsSave";
    componentResourceManager.ApplyResources((object) this.lbNavColumnsReset, "lbNavColumnsReset");
    this.lbNavColumnsReset.Name = "lbNavColumnsReset";
    componentResourceManager.ApplyResources((object) this._closeAllWindowsLinkLabel, "_closeAllWindowsLinkLabel");
    this.tablePanelNavColumns.SetColumnSpan((Control) this._closeAllWindowsLinkLabel, 5);
    this._closeAllWindowsLinkLabel.Name = "_closeAllWindowsLinkLabel";
    this._closeAllWindowsLinkLabel.TabStop = true;
    this._closeAllWindowsLinkLabel.VisitedLinkColor = Color.Blue;
    this._closeAllWindowsLinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.CloseAllWindowsLinkLabel_LinkClicked);
    this._buttonBarsEditorViewTabPage.Controls.Add((Control) this._buttonBarsEditorView);
    componentResourceManager.ApplyResources((object) this._buttonBarsEditorViewTabPage, "_buttonBarsEditorViewTabPage");
    this._buttonBarsEditorViewTabPage.Name = "_buttonBarsEditorViewTabPage";
    this._buttonBarsEditorViewTabPage.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._buttonBarsEditorView, "_buttonBarsEditorView");
    this._buttonBarsEditorView.Name = "_buttonBarsEditorView";
    this._contextMenusTabPage.Controls.Add((Control) this._contextMenusForObjectEditorControl);
    componentResourceManager.ApplyResources((object) this._contextMenusTabPage, "_contextMenusTabPage");
    this._contextMenusTabPage.Name = "_contextMenusTabPage";
    this._contextMenusTabPage.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._contextMenusForObjectEditorControl, "_contextMenusForObjectEditorControl");
    this._contextMenusForObjectEditorControl.Name = "_contextMenusForObjectEditorControl";
    this.imagesTab.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesTab.ImageStream");
    this.imagesTab.TransparentColor = Color.Transparent;
    this.imagesTab.Images.SetKeyName(0, "asterisk.ico");
    this.toolBarTop.AddRemoveButtonsVisible = false;
    this.toolBarTop.AllowHorizontalDock = false;
    this.toolBarTop.DockLine = 3;
    this.toolBarTop.DrawActionsButton = false;
    this.toolBarTop.FullMenus = true;
    this.toolBarTop.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarTop.Hidden = false;
    this.toolBarTop.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this._loadSettingsFormFileButtonItem,
      (ToolbarItemBase) this._saveSettingsToFileButtonItem
    });
    componentResourceManager.ApplyResources((object) this.toolBarTop, "toolBarTop");
    this.toolBarTop.MinimumFloatingSize = new Size(250, 30);
    this.toolBarTop.Name = "toolBarTop";
    this.toolBarTop.Overflow = ToolBarOverflow.Wrap;
    this.toolBarTop.Stretch = true;
    this.toolBarTop.Tearable = false;
    componentResourceManager.ApplyResources((object) this._loadSettingsFormFileButtonItem, "_loadSettingsFormFileButtonItem");
    this._loadSettingsFormFileButtonItem.Click += new EventHandler(this.LoadSettingsFromFileButtonItem_Click);
    componentResourceManager.ApplyResources((object) this._saveSettingsToFileButtonItem, "_saveSettingsToFileButtonItem");
    this._saveSettingsToFileButtonItem.Click += new EventHandler(this.SaveSettingsToFileButtonItem_Click);
    this._settingsOpenFileDialog.DefaultExt = "settings";
    componentResourceManager.ApplyResources((object) this._settingsOpenFileDialog, "_settingsOpenFileDialog");
    this._settingsOpenFileDialog.RestoreDirectory = true;
    this._settingsOpenFileDialog.ShowReadOnly = true;
    this._settingsOpenFileDialog.SupportMultiDottedExtensions = true;
    this._settingsSaveFileDialog.DefaultExt = "settings";
    componentResourceManager.ApplyResources((object) this._settingsSaveFileDialog, "_settingsSaveFileDialog");
    this._settingsSaveFileDialog.RestoreDirectory = true;
    this._settingsSaveFileDialog.SupportMultiDottedExtensions = true;
    this._colorDialog.AnyColor = true;
    this._colorDialog.FullOpen = true;
    this._columnsSettingsOpenFileDialog.DefaultExt = "navcols";
    componentResourceManager.ApplyResources((object) this._columnsSettingsOpenFileDialog, "_columnsSettingsOpenFileDialog");
    this._columnsSettingsOpenFileDialog.RestoreDirectory = true;
    this._columnsSettingsOpenFileDialog.ShowReadOnly = true;
    this._columnsSettingsOpenFileDialog.SupportMultiDottedExtensions = true;
    this._columnsSettingsSaveFileDialog.DefaultExt = "navcols";
    componentResourceManager.ApplyResources((object) this._columnsSettingsSaveFileDialog, "_columnsSettingsSaveFileDialog");
    this._columnsSettingsSaveFileDialog.RestoreDirectory = true;
    this._columnsSettingsSaveFileDialog.SupportMultiDottedExtensions = true;
    this.AcceptButton = (IButtonControl) this._closeButton;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this._closeButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._tabControl);
    this.Controls.Add((Control) this.toolBarTop);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (CustomizationForm);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.HelpButtonClicked += new CancelEventHandler(this.CustomizationForm_HelpButtonClicked);
    this.FormClosed += new FormClosedEventHandler(this.CustomizationForm_FormClosed);
    this.Load += new EventHandler(this.CustomizationForm_Load);
    this.HelpRequested += new HelpEventHandler(this.CustomizationForm_HelpRequested);
    this.panelBottom.ResumeLayout(false);
    this._tabControl.ResumeLayout(false);
    this.page_Toolbars.ResumeLayout(false);
    this._toolbarsTree.EndInit();
    this.page_ContextMenus.ResumeLayout(false);
    ((ISupportInitialize) this._contextMenuEditor).EndInit();
    this.panelMenuCommands.ResumeLayout(false);
    this.page_Views.ResumeLayout(false);
    ((ISupportInitialize) this._viewsGrid).EndInit();
    this.panel1.ResumeLayout(false);
    this.page_Color.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel3.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.page_NavColumns.ResumeLayout(false);
    this.panelNavColumns.ResumeLayout(false);
    this.tablePanelNavColumns.ResumeLayout(false);
    this.tablePanelNavColumns.PerformLayout();
    ((ISupportInitialize) this.pictureReset).EndInit();
    ((ISupportInitialize) this.pictureSave).EndInit();
    ((ISupportInitialize) this.pictureLoad).EndInit();
    this._buttonBarsEditorViewTabPage.ResumeLayout(false);
    this._contextMenusTabPage.ResumeLayout(false);
    this._contextMenusForObjectEditorControl.EndInit();
    this.ResumeLayout(false);
  }

  private sealed class CurrentUserConfigurationSelectedItems : ISelectedItems, ISimpleSelectedItems
  {
    private DBTypedObjectID _currentUserConfigurationTypedObjectID;

    public CurrentUserConfigurationSelectedItems()
    {
      this.FillCurrentUserConfigurationTypedObjectID();
    }

    public long CurrentUserConfigurationVersionID
    {
      get => this._currentUserConfigurationTypedObjectID.ObjectID;
    }

    public bool IsCollage => throw new NotImplementedException();

    public INodeID GetItemID(int index) => throw new NotImplementedException();

    public object GetParentData(int index, System.Type dataFormat)
    {
      throw new NotImplementedException();
    }

    public NodeIDPath GetParentPath(int index) => throw new NotImplementedException();

    public int Count => 1;

    public object GetItemData(int index, System.Type dataFormat)
    {
      return dataFormat == typeof (IDBTypedObjectID) ? (object) this._currentUserConfigurationTypedObjectID : (object) null;
    }

    private void FillCurrentUserConfigurationTypedObjectID()
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams()
        {
          Columns = new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          },
          RecordCount = -1
        };
        long int64Value = DataSetProcessor.GetInt64Value(sessionKeeper.Session.ObjectsSelect(Constants.UserConfigurationObjectTypeGuid, dbRecordSetParams).Rows[0], 0, 0L);
        IDBObject dbObject = sessionKeeper.Session.GetObject(int64Value);
        this._currentUserConfigurationTypedObjectID = new DBTypedObjectID(dbObject.ObjectType, dbObject.ObjectID, dbObject.ID, dbObject.Caption, dbObject.OwnerID, (long) dbObject.VersionID, ObjectHelper.ConvertBooleanToBaseVersionSing(dbObject.IsBaseVersion), dbObject.SiteID, dbObject.ModificationID);
      }
    }
  }
}
