// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Cadmech.ImportModelLibraryForm
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.StandardParts.Cadmech;

internal sealed class ImportModelLibraryForm : Form
{
  private readonly IIntegratorRegistry integrators;
  private StandardPartImporter modelImporter;
  private StandardPartRelinker modelRelinked;
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

  public ImportModelLibraryForm()
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

  private void InitServices()
  {
    this.modelImporter = new StandardPartImporter(ClientContext.FileVault);
    this.modelRelinked = new StandardPartRelinker();
  }

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
      if (this.cbCadSystem.SelectedIndex == -1)
        throw new FaultException(LocalizationHolder.rm.GetString("Tools.Client_185"));
      if (string.IsNullOrEmpty(this.tbLibraryDir.Text))
        throw new FaultException(LocalizationHolder.rm.GetString("Tools.Client_186"));
      ImportContext importContext = this.CreateImportContext((IntegratorObject) this.cbCadSystem.SelectedItem);
      ServiceUtils.GetService<IStandardPartLibraryService>((object) importContext.Integrator, false)?.PrepareToImportCadmechLibrary(this.tbLibraryDir.Text);
      string[] files = Directory.GetFiles(this.tbLibraryDir.Text, "*", SearchOption.TopDirectoryOnly);
      if (files.Length != 0)
        this.ImportCore(importContext, files);
      int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Tools.Client_187"), LocalizationHolder.rm.GetString("Tools.Client_188"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
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
    try
    {
      this.modelImporter.ImportContext = ctx;
      this.modelRelinked.ImportContext = ctx;
      double progress = 0.0;
      double num = 100.0 / (double) models.Length;
      for (int index = 0; index < models.Length; ++index)
      {
        string model = models[index];
        this.ShowModelFile(model);
        if (this.modelImporter.CanOpenModel(model))
        {
          try
          {
            ImportedStandardPart importedStandardPart = this.modelImporter.ImportModel(model);
            if (importedStandardPart != null)
            {
              foreach (long articleId in (IEnumerable<long>) importedStandardPart.ArticleIds)
                this.modelRelinked.RelinkPart(articleId, importedStandardPart.ModelId);
            }
            ctx.NotifyQueue.FlushQueue();
          }
          catch (Exception ex)
          {
            ExceptionHelper.ExceptionService.ShowException(ex);
          }
        }
        progress += num;
        this.ShowProgress(progress);
      }
    }
    finally
    {
      this.modelImporter.ImportContext = (ImportContext) null;
      this.modelRelinked.ImportContext = (ImportContext) null;
    }
  }

  private ImportContext CreateImportContext(IntegratorObject integratorObject)
  {
    ImportContext importContext = new ImportContext();
    importContext.VersionsRule = VersionsRuleSources.GetEditorRule();
    importContext.Integrator = this.integrators.GetIntegrator(integratorObject, true);
    lock (importContext.Integrator)
    {
      importContext.StandardModelType = StandardLibraryServices.GetModelType((System.IServiceProvider) importContext.Integrator);
      CADSettings cadSettings = ServiceUtils.GetService<ICADSettingsService>((object) importContext.Integrator, true).GetCADSettings();
      importContext.AssemblyModelTypes.AddRange((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("Assembly", true).DocumentTypes);
      importContext.PartModelTypes.AddRange((IEnumerable<LocalId<int>>) cadSettings.FileDocumentGroups.FindByName("Part", true).DocumentTypes);
    }
    using (CADApiSession cadApiSession = new CADApiSession(importContext.Integrator))
    {
      CADSystemProxy application = cadApiSession.Application;
      importContext.PartModelExtensions = application.GetFileExtensions(CADDocumentType.Part);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ImportModelLibraryForm));
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
    this.AcceptButton = (IButtonControl) this.btImport;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btClose;
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
    this.Name = nameof (ImportModelLibraryForm);
    this.Load += new EventHandler(this.ImportModelLibraryForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
