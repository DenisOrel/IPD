
// Type: Intermech.Client.Core.AuthFilesAskListForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core;

public class AuthFilesAskListForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnContinue;
  private Button btnCancel;
  private ListView listView;
  private ColumnHeader columnHeaderVersionId;
  private ColumnHeader columnHeaderCaption;

  public AuthFilesAskListForm() => this.InitializeComponent();

  public DialogResult ShowDialog(List<Tuple<long, int, string>> objList)
  {
    this.listView.BeginUpdate();
    this.listView.Items.Clear();
    for (int index = 0; index < objList.Count; ++index)
      this.listView.Items.Add(objList[index].Item1.ToString()).SubItems.Add(objList[index].Item3.ToString());
    this.listView.EndUpdate();
    return this.ShowDialog();
  }

  private void AuthFilesAskListForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.columnHeaderCaption.Width = -2;
  }

  private void AuthFilesAskListForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    this.btnContinue = new Button();
    this.btnCancel = new Button();
    this.listView = new ListView();
    this.columnHeaderVersionId = new ColumnHeader();
    this.columnHeaderCaption = new ColumnHeader();
    this.SuspendLayout();
    this.btnContinue.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnContinue.DialogResult = DialogResult.Yes;
    this.btnContinue.Location = new Point(292, 100);
    this.btnContinue.Name = "btnContinue";
    this.btnContinue.Size = new Size(86, 23);
    this.btnContinue.TabIndex = 0;
    this.btnContinue.Text = "Продолжить";
    this.btnContinue.UseVisualStyleBackColor = true;
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point(386, 100);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(86, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listView.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeaderVersionId,
      this.columnHeaderCaption
    });
    this.listView.FullRowSelect = true;
    this.listView.HideSelection = false;
    this.listView.Location = new Point(12, 11);
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Size = new Size(459, 80 /*0x50*/);
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.TabIndex = 2;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.columnHeaderVersionId.Text = "Идентификатор версии";
    this.columnHeaderVersionId.Width = 80 /*0x50*/;
    this.columnHeaderCaption.Text = "Заголовок";
    this.columnHeaderCaption.Width = 452;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(484, 135);
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnContinue);
    this.MinimumSize = new Size(500, 175);
    this.Name = nameof (AuthFilesAskListForm);
    this.Text = "Объекты без актуальных аутентичных файлов";
    this.FormClosed += new FormClosedEventHandler(this.AuthFilesAskListForm_FormClosed);
    this.Load += new EventHandler(this.AuthFilesAskListForm_Load);
    this.ResumeLayout(false);
  }
}
