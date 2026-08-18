
// Type: IMClient.Splash.FormOpacityAnimator




using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace IMClient.Splash
{
    internal class FormOpacityAnimator : AnimatorBase
    {
      private const double DEFAULT_OPACITY = 1.0;
      private Form _form;
      private double _startOpacity = 1.0;
      private double _endOpacity = 1.0;

      public FormOpacityAnimator(IContainer container)
        : base(container)
      {
      }

      public FormOpacityAnimator()
      {
      }

      [Description("Gets or sets the starting opacity for the animation.")]
      [Category("Appearance")]
      [DefaultValue(1.0)]
      [Browsable(true)]
      [TypeConverter(typeof (OpacityConverter))]
      public double StartOpacity
      {
        get => this._startOpacity;
        set
        {
          if (this._startOpacity == value)
            return;
          this._startOpacity = value;
          this.OnStartValueChanged(EventArgs.Empty);
        }
      }

      [DefaultValue(1.0)]
      [Description("Gets or sets the ending opacity for the animation.")]
      [Category("Appearance")]
      [Browsable(true)]
      [TypeConverter(typeof (OpacityConverter))]
      public double EndOpacity
      {
        get => this._endOpacity;
        set
        {
          if (this._endOpacity == value)
            return;
          this._endOpacity = value;
          this.OnEndValueChanged(EventArgs.Empty);
        }
      }

      [RefreshProperties(RefreshProperties.Repaint)]
      [Description("Gets or sets which Form should be animated.")]
      [Browsable(true)]
      [DefaultValue(null)]
      [Category("Behavior")]
      public Form Form
      {
        get => this._form;
        set
        {
          if (this._form == value)
            return;
          this._form = value;
          this.ResetValues();
        }
      }

      protected override object CurrentValueInternal
      {
        get => (object) (this._form == null ? 0.0 : this._form.Opacity);
        set
        {
          if (this._form == null)
            return;
          this.SetOpacityInternal(value);
        }
      }

      private void SetOpacityInternal(object value)
      {
        if (this._form.InvokeRequired)
          this._form.Invoke((Delegate) new FormOpacityAnimator.OpacityDelegateHandler(this.SetOpacityInternal), value);
        else
          this._form.Opacity = (double) value;
      }

      public override object StartValue
      {
        get => (object) this.StartOpacity;
        set => this.StartOpacity = Convert.ToDouble(value);
      }

      public override object EndValue
      {
        get => (object) this.EndOpacity;
        set => this.EndOpacity = Convert.ToDouble(value);
      }

      protected override object GetValueForStep(double step)
      {
        return (object) AnimatorBase.InterpolateDoubleValues(this._startOpacity, this._endOpacity, step);
      }

      private delegate void OpacityDelegateHandler(object value);
    }
}
