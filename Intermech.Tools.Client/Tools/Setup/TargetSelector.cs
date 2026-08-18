// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.TargetSelector
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Tools.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class TargetSelector
{
  private ToolSecurityContext securityContext;
  private bool attached;
  private ToolStripLabel tslActiveTarget;
  private ToolStripTextBox tstActiveTarget;
  private ToolStripButton tsbPublicSettings;
  private ToolStripButton tsbPersonalSettings;
  private ToolStripButton tsbAnotherUserSettings;
  private ToolStripSeparator tssRightSeparator;

  public void Attach(ToolStrip toolbar, ToolSecurityContext securityContext)
  {
    if (this.attached)
      this.Detach();
    this.securityContext = securityContext;
    this.InsertControls(toolbar);
    this.DisplayActiveTarget();
    this.attached = true;
  }

  public void Detach()
  {
    if (!this.attached)
      return;
    this.RemoveControls();
    this.attached = false;
  }

  private void InsertControls(ToolStrip toolbar)
  {
    this.tslActiveTarget = new ToolStripLabel();
    this.tslActiveTarget.Name = "tslActiveTarget";
    this.tslActiveTarget.Size = new Size(83, 22);
    this.tslActiveTarget.Text = LocalizationHolder.rm.GetString("Tools.Client_174");
    this.tstActiveTarget = new ToolStripTextBox();
    this.tstActiveTarget.BackColor = SystemColors.Window;
    this.tstActiveTarget.Name = "tstActiveTarget";
    this.tstActiveTarget.ReadOnly = true;
    this.tstActiveTarget.ShortcutsEnabled = false;
    this.tstActiveTarget.Size = new Size(192 /*0xC0*/, 25);
    this.tsbPublicSettings = new ToolStripButton();
    this.tsbPublicSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbPublicSettings.Image = (Image) Intermech.Tools.Client.Properties.Resources.IR_PublicSettings;
    this.tsbPublicSettings.Name = "tsbPublicSettings";
    this.tsbPublicSettings.Size = new Size(23, 22);
    this.tsbPublicSettings.Text = LocalizationHolder.rm.GetString("Tools.Client_173");
    this.tsbPublicSettings.Click += new EventHandler(this.OnSelectPublicSettings);
    this.tsbPublicSettings.Available = this.securityContext.CanEditPublicSettings;
    this.tsbPersonalSettings = new ToolStripButton();
    this.tsbPersonalSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbPersonalSettings.Image = (Image) Intermech.Tools.Client.Properties.Resources.IR_PersonalSettings;
    this.tsbPersonalSettings.Name = "tsbPersonalSettings";
    this.tsbPersonalSettings.Size = new Size(23, 22);
    this.tsbPersonalSettings.Text = LocalizationHolder.rm.GetString("Tools.Client_175");
    this.tsbPersonalSettings.Click += new EventHandler(this.OnSelectPersonalSettings);
    this.tsbPersonalSettings.Available = this.securityContext.CanEditPublicSettings;
    this.tsbAnotherUserSettings = new ToolStripButton();
    this.tsbAnotherUserSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbAnotherUserSettings.Image = (Image) Intermech.Tools.Client.Properties.Resources.IR_AnotherUserSettings;
    this.tsbAnotherUserSettings.Name = "tsbAnotherUserSettings";
    this.tsbAnotherUserSettings.Size = new Size(23, 22);
    this.tsbAnotherUserSettings.Text = LocalizationHolder.rm.GetString("Tools.Client_176");
    this.tsbAnotherUserSettings.ToolTipText = LocalizationHolder.rm.GetString("Tools.Client_177");
    this.tsbAnotherUserSettings.Click += new EventHandler(this.OnAnotherUserSettings);
    this.tsbAnotherUserSettings.Available = this.securityContext.CanOverrideTarget;
    this.tssRightSeparator = new ToolStripSeparator();
    this.tssRightSeparator.Margin = new Padding(2, 0, 2, 0);
    this.tssRightSeparator.Name = "tssRightSeparator";
    this.tssRightSeparator.Size = new Size(6, 25);
    toolbar.Items.Add((ToolStripItem) this.tslActiveTarget);
    toolbar.Items.Add((ToolStripItem) this.tstActiveTarget);
    toolbar.Items.Add((ToolStripItem) this.tsbPublicSettings);
    toolbar.Items.Add((ToolStripItem) this.tsbPersonalSettings);
    toolbar.Items.Add((ToolStripItem) this.tsbAnotherUserSettings);
    toolbar.Items.Add((ToolStripItem) this.tssRightSeparator);
  }

  private void RemoveControls()
  {
    this.tssRightSeparator.Dispose();
    this.tsbAnotherUserSettings.Dispose();
    this.tsbPersonalSettings.Dispose();
    this.tsbPublicSettings.Dispose();
    this.tstActiveTarget.Dispose();
    this.tslActiveTarget.Dispose();
  }

  private void DisplayActiveTarget()
  {
    this.tstActiveTarget.Text = this.securityContext.ActiveTarget.DisplayName;
  }

  private void OnSelectPublicSettings(object sender, EventArgs e)
  {
    this.ChangeActiveTarget(TargetDescriptor.PublicSettings);
  }

  private void OnSelectPersonalSettings(object sender, EventArgs e)
  {
    this.ChangeActiveTarget(TargetDescriptor.CurrentUser);
  }

  private void OnAnotherUserSettings(object sender, EventArgs e)
  {
    ConditionStructure conditionStructure1 = new ConditionStructure(-50, RelationalOperators.NotEmpty, (object) null, LogicalOperators.AND, 0, true);
    ConditionStructure conditionStructure2 = new ConditionStructure(-12, RelationalOperators.NotEqual, (object) "CAD0000D-306C-11D8-B4E9-00304F19F545", LogicalOperators.NONE, 0, true);
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[3]
    {
      (object) ObligatoryObjectAttributes.F_GUID,
      (object) ObligatoryObjectAttributes.F_OBJECT_ID,
      (object) ObligatoryObjectAttributes.CAPTION
    };
    paramSet.SortColumns = new object[1]
    {
      (object) ObligatoryObjectAttributes.CAPTION
    };
    paramSet.Orders = new SortOrders[1]{ SortOrders.ASC };
    paramSet.Conditions = new ConditionStructure[2]
    {
      conditionStructure1,
      conditionStructure2
    };
    List<object> objectList;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(sessionKeeper.Session.IdentHelper.UsersTypeID).Select(paramSet);
      objectList = new List<object>(dataTable.Rows.Count);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Guid guid = new Guid(Convert.ToString(row[0]));
        long int64 = Convert.ToInt64(row[1]);
        string displayName = Convert.ToString(row[2]);
        Guid userGuid = guid;
        TargetDescriptor targetDescriptor = new TargetDescriptor((ITarget) new UserTarget(int64, userGuid), displayName);
        objectList.Add((object) targetDescriptor);
      }
    }
    SelectItemForm currentControl = new SelectItemForm();
    currentControl.Text = LocalizationHolder.rm.GetString("Tools.Client_176");
    currentControl.Description = LocalizationHolder.rm.GetString("Tools.Client_217");
    currentControl.Items = (IEnumerable) objectList;
    HelpProvidersClass.SetHelpOptionForControl((Control) currentControl, 1633);
    if (currentControl.ShowDialog() != DialogResult.OK)
      return;
    this.ChangeActiveTarget((TargetDescriptor) currentControl.SelectedItem);
  }

  private void ChangeActiveTarget(TargetDescriptor newDescriptor)
  {
    if (newDescriptor.Equals((object) this.securityContext.ActiveTarget))
      return;
    this.securityContext.ActiveTarget = newDescriptor;
    this.DisplayActiveTarget();
    this.RaiseTargetChanged();
  }

  private void RaiseTargetChanged()
  {
    if (this.TargetChanged == null)
      return;
    this.TargetChanged((object) this, EventArgs.Empty);
  }

  public event EventHandler TargetChanged;
}
