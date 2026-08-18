
// Type: Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors.AttributeProcessorDropDownEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Client.Core.PropertyEditors.AttrProcessor.Editors;

public class AttributeProcessorDropDownEditorForm : DropDownForm, IWindowsFormsEditorService
{
  private Rectangle editorBounds = Rectangle.Empty;
  private Control editor;
  private Point startLocation;
  private IContainer components;

  /// <summary>Конструктор.</summary>
  public AttributeProcessorDropDownEditorForm(Control editor, Rectangle? editorBounds)
  {
    this.editor = editor;
    if (!editorBounds.HasValue)
      return;
    this.editorBounds = editorBounds.Value;
  }

  public Point StartLocation
  {
    get => this.startLocation;
    set => this.startLocation = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public void CloseDropDown() => this.Close();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="control"></param>
  public void DropDownControl(Control control)
  {
    DropDownForm dropDownForm = (DropDownForm) this;
    dropDownForm.ManageContainedControlDisposal = false;
    dropDownForm.BackColor = this.BackColor;
    dropDownForm.ForeColor = this.ForeColor;
    control.CreateControl();
    dropDownForm.FormBorderStyle = FormBorderStyle.None;
    if (control.GetType().Name == "DateTimeUI")
      dropDownForm.Width = control.Width + 4;
    else
      dropDownForm.Width = Math.Max(this.Width, control.Width + 4);
    dropDownForm.Height = control.Height + 4;
    dropDownForm.ContainedControl = control;
    dropDownForm.ShowModal(this.editor, this.editorBounds);
    dropDownForm.Close();
    dropDownForm.Dispose();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="dialog"></param>
  /// <returns></returns>
  public DialogResult ShowDialog(Form dialog) => dialog.ShowDialog();
}
