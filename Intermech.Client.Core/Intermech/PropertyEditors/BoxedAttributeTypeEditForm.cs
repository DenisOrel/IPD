
// Type: Intermech.PropertyEditors.BoxedAttributeTypeEditForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

public class BoxedAttributeTypeEditForm : Form
{
  private FileTypes selectedFileType;
  private bool blockOnCheck;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnOK;
  private Button btnCancel;
  private CheckedListBox checkedLB;
  private Label AttributeNameLabel;

  public FileTypes SelectedFileType
  {
    get => this.selectedFileType;
    set => this.selectedFileType = value;
  }

  public BoxedAttributeTypeEditForm() => this.InitializeComponent();

  private void FileAttributeTypeEditForm_Load(object sender, EventArgs e)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="isReadonly">вырезать тип "Файл объекта"</param>
  private void InitFileTypesList(bool isReadonly)
  {
    this.checkedLB.Items.Clear();
    ArrayList arrayList = new ArrayList((ICollection) Enum.GetValues(typeof (FileTypes)));
    for (int index = 0; index < arrayList.Count; ++index)
    {
      if (!isReadonly || isReadonly && (FileTypes) arrayList[index] != FileTypes.ftNormal)
        this.checkedLB.Items.Add((object) new FileTypePropertyClass((FileTypes) arrayList[index]), (FileTypes) arrayList[index] == this.selectedFileType);
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeName">title</param>
  /// <param name="isReadOnly">если true, то не давать на выбор тип "Файл объекта"</param>
  /// <returns></returns>
  public DialogResult ShowDialog(string attributeName, bool isReadOnly)
  {
    this.InitFileTypesList(isReadOnly);
    this.AttributeNameLabel.Text = attributeName;
    return base.ShowDialog();
  }

  public new DialogResult ShowDialog()
  {
    this.AttributeNameLabel.Text = string.Empty;
    return base.ShowDialog();
  }

  private void btnOK_Click(object sender, EventArgs e)
  {
    if (this.checkedLB.CheckedItems.Count == 0)
      this.DialogResult = DialogResult.None;
    else
      this.selectedFileType = ((FileTypePropertyClass) this.checkedLB.CheckedItems[0]).FileType;
  }

  private void checkedLB_ItemCheck(object sender, ItemCheckEventArgs e)
  {
    if (this.blockOnCheck || e.NewValue != CheckState.Checked)
      return;
    this.blockOnCheck = true;
    try
    {
      for (int index = 0; index < this.checkedLB.Items.Count; ++index)
      {
        if (index != e.Index && this.checkedLB.GetItemCheckState(index) == CheckState.Checked)
          this.checkedLB.SetItemCheckState(index, CheckState.Unchecked);
      }
    }
    finally
    {
      this.blockOnCheck = false;
    }
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
    this.checkedLB = new CheckedListBox();
    this.AttributeNameLabel = new Label();
    this.SuspendLayout();
    this.btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOK.DialogResult = DialogResult.OK;
    this.btnOK.Location = new Point(126, 141);
    this.btnOK.Name = "btnOK";
    this.btnOK.Size = new Size(75, 23);
    this.btnOK.TabIndex = 0;
    this.btnOK.Text = "OK";
    this.btnOK.UseVisualStyleBackColor = true;
    this.btnOK.Click += new EventHandler(this.btnOK_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(207, 141);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.checkedLB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.checkedLB.CheckOnClick = true;
    this.checkedLB.FormattingEnabled = true;
    this.checkedLB.Location = new Point(12, 27);
    this.checkedLB.Name = "checkedLB";
    this.checkedLB.Size = new Size(269, 94);
    this.checkedLB.TabIndex = 2;
    this.checkedLB.ItemCheck += new ItemCheckEventHandler(this.checkedLB_ItemCheck);
    this.AttributeNameLabel.AutoSize = true;
    this.AttributeNameLabel.Location = new Point(15, 10);
    this.AttributeNameLabel.Name = "AttributeNameLabel";
    this.AttributeNameLabel.Size = new Size(47, 13);
    this.AttributeNameLabel.TabIndex = 3;
    this.AttributeNameLabel.Text = "Атрибут";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(293, 173);
    this.Controls.Add((Control) this.AttributeNameLabel);
    this.Controls.Add((Control) this.checkedLB);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOK);
    this.Name = nameof (BoxedAttributeTypeEditForm);
    this.Text = "Выбор типа файла";
    this.Load += new EventHandler(this.FileAttributeTypeEditForm_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
