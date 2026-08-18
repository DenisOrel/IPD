// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.ExtApps.ExtAppActionsEditor
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.LaunchActions;
using Intermech.Tools.Settings;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace Intermech.Tools.ExtApps;

internal sealed class ExtAppActionsEditor : DataEditorControl
{
  private readonly ExtAppSettingsValidator validator;
  private readonly ExtAppSettingsCodec codec;
  private IContainer components;
  private TextBox tbAppName;
  private TextBox tbExecutable;
  private TextBox tbWorkDirectory;
  private TextBox tbArguments;
  private Label lbAppName;
  private Label lbExecutable;
  private Label lbWorkDirectory;
  private Label lbArguments;
  private GroupBox gbApplication;
  private Label lbWindowStyle;
  private ComboBox cbWindowStyle;
  private Button btSysVarInExecutable;
  private Button btSysVarInWorkDir;
  private Button btSelectWorkDir;
  private Button btSelectExecutable;
  private OpenFileDialog ofdSelectExecutable;
  private FolderBrowserDialog fbdSelectWorkDir;

  public ExtAppActionsEditor()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.validator = new ExtAppSettingsValidator();
    this.codec = new ExtAppSettingsCodec();
    INamedImageList service = ServiceUtils.GetService<INamedImageList>((object) ServicesManager.ServiceContainer, true);
    this.btSelectExecutable.Image = service.ImageList.Images[service.ImageIndex("imgOpenItem")];
    this.btSelectWorkDir.Image = service.ImageList.Images[service.ImageIndex("imgFolder")];
    this.btSysVarInExecutable.Image = service.ImageList.Images[service.ImageIndex("imgSystemVariables")];
    this.btSysVarInWorkDir.Image = service.ImageList.Images[service.ImageIndex("imgSystemVariables")];
    foreach (ProcessWindowStyle processWindowStyle in (ProcessWindowStyle[]) Enum.GetValues(typeof (ProcessWindowStyle)))
      this.cbWindowStyle.Items.Add((object) new ExtAppActionsEditor.WindowStyleWrapper(processWindowStyle));
  }

  public override void SetData(XmlDocument data, bool readOnly)
  {
    base.SetData(data, readOnly);
    ExtAppSettings extAppSettings = (ExtAppSettings) this.codec.Decode(data);
    this.tbAppName.Text = extAppSettings.ApplicationName;
    this.tbAppName.ReadOnly = readOnly;
    this.tbExecutable.Text = extAppSettings.Executable;
    this.tbExecutable.ReadOnly = readOnly;
    this.tbWorkDirectory.Text = extAppSettings.WorkDirectory;
    this.tbWorkDirectory.ReadOnly = readOnly;
    this.tbArguments.Text = extAppSettings.Arguments;
    this.tbArguments.ReadOnly = readOnly;
    this.cbWindowStyle.SelectedItem = (object) new ExtAppActionsEditor.WindowStyleWrapper(extAppSettings.WindowStyle);
    this.cbWindowStyle.Enabled = !readOnly;
  }

  public override XmlDocument GetData()
  {
    ExtAppSettings extAppSettings = new ExtAppSettings();
    extAppSettings.ApplicationName = this.tbAppName.Text;
    extAppSettings.Executable = this.tbExecutable.Text;
    extAppSettings.WorkDirectory = this.tbWorkDirectory.Text;
    extAppSettings.Arguments = this.tbArguments.Text;
    extAppSettings.WindowStyle = ((ExtAppActionsEditor.WindowStyleWrapper) this.cbWindowStyle.SelectedItem).Value;
    this.validator.Validate((ISettingsObject) extAppSettings, SettingsValidatorContext.SettingsObjectOnly);
    return this.codec.Encode((ISettingsObject) extAppSettings);
  }

  private void OnValueChanged(object sender, EventArgs e) => this.RaiseDataChanged();

  private void OnSysVarInExecutable(object sender, EventArgs e)
  {
    this.InsertSystemVariable(this.tbExecutable);
  }

  private void OnSysVarInWorkDir(object sender, EventArgs e)
  {
    this.InsertSystemVariable(this.tbWorkDirectory);
  }

  private void OnSelectExecutable(object sender, EventArgs e)
  {
    if (this.ofdSelectExecutable.ShowDialog() != DialogResult.OK)
      return;
    this.tbExecutable.Text = this.ofdSelectExecutable.FileName;
  }

  private void OnSelectWorkDir(object sender, EventArgs e)
  {
    if (this.fbdSelectWorkDir.ShowDialog() != DialogResult.OK || !(this.fbdSelectWorkDir.SelectedPath != string.Empty))
      return;
    this.tbWorkDirectory.Text = this.fbdSelectWorkDir.SelectedPath;
  }

  private void InsertSystemVariable(TextBox textBox)
  {
    SystemVariablesForm systemVariablesForm = new SystemVariablesForm();
    systemVariablesForm.Initialize(Environment.GetEnvironmentVariables());
    if (systemVariablesForm.ShowDialog() != DialogResult.OK || !(systemVariablesForm.ChoiseVariable != string.Empty))
      return;
    int selectionStart = textBox.SelectionStart;
    StringBuilder stringBuilder = new StringBuilder(textBox.Text);
    stringBuilder.Insert(selectionStart, $"%{systemVariablesForm.ChoiseVariable}%");
    textBox.Text = stringBuilder.ToString();
    textBox.SelectionStart = selectionStart;
    textBox.Focus();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ExtAppActionsEditor));
    this.tbAppName = new TextBox();
    this.tbExecutable = new TextBox();
    this.tbWorkDirectory = new TextBox();
    this.tbArguments = new TextBox();
    this.lbAppName = new Label();
    this.lbExecutable = new Label();
    this.lbWorkDirectory = new Label();
    this.lbArguments = new Label();
    this.gbApplication = new GroupBox();
    this.btSelectWorkDir = new Button();
    this.btSelectExecutable = new Button();
    this.btSysVarInExecutable = new Button();
    this.btSysVarInWorkDir = new Button();
    this.lbWindowStyle = new Label();
    this.cbWindowStyle = new ComboBox();
    this.ofdSelectExecutable = new OpenFileDialog();
    this.fbdSelectWorkDir = new FolderBrowserDialog();
    this.gbApplication.SuspendLayout();
    this.SuspendLayout();
    this.ofdSelectExecutable.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.tbAppName, "tbAppName");
    this.tbAppName.Name = "tbAppName";
    this.tbAppName.TextChanged += new EventHandler(this.OnValueChanged);
    componentResourceManager.ApplyResources((object) this.tbExecutable, "tbExecutable");
    this.tbExecutable.Name = "tbExecutable";
    this.tbExecutable.TextChanged += new EventHandler(this.OnValueChanged);
    componentResourceManager.ApplyResources((object) this.tbWorkDirectory, "tbWorkDirectory");
    this.tbWorkDirectory.Name = "tbWorkDirectory";
    this.tbWorkDirectory.TextChanged += new EventHandler(this.OnValueChanged);
    componentResourceManager.ApplyResources((object) this.tbArguments, "tbArguments");
    this.tbArguments.Name = "tbArguments";
    this.tbArguments.TextChanged += new EventHandler(this.OnValueChanged);
    componentResourceManager.ApplyResources((object) this.lbAppName, "lbAppName");
    this.lbAppName.Name = "lbAppName";
    componentResourceManager.ApplyResources((object) this.lbExecutable, "lbExecutable");
    this.lbExecutable.Name = "lbExecutable";
    componentResourceManager.ApplyResources((object) this.lbWorkDirectory, "lbWorkDirectory");
    this.lbWorkDirectory.Name = "lbWorkDirectory";
    componentResourceManager.ApplyResources((object) this.lbArguments, "lbArguments");
    this.lbArguments.Name = "lbArguments";
    componentResourceManager.ApplyResources((object) this.gbApplication, "gbApplication");
    this.gbApplication.Controls.Add((Control) this.btSelectWorkDir);
    this.gbApplication.Controls.Add((Control) this.btSelectExecutable);
    this.gbApplication.Controls.Add((Control) this.btSysVarInExecutable);
    this.gbApplication.Controls.Add((Control) this.btSysVarInWorkDir);
    this.gbApplication.Controls.Add((Control) this.lbWindowStyle);
    this.gbApplication.Controls.Add((Control) this.cbWindowStyle);
    this.gbApplication.Controls.Add((Control) this.tbExecutable);
    this.gbApplication.Controls.Add((Control) this.lbArguments);
    this.gbApplication.Controls.Add((Control) this.tbAppName);
    this.gbApplication.Controls.Add((Control) this.lbWorkDirectory);
    this.gbApplication.Controls.Add((Control) this.tbWorkDirectory);
    this.gbApplication.Controls.Add((Control) this.lbExecutable);
    this.gbApplication.Controls.Add((Control) this.tbArguments);
    this.gbApplication.Controls.Add((Control) this.lbAppName);
    this.gbApplication.Name = "gbApplication";
    this.gbApplication.TabStop = false;
    componentResourceManager.ApplyResources((object) this.btSelectWorkDir, "btSelectWorkDir");
    this.btSelectWorkDir.Name = "btSelectWorkDir";
    this.btSelectWorkDir.UseVisualStyleBackColor = true;
    this.btSelectWorkDir.Click += new EventHandler(this.OnSelectWorkDir);
    componentResourceManager.ApplyResources((object) this.btSelectExecutable, "btSelectExecutable");
    this.btSelectExecutable.Name = "btSelectExecutable";
    this.btSelectExecutable.UseVisualStyleBackColor = true;
    this.btSelectExecutable.Click += new EventHandler(this.OnSelectExecutable);
    componentResourceManager.ApplyResources((object) this.btSysVarInExecutable, "btSysVarInExecutable");
    this.btSysVarInExecutable.Name = "btSysVarInExecutable";
    this.btSysVarInExecutable.UseVisualStyleBackColor = true;
    this.btSysVarInExecutable.Click += new EventHandler(this.OnSysVarInExecutable);
    componentResourceManager.ApplyResources((object) this.btSysVarInWorkDir, "btSysVarInWorkDir");
    this.btSysVarInWorkDir.Name = "btSysVarInWorkDir";
    this.btSysVarInWorkDir.UseVisualStyleBackColor = true;
    this.btSysVarInWorkDir.Click += new EventHandler(this.OnSysVarInWorkDir);
    componentResourceManager.ApplyResources((object) this.lbWindowStyle, "lbWindowStyle");
    this.lbWindowStyle.Name = "lbWindowStyle";
    componentResourceManager.ApplyResources((object) this.cbWindowStyle, "cbWindowStyle");
    this.cbWindowStyle.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbWindowStyle.FormattingEnabled = true;
    this.cbWindowStyle.Name = "cbWindowStyle";
    this.cbWindowStyle.SelectedIndexChanged += new EventHandler(this.OnValueChanged);
    this.ofdSelectExecutable.DefaultExt = "exe";
    componentResourceManager.ApplyResources((object) this.ofdSelectExecutable, "ofdSelectExecutable");
    this.ofdSelectExecutable.SupportMultiDottedExtensions = true;
    componentResourceManager.ApplyResources((object) this.fbdSelectWorkDir, "fbdSelectWorkDir");
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gbApplication);
    this.MinimumSize = new Size(500, 195);
    this.Name = nameof (ExtAppActionsEditor);
    this.gbApplication.ResumeLayout(false);
    this.gbApplication.PerformLayout();
    this.ResumeLayout(false);
  }

  private class WindowStyleWrapper
  {
    private ProcessWindowStyle value;
    private string displayName;

    public WindowStyleWrapper(ProcessWindowStyle value)
    {
      this.value = value;
      this.displayName = ExtAppActionsEditor.WindowStyleWrapper.GetDisplayName(value);
    }

    private static string GetDisplayName(ProcessWindowStyle value)
    {
      switch (value)
      {
        case ProcessWindowStyle.Normal:
          return LocalizationHolder.rm.GetString("Tools.Client_114");
        case ProcessWindowStyle.Hidden:
          return LocalizationHolder.rm.GetString("Tools.Client_113");
        case ProcessWindowStyle.Minimized:
          return LocalizationHolder.rm.GetString("Tools.Client_115");
        case ProcessWindowStyle.Maximized:
          return LocalizationHolder.rm.GetString("Tools.Client_116");
        default:
          throw new NotSupportedEnumException((Enum) value);
      }
    }

    public ProcessWindowStyle Value => this.value;

    public override int GetHashCode() => this.value.GetHashCode();

    public override bool Equals(object obj)
    {
      return !(obj is ExtAppActionsEditor.WindowStyleWrapper windowStyleWrapper) ? base.Equals(obj) : windowStyleWrapper.value == this.value;
    }

    public override string ToString() => this.displayName;
  }
}
