// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailSettingsForm
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using DevExpress.IM.XtraEditors;
using DevExpress.IM.XtraEditors.Controls;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client;

public class MailSettingsForm : FormEx
{
  private Dictionary<ProcessPriority, CheckBox> PriorityChecks = new Dictionary<ProcessPriority, CheckBox>();
  private MediaPlayer _player;
  private bool _changing;
  private IContainer components;
  private Panel Panel2;
  private Button CancButton;
  private Button OkButton;
  private GroupBox groupBox1;
  private GroupBox groupBox2;
  private CheckBox clearTrashCheckBox;
  private CheckBox warnCheckBox;
  private Label label1;
  private CheckBox refreshCheckBox;
  private CheckBox highCheckBox;
  private CheckBox normCheckBox;
  private CheckBox lowCheckBox;
  private Label label2;
  private NumericUpDown refreshUpDown;
  private Label label4;
  private Label label3;
  private NumericUpDown markUpDown;
  private GroupBox groupBox3;
  private CheckBox showTabsCheckBox;
  private GroupBox groupBox4;
  private CheckBox PlaySoundCheckBox;
  private ButtonEdit SoundFileNameBox;
  private Button PlaySoundButton;
  private ImageList SndImageList;
  private Timer SndTimer;
  private GroupBox ConfirmGroupBox;
  private CheckBox ConfirmNextCheckBox;
  private CheckBox ConfirmBackCheckBox;
  private CheckBox disableAllNotify;

