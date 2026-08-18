
// Type: Intermech.Docking.PageControlCancelEventArgs
// Assembly: Intermech.Docking, Version=4.0.25.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F97F850-2D29-46D1-A3D7-6B2A02E86D46
:\IPS\Client\Intermech.Docking.dll

using System.ComponentModel;


namespace Intermech.Docking;

public class PageControlCancelEventArgs : CancelEventArgs
{
  private TabPage tabPage;
  private int tabIndex;

  public PageControlCancelEventArgs(TabPage tabPage, int tabIndex)
    : this(tabPage, tabIndex, false)
  {
  }

  public PageControlCancelEventArgs(TabPage tabPage, int tabIndex, bool cancel)
    : base(cancel)
  {
    this.tabPage = tabPage;
    this.tabIndex = tabIndex;
  }

  public TabPage TabPage => this.tabPage;

  public int TabIndex => this.tabIndex;
}
