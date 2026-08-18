
// Type: Intermech.Client.Core.SaveToDiskClassifierForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Holders;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class SaveToDiskClassifierForm : Form
{
  /// <summary>куда сохраняем</summary>
  private string saveFolder = string.Empty;
  /// <summary>только базовые версии</summary>
  private bool baseVersionsOnly = true;
  private FoldersRecentHolder frh = new FoldersRecentHolder();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private Label lbFolder;
  private CheckBox cbBaseVersionsOnly;
  private FolderBrowserDialog fbSave;
  private ComboBox cbFolderPath;
  private Button btnSelectFolder;

  /// <summary>куда сохраняем файлы</summary>
  public string Folder
  {
    get => this.saveFolder;
    set => this.saveFolder = value;
  }

  /// <summary>куда сохраняем файлы</summary>
  public bool BaseVersions
  {
    get => this.baseVersionsOnly;
    set => this.baseVersionsOnly = value;
  }

  public SaveToDiskClassifierForm() => this.InitializeComponent();

  private void InitFolderPathRecents()
  {
    this.frh.Load();
    this.cbFolderPath.Items.Clear();
    for (int index = 0; index < this.frh.ParamValues.Count; ++index)
      this.cbFolderPath.Items.Add((object) this.frh.ParamValues[index]);
  }

  private void AddToRecentsAndSave(string recentText)
  {
    for (int index = 0; index < this.frh.ParamValues.Count; ++index)
    {
      if (this.frh.ParamValues[index].Equals(recentText, StringComparison.InvariantCultureIgnoreCase))
        return;
    }
    if (this.frh.ParamValues.Count == 0)
      this.frh.ParamValues.Add(recentText);
    else
      this.frh.ParamValues.Insert(0, recentText);
    this.frh.Save();
  }

  private void UpdateControls() => this.btnOK.Enabled = this.saveFolder != string.Empty;

  private void SaveToDiskClassifierForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.cbFolderPath.Text = this.saveFolder;
    this.cbBaseVersionsOnly.Checked = this.baseVersionsOnly;
    this.InitFolderPathRecents();
    this.UpdateControls();
  }

  private void SaveToDiskClassifierForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    this.saveFolder = this.cbFolderPath.Text;
    this.baseVersionsOnly = this.cbBaseVersionsOnly.Checked;
    this.AddToRecentsAndSave(this.cbFolderPath.Text);
  }

  private void btnSelectFolder_Click(object sender, EventArgs e)
  {
    if (this.fbSave.ShowDialog() == DialogResult.OK)
      this.cbFolderPath.Text = this.saveFolder = this.fbSave.SelectedPath;
    this.UpdateControls();
  }

  private void cbFolderPath_SelectedValueChanged(object sender, EventArgs e)
  {
    this.saveFolder = this.cbFolderPath.Text;
    this.UpdateControls();
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
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.lbFolder = new Label();
    this.cbBaseVersionsOnly = new CheckBox();
    this.fbSave = new FolderBrowserDialog();
    this.cbFolderPath = new ComboBox();
    this.btnSelectFolder = new Button();
    this.SuspendLayout();
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Enabled = false;
    this.btnOK.ImeMode = ImeMode.NoControl;
    this.btnOK.Location = new Point(224 /*0xE0*/, 90);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(121, 27);
    this.btnOK.TabIndex = 4;
    this.btnOK.Text = "Сохранить";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(351, 90);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 3;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.lbFolder.AutoSize = true;
    this.lbFolder.ImeMode = ImeMode.NoControl;
    this.lbFolder.Location = new Point(9, 12);
    this.lbFolder.Name = "lbFolder";
    this.lbFolder.Size = new Size(158, 13);
    this.lbFolder.TabIndex = 14;
    this.lbFolder.Text = "Путь для сохранения файлов:";
    this.cbBaseVersionsOnly.AutoSize = true;
    this.cbBaseVersionsOnly.Checked = true;
    this.cbBaseVersionsOnly.CheckState = CheckState.Checked;
    this.cbBaseVersionsOnly.ImeMode = ImeMode.NoControl;
    this.cbBaseVersionsOnly.Location = new Point(12, 54);
    this.cbBaseVersionsOnly.Name = "cbBaseVersionsOnly";
    this.cbBaseVersionsOnly.Size = new Size(212, 17);
    this.cbBaseVersionsOnly.TabIndex = 16 /*0x10*/;
    this.cbBaseVersionsOnly.Text = "Только базовые версии документов";
    this.cbBaseVersionsOnly.UseVisualStyleBackColor = true;
    this.cbFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.cbFolderPath.FormattingEnabled = true;
    this.cbFolderPath.Location = new Point(12, 28);
    this.cbFolderPath.Name = "cbFolderPath";
    this.cbFolderPath.Size = new Size(379, 21);
    this.cbFolderPath.TabIndex = 17;
    this.cbFolderPath.SelectedValueChanged += new EventHandler(this.cbFolderPath_SelectedValueChanged);
    this.cbFolderPath.TextChanged += new EventHandler(this.cbFolderPath_SelectedValueChanged);
    this.btnSelectFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnSelectFolder.Location = new Point(397, 28);
    this.btnSelectFolder.Name = "btnSelectFolder";
    this.btnSelectFolder.Size = new Size(75, 23);
    this.btnSelectFolder.TabIndex = 18;
    this.btnSelectFolder.Text = "Выбрать";
    this.btnSelectFolder.UseVisualStyleBackColor = true;
    this.btnSelectFolder.Click += new EventHandler(this.btnSelectFolder_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(484, 120);
    this.Controls.Add((Control) this.btnSelectFolder);
    this.Controls.Add((Control) this.cbFolderPath);
    this.Controls.Add((Control) this.lbFolder);
    this.Controls.Add((Control) this.cbBaseVersionsOnly);
    this.Controls.Add((Control) this.btnOK);
    this.Controls.Add((Control) this.btnCancel);
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.MinimumSize = new Size(500, 160 /*0xA0*/);
    this.Name = nameof (SaveToDiskClassifierForm);
    this.Text = "Сохранить на диск";
    this.FormClosed += new FormClosedEventHandler(this.SaveToDiskClassifierForm_FormClosed);
    this.Load += new EventHandler(this.SaveToDiskClassifierForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
