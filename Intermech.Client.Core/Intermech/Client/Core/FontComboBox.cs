
// Type: Intermech.Client.Core.FontComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Bars;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;


namespace Intermech.Client.Core;

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
  private int _onSelectedLockedCount;

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.nonReadableStringFormat != null)
      {
        this.nonReadableStringFormat.Dispose();
        this.nonReadableStringFormat = (StringFormat) null;
      }
      if (this.standardStringFormat != null)
      {
        this.standardStringFormat.Dispose();
        this.standardStringFormat = (StringFormat) null;
      }
    }
    base.Dispose(disposing);
  }

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
      this.BeginUpdate();
      foreach (FontFamily fontFamily in families)
      {
        string str = fontFamily.Name.Trim();
        if (!this.Items.Contains((object) str) && fontFamily.IsStyleAvailable(FontStyle.Regular))
          this.Items.Add((object) str);
      }
      this.EndUpdate();
    }
  }

  private void DrawSeperator(Graphics g, Rectangle rect)
  {
    using (Pen pen = new Pen(ControlPaint.LightLight(this.ForeColor)))
    {
      g.DrawLine(pen, rect.X + 1, rect.Y + rect.Height - 3, rect.X + rect.Width - 2, rect.Y + rect.Height - 3);
      g.DrawLine(pen, rect.X + 1, rect.Y + rect.Height - 1, rect.X + rect.Width - 2, rect.Y + rect.Height - 1);
    }
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
    using (font)
    {
      using (SolidBrush solidBrush = this.Enabled ? new SolidBrush(e.ForeColor) : new SolidBrush(SystemColors.GrayText))
      {
        e.DrawBackground();
        Rectangle bounds;
        Rectangle rect;
        if (this.image != null)
        {
          bounds = e.Bounds;
          int x = bounds.X;
          bounds = e.Bounds;
          int y = bounds.Y;
          int width = this.image.Width;
          int height = this.image.Height;
          rect = new Rectangle(x, y, width, height);
        }
        else
        {
          bounds = e.Bounds;
          int x = bounds.X;
          bounds = e.Bounds;
          int y = bounds.Y;
          rect = new Rectangle(x, y, 0, 0);
        }
        int x1 = rect.X + rect.Width + 2;
        bounds = e.Bounds;
        int y1 = bounds.Y;
        bounds = e.Bounds;
        int width1 = bounds.Width;
        bounds = e.Bounds;
        int height1 = bounds.Height;
        Rectangle layoutRectangle = new Rectangle(x1, y1, width1, height1);
        if (this.image != null)
          graphics.DrawImage(this.image, rect);
        graphics.DrawString(str, font, (Brush) solidBrush, (RectangleF) layoutRectangle, this.standardStringFormat);
      }
    }
    e.DrawFocusRectangle();
    if (this.favourites.Count - 1 != e.Index || !this.DroppedDown)
      return;
    this.DrawSeperator(graphics, e.Bounds);
  }

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
}
