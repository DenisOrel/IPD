
// Type: Intermech.Navigator.Controls.NavigatorTreeViewClientService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Search.UI;
using System;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Navigator.Controls;

public sealed class NavigatorTreeViewClientService : INavigatorTreeViewClientService
{
  private volatile bool _stopExpandAll;

  public void ExpandAll(NavigatorTreeNode navigatorTreeNode)
  {
    if (navigatorTreeNode == null)
      throw new ArgumentNullException(nameof (navigatorTreeNode));
    if (navigatorTreeNode.Tree == null || navigatorTreeNode.Handle == null)
      throw new ArgumentException();
    ProgressDialog progressForm = new ProgressDialog();
    progressForm.ButtonClick += new EventHandler(this.ProgressForm_ButtonClick);
    progressForm.FormClosing += new FormClosingEventHandler(this.ProgressForm_FormClosing);
    progressForm.LabelText = "Выполняется раскрытие узла ...";
    progressForm.Style = ProgressBarStyle.Marquee;
    this._stopExpandAll = false;
    new Thread((ThreadStart) (() =>
    {
      try
      {
        try
        {
          navigatorTreeNode.Tree.Invoke((Delegate) (() => navigatorTreeNode.Tree.RefreshNode(navigatorTreeNode)));
          this.FetchNavigatorTreeNode(navigatorTreeNode);
          if (this._stopExpandAll)
            return;
          navigatorTreeNode.Tree.Invoke((Delegate) (() =>
          {
            navigatorTreeNode.Tree.DisableTreeRowExpand = true;
            try
            {
              navigatorTreeNode.Handle.Expand();
              navigatorTreeNode.Handle.ExpandChildren(true);
              navigatorTreeNode.Tree.RebuildHandles();
            }
            catch
            {
            }
            finally
            {
              navigatorTreeNode.Tree.DisableTreeRowExpand = false;
            }
          }));
        }
        finally
        {
          navigatorTreeNode.Tree.Invoke((Delegate) (() =>
          {
            if (progressForm.IsDisposed)
              return;
            progressForm.Close();
          }));
        }
      }
      catch (Exception ex)
      {
        try
        {
          navigatorTreeNode.Tree.Invoke((Delegate) (() => ExceptionHelper.ExceptionService.ShowException(ex)));
        }
        catch
        {
        }
      }
    })).Start();
    int num = (int) progressForm.ShowDialog((IWin32Window) navigatorTreeNode.Tree);
  }

  private void ProgressForm_ButtonClick(object sender, EventArgs e) => this._stopExpandAll = true;

  private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    this._stopExpandAll = true;
  }

  private void FetchNavigatorTreeNode(NavigatorTreeNode navigatorTreeNode)
  {
    if (this._stopExpandAll)
      return;
    navigatorTreeNode.Fetch();
    foreach (NavigatorTreeNode navigatorTreeNode1 in navigatorTreeNode.Children.ToArray())
      this.FetchNavigatorTreeNode(navigatorTreeNode1);
  }
}
