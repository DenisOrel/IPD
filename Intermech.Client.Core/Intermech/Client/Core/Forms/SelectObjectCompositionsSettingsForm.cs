
// Type: Intermech.Client.Core.Forms.SelectObjectCompositionsSettingsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using Intermech.UI;
using Intermech.Windows.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace Intermech.Client.Core.Forms;

public class SelectObjectCompositionsSettingsForm : 
  IpsBaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IContextAware,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected CheckBox _checkBoxBackgroundVisibleObjectsCompositionLoad;
  protected GroupBox _groupBoxOnLoad;
  protected CheckBox _checkBoxCheckAllObjectsOnLoad;
  protected Panel _panelCommonOptions;
  protected RadioButton _radioButtonAutoLoadCompositionDepth;
  protected RadioButton _radioButtonAutoLoadCompositionFull;
  protected RadioButton _radioButtonAutoLoadCompositionNone;
  protected NumericUpDown _editAutoLoadCompositionDepth;
  protected GroupBox _groupBoxWarningIf;
  protected NumericUpDown _editWarningWhenCheckedCountMoreThanCount;
  protected CheckBox _checkBoxWarningWhenCheckedCountMoreThan;
  protected CheckBox _checkBoxWarningWhenCheckedNotLoaded;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxBackgroundVisibleObjectsCompositionLoad
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxBackgroundVisibleObjectsCompositionLoad.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal GroupBox GroupBoxOnLoad
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBoxOnLoad.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxCheckAllObjectsOnLoad
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxCheckAllObjectsOnLoad.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal Panel PanelCommonOptions
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._panelCommonOptions.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal RadioButton RadioButtonAutoLoadCompositionDepth
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonAutoLoadCompositionDepth.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal RadioButton RadioButtonAutoLoadCompositionFull
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonAutoLoadCompositionFull.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal RadioButton RadioButtonAutoLoadCompositionNone
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._radioButtonAutoLoadCompositionNone.CheckInitializedIn<RadioButton>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal NumericUpDown EditAutoLoadCompositionDepth
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editAutoLoadCompositionDepth.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal GroupBox GroupBoxWarningIf
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._groupBoxWarningIf.CheckInitializedIn<GroupBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal NumericUpDown EditWarningWhenCheckedCountMoreThanCount
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._editWarningWhenCheckedCountMoreThanCount.CheckInitializedIn<NumericUpDown>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxWarningWhenCheckedCountMoreThan
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxWarningWhenCheckedCountMoreThan.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DebuggerHidden]
  protected internal CheckBox CheckBoxWarningWhenCheckedNotLoaded
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._checkBoxWarningWhenCheckedNotLoaded.CheckInitializedIn<CheckBox>((object) this);
    }
  }

  public SelectObjectCompositionsSettingsForm()
  {
    this.InitializeComponent();
    if (!this.InDesignMode)
      throw new Exception($"Default constructor for class \"{this.GetType().Name}\" only for design mode");
  }

  public SelectObjectCompositionsSettingsForm(
    [CanBeNull] Form parentForm,
    [CanBeNull] System.IServiceProvider ownerServices,
    [CanBeNull] string contextName,
    [NotNull] SelectObjectCompositionSettings settings)
    : base(parentForm, ownerServices, contextName)
  {
    this.InitializeComponent();
    this.Settings = settings;
  }

  [NotNull]
  public SelectObjectCompositionSettings Settings
  {
    [DebuggerStepThrough] get
    {
      return new SelectObjectCompositionSettings(this.CheckBoxBackgroundVisibleObjectsCompositionLoad.Checked, this.CheckBoxCheckAllObjectsOnLoad.Checked, this.Autoload, (int) this.EditAutoLoadCompositionDepth.Value, this.CheckBoxWarningWhenCheckedNotLoaded.Checked, this.WarningWhenCheckedCountMoreThan, this.WarningWhenCheckedCountMoreThanCount);
    }
    private set
    {
      this.CheckBoxBackgroundVisibleObjectsCompositionLoad.Checked = value.BackgroundVisibleObjectsCompositionLoad;
      this.CheckBoxCheckAllObjectsOnLoad.Checked = value.CheckAllObjectsOnLoad;
      this.Autoload = value.AutoLoadComposition;
      this.EditAutoLoadCompositionDepth.Value = (Decimal) value.AutoLoadCompositionDepth;
      this.CheckBoxWarningWhenCheckedNotLoaded.Checked = value.WarningWhenCheckedNotLoaded;
      this.WarningWhenCheckedCountMoreThan = value.WarningWhenCheckedCountMoreThan;
      this.WarningWhenCheckedCountMoreThanCount = value.WarningWhenCheckedCountMoreThanCount;
    }
  }

  protected SelectObjectCompositionAutoload Autoload
  {
    get
    {
      if (this.RadioButtonAutoLoadCompositionFull.Checked)
        return SelectObjectCompositionAutoload.Full;
      return !this.RadioButtonAutoLoadCompositionDepth.Checked ? SelectObjectCompositionAutoload.None : SelectObjectCompositionAutoload.Depth;
    }
    set
    {
      switch (value)
      {
        case SelectObjectCompositionAutoload.None:
          if (!this.RadioButtonAutoLoadCompositionNone.Checked)
            this.RadioButtonAutoLoadCompositionNone.Checked = true;
          this.CheckAutoLoadCompositionDepthEnabled();
          break;
        case SelectObjectCompositionAutoload.Full:
          if (!this.RadioButtonAutoLoadCompositionFull.Checked)
            this.RadioButtonAutoLoadCompositionFull.Checked = true;
          this.CheckAutoLoadCompositionDepthEnabled();
          break;
        case SelectObjectCompositionAutoload.Depth:
          if (!this.RadioButtonAutoLoadCompositionDepth.Checked)
            this.RadioButtonAutoLoadCompositionDepth.Checked = true;
          this.CheckAutoLoadCompositionDepthEnabled();
          break;
      }
    }
  }

  protected bool WarningWhenCheckedCountMoreThan
  {
    [DebuggerStepThrough] get => this.CheckBoxWarningWhenCheckedCountMoreThan.Checked;
    [DebuggerStepThrough] set
    {
      if (this.CheckBoxWarningWhenCheckedCountMoreThan.Checked == value)
        return;
      this.CheckBoxWarningWhenCheckedCountMoreThan.Checked = value;
      this.EditWarningWhenCheckedCountMoreThanCount.Enabled = value;
      this.EditWarningWhenCheckedCountMoreThanCount.BackColor = value ? SystemColors.Window : SystemColors.ButtonFace;
    }
  }

  protected int WarningWhenCheckedCountMoreThanCount
  {
    [DebuggerStepThrough] get => (int) this.EditWarningWhenCheckedCountMoreThanCount.Value;
    [DebuggerStepThrough] set
    {
      this.EditWarningWhenCheckedCountMoreThanCount.Value = (Decimal) value;
    }
  }

  private void CheckWarningWhenCheckedCountMoreThanCountEnabled()
  {
    bool flag = this.CheckBoxWarningWhenCheckedCountMoreThan.Checked;
    if (this.EditWarningWhenCheckedCountMoreThanCount.Enabled == flag)
      return;
    this.EditWarningWhenCheckedCountMoreThanCount.Enabled = flag;
    this.EditWarningWhenCheckedCountMoreThanCount.BackColor = flag ? SystemColors.Window : SystemColors.ButtonFace;
  }

  private void CheckAutoLoadCompositionDepthEnabled()
  {
    bool flag = this.RadioButtonAutoLoadCompositionDepth.Checked;
    if (this.EditAutoLoadCompositionDepth.Enabled == flag)
      return;
    this.EditAutoLoadCompositionDepth.Enabled = flag;
    this.EditAutoLoadCompositionDepth.BackColor = flag ? SystemColors.Window : SystemColors.ButtonFace;
  }

  private void _radioButtonAutoLoadCompositionFull_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckAutoLoadCompositionDepthEnabled();
  }

  private void _checkBoxWarningWhenCheckedCountMoreThan_CheckedChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.CheckWarningWhenCheckedCountMoreThanCountEnabled();
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this._checkBoxBackgroundVisibleObjectsCompositionLoad = new CheckBox();
    this._groupBoxOnLoad = new GroupBox();
    this._editAutoLoadCompositionDepth = new NumericUpDown();
    this._radioButtonAutoLoadCompositionDepth = new RadioButton();
    this._radioButtonAutoLoadCompositionFull = new RadioButton();
    this._radioButtonAutoLoadCompositionNone = new RadioButton();
    this._checkBoxCheckAllObjectsOnLoad = new CheckBox();
    this._panelCommonOptions = new Panel();
    this._groupBoxWarningIf = new GroupBox();
    this._editWarningWhenCheckedCountMoreThanCount = new NumericUpDown();
    this._checkBoxWarningWhenCheckedCountMoreThan = new CheckBox();
    this._checkBoxWarningWhenCheckedNotLoaded = new CheckBox();
    this._pnlDialogButtons.SuspendLayout();
    this._panelBtns.SuspendLayout();
    this._groupBoxOnLoad.SuspendLayout();
    this._editAutoLoadCompositionDepth.BeginInit();
    this._panelCommonOptions.SuspendLayout();
    this._groupBoxWarningIf.SuspendLayout();
    this._editWarningWhenCheckedCountMoreThanCount.BeginInit();
    this.SuspendLayout();
    this._pnlDialogButtons.Location = new Point(0, 272);
    this._pnlDialogButtons.Size = new Size(361, 36);
    this._pnlDialogButtons.TabIndex = 2;
    this._bevelDialogButtons.Location = new Point(0, 270);
    this._bevelDialogButtons.Shape = BevelShape.Box;
    this._bevelDialogButtons.Size = new Size(361, 2);
    this._bevelDialogButtons.Style = BevelStyle.Lowered;
    this._panelBtns.Location = new Point(188, 0);
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.AutoSize = true;
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.Checked = true;
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.CheckState = CheckState.Checked;
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.Location = new Point(23, 12);
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.Name = "_checkBoxBackgroundVisibleObjectsCompositionLoad";
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.Size = new Size(265, 17);
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.TabIndex = 0;
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.Text = "Фоновая загрузка состава видимых объектов";
    this._checkBoxBackgroundVisibleObjectsCompositionLoad.UseVisualStyleBackColor = true;
    this._groupBoxOnLoad.Controls.Add((Control) this._editAutoLoadCompositionDepth);
    this._groupBoxOnLoad.Controls.Add((Control) this._radioButtonAutoLoadCompositionDepth);
    this._groupBoxOnLoad.Controls.Add((Control) this._radioButtonAutoLoadCompositionFull);
    this._groupBoxOnLoad.Controls.Add((Control) this._radioButtonAutoLoadCompositionNone);
    this._groupBoxOnLoad.Controls.Add((Control) this._checkBoxCheckAllObjectsOnLoad);
    this._groupBoxOnLoad.Dock = DockStyle.Top;
    this._groupBoxOnLoad.Location = new Point(0, 40);
    this._groupBoxOnLoad.Name = "_groupBoxOnLoad";
    this._groupBoxOnLoad.Size = new Size(361, 140);
    this._groupBoxOnLoad.TabIndex = 1;
    this._groupBoxOnLoad.TabStop = false;
    this._groupBoxOnLoad.Text = "При загрузке формы";
    this._editAutoLoadCompositionDepth.BackColor = SystemColors.ButtonFace;
    this._editAutoLoadCompositionDepth.Enabled = false;
    this._editAutoLoadCompositionDepth.Location = new Point(258, 78);
    this._editAutoLoadCompositionDepth.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._editAutoLoadCompositionDepth.Name = "_editAutoLoadCompositionDepth";
    this._editAutoLoadCompositionDepth.Size = new Size(58, 20);
    this._editAutoLoadCompositionDepth.TabIndex = 3;
    this._editAutoLoadCompositionDepth.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this._radioButtonAutoLoadCompositionDepth.AutoSize = true;
    this._radioButtonAutoLoadCompositionDepth.Location = new Point(23, 78);
    this._radioButtonAutoLoadCompositionDepth.Name = "_radioButtonAutoLoadCompositionDepth";
    this._radioButtonAutoLoadCompositionDepth.Size = new Size(225, 17);
    this._radioButtonAutoLoadCompositionDepth.TabIndex = 2;
    this._radioButtonAutoLoadCompositionDepth.Text = "Загружать состав объектов на глубину";
    this._radioButtonAutoLoadCompositionDepth.UseVisualStyleBackColor = true;
    this._radioButtonAutoLoadCompositionDepth.Click += new EventHandler(this._radioButtonAutoLoadCompositionFull_CheckedChanged);
    this._radioButtonAutoLoadCompositionFull.AutoSize = true;
    this._radioButtonAutoLoadCompositionFull.Checked = true;
    this._radioButtonAutoLoadCompositionFull.Location = new Point(23, 53);
    this._radioButtonAutoLoadCompositionFull.Name = "_radioButtonAutoLoadCompositionFull";
    this._radioButtonAutoLoadCompositionFull.Size = new Size(209, 17);
    this._radioButtonAutoLoadCompositionFull.TabIndex = 1;
    this._radioButtonAutoLoadCompositionFull.TabStop = true;
    this._radioButtonAutoLoadCompositionFull.Text = "Загружать полный состав объектов";
    this._radioButtonAutoLoadCompositionFull.UseVisualStyleBackColor = true;
    this._radioButtonAutoLoadCompositionFull.CheckedChanged += new EventHandler(this._radioButtonAutoLoadCompositionFull_CheckedChanged);
    this._radioButtonAutoLoadCompositionNone.AutoSize = true;
    this._radioButtonAutoLoadCompositionNone.Location = new Point(23, 103);
    this._radioButtonAutoLoadCompositionNone.Name = "_radioButtonAutoLoadCompositionNone";
    this._radioButtonAutoLoadCompositionNone.Size = new Size(184, 17);
    this._radioButtonAutoLoadCompositionNone.TabIndex = 4;
    this._radioButtonAutoLoadCompositionNone.Text = "Не загружать состав объектов";
    this._radioButtonAutoLoadCompositionNone.UseVisualStyleBackColor = true;
    this._radioButtonAutoLoadCompositionNone.Click += new EventHandler(this._radioButtonAutoLoadCompositionFull_CheckedChanged);
    this._checkBoxCheckAllObjectsOnLoad.AutoSize = true;
    this._checkBoxCheckAllObjectsOnLoad.Checked = true;
    this._checkBoxCheckAllObjectsOnLoad.CheckState = CheckState.Checked;
    this._checkBoxCheckAllObjectsOnLoad.Location = new Point(23, 24);
    this._checkBoxCheckAllObjectsOnLoad.Name = "_checkBoxCheckAllObjectsOnLoad";
    this._checkBoxCheckAllObjectsOnLoad.Size = new Size(143, 17);
    this._checkBoxCheckAllObjectsOnLoad.TabIndex = 0;
    this._checkBoxCheckAllObjectsOnLoad.Text = "Отметить все объекты";
    this._checkBoxCheckAllObjectsOnLoad.UseVisualStyleBackColor = true;
    this._panelCommonOptions.Controls.Add((Control) this._checkBoxBackgroundVisibleObjectsCompositionLoad);
    this._panelCommonOptions.Dock = DockStyle.Top;
    this._panelCommonOptions.Location = new Point(0, 0);
    this._panelCommonOptions.Name = "_panelCommonOptions";
    this._panelCommonOptions.Size = new Size(361, 40);
    this._panelCommonOptions.TabIndex = 0;
    this._groupBoxWarningIf.Controls.Add((Control) this._editWarningWhenCheckedCountMoreThanCount);
    this._groupBoxWarningIf.Controls.Add((Control) this._checkBoxWarningWhenCheckedCountMoreThan);
    this._groupBoxWarningIf.Controls.Add((Control) this._checkBoxWarningWhenCheckedNotLoaded);
    this._groupBoxWarningIf.Dock = DockStyle.Fill;
    this._groupBoxWarningIf.Location = new Point(0, 180);
    this._groupBoxWarningIf.Name = "_groupBoxWarningIf";
    this._groupBoxWarningIf.Size = new Size(361, 90);
    this._groupBoxWarningIf.TabIndex = 2;
    this._groupBoxWarningIf.TabStop = false;
    this._groupBoxWarningIf.Text = "Предупреждать, если";
    this._editWarningWhenCheckedCountMoreThanCount.BackColor = SystemColors.Window;
    this._editWarningWhenCheckedCountMoreThanCount.Location = new Point(271, 23);
    this._editWarningWhenCheckedCountMoreThanCount.Maximum = new Decimal(new int[4]
    {
      999999,
      0,
      0,
      0
    });
    this._editWarningWhenCheckedCountMoreThanCount.Name = "_editWarningWhenCheckedCountMoreThanCount";
    this._editWarningWhenCheckedCountMoreThanCount.Size = new Size(64 /*0x40*/, 20);
    this._editWarningWhenCheckedCountMoreThanCount.TabIndex = 1;
    this._editWarningWhenCheckedCountMoreThanCount.Value = new Decimal(new int[4]
    {
      1000,
      0,
      0,
      0
    });
    this._checkBoxWarningWhenCheckedCountMoreThan.AutoSize = true;
    this._checkBoxWarningWhenCheckedCountMoreThan.Checked = true;
    this._checkBoxWarningWhenCheckedCountMoreThan.CheckState = CheckState.Checked;
    this._checkBoxWarningWhenCheckedCountMoreThan.Location = new Point(23, 25);
    this._checkBoxWarningWhenCheckedCountMoreThan.Name = "_checkBoxWarningWhenCheckedCountMoreThan";
    this._checkBoxWarningWhenCheckedCountMoreThan.Size = new Size(239, 17);
    this._checkBoxWarningWhenCheckedCountMoreThan.TabIndex = 0;
    this._checkBoxWarningWhenCheckedCountMoreThan.Text = "Число отмеченных объектов больше, чем";
    this._checkBoxWarningWhenCheckedCountMoreThan.UseVisualStyleBackColor = true;
    this._checkBoxWarningWhenCheckedCountMoreThan.CheckedChanged += new EventHandler(this._checkBoxWarningWhenCheckedCountMoreThan_CheckedChanged);
    this._checkBoxWarningWhenCheckedNotLoaded.AutoSize = true;
    this._checkBoxWarningWhenCheckedNotLoaded.Checked = true;
    this._checkBoxWarningWhenCheckedNotLoaded.CheckState = CheckState.Checked;
    this._checkBoxWarningWhenCheckedNotLoaded.Location = new Point(23, 52);
    this._checkBoxWarningWhenCheckedNotLoaded.Name = "_checkBoxWarningWhenCheckedNotLoaded";
    this._checkBoxWarningWhenCheckedNotLoaded.Size = new Size(277, 17);
    this._checkBoxWarningWhenCheckedNotLoaded.TabIndex = 2;
    this._checkBoxWarningWhenCheckedNotLoaded.Text = "Отмечены объекты, состав которых не загружен";
    this._checkBoxWarningWhenCheckedNotLoaded.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(361, 308);
    this.Controls.Add((Control) this._groupBoxWarningIf);
    this.Controls.Add((Control) this._groupBoxOnLoad);
    this.Controls.Add((Control) this._panelCommonOptions);
    this.Name = nameof (SelectObjectCompositionsSettingsForm);
    this.Text = "Настройки выбора объектов из состава";
    this.Controls.SetChildIndex((Control) this._panelCommonOptions, 0);
    this.Controls.SetChildIndex((Control) this._pnlDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._bevelDialogButtons, 0);
    this.Controls.SetChildIndex((Control) this._groupBoxOnLoad, 0);
    this.Controls.SetChildIndex((Control) this._groupBoxWarningIf, 0);
    this._pnlDialogButtons.ResumeLayout(false);
    this._panelBtns.ResumeLayout(false);
    this._groupBoxOnLoad.ResumeLayout(false);
    this._groupBoxOnLoad.PerformLayout();
    this._editAutoLoadCompositionDepth.EndInit();
    this._panelCommonOptions.ResumeLayout(false);
    this._panelCommonOptions.PerformLayout();
    this._groupBoxWarningIf.ResumeLayout(false);
    this._groupBoxWarningIf.PerformLayout();
    this._editWarningWhenCheckedCountMoreThanCount.EndInit();
    this.ResumeLayout(false);
  }
}
