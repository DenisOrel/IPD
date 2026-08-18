// Decompiled with JetBrains decompiler
// Type: Intermech.EditorsList
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System;
using System.Collections.Generic;
using System.Windows.Forms;

#nullable disable
namespace Intermech;

public class EditorsList : List<EditorInfo>
{
  public void RegisterEditor(Control form, long id, bool isEditMode)
  {
    if (this.FindEditor(id, isEditMode) != null)
      return;
    this.Add(new EditorInfo(form, id, isEditMode));
  }

  public void UnregisterEditor(Control form)
  {
    foreach (EditorInfo editorInfo in (List<EditorInfo>) this)
    {
      if (editorInfo.Form == form)
      {
        this.Remove(editorInfo);
        break;
      }
    }
  }

  public Control FindEditor(long id, bool isEditMode)
  {
    foreach (EditorInfo editorInfo in (List<EditorInfo>) this)
    {
      if (Math.Abs(editorInfo.ID) == Math.Abs(id) && editorInfo.IsEditMode == isEditMode)
        return editorInfo.Form;
    }
    return (Control) null;
  }
}
