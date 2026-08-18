// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeExtendedEventArgs
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Controls;

public class TreeNodeExtendedEventArgs : EventArgs
{
  public TreeNodeExtendedEventArgs([NotNull] TreeNodeExtendedBase node) => this.Node = node;

  [NotNull]
  public TreeNodeExtendedBase Node { get; }
}
