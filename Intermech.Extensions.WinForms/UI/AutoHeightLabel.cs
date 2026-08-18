// Decompiled with JetBrains decompiler
// Type: Intermech.UI.AutoHeightLabel
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.UI;

[System.ComponentModel.Designer(typeof (AutoHeightLabel.Designer))]
[Serializable]
public class AutoHeightLabel : SmoothLabel
{
  private bool _lockedAutoSize;
  private Size _lockedMaximumSize = Size.Empty;

  public AutoHeightLabel()
  {
    base.AutoSize = false;
    this.SizeChanged += new EventHandler(this.OnSizeChanged);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.SizeChanged -= new EventHandler(this.OnSizeChanged);
    base.Dispose(disposing);
  }

  protected virtual void OnSizeChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.ForceAutoHeight();
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public override bool AutoSize
  {
    get => this._lockedAutoSize;
    set
    {
    }
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Size Size
  {
    get => base.Size;
    set
    {
      switch (base.Dock)
      {
        case DockStyle.Left:
        case DockStyle.Right:
        case DockStyle.Fill:
          base.Size = value;
          break;
        default:
          this.Width = value.Width;
          break;
      }
    }
  }

  [Category("Layout")]
  [DisplayName("Width")]
  [Description("Width of the label")]
  [Browsable(true)]
  [EditorBrowsable(EditorBrowsableState.Always)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  public new int Width
  {
    get => base.Width;
    set
    {
      if (value == base.Width)
        return;
      Point location = this.Location;
      int x = location.X;
      location = this.Location;
      int y = location.Y;
      int width = value;
      int height = base.Size.Height;
      this.SetBounds(x, y, width, height, BoundsSpecified.Width);
    }
  }

  [Browsable(false)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new int Height
  {
    get => base.Height;
    set
    {
      switch (base.Dock)
      {
        case DockStyle.Left:
        case DockStyle.Right:
        case DockStyle.Fill:
          base.Height = value;
          break;
      }
    }
  }

  public override Size MaximumSize
  {
    get
    {
      if (!this._lockedAutoSize)
        return base.MaximumSize;
      int width = base.MaximumSize.Width;
      return new Size(width > 0 ? Math.Min(width, this.Width) : this.Width, base.MaximumSize.Height);
    }
    set => base.MaximumSize = value;
  }

  protected override void SetBoundsCore(
    int x,
    int y,
    int width,
    int height,
    BoundsSpecified specified)
  {
    if ((specified & BoundsSpecified.Size) != BoundsSpecified.None)
    {
      switch (base.Dock)
      {
        case DockStyle.Left:
        case DockStyle.Right:
        case DockStyle.Fill:
          break;
        default:
          int width1 = this.Width;
          Size maximumSize = this.MaximumSize;
          int height1;
          if (maximumSize.Height <= 0)
          {
            height1 = 9999;
          }
          else
          {
            maximumSize = this.MaximumSize;
            height1 = maximumSize.Height;
          }
          Size preferredSize = this.GetPreferredSize(new Size(width1, height1));
          if (preferredSize.Height > 0)
          {
            height = preferredSize.Height;
            break;
          }
          break;
      }
    }
    base.SetBoundsCore(x, y, width, height, specified);
  }

  private void ForceAutoHeight()
  {
    switch (base.Dock)
    {
      case DockStyle.Left:
        break;
      case DockStyle.Right:
        break;
      case DockStyle.Fill:
        break;
      default:
        Size preferredSize = this.GetPreferredSize(new Size(this.Width, this.MaximumSize.Height > 0 ? this.MaximumSize.Height : 9999));
        if (preferredSize.Height <= 0 || preferredSize.Height == this.Height)
          break;
        base.SetBoundsCore(this.Left, this.Top, this.Width, preferredSize.Height, BoundsSpecified.Height);
        break;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  public override Size GetPreferredSize(Size proposedSize)
  {
    switch (base.Dock)
    {
      case DockStyle.Left:
      case DockStyle.Right:
      case DockStyle.Fill:
        return base.GetPreferredSize(proposedSize);
      default:
        this._lockedAutoSize = true;
        try
        {
          return base.GetPreferredSize(proposedSize);
        }
        finally
        {
          this._lockedAutoSize = false;
        }
    }
  }

  protected override void OnPaddingChanged(EventArgs e)
  {
    base.OnPaddingChanged(e);
    this.ForceAutoHeight();
  }

  protected override void OnTextAlignChanged([NotNull] EventArgs e)
  {
    base.OnTextAlignChanged(e);
    this.ForceAutoHeight();
  }

  protected override void OnMarginChanged([NotNull] EventArgs e)
  {
    base.OnMarginChanged(e);
    this.ForceAutoHeight();
  }

  protected override void OnDpiChangedAfterParent([NotNull] EventArgs e)
  {
    base.OnDpiChangedAfterParent(e);
    this.ForceAutoHeight();
  }

  protected override void OnFontChanged([NotNull] EventArgs e)
  {
    base.OnFontChanged(e);
    this.ForceAutoHeight();
  }

  protected override void OnRightToLeftChanged([NotNull] EventArgs e)
  {
    base.OnRightToLeftChanged(e);
    this.ForceAutoHeight();
  }

  protected override void OnTextChanged([NotNull] EventArgs e)
  {
    base.OnTextChanged(e);
    this.ForceAutoHeight();
  }

  protected override void OnImeModeChanged([NotNull] EventArgs e)
  {
    base.OnImeModeChanged(e);
    this.ForceAutoHeight();
  }

  public override TextRenderingHint TextRenderingHint
  {
    get => base.TextRenderingHint;
    set
    {
      if (base.TextRenderingHint == value)
        return;
      base.TextRenderingHint = value;
      this.ForceAutoHeight();
    }
  }

  public override DockStyle Dock
  {
    get => base.Dock;
    set
    {
      if (base.Dock == value)
        return;
      base.Dock = value;
      this.ForceAutoHeight();
    }
  }

  internal class Designer : ControlDesigner
  {
    public Designer() => this.AutoResizeHandles = true;

    public override SelectionRules SelectionRules
    {
      get => SelectionRules.Moveable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
    }
  }
}
