
// Type: IMClient.PluginView




using IMClient.UI.Winforms;
using Intermech;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace IMClient
{
    public class PluginView : DockControl, System.IServiceProvider
    {
      private TreeView _treeView;
      private PropertyGrid propertyGrid1;
      private IContainer components;
      private ServiceContainer _services;
      private PluginManager _pluginManager;
      private int _pluginId;
      private int _packageId;
      private int _pluginSessionId;
      private SplitContainer splitContainer1;
      private ContextMenuStrip addPluginContextMenu;
      private ToolStripMenuItem loadPluginMenuStrip;
      private int _pluginWarnId;

      public PluginView()
      {
        this.InitializeComponent();
        INamedImageList service1 = (INamedImageList) ServicesManager.GetService(typeof (INamedImageList));
        if (service1 != null)
        {
          this._treeView.ImageList = service1.ImageList;
          this._packageId = service1.ImageIndex("imgPackage");
          this._pluginId = service1.ImageIndex("imgPlugin");
          this._pluginWarnId = service1.ImageIndex("imgPluginWarn");
          this._pluginSessionId = service1.ImageIndex("imgSessionPlugin");
          this.TabImageIndex = service1.ImageIndex("imgPluginList");
        }
        IPluginManager service2 = (IPluginManager) ServicesManager.GetService(typeof (IPluginManager));
        service2.PluginAdded += new PluginEventHandler(this.pluginManager_PluginAdded);
        service2.PluginRemoved += new PluginEventHandler(this.pluginManager_PluginRemoved);
        this._pluginManager = service2 as PluginManager;
        this._services = new ServiceContainer();
        this._services.AddService(typeof (INavigate), (object) new TreeViewNavigator(this._treeView));
      }

      private void BuildPluginsList()
      {
        try
        {
          this._treeView.BeginUpdate();
          this._treeView.Nodes.Clear();
        }
        finally
        {
          this._treeView.EndUpdate();
        }
      }

      protected void pluginManager_PluginAdded(object sender, PluginEventArgs e)
      {
        if (e == null || e.Plugin == null)
          return;
        IPlugin plugin = e.Plugin;
        TreeNode node = new TreeNode(plugin.Name);
        if (!plugin.AutoReload)
        {
          node.Tag = (object) plugin;
          node.ImageIndex = this._pluginSessionId;
        }
        else
        {
          node.Tag = (object) new RemovablePluginView(this._pluginManager, plugin);
          node.ImageIndex = this._pluginId;
        }
        node.SelectedImageIndex = node.ImageIndex;
        this._treeView.Nodes.Add(node);
        foreach (IPackage package in (IEnumerable<IPackage>) plugin.Packages)
          node.Nodes.Add(new TreeNode(package.Name)
          {
            Tag = (object) package,
            ImageIndex = this._packageId,
            SelectedImageIndex = this._packageId
          });
      }

      protected void pluginManager_PluginRemoved(object sender, PluginEventArgs e)
      {
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing)
        {
          IPluginManager service = (IPluginManager) ServicesManager.GetService(typeof (IPluginManager));
          if (service != null)
          {
            service.PluginAdded -= new PluginEventHandler(this.pluginManager_PluginAdded);
            service.PluginRemoved -= new PluginEventHandler(this.pluginManager_PluginRemoved);
          }
          if (this.components != null)
            this.components.Dispose();
        }
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.components = (IContainer) new System.ComponentModel.Container();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PluginView));
        this._treeView = new TreeView();
        this.propertyGrid1 = new PropertyGrid();
        this.splitContainer1 = new SplitContainer();
        this.addPluginContextMenu = new ContextMenuStrip(this.components);
        this.loadPluginMenuStrip = new ToolStripMenuItem();
        this.splitContainer1.BeginInit();
        this.splitContainer1.Panel1.SuspendLayout();
        this.splitContainer1.Panel2.SuspendLayout();
        this.splitContainer1.SuspendLayout();
        this.addPluginContextMenu.SuspendLayout();
        this.SuspendLayout();
        this._treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this._treeView.ContextMenuStrip = this.addPluginContextMenu;
        componentResourceManager.ApplyResources((object) this._treeView, "_treeView");
        this._treeView.HideSelection = false;
        this._treeView.Name = "_treeView";
        this._treeView.Sorted = true;
        this._treeView.AfterSelect += new TreeViewEventHandler(this._treeView_AfterSelect);
        componentResourceManager.ApplyResources((object) this.propertyGrid1, "propertyGrid1");
        this.propertyGrid1.LineColor = SystemColors.ScrollBar;
        this.propertyGrid1.Name = "propertyGrid1";
        this.propertyGrid1.PropertySort = PropertySort.Alphabetical;
        this.propertyGrid1.ToolbarVisible = false;
        componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
        this.splitContainer1.Name = "splitContainer1";
        this.splitContainer1.Panel1.Controls.Add((Control) this._treeView);
        this.splitContainer1.Panel2.Controls.Add((Control) this.propertyGrid1);
        this.addPluginContextMenu.Items.AddRange(new ToolStripItem[1]
        {
          (ToolStripItem) this.loadPluginMenuStrip
        });
        this.addPluginContextMenu.Name = "addPluginContextMenu";
        componentResourceManager.ApplyResources((object) this.addPluginContextMenu, "addPluginContextMenu");
        this.loadPluginMenuStrip.Name = "loadPluginMenuStrip";
        componentResourceManager.ApplyResources((object) this.loadPluginMenuStrip, "loadPluginMenuStrip");
        this.loadPluginMenuStrip.Click += new EventHandler(this.loadPluginMenuStrip_Click);
        this.AllowedStates = DockLocation.Left | DockLocation.Right | DockLocation.Bottom | DockLocation.Float;
        this.BorderStyle = Intermech.Docking.Rendering.BorderStyle.Flat;
        this.Controls.Add((Control) this.splitContainer1);
        this.Guid = new Guid("1b935329-b9f9-4641-9ec8-7b4d8c5bc634");
        this.HideOnClose = true;
        this.Name = nameof (PluginView);
        componentResourceManager.ApplyResources((object) this, "$this");
        this.splitContainer1.Panel1.ResumeLayout(false);
        this.splitContainer1.Panel2.ResumeLayout(false);
        this.splitContainer1.EndInit();
        this.splitContainer1.ResumeLayout(false);
        this.addPluginContextMenu.ResumeLayout(false);
        this.ResumeLayout(false);
      }

      private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
      {
        this.propertyGrid1.SelectedObject = e.Node.Tag;
      }

      object System.IServiceProvider.GetService(System.Type serviceType)
      {
        return this._services.GetService(serviceType);
      }

      private void loadPluginMenuStrip_Click(object sender, EventArgs e)
      {
        using (SelectPluginsForm selectPluginsForm = new SelectPluginsForm())
        {
          selectPluginsForm.InitForm();
          if (selectPluginsForm.ShowDialog() != DialogResult.OK)
            return;
          foreach (string selectedDll in selectPluginsForm.SelectedDlls)
          {
            try
            {
              if (File.Exists(selectedDll))
                this._pluginManager.Load(selectedDll, true);
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
          foreach (string selectedLoadModule in selectPluginsForm.SelectedLoadModules)
          {
            try
            {
              string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, selectedLoadModule);
              if (File.Exists(str))
                this._pluginManager.Load(str, true);
            }
            catch (Exception ex)
            {
              ExceptionHelper.ExceptionService.ShowException(ex);
            }
          }
        }
      }
    }
}
