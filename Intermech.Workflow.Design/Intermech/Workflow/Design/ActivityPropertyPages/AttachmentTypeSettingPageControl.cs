// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.AttachmentTypeSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class AttachmentTypeSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private Intermech.Workflow.Design.AdvNavigatorTreeView _attachTypesView;
  private bool _attachTypesViewLoading;
  private AllowedTypes _attachTypes;
  private List<NavigatorTreeNode> _expandedNodes = new List<NavigatorTreeNode>();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox SchemeAttachsGB;

  public AttachmentTypeSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value);
    }
  }

  public void LoadAttachmentTypeSettingControl(ActivitySettings settings)
  {
    this._settings = settings;
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);
    this.InitSchemeAttachTypes();
  }

  private void InitSchemeAttachTypes()
  {
    if (this._attachTypesView != null || this._settings == null)
      return;
    this._attachTypes = new AllowedTypes(this._settings.ActivityObjectID);
    DescriptorCollection descriptors = new DescriptorCollection();
    foreach (int allAttachType in this._attachTypes.AllAttachTypes)
      descriptors.Add((IDescriptor) new Intermech.Navigator.DBObjectTypes.Descriptor(allAttachType));
    Intermech.Navigator.CustomNode.Descriptor rootDescriptor = new Intermech.Navigator.CustomNode.Descriptor("Все разрешенные типы вложений", descriptors);
    Intermech.Workflow.Design.AdvNavigatorTreeView navigatorTreeView = new Intermech.Workflow.Design.AdvNavigatorTreeView();
    navigatorTreeView.Parent = (Control) this.SchemeAttachsGB;
    navigatorTreeView.Dock = DockStyle.Fill;
    navigatorTreeView.CheckBoxStyle = NavigatorTreeViewCheckBoxStyle.ThreeState;
    this._attachTypesView = navigatorTreeView;
    this._attachTypesView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    this._attachTypesView.BeforeSetCheckState = new BeforeSetCheckStateEventHandler(this.BeforeSetCheckState);
    this._attachTypesView.AfterExpand += new EventHandler<NodeEventArgs>(this._attachTypesView_AfterExpand);
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog));
    serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    serviceContainer.AddService(typeof (IObjectTypeNodeOptionsHolder), (object) new ObjectTypeNodeOptionsHolder(ObjectTypeNodeOptions.OnlyTypesMode));
    this._attachTypesView.Services = (System.IServiceProvider) serviceContainer;
    this._attachTypesView.Build((IDescriptor) rootDescriptor);
    this._attachTypesViewLoading = true;
    try
    {
      if (this._attachTypesView.Nodes.Count <= 0)
        return;
      this._attachTypesView.Nodes[0].CheckState = CheckState.Checked;
    }
    finally
    {
      this._attachTypesViewLoading = false;
    }
  }

  private void BeforeSetCheckState(NavigatorTreeNode node, ref CheckState checkState)
  {
    int atype = !(node.NodeID is NodeID nodeId) ? 3 : nodeId.TypeID;
    if (this._attachTypesViewLoading)
      checkState = this._attachTypes.CalcCheckState(atype);
    else if (checkState == CheckState.Unchecked)
    {
      if ((node.State & NavigatorTreeNode.UpdateState.UpdatedAsChild) != NavigatorTreeNode.UpdateState.None)
        return;
      this._attachTypes.IDs.Remove(atype);
      this._attachTypes.IDs.Add(-atype);
      foreach (int typeChild in this._attachTypes.GetTypeChildren(atype, true))
      {
        this._attachTypes.IDs.Remove(typeChild);
        this._attachTypes.IDs.Remove(-typeChild);
      }
    }
    else
    {
      if ((node.State & NavigatorTreeNode.UpdateState.UpdatedAsChild) != NavigatorTreeNode.UpdateState.None)
        return;
      this._attachTypes.IDs.Remove(-atype);
      this._attachTypes.IDs.Add(atype);
      foreach (int typeChild in this._attachTypes.GetTypeChildren(atype, true))
      {
        this._attachTypes.IDs.Remove(-typeChild);
        this._attachTypes.IDs.Remove(typeChild);
      }
    }
  }

  private void _attachTypesView_AfterExpand(object sender, NodeEventArgs e)
  {
    NavigatorTreeNode node = e.Node;
    if (this._expandedNodes.Contains(node))
      return;
    CheckState checkState = node.CheckState;
    this._attachTypesViewLoading = true;
    try
    {
      foreach (NavigatorTreeNode child in (List<NavigatorTreeNode>) node.Children)
        child.CheckState = checkState;
    }
    finally
    {
      this._attachTypesViewLoading = false;
    }
    this._expandedNodes.Add(node);
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    if (this._attachTypes != null)
    {
      string asString = this._attachTypes.AsString;
      if (asString != this._attachTypes.PrevAsString)
      {
        activityToSave.Attributes.AddAttribute(wfConsts.AttrAllowedAttachTypesID, false, new object[1]
        {
          (object) asString
        });
        modified = true;
      }
    }
    return modified;
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.SchemeAttachsGB = new GroupBox();
    this.SuspendLayout();
    this.SchemeAttachsGB.Dock = DockStyle.Fill;
    this.SchemeAttachsGB.Location = new Point(0, 0);
    this.SchemeAttachsGB.Name = "SchemeAttachsGB";
    this.SchemeAttachsGB.Padding = new Padding(10, 11, 11, 11);
    this.SchemeAttachsGB.Size = new Size(495, 394);
    this.SchemeAttachsGB.TabIndex = 1;
    this.SchemeAttachsGB.TabStop = false;
    this.SchemeAttachsGB.Text = "Типы объектов, разрешенные для отправки по процессу";
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.SchemeAttachsGB);
    this.Name = nameof (AttachmentTypeSettingPageControl);
    this.Size = new Size(495, 394);
    this.ResumeLayout(false);
  }
}
