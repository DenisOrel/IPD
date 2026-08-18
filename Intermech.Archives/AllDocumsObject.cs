// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.AllDocumsObject
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using TenTec.Windows.iGridLib;

#nullable disable
namespace Intermech.Archives;

public class AllDocumsObject : ObjectsViewBase
{
  private INotificationService _ns;
  private bool _hasViewArchives;

  public AllDocumsObject()
  {
    this._ns = ServicesManager.GetService(typeof (INotificationService)) as INotificationService;
    this._ns.Subscribe("ObjectsChanged", new NotificationEventHandler(this.NotifyEvent));
    this._ns.Subscribe("ArchiveChanged", new NotificationEventHandler(this.NotifyEvent));
  }

  protected override void Dispose(bool disposing)
  {
    this._ns.Unsubscribe("ObjectsChanged", new NotificationEventHandler(this.NotifyEvent));
    this._ns.Unsubscribe("ArchiveChanged", new NotificationEventHandler(this.NotifyEvent));
    base.Dispose(disposing);
  }

  public override string Caption
  {
    get
    {
      return this._services.GetService(typeof (ViewArchives)) != null ? ServiceHolder.rm.GetString("Archives_1") : ServiceHolder.rm.GetString("Archives_2");
    }
  }

  public override int OrderID => 10;

  public override ContentType ViewContentType
  {
    get => this._hasViewArchives ? ContentType.Folders : ContentType.NonFolders;
  }

  private void NotifyEvent(object sender, NotificationEventArgs e)
  {
    if (!e.EventName.Equals("ArchiveChanged") && !e.EventName.Equals("ObjectsChanged"))
      return;
    this.ReloadItems();
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AllDocumsObject));
    ((ISupportInitialize) this._grid).BeginInit();
    ((ISupportInitialize) this._pictureBox).BeginInit();
    this.SuspendLayout();
    this._grid.DefaultAutoGroupRow.Height = 21;
    this._grid.FrozenArea.ColCount = 1;
    this._grid.FrozenArea.SortFrozenRows = true;
    this._grid.GroupBox.BackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintBackColor = SystemColors.GrayText;
    this._grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this._grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this._grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    componentResourceManager.ApplyResources((object) this._grid, "grid");
    this.buttonHeightSet.Padding.Bottom = 0;
    this.buttonHeightSet.Padding.Left = 0;
    this.buttonHeightSet.Padding.Right = 0;
    this.buttonHeightSet.Padding.Top = 0;
    this._filtersComboBoxItem.Padding.Bottom = 0;
    this._filtersComboBoxItem.Padding.Left = 1;
    this._filtersComboBoxItem.Padding.Right = 1;
    this._filtersComboBoxItem.Padding.Top = 0;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Name = nameof (AllDocumsObject);
    this.Tag = (object) " ";
    ((ISupportInitialize) this._grid).EndInit();
    ((ISupportInitialize) this._pictureBox).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public override void Activate(IView previousView)
  {
    this._hasViewArchives = this._services.GetService(typeof (ViewArchives)) != null;
    base.Activate(previousView);
  }

  protected override INode GetNode()
  {
    INode node = base.GetNode();
    if (node != null && node is IContextAware)
    {
      IContextAware contextAware = (IContextAware) node;
      if (contextAware.Services is ServiceContainer)
      {
        ServiceContainer services = (ServiceContainer) contextAware.Services;
        if (this._hasViewArchives)
        {
          if (services.GetService(typeof (ViewArchives)) == null)
            services.AddService(typeof (ViewArchives), (object) new ViewArchives());
        }
        else if (services.GetService(typeof (ViewArchives)) != null)
          services.RemoveService(typeof (ViewArchives));
      }
    }
    return node;
  }
}
