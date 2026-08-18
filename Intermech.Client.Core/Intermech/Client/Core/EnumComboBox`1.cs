
// Type: Intermech.Client.Core.EnumComboBox`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>настройка изменения толщины линии</summary>
public class EnumComboBox<TEnum> : IDisposable where TEnum : struct, Enum, IConvertible, IComparable, IFormattable
{
  /// <summary>ссылка на ComboBox</summary>
  protected ComboBox _box;
  /// <summary>ссылка на Thickness</summary>
  protected Rclass<TEnum> _enum;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._box != null)
      this._box = (ComboBox) null;
    if (this._enum == null)
      return;
    this._enum.ValueChanged -= new EventHandler<EventArgs<TEnum>>(this.Enum_ValueChanged);
    this._enum = (Rclass<TEnum>) null;
  }

  /// <summary>Инициализация изменения толщины линии</summary>
  /// <param name="varbox">ссылка на ComboBox</param>
  /// <param name="varenum">ссылка на Enum</param>
  public void Initialize(ComboBox varbox, Rclass<TEnum> varenum)
  {
    if (varbox == null)
      throw new ArgumentNullException(nameof (varbox));
    if (varenum == null)
      throw new ArgumentNullException(nameof (varenum));
    this._box = varbox;
    this._enum = varenum;
    this._box.DropDownStyle = ComboBoxStyle.DropDownList;
    this._box.DataSource = (object) Enums.ToList<TEnum>();
    this._box.ValueMember = "Key";
    this._box.DisplayMember = "Value";
    this._box.SelectedItem = (object) this._enum.Value;
    this._box.SelectedIndexChanged += new EventHandler(this.ComboBox_SelectedIndexChanged);
    this._enum.ValueChanged += new EventHandler<EventArgs<TEnum>>(this.Enum_ValueChanged);
    this._box.SelectedValue = (object) this._enum.Value;
    this.UpdateBox(this._enum.Value);
  }

  private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    string text = (sender as ComboBox).Text;
    this._enum.Value = this.UpdateBox((TEnum) this._box.SelectedValue);
  }

  private void Enum_ValueChanged(object sender, EventArgs<TEnum> e) => this.UpdateBox(e.Value);

  private TEnum UpdateBox(TEnum value)
  {
    this._box.Invalidate();
    return value;
  }
}
