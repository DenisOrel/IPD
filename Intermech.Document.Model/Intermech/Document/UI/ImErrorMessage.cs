// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.ImErrorMessage
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Document.UI;

public class ImErrorMessage
{
  private ErrorsUserControl errorsUserControl;
  private string text;

  public virtual string Text
  {
    get => this.text;
    set => this.text = value;
  }

  public ErrorsUserControl ErrorsControl
  {
    get => this.errorsUserControl;
    set => this.errorsUserControl = value;
  }

  public virtual void DoubleClick()
  {
  }

  public virtual void GetContextMenu(List<ToolbarItemBase> contextMenuItems)
  {
  }
}
