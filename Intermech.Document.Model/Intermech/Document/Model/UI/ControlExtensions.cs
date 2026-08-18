// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Model.UI.ControlExtensions
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.Document.Model.UI;

public static class ControlExtensions
{
  public static Control GetFocusedControl(this Control parent)
  {
    if (parent.Focused)
      return parent;
    foreach (Control control in (ArrangedElementCollection) parent.Controls)
    {
      Control focusedControl = control.GetFocusedControl();
      if (focusedControl != null)
        return focusedControl;
    }
    return (Control) null;
  }
}
