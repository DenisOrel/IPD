
// Type: Intermech.Expressions.FilterBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Expressions;

public class FilterBox : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Intermech.Bars.ToolBar toolBar1;
  private ComboBoxItem comboBoxItem1;
  private ButtonItem findButton;
  private ButtonItem clearButton;
  private ImageList imageList1;

  public FilterBox() => this.InitializeComponent();

  public event EventHandler Clear;

  public event EventHandler Find;

  [DefaultValue(false)]
  public bool CanClear
  {
    get => this.clearButton.Enabled;
    set => this.clearButton.Enabled = value;
  }

  [Browsable(false)]
  public string FindText
  {
    get => this.comboBoxItem1.ComboBox.Text;
    set => this.comboBoxItem1.ComboBox.Text = value;
  }

  private void findButton_Click(object sender, EventArgs e)
  {
    if (this.Find == null)
      return;
    this.Find((object) this, EventArgs.Empty);
  }

  private void clearButton_Click(object sender, EventArgs e)
  {
    if (this.Clear == null)
      return;
    this.Clear((object) this, EventArgs.Empty);
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    base.OnPaint(e);
    Rectangle clientRectangle = this.ClientRectangle;
    int num = clientRectangle.Bottom - 2;
    e.Graphics.DrawLine(SystemPens.ControlDark, clientRectangle.Left, num, clientRectangle.Right - 1, num);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterBox));
    this.toolBar1 = new Intermech.Bars.ToolBar();
    this.imageList1 = new ImageList();
    this.comboBoxItem1 = new ComboBoxItem();
    this.findButton = new ButtonItem();
    this.clearButton = new ButtonItem();
    this.SuspendLayout();
    this.toolBar1.FullMenus = true;
    this.toolBar1.Guid = new Guid("0012c01b-8488-497d-8cbb-36cf13bbe5d5");
    this.toolBar1.Hidden = false;
    this.toolBar1.ImageList = this.imageList1;
    this.toolBar1.Items.AddRange(new ToolbarItemBase[3]
    {
      (ToolbarItemBase) this.comboBoxItem1,
      (ToolbarItemBase) this.findButton,
      (ToolbarItemBase) this.clearButton
    });
    componentResourceManager.ApplyResources((object) this.toolBar1, "toolBar1");
    this.toolBar1.Name = "toolBar1";
    this.toolBar1.Renderer = (IToolBarRenderer) new WhidbeyRenderer();
    this.toolBar1.StretchItem = (ToolbarItemBase) this.comboBoxItem1;
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Magenta;
    this.imageList1.Images.SetKeyName(0, "Переход-по-указанному-адрес.png");
    this.imageList1.Images.SetKeyName(1, "Очистить-историю-переходов.png");
    this.imageList1.Images.SetKeyName(2, "Search.bmp");
    this.imageList1.Images.SetKeyName(3, "ClearFind.bmp");
    componentResourceManager.ApplyResources((object) this.comboBoxItem1, "comboBoxItem1");
    this.comboBoxItem1.MinimumControlWidth = 50;
    this.comboBoxItem1.Padding.Bottom = 0;
    this.comboBoxItem1.Padding.Left = 1;
    this.comboBoxItem1.Padding.Right = 1;
    this.comboBoxItem1.Padding.Top = 0;
    this.comboBoxItem1.Stretch = true;
    componentResourceManager.ApplyResources((object) this.findButton, "findButton");
    this.findButton.ImageIndex = 0;
    this.findButton.Click += new EventHandler(this.findButton_Click);
    componentResourceManager.ApplyResources((object) this.clearButton, "clearButton");
    this.clearButton.Enabled = false;
    this.clearButton.ImageIndex = 1;
    this.clearButton.Click += new EventHandler(this.clearButton_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.toolBar1);
    this.Name = nameof (FilterBox);
    this.Tag = (object) "  ";
    this.ResumeLayout(false);
  }
}
