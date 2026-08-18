// Decompiled with JetBrains decompiler
// Type: Intermech.Windows.Forms.AdvParentControlDesigner
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.Windows.Forms;

[CLSCompliant(false)]
public class AdvParentControlDesigner : ParentControlDesigner
{
  [CanBeNull]
  public SimpleBaseUserControl UserControl
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Control as SimpleBaseUserControl;
    }
  }

  public override void Initialize(IComponent component)
  {
    base.Initialize(component);
    if (!(this.Control is IDesignModeControlsContainer control1))
      return;
    List<(Control DesignModeControl, string FieldName)> modeChildControls = control1.GetDesignModeChildControls();
    if (modeChildControls == null || modeChildControls.Count <= 0)
      return;
    foreach ((Control control2, string FieldName) in modeChildControls)
    {
      if (control2 != null && (!string.IsNullOrEmpty(FieldName) || control2.Name != string.Empty))
      {
        string name = FieldName ?? control2.Name;
        if (!string.IsNullOrWhiteSpace(name))
        {
          this.EnableDesignMode(control2, name);
          if (AdvParentControlDesigner.IsDesignParentControl(control2))
            control2.ControlAdded += (ControlEventHandler) ((sender, e) => e?.Control?.BringToFront());
        }
      }
    }
  }

  private static bool IsDesignParentControl([NotNull] Control control)
  {
    IDesignerHost service;
    return control.Site != null && control.Site.TryGetService<IDesignerHost>(out service) && service.GetDesigner((IComponent) control) is ParentControlDesigner;
  }
}
