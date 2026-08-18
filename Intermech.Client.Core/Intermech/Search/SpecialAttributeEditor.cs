
// Type: Intermech.Search.SpecialAttributeEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Search;

public sealed class SpecialAttributeEditor : AttributeEditor
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBoxWithCheckBoxAndButtons _textBoxWithCheckBoxAndButtons;

  public SpecialAttributeEditor()
  {
    this.InitializeComponent();
    this.SuspendLayout();
    this._textBoxWithCheckBoxAndButtons.SuspendAllLayout();
    this._textBoxWithCheckBoxAndButtons.CheckBox.Visible = false;
    this._textBoxWithCheckBoxAndButtons.TextBox.BorderStyle = BorderStyle.None;
    this._textBoxWithCheckBoxAndButtons.TextBox.Multiline = true;
    this._textBoxWithCheckBoxAndButtons.TextBox.ReadOnly = true;
    this._textBoxWithCheckBoxAndButtons.TextBox.WordWrap = false;
    this._textBoxWithCheckBoxAndButtons.EditButton.FlatAppearance.BorderSize = 0;
    this._textBoxWithCheckBoxAndButtons.EditButton.FlatStyle = FlatStyle.Flat;
    this._textBoxWithCheckBoxAndButtons.ClearButton.FlatAppearance.BorderSize = 0;
    this._textBoxWithCheckBoxAndButtons.ClearButton.FlatStyle = FlatStyle.Flat;
    this._textBoxWithCheckBoxAndButtons.EditButton.Click += new EventHandler(this.TextBoxWithCheckBoxAndButtonsEditButton_Click);
    this._textBoxWithCheckBoxAndButtons.ClearButton.Click += new EventHandler(this.TextBoxWithCheckBoxAndButtonsClearButton_Click);
    this._textBoxWithCheckBoxAndButtons.ResumeAllLayout(false);
    this.ResumeLayout(false);
  }

  protected override void DoSetValue()
  {
    if (this.AttributePropertyDescriber != null && this.ElementInfo != null)
    {
      object propDescriptorValue = this.AttributePropertyDescriber.GetPropDescriptorValue(this.ElementInfo, this.AttributeType.AttributeID, this.Value);
      if (propDescriptorValue != null)
        this._textBoxWithCheckBoxAndButtons.TextBox.Text = propDescriptorValue.ToString();
      else if (this.Value != null)
        this._textBoxWithCheckBoxAndButtons.TextBox.Text = this.Value.ToString();
      else
        this._textBoxWithCheckBoxAndButtons.TextBox.Text = (string) null;
    }
    else
      this._textBoxWithCheckBoxAndButtons.TextBox.Text = (string) null;
  }

  protected override void DoInitializeEditor()
  {
    this._textBoxWithCheckBoxAndButtons.ClearButton.Visible = this.AllowEmpty;
  }

  private void TextBoxWithCheckBoxAndButtonsEditButton_Click(object sender, EventArgs e)
  {
    if (this.AttributeType == null || this.UITypeEditor == null)
      return;
    using (ServiceContainer provider = new ServiceContainer())
    {
      using (DropDownEditorForm serviceInstance = new DropDownEditorForm())
      {
        provider.AddService(typeof (IWindowsFormsEditorService), (object) serviceInstance);
        ControlsContext context = new ControlsContext(new AttributeValues(this.AttributeType.AttributeID, this.Value)
        {
          AttributeName = this.AttributeType.Name
        }, this.AttributePropertyDescriber, this.ElementInfo);
        switch (this.UITypeEditor.GetEditStyle((ITypeDescriptorContext) context))
        {
          case UITypeEditorEditStyle.Modal:
          case UITypeEditorEditStyle.DropDown:
            object propDescriptorValue = this.AttributePropertyDescriber.GetPropDescriptorValue(this.ElementInfo, this.AttributeType.AttributeID, this.Value);
            object propertyValue = this.UITypeEditor.EditValue((ITypeDescriptorContext) context, (System.IServiceProvider) provider, propDescriptorValue);
            if (propertyValue == null)
              break;
            this.Value = this.AttributePropertyDescriber.GetAttributeValue(this.ElementInfo, this.AttributeType.AttributeID, propertyValue);
            this.HandleKeyUp(Keys.Return);
            break;
        }
      }
    }
  }

  private void TextBoxWithCheckBoxAndButtonsClearButton_Click(object sender, EventArgs e)
  {
    this.Value = (object) null;
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
    this._textBoxWithCheckBoxAndButtons = new TextBoxWithCheckBoxAndButtons();
    this.SuspendLayout();
    this._textBoxWithCheckBoxAndButtons.Dock = DockStyle.Fill;
    this._textBoxWithCheckBoxAndButtons.Location = new Point(0, 0);
    this._textBoxWithCheckBoxAndButtons.Name = "_textBoxWithCheckBoxAndButtons";
    this._textBoxWithCheckBoxAndButtons.Size = new Size(200, 20);
    this._textBoxWithCheckBoxAndButtons.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._textBoxWithCheckBoxAndButtons);
    this.Name = nameof (SpecialAttributeEditor);
    this.Size = new Size(200, 20);
    this.ResumeLayout(false);
  }
}
