// Decompiled with JetBrains decompiler
// Type: Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings.LayoutEditor
// Assembly: PdfPrintCenter, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 78C265CD-C195-45CA-AEC0-1C98D45B3103
// Assembly location: D:\IPS\Client\PdfPrintCenter\PdfPrintCenter.exe

using Intermech.PdfPrintCenter.Interfaces;
using Intermech.PdfPrintCenter.PrintCenterTools.PdfFileSettings;
using Intermech.PdfPrintCenter.Properties;
using Intermech.PdfPrintCenter.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.PdfPrintCenter.PrintCenterTools.LayoutsSettings
{
    internal class LayoutEditor : Form
    {
        private ILayoutSettingsService layoutSettingsService;
        private object _layoutId;
        private bool _isSelectionChanged;
        private IContainer components;
        private ToolStrip toolStripMenu;
        private ToolStripButton toolStripButtonOpen;
        private ToolStripButton toolStripButtonSave;
        private SplitContainer EdiorContainer;
        private Panel panelFormat;
        private Panel panelLayoutEditor;
        private GroupBox groupBoxSheetsOnLayout;
        private GroupBox groupBoxLayoutParameters;
        private ComboBox comboBoxLayoutFormat;
        private Button buttonDeleteSheet;
        private Button buttonAddSheet;
        private ListView listViewLayoutSheets;
        private ColumnHeader columnPosition;
        private ColumnHeader columnSize;
        private TextBox textBoxLayoutName;
        private Label labelLayoutName;
        private GroupBox groupBoxSheetLocation;
        private NumericUpDown pageTopUpDown;
        private Label label3;
        private Label label4;
        private NumericUpDown pageLeftUpDown;
        private ColumnHeader columnFormat;
        private Label labelFormat;
        private Button buttonAuto;
        private ToolStripButton toolStripButtonNew;

        public LayoutEditor()
        {
            this.InitializeComponent();
            this.InitializeFormSettings();
        }

        [Inject]
        public LayoutEditor(ILayoutSettingsService layoutSettingsService)
        {
            this.InitializeComponent();
            this.InitializeServices(layoutSettingsService);
            this.InitializeFormSettings();
        }

        public List<RenamedLayout> RenamedLayouts { get; private set; }

        private Panel ActivePanel { get; set; }

        private LayoutDescriptor InitPdfLayout { get; set; }

        private void InitializeFormSettings()
        {
            this.InitializeTextBoxLayoutName();
            this.InitializeComboBoxLayoutFormat();
            this.InitializePdfLayout();
            this.InitializeRenamedLayouts();
            this.InitializeSheetLocationUpDowns();
            this.InitializeActivePanel();
            this.CheckSaveButton();
        }

        private void InitializeActivePanel() => this.ActivePanel = new Panel();

        private void InitializeComboBoxLayoutFormat()
        {
            KnownPaperFormats.LoadToComboBox(this.comboBoxLayoutFormat);
            this.comboBoxLayoutFormat.SelectedIndex = KnownPaperFormats.Formats.IndexOf(KnownPaperFormats.GetFormat(LayoutDescriptor.DefaultMainFormatName));
        }

        private void InitializePdfLayout() => this.InitPdfLayout = new LayoutDescriptor();

        private void InitializeServices(ILayoutSettingsService layoutSettingsService)
        {
            this.layoutSettingsService = layoutSettingsService;
        }

        private void InitializeTextBoxLayoutName()
        {
            this.textBoxLayoutName.Text = LayoutDescriptor.DefaultCaption;
        }

        private void InitializeRenamedLayouts() => this.RenamedLayouts = new List<RenamedLayout>();

        private void InitializeSheetLocationUpDowns() => this.EnableSheetConnectedControls();

        private void ButtonAddSheet_Click(object sender, EventArgs e)
        {
            NewSheetDialog newSheetDialog = new NewSheetDialog();
            if (newSheetDialog.ShowDialog() != DialogResult.OK)
                return;
            this.AddSheetToPanel(newSheetDialog.SelectedFormat, true);
            this.ScalePanelFormat();
            this.CheckSaveButton();
        }

        private void ButtonAuto_Click(object sender, EventArgs e)
        {
            KnownPaperFormat selectedItem = this.comboBoxLayoutFormat.SelectedItem as KnownPaperFormat;
            List<KnownPaperFormat> smallerFormats = KnownPaperFormats.GetSmallerFormats(KnownPaperFormats.GetFormat(selectedItem.BaseName));
            List<KnownPaperFormat> list = smallerFormats != null ? smallerFormats.Where<KnownPaperFormat>((Func<KnownPaperFormat, bool>)(format => format.IsPortait)).ToList<KnownPaperFormat>() : (List<KnownPaperFormat>)null;
            if (!list.Any<KnownPaperFormat>())
            {
                int num = (int)MessageBox.Show("Для выбранной основы макета не нашлось форматов меньших размеров.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SelectFormatDialog selectFormatDialog = new SelectFormatDialog(list);
                if (selectFormatDialog.ShowDialog() != DialogResult.OK)
                    return;
                KnownPaperFormat selectedFormat = selectFormatDialog.SelectedFormat;
                this.SetInternalFormats(new OptimalLayoutCreator(selectedItem, selectedFormat).CreateOptimalLayout());
                this.CheckSaveButton();
            }
        }

        private void ButtonDeleteSheet_Click(object sender, EventArgs e)
        {
            if (this.listViewLayoutSheets.SelectedItems.Count == 0)
                return;
            ListViewItem selectedItem = this.listViewLayoutSheets.SelectedItems[0];
            int index = selectedItem.Index;
            if (!(selectedItem.Tag is SheetPanelLocation tag))
                return;
            this.panelFormat.Controls.Remove((Control)tag.Panel);
            selectedItem.Remove();
            this.SetNextSelectedListItem(index);
            this.CheckSaveButton();
        }

        private void ComboBoxLayoutFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ScalePanelFormat();
            this.CheckSaveButton();
        }

        private void LayoutEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.LayoutChanged())
                return;
            switch (MessageBox.Show("Макет был изменён. Сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    this.SaveLayout(this._layoutId == null);
                    break;
            }
        }

        private void ListViewLayoutSheets_ItemSelectionChanged(
          object sender,
          ListViewItemSelectionChangedEventArgs e)
        {
            this.EnableSheetConnectedControls();
            if (e.Item == null || !e.IsSelected || !(e.Item.Tag is SheetPanelLocation tag))
                return;
            this.SetActivePanel(tag.Panel);
            this._isSelectionChanged = true;
            this.pageLeftUpDown.Value = (Decimal)tag.FormatLocation.Left;
            this.pageTopUpDown.Value = (Decimal)tag.FormatLocation.Top;
            this._isSelectionChanged = false;
        }

        private void PanelLayoutEditor_Resize(object sender, EventArgs e) => this.ScalePanelFormat();

        private void PanelPage_Click(object sender, EventArgs e)
        {
            if (!(sender is Panel panel))
                return;
            this.SetActivePanel(panel);
        }

        private void TextBoxLayoutName_TextChanged(object sender, EventArgs e) => this.CheckSaveButton();

        private void ToolStripButtonNew_Click(object sender, EventArgs e)
        {
            if (this.LayoutChanged())
            {
                switch (MessageBox.Show("Сохранить текущий макет?", "Подтверждение", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        this.SaveLayout();
                        break;
                }
            }
            this.SetNewLayout();
            this.CheckSaveButton();
        }

        private void ToolStripButtonOpen_Click(object sender, EventArgs e)
        {
            if (this.LayoutChanged())
            {
                switch (MessageBox.Show("Сохранить текущий макет?", "Подтверждение", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        this.SaveLayout();
                        break;
                }
            }
            this.OpenLayout();
            this.CheckSaveButton();
        }

        private void ToolStripButtonSave_Click(object sender, EventArgs e)
        {
            this.SaveLayout(this._layoutId == null);
            this.CheckSaveButton();
        }

        private void UpDownPage_ValueChanged(object sender, EventArgs e)
        {
            if (this.ActivePanel == null)
                return;
            SheetPanelLocation tag = this.ActivePanel.Tag as SheetPanelLocation;
            Decimal num1 = this.pageLeftUpDown.Value;
            Decimal num2 = this.pageTopUpDown.Value;
            if (!this._isSelectionChanged)
                tag.MovePanelFormat(Convert.ToInt32(this.pageLeftUpDown.Value), Convert.ToInt32(this.pageTopUpDown.Value));
            this.ScalePanelFormat();
            this.CheckSaveButton();
        }

        private void AddSheetToPanel(FormatLocation formatLocation, bool selectSheet = false)
        {
            Panel sheetPanel = this.CreateSheetPanel();
            this.panelFormat.Controls.Add((Control)sheetPanel);
            ListViewItem listViewItem = this.CreateListViewItem(formatLocation);
            SheetPanelLocation sheetPanelLocation = new SheetPanelLocation(sheetPanel, listViewItem, formatLocation);
            listViewItem.Tag = (object)sheetPanelLocation;
            sheetPanel.Tag = (object)sheetPanelLocation;
            if (!selectSheet)
                return;
            listViewItem.Selected = true;
        }

        private double CalcRatio()
        {
            KnownPaperFormat format = KnownPaperFormats.GetFormat(this.comboBoxLayoutFormat.SelectedItem.ToString());
            return Math.Floor(((double)format.WidthF <= (double)format.HeightF ? (double)(this.panelLayoutEditor.Height - this.panelFormat.Location.Y * 2 - SystemInformation.HorizontalScrollBarHeight) / (double)format.HeightF : (double)(this.panelLayoutEditor.Width - this.panelFormat.Location.X * 2 - SystemInformation.VerticalScrollBarWidth) / (double)format.WidthF) * 1000.0) / 1000.0;
        }

        private void CheckSaveButton()
        {
            this.toolStripButtonSave.Enabled = this.listViewLayoutSheets.Items.Count > 0 && this.panelFormat.Controls.OfType<Panel>().Count<Panel>() > 0 && this.LayoutChanged();
        }

        private ListViewItem CreateListViewItem(FormatLocation formatLocation)
        {
            ListViewItem listViewItem = this.listViewLayoutSheets.Items.Add($"{formatLocation.Left}; {formatLocation.Top}");
            listViewItem.SubItems.Add(formatLocation.Format.FullName);
            listViewItem.SubItems.Add($"{formatLocation.Format.Width}x{formatLocation.Format.Height}");
            listViewItem.SubItems.Add(formatLocation.IsRotate.ToString());
            return listViewItem;
        }

        private Panel CreateSheetPanel()
        {
            Panel sheetPanel = new Panel();
            sheetPanel.BackColor = Color.LightGray;
            sheetPanel.BorderStyle = BorderStyle.FixedSingle;
            sheetPanel.TabStop = true;
            sheetPanel.Click += new EventHandler(this.PanelPage_Click);
            return sheetPanel;
        }

        private void EnableSheetConnectedControls()
        {
            bool flag = this.listViewLayoutSheets.SelectedItems.Count != 0;
            this.buttonDeleteSheet.Enabled = flag;
            this.pageLeftUpDown.Enabled = flag;
            this.pageTopUpDown.Enabled = flag;
        }

        private LayoutDescriptor GetCurrentLayout()
        {
            LayoutDescriptor currentLayout = new LayoutDescriptor()
            {
                Caption = this.textBoxLayoutName.Text
            };
            if (this.comboBoxLayoutFormat.SelectedItem == null)
                return currentLayout;
            currentLayout.SetMainFormat(this.comboBoxLayoutFormat.SelectedItem.ToString());
            foreach (Control control in this.panelFormat.Controls.OfType<Panel>())
            {
                if (control.Tag is SheetPanelLocation tag)
                {
                    FormatLocation formatLocation = tag.FormatLocation;
                    currentLayout.InternalFormats.Add(formatLocation);
                }
            }
            return currentLayout;
        }

        private bool LayoutChanged() => !this.InitPdfLayout.Equals((object)this.GetCurrentLayout());

        private void OpenLayout()
        {
            object layoutId = this.layoutSettingsService.ChooseLayout();
            if (layoutId == null)
                return;
            this._layoutId = layoutId;
            this.SetLayout(this.layoutSettingsService.LoadLayout(layoutId));
        }

        private void SaveLayout(bool saveAsNew = true)
        {
            if (saveAsNew)
                this._layoutId = (object)null;
            object obj = this.layoutSettingsService.SaveLayout(this.GetCurrentLayout(), this._layoutId);
            if (obj == null)
                return;
            this._layoutId = obj;
            if (!saveAsNew && this.InitPdfLayout.Caption != "" && this.InitPdfLayout.Caption != this.textBoxLayoutName.Text)
                this.RenamedLayouts.Add(new RenamedLayout(this.InitPdfLayout.Caption, this.textBoxLayoutName.Text));
            this.InitPdfLayout = this.GetCurrentLayout();
        }

        private void ScalePanelFormat()
        {
            if (this.comboBoxLayoutFormat.SelectedItem == null)
                return;
            LayoutDescriptor currentLayout = this.GetCurrentLayout();
            double num = this.CalcRatio();
            this.panelFormat.Width = Convert.ToInt32((double)currentLayout.WidthF * num);
            this.panelFormat.Height = Convert.ToInt32((double)currentLayout.HeightF * num);
            foreach (Panel panel in this.panelFormat.Controls.OfType<Panel>())
            {
                if (panel.Tag is SheetPanelLocation tag)
                {
                    FormatLocation formatLocation = tag.FormatLocation;
                    panel.Location = new Point(Convert.ToInt32(Math.Ceiling(formatLocation.LeftD * num)), Convert.ToInt32(Math.Ceiling(formatLocation.TopD * num)));
                    panel.Size = new Size(Convert.ToInt32(Math.Ceiling((double)formatLocation.Format.WidthF * num)), Convert.ToInt32(Math.Ceiling((double)formatLocation.Format.HeightF * num)));
                }
            }
        }

        private void SetActivePanel(Panel panel)
        {
            if (this.ActivePanel == panel || this.ActivePanel == null)
                return;
            this.ActivePanel.BackColor = Color.LightGray;
            this.ActivePanel = (Panel)null;
            panel.BackColor = Color.LightSkyBlue;
            SheetPanelLocation tag = panel.Tag as SheetPanelLocation;
            this.ActivePanel = panel;
            tag.ListViewItem.Selected = true;
        }

        private void SetInternalFormats(List<FormatLocation> internalFormats)
        {
            this.panelFormat.Controls.Clear();
            this.listViewLayoutSheets.Items.Clear();
            foreach (FormatLocation internalFormat in internalFormats)
                this.AddSheetToPanel(new FormatLocation()
                {
                    Format = internalFormat.Format,
                    Left = internalFormat.Left,
                    Top = internalFormat.Top
                });
            this.ScalePanelFormat();
        }

        private void SetLayout(LayoutDescriptor layout)
        {
            if (layout.IsLoaded)
            {
                this.InitPdfLayout = layout;
                this.textBoxLayoutName.Text = this.InitPdfLayout.Caption;
                this.comboBoxLayoutFormat.SelectedIndex = this.comboBoxLayoutFormat.Items.IndexOf((object)this.InitPdfLayout.MainFormat);
                this.SetInternalFormats(this.InitPdfLayout.InternalFormats);
            }
            else
            {
                int num = (int)MessageBox.Show("Не удалось загрузить макет", "Ошибка");
            }
        }

        private void SetNextSelectedListItem(int index)
        {
            if (this.listViewLayoutSheets.Items.Count == 0)
                return;
            if (index < this.listViewLayoutSheets.Items.Count)
                this.listViewLayoutSheets.Items[index].Selected = true;
            else
                this.listViewLayoutSheets.Items[index - 1].Selected = true;
        }

        private void SetNewLayout()
        {
            this.InitializeTextBoxLayoutName();
            this.InitializePdfLayout();
            this.InitializeSheetLocationUpDowns();
            this.InitializeActivePanel();
            this.panelFormat.Controls.Clear();
            this.listViewLayoutSheets.Items.Clear();
            this._layoutId = (object)null;
            this.CheckSaveButton();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.toolStripMenu = new ToolStrip();
            this.toolStripButtonNew = new ToolStripButton();
            this.toolStripButtonOpen = new ToolStripButton();
            this.toolStripButtonSave = new ToolStripButton();
            this.EdiorContainer = new SplitContainer();
            this.groupBoxSheetLocation = new GroupBox();
            this.pageTopUpDown = new NumericUpDown();
            this.label3 = new Label();
            this.label4 = new Label();
            this.pageLeftUpDown = new NumericUpDown();
            this.groupBoxSheetsOnLayout = new GroupBox();
            this.buttonAuto = new Button();
            this.buttonDeleteSheet = new Button();
            this.listViewLayoutSheets = new ListView();
            this.columnPosition = new ColumnHeader();
            this.columnFormat = new ColumnHeader();
            this.columnSize = new ColumnHeader();
            this.buttonAddSheet = new Button();
            this.groupBoxLayoutParameters = new GroupBox();
            this.labelFormat = new Label();
            this.textBoxLayoutName = new TextBox();
            this.labelLayoutName = new Label();
            this.comboBoxLayoutFormat = new ComboBox();
            this.panelLayoutEditor = new Panel();
            this.panelFormat = new Panel();
            this.toolStripMenu.SuspendLayout();
            this.EdiorContainer.BeginInit();
            this.EdiorContainer.Panel1.SuspendLayout();
            this.EdiorContainer.Panel2.SuspendLayout();
            this.EdiorContainer.SuspendLayout();
            this.groupBoxSheetLocation.SuspendLayout();
            this.pageTopUpDown.BeginInit();
            this.pageLeftUpDown.BeginInit();
            this.groupBoxSheetsOnLayout.SuspendLayout();
            this.groupBoxLayoutParameters.SuspendLayout();
            this.panelLayoutEditor.SuspendLayout();
            this.SuspendLayout();
            this.toolStripMenu.Items.AddRange(new ToolStripItem[3]
            {
          (ToolStripItem) this.toolStripButtonNew,
          (ToolStripItem) this.toolStripButtonOpen,
          (ToolStripItem) this.toolStripButtonSave
            });
            this.toolStripMenu.Location = new Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new Size(807, 25);
            this.toolStripMenu.TabIndex = 0;
            this.toolStripMenu.Text = "toolStrip1";
            this.toolStripButtonNew.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = (Image)Resources.PNG_New;
            this.toolStripButtonNew.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new Size(23, 22);
            this.toolStripButtonNew.Text = "Новый макет";
            this.toolStripButtonNew.Click += new EventHandler(this.ToolStripButtonNew_Click);
            this.toolStripButtonOpen.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = (Image)Resources.PNG_Open;
            this.toolStripButtonOpen.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new Size(23, 22);
            this.toolStripButtonOpen.Text = "Открыть";
            this.toolStripButtonOpen.Click += new EventHandler(this.ToolStripButtonOpen_Click);
            this.toolStripButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = (Image)Resources.PNG_Save;
            this.toolStripButtonSave.ImageTransparentColor = Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new Size(23, 22);
            this.toolStripButtonSave.Text = "Сохранить";
            this.toolStripButtonSave.Click += new EventHandler(this.ToolStripButtonSave_Click);
            this.EdiorContainer.Dock = DockStyle.Fill;
            this.EdiorContainer.Location = new Point(0, 25);
            this.EdiorContainer.Name = "EdiorContainer";
            this.EdiorContainer.Panel1.Controls.Add((Control)this.groupBoxSheetLocation);
            this.EdiorContainer.Panel1.Controls.Add((Control)this.groupBoxSheetsOnLayout);
            this.EdiorContainer.Panel1.Controls.Add((Control)this.groupBoxLayoutParameters);
            this.EdiorContainer.Panel1MinSize = 276;
            this.EdiorContainer.Panel2.BackColor = SystemColors.Control;
            this.EdiorContainer.Panel2.Controls.Add((Control)this.panelLayoutEditor);
            this.EdiorContainer.Panel2MinSize = 400;
            this.EdiorContainer.Size = new Size(807, 608);
            this.EdiorContainer.SplitterDistance = 313;
            this.EdiorContainer.TabIndex = 1;
            this.groupBoxSheetLocation.Controls.Add((Control)this.pageTopUpDown);
            this.groupBoxSheetLocation.Controls.Add((Control)this.label3);
            this.groupBoxSheetLocation.Controls.Add((Control)this.label4);
            this.groupBoxSheetLocation.Controls.Add((Control)this.pageLeftUpDown);
            this.groupBoxSheetLocation.Dock = DockStyle.Bottom;
            this.groupBoxSheetLocation.Location = new Point(0, 509);
            this.groupBoxSheetLocation.Name = "groupBoxSheetLocation";
            this.groupBoxSheetLocation.Size = new Size(313, 99);
            this.groupBoxSheetLocation.TabIndex = 2;
            this.groupBoxSheetLocation.TabStop = false;
            this.groupBoxSheetLocation.Text = "Расположение листа";
            this.pageTopUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.pageTopUpDown.Location = new Point(90, 49);
            this.pageTopUpDown.Maximum = new Decimal(new int[4]
            {
          10000,
          0,
          0,
          0
            });
            this.pageTopUpDown.Name = "pageTopUpDown";
            this.pageTopUpDown.Size = new Size(49, 20);
            this.pageTopUpDown.TabIndex = 14;
            this.pageTopUpDown.ValueChanged += new EventHandler(this.UpDownPage_ValueChanged);
            this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(66, 51);
            this.label3.Name = "label3";
            this.label3.Size = new Size(17, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y:";
            this.label4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(65, 28);
            this.label4.Name = "label4";
            this.label4.Size = new Size(17, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "X:";
            this.pageLeftUpDown.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.pageLeftUpDown.Location = new Point(90, 26);
            this.pageLeftUpDown.Maximum = new Decimal(new int[4]
            {
          10000,
          0,
          0,
          0
            });
            this.pageLeftUpDown.Name = "pageLeftUpDown";
            this.pageLeftUpDown.Size = new Size(49, 20);
            this.pageLeftUpDown.TabIndex = 11;
            this.pageLeftUpDown.ValueChanged += new EventHandler(this.UpDownPage_ValueChanged);
            this.groupBoxSheetsOnLayout.Controls.Add((Control)this.buttonAuto);
            this.groupBoxSheetsOnLayout.Controls.Add((Control)this.buttonDeleteSheet);
            this.groupBoxSheetsOnLayout.Controls.Add((Control)this.listViewLayoutSheets);
            this.groupBoxSheetsOnLayout.Controls.Add((Control)this.buttonAddSheet);
            this.groupBoxSheetsOnLayout.Dock = DockStyle.Fill;
            this.groupBoxSheetsOnLayout.Location = new Point(0, 105);
            this.groupBoxSheetsOnLayout.Name = "groupBoxSheetsOnLayout";
            this.groupBoxSheetsOnLayout.Size = new Size(313, 503);
            this.groupBoxSheetsOnLayout.TabIndex = 1;
            this.groupBoxSheetsOnLayout.TabStop = false;
            this.groupBoxSheetsOnLayout.Text = "Листы на странице";
            this.buttonAuto.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonAuto.Location = new Point(6, 373);
            this.buttonAuto.Name = "buttonAuto";
            this.buttonAuto.Size = new Size(95, 25);
            this.buttonAuto.TabIndex = 4;
            this.buttonAuto.Text = "Авто";
            this.buttonAuto.UseVisualStyleBackColor = true;
            this.buttonAuto.Click += new EventHandler(this.ButtonAuto_Click);
            this.buttonDeleteSheet.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonDeleteSheet.Location = new Point(111, 373);
            this.buttonDeleteSheet.Name = "buttonDeleteSheet";
            this.buttonDeleteSheet.Size = new Size(95, 25);
            this.buttonDeleteSheet.TabIndex = 3;
            this.buttonDeleteSheet.Text = "Удалить";
            this.buttonDeleteSheet.UseVisualStyleBackColor = true;
            this.buttonDeleteSheet.Click += new EventHandler(this.ButtonDeleteSheet_Click);
            this.listViewLayoutSheets.Activation = ItemActivation.OneClick;
            this.listViewLayoutSheets.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.listViewLayoutSheets.Columns.AddRange(new ColumnHeader[3]
            {
          this.columnPosition,
          this.columnFormat,
          this.columnSize
            });
            this.listViewLayoutSheets.FullRowSelect = true;
            this.listViewLayoutSheets.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.listViewLayoutSheets.HideSelection = false;
            this.listViewLayoutSheets.Location = new Point(12, 19);
            this.listViewLayoutSheets.MultiSelect = false;
            this.listViewLayoutSheets.Name = "listViewLayoutSheets";
            this.listViewLayoutSheets.Size = new Size(288, 347);
            this.listViewLayoutSheets.TabIndex = 0;
            this.listViewLayoutSheets.UseCompatibleStateImageBehavior = false;
            this.listViewLayoutSheets.View = View.Details;
            this.listViewLayoutSheets.ItemSelectionChanged += new ListViewItemSelectionChangedEventHandler(this.ListViewLayoutSheets_ItemSelectionChanged);
            this.columnPosition.Text = "Позиция";
            this.columnFormat.Text = "Формат";
            this.columnSize.Text = "Размер";
            this.buttonAddSheet.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonAddSheet.Location = new Point(212, 373);
            this.buttonAddSheet.Name = "buttonAddSheet";
            this.buttonAddSheet.Size = new Size(95, 25);
            this.buttonAddSheet.TabIndex = 2;
            this.buttonAddSheet.Text = "Добавить";
            this.buttonAddSheet.UseVisualStyleBackColor = true;
            this.buttonAddSheet.Click += new EventHandler(this.ButtonAddSheet_Click);
            this.groupBoxLayoutParameters.Controls.Add((Control)this.labelFormat);
            this.groupBoxLayoutParameters.Controls.Add((Control)this.textBoxLayoutName);
            this.groupBoxLayoutParameters.Controls.Add((Control)this.labelLayoutName);
            this.groupBoxLayoutParameters.Controls.Add((Control)this.comboBoxLayoutFormat);
            this.groupBoxLayoutParameters.Dock = DockStyle.Top;
            this.groupBoxLayoutParameters.Location = new Point(0, 0);
            this.groupBoxLayoutParameters.Name = "groupBoxLayoutParameters";
            this.groupBoxLayoutParameters.Size = new Size(313, 105);
            this.groupBoxLayoutParameters.TabIndex = 0;
            this.groupBoxLayoutParameters.TabStop = false;
            this.groupBoxLayoutParameters.Text = "Параметры макета";
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new Point(15, 74);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new Size(52, 13);
            this.labelFormat.TabIndex = 11;
            this.labelFormat.Text = "Формат:";
            this.textBoxLayoutName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxLayoutName.Location = new Point(18, 39);
            this.textBoxLayoutName.Name = "textBoxLayoutName";
            this.textBoxLayoutName.Size = new Size(279, 20);
            this.textBoxLayoutName.TabIndex = 9;
            this.textBoxLayoutName.TextChanged += new EventHandler(this.TextBoxLayoutName_TextChanged);
            this.labelLayoutName.AutoSize = true;
            this.labelLayoutName.Location = new Point(15, 23);
            this.labelLayoutName.Name = "labelLayoutName";
            this.labelLayoutName.Size = new Size(60, 13);
            this.labelLayoutName.TabIndex = 8;
            this.labelLayoutName.Text = "Название:";
            this.comboBoxLayoutFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxLayoutFormat.FormattingEnabled = true;
            this.comboBoxLayoutFormat.Items.AddRange(new object[6]
            {
          (object) "A0",
          (object) "A1",
          (object) "A2",
          (object) "A3",
          (object) "A4",
          (object) "A5"
            });
            this.comboBoxLayoutFormat.Location = new Point(73, 71);
            this.comboBoxLayoutFormat.Name = "comboBoxLayoutFormat";
            this.comboBoxLayoutFormat.Size = new Size(187, 21);
            this.comboBoxLayoutFormat.TabIndex = 1;
            this.comboBoxLayoutFormat.SelectedIndexChanged += new EventHandler(this.ComboBoxLayoutFormat_SelectedIndexChanged);
            this.panelLayoutEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panelLayoutEditor.AutoScroll = true;
            this.panelLayoutEditor.BackColor = SystemColors.ControlDarkDark;
            this.panelLayoutEditor.Controls.Add((Control)this.panelFormat);
            this.panelLayoutEditor.Location = new Point(0, 0);
            this.panelLayoutEditor.Name = "panelLayoutEditor";
            this.panelLayoutEditor.Size = new Size(490, 608);
            this.panelLayoutEditor.TabIndex = 1;
            this.panelLayoutEditor.Resize += new EventHandler(this.PanelLayoutEditor_Resize);
            this.panelFormat.BackColor = Color.White;
            this.panelFormat.BorderStyle = BorderStyle.FixedSingle;
            this.panelFormat.Location = new Point(3, 3);
            this.panelFormat.Name = "panelFormat";
            this.panelFormat.Size = new Size(200, 244);
            this.panelFormat.TabIndex = 0;
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(807, 633);
            this.Controls.Add((Control)this.EdiorContainer);
            this.Controls.Add((Control)this.toolStripMenu);
            this.MinimumSize = new Size(823, 672);
            this.Name = nameof(LayoutEditor);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Редактор макетов";
            this.FormClosing += new FormClosingEventHandler(this.LayoutEditor_FormClosing);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.EdiorContainer.Panel1.ResumeLayout(false);
            this.EdiorContainer.Panel2.ResumeLayout(false);
            this.EdiorContainer.EndInit();
            this.EdiorContainer.ResumeLayout(false);
            this.groupBoxSheetLocation.ResumeLayout(false);
            this.groupBoxSheetLocation.PerformLayout();
            this.pageTopUpDown.EndInit();
            this.pageLeftUpDown.EndInit();
            this.groupBoxSheetsOnLayout.ResumeLayout(false);
            this.groupBoxLayoutParameters.ResumeLayout(false);
            this.groupBoxLayoutParameters.PerformLayout();
            this.panelLayoutEditor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
