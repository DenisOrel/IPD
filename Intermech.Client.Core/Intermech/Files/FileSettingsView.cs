
// Type: Intermech.Files.FileSettingsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Mvp;
using Intermech.Mvp.Winforms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Files;

internal class FileSettingsView : MvpUserControl, IFileSettingsView, IView
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tlpMainGrid;
  private TableLayoutPanel tableLayoutPanel1;
  private Label lbDriveLetter;
  private ComboBox ddDriveLetter;
  private Label lbSymlinkFolder;
  private TextBox tbSymlinkFolder;
  private GroupBox gbImportOptions;
  private CheckBox cbLeaveSourcesOfImportedFiles;

  public FileSettingsView() => this.InitializeComponent();

  private void OnPageItemChanged(object sender, EventArgs e) => this.RaisePageChanged();

  private void RaisePageChanged()
  {
    if (this.EditableStateChanged == null)
      return;
    this.EditableStateChanged((object) this, EventArgs.Empty);
  }

  char IFileSettingsView.DriveLetter
  {
    get
    {
      return this.ddDriveLetter.SelectedIndex > 0 ? ((string) this.ddDriveLetter.SelectedItem)[0] : char.MinValue;
    }
    set
    {
      if (value == char.MinValue)
      {
        this.ddDriveLetter.SelectedIndex = 0;
      }
      else
      {
        string strA = value.ToString();
        for (int index = 1; index < this.ddDriveLetter.Items.Count; ++index)
        {
          if (string.Compare(strA, (string) this.ddDriveLetter.Items[index], true) == 0)
          {
            this.ddDriveLetter.SelectedIndex = index;
            break;
          }
        }
      }
    }
  }

  string IFileSettingsView.SymlinkFolder
  {
    get => this.tbSymlinkFolder.Text;
    set => this.tbSymlinkFolder.Text = value;
  }

  bool IFileSettingsView.LeaveSourcesOfImportedFiles
  {
    get => this.cbLeaveSourcesOfImportedFiles.Checked;
    set => this.cbLeaveSourcesOfImportedFiles.Checked = value;
  }

  void IFileSettingsView.AttachPageChangedHandlers()
  {
    this.ddDriveLetter.SelectedIndexChanged += new EventHandler(this.OnPageItemChanged);
    this.tbSymlinkFolder.TextChanged += new EventHandler(this.OnPageItemChanged);
    this.cbLeaveSourcesOfImportedFiles.CheckedChanged += new EventHandler(this.OnPageItemChanged);
  }

  void IFileSettingsView.DetachPageChangedHandlers()
  {
    this.ddDriveLetter.SelectedIndexChanged -= new EventHandler(this.OnPageItemChanged);
    this.tbSymlinkFolder.TextChanged -= new EventHandler(this.OnPageItemChanged);
    this.cbLeaveSourcesOfImportedFiles.CheckedChanged -= new EventHandler(this.OnPageItemChanged);
  }

  void IFileSettingsView.EnableDriveLetter(bool enabled) => this.ddDriveLetter.Enabled = enabled;

  void IFileSettingsView.EnableSymlinkFolder(bool enabled)
  {
    this.tbSymlinkFolder.Enabled = enabled;
  }

  void IFileSettingsView.EnableImportOptions(bool enabled)
  {
    this.gbImportOptions.Enabled = enabled;
  }

  /// <summary>Событие изменения какого-либо элемента управления.</summary>
  public event EventHandler EditableStateChanged;

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
    this.tlpMainGrid = new TableLayoutPanel();
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this.lbDriveLetter = new Label();
    this.ddDriveLetter = new ComboBox();
    this.lbSymlinkFolder = new Label();
    this.tbSymlinkFolder = new TextBox();
    this.gbImportOptions = new GroupBox();
    this.cbLeaveSourcesOfImportedFiles = new CheckBox();
    this.tlpMainGrid.SuspendLayout();
    this.tableLayoutPanel1.SuspendLayout();
    this.gbImportOptions.SuspendLayout();
    this.SuspendLayout();
    this.tlpMainGrid.ColumnCount = 1;
    this.tlpMainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpMainGrid.Controls.Add((Control) this.tableLayoutPanel1, 0, 0);
    this.tlpMainGrid.Controls.Add((Control) this.gbImportOptions, 0, 1);
    this.tlpMainGrid.Dock = DockStyle.Fill;
    this.tlpMainGrid.Location = new Point(4, 4);
    this.tlpMainGrid.Margin = new Padding(0);
    this.tlpMainGrid.Name = "tlpMainGrid";
    this.tlpMainGrid.RowCount = 3;
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.RowStyles.Add(new RowStyle());
    this.tlpMainGrid.Size = new Size(581, 235);
    this.tlpMainGrid.TabIndex = 0;
    this.tableLayoutPanel1.AutoSize = true;
    this.tableLayoutPanel1.ColumnCount = 1;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.Controls.Add((Control) this.lbDriveLetter, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.ddDriveLetter, 0, 1);
    this.tableLayoutPanel1.Controls.Add((Control) this.lbSymlinkFolder, 0, 2);
    this.tableLayoutPanel1.Controls.Add((Control) this.tbSymlinkFolder, 0, 3);
    this.tableLayoutPanel1.Dock = DockStyle.Top;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Margin = new Padding(0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 4;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
    this.tableLayoutPanel1.Size = new Size(581, 121);
    this.tableLayoutPanel1.TabIndex = 1;
    this.lbDriveLetter.AutoSize = true;
    this.lbDriveLetter.Location = new Point(3, 3);
    this.lbDriveLetter.Margin = new Padding(3);
    this.lbDriveLetter.Name = "lbDriveLetter";
    this.lbDriveLetter.Padding = new Padding(0, 8, 0, 4);
    this.lbDriveLetter.Size = new Size(352, 25);
    this.lbDriveLetter.TabIndex = 0;
    this.lbDriveLetter.Text = "Буква диска для подключения файлового хранилища пользователя";
    this.ddDriveLetter.DropDownStyle = ComboBoxStyle.DropDownList;
    this.ddDriveLetter.FormattingEnabled = true;
    this.ddDriveLetter.Items.AddRange(new object[27]
    {
      (object) "<Не задана>",
      (object) "A",
      (object) "B",
      (object) "C",
      (object) "D",
      (object) "E",
      (object) "F",
      (object) "G",
      (object) "H",
      (object) "I",
      (object) "J",
      (object) "K",
      (object) "L",
      (object) "M",
      (object) "N",
      (object) "O",
      (object) "P",
      (object) "Q",
      (object) "R",
      (object) "S",
      (object) "T",
      (object) "U",
      (object) "V",
      (object) "W",
      (object) "X",
      (object) "Y",
      (object) "Z"
    });
    this.ddDriveLetter.Location = new Point(5, 35);
    this.ddDriveLetter.Margin = new Padding(5, 4, 3, 5);
    this.ddDriveLetter.Name = "ddDriveLetter";
    this.ddDriveLetter.Size = new Size(102, 21);
    this.ddDriveLetter.TabIndex = 1;
    this.lbSymlinkFolder.AutoSize = true;
    this.lbSymlinkFolder.Location = new Point(3, 64 /*0x40*/);
    this.lbSymlinkFolder.Margin = new Padding(3);
    this.lbSymlinkFolder.Name = "lbSymlinkFolder";
    this.lbSymlinkFolder.Padding = new Padding(0, 8, 0, 4);
    this.lbSymlinkFolder.Size = new Size(454, 25);
    this.lbSymlinkFolder.TabIndex = 2;
    this.lbSymlinkFolder.Text = "Имя папки в \"Моих документах\" для подключения файлового хранилища пользователя";
    this.tbSymlinkFolder.Location = new Point(5, 96 /*0x60*/);
    this.tbSymlinkFolder.Margin = new Padding(5, 4, 3, 5);
    this.tbSymlinkFolder.Name = "tbSymlinkFolder";
    this.tbSymlinkFolder.Size = new Size(183, 20);
    this.tbSymlinkFolder.TabIndex = 3;
    this.gbImportOptions.AutoSize = true;
    this.gbImportOptions.Controls.Add((Control) this.cbLeaveSourcesOfImportedFiles);
    this.gbImportOptions.Location = new Point(3, 133);
    this.gbImportOptions.Margin = new Padding(3, 12, 3, 3);
    this.gbImportOptions.Name = "gbImportOptions";
    this.gbImportOptions.Padding = new Padding(8, 8, 8, 4);
    this.gbImportOptions.Size = new Size(509, 69);
    this.gbImportOptions.TabIndex = 2;
    this.gbImportOptions.TabStop = false;
    this.gbImportOptions.Text = "Опции импорта файлов";
    this.cbLeaveSourcesOfImportedFiles.AutoSize = true;
    this.cbLeaveSourcesOfImportedFiles.Location = new Point(11, 24);
    this.cbLeaveSourcesOfImportedFiles.Name = "cbLeaveSourcesOfImportedFiles";
    this.cbLeaveSourcesOfImportedFiles.Padding = new Padding(0, 8, 0, 0);
    this.cbLeaveSourcesOfImportedFiles.Size = new Size(487, 25);
    this.cbLeaveSourcesOfImportedFiles.TabIndex = 0;
    this.cbLeaveSourcesOfImportedFiles.Text = "При перемещении импортируемых файлов в рабочую область оставлять исходные файлы";
    this.cbLeaveSourcesOfImportedFiles.UseVisualStyleBackColor = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpMainGrid);
    this.Margin = new Padding(8);
    this.Name = nameof (FileSettingsView);
    this.Padding = new Padding(4);
    this.Size = new Size(589, 243);
    this.tlpMainGrid.ResumeLayout(false);
    this.tlpMainGrid.PerformLayout();
    this.tableLayoutPanel1.ResumeLayout(false);
    this.tableLayoutPanel1.PerformLayout();
    this.gbImportOptions.ResumeLayout(false);
    this.gbImportOptions.PerformLayout();
    this.ResumeLayout(false);
  }
}
