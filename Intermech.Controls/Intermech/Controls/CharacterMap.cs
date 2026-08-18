
// Type: Intermech.Controls.CharacterMap
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;


namespace Intermech.Controls;

public class CharacterMap : Control
{
  private CharacterMapGrid characterMapGrid;
  private IContainer components;
  private Label fontLabel;
  private ComboBoxEx fontsListCombo;
  private ImageList fontTypesImageList;

  public event CharacterMap.CharSelectedEventHandler OnCharSelected;

  public CharacterMap()
  {
    this.fontsListCombo = new ComboBoxEx();
    this.fontsListCombo.TabStop = false;
    this.fontLabel = new Label();
    this.fontLabel.AutoSize = true;
    this.fontTypesImageList = new ImageList();
    this.characterMapGrid = new CharacterMapGrid();
    this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
    ResourceManager resourceManager = new ResourceManager(typeof (CharacterMap));
    this.fontTypesImageList.ColorDepth = ColorDepth.Depth4Bit;
    this.fontTypesImageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.fontTypesImageList.ImageStream = (ImageListStreamer) resourceManager.GetObject("fontTypesImageList.ImageStream");
    this.fontTypesImageList.TransparentColor = Color.Fuchsia;
    this.fontsListCombo.ImageList = this.fontTypesImageList;
    this.fontsListCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    foreach (FontFamily family in new InstalledFontCollection().Families)
    {
      int length = family.Name.Length;
      if ((double) length > this.fontsListCombo.max_size)
        this.fontsListCombo.max_size = (double) length;
      this.fontsListCombo.Items.Add((object) new ComboBoxExItem(family.Name, 0));
    }
    this.fontsListCombo.max_size *= 6.0;
    this.Controls.AddRange(new Control[3]
    {
      (Control) this.fontsListCombo,
      (Control) this.fontLabel,
      (Control) this.characterMapGrid
    });
    this.TabStop = false;
    this.fontLabel.TabStop = false;
    this.fontsListCombo.SelectedValueChanged += new EventHandler(this.fontsListCombo_SelectedValueChanged);
    this.characterMapGrid.TabStop = false;
    this.characterMapGrid.OnCharSelected += new CharacterMapGrid.CharSelectedEventHandler(this.characterMapGrid_OnCharSelected);
    this.OnResize(new EventArgs());
  }

  private void characterMapGrid_OnCharSelected(object source, CharacterMap.CharacterMapEventArgs e)
  {
    if (this.OnCharSelected == null)
      return;
    this.OnCharSelected(source, e);
  }

  protected override void Dispose(bool disposing)
  {
  }

