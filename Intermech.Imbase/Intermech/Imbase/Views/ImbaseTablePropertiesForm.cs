// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ImbaseTablePropertiesForm
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Client.Core;
using Intermech.Imbase.Selection;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

public class ImbaseTablePropertiesForm : Form
{
  private IServiceContainer _services;
  private long _refID;
  private long _tblID;
  private IContainer components;
  private Panel _pnlBottom;
  private Button _btnClose;
  public PageViewsManager _viewsMngr;

  public ImbaseTablePropertiesForm(long refID, long tblID)
  {
    this.InitializeComponent();
    this._refID = refID;
    this._tblID = tblID;
    if (this._refID == 0L && this._tblID == 0L)
      return;
    this.InitServices();
    this.LoadInfo();
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    this._viewsMngr.CloseViews();
    base.OnClosing(e);
  }

  private void InitServices()
  {
    this._services = (IServiceContainer) new ServiceContainer();
    if (ServicesManager.GetService(typeof (INotificationService)) is INotificationService service)
      this._services.AddService(typeof (INotificationService), (object) service);
    this._services.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.NoCompositionView | ViewStateFlags.NoContainsInView | ViewStateFlags.NoEventsView | ViewStateFlags.NoPluginsViews | ViewStateFlags.NoGroupingObjectsViews));
    this._services.AddService(typeof (IIODispatcher), (object) new IODispatcher());
    this._viewsMngr.Services = (System.IServiceProvider) this._services;
  }

  private void LoadInfo()
  {
    NodeIDPath handlerPath = new NodeIDPath((IDescriptor) new ImbaseFilterDescriptor(this._refID == 0L || this._refID == -1L ? this._tblID : this._refID));
    INode handler = (INode) new EtherealNode(handlerPath.RootDescriptor);
    INodeQuery query = handler.GetQuery(ContentType.Folders);
    query.Execute((object) null, 1);
    NodeIDCollection nodeIDs = new NodeIDCollection();
    if (query.RecordCount == 0)
      return;
    nodeIDs.Add(query.GetRecordNodeID(0));
    ISelectedItems items = (ISelectedItems) new NodeItems(handlerPath, handler, nodeIDs, (System.IServiceProvider) this._services);
    this._viewsMngr.AllowedViews = new string[2]
    {
      "ObjectProperties",
      "ImbaseTableRefView"
    };
    this._viewsMngr.UpdateViews(items, true);
  }

  private void ServicesFinalization()
  {
    this._services.RemoveService(typeof (IIODispatcher));
    this._services.RemoveService(typeof (INotificationService));
    this._services.RemoveService(typeof (IViewState));
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
    {
      this.ServicesFinalization();
      this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImbaseTablePropertiesForm));
    this._pnlBottom = new Panel();
    this._btnClose = new Button();
    this._viewsMngr = new PageViewsManager();
    this._pnlBottom.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this._pnlBottom, "_pnlBottom");
    this._pnlBottom.Controls.Add((Control) this._btnClose);
    this._pnlBottom.Name = "_pnlBottom";
    componentResourceManager.ApplyResources((object) this._btnClose, "_btnClose");
    this._btnClose.DialogResult = DialogResult.Cancel;
    this._btnClose.Name = "_btnClose";
    componentResourceManager.ApplyResources((object) this._viewsMngr, "_viewsMngr");
    this._viewsMngr.ActiveViewPage = (IViewPage) null;
    this._viewsMngr.CausesValidation = false;
    this._viewsMngr.Name = "_viewsMngr";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this._btnClose;
    this.Controls.Add((Control) this._viewsMngr);
    this.Controls.Add((Control) this._pnlBottom);
    this.DoubleBuffered = true;
    this.Name = nameof (ImbaseTablePropertiesForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this._pnlBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
