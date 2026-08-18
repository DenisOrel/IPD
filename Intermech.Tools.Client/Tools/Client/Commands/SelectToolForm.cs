// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Commands.SelectToolForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Tools.LaunchActions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Commands;

internal class SelectToolForm : Form
{
  private Dictionary<LaunchType, IList> toolInfos;
  private IContainer components;
  private ListBox lbTools;
  private Button btCancel;
  private Button btOK;
  private Label lbAction;
  private ComboBox cbAction;
  public CheckBox MakeDefault;
  public CheckBox NeedCheckOut;

  public SelectToolForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 707);
    this.toolInfos = new Dictionary<LaunchType, IList>();
  }

  public void RegisterTools(LaunchType action, IList toolInfos)
  {
    this.toolInfos[action] = toolInfos;
    this.cbAction.Items.Add((object) new SelectToolForm.ComboBoxItem(action, EnumTypeHelper.GetCaption((Enum) action)));
    this.cbAction.Enabled = this.cbAction.Items.Count > 1;
    if (this.cbAction.SelectedIndex >= 0)
      return;
    this.cbAction.SelectedIndex = 0;
  }

  [Browsable(false)]
  public LaunchType SelectedLaunchType
  {
    get => ((SelectToolForm.ComboBoxItem) this.cbAction.SelectedItem).Action;
  }

  [Browsable(false)]
  public object SelectedTool => this.lbTools.SelectedItem;

  private void cbAction_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.lbTools.BeginUpdate();
    try
    {
      this.lbTools.Items.Clear();
      IList toolInfo = this.toolInfos[((SelectToolForm.ComboBoxItem) this.cbAction.SelectedItem).Action];
      for (int index = 0; index < toolInfo.Count; ++index)
        this.lbTools.Items.Add(toolInfo[index]);
      if (this.lbTools.SelectedIndex >= 0)
        return;
      this.lbTools.SelectedIndex = 0;
    }
    finally
    {
      this.lbTools.EndUpdate();
    }
  }

  private void lbTools_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btOK.Enabled = this.lbTools.SelectedItem != null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectToolForm));
    this.lbTools = new ListBox();
    this.btCancel = new Button();
    this.btOK = new Button();
    this.lbAction = new Label();
    this.cbAction = new ComboBox();
    this.MakeDefault = new CheckBox();
    this.NeedCheckOut = new CheckBox();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbTools, "lbTools");
    this.lbTools.FormattingEnabled = true;
    this.lbTools.Name = "lbTools";
    this.lbTools.SelectedIndexChanged += new EventHandler(this.lbTools_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    this.btOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lbAction, "lbAction");
    this.lbAction.Name = "lbAction";
    componentResourceManager.ApplyResources((object) this.cbAction, "cbAction");
    this.cbAction.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbAction.FormattingEnabled = true;
    this.cbAction.Name = "cbAction";
    this.cbAction.SelectedIndexChanged += new EventHandler(this.cbAction_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.MakeDefault, "MakeDefault");
    this.MakeDefault.Name = "MakeDefault";
    this.MakeDefault.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.NeedCheckOut, "NeedCheckOut");
    this.NeedCheckOut.Name = "NeedCheckOut";
    this.NeedCheckOut.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.NeedCheckOut);
    this.Controls.Add((Control) this.MakeDefault);
    this.Controls.Add((Control) this.cbAction);
    this.Controls.Add((Control) this.lbAction);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.lbTools);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectToolForm);
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private class ComboBoxItem
  {
    private LaunchType action;
    private string name;

    public ComboBoxItem(LaunchType action, string name)
    {
      this.action = action;
      this.name = name;
    }

    public LaunchType Action => this.action;

    public override string ToString() => this.name;
  }
}
