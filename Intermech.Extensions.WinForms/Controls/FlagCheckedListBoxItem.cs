// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.FlagCheckedListBoxItem
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Controls;

public class FlagCheckedListBoxItem
{
  public FlagCheckedListBoxItem(int value, [NotNull] string caption)
  {
    this.Value = value;
    this.Caption = caption;
  }

  public override string ToString() => this.Caption;

  public bool IsFlag => (this.Value & this.Value - 1) == 0;

  public bool IsMemberFlag([NotNull] FlagCheckedListBoxItem composite)
  {
    return this.IsFlag && (this.Value & composite.Value) == this.Value;
  }

  public int Value { get; }

  [NotNull]
  public string Caption { get; }
}
