
// Type: Intermech.Search.DateTimeComboBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class DateTimeComboBox : BaseComboBox<DateTime>
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public DateTimeComboBox()
  {
    this.InitializeComponent();
    this.Format = DateTimePickerFormat.Long;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public DateTimePickerFormat Format { get; set; }

  protected override bool SupportedTextInput => false;

  protected override void Edit()
  {
    using (MonthCalendarWithButtonsForm calendarWithButtonsForm = new MonthCalendarWithButtonsForm())
    {
      if (calendarWithButtonsForm.ShowDialog() != DialogResult.OK)
        return;
      this.Value = (object) calendarWithButtonsForm.MonthCalendar.SelectionStart;
    }
  }

  protected override string GetDisplayValue(DateTime item)
  {
    switch (this.Format)
    {
      case DateTimePickerFormat.Long:
        return item.ToLongDateString();
      case DateTimePickerFormat.Short:
        return item.ToShortDateString();
      case DateTimePickerFormat.Time:
        return item.ToLongTimeString();
      default:
        throw new NotSupportedException();
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
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (DateTimeComboBox);
    this.ResumeLayout(false);
  }
}
