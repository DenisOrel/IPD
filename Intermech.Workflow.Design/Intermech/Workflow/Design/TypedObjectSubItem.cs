// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.TypedObjectSubItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>
/// Используется для хранения типизированных колонов в ListView, в итоге получаем правильную сортировку
/// </summary>
public class TypedObjectSubItem : ListViewItem.ListViewSubItem
{
  protected object _object;

  public TypedObjectSubItem(object obj)
  {
    this._object = obj;
    this.Text = obj.ToString();
  }

  /// <summary>
  /// Используется, когда среди типизированных данных нужно вставить данные, у которых отображение и значение (использумое для сортировки) отличаются
  /// </summary>
  public TypedObjectSubItem(object obj, string text)
    : this(obj)
  {
    this.Text = text;
  }

  public int Compare(object obj)
  {
    if (obj is TypedObjectSubItem)
      obj = ((TypedObjectSubItem) obj)._object;
    if (this._object.GetType() == obj.GetType() && this._object is IComparable comparable)
      return comparable.CompareTo(obj);
    if (obj is ListViewItem.ListViewSubItem)
      obj = (object) (obj as ListViewItem.ListViewSubItem).Text;
    return string.Compare(this._object.ToString(), obj.ToString());
  }
}
