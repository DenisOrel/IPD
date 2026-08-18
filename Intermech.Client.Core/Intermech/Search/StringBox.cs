
// Type: Intermech.Search.StringBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class StringBox : Box<string>
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public StringBox() => this.InitializeComponent();

  public override bool IsEmpty => string.IsNullOrEmpty(this.TypedValue);

  protected override void Edit()
  {
    using (TextBoxWithButtonsForm boxWithButtonsForm = new TextBoxWithButtonsForm())
    {
      boxWithButtonsForm.AcceptButton = (IButtonControl) null;
      boxWithButtonsForm.TextBox.MaxLength = this.TextBoxMaxLength;
      boxWithButtonsForm.TextBox.Text = this.TypedValue;
      if (boxWithButtonsForm.ShowDialog() != DialogResult.OK)
        return;
      this.Value = (object) boxWithButtonsForm.TextBox.Text;
      this.HandleKeyUp(Keys.Return);
    }
  }

  protected override bool TryParse(string text, out string result)
  {
    result = text;
    return true;
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
    this.Name = nameof (StringBox);
    this.ResumeLayout(false);
  }
}
