
// Type: Intermech.Tools.Integrators.IntegratorSettingsPropertyEditor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Settings;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует редактор настроек интегратора на основе PropertyGrid.
/// </summary>
public sealed class IntegratorSettingsPropertyEditor : DataEditorControl
{
  private IIntegrator integrator;
  private IPersistentIntegratorSettingsService settingsService;
  private IIntegratorSettingsViewModelService settingsViewModelService;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Label lbDescription;
  private PictureBox pbIntegratorIcon;
  private PropertyGrid pgToolSettings;

  /// <summary>Создает объект.</summary>
  public IntegratorSettingsPropertyEditor() => this.InitializeComponent();

  /// <summary>Выполняет начальную инициализацию редактора настроек.</summary>
  /// <param name="integrator">Объект интегратора</param>
  /// <param name="settingsService">Сервис настроек интегратора</param>
  /// <param name="settingsViewModelService">Сервис моделей представления для настроек интегратора. Может быть не задан</param>
  public void Initialize(
    IIntegrator integrator,
    IPersistentIntegratorSettingsService settingsService,
    IIntegratorSettingsViewModelService settingsViewModelService)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (settingsService == null)
      throw new ArgumentNullException(nameof (settingsService));
    this.integrator = integrator;
    this.settingsService = settingsService;
    this.settingsViewModelService = settingsViewModelService;
    Image applicationImage = this.integrator.GetApplicationImage(AppImageSize.Image32x32);
    if (applicationImage == null)
      return;
    this.pbIntegratorIcon.Image = applicationImage;
  }

  /// <summary>Передает редактору объект с настройками.</summary>
  /// <param name="data">Настройки</param>
  /// <param name="readOnly">Признак режима отображения настроек без возможности редактирования</param>
  public override void SetData(XmlDocument data, bool readOnly)
  {
    this.RequireInitialized();
    base.SetData(data, readOnly);
    ISettingsObject settingsObject = this.settingsService.DecodeSettings(data);
    this.FillPropertyGrid(this.settingsViewModelService != null ? this.settingsViewModelService.CreateViewModel(settingsObject) : (object) settingsObject);
  }

  private void FillPropertyGrid(object editableObject)
  {
    if (editableObject == null)
      throw new ArgumentNullException(nameof (editableObject));
    if (editableObject is ICloneable)
      editableObject = (object) new EditableObjectChangeHighlighter((ICloneable) editableObject);
    this.pgToolSettings.SelectedObject = editableObject;
    this.pgToolSettings.ExpandAllGridItems();
    this.ActiveControl = (Control) this.pgToolSettings;
  }

  /// <summary>
  /// Редактор возвращает новый объект настроек, содержащий все сделанные пользователем изменения.
  /// </summary>
  /// <returns>Объект с настройками</returns>
  public override XmlDocument GetData()
  {
    this.RequireInitialized();
    object viewModelObject = this.pgToolSettings.SelectedObject;
    if (viewModelObject == null)
      throw new InvalidOperationException();
    if (viewModelObject is EditableObjectChangeHighlighter)
      viewModelObject = ((EditableObjectChangeHighlighter) viewModelObject).EditableObject;
    ISettingsObject settingsObject = this.settingsViewModelService != null ? this.settingsViewModelService.CreateSettingsFromViewModel(viewModelObject) : (ISettingsObject) viewModelObject;
    this.settingsService.ValidateSettings(settingsObject, SettingsValidatorContext.SettingsObjectOnly);
    return this.settingsService.EncodeSettings(settingsObject);
  }

  private void OnPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    this.RaiseDataChanged();
  }

  private void RequireInitialized()
  {
    if (this.integrator == null || this.settingsService == null)
      throw this.NotInitializedException();
  }

  private Exception NotInitializedException()
  {
    return (Exception) new InvalidOperationException($"{this.GetType()} must be initialized first. Use the Initialize() method.");
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (IntegratorSettingsPropertyEditor));
    this.lbDescription = new Label();
    this.pbIntegratorIcon = new PictureBox();
    this.pgToolSettings = new PropertyGrid();
    ((ISupportInitialize) this.pbIntegratorIcon).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this.pbIntegratorIcon, "pbIntegratorIcon");
    this.pbIntegratorIcon.Name = "pbIntegratorIcon";
    this.pbIntegratorIcon.TabStop = false;
    componentResourceManager.ApplyResources((object) this.pgToolSettings, "pgToolSettings");
    this.pgToolSettings.Name = "pgToolSettings";
    this.pgToolSettings.PropertySort = PropertySort.Categorized;
    this.pgToolSettings.ToolbarVisible = false;
    this.pgToolSettings.PropertyValueChanged += new PropertyValueChangedEventHandler(this.OnPropertyValueChanged);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.pbIntegratorIcon);
    this.Controls.Add((Control) this.pgToolSettings);
    this.MinimumSize = new Size(570, 310);
    this.Name = "SettingsPropertyEditor";
    this.Tag = (object) "";
    ((ISupportInitialize) this.pbIntegratorIcon).EndInit();
    this.ResumeLayout(false);
  }
}
