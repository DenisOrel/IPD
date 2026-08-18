// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.EnterpriseArchive.ArchiveParametersPresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Mvp;
using Intermech.Mvp.Components.Dialogs;
using Intermech.UI.PropertyPages;
using System;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.EnterpriseArchive;

internal sealed class ArchiveParametersPresenter : 
  Presenter<IArchiveParametersView>,
  IPropertyPageMvpPresenter,
  IPresenter
{
  private ArchiveParametersEditorModel model;
  private bool? isAdmin;

  public ArchiveParametersPresenter() => this.isAdmin = new bool?();

  public ArchiveParametersEditorModel Model
  {
    [DebuggerStepThrough] get => this.model;
    set
    {
      this.CheckAllowPropertyChange();
      this.model = value;
    }
  }

  private bool IsAdmin
  {
    get
    {
      if (!this.isAdmin.HasValue)
        this.LoadIsAdmin();
      return this.isAdmin.Value;
    }
  }

  protected override void DoValidate()
  {
    base.DoValidate();
    if (this.Model == null)
      throw new PresenterPropertyException("Model");
  }

  protected override void OnAttachView()
  {
    base.OnAttachView();
    this.SetupViewState();
    this.View.SelectLocation += new EventHandler(this.OnSelectLocation);
    this.View.AttachPageChangedHandlers();
    this.View.EditableStateChanged += new EventHandler(this.OnViewEditableStateChanged);
  }

  protected override void OnDetachView()
  {
    this.View.EditableStateChanged -= new EventHandler(this.OnViewEditableStateChanged);
    this.ResetViewState();
    this.View.SelectLocation -= new EventHandler(this.OnSelectLocation);
    this.View.DetachPageChangesHandlers();
    base.OnDetachView();
  }

  private void SetupViewState()
  {
    this.View.ArchiveLocation = this.Model.EditableState.Location.RawValue;
    this.View.EnableArchiveLocation(this.IsAdmin);
    this.View.ImportBatchSize = this.Model.EditableState.ImportBatchSize.RawValue;
    this.View.EnableImportBatchSize(this.IsAdmin);
  }

  private void ResetViewState()
  {
    this.View.ArchiveLocation = string.Empty;
    this.View.ImportBatchSize = 100;
  }

  private void OnSelectLocation(object sender, EventArgs e)
  {
    FolderBrowserPresenter browserPresenter = new FolderBrowserPresenter();
    browserPresenter.Description = LocalizationHolder.rm.GetString("SR_251");
    browserPresenter.AllowNewFolders = false;
    MvpContext.ViewService.ShowModal((IPresenter) browserPresenter);
    if (string.IsNullOrEmpty(browserPresenter.SelectedPath) || !Path.IsPathRooted(browserPresenter.SelectedPath) || !Directory.Exists(browserPresenter.SelectedPath))
      return;
    this.View.ArchiveLocation = browserPresenter.SelectedPath;
  }

  private void OnViewEditableStateChanged(object sender, EventArgs e)
  {
    if (this.SettingsChanged == null)
      return;
    this.SettingsChanged((object) this, EventArgs.Empty);
  }

  private void LoadIsAdmin()
  {
    this.isAdmin = new bool?(ServiceUtils.GetService<ICurrentUserAndRole>((object) ServicesManager.ServiceContainer, true).IsAdmin);
  }

  public void AcceptChanges()
  {
    string str = this.View.ArchiveLocation?.Trim();
    if (!string.IsNullOrEmpty(str))
      this.Model.EditableState.Location.RawValue = str;
    this.Model.EditableState.ImportBatchSize.RawValue = this.View.ImportBatchSize;
  }

  public void RevertChanges()
  {
    this.Model.Reset();
    this.SetupViewState();
  }

  public event EventHandler SettingsChanged;
}
