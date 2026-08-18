
// Type: IMClient.AuthorizeForm




using IMClient.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace IMClient
{
    public class AuthorizeForm : Form
    {
      private IContainer components;
      private Button btCancel;
      private Button btAutorize;
      private Button btContinue;
      private PictureBox pictureBox1;
      private GroupBox groupBox1;
      private Label label1;
      private TextBox tbSerial;
      private Button btCopy;
      private Button btPaste;
      private TextBox tbAuthCode;
      private Label label2;
      private Timer timer1;
      private Label label4;
      private Label lbDaysLeft;
      private ToolTip toolTip1;
      private Label label7;
      private Label label6;
      private Label label5;
      private Label label3;

      public AuthorizeForm()
      {
        this.InitializeComponent();
        this.ActiveControl = (Control) this.tbAuthCode;
      }

      internal static string ShowDialog(int daysLeft, string serialCode, ref bool cancel)
      {
        AuthorizeForm authorizeForm = new AuthorizeForm();
        cancel = true;
        string str = string.Empty;
        authorizeForm.SetData(daysLeft, serialCode);
        switch (authorizeForm.ShowDialog())
        {
          case DialogResult.OK:
            str = authorizeForm.GetData();
            cancel = false;
            break;
          case DialogResult.Yes:
            cancel = false;
            break;
          default:
            cancel = true;
            break;
        }
        return str;
      }

      private void timer1_Tick(object sender, EventArgs e)
      {
        this.timer1.Enabled = false;
        this.btContinue.Visible = true;
      }

      private void textBox2_TextChanged(object sender, EventArgs e)
      {
        this.btAutorize.Enabled = this.tbAuthCode.Text.Length == 16 /*0x10*/;
      }

      private void label5_Click(object sender, EventArgs e)
      {
        this.Execute(LocalizationHolder.rm.GetString("IMClient_90"), (string) null);
      }

      private void Execute(string cmd, string args)
      {
        ProcessStartInfo processStartInfo = new ProcessStartInfo(cmd, args);
        processStartInfo.UseShellExecute = true;
        processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        Process process = new Process();
        try
        {
          try
          {
            process.StartInfo = processStartInfo;
            process.Start();
          }
          finally
          {
            process.Close();
          }
        }
        catch
        {
        }
      }

      private void btCopy_Click(object sender, EventArgs e)
      {
        try
        {
          Clipboard.SetText(this.tbSerial.Text);
        }
        catch
        {
        }
      }

      private void btPaste_Click(object sender, EventArgs e)
      {
        this.tbAuthCode.Text = Clipboard.GetText().Trim();
      }

      internal void SetData(int days, string serialCode)
      {
        this.tbSerial.Text = "H" + serialCode;
        if (days > 0)
        {
          this.timer1.Enabled = true;
          this.lbDaysLeft.Text = string.Format(LocalizationHolder.rm.GetString("IMClient_91"), (object) days);
        }
        else
        {
          this.timer1.Enabled = false;
          this.btContinue.Visible = false;
          this.lbDaysLeft.Text = LocalizationHolder.rm.GetString("IMClient_92");
          this.lbDaysLeft.ForeColor = Color.Red;
        }
      }

      internal string GetData() => this.tbAuthCode.Text;

      protected override void Dispose(bool disposing)
      {
        if (disposing && this.components != null)
          this.components.Dispose();
        base.Dispose(disposing);
      }

      private void InitializeComponent()
      {
        this.components = (IContainer) new System.ComponentModel.Container();
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AuthorizeForm));
        this.btCancel = new Button();
        this.btAutorize = new Button();
        this.btContinue = new Button();
        this.groupBox1 = new GroupBox();
        this.label7 = new Label();
        this.label6 = new Label();
        this.label5 = new Label();
        this.label3 = new Label();
        this.btCopy = new Button();
        this.label4 = new Label();
        this.tbSerial = new TextBox();
        this.lbDaysLeft = new Label();
        this.label1 = new Label();
        this.btPaste = new Button();
        this.tbAuthCode = new TextBox();
        this.label2 = new Label();
        this.pictureBox1 = new PictureBox();
        this.timer1 = new Timer(this.components);
        this.toolTip1 = new ToolTip(this.components);
        this.groupBox1.SuspendLayout();
        ((ISupportInitialize) this.pictureBox1).BeginInit();
        this.SuspendLayout();
        this.btCancel.DialogResult = DialogResult.Cancel;
        componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
        this.btCancel.Name = "btCancel";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btAutorize.DialogResult = DialogResult.OK;
        componentResourceManager.ApplyResources((object) this.btAutorize, "btAutorize");
        this.btAutorize.Name = "btAutorize";
        this.btAutorize.UseVisualStyleBackColor = true;
        this.btContinue.DialogResult = DialogResult.Yes;
        componentResourceManager.ApplyResources((object) this.btContinue, "btContinue");
        this.btContinue.Name = "btContinue";
        this.btContinue.UseVisualStyleBackColor = true;
        this.groupBox1.Controls.Add((Control) this.label7);
        this.groupBox1.Controls.Add((Control) this.label6);
        this.groupBox1.Controls.Add((Control) this.label5);
        this.groupBox1.Controls.Add((Control) this.label3);
        this.groupBox1.Controls.Add((Control) this.btCopy);
        this.groupBox1.Controls.Add((Control) this.label4);
        this.groupBox1.Controls.Add((Control) this.tbSerial);
        this.groupBox1.Controls.Add((Control) this.lbDaysLeft);
        this.groupBox1.Controls.Add((Control) this.label1);
        this.groupBox1.Controls.Add((Control) this.btPaste);
        this.groupBox1.Controls.Add((Control) this.tbAuthCode);
        this.groupBox1.Controls.Add((Control) this.label2);
        this.groupBox1.Controls.Add((Control) this.pictureBox1);
        componentResourceManager.ApplyResources((object) this.groupBox1, "groupBox1");
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.TabStop = false;
        componentResourceManager.ApplyResources((object) this.label7, "label7");
        this.label7.Name = "label7";
        componentResourceManager.ApplyResources((object) this.label6, "label6");
        this.label6.Name = "label6";
        componentResourceManager.ApplyResources((object) this.label5, "label5");
        this.label5.ForeColor = Color.RoyalBlue;
        this.label5.Name = "label5";
        this.label5.Click += new EventHandler(this.label5_Click);
        componentResourceManager.ApplyResources((object) this.label3, "label3");
        this.label3.Name = "label3";
        componentResourceManager.ApplyResources((object) this.btCopy, "btCopy");
        this.btCopy.Image = (Image) Resources.CopyHS;
        this.btCopy.Name = "btCopy";
        this.toolTip1.SetToolTip((Control) this.btCopy, componentResourceManager.GetString("btCopy.ToolTip"));
        this.btCopy.UseVisualStyleBackColor = true;
        this.btCopy.Click += new EventHandler(this.btCopy_Click);
        componentResourceManager.ApplyResources((object) this.label4, "label4");
        this.label4.Name = "label4";
        this.tbSerial.AcceptsReturn = true;
        componentResourceManager.ApplyResources((object) this.tbSerial, "tbSerial");
        this.tbSerial.Name = "tbSerial";
        componentResourceManager.ApplyResources((object) this.lbDaysLeft, "lbDaysLeft");
        this.lbDaysLeft.Name = "lbDaysLeft";
        componentResourceManager.ApplyResources((object) this.label1, "label1");
        this.label1.Name = "label1";
        componentResourceManager.ApplyResources((object) this.btPaste, "btPaste");
        this.btPaste.Image = (Image) Resources.PasteHS;
        this.btPaste.Name = "btPaste";
        this.toolTip1.SetToolTip((Control) this.btPaste, componentResourceManager.GetString("btPaste.ToolTip"));
        this.btPaste.UseVisualStyleBackColor = true;
        this.btPaste.Click += new EventHandler(this.btPaste_Click);
        componentResourceManager.ApplyResources((object) this.tbAuthCode, "tbAuthCode");
        this.tbAuthCode.Name = "tbAuthCode";
        this.tbAuthCode.TextChanged += new EventHandler(this.textBox2_TextChanged);
        componentResourceManager.ApplyResources((object) this.label2, "label2");
        this.label2.Name = "label2";
        this.pictureBox1.Image = (Image) Resources.auth;
        componentResourceManager.ApplyResources((object) this.pictureBox1, "pictureBox1");
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.TabStop = false;
        this.timer1.Interval = 7000;
        this.timer1.Tick += new EventHandler(this.timer1_Tick);
        this.AcceptButton = (IButtonControl) this.btAutorize;
        this.CancelButton = (IButtonControl) this.btCancel;
        componentResourceManager.ApplyResources((object) this, "$this");
        this.Controls.Add((Control) this.groupBox1);
        this.Controls.Add((Control) this.btContinue);
        this.Controls.Add((Control) this.btAutorize);
        this.Controls.Add((Control) this.btCancel);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.Name = nameof (AuthorizeForm);
        this.TopMost = true;
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        ((ISupportInitialize) this.pictureBox1).EndInit();
        this.ResumeLayout(false);
      }
    }
}
