
// Type: Intermech.Search.ObjectLinkAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Search;

public sealed class ObjectLinkAttributeEditor : SingleValueAttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ObjectLinkBox _objectLinkBox;

  public ObjectLinkAttributeEditor() => this.InitializeComponent();

  protected override void DoInitializeEditor()
  {
    base.DoInitializeEditor();
    if (this.AttributeType == null)
      return;
    this._objectLinkBox.ObjectTypeID = this.AttributeType.AttributeID != -8 ? (this.AttributeType.AttributeID != -14 ? (this.AttributeType.SizeType != 0L ? (int) this.AttributeType.SizeType : -1) : MetaDataHelper.GetObjectTypeID(Guid.Parse("cad00812-306c-11d8-b4e9-00304f19f545"))) : MetaDataHelper.GetObjectTypeID(Guid.Parse("cad00002-306c-11d8-b4e9-00304f19f545"));
    if (this.AttributeType.AttributeID == -8)
      this._objectLinkBox.AllowEmpty = false;
    else
      this._objectLinkBox.AllowEmpty = this.AllowEmpty;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  protected override ISingleValueEditor ValueEditor => (ISingleValueEditor) this._objectLinkBox;

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
    this._objectLinkBox = new ObjectLinkBox();
    this.SuspendLayout();
    this._objectLinkBox.Dock = DockStyle.Fill;
    this._objectLinkBox.Location = new Point(0, 0);
    this._objectLinkBox.Name = "_objectLinkBox";
    this._objectLinkBox.Size = new Size(200, 20);
    this._objectLinkBox.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._objectLinkBox);
    this.Name = nameof (ObjectLinkAttributeEditor);
    this.Size = new Size(200, 20);
    this.ResumeLayout(false);
  }
}
