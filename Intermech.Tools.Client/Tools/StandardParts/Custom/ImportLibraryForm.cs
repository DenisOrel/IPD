// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Custom.ImportLibraryForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.StandardParts.Custom;

internal sealed class ImportLibraryForm : Form
{
  private readonly IIntegratorRegistry integrators;
  private StandardPartImporter importer;
  private IContainer components;
  private Label lbCadSystem;
  private ComboBox cbCadSystem;
  private Label lbLibraryDir;
  private TextBox tbLibraryDir;
  private Button btSelectLibraryDir;
  private Label lbProgress;
  private ProgressBar pbProgress;
  private Button btClose;
  private FolderBrowserDialog fbdSelectLibraryDir;
  private Button btImport;
  private GroupBox gbOptions;
  private CheckBox cbFillEmptyNamesOnly;
  private CheckBox cbFillNames;
  private CheckBox cbClearDesignations;
  private CheckBox cbLinkToImbase;
  private CheckBox cbCorrectPartTypes;

  public ImportLibraryForm()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.integrators = ClientContext.Integrators;
  }

  private void ImportModelLibraryForm_Load(object sender, EventArgs e)
  {
    if (this.DesignMode)
      return;
    this.InitServices();
    this.PopulateCadSystemSelector();
  }

  private void InitServices() => this.importer = new StandardPartImporter();

  private void PopulateCadSystemSelector()
  {
    this.cbCadSystem.BeginUpdate();
    try
    {
      List<IIntegrator> integrators = this.integrators.GetIntegrators();
      List<IIntegrator> integratorList = new List<IIntegrator>(integrators.Count);
      foreach (IIntegrator serviceProvider in integrators)
      {
        ICADInterfaceService service = ServiceUtils.GetService<ICADInterfaceService>((object) serviceProvider, false);
        if (service != null && service.IsApplicationInstalled)
          integratorList.Add(serviceProvider);
      }
      if (integratorList.Count <= 0)
        return;
      foreach (IIntegrator serviceProvider in integratorList)
      {
        ICADInterfaceService service = ServiceUtils.GetService<ICADInterfaceService>((object) serviceProvider, true);
        this.cbCadSystem.Items.Add((object) new IntegratorObject(serviceProvider.Id, service.ApplicationName));
      }
      this.cbCadSystem.SelectedIndex = 0;
    }
    finally
    {
      this.cbCadSystem.EndUpdate();
    }
  }

  private void cbFillNames_CheckedChanged(object sender, EventArgs e)
  {
    this.cbFillEmptyNamesOnly.Enabled = this.cbFillNames.Checked;
  }

  private void btSelectLibraryDir_Click(object sender, EventArgs e)
  {
    if (this.fbdSelectLibraryDir.ShowDialog() != DialogResult.OK)
      return;
    string selectedPath = this.fbdSelectLibraryDir.SelectedPath;
    if (!Directory.Exists(selectedPath))
      return;
    this.tbLibraryDir.Text = selectedPath;
  }

  private void btImport_Click(object sender, EventArgs e)
  {
    this.btImport.Enabled = false;
    this.btClose.Enabled = false;
    try
    {
      this.ValidateImportSettings();
      ImportContext importContext = this.CreateImportContext();
      string[] files = Directory.GetFiles(this.tbLibraryDir.Text, "*", SearchOption.AllDirectories);
      if (files.Length != 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(ImportConsts.NeedBackupWarning);
        stringBuilder.AppendLine(string.Empty);
        stringBuilder.AppendLine(ImportConsts.ProceedQuesting);
        if (MessageBox.Show(stringBuilder.ToString(), ImportConsts.WizardCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
          return;
        this.ImportCore(importContext, files);
      }
      this.PrintProtocol(importContext);
      if (importContext.Protocol.Count > 0)
      {
        int num = (int) MessageBox.Show(ImportConsts.CompleteWithErrors, ImportConsts.WizardCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, false)?.ShowView();
      }
      else
      {
        int num1 = (int) MessageBox.Show(ImportConsts.CompleteWithNoErrors, ImportConsts.WizardCaption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
      }
    }
    finally
    {
      this.ShowModelFile((string) null);
      this.ShowProgress(0.0);
      this.btImport.Enabled = true;
      this.btClose.Enabled = true;
    }
  }

  private void ImportCore(ImportContext ctx, string[] models)
  {
    this.CheckCadEmpty(ctx);
    double progress = 0.0;
    double num = 100.0 / (double) models.Length;
    for (int index = 0; index < models.Length; ++index)
    {
      string model = models[index];
      this.ShowModelFile(PathUtils.GetRelativePath(model, ctx.RootPath, RelativePathOptions.None));
      if (this.importer.CanOpenModel(model, ctx))
      {
        try
        {
          this.importer.ImportModel(model, ctx);
          ctx.NotifyQueue.FlushQueue();
        }
        catch (Exception ex)
        {
          ctx.Protocol.Add(string.Format(LocalizationHolder.rm.GetString("Tools.Client_192"), (object) model));
          ctx.Protocol.Add(ex.Message);
          ctx.Protocol.Add(string.Empty);
        }
      }
      progress += num;
      this.ShowProgress(progress);
    }
  }

  private void PrintProtocol(ImportContext ctx)
  {
    if (ctx.Protocol.Count <= 0)
      return;
    IIntegratorOutput service = ServiceUtils.GetService<IIntegratorOutput>((object) ctx.Integrator, true);
    service.WriteLine(LocalizationHolder.rm.GetString("Tools.Client_193"));
    service.WriteLine(new string('-', 64 /*0x40*/));
    foreach (string text in ctx.Protocol)
      service.WriteLine(text);
    service.WriteLine(LocalizationHolder.rm.GetString("Tools.Client_194"));
    service.WriteLine(string.Empty);
  }

  private void ValidateImportSettings()
  {
    if (this.cbCadSystem.SelectedIndex == -1)
      throw new FaultException(LocalizationHolder.rm.GetString("Tools.Client_185"));
    if (string.IsNullOrEmpty(this.tbLibraryDir.Text))
      throw new FaultException(LocalizationHolder.rm.GetString("Tools.Client_186"));
  }

  private ImportContext CreateImportContext()
  {
    ImportContext importContext = new ImportContext();
    importContext.VersionsRule = VersionsRuleSources.GetEditorRule();
    importContext.RootPath = this.tbLibraryDir.Text;
    importContext.Integrator = this.integrators.GetIntegrator((IntegratorObject) this.cbCadSystem.SelectedItem, true);
    lock (importContext.Integrator)
    {
      ServiceUtils.GetService<ICADSettingsService>((object) importContext.Integrator, true);
      importContext.StandardModelType = StandardLibraryServices.GetModelType((System.IServiceProvider) importContext.Integrator);
    }
    using (CADApiSession cadApiSession = new CADApiSession(importContext.Integrator))
    {
      CADSystemProxy application = cadApiSession.Application;
      importContext.PartModelExtensions = application.GetFileExtensions(CADDocumentType.Part);
    }
    importContext.ClearDesignation = this.cbClearDesignations.Checked;
    importContext.FillNames = this.cbFillNames.Checked;
    importContext.FillEmptyNamesOnly = this.cbFillEmptyNamesOnly.Enabled && this.cbFillEmptyNamesOnly.Checked;
    importContext.LinkToImbase = this.cbLinkToImbase.Checked;
    importContext.CorrectPartTypes = this.cbCorrectPartTypes.Checked;
    return importContext;
  }

  private void ShowModelFile(string modelFullPath)
  {
    this.lbProgress.Text = string.IsNullOrEmpty(modelFullPath) ? LocalizationHolder.rm.GetString("Tools.Client_189") : modelFullPath;
    Application.DoEvents();
  }

  private void ShowProgress(double progress)
  {
    this.pbProgress.Value = (int) Math.Round(progress);
    Application.DoEvents();
  }

  private void CheckCadEmpty(ImportContext ctx)
  {
    ICADInterfaceService service = ServiceUtils.GetService<ICADInterfaceService>((object) ctx.Integrator, true);
    if (!service.IsApplicationRunning)
      return;
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) service))
    {
      if (cadApiSession.Application.HasOpenFiles())
        throw new FaultException(LocalizationHolder.rm.GetString("Tools.Client_190"));
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImportLibraryForm));
    this.lbCadSystem = new Label();
    this.cbCadSystem = new ComboBox();
    this.lbLibraryDir = new Label();
    this.tbLibraryDir = new TextBox();
    this.btSelectLibraryDir = new Button();
    this.lbProgress = new Label();
    this.pbProgress = new ProgressBar();
    this.btClose = new Button();
    this.fbdSelectLibraryDir = new FolderBrowserDialog();
    this.btImport = new Button();
    this.gbOptions = new GroupBox();
    this.cbCorrectPartTypes = new CheckBox();
    this.cbLinkToImbase = new CheckBox();
    this.cbFillEmptyNamesOnly = new CheckBox();
    this.cbFillNames = new CheckBox();
    this.cbClearDesignations = new CheckBox();
    this.gbOptions.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbCadSystem, "lbCadSystem");
    this.lbCadSystem.Name = "lbCadSystem";
    componentResourceManager.ApplyResources((object) this.cbCadSystem, "cbCadSystem");
    this.cbCadSystem.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCadSystem.FormattingEnabled = true;
    this.cbCadSystem.Name = "cbCadSystem";
    componentResourceManager.ApplyResources((object) this.lbLibraryDir, "lbLibraryDir");
    this.lbLibraryDir.Name = "lbLibraryDir";
    componentResourceManager.ApplyResources((object) this.tbLibraryDir, "tbLibraryDir");
    this.tbLibraryDir.Name = "tbLibraryDir";
    componentResourceManager.ApplyResources((object) this.btSelectLibraryDir, "btSelectLibraryDir");
    this.btSelectLibraryDir.Name = "btSelectLibraryDir";
    this.btSelectLibraryDir.UseVisualStyleBackColor = true;
    this.btSelectLibraryDir.Click += new EventHandler(this.btSelectLibraryDir_Click);
    componentResourceManager.ApplyResources((object) this.lbProgress, "lbProgress");
    this.lbProgress.Name = "lbProgress";
    componentResourceManager.ApplyResources((object) this.pbProgress, "pbProgress");
    this.pbProgress.Name = "pbProgress";
    componentResourceManager.ApplyResources((object) this.btClose, "btClose");
    this.btClose.DialogResult = DialogResult.Cancel;
    this.btClose.Name = "btClose";
    this.btClose.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.fbdSelectLibraryDir, "fbdSelectLibraryDir");
    this.fbdSelectLibraryDir.ShowNewFolderButton = false;
    componentResourceManager.ApplyResources((object) this.btImport, "btImport");
    this.btImport.Name = "btImport";
    this.btImport.UseVisualStyleBackColor = true;
    this.btImport.Click += new EventHandler(this.btImport_Click);
    componentResourceManager.ApplyResources((object) this.gbOptions, "gbOptions");
    this.gbOptions.Controls.Add((Control) this.cbCorrectPartTypes);
    this.gbOptions.Controls.Add((Control) this.cbLinkToImbase);
    this.gbOptions.Controls.Add((Control) this.cbFillEmptyNamesOnly);
    this.gbOptions.Controls.Add((Control) this.cbFillNames);
    this.gbOptions.Controls.Add((Control) this.cbClearDesignations);
    this.gbOptions.Name = "gbOptions";
    this.gbOptions.TabStop = false;
    componentResourceManager.ApplyResources((object) this.cbCorrectPartTypes, "cbCorrectPartTypes");
    this.cbCorrectPartTypes.Checked = true;
    this.cbCorrectPartTypes.CheckState = CheckState.Checked;
    this.cbCorrectPartTypes.Name = "cbCorrectPartTypes";
    this.cbCorrectPartTypes.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbLinkToImbase, "cbLinkToImbase");
    this.cbLinkToImbase.Name = "cbLinkToImbase";
    this.cbLinkToImbase.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbFillEmptyNamesOnly, "cbFillEmptyNamesOnly");
    this.cbFillEmptyNamesOnly.Checked = true;
    this.cbFillEmptyNamesOnly.CheckState = CheckState.Checked;
    this.cbFillEmptyNamesOnly.Name = "cbFillEmptyNamesOnly";
    this.cbFillEmptyNamesOnly.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.cbFillNames, "cbFillNames");
    this.cbFillNames.Checked = true;
    this.cbFillNames.CheckState = CheckState.Checked;
    this.cbFillNames.Name = "cbFillNames";
    this.cbFillNames.UseVisualStyleBackColor = true;
    this.cbFillNames.CheckedChanged += new EventHandler(this.cbFillNames_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.cbClearDesignations, "cbClearDesignations");
    this.cbClearDesignations.Checked = true;
    this.cbClearDesignations.CheckState = CheckState.Checked;
    this.cbClearDesignations.Name = "cbClearDesignations";
    this.cbClearDesignations.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.btImport;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btClose;
    this.Controls.Add((Control) this.gbOptions);
    this.Controls.Add((Control) this.btImport);
    this.Controls.Add((Control) this.btClose);
    this.Controls.Add((Control) this.pbProgress);
    this.Controls.Add((Control) this.lbProgress);
    this.Controls.Add((Control) this.btSelectLibraryDir);
    this.Controls.Add((Control) this.tbLibraryDir);
    this.Controls.Add((Control) this.lbLibraryDir);
    this.Controls.Add((Control) this.cbCadSystem);
    this.Controls.Add((Control) this.lbCadSystem);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ImportLibraryForm);
    this.Load += new EventHandler(this.ImportModelLibraryForm_Load);
    this.gbOptions.ResumeLayout(false);
    this.gbOptions.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
