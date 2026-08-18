// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ContentAlignmentExtensions
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class ContentAlignmentExtensions
{
  private const ContentAlignment AnyRight = ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight;
  private const ContentAlignment AnyBottom = ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight;
  private const ContentAlignment AnyCenter = ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter;
  private const ContentAlignment AnyMiddle = ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static StringAlignment TranslateAlignment(this ContentAlignment align)
  {
    if ((align & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
      return StringAlignment.Far;
    return (align & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0 ? StringAlignment.Center : StringAlignment.Near;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TextFormatFlags TranslateAlignmentForGDI(this ContentAlignment align)
  {
    if ((align & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
      return TextFormatFlags.Bottom;
    return (align & (ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight)) != (ContentAlignment) 0 ? TextFormatFlags.VerticalCenter : TextFormatFlags.Default;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static StringAlignment TranslateLineAlignment(this ContentAlignment align)
  {
    if ((align & (ContentAlignment.BottomLeft | ContentAlignment.BottomCenter | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
      return StringAlignment.Far;
    return (align & (ContentAlignment.MiddleLeft | ContentAlignment.MiddleCenter | ContentAlignment.MiddleRight)) != (ContentAlignment) 0 ? StringAlignment.Center : StringAlignment.Near;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TextFormatFlags TranslateLineAlignmentForGDI(this ContentAlignment align)
  {
    if ((align & (ContentAlignment.TopRight | ContentAlignment.MiddleRight | ContentAlignment.BottomRight)) != (ContentAlignment) 0)
      return TextFormatFlags.Right;
    return (align & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != (ContentAlignment) 0 ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Default;
  }
}
