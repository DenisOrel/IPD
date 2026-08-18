
// Type: IMClient.Splash.ControlForeColorAnimator




using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient.Splash
{
    internal class ControlForeColorAnimator : AnimatorBase
    {
      private Control _control;
      private Color _startColor;
      private Color _endColor;

      public ControlForeColorAnimator(IContainer container)
        : base(container)
      {
        this.Initialize();
      }

      public ControlForeColorAnimator() => this.Initialize();

      private void Initialize()
      {
        this._startColor = this.DefaultStartColor;
        this._endColor = this.DefaultEndColor;
      }

      [Description("Gets or sets the starting color for the animation.")]
      [Browsable(true)]
      [Category("Appearance")]
      public Color StartColor
      {
        get => this._startColor;
        set
        {
          if (this._startColor == value)
            return;
          this._startColor = value;
          this.OnStartValueChanged(EventArgs.Empty);
        }
      }

      [Browsable(true)]
      [Description("Gets or sets the ending Color for the animation.")]
      [Category("Appearance")]
      public Color EndColor
      {
        get => this._endColor;
        set
        {
          if (this._endColor == value)
            return;
          this._endColor = value;
          this.OnEndValueChanged(EventArgs.Empty);
        }
      }

      [DefaultValue(null)]
      [Browsable(true)]
      [Category("Behavior")]
      [Description("Gets or sets which Control should be animated.")]
      [RefreshProperties(RefreshProperties.Repaint)]
      public Control Control
      {
        get => this._control;
        set
        {
          if (this._control == value)
            return;
          if (this._control != null)
            this._control.ForeColorChanged -= new EventHandler(this.OnControlColorChanged);
          this._control = value;
          if (this._control != null)
            this._control.ForeColorChanged += new EventHandler(this.OnControlColorChanged);
          this.ResetValues();
        }
      }

      protected override object CurrentValueInternal
      {
        get => (object) (this._control == null ? Color.Empty : this._control.ForeColor);
        set
        {
          if (this._control == null)
            return;
          this._control.ForeColor = (Color) value;
        }
      }

      public override object StartValue
      {
        get => (object) this.StartColor;
        set => this.StartColor = (Color) value;
      }

      public override object EndValue
      {
        get => (object) this.EndColor;
        set => this.EndColor = (Color) value;
      }

      protected override object GetValueForStep(double step)
      {
        return this._startColor == Color.Empty || this._endColor == Color.Empty ? this.CurrentValue : (object) AnimatorBase.InterpolateColors(this._startColor, this._endColor, step);
      }

      protected virtual Color DefaultStartColor => Color.Empty;

      protected virtual Color DefaultEndColor => Color.Empty;

      protected virtual bool ShouldSerializeStartColor() => this._startColor != this.DefaultStartColor;

      protected virtual bool ShouldSerializeEndColor() => this._endColor != this.DefaultEndColor;

      private void OnControlColorChanged(object sender, EventArgs e) => this.SynchronizeFromSource();
    }
}
