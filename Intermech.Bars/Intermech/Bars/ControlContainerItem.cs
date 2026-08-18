
// Type: Intermech.Bars.ControlContainerItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    public abstract class ControlContainerItem : ToolbarItemBase
    {
      private Control _containedControl;
      private int _minimumControlWidth;
      internal Rectangle _bounds;

      protected ControlContainerItem(Control control)
      {
        this._minimumControlWidth = 50;
        this._containedControl = control;
        this.Padding.Left = 1;
        this.Padding.Top = 0;
        this.Padding.Right = 1;
        this.Padding.Bottom = 0;
      }

      protected internal override void ApplyLayout(
        Rectangle buttonBounds,
        Graphics graphics,
        bool vertical,
        bool rightToLeft)
      {
        base.ApplyLayout(buttonBounds, graphics, vertical, rightToLeft);
        this._bounds = this.ButtonInnerBounds;
        int num = 0;
        if (this.Text.Length != 0)
          num = (int) Math.Ceiling((double) graphics.MeasureString(this.Text, this.ToolBar.Font, int.MaxValue, StringFormat.GenericDefault).Width) + 2;
        this._bounds.X += num;
        this._bounds.Width -= num;
        this.ContainedControl.Bounds = this._bounds;
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
          this._containedControl.Dispose();
        base.Dispose(disposing);
      }

      protected internal virtual void DrawDesignTimeControl(
        IToolBarRenderer renderer,
        Graphics graphics,
        DrawItemState state)
      {
        graphics.DrawRectangle(SystemPens.ControlText, this.ButtonInnerBounds);
      }

      [Browsable(false)]
      public Control ContainedControl => this._containedControl;

      [Category("Appearance")]
      [DefaultValue("")]
      [Description("The text contained in the control.")]
      public string ControlText
      {
        get => this._containedControl.Text;
        set => this._containedControl.Text = value;
      }

      [Browsable(false)]
      [Obsolete("Use the MinimumControlWidth property instead.")]
      [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
      public int ControlWidth
      {
        get => this.MinimumControlWidth;
        set => this.MinimumControlWidth = value;
      }

      [DefaultValue(true)]
      public override bool Enabled
      {
        get => base.Enabled;
        set
        {
          base.Enabled = value;
          if (this._containedControl != null)
            this._containedControl.Enabled = value;
          if (!this.DesignMode)
            return;
          this._containedControl.Enabled = false;
        }
      }

      [Category("Layout")]
      [Description("Sets the minimum acceptable width of the hosted control.")]
      public int MinimumControlWidth
      {
        get => this._minimumControlWidth;
        set
        {
          this._minimumControlWidth = value;
          this.LayoutNeeded();
        }
      }

      [Browsable(false)]
      public override string ToolTipText
      {
        get => base.ToolTipText;
        set => base.ToolTipText = value;
      }

      [Description("Indicates whether this item is visible or not.")]
      [DefaultValue(true)]
      public override bool Visible
      {
        get => base.Visible;
        set
        {
          base.Visible = value;
          if (this._containedControl.Visible != value)
            this._containedControl.Visible = value;
          if (this.ToolBar == null)
            return;
          this.ToolBar.DoLayout();
        }
      }
    }
}
