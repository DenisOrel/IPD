// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Setup.StandaloneViewSettingsControl
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.StandaloneView;
using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Tools.Setup;

internal class StandaloneViewSettingsControl : MvpUserControl, IView
{
  private IStandaloneViewSettingsService settingsService;
  private int objectType;
  private StandaloneViewObjectTypeSettings originalSettings;
  private bool initControlsMode;
  private bool isModified;
  private IContainer components;
  private ListBox libObjectAttributes;
  private CheckBox cbInheritObjectAttributesSettings;
  private GroupBox gbObjectAttributesSettings;
  private Button btRemoveAttribute;
  private Button btAddAttribute;
  private CheckBox cbWriteObjectAttributes;
  private Button btApplyChanges;
  private Button btRevertChanges;
  private GroupBox gbSignsSettings;
  private CheckBox cbWriteSigns;
  private CheckBox cbInheritSignsSettings;
  private GroupBox gbFileChecksumSettings;
  private CheckBox cbWriteFileChecksum;
  private CheckBox cbInheritFileChecksumSettings;

  public StandaloneViewSettingsControl()
  {
    this.InitializeComponent();
    this.objectType = -1;
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IStandaloneViewSettingsService SettingsService
  {
    [DebuggerStepThrough] get => this.settingsService;
    [DebuggerStepThrough] set => this.settingsService = value;
  }

  private void CheckInitialized()
  {
    if (this.settingsService == null)
      throw new InvalidOperationException("The settings service property is not set.");
  }

  public void InitializeData(int objectType)
  {
    this.CheckInitialized();
    this.objectType = objectType;
    this.initControlsMode = true;
    try
    {
      this.originalSettings = this.settingsService.TryLoadSettings(this.objectType);
      if (this.originalSettings == null)
        this.originalSettings = new StandaloneViewObjectTypeSettings();
      this.InitializeSignsSettings(this.originalSettings);
      this.InitializeFileChecksumSettings(this.originalSettings);
      this.InitializeInjectedAttributesSettings(this.originalSettings);
    }
    finally
    {
      this.initControlsMode = false;
    }
    this.ResetPageIsModified();
  }

  public void ApplyChangesIfModified(bool askUser)
  {
    this.CheckInitialized();
    if (!this.IsPageModified())
      return;
    if (askUser && MessageBox.Show("Сохранить изменения в настройках типа объектов?", "Сохранение изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
    {
      this.RevertChangesIfModified();
    }
    else
    {
      StandaloneViewObjectTypeSettings settings = this.originalSettings.Clone();
      this.CaptureSignsSettings(settings);
      this.CaptureFileChecksumSettings(settings);
      this.CaptureInjectedAttributesSettings(settings);
      if (settings.IsEmpty)
        this.settingsService.RemoveSettings(this.objectType);
      else
        this.settingsService.SaveSettings(this.objectType, settings);
      this.originalSettings = settings;
      this.ResetPageIsModified();
    }
  }

  public void RevertChangesIfModified()
  {
    if (!this.IsPageModified())
      return;
    this.InitializeData(this.objectType);
  }

  private void SetPageIsModified()
  {
    this.isModified = true;
    this.btApplyChanges.Enabled = true;
    this.btRevertChanges.Enabled = true;
  }

  private void ResetPageIsModified()
  {
    this.isModified = false;
    this.btApplyChanges.Enabled = false;
    this.btRevertChanges.Enabled = false;
  }

  private bool IsPageModified() => this.isModified;

  private void btApplyChanges_Click(object sender, EventArgs e)
  {
    this.ApplyChangesIfModified(false);
  }

  private void btRevertChanges_Click(object sender, EventArgs e) => this.RevertChangesIfModified();

  private void PageControl_Changed(object sender, EventArgs e)
  {
    if (this.initControlsMode)
      return;
    this.SetPageIsModified();
  }

  private void InitializeSignsSettings(StandaloneViewObjectTypeSettings settings)
  {
    if (!settings.InjectSigns.HasValue)
    {
      this.cbInheritSignsSettings.Checked = true;
      this.InitializeInheritedSignsControls();
    }
    else
    {
      this.cbInheritSignsSettings.Checked = false;
      this.InitializeCustomSignsControls(settings.InjectSigns.Value);
    }
  }

  private void InitializeInheritedSignsControls()
  {
    this.cbWriteSigns.Enabled = false;
    this.cbWriteSigns.Checked = false;
  }

  private void InitializeCustomSignsControls(bool value)
  {
    this.cbWriteSigns.Enabled = true;
    this.cbWriteSigns.Checked = value;
  }

  private void CaptureSignsSettings(StandaloneViewObjectTypeSettings settings)
  {
    if (this.cbInheritSignsSettings.Checked)
      settings.InjectSigns = new bool?();
    else
      settings.InjectSigns = new bool?(this.cbWriteSigns.Checked);
  }

  private void cbInheritSignsSettings_CheckedChanged(object sender, EventArgs e)
  {
    if (this.initControlsMode)
      return;
    this.initControlsMode = true;
    try
    {
      if (this.cbInheritSignsSettings.Checked)
        this.InitializeInheritedSignsControls();
      else
        this.InitializeCustomSignsControls(false);
    }
    finally
    {
      this.initControlsMode = false;
    }
    this.SetPageIsModified();
  }

  private void InitializeFileChecksumSettings(StandaloneViewObjectTypeSettings settings)
  {
    if (!settings.InjectFileChecksum.HasValue)
    {
      this.cbInheritFileChecksumSettings.Checked = true;
      this.InitializeInheritedFileChecksumControls();
    }
    else
    {
      this.cbInheritFileChecksumSettings.Checked = false;
      this.InitializeCustomFileChecksumControls(settings.InjectFileChecksum.Value);
    }
  }

  private void InitializeInheritedFileChecksumControls()
  {
    this.cbWriteFileChecksum.Enabled = false;
    this.cbWriteFileChecksum.Checked = false;
  }

  private void InitializeCustomFileChecksumControls(bool value)
  {
    this.cbWriteFileChecksum.Enabled = true;
    this.cbWriteFileChecksum.Checked = value;
  }

  private void CaptureFileChecksumSettings(StandaloneViewObjectTypeSettings settings)
  {
    if (this.cbInheritFileChecksumSettings.Checked)
      settings.InjectFileChecksum = new bool?();
    else
      settings.InjectFileChecksum = new bool?(this.cbWriteFileChecksum.Checked);
  }

  private void cbInheritFileChecksumSettings_CheckedChanged(object sender, EventArgs e)
  {
    if (this.initControlsMode)
      return;
    this.initControlsMode = true;
    try
    {
      if (this.cbInheritFileChecksumSettings.Checked)
        this.InitializeInheritedFileChecksumControls();
      else
        this.InitializeCustomFileChecksumControls(false);
    }
    finally
    {
      this.initControlsMode = false;
    }
    this.SetPageIsModified();
  }

  private void InitializeInjectedAttributesSettings(StandaloneViewObjectTypeSettings settings)
  {
    if (settings.InjectedAttributes == null)
    {
      this.cbInheritObjectAttributesSettings.Checked = true;
      this.InitializeInheritedInjectedAttributesControls();
    }
    else
    {
      this.cbInheritObjectAttributesSettings.Checked = false;
      this.InitializeCustomInjectedAttributesControls(settings.InjectedAttributes);
    }
  }

  private void InitializeInheritedInjectedAttributesControls()
  {
    this.cbWriteObjectAttributes.Enabled = false;
    this.cbWriteObjectAttributes.Checked = false;
    this.libObjectAttributes.Enabled = false;
    this.libObjectAttributes.Items.Clear();
    this.btAddAttribute.Enabled = false;
    this.btRemoveAttribute.Enabled = false;
  }

  private void InitializeCustomInjectedAttributesControls(
    StandaloneViewInjectedAttributesSettings injectedAttributes)
  {
    this.cbWriteObjectAttributes.Enabled = true;
    this.cbWriteObjectAttributes.Checked = injectedAttributes.Enabled;
    this.libObjectAttributes.BeginUpdate();
    try
    {
      this.libObjectAttributes.Enabled = true;
      this.libObjectAttributes.Items.Clear();
      foreach (Guid identifier in (IEnumerable<Guid>) injectedAttributes.Identifiers)
      {
        GlobalId<int> attributeGid = this.TryConvertToAttributeGID(identifier);
        if (attributeGid != null)
          this.libObjectAttributes.Items.Add((object) attributeGid);
      }
      if (this.libObjectAttributes.Items.Count != 0)
        this.libObjectAttributes.SelectedIndex = 0;
    }
    finally
    {
      this.libObjectAttributes.EndUpdate();
    }
    this.btAddAttribute.Enabled = true;
    this.UpdateRemoveAttributeButtonState();
  }

  private void CaptureInjectedAttributesSettings(StandaloneViewObjectTypeSettings settings)
  {
    if (this.cbInheritObjectAttributesSettings.Checked)
    {
      settings.InjectedAttributes = (StandaloneViewInjectedAttributesSettings) null;
    }
    else
    {
      if (settings.InjectedAttributes == null)
        settings.InjectedAttributes = new StandaloneViewInjectedAttributesSettings();
      this.CaptureCustomInjectedAttributes(settings.InjectedAttributes);
    }
  }

  private void CaptureCustomInjectedAttributes(
    StandaloneViewInjectedAttributesSettings injectedAttributes)
  {
    injectedAttributes.Enabled = this.cbWriteObjectAttributes.Checked;
    injectedAttributes.Identifiers.Clear();
    foreach (GlobalId<int> globalId in this.libObjectAttributes.Items)
      injectedAttributes.Identifiers.Add(globalId.Guid);
  }

  private void cbInheritObjectAttributesSettings_CheckedChanged(object sender, EventArgs e)
  {
    if (this.initControlsMode)
      return;
    this.initControlsMode = true;
    try
    {
      if (this.cbInheritObjectAttributesSettings.Checked)
        this.InitializeInheritedInjectedAttributesControls();
      else
        this.InitializeCustomInjectedAttributesControls(new StandaloneViewInjectedAttributesSettings());
    }
    finally
    {
      this.initControlsMode = false;
    }
    this.SetPageIsModified();
  }

  private void btAddAttribute_Click(object sender, EventArgs e)
  {
    if (this.initControlsMode)
      return;
    using (AttributesSelectDlg attributesSelectDlg = new AttributesSelectDlg(false))
    {
      attributesSelectDlg.ShowCreateAttrBtn = false;
      attributesSelectDlg.RelationGroupEnable = false;
      attributesSelectDlg.ForbiddenAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[4]
      {
        FieldTypes.ftBlob,
        FieldTypes.ftFile,
        FieldTypes.ftShortBlob,
        FieldTypes.ftPassword
      });
      if (attributesSelectDlg.ShowDialog() != DialogResult.OK || attributesSelectDlg.SelectedAttributesID.Count != 1)
        return;
      GlobalId<int> attributeGid = this.TryConvertToAttributeGID(attributesSelectDlg.SelectedAttributesID[0]);
      if (attributeGid == null)
        return;
      this.libObjectAttributes.SelectedIndex = this.libObjectAttributes.Items.Add((object) attributeGid);
      this.SetPageIsModified();
    }
  }

  private void btRemoveAttribute_Click(object sender, EventArgs e)
  {
    if (this.initControlsMode)
      return;
    int selectedIndex = this.libObjectAttributes.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.libObjectAttributes.Items.RemoveAt(selectedIndex);
    if (selectedIndex >= this.libObjectAttributes.Items.Count)
      --selectedIndex;
    if (selectedIndex >= 0)
      this.libObjectAttributes.SelectedIndex = selectedIndex;
    this.SetPageIsModified();
  }

  private void libObjectAttributes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.libObjectAttributes.Enabled)
      return;
    this.UpdateRemoveAttributeButtonState();
  }

