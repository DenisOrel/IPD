// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.FontComboBox
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>FontComboBox</summary>
public class FontComboBox : FlatComboBox
{
  private int maxFavourites;
  private ArrayList favourites;
  private string[] nonreadable;
  private Image image;
  private Font defaultFont;
  private StringFormat nonReadableStringFormat;
  private StringFormat standardStringFormat;
  private static FontStyle[] styles = new FontStyle[5]
  {
    FontStyle.Regular,
    FontStyle.Bold,
    FontStyle.Italic,
    FontStyle.Underline,
    FontStyle.Strikeout
  };
  private int _onSelectedLockedCount;

  /// <summary>Конструктор</summary>
  public FontComboBox()
  {
    this.DrawMode = DrawMode.OwnerDrawFixed;
    this.DropDownStyle = ComboBoxStyle.DropDownList;
    this.maxFavourites = 5;
    this.image = (Image) null;
    this.defaultFont = new Font("Tahoma", 8f);
    this.nonReadableStringFormat = new StringFormat();
    this.nonReadableStringFormat.LineAlignment = StringAlignment.Center;
    this.standardStringFormat = new StringFormat();
    this.standardStringFormat.FormatFlags = StringFormatFlags.NoWrap;
    this.favourites = new ArrayList();
    if (!this.DesignMode)
    {
      this.GetFonts(this.CreateGraphics());
      this.favourites.Add((object) "Arial");
      this.Items.Insert(0, (object) this.favourites[0].ToString());
    }
    this.nonreadable = new string[18]
    {
      "CommercialPi BT",
      "GreekC",
      "GreekS",
      "Marlett",
      "Monotype Corsiva",
      "MS Outlook",
      "Nokia PC Composer",
      "UniversalMath1 BT",
      "Symusic",
      "Symeteo",
      "Symbol",
      "Symath",
      "Symap",
      "Syastro",
      "Webdings",
      "Wingdings",
      "Wingdings 2",
      "Wingdings 3"
    };
  }

  /// <summary>Image</summary>
  [DefaultValue(null)]
  public Image Image
  {
    [DebuggerStepThrough] get => this.image;
    set
    {
      this.image = value;
      this.Invalidate();
    }
  }

  /// <summary>NonReadableFonts</summary>
  [Browsable(false)]
  public string[] NonReadableFonts
  {
    [DebuggerStepThrough] get => this.nonreadable;
    set
    {
      this.nonreadable = value;
      this.Invalidate();
    }
  }

  /// <summary>MaximumFavourites</summary>
  [DefaultValue(5)]
  public int MaximumFavourites
  {
    [DebuggerStepThrough] get => this.maxFavourites;
    set
    {
      this.maxFavourites = value;
      this.Invalidate();
    }
  }

  private void GetFonts(Graphics g)
  {
    using (InstalledFontCollection installedFontCollection = new InstalledFontCollection())
    {
      FontFamily[] families = installedFontCollection.Families;
      HashSet<string> stringSet = new HashSet<string>();
      List<object> objectList = new List<object>(families.Length);
      this.BeginUpdate();
      foreach (FontFamily fontFamily in families)
      {
        string str = fontFamily.Name.Trim();
        if (!this.Items.Contains((object) str) && fontFamily.IsStyleAvailable(FontStyle.Regular))
        {
          stringSet.Add(str);
          objectList.Add((object) str);
        }
      }
      bool sorted = this.Sorted;
      try
      {
        this.Items.AddRange(objectList.ToArray());
      }
      finally
      {
        this.Sorted = sorted;
      }
      this.EndUpdate();
    }
  }

  private void DrawSeperator(Graphics g, Rectangle rect)
  {
    Pen pen = new Pen(ControlPaint.LightLight(this.ForeColor));
    g.DrawLine(pen, rect.X + 1, rect.Y + rect.Height - 3, rect.X + rect.Width - 2, rect.Y + rect.Height - 3);
    g.DrawLine(pen, rect.X + 1, rect.Y + rect.Height - 1, rect.X + rect.Width - 2, rect.Y + rect.Height - 1);
  }

  private bool IsNonReadableFont(string FontName)
  {
    foreach (string str in this.nonreadable)
    {
      if (FontName == str)
        return true;
    }
    return false;
  }

  /// <summary>OnMouseWheel</summary>
  /// <param name="e"></param>
  protected override void OnMouseWheel(MouseEventArgs e)
  {
    base.OnMouseWheel(e);
    this.Invalidate(true);
  }

  /// <summary>OnDrawItem</summary>
  /// <param name="e"></param>
  protected override void OnDrawItem(DrawItemEventArgs e)
  {
    if (e.Index < 0)
      return;
    Graphics graphics = e.Graphics;
    string str = this.Items[e.Index].ToString();
    Font font;
    try
    {
      font = new Font(str, this.Font.Size);
    }
    catch
    {
      font = this.defaultFont;
    }
    using (this.Enabled ? new SolidBrush(e.ForeColor) : new SolidBrush(SystemColors.GrayText))
    {
      e.DrawBackground();
      Rectangle rect;
      if (this.image != null)
      {
        Rectangle bounds = e.Bounds;
        int x = bounds.X;
        bounds = e.Bounds;
        int y = bounds.Y;
        int width = this.image.Width;
        int height = this.image.Height;
        rect = new Rectangle(x, y, width, height);
      }
      else
      {
        Rectangle bounds = e.Bounds;
        int x = bounds.X;
        bounds = e.Bounds;
        int y = bounds.Y;
        rect = new Rectangle(x, y, 0, 0);
      }
      if (this.image != null)
        graphics.DrawImage(this.image, rect);
      using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
      {
        using (new SolidBrush(this.BackColor))
        {
          using (FontFamily fontFamily = FontComboBox.CreateFontFamily(str))
          {
            using (new Font(fontFamily, this.Font.Size, FontComboBox.GetFirstAvailableFontStyle(fontFamily)))
              FontComboBox.DrawFontName(e.Graphics, str, font, this.defaultFont, (Brush) solidBrush, e.Bounds, true, false);
          }
        }
      }
    }
    e.DrawFocusRectangle();
    if (this.favourites.Count - 1 != e.Index || !this.DroppedDown)
      return;
    this.DrawSeperator(graphics, e.Bounds);
  }

