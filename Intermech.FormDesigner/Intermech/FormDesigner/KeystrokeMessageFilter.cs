// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.KeystrokeMessageFilter
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

#nullable disable
namespace Intermech.FormDesigner;

internal class KeystrokeMessageFilter : IMessageFilter
{
  private IDesignerHost _host;
  private IHostView _hostView;

  /// <summary>Конструктор.</summary>
  /// <param name="host"></param>
  public KeystrokeMessageFilter(IDesignerHost host)
  {
    this._host = host;
    this._hostView = this._host.GetService(typeof (IHostView)) as IHostView;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="m"></param>
  /// <returns></returns>
  public bool PreFilterMessage(ref Message m)
  {
    IMenuCommandService service = this._host.GetService(typeof (IMenuCommandService)) as IMenuCommandService;
    if (this._hostView == null || !this._hostView.View.Focused || service == null || m.Msg != 256 /*0x0100*/)
      return false;
    switch ((Keys) (int) m.WParam | Control.ModifierKeys)
    {
      case Keys.Return:
        service.GlobalInvoke(MenuCommands.KeyDefaultAction);
        break;
      case Keys.Escape:
        service.GlobalInvoke(MenuCommands.KeyCancel);
        break;
      case Keys.Left:
        service.GlobalInvoke(MenuCommands.KeyMoveLeft);
        break;
      case Keys.Up:
        service.GlobalInvoke(MenuCommands.KeyMoveUp);
        break;
      case Keys.Right:
        service.GlobalInvoke(MenuCommands.KeyMoveRight);
        break;
      case Keys.Down:
        service.GlobalInvoke(MenuCommands.KeyMoveDown);
        break;
      case Keys.Delete:
        service.GlobalInvoke(StandardCommands.Delete);
        break;
      case Keys.Escape | Keys.Shift:
        service.GlobalInvoke(MenuCommands.KeyReverseCancel);
        break;
      case Keys.Left | Keys.Shift:
        service.GlobalInvoke(MenuCommands.KeySizeWidthDecrease);
        break;
      case Keys.Up | Keys.Shift:
        service.GlobalInvoke(MenuCommands.KeySizeHeightDecrease);
        break;
      case Keys.Right | Keys.Shift:
        service.GlobalInvoke(MenuCommands.KeySizeWidthIncrease);
        break;
      case Keys.Down | Keys.Shift:
        service.GlobalInvoke(MenuCommands.KeySizeHeightIncrease);
        break;
      case Keys.D5 | Keys.Shift:
        service.GlobalInvoke(MenuCommands.KeyNudgeWidthDecrease);
        break;
      case Keys.Left | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeLeft);
        break;
      case Keys.Up | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeUp);
        break;
      case Keys.Right | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeRight);
        break;
      case Keys.Down | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeDown);
        break;
      case Keys.C | Keys.Control:
        service.GlobalInvoke(StandardCommands.Copy);
        break;
      case Keys.V | Keys.Control:
        service.GlobalInvoke(StandardCommands.Paste);
        break;
      case Keys.X | Keys.Control:
        service.GlobalInvoke(StandardCommands.Cut);
        break;
      case Keys.Up | Keys.Shift | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeHeightDecrease);
        break;
      case Keys.Right | Keys.Shift | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeWidthIncrease);
        break;
      case Keys.Down | Keys.Shift | Keys.Control:
        service.GlobalInvoke(MenuCommands.KeyNudgeHeightIncrease);
        break;
    }
    return false;
  }
}
