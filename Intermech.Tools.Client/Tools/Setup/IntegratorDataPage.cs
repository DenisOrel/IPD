// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.IntegratorDataPage
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.Integrators;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Setup;

internal sealed class IntegratorDataPage : UserControl
{
  private readonly IIntegratorRegistry integrators;
  private IntegratorObject selectedIntegrator;
  private DataEditorControl dataEditor;
  private bool dataChanged;
  private IContainer components;
  private TabControl tcPages;
  private TabPage tpData;
  private Button btRevert;
  private Button btClose;
  private Button btApply;

  public IntegratorDataPage()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.integrators = ClientContext.Integrators;
  }

  public void InitializePage(IntegratorObject integratorObject, bool readOnly)
  {
    if (integratorObject == null)
      throw new ArgumentNullException();
    if (this.PageInitialized)
      this.CloseDataEditor();
    this.OpenDataEditor(integratorObject, readOnly);
  }

  public void ClosePage()
  {
    if (!this.PageInitialized)
      return;
    if (this.PageDirty)
      this.SuggestApplyChanges();
    this.CloseDataEditor();
  }

  public IntegratorObject SelectedIntegrator => this.selectedIntegrator;

  public event EventHandler InfoUpdated;

  public event EventHandler PageClose;

  private void OpenDataEditor(IntegratorObject integratorObject, bool readOnly)
  {
    XmlDocument data = new XmlDocument();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      data.LoadXml(service.GetIntegratorData(integratorObject.Id));
    }
    this.dataEditor = this.integrators.GetIntegrator(integratorObject, false)?.CreateSettingsEditor();
    if (this.dataEditor == null)
      this.dataEditor = (DataEditorControl) new XmlTextEditor();
    this.dataEditor.Parent = (Control) this.tpData;
    this.dataEditor.Dock = DockStyle.Fill;
    this.selectedIntegrator = integratorObject;
    this.dataChanged = false;
    this.ToggleApplyRevertButtons(false);
    try
    {
      this.dataEditor.SetData(data, readOnly);
      if (!readOnly)
        this.dataEditor.DataChanged += new EventHandler(this.OnDataChanged);
      this.ActiveControl = (Control) this.tcPages;
    }
    catch
    {
      this.CloseDataEditor();
      throw;
    }
  }

  private void CloseDataEditor()
  {
    this.dataEditor.Dispose();
    this.dataEditor = (DataEditorControl) null;
    this.selectedIntegrator = (IntegratorObject) null;
    this.dataChanged = false;
  }

  private bool PageInitialized => this.dataEditor != null;

  private bool PageDirty => this.dataChanged;

  private void OnDataChanged(object sender, EventArgs e)
  {
    this.dataChanged = true;
    this.ToggleApplyRevertButtons(true);
  }

  private void ToggleApplyRevertButtons(bool enabled)
  {
    this.btApply.Enabled = enabled;
    this.btRevert.Enabled = enabled;
  }

  private void SuggestApplyChanges()
  {
    try
    {
      if (MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_157"), LocalizationHolder.rm.GetString("Tools.Client_158"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      this.ApplyChanges();
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }

  private void ApplyChanges()
  {
    XmlDocument data = this.dataEditor.GetData();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      service.SetIntegratorData(this.selectedIntegrator.Id, data.OuterXml);
      this.selectedIntegrator = service.GetIntegrator(this.selectedIntegrator.Id);
    }
    this.dataEditor.SetData(data, this.dataEditor.ReadOnly);
    this.dataChanged = false;
    this.ToggleApplyRevertButtons(false);
    if (this.InfoUpdated == null)
      return;
    this.InfoUpdated((object) this, EventArgs.Empty);
  }

  private void RevertChanges()
  {
    this.dataEditor.DataChanged -= new EventHandler(this.OnDataChanged);
    this.dataChanged = false;
    this.ToggleApplyRevertButtons(false);
    this.dataEditor.SetData(this.dataEditor.OriginalData, this.dataEditor.ReadOnly);
    if (this.dataEditor.ReadOnly)
      return;
    this.dataEditor.DataChanged += new EventHandler(this.OnDataChanged);
  }

  private void btClose_Click(object sender, EventArgs e)
  {
    this.ClosePage();
    if (this.PageClose == null)
      return;
    this.PageClose((object) this, EventArgs.Empty);
  }

  private void btApply_Click(object sender, EventArgs e) => this.ApplyChanges();

  private void btRevert_Click(object sender, EventArgs e) => this.RevertChanges();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IntegratorDataPage));
    this.tcPages = new TabControl();
    this.tpData = new TabPage();
    this.btRevert = new Button();
    this.btClose = new Button();
    this.btApply = new Button();
    this.tcPages.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tcPages, "tcPages");
    this.tcPages.Controls.Add((Control) this.tpData);
    this.tcPages.Name = "tcPages";
    this.tcPages.SelectedIndex = 0;
    componentResourceManager.ApplyResources((object) this.tpData, "tpData");
    this.tpData.Name = "tpData";
    this.tpData.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btRevert, "btRevert");
    this.btRevert.Name = "btRevert";
    this.btRevert.UseVisualStyleBackColor = true;
    this.btRevert.Click += new EventHandler(this.btRevert_Click);
    componentResourceManager.ApplyResources((object) this.btClose, "btClose");
    this.btClose.Name = "btClose";
    this.btClose.UseVisualStyleBackColor = true;
    this.btClose.Click += new EventHandler(this.btClose_Click);
    componentResourceManager.ApplyResources((object) this.btApply, "btApply");
    this.btApply.Name = "btApply";
    this.btApply.UseVisualStyleBackColor = true;
    this.btApply.Click += new EventHandler(this.btApply_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.btApply);
    this.Controls.Add((Control) this.btClose);
    this.Controls.Add((Control) this.btRevert);
    this.Controls.Add((Control) this.tcPages);
    this.Name = nameof (IntegratorDataPage);
    this.tcPages.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
