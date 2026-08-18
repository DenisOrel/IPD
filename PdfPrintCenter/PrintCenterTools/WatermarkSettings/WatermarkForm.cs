// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkForm
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.Utils.UtilMethods;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Ninject;
using PdfiumViewer;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings
{
    internal class WatermarkForm : Form
    {
        private IWatermarkSettingsService watermarkSettingsService;
        private Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings _initialWatermarkSettings;
        private IContainer components;
        private SplitContainer splitContainer1;
        private CheckBox checkBoxEnableAutoView;
        private Button buttonView;
        private ComboBox comboBoxWatermarkLayer;
        private Label labelLayer;
        private NumericUpDown upDownFontSize;
        private Label labelFontSize;
        private Button buttonOk;
        private Button buttonCancel;
        private NumericUpDown upDownWatermarkRotation;
        private Label labelRotation;
        private Label labelText;
        private ComboBox comboBoxWaterMarkPosition;
        private Label labelPosition;
        private PdfViewer pdfViewer;
        private TextBox tbWatermarkText;

        public WatermarkForm()
        {
            this.InitializeComponent();
            this.InitializeFormSettings();
        }

        [Inject]
        public WatermarkForm(IWatermarkSettingsService watermarkSettingsService)
        {
            this.InitializeComponent();
            this.InitializeServices(watermarkSettingsService);
            this.InitializeFormSettings();
        }

        private void InitializeFormSettings()
        {
            this.InitializeWatermark();
            this.InitializeControls();
            this.InitializeHandlers();
            this.InitializePdfViewer();
        }

        private void InitializeControls()
        {
            this.tbWatermarkText.Text = this._initialWatermarkSettings.Text;
            this.comboBoxWaterMarkPosition.SelectedIndex = (int)this._initialWatermarkSettings.Position;
            this.comboBoxWatermarkLayer.SelectedIndex = (int)this._initialWatermarkSettings.Layer;
            this.upDownWatermarkRotation.Value = (Decimal)this._initialWatermarkSettings.Angle;
            this.upDownFontSize.Value = (Decimal)this._initialWatermarkSettings.FontSize;
        }

        private void InitializeHandlers()
        {
            this.tbWatermarkText.TextChanged += new EventHandler(this.TextBoxWatermarkText_TextChanged);
            this.tbWatermarkText.LostFocus += new EventHandler(this.TextBoxWatermarkText_LostFocus);
            this.comboBoxWaterMarkPosition.SelectionChangeCommitted += new EventHandler(this.ComboBoxWaterMarkPosition_SelectionChangeCommited);
            this.upDownWatermarkRotation.TextChanged += new EventHandler(this.UpDownWatermarkRotation_TextChanged);
            this.upDownFontSize.TextChanged += new EventHandler(this.UpDownFontSize_TextChanged);
            this.comboBoxWatermarkLayer.SelectionChangeCommitted += new EventHandler(this.ComboBoxWatermarkLayer_SelectionChangeCommitted);
        }

        private void InitializePdfViewer() => this.ShowPreview();

        private void InitializeServices(IWatermarkSettingsService watermarkSettingsService)
        {
            this.watermarkSettingsService = watermarkSettingsService;
        }

        private void InitializeWatermark()
        {
            this._initialWatermarkSettings = this.watermarkSettingsService.GetWatermarkSettings().Clone() as Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
                this.SaveSettings();
            else if (this.DialogResult == DialogResult.Cancel && this.IsSettingsChanged())
            {
                switch (MessageBox.Show("Сохранить изменения перед выходом?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk))
                {
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                    case DialogResult.Yes:
                        this.SaveSettings();
                        this.DialogResult = DialogResult.OK;
                        break;
                }
            }
            base.OnFormClosing(e);
        }

        private void ButtonView_Click(object sender, EventArgs e) => this.ShowPreview();

        private void CheckBoxEnableAutoView_CheckedChanged(object sender, EventArgs e)
        {
            this.buttonView.Enabled = !this.checkBoxEnableAutoView.Checked;
            if (!this.checkBoxEnableAutoView.Checked)
                return;
            this.ShowPreview();
        }

        private void ComboBoxWatermarkLayer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.AutoView();
        }

        private void ComboBoxWaterMarkPosition_SelectionChangeCommited(object sender, EventArgs e)
        {
            this.AutoView();
        }

        private void TextBoxWatermarkText_TextChanged(object sender, EventArgs e) => this.AutoView();

        private void TextBoxWatermarkText_LostFocus(object sender, EventArgs e) => this.AutoView();

        private void UpDownFontSize_TextChanged(object sender, EventArgs e) => this.AutoView();

        private void UpDownWatermarkRotation_TextChanged(object sender, EventArgs e) => this.AutoView();

        private void AutoView()
        {
            if (!this.checkBoxEnableAutoView.Checked)
                return;
            this.ShowPreview();
        }

        private Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings GetCurrentWatermarkSettings()
        {
            return new Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings()
            {
                Text = this.tbWatermarkText.Text,
                Position = (WatermarkPosition)this.comboBoxWaterMarkPosition.SelectedIndex,
                Layer = (WatermarkLayer)this.comboBoxWatermarkLayer.SelectedIndex,
                Angle = Convert.ToInt32(this.upDownWatermarkRotation.Value),
                FontSize = Convert.ToInt32(this.upDownFontSize.Value)
            };
        }

        private bool IsSettingsChanged()
        {
            return !this.GetCurrentWatermarkSettings().Equals((object)this._initialWatermarkSettings);
        }

        private void SaveSettings()
        {
            Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings = this.GetCurrentWatermarkSettings();
            watermarkSettings.Freeze();
            this.watermarkSettingsService.PutWatermarkSettings(watermarkSettings);
        }

        private void ShowPreview()
        {
            Intermech.PdfPrintCenter.PrintCenterTools.WatermarkSettings.WatermarkSettings watermarkSettings = this.GetCurrentWatermarkSettings();
            MemoryStream os = new MemoryStream();
            using (Document document = new Document())
            {
                using (PdfWriter instance = PdfWriter.GetInstance(document, (Stream)os))
                {
                    instance.CloseStream = false;
                    document.Open();
                    document.SetPageSize(PageSize.A4);
                    document.NewPage();
                    string filename = Path.Combine(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName), "testpage.pdf");
                    try
                    {
                        PdfReader reader = new PdfReader(filename);
                        PdfImportedPage importedPage = instance.GetImportedPage(reader, 1);
                        instance.DirectContent.AddTemplate((PdfTemplate)importedPage, 0.0f, 0.0f);
                    }
                    catch (Exception ex)
                    {
                    }
                    if (!string.IsNullOrWhiteSpace(this.tbWatermarkText.Text))
                        instance.PrintWatermark(watermarkSettings, PageSize.A4);
                    document.Close();
                    os.Position = 0L;
                    this.pdfViewer.Document = (IPdfDocument)PdfiumViewer.PdfDocument.Load((Stream)os);
                }
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
            this.splitContainer1 = new SplitContainer();
            this.checkBoxEnableAutoView = new CheckBox();
            this.buttonView = new Button();
            this.comboBoxWatermarkLayer = new ComboBox();
            this.labelLayer = new Label();
            this.upDownFontSize = new NumericUpDown();
            this.labelFontSize = new Label();
            this.buttonOk = new Button();
            this.buttonCancel = new Button();
            this.upDownWatermarkRotation = new NumericUpDown();
            this.labelRotation = new Label();
            this.labelText = new Label();
            this.comboBoxWaterMarkPosition = new ComboBox();
            this.labelPosition = new Label();
            this.pdfViewer = new PdfViewer();
            this.tbWatermarkText = new TextBox();
            this.splitContainer1.BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.upDownFontSize.BeginInit();
            this.upDownWatermarkRotation.BeginInit();
            this.SuspendLayout();
            this.splitContainer1.BackColor = SystemColors.ControlLight;
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add((Control)this.tbWatermarkText);
            this.splitContainer1.Panel1.Controls.Add((Control)this.checkBoxEnableAutoView);
            this.splitContainer1.Panel1.Controls.Add((Control)this.buttonView);
            this.splitContainer1.Panel1.Controls.Add((Control)this.comboBoxWatermarkLayer);
            this.splitContainer1.Panel1.Controls.Add((Control)this.labelLayer);
            this.splitContainer1.Panel1.Controls.Add((Control)this.upDownFontSize);
            this.splitContainer1.Panel1.Controls.Add((Control)this.labelFontSize);
            this.splitContainer1.Panel1.Controls.Add((Control)this.buttonOk);
            this.splitContainer1.Panel1.Controls.Add((Control)this.buttonCancel);
            this.splitContainer1.Panel1.Controls.Add((Control)this.upDownWatermarkRotation);
            this.splitContainer1.Panel1.Controls.Add((Control)this.labelRotation);
            this.splitContainer1.Panel1.Controls.Add((Control)this.labelText);
            this.splitContainer1.Panel1.Controls.Add((Control)this.comboBoxWaterMarkPosition);
            this.splitContainer1.Panel1.Controls.Add((Control)this.labelPosition);
            this.splitContainer1.Panel1MinSize = 220;
            this.splitContainer1.Panel2.BackColor = SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add((Control)this.pdfViewer);
            this.splitContainer1.Size = new Size(595, 425);
            this.splitContainer1.SplitterDistance = 237;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 15;
            this.checkBoxEnableAutoView.AutoSize = true;
            this.checkBoxEnableAutoView.Checked = true;
            this.checkBoxEnableAutoView.CheckState = CheckState.Checked;
            this.checkBoxEnableAutoView.Location = new Point(12, 214);
            this.checkBoxEnableAutoView.Name = "checkBoxEnableAutoView";
            this.checkBoxEnableAutoView.Size = new Size(162, 17);
            this.checkBoxEnableAutoView.TabIndex = 24;
            this.checkBoxEnableAutoView.Text = "Автоматический просмотр";
            this.checkBoxEnableAutoView.UseVisualStyleBackColor = true;
            this.buttonView.Enabled = false;
            this.buttonView.Location = new Point(12, 237);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new Size(95, 25);
            this.buttonView.TabIndex = 25;
            this.buttonView.Text = "Просмотр";
            this.buttonView.UseVisualStyleBackColor = true;
            this.comboBoxWatermarkLayer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.comboBoxWatermarkLayer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxWatermarkLayer.FormattingEnabled = true;
            this.comboBoxWatermarkLayer.Items.AddRange(new object[2]
            {
          (object) "Под документом",
          (object) "Над документом"
            });
            this.comboBoxWatermarkLayer.Location = new Point(12, 182);
            this.comboBoxWatermarkLayer.Name = "comboBoxWatermarkLayer";
            this.comboBoxWatermarkLayer.Size = new Size(213, 21);
            this.comboBoxWatermarkLayer.TabIndex = 23;
            this.labelLayer.AutoSize = true;
            this.labelLayer.Location = new Point(9, 166);
            this.labelLayer.Name = "labelLayer";
            this.labelLayer.Size = new Size(35, 13);
            this.labelLayer.TabIndex = 22;
            this.labelLayer.Text = "Слой:";
            this.upDownFontSize.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.upDownFontSize.Location = new Point(12, 143);
            this.upDownFontSize.Minimum = new Decimal(new int[4]
            {
          1,
          0,
          0,
          0
            });
            this.upDownFontSize.Name = "upDownFontSize";
            this.upDownFontSize.Size = new Size(213, 20);
            this.upDownFontSize.TabIndex = 21;
            this.upDownFontSize.Value = new Decimal(new int[4]
            {
          14,
          0,
          0,
          0
            });
            this.labelFontSize.AutoSize = true;
            this.labelFontSize.Location = new Point(9, 126);
            this.labelFontSize.Name = "labelFontSize";
            this.labelFontSize.Size = new Size(91, 13);
            this.labelFontSize.TabIndex = 20;
            this.labelFontSize.Text = "Размер шрифта:";
            this.buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.buttonOk.DialogResult = DialogResult.OK;
            this.buttonOk.Location = new Point(12, 388);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new Size(95, 25);
            this.buttonOk.TabIndex = 26;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Location = new Point(113, 388);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(95, 25);
            this.buttonCancel.TabIndex = 27;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.upDownWatermarkRotation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.upDownWatermarkRotation.Location = new Point(12, 103);
            this.upDownWatermarkRotation.Maximum = new Decimal(new int[4]
            {
          180,
          0,
          0,
          0
            });
            this.upDownWatermarkRotation.Minimum = new Decimal(new int[4]
            {
          180,
          0,
          0,
          int.MinValue
            });
            this.upDownWatermarkRotation.Name = "upDownWatermarkRotation";
            this.upDownWatermarkRotation.Size = new Size(213, 20);
            this.upDownWatermarkRotation.TabIndex = 19;
            this.labelRotation.AutoSize = true;
            this.labelRotation.Location = new Point(9, 86);
            this.labelRotation.Name = "labelRotation";
            this.labelRotation.Size = new Size(76, 13);
            this.labelRotation.TabIndex = 18;
            this.labelRotation.Text = "Поворот (гр.):";
            this.labelText.AutoSize = true;
            this.labelText.Location = new Point(9, 6);
            this.labelText.Name = "labelText";
            this.labelText.Size = new Size(40, 13);
            this.labelText.TabIndex = 14;
            this.labelText.Text = "Текст:";
            this.comboBoxWaterMarkPosition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.comboBoxWaterMarkPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxWaterMarkPosition.FormattingEnabled = true;
            this.comboBoxWaterMarkPosition.Items.AddRange(new object[5]
            {
          (object) "Внизу слева",
          (object) "Внизу справа",
          (object) "Вверху слева",
          (object) "Вверху справа",
          (object) "Замостить"
            });
            this.comboBoxWaterMarkPosition.Location = new Point(12, 62);
            this.comboBoxWaterMarkPosition.Name = "comboBoxWaterMarkPosition";
            this.comboBoxWaterMarkPosition.Size = new Size(213, 21);
            this.comboBoxWaterMarkPosition.TabIndex = 17;
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new Point(9, 46);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new Size(85, 13);
            this.labelPosition.TabIndex = 16 /*0x10*/;
            this.labelPosition.Text = "Расположение:";
            this.pdfViewer.AutoScroll = true;
            this.pdfViewer.BackColor = SystemColors.ControlDarkDark;
            this.pdfViewer.Dock = DockStyle.Fill;
            this.pdfViewer.Location = new Point(0, 0);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.ShowBookmarks = false;
            this.pdfViewer.ShowToolbar = false;
            this.pdfViewer.Size = new Size(351, 425);
            this.pdfViewer.TabIndex = 15;
            this.tbWatermarkText.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.tbWatermarkText.Location = new Point(12, 23);
            this.tbWatermarkText.Name = "tbWatermarkText";
            this.tbWatermarkText.Size = new Size(213, 20);
            this.tbWatermarkText.TabIndex = 28;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(595, 425);
            this.Controls.Add((Control)this.splitContainer1);
            this.MinimumSize = new Size(567, 425);
            this.Name = nameof(WatermarkForm);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Настройка водяного знака";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.upDownFontSize.EndInit();
            this.upDownWatermarkRotation.EndInit();
            this.ResumeLayout(false);
        }
    }
}
