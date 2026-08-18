// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.LaunchActionsPage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Infralution.Controls.VirtualTree;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
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

internal sealed class LaunchActionsPage : UserControl, IPageControl, IDisposable
{
  private IPagerControl pager;
  private ToolSecurityContext securityContext;
  private TargetSelector targetSelector;
  private int currentObjectType;
  private bool editOpen;
  private LaunchActionEditorEvents editorEvents;
  private IContainer components;
  private NavigatorTreeView tvObjectTypes;
  private Label lbDescription;
  private PictureBox pbImage;
  private SplitContainer scMainContainer;
  private TabControl tcPages;
  private TabPage tpLaunchActions;
  private TabPage tpDefaults;
  private DefaultActionsEditor defaultsEditor;
  private LaunchActionsEditor actionsEditor;

  public LaunchActionsPage()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1628);
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
    if (this.editOpen)
      this.CloseEditors();
    this.CloseSecurityContext();
    this.pager = (IPagerControl) null;
  }

  public event EventHandler DynamicContentChanged;

  private void InitSecurityContext()
  {
    this.securityContext = new ToolSecurityContext();
    this.targetSelector = new TargetSelector();
    this.targetSelector.Attach(this.pager.Toolbar, this.securityContext);
    this.targetSelector.TargetChanged += (EventHandler) ((sender, e) => this.ReopenEditors());
  }

  private void CloseSecurityContext() => this.targetSelector.Detach();

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
    if (this.currentObjectType != -1)
      this.CloseEditors();
    IDBObjectTypeID data = (IDBObjectTypeID) this.tvObjectTypes.GetNodeHandler(e.Node).GetData(e.Node.NodeID, typeof (IDBObjectTypeID));
    if (data != null)
    {
      this.currentObjectType = data.Value;
      this.OpenEditors();
    }
    else
      this.currentObjectType = -1;
  }

  private void InitEditors() => this.editOpen = false;

  private void OpenEditors()
  {
    Guid guid;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      guid = ((IDBGuid) sessionKeeper.Session.GetObjectType(this.currentObjectType, true)).GUID;
    this.editorEvents = new LaunchActionEditorEvents();
    this.actionsEditor.InitEditor(guid, this.securityContext, this.editorEvents);
    this.defaultsEditor.InitEditor(guid, this.securityContext, this.editorEvents);
    this.editOpen = true;
  }

  private void CloseEditors()
  {
    this.actionsEditor.CloseEditor();
    this.defaultsEditor.CloseEditor();
    this.editorEvents = (LaunchActionEditorEvents) null;
    this.editOpen = false;
  }

  private void ReopenEditors()
  {
    if (this.currentObjectType == -1)
      return;
    this.CloseEditors();
    this.OpenEditors();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LaunchActionsPage));
    this.lbDescription = new Label();
    this.pbImage = new PictureBox();
    this.scMainContainer = new SplitContainer();
    this.tvObjectTypes = new NavigatorTreeView();
    this.tcPages = new TabControl();
    this.tpLaunchActions = new TabPage();
    this.tpDefaults = new TabPage();
    this.actionsEditor = new LaunchActionsEditor();
    this.defaultsEditor = new DefaultActionsEditor();
    ((ISupportInitialize) this.pbImage).BeginInit();
    this.scMainContainer.BeginInit();
    this.scMainContainer.Panel1.SuspendLayout();
    this.scMainContainer.Panel2.SuspendLayout();
    this.scMainContainer.SuspendLayout();
    this.tvObjectTypes.BeginInit();
    this.tcPages.SuspendLayout();
    this.tpLaunchActions.SuspendLayout();
    this.tpDefaults.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this.pbImage, "pbImage");
    this.pbImage.Name = "pbImage";
    this.pbImage.TabStop = false;
    componentResourceManager.ApplyResources((object) this.scMainContainer, "scMainContainer");
    this.scMainContainer.FixedPanel = FixedPanel.Panel1;
    this.scMainContainer.Name = "scMainContainer";
    this.scMainContainer.Panel1.Controls.Add((Control) this.tvObjectTypes);
    this.scMainContainer.Panel2.Controls.Add((Control) this.tcPages);
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
    this.tcPages.Controls.Add((Control) this.tpLaunchActions);
    this.tcPages.Controls.Add((Control) this.tpDefaults);
    componentResourceManager.ApplyResources((object) this.tcPages, "tcPages");
    this.tcPages.Name = "tcPages";
    this.tcPages.SelectedIndex = 0;
    this.tpLaunchActions.Controls.Add((Control) this.actionsEditor);
    componentResourceManager.ApplyResources((object) this.tpLaunchActions, "tpLaunchActions");
    this.tpLaunchActions.Name = "tpLaunchActions";
    this.tpLaunchActions.UseVisualStyleBackColor = true;
    this.tpDefaults.Controls.Add((Control) this.defaultsEditor);
    componentResourceManager.ApplyResources((object) this.tpDefaults, "tpDefaults");
    this.tpDefaults.Name = "tpDefaults";
    this.tpDefaults.UseVisualStyleBackColor = true;
    this.actionsEditor.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.actionsEditor, "actionsEditor");
    this.actionsEditor.Name = "actionsEditor";
    this.defaultsEditor.BackColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.defaultsEditor, "defaultsEditor");
    this.defaultsEditor.Name = "defaultsEditor";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.scMainContainer);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.pbImage);
    this.MinimumSize = new Size(700, 400);
    this.Name = nameof (LaunchActionsPage);
    ((ISupportInitialize) this.pbImage).EndInit();
    this.scMainContainer.Panel1.ResumeLayout(false);
    this.scMainContainer.Panel2.ResumeLayout(false);
    this.scMainContainer.EndInit();
    this.scMainContainer.ResumeLayout(false);
    this.tvObjectTypes.EndInit();
    this.tcPages.ResumeLayout(false);
    this.tpLaunchActions.ResumeLayout(false);
    this.tpDefaults.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
