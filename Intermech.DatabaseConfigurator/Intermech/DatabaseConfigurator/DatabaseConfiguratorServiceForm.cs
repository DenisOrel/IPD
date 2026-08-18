// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.DatabaseConfiguratorServiceForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator;

public class DatabaseConfiguratorServiceForm : Form
{
  private Panel panel;
  private System.ComponentModel.Container components;
  private DatabaseConfiguratorControl configurator;
  private object[] result;

  public DatabaseConfiguratorServiceForm()
  {
    this.InitializeComponent();
    this.configurator = new DatabaseConfiguratorControl();
    this.configurator.Parent = (Control) this.panel;
    this.configurator.Dock = DockStyle.Fill;
    this.configurator.ExternalApply += new ApplyEventHandler(this.configurator_Apply);
    this.configurator.ExternalCancel += new EventHandler(this.configurator_Cancel);
    this.configurator.NeedExpandAll = false;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DatabaseConfiguratorServiceForm));
    this.panel = new Panel();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.panel, "panel");
    this.panel.Name = "panel";
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panel);
    this.DoubleBuffered = true;
    this.Name = nameof (DatabaseConfiguratorServiceForm);
    this.ShowIcon = false;
    this.ShowInTaskbar = false;
    this.Tag = (object) " ";
    this.Load += new EventHandler(this.DatabaseConfiguratorServiceForm_Load);
    this.ResumeLayout(false);
  }

  private void DatabaseConfiguratorServiceForm_Load(object sender, EventArgs e)
  {
  }

  public object[] ExecuteDialog(ConfiguratorAction action, int category, params object[] args)
  {
    this.result = (object[]) null;
    this.configurator.PrepareModalExecute(action, category, args);
    int num = (int) this.ShowDialog();
    return this.result;
  }

  private void configurator_Apply(object sender, ApplyEventArgs e)
  {
    this.result = new object[1]{ e.Data };
    this.DialogResult = DialogResult.OK;
  }

  private void configurator_Cancel(object sender, EventArgs e)
  {
    this.result = new object[1];
    this.DialogResult = DialogResult.Cancel;
  }
}
