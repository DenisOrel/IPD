// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivityPropertyPages.RemoteSubProcessSettingPageControl
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.WebPortal;
using Intermech.Interfaces.Workflow;
using Intermech.Site.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design.ActivityPropertyPages;

public class RemoteSubProcessSettingPageControl : UserControl
{
  private bool _readOnly;
  private ActivitySettings _settings;
  private IPortalConnector _portalSrv;
  private bool _chekedRemoteWaitSaved;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private GroupBox RemoteProcess2GroupBox;
  private Button loadPublishOptions;
  private Button savePublishOptions;
  private CheckBox CreateReceiptCheckBox;
  private Label label13;
  private ComboBox PubCompositionCombo;
  private Button PubRelTypesButton;
  private CheckBox GiveOwnershipCheckBox;
  private Button PubObjectTypesButton;
  private Panel panel9;
  private GroupBox RemoteProcessGroupBox;
  private ComboBox RemoteSchemesComboBox;
  private ComboBox SitesComboBox;
  private Label label12;
  private Label label11;
  private CheckBox RemoteWaitCheckBox;
  private Panel PortalErrPanel;
  private AutoSizeLabel PortalErrLabel;
  private PictureBox PortalErrImage;
  private ImageList MiscIL;
  private CheckBox autoPublishReplicationCheckBox;
  private GroupBox groupBox3;
  private ComboBox cbPriorityRemoteTask;

