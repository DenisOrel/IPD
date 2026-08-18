// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ComposeMessageForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Summary description for ComposeMessageForm.</summary>
public class ComposeMessageForm : FormEx
{
  private Panel panel1;
  private Panel panel2;
  private Label label3;
  private Button usersButton;
  private TextBox messageToEdit;
  private TextBox subjectEdit;
  private Panel timePanel;
  private Button chooseTimeButton;
  private TextBox timeEdit;
  private ImageList imageList1;
  private IContainer components;
  private TextBox textEdit;
  private Panel panel3;
  private Button CancButton;
  private Button OkButton;
  private Notification _notif;
  private ParticipantList _recips;
  public long ProcessID;

  public Notification Notification
  {
    get => this._notif;
    set
    {
      this._notif = value;
      this.subjectEdit.Text = this._notif.Subject;
      this.textEdit.Text = this._notif.Text;
      if (this._notif is PeriodNotification)
      {
        this.timePanel.Visible = true;
        this.UpdatePeriodText();
      }
      this._recips = this._notif.Recips;
      if (this._recips == null)
        return;
      this.messageToEdit.Text = this._notif.Recips.ToUserString();
    }
  }

  public ComposeMessageForm() => this.InitializeComponent();

  public bool ReadOnly
  {
    get => this.textEdit.ReadOnly;
    set
    {
      this.usersButton.Enabled = !value;
      this.chooseTimeButton.Enabled = !value;
      this.subjectEdit.ReadOnly = value;
      this.textEdit.ReadOnly = value;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ComposeMessageForm));
    this.panel1 = new Panel();
    this.textEdit = new TextBox();
    this.panel3 = new Panel();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.timePanel = new Panel();
    this.chooseTimeButton = new Button();
    this.timeEdit = new TextBox();
    this.panel2 = new Panel();
    this.label3 = new Label();
    this.usersButton = new Button();
    this.messageToEdit = new TextBox();
    this.subjectEdit = new TextBox();
    this.imageList1 = new ImageList(this.components);
    this.panel1.SuspendLayout();
    this.panel3.SuspendLayout();
    this.timePanel.SuspendLayout();
    this.panel2.SuspendLayout();
    this.SuspendLayout();
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.textEdit);
    this.panel1.Controls.Add((Control) this.panel3);
    this.panel1.Controls.Add((Control) this.timePanel);
    this.panel1.Controls.Add((Control) this.panel2);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.textEdit.AcceptsReturn = true;
    componentResourceManager.ApplyResources((object) this.textEdit, "textEdit");
    this.textEdit.Name = "textEdit";
    this.panel3.Controls.Add((Control) this.CancButton);
    this.panel3.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.timePanel.Controls.Add((Control) this.chooseTimeButton);
    this.timePanel.Controls.Add((Control) this.timeEdit);
    componentResourceManager.ApplyResources((object) this.timePanel, "timePanel");
    this.timePanel.Name = "timePanel";
    componentResourceManager.ApplyResources((object) this.chooseTimeButton, "chooseTimeButton");
    this.chooseTimeButton.Name = "chooseTimeButton";
    this.chooseTimeButton.Click += new EventHandler(this.ChooseTimeButtonClick);
    componentResourceManager.ApplyResources((object) this.timeEdit, "timeEdit");
    this.timeEdit.Name = "timeEdit";
    this.timeEdit.ReadOnly = true;
    this.timeEdit.TabStop = false;
    this.panel2.Controls.Add((Control) this.label3);
    this.panel2.Controls.Add((Control) this.usersButton);
    this.panel2.Controls.Add((Control) this.messageToEdit);
    this.panel2.Controls.Add((Control) this.subjectEdit);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.usersButton, "usersButton");
    this.usersButton.Name = "usersButton";
    this.usersButton.Click += new EventHandler(this.UsersButtonClick);
    componentResourceManager.ApplyResources((object) this.messageToEdit, "messageToEdit");
    this.messageToEdit.Name = "messageToEdit";
    this.messageToEdit.ReadOnly = true;
    this.messageToEdit.TabStop = false;
    componentResourceManager.ApplyResources((object) this.subjectEdit, "subjectEdit");
    this.subjectEdit.Name = "subjectEdit";
    this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
    this.imageList1.TransparentColor = Color.Transparent;
    this.imageList1.Images.SetKeyName(0, "");
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.panel1);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ComposeMessageForm);
    this.ShowInTaskbar = false;
    this.Closed += new EventHandler(this.ComposeMessageForm_Closed);
    this.Closing += new CancelEventHandler(this.ComposeMessageForm_Closing);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.panel3.ResumeLayout(false);
    this.timePanel.ResumeLayout(false);
    this.timePanel.PerformLayout();
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.ResumeLayout(false);
  }

  private void UsersButtonClick(object sender, EventArgs e)
  {
    if (!wfFunx.BrowseForUsers(this._recips, this.ProcessID))
      return;
    this.messageToEdit.Text = this._recips.ToUserString();
    this._notif.Modified = true;
  }

  private void UpdatePeriodText()
  {
    this.timeEdit.Text = (this._notif as PeriodNotification).Period.PeriodText;
  }

  private void ChooseTimeButtonClick(object sender, EventArgs e)
  {
    using (TimePeriodForm timePeriodForm = new TimePeriodForm(this.ProcessID))
    {
      PeriodInformation period = (this._notif as PeriodNotification).Period;
      if (!timePeriodForm.EditPeriod(ref period, (IUserSession) null))
        return;
      this.timeEdit.Text = period.PeriodText;
    }
  }

  private void ComposeMessageForm_Closed(object sender, EventArgs e)
  {
  }

  private void ComposeMessageForm_Closing(object sender, CancelEventArgs e)
  {
    if (this._notif == null)
      return;
    if (this.ReadOnly)
      this.DialogResult = DialogResult.Cancel;
    if (this.DialogResult != DialogResult.OK)
      return;
    string s = "";
    if (this.messageToEdit.Text.Trim() == "")
      MiscFunx.AddNewLined(ref s, LocalizationHolder.rm.GetString("Workflow.Design_28"));
    if (this.subjectEdit.Text.Trim() == "")
      MiscFunx.AddNewLined(ref s, LocalizationHolder.rm.GetString("Workflow.Design_29"));
    if (this.textEdit.Text.Trim() == "")
      MiscFunx.AddNewLined(ref s, LocalizationHolder.rm.GetString("Workflow.Design_30"));
    if (s != "")
      throw new ClientException(s);
    this._notif.Recips = this._recips;
    this._notif.Subject = this.subjectEdit.Text;
    this._notif.Text = this.textEdit.Text;
  }
}
