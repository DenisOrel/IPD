// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.SelectPrototypeConfigurationForm
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Позволяет выбрать конфигурацию документа, которая послужила основой для создания новых конфигураций.
/// </summary>
internal sealed class SelectPrototypeConfigurationForm : Form
{
  private string descriptionTemplate;
  private string document;
  private string configurationName;
  private List<SelectPrototypeConfigurationForm.ConfigurationInfo> configurations;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PictureBox pbAlert;
  private Button btOK;
  private Button btCancel;
  private Label lbDescription;
  private ListView lvConfigurations;
  private ColumnHeader chConfigurationName;
  private ColumnHeader chDesignation;
  private ColumnHeader chOKPCode;
  private ColumnHeader chName;

  /// <summary>Создает объект.</summary>
  public SelectPrototypeConfigurationForm()
  {
    this.InitializeComponent();
    this.descriptionTemplate = this.lbDescription.Text;
  }

  /// <summary>Возвращает или задает описание документа.</summary>
  public string Document
  {
    get => this.document;
    set => this.document = value;
  }

  /// <summary>
  /// Возвращает или задает имя искмой конфигурации документа.
  /// </summary>
  public string ConfigurationName
  {
    get => this.configurationName;
    set => this.configurationName = value;
  }

  /// <summary>
  /// Возвращает или задает список конфигураций документа, среди которых пользователь будет выбирать
  /// конфигурацию-прототип.
  /// </summary>
  public List<SelectPrototypeConfigurationForm.ConfigurationInfo> Configurations
  {
    get => this.configurations;
    set => this.configurations = value;
  }

  /// <summary>Возвращает индекс изделия, выбранного пользователем.</summary>
  public int SelectedConfiguration
  {
    get
    {
      return this.lvConfigurations.SelectedIndices.Count <= 0 ? -1 : this.lvConfigurations.SelectedIndices[0];
    }
  }

  private void SelectPrototypeConfigurationForm_Shown(object sender, EventArgs e)
  {
    this.SetupDescription();
    this.SetupConfigurationList();
  }

  private void SetupDescription()
  {
    this.lbDescription.Text = string.Format(this.descriptionTemplate, this.document == null ? (object) string.Empty : (object) this.document, this.configurationName == null ? (object) string.Empty : (object) this.configurationName);
  }

  private void SetupConfigurationList()
  {
    this.lvConfigurations.BeginUpdate();
    try
    {
      this.lvConfigurations.Items.Clear();
      if (this.configurations == null)
        return;
      for (int index = 0; index < this.configurations.Count; ++index)
      {
        SelectPrototypeConfigurationForm.ConfigurationInfo configuration = this.configurations[index];
        this.lvConfigurations.Items.Add(new ListViewItem(configuration.ConfigurationName)
        {
          SubItems = {
            configuration.Designation,
            configuration.OKPCode,
            configuration.Name
          },
          Tag = (object) configuration
        });
      }
    }
    finally
    {
      this.lvConfigurations.EndUpdate();
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectPrototypeConfigurationForm));
    this.pbAlert = new PictureBox();
    this.btOK = new Button();
    this.btCancel = new Button();
    this.lbDescription = new Label();
    this.lvConfigurations = new ListView();
    this.chConfigurationName = new ColumnHeader();
    this.chDesignation = new ColumnHeader();
    this.chOKPCode = new ColumnHeader();
    this.chName = new ColumnHeader();
    ((ISupportInitialize) this.pbAlert).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.pbAlert, "pbAlert");
    this.pbAlert.Name = "pbAlert";
    this.pbAlert.TabStop = false;
    componentResourceManager.ApplyResources((object) this.btOK, "btOK");
    this.btOK.DialogResult = DialogResult.OK;
    this.btOK.Name = "btOK";
    this.btOK.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
    this.btCancel.DialogResult = DialogResult.Cancel;
    this.btCancel.Name = "btCancel";
    this.btCancel.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lbDescription, "lbDescription");
    this.lbDescription.Name = "lbDescription";
    componentResourceManager.ApplyResources((object) this.lvConfigurations, "lvConfigurations");
    this.lvConfigurations.Columns.AddRange(new ColumnHeader[4]
    {
      this.chConfigurationName,
      this.chDesignation,
      this.chOKPCode,
      this.chName
    });
    this.lvConfigurations.FullRowSelect = true;
    this.lvConfigurations.GridLines = true;
    this.lvConfigurations.HideSelection = false;
    this.lvConfigurations.MultiSelect = false;
    this.lvConfigurations.Name = "lvConfigurations";
    this.lvConfigurations.UseCompatibleStateImageBehavior = false;
    this.lvConfigurations.View = View.Details;
    componentResourceManager.ApplyResources((object) this.chConfigurationName, "chConfigurationName");
    componentResourceManager.ApplyResources((object) this.chDesignation, "chDesignation");
    componentResourceManager.ApplyResources((object) this.chOKPCode, "chOKPCode");
    componentResourceManager.ApplyResources((object) this.chName, "chName");
    this.AcceptButton = (IButtonControl) this.btCancel;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btCancel;
    this.Controls.Add((Control) this.lvConfigurations);
    this.Controls.Add((Control) this.lbDescription);
    this.Controls.Add((Control) this.btCancel);
    this.Controls.Add((Control) this.btOK);
    this.Controls.Add((Control) this.pbAlert);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectPrototypeConfigurationForm);
    this.ShowInTaskbar = false;
    this.Shown += new EventHandler(this.SelectPrototypeConfigurationForm_Shown);
    ((ISupportInitialize) this.pbAlert).EndInit();
    this.ResumeLayout(false);
  }

  public sealed class ConfigurationInfo
  {
    private string configurationName;
    private string designation;
    private string okpCode;
    private string name;

    public ConfigurationInfo(
      string configurationName,
      string designation,
      string okpCode,
      string name)
    {
      this.configurationName = configurationName == null ? string.Empty : configurationName;
      this.designation = designation == null ? string.Empty : designation;
      this.okpCode = okpCode == null ? string.Empty : okpCode;
      this.name = name == null ? string.Empty : name;
    }

    public string ConfigurationName => this.configurationName;

    public string Designation => this.designation;

    public string OKPCode => this.okpCode;

    public string Name => this.name;
  }
}
