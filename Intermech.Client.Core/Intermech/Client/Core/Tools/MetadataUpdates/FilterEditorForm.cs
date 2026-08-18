
// Type: Intermech.Client.Core.Tools.MetadataUpdates.FilterEditorForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Tools.MetadataUpdates;

internal class FilterEditorForm : Form
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TextBox tbFilter;
  private Button bOK;
  private Button bCancel;

  public FilterEditorForm(FilterEditorFormMode mode, string filterString)
  {
    this.InitializeComponent();
    FormStorage.LoadLayout((Control) this);
    switch (mode)
    {
      case FilterEditorFormMode.Add:
        this.Text = "Новый фильтр";
        this.bOK.Text = "Добавить";
        this.OldFilterString = string.Empty;
        break;
      case FilterEditorFormMode.Edit:
        this.Text = "Редактировать фильтр";
        this.bOK.Text = "Изменить";
        this.OldFilterString = filterString;
        break;
    }
    this.tbFilter.Text = filterString;
  }

  public string FilterString => this.tbFilter.Text;

  public string OldFilterString { get; private set; }

  private void FilterEditorForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  private void RefreshButtons()
  {
    this.bOK.Enabled = !this.OldFilterString.Equals(this.tbFilter.Text);
  }

  private void tbFilter_TextChanged(object sender, EventArgs e) => this.RefreshButtons();

  private void bOK_Click(object sender, EventArgs e)
  {
    if (string.IsNullOrEmpty(this.tbFilter.Text))
    {
      int num1 = (int) MessageBox.Show("Фильтр не может быть пустым!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else if (this.tbFilter.Text.Length > Consts.MaxStringSize)
    {
      int num2 = (int) MessageBox.Show($"Максимальная длина фильтра составляет {Consts.MaxStringSize} символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    else
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
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
    this.tbFilter = new TextBox();
    this.bOK = new Button();
    this.bCancel = new Button();
    this.SuspendLayout();
    this.tbFilter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tbFilter.Location = new Point(12, 12);
    this.tbFilter.Multiline = true;
    this.tbFilter.Name = "tbFilter";
    this.tbFilter.Size = new Size(445, 102);
    this.tbFilter.TabIndex = 0;
    this.tbFilter.TextChanged += new EventHandler(this.tbFilter_TextChanged);
    this.bOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bOK.Location = new Point(209, 120);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 1;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.bCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(336, 120);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 2;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.bOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(469, 159);
    this.Controls.Add((Control) this.bCancel);
    this.Controls.Add((Control) this.bOK);
    this.Controls.Add((Control) this.tbFilter);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MinimumSize = new Size(290, 120);
    this.Name = nameof (FilterEditorForm);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Фильтр";
    this.FormClosing += new FormClosingEventHandler(this.FilterEditorForm_FormClosing);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
