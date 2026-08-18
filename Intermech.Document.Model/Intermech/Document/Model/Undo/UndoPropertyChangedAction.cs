// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.Undo.UndoPropertyChangedAction
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

internal class UndoPropertyChangedAction : IUndoAction, ICloneAction
{
  private readonly IUndoManager manager;
  private string nodeId;
  private string cloneId;
  private readonly string propertyName;
  private readonly object oldValue;
  private readonly object newValue;
  private readonly string caption;
  private DocumentTreeNode clone;

  public UndoPropertyChangedAction(
    IUndoManager manager,
    DocumentTreeNode node,
    string propertyName,
    object oldValue,
    object newValue)
  {
    this.manager = manager;
    this.nodeId = node.Id;
    this.propertyName = propertyName;
    this.oldValue = !(oldValue is ICloneable cloneable1) ? oldValue : cloneable1.Clone();
    this.newValue = !(newValue is ICloneable cloneable2) ? newValue : cloneable2.Clone();
    object[] attributes = FindFieldHelper.FindAttributes(node.GetType(), typeof (DisplayNameAttribute), propertyName);
    this.caption = LocalizationHolder.rm.GetString("Document.Model_559");
    this.caption = attributes == null || attributes.Length == 0 ? this.caption + propertyName : this.caption + (attributes[0] as DisplayNameAttribute).DisplayName;
    this.caption += "'";
  }

  private UndoPropertyChangedAction(
    IUndoManager manager,
    string caption,
    string nodeId,
    string propertyName,
    object oldValue,
    object newValue)
  {
    this.manager = manager;
    this.nodeId = nodeId;
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
    if (this.oldValue == null && this.newValue == null)
      return false;
    VisualNode document = this.manager.Document;
    if (document == null)
      return false;
    DocumentTreeNode node = document.FindNode(this.nodeId);
    bool flag = false;
    if (node != null)
    {
      PropertyInfo property = FindFieldHelper.FindProperty(node.GetType(), this.propertyName);
      if (property != (PropertyInfo) null)
      {
        property.SetValue((object) node, this.oldValue, (object[]) null);
        flag = true;
      }
      else
      {
        FieldInfo field = FindFieldHelper.FindField(node.GetType(), this.propertyName);
        if (field != (FieldInfo) null)
        {
          field.SetValue((object) node, this.oldValue);
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
    if (this.nodeId == oldValue)
      this.nodeId = newValue;
    if (!(this.cloneId == oldValue))
      return;
    this.cloneId = newValue;
  }

  public IUndoAction CreateRedoAction()
  {
    return (IUndoAction) new UndoPropertyChangedAction(this.manager, this.caption, this.nodeId, this.propertyName, this.newValue, this.oldValue);
  }

  DocumentTreeNode ICloneAction.Clone
  {
    get => this.clone;
    set
    {
      this.clone = value;
      if (this.clone == null)
        return;
      this.cloneId = this.clone.Id;
    }
  }

  string ICloneAction.NodeId
  {
    get => this.nodeId;
    set => this.nodeId = value;
  }

  string ICloneAction.CloneId
  {
    get => this.cloneId;
    set => this.cloneId = value;
  }
}
