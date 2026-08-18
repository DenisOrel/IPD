// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.MeasureTextBox
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using DevAge.ComponentModel.Validator;
using DevAge.Windows.Forms;
using Intermech.Interfaces;
using SourceGrid3;
using System;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>
/// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
/// </summary>
/// <param name="p_Type">The type of this model</param>
public class MeasureTextBox(System.Type p_Type) : SourceGrid3.Cells.Editors.TextBox(typeof (MeasuredValue))
{
  private long _physID = -1;

  public long PhysID
  {
    get => this._physID;
    set => this._physID = value;
  }

  /// <summary>
  /// This method is called just before the edit start. You can use this method to customize the editor with the cell informations.
  /// </summary>
  /// <param name="cellContext"></param>
  /// <param name="editorControl"></param>
  protected override void OnStartingEdit(CellContext cellContext, System.Windows.Forms.Control editorControl)
  {
    base.OnStartingEdit(cellContext, editorControl);
    TextBoxTyped textBoxTyped = (TextBoxTyped) editorControl;
    textBoxTyped.EnableAutoValidation = false;
    int column = cellContext.Position.Column;
    textBoxTyped.Validator = (IValidator) new ValidatorTypeConverter(typeof (MeasuredValue), (TypeConverter) new MeasureTypeConverter(this._physID));
    textBoxTyped.ValueChanged += new EventHandler(this.l_TxtBox_ValueChanged);
  }

  private void l_TxtBox_ValueChanged(object sender, EventArgs e)
  {
    TextBoxTyped textBoxTyped = (TextBoxTyped) sender;
    bool flag = false;
    if (textBoxTyped.Value != null)
    {
      if (textBoxTyped.Value is MeasuredValue)
      {
        MeasuredValue mValue = (MeasuredValue) textBoxTyped.Value;
        MeasureDescriptor descriptor = MeasureHelper.FindDescriptor(mValue);
        if (descriptor.Empty || descriptor.PhysicalQuantityID != this._physID)
          flag = true;
        if (mValue.Caption != "")
        {
          try
          {
            string[] strArray = mValue.Caption.Split(' ');
            mValue.Value = Convert.ToDouble(strArray[0]);
          }
          catch
          {
            flag = true;
          }
        }
      }
      else
        flag = true;
    }
    else
      flag = textBoxTyped.Text != "";
    if (flag)
      textBoxTyped.ForeColor = Color.Red;
    else
      textBoxTyped.ForeColor = Color.Black;
  }

  /// <summary>
  /// Set the specified value in the current editor control.
  /// </summary>
  /// <param name="editValue"></param>
  public override void SetEditValue(object editValue)
  {
    this.Control.Value = editValue;
    this.Control.SelectAll();
  }

  /// <summary>
  /// Returns the value inserted with the current editor control
  /// </summary>
  /// <returns></returns>
  public override object GetEditedValue() => this.Control.Value;
}
