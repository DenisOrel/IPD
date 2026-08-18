
// Type: Intermech.Navigator.GlobalNode.MainPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.ButtonsPanel;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.NavBars;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Navigator.GlobalNode;

/// <summary>
/// Главная страничка программы "Универсальный клиент InterMech".
/// </summary>
public class MainPage : UserControl, IView
{
  /// <summary>Сервис именованных значков.</summary>
  private INamedImageList _namedImageList;
  private bool _oddPane;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private WebBrowser webBrowser;
  private Panel _pnlLeft;
  private Label label;
  private LinkLabel linkLabel;
  private Panel _pnlRight;
  private TableLayoutPanel _tlp;

  /// <summary>Конструктор.</summary>
  public MainPage() => this.InitializeComponent();

  /// <summary>Инициализировать закладку.</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="provider">Контейнер сервисов</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    INavigationBar service = (INavigationBar) ServicesManager.GetService(typeof (INavigationBar));
    if (service == null)
      return;
    foreach (INavigationPane pane in service.Panes)
      this.CreateGroup(pane);
  }

  /// <summary>Активировать закладку.</summary>
  /// <param name="previousView">Предыдущая закладка</param>
  public void Activate(IView previousView)
  {
  }

  /// <summary>Декативировать закладку.</summary>
  /// <param name="nextView">Следующая закладка</param>
  public void Deactivate(IView nextView)
  {
    this._pnlLeft.Controls.Clear();
    this._pnlRight.Controls.Clear();
  }

  /// <summary>Заголовок закладки.</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_618");

  /// <summary>Индекс значка.</summary>
  public int ImageIndex
  {
    get => this._namedImageList == null ? -1 : this._namedImageList.ImageIndex("imgMainPageIcon");
  }

  /// <summary>Порядковый номер закладки.</summary>
  public int OrderID => 1;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pane"></param>
  private void CreateGroup(INavigationPane pane)
  {
    if (pane == null || !(pane is IAppPane))
      return;
    IAppItem[] items = (pane as IAppPane).GetItems();
    if (items == null || items.Length == 0)
      return;
    XPGroupBox xpGroupBox = new XPGroupBox();
    xpGroupBox.StopUpdate();
    xpGroupBox.Dock = DockStyle.Top;
    xpGroupBox.CaptionText = pane.Text;
    for (int index = items.Length - 1; index > -1; --index)
    {
      if (items[index] is PanelButton panelButton)
      {
        XPGroupItem xpGroupItem = new XPGroupItem();
        xpGroupItem.Text = panelButton.Text;
        xpGroupItem.ImageList = this._namedImageList.ImageList;
        xpGroupItem.ImageIndex = panelButton.ImageIndex;
        xpGroupItem.Visible = panelButton.Visible;
        xpGroupItem.Enabled = panelButton.Enabled;
        xpGroupItem.Hint = panelButton.ToolTipText;
        if (panelButton.GetClickEvent != null)
          xpGroupItem.Click += panelButton.GetClickEvent;
        xpGroupBox.Controls.Add((Control) xpGroupItem);
      }
    }
    xpGroupBox.StartUpdate();
    if (this._oddPane)
    {
      this._pnlLeft.Controls.Add((Control) xpGroupBox);
      this._oddPane = false;
    }
    else
    {
      this._pnlRight.Controls.Add((Control) xpGroupBox);
      this._oddPane = true;
    }
  }

  /// <summary>Покажем форумы.</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    this._pnlLeft.Visible = false;
    this.webBrowser.Url = new Uri("http://mailserver:800/index.php");
    this.webBrowser.Visible = true;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainPage));
    this._pnlLeft = new Panel();
    this.linkLabel = new LinkLabel();
    this.label = new Label();
    this._pnlRight = new Panel();
    this._tlp = new TableLayoutPanel();
    this.webBrowser = new WebBrowser();
    this._pnlLeft.SuspendLayout();
    this._tlp.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._pnlLeft, "_pnlLeft");
    this._pnlLeft.Controls.Add((Control) this.linkLabel);
    this._pnlLeft.Controls.Add((Control) this.label);
    this._pnlLeft.Name = "_pnlLeft";
    componentResourceManager.ApplyResources((object) this.linkLabel, "linkLabel");
    this.linkLabel.Name = "linkLabel";
    this.linkLabel.TabStop = true;
    this.linkLabel.VisitedLinkColor = Color.Blue;
    this.linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
    componentResourceManager.ApplyResources((object) this.label, "label");
    this.label.Name = "label";
    componentResourceManager.ApplyResources((object) this._pnlRight, "_pnlRight");
    this._pnlRight.Name = "_pnlRight";
    componentResourceManager.ApplyResources((object) this._tlp, "_tlp");
    this._tlp.Controls.Add((Control) this._pnlLeft, 1, 1);
    this._tlp.Controls.Add((Control) this._pnlRight, 3, 1);
    this._tlp.Name = "_tlp";
    componentResourceManager.ApplyResources((object) this.webBrowser, "webBrowser");
    this.webBrowser.IsWebBrowserContextMenuEnabled = false;
    this.webBrowser.MinimumSize = new Size(20, 20);
    this.webBrowser.Name = "webBrowser";
    this.webBrowser.ScriptErrorsSuppressed = true;
    this.webBrowser.Url = new Uri("", UriKind.Relative);
    this.webBrowser.WebBrowserShortcutsEnabled = false;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this._tlp);
    this.Controls.Add((Control) this.webBrowser);
    this.Name = nameof (MainPage);
    componentResourceManager.ApplyResources((object) this, "$this");
    this._pnlLeft.ResumeLayout(false);
    this._pnlLeft.PerformLayout();
    this._tlp.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
