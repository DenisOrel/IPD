// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Victor.ChangeDocumentType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Victor;

public class ChangeDocumentType : Form
{
  public string _designationDocCurr;
  public string _designationDocNew;
  public string _typeVedCurr;
  public string _typeVedNew;
  private bool noClosing;
  public bool quietMode;
  private bool isCreate;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelButtons;
  private Button bCancel;
  private Button bOK;
  private GroupBox groupBoxCurr;
  private GroupBox groupBoxNew;
  public TextBox textBoxDesignationCurr;
  private Label label3;
  private Label label_typeVedCurr;
  public TextBox textBoxDesignationNew;
  private Label label4;
  private Label label_typeVedNew;
  private GroupBox groupBox_TypeChange;
  private ToolTip toolTip1;
  public RadioButton radioButton_NewDoc;
  public RadioButton radioButton_CurrentDoc;
  private Label label_warning;

  public ChangeDocumentType() => this.InitializeComponent();

  private void ChangeDocumentType_Load(object sender, EventArgs e)
  {
    this.quietMode = true;
    this.isCreate = true;
    this.textBoxDesignationCurr.Text = this._designationDocCurr;
    this._designationDocNew = this._designationDocCurr;
    this.textBoxDesignationNew.Text = this._designationDocNew;
    this.label_typeVedCurr.Text = this._typeVedCurr;
    this.label_typeVedNew.Text = this._typeVedNew;
    this.isCreate = false;
  }

  /// <summary> Переписывать существующий документ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void radioButton_CurrentDoc_Click(object sender, EventArgs e)
  {
    this.label4.Visible = false;
    this.textBoxDesignationNew.Visible = false;
    this.label_warning.Visible = false;
  }

  /// <summary> Создавать НОВЫЙ документ </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void radioButton_NewDoc_Click(object sender, EventArgs e)
  {
    this.label4.Visible = true;
    this.textBoxDesignationNew.Visible = true;
    this.label_warning.Visible = true;
  }

