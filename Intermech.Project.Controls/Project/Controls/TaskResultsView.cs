// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.TaskResultsView
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Metadata;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow;
using Intermech.Workflow.Design;
using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project.Controls;

[ViewDescriptionProvider(typeof (TaskResultsView.Description))]
public class TaskResultsView : 
  TaskAttachmentsView,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
  IAdvancedView,
  IView,
  IEmbeddedViews,
  IViewData,
  ICommandTarget,
  ISelectedItemsHost,
  INodeView,
  IIOSource,
  IReportView,
  INavigatorContextSearch,
  ISelectedItemsText
{
  private ButtonItem _verifyButton;

  public long VerifySchemeID { get; private set; }

  public bool NeedToFindActualVerifyScheme { get; private set; }

  public long ActualVerifySchemeID
  {
    get
    {
      if (this.VerifySchemeID == 0L || !this.NeedToFindActualVerifyScheme)
        return this.VerifySchemeID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        this.NeedToFindActualVerifyScheme = false;
        QuickObjectInfo objectInfo = session.GetObjectInfo(this.VerifySchemeID);
        if (objectInfo.Empty)
          return this.VerifySchemeID = 0L;
        IDBObject dbObject = session.GetObjectBaseVersionByID(objectInfo.ID, false) ?? session.GetObject(this.VerifySchemeID, false);
        return this.VerifySchemeID = dbObject != null ? dbObject.ObjectID : 0L;
      }
    }
  }

  protected override void AdjustObject(ref IDBObject obj, ref bool readOnly)
  {
    base.AdjustObject(ref obj, ref readOnly);
    this.VerifySchemeID = 0L;
    this.NeedToFindActualVerifyScheme = false;
    if (readOnly || obj == null)
      return;
    IDBAttribute attributeById1 = obj.GetAttributeByID((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.VerifyScheme);
    if (attributeById1 != null)
      this.VerifySchemeID = attributeById1.AsInteger;
    if (this.VerifySchemeID != 0L)
    {
      IDBAttribute attributeById2 = obj.GetAttributeByID(Intermech.Project.Attributes.Flags.ID);
      if (attributeById2 != null)
        this.NeedToFindActualVerifyScheme = ((uint) Convert.ToInt32(attributeById2.AsInteger) & 8U) > 0U;
    }
    this.UpdateCommands();
  }

  protected override void UpdateCommands()
  {
    if (this.VerifySchemeID != 0L && this._verifyButton == null)
    {
      this._verifyButton = new ButtonItem();
      this._verifyButton.Text = Localization.GetString("CmdVerifyResults");
      this._verifyButton.ToolTipText = Localization.GetString("CmdVerifyResultsHint");
      this._verifyButton.CommandName = "VerifyResults";
      this._verifyButton.BeginGroup = true;
      this._verifyButton.ImageIndex = Intermech.Workflow.Images.LaunchProcessImageIndex;
      this._verifyButton.Click += new EventHandler(this.VerifyResults);
      this._verifyButton.ShowText = true;
      this._toolBar.Items.Add((ToolbarItemBase) this._verifyButton);
    }
    else if (this.VerifySchemeID == 0L && this._verifyButton != null)
    {
      this._toolBar.Items.Remove((ToolbarItemBase) this._verifyButton);
      this._verifyButton = (ButtonItem) null;
    }
    if (this._verifyButton == null)
      return;
    this._verifyButton.Enabled = this.Attachments.Count > 0;
  }

  [NotNull]
  protected override ICommandsProvider GetCommandsProvider()
  {
    return (ICommandsProvider) new TaskResultsView.TaskResultsViewCommandsProvider(this);
  }

  protected void VerifyResults([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    long process = wfFunx.CreateProcess(this.ActualVerifySchemeID, this.Attachments);
    if (process == 0L)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetRelationCollection(wfConsts.LinkedTaskRelationTypeID).Create(this.GetObject(sessionKeeper.Session).ObjectID, process);
  }

  protected void VerifyResults(
    [NotNull] ISelectedItems items,
    [NotNull] System.IServiceProvider viewServices,
    [CanBeNull] object additionalInfo)
  {
    this.VerifyResults((object) null, EventArgs.Empty);
  }

  protected override void SetModified(bool value)
  {
    if (this._modified == value)
      return;
    base.SetModified(value);
    this.UpdateCommands();
  }

  protected new class Description : TaskAttachmentsView.Description
  {
    [NotNull]
    public override ViewDescription DoGetViewDescription(
      [NotNull] ISelectedItems selectedItems,
      [CanBeNull] System.IServiceProvider serviceProvider)
    {
      ViewDescription viewDescription = base.DoGetViewDescription(selectedItems, serviceProvider);
      viewDescription.Caption = Localization.GetString("TaskResults");
      viewDescription.ImageIndex = Images.ResultsImageIndex;
      return viewDescription;
    }
  }

  private sealed class TaskResultsViewCommandsProvider : ICommandsProvider
  {
    [NotNull]
    private readonly TaskResultsView _taskResultsView;
    [NotNull]
    private readonly ChildrenViewCommandsProvider _childrenViewCommandsProvider;

    public TaskResultsViewCommandsProvider([NotNull] TaskResultsView taskResultsView)
    {
      this._taskResultsView = taskResultsView;
      this._childrenViewCommandsProvider = new ChildrenViewCommandsProvider((ChildrenView) taskResultsView);
    }

    public CommandsInfo GetMergedCommands(ISelectedItems items, System.IServiceProvider viewServices)
    {
      return this._childrenViewCommandsProvider.GetMergedCommands(items, viewServices);
    }

    public CommandsInfo GetGroupCommands(ISelectedItems items, System.IServiceProvider viewServices)
    {
      CommandsInfo groupCommands = this._childrenViewCommandsProvider.GetGroupCommands(items, viewServices);
      if (this._taskResultsView.VerifySchemeID != 0L && this._taskResultsView.Attachments.Count > 0)
        groupCommands.Add("VerifyResults", new CommandInfo(0, new ClickEventHandler(this._taskResultsView.VerifyResults)));
      return groupCommands;
    }
  }
}
