// Decompiled with JetBrains decompiler
// Type: Intermech.UI.FlatButton
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.UI;

public class FlatButton : Button
{
  private Color _oldFlatAppearanceBorderColor = Color.Empty;
  private Color _oldBackColor = Color.Empty;

  public FlatButton()
  {
    this.BackColor = Color.FromArgb(253, 253, 253);
    this.FlatAppearance.BorderColor = SystemColors.ControlDark;
    this.FlatAppearance.MouseDownBackColor = Color.Silver;
    this.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 220, 220);
    this.FlatStyle = FlatStyle.Flat;
    this.UseVisualStyleBackColor = false;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(typeof (FlatStyle), "Flat")]
  public new FlatStyle FlatStyle => FlatStyle.Flat;

  protected override void OnEnter([NotNull] EventArgs e)
  {
    if (this.FlatAppearance.MouseOverBackColor != Color.Empty)
    {
      this._oldBackColor = this.BackColor;
      this.BackColor = this.FlatAppearance.MouseOverBackColor;
    }
    this._oldFlatAppearanceBorderColor = this.FlatAppearance.BorderColor;
    this.FlatAppearance.BorderColor = SystemColors.ControlDarkDark;
    base.OnEnter(e);
  }

  protected override void OnLeave([NotNull] EventArgs e)
  {
    if (this._oldBackColor != Color.Empty)
      this.BackColor = this._oldBackColor;
    this.FlatAppearance.BorderColor = this._oldFlatAppearanceBorderColor;
    base.OnLeave(e);
  }

  protected override bool ShowFocusCues => false;
}