  /// <summary> Кнопка OK </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void bOK_Click(object sender, EventArgs e)
  {
    this.noClosing = false;
    if (this.radioButton_NewDoc.Checked && MessageBox.Show("Режим \"Создать новый документ\",\r\n\r\nно Вы не изменили Обозначение\r\n\r\nПродолжать изменение типа документа?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
    {
      this.noClosing = true;
    }
    else
    {
      string designation = !this.radioButton_NewDoc.Checked ? this.textBoxDesignationCurr.Text : this.textBoxDesignationNew.Text;
      if (!this.ControlExistingDoc(designation))
        return;
      if (MessageBox.Show($"Документ\r\n\r\n{designation}\r\n\r\nуже существует!\r\nЗаменить его?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        this.noClosing = false;
        this.quietMode = false;
      }
      else
        this.noClosing = true;
    }
  }

  /// <summary> При попыке закрыть окно </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ChangeDocumentType_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.noClosing)
      return;
    e.Cancel = true;
    this.noClosing = false;
  }

  /// <summary> Контроль существования документа с ЭТИМ обозначением </summary>
  /// <param name="designation"></param>
  /// <returns></returns>
  private bool ControlExistingDoc(string designation)
  {
    if (string.IsNullOrEmpty(designation))
      return false;
    using (new SessionKeeper())
      return !(ServicesManager.GetService(typeof (IPDMSpecificationsService)) as IPDMSpecificationsService).GetObjectWithDesignation(AvsIDCache.ObjType_Document, designation).IsUndefinedId();
  }

  /// <summary> Изменился textBox </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void textBoxDesignationNew_TextChanged(object sender, EventArgs e)
  {
    if (this.isCreate)
      return;
    this.label_warning.Visible = false;
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.panelButtons = new Panel();
    this.bCancel = new Button();
    this.bOK = new Button();
    this.groupBoxCurr = new GroupBox();
    this.textBoxDesignationCurr = new TextBox();
    this.label3 = new Label();
    this.label_typeVedCurr = new Label();
    this.groupBoxNew = new GroupBox();
    this.textBoxDesignationNew = new TextBox();
    this.label4 = new Label();
    this.label_typeVedNew = new Label();
    this.groupBox_TypeChange = new GroupBox();
    this.radioButton_NewDoc = new RadioButton();
    this.radioButton_CurrentDoc = new RadioButton();
    this.toolTip1 = new ToolTip(this.components);
    this.label_warning = new Label();
    this.panelButtons.SuspendLayout();
    this.groupBoxCurr.SuspendLayout();
    this.groupBoxNew.SuspendLayout();
    this.groupBox_TypeChange.SuspendLayout();
    this.SuspendLayout();
    this.panelButtons.BorderStyle = BorderStyle.Fixed3D;
    this.panelButtons.Controls.Add((Control) this.bCancel);
    this.panelButtons.Controls.Add((Control) this.bOK);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(0, 350);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(559, 42);
    this.panelButtons.TabIndex = 3;
    this.bCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bCancel.DialogResult = DialogResult.Cancel;
    this.bCancel.Location = new Point(394, 8);
    this.bCancel.Name = "bCancel";
    this.bCancel.Size = new Size(121, 27);
    this.bCancel.TabIndex = 3;
    this.bCancel.Text = "Отмена";
    this.bCancel.UseVisualStyleBackColor = true;
    this.bOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bOK.DialogResult = DialogResult.OK;
    this.bOK.Location = new Point(254, 8);
    this.bOK.Name = "bOK";
    this.bOK.Size = new Size(121, 27);
    this.bOK.TabIndex = 2;
    this.bOK.Text = "OK";
    this.bOK.UseVisualStyleBackColor = true;
    this.bOK.Click += new EventHandler(this.bOK_Click);
    this.groupBoxCurr.Controls.Add((Control) this.textBoxDesignationCurr);
    this.groupBoxCurr.Controls.Add((Control) this.label3);
    this.groupBoxCurr.Controls.Add((Control) this.label_typeVedCurr);
    this.groupBoxCurr.Location = new Point(22, 12);
    this.groupBoxCurr.Name = "groupBoxCurr";
    this.groupBoxCurr.Size = new Size(495, 100);
    this.groupBoxCurr.TabIndex = 4;
    this.groupBoxCurr.TabStop = false;
    this.groupBoxCurr.Text = "Данные текущего документа";
    this.textBoxDesignationCurr.Enabled = false;
    this.textBoxDesignationCurr.Location = new Point(117, 57);
    this.textBoxDesignationCurr.Name = "textBoxDesignationCurr";
    this.textBoxDesignationCurr.Size = new Size(360, 20);
    this.textBoxDesignationCurr.TabIndex = 2;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(15, 57);
    this.label3.Name = "label3";
    this.label3.Size = new Size(74, 13);
    this.label3.TabIndex = 1;
    this.label3.Text = "Обозначение";
    this.label_typeVedCurr.AutoSize = true;
    this.label_typeVedCurr.Location = new Point(15, 27);
    this.label_typeVedCurr.Name = "label_typeVedCurr";
    this.label_typeVedCurr.Size = new Size(83, 13);
    this.label_typeVedCurr.TabIndex = 0;
    this.label_typeVedCurr.Text = "Тип документа";
    this.groupBoxNew.Controls.Add((Control) this.label_warning);
    this.groupBoxNew.Controls.Add((Control) this.textBoxDesignationNew);
    this.groupBoxNew.Controls.Add((Control) this.label4);
    this.groupBoxNew.Controls.Add((Control) this.label_typeVedNew);
    this.groupBoxNew.Location = new Point(22, 221);
    this.groupBoxNew.Name = "groupBoxNew";
    this.groupBoxNew.Size = new Size(495, 100);
    this.groupBoxNew.TabIndex = 5;
    this.groupBoxNew.TabStop = false;
    this.groupBoxNew.Text = "Данные документа после изменения его типа";
    this.textBoxDesignationNew.Location = new Point(117, 67);
    this.textBoxDesignationNew.Name = "textBoxDesignationNew";
    this.textBoxDesignationNew.Size = new Size(360, 20);
    this.textBoxDesignationNew.TabIndex = 3;
    this.textBoxDesignationNew.TextChanged += new EventHandler(this.textBoxDesignationNew_TextChanged);
    this.label4.AutoSize = true;
    this.label4.Location = new Point(15, 67);
    this.label4.Name = "label4";
    this.label4.Size = new Size(74, 13);
    this.label4.TabIndex = 2;
    this.label4.Text = "Обозначение";
    this.label_typeVedNew.AutoSize = true;
    this.label_typeVedNew.Location = new Point(15, 30);
    this.label_typeVedNew.Name = "label_typeVedNew";
    this.label_typeVedNew.Size = new Size(83, 13);
    this.label_typeVedNew.TabIndex = 1;
    this.label_typeVedNew.Text = "Тип документа";
    this.groupBox_TypeChange.Controls.Add((Control) this.radioButton_NewDoc);
    this.groupBox_TypeChange.Controls.Add((Control) this.radioButton_CurrentDoc);
    this.groupBox_TypeChange.Location = new Point(22, 128 /*0x80*/);
    this.groupBox_TypeChange.Name = "groupBox_TypeChange";
    this.groupBox_TypeChange.Size = new Size(495, 75);
    this.groupBox_TypeChange.TabIndex = 16 /*0x10*/;
    this.groupBox_TypeChange.TabStop = false;
    this.groupBox_TypeChange.Text = "Порядок изменения";
    this.radioButton_NewDoc.AutoSize = true;
    this.radioButton_NewDoc.Checked = true;
    this.radioButton_NewDoc.Location = new Point(6, 43);
    this.radioButton_NewDoc.Name = "radioButton_NewDoc";
    this.radioButton_NewDoc.Size = new Size(153, 17);
    this.radioButton_NewDoc.TabIndex = 1;
    this.radioButton_NewDoc.TabStop = true;
    this.radioButton_NewDoc.Text = "Создать новый документ";
    this.toolTip1.SetToolTip((Control) this.radioButton_NewDoc, "Будет созан новый документ");
    this.radioButton_NewDoc.UseVisualStyleBackColor = true;
    this.radioButton_NewDoc.Click += new EventHandler(this.radioButton_NewDoc_Click);
    this.radioButton_CurrentDoc.AutoSize = true;
    this.radioButton_CurrentDoc.Location = new Point(6, 19);
    this.radioButton_CurrentDoc.Name = "radioButton_CurrentDoc";
    this.radioButton_CurrentDoc.Size = new Size(196, 17);
    this.radioButton_CurrentDoc.TabIndex = 0;
    this.radioButton_CurrentDoc.Text = "Сохранить как текущий документ";
    this.toolTip1.SetToolTip((Control) this.radioButton_CurrentDoc, "Новый документ будет сохранен вместо текущего документа");
    this.radioButton_CurrentDoc.UseVisualStyleBackColor = true;
    this.radioButton_CurrentDoc.Click += new EventHandler(this.radioButton_CurrentDoc_Click);
    this.label_warning.AutoSize = true;
    this.label_warning.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this.label_warning.ForeColor = Color.Coral;
    this.label_warning.Location = new Point(120, 47);
    this.label_warning.Name = "label_warning";
    this.label_warning.Size = new Size(213, 13);
    this.label_warning.TabIndex = 4;
    this.label_warning.Text = "Измените обозначение документа";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.bCancel;
    this.ClientSize = new Size(559, 392);
    this.Controls.Add((Control) this.groupBox_TypeChange);
    this.Controls.Add((Control) this.groupBoxNew);
    this.Controls.Add((Control) this.groupBoxCurr);
    this.Controls.Add((Control) this.panelButtons);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ChangeDocumentType);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Изменение типа документа";
    this.FormClosing += new FormClosingEventHandler(this.ChangeDocumentType_FormClosing);
    this.Load += new EventHandler(this.ChangeDocumentType_Load);
    this.panelButtons.ResumeLayout(false);
    this.groupBoxCurr.ResumeLayout(false);
    this.groupBoxCurr.PerformLayout();
    this.groupBoxNew.ResumeLayout(false);
    this.groupBoxNew.PerformLayout();
    this.groupBox_TypeChange.ResumeLayout(false);
    this.groupBox_TypeChange.PerformLayout();
    this.ResumeLayout(false);
  }
}