  private void UpdateRemoveAttributeButtonState()
  {
    this.btRemoveAttribute.Enabled = this.libObjectAttributes.SelectedIndex >= 0;
  }

  private GlobalId<int> TryConvertToAttributeGID(Guid attrTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrTypeId, false);
      if (attributeType != null)
        return new GlobalId<int>(attrTypeId, attributeType.AttributeID, attributeType.Name);
    }
    return (GlobalId<int>) null;
  }

  private GlobalId<int> TryConvertToAttributeGID(int attrTypeId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(attrTypeId, false);
      if (attributeType != null)
        return new GlobalId<int>(((IDBGuid) attributeType).GUID, attrTypeId, attributeType.Name);
    }
    return (GlobalId<int>) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.libObjectAttributes = new ListBox();
    this.cbInheritObjectAttributesSettings = new CheckBox();
    this.gbObjectAttributesSettings = new GroupBox();
    this.btRemoveAttribute = new Button();
    this.btAddAttribute = new Button();
    this.cbWriteObjectAttributes = new CheckBox();
    this.btApplyChanges = new Button();
    this.btRevertChanges = new Button();
    this.gbSignsSettings = new GroupBox();
    this.cbWriteSigns = new CheckBox();
    this.cbInheritSignsSettings = new CheckBox();
    this.gbFileChecksumSettings = new GroupBox();
    this.cbWriteFileChecksum = new CheckBox();
    this.cbInheritFileChecksumSettings = new CheckBox();
    this.gbObjectAttributesSettings.SuspendLayout();
    this.gbSignsSettings.SuspendLayout();
    this.gbFileChecksumSettings.SuspendLayout();
    this.SuspendLayout();
    this.libObjectAttributes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.libObjectAttributes.FormattingEnabled = true;
    this.libObjectAttributes.Location = new Point(30, 76);
    this.libObjectAttributes.Name = "libObjectAttributes";
    this.libObjectAttributes.Size = new Size(297, 82);
    this.libObjectAttributes.TabIndex = 2;
    this.libObjectAttributes.SelectedIndexChanged += new EventHandler(this.libObjectAttributes_SelectedIndexChanged);
    this.cbInheritObjectAttributesSettings.AutoSize = true;
    this.cbInheritObjectAttributesSettings.Location = new Point(6, 30);
    this.cbInheritObjectAttributesSettings.Name = "cbInheritObjectAttributesSettings";
    this.cbInheritObjectAttributesSettings.Size = new Size(311, 17);
    this.cbInheritObjectAttributesSettings.TabIndex = 0;
    this.cbInheritObjectAttributesSettings.Text = "Использовать настройки родительского типа объектов";
    this.cbInheritObjectAttributesSettings.UseVisualStyleBackColor = true;
    this.cbInheritObjectAttributesSettings.CheckedChanged += new EventHandler(this.cbInheritObjectAttributesSettings_CheckedChanged);
    this.gbObjectAttributesSettings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.gbObjectAttributesSettings.Controls.Add((Control) this.btRemoveAttribute);
    this.gbObjectAttributesSettings.Controls.Add((Control) this.libObjectAttributes);
    this.gbObjectAttributesSettings.Controls.Add((Control) this.btAddAttribute);
    this.gbObjectAttributesSettings.Controls.Add((Control) this.cbWriteObjectAttributes);
    this.gbObjectAttributesSettings.Controls.Add((Control) this.cbInheritObjectAttributesSettings);
    this.gbObjectAttributesSettings.Location = new Point(3, 215);
    this.gbObjectAttributesSettings.Name = "gbObjectAttributesSettings";
    this.gbObjectAttributesSettings.Size = new Size(444, 185);
    this.gbObjectAttributesSettings.TabIndex = 2;
    this.gbObjectAttributesSettings.TabStop = false;
    this.gbObjectAttributesSettings.Text = "Запись атрибутов объекта в файл";
    this.btRemoveAttribute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btRemoveAttribute.Enabled = false;
    this.btRemoveAttribute.Location = new Point(343, 107);
    this.btRemoveAttribute.Name = "btRemoveAttribute";
    this.btRemoveAttribute.Size = new Size(95, 25);
    this.btRemoveAttribute.TabIndex = 4;
    this.btRemoveAttribute.Text = "Удалить";
    this.btRemoveAttribute.UseVisualStyleBackColor = true;
    this.btRemoveAttribute.Click += new EventHandler(this.btRemoveAttribute_Click);
    this.btAddAttribute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btAddAttribute.Location = new Point(343, 76);
    this.btAddAttribute.Name = "btAddAttribute";
    this.btAddAttribute.Size = new Size(95, 25);
    this.btAddAttribute.TabIndex = 3;
    this.btAddAttribute.Text = "Добавить";
    this.btAddAttribute.UseVisualStyleBackColor = true;
    this.btAddAttribute.Click += new EventHandler(this.btAddAttribute_Click);
    this.cbWriteObjectAttributes.AutoSize = true;
    this.cbWriteObjectAttributes.Location = new Point(30, 53);
    this.cbWriteObjectAttributes.Name = "cbWriteObjectAttributes";
    this.cbWriteObjectAttributes.Size = new Size(168, 17);
    this.cbWriteObjectAttributes.TabIndex = 1;
    this.cbWriteObjectAttributes.Text = "Включить запись атрибутов";
    this.cbWriteObjectAttributes.UseVisualStyleBackColor = true;
    this.cbWriteObjectAttributes.CheckedChanged += new EventHandler(this.PageControl_Changed);
    this.btApplyChanges.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btApplyChanges.Location = new Point(251, 422);
    this.btApplyChanges.Name = "btApplyChanges";
    this.btApplyChanges.Size = new Size(95, 25);
    this.btApplyChanges.TabIndex = 3;
    this.btApplyChanges.Text = "Применить";
    this.btApplyChanges.UseVisualStyleBackColor = true;
    this.btApplyChanges.Click += new EventHandler(this.btApplyChanges_Click);
    this.btRevertChanges.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btRevertChanges.Location = new Point(352, 422);
    this.btRevertChanges.Name = "btRevertChanges";
    this.btRevertChanges.Size = new Size(95, 25);
    this.btRevertChanges.TabIndex = 4;
    this.btRevertChanges.Text = "Отменить";
    this.btRevertChanges.UseVisualStyleBackColor = true;
    this.btRevertChanges.Click += new EventHandler(this.btRevertChanges_Click);
    this.gbSignsSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbSignsSettings.Controls.Add((Control) this.cbWriteSigns);
    this.gbSignsSettings.Controls.Add((Control) this.cbInheritSignsSettings);
    this.gbSignsSettings.Location = new Point(3, 3);
    this.gbSignsSettings.Name = "gbSignsSettings";
    this.gbSignsSettings.Size = new Size(444, 100);
    this.gbSignsSettings.TabIndex = 0;
    this.gbSignsSettings.TabStop = false;
    this.gbSignsSettings.Text = "Запись подписей объекта в файл";
    this.cbWriteSigns.AutoSize = true;
    this.cbWriteSigns.Location = new Point(30, 53);
    this.cbWriteSigns.Name = "cbWriteSigns";
    this.cbWriteSigns.Size = new Size(178, 17);
    this.cbWriteSigns.TabIndex = 1;
    this.cbWriteSigns.Text = "Записывать подписи объекта";
    this.cbWriteSigns.UseVisualStyleBackColor = true;
    this.cbWriteSigns.CheckedChanged += new EventHandler(this.PageControl_Changed);
    this.cbInheritSignsSettings.AutoSize = true;
    this.cbInheritSignsSettings.Location = new Point(6, 30);
    this.cbInheritSignsSettings.Name = "cbInheritSignsSettings";
    this.cbInheritSignsSettings.Size = new Size(311, 17);
    this.cbInheritSignsSettings.TabIndex = 0;
    this.cbInheritSignsSettings.Text = "Использовать настройки родительского типа объектов";
    this.cbInheritSignsSettings.UseVisualStyleBackColor = true;
    this.cbInheritSignsSettings.CheckedChanged += new EventHandler(this.cbInheritSignsSettings_CheckedChanged);
    this.gbFileChecksumSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbFileChecksumSettings.Controls.Add((Control) this.cbWriteFileChecksum);
    this.gbFileChecksumSettings.Controls.Add((Control) this.cbInheritFileChecksumSettings);
    this.gbFileChecksumSettings.Location = new Point(3, 109);
    this.gbFileChecksumSettings.Name = "gbFileChecksumSettings";
    this.gbFileChecksumSettings.Size = new Size(444, 100);
    this.gbFileChecksumSettings.TabIndex = 1;
    this.gbFileChecksumSettings.TabStop = false;
    this.gbFileChecksumSettings.Text = "Запись контрольной суммы в файл";
    this.cbWriteFileChecksum.AutoSize = true;
    this.cbWriteFileChecksum.Location = new Point(30, 53);
    this.cbWriteFileChecksum.Name = "cbWriteFileChecksum";
    this.cbWriteFileChecksum.Size = new Size(227, 17);
    this.cbWriteFileChecksum.TabIndex = 3;
    this.cbWriteFileChecksum.Text = "Записывать контрольную сумму файла";
    this.cbWriteFileChecksum.UseVisualStyleBackColor = true;
    this.cbWriteFileChecksum.CheckedChanged += new EventHandler(this.PageControl_Changed);
    this.cbInheritFileChecksumSettings.AutoSize = true;
    this.cbInheritFileChecksumSettings.Location = new Point(6, 30);
    this.cbInheritFileChecksumSettings.Name = "cbInheritFileChecksumSettings";
    this.cbInheritFileChecksumSettings.Size = new Size(311, 17);
    this.cbInheritFileChecksumSettings.TabIndex = 2;
    this.cbInheritFileChecksumSettings.Text = "Использовать настройки родительского типа объектов";
    this.cbInheritFileChecksumSettings.UseVisualStyleBackColor = true;
    this.cbInheritFileChecksumSettings.CheckedChanged += new EventHandler(this.cbInheritFileChecksumSettings_CheckedChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gbFileChecksumSettings);
    this.Controls.Add((Control) this.gbSignsSettings);
    this.Controls.Add((Control) this.btRevertChanges);
    this.Controls.Add((Control) this.btApplyChanges);
    this.Controls.Add((Control) this.gbObjectAttributesSettings);
    this.MinimumSize = new Size(450, 450);
    this.Name = nameof (StandaloneViewSettingsControl);
    this.Size = new Size(450, 450);
    this.gbObjectAttributesSettings.ResumeLayout(false);
    this.gbObjectAttributesSettings.PerformLayout();
    this.gbSignsSettings.ResumeLayout(false);
    this.gbSignsSettings.PerformLayout();
    this.gbFileChecksumSettings.ResumeLayout(false);
    this.gbFileChecksumSettings.PerformLayout();
    this.ResumeLayout(false);
  }
}
