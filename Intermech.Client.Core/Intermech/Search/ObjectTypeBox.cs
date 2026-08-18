
// Type: Intermech.Search.ObjectTypeBox
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Search.Utilities;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class ObjectTypeBox : Box<int>
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ObjectTypeBox() => this.InitializeComponent();

  protected override int DefaultValue => -1;

  protected override bool SupportedTextInput => false;

  protected override string GetTextBoxText()
  {
    if (this.IsEmpty)
      return string.Empty;
    IMSObjectType objectType = !ObjectTypeHelper.IsUnknownObjectTypeID(this.TypedValue) ? MetaDataHelper.GetObjectType(this.TypedValue) : (IMSObjectType) null;
    return objectType == null ? string.Empty : objectType.ObjectTypeName;
  }

  protected override void Edit()
  {
    this.Value = (object) ObjectTypeClientHelper.SelectObjectType(this.TypedValue, false);
    this.HandleKeyUp(Keys.Return);
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
    this.Name = nameof (ObjectTypeBox);
    this.Size = new Size(250, 20);
    this.ResumeLayout(false);
  }
}
