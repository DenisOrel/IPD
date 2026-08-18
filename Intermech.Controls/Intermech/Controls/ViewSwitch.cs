
// Type: Intermech.Controls.ViewSwitch
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using DevExpress.IM.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary> UserControl для переключения видов документа </summary>
public class ViewSwitch : UserControl
{
  internal static Color _defaultActivePageColor = Color.FromArgb((int) byte.MaxValue, 192 /*0xC0*/, 111);
  internal static Color _defaultHlightPageColor = Color.FromArgb((int) byte.MaxValue, 238, 194);
  internal static Color _defaultInactivePageColor = SystemColors.Control;
  private Color _activePageColor = ViewSwitch._defaultActivePageColor;
  private Color _hlightPageColor = ViewSwitch._defaultHlightPageColor;
  private Color _inactivePageColor = ViewSwitch._defaultInactivePageColor;
  private string[] _viewsCaptions = new string[0];
  private string[] _viewsHints = new string[0];
  private ImageList _imageList;
  private int[] _imageIndexes = new int[0];
  private int _activepageIndex = -1;
  private Label[] _labels = new Label[0];
  private Timer _timer;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ToolTipController toolTipController1;

  public ViewSwitch() => this.InitializeComponent();

  public event EventHandler OnActivePageChanged;

  /// <summary> Массив заголовков страниц </summary>
  [Description("Массив заголовков страниц")]
  public string[] ViewsCaptions
  {
    get => this._viewsCaptions;
    set
    {
      this._viewsCaptions = value;
      if (this._activepageIndex >= this._viewsCaptions.Length)
        this._activepageIndex = this._viewsCaptions.Length - 1;
      this.RebuildPages();
      if (this._activepageIndex != -1 || this._viewsCaptions.Length == 0)
        return;
      this.ActivepageIndex = 0;
    }
  }

  /// <summary> Список хинтов соотв. закладок  </summary>
  [Description("Список хинтов соотв. закладок ")]
  public string[] ViewsHints
  {
    get => this._viewsHints;
    set
    {
      this._viewsHints = value;
      this.ReasignPageHints();
    }
  }

  /// <summary> Список иконок для закладок  </summary>
  [Description("Список иконок для закладок ")]
  public ImageList ImageList
  {
    get => this._imageList;
    set
    {
      this._imageList = value;
      this.ReassignImages();
    }
  }

  /// <summary> Номера иконок соотв. закладок  </summary>
  [Description("Номера иконок соотв. закладок")]
  public int[] ImageIndexes
  {
    get => this._imageIndexes;
    set
    {
      this._imageIndexes = value;
      this.ReassignImages();
    }
  }

  /// <summary> Индекс активной в данный момент закладки </summary>
  [Description("Индекс активной в данный момент закладки")]
  public int ActivepageIndex
  {
    get => this._activepageIndex;
    set
    {
      if (value < -1 || value >= this._viewsCaptions.Length)
        value = this._viewsCaptions.Length != 0 ? 0 : -1;
      this._activepageIndex = value;
      this.ReassignPageColors();
      if (this.OnActivePageChanged == null)
        return;
      this.OnActivePageChanged((object) this, new EventArgs());
    }
  }

  /// <summary> Цвет активной в данный момент закладки </summary>
  [Description("Цвет активной в данный момент закладки")]
  public Color ActivePageColor
  {
    get => this._activePageColor;
    set
    {
      this._activePageColor = value;
      this.ReassignPageColors();
    }
  }

  /// <summary> Цвет закладки над которой в данный момент находиться курсор мыши </summary>
  [Description("Цвет закладки над которой в данный момент находиться курсор мыши")]
  public Color HlightPageColor
  {
    get => this._hlightPageColor;
    set
    {
      this._hlightPageColor = value;
      this.ReassignPageColors();
    }
  }

  /// <summary> Цвет закладок </summary>
  [Description("Цвет закладок")]
  public Color InactivePageColor
  {
    get => this._inactivePageColor;
    set
    {
      this._inactivePageColor = value;
      this.ReassignPageColors();
    }
  }

