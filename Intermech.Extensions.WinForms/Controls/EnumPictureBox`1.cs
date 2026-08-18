// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.EnumPictureBox`1
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

[Serializable]
public abstract class EnumPictureBox<TEnum> : PictureBox where TEnum : struct, Enum
{
  [UsedImplicitly]
  [NonSerialized]
  private readonly TEnum _defaultEnumValue;
  private TEnum _enumValue;

  protected EnumPictureBox(TEnum defaultEnumValue = default (TEnum))
  {
    this._enumValue = this._defaultEnumValue = defaultEnumValue;
    this.LoadImage();
    this.SizeMode = PictureBoxSizeMode.AutoSize;
  }

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Behavior")]
  [DefaultValue(typeof (PictureBoxSizeMode), "AutoSize")]
  public new PictureBoxSizeMode SizeMode
  {
    get => base.SizeMode;
    set => base.SizeMode = value;
  }

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Image ErrorImage => (Image) null;

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Image Image => base.Image;

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new string ImageLocation => base.ImageLocation;

  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public new Image InitialImage => base.InitialImage;

  [Browsable(true)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Category("Appearance")]
  [DisplayName("Icon")]
  public TEnum EnumValue
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._enumValue;
    set
    {
      Intermech.Diagnostics.Check.EnumInRange<TEnum>(value, "Icon");
      if (object.Equals((object) value, (object) this._enumValue))
        return;
      this._enumValue = value;
      this.LoadImage();
    }
  }

  public bool ShouldSerializeEnumValue()
  {
    return !object.Equals((object) this._enumValue, (object) this._defaultEnumValue);
  }

  public void ResetEnumValue() => this._enumValue = this._defaultEnumValue;

  [CanBeNull]
  protected abstract object GetPictureByEnumValue(TEnum enumValue);

  private void LoadImage()
  {
    Intermech.Diagnostics.Check.EnumInRange<TEnum>(this._enumValue, "_enumValue");
    switch (this.GetPictureByEnumValue(this._enumValue))
    {
      case null:
        this.Image = (Image) null;
        break;
      case Image image:
        this.Image = image;
        break;
      case Icon icon:
        this.Image = (Image) icon.ToBitmap();
        break;
    }
  }
}
