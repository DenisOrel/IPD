// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.ExtApps.SystemVariablesForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.ExtApps;

internal sealed class SystemVariablesForm : Form
{
  private IContainer components;
  private Panel panel2;
  private Button bCancel;
  private Button bOk;
  private Panel panel1;
  private ListView lvVariables;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;

  public string ChoiseVariable
  {
    get
    {
      return this.lvVariables.SelectedItems.Count > 0 ? this.lvVariables.SelectedItems[0].Tag.ToString() : string.Empty;
    }
  }

  public SystemVariablesForm()
  {
    this.InitializeComponent();
    int num = this.lvVariables.ClientRectangle.Width - 17;
    this.columnHeader1.Width = Convert.ToInt32((double) num * 0.4);
    this.columnHeader2.Width = Convert.ToInt32((double) num * 0.6);
  }

  public void Initialize(IDictionary variables)
  {
    IDictionaryEnumerator enumerator = variables.GetEnumerator();
    while (enumerator.MoveNext())
      this.lvVariables.Items.Add(new ListViewItem()
      {
        Text = Convert.ToString(enumerator.Key),
        SubItems = {
          enumerator.Value.ToString()
        },
        Tag = enumerator.Key
      });
  }

  private void lvVariables_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.bOk.Enabled = this.lvVariables.SelectedItems.Count > 0;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SystemVariablesForm));
    this.panel2 = new Panel();
    this.bCancel = new Button();
    this.bOk = new Button();
    this.panel1 = new Panel();
    this.lvVariables = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.panel2.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel2.Controls.Add((Control) this.bCancel);
    this.panel2.Controls.Add((Control) this.bOk);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.bCancel, "bCancel");
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Name = "bCancel";
    this.bCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.bOk, "bOk");
    this.bOk.DialogResult = DialogResult.OK;
    this.bOk.Name = "bOk";
    this.bOk.UseVisualStyleBackColor = true;
    this.panel1.Controls.Add((Control) this.lvVariables);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.lvVariables.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    componentResourceManager.ApplyResources((object) this.lvVariables, "lvVariables");
    this.lvVariables.FullRowSelect = true;
    this.lvVariables.GridLines = true;
    this.lvVariables.MultiSelect = false;
    this.lvVariables.Name = "lvVariables";
    this.lvVariables.UseCompatibleStateImageBehavior = false;
    this.lvVariables.View = View.Details;
    this.lvVariables.SelectedIndexChanged += new EventHandler(this.lvVariables_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panel2);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.Name = nameof (SystemVariablesForm);
    this.ShowInTaskbar = false;
    this.panel2.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
