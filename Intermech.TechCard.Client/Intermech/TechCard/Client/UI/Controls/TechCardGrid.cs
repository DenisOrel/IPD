// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechCardGrid
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>Create custom control for techcard uses</summary>
public class TechCardGrid : iGrid
{
  /// <summary>
  /// 
  /// </summary>
  private void OnShowGridHeaderMenu(iGColHdrMouseUpEventArgs e)
  {
    iGColHdrMouseUpEventHandler showGridHeaderMenu = this.ShowGridHeaderMenu;
    if (showGridHeaderMenu == null)
      return;
    showGridHeaderMenu((object) this, e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="e"></param>
  protected override void OnColHdrMouseUp(iGColHdrMouseUpEventArgs e)
  {
    base.OnColHdrMouseUp(e);
    if (e.Button != MouseButtons.Right)
      return;
    this.OnShowGridHeaderMenu(e);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  public void OnHeaderMenuCustomizeClick(object sender, EventArgs e)
  {
    EventHandler menuCustomizeClick = this.HeaderMenuCustomizeClick;
    if (menuCustomizeClick == null)
      return;
    menuCustomizeClick(sender, e);
  }

  /// <summary>Show header menu event</summary>
  public event iGColHdrMouseUpEventHandler ShowGridHeaderMenu;

  /// <summary>Header's menu customization click</summary>
  [Category("Header")]
  public event EventHandler HeaderMenuCustomizeClick;
}
