
// Type: Intermech.UI.Winforms.MasterSlavePercentageProgressView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    internal class MasterSlavePercentageProgressView : Form, IMasterSlaveProgressSink
    {
      private IPercentageProgressSink masterProgressSink;
      private bool isCancelled;
      /// <summary>Required designer variable.</summary>
      private IContainer components;
      private ProgressBar pbMaster;
      private Label lbMaster;
      private Label lbSlave;
      private ProgressBar pbSlave;
      private Button btCancel;

      public MasterSlavePercentageProgressView() => this.InitializeComponent();

      [Browsable(false)]
      public IPercentageProgressSink MasterSink
      {
        get
        {
          if (this.masterProgressSink == null)
            this.masterProgressSink = (IPercentageProgressSink) new ProgressBarToPercentageProgressSinkAdapter((Control) this, this.lbMaster, this.pbMaster, new Func<bool>(this.QueryCancelledState));
          return this.masterProgressSink;
        }
      }

      public IPercentageProgressSink CreateSlaveSink()
      {
        this.lbSlave.Text = string.Empty;
        this.lbSlave.Enabled = true;
        this.pbSlave.Value = this.pbSlave.Minimum;
        this.pbSlave.Enabled = true;
        return (IPercentageProgressSink) new ProgressBarToPercentageProgressSinkAdapter((Control) this, this.lbSlave, this.pbSlave, new Func<bool>(this.QueryCancelledState));
      }

      private bool QueryCancelledState() => this.isCancelled;

      private void btCancel_Click(object sender, EventArgs e) => this.AskCancel();

      private void MasterSlavePercentageProgressView_FormClosing(object sender, FormClosingEventArgs e)
      {
        if (e.CloseReason == CloseReason.UserClosing)
        {
          this.AskCancel();
          e.Cancel = true;
        }
        else
          this.SetCancelState();
      }

      private void AskCancel()
      {
        if (this.isCancelled || MessageBox.Show("Вы действительно хотите прервать выполняемую операцию?", "Прерывание текущей операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
          return;
        this.SetCancelState();
      }

      private void SetCancelState()
      {
        if (this.isCancelled)
          return;
        this.isCancelled = true;
        this.lbMaster.Text = "Ожидание прерывания операции...";
        this.lbSlave.Text = "Ожидание прерывания операции...";
        this.lbSlave.Enabled = false;
        this.pbSlave.Enabled = false;
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
        ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MasterSlavePercentageProgressView));
        this.pbMaster = new ProgressBar();
        this.lbMaster = new Label();
        this.lbSlave = new Label();
        this.pbSlave = new ProgressBar();
        this.btCancel = new Button();
        this.SuspendLayout();
        componentResourceManager.ApplyResources((object) this.pbMaster, "pbMaster");
        this.pbMaster.Name = "pbMaster";
        this.pbMaster.Step = 1;
        componentResourceManager.ApplyResources((object) this.lbMaster, "lbMaster");
        this.lbMaster.Name = "lbMaster";
        componentResourceManager.ApplyResources((object) this.lbSlave, "lbChild");
        this.lbSlave.Name = "lbChild";
        componentResourceManager.ApplyResources((object) this.pbSlave, "pbChild");
        this.pbSlave.Name = "pbChild";
        this.pbSlave.Step = 1;
        componentResourceManager.ApplyResources((object) this.btCancel, "btCancel");
        this.btCancel.Name = "btCancel";
        this.btCancel.UseVisualStyleBackColor = true;
        this.btCancel.Click += new EventHandler(this.btCancel_Click);
        componentResourceManager.ApplyResources((object) this, "$this");
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add((Control) this.btCancel);
        this.Controls.Add((Control) this.lbSlave);
        this.Controls.Add((Control) this.pbSlave);
        this.Controls.Add((Control) this.lbMaster);
        this.Controls.Add((Control) this.pbMaster);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (MasterSlavePercentageProgressView);
        this.FormClosing += new FormClosingEventHandler(this.MasterSlavePercentageProgressView_FormClosing);
        this.ResumeLayout(false);
      }
    }
}
