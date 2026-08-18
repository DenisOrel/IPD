// Decompiled with JetBrains decompiler
// Type: Intermech.ControlFuncs
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech;

public class ControlFuncs
{
  public static string NullablePickerNullText = LocalizationHolder.rm.GetString("Interfaces.Workflow_9");

  public static void SetControlsReadOnly(Control parent, bool _ro)
  {
    ControlFuncs.SetControlsReadOnly(parent, _ro, (List<Control>) null);
  }

  public static void SetControlsReadOnly(Control parent, bool _ro, List<Control> ExcludeControls = null)
  {
    Form form = (Form) null;
    foreach (Control control in (ArrangedElementCollection) parent.Controls)
    {
      if (ExcludeControls == null || ExcludeControls.IndexOf(control) == -1)
      {
        switch (control)
        {
          case IReadOnlyEnabledControl _:
            (control as IReadOnlyEnabledControl).ReadOnly = _ro;
            goto label_12;
          case TextBox _:
            Color backColor = control.BackColor;
            (control as TextBox).ReadOnly = _ro;
            control.BackColor = backColor;
            goto label_12;
          case Button _:
            if (form == null)
              form = control.FindForm();
            if (form != null && form.AcceptButton != control && form.CancelButton != control)
            {
              control.Enabled = !_ro;
              goto label_12;
            }
            goto label_12;
          case ListView _:
            (control as ListView).LabelEdit = !_ro;
            goto label_12;
          case GroupBox _:
          case Panel _:
          case Form _:
          case TabPage _:
          case TabControl _:
          case Label _:
label_12:
            if (control.HasChildren)
            {
              ControlFuncs.SetControlsReadOnly(control, _ro, ExcludeControls);
              continue;
            }
            continue;
          default:
            control.Enabled = !_ro;
            goto label_12;
        }
      }
    }
  }

  public static void SetNullablePickerValue(DateTimePicker dtpicker, DateTime dt)
  {
    dtpicker.CloseUp -= new EventHandler(ControlFuncs.dtpicker_CloseUp);
    dtpicker.CloseUp += new EventHandler(ControlFuncs.dtpicker_CloseUp);
    dtpicker.Format = DateTimePickerFormat.Custom;
    if (dt == DateTime.MinValue)
    {
      if (dtpicker.Tag == null)
        dtpicker.Tag = (object) dtpicker.CustomFormat;
      dtpicker.CustomFormat = ControlFuncs.NullablePickerNullText;
    }
    else
    {
      string tag = (string) dtpicker.Tag;
      if (tag != null)
        dtpicker.CustomFormat = tag;
      dtpicker.Value = dt;
    }
  }

  protected static void dtpicker_CloseUp(object sender, EventArgs e)
  {
    DateTimePicker dateTimePicker = (DateTimePicker) sender;
    string tag = (string) dateTimePicker.Tag;
    if (tag == null)
      return;
    dateTimePicker.CustomFormat = tag;
  }

  public static DateTime GetNullablePickerValue(DateTimePicker dtpicker)
  {
    return dtpicker.CustomFormat == ControlFuncs.NullablePickerNullText ? DateTime.MinValue : dtpicker.Value.AddSeconds((double) -dtpicker.Value.Second);
  }

  public static void EnumToCombo(ComboBox combo, Enum Value)
  {
    ControlFuncs.EnumToCombo(combo, Value, (List<Enum>) null);
  }

  public static void EnumToCombo(ComboBox combo, Enum Value, List<Enum> skip)
  {
    combo.DataSource = (object) SimpleFuncs.EnumToList(Value.GetType(), skip);
    combo.DisplayMember = nameof (Value);
    combo.ValueMember = "Key";
    combo.SelectedItem = (object) SimpleFuncs.EnumToKVP(Value);
  }

  [DllImport("User32.dll")]
  private static extern short GetAsyncKeyState(Keys vKey);

  public static bool IsKeyPressed(Keys key)
  {
    return ((int) ControlFuncs.GetAsyncKeyState(key) & 32768 /*0x8000*/) == 32768 /*0x8000*/;
  }

  public static float MeasureTextBoxHeight(TextBox box)
  {
    using (Graphics graphics = box.CreateGraphics())
    {
      Point point = new Point(3, 2);
      return graphics.MeasureString(box.Text, box.Font, box.ClientSize.Width - point.X * 2).Height + (float) (point.Y * 2);
    }
  }

  public static void CalcScrollBarsNeeded(TextBox box)
  {
    if ((int) ControlFuncs.MeasureTextBoxHeight(box) > box.ClientSize.Height)
      box.ScrollBars = ScrollBars.Vertical;
    else
      box.ScrollBars = ScrollBars.None;
  }

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  [return: MarshalAs(UnmanagedType.Bool)]
  public static extern bool ReleaseCapture();
}
