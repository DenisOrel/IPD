// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Editors.FindService
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces.Client;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Editors;

internal class FindService
{
  private static FindDialog _dialog;
  private static Form _mainForm;
  private static bool _autoShow;

  public static event EventHandler Closed;

  static FindService()
  {
    if (ServicesManager.GetService(typeof (DockManager)) is DockManager service)
      service.DockControlActivated += new DockControlEventHandler(FindService.OnDockControlActivated);
    FindService._mainForm = (ServicesManager.GetService(typeof (IMainFormUpdate)) as IMainFormUpdate).MainForm;
  }

  private static void OnDockControlActivated(object sender, DockControlEventArgs e)
  {
    if (FindService._dialog == null || e.DockControl is ISkipTargetActivate)
      return;
    if (!(e.DockControl is IFindTarget dockControl))
    {
      if (FindService._dialog.Visible)
      {
        FindService._dialog.Visible = false;
        FindService._autoShow = true;
      }
    }
    else if (FindService._autoShow)
    {
      FindService._autoShow = false;
      FindService._dialog.Visible = true;
    }
    FindService._dialog.Target = dockControl;
  }

  private static FindDialog Dialog
  {
    get
    {
      if (FindService._dialog == null)
      {
        FindService._dialog = new FindDialog();
        FindService._dialog.DialogClosed += new EventHandler(FindService.OnDialog_Closed);
        FindService._mainForm.AddOwnedForm((Form) FindService._dialog);
      }
      return FindService._dialog;
    }
  }

  private static void OnDialog_Closed(object sender, EventArgs e)
  {
    EventHandler closed = FindService.Closed;
    if (closed != null)
      closed((object) null, e);
    FindService._mainForm.Activate();
  }

  internal static void ShowDialog(IFindTarget target, bool replace)
  {
    FindService.Dialog.ShowTab(replace);
    FindService.Dialog.Target = target;
  }
}
