
// Type: Intermech.UI.Winforms.PercentageProgressView
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.UI.Winforms
{
    internal class PercentageProgressView : Form
    {
      private IPercentageProgressSink progressSink;
      private bool isCancelled;
      /// <summary>Required designer variable.</summary>
      private IContainer components;
      private Label lbState;
      private ProgressBar pbPercents;

      public PercentageProgressView() => this.InitializeComponent();

      [Browsable(false)]
      public IPercentageProgressSink ProgressSink
      {
        get
        {
          if (this.progressSink == null)
            this.progressSink = (IPercentageProgressSink) new ProgressBarToPercentageProgressSinkAdapter((Control) this, this.lbState, this.pbPercents, new Func<bool>(this.QueryCancelledState));
          return this.progressSink;
        }
      }

      private bool QueryCancelledState() => this.isCancelled;

      private void PercentageProgressView_FormClosing(object sender, FormClosingEventArgs e)
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
        this.lbState.Text = "Ожидание прерывания операции...";
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
        this.lbState = new Label();
        this.pbPercents = new ProgressBar();
        this.SuspendLayout();
        this.lbState.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.lbState.ImeMode = ImeMode.NoControl;
        this.lbState.Location = new Point(19, 12);
        this.lbState.Name = "lbStage";
        this.lbState.Size = new Size(462, 23);
        this.lbState.TabIndex = 2;
        this.lbState.Text = "Выполнение...";
        this.lbState.TextAlign = ContentAlignment.BottomLeft;
        this.pbPercents.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        this.pbPercents.ImeMode = ImeMode.NoControl;
        this.pbPercents.Location = new Point(22, 38);
        this.pbPercents.Name = "pbPercents";
        this.pbPercents.Size = new Size(459, 23);
        this.pbPercents.Step = 1;
        this.pbPercents.TabIndex = 3;
        this.AutoScaleDimensions = new SizeF(6f, 13f);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(500, 87);
        this.Controls.Add((Control) this.lbState);
        this.Controls.Add((Control) this.pbPercents);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = nameof (PercentageProgressView);
        this.Padding = new Padding(16 /*0x10*/, 12, 16 /*0x10*/, 8);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "Пожалуйста, подождите...";
        this.FormClosing += new FormClosingEventHandler(this.PercentageProgressView_FormClosing);
        this.ResumeLayout(false);
      }
    }
}
