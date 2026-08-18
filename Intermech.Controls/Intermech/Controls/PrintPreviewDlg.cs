
// Type: Intermech.Controls.PrintPreviewDlg
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.Bars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>
/// Диалог был перенесен Осипенко А. из сборки Intermech.Document.Model 13.05.10
/// </summary>
public class PrintPreviewDlg : Form
{
  private int _pagesCount;
  private int _curPage;
  private string _zoomValue = string.Empty;
  private bool _printToPreview;
  private List<int> zooms = new List<int>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar ToolBar;
  private ButtonItem _biPrint;
  private ButtonItem _biZoomPlus;
  private ComboBoxItem _cbiZoomValues;
  private ButtonItem _biZoomMinus;
  private ButtonItem _biZoom100;
  private ButtonItem _biFirst;
  private ButtonItem _biPrevious;
  private ComboBoxItem _cbiPages;
  private LabelItem _liPage;
  private ButtonItem _biNext;
  private ButtonItem _biLast;
  private ButtonItem _biOnePage;
  private ButtonItem _biTwoPages;
  private LabelItem _liZoom;
  private PreviewPrintControl _printPreviewCtrl;
  private ButtonItem _biPortrait;
  private ButtonItem _biLandscape;

  /// <summary>Конструктор.</summary>
  public PrintPreviewDlg()
  {
    this.InitializeComponent();
    this._cbiPages.ComboBox.SelectedIndexChanged += new EventHandler(this.PagesComboBox_SelectedIndexChanged);
    this._cbiPages.ComboBox.KeyPress += new KeyPressEventHandler(this.PagesComboBox_KeyPress);
    this._cbiZoomValues.ComboBox.SelectedIndexChanged += new EventHandler(this.ZoomValuesComboBox_SelectedIndexChanged);
    this._cbiZoomValues.ComboBox.KeyPress += new KeyPressEventHandler(this.ZoomValuesComboBox_KeyPress);
    this.zooms.AddRange((IEnumerable<int>) new int[8]
    {
      500,
      200,
      150,
      100,
      75,
      50,
      25,
      10
    });
  }

  /// <summary>
  /// 
  /// </summary>
  public int CurrentPage
  {
    get => this._curPage;
    set
    {
      if (value > 0 && value <= this._pagesCount)
        this._curPage = value;
      this._printPreviewCtrl.StartPage = this._curPage - 1;
      this._cbiPages.ComboBox.SelectedItem = (object) this._curPage;
      this.QueryStatus();
    }
  }

  /// <summary>
  /// 
  /// </summary>
  public PrintDocument Document
  {
    get => this._printPreviewCtrl.Document;
    set
    {
      this._printPreviewCtrl.Document = value;
      this._printPreviewCtrl.Document.DefaultPageSettings.Landscape = false;
      if (value == null)
        return;
      this._printPreviewCtrl.Document.BeginPrint += new PrintEventHandler(this.Document_BeginPrint);
      this._printPreviewCtrl.Document.PrintPage += new PrintPageEventHandler(this.Document_PrintPage);
      this._printPreviewCtrl.Document.EndPrint += new PrintEventHandler(this.Document_EndPrint);
    }
  }

