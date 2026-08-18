
// Type: MWControls.MWLabel
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using MWCommon;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace MWControls;

/// <summary>
/// A proper Label Control at last...
/// Text uses StringFormat.GenericTypographic and thus fills the client area properly.
/// Images are placed at the edge of the Control - not some weird arbitrary distance from it (on my system 1 pixel from left
/// 	and top and 4 pixels from right and bottom for a normal Label Control).
/// When Control has Enabled set to false the Text looks exactly like that of a CheckBox.
/// Mnemonics are not implemented.
/// </summary>
public class MWLabel : Label
{
  private StringFormat strfmt = StringFormat.GenericTypographic;
  private StringFormatEnum sfe = StringFormatEnum.GenericTypographic;
  private bool bImageOverText;
  private TextDir tdTextDir;
  private System.ComponentModel.Container components;

  /// <summary>Standard Constructor.</summary>
  public MWLabel()
  {
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.DoubleBuffer, true);
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.Selectable, true);
    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.InitializeComponent();
  }

  /// <summary>Clean up any resources being used.</summary>
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
  private void InitializeComponent() => this.components = new System.ComponentModel.Container();

  /// <summary>
  /// Overridden OnPaint EventHandler that draws the Text and the Image.
  /// </summary>
  /// <param name="e">Standard PaintEventArgs object.</param>
  protected override void OnPaint(PaintEventArgs e)
  {
    if (this.bImageOverText)
    {
      this.PaintText(e.Graphics);
      this.PaintImage(e.Graphics);
    }
    else
    {
      this.PaintImage(e.Graphics);
      this.PaintText(e.Graphics);
    }
  }

  /// <summary>
  /// Gets the Graphics object for this Control and paints the Image and the Text.
  /// </summary>
  private void PaintAll()
  {
    Graphics graphics = this.CreateGraphics();
    graphics.Clear(this.BackColor);
    if (this.bImageOverText)
    {
      this.PaintText(graphics);
      this.PaintImage(graphics);
    }
    else
    {
      this.PaintImage(graphics);
      this.PaintText(graphics);
    }
  }

  private void PaintImage(Graphics g)
  {
    Image image = (Image) null;
    ContentAlignment contentAlignment = this.ImageAlign;
    if (this.RightToLeft == RightToLeft.Yes)
    {
      switch (contentAlignment)
      {
        case ContentAlignment.TopLeft:
          contentAlignment = ContentAlignment.TopRight;
          break;
        case ContentAlignment.TopRight:
          contentAlignment = ContentAlignment.TopLeft;
          break;
        case ContentAlignment.MiddleLeft:
          contentAlignment = ContentAlignment.MiddleRight;
          break;
        case ContentAlignment.MiddleRight:
          contentAlignment = ContentAlignment.MiddleLeft;
          break;
        case ContentAlignment.BottomLeft:
          contentAlignment = ContentAlignment.BottomRight;
          break;
        case ContentAlignment.BottomRight:
          contentAlignment = ContentAlignment.BottomLeft;
          break;
      }
    }
    if (this.Image != null)
    {
      try
      {
        image = this.Image;
      }
      catch
      {
      }
    }
    else if (this.ImageList != null)
    {
      try
      {
        image = this.ImageList.Images[this.ImageIndex];
      }
      catch
      {
      }
    }
    if (image == null)
      return;
    switch (contentAlignment)
    {
      case ContentAlignment.TopLeft:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, 0, 0);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, 0, 0, this.BackColor);
        break;
      case ContentAlignment.TopCenter:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, (this.Width - image.Width) / 2, 0);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, (this.Width - image.Width) / 2, 0, this.BackColor);
        break;
      case ContentAlignment.TopRight:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, this.Width - image.Width, 0);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, this.Width - image.Width, 0, this.BackColor);
        break;
      case ContentAlignment.MiddleLeft:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, 0, (this.Height - image.Height) / 2);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, 0, (this.Height - image.Height) / 2, this.BackColor);
        break;
      case ContentAlignment.MiddleCenter:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, (this.Width - image.Width) / 2, (this.Height - image.Height) / 2);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, (this.Width - image.Width) / 2, (this.Height - image.Height) / 2, this.BackColor);
        break;
      case ContentAlignment.MiddleRight:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, this.Width - image.Width, (this.Height - image.Height) / 2);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, this.Width - image.Width, (this.Height - image.Height) / 2, this.BackColor);
        break;
      case ContentAlignment.BottomLeft:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, 0, this.Height - image.Height);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, 0, this.Height - image.Height, this.BackColor);
        break;
      case ContentAlignment.BottomCenter:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, (this.Width - image.Width) / 2, this.Height - image.Height);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, (this.Width - image.Width) / 2, this.Height - image.Height, this.BackColor);
        break;
      case ContentAlignment.BottomRight:
        if (this.Enabled)
        {
          g.DrawImageUnscaled(image, this.Width - image.Width, this.Height - image.Height);
          break;
        }
        ControlPaint.DrawImageDisabled(g, image, this.Width - image.Width, this.Height - image.Height, this.BackColor);
        break;
    }
  }

  private void PaintText(Graphics g)
  {
    this.SetStringFormat();
    if (this.tdTextDir == TextDir.UpsideDown)
    {
      g.RotateTransform(180f);
      g.TranslateTransform((float) -this.ClientRectangle.Width, (float) -this.ClientRectangle.Height);
    }
    else if (this.tdTextDir == TextDir.Left)
    {
      g.RotateTransform(270f);
      g.TranslateTransform((float) -this.ClientRectangle.Height, 0.0f);
    }
    else if (this.tdTextDir == TextDir.Right)
    {
      g.RotateTransform(90f);
      g.TranslateTransform(0.0f, (float) -this.ClientRectangle.Width);
    }
    if (this.Enabled)
    {
      using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        g.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) this.GetModifiedClientRectangle(), this.strfmt);
    }
    else
    {
      Rectangle modifiedClientRectangle1 = this.GetModifiedClientRectangle(true);
      Rectangle modifiedClientRectangle2 = this.GetModifiedClientRectangle(false);
      using (SolidBrush solidBrush = new SolidBrush(ControlPaint.LightLight(this.BackColor)))
        g.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) modifiedClientRectangle1, this.strfmt);
      if (this.BackColor == SystemColors.Control)
      {
        using (SolidBrush solidBrush = new SolidBrush(ControlPaint.ContrastControlDark))
          g.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) modifiedClientRectangle2, this.strfmt);
      }
      else
      {
        using (SolidBrush solidBrush = new SolidBrush(ControlPaint.Dark(this.BackColor)))
          g.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) modifiedClientRectangle2, this.strfmt);
      }
    }
    g.ResetTransform();
  }

  /// <summary>Gets the Rectangle needed to draw the Text correctly.</summary>
  /// <returns>Rectangle needed to draw the Text correctly.</returns>
  private Rectangle GetModifiedClientRectangle()
  {
    if (this.tdTextDir == TextDir.Normal || this.tdTextDir == TextDir.UpsideDown)
      return this.ClientRectangle;
    int y = this.ClientRectangle.Y;
    int x = this.ClientRectangle.X;
    Rectangle clientRectangle = this.ClientRectangle;
    int height = clientRectangle.Height;
    clientRectangle = this.ClientRectangle;
    int width = clientRectangle.Width;
    return new Rectangle(y, x, height, width);
  }

  /// <summary>
  /// Gets the Rectangle needed to draw the Text correctly modified by a certain number of pixels.
  /// Used for drawing disabled/embossed Text (this.Enabled = false).
  /// </summary>
  /// <param name="bTop">True if embossed text (top left) or false for highlighted text (bottom right).</param>
  /// <returns>Rectangle needed to draw the Text correctly.</returns>
  private Rectangle GetModifiedClientRectangle(bool bTop)
  {
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    if (this.strfmt.Alignment == StringAlignment.Near)
    {
      num1 = 1;
      num2 = 0;
    }
    else if (this.strfmt.Alignment == StringAlignment.Center)
    {
      num1 = 1;
      num2 = 1;
    }
    else if (this.strfmt.Alignment == StringAlignment.Far)
    {
      num1 = 2;
      num2 = 1;
    }
    if (this.strfmt.LineAlignment == StringAlignment.Near)
    {
      num3 = 1;
      num4 = 0;
    }
    else if (this.strfmt.LineAlignment == StringAlignment.Center)
    {
      num3 = 2;
      num4 = 0;
    }
    else if (this.strfmt.LineAlignment == StringAlignment.Far)
    {
      num3 = 2;
      num4 = 1;
    }
    Rectangle rectangle = this.tdTextDir == TextDir.Normal || this.tdTextDir == TextDir.UpsideDown ? this.ClientRectangle : new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height, this.ClientRectangle.Width);
    return bTop ? new Rectangle(rectangle.X + num1, rectangle.Y + num3, rectangle.Width - 1, rectangle.Height - 1) : new Rectangle(rectangle.X - num2, rectangle.Y - num4, rectangle.Width + 1, rectangle.Height + 1);
  }

  /// <summary>
  /// Sets the StringFormat so that it can be used throughout the Class.
  /// </summary>
  private void SetStringFormat()
  {
    this.strfmt = new StringFormat(StringFormat.GenericTypographic);
    if (this.sfe == StringFormatEnum.GenericDefault)
      this.strfmt = new StringFormat(StringFormat.GenericDefault);
    ContentAlignment contentAlignment = this.TextAlign;
    if (this.RightToLeft == RightToLeft.Yes)
    {
      if (contentAlignment == ContentAlignment.BottomLeft)
        contentAlignment = ContentAlignment.BottomRight;
      else if (contentAlignment == ContentAlignment.BottomRight)
        contentAlignment = ContentAlignment.BottomLeft;
      else if (contentAlignment == ContentAlignment.MiddleLeft)
        contentAlignment = ContentAlignment.MiddleRight;
      else if (contentAlignment == ContentAlignment.MiddleRight)
        contentAlignment = ContentAlignment.MiddleLeft;
      else if (contentAlignment == ContentAlignment.TopLeft)
        contentAlignment = ContentAlignment.TopRight;
      else if (contentAlignment == ContentAlignment.TopRight)
        contentAlignment = ContentAlignment.TopLeft;
    }
    if (contentAlignment <= ContentAlignment.MiddleCenter)
    {
      switch (contentAlignment - 1)
      {
        case (ContentAlignment) 0:
          this.strfmt.Alignment = StringAlignment.Near;
          this.strfmt.LineAlignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopLeft:
          this.strfmt.Alignment = StringAlignment.Center;
          this.strfmt.LineAlignment = StringAlignment.Near;
          break;
        case ContentAlignment.TopCenter:
          break;
        case ContentAlignment.TopLeft | ContentAlignment.TopCenter:
          this.strfmt.Alignment = StringAlignment.Far;
          this.strfmt.LineAlignment = StringAlignment.Near;
          break;
        default:
          if (contentAlignment != ContentAlignment.MiddleLeft)
          {
            if (contentAlignment != ContentAlignment.MiddleCenter)
              break;
            this.strfmt.Alignment = StringAlignment.Center;
            this.strfmt.LineAlignment = StringAlignment.Center;
            break;
          }
          this.strfmt.Alignment = StringAlignment.Near;
          this.strfmt.LineAlignment = StringAlignment.Center;
          break;
      }
    }
    else if (contentAlignment <= ContentAlignment.BottomLeft)
    {
      if (contentAlignment != ContentAlignment.MiddleRight)
      {
        if (contentAlignment != ContentAlignment.BottomLeft)
          return;
        this.strfmt.Alignment = StringAlignment.Near;
        this.strfmt.LineAlignment = StringAlignment.Far;
      }
      else
      {
        this.strfmt.Alignment = StringAlignment.Far;
        this.strfmt.LineAlignment = StringAlignment.Center;
      }
    }
    else if (contentAlignment != ContentAlignment.BottomCenter)
    {
      if (contentAlignment != ContentAlignment.BottomRight)
        return;
      this.strfmt.Alignment = StringAlignment.Far;
      this.strfmt.LineAlignment = StringAlignment.Far;
    }
    else
    {
      this.strfmt.Alignment = StringAlignment.Center;
      this.strfmt.LineAlignment = StringAlignment.Far;
    }
  }

  /// <summary>
  /// Decides whether the Image should be painted above the Text or not.
  /// </summary>
  [Browsable(true)]
  [Category("Appearance")]
  [Description("True if the Image should be painted above the Text.")]
  [DefaultValue(false)]
  public bool ImageOverText
  {
    get => this.bImageOverText;
    set
    {
      if (this.bImageOverText == value)
        return;
      this.bImageOverText = value;
      this.OnImageOverTextChanged(new EventArgs());
      this.PaintAll();
    }
  }

  /// <summary>Occurs when the ImageOverText property changes.</summary>
  [Browsable(true)]
  [Category("Appearance")]
  [Description("Occurs when the ImageOverText property changes.")]
  public event EventHandler ImageOverTextChanged;

  /// <summary>Raises the ImageOverTextChanged Event.</summary>
  /// <param name="e">Standard EventArgs object.</param>
  public virtual void OnImageOverTextChanged(EventArgs e)
  {
    if (this.ImageOverTextChanged == null)
      return;
    this.ImageOverTextChanged((object) this, e);
  }

  /// <summary>Decides which direction the Text should be painted.</summary>
  [Browsable(true)]
  [Category("Appearance")]
  [Description("Direction of the Text.")]
  [DefaultValue(TextDir.Normal)]
  [Editor(typeof (EditorTextDir), typeof (UITypeEditor))]
  public TextDir TextDir
  {
    get => this.tdTextDir;
    set
    {
      TextDir tdTextDir = this.tdTextDir;
      if (this.tdTextDir == value)
        return;
      this.tdTextDir = value;
      this.OnTextDirChanged(new TextDirEventArgs(tdTextDir, this.tdTextDir));
      this.PaintAll();
    }
  }

  /// <summary>Occurs when the TextDir property changes.</summary>
  [Browsable(true)]
  [Category("Appearance")]
  [Description("Occurs when the TextDir property changes.")]
  public event MWCommon.TextDirEventHandler TextDirChanged;

  /// <summary>Raises the TextDirChanged Event.</summary>
  /// <param name="e">Standard TextDirEventArgs object.</param>
  public virtual void OnTextDirChanged(TextDirEventArgs e)
  {
    if (this.TextDirChanged == null)
      return;
    this.TextDirChanged((object) this, e);
  }

  /// <summary>Decides which StringFormatEnum the Text should use.</summary>
  [Browsable(true)]
  [Category("Appearance")]
  [Description("StringFormatEnum of the Text.")]
  public StringFormatEnum StringFrmt
  {
    get => this.sfe;
    set
    {
      StringFormatEnum sfe = this.sfe;
      if (this.sfe == value)
        return;
      this.sfe = value;
      this.OnStringFrmtChanged(new StringFormatEnumEventArgs(sfe, this.sfe));
      this.PaintAll();
    }
  }

  /// <summary>Occurs when the StringFrmt property changes.</summary>
  [Browsable(true)]
  [Category("Appearance")]
  [Description("Occurs when the StringFrmt property changes.")]
  public event MWCommon.StringFormatEnumEventHandler StringFrmtChanged;

  /// <summary>Raises the StringFrmtChanged Event.</summary>
  /// <param name="e">Standard StringFormatEnumEventArgs object.</param>
  public virtual void OnStringFrmtChanged(StringFormatEnumEventArgs e)
  {
    if (this.StringFrmtChanged == null)
      return;
    this.StringFrmtChanged((object) this, e);
  }

  /// <summary>A delegate for event TextDirEventHandler.</summary>
  public delegate void TextDirEventHandler(object sender, TextDirEventArgs e);

  /// <summary>A delegate for event StringFormatEnumEventHandler.</summary>
  public delegate void StringFormatEnumEventHandler(object sender, StringFormatEnumEventArgs e);
}