  private void fontsListCombo_SelectedValueChanged(object sender, EventArgs e)
  {
    Graphics graphics = (Graphics) null;
    try
    {
      if (this.characterMapGrid.CharFont != null)
        this.characterMapGrid.CharFont.Dispose();
      FontFamily fontFamily = new FontFamily(this.fontsListCombo.SelectedItem.ToString());
      FontStyle style = !fontFamily.IsStyleAvailable(FontStyle.Regular) ? (!fontFamily.IsStyleAvailable(FontStyle.Italic) ? (!fontFamily.IsStyleAvailable(FontStyle.Bold) ? (!fontFamily.IsStyleAvailable(FontStyle.Strikeout) ? FontStyle.Underline : FontStyle.Strikeout) : FontStyle.Bold) : FontStyle.Italic) : FontStyle.Regular;
      Font outFont1;
      CharacterMap.GetItemFont(new Font(this.fontsListCombo.SelectedItem.ToString(), (float) this.characterMapGrid.CellWidth, style, GraphicsUnit.Pixel), graphics, this.characterMapGrid.CellWidth, out outFont1);
      this.characterMapGrid.CharFont = outFont1;
      graphics = this.CreateGraphics();
      NativeWindowMethods.FONTSIGNATURE fs = new NativeWindowMethods.FONTSIGNATURE();
      NativeWindowMethods.NativeGetTextCharsetInfo(graphics, this.characterMapGrid.CharFont, fs);
      if (Environment.OSVersion.Version.Major < 5 || fs.fsUsb[0] == 0 && fs.fsUsb[1] == 0 && fs.fsUsb[2] == 0 && fs.fsUsb[3] == 0)
      {
        NativeWindowMethods.GLYPHSET glyphset = new NativeWindowMethods.GLYPHSET();
        glyphset.header.cRanges = 1;
        glyphset.header.cGlyphsSupported = 223;
        glyphset.ranges = new NativeWindowMethods.WCRANGE[1];
        glyphset.ranges[0].cGlyphs = (short) 223;
        glyphset.ranges[0].wcLow = '!';
        this.characterMapGrid.gs = glyphset;
        if (!this.characterMapGrid.ClientSize.IsEmpty)
          this.characterMapGrid.SetScrollMaximum();
      }
      else
      {
        this.characterMapGrid.gs = NativeWindowMethods.NativeGetFontUnicodeRanges(graphics, this.characterMapGrid.CharFont);
        if (!this.characterMapGrid.ClientSize.IsEmpty)
          this.characterMapGrid.SetScrollMaximum();
      }
      if (this.characterMapGrid.PreviewSymbolFont != null)
        this.characterMapGrid.PreviewSymbolFont.Dispose();
      Font outFont2;
      CharacterMap.GetItemFont(new Font(this.characterMapGrid.CharFont.FontFamily, (float) this.characterMapGrid.PreviewCellWidth, style, GraphicsUnit.Pixel), graphics, this.characterMapGrid.PreviewCellWidth, out outFont2);
      this.characterMapGrid.PreviewSymbolFont = outFont2;
    }
    finally
    {
      this.characterMapGrid.Invalidate();
      graphics.Dispose();
    }
  }

  private static bool GetItemFont(Font itemFont, Graphics gr, int height, out Font outFont)
  {
    bool flag = false;
    float size1 = itemFont.Size;
    FontStyle style = itemFont.Style;
    string name = itemFont.Name;
    while (!flag)
    {
      float size2 = itemFont.Size;
      if (itemFont.Height >= height)
      {
        float emSize = itemFont.Size - 0.5f;
        if ((double) emSize < 1.0)
        {
          emSize = 1f;
          flag = true;
        }
        itemFont?.Dispose();
        itemFont = new Font(name, emSize, style, GraphicsUnit.Pixel);
      }
      else
      {
        flag = true;
        if ((double) size1 != (double) size2)
        {
          for (float emSize = size2; (double) emSize < (double) size2 + 0.5; emSize += 0.05f)
          {
            itemFont?.Dispose();
            itemFont = new Font(itemFont.Name, emSize, style);
            if (itemFont.Height >= height)
            {
              itemFont?.Dispose();
              itemFont = new Font(itemFont.Name, emSize - 0.05f, style, GraphicsUnit.Pixel);
              break;
            }
          }
        }
      }
    }
    outFont = itemFont;
    return true;
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.fontTypesImageList = new ImageList(this.components);
  }

  public void LoadSettings()
  {
    XmlSerializer xmlSerializer = new XmlSerializer(typeof (CharacterMapProperties));
    FileInfo fileInfo = new FileInfo("Settings.xml");
    if (!fileInfo.Exists)
      return;
    FileStream fileStream = fileInfo.OpenRead();
    CharacterMapProperties characterMapProperties = (CharacterMapProperties) xmlSerializer.Deserialize((Stream) fileStream);
    this.CellBackGroundColor = Color.FromArgb(characterMapProperties.CellBackGround);
    this.CellBorderColor = Color.FromArgb(characterMapProperties.CellBorder);
    this.CellBorderWidth = characterMapProperties.CellBorderWidth;
    this.CellSpacing = characterMapProperties.CellSpacing;
    this.CellWidth = characterMapProperties.CellWidh;
    this.CharMapBackGroundColor = Color.FromArgb(characterMapProperties.CharMapBackGround);
    this.GridBackGroundColor = Color.FromArgb(characterMapProperties.GridBackGround);
    this.GridFontColor = Color.FromArgb(characterMapProperties.GridFontColor);
    this.PreviewBackGroundColor = Color.FromArgb(characterMapProperties.PreviewBackGround);
    this.PreviewCellWidth = characterMapProperties.PreviewCellWidth;
    this.PreviewFontColor = Color.FromArgb(characterMapProperties.PreviewFontColor);
    fileStream.Close();
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;
    Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
    base.OnPaint(e);
  }

