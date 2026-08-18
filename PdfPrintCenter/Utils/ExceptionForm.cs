// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.Utils.ExceptionForm
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.Utils
{
    public class ExceptionForm : Form
    {
        private const int Delta = 6;
        private const int CollapsedMinhHeight = 215;
        private const int ExpandedMinHeight = 364;
        private int _savedTextBoxStackHeight;
        private IContainer components;
        private Label labelHint;
        private TextBox textBoxException;
        private Button buttonCloseApp;
        private Button buttonSkip;
        private Button buttonDetails;
        private RichTextBox textBoxExceptionStack;

        public ExceptionForm()
        {
            this.InitializeComponent();
            this.ToggleDetails(false);
        }

        public DialogResult ShowException(Exception e)
        {
            this.textBoxException.Text = e.Message;
            this.textBoxExceptionStack.Text = ExceptionServices.GetExtendedStackTrace(e);
            return this.ShowDialog();
        }

        private void ButtonDetails_Click(object sender, EventArgs e)
        {
            this.ToggleDetails(!this.textBoxExceptionStack.Visible);
        }

        private void ToggleDetails(bool showDetails)
        {
            if (showDetails)
            {
                this.MinimumSize = new Size(this.MinimumSize.Width, 364);
                this.textBoxExceptionStack.Visible = true;
                this.Height += this._savedTextBoxStackHeight - 364 + 215;
                this._savedTextBoxStackHeight = 0;
                this.buttonDetails.Text = "Скрыть детали";
            }
            else
            {
                this._savedTextBoxStackHeight = this.textBoxExceptionStack.Height + 6;
                this.MinimumSize = new Size(this.MinimumSize.Width, 215);
                this.Height -= this._savedTextBoxStackHeight;
                this.textBoxExceptionStack.Visible = false;
                this.buttonDetails.Text = "Подробнее";
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ExceptionForm));
            this.labelHint = new Label();
            this.textBoxException = new TextBox();
            this.buttonCloseApp = new Button();
            this.buttonSkip = new Button();
            this.buttonDetails = new Button();
            this.textBoxExceptionStack = new RichTextBox();
            this.SuspendLayout();
            this.labelHint.AutoSize = true;
            this.labelHint.Location = new Point(12, 9);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new Size(250, 13);
            this.labelHint.TabIndex = 0;
            this.labelHint.Text = "В системе возникла исключительная ситуация.";
            this.textBoxException.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxException.Location = new Point(12, 25);
            this.textBoxException.Multiline = true;
            this.textBoxException.Name = "textBoxException";
            this.textBoxException.ReadOnly = true;
            this.textBoxException.ScrollBars = ScrollBars.Both;
            this.textBoxException.Size = new Size(560, 110);
            this.textBoxException.TabIndex = 1;
            this.buttonCloseApp.DialogResult = DialogResult.Abort;
            this.buttonCloseApp.ImeMode = ImeMode.NoControl;
            this.buttonCloseApp.Location = new Point(12, 141);
            this.buttonCloseApp.Name = "buttonCloseApp";
            this.buttonCloseApp.Size = new Size(144 /*0x90*/, 25);
            this.buttonCloseApp.TabIndex = 5;
            this.buttonCloseApp.Text = "Закрыть приложение";
            this.buttonSkip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.buttonSkip.DialogResult = DialogResult.Ignore;
            this.buttonSkip.ImeMode = ImeMode.NoControl;
            this.buttonSkip.Location = new Point(476, 141);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new Size(96 /*0x60*/, 25);
            this.buttonSkip.TabIndex = 6;
            this.buttonSkip.Text = "Пропустить";
            this.buttonDetails.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.buttonDetails.ImeMode = ImeMode.NoControl;
            this.buttonDetails.Location = new Point(374, 141);
            this.buttonDetails.Name = "buttonDetails";
            this.buttonDetails.Size = new Size(96 /*0x60*/, 25);
            this.buttonDetails.TabIndex = 7;
            this.buttonDetails.Text = "Подробнее";
            this.buttonDetails.Click += new EventHandler(this.ButtonDetails_Click);
            this.textBoxExceptionStack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxExceptionStack.Location = new Point(12, 172);
            this.textBoxExceptionStack.Name = "textBoxExceptionStack";
            this.textBoxExceptionStack.ReadOnly = true;
            this.textBoxExceptionStack.Size = new Size(560, 141);
            this.textBoxExceptionStack.TabIndex = 8;
            this.textBoxExceptionStack.Text = "";
            this.textBoxExceptionStack.WordWrap = false;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(584, 325);
            this.Controls.Add((Control)this.textBoxExceptionStack);
            this.Controls.Add((Control)this.buttonDetails);
            this.Controls.Add((Control)this.buttonSkip);
            this.Controls.Add((Control)this.buttonCloseApp);
            this.Controls.Add((Control)this.textBoxException);
            this.Controls.Add((Control)this.labelHint);
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MinimumSize = new Size(600, 215);
            this.Name = nameof(ExceptionForm);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = PrintCenterConsts.PrintCenterTitle;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
