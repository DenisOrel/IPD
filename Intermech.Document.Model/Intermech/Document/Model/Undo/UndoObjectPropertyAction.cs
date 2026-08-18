// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoObjectPropertyAction
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.Reflection;

#nullable disable
namespace Intermech.Document.Model.Undo;

public class UndoObjectPropertyAction : IUndoAction
{
  private readonly IUndoManager manager;
  private readonly object obj;
  private readonly string propertyName;
  private readonly object oldValue;
  private readonly object newValue;
  private readonly string caption;

  public UndoObjectPropertyAction(
    IUndoManager manager,
    object obj,
    string propertyName,
    object oldValue,
    object newValue)
  {
    this.manager = manager;
    this.obj = obj;
    this.propertyName = propertyName;
    this.oldValue = !(oldValue is ICloneable cloneable1) ? oldValue : cloneable1.Clone();
    this.newValue = !(newValue is ICloneable cloneable2) ? newValue : cloneable2.Clone();
    object[] attributes = FindFieldHelper.FindAttributes(obj.GetType(), typeof (DisplayNameAttribute), propertyName);
    this.caption = LocalizationHolder.rm.GetString("Document.Model_559");
    this.caption = attributes == null || attributes.Length == 0 ? this.caption + propertyName : this.caption + (attributes[0] as DisplayNameAttribute).DisplayName;
    this.caption += "'";
  }

  private UndoObjectPropertyAction(
    IUndoManager manager,
    string caption,
    object obj,
    string propertyName,
    object oldValue,
    object newValue)
  {
    this.manager = manager;
    this.obj = obj;
    this.propertyName = propertyName;
    this.caption = caption;
    this.oldValue = !(oldValue is ICloneable cloneable1) ? oldValue : cloneable1.Clone();
    if (newValue is ICloneable cloneable2)
      this.newValue = cloneable2.Clone();
    else
      this.newValue = newValue;
  }

  public override string ToString() => this.propertyName;

  public bool DoAction()
  {
    VisualNode document = this.manager.Document;
    if (document == null)
      return false;
    bool flag = false;
    if (this.obj != null)
    {
      PropertyInfo property = FindFieldHelper.FindProperty(this.obj.GetType(), this.propertyName);
      if (property != (PropertyInfo) null)
      {
        property.SetValue(this.obj, this.oldValue, (object[]) null);
        flag = true;
      }
      else
      {
        FieldInfo field = FindFieldHelper.FindField(this.obj.GetType(), this.propertyName);
        if (field != (FieldInfo) null)
        {
          field.SetValue(this.obj, this.oldValue);
          document.UpdateLayout(true);
          flag = true;
        }
      }
    }
    return flag;
  }

  public string Caption => this.caption;

  public void IdChanged(string oldValue, string newValue)
  {
  }

  public IUndoAction CreateRedoAction()
  {
    return (IUndoAction) new UndoObjectPropertyAction(this.manager, this.caption, this.obj, this.propertyName, this.newValue, this.oldValue);
  }
}