  public RemoteSubProcessSettingPageControl() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this._readOnly;
    set
    {
      this._readOnly = value;
      if (!this._readOnly)
        return;
      ControlFuncs.SetControlsReadOnly((Control) this, value);
    }
  }

  public bool LoadRemoteSubProcessSettingPageControl(
    ActivitySettings settings,
    IDBObject activityObject)
  {
    this._settings = settings;
    bool flag = false;
    if (settings.ActivityType == wfConsts.RemoteSubProcessTypeID)
    {
      this._portalSrv = (ApplicationServices.Container.GetService(typeof (IMServerService)) is IMServerService service ? service.GetCustomService(typeof (IPortalConnector)) : (object) null) as IPortalConnector;
      if (this._portalSrv != null)
      {
        if (service?.GetCustomService(typeof (ISitesCacheService)) is ISitesCacheService customService && customService.Info != null)
        {
          foreach (SiteInfo site in customService.Sites)
          {
            if (site.ID != customService.Info.ID)
              this.SitesComboBox.Items.Add((object) site);
          }
        }
        this.RemoteSchemesComboBox.DropDown += new EventHandler(this.RemoteSchemesComboBox_DropDown);
      }
      else
      {
        this.PortalErrImage.Image = this.MiscIL.Images[10];
        this.PortalErrPanel.Visible = true;
        this.RemoteProcessGroupBox.Enabled = false;
        this.RemoteProcess2GroupBox.Enabled = false;
      }
      string g = settings.ExtProperties.Read("Site");
      string name = "";
      if (g != "")
      {
        Guid guid = new Guid(g);
        foreach (SiteInfo siteInfo in this.SitesComboBox.Items)
        {
          if (siteInfo.GUID == guid)
          {
            this.SitesComboBox.SelectedItem = (object) siteInfo;
            break;
          }
        }
        name = settings.ExtProperties.Read("TplName");
        if (name != "")
        {
          this.RemoteSchemesComboBox.Items.Add((object) new ProcessTemplateInfo(new Guid(settings.ExtProperties.Read("TplGuid")), name));
          this.RemoteSchemesComboBox.SelectedIndex = 0;
        }
      }
      IDBAttribute attributeById = activityObject.GetAttributeByID(wfConsts.AttrWaitForCompletionID);
      if (attributeById != null)
        this.RemoteWaitCheckBox.Checked = attributeById.AsBoolean;
      if (settings.Participants != null && settings.Participants.Count > 0)
        this.RemoteWaitCheckBox.Visible = false;
      this.GiveOwnershipCheckBox.Checked = settings.ExtProperties.ReadBool("GiveOwnership");
      int num1 = (int) settings.ExtProperties.ReadInteger("MaxCompositionLevel", -1L);
      int num2;
      switch (num1)
      {
        case -1:
          num2 = 0;
          break;
        case 0:
          num2 = 2;
          break;
        case 1:
          num2 = 1;
          break;
        default:
          num2 = num1 + 1;
          break;
      }
      this.PubCompositionCombo.SelectedIndex = num2;
      this.CreateReceiptCheckBox.Checked = settings.ExtProperties.ReadBool("CreateReceipt");
      if (this.SitesComboBox.SelectedItem == null && this.SitesComboBox.Items.Count > 0)
        this.SitesComboBox.SelectedIndex = 0;
      this.SitesComboBox.SelectedIndexChanged += new EventHandler(this.SitesComboBox_SelectedIndexChanged);
      if (name == "")
        this.SitesComboBox_SelectedIndexChanged((object) null, (EventArgs) null);
      settings.PubFilteredTypes = settings.ExtProperties.ReadList<int>("FTypes");
      settings.PubFilteredRelTypes = settings.ExtProperties.ReadList<int>("FRelTypes");
      this.autoPublishReplicationCheckBox.Checked = settings.ExtProperties.Ini.ReadBoolean("Props", "AutoPublishReplication", true);
      this.InitializeTaskPriority((TaskPriority) settings.ExtProperties.ReadInteger("RemoteTaskPriority", 0L));
      if (settings.Participants != null)
        settings.Participants.ParticipantsChanged += new ParticipantList.ModifyItems(this.Participants_ParticipantsChanged);
    }
    else
      flag = true;
    return flag;
  }

  private void Participants_ParticipantsChanged()
  {
    if (this.RemoteWaitCheckBox.Checked && !this._chekedRemoteWaitSaved)
      this._chekedRemoteWaitSaved = true;
    this.RemoteWaitCheckBox.Checked = this._settings.Participants.Count == 0 && this._chekedRemoteWaitSaved;
    this.RemoteWaitCheckBox.Visible = this._settings.Participants.Count == 0;
  }

  private void SitesComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.RemoteSchemesComboBox.Items.Clear();
    this.RemoteSchemesComboBox.Items.Add((object) LocalizationHolder.rm.GetString("EmptyMsg"));
    this.RemoteSchemesComboBox.SelectedIndex = 0;
    SiteInfo selectedItem = this.SitesComboBox.SelectedItem as SiteInfo;
    this.CreateReceiptCheckBox.Enabled = selectedItem != null && selectedItem.SystemType == SystemTypes.IPS;
    if (this.CreateReceiptCheckBox.Enabled || !this.CreateReceiptCheckBox.Checked)
      return;
    this.CreateReceiptCheckBox.Checked = false;
  }

  private void RemoteSchemesComboBox_DropDown(object sender, EventArgs e)
  {
    Cursor.Current = Cursors.WaitCursor;
    try
    {
      SiteInfo selectedItem = this.SitesComboBox.SelectedItem as SiteInfo;
      this.RemoteSchemesComboBox.Enabled = selectedItem != null;
      if (selectedItem == null)
        return;
      ProcessTemplateInfo[] processTemplateInfoArray = (ProcessTemplateInfo[]) null;
      try
      {
        processTemplateInfoArray = this._portalSrv.GetProcessTemplates(selectedItem.GUID);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(string.Format(LocalizationHolder.rm.GetString("Workflow.Design_194"), (object) selectedItem.ToString()) + ex.Message);
      }
      if (processTemplateInfoArray != null)
      {
        this.RemoteSchemesComboBox.Items.Clear();
        foreach (object obj in processTemplateInfoArray)
          this.RemoteSchemesComboBox.Items.Add(obj);
      }
      if (this.RemoteSchemesComboBox.Items.Count == 0)
        this.RemoteSchemesComboBox.Items.Add((object) LocalizationHolder.rm.GetString("EmptyMsg"));
      this.RemoteSchemesComboBox.SelectedIndex = 0;
    }
    finally
    {
      Cursor.Current = Cursors.Default;
    }
  }

  private void savePublishOptions_Click(object sender, EventArgs e)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ISaveDiskPublishOptionsDialogService)) is ISaveDiskPublishOptionsDialogService service))
    {
      int num = (int) MessageBox.Show("Сервис работы с настройками публикации не найден.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      string empty = string.Empty;
      if (this.SitesComboBox.SelectedItem is SiteInfo selectedItem)
        empty = selectedItem.GUID.ToString();
      int selectedIndex = this.PubCompositionCombo.SelectedIndex;
      int countLevels;
      switch (selectedIndex)
      {
        case 0:
          countLevels = -1;
          break;
        case 1:
          countLevels = 1;
          break;
        case 2:
          countLevels = 0;
          break;
        default:
          countLevels = selectedIndex - 1;
          break;
      }
      ExtendedPublishOptions publishOptions = new ExtendedPublishOptions(PublishCompositionOptions.None, countLevels, this._settings.PubFilteredRelTypes, this._settings.PubFilteredTypes, (FiltrationSettings) null);
      if (this.GiveOwnershipCheckBox.Checked)
        publishOptions.OwnerSite = new char?('Y');
      publishOptions.EnableSites = empty;
      publishOptions.TaskPriority = ((RemoteSubProcessSettingPageControl.PriorityItem) this.cbPriorityRemoteTask.SelectedItem).Value;
      publishOptions.AutoReplication = this.autoPublishReplicationCheckBox.Checked;
      service.SaveOptions(publishOptions, true);
    }
  }

  private void loadPublishOptions_Click(object sender, EventArgs e)
  {
    if (!(ApplicationServices.Container.GetService(typeof (ISaveDiskPublishOptionsDialogService)) is ISaveDiskPublishOptionsDialogService service))
    {
      int num1 = (int) MessageBox.Show("Сервис работы с настройками публикации не найден.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    else
    {
      ExtendedPublishOptions extendedPublishOptions = service.LoadOptions();
      if (extendedPublishOptions == null)
        return;
      if (!string.IsNullOrEmpty(extendedPublishOptions.EnableSites))
      {
        Guid guid = new Guid(extendedPublishOptions.EnableSites);
        foreach (SiteInfo siteInfo in this.SitesComboBox.Items)
        {
          if (siteInfo.GUID == guid)
          {
            this.SitesComboBox.SelectedItem = (object) siteInfo;
            break;
          }
        }
      }
      CheckBox ownershipCheckBox = this.GiveOwnershipCheckBox;
      char? ownerSite = extendedPublishOptions.OwnerSite;
      int? nullable = ownerSite.HasValue ? new int?((int) ownerSite.GetValueOrDefault()) : new int?();
      int num2 = 89;
      int num3 = nullable.GetValueOrDefault() == num2 & nullable.HasValue ? 1 : 0;
      ownershipCheckBox.Checked = num3 != 0;
      int countLevels = extendedPublishOptions.CountLevels;
      int num4;
      switch (countLevels)
      {
        case -1:
          num4 = 0;
          break;
        case 0:
          num4 = 2;
          break;
        case 1:
          num4 = 1;
          break;
        default:
          num4 = countLevels + 1;
          break;
      }
      this.PubCompositionCombo.SelectedIndex = num4;
      if (this.SitesComboBox.SelectedItem == null && this.SitesComboBox.Items.Count > 0)
        this.SitesComboBox.SelectedIndex = 0;
      this.SitesComboBox.SelectedIndexChanged += new EventHandler(this.SitesComboBox_SelectedIndexChanged);
      this.SitesComboBox_SelectedIndexChanged((object) null, (EventArgs) null);
      this._settings.PubFilteredTypes = extendedPublishOptions.EnableTypes;
      this._settings.PubFilteredRelTypes = extendedPublishOptions.EnableRelationTypes;
      this.InitializeTaskPriority(extendedPublishOptions.TaskPriority);
      this.autoPublishReplicationCheckBox.Checked = extendedPublishOptions.AutoReplication;
    }
  }

  private void PubObjectTypesButton_Click(object sender, EventArgs e)
  {
    using (ObjectTypesFilterForm objectTypesFilterForm = new ObjectTypesFilterForm())
    {
      if ((ApplicationServices.Container.GetService(typeof (IMServerService)) is IMServerService service ? service.GetCustomService(typeof (IPublishTypesConfiguration)) : (object) null) is IPublishTypesConfiguration customService)
        objectTypesFilterForm.LoadData(customService.PublishObjectTypes, (List<int>) null, this._settings.PubFilteredTypes);
      if (objectTypesFilterForm.ShowDialog() != DialogResult.OK)
        return;
      this._settings.PubFilteredTypes = objectTypesFilterForm.FilteredObjectTypes;
    }
  }

  private void PubRelTypesButton_Click(object sender, EventArgs e)
  {
    using (RelationTypesFilterForm relationTypesFilterForm = new RelationTypesFilterForm())
    {
      relationTypesFilterForm.LoadData(this._settings.PubFilteredRelTypes);
      if (relationTypesFilterForm.ShowDialog() != DialogResult.OK)
        return;
      this._settings.PubFilteredRelTypes = relationTypesFilterForm.FilteredRelationTypes;
    }
  }

  public bool Save(IDBObject activityToSave, bool modified)
  {
    string str1 = "";
    string str2 = "";
    string str3 = "";
    if (this.SitesComboBox.SelectedItem is SiteInfo selectedItem1)
    {
      str1 = selectedItem1.GUID.ToString();
      if (this.RemoteSchemesComboBox.SelectedItem is ProcessTemplateInfo selectedItem)
      {
        str2 = selectedItem.Name;
        str3 = selectedItem.Guid.ToString();
      }
    }
    if (this._settings.ExtProperties.Write("Site", str1, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    if (this._settings.ExtProperties.Write("TplName", str2, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    if (this._settings.ExtProperties.Write("TplGuid", str3, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    IDBAttribute attributeById = activityToSave.GetAttributeByID(wfConsts.AttrWaitForCompletionID);
    if (attributeById != null && attributeById.AsBoolean != this.RemoteWaitCheckBox.Checked)
    {
      modified = true;
      attributeById.AsBoolean = this.RemoteWaitCheckBox.Checked;
    }
    if (this.GiveOwnershipCheckBox.Checked != this._settings.ExtProperties.ReadBool("GiveOwnership"))
    {
      this._settings.ExtProperties.WriteBool("GiveOwnership", this.GiveOwnershipCheckBox.Checked, ExtPropertiesFlag.RemoteSubprocess);
      modified = true;
    }
    int selectedIndex = this.PubCompositionCombo.SelectedIndex;
    int num;
    switch (selectedIndex)
    {
      case 0:
        num = -1;
        break;
      case 1:
        num = 1;
        break;
      case 2:
        num = 0;
        break;
      default:
        num = selectedIndex - 1;
        break;
    }
    if (this._settings.ExtProperties.Write("MaxCompositionLevel", (long) num, ExtPropertiesFlag.RemoteSubprocess, "-1"))
      modified = true;
    if (this.CreateReceiptCheckBox.Checked != this._settings.ExtProperties.ReadBool("CreateReceipt"))
    {
      this._settings.ExtProperties.WriteBool("CreateReceipt", this.CreateReceiptCheckBox.Checked, ExtPropertiesFlag.RemoteSubprocess);
      modified = true;
    }
    if (this._settings.ExtProperties.WriteList<int>("FTypes", this._settings.PubFilteredTypes, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    if (this._settings.ExtProperties.WriteList<int>("FRelTypes", this._settings.PubFilteredRelTypes, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    if (this._settings.ExtProperties.Write("AutoPublishReplication", this.autoPublishReplicationCheckBox.Checked ? "1" : "0", ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    if (this._settings.ExtProperties.Write("RemoteTaskPriority", (long) ((RemoteSubProcessSettingPageControl.PriorityItem) this.cbPriorityRemoteTask.SelectedItem).Value, ExtPropertiesFlag.RemoteSubprocess))
      modified = true;
    return modified;
  }

  private void InitializeTaskPriority(TaskPriority priority = TaskPriority.Normal)
  {
    this.cbPriorityRemoteTask.Items.Clear();
    int num = 0;
    foreach (TaskPriority taskPriority in Enum.GetValues(typeof (TaskPriority)))
    {
      this.cbPriorityRemoteTask.Items.Add((object) new RemoteSubProcessSettingPageControl.PriorityItem(taskPriority));
      if (taskPriority.Equals((object) priority))
        num = this.cbPriorityRemoteTask.Items.Count - 1;
    }
    this.cbPriorityRemoteTask.SelectedIndex = num;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RemoteSubProcessSettingPageControl));
    this.RemoteProcess2GroupBox = new GroupBox();
    this.autoPublishReplicationCheckBox = new CheckBox();
    this.loadPublishOptions = new Button();
    this.savePublishOptions = new Button();
    this.CreateReceiptCheckBox = new CheckBox();
    this.label13 = new Label();
    this.PubCompositionCombo = new ComboBox();
    this.PubRelTypesButton = new Button();
    this.GiveOwnershipCheckBox = new CheckBox();
    this.PubObjectTypesButton = new Button();
    this.panel9 = new Panel();
    this.RemoteProcessGroupBox = new GroupBox();
    this.RemoteSchemesComboBox = new ComboBox();
    this.SitesComboBox = new ComboBox();
    this.label12 = new Label();
    this.label11 = new Label();
    this.RemoteWaitCheckBox = new CheckBox();
    this.PortalErrPanel = new Panel();
    this.PortalErrLabel = new AutoSizeLabel();
    this.PortalErrImage = new PictureBox();
    this.MiscIL = new ImageList(this.components);
    this.groupBox3 = new GroupBox();
    this.cbPriorityRemoteTask = new ComboBox();
    this.RemoteProcess2GroupBox.SuspendLayout();
    this.RemoteProcessGroupBox.SuspendLayout();
    this.PortalErrPanel.SuspendLayout();
    ((ISupportInitialize) this.PortalErrImage).BeginInit();
    this.groupBox3.SuspendLayout();
    this.SuspendLayout();
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.groupBox3);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.autoPublishReplicationCheckBox);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.loadPublishOptions);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.savePublishOptions);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.CreateReceiptCheckBox);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.label13);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.PubCompositionCombo);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.PubRelTypesButton);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.GiveOwnershipCheckBox);
    this.RemoteProcess2GroupBox.Controls.Add((Control) this.PubObjectTypesButton);
    this.RemoteProcess2GroupBox.Dock = DockStyle.Top;
    this.RemoteProcess2GroupBox.Location = new Point(0, 227);
    this.RemoteProcess2GroupBox.Name = "RemoteProcess2GroupBox";
    this.RemoteProcess2GroupBox.Size = new Size(696, 278);
    this.RemoteProcess2GroupBox.TabIndex = 11;
    this.RemoteProcess2GroupBox.TabStop = false;
    this.RemoteProcess2GroupBox.Text = "Настройки публикации";
    this.autoPublishReplicationCheckBox.AutoSize = true;
    this.autoPublishReplicationCheckBox.Checked = true;
    this.autoPublishReplicationCheckBox.CheckState = CheckState.Checked;
    this.autoPublishReplicationCheckBox.ImeMode = ImeMode.NoControl;
    this.autoPublishReplicationCheckBox.Location = new Point(286, 136);
    this.autoPublishReplicationCheckBox.Name = "autoPublishReplicationCheckBox";
    this.autoPublishReplicationCheckBox.Size = new Size(222, 21);
    this.autoPublishReplicationCheckBox.TabIndex = 18;
    this.autoPublishReplicationCheckBox.Text = "Автопубликация обновлений";
    this.autoPublishReplicationCheckBox.UseVisualStyleBackColor = true;
    this.loadPublishOptions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.loadPublishOptions.ImeMode = ImeMode.NoControl;
    this.loadPublishOptions.Location = new Point(581, 132);
    this.loadPublishOptions.Name = "loadPublishOptions";
    this.loadPublishOptions.Size = new Size(103, 27);
    this.loadPublishOptions.TabIndex = 17;
    this.loadPublishOptions.Text = "Загрузить";
    this.loadPublishOptions.UseVisualStyleBackColor = true;
    this.loadPublishOptions.Click += new EventHandler(this.loadPublishOptions_Click);
    this.savePublishOptions.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.savePublishOptions.ImeMode = ImeMode.NoControl;
    this.savePublishOptions.Location = new Point(581, 90);
    this.savePublishOptions.Name = "savePublishOptions";
    this.savePublishOptions.Size = new Size(103, 27);
    this.savePublishOptions.TabIndex = 16 /*0x10*/;
    this.savePublishOptions.Text = "Сохранить";
    this.savePublishOptions.UseVisualStyleBackColor = true;
    this.savePublishOptions.Click += new EventHandler(this.savePublishOptions_Click);
    this.CreateReceiptCheckBox.AutoSize = true;
    this.CreateReceiptCheckBox.ImeMode = ImeMode.NoControl;
    this.CreateReceiptCheckBox.Location = new Point(11, 163);
    this.CreateReceiptCheckBox.Name = "CreateReceiptCheckBox";
    this.CreateReceiptCheckBox.Size = new Size(196, 21);
    this.CreateReceiptCheckBox.TabIndex = 13;
    this.CreateReceiptCheckBox.Text = "Формировать квитанцию";
    this.label13.AutoSize = true;
    this.label13.ImeMode = ImeMode.NoControl;
    this.label13.Location = new Point(7, 27);
    this.label13.Name = "label13";
    this.label13.Size = new Size(148, 17);
    this.label13.TabIndex = 12;
    this.label13.Text = "Публикация состава:";
    this.PubCompositionCombo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.PubCompositionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
    this.PubCompositionCombo.FormattingEnabled = true;
    this.PubCompositionCombo.Items.AddRange(new object[3]
    {
      (object) "Полный состав",
      (object) "Первый уровень",
      (object) "Без состава"
    });
    this.PubCompositionCombo.Location = new Point(11, 48 /*0x30*/);
    this.PubCompositionCombo.Name = "PubCompositionCombo";
    this.PubCompositionCombo.Size = new Size(673, 24);
    this.PubCompositionCombo.TabIndex = 1;
    this.PubRelTypesButton.ImeMode = ImeMode.NoControl;
    this.PubRelTypesButton.Location = new Point(150, 90);
    this.PubRelTypesButton.Name = "PubRelTypesButton";
    this.PubRelTypesButton.Size = new Size(132, 27);
    this.PubRelTypesButton.TabIndex = 10;
    this.PubRelTypesButton.Text = "Типы связей...";
    this.PubRelTypesButton.UseVisualStyleBackColor = true;
    this.PubRelTypesButton.Click += new EventHandler(this.PubRelTypesButton_Click);
    this.GiveOwnershipCheckBox.AutoSize = true;
    this.GiveOwnershipCheckBox.ImeMode = ImeMode.NoControl;
    this.GiveOwnershipCheckBox.Location = new Point(11, 136);
    this.GiveOwnershipCheckBox.Name = "GiveOwnershipCheckBox";
    this.GiveOwnershipCheckBox.Size = new Size(219, 21);
    this.GiveOwnershipCheckBox.TabIndex = 2;
    this.GiveOwnershipCheckBox.Text = "Передавать права владения";
    this.PubObjectTypesButton.ImeMode = ImeMode.NoControl;
    this.PubObjectTypesButton.Location = new Point(11, 90);
    this.PubObjectTypesButton.Name = "PubObjectTypesButton";
    this.PubObjectTypesButton.Size = new Size(132, 27);
    this.PubObjectTypesButton.TabIndex = 9;
    this.PubObjectTypesButton.Text = "Типы объектов...";
    this.PubObjectTypesButton.UseVisualStyleBackColor = true;
    this.PubObjectTypesButton.Click += new EventHandler(this.PubObjectTypesButton_Click);
    this.panel9.Dock = DockStyle.Top;
    this.panel9.Location = new Point(0, 215);
    this.panel9.Name = "panel9";
    this.panel9.Size = new Size(696, 12);
    this.panel9.TabIndex = 12;
    this.RemoteProcessGroupBox.Controls.Add((Control) this.RemoteSchemesComboBox);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.SitesComboBox);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.label12);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.label11);
    this.RemoteProcessGroupBox.Controls.Add((Control) this.RemoteWaitCheckBox);
    this.RemoteProcessGroupBox.Dock = DockStyle.Top;
    this.RemoteProcessGroupBox.Location = new Point(0, 45);
    this.RemoteProcessGroupBox.Name = "RemoteProcessGroupBox";
    this.RemoteProcessGroupBox.Size = new Size(696, 170);
    this.RemoteProcessGroupBox.TabIndex = 9;
    this.RemoteProcessGroupBox.TabStop = false;
    this.RemoteSchemesComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.RemoteSchemesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.RemoteSchemesComboBox.FormattingEnabled = true;
    this.RemoteSchemesComboBox.Location = new Point(11, 96 /*0x60*/);
    this.RemoteSchemesComboBox.Name = "RemoteSchemesComboBox";
    this.RemoteSchemesComboBox.Size = new Size(673, 24);
    this.RemoteSchemesComboBox.TabIndex = 8;
    this.SitesComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.SitesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.SitesComboBox.FormattingEnabled = true;
    this.SitesComboBox.Location = new Point(11, 40);
    this.SitesComboBox.Name = "SitesComboBox";
    this.SitesComboBox.Size = new Size(673, 24);
    this.SitesComboBox.TabIndex = 7;
    this.label12.AutoSize = true;
    this.label12.ImeMode = ImeMode.NoControl;
    this.label12.Location = new Point(7, 18);
    this.label12.Name = "label12";
    this.label12.Size = new Size(192 /*0xC0*/, 17);
    this.label12.TabIndex = 6;
    this.label12.Text = "Запустить процесс на узле:";
    this.label11.AutoSize = true;
    this.label11.ImeMode = ImeMode.NoControl;
    this.label11.Location = new Point(7, 73);
    this.label11.Name = "label11";
    this.label11.Size = new Size(220, 17);
    this.label11.TabIndex = 0;
    this.label11.Text = "Запустить процесс по шаблону:";
    this.RemoteWaitCheckBox.AutoSize = true;
    this.RemoteWaitCheckBox.ImeMode = ImeMode.NoControl;
    this.RemoteWaitCheckBox.Location = new Point(11, 135);
    this.RemoteWaitCheckBox.Name = "RemoteWaitCheckBox";
    this.RemoteWaitCheckBox.Size = new Size(158, 21);
    this.RemoteWaitCheckBox.TabIndex = 3;
    this.RemoteWaitCheckBox.Text = "Ждать завершения";
    this.PortalErrPanel.BackColor = SystemColors.Info;
    this.PortalErrPanel.Controls.Add((Control) this.PortalErrLabel);
    this.PortalErrPanel.Controls.Add((Control) this.PortalErrImage);
    this.PortalErrPanel.Dock = DockStyle.Top;
    this.PortalErrPanel.Location = new Point(0, 0);
    this.PortalErrPanel.Name = "PortalErrPanel";
    this.PortalErrPanel.Padding = new Padding(7);
    this.PortalErrPanel.Size = new Size(696, 45);
    this.PortalErrPanel.TabIndex = 10;
    this.PortalErrPanel.Visible = false;
    this.PortalErrLabel.Dock = DockStyle.Top;
    this.PortalErrLabel.ImeMode = ImeMode.NoControl;
    this.PortalErrLabel.Location = new Point(23, 7);
    this.PortalErrLabel.Name = "PortalErrLabel";
    this.PortalErrLabel.Padding = new Padding(5, 0, 0, 0);
    this.PortalErrLabel.Size = new Size(666, 19);
    this.PortalErrLabel.TabIndex = 15;
    this.PortalErrLabel.Text = "Служба портала не инициализирована, настройка действия невозможна.";
    this.PortalErrLabel.TextAlign = ContentAlignment.MiddleLeft;
    this.PortalErrImage.BackColor = Color.Transparent;
    this.PortalErrImage.Dock = DockStyle.Left;
    this.PortalErrImage.ImeMode = ImeMode.NoControl;
    this.PortalErrImage.Location = new Point(7, 7);
    this.PortalErrImage.Name = "PortalErrImage";
    this.PortalErrImage.Size = new Size(16 /*0x10*/, 31 /*0x1F*/);
    this.PortalErrImage.SizeMode = PictureBoxSizeMode.AutoSize;
    this.PortalErrImage.TabIndex = 14;
    this.PortalErrImage.TabStop = false;
    this.MiscIL.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("MiscIL.ImageStream");
    this.MiscIL.TransparentColor = Color.Fuchsia;
    this.MiscIL.Images.SetKeyName(0, "");
    this.MiscIL.Images.SetKeyName(1, "");
    this.MiscIL.Images.SetKeyName(2, "");
    this.MiscIL.Images.SetKeyName(3, "");
    this.MiscIL.Images.SetKeyName(4, "");
    this.MiscIL.Images.SetKeyName(5, "");
    this.MiscIL.Images.SetKeyName(6, "");
    this.MiscIL.Images.SetKeyName(7, "");
    this.MiscIL.Images.SetKeyName(8, "");
    this.MiscIL.Images.SetKeyName(9, "");
    this.MiscIL.Images.SetKeyName(10, "abort16x16.bmp");
    this.groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.groupBox3.Controls.Add((Control) this.cbPriorityRemoteTask);
    this.groupBox3.Location = new Point(10, 191);
    this.groupBox3.Margin = new Padding(4);
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.Padding = new Padding(4);
    this.groupBox3.Size = new Size(301, 68);
    this.groupBox3.TabIndex = 19;
    this.groupBox3.TabStop = false;
    this.groupBox3.Text = "Приоритет задачи";
    this.cbPriorityRemoteTask.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbPriorityRemoteTask.FormattingEnabled = true;
    this.cbPriorityRemoteTask.Items.AddRange(new object[3]
    {
      (object) "Низкий",
      (object) "Обычный",
      (object) "Высокий"
    });
    this.cbPriorityRemoteTask.Location = new Point(23, 26);
    this.cbPriorityRemoteTask.Margin = new Padding(4);
    this.cbPriorityRemoteTask.Name = "cbPriorityRemoteTask";
    this.cbPriorityRemoteTask.Size = new Size(252, 24);
    this.cbPriorityRemoteTask.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(120f, 120f);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.BackColor = SystemColors.ControlLightLight;
    this.Controls.Add((Control) this.RemoteProcess2GroupBox);
    this.Controls.Add((Control) this.panel9);
    this.Controls.Add((Control) this.RemoteProcessGroupBox);
    this.Controls.Add((Control) this.PortalErrPanel);
    this.Name = nameof (RemoteSubProcessSettingPageControl);
    this.Size = new Size(696, 516);
    this.RemoteProcess2GroupBox.ResumeLayout(false);
    this.RemoteProcess2GroupBox.PerformLayout();
    this.RemoteProcessGroupBox.ResumeLayout(false);
    this.RemoteProcessGroupBox.PerformLayout();
    this.PortalErrPanel.ResumeLayout(false);
    this.PortalErrPanel.PerformLayout();
    ((ISupportInitialize) this.PortalErrImage).EndInit();
    this.groupBox3.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class PriorityItem
  {
    public TaskPriority Value { get; private set; }

    public PriorityItem(TaskPriority value) => this.Value = value;

    public override string ToString() => EnumDescConverter.GetEnumDescription((Enum) this.Value);
  }
}
