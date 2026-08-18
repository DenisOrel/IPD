// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.VarCustomEditors
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

internal class VarCustomEditors : List<VarCustomEditor>
{
  private VarCustomEditor _editor;
  private VarType _varType;

  public VarCustomEditor FindEditor(VarType vt)
  {
    foreach (VarCustomEditor editor in (List<VarCustomEditor>) this)
    {
      if (editor.Type == vt || editor.Type == VarType.Unknown)
        return editor;
    }
    return (VarCustomEditor) null;
  }

  public Control Control => this._editor?.Control;

  public bool Enabled
  {
    get => this.Count > 0 && this[0].Control != null && this[0].Control.Enabled;
    set
    {
      foreach (VarCustomEditor varCustomEditor in (List<VarCustomEditor>) this)
      {
        if (varCustomEditor.Control != null)
          varCustomEditor.Control.Enabled = value;
      }
    }
  }

  public VarType VarType
  {
    get => this._varType;
    set
    {
      if (this._editor?.Control != null)
        this._editor.Control.Visible = false;
      this._varType = value;
      this._editor = this.FindEditor(this._varType);
      if (this._editor?.Control == null)
        return;
      this._editor.Control.Visible = true;
    }
  }

  public string Value
  {
    get
    {
      if (this._editor == null)
        return string.Empty;
      Control control = this._editor.Control;
      if (control == null)
        return string.Empty;
      if (this._editor.Type == VarType.DateTime)
        return ((DateTimeWithNull) control).DateTime;
      if (this._editor.Type == VarType.Boolean && control is ComboBox comboBox)
        return comboBox.SelectedIndex.ToString();
      if (this._editor.Type != VarType.Archive)
        return control.Text;
      return control.Tag == null ? string.Empty : ((Guid) control.Tag).ToString();
    }
    set
    {
      if (this._editor == null)
        return;
      Control control = this._editor.Control;
      if (control == null)
        return;
      if (this._editor.Type == VarType.DateTime)
        ((DateTimeWithNull) control).DateTime = value;
      if (this._editor.Type == VarType.Boolean && control is ComboBox)
      {
        int num = 0;
        try
        {
          num = Convert.ToInt32(Convert.ToBoolean(value));
        }
        catch
        {
          try
          {
            num = Convert.ToInt32(value);
          }
          catch
          {
          }
        }
        (control as ComboBox).SelectedIndex = num;
      }
      else if (this._editor.Type == VarType.Archive)
      {
        if (string.IsNullOrEmpty(value))
          return;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          try
          {
            ObjectSystemPropertiesEx systemPropertiesEx = sessionKeeper.Session.GetObjectSystemPropertiesEx(new Guid(value), false);
            if (systemPropertiesEx != null)
            {
              control.Text = systemPropertiesEx.Caption;
              control.Tag = (object) systemPropertiesEx.VersionGuid;
            }
            else
            {
              control.Tag = (object) null;
              control.Text = "???";
            }
          }
          catch
          {
            control.Tag = (object) null;
            control.Text = "???";
          }
        }
      }
      else
        control.Text = value;
    }
  }

  public bool Visible
  {
    get => this._editor?.Control != null && this._editor.Control.Visible;
    set
    {
      if (this._editor?.Control == null)
        return;
      this._editor.Control.Visible = value;
    }
  }
}