  protected override void OnResize(EventArgs e)
  {
    this.fontLabel.Location = new Point(9, 11);
    this.fontLabel.Text = "Шрифт:";
    this.fontLabel.AutoSize = true;
    this.fontsListCombo.Location = new Point(this.fontLabel.Size.Width + 7, 7);
    ComboBoxEx fontsListCombo = this.fontsListCombo;
    Size size1 = this.ClientSize;
    int width1 = size1.Width;
    size1 = this.fontLabel.Size;
    int width2 = size1.Width;
    Size size2 = new Size(width1 - width2 - 19, 21);
    fontsListCombo.Size = size2;
    this.fontsListCombo.SelectedIndex = 0;
    this.characterMapGrid.Location = new Point(12, this.fontsListCombo.Size.Height + 15);
    CharacterMapGrid characterMapGrid = this.characterMapGrid;
    Size clientSize = this.ClientSize;
    int width3 = clientSize.Width - 24;
    clientSize = this.ClientSize;
    int height = clientSize.Height - 23;
    Size size3 = new Size(width3, height);
    characterMapGrid.Size = size3;
    this.Invalidate();
    base.OnResize(e);
  }

  public void SerializeObject(string filename)
  {
    XmlSerializer xmlSerializer = new XmlSerializer(typeof (CharacterMapProperties));
    CharacterMapProperties characterMapProperties = new CharacterMapProperties();
    characterMapProperties.CellBackGround = this.CellBackGroundColor.ToArgb();
    characterMapProperties.CellBorder = this.CellBorderColor.ToArgb();
    characterMapProperties.CellBorderWidth = this.CellBorderWidth;
    characterMapProperties.CellSpacing = this.CellSpacing;
    characterMapProperties.CellWidh = this.CellWidth;
    characterMapProperties.CharMapBackGround = this.CharMapBackGroundColor.ToArgb();
    characterMapProperties.GridBackGround = this.GridBackGroundColor.ToArgb();
    characterMapProperties.GridFontColor = this.GridFontColor.ToArgb();
    characterMapProperties.PreviewBackGround = this.PreviewBackGroundColor.ToArgb();
    characterMapProperties.PreviewCellWidth = this.PreviewCellWidth;
    characterMapProperties.PreviewFontColor = this.PreviewFontColor.ToArgb();
    XmlSerializerNamespaces serializerNamespaces = new XmlSerializerNamespaces();
    serializerNamespaces.Add("Settings", "CharacterNavigator");
    XmlWriter xmlWriter1 = (XmlWriter) new XmlTextWriter((Stream) new FileStream(filename, FileMode.Create), (Encoding) new UTF8Encoding());
    XmlWriter xmlWriter2 = xmlWriter1;
    CharacterMapProperties o = characterMapProperties;
    XmlSerializerNamespaces namespaces = serializerNamespaces;
    xmlSerializer.Serialize(xmlWriter2, (object) o, namespaces);
    xmlWriter1.Close();
    this.fontsListCombo.Invalidate();
  }

