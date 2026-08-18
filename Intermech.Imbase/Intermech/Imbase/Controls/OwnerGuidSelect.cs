// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.OwnerGuidSelect
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Controls;

public class OwnerGuidSelect : UserControl
{
  private string _userGuid;
  private string _roleGuid;
  private IContainer components;
  private GroupBox groupBox1;
  private RadioButton rbArea;
  private RadioButton rbUser;
  private RadioButton rbCommon;
  private RadioButton rbRole;

  public event EventHandler OwnerChanged;

  public OwnerGuidSelect() => this.InitializeComponent();

  public string Caption
  {
    get => this.groupBox1.Text;
    set => this.groupBox1.Text = value;
  }

  public string OwnerGuid
  {
    get
    {
      if (this.rbCommon.Checked)
        return (string) null;
      if (this.DesignMode)
        return "unknown";
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        if (this.rbArea.Checked)
        {
          if (session.AreaID.Length <= 0)
            return (string) null;
          DataRow[] dataRowArray = session.GetSubjectAreaCollection().Select(string.Empty).Select($"F_AREA_ID='{session.AreaID[0]}'");
          if (dataRowArray.Length != 0)
            return dataRowArray[0]["F_GUID"].ToString();
        }
        else
        {
          if (this.rbRole.Checked)
          {
            if (this._roleGuid == null)
              this._roleGuid = session.GetObject(session.RoleID).ObjectGUID.ToString();
            return this._roleGuid;
          }
          if (this.rbUser.Checked)
          {
            if (this._userGuid == null)
              this._userGuid = session.GetObject(session.UserID).ObjectGUID.ToString();
            return this._userGuid;
          }
        }
      }
      return (string) null;
    }
  }

  private void OnOwnerChanged()
  {
    EventHandler ownerChanged = this.OwnerChanged;
    if (ownerChanged == null)
      return;
    ownerChanged((object) this, EventArgs.Empty);
  }

  private void RB_CheckedChanged(object sender, EventArgs e) => this.OnOwnerChanged();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OwnerGuidSelect));
    this.groupBox1 = new GroupBox();
    this.rbRole = new RadioButton();
    this.rbArea = new RadioButton();
    this.rbUser = new RadioButton();
    this.rbCommon = new RadioButton();
    this.groupBox1.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Controls.Add((Control) this.rbRole);
    this.groupBox1.Controls.Add((Control) this.rbArea);
    this.groupBox1.Controls.Add((Control) this.rbUser);
    this.groupBox1.Controls.Add((Control) this.rbCommon);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.rbRole, "rbRole");
    this.rbRole.Name = "rbRole";
    this.rbRole.Tag = (object) "3";
    this.rbRole.UseVisualStyleBackColor = true;
    this.rbRole.CheckedChanged += new EventHandler(this.RB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbArea, "rbArea");
    this.rbArea.Name = "rbArea";
    this.rbArea.Tag = (object) "2";
    this.rbArea.UseVisualStyleBackColor = true;
    this.rbArea.CheckedChanged += new EventHandler(this.RB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbUser, "rbUser");
    this.rbUser.Name = "rbUser";
    this.rbUser.Tag = (object) "1";
    this.rbUser.UseVisualStyleBackColor = true;
    this.rbUser.CheckedChanged += new EventHandler(this.RB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.rbCommon, "rbCommon");
    this.rbCommon.Checked = true;
    this.rbCommon.Name = "rbCommon";
    this.rbCommon.TabStop = true;
    this.rbCommon.Tag = (object) "0";
    this.rbCommon.UseVisualStyleBackColor = true;
    this.rbCommon.CheckedChanged += new EventHandler(this.RB_CheckedChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.groupBox1);
    this.Name = nameof (OwnerGuidSelect);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.ResumeLayout(false);
  }
}
