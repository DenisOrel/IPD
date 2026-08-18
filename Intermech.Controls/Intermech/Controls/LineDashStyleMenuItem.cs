
// Type: Intermech.Controls.LineDashStyleMenuItem
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;


namespace Intermech.Controls;

[Designer(typeof (LineDashStyleMenuItemDesigner))]
public class LineDashStyleMenuItem : 
  LineMenuItem,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IPopupControlHost,
  IPopupMenuItem,
  IArrowKeysNavigationSupported
{
  public new const int DefaultLineThickness = 300;

  public LineDashStyleMenuItem() => this._lineThickness = 300;

  [DefaultValue(300)]
  public override int LineThickness
  {
    [DebuggerStepThrough] get => this._lineThickness;
    set
    {
      if (this._lineThickness == value)
        return;
      base.LineThickness = value;
    }
  }
}
