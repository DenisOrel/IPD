// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.EditReason
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class EditReason : Form
{
  private List<string> reasonIds;
  public string userShifr = "-";
  private IContainer components;
  private Panel panel1;
  private Button button2;
  private Button button1;
  private ComboBox cb;
  private TextBox edUserReason;
  private GroupBox gb;
  private Label label2;
  private Label label1;
  private TextBox edUserShifr;

  public EditReason()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 841);
  }

  public string Execute(ref string reason, Guid attrGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrGuid);
      if (attributeType == null)
        return "-2";
      this.Text = attributeType.Name;
      this.cb.Items.Clear();
      DataRow[] possibleValuesRows = attributeType.GetPossibleValuesRows();
      this.reasonIds = new List<string>(possibleValuesRows.Length);
      foreach (DataRow dataRow in possibleValuesRows)
      {
        string str1 = Convert.ToString(dataRow["F_STRING_VALUE"]);
        string str2 = Convert.ToString(dataRow["F_DESCRIPTION"]);
        this.reasonIds.Add(str1);
        this.cb.Items.Add((object) str2);
      }
      if (reason != "")
      {
        int num = this.cb.Items.IndexOf((object) reason);
        if (num >= 0)
        {
          this.cb.SelectedIndex = num;
        }
        else
        {
          this.cb.SelectedIndex = this.cb.Items.Count - 1;
          this.edUserReason.Text = reason;
          this.edUserShifr.Text = this.userShifr;
        }
      }
      else
        this.cb.SelectedIndex = 1;
    }
    if (this.ShowDialog() != DialogResult.OK || this.cb.SelectedIndex < 0)
      return "-2";
    if (this.IsOtherReason())
    {
      reason = this.edUserReason.Text;
      this.userShifr = this.edUserShifr.Text;
    }
    else
    {
      this.userShifr = "";
      if (this.cb.SelectedItem != null)
        reason = Convert.ToString(this.cb.SelectedItem);
    }
    return this.reasonIds[this.cb.SelectedIndex];
  }

  private void cb_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.IsOtherReason())
    {
      this.gb.Enabled = true;
      if (!ECOPlugin.plugin.eps.Current.ProhibitCustomReason)
        return;
      this.edUserShifr.Enabled = false;
    }
    else
    {
      this.edUserReason.Text = "";
      this.edUserShifr.Text = "";
      this.gb.Enabled = false;
    }
  }

  private bool IsOtherReason()
  {
    if (this.cb.SelectedIndex < 0)
      return false;
    string reasonId = this.reasonIds[this.cb.SelectedIndex];
    return reasonId == "-1" || reasonId == "-";
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EditReason));
    this.panel1 = new Panel();
    this.button2 = new Button();
    this.button1 = new Button();
    this.cb = new ComboBox();
    this.edUserReason = new TextBox();
    this.gb = new GroupBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.edUserShifr = new TextBox();
    this.panel1.SuspendLayout();
    this.gb.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.DialogResult = DialogResult.Cancel;
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.DialogResult = DialogResult.OK;
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cb, "cb");
    this.cb.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cb.FormattingEnabled = true;
    this.cb.Name = "cb";
    this.cb.SelectedIndexChanged += new EventHandler(this.cb_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.edUserReason, "edUserReason");
    this.edUserReason.Name = "edUserReason";
    this.gb.Controls.Add((Control) this.label2);
    this.gb.Controls.Add((Control) this.label1);
    this.gb.Controls.Add((Control) this.edUserShifr);
    this.gb.Controls.Add((Control) this.edUserReason);
    componentResourceManager.ApplyResources((object) this.gb, "gb");
    this.gb.Name = "gb";
    this.gb.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.edUserShifr, "edUserShifr");
    this.edUserShifr.Name = "edUserShifr";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gb);
    this.Controls.Add((Control) this.cb);
    this.Controls.Add((Control) this.panel1);
    this.FormBorderStyle = FormBorderStyle.FixedSingle;
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (EditReason);
    this.ShowInTaskbar = false;
    this.Tag = (object) "";
    this.panel1.ResumeLayout(false);
    this.gb.ResumeLayout(false);
    this.gb.PerformLayout();
    this.ResumeLayout(false);
  }
}
