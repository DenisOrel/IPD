// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Controls
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.WindowsDll;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Extensions;

public static class Controls
{
  [CanBeNull]
  public static Control GetControlUnderMouseCursor()
  {
    IntPtr handle = User32.WindowFromPoint((Interop.POINT) Cursor.Position);
    return !(handle != IntPtr.Zero) ? (Control) null : Control.FromHandle(handle);
  }
}
