
// Type: IMClient.AppPanel




using Intermech.Actions;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.NavBars;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient
{
    internal class AppPanel : DockControl
    {
      private IContainer components;
      private Action action1;
      public NavigationBar _navigationBar;
      private AppPane adminPane;
      private AppPane appPane;

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.components = (IContainer) new System.ComponentModel.Container();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AppPanel));
        this._navigationBar = new NavigationBar();
        this.adminPane = new AppPane();
        this.appPane = new AppPane();
        this.action1 = new Action(this.components);
        this._navigationBar.SuspendLayout();
        this.SuspendLayout();
        this._navigationBar.Controls.Add((Control) this.adminPane);
        this._navigationBar.Controls.Add((Control) this.appPane);
        componentResourceManager.ApplyResources((object) this._navigationBar, "_navigationBar");
        this._navigationBar.Name = "_navigationBar";
        this._navigationBar.SelectedPane = (NavigationPane) this.appPane;
        this._navigationBar.ShowPanes = 2;
        this.adminPane.LargeImage = (Image) componentResourceManager.GetObject("adminPane.LargeImage");
        componentResourceManager.ApplyResources((object) this.adminPane, "adminPane");
        this.adminPane.Name = "adminPane";
        this.adminPane.SmallImage = (Image) componentResourceManager.GetObject("adminPane.SmallImage");
        this.appPane.LargeImage = (Image) componentResourceManager.GetObject("appPane.LargeImage");
        componentResourceManager.ApplyResources((object) this.appPane, "appPane");
        this.appPane.Name = "appPane";
        this.appPane.SmallImage = (Image) componentResourceManager.GetObject("appPane.SmallImage");
        componentResourceManager.ApplyResources((object) this.action1, "action1");
        this.AllowedStates = DockLocation.Left | DockLocation.Right;
        this.Controls.Add((Control) this._navigationBar);
        this.Guid = ViewGuids.AppPanel_Guid;
        this.HideOnClose = true;
        this.Name = nameof (AppPanel);
        this.ShowHint = DockState.DockLeft;
        componentResourceManager.ApplyResources((object) this, "$this");
        this._navigationBar.ResumeLayout(false);
        this.ResumeLayout(false);
      }

      public AppPanel()
      {
        this.InitializeComponent();
        INamedImageList service = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
        if (service == null)
          return;
        this.adminPane.ButtonsPanel.ImageList = service.ImageList;
        this.appPane.ButtonsPanel.ImageList = service.ImageList;
        this.TabImageIndex = service.ImageIndex("imgAppPane");
      }

      public override string HelpID => "662";
    }
}