  /// <summary>Видимость кнопки ориентации страницы.</summary>
  /// <remark>У А.Кольцова ориентация страницы определяется автоматически
  /// В IMBASE необходимо руками выставлять ориентацию.
  /// Поэтому по умолчанию кнопка невидима.
  /// Кому понадобится, пусть сам ее делает видимой и использует.</remark>
  public bool PageOrientationVisible
  {
    get => this._biPortrait.Visible && this._biLandscape.Visible;
    set => this._biPortrait.Visible = this._biLandscape.Visible = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Button_Click(object sender, EventArgs e)
  {
    string commandName = (sender as ButtonItem).CommandName;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(commandName))
    {
      case 688467962:
        if (!(commandName == "Portrait"))
          return;
        this._printPreviewCtrl.Document.DefaultPageSettings.Landscape = false;
        this._printPreviewCtrl.InvalidatePreview();
        return;
      case 737929362:
        if (!(commandName == "Landscape"))
          return;
        this._printPreviewCtrl.Document.DefaultPageSettings.Landscape = true;
        this._printPreviewCtrl.InvalidatePreview();
        return;
      case 980318452:
        if (!(commandName == "OnePage"))
          return;
        this._printPreviewCtrl.Columns = 1;
        this.QueryStatus();
        return;
      case 1591809563:
        if (!(commandName == "TwoPages"))
          return;
        this._printPreviewCtrl.Columns = 2;
        this.QueryStatus();
        return;
      case 2563999009:
        if (!(commandName == "Zoom100"))
          return;
        this._cbiZoomValues.ComboBox.SelectedIndex = 3;
        this.QueryStatus();
        return;
      case 3371161582:
        if (!(commandName == "ZoomMinus"))
          return;
        int zoomValue1 = this.GetZoomValue();
        if (zoomValue1 != 0)
        {
          int num = 0;
          foreach (int zoom in this.zooms)
          {
            if (zoomValue1 <= zoom)
              num = this.zooms.IndexOf(zoom) + 1;
          }
          if (num < this.zooms.Count)
            this._cbiZoomValues.ComboBox.SelectedIndex = num;
        }
        this.QueryStatus();
        return;
      case 3406190625:
        if (!(commandName == "PrintDocument"))
          return;
        break;
      case 3702027398:
        if (!(commandName == "ZoomPlus"))
          return;
        int zoomValue2 = this.GetZoomValue();
        if (zoomValue2 != 0)
        {
          int num = -1;
          foreach (int zoom in this.zooms)
          {
            if (zoomValue2 <= zoom)
              num = zoomValue2 >= zoom ? this.zooms.IndexOf(zoom) - 1 : this.zooms.IndexOf(zoom);
          }
          if (num > -1)
            this._cbiZoomValues.ComboBox.SelectedIndex = num;
        }
        this.QueryStatus();
        return;
      case 3705854472:
        if (!(commandName == "Next"))
          return;
        ++this.CurrentPage;
        this.QueryStatus();
        return;
      case 3826132025:
        if (!(commandName == "Last"))
          return;
        this.CurrentPage = this._pagesCount - (this._printPreviewCtrl.Columns - 1);
        this.QueryStatus();
        return;
      case 3895594280:
        if (!(commandName == "Print"))
          return;
        break;
      case 3939629896:
        if (!(commandName == "Previous"))
          return;
        --this.CurrentPage;
        this.QueryStatus();
        return;
      case 3996994017:
        if (!(commandName == "First"))
          return;
        this.CurrentPage = 1;
        this.QueryStatus();
        return;
      default:
        return;
    }
    this.Print();
    this._printPreviewCtrl.CalculatePageInfo(true);
  }

