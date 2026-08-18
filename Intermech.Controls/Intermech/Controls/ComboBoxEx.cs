
// Type: Intermech.Controls.ComboBoxEx
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls;

public class ComboBoxEx : ComboBox
{
  private FontComboStyle fontComboStyle;
  private ImageList imageList;
  public double max_size;
  private StringFormat stringFormat;

  public ComboBoxEx()
  {
    this.stringFormat = new StringFormat();
    this.max_size = 0.0;
    this.DrawMode = DrawMode.OwnerDrawFixed;
    this.stringFormat.Alignment = StringAlignment.Near;
    this.stringFormat.LineAlignment = StringAlignment.Near;
    this.SetStyle(ControlStyles.Selectable, false);
  }

  private bool GetItemFont(
    Font itemFont,
    Graphics gr,
    string displayText,
    int height,
    out Font font,
    out int itemWidth)
  {
    bool flag = false;
    itemWidth = 0;
    float sizeInPoints1 = itemFont.SizeInPoints;
    while (!flag)
    {
      Size size = gr.MeasureString(displayText, itemFont).ToSize();
      itemWidth = size.Width;
      float sizeInPoints2 = itemFont.SizeInPoints;
      FontStyle style = itemFont.Style;
      string name = itemFont.Name;
      if (size.Height >= height)
      {
        float emSize = itemFont.SizeInPoints - 0.5f;
        if ((double) emSize < 1.0)
        {
          emSize = 1f;
          flag = true;
        }
        itemFont?.Dispose();
        itemFont = new Font(name, emSize, style);
      }
      else
      {
        flag = true;
        if ((double) sizeInPoints1 != (double) sizeInPoints2)
        {
          for (float emSize = sizeInPoints2; (double) emSize < (double) sizeInPoints2 + 0.5; emSize += 0.05f)
          {
            itemFont?.Dispose();
            itemFont = new Font(itemFont.Name, emSize, style);
            if (gr.MeasureString(displayText, itemFont).ToSize().Height >= height)
            {
              itemFont?.Dispose();
              itemFont = new Font(itemFont.Name, emSize - 0.05f, style);
              break;
            }
          }
        }
      }
    }
    font = itemFont;
    return true;
  }

  protected override void OnDrawItem(DrawItemEventArgs ea)
  {
    ea.DrawBackground();
    Size imageSize = this.imageList.ImageSize;
    Rectangle bounds = ea.Bounds;
    try
    {
      ComboBoxExItem comboBoxExItem = (ComboBoxExItem) this.Items[ea.Index];
      Font font = (Font) null;
      FontStyle style = FontStyle.Regular;
      using (FontFamily fontFamily = new FontFamily(comboBoxExItem.Text))
      {
        if (fontFamily.IsStyleAvailable(FontStyle.Regular))
          style = FontStyle.Regular;
        else if (fontFamily.IsStyleAvailable(FontStyle.Italic))
          style = FontStyle.Italic;
        else if (fontFamily.IsStyleAvailable(FontStyle.Strikeout))
          style = FontStyle.Strikeout;
        else if (fontFamily.IsStyleAvailable(FontStyle.Underline))
          style = FontStyle.Underline;
        else if (fontFamily.IsStyleAvailable(FontStyle.Bold))
          style = FontStyle.Bold;
      }
      if (this.FontComboStyle == FontComboStyle.Standard)
      {
        font = new Font("Arial", 8.25f, style);
      }
      else
      {
        Font itemFont = new Font(comboBoxExItem.Text, (float) bounds.Height, style, GraphicsUnit.Pixel);
        int itemWidth = 0;
        this.GetItemFont(itemFont, ea.Graphics, comboBoxExItem.Text, bounds.Height, out font, out itemWidth);
      }
      if (comboBoxExItem.ImageIndex != -1)
      {
        NativeWindowMethods.LOGFONT logFont = new NativeWindowMethods.LOGFONT(false);
        bool flag = true;
        try
        {
          font.ToLogFont((object) logFont);
          if (logFont.lfCharSet == (byte) 2)
            flag = false;
        }
        catch
        {
        }
        this.imageList.Draw(ea.Graphics, bounds.Left, bounds.Top, comboBoxExItem.ImageIndex);
        if (this.FontComboStyle != FontComboStyle.Selected)
        {
          using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
            ea.Graphics.DrawString(comboBoxExItem.Text, ea.Font, (Brush) solidBrush, (float) (bounds.Left + imageSize.Width), (float) bounds.Top);
        }
        else if (flag)
        {
          Rectangle layoutRectangle = new Rectangle(bounds.Left + imageSize.Width, bounds.Top + bounds.Height / 2 - font.Height / 2, bounds.Width - imageSize.Width, bounds.Height);
          using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
            ea.Graphics.DrawString(comboBoxExItem.Text, font, (Brush) solidBrush, (RectangleF) layoutRectangle, this.stringFormat);
        }
        else
        {
          using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
            ea.Graphics.DrawString(comboBoxExItem.Text, ea.Font, (Brush) solidBrush, (float) (bounds.Left + imageSize.Width), (float) bounds.Top);
        }
        if (this.FontComboStyle == FontComboStyle.Mixed)
        {
          ea.Graphics.MeasureString(comboBoxExItem.Text, font);
          Rectangle layoutRectangle = new Rectangle(bounds.Left + imageSize.Width + (int) this.max_size, bounds.Top + bounds.Height / 2 - font.Height / 2, bounds.Width - (imageSize.Width + (int) this.max_size), bounds.Height);
          using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
            ea.Graphics.DrawString(comboBoxExItem.Text, font, (Brush) solidBrush, (RectangleF) layoutRectangle, this.stringFormat);
        }
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
          ea.Graphics.DrawString(comboBoxExItem.Text, font, (Brush) solidBrush, (float) bounds.Left, (float) bounds.Top);
      }
      font.Dispose();
    }
    catch
    {
      if (ea.Index != -1)
      {
        using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
          ea.Graphics.DrawString(this.Items[ea.Index].ToString(), ea.Font, (Brush) solidBrush, (float) bounds.Left, (float) bounds.Top);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(ea.ForeColor))
          ea.Graphics.DrawString(this.Text, ea.Font, (Brush) solidBrush, (float) bounds.Left, (float) bounds.Top);
      }
    }
    ea.DrawFocusRectangle();
  }

  public FontComboStyle FontComboStyle
  {
    get => this.fontComboStyle;
    set => this.fontComboStyle = value;
  }

  public ImageList ImageList
  {
    get => this.imageList;
    set => this.imageList = value;
  }
}
