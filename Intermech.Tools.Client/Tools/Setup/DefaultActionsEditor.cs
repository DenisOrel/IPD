// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.DefaultActionsEditor
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

internal sealed class DefaultActionsEditor : UserControl
{
  private readonly string ActionIsNotSet = LocalizationHolder.rm.GetString("Tools.Client_166");
  private Guid objectType;
  private ToolSecurityContext securityContext;
  private LaunchActionEditorEvents editorEvents;
  private List<DefaultActionsEditor.ActionDescriptor> descriptors;
  private SizeF? realAutoScaleFactor;
  private IContainer components;
  private TableLayoutPanel tlpDefaultActions;

  public DefaultActionsEditor() => this.InitializeComponent();

  public void InitEditor(
    Guid objectType,
    ToolSecurityContext securityContext,
    LaunchActionEditorEvents editorEvents)
  {
    this.objectType = objectType;
    this.securityContext = securityContext;
    this.editorEvents = editorEvents;
    this.InitActionGrid(this.tlpDefaultActions);
    this.InitEditorEvents();
  }

  public void CloseEditor()
  {
    this.tlpDefaultActions.Controls.Clear();
    this.editorEvents = (LaunchActionEditorEvents) null;
    this.securityContext = (ToolSecurityContext) null;
    this.objectType = Guid.Empty;
  }

