// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Requirement.RequirementsSettingsPage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces.Client;
using Intermech.Mvp.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Client.Requirement;

public class RequirementsSettingsPage : 
  MvpUserControl,
  IPropertyPage,
  IPropertyPageSearchOptionEvents
{
  private RequirementsSettings _requirementsSettings;
  private CheckBox enableRequirementsCheckBox;
  private CheckBox enableRequirementForCurrentUserCheckBox;
  private IContainer components;
  private EventHandler pageChanged;

  public RequirementsSettingsPage()
  {
    this.InitializeComponent();
    this._requirementsSettings = new RequirementsSettings();
    this._requirementsSettings.Load();
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).IsAdmin)
      this.enableRequirementsCheckBox.Visible = false;
    this.enableRequirementsCheckBox.CheckedChanged -= new EventHandler(this.enableRequirementsCheckBox_CheckedChanged);
    this.enableRequirementForCurrentUserCheckBox.CheckedChanged -= new EventHandler(this.enableRequirementForCurrentUserCheckBox_CheckedChanged);
    this.enableRequirementsCheckBox.Checked = this._requirementsSettings.EnableRequirement;
    if (!this._requirementsSettings.EnableRequirement)
    {
      this.enableRequirementForCurrentUserCheckBox.Checked = false;
      this.enableRequirementForCurrentUserCheckBox.Enabled = false;
    }
    else
      this.enableRequirementForCurrentUserCheckBox.Checked = this._requirementsSettings.EnableRequirementForCurrentUser;
    this.enableRequirementsCheckBox.CheckedChanged += new EventHandler(this.enableRequirementsCheckBox_CheckedChanged);
    this.enableRequirementForCurrentUserCheckBox.CheckedChanged += new EventHandler(this.enableRequirementForCurrentUserCheckBox_CheckedChanged);
  }

  private void RaisePageChanged()
  {
    if (this.pageChanged == null)
      return;
    this.pageChanged((object) this, EventArgs.Empty);
  }

  public event EventHandler Changed
  {
    add => this.pageChanged += value;
    remove => this.pageChanged -= value;
  }

  public PropertyPageType Type => PropertyPageType.Control;

  public object Control => (object) this;

  public string PageName => "Requirements";

  public void Apply() => this._requirementsSettings.Save();

  public void Cancel()
  {
    this._requirementsSettings.Load();
    this.enableRequirementsCheckBox.CheckedChanged -= new EventHandler(this.enableRequirementsCheckBox_CheckedChanged);
    this.enableRequirementForCurrentUserCheckBox.CheckedChanged -= new EventHandler(this.enableRequirementForCurrentUserCheckBox_CheckedChanged);
    this.enableRequirementsCheckBox.Checked = this._requirementsSettings.EnableRequirement;
    if (!this._requirementsSettings.EnableRequirement)
    {
      this.enableRequirementForCurrentUserCheckBox.Checked = false;
      this.enableRequirementForCurrentUserCheckBox.Enabled = false;
    }
    else
      this.enableRequirementForCurrentUserCheckBox.Checked = this._requirementsSettings.EnableRequirementForCurrentUser;
    this.enableRequirementsCheckBox.CheckedChanged += new EventHandler(this.enableRequirementsCheckBox_CheckedChanged);
    this.enableRequirementForCurrentUserCheckBox.CheckedChanged += new EventHandler(this.enableRequirementForCurrentUserCheckBox_CheckedChanged);
  }

  public string HelpTopicID => string.Empty;

  public string HeaderText => string.Empty;

  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  private void InitializeComponent()
  {
    this.enableRequirementsCheckBox = new CheckBox();
    this.enableRequirementForCurrentUserCheckBox = new CheckBox();
    this.SuspendLayout();
    this.enableRequirementsCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.enableRequirementsCheckBox.AutoSize = true;
    this.enableRequirementsCheckBox.Location = new Point(3, 26);
    this.enableRequirementsCheckBox.Name = "enableRequirementsCheckBox";
    this.enableRequirementsCheckBox.Size = new Size(307, 17);
    this.enableRequirementsCheckBox.TabIndex = 1;
    this.enableRequirementsCheckBox.Text = "Включить режим создания ТТ для всех пользователей";
    this.enableRequirementsCheckBox.UseVisualStyleBackColor = true;
    this.enableRequirementsCheckBox.CheckedChanged += new EventHandler(this.enableRequirementsCheckBox_CheckedChanged);
    this.enableRequirementForCurrentUserCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.enableRequirementForCurrentUserCheckBox.AutoSize = true;
    this.enableRequirementForCurrentUserCheckBox.Location = new Point(3, 3);
    this.enableRequirementForCurrentUserCheckBox.Name = "enableRequirementForCurrentUserCheckBox";
    this.enableRequirementForCurrentUserCheckBox.Size = new Size(180, 17);
    this.enableRequirementForCurrentUserCheckBox.TabIndex = 1;
    this.enableRequirementForCurrentUserCheckBox.Text = "Включить режим создания ТТ";
    this.enableRequirementForCurrentUserCheckBox.UseVisualStyleBackColor = true;
    this.enableRequirementForCurrentUserCheckBox.CheckedChanged += new EventHandler(this.enableRequirementForCurrentUserCheckBox_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.Controls.Add((System.Windows.Forms.Control) this.enableRequirementForCurrentUserCheckBox);
    this.Controls.Add((System.Windows.Forms.Control) this.enableRequirementsCheckBox);
    this.Name = nameof (RequirementsSettingsPage);
    this.Size = new Size(312, 53);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void enableRequirementsCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._requirementsSettings.EnableRequirement = this.enableRequirementsCheckBox.Checked;
    this.enableRequirementForCurrentUserCheckBox.Checked = this._requirementsSettings.EnableRequirement;
    this.enableRequirementForCurrentUserCheckBox.Enabled = this._requirementsSettings.EnableRequirement;
    this.RaisePageChanged();
  }

  private void enableRequirementForCurrentUserCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this._requirementsSettings.EnableRequirementForCurrentUser = this.enableRequirementForCurrentUserCheckBox.Checked;
    this.RaisePageChanged();
  }
}
