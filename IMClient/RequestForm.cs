
// Type: IMClient.RequestForm




using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.InformationCollector;
using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace IMClient
{
    public class RequestForm : Form
    {
      private readonly string defaultTopic = "Запрос технической поддержки IPS";
      public InformationNode RequestInformation = new InformationNode(nameof (RequestInformation), string.Empty);
      public string[] Attach;
      private IContainer components;
      private Label lbTopic;
      private TextBox tbTopic;
      private Label lbOrganization;
      private TextBox tbOrganization;
      private Label lbUser;
      private TextBox tbUser;
      private Label lbMail;
      private TextBox tbMail;
      private Button btnOk;
      private Button btnCancel;
      private Label lbText;
      private TextBox tbRequest;
      private Label label1;
      private ListBox lbAttach;
      private Button btnAdd;
      private Button btnDelete;
      private OpenFileDialog fdAttach;

      public RequestForm()
      {
        this.InitializeComponent();
        this.fdAttach.RestoreDirectory = true;
      }

      public DialogResult ShowForm()
      {
        this.tbOrganization.Text = IPSInformation.OrganizationName();
        if (ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service)
          this.tbUser.Text = service.UserName;
        return this.ShowDialog();
      }

      private void btnOk_Click(object sender, EventArgs e)
      {
        this.RequestInformation.Add(new InformationNode("Topic", string.IsNullOrEmpty(this.tbTopic.Text) ? this.defaultTopic : this.tbTopic.Text));
        this.RequestInformation.Add(new InformationNode("Organization", this.tbOrganization.Text));
        this.RequestInformation.Add(new InformationNode("UserName", this.tbUser.Text));
        this.RequestInformation.Add(new InformationNode("MailTo", this.tbMail.Text));
        this.RequestInformation.Add(new InformationNode("Request", this.tbRequest.Text));
        if (this.lbAttach.Items.Count <= 0)
          return;
        this.Attach = new string[this.lbAttach.Items.Count];
        for (int index = 0; index < this.lbAttach.Items.Count; ++index)
          this.Attach[index] = this.lbAttach.Items[index].ToString();
      }

      private void btnAdd_Click(object sender, EventArgs e)
      {
        if (this.fdAttach.ShowDialog() != DialogResult.OK)
          return;
        foreach (object fileName in this.fdAttach.FileNames)
          this.lbAttach.Items.Add(fileName);
      }

      private void btnDelete_Click(object sender, EventArgs e)
      {
        if (this.lbAttach.SelectedIndex == -1)
          return;
        this.lbAttach.Items.RemoveAt(this.lbAttach.SelectedIndex);
      }

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RequestForm));
        this.lbTopic = new Label();
        this.tbTopic = new TextBox();
        this.lbOrganization = new Label();
        this.tbOrganization = new TextBox();
        this.lbUser = new Label();
        this.tbUser = new TextBox();
        this.lbMail = new Label();
        this.tbMail = new TextBox();
        this.btnOk = new Button();
        this.btnCancel = new Button();
        this.lbText = new Label();
        this.tbRequest = new TextBox();
        this.label1 = new Label();
        this.lbAttach = new ListBox();
        this.btnAdd = new Button();
        this.btnDelete = new Button();
        this.fdAttach = new OpenFileDialog();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this.lbTopic, "lbTopic");
        this.lbTopic.Name = "lbTopic";
        componentResourceManager.ApplyResources((object) this.tbTopic, "tbTopic");
        this.tbTopic.Name = "tbTopic";
        componentResourceManager.ApplyResources((object) this.lbOrganization, "lbOrganization");
        this.lbOrganization.Name = "lbOrganization";
        componentResourceManager.ApplyResources((object) this.tbOrganization, "tbOrganization");
        this.tbOrganization.Name = "tbOrganization";
        componentResourceManager.ApplyResources((object) this.lbUser, "lbUser");
        this.lbUser.Name = "lbUser";
        componentResourceManager.ApplyResources((object) this.tbUser, "tbUser");
        this.tbUser.Name = "tbUser";
        componentResourceManager.ApplyResources((object) this.lbMail, "lbMail");
        this.lbMail.Name = "lbMail";
        componentResourceManager.ApplyResources((object) this.tbMail, "tbMail");
        this.tbMail.Name = "tbMail";
        componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
        this.btnOk.DialogResult = DialogResult.OK;
        this.btnOk.Name = "btnOk";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new EventHandler(this.btnOk_Click);
        componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
        this.btnCancel.DialogResult = DialogResult.Cancel;
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        componentResourceManager.ApplyResources((object) this.lbText, "lbText");
        this.lbText.Name = "lbText";
        componentResourceManager.ApplyResources((object) this.tbRequest, "tbRequest");
        this.tbRequest.Name = "tbRequest";
        componentResourceManager.ApplyResources((object) this.label1, "label1");
        this.label1.Name = "label1";
        componentResourceManager.ApplyResources((object) this.lbAttach, "lbAttach");
        this.lbAttach.FormattingEnabled = true;
        this.lbAttach.Name = "lbAttach";
        componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.UseVisualStyleBackColor = true;
        this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
        componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.UseVisualStyleBackColor = true;
        this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
        this.fdAttach.Multiselect = true;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AutoScaleMode = AutoScaleMode.Font;
        this.CancelButton = (IButtonControl) this.btnCancel;
        this.Controls.Add((Control) this.btnDelete);
        this.Controls.Add((Control) this.btnAdd);
        this.Controls.Add((Control) this.lbAttach);
        this.Controls.Add((Control) this.label1);
        this.Controls.Add((Control) this.tbRequest);
        this.Controls.Add((Control) this.lbText);
        this.Controls.Add((Control) this.btnCancel);
        this.Controls.Add((Control) this.btnOk);
        this.Controls.Add((Control) this.tbMail);
        this.Controls.Add((Control) this.lbMail);
        this.Controls.Add((Control) this.tbUser);
        this.Controls.Add((Control) this.lbUser);
        this.Controls.Add((Control) this.tbOrganization);
        this.Controls.Add((Control) this.lbOrganization);
        this.Controls.Add((Control) this.tbTopic);
        this.Controls.Add((Control) this.lbTopic);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (RequestForm);
        this.ShowInTaskbar = false;
        this.ResumeLayout(false);
        this.PerformLayout();
      }
    }
}