  private void InitActionGrid(TableLayoutPanel panel)
  {
    SizeF size = new SizeF(95f, 25f);
    Padding padding1 = new Padding(3);
    Padding padding2 = padding1;
    ++padding2.Top;
    ++padding2.Bottom;
    if (this.realAutoScaleFactor.HasValue)
    {
      SizeF factor = this.realAutoScaleFactor.Value;
      size = this.ScaleSize(size, factor);
      padding1 = this.ScalePadding(padding1, factor);
      padding2 = this.ScalePadding(padding2, factor);
    }
    LaunchType[] values = (LaunchType[]) Enum.GetValues(typeof (LaunchType));
    Guid objectType = this.GetObjectType();
    panel.AutoSize = true;
    panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    panel.ColumnCount = 4;
    panel.ColumnStyles.Clear();
    panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, size.Width + (float) padding1.Horizontal));
    panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, size.Width + (float) padding1.Horizontal));
    panel.RowCount = values.Length;
    panel.RowStyles.Clear();
    this.descriptors = new List<DefaultActionsEditor.ActionDescriptor>(values.Length);
    for (int row = 0; row < values.Length; ++row)
    {
      LaunchType launchType = values[row];
      LaunchActionInfo defaultAction;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        defaultAction = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).GetDefaultAction(objectType, this.securityContext.ActiveTarget.Target, launchType);
      panel.RowStyles.Add(new RowStyle(SizeType.Absolute, size.Height + (float) padding1.Vertical));
      Label label = new Label();
      label.Name = $"lb{Enum.GetName(typeof (LaunchType), (object) launchType)}";
      label.Text = EnumTypeHelper.GetCaption((Enum) launchType);
      label.TextAlign = ContentAlignment.MiddleLeft;
      label.AutoSize = true;
      label.Dock = DockStyle.Fill;
      panel.Controls.Add((Control) label, 0, row);
      Label textControl = new Label();
      textControl.Name = $"lb{Enum.GetName(typeof (LaunchType), (object) launchType)}Action";
      textControl.Text = defaultAction != null ? defaultAction.DisplayName : this.ActionIsNotSet;
      textControl.TextAlign = ContentAlignment.MiddleLeft;
      textControl.AutoSize = false;
      textControl.BackColor = SystemColors.Window;
      textControl.BorderStyle = BorderStyle.FixedSingle;
      textControl.Margin = padding2;
      textControl.Dock = DockStyle.Fill;
      panel.Controls.Add((Control) textControl, 1, row);
      Button button1 = new Button();
      button1.Name = $"btSelect{Enum.GetName(typeof (LaunchType), (object) launchType)}";
      button1.Text = LocalizationHolder.rm.GetString("Tools.Client_163");
      button1.Enabled = this.securityContext.CanEditTargetSettings;
      button1.Dock = DockStyle.Fill;
      panel.Controls.Add((Control) button1, 2, row);
      Button button2 = new Button();
      button2.Name = $"btReset{Enum.GetName(typeof (LaunchType), (object) launchType)}";
      button2.Text = LocalizationHolder.rm.GetString("Tools.Client_164");
      button2.Enabled = this.securityContext.CanEditTargetSettings;
      button2.Dock = DockStyle.Fill;
      panel.Controls.Add((Control) button2, 3, row);
      DefaultActionsEditor.ActionDescriptor descriptor = new DefaultActionsEditor.ActionDescriptor(defaultAction, launchType, textControl);
      button1.Click += (EventHandler) ((sender, e) => this.SelectDefaultAction(descriptor));
      button2.Click += (EventHandler) ((sender, e) => this.ResetDefaultAction(descriptor));
      this.descriptors.Add(descriptor);
    }
  }

  private SizeF ScaleSize(SizeF size, SizeF factor)
  {
    return new SizeF(size.Width * factor.Width, size.Height * factor.Height);
  }

  private Padding ScalePadding(Padding padding, SizeF factor)
  {
    return new Padding((int) Math.Round((double) padding.Left * (double) factor.Width), (int) Math.Round((double) padding.Top * (double) factor.Height), (int) Math.Round((double) padding.Right * (double) factor.Width), (int) Math.Round((double) padding.Bottom * (double) factor.Height));
  }

  private Guid GetObjectType()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((IDBGuid) sessionKeeper.Session.GetObjectType(this.objectType, true)).GUID;
  }

  private void SelectDefaultAction(DefaultActionsEditor.ActionDescriptor descriptor)
  {
    Guid objectType = this.GetObjectType();
    List<LaunchActionInfo> launchActionInfoList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      launchActionInfoList = ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).LookupActionList(objectType, this.securityContext.ActiveTarget.Target, descriptor.LaunchType);
    List<object> objectList = new List<object>(launchActionInfoList.Count);
    foreach (LaunchActionInfo launchActionInfo in launchActionInfoList)
      objectList.Add((object) launchActionInfo);
    SelectItemForm currentControl = new SelectItemForm();
    currentControl.Text = LocalizationHolder.rm.GetString("Tools.Client_165");
    currentControl.Description = LocalizationHolder.rm.GetString("Tools.Client_213");
    currentControl.Items = (IEnumerable) objectList;
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1629);
    if (currentControl.ShowDialog() != DialogResult.OK)
      return;
    LaunchActionInfo selectedItem = (LaunchActionInfo) currentControl.SelectedItem;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).SetDefaultAction(objectType, this.securityContext.ActiveTarget.Target, selectedItem.ActionId);
      descriptor.ActionInfo = selectedItem;
      descriptor.TextControl.Text = selectedItem.DisplayName;
    }
  }

  private void ResetDefaultAction(DefaultActionsEditor.ActionDescriptor descriptor)
  {
    if (descriptor.ActionInfo == null)
      return;
    Guid objectType = this.GetObjectType();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      ServiceUtils.GetService<ILaunchActionServer>((object) sessionKeeper.Session, true).ResetDefaultAction(objectType, this.securityContext.ActiveTarget.Target, descriptor.ActionInfo.ActionId);
    descriptor.TextControl.Text = this.ActionIsNotSet;
  }

  private void InitEditorEvents()
  {
    this.editorEvents.LaunchActionUpdated += new EventHandler<LaunchActionArgs>(this.OnLaunchActionUpdated);
    this.editorEvents.LaunchActionRemoved += new EventHandler<LaunchActionArgs>(this.OnLaunchActionRemoved);
  }

  private void OnLaunchActionRemoved(object sender, LaunchActionArgs e)
  {
    foreach (DefaultActionsEditor.ActionDescriptor descriptor in this.descriptors)
    {
      if (descriptor.ActionInfo != null && descriptor.ActionInfo.ActionId == e.ActionInfo.ActionId)
      {
        descriptor.ActionInfo = (LaunchActionInfo) null;
        descriptor.TextControl.Text = this.ActionIsNotSet;
        break;
      }
    }
  }

  private void OnLaunchActionUpdated(object sender, LaunchActionArgs e)
  {
    foreach (DefaultActionsEditor.ActionDescriptor descriptor in this.descriptors)
    {
      if (descriptor.ActionInfo != null && descriptor.ActionInfo.ActionId == e.ActionInfo.ActionId)
      {
        descriptor.ActionInfo = e.ActionInfo;
        descriptor.TextControl.Text = e.ActionInfo.DisplayName;
        break;
      }
    }
  }

  protected override void OnParentChanged(EventArgs e)
  {
    base.OnParentChanged(e);
    if (this.Parent != null)
      return;
    this.realAutoScaleFactor = new SizeF?();
  }

  protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
  {
    base.ScaleControl(factor, specified);
    if (this.realAutoScaleFactor.HasValue)
      return;
    this.realAutoScaleFactor = new SizeF?(factor);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DefaultActionsEditor));
    this.tlpDefaultActions = new TableLayoutPanel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tlpDefaultActions, "tlpDefaultActions");
    this.tlpDefaultActions.Name = "tlpDefaultActions";
    this.BackColor = Color.Transparent;
    this.Controls.Add((Control) this.tlpDefaultActions);
    this.DoubleBuffered = true;
    this.Name = nameof (DefaultActionsEditor);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.ResumeLayout(false);
  }

  private class ActionDescriptor
  {
    private LaunchActionInfo actionInfo;
    private LaunchType launchType;
    private Label textControl;

    public ActionDescriptor(LaunchActionInfo actionInfo, LaunchType launchType, Label textControl)
    {
      this.actionInfo = actionInfo;
      this.launchType = launchType;
      this.textControl = textControl;
    }

    public LaunchActionInfo ActionInfo
    {
      get => this.actionInfo;
      set => this.actionInfo = value;
    }

    public LaunchType LaunchType => this.launchType;

    public Label TextControl => this.textControl;
  }
}
