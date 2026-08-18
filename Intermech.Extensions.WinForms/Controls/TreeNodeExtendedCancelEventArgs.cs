// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeExtendedCancelEventArgs
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.ComponentModel;

#nullable disable
namespace Intermech.Controls;

public class TreeNodeExtendedCancelEventArgs : CancelEventArgs
{
  public TreeNodeExtendedCancelEventArgs([NotNull] TreeNodeExtendedBase node, bool cancel = false)
    : base(cancel)
  {
    this.Node = node;
  }

  [NotNull]
  public TreeNodeExtendedBase Node { get; }
}