  private static void DrawFontName(
    Graphics gr,
    string name,
    Font font,
    Font normalFont,
    Brush brush,
    Rectangle bounds,
    bool showPreview,
    bool rightToLeft)
  {
    using (StringFormat format = new StringFormat())
    {
      format.FormatFlags |= StringFormatFlags.NoWrap;
      if (rightToLeft)
        format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      if (font.Height < bounds.Height || font.Height / bounds.Height > 2)
        format.LineAlignment = StringAlignment.Center;
      if (!FontComboBox.IsSymbolFont(font))
      {
        try
        {
          gr.DrawString(name, font, brush, (RectangleF) bounds, format);
        }
        catch
        {
          gr.DrawString(name, normalFont, brush, (RectangleF) bounds, format);
        }
      }
      else
      {
        gr.DrawString(name, normalFont, brush, (RectangleF) bounds, format);
        if (!showPreview)
          return;
        SizeF sizeF = gr.MeasureString(name + "w", normalFont);
        RectangleF layoutRectangle = (RectangleF) bounds;
        layoutRectangle.Offset(rightToLeft ? -sizeF.Width : sizeF.Width, 0.0f);
        gr.DrawString(name, font, brush, layoutRectangle, format);
      }
    }
  }

  private static FontFamily CreateFontFamily(string name)
  {
    try
    {
      return new FontFamily(name);
    }
    catch
    {
      return new FontFamily("Tahoma");
    }
  }

  public static FontStyle GetFirstAvailableFontStyle(FontFamily fontFamily)
  {
    foreach (FontStyle style in FontComboBox.styles)
    {
      if (fontFamily.IsStyleAvailable(style))
        return style;
    }
    return FontStyle.Regular;
  }

  public static bool IsSymbolFont(Font font) => FontComboBox.GetFontCharSet(font) == (byte) 2;

  public static byte GetFontCharSet(Font font)
  {
    FontComboBox.LOGFONT logfont = new FontComboBox.LOGFONT();
    IntPtr hfont = font.ToHfont();
    FontComboBox.GetObject(hfont, Marshal.SizeOf<FontComboBox.LOGFONT>(logfont), logfont);
    FontComboBox.DeleteObject(hfont);
    return logfont.lfCharSet;
  }

  [DllImport("gdi32.dll")]
  internal static extern bool DeleteObject(IntPtr hObject);

  [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
  internal static extern int GetObject(IntPtr hObject, int nSize, [In, Out] FontComboBox.LOGFONT lf);

  /// <summary>IsOnSelectedLocked</summary>
  /// <returns></returns>
  protected bool IsOnSelectedLocked() => this._onSelectedLockedCount > 0;

  /// <summary>LockOnSelected</summary>
  protected void LockOnSelected() => ++this._onSelectedLockedCount;

  /// <summary>UnlockOnSelected</summary>
  protected void UnlockOnSelected()
  {
    if (this._onSelectedLockedCount <= 0)
      return;
    --this._onSelectedLockedCount;
  }

  /// <summary>OnSelectedIndexChanged</summary>
  /// <param name="e"></param>
  protected override void OnSelectedIndexChanged(EventArgs e)
  {
    if (this.IsOnSelectedLocked())
      return;
    base.OnSelectedIndexChanged(e);
    string text = this.Text;
    if (text == "")
      return;
    int index = this.favourites.IndexOf((object) text);
    if (index == -1)
    {
      if (this.maxFavourites > this.favourites.Count)
      {
        this.favourites.Insert(0, (object) text);
        this.Items.Insert(0, (object) text);
      }
      else
      {
        this.favourites.RemoveAt(this.maxFavourites - 1);
        this.favourites.Insert(0, (object) text);
        this.Items.RemoveAt(this.maxFavourites - 1);
        this.Items.Insert(0, (object) text);
      }
    }
    else if (this.favourites.Count > 1)
    {
      this.favourites.RemoveAt(index);
      this.favourites.Insert(0, (object) text);
      this.Items.RemoveAt(index);
      this.Items.Insert(0, (object) text);
      this.LockOnSelected();
      try
      {
        this.SelectedIndex = 0;
      }
      finally
      {
        this.UnlockOnSelected();
      }
    }
    this.EndUpdate();
  }

  /// <summary>OnFontChanged</summary>
  /// <param name="e"></param>
  protected override void OnFontChanged(EventArgs e) => base.OnFontChanged(e);

  /// <summary>
  /// This is a Hack so that the fonts aren't added at design time
  /// </summary>
  public new bool DesignMode
  {
    [DebuggerStepThrough] get => Process.GetCurrentProcess().ProcessName == "devenv";
  }

  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
  public class LOGFONT
  {
    public int lfHeight;
    public int lfWidth;
    public int lfEscapement;
    public int lfOrientation;
    public int lfWeight;
    public byte lfItalic;
    public byte lfUnderline;
    public byte lfStrikeOut;
    public byte lfCharSet;
    public byte lfOutPrecision;
    public byte lfClipPrecision;
    public byte lfQuality;
    public byte lfPitchAndFamily;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32 /*0x20*/)]
    public string lfFaceName;
  }
}
