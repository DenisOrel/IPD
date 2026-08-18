// Decompiled with JetBrains decompiler
// Type: Intermech.UI.LinkLabelAdv
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public class LinkLabelAdv : LinkLabel
{
  private LinkBehavior _oldLinkBehavior;

  protected override bool ShowFocusCues => false;

  protected override void OnEnter([NotNull] EventArgs e)
  {
    this._oldLinkBehavior = this.LinkBehavior;
    this.LinkBehavior = LinkBehavior.AlwaysUnderline;
    base.OnEnter(e);
  }

  protected override void OnLeave([NotNull] EventArgs e)
  {
    this.LinkBehavior = this._oldLinkBehavior;
    base.OnLeave(e);
  }
}
