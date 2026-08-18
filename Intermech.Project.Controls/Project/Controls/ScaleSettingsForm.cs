// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ScaleSettingsForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Project.Controls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

public class ScaleSettingsForm : Form
{
  [CanBeNull]
  private ProjectDisplayOptions _options;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label _label1;
  private ComboBox _unitsBox;
  private ComboBox _dateFormatBox;
  private Label _label2;
  private Button _okButton;
  private Button _cancButton;

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label1
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label1.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox UnitsBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._unitsBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal ComboBox DateFormatBox
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._dateFormatBox.CheckInitializedIn<ComboBox>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Label Label2
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._label2.CheckInitializedIn<Label>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected internal Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  public ScaleSettingsForm()
  {
    this.InitializeComponent();
    foreach (ScaleType type in Enum.GetValues(typeof (ScaleType)))
      this.UnitsBox.Items.Add((object) new ScaleTypeItem(SimpleFuncs.GetEnumDescription((Enum) type), type));
  }

  private void ScaleSettingsForm_Load([CanBeNull] object sender, [NotNull] EventArgs e)
  {
  }

  private ScaleType ScaleType
  {
    get
    {
      ScaleTypeItem selectedItem = this.UnitsBox.SelectedItem as ScaleTypeItem;
      return this.UnitsBox.SelectedItem == null ? ScaleType.Weeks : selectedItem.Type;
    }
    set
    {
      foreach (ScaleTypeItem scaleTypeItem in this.UnitsBox.Items)
      {
        if (scaleTypeItem.Type == value)
        {
          this.UnitsBox.SelectedItem = (object) scaleTypeItem;
          break;
        }
      }
    }
  }

  [NotNull]
  private string Format
  {
    get
    {
      DateFormatItem selectedItem = this.DateFormatBox.SelectedItem as DateFormatItem;
      return this.DateFormatBox.SelectedItem == null ? string.Empty : selectedItem.Format;
    }
    set
    {
      foreach (DateFormatItem dateFormatItem in this.DateFormatBox.Items)
      {
        if (dateFormatItem.Format == value)
        {
          this.DateFormatBox.SelectedItem = (object) dateFormatItem;
          break;
        }
      }
    }
  }

  [NotNull]
  public ProjectDisplayOptions Options
  {
    set
    {
      this._options = value;
      this.ScaleType = this._options.ScaleType;
      string str;
      if (this._options.TopLevelFormat.TryGetValue(this.ScaleType, out str))
        this.Format = str ?? string.Empty;
      else
        this.DateFormatBox.SelectedIndex = -1;
    }
  }

  private void ScaleSettingsForm_FormClosing([CanBeNull] object sender, [NotNull] FormClosingEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK || this._options == null)
      return;
    this._options.ScaleType = this.ScaleType;
    this._options.TopLevelFormat[this.ScaleType] = this.Format;
    this._options.UpdateControls();
  }

  private void UnitsBox_SelectedValueChanged([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    this.DateFormatBox.Items.Clear();
    List<string> stringList;
    if (DefaultDateFormats.GanttFormats.TryGetValue(this.ScaleType, out stringList) && stringList != null)
    {
      foreach (string format in stringList)
        this.DateFormatBox.Items.Add((object) new DateFormatItem(format));
    }
    this.DateFormatBox.Enabled = stringList != null;
    if (!this.DateFormatBox.Enabled)
      return;
    this.DateFormatBox.SelectedIndex = 0;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ScaleSettingsForm));
    this._label1 = new Label();
    this._unitsBox = new ComboBox();
    this._dateFormatBox = new ComboBox();
    this._label2 = new Label();
    this._okButton = new Button();
    this._cancButton = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._label1, "_label1");
    this._label1.ImageKey = Resources.False;
    this._label1.Name = "_label1";
    componentResourceManager.ApplyResources((object) this._unitsBox, "_unitsBox");
    this._unitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._unitsBox.FormattingEnabled = true;
    this._unitsBox.Name = "_unitsBox";
    this._unitsBox.SelectedValueChanged += new EventHandler(this.UnitsBox_SelectedValueChanged);
    componentResourceManager.ApplyResources((object) this._dateFormatBox, "_dateFormatBox");
    this._dateFormatBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this._dateFormatBox.FormattingEnabled = true;
    this._dateFormatBox.Name = "_dateFormatBox";
    componentResourceManager.ApplyResources((object) this._label2, "_label2");
    this._label2.ImageKey = Resources.False;
    this._label2.Name = "_label2";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.ImageKey = Resources.False;
    this._okButton.Name = "_okButton";
    this._okButton.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this._cancButton, "_cancButton");
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.ImageKey = Resources.False;
    this._cancButton.Name = "_cancButton";
    this._cancButton.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.Controls.Add((Control) this._cancButton);
    this.Controls.Add((Control) this._okButton);
    this.Controls.Add((Control) this._dateFormatBox);
    this.Controls.Add((Control) this._label2);
    this.Controls.Add((Control) this._unitsBox);
    this.Controls.Add((Control) this._label1);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ScaleSettingsForm);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.ScaleSettingsForm_FormClosing);
    this.Load += new EventHandler(this.ScaleSettingsForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
