// Decompiled with JetBrains decompiler
// Type: Intermech.UI.FlatRadioButton
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

public class FlatRadioButton : RadioButton
{
  private Color _oldFlatAppearanceBorderColor = Color.Empty;
  private Color _oldBackColor = Color.Empty;
  private Color _checkedBorderColor = SystemColors.ControlDarkDark;
  private Color _hoverBorderColor = SystemColors.ControlDark;
  private bool _updatingBorder;

  public FlatRadioButton()
  {
    this.Appearance = Appearance.Button;
    this.FlatAppearance.BorderColor = SystemColors.ControlDarkDark;
    this.FlatAppearance.CheckedBackColor = Color.FromArgb(200, 200, 200);
    this.FlatAppearance.MouseDownBackColor = Color.FromArgb(185, 185, 185);
    this.FlatAppearance.MouseOverBackColor = Color.FromArgb(230, 230, 230);
    this.FlatAppearance.BorderSize = 0;
    this.FlatStyle = FlatStyle.Flat;
    this.UseVisualStyleBackColor = false;
    this.SetStyle(ControlStyles.UserPaint, true);
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
      this.FlatAppearance.BorderSize = 1;
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

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (SystemColors), "ControlDarkDark")]
  public Color CheckedBorderColor
  {
    get => this._checkedBorderColor;
    set
    {
      if (!(this._checkedBorderColor != value))
        return;
      this._checkedBorderColor = value;
      this.Invalidate();
    }
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [DefaultValue(typeof (SystemColors), "ControlDark")]
  public Color HoverBorderColor
  {
    get => this._hoverBorderColor;
    set
    {
      if (!(this._hoverBorderColor != value))
        return;
      this._hoverBorderColor = value;
      this.Invalidate();
    }
  }

  private void UpdateBorder()
  {
    if (this._updatingBorder)
      return;
    this._updatingBorder = true;
    int num = this.Checked || this.Focused || this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)) ? 1 : 0;
    if (num != this.FlatAppearance.BorderSize)
    {
      this.FlatAppearance.BorderSize = num;
      this.FlatAppearance.BorderColor = this.Checked ? this._checkedBorderColor : this._hoverBorderColor;
    }
    this._updatingBorder = false;
  }

  protected override void OnCheckedChanged([NotNull] EventArgs e)
  {
    base.OnCheckedChanged(e);
    this.UpdateBorder();
  }

  protected override bool ShowFocusCues => false;

  protected override void OnPaint([NotNull] PaintEventArgs pEvent)
  {
    this.UpdateBorder();
    base.OnPaint(pEvent);
  }
}