  [Description("Specifies the cell's color")]
  [Category("CharacterMap")]
  public Color CellBackGroundColor
  {
    get => this.characterMapGrid.CellBackGroundColor;
    set
    {
      this.characterMapGrid.CellBackGroundColor = value;
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  [Category("CharacterMap")]
  [Description("Specifies cell border color")]
  public Color CellBorderColor
  {
    get => this.characterMapGrid.CellBorderColor;
    set
    {
      this.characterMapGrid.CellBorderColor = value;
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  [Description("Specifies the grid's cell border width")]
  [Category("CharacterMap")]
  public int CellBorderWidth
  {
    get => this.characterMapGrid.CellBorderWidth;
    set
    {
      this.characterMapGrid.CellBorderWidth = value;
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  [Category("CharacterMap")]
  [Description("Specifies the amount of space between cells in a grid")]
  public int CellSpacing
  {
    get => this.characterMapGrid.CellSpacing;
    set
    {
      this.characterMapGrid.CellSpacing = value;
      this.characterMapGrid.SetScrollMaximum();
      this.characterMapGrid.InvalidateGrid(true);
    }
  }

  [Category("CharacterMap")]
  [Description("Specifies the grid's cell width")]
  public int CellWidth
  {
    get => this.characterMapGrid.CellWidth;
    set
    {
      this.characterMapGrid.CellWidth = value;
      this.characterMapGrid.CellWidthHalf = value / 2;
      this.fontsListCombo_SelectedValueChanged((object) this, (EventArgs) null);
      this.characterMapGrid.SetScrollMaximum();
      this.characterMapGrid.InvalidateGrid(true);
      this.Invalidate();
    }
  }

  [Description("Specifies character map background color")]
  [Category("CharacterMap")]
  public Color CharMapBackGroundColor
  {
    get => this.BackColor;
    set
    {
      this.BackColor = value;
      this.Invalidate();
    }
  }

  [Category("CharacterMap")]
  [Description("Retrieves the character font")]
  public Font CurrentFont
  {
    get => this.characterMapGrid.CharFont;
    set => this.characterMapGrid.CharFont = value;
  }

  protected override Size DefaultSize => new Size(287, 183);

  [Description("Specifies FontComboStyle")]
  [Category("CharacterMap")]
  internal FontComboStyle FontComboStyle
  {
    get => this.fontsListCombo.FontComboStyle;
    set => this.fontsListCombo.FontComboStyle = value;
  }

  [Category("CharacterMap")]
  [Description("Specifies grid background color")]
  public Color GridBackGroundColor
  {
    get => this.characterMapGrid.BackColor;
    set
    {
      this.characterMapGrid.BackColor = value;
      this.characterMapGrid.Invalidate();
    }
  }

  [Category("CharacterMap")]
  [Description("Specifies grid font color")]
  public Color GridFontColor
  {
    get => this.characterMapGrid.GridFontColor;
    set
    {
      this.characterMapGrid.GridFontColor = value;
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  [Description("Specifies enlarge cell background color")]
  [Category("CharacterMap")]
  public Color PreviewBackGroundColor
  {
    get => this.characterMapGrid.PreviewBackGroundColor;
    set
    {
      this.characterMapGrid.PreviewBackGroundColor = value;
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  [Category("CharacterMap")]
  [Description("Specifies enlarge cell width")]
  public int PreviewCellWidth
  {
    get => this.characterMapGrid.PreviewCellWidth;
    set
    {
      this.characterMapGrid.PreviewCellWidth = value;
      this.characterMapGrid.PreviewCellWidthHalf = value / 2;
      this.fontsListCombo_SelectedValueChanged((object) this, (EventArgs) null);
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  [Category("CharacterMap")]
  [Description("Specifies enlarge symbol color")]
  public Color PreviewFontColor
  {
    get => this.characterMapGrid.PreviewFontColor;
    set
    {
      this.characterMapGrid.PreviewFontColor = value;
      this.characterMapGrid.InvalidateGrid(false);
    }
  }

  public class CharacterMapEventArgs : EventArgs
  {
    private string selectedChar;
    private Font selectedFont;

    public CharacterMapEventArgs(string _selectedChar, Font _selectedFont)
    {
      this.selectedChar = (string) null;
      this.selectedFont = (Font) null;
      this.selectedChar = _selectedChar;
      this.selectedFont = _selectedFont;
    }

    public string SelectedChar => this.selectedChar;

    public Font SelectedFont => this.selectedFont;
  }

  public delegate void CharSelectedEventHandler(object source, CharacterMap.CharacterMapEventArgs e);
}
