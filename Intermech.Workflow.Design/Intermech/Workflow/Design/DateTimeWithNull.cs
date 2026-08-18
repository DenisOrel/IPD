// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.DateTimeWithNull
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class DateTimeWithNull : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private DateTimePicker dateTimePicker;
  private CheckBox dateTimeIsNow;

  public DateTimeWithNull() => this.InitializeComponent();

  public string DateTime
  {
    get
    {
      return this.dateTimeIsNow.Checked ? string.Empty : this.dateTimePicker.Value.ToString("dd.MM.yyyy H:mm:ss");
    }
    set
    {
      if (!string.IsNullOrEmpty(value))
      {
        System.DateTime result;
        if (System.DateTime.TryParseExact(value, "dd.MM.yyyy H:mm:ss", (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
          this.dateTimePicker.Value = result;
        else
          this.dateTimePicker.Value = Convert.ToDateTime(value);
      }
      else
        this.dateTimeIsNow.Checked = true;
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
    this.dateTimePicker = new DateTimePicker();
    this.dateTimeIsNow = new CheckBox();
    this.SuspendLayout();
    this.dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
    this.dateTimePicker.CustomFormat = "dd.MM.yyyy H:mm:ss";
    this.dateTimePicker.Format = DateTimePickerFormat.Custom;
    this.dateTimePicker.Location = new Point(0, 0);
    this.dateTimePicker.Name = "dateTimePicker";
    this.dateTimePicker.Size = new Size(137, 20);
    this.dateTimePicker.TabIndex = 1;
    this.dateTimeIsNow.Anchor = AnchorStyles.Right;
    this.dateTimeIsNow.AutoSize = true;
    this.dateTimeIsNow.Location = new Point(143, 3);
    this.dateTimeIsNow.Name = "dateTimeIsNow";
    this.dateTimeIsNow.Size = new Size(97, 17);
    this.dateTimeIsNow.TabIndex = 2;
    this.dateTimeIsNow.Text = "Текущая дата";
    this.dateTimeIsNow.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.dateTimeIsNow);
    this.Controls.Add((Control) this.dateTimePicker);
    this.Name = nameof (DateTimeWithNull);
    this.Size = new Size(234, 21);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
