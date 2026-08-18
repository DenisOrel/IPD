// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.ChooseFontComboBoxToolbarItem
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Model;

/// <summary>ChooseFontComboBoxToolbarItem</summary>
[ToolboxItem(false)]
[TypeConverter(typeof (ToolbarItemBaseConverter))]
public class ChooseFontComboBoxToolbarItem : ControlContainerItem
{
  private FontComboBox _comboBox;

  /// <summary>ChooseFontComboBoxToolbarItem</summary>
  public ChooseFontComboBoxToolbarItem()
    : base((Control) new FontComboBox())
  {
    this._comboBox = (FontComboBox) this.ContainedControl;
    this._comboBox.TextChanged += new EventHandler(this.ComboBox_TextChanged);
  }

  private void ComboBox_TextChanged(object A_0, EventArgs A_1)
  {
    if (this.ToolBar == null)
      return;
    this.ToolBar.Invalidate(this.ButtonInnerBounds);
  }

  /// <summary>CloneItem</summary>
  /// <returns></returns>
  public override ToolbarItemBase CloneItem()
  {
    ComboBoxItem comboBoxItem = (ComboBoxItem) base.CloneItem();
    comboBoxItem.DropDownStyle = this.DropDownStyle;
    return (ToolbarItemBase) comboBoxItem;
  }

  /// <summary>ComboBox</summary>
  [Browsable(false)]
  public ComboBox ComboBox
  {
    [DebuggerStepThrough] get => (ComboBox) this._comboBox;
  }

  /// <summary>DropDownStyle</summary>
  [DefaultValue(typeof (ComboBoxStyle), "DropDown")]
  [Category("Appearance")]
  [Description("Controls the appearance and functionality of the combo box.")]
  public ComboBoxStyle DropDownStyle
  {
    [DebuggerStepThrough] get => this._comboBox.DropDownStyle;
    set
    {
      this._comboBox.DropDownStyle = value != ComboBoxStyle.Simple ? value : throw new ArgumentException("This style is not supported for a hosted combo box.");
    }
  }

  /// <summary>The items in the combo box</summary>
  [Description("The items in the combo box.")]
  [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, Custom=null", typeof (UITypeEditor))]
  [Category("Data")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
  public ComboBox.ObjectCollection Items
  {
    [DebuggerStepThrough] get => this._comboBox.Items;
  }
}
