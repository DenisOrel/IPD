// Decompiled with JetBrains decompiler
// Type: Intermech.UI.Winforms.ShowMenuStripEventArgs
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.UI.Winforms;

[ComVisible(true)]
[Serializable]
public class ShowMenuStripEventArgs : EventArgs
{
  public bool Handled;

  [NotNull]
  public static ShowMenuStripEventArgs Empty { [DebuggerStepThrough] get; } = new ShowMenuStripEventArgs();
}
