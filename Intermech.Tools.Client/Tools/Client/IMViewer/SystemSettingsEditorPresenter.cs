// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.IMViewer.SystemSettingsEditorPresenter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Mvp;
using Intermech.UI.PropertyPages;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.IMViewer;

internal sealed class SystemSettingsEditorPresenter : 
  Presenter<ISystemSettingsEditorView>,
  IPropertyPageMvpPresenter,
  IPresenter
{
  private SystemSettingsEditorModel model;
  private EventHandler settingsChangedHandler;

  public SystemSettingsEditorModel Model
  {
    [DebuggerStepThrough] get => this.model;
    set
    {
      this.CheckAllowPropertyChange();
      this.model = value;
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
    this.View.EditableStateChanged += new EventHandler(this.OnViewEditableStateChanged);
  }

  protected override void OnDetachView()
  {
    this.View.EditableStateChanged -= new EventHandler(this.OnViewEditableStateChanged);
    this.ResetViewState();
    base.OnDetachView();
  }

  private void SetupViewState()
  {
    this.View.AllowEditSettings = this.Model.CurrentUserService.IsAdmin;
    this.View.EnableIntegration = this.Model.EditableState.EnableIntegration;
    this.UpdateViewDynamicState();
  }

  private void ResetViewState()
  {
    this.View.AllowEditSettings = false;
    this.View.EnableIntegration = false;
    this.View.ShowRestartRequiredWarning = false;
  }

  private void UpdateViewDynamicState()
  {
    this.View.ShowRestartRequiredWarning = this.View.EnableIntegration != this.Model.IMViewerService.Settings.EnableIntegration;
  }

  private void OnViewEditableStateChanged(object sender, EventArgs e)
  {
    this.UpdateViewDynamicState();
    this.RaiseSettingsChanges();
  }

  void IPropertyPageMvpPresenter.AcceptChanges()
  {
    this.Model.EditableState.EnableIntegration = this.View.EnableIntegration;
  }

  void IPropertyPageMvpPresenter.RevertChanges()
  {
    this.Model.Reset();
    this.SetupViewState();
  }

  event EventHandler IPropertyPageMvpPresenter.SettingsChanged
  {
    add => this.settingsChangedHandler += value;
    remove => this.settingsChangedHandler -= value;
  }

  private void RaiseSettingsChanges()
  {
    if (this.settingsChangedHandler == null)
      return;
    this.settingsChangedHandler((object) this, EventArgs.Empty);
  }
}
