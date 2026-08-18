// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.LabelExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class LabelExtensions
{
  [NotNull]
  public static StringFormat CreateStringFormat([NotNull] this Label label)
  {
    StringFormat stringFormat = new StringFormat()
    {
      Alignment = label.TextAlign.TranslateAlignment(),
      LineAlignment = label.TextAlign.TranslateLineAlignment()
    };
    if (label.RightToLeft == RightToLeft.Yes)
      stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
    if (label.AutoEllipsis)
    {
      stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      stringFormat.FormatFlags |= StringFormatFlags.LineLimit;
    }
    stringFormat.HotkeyPrefix = label.UseMnemonic ? HotkeyPrefix.Hide : HotkeyPrefix.None;
    if (label.AutoSize)
      stringFormat.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
    return stringFormat;
  }
}