  public MailSettingsForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 828);
    this.PriorityChecks.Add(ProcessPriority.Low, this.lowCheckBox);
    this.PriorityChecks.Add(ProcessPriority.Normal, this.normCheckBox);
    this.PriorityChecks.Add(ProcessPriority.High, this.highCheckBox);
    this._player = new MediaPlayer();
  }

  public void GetProperties(MailSettings settings)
  {
    this.refreshCheckBox.Checked = settings.RefreshInterval > 0;
    this.refreshUpDown.Value = (Decimal) Math.Abs(settings.RefreshInterval);
    this.markUpDown.Value = (Decimal) settings.MarkReadInterval;
    this.warnCheckBox.Checked = settings.WarnOnDeletion;
    this.clearTrashCheckBox.Checked = settings.ClearTrashOnExit;
    this.Priority = settings.NotifyPriority;
    this.ConfirmBackCheckBox.Checked = settings.ConfirmSendBack;
    this.ConfirmNextCheckBox.Checked = settings.ConfirmSendNext;
    this.showTabsCheckBox.Checked = settings.ShowTabs;
    this.PlaySoundCheckBox.Checked = settings.SoundFileName != "";
    this.PlaySoundCheckBox_CheckedChanged((object) null, (EventArgs) null);
    this.SoundFileNameBox.Text = settings.SoundFileName;
    this.disableAllNotify.Checked = settings.DisableAllNotify;
  }

  public void SetProperties(MailSettings settings)
  {
    settings.RefreshInterval = Convert.ToInt32(this.refreshUpDown.Value);
    if (!this.refreshCheckBox.Checked)
      settings.RefreshInterval = -settings.RefreshInterval;
    settings.MarkReadInterval = Convert.ToInt32(this.markUpDown.Value);
    settings.WarnOnDeletion = this.warnCheckBox.Checked;
    settings.ClearTrashOnExit = this.clearTrashCheckBox.Checked;
    settings.NotifyPriority = this.Priority;
    settings.ConfirmSendBack = this.ConfirmBackCheckBox.Checked;
    settings.ConfirmSendNext = this.ConfirmNextCheckBox.Checked;
    settings.ShowTabs = this.showTabsCheckBox.Checked;
    settings.SoundFileName = this.SoundFileNameBox.Text;
    settings.DisableAllNotify = this.disableAllNotify.Checked;
    settings.Save();
  }

  public ProcessPriority Priority
  {
    get
    {
      foreach (KeyValuePair<ProcessPriority, CheckBox> priorityCheck in this.PriorityChecks)
      {
        if (priorityCheck.Value.Checked)
          return priorityCheck.Key;
      }
      return ProcessPriority.Unreal;
    }
    set
    {
      this._changing = true;
      try
      {
        for (ProcessPriority key = ProcessPriority.High; key >= ProcessPriority.Low; --key)
        {
          KeyValuePair<ProcessPriority, CheckBox> keyValuePair = new KeyValuePair<ProcessPriority, CheckBox>(key, this.PriorityChecks[key]);
          if (value == ProcessPriority.Unreal)
            keyValuePair.Value.Checked = false;
          else if (keyValuePair.Key >= value)
            keyValuePair.Value.Checked = true;
        }
      }
      finally
      {
        this._changing = false;
      }
    }
  }

  private void UpdatePriorityChecks(ProcessPriority priority)
  {
  }

  public static bool EditSettings()
  {
    using (MailSettingsForm mailSettingsForm = new MailSettingsForm())
    {
      mailSettingsForm.GetProperties(MailSettings.Cfg);
      if (mailSettingsForm.ShowDialog() != DialogResult.OK)
        return false;
      mailSettingsForm.SetProperties(MailSettings.Cfg);
      if (ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service)
        service.StartListener();
      return true;
    }
  }

  private void PriorityChecksChanged(object sender, EventArgs e)
  {
    if (this._changing)
      return;
    this._changing = true;
    try
    {
      CheckBox checkBox = sender as CheckBox;
      if (!checkBox.Checked)
      {
        ProcessPriority processPriority = ProcessPriority.Unreal;
        foreach (KeyValuePair<ProcessPriority, CheckBox> priorityCheck in this.PriorityChecks)
        {
          if (priorityCheck.Value == checkBox)
          {
            processPriority = priorityCheck.Key;
            break;
          }
        }
        for (ProcessPriority key = processPriority - 1; key >= ProcessPriority.Low; --key)
          this.PriorityChecks[key].Checked = false;
      }
      this.Priority = this.Priority;
    }
    finally
    {
      this._changing = false;
    }
  }

  private void PlaySoundCheckBox_CheckedChanged(object sender, EventArgs e)
  {
    this.SoundFileNameBox.Enabled = this.PlaySoundCheckBox.Checked;
    if (!this.PlaySoundCheckBox.Checked)
      this.SoundFileNameBox.Text = "";
    this.PlaySoundButton.Enabled = this.PlaySoundCheckBox.Checked;
  }

  private bool IsSoundPlaying() => this._player.ModeString == "playing";

  private void PlaySoundButton_Click(object sender, EventArgs e)
  {
    bool flag = this.IsSoundPlaying();
    this.PlaySoundButton.ImageIndex = Convert.ToInt32(!flag);
    if (flag)
    {
      this._player.Close();
    }
    else
    {
      this._player.Play(this.SoundFileNameBox.Text);
      this.SndTimer.Start();
    }
  }

  private void SndTimer_Tick(object sender, EventArgs e)
  {
    bool flag = this.IsSoundPlaying();
    this.PlaySoundButton.ImageIndex = Convert.ToInt32(flag);
    if (flag)
      return;
    this.SndTimer.Stop();
  }

  private void SoundFileNameBox_ButtonClick(object sender, ButtonPressedEventArgs e)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.FileName = this.SoundFileNameBox.Text;
      openFileDialog.Filter = "(*.wav;*.mp3)|*.wav;*.mp3|(*.*)|*.*";
      openFileDialog.RestoreDirectory = true;
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      this.SoundFileNameBox.Text = openFileDialog.FileName;
    }
  }

  private void MailSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    this._player.Close();
  }

  protected override void OnKeyDown(KeyEventArgs e)
  {
    base.OnKeyDown(e);
    if (!e.Shift || !e.Control || !e.Alt || e.KeyCode != Keys.I || !(ApplicationServices.Container.GetService(typeof (ICheckMailService)) is ICheckMailService service))
      return;
    service.ShowDebug();
  }

  private void disableAllNotify_CheckedChanged(object sender, EventArgs e)
  {
    this.lowCheckBox.Enabled = !this.disableAllNotify.Checked;
    this.normCheckBox.Enabled = !this.disableAllNotify.Checked;
    this.highCheckBox.Enabled = !this.disableAllNotify.Checked;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MailSettingsForm));
    this.Panel2 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.groupBox1 = new GroupBox();
    this.label4 = new Label();
    this.label3 = new Label();
    this.markUpDown = new NumericUpDown();
    this.refreshUpDown = new NumericUpDown();
    this.clearTrashCheckBox = new CheckBox();
    this.warnCheckBox = new CheckBox();
    this.label1 = new Label();
    this.refreshCheckBox = new CheckBox();
    this.groupBox2 = new GroupBox();
    this.normCheckBox = new CheckBox();
    this.lowCheckBox = new CheckBox();
    this.label2 = new Label();
    this.highCheckBox = new CheckBox();
    this.groupBox3 = new GroupBox();
    this.showTabsCheckBox = new CheckBox();
    this.groupBox4 = new GroupBox();
    this.PlaySoundButton = new Button();
    this.SndImageList = new ImageList(this.components);
    this.SoundFileNameBox = new ButtonEdit();
    this.PlaySoundCheckBox = new CheckBox();
    this.SndTimer = new Timer(this.components);
    this.ConfirmGroupBox = new GroupBox();
    this.ConfirmNextCheckBox = new CheckBox();
    this.ConfirmBackCheckBox = new CheckBox();
    this.disableAllNotify = new CheckBox();
    this.Panel2.SuspendLayout();
    this.groupBox1.SuspendLayout();
    this.markUpDown.BeginInit();
    this.refreshUpDown.BeginInit();
    this.groupBox2.SuspendLayout();
    this.groupBox3.SuspendLayout();
    this.groupBox4.SuspendLayout();
    this.SoundFileNameBox.Properties.BeginInit();
    this.ConfirmGroupBox.SuspendLayout();
    this.SuspendLayout();
    this.Panel2.Controls.Add((Control) this.CancButton);
    this.Panel2.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.Panel2, "Panel2");
    this.Panel2.Name = "Panel2";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.groupBox1.Controls.Add((Control) this.label4);
    this.groupBox1.Controls.Add((Control) this.label3);
    this.groupBox1.Controls.Add((Control) this.markUpDown);
    this.groupBox1.Controls.Add((Control) this.refreshUpDown);
    this.groupBox1.Controls.Add((Control) this.clearTrashCheckBox);
    this.groupBox1.Controls.Add((Control) this.warnCheckBox);
    this.groupBox1.Controls.Add((Control) this.label1);
    this.groupBox1.Controls.Add((Control) this.refreshCheckBox);
    componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.TabStop = false;
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.markUpDown, "markUpDown");
    this.markUpDown.Name = "markUpDown";
    componentResourceManager.ApplyResources((object) this.refreshUpDown, "refreshUpDown");
    this.refreshUpDown.Name = "refreshUpDown";
    componentResourceManager.ApplyResources((object) this.clearTrashCheckBox, "clearTrashCheckBox");
    this.clearTrashCheckBox.Name = "clearTrashCheckBox";
    this.clearTrashCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.warnCheckBox, "warnCheckBox");
    this.warnCheckBox.Name = "warnCheckBox";
    this.warnCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.refreshCheckBox, "refreshCheckBox");
    this.refreshCheckBox.Name = "refreshCheckBox";
    this.refreshCheckBox.UseVisualStyleBackColor = true;
    this.groupBox2.Controls.Add((Control) this.disableAllNotify);
    this.groupBox2.Controls.Add((Control) this.normCheckBox);
    this.groupBox2.Controls.Add((Control) this.lowCheckBox);
    this.groupBox2.Controls.Add((Control) this.label2);
    this.groupBox2.Controls.Add((Control) this.highCheckBox);
    componentResourceManager.ApplyResources((object) this.groupBox2, "groupBox2");
    this.groupBox2.Name = "groupBox2";
    this.groupBox2.TabStop = false;
    componentResourceManager.ApplyResources((object) this.normCheckBox, "normCheckBox");
    this.normCheckBox.Name = "normCheckBox";
    this.normCheckBox.UseVisualStyleBackColor = true;
    this.normCheckBox.CheckedChanged += new EventHandler(this.PriorityChecksChanged);
    componentResourceManager.ApplyResources((object) this.lowCheckBox, "lowCheckBox");
    this.lowCheckBox.Name = "lowCheckBox";
    this.lowCheckBox.UseVisualStyleBackColor = true;
    this.lowCheckBox.CheckedChanged += new EventHandler(this.PriorityChecksChanged);
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.highCheckBox, "highCheckBox");
    this.highCheckBox.Name = "highCheckBox";
    this.highCheckBox.UseVisualStyleBackColor = true;
    this.highCheckBox.CheckedChanged += new EventHandler(this.PriorityChecksChanged);
    this.groupBox3.Controls.Add((Control) this.showTabsCheckBox);
    componentResourceManager.ApplyResources((object) this.groupBox3, "groupBox3");
    this.groupBox3.Name = "groupBox3";
    this.groupBox3.TabStop = false;
    componentResourceManager.ApplyResources((object) this.showTabsCheckBox, "showTabsCheckBox");
    this.showTabsCheckBox.Name = "showTabsCheckBox";
    this.showTabsCheckBox.UseVisualStyleBackColor = true;
    this.groupBox4.Controls.Add((Control) this.PlaySoundButton);
    this.groupBox4.Controls.Add((Control) this.SoundFileNameBox);
    this.groupBox4.Controls.Add((Control) this.PlaySoundCheckBox);
    componentResourceManager.ApplyResources((object) this.groupBox4, "groupBox4");
    this.groupBox4.Name = "groupBox4";
    this.groupBox4.TabStop = false;
    componentResourceManager.ApplyResources((object) this.PlaySoundButton, "PlaySoundButton");
    this.PlaySoundButton.ImageList = this.SndImageList;
    this.PlaySoundButton.Name = "PlaySoundButton";
    this.PlaySoundButton.UseVisualStyleBackColor = true;
    this.PlaySoundButton.Click += new EventHandler(this.PlaySoundButton_Click);
    this.SndImageList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("SndImageList.ImageStream");
    this.SndImageList.TransparentColor = Color.Fuchsia;
    this.SndImageList.Images.SetKeyName(0, "");
    this.SndImageList.Images.SetKeyName(1, "");
    componentResourceManager.ApplyResources((object) this.SoundFileNameBox, "SoundFileNameBox");
    this.SoundFileNameBox.Name = "SoundFileNameBox";
    this.SoundFileNameBox.Properties.Buttons.AddRange(new EditorButton[1]
    {
      new EditorButton()
    });
    this.SoundFileNameBox.ButtonClick += new ButtonPressedEventHandler(this.SoundFileNameBox_ButtonClick);
    componentResourceManager.ApplyResources((object) this.PlaySoundCheckBox, "PlaySoundCheckBox");
    this.PlaySoundCheckBox.Name = "PlaySoundCheckBox";
    this.PlaySoundCheckBox.UseVisualStyleBackColor = true;
    this.PlaySoundCheckBox.CheckedChanged += new EventHandler(this.PlaySoundCheckBox_CheckedChanged);
    this.SndTimer.Interval = 500;
    this.SndTimer.Tick += new EventHandler(this.SndTimer_Tick);
    this.ConfirmGroupBox.Controls.Add((Control) this.ConfirmNextCheckBox);
    this.ConfirmGroupBox.Controls.Add((Control) this.ConfirmBackCheckBox);
    componentResourceManager.ApplyResources((object) this.ConfirmGroupBox, "ConfirmGroupBox");
    this.ConfirmGroupBox.Name = "ConfirmGroupBox";
    this.ConfirmGroupBox.TabStop = false;
    componentResourceManager.ApplyResources((object) this.ConfirmNextCheckBox, "ConfirmNextCheckBox");
    this.ConfirmNextCheckBox.Name = "ConfirmNextCheckBox";
    this.ConfirmNextCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.ConfirmBackCheckBox, "ConfirmBackCheckBox");
    this.ConfirmBackCheckBox.Name = "ConfirmBackCheckBox";
    this.ConfirmBackCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.disableAllNotify, "disableAllNotify");
    this.disableAllNotify.Name = "disableAllNotify";
    this.disableAllNotify.UseVisualStyleBackColor = true;
    this.disableAllNotify.CheckedChanged += new EventHandler(this.disableAllNotify_CheckedChanged);
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.groupBox3);
    this.Controls.Add((Control) this.groupBox4);
    this.Controls.Add((Control) this.ConfirmGroupBox);
    this.Controls.Add((Control) this.groupBox2);
    this.Controls.Add((Control) this.groupBox1);
    this.Controls.Add((Control) this.Panel2);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.HelpButton = true;
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (MailSettingsForm);
    this.ShowInTaskbar = false;
    this.Tag = (object) "  ";
    this.FormClosed += new FormClosedEventHandler(this.MailSettingsForm_FormClosed);
    this.Panel2.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    this.markUpDown.EndInit();
    this.refreshUpDown.EndInit();
    this.groupBox2.ResumeLayout(false);
    this.groupBox2.PerformLayout();
    this.groupBox3.ResumeLayout(false);
    this.groupBox3.PerformLayout();
    this.groupBox4.ResumeLayout(false);
    this.groupBox4.PerformLayout();
    this.SoundFileNameBox.Properties.EndInit();
    this.ConfirmGroupBox.ResumeLayout(false);
    this.ConfirmGroupBox.PerformLayout();
    this.ResumeLayout(false);
  }
}
