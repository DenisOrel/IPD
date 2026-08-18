
// Type: Intermech.Client.Core.ChoosingFileForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Форма выбора файла документа</summary>
public class ChoosingFileForm : Form
{
  public ObjectFileInfo ChoosedObjectFileInfo = new ObjectFileInfo(BlobInformation.EmptyBlobInformation(), -1, 0L, string.Empty);
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel btnPanel;
  private Button btnOK;
  private Button btnCancel;
  private ListView listView;
  private Label label1;
  private Label label2;
  private ColumnHeader FileName;

  public ChoosingFileForm()
  {
    this.InitializeComponent();
    this.btnOK.Enabled = false;
  }

  public void Init(string objectName, List<ObjectFileInfo> filesInfo)
  {
    this.label2.Text = objectName;
    foreach (ObjectFileInfo objectFileInfo in filesInfo)
    {
      if (!(objectFileInfo.FileName == string.Empty))
        this.listView.Items.Add(new ListViewItem(objectFileInfo.FileName)
        {
          Tag = (object) objectFileInfo
        });
    }
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    this.ChoosedObjectFileInfo = this.listView.SelectedItems[0].Tag as ObjectFileInfo;
    this.Close();
  }

  private void btnCancel_Click(object sender, EventArgs e) => this.Close();

  private void listView_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.btnOK.Enabled = true;
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
    this.btnPanel = new Panel();
    this.btnOK = new Button();
    this.btnCancel = new Button();
    this.listView = new ListView();
    this.FileName = new ColumnHeader();
    this.label1 = new Label();
    this.label2 = new Label();
    this.btnPanel.SuspendLayout();
    this.SuspendLayout();
    this.btnPanel.Controls.Add((Control) this.btnOK);
    this.btnPanel.Controls.Add((Control) this.btnCancel);
    this.btnPanel.Dock = DockStyle.Bottom;
    this.btnPanel.Location = new Point(0, 199);
    this.btnPanel.Name = "btnPanel";
    this.btnPanel.Size = new Size(350, 39);
    this.btnPanel.TabIndex = 1;
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(114, 5);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(108, 27);
    this.btnOK.TabIndex = 19;
    this.btnOK.Text = "ОК";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(228, 5);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(115, 27);
    this.btnCancel.TabIndex = 18;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.FileName
    });
    this.listView.HideSelection = false;
    this.listView.Location = new Point(0, 34);
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Size = new Size(353, 164);
    this.listView.TabIndex = 2;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.SelectedIndexChanged += new EventHandler(this.listView_SelectedIndexChanged);
    this.FileName.Text = "Наименование файла";
    this.FileName.Width = 342;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(0, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(234, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Выберите сравниваемый файл для объекта:";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(0, 18);
    this.label2.Name = "label2";
    this.label2.Size = new Size(35, 13);
    this.label2.TabIndex = 4;
    this.label2.Text = "label2";
    this.AcceptButton = (IButtonControl) this.btnOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(350, 238);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.btnPanel);
    this.MinimumSize = new Size(366, 277);
    this.Name = nameof (ChoosingFileForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Выбор файла";
    this.btnPanel.ResumeLayout(false);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
