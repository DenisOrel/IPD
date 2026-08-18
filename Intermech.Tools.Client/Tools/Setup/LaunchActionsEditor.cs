// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.LaunchActionsEditor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.LaunchActions;
using Intermech.Tools.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class LaunchActionsEditor : UserControl
{
  private Guid objectType;
  private ToolSecurityContext securityContext;
  private LaunchActionEditorEvents editorEvents;
  private IContainer components;
  private Label lbActions;
  private ListView lvActions;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private Button btCreateAction;
  private Button btRemoveAction;
  private Button btProperties;
  private ImageList ilActions;

  public LaunchActionsEditor() => this.InitializeComponent();

  public void InitEditor(
    Guid objectType,
    ToolSecurityContext securityContext,
    LaunchActionEditorEvents editorEvents)
  {
    this.objectType = objectType;
    this.securityContext = securityContext;
    this.editorEvents = editorEvents;
    this.ShowActions();
    this.InitEditorEvents();
    this.btCreateAction.Enabled = this.securityContext.CanEditTargetSettings;
  }

  public void CloseEditor()
  {
    this.btCreateAction.Enabled = false;
    this.btRemoveAction.Enabled = false;
    this.btProperties.Enabled = false;
    this.ClearActions();
    this.editorEvents = (LaunchActionEditorEvents) null;
    this.securityContext = (ToolSecurityContext) null;
    this.objectType = Guid.Empty;
  }

  private void ShowActions()
  {
    foreach (LaunchType launchType in (LaunchType[]) Enum.GetValues(typeof (LaunchType)))
      this.ShowActions(this.GetActions(this.objectType, launchType), launchType, this.lvActions);
  }

  private void ClearActions() => this.lvActions.Items.Clear();

  private List<LaunchActionInfo> GetActions(Guid objectType, LaunchType launchType)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).GetActionList(objectType, this.securityContext.ActiveTarget.Target, launchType);
  }

  private void lvActions_SelectedIndexChanged(object sender, EventArgs e)
  {
    bool flag = this.securityContext.CanEditTargetSettings && this.lvActions.SelectedItems.Count > 0;
    this.btProperties.Enabled = flag;
    this.btRemoveAction.Enabled = flag;
  }

  private void OnActionDoubleClick(object sender, EventArgs e)
  {
    if (this.lvActions.SelectedItems.Count == 0)
      return;
    this.btProperties.PerformClick();
  }

  private void ShowActions(
    List<LaunchActionInfo> actions,
    LaunchType launchType,
    ListView listView)
  {
    listView.BeginUpdate();
    try
    {
      foreach (LaunchActionInfo action in actions)
        listView.Items.Add(this.MakeListViewItem(action, launchType));
      if (listView.Items.Count <= 0)
        return;
      listView.Items[0].Selected = true;
    }
    finally
    {
      listView.EndUpdate();
    }
  }

  private void ShowAction(LaunchActionInfo action, LaunchType launchType, ListView listView)
  {
    ListViewItem listViewItem = this.MakeListViewItem(action, launchType);
    listView.Items.Add(listViewItem);
    listViewItem.Selected = true;
  }

  private ListViewItem MakeListViewItem(LaunchActionInfo action, LaunchType launchType)
  {
    return new ListViewItem()
    {
      Text = EnumTypeHelper.GetCaption((Enum) launchType),
      SubItems = {
        action.DisplayName
      },
      ImageKey = Enum.GetName(typeof (LaunchType), (object) launchType),
      Tag = (object) action
    };
  }

  private void btCreateAction_Click(object sender, EventArgs e)
  {
    LaunchType[] values = (LaunchType[]) Enum.GetValues(typeof (LaunchType));
    List<LaunchActionsEditor.LaunchTypeItem> launchTypeItemList = new List<LaunchActionsEditor.LaunchTypeItem>(values.Length);
    foreach (LaunchType launchType in values)
      launchTypeItemList.Add(new LaunchActionsEditor.LaunchTypeItem(launchType, EnumTypeHelper.GetCaption((Enum) launchType)));
    SelectItemForm currentControl1 = new SelectItemForm();
    currentControl1.Items = (IEnumerable) launchTypeItemList;
    currentControl1.Text = LocalizationHolder.rm.GetString("Tools.Client_167");
    currentControl1.Description = LocalizationHolder.rm.GetString("Tools.Client_214");
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl1, 1629);
    if (currentControl1.ShowDialog() != DialogResult.OK)
      return;
    LaunchType launchType1 = ((LaunchActionsEditor.LaunchTypeItem) currentControl1.SelectedItem).LaunchType;
    List<ILaunchHandler> handlers = ClientContext.LaunchActions.GetHandlers();
    List<LaunchActionsEditor.ActionTemplate> actionTemplateList = new List<LaunchActionsEditor.ActionTemplate>(handlers.Count);
    foreach (ILaunchHandler launchHandler in handlers)
      actionTemplateList.Add(new LaunchActionsEditor.ActionTemplate(launchHandler.Id, launchHandler.DisplayName, launchHandler.GetServerObjectTemplate()));
    actionTemplateList.Sort((Comparison<LaunchActionsEditor.ActionTemplate>) ((x, y) => StringComparer.CurrentCultureIgnoreCase.Compare(x.HandlerName, y.HandlerName)));
    SelectItemForm currentControl2 = new SelectItemForm();
    currentControl2.Items = (IEnumerable) actionTemplateList;
    currentControl2.Text = LocalizationHolder.rm.GetString("Tools.Client_167");
    currentControl2.Description = LocalizationHolder.rm.GetString("Tools.Client_215");
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl2, 1629);
    if (currentControl2.ShowDialog() != DialogResult.OK)
      return;
    LaunchActionsEditor.ActionTemplate selectedItem = (LaunchActionsEditor.ActionTemplate) currentControl2.SelectedItem;
    LaunchActionInfo action;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      action = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).CreateAction(this.objectType, this.securityContext.ActiveTarget.Target, launchType1, selectedItem.HandlerId, selectedItem.ServerObjectXml);
    this.ShowAction(action, launchType1, this.lvActions);
  }

  private void btProperties_Click(object sender, EventArgs e)
  {
    LaunchActionInfo tag = (LaunchActionInfo) this.lvActions.SelectedItems[0].Tag;
    LaunchActionInfo newActionInfo = (LaunchActionInfo) null;
    Form form = new Form();
    form.Text = string.Format(LocalizationHolder.rm.GetString("Tools.Client_212"), (object) tag.DisplayName);
    form.StartPosition = FormStartPosition.CenterParent;
    form.Size = new Size(700, 450);
    form.MinimumSize = form.Size;
    form.MinimizeBox = false;
    form.MaximizeBox = false;
    form.Padding = new Padding(4);
    LaunchActionDataPage dataPage = new LaunchActionDataPage();
    dataPage.Parent = (Control) form;
    dataPage.Dock = DockStyle.Fill;
    dataPage.InfoUpdated += (EventHandler) ((x, y) =>
    {
      newActionInfo = dataPage.SelectedAction;
      form.Text = string.Format(LocalizationHolder.rm.GetString("Tools.Client_212"), (object) newActionInfo.DisplayName);
    });
    dataPage.PageClose += (EventHandler) ((x, y) => form.Close());
    dataPage.InitializePage(tag, false);
    form.ActiveControl = (Control) dataPage;
    int num = (int) form.ShowDialog();
    if (newActionInfo != null)
      this.editorEvents.FireLaunchActionUpdated(newActionInfo);
    this.lvActions.Focus();
  }

  private void btRemoveAction_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_168"), LocalizationHolder.rm.GetString("Tools.Client_169"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    LaunchActionInfo tag = (LaunchActionInfo) this.lvActions.SelectedItems[0].Tag;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).RemoveAction(tag.ActionId);
    this.editorEvents.FireLaunchActionRemoved(tag);
    this.lvActions.Focus();
  }

  private void InitEditorEvents()
  {
    this.editorEvents.LaunchActionUpdated += new EventHandler<LaunchActionArgs>(this.OnLaunchActionUpdated);
    this.editorEvents.LaunchActionRemoved += new EventHandler<LaunchActionArgs>(this.OnLaunchActionRemoved);
  }

  private void OnLaunchActionUpdated(object sender, LaunchActionArgs e)
  {
    foreach (ListViewItem listViewItem in this.lvActions.Items)
    {
      if (((LaunchActionInfo) listViewItem.Tag).ActionId == e.ActionInfo.ActionId)
      {
        listViewItem.SubItems[1].Text = e.ActionInfo.DisplayName;
        break;
      }
    }
  }

  private void OnLaunchActionRemoved(object sender, LaunchActionArgs e)
  {
    this.RemoveItem(e.ActionInfo, this.lvActions);
  }

  private void RemoveItem(LaunchActionInfo actionInfo, ListView listView)
  {
    foreach (ListViewItem listViewItem in listView.Items)
    {
      if (((LaunchActionInfo) listViewItem.Tag).ActionId == actionInfo.ActionId)
      {
        int index = listViewItem.Index;
        listView.Items.RemoveAt(index);
        if (listView.Items.Count <= 0)
          break;
        if (index >= listView.Items.Count)
          --index;
        listView.Items[index].Selected = true;
        break;
      }
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (LaunchActionsEditor));
    this.lbActions = new Label();
    this.lvActions = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.ilActions = new ImageList(this.components);
    this.btCreateAction = new Button();
    this.btRemoveAction = new Button();
    this.btProperties = new Button();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbActions, "lbActions");
    this.lbActions.Name = "lbActions";
    componentResourceManager.ApplyResources((object) this.lvActions, "lvActions");
    this.lvActions.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.lvActions.FullRowSelect = true;
    this.lvActions.GridLines = true;
    this.lvActions.HideSelection = false;
    this.lvActions.MultiSelect = false;
    this.lvActions.Name = "lvActions";
    this.lvActions.SmallImageList = this.ilActions;
    this.lvActions.UseCompatibleStateImageBehavior = false;
    this.lvActions.View = View.Details;
    this.lvActions.SelectedIndexChanged += new EventHandler(this.lvActions_SelectedIndexChanged);
    this.lvActions.DoubleClick += new EventHandler(this.OnActionDoubleClick);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    this.ilActions.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("ilActions.ImageStream");
    this.ilActions.TransparentColor = Color.Transparent;
    this.ilActions.Images.SetKeyName(0, "Edit");
    this.ilActions.Images.SetKeyName(1, "View");
    this.ilActions.Images.SetKeyName(2, "Print");
    componentResourceManager.ApplyResources((object) this.btCreateAction, "btCreateAction");
    this.btCreateAction.Name = "btCreateAction";
    this.btCreateAction.UseVisualStyleBackColor = true;
    this.btCreateAction.Click += new EventHandler(this.btCreateAction_Click);
    componentResourceManager.ApplyResources((object) this.btRemoveAction, "btRemoveAction");
    this.btRemoveAction.Name = "btRemoveAction";
    this.btRemoveAction.UseVisualStyleBackColor = true;
    this.btRemoveAction.Click += new EventHandler(this.btRemoveAction_Click);
    componentResourceManager.ApplyResources((object) this.btProperties, "btProperties");
    this.btProperties.Name = "btProperties";
    this.btProperties.UseVisualStyleBackColor = true;
    this.btProperties.Click += new EventHandler(this.btProperties_Click);
    this.BackColor = Color.Transparent;
    this.Controls.Add((Control) this.btProperties);
    this.Controls.Add((Control) this.btRemoveAction);
    this.Controls.Add((Control) this.btCreateAction);
    this.Controls.Add((Control) this.lvActions);
    this.Controls.Add((Control) this.lbActions);
    this.Name = nameof (LaunchActionsEditor);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class LaunchTypeItem
  {
    private LaunchType launchType;
    private string displayName;

    public LaunchTypeItem(LaunchType launchType, string displayName)
    {
      this.launchType = launchType;
      this.displayName = displayName;
    }

    public LaunchType LaunchType => this.launchType;

    public string DisplayName => this.displayName;

    public override string ToString() => this.DisplayName;
  }

  private sealed class ActionTemplate
  {
    private readonly Guid handlerId;
    private readonly string handlerName;
    private readonly string serverObjectXml;

    public ActionTemplate(Guid handlerId, string handlerName, string serverObjectXml)
    {
      this.handlerId = handlerId;
      this.handlerName = handlerName;
      this.serverObjectXml = serverObjectXml;
    }

    public Guid HandlerId => this.handlerId;

    public string HandlerName => this.handlerName;

    public string ServerObjectXml => this.serverObjectXml;

    public override string ToString() => this.HandlerName;
  }
}
