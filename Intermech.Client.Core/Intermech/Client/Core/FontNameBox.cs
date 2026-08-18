
// Type: Intermech.Client.Core.FontNameBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>настройка изменения имени фонта</summary>
public class FontNameBox : IDisposable
{
  /// <summary>ссылка на ComboBox</summary>
  protected ComboBox _box;
  /// <summary>ссылка на Thickness</summary>
  protected Rclass<string> _name;

  /// <summary>Освободить ресурсы</summary>
  public void Dispose()
  {
    if (this._box != null)
    {
      this._box.SelectedIndexChanged -= new EventHandler(this.box_Validating);
      this._box.Validating -= new CancelEventHandler(this.box_Validating);
      this._box = (ComboBox) null;
    }
    if (this._name == null)
      return;
    this._name.ValueChanged -= new EventHandler<EventArgs<string>>(this.box_Validating);
    this._name = (Rclass<string>) null;
  }

  /// <summary>Инициализация изменения </summary>
  /// <param name="varbox">ссылка на ComboBox</param>
  /// <param name="varName">ссылка на FontName</param>
  public void Initialize(ComboBox varbox, Rclass<string> varName)
  {
    if (varbox == null)
      throw new ArgumentNullException(nameof (varbox));
    if (varName == null)
      throw new ArgumentNullException(nameof (varName));
    this._box = varbox;
    this._name = varName;
    this._box.TabStop = false;
    this._box.Enabled = true;
    this._box.SelectedIndex = -1;
    this._box.SelectedIndexChanged += new EventHandler(this.box_Validating);
    this._box.Validating += new CancelEventHandler(this.box_Validating);
    this._name.ValueChanged += new EventHandler<EventArgs<string>>(this.name_ValueChanged);
    this._box.SelectedValue = (object) this._name.Value;
    this.UpdateBox(this._name.Value);
  }

  private void name_ValueChanged(object sender, EventArgs<string> e) => this.UpdateBox(e.Value);

  private string UpdateBox(string name)
  {
    int num = this._box.Items.IndexOf((object) name);
    if (num == -1)
      num = num;
    if (this._box.SelectedIndex != num)
      this._box.SelectedIndex = num;
    this._box.Text = name;
    return name;
  }

  private void box_Validating(object sender, EventArgs e)
  {
    this._name.Value = (sender as ComboBox).Text;
  }
}
