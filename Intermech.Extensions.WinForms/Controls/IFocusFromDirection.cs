// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.IFocusFromDirection
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public interface IFocusFromDirection
{
  [NotNull]
  [ItemNotNull]
  IEnumerable<Control> LeftMostControls { get; }

  [NotNull]
  [ItemNotNull]
  IEnumerable<Control> TopMostControls { get; }

  [NotNull]
  [ItemNotNull]
  IEnumerable<Control> RightMostControls { get; }

  [NotNull]
  [ItemNotNull]
  IEnumerable<Control> BottomMostControls { get; }
}
