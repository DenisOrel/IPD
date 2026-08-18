// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Email.EmailView_test
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Client.Email;

public class EmailView_test : UserControl, IView
{
  private IContainer components;
  private Button button1;
  private TextBox tbFrom;
  private Label label1;
  private Label label2;
  private TextBox tbTo;
  private Label label3;
  private TextBox tbSubject;
  private TextBox tbMessage;
  private Label label4;
  private TextBox tbFileName;
  private Button button2;
  private OpenFileDialog openFileDialog1;
  private TabControl tabControl1;
  private TabPage tabPage1;
  private Panel panel1;
  private TabPage tabPage2;
  private SplitContainer splitContainer1;
  private ListView listView1;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private Panel panel3;
  private Panel panel2;
  private Button button3;
  private TextBox tbEmail;
  private WebBrowser webBrowser1;
  private Panel panel5;
  private Panel panel4;
  private TextBox tbSubject_1;
  private TabPage tabPage3;
  private TextBox textBox2;
  private Button button4;

  public EmailView_test() => this.InitializeComponent();

  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
  }

  public void Activate(IView previousView)
  {
  }

  public void Deactivate(IView nextView)
  {
  }

  public string Caption => "Бла бла";

  public int ImageIndex => -1;

  public int OrderID => 0;

  private void button2_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog1.ShowDialog() != DialogResult.OK)
      return;
    this.tbFileName.Text = this.openFileDialog1.FileName;
  }

  private void button1_Click(object sender, EventArgs e)
  {
  }

  private void button3_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IEmailService customService = (IEmailService) sessionKeeper.Session.GetCustomService(typeof (IEmailService));
      EmailAccaunt accaunt = ((IEmailService) sessionKeeper.Session.GetCustomService(typeof (IEmailService))).GetAccaunt(this.tbFrom.Text);
      if (accaunt == null)
        throw new Exception($"{this.tbFrom.Text} не найден");
      Guid sessionGuid = sessionKeeper.Session.SessionGUID;
      Guid guid = accaunt.Guid;
      List<string> presentMessageIDs = new List<string>(0);
      List<EmailMessage> inboxMessages = customService.GetInboxMessages(sessionGuid, guid, presentMessageIDs);
      this.listView1.Items.Clear();
      if (inboxMessages == null || inboxMessages.Count <= 0)
        return;
      for (int index = 0; index < inboxMessages.Count; ++index)
        this.listView1.Items.Add(new ListViewItem(new string[3]
        {
          inboxMessages[index].From,
          inboxMessages[index].Date.ToString(),
          inboxMessages[index].Subject
        })
        {
          Tag = (object) inboxMessages[index]
        });
    }
  }

  private void listView1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.listView1.FocusedItem == null || this.listView1.FocusedItem.Tag == null)
      return;
    this.tbSubject_1.Text = ((EmailMessage) this.listView1.FocusedItem.Tag).Subject;
    this.webBrowser1.DocumentText = ((EmailMessage) this.listView1.FocusedItem.Tag).Message;
  }

  private void button4_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      EmailAccaunt accaunt = ((IEmailService) sessionKeeper.Session.GetCustomService(typeof (IEmailService))).GetAccaunt(this.tbFrom.Text);
      if (accaunt == null)
        throw new Exception($"{this.tbFrom.Text} не найден");
      this.textBox2.Text = "MessageID = " + ((IEmailService) sessionKeeper.Session.GetCustomService(typeof (IEmailService))).GetMessageID(sessionKeeper.Session.SessionGUID, accaunt.Guid, this.tbSubject_1.Text);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (EmailView_test));
    this.button1 = new Button();
    this.tbFrom = new TextBox();
    this.label1 = new Label();
    this.label2 = new Label();
    this.tbTo = new TextBox();
    this.label3 = new Label();
    this.tbSubject = new TextBox();
    this.tbMessage = new TextBox();
    this.label4 = new Label();
    this.tbFileName = new TextBox();
    this.button2 = new Button();
    this.openFileDialog1 = new OpenFileDialog();
    this.tabControl1 = new TabControl();
    this.tabPage1 = new TabPage();
    this.panel1 = new Panel();
    this.tabPage2 = new TabPage();
    this.splitContainer1 = new SplitContainer();
    this.panel3 = new Panel();
    this.listView1 = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.panel2 = new Panel();
    this.tbEmail = new TextBox();
    this.button3 = new Button();
    this.panel5 = new Panel();
    this.webBrowser1 = new WebBrowser();
    this.panel4 = new Panel();
    this.tbSubject_1 = new TextBox();
    this.tabPage3 = new TabPage();
    this.textBox2 = new TextBox();
    this.button4 = new Button();
    this.tabControl1.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.panel3.SuspendLayout();
    this.panel2.SuspendLayout();
    this.panel5.SuspendLayout();
    this.panel4.SuspendLayout();
    this.tabPage3.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.button1, "button1");
    this.button1.Name = "button1";
    this.button1.UseVisualStyleBackColor = true;
    this.button1.Click += new EventHandler(this.button1_Click);
    componentResourceManager.ApplyResources((object) this.tbFrom, "tbFrom");
    this.tbFrom.Name = "tbFrom";
    componentResourceManager.ApplyResources((object) this.label1, "label1");
    this.label1.Name = "label1";
    componentResourceManager.ApplyResources((object) this.label2, "label2");
    this.label2.Name = "label2";
    componentResourceManager.ApplyResources((object) this.tbTo, "tbTo");
    this.tbTo.Name = "tbTo";
    componentResourceManager.ApplyResources((object) this.label3, "label3");
    this.label3.Name = "label3";
    componentResourceManager.ApplyResources((object) this.tbSubject, "tbSubject");
    this.tbSubject.Name = "tbSubject";
    componentResourceManager.ApplyResources((object) this.tbMessage, "tbMessage");
    this.tbMessage.Name = "tbMessage";
    componentResourceManager.ApplyResources((object) this.label4, "label4");
    this.label4.Name = "label4";
    componentResourceManager.ApplyResources((object) this.tbFileName, "tbFileName");
    this.tbFileName.Name = "tbFileName";
    componentResourceManager.ApplyResources((object) this.button2, "button2");
    this.button2.Name = "button2";
    this.button2.UseVisualStyleBackColor = true;
    this.button2.Click += new EventHandler(this.button2_Click);
    this.openFileDialog1.FileName = "openFileDialog1";
    componentResourceManager.ApplyResources((object) this.openFileDialog1, "openFileDialog1");
    this.openFileDialog1.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this.tabControl1, "tabControl1");
    this.tabControl1.Controls.Add((Control) this.tabPage1);
    this.tabControl1.Controls.Add((Control) this.tabPage2);
    this.tabControl1.Controls.Add((Control) this.tabPage3);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    componentResourceManager.ApplyResources((object) this.tabPage1, "tabPage1");
    this.tabPage1.Controls.Add((Control) this.panel1);
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.BackColor = SystemColors.Control;
    this.panel1.Controls.Add((Control) this.tbTo);
    this.panel1.Controls.Add((Control) this.button2);
    this.panel1.Controls.Add((Control) this.button1);
    this.panel1.Controls.Add((Control) this.tbFileName);
    this.panel1.Controls.Add((Control) this.tbFrom);
    this.panel1.Controls.Add((Control) this.label4);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.tbMessage);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.tbSubject);
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.tabPage2, "tabPage2");
    this.tabPage2.Controls.Add((Control) this.splitContainer1);
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.Controls.Add((Control) this.panel3);
    this.splitContainer1.Panel1.Controls.Add((Control) this.panel2);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.Controls.Add((Control) this.panel5);
    this.splitContainer1.Panel2.Controls.Add((Control) this.panel4);
    componentResourceManager.ApplyResources((object) this.panel3, "panel3");
    this.panel3.Controls.Add((Control) this.listView1);
    this.panel3.Name = "panel3";
    componentResourceManager.ApplyResources((object) this.listView1, "listView1");
    this.listView1.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3
    });
    this.listView1.GridLines = true;
    this.listView1.Name = "listView1";
    this.listView1.UseCompatibleStateImageBehavior = false;
    this.listView1.View = View.Details;
    this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
    componentResourceManager.ApplyResources((object) this.columnHeader1, "columnHeader1");
    componentResourceManager.ApplyResources((object) this.columnHeader2, "columnHeader2");
    componentResourceManager.ApplyResources((object) this.columnHeader3, "columnHeader3");
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Controls.Add((Control) this.tbEmail);
    this.panel2.Controls.Add((Control) this.button3);
    this.panel2.Name = "panel2";
    componentResourceManager.ApplyResources((object) this.tbEmail, "tbEmail");
    this.tbEmail.Name = "tbEmail";
    componentResourceManager.ApplyResources((object) this.button3, "button3");
    this.button3.Name = "button3";
    this.button3.UseVisualStyleBackColor = true;
    this.button3.Click += new EventHandler(this.button3_Click);
    componentResourceManager.ApplyResources((object) this.panel5, "panel5");
    this.panel5.Controls.Add((Control) this.webBrowser1);
    this.panel5.Name = "panel5";
    componentResourceManager.ApplyResources((object) this.webBrowser1, "webBrowser1");
    this.webBrowser1.MinimumSize = new Size(20, 20);
    this.webBrowser1.Name = "webBrowser1";
    componentResourceManager.ApplyResources((object) this.panel4, "panel4");
    this.panel4.Controls.Add((Control) this.tbSubject_1);
    this.panel4.Name = "panel4";
    componentResourceManager.ApplyResources((object) this.tbSubject_1, "tbSubject_1");
    this.tbSubject_1.Name = "tbSubject_1";
    componentResourceManager.ApplyResources((object) this.tabPage3, "tabPage3");
    this.tabPage3.Controls.Add((Control) this.textBox2);
    this.tabPage3.Controls.Add((Control) this.button4);
    this.tabPage3.Name = "tabPage3";
    this.tabPage3.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.textBox2, "textBox2");
    this.textBox2.Name = "textBox2";
    componentResourceManager.ApplyResources((object) this.button4, "button4");
    this.button4.Name = "button4";
    this.button4.UseVisualStyleBackColor = true;
    this.button4.Click += new EventHandler(this.button4_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tabControl1);
    this.Name = nameof (EmailView_test);
    this.tabControl1.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.tabPage2.ResumeLayout(false);
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.panel3.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.panel2.PerformLayout();
    this.panel5.ResumeLayout(false);
    this.panel4.ResumeLayout(false);
    this.panel4.PerformLayout();
    this.tabPage3.ResumeLayout(false);
    this.tabPage3.PerformLayout();
    this.ResumeLayout(false);
  }
}