  private void RebuildPages()
  {
    this.SuspendLayout();
    try
    {
      foreach (Label label in this._labels)
      {
        this.Controls.Remove((Control) label);
        label.Dispose();
      }
      this._labels = new Label[this._viewsCaptions.Length];
      for (int index = this._viewsCaptions.Length - 1; index >= 0; --index)
      {
        Label label = new Label();
        label.MouseLeave += new EventHandler(this.Label_MouseLeave);
        label.MouseEnter += new EventHandler(this.Label_MouseEnter);
        label.MouseDown += new MouseEventHandler(this.Label_MouseDown);
        label.MouseUp += new MouseEventHandler(this.Label_MouseUp);
        label.Dock = DockStyle.Left;
        label.ImageAlign = ContentAlignment.MiddleLeft;
        label.Location = new Point(0, 0);
        label.Name = "label" + index.ToString();
        label.Size = new Size(72, this.Height);
        label.AutoSize = true;
        label.TabIndex = index;
        label.Tag = (object) index;
        label.Text = this._viewsCaptions[index];
        label.TextAlign = ContentAlignment.MiddleRight;
        label.UseCompatibleTextRendering = true;
        this._labels[index] = label;
        this.Controls.Add((Control) label);
      }
    }
    finally
    {
      this.ResumeLayout(true);
    }
    this.ReassignImages();
    this.ReasignPageHints();
    this.ReassignPageColors();
  }

  private void ReassignImages()
  {
    int num = 0;
    using (Graphics graphics = Graphics.FromHwnd(this.Handle))
    {
      for (int index = 0; index < this._labels.Length; ++index)
      {
        Label label = this._labels[index];
        Image image = this._imageList == null || index >= this._imageIndexes.Length || this._imageIndexes[index] < 0 || this._imageIndexes[index] >= this._imageList.Images.Count ? (Image) null : this._imageList.Images[this._imageIndexes[index]];
        if (image == null)
        {
          label.Image = (Image) null;
          label.AutoSize = true;
        }
        else
        {
          label.Image = image;
          label.AutoSize = false;
          label.Width = (int) graphics.MeasureString(label.Text, label.Font).Width + image.Width + 5;
        }
        num += label.Width;
      }
    }
    if (!this.AutoSize)
      return;
    this.Width = num;
  }

  private void ReasignPageHints()
  {
    string empty = string.Empty;
    for (int index = 0; index < this._labels.Length; ++index)
      this.toolTipController1.SetToolTip((Control) this._labels[index], index >= this._viewsHints.Length ? string.Empty : this._viewsHints[index]);
  }

  private void ReassignPageColors()
  {
    Control childAtPoint = this.GetChildAtPoint(this.PointToClient(Cursor.Position));
    if (childAtPoint != null && childAtPoint is Label)
      this.CaptureMouseEvents();
    else
      this.ReleaseCapture();
    foreach (Label label in this._labels)
    {
      if (childAtPoint == label)
      {
        label.BackColor = this._hlightPageColor;
        label.BorderStyle = BorderStyle.FixedSingle;
      }
      else if (label.Tag.Equals((object) this._activepageIndex))
      {
        label.BackColor = this._activePageColor;
        label.BorderStyle = BorderStyle.FixedSingle;
      }
      else
      {
        label.BackColor = this._inactivePageColor;
        label.BorderStyle = BorderStyle.None;
      }
    }
  }

  private void CaptureMouseEvents()
  {
    if (this._timer != null)
      return;
    this._timer = new Timer();
    this._timer.Interval = 100;
    this._timer.Tick += new EventHandler(this._timer_Tick);
    this._timer.Enabled = true;
  }

  private void _timer_Tick(object sender, EventArgs e) => this.ReassignPageColors();

  private void ReleaseCapture()
  {
    if (this._timer == null)
      return;
    this._timer.Dispose();
    this._timer = (Timer) null;
  }

  private void Label_MouseUp(object sender, MouseEventArgs e) => this.ReassignPageColors();

  private void Label_MouseDown(object sender, MouseEventArgs e)
  {
    if (sender == null || !(sender is Label))
      return;
    Label label = (Label) sender;
    if (label.Tag == null || !(label.Tag is int))
      return;
    this.ActivepageIndex = (int) label.Tag;
  }

  private void Label_MouseEnter(object sender, EventArgs e) => this.ReassignPageColors();

  private void Label_MouseLeave(object sender, EventArgs e) => this.ReassignPageColors();

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    if (this._timer != null)
    {
      this._timer.Dispose();
      this._timer = (Timer) null;
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.toolTipController1 = new ToolTipController(this.components);
    this.SuspendLayout();
    this.toolTipController1.Style = new ViewStyle("ToolTip style");
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.Margin = new Padding(4, 4, 4, 4);
    this.Name = nameof (ViewSwitch);
    this.Size = new Size(385, 20);
    this.ResumeLayout(false);
  }
}
