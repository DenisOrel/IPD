
// Type: Intermech.Security.RightConditionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Security;

public class RightConditionForm : Form
{
  private RightConditionList rcl = new RightConditionList();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnCancel;
  private Button btnOk;
  private ListBox lbConditions;

  public RightConditionForm() => this.InitializeComponent();

  public DialogResult Execute(ref object condition, bool aReadonly)
  {
    this.btnOk.Enabled = !aReadonly;
    this.rcl.Initialize();
    this.FillList();
    this.SetSelectedItem(condition);
    int num = (int) this.ShowDialog();
    if (num != 1)
      return (DialogResult) num;
    condition = (object) ((RightConditionClass) this.lbConditions.SelectedItem).Value;
    return (DialogResult) num;
  }

  private void SetSelectedItem(object condition)
  {
    long num = 0;
    if (condition != null)
    {
      if (condition != DBNull.Value)
      {
        try
        {
          num = Convert.ToInt64(condition);
        }
        catch
        {
        }
      }
    }
    this.lbConditions.SelectedItem = (object) null;
    for (int index = 0; index < this.lbConditions.Items.Count; ++index)
    {
      if (((RightConditionClass) this.lbConditions.Items[index]).Value == num)
      {
        this.lbConditions.SelectedItem = this.lbConditions.Items[index];
        break;
      }
    }
  }

  private void FillList()
  {
    this.lbConditions.Items.Clear();
    this.lbConditions.Items.AddRange((object[]) this.rcl.ToArray());
  }

  private void btnOk_Click(object sender, EventArgs e)
  {
    if (this.lbConditions.SelectedItem == null)
      return;
    this.DialogResult = DialogResult.OK;
  }

  private void RightConditionForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void RightConditionForm_FormClosed(object sender, FormClosedEventArgs e)
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
    this.btnCancel = new Button();
    this.btnOk = new Button();
    this.lbConditions = new ListBox();
    this.SuspendLayout();
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.ImeMode = ImeMode.NoControl;
    this.btnCancel.Location = new Point(257, 182);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(121, 27);
    this.btnCancel.TabIndex = 6;
    this.btnCancel.Text = "Отмена";
    this.btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOk.ImeMode = ImeMode.NoControl;
    this.btnOk.Location = new Point(130, 182);
    this.btnOk.Name = "btnOk";
    this.btnOk.Size = new Size(121, 27);
    this.btnOk.TabIndex = 5;
    this.btnOk.Text = "OK";
    this.btnOk.Click += new EventHandler(this.btnOk_Click);
    this.lbConditions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lbConditions.FormattingEnabled = true;
    this.lbConditions.Location = new Point(12, 12);
    this.lbConditions.Name = "lbConditions";
    this.lbConditions.Size = new Size(366, 160 /*0xA0*/);
    this.lbConditions.TabIndex = 8;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(390, 221);
    this.Controls.Add((Control) this.lbConditions);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOk);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.Name = nameof (RightConditionForm);
    this.Text = "Проверка прав доступа";
    this.FormClosed += new FormClosedEventHandler(this.RightConditionForm_FormClosed);
    this.Load += new EventHandler(this.RightConditionForm_Load);
    this.ResumeLayout(false);
  }
}
