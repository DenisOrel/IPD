
// Type: Intermech.Mvp.Winforms.TabControlWindowCollection`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Mvp.Components;
using System;
using System.Windows.Forms;


namespace Intermech.Mvp.Winforms
{
    public abstract class TabControlWindowCollection<TWindow> : IWindowCollection<TWindow>
    {
      private readonly TabControl tabControl;

      public TabControlWindowCollection(TabControl tabControl)
      {
        this.tabControl = tabControl != null ? tabControl : throw new ArgumentNullException(nameof (tabControl));
        this.tabControl.Selected += new TabControlEventHandler(this.TabPageSelectedHandler);
      }

      public TWindow AddWindow()
      {
        TabPage tabPage = new TabPage();
        this.CreateWindowControls(tabPage);
        TWindow window = this.Wrap(tabPage);
        tabPage.Tag = (object) window;
        this.tabControl.TabPages.Add(tabPage);
        if (this.tabControl.TabPages.Count == 1)
          this.TabPageSelectedHandler((object) this.tabControl, new TabControlEventArgs(tabPage, 0, TabControlAction.Selected));
        else
          this.tabControl.SelectedTab = tabPage;
        return window;
      }

      public void RemoveWindow(TWindow window) => this.tabControl.TabPages.Remove(this.Unwrap(window));

      protected virtual void CreateWindowControls(TabPage windowControl)
      {
      }

      protected abstract TWindow Wrap(TabPage windowControl);

      protected abstract TabPage Unwrap(TWindow window);

      public TWindow ActiveWindow
      {
        get
        {
          TabPage selectedTab = this.tabControl.SelectedTab;
          return selectedTab == null ? default (TWindow) : (TWindow) selectedTab.Tag;
        }
        set
        {
          this.tabControl.SelectedTab = (object) value != null ? this.Unwrap(value) : throw new ArgumentNullException(nameof (value));
        }
      }

      private void TabPageSelectedHandler(object sender, TabControlEventArgs e)
      {
        if (e.Action != TabControlAction.Selected)
          return;
        EventHandler activeWindowChanged = this.ActiveWindowChanged;
        if (activeWindowChanged == null)
          return;
        activeWindowChanged((object) this, EventArgs.Empty);
      }

      public event EventHandler ActiveWindowChanged;
    }
}
