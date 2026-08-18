
// Type: IMClient.Views.DocumentOverview




using Intermech.Bars;
using Intermech.Client.Core.Visualizers;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Map;
using Intermech.Redline;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;


namespace IMClient.Views
{
    internal class DocumentOverview : DockControl, IVisualizerOverview, ISkipTargetActivate
    {
      private RedlineOverview panel1;
      private System.IServiceProvider _serviceProvider;

      public DocumentOverview(System.IServiceProvider provider)
      {
        this.InitializeComponent();
        this._serviceProvider = provider;
        this.TabImageIndex = ((INamedImageList) this._serviceProvider.GetService(typeof (INamedImageList))).ImageIndex("imgOverview");
        if (provider.GetService(typeof (IVisualizerOverview)) != null || !(provider is IServiceContainer serviceContainer))
          return;
        serviceContainer.AddService(typeof (IVisualizerOverview), (object) this);
      }

      public override string HelpID => "706";

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentOverview));
        this.panel1 = new RedlineOverview();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this.panel1, "panel1");
        this.panel1.AllowCopy = false;
        this.panel1.AllowDelete = false;
        this.panel1.AllowDragOut = false;
        this.panel1.AllowEdit = false;
        this.panel1.AllowInsert = false;
        this.panel1.AllowLink = false;
        this.panel1.AllowReshape = false;
        this.panel1.AllowResize = false;
        this.panel1.AllowSelect = false;
        this.panel1.BackColor = Color.White;
        this.panel1.Border3DStyle = Border3DStyle.Etched;
        this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.panel1.DragsRealtime = true;
        this.panel1.GridPenDashStyle = DashStyle.Solid;
        this.panel1.GridSnapDrag = MapViewSnapStyle.None;
        this.panel1.GridSnapResize = MapViewSnapStyle.None;
        this.panel1.GridStyle = MapViewGridStyle.None;
        this.panel1.InterpolationMode = InterpolationMode.High;
        this.panel1.Name = "panel1";
        this.panel1.ShowHorizontalScrollBar = MapViewScrollBarVisibility.Hide;
        this.panel1.ShowsNegativeCoordinates = false;
        this.panel1.ShowVerticalScrollBar = MapViewScrollBarVisibility.Hide;
        this.panel1.SmoothingMode = SmoothingMode.HighQuality;
        this.panel1.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Top | DockLocation.Bottom | DockLocation.Float;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this.panel1);
        this.DoubleBuffered = true;
        this.Guid = ViewGuids.DocumentOverview_Guid;
        this.HideOnClose = true;
        this.Name = nameof (DocumentOverview);
        this.ShowHint = DockState.DockLeftAutoHide;
        this.Leave += new EventHandler(this.DocumentOverview_Leave);
        this.ResumeLayout(false);
      }

      private void DocumentOverview_Leave(object sender, EventArgs e) => this.panel1.UpdateView();

      public void Attach(MapView observerd)
      {
        this.panel1.Observed = observerd;
        if (observerd == null)
          return;
        this.panel1.ZoomToFit();
      }
    }
}
