// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.FilterDialogForm
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Project.Controls.Properties;
using Intermech.Project.Evaluator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Project.Controls;

public class FilterDialogForm : Form
{
  private int _top = 15;
  private readonly int _vSpacing = 7;
  [CanBeNull]
  public ClientProject Project;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel _buttonsPanel;
  private Button _cancButton;
  private Button _okButton;

  [NotNull]
  protected Panel ButtonsPanel
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._buttonsPanel.CheckInitializedIn<Panel>((object) this);
    }
  }

  [NotNull]
  protected Button CancButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._cancButton.CheckInitializedIn<Button>((object) this);
    }
  }

  [NotNull]
  protected Button OkButton
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this._okButton.CheckInitializedIn<Button>((object) this);
    }
  }

  public static bool Query([NotNull] TaskFilter tf, [NotNull] ClientProject project)
  {
    using (FilterDialogForm filterDialogForm = new FilterDialogForm())
    {
      filterDialogForm.Project = project;
      filterDialogForm.Load(tf);
      int num = filterDialogForm.ShowDialog() == DialogResult.OK ? 1 : 0;
      if (num != 0)
        filterDialogForm.Save();
      return num != 0;
    }
  }

  public FilterDialogForm() => this.InitializeComponent();

  [NotNull]
  private Control AddControl([NotNull] string text, [NotNull] Control editControl)
  {
    Padding padding;
    Size clientSize;
    if (editControl.Text != text)
    {
      Label label1 = new Label();
      label1.Parent = (Control) this;
      Label label2 = label1;
      padding = this.Padding;
      int left1 = padding.Left;
      label2.Left = left1;
      label1.Top = this._top;
      label1.Text = text;
      Label label3 = label1;
      clientSize = this.ClientSize;
      int width = clientSize.Width;
      padding = this.Padding;
      int left2 = padding.Left;
      int num1 = width - left2;
      padding = this.Padding;
      int right = padding.Right;
      int num2 = num1 - right;
      label3.Width = num2;
      label1.AutoSize = true;
      label1.FlatStyle = FlatStyle.System;
      this._top += label1.Height;
      this._top += this._vSpacing;
    }
    Control control1 = editControl;
    control1.Parent = (Control) this;
    Control control2 = control1;
    padding = this.Padding;
    int left3 = padding.Left;
    control2.Left = left3;
    control1.Top = this._top;
    Control control3 = control1;
    clientSize = this.ClientSize;
    int width1 = clientSize.Width;
    padding = this.Padding;
    int left4 = padding.Left;
    int num3 = width1 - left4;
    padding = this.Padding;
    int right1 = padding.Right;
    int num4 = num3 - right1;
    control3.Width = num4;
    this._top += control1.Height;
    this._top += 2 * this._vSpacing;
    clientSize = this.ClientSize;
    int height = clientSize.Height;
    padding = this.Padding;
    int bottom = padding.Bottom;
    this.Height -= height - bottom - this.ButtonsPanel.Height - this._top;
    if (this.ActiveControl == null)
      this.ActiveControl = control1;
    return control1;
  }

  [NotNull]
  private Control AddControl([NotNull] Expression e)
  {
    string text = "?";
    string input = e.Value.ToString();
    Match match = Expression.InputFormatRegex.Match(input);
    if (match.Success)
      text = match.Groups[1].Value;
    Control editControl = (Control) null;
    if (e.Property != null)
    {
      if (e.Property.Name == "Assignments")
      {
        ComboBox comboBox = new ComboBox();
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        if (this.Project != null)
          comboBox.Items.AddRange((object[]) this.Project.AllResources.ToArray());
        comboBox.Sorted = true;
        editControl = (Control) comboBox;
      }
      else
      {
        System.Type propType = e.Property.PropType;
        if (propType == typeof (bool))
        {
          editControl = (Control) new CheckBox();
          editControl.Text = text;
        }
        else if (propType == typeof (DateTime))
        {
          DateTimePicker dateTimePicker = new DateTimePicker();
          if (this.Project != null)
          {
            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = this.Project.DisplayOptions.PickerDateFormat;
          }
          editControl = (Control) dateTimePicker;
        }
      }
    }
    if (editControl == null)
      editControl = (Control) new TextBox();
    editControl.Tag = (object) e;
    return this.AddControl(text, editControl);
  }

  public void Load([NotNull] TaskFilter tf)
  {
    this.Text = tf.Name;
    foreach (Expression expression in (List<Expression>) tf.Expressions)
    {
      if (expression.RequiresInput)
        this.AddControl(expression);
    }
  }

  [NotNull]
  private static object GetValue([NotNull] Control c)
  {
    switch (c)
    {
      case ComboBox comboBox:
        long num;
        switch (comboBox.SelectedItem)
        {
          case Resource resource:
            return (object) resource.ObjectID;
          case IDInfo idInfo:
            num = idInfo.ID;
            break;
          default:
            num = 0L;
            break;
        }
        return (object) num;
      case CheckBox checkBox:
        return (object) checkBox.Checked;
      case DateTimePicker dateTimePicker:
        return (object) dateTimePicker.Value;
      default:
        return (object) c.Text;
    }
  }

  public void Save()
  {
    foreach (Control control in (ArrangedElementCollection) this.Controls)
    {
      if (control.Tag is Expression tag)
        tag.Value = FilterDialogForm.GetValue(control);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilterDialogForm));
    this._buttonsPanel = new Panel();
    this._cancButton = new Button();
    this._okButton = new Button();
    this._buttonsPanel.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._buttonsPanel, "_buttonsPanel");
    this._buttonsPanel.BackColor = Color.Transparent;
    this._buttonsPanel.Controls.Add((Control) this._cancButton);
    this._buttonsPanel.Controls.Add((Control) this._okButton);
    this._buttonsPanel.Name = "_buttonsPanel";
    componentResourceManager.ApplyResources((object) this._cancButton, "_cancButton");
    this._cancButton.DialogResult = DialogResult.Cancel;
    this._cancButton.ImageKey = Resources.False;
    this._cancButton.Name = "_cancButton";
    componentResourceManager.ApplyResources((object) this._okButton, "_okButton");
    this._okButton.DialogResult = DialogResult.OK;
    this._okButton.ImageKey = Resources.False;
    this._okButton.Name = "_okButton";
    this.AcceptButton = (IButtonControl) this._okButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this._cancButton;
    this.Controls.Add((Control) this._buttonsPanel);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FilterDialogForm);
    this.ShowInTaskbar = false;
    this._buttonsPanel.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
