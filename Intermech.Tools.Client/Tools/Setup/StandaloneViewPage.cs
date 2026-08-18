// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.StandaloneViewPage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.StandaloneView;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class StandaloneViewPage : UserControl, IPageControl, IDisposable
{
  private HostControlSizeManager hostControlSizeManager;
  private IPagerControl pager;
  private ToolSecurityContext securityContext;
  private int currentObjectType;
  private bool editorIsOpen;
  private StandaloneViewSettingsControl editorControl;
  private IContainer components;
  private NavigatorTreeView tvObjectTypes;
  private Label lbDescription;
  private PictureBox pbImage;
  private SplitContainer scMainContainer;
  private Label lbSelectTypeHint;

  public StandaloneViewPage()
  {
    this.InitializeComponent();
    this.hostControlSizeManager = new HostControlSizeManager();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 2882);
  }

  public void Initialize(IPagerControl pagerControl)
  {
    this.pager = pagerControl;
    this.InitSecurityContext();
    this.InitObjectTree();
    this.InitEditors();
    this.PopulateTree();
  }

  public bool CanClose => true;

  public void Close()
  {
    if (this.editorIsOpen)
      this.CloseEditors(true);
    this.pager = (IPagerControl) null;
  }

  public event EventHandler DynamicContentChanged;

  private void InitSecurityContext() => this.securityContext = new ToolSecurityContext();

  private void InitObjectTree()
  {
    this.currentObjectType = -1;
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog | ViewStateFlags.ReadOnly));
    this.tvObjectTypes.Services = (System.IServiceProvider) serviceContainer;
    this.tvObjectTypes.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
  }

  private void PopulateTree()
  {
    this.tvObjectTypes.Build((IDescriptor) new ObjectTypesNodeDescriptor());
  }

  private void tvObjectTypes_AfterFocusNode(object sender, NavigatorTreeNodeEventArgs e)
  {
    IDBObjectTypeID dbObjectTypeId = (IDBObjectTypeID) null;
    if (e.Node != null)
      dbObjectTypeId = (IDBObjectTypeID) this.tvObjectTypes.GetNodeHandler(e.Node).GetData(e.Node.NodeID, typeof (IDBObjectTypeID));
    if (dbObjectTypeId != null)
    {
      if (this.editorIsOpen)
        this.CloseEditors(false);
      this.currentObjectType = dbObjectTypeId.Value;
      this.OpenEditors();
    }
    else
    {
      if (this.editorIsOpen)
        this.CloseEditors(true);
      this.currentObjectType = -1;
    }
  }

  private void InitEditors() => this.editorIsOpen = false;

  private void OpenEditors()
  {
    if (this.editorControl == null)
    {
      this.editorControl = new StandaloneViewSettingsControl();
      this.editorControl.Dock = DockStyle.Fill;
      this.editorControl.Parent = (Control) this.scMainContainer.Panel2;
      this.editorControl.BringToFront();
      this.editorControl.SettingsService = ServiceUtils.GetService<IStandaloneViewSettingsService>((object) ServicesManager.ServiceContainer, true);
      this.hostControlSizeManager.ContentControl = (Control) this.editorControl;
      this.RaiseDynamicContentChanged();
    }
    this.editorControl.InitializeData(this.currentObjectType);
    this.editorIsOpen = true;
  }

  private void CloseEditors(bool removeControl)
  {
    if (this.editorControl != null)
    {
      this.editorControl.ApplyChangesIfModified(true);
      if (removeControl)
      {
        this.editorControl.Parent = (Control) null;
        this.editorControl.Dispose();
        this.editorControl = (StandaloneViewSettingsControl) null;
        this.hostControlSizeManager.ContentControl = (Control) null;
        this.RaiseDynamicContentChanged();
      }
    }
    this.editorIsOpen = false;
  }

  private void RaiseDynamicContentChanged()
  {
    if (this.DynamicContentChanged == null)
      return;
    this.DynamicContentChanged((object) this, EventArgs.Empty);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StandaloneViewPage));
    this.scMainContainer = new SplitContainer();
    this.tvObjectTypes = new NavigatorTreeView();
    this.lbSelectTypeHint = new Label();
    this.lbDescription = new Label();
    this.pbImage = new PictureBox();
    this.scMainContainer.BeginInit();
    this.scMainContainer.Panel1.SuspendLayout();
    this.scMainContainer.Panel2.SuspendLayout();
    this.scMainContainer.SuspendLayout();
    this.tvObjectTypes.BeginInit();
    ((ISupportInitialize) this.pbImage).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.scMainContainer, "scMainContainer");
    this.scMainContainer.FixedPanel = FixedPanel.Panel1;
    this.scMainContainer.Name = "scMainContainer";
    this.scMainContainer.Panel1.Controls.Add((Control) this.tvObjectTypes);
    componentResourceManager.ApplyResources((object) this.scMainContainer.Panel1, "scMainContainer.Panel1");
    this.scMainContainer.Panel2.Controls.Add((Control) this.lbSelectTypeHint);
    this.tvObjectTypes.AllowDrop = true;
    this.tvObjectTypes.AllowMultiSelect = false;
    this.tvObjectTypes.AllowUserPinnedColumns = false;
    this.tvObjectTypes.DisableCheckedOutColumn = true;
    componentResourceManager.ApplyResources((object) this.tvObjectTypes, "tvObjectTypes");
    this.tvObjectTypes.LineStyle = LineStyle.Dot;
    this.tvObjectTypes.Name = "tvObjectTypes";
    this.tvObjectTypes.SelectBeforeEdit = true;
    this.tvObjectTypes.ShowRootRow = false;
    this.tvObjectTypes.SuppressErrorMessages = true;
    this.tvObjectTypes.AfterFocusNode += new EventHandler<NavigatorTreeNodeEventArgs>(this.tvObjectTypes_AfterFocusNode);
    componentResourceManager.ApplyResources((object) this.lbSelectTypeHint, "lbSelectTypeHint");
    this.lbSelectTypeHint.Name = "lbSelectTypeHint";
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this.pbImage, "pbImage");
    this.pbImage.Name = "pbImage";
    this.pbImage.TabStop = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.scMainContainer);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.pbImage);
    this.MinimumSize = new Size(700, 400);
    this.Name = nameof (StandaloneViewPage);
    this.scMainContainer.Panel1.ResumeLayout(false);
    this.scMainContainer.Panel2.ResumeLayout(false);
    this.scMainContainer.EndInit();
    this.scMainContainer.ResumeLayout(false);
    this.tvObjectTypes.EndInit();
    ((ISupportInitialize) this.pbImage).EndInit();
    this.ResumeLayout(false);
  }
}