  public virtual void Print()
  {
    PrintDlg printDlg = new PrintDlg();
    this.Document.PrinterSettings.MinimumPage = 1;
    this.Document.PrinterSettings.FromPage = 1;
    this.Document.PrinterSettings.MaximumPage = this._pagesCount;
    this.Document.PrinterSettings.ToPage = this._pagesCount;
    printDlg.PrintDocument = this.Document;
    if (printDlg.ShowDialog() != DialogResult.OK)
      return;
    this.Document.Print();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Document_BeginPrint(object sender, PrintEventArgs e)
  {
    this._printToPreview = e.PrintAction == PrintAction.PrintToPreview;
    if (!this._printToPreview)
      return;
    this._pagesCount = 0;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Document_EndPrint(object sender, PrintEventArgs e)
  {
    this._cbiPages.ComboBox.Items.Clear();
    for (int index = 1; index <= this._pagesCount; ++index)
      this._cbiPages.ComboBox.Items.Add((object) index);
    if (this._pagesCount <= 0)
      return;
    this._cbiPages.ComboBox.SelectedIndex = 0;
    this._printPreviewCtrl.Columns = 1;
    this.setZoom((string) this._cbiZoomValues.ComboBox.Items[this._cbiZoomValues.ComboBox.Items.Count - 1]);
    this.QueryStatus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void Document_PrintPage(object sender, PrintPageEventArgs e)
  {
    if (!this._printToPreview)
      return;
    ++this._pagesCount;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PagesComboBox_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    int result;
    if (int.TryParse(this._cbiPages.ComboBox.Text, out result))
      this.CurrentPage = result;
    else
      this.CurrentPage = this._curPage;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void PagesComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.CurrentPage = (int) this._cbiPages.ComboBox.SelectedItem;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ZoomValuesComboBox_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.setZoom(this._cbiZoomValues.ComboBox.Text);
    this.QueryStatus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ZoomValuesComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.setZoom((string) this._cbiZoomValues.ComboBox.SelectedItem);
    this.QueryStatus();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnClosed(EventArgs e)
  {
    base.OnClosed(e);
    if (this._printPreviewCtrl == null || this._printPreviewCtrl.Document == null)
      return;
    this._printPreviewCtrl.Document.BeginPrint -= new PrintEventHandler(this.Document_BeginPrint);
    this._printPreviewCtrl.Document.PrintPage -= new PrintPageEventHandler(this.Document_PrintPage);
    this._printPreviewCtrl.Document.EndPrint -= new PrintEventHandler(this.Document_EndPrint);
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    switch (keyData)
    {
      case Keys.Prior:
        if (this._biPrevious.Enabled)
          this.Button_Click((object) this._biPrevious, (EventArgs) null);
        return true;
      case Keys.Next:
        if (this._biNext.Enabled)
          this.Button_Click((object) this._biNext, (EventArgs) null);
        return true;
      case Keys.End:
        if (this._biLast.Enabled)
          this.Button_Click((object) this._biLast, (EventArgs) null);
        return true;
      case Keys.Home:
        if (this._biFirst.Enabled)
          this.Button_Click((object) this._biFirst, (EventArgs) null);
        return true;
      default:
        return base.ProcessCmdKey(ref msg, keyData);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  private void QueryStatus()
  {
    if (this._printPreviewCtrl.StartPage != 0)
    {
      this._biFirst.Enabled = true;
      this._biPrevious.Enabled = true;
    }
    else
    {
      this._biFirst.Enabled = false;
      this._biPrevious.Enabled = false;
    }
    if (this._printPreviewCtrl.StartPage < this._pagesCount - 1 - (this._printPreviewCtrl.Columns - 1))
    {
      this._biLast.Enabled = true;
      this._biNext.Enabled = true;
    }
    else
    {
      this._biLast.Enabled = false;
      this._biNext.Enabled = false;
    }
    int zoomValue = this.GetZoomValue();
    if (zoomValue != 0)
    {
      int num = -1;
      foreach (int zoom in this.zooms)
      {
        if (zoomValue <= zoom)
          num = this.zooms.IndexOf(zoom);
      }
      this._biZoomPlus.Enabled = num > 0;
      this._biZoomMinus.Enabled = num < this.zooms.Count - 1;
    }
    else
    {
      this._biZoomPlus.Enabled = false;
      this._biZoomMinus.Enabled = false;
    }
  }

  private int GetZoomValue()
  {
    string s = this._cbiZoomValues.ComboBox.Text;
    if (s.EndsWith("%"))
      s = s.Substring(0, s.Length - 1);
    int result = 0;
    int.TryParse(s, out result);
    return result;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="text"></param>
  private void setZoom(string text)
  {
    if (!(this._zoomValue != text) && !(this._zoomValue != this._cbiZoomValues.ComboBox.Text))
      return;
    string s = text;
    if (s == (string) this._cbiZoomValues.ComboBox.Items[this._cbiZoomValues.ComboBox.Items.Count - 1])
    {
      this._printPreviewCtrl.AutoZoom = true;
      this._zoomValue = s;
      this._cbiZoomValues.ComboBox.Text = this._zoomValue;
    }
    else
    {
      if (string.IsNullOrEmpty(s))
        return;
      this._printPreviewCtrl.AutoZoom = false;
      if (s.EndsWith("%"))
        s = s.Substring(0, s.Length - 1);
      int result;
      if (int.TryParse(s, out result))
      {
        this._printPreviewCtrl.Zoom = (double) result / 100.0;
        this._zoomValue = s;
        this._cbiZoomValues.ComboBox.Text = text;
      }
      else
      {
        if (!(this._zoomValue != s))
          return;
        this.setZoom(this._zoomValue);
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PrintPreviewDlg));
    this.ToolBar = new Intermech.Bars.ToolBar();
    this._biPrint = new ButtonItem();
    this._biPortrait = new ButtonItem();
    this._biLandscape = new ButtonItem();
    this._biOnePage = new ButtonItem();
    this._biTwoPages = new ButtonItem();
    this._biZoomPlus = new ButtonItem();
    this._biZoomMinus = new ButtonItem();
    this._biZoom100 = new ButtonItem();
    this._liZoom = new LabelItem();
    this._cbiZoomValues = new ComboBoxItem();
    this._liPage = new LabelItem();
    this._biFirst = new ButtonItem();
    this._biPrevious = new ButtonItem();
    this._cbiPages = new ComboBoxItem();
    this._biNext = new ButtonItem();
    this._biLast = new ButtonItem();
    this._printPreviewCtrl = new PreviewPrintControl();
    this.SuspendLayout();
    this.ToolBar.DockLine = 1;
    this.ToolBar.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.ToolBar.FullMenus = true;
    this.ToolBar.Guid = new Guid("6cb8f8f2-0dd1-4f8a-b642-ece847e92228");
    this.ToolBar.Hidden = false;
    this.ToolBar.Items.AddRange(new ToolbarItemBase[16 /*0x10*/]
    {
      (ToolbarItemBase) this._biPrint,
      (ToolbarItemBase) this._biPortrait,
      (ToolbarItemBase) this._biLandscape,
      (ToolbarItemBase) this._biOnePage,
      (ToolbarItemBase) this._biTwoPages,
      (ToolbarItemBase) this._biZoomPlus,
      (ToolbarItemBase) this._biZoomMinus,
      (ToolbarItemBase) this._biZoom100,
      (ToolbarItemBase) this._liZoom,
      (ToolbarItemBase) this._cbiZoomValues,
      (ToolbarItemBase) this._liPage,
      (ToolbarItemBase) this._biFirst,
      (ToolbarItemBase) this._biPrevious,
      (ToolbarItemBase) this._cbiPages,
      (ToolbarItemBase) this._biNext,
      (ToolbarItemBase) this._biLast
    });
    this.ToolBar.Location = new Point(0, 0);
    this.ToolBar.Margin = new Padding(10);
    this.ToolBar.MinimumSize = new Size(0, 24);
    this.ToolBar.Name = "ToolBar";
    this.ToolBar.Padding = new Padding(10);
    this.ToolBar.Renderer = (IToolBarRenderer) new Office2002Renderer();
    this.ToolBar.Size = new Size(759, 33);
    this.ToolBar.TabIndex = 2;
    this.ToolBar.Text = "";
    this._biPrint.CommandName = "Print";
    this._biPrint.Icon = (Icon) componentResourceManager.GetObject("_biPrint.Icon");
    this._biPrint.Padding.Bottom = 7;
    this._biPrint.Padding.Left = 5;
    this._biPrint.Padding.Top = 7;
    this._biPrint.ToolTipText = "Печать";
    this._biPrint.Click += new EventHandler(this.Button_Click);
    this._biPortrait.BeginGroup = true;
    this._biPortrait.CommandName = "Portrait";
    this._biPortrait.Icon = (Icon) componentResourceManager.GetObject("_biPortrait.Icon");
    this._biPortrait.ToolTipText = "Книжная";
    this._biPortrait.Visible = false;
    this._biPortrait.Click += new EventHandler(this.Button_Click);
    this._biLandscape.CommandName = "Landscape";
    this._biLandscape.Icon = (Icon) componentResourceManager.GetObject("_biLandscape.Icon");
    this._biLandscape.ToolTipText = "Альбомная";
    this._biLandscape.Visible = false;
    this._biLandscape.Click += new EventHandler(this.Button_Click);
    this._biOnePage.BeginGroup = true;
    this._biOnePage.CommandName = "OnePage";
    this._biOnePage.Icon = (Icon) componentResourceManager.GetObject("_biOnePage.Icon");
    this._biOnePage.ToolTipText = "Одна страница";
    this._biOnePage.Click += new EventHandler(this.Button_Click);
    this._biTwoPages.CommandName = "TwoPages";
    this._biTwoPages.Icon = (Icon) componentResourceManager.GetObject("_biTwoPages.Icon");
    this._biTwoPages.ToolTipText = "Две страницы";
    this._biTwoPages.Click += new EventHandler(this.Button_Click);
    this._biZoomPlus.BeginGroup = true;
    this._biZoomPlus.CommandName = "ZoomPlus";
    this._biZoomPlus.Icon = (Icon) componentResourceManager.GetObject("_biZoomPlus.Icon");
    this._biZoomPlus.ToolTipText = "Увеличить масштаб";
    this._biZoomPlus.Click += new EventHandler(this.Button_Click);
    this._biZoomMinus.CommandName = "ZoomMinus";
    this._biZoomMinus.Icon = (Icon) componentResourceManager.GetObject("_biZoomMinus.Icon");
    this._biZoomMinus.ToolTipText = "Уменьшить масштаб";
    this._biZoomMinus.Click += new EventHandler(this.Button_Click);
    this._biZoom100.CommandName = "Zoom100";
    this._biZoom100.Icon = (Icon) componentResourceManager.GetObject("_biZoom100.Icon");
    this._biZoom100.ToolTipText = "Отображение документа в масштабе 1:1";
    this._biZoom100.Click += new EventHandler(this.Button_Click);
    this._liZoom.CommandName = "labelZoom";
    this._liZoom.Locked = true;
    this._liZoom.Text = "Масштаб";
    this._liZoom.ToolTipText = "Масштаб";
    this._cbiZoomValues.CommandName = "ZoomValues";
    this._cbiZoomValues.Items.AddRange(new object[9]
    {
      (object) "500%",
      (object) "200%",
      (object) "150%",
      (object) "100%",
      (object) "75%",
      (object) "50%",
      (object) "25%",
      (object) "10%",
      (object) "Авто"
    });
    this._cbiZoomValues.MinimumControlWidth = 80 /*0x50*/;
    this._cbiZoomValues.Padding.Bottom = 0;
    this._cbiZoomValues.Padding.Left = 1;
    this._cbiZoomValues.Padding.Right = 1;
    this._cbiZoomValues.Padding.Top = 0;
    this._liPage.BeginGroup = true;
    this._liPage.CommandName = "labelPage";
    this._liPage.Locked = true;
    this._liPage.Text = "Страница :";
    this._liPage.ToolTipText = "Страница";
    this._biFirst.CommandName = "First";
    this._biFirst.Icon = (Icon) componentResourceManager.GetObject("_biFirst.Icon");
    this._biFirst.ToolTipText = "Первая страница";
    this._biFirst.Click += new EventHandler(this.Button_Click);
    this._biPrevious.CommandName = "Previous";
    this._biPrevious.Icon = (Icon) componentResourceManager.GetObject("_biPrevious.Icon");
    this._biPrevious.ToolTipText = "Предыдущая страница";
    this._biPrevious.Click += new EventHandler(this.Button_Click);
    this._cbiPages.CommandName = "comboBoxItem1";
    this._cbiPages.MinimumControlWidth = 50;
    this._cbiPages.Padding.Bottom = 0;
    this._cbiPages.Padding.Left = 1;
    this._cbiPages.Padding.Right = 1;
    this._cbiPages.Padding.Top = 0;
    this._biNext.CommandName = "Next";
    this._biNext.Icon = (Icon) componentResourceManager.GetObject("_biNext.Icon");
    this._biNext.ToolTipText = "Следующая страница";
    this._biNext.Click += new EventHandler(this.Button_Click);
    this._biLast.CommandName = "Last";
    this._biLast.Icon = (Icon) componentResourceManager.GetObject("_biLast.Icon");
    this._biLast.ToolTipText = "Последняя страница";
    this._biLast.Click += new EventHandler(this.Button_Click);
    this._printPreviewCtrl.AutoScroll = true;
    this._printPreviewCtrl.AutoZoom = true;
    this._printPreviewCtrl.BackColor = SystemColors.AppWorkspace;
    this._printPreviewCtrl.Columns = 1;
    this._printPreviewCtrl.Dock = DockStyle.Fill;
    this._printPreviewCtrl.Document = (PrintDocument) null;
    this._printPreviewCtrl.ForeColor = Color.White;
    this._printPreviewCtrl.Location = new Point(0, 33);
    this._printPreviewCtrl.Name = "_printPreviewCtrl";
    this._printPreviewCtrl.Rows = 1;
    this._printPreviewCtrl.Size = new Size(759, 436);
    this._printPreviewCtrl.StartPage = 0;
    this._printPreviewCtrl.TabIndex = 3;
    this._printPreviewCtrl.UseAntiAlias = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(759, 469);
    this.Controls.Add((Control) this._printPreviewCtrl);
    this.Controls.Add((Control) this.ToolBar);
    this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
    this.Name = nameof (PrintPreviewDlg);
    this.ShowInTaskbar = false;
    this.Text = "Предварительный просмотр";
    this.ResumeLayout(false);
  }
}
