
// Type: IMClient.Splash.AnimatorBase




using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient.Splash
{
    internal abstract class AnimatorBase : Component, ISupportInitialize
    {
      public const double DEFAULT_STEP_SIZE = 2.0;
      public const int DEFAULT_INTERVALL = 10;
      public const bool DEFAULT_LOOP_ANIMATION = false;
      private const bool DEFAULT_NEVER_ENDING_TIMER = false;
      private const SynchronizationMode DEFAULT_SYNCHRONIZATION_MODE = SynchronizationMode.None;
      private const string SET_PROP_WITH_PARENT_ANIMATOR_ERROR_MESSAGE = "Property cannot be set while ParentAnimator is set to anything other than null.";
      private Timer _timer;
      private System.ComponentModel.Container components;
      private double _stepSize = 2.0;
      private double _currentStep;
      private bool _loopAnimation;
      private bool _neverEndingTimer;
      private SynchronizationMode _syncMode;
      private AnimatorBase _parentAnimator;
      private AnimatorBase _triggerAnimator;
      private bool _isInitializing;
      private ArrayList _childAnimators = new ArrayList();
      private bool _settingCurrentValue;

      public event EventHandler AnimationStarted;

      public event EventHandler AnimationStopped;

      public event EventHandler AnimationContinued;

      public event EventHandler AnimationFinished;

      public event EventHandler StepSizeChanged;

      public event EventHandler IntervallChanged;

      public event EventHandler CurrentStepChanged;

      public event EventHandler LoopAnimationChanged;

      public event EventHandler StartValueChanged;

      public event EventHandler EndValueChanged;

      public event EventHandler SynchronizationModeChanged;

      public AnimatorBase(IContainer container)
      {
        container.Add((IComponent) this);
        this.InitializeComponent();
        this.Initialize();
      }

      public AnimatorBase()
      {
        this.InitializeComponent();
        this.Initialize();
      }

      private void Initialize() => this._timer.Interval = 10;

      private void InitializeComponent()
      {
        this._timer = new Timer();
        this._timer.Tick += new EventHandler(this.OnTimerElapsed);
      }

      protected override void Dispose(bool disposing)
      {
        this.ParentAnimator = (AnimatorBase) null;
        this._childAnimators.Clear();
        this.TriggerAnimator = (AnimatorBase) null;
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      [Browsable(false)]
      public abstract object StartValue { get; set; }

      [Browsable(false)]
      public abstract object EndValue { get; set; }

      [Browsable(false)]
      public object CurrentValue
      {
        get => this.CurrentValueInternal;
        set
        {
          if (this._settingCurrentValue)
            return;
          try
          {
            this._settingCurrentValue = true;
            this.CurrentValueInternal = value;
          }
          finally
          {
            this._settingCurrentValue = false;
          }
        }
      }

      [Description("Gets or sets the AnimatorBase which should trigger the animation of this instance when it has finished animating.")]
      [Browsable(true)]
      [DefaultValue(null)]
      [Category("Behavior")]
      public AnimatorBase TriggerAnimator
      {
        get => this._triggerAnimator;
        set
        {
          if (this._triggerAnimator == value)
            return;
          if (this._triggerAnimator == this)
            throw new InvalidOperationException("Cannot set itself as TriggerAnimator.");
          if (this._triggerAnimator != null)
            this._triggerAnimator.AnimationFinished -= new EventHandler(this.OnTriggerAnimatorAnimationFinished);
          this._triggerAnimator = value;
          if (this._triggerAnimator == null)
            return;
          this._triggerAnimator.AnimationFinished += new EventHandler(this.OnTriggerAnimatorAnimationFinished);
        }
      }

      [Browsable(true)]
      [RefreshProperties(RefreshProperties.Repaint)]
      [Category("Behavior")]
      [DefaultValue(null)]
      public AnimatorBase ParentAnimator
      {
        get => this._parentAnimator;
        set
        {
          if (this._parentAnimator == value)
            return;
          if (this._parentAnimator == this)
            throw new InvalidOperationException("Cannot set itself as ParentAnimator.");
          if (this._parentAnimator != null)
            this._parentAnimator.RemoveChildAnimator(this);
          this._parentAnimator = value;
          if (this._parentAnimator == null)
            return;
          this._parentAnimator.AddChildAnimator(this);
        }
      }

      [Browsable(true)]
      [Description("Gets or sets the mode of design time synchronization.")]
      [RefreshProperties(RefreshProperties.Repaint)]
      [Category("Design")]
      public SynchronizationMode SynchronizationMode
      {
        get => this._syncMode;
        set => this.SetSynchronizationMode(value, true);
      }

      [Description("Gets or sets the intervall (in milliseconds) between updates to the animation.")]
      [DefaultValue(10)]
      [Browsable(true)]
      [Category("Behavior")]
      public int Intervall
      {
        get => this._timer.Interval;
        set => this.SetIntervall(value, true);
      }

      [Category("Behavior")]
      [Description("Gets or sets the size of each step (in %) when updating the animation.")]
      [Browsable(true)]
      [DefaultValue(2.0)]
      public double StepSize
      {
        get => this._stepSize;
        set => this.SetStepSize(value, true);
      }

      [DefaultValue(false)]
      [Description("Gets or sets whether the animation should loop between StartValue and EndValue until Stop() is called.")]
      [Browsable(true)]
      [Category("Behavior")]
      public bool LoopAnimation
      {
        get => this._loopAnimation;
        set => this.SetLoopAnimation(value, true);
      }

      [Browsable(false)]
      public double CurrentStep
      {
        get => this._currentStep;
        set
        {
          if (this._currentStep == value)
            return;
          this._currentStep = value;
          if (this._currentStep > 100.0)
            this._currentStep = 100.0;
          else if (this._currentStep < 0.0)
            this._currentStep = 0.0;
          this.CurrentValue = this.GetValueForStep(this._currentStep);
          foreach (AnimatorBase childAnimator in this._childAnimators)
            childAnimator.CurrentStep = this._currentStep;
          this.OnCurrentStepChanged(EventArgs.Empty);
        }
      }

      [Browsable(false)]
      public bool IsRunning
      {
        get => this._parentAnimator != null ? this._parentAnimator.IsRunning : this._timer.Enabled;
      }

      [Category("Behavior")]
      [DefaultValue(false)]
      [Browsable(true)]
      [Description("Gets or sets whether the internal timer should always continue running even if the animation has reached its end")]
      public bool NeverEndingTimer
      {
        get => this._neverEndingTimer;
        set => this._neverEndingTimer = value;
      }

      public void Continue()
      {
        this._timer.Start();
        this.OnAnimationContinued(EventArgs.Empty);
      }

      public void Start(object endValue)
      {
        if (this._childAnimators.Count > 0)
          throw new InvalidOperationException("Function cannot be called when ChildAnimators are set.");
        this.EndValue = endValue;
        this.Start(true);
      }

      public void Start() => this.Start(true);

      public void Start(bool setStartValuesToCurrentValues)
      {
        if (setStartValuesToCurrentValues)
          this.SetStartValuesToCurrentValue();
        this.CurrentStep = 0.0;
        if (!this._timer.Enabled)
          this._timer.Start();
        this.OnAnimationStarted(EventArgs.Empty);
      }

      public void SetCurrentValuesToStartValues()
      {
        this.CurrentValue = this.StartValue;
        foreach (AnimatorBase childAnimator in this._childAnimators)
          childAnimator.SetCurrentValuesToStartValues();
      }

      public void SetStartValuesToCurrentValue()
      {
        this.StartValue = this.CurrentValue;
        foreach (AnimatorBase childAnimator in this._childAnimators)
          childAnimator.SetStartValuesToCurrentValue();
      }

      public void Stop()
      {
        if (!this._timer.Enabled)
          return;
        this._timer.Stop();
        this.OnAnimationStopped(EventArgs.Empty);
      }

      protected void SynchronizeToSource()
      {
        if (!this.DesignMode)
          return;
        switch (this._syncMode)
        {
          case SynchronizationMode.Start:
            this.CurrentValue = this.StartValue;
            break;
          case SynchronizationMode.End:
            this.CurrentValue = this.EndValue;
            break;
        }
      }

      protected void SynchronizeFromSource()
      {
        if (!this.DesignMode)
          return;
        switch (this._syncMode)
        {
          case SynchronizationMode.Start:
            this.StartValue = this.CurrentValue;
            break;
          case SynchronizationMode.End:
            this.EndValue = this.CurrentValue;
            break;
        }
      }

      protected void ResetValues()
      {
        if (this._isInitializing)
          return;
        this.StartValue = this.CurrentValue;
        this.EndValue = this.CurrentValue;
      }

      protected virtual void OnAnimationStarted(EventArgs eventArgs)
      {
        if (this.AnimationStarted == null)
          return;
        this.AnimationStarted((object) this, eventArgs);
      }

      protected virtual void OnAnimationContinued(EventArgs eventArgs)
      {
        if (this.AnimationContinued == null)
          return;
        this.AnimationContinued((object) this, eventArgs);
      }

      protected virtual void OnAnimationStopped(EventArgs eventArgs)
      {
        if (this.AnimationStopped == null)
          return;
        this.AnimationStopped((object) this, eventArgs);
      }

      protected virtual void OnAnimationFinished(EventArgs eventArgs)
      {
        if (this.AnimationFinished == null)
          return;
        this.AnimationFinished((object) this, eventArgs);
      }

      protected virtual void OnLoopAnimationChanged(EventArgs eventArgs)
      {
        if (this.LoopAnimationChanged == null)
          return;
        this.LoopAnimationChanged((object) this, eventArgs);
      }

      protected virtual void OnStepSizeChanged(EventArgs eventArgs)
      {
        if (this.StepSizeChanged == null)
          return;
        this.StepSizeChanged((object) this, eventArgs);
      }

      protected virtual void OnIntervallChanged(EventArgs eventArgs)
      {
        if (this.IntervallChanged == null)
          return;
        this.IntervallChanged((object) this, eventArgs);
      }

      protected void OnSynchronizationModeChanged(EventArgs eventArgs)
      {
        if (this.SynchronizationModeChanged == null)
          return;
        this.SynchronizationModeChanged((object) this, eventArgs);
      }

      protected virtual void OnCurrentStepChanged(EventArgs eventArgs)
      {
        if (this.CurrentStepChanged == null)
          return;
        this.CurrentStepChanged((object) this, eventArgs);
      }

      protected virtual void OnStartValueChanged(EventArgs eventArgs)
      {
        if (this._syncMode == SynchronizationMode.Start)
          this.CurrentValue = this.StartValue;
        if (this.StartValueChanged == null)
          return;
        this.StartValueChanged((object) this, eventArgs);
      }

      protected virtual void OnEndValueChanged(EventArgs eventArgs)
      {
        if (this._syncMode == SynchronizationMode.End)
          this.CurrentValue = this.EndValue;
        if (this.EndValueChanged == null)
          return;
        this.EndValueChanged((object) this, eventArgs);
      }

      protected abstract object CurrentValueInternal { get; set; }

      protected bool SettingCurrentValue => this._settingCurrentValue;

      protected abstract object GetValueForStep(double step);

      protected void AddChildAnimator(AnimatorBase animator)
      {
        if (animator == null)
          throw new ArgumentNullException(nameof (animator));
        if (!this._childAnimators.Contains((object) animator))
          this._childAnimators.Add((object) animator);
        animator.SetIntervall(this.Intervall, false);
        animator.SetStepSize(this.StepSize, false);
        animator.SetLoopAnimation(this.LoopAnimation, false);
        animator.SetSynchronizationMode(this.SynchronizationMode, false);
      }

      protected void RemoveChildAnimator(AnimatorBase animator)
      {
        if (animator == null)
          throw new ArgumentNullException(nameof (animator));
        if (!this._childAnimators.Contains((object) animator))
          return;
        this._childAnimators.Remove((object) animator);
      }

      protected bool IsInitializing => this._isInitializing;

      protected virtual bool ShouldSerializeSynchronizationMode() => false;

      private void SwitchStartEndValues()
      {
        object startValue = this.StartValue;
        this.StartValue = this.EndValue;
        this.EndValue = startValue;
        foreach (AnimatorBase childAnimator in this._childAnimators)
          childAnimator.SwitchStartEndValues();
      }

      private void SetSynchronizationMode(
        SynchronizationMode synchronizationMode,
        bool checkParentAnimator)
      {
        if (this._syncMode == synchronizationMode)
          return;
        if (synchronizationMode == SynchronizationMode.ResetToCurrent)
        {
          if (!this.DesignMode)
            return;
          this.ResetValues();
        }
        else
        {
          if (this._parentAnimator != null & checkParentAnimator && !this._isInitializing)
            throw new InvalidOperationException("Property cannot be set while ParentAnimator is set to anything other than null.");
          this._syncMode = synchronizationMode;
          this.SynchronizeToSource();
          foreach (AnimatorBase childAnimator in this._childAnimators)
            childAnimator.SetSynchronizationMode(synchronizationMode, false);
          this.OnSynchronizationModeChanged(EventArgs.Empty);
        }
      }

      private void SetIntervall(int intervall, bool checkParentAnimator)
      {
        if (this._timer.Interval == intervall)
          return;
        if (this._parentAnimator != null & checkParentAnimator && !this._isInitializing)
          throw new InvalidOperationException("Property cannot be set while ParentAnimator is set to anything other than null.");
        this._timer.Interval = intervall;
        foreach (AnimatorBase childAnimator in this._childAnimators)
          childAnimator.SetIntervall(intervall, false);
        this.OnIntervallChanged(EventArgs.Empty);
      }

      private void SetStepSize(double stepSize, bool checkParentAnimator)
      {
        if (this._stepSize == stepSize)
          return;
        if (this._parentAnimator != null & checkParentAnimator && !this._isInitializing)
          throw new InvalidOperationException("Property cannot be set while ParentAnimator is set to anything other than null.");
        this._stepSize = stepSize;
        foreach (AnimatorBase childAnimator in this._childAnimators)
          childAnimator.SetStepSize(stepSize, false);
        this.OnStepSizeChanged(EventArgs.Empty);
      }

      private void SetLoopAnimation(bool loopAnimation, bool checkParentAnimator)
      {
        if (this._loopAnimation == loopAnimation)
          return;
        if (this._parentAnimator != null & checkParentAnimator && !this._isInitializing)
          throw new InvalidOperationException("Property cannot be set while ParentAnimator is set to anything other than null.");
        this._loopAnimation = loopAnimation;
        foreach (AnimatorBase childAnimator in this._childAnimators)
          childAnimator.SetLoopAnimation(loopAnimation, false);
        this.OnLoopAnimationChanged(EventArgs.Empty);
      }

      private void OnTimerElapsed(object sender, EventArgs e)
      {
        this.CurrentStep += this._stepSize;
        if (this.CurrentStep < 100.0)
          return;
        bool enabled = this._timer.Enabled;
        if (this._timer.Enabled && !this._neverEndingTimer && !this._loopAnimation)
          this._timer.Stop();
        this.OnAnimationFinished(EventArgs.Empty);
        if (!(this._loopAnimation & enabled))
          return;
        this.SwitchStartEndValues();
        this.Start();
      }

      private void OnTriggerAnimatorAnimationFinished(object sender, EventArgs e) => this.Start();

      public static Color InterpolateColors(Color color1, Color color2, double percent)
      {
        return Color.FromArgb(AnimatorBase.InterpolateIntegerValues((int) color1.A, (int) color2.A, percent), AnimatorBase.InterpolateIntegerValues((int) color1.R, (int) color2.R, percent), AnimatorBase.InterpolateIntegerValues((int) color1.G, (int) color2.G, percent), AnimatorBase.InterpolateIntegerValues((int) color1.B, (int) color2.B, percent));
      }

      public static Rectangle InterpolateRectangles(
        Rectangle rectangle1,
        Rectangle rectangle2,
        double percent)
      {
        return new Rectangle(AnimatorBase.InterpolatePoints(rectangle1.Location, rectangle2.Location, percent), AnimatorBase.InterpolateSizes(rectangle1.Size, rectangle2.Size, percent));
      }

      public static Point InterpolatePoints(Point point1, Point point2, double percent)
      {
        return new Point(AnimatorBase.InterpolateIntegerValues(point1.X, point2.X, percent), AnimatorBase.InterpolateIntegerValues(point1.Y, point2.Y, percent));
      }

      public static Size InterpolateSizes(Size size1, Size size2, double percent)
      {
        return new Size(AnimatorBase.InterpolateIntegerValues(size1.Width, size2.Width, percent), AnimatorBase.InterpolateIntegerValues(size1.Height, size2.Height, percent));
      }

      public static double InterpolateDoubleValues(double value1, double value2, double percent)
      {
        if (percent < 0.0 || percent > 100.0)
          throw new ArgumentException("Value must be between 0 and 100.", nameof (percent));
        return percent * (value2 - value1) / 100.0 + value1;
      }

      public static int InterpolateIntegerValues(int value1, int value2, double percent)
      {
        if (percent < 0.0 || percent > 100.0)
          throw new ArgumentException("Value must be between 0 and 100.", nameof (percent));
        return Convert.ToInt32(percent * (double) (value2 - value1) / 100.0 + (double) value1);
      }

      public void BeginInit() => this._isInitializing = true;

      public void EndInit() => this._isInitializing = false;
    }
}
