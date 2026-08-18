// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.SelectCompNameForm
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Security.EventLog;

public class SelectCompNameForm : Form
{
  private IContainer components;
  private Button btnOk;
  private Button btnCancel;
  private ListView listView;
  private ColumnHeader chName;

  public string SelectedName { get; set; }

  public SelectCompNameForm() => this.InitializeComponent();

  public DialogResult ShowDialog(List<string> names)
  {
    this.InitListView(names, this.SelectedName);
    return this.ShowDialog();
  }

  private void InitListView(List<string> names, string selName)
  {
    ListViewItem listViewItem1 = (ListViewItem) null;
    this.listView.Items.Clear();
    this.listView.BeginUpdate();
    try
    {
      for (int index = 0; index < names.Count; ++index)
      {
        ListViewItem listViewItem2 = this.listView.Items.Add(names[index]);
        if (names[index].Equals(selName, StringComparison.InvariantCultureIgnoreCase))
          listViewItem1 = listViewItem2;
      }
    }
    finally
    {
      this.listView.EndUpdate();
    }
    if (listViewItem1 == null)
      return;
    listViewItem1.Selected = true;
    this.listView.TopItem = listViewItem1;
  }

  private void btnOk_Click(object sender, EventArgs e) => this.BtnOkPress();

  private void BtnOkPress()
  {
    if (this.listView.SelectedItems.Count == 0)
      this.DialogResult = DialogResult.None;
    else
      this.SelectedName = this.listView.SelectedItems[0].Text;
  }

  private void SelectCompNameForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
    this.listView.Select();
  }

  private void SelectCompNameForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void listView_DoubleClick(object sender, EventArgs e)
  {
    if (this.listView.SelectedItems.Count <= 0)
      return;
    this.BtnOkPress();
    this.DialogResult = DialogResult.OK;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btnOk = new Button();
    this.btnCancel = new Button();
    this.listView = new ListView();
    this.chName = new ColumnHeader();
    this.SuspendLayout();
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.DialogResult = DialogResult.OK;
    this.btnOk.Location = new Point(174, 339);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(75, 23);
    this.btnOk.TabIndex = 0;
    this.btnOk.Text = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Location = new Point((int) byte.MaxValue, 339);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(75, 23);
    this.btnCancel.TabIndex = 1;
    this.btnCancel.Text = "Отмена";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listView.Columns.AddRange(new ColumnHeader[1]
    {
      this.chName
    });
    this.listView.FullRowSelect = true;
    this.listView.HideSelection = false;
    this.listView.Location = new Point(12, 12);
    this.listView.MultiSelect = false;
    this.listView.Name = "listView";
    this.listView.Size = new Size(318, 321);
    this.listView.Sorting = SortOrder.Ascending;
    this.listView.TabIndex = 2;
    this.listView.UseCompatibleStateImageBehavior = false;
    this.listView.View = View.Details;
    this.listView.DoubleClick += new EventHandler(this.listView_DoubleClick);
    this.chName.Text = "Имя компьютера";
    this.chName.Width = 200;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.ClientSize = new Size(342, 374);
    this.Controls.Add((Control) this.listView);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.MinimizeBox = false;
    this.Name = nameof (SelectCompNameForm);
    this.Text = "Выбор имени компьютера";
    this.FormClosed += new FormClosedEventHandler(this.SelectCompNameForm_FormClosed);
    this.Load += new EventHandler(this.SelectCompNameForm_Load);
    this.ResumeLayout(false);
  }
}
