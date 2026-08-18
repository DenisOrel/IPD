// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.Win32Window
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public sealed class Win32Window : IWin32Window
{
  [CanBeNull]
  public static IWin32Window Create(IntPtr handle)
  {
    return !(handle != IntPtr.Zero) ? (IWin32Window) null : (IWin32Window) new Win32Window(handle);
  }

  private Win32Window([NotEmpty] IntPtr handle) => this.Handle = handle;

  [NotEmpty]
  public IntPtr Handle { get; }
}
