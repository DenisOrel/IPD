
// Type: Intermech.Search.MeasuredValueBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class MeasuredValueBox : Box<MeasuredValue>
{
  private static MeasureDescriptor[] DefaultMeasureDescriptors = new MeasureDescriptor[0];
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public MeasuredValueBox()
  {
    this.InitializeComponent();
    this.DefaultMeasureVersionID = 0L;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public MeasureDescriptor[] MeasureDescriptors { get; set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long DefaultMeasureVersionID { get; set; }

  protected override bool TryParse(string text, out MeasuredValue result)
  {
    return MeasuredValueHelper.TryParse(text, out result, this.DefaultMeasureVersionID, this.MeasureDescriptors);
  }

  protected override void Edit()
  {
    using (MeasureForm measureForm = new MeasureForm())
    {
      MeasuredValue typedValue = this.TypedValue;
      if (measureForm.ExecuteDialog(ref typedValue, this.MeasureDescriptors ?? MeasuredValueBox.DefaultMeasureDescriptors) != DialogResult.OK)
        return;
      this.Value = (object) typedValue;
      this.HandleKeyUp(Keys.Return);
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
    this.Margin = new Padding(0);
    this.Name = nameof (MeasuredValueBox);
    this.Size = new Size(320, 27);
    this.ResumeLayout(false);
  }
}
