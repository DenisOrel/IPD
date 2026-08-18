
// Type: Intermech.Controls.CheckedTreeView
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Windows.Forms;


namespace Intermech.Controls;

/// <summary>
/// TreeView c отключенным DblClick (который работает очень плохо на TreeView с включенными CheckBox)
/// MS know about the problem but refuse to fix it  (c)
/// </summary>
public class CheckedTreeView : TreeView
{
  protected override void WndProc(ref Message m)
  {
    if (m.Msg == 515)
    {
      if (this.HitTest(this.PointToClient(Cursor.Position)).Location == TreeViewHitTestLocations.StateImage)
        m.Result = IntPtr.Zero;
      else
        base.WndProc(ref m);
    }
    else
      base.WndProc(ref m);
  }
}
