
// Type: Intermech.Navigator.Controls.NavWindow
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Xml;


namespace Intermech.Navigator.Controls;

public class NavWindow : NavWindowBase
{
  private static readonly Guid _persistStateGuid = new Guid("{6C61210E-AE4E-49c9-9913-63125D27CEC9}");

  public NavWindow()
  {
    this.Guid = NavWindow._persistStateGuid;
    this.TreeView.RootNodeModified += new EventHandler(this.TreeView_RootNodeModified);
    this.TreeView.RootNodeReplaced += new EventHandler(this.TreeView_RootNodeReplaced);
    this.TreeView.BuildTree += new EventHandler(this.TreeView_BuildTree);
  }

  public override void Activated()
  {
    base.Activated();
    this.UpdateWindowTextIcon();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.TreeView.RootNodeModified -= new EventHandler(this.TreeView_RootNodeModified);
      this.TreeView.RootNodeReplaced -= new EventHandler(this.TreeView_RootNodeReplaced);
      this.TreeView.BuildTree -= new EventHandler(this.TreeView_BuildTree);
    }
    base.Dispose(disposing);
  }

  protected static DockControl RestoreWindow(NavWindow navWindow, Guid guid, string persistString)
  {
    try
    {
      XmlDocument xmlDoc = new XmlDocument();
      xmlDoc.LoadXml(persistString);
      navWindow.RestoreState(xmlDoc);
      if (navWindow.RootDescriptor == null)
      {
        navWindow.HideOnClose = false;
        navWindow.Close();
        navWindow.Dispose();
        return (DockControl) null;
      }
      bool flag1 = false;
      if (ServicesManager.GetService(typeof (IEnableTreeMultiSelectService)) is IEnableTreeMultiSelectService service1)
        flag1 = service1.EnableTreeMultiSelect(navWindow.RootDescriptor, (IServiceProvider) navWindow.Services);
      if (flag1)
        navWindow.TreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.TwoState;
      bool flag2 = true;
      if (ServicesManager.GetService(typeof (IEnableTreeColumnsSortingService)) is IEnableTreeColumnsSortingService service2)
        flag2 = service2.EnableTreeColumnsSorting(navWindow.RootDescriptor, (IServiceProvider) navWindow.Services);
      navWindow.TreeView.DisableColumnsSorting = !flag2;
      navWindow.TreeViewControl.BtnClearSorting.Enabled = flag2;
      ServicesManager.GetService<INotificationService>().FireEvent((object) null, (NotificationEventArgs) new NavigatorWindowOpenedEventArgs(navWindow, (IServiceProvider) navWindow.Services));
      return (DockControl) navWindow;
    }
    catch (Exception ex)
    {
      navWindow.HideOnClose = false;
      navWindow.Close();
      navWindow.Dispose();
      IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
      service.WriteString("Navigator", LocalizationHolder.rm.GetString("Client.Core_326"));
      service.WriteString("Navigator", ex.Message);
      return (DockControl) null;
    }
  }

  public static DockControl RestoreWindowCallback(Guid guid, string persistString)
  {
    return guid != NavWindow._persistStateGuid ? (DockControl) null : NavWindow.RestoreWindow(new NavWindow(), guid, persistString);
  }

  private void TreeView_RootNodeModified(object sender, EventArgs e) => this.UpdateWindowTextIcon();

  private void TreeView_RootNodeReplaced(object sender, EventArgs e) => this.UpdateWindowTextIcon();

  private void TreeView_BuildTree(object sender, EventArgs e) => this.UpdateWindowTextIcon();

  protected virtual int GetTabImageIndex(INodeID nodeID)
  {
    return Holder.IconService != null ? Holder.IconService.IndexOf(nodeID.CategoryID, nodeID.TypeID, (object) null) : -1;
  }

  private void UpdateWindowTextIcon()
  {
    INode rootHandler = this.TreeView.RootHandler;
    INodeID rootNodeId = this.TreeView.RootNodeID;
    if (rootHandler == null || rootNodeId == null)
      return;
    if (Holder.IconService != null)
    {
      int tabImageIndex = this.GetTabImageIndex(rootNodeId);
      if (tabImageIndex >= 0)
        this.TabImage = Holder.IconService.ImageList.Images[tabImageIndex];
    }
    NavigatorWindowCaptionEventArgs e = new NavigatorWindowCaptionEventArgs(this.RootDescriptor, (IServiceProvider) this.Services, rootHandler.GetAddress(rootNodeId), string.Empty, string.Empty);
    NavigatorWindowCaptionsHelper.GetNavigatorWindowCaption((object) this, e);
    this.Text = e.Text;
    this.ExtraText = e.ExtraText;
    this.ToolTipText = e.TextHint;
  }
}
