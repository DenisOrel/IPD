
// Type: Intermech.Bars.ComboBoxItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;


namespace Intermech.Bars
{
    [ToolboxItem(false)]
    [TypeConverter(typeof (ToolbarItemBaseConverter))]
    public class ComboBoxItem : ControlContainerItem
    {
      private FlatComboBox _comboBox;
      private StringFormat _stringFormat;

      public event EventHandler SelectedValueChanged;

      public ComboBoxItem()
        : base((Control) new FlatComboBox())
      {
        this._comboBox = (FlatComboBox) this.ContainedControl;
        this._comboBox.TextChanged += new EventHandler(this.ComboBox_TextChanged);
        this._comboBox.DefaultTextChanged += new EventHandler(this.ComboBox_TextChanged);
        this._comboBox.SelectedValueChanged += new EventHandler(this.ComboBox_SelectedValueChanged);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          this._comboBox.TextChanged -= new EventHandler(this.ComboBox_TextChanged);
          this._comboBox.DefaultTextChanged -= new EventHandler(this.ComboBox_TextChanged);
          this._comboBox = (FlatComboBox) null;
          if (this._stringFormat != null)
            this._stringFormat.Dispose();
        }
        base.Dispose(disposing);
      }

      private void ComboBox_TextChanged(object A_0, EventArgs A_1)
      {
        if (this.ToolBar == null || !this.ToolBar.FriendDesignMode)
          return;
        this.ToolBar.Invalidate(this.ButtonInnerBounds);
      }

      public override ToolbarItemBase CloneItem()
      {
        ComboBoxItem comboBoxItem = (ComboBoxItem) base.CloneItem();
        comboBoxItem.DefaultText = this.DefaultText;
        comboBoxItem.DropDownStyle = this.DropDownStyle;
        return (ToolbarItemBase) comboBoxItem;
      }

      protected internal override void DrawDesignTimeControl(
        IToolBarRenderer renderer,
        Graphics graphics,
        DrawItemState state)
      {
        if (this._stringFormat == null)
        {
          this._stringFormat = new StringFormat(StringFormat.GenericDefault);
          this._stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
          this._stringFormat.LineAlignment = StringAlignment.Center;
        }
        if (this.Enabled)
          graphics.FillRectangle(SystemBrushes.Window, this._bounds);
        else
          graphics.FillRectangle(SystemBrushes.Control, this._bounds);
        if (renderer is Office2003Renderer)
          ((Office2003Renderer) renderer)._comboBox = this.ComboBox;
        renderer.DrawComboBox(this.ComboBox, graphics, this._bounds, state, false);
        Rectangle bounds = this._bounds;
        bounds.Inflate(-2, -2);
        bounds.Width -= SystemInformation.VerticalScrollBarWidth;
        Brush brush = this.Enabled ? SystemBrushes.ControlText : SystemBrushes.ControlDark;
        if (this.ComboBox.Text.Length == 0)
          graphics.DrawString(this._comboBox.DefaultText, this.ComboBox.Font, brush, (RectangleF) bounds, this._stringFormat);
        else
          graphics.DrawString(this.ComboBox.Text, this.ComboBox.Font, brush, (RectangleF) bounds, this._stringFormat);
      }

      [Browsable(false)]
      public ComboBox ComboBox => (ComboBox) this._comboBox;

      [Description("Provides a textual hint as to the type of data to enter, before any is entered.")]
      [DefaultValue("")]
      [Localizable(true)]
      [Category("Appearance")]
      public string DefaultText
      {
        get => this._comboBox.DefaultText;
        set => this._comboBox.DefaultText = value;
      }

      [DefaultValue(typeof (ComboBoxStyle), "DropDown")]
      [Category("Appearance")]
      [Description("Controls the appearance and functionality of the combo box.")]
      public ComboBoxStyle DropDownStyle
      {
        get => this._comboBox.DropDownStyle;
        set
        {
          this._comboBox.DropDownStyle = value != ComboBoxStyle.Simple ? value : throw new ArgumentException("This style is not supported for a hosted combo box.");
        }
      }

      [Description("The items in the combo box.")]
      [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, Custom=null", typeof (UITypeEditor))]
      [Category("Data")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
      public ComboBox.ObjectCollection Items => this._comboBox.Items;

      private void ComboBox_SelectedValueChanged(object sender, EventArgs e)
      {
        this.OnSelectedValueChanged();
      }

      private void OnSelectedValueChanged()
      {
        EventHandler selectedValueChanged = this.SelectedValueChanged;
        if (selectedValueChanged == null)
          return;
        selectedValueChanged((object) this, new EventArgs());
      }
    }
}
