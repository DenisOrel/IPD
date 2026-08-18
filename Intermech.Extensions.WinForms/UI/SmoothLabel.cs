// Decompiled with JetBrains decompiler
// Type: Intermech.UI.SmoothLabel
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

[Serializable]
public class SmoothLabel : Label
{
  private TextRenderingHint _hint = TextRenderingHint.AntiAlias;

  [Category("Appearance")]
  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (TextRenderingHint), "AntiAlias")]
  public virtual TextRenderingHint TextRenderingHint
  {
    get => this._hint;
    set
    {
      if (this._hint == value)
        return;
      this._hint = value;
      if (!(this.Handle != IntPtr.Zero) || !this.Visible)
        return;
      this.Refresh();
    }
  }

  protected override void OnPaint(PaintEventArgs pe)
  {
    if (pe.Graphics.TextRenderingHint == this.TextRenderingHint)
    {
      base.OnPaint(pe);
    }
    else
    {
      TextRenderingHint textRenderingHint = pe.Graphics.TextRenderingHint;
      pe.Graphics.TextRenderingHint = this.TextRenderingHint;
      try
      {
        base.OnPaint(pe);
      }
      finally
      {
        pe.Graphics.TextRenderingHint = textRenderingHint;
      }
    }
  }

  [NotNull]
  protected virtual StringFormat CreateStringFormat()
  {
    StringFormat stringFormat = LabelExtensions.CreateStringFormat(this);
    if (stringFormat.HotkeyPrefix == HotkeyPrefix.Hide && this.ShowKeyboardCues)
      stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
    return stringFormat;
  }

  protected Size GetBordersAndPadding()
  {
    Size size = this.Padding.Size;
    if (this.UseCompatibleTextRendering)
    {
      if (this.BorderStyle != BorderStyle.None)
      {
        size.Height += 6;
        size.Width += 2;
      }
      else
        size.Height += 3;
    }
    else
    {
      size += this.SizeFromClientSize(Size.Empty);
      if (this.BorderStyle == BorderStyle.Fixed3D)
        size += new Size(2, 2);
    }
    return size;
  }
}
